using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.Dal.JXC
{
    public class TB_ToInvoiceService
    {
        public bool updateTran(TB_ToInvoice model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    model.State = eform.state;
                    objCommand.Parameters.Clear();
                    model.UpAccount = accountXishu(model, eform, objCommand);
                    Update(model, objCommand);

                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);
                    //判断是否是删除 -到款单删除
                    if (eform.proId == 38&& eform.state == "通过")
                    {
                        //及到款单删除会有两个层面，
                        //1，预付款模式，这时只需要在审批的最后一个流程总经理点确定时，删除相应的到款单；并弹出一个提示框，该预付到款单已删除，点确定，完成。
                        //2，发票到款模式，我们需要在最后一个审批流程点确定时，删除相应项目编号针对该发票号的发票签回单（如果有在审批执行中或已完成审批的），再删除该项目编号针对该发票号的到款单，
                        //并弹出一个提示框，该发票到款单已删除，点确定，完成。
                        if (model.BusType == 0)//实际发票到款
                        {
                            //删除发票签回单（如果有）
                            string deleteFPBack = string.Format("delete tb_EForms where e_Id in (select id from tb_EForm where proId=29 and allE_id in (select id from Sell_OrderFPBack where PId={0}));", model.FPId);
                            deleteFPBack += string.Format("delete tb_EForm where proId=29 and allE_id in (select id from Sell_OrderFPBack where PId={0});", model.FPId);
                            deleteFPBack += string.Format("delete Sell_OrderFPBacks  where Id in (select id from Sell_OrderFPBack where PId={0});delete Sell_OrderFPBack  where PId={0};", model.FPId);

                            objCommand.CommandText = deleteFPBack;
                            objCommand.ExecuteNonQuery();                            
                        }

                        //删除发票删除单 审批流
                        string deleteFPDelete = string.Format("delete tb_EForms where e_Id in (select id from tb_EForm where proId in (26,34,37) and allE_id={0});", model.Id);
                        deleteFPDelete += string.Format("delete tb_EForm where proId in (27,38) and allE_id={0};", model.Id);
                        objCommand.CommandText = deleteFPDelete;
                        objCommand.ExecuteNonQuery();

                        string DeleteAll = string.Format("delete from [TB_ToInvoice] where id={0};", model.Id);
                        objCommand.CommandText = DeleteAll;
                        objCommand.ExecuteNonQuery();

                    }

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

               

            }

            return true;
        }

        private decimal accountXishu(TB_ToInvoice model, VAN_OA.Model.EFrom.tb_EForm eform, SqlCommand objCommand)
        {
            if (eform.state == "通过")
            {

                string sql = string.Format(@"declare @cou int;
                                        declare @lastDate datetime;
                                        select @cou=COUNT(*) from TB_ToInvoice where FPId={2};
                                        if (@cou=0)
                                        begin  
                                        select @lastDate=DaoKuanDate from TB_ToInvoice where State='通过' and PoNo='{0}' and Id<>{1} order by DaoKuanDate
                                        end
                                        select @lastDate;", model.PoNo, model.Id, model.FPId);
                objCommand.CommandText = sql;
                object objdate = objCommand.ExecuteScalar();

                //查找第一次销售出库日期

                sql = string.Format("select  top 1 rutime from Sell_OrderOutHouse where pono='{0}' order by rutime ", model.PoNo);
                objCommand.CommandText = sql;
                DateTime sellDate = Convert.ToDateTime(objCommand.ExecuteScalar());

                DateTime fristDate = DateTime.Now;
                if (objdate is DBNull)
                {
                    fristDate = model.DaoKuanDate;
                }
                else
                {
                    fristDate = Convert.ToDateTime(objdate);
                }
                TimeSpan ts = fristDate - sellDate;
                sql = string.Format("select top 1 accountXishu from TB_AccountPeriod where  accountName<={0}  order by accountName desc", ts.Days);
                objCommand.CommandText = sql;
                decimal accountXishu = Convert.ToDecimal(objCommand.ExecuteScalar());
                return accountXishu;
            }
            return 0;
        }
        public int addTran(TB_ToInvoice model, VAN_OA.Model.EFrom.tb_EForm eform)
        {
            int id = 0;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {



                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("TB_ToInvoice", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    model.State = eform.state;
                    model.UpAccount = accountXishu(model, eform, objCommand);
                    id = Add(model, objCommand);


                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TB_ToInvoice model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("'" + model.CreateUser + "',");
            }
            if (model.AppleDate != null)
            {
                strSql1.Append("AppleDate,");
                strSql2.Append("'" + model.AppleDate + "',");
            }
            if (model.DaoKuanDate != null)
            {
                strSql1.Append("DaoKuanDate,");
                strSql2.Append("'" + model.DaoKuanDate + "',");
            }
            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }
            if (model.LastPayTotal != null)
            {
                strSql1.Append("LastPayTotal,");
                strSql2.Append("" + model.LastPayTotal + ",");
            }

            if (model.UpAccount != null)
            {
                strSql1.Append("UpAccount,");
                strSql2.Append("" + model.UpAccount + ",");
            }
            if (model.PoNo != null)
            {
                strSql1.Append("PoNo,");
                strSql2.Append("'" + model.PoNo + "',");
            }
            if (model.PoName != null)
            {
                strSql1.Append("PoName,");
                strSql2.Append("'" + model.PoName + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }

            if (model.ZhangQi != null)
            {
                strSql1.Append("ZhangQi,");
                strSql2.Append("" + model.ZhangQi + ",");
            }
            if (model.FPNo != null)
            {
                strSql1.Append("FPNo,");
                strSql2.Append("'" + model.FPNo + "',");
            }

            if (model.FPId != null)
            {
                strSql1.Append("FPId,");
                strSql2.Append("" + model.FPId + ",");
            }
            if (model.BusType != null)
            {
                strSql1.Append("BusType,");
                strSql2.Append("" + model.BusType + ",");
            }

            if (model.TempGuid != null)
            {
                strSql1.Append("TempGuid,");
                strSql2.Append("'" + model.TempGuid + "',");
            }


            strSql.Append("insert into TB_ToInvoice(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(TB_ToInvoice model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_ToInvoice set ");
            if (model.ProNo != null)
            {
                strSql.Append("ProNo='" + model.ProNo + "',");
            }
            if (model.CreateUser != null)
            {
                strSql.Append("CreateUser='" + model.CreateUser + "',");
            }
            if (model.AppleDate != null)
            {
                strSql.Append("AppleDate='" + model.AppleDate + "',");
            }
            if (model.DaoKuanDate != null)
            {
                strSql.Append("DaoKuanDate='" + model.DaoKuanDate + "',");
            }
            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            if (model.UpAccount != null)
            {
                strSql.Append("UpAccount=" + model.UpAccount + ",");
            }
            if (model.PoNo != null)
            {
                strSql.Append("PoNo='" + model.PoNo + "',");
            }
            if (model.PoName != null)
            {
                strSql.Append("PoName='" + model.PoName + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            else
            {
                strSql.Append("State= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");

            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_ToInvoice ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }




        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TB_ToInvoice GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LastPayTotal,TempGuid,Id,ProNo,CreateUser,AppleDate,DaoKuanDate,Total,UpAccount,PoNo,PoName,GuestName,Remark,State,ZhangQi,FPNo,FPId,BusType ");
            strSql.Append(" FROM TB_ToInvoice ");
            strSql.Append(" where TB_ToInvoice.id=" + id + "");
            TB_ToInvoice model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                        model.TempGuid = dataReader["TempGuid"].ToString();
                        model.LastPayTotal = Convert.ToDecimal(dataReader["LastPayTotal"]);

                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<TB_ToInvoice> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CreateUser,AppleDate,DaoKuanDate,Total,UpAccount,PoNo,PoName,GuestName,Remark,State,ZhangQi,FPNo,FPId,BusType ");
            strSql.Append(" FROM TB_ToInvoice ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc");
            List<TB_ToInvoice> list = new List<TB_ToInvoice>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        if (model.BusType == 0)
                        {
                            model.BusTypeStr = "实际发票到款";
                        }
                        else
                        {
                            model.BusTypeStr = "预付款";
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<TB_ToInvoice> GetListArray_History(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CreateUser,AppleDate,DaoKuanDate,Total,UpAccount,PoNo,PoName,GuestName,Remark,State,ZhangQi,FPNo,FPId,BusType,NewFPNo ");
            strSql.Append(" FROM TB_ToInvoice_History left join (select id as sellFP_Id,FPNo as NewFPNo  from Sell_OrderFP) as FP  on FP.sellFP_Id=TB_ToInvoice_History.fpid");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc");
            List<TB_ToInvoice> list = new List<TB_ToInvoice>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);

                        if (model.BusType == 0)
                        {
                            model.BusTypeStr = "实际发票到款";
                        }
                        else
                        {
                            model.BusTypeStr = "预付款";
                        }
                        var ojb = dataReader["NewFPNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NewFPNo = (string)ojb;
                        }
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<TB_ToInvoice> GetListArrayReport(string strWhere, string strWhere2, string isColse)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select FPDate,sumTotal,sumFPTotal,CG_POOrder.AE,CG_POOrder.IsPoFax,newtable1.PONo,newtable1.POTotal-isnull(TuiTotal,0) as POTotal,hadFpTotal,minOutTime,CG_POOrder.PODate as minPoDate,
TB_ToInvoice.Id,TB_ToInvoice.ProNo,DaoKuanDate,Total,UpAccount,newtable1.PoName,newtable1.GuestName,State,FPNo,FPId,BusType,CreateUser,AppleDate,Remark,newtable1.AppName from(
select PONo,sum(POTotal) AS POTotal,PoName,GuestName,AppName  from CG_POOrder where Status='通过' {0} {1} group by PONo,PoName,GuestName,AppName  ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal,min(ruTime) as FPDate from Sell_OrderFP where Status='通过' group by PONo) as newtable3 on newtable1.PONo= newtable3.PONo
left join( select min(RuTime) as minOutTime, PONo from Sell_OrderOutHouse group by PONo) as newtable4 on newtable1.PONo= newtable4.PONo
left join TB_ToInvoice on  newtable1.PONo= TB_ToInvoice.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0
left join (SELECT FPId AS TempFPId,sumTotal,Total AS sumFPTotal FROM (SELECT FPId,sum(Total) as sumTotal FROM TB_ToInvoice WHERE State<>'不通过' GROUP BY FPId) as newtable
 left join Sell_OrderFP on Sell_OrderFP.Id=newtable.FPId) AS TB ON TB.TempFPId=TB_ToInvoice.FPID ", strWhere2, isColse);

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by newtable1.PONo desc,DaoKuanDate desc,ProNo desc");
            List<TB_ToInvoice> list = new List<TB_ToInvoice>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        TB_ToInvoice model = new TB_ToInvoice();
                        object ojb;
                        ojb = dataReader["FPDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPDate = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = Convert.ToString(ojb);
                        }
                        ojb = dataReader["HadFpTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HadFpTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["MinOutTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinOutTime = Convert.ToDateTime(ojb);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd")));

                            model.Days = ts.Days;
                        }
                        ojb = dataReader["MinPoDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinPoDate = Convert.ToDateTime(ojb);


                        }
                        ojb = dataReader["IsPoFax"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsPoFax = Convert.ToBoolean(ojb);
                        }


                        model.PoNo = dataReader["PoNo"].ToString();
                        model.PoName = dataReader["PoName"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();

                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;


                            model.ProNo = dataReader["ProNo"].ToString();
                            model.CreateUser = dataReader["CreateUser"].ToString();
                            ojb = dataReader["AppleDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.AppleDate = (DateTime)ojb;
                            }
                            ojb = dataReader["DaoKuanDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.DaoKuanDate1 = (DateTime)ojb;
                                if (model.MinOutTime != null)
                                {
                                    TimeSpan ts = (Convert.ToDateTime(Convert.ToDateTime(ojb).ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd")));

                                    model.Days = ts.Days;
                                }

                            }
                            ojb = dataReader["Total"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.Total = (decimal)ojb;
                            }
                            ojb = dataReader["UpAccount"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.UpAccount = (decimal)ojb;
                            }


                            model.Remark = dataReader["Remark"].ToString();
                            model.State = dataReader["State"].ToString();
                            //ojb = dataReader["ZhangQi"];
                            //if (ojb != null && ojb != DBNull.Value)
                            //{
                            //    model.ZhangQi = (decimal)ojb;
                            //}
                            ojb = dataReader["FPNo"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.FPNo = ojb.ToString();
                            }

                            ojb = dataReader["FPId"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.FPId = (int)ojb;
                            }

                            ojb = dataReader["BusType"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BusType = (int)ojb;
                            }
                            if (model.BusType == 0)
                            {
                                model.BusTypeStr = "实际发票到款";
                            }
                            else
                            {
                                model.BusTypeStr = "预付款";
                            }
                        }
                        model.PONo_Id = model.PoNo + "_" + model.Id;
                        decimal sumTotal = 0;
                        ojb = dataReader["sumTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            sumTotal = (decimal)ojb;
                        }
                        decimal sumFPTotal = 0;
                        ojb = dataReader["sumFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            sumFPTotal = (decimal)ojb;
                        }

                        if (sumTotal <= sumFPTotal)
                        {
                            model.IsQuanDao = true;
                        }

                        // 开票天数                  
                        if (model.FPDate != null)
                        {
                            if (model.Total < model.POTotal)
                            {
                                // 1.到款金额<项目金额 开票天数=今天-开票日期  
                                TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.FPDate.Value.ToString("yyyy-MM-dd")));
                                model.FPDays = ts.Days;
                            }
                            else if (model.Total == model.POTotal)
                            {
                                //2.到款金额=项目金额 开票天数=最后一笔到款的日期-开票日期，开票天数用下划线 3.开票日期没有的话，开票日期和开票天数均不显示
                                TimeSpan ts = (Convert.ToDateTime(model.DaoKuanDate1) - Convert.ToDateTime(model.FPDate.Value.ToString("yyyy-MM-dd")));
                                model.FPDays = ts.Days;
                            }
                        }
                        //增加一列 
                        if (model.MinOutTime != null)
                        {
                            if (model.FPDate != null)
                            {
                                //如该项目某天开出发票了，未开票天数=（某天-该项目第一笔的出库日期）-1
                                TimeSpan ts = model.FPDate.Value.Date - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd"));
                                model.WeiFPDays = ts.Days;
                            }
                            else
                            {
                                //未开票天数=没有开过票的项目的 今天-该项目第一笔的出库日期；
                                TimeSpan ts = DateTime.Now.Date - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd"));
                                model.WeiFPDays = ts.Days;
                            }
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<TB_ToInvoice> GetListArrayReport_HeBing(string strWhere, string strWhere2, string strWhere3, string fpTotal, string isColse)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select Model,FPDate,CG_POOrder.AE,MaxDaoKuanDate,MinDaoKuanDate,CG_POOrder.IsPoFax,minProNo,minOutTime,FPTotal,newtable1.PONo,newtable1.POTotal-isnull(TuiTotal,0) as POTotal,hadFpTotal,CG_POOrder.PODate as minPoDate,Total,newtable1.PoName,newtable1.GuestName from(
select PONo,sum(POTotal) AS POTotal,PoName,GuestName from CG_POOrder where Status='通过' {0} {2} {3} group by PONo,PoName,GuestName ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal,min(ruTime) as FPDate from Sell_OrderFP where Status='通过' group by PONo) as newtable3 on newtable1.PONo= newtable3.PONo
left join (select pono ,sum(Total) as Total,Min(ProNo) as minProNo,min(DaoKuanDate) as MinDaoKuanDate from  TB_ToInvoice where {1} group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join( select min(RuTime) as minOutTime, PONo from Sell_OrderOutHouse group by PONo) as newtable5 on newtable1.PONo= newtable5.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
left join (select pono ,max(DaoKuanDate) as MaxDaoKuanDate from  TB_ToInvoice where State='通过' group by PONo )as newtable6 on newtable1.PONo= newtable6.PONo ", strWhere3, strWhere, fpTotal, isColse);

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere2);
            }
            strSql.Append(" order by newtable1.PONo desc");
            List<TB_ToInvoice> list = new List<TB_ToInvoice>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        TB_ToInvoice model = new TB_ToInvoice();
                        object ojb;
                        ojb = dataReader["Model"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Model = Convert.ToString(ojb);
                        }

                        ojb = dataReader["FPDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPDate = Convert.ToDateTime(ojb);
                        }

                        ojb = dataReader["IsPoFax"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsPoFax = Convert.ToBoolean(ojb);
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = Convert.ToString(ojb);
                        }
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["HadFpTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HadFpTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["MinPoDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinPoDate = Convert.ToDateTime(ojb);
                        }
                        model.PoNo = dataReader["PoNo"].ToString();
                        model.PoName = dataReader["PoName"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Total = (decimal)ojb;
                        }
                        ojb = dataReader["MinOutTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinOutTime = Convert.ToDateTime(ojb);
                            //TimeSpan ts=(DateTime.Now - model.MinOutTime.Value);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd")));

                            model.Days = ts.Days;


                        }
                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        if (model.POTotal == 0)
                        {
                            model.Days = 0;
                        }

                        ojb = dataReader["FPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPNo = ojb.ToString();
                        }
                        model.BusTypeStr = "发票";
                        model.PONo_Id = model.PoNo + "_" + model.Id;
                        ojb = dataReader["MinDaoKuanDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.DaoKuanDate1 = (DateTime)ojb;
                        }
                        ojb = dataReader["MinProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }
                        ojb = dataReader["MaxDaoKuanDate"];
                        if (ojb != null && ojb != DBNull.Value && model.MinOutTime.HasValue && model.POTotal != 0 && model.POTotal <= model.Total)
                        {
                            TimeSpan ts = (Convert.ToDateTime(Convert.ToDateTime(ojb).ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd")));

                            model.Days = ts.Days;

                            //model.Days = Convert.ToDateTime(ojb).Subtract(model.MinOutTime.Value).Days; 
                        }
                        if (model.POTotal <= model.Total)
                        {
                            model.IsQuanDao = true;
                        }
                        // 开票天数                  
                        if (model.FPDate != null)
                        {
                            if (model.Total < model.POTotal)
                            {
                                // 1.到款金额<项目金额 开票天数=今天-开票日期  
                                TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(model.FPDate.Value.ToString("yyyy-MM-dd")));
                                model.FPDays = ts.Days;
                            }
                            else if (model.Total == model.POTotal)
                            {
                                //2.到款金额=项目金额 开票天数=最后一笔到款的日期-开票日期，开票天数用下划线 3.开票日期没有的话，开票日期和开票天数均不显示
                                TimeSpan ts = (Convert.ToDateTime(model.DaoKuanDate1) - Convert.ToDateTime(model.FPDate.Value.ToString("yyyy-MM-dd")));
                                model.FPDays = ts.Days;
                            }
                        }
                        //增加一列 
                        if (model.MinOutTime != null)
                        {
                            if (model.FPDate != null)
                            {
                                //如该项目某天开出发票了，未开票天数=（某天-该项目第一笔的出库日期）-1
                                TimeSpan ts = model.FPDate.Value.Date - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd"));
                                model.WeiFPDays = ts.Days;
                            }
                            else
                            {
                                //未开票天数=没有开过票的项目的 今天-该项目第一笔的出库日期；
                                TimeSpan ts = DateTime.Now.Date - Convert.ToDateTime(model.MinOutTime.Value.ToString("yyyy-MM-dd"));
                                model.WeiFPDays = ts.Days;
                            }
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public TB_ToInvoice ReaderBind(IDataReader dataReader)
        {
            TB_ToInvoice model = new TB_ToInvoice();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.CreateUser = dataReader["CreateUser"].ToString();
            ojb = dataReader["AppleDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppleDate = (DateTime)ojb;
            }
            ojb = dataReader["DaoKuanDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DaoKuanDate = (DateTime)ojb;
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            ojb = dataReader["UpAccount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpAccount = (decimal)ojb;
            }
            model.PoNo = dataReader["PoNo"].ToString();
            model.PoName = dataReader["PoName"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.State = dataReader["State"].ToString();
            ojb = dataReader["ZhangQi"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ZhangQi = (decimal)ojb;
            }
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }

            ojb = dataReader["FPId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPId = (int)ojb;
            }

            ojb = dataReader["BusType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusType = (int)ojb;
            }
            return model;
        }

        /// <summary>
        /// 获取预付款结转
        /// </summary>
        /// <param name="pono"></param>
        /// <returns></returns>
        public decimal GetPayTotal(string pono, decimal total)
        {
            string sql = string.Format("select isnull(sum(Total),0) from TB_ToInvoice where BusType=1 and PoNo='{0}' and State='通过'", pono);
            var payTotal = Convert.ToDecimal(DBHelp.ExeScalar(sql));
            if (payTotal > 0)
            {
                if (payTotal <= total)
                {
                    return payTotal;
                }
                return total;
            }
            return payTotal;
        }

        /// <summary>
        /// 预付款结转 生成 实际到款单
        /// </summary>
        /// <returns></returns>
        public bool YuPay_CreateInvoice(Sell_OrderFP model, decimal PayTotal)
        {
            DateTime daoKuanDate = Convert.ToDateTime("1900-1-1");
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                objCommand.CommandText = string.Format("select isnull(sum(Total),0) from TB_ToInvoice where BusType=1 and PoNo='{0}' and State='通过'", model.PONo);
                var result_payTotal = Convert.ToDecimal(objCommand.ExecuteScalar());



                objCommand.CommandText = string.Format("select isnull(max(DaoKuanDate),getdate()) from TB_ToInvoice where BusType=1 and PoNo='{0}' and State='通过'", model.PONo);
                daoKuanDate = Convert.ToDateTime(objCommand.ExecuteScalar());

                if (PayTotal < model.Total || (PayTotal == model.Total && result_payTotal == PayTotal))
                {
                    //所有预付款记录的剩余预付款字段金额更新 为0
                    objCommand.CommandText = string.Format("update TB_ToInvoice set Total=0 where BusType=1 and PoNo='{0}' and State='通过' ", model.PONo);
                    objCommand.ExecuteNonQuery();
                }
                if (PayTotal == model.Total && result_payTotal > PayTotal)
                {
                    //提交审批通过后 第一条预付款记录的剩余预付款字段金额更新 为 （原所有预付款记录的剩余金额合计值-此发票金额），
                    objCommand.CommandText = string.Format("select id,Total,DaoKuanDate from TB_ToInvoice where BusType=1 and PoNo='{0}'  and State='通过' order by AppleDate ", model.PONo);

                    List<TB_ToInvoice> invoList = new List<TB_ToInvoice>();
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            TB_ToInvoice invM = new TB_ToInvoice();
                            invM.Id = (int)dataReader["id"];
                            invM.Total = Convert.ToDecimal(dataReader["Total"]);

                            try
                            {
                                invM.DaoKuanDate = Convert.ToDateTime(dataReader["DaoKuanDate"]);
                            }
                            catch (Exception)
                            {

                            }
                            invoList.Add(invM);
                            if (invoList.Sum(t => t.Total) > PayTotal)
                            {
                                break;
                            }
                        }
                    }
                    decimal sum = 0;
                    for (int i = 0; i < invoList.Count; i++)
                    {
                        if (i == invoList.Count - 1)
                        {
                            objCommand.CommandText = string.Format("update TB_ToInvoice set Total=Total-{1} where id={0} ",
                                invoList[i].Id, (PayTotal - sum));
                            objCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            objCommand.CommandText = string.Format("update TB_ToInvoice set Total=0 where id={0} ", invoList[i].Id);
                            objCommand.ExecuteNonQuery();
                            sum += invoList[i].Total;
                        }
                    }
                }
                tan.Commit();
            }
            //生成预付款单
            TB_ToInvoice toInvoic_model = new TB_ToInvoice();
            toInvoic_model.AppleDate = DateTime.Now;
            toInvoic_model.CreateUser = "admin";
            toInvoic_model.DaoKuanDate = daoKuanDate;
            toInvoic_model.GuestName = model.GuestName;
            toInvoic_model.PoName = model.POName;
            toInvoic_model.PoNo = model.PONo;
            toInvoic_model.Total = PayTotal;
            toInvoic_model.UpAccount = 0;
            toInvoic_model.FPNo = model.FPNo;
            string sql = string.Format("select top 1 guestDays from TB_GuestTrack where guestName='{0}'", model.GuestName);
            object ob = DBHelp.ExeScalar(sql);
            toInvoic_model.ZhangQi = ob is DBNull ? 0 : Convert.ToDecimal(ob);
            toInvoic_model.FPId = model.Id;
            toInvoic_model.BusType = 0;
            toInvoic_model.State = "通过";
            toInvoic_model.Remark = "";

            VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();
            eform.appPer = 1;
            eform.appTime = DateTime.Now;
            eform.createPer = 1;
            eform.createTime = DateTime.Now;
            eform.proId = 27;
            eform.state = "通过";
            eform.toPer = 0;
            eform.toProsId = 0;
            if (addTran(toInvoic_model, eform) > 0)
            {
                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(toInvoic_model.PoNo);
            }
            return true;

        }

    }
}
