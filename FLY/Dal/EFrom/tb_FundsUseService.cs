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


namespace VAN_OA.Dal.EFrom
{
    public class tb_FundsUseService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_FundsUse model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
                    Update(model, objCommand);


                    tb_EFormService eformSer = new tb_EFormService();

                    eformSer.Update(eform, objCommand);


                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);


                    if (eform.state == "通过")
                    {

                        string[] nos = model.HouseNo.Split('/');
                        if (nos.Length > 0)
                        {
                            string sql = string.Format("update CAI_OrderInHouse set FPNo='{0}' where ProNo in (",model.Invoce);
                            foreach (var no in nos)
                            {
                                sql += string.Format(" '{0}',", no);
                            }
                            sql = sql.Substring(0, sql.Length - 1);
                            sql += ")";

                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();

                            foreach (var no in nos)
                            {
                                if (no == "")
                                    continue;
                                string insertRu = string.Format("insert into TB_CaiXiaoNo values ('入库','{0}')",no);
                                objCommand.CommandText = insertRu;
                                objCommand.ExecuteNonQuery();
                            }
                        }

                        string[] chuNo = model.ExpNo.Split('/');
                        foreach (var no in chuNo)
                        {
                            if (no == "")
                                continue;
                            string insertRu = string.Format("insert into TB_CaiXiaoNo values ('出库','{0}')", no);
                            objCommand.CommandText = insertRu;
                            objCommand.ExecuteNonQuery();
                        }
                       
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
        public int addTran(VAN_OA.Model.EFrom.tb_FundsUse model, VAN_OA.Model.EFrom.tb_EForm eform, out int MainId)
        {
            int id = 0;
            MainId = 0;
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
                    string proNo = eformSer.GetAllE_No("tb_FundsUse", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    model.State = eform.state;
                    id = Add(model, objCommand);
                    MainId = id;
                  
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    if (eform.state == "通过")
                    {

                        string[] nos = model.HouseNo.Split('/');
                        if (nos.Length > 0)
                        {
                            string sql = string.Format("update CAI_OrderInHouse set FPNo='{0}' where ProNo in (",model.Invoce);
                            foreach (var no in nos)
                            {
                                sql += string.Format(" {0}", no);
                            }
                            sql += ")";

                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }
                    }
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
        public int Add(VAN_OA.Model.EFrom.tb_FundsUse model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.appUserId != null)
            {
                strSql1.Append("appUserId,");
                strSql2.Append("" + model.appUserId + ",");
            }
            if (model.datetiem != null)
            {
                strSql1.Append("datetiem,");
                strSql2.Append("'" + model.datetiem + "',");
            }
            if (model.useTo != null)
            {
                strSql1.Append("useTo,");
                strSql2.Append("'" + model.useTo + "',");
            }
            if (model.type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.type + "',");
            }
            if (model.total != null)
            {
                strSql1.Append("total,");
                strSql2.Append("" + model.total + ",");
            }


            if (model.Invoce != null)
            {
                strSql1.Append("Invoce,");
                strSql2.Append("'" + model.Invoce + "',");
            }
            if (model.HouseNo != null)
            {
                strSql1.Append("HouseNo,");
                strSql2.Append("'" + model.HouseNo + "',");
            }
            if (model.Idea != null)
            {
                strSql1.Append("Idea,");
                strSql2.Append("'" + model.Idea + "',");
            }
            if (model.fileName != null)
            {
                strSql1.Append("fileName,");
                strSql2.Append("'" + model.fileName + "',");
            }
            if (model.fileNo != null)
            {
                strSql1.Append("fileNo,");
                strSql2.Append("@fileNo,");

                SqlParameter pp = new System.Data.SqlClient.SqlParameter("@fileNo", System.Data.SqlDbType.Image, model.fileNo.Length);
                pp.Value = model.fileNo;
                objCommand.Parameters.Add(pp);
            }
            if (model.fileType != null)
            {
                strSql1.Append("fileType,");
                strSql2.Append("'" + model.fileType + "',");
            }

            if (model.ExpNo != null)
            {
                strSql1.Append("ExpNo,");
                strSql2.Append("'" + model.ExpNo + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }


            if (model.PONo != null)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            if (model.POName != null)
            {
                strSql1.Append("POName,");
                strSql2.Append("'" + model.POName + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.CaiTotal != null)
            {
                strSql1.Append("CaiTotal,");
                strSql2.Append("" + model.CaiTotal + ",");
            }
            if (model.CAIXS != null)
            {
                strSql1.Append("CAIXS,");
                strSql2.Append("" + model.CAIXS + ",");
            }
            if (model.Hui != null)
            {
                strSql1.Append("Hui,");
                strSql2.Append("" + model.Hui + ",");
            }
            if (model.HuiXS != null)
            {
                strSql1.Append("HuiXS,");
                strSql2.Append("" + model.HuiXS + ",");
            }
            if (model.HuiTotal != null)
            {
                strSql1.Append("HuiTotal,");
                strSql2.Append("" + model.HuiTotal + ",");
            }
            if (model.Ren != null)
            {
                strSql1.Append("Ren,");
                strSql2.Append("" + model.Ren + ",");
            }
            if (model.RenXS != null)
            {
                strSql1.Append("RenXS,");
                strSql2.Append("" + model.RenXS + ",");
            }
            if (model.RenTotal != null)
            {
                strSql1.Append("RenTotal,");
                strSql2.Append("" + model.RenTotal + ",");
            }
            if (model.VAT != null)
            {
                strSql1.Append("VAT,");
                strSql2.Append("'" + model.VAT + "',");
            } 
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }

            if (model.XingCai != null)
            {
                strSql1.Append("XingCai,");
                strSql2.Append("" + model.XingCai + ",");
            }
            if (model.XingCaiXS != null)
            {
                strSql1.Append("XingCaiXS,");
                strSql2.Append("" + model.XingCaiXS + ",");
            }
            if (model.XingCaiTotal != null)
            {
                strSql1.Append("XingCaiTotal,");
                strSql2.Append("" + model.XingCaiTotal + ",");
            }
            if (model.FundType != null)
            {
                strSql1.Append("FundType,");
                strSql2.Append("'" + model.FundType + "',");
            }
            if (model.ShuoMing != null)
            {
                strSql1.Append("ShuoMing,");
                strSql2.Append("'" + model.ShuoMing + "',");
            }
            if (model.MyTeamLever != null)
            {
                strSql1.Append("MyTeamLever,");
                strSql2.Append("'" + model.MyTeamLever + "',");
            }
            
            strSql.Append("insert into tb_FundsUse(");
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
        public void Update(VAN_OA.Model.EFrom.tb_FundsUse model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_FundsUse set ");
            //if (model.appUserId != null)
            //{
            //    strSql.Append("appUserId=" + model.appUserId + ",");
            //}
            //if (model.datetiem != null)
            //{
            //    strSql.Append("datetiem='" + model.datetiem + "',");
            //}
            if (model.useTo != null)
            {
                strSql.Append("useTo='" + model.useTo + "',");
            }
            else
            {
                strSql.Append("useTo= null ,");
            }
            if (model.type != null)
            {
                strSql.Append("type='" + model.type + "',");
            }
            if (model.total != null)
            {
                strSql.Append("total=" + model.total + ",");
            }


            if (model.Invoce != null)
            {
                strSql.Append("Invoce='" + model.Invoce + "',");
            }
            else
            {
                strSql.Append("Invoce= null ,");
            }
            if (model.HouseNo != null)
            {
                strSql.Append("HouseNo='" + model.HouseNo + "',");
            }
            else
            {
                strSql.Append("HouseNo= null ,");
            }
            if (model.Idea != null)
            {
                strSql.Append("Idea='" + model.Idea + "',");
            }
            else
            {
                strSql.Append("Idea= null ,");
            }

            if (model.ExpNo != null)
            {
                strSql.Append("ExpNo='" + model.ExpNo + "',");
            }
            else
            {
                strSql.Append("ExpNo= null ,");
            }

            if (model.CaiTotal != null)
            {
                strSql.Append("CaiTotal=" + model.CaiTotal + ",");
            }
            else
            {
                strSql.Append("CaiTotal= null ,");
            }
            if (model.CAIXS != null)
            {
                strSql.Append("CAIXS=" + model.CAIXS + ",");
            }
            else
            {
                strSql.Append("CAIXS= null ,");
            }
            if (model.Hui != null)
            {
                strSql.Append("Hui=" + model.Hui + ",");
            }
            else
            {
                strSql.Append("Hui= null ,");
            }
            if (model.HuiXS != null)
            {
                strSql.Append("HuiXS=" + model.HuiXS + ",");
            }
            else
            {
                strSql.Append("HuiXS= null ,");
            }
            if (model.HuiTotal != null)
            {
                strSql.Append("HuiTotal=" + model.HuiTotal + ",");
            }
            else
            {
                strSql.Append("HuiTotal= null ,");
            }
            if (model.Ren != null)
            {
                strSql.Append("Ren=" + model.Ren + ",");
            }
            else
            {
                strSql.Append("Ren= null ,");
            }
            if (model.RenXS != null)
            {
                strSql.Append("RenXS=" + model.RenXS + ",");
            }
            else
            {
                strSql.Append("RenXS= null ,");
            }
            if (model.RenTotal != null)
            {
                strSql.Append("RenTotal=" + model.RenTotal + ",");
            }
            else
            {
                strSql.Append("RenTotal= null ,");
            }
            if (model.VAT != null)
            {
                strSql.Append("VAT='" + model.VAT + "',");
            }
            else
            {
                strSql.Append("VAT= null ,");
            }


            //if (model.fileName != null)
            //{
            //    strSql.Append("fileName='" + model.fileName + "',");
            //}
            //else
            //{
            //    strSql.Append("fileName= null ,");
            //}
            //if (model.fileNo != null)
            //{
            //    strSql.Append("fileNo=" + model.fileNo + ",");
            //}
            //else
            //{
            //    strSql.Append("fileNo= null ,");
            //}
            //if (model.fileType != null)
            //{
            //    strSql.Append("fileType='" + model.fileType + "',");
            //}
            //else
            //{
            //    strSql.Append("fileType= null ,");
            //}
            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            else
            {
                strSql.Append("State= null ,");
            }

            if (model.MyTeamLever != null)
            {
                strSql.Append("MyTeamLever='" + model.MyTeamLever + "',");
            }
           
            if (model.XingCai != null)
            {
                strSql.Append("XingCai=" + model.XingCai + ",");
            }
            else
            {
                strSql.Append("XingCai= null ,");
            }
            if (model.XingCaiXS != null)
            {
                strSql.Append("XingCaiXS=" + model.XingCaiXS + ",");
            }
            else
            {
                strSql.Append("XingCaiXS= null ,");
            }
            if (model.XingCaiTotal != null)
            {
                strSql.Append("XingCaiTotal=" + model.XingCaiTotal + ",");
            }
            else
            {
                strSql.Append("XingCaiTotal= null ,");
            }

            strSql.Append("FundType='" + model.FundType + "',");
            strSql.Append("ShuoMing='" + model.ShuoMing + "',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");

            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_FundsUse ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_FundsUse GetModel_File(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" fileName,fileType,fileNo ");
            strSql.Append(" from tb_FundsUse ");
            strSql.Append(" where tb_FundsUse.Id=" + id + "");

            VAN_OA.Model.EFrom.tb_FundsUse model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = new tb_FundsUse();
                        object ojb;
                        ojb = dataReader["fileName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fileName = ojb.ToString();
                        }

                        ojb = dataReader["fileType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fileType = ojb.ToString();
                        }

                        ojb = dataReader["fileNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fileNo = (byte[])ojb;
                        }


                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_FundsUse GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" MyTeamLever,ShuoMing,FundType,tb_FundsUse.id,appUserId,datetiem,useTo,type,total ,loginIPosition,loginName,Invoce,HouseNo,Idea,fileName,fileType,ExpNo,proNo,PONO,POName,GuestName,CaiTotal,CAIXS,Hui,HuiXS,HuiTotal,Ren,RenXS,RenTotal,VAT,state,XingCai,XingCaiXS,XingCaiTotal ");
            strSql.Append(" from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId");
            strSql.Append(" where tb_FundsUse.id=" + id + "");
            VAN_OA.Model.EFrom.tb_FundsUse model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_FundsUse> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select MyTeamLever,ShuoMing,FundType,tb_FundsUse.id,appUserId,datetiem,useTo,type,total ,loginIPosition,loginName,Invoce,HouseNo,Idea,fileName,fileType,ExpNo,proNo,PONO,POName,GuestName,CaiTotal,CAIXS,Hui,HuiXS,HuiTotal,Ren,RenXS,RenTotal,VAT,state,XingCai,XingCaiXS,XingCaiTotal  ");
            strSql.Append(" from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId");
            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_FundsUse> list = new List<VAN_OA.Model.EFrom.tb_FundsUse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_FundsUse> GetFundList(string strWhere)
        {
            string sql = "select * from FpTaxInfo";
            System.Collections.Hashtable hs = new System.Collections.Hashtable();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        hs.Add(dataReader["Tax"].ToString(), dataReader["FpType"].ToString());                       
                    }
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select tb_FundsUse.id,tb_FundsUse.ProNo,datetiem,tb_FundsUse.PONo,tb_FundsUse.POName,tb_FundsUse.GuestName,AE,
ShuoMing,FundType,TB_Company.ComName,
ISNULL(CaiTotal,0)+ISNULL(RenTotal,0)+ISNULL(HuiTotal,0)+ISNULL(XingCaiTotal,0) as AllTotal,
ISNULL(total,0)+ISNULL(Ren,0)+ISNULL(Hui,0)+ISNULL(XingCai,0) as AllTrueTotal,CAIXS,HuiXS,RenXS,XingCaiXS
from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId
left join TB_Company on TB_Company.ComCode=tb_User.CompanyCode
left join CG_POOrder on CG_POOrder.PONo=tb_FundsUse.PONo AND IFZhui=0");
        

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by tb_FundsUse.Id desc");
            List<VAN_OA.Model.EFrom.tb_FundsUse> list = new List<VAN_OA.Model.EFrom.tb_FundsUse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_FundsUse model = new VAN_OA.Model.EFrom.tb_FundsUse();
                        object ojb;

                        model.id = Convert.ToInt32( dataReader["id"]);
                        ojb = dataReader["datetiem"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.datetiem = (DateTime)ojb;
                        }
                       
                                       
                      
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["PONo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo = ojb.ToString();
                        }
                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }
                        ojb = dataReader["GuestName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }
                        ojb = dataReader["FundType"];
                        model.FundType = ojb.ToString();

                        ojb = dataReader["ShuoMing"];
                        model.ShuoMing = ojb.ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.ComName = dataReader["ComName"].ToString();

                        model.AllTotal =Convert.ToDecimal(dataReader["AllTotal"]);
                        model.AllTrueTotal = Convert.ToDecimal(dataReader["AllTrueTotal"]);
                        ojb=dataReader["CAIXS"];
                        if (ojb != DBNull.Value && Convert.ToDecimal(ojb) > 0 && hs.ContainsKey(ojb.ToString()))
                        {
                            model.XishuDes = ojb + "-" + hs[ojb.ToString()].ToString();
                        }
                        ojb = dataReader["HuiXS"];
                        if (ojb != DBNull.Value && Convert.ToDecimal(ojb) > 0 && hs.ContainsKey(ojb.ToString()))
                        {
                            model.XishuDes = ojb + "-" + hs[ojb.ToString()].ToString();
                        }                   
                        ojb = dataReader["XingCaiXS"];
                        if (ojb != DBNull.Value && Convert.ToDecimal(ojb) > 0 && hs.ContainsKey(ojb.ToString()))
                        {
                            model.XishuDes = ojb + "-" + hs[ojb.ToString()].ToString();
                        }
                        ojb = dataReader["RenXS"];
                        if (ojb != DBNull.Value && Convert.ToDecimal(ojb) > 0 && hs.ContainsKey(ojb.ToString()))
                        {
                            model.XishuDes = ojb + "-" + hs[ojb.ToString()].ToString();
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
        public VAN_OA.Model.EFrom.tb_FundsUse ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_FundsUse model = new VAN_OA.Model.EFrom.tb_FundsUse();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["appUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.appUserId = (int)ojb;
            }
            ojb = dataReader["datetiem"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.datetiem = (DateTime)ojb;
            }
            model.useTo = dataReader["useTo"].ToString();
            model.type = dataReader["type"].ToString();
            ojb = dataReader["total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.total = (decimal)ojb;
            }

            ojb = dataReader["loginIPosition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DepartName =ojb.ToString();
            }
            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }

            ojb = dataReader["Invoce"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Invoce = ojb.ToString();
            }

            ojb = dataReader["HouseNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseNo = ojb.ToString();
            }
            ojb = dataReader["Idea"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Idea = ojb.ToString();
            }
            ojb = dataReader["fileName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.fileName = ojb.ToString();
            }

            ojb = dataReader["fileType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.fileType = ojb.ToString();
            }     
      
            ojb = dataReader["ExpNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ExpNo = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }


           
            ojb = dataReader["PONo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PONo =  ojb.ToString();
            }


            
            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
         

            ojb = dataReader["GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestName = ojb.ToString();
            }


            ojb = dataReader["CaiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiTotal = (decimal)ojb;
            }
            ojb = dataReader["CAIXS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CAIXS = (decimal)ojb;
            }
            ojb = dataReader["Hui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Hui = (decimal)ojb;
            }
            ojb = dataReader["HuiXS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HuiXS = (decimal)ojb;
            }
            ojb = dataReader["HuiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HuiTotal = (decimal)ojb;
            }
            ojb = dataReader["Ren"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ren = (decimal)ojb;
            }
            ojb = dataReader["RenXS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RenXS = (decimal)ojb;
            }
            ojb = dataReader["RenTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RenTotal = (decimal)ojb;
            }
             
            ojb = dataReader["VAT"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.VAT = ojb.ToString();
            } 
            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }



            ojb = dataReader["XingCai"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.XingCai = (decimal)ojb;
            }
            ojb = dataReader["XingCaiXS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.XingCaiXS = (decimal)ojb;
            }
            ojb = dataReader["XingCaiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.XingCaiTotal = (decimal)ojb;
            }
            ojb = dataReader["FundType"];
            model.FundType =ojb.ToString();

            ojb = dataReader["ShuoMing"];
            model.ShuoMing = ojb.ToString();


            model.MyTeamLever= dataReader["MyTeamLever"].ToString();
            return model;
        }


        public void Edit(VAN_OA.Model.EFrom.tb_FundsUse model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_FundsUse set ");
          
            if (model.useTo != null)
            {
                strSql.Append("useTo='" + model.useTo + "',");
            }
            else
            {
                strSql.Append("useTo= null ,");
            }
           

            strSql.Append("ShuoMing='" + model.ShuoMing + "',");
            strSql.Append("MyTeamLever='" + model.MyTeamLever + "',");
            strSql.Append("FundType='" + model.FundType + "',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");

            DBHelp.ExeCommand(strSql.ToString()); 
        }


    }
}
