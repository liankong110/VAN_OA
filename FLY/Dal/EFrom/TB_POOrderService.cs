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
    public class TB_POOrderService
    {
        public bool updateTran(VAN_OA.Model.EFrom.TB_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms,List<TB_POOrders> orders,string IDS,
            List<TB_POCai> Cais, string CAI_IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                TB_POOrdersService OrdersSer = new TB_POOrdersService();
                TB_POCaiService CaiSer = new TB_POCaiService();
                try
                {

                    objCommand.Parameters.Clear();
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);


                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].Id = model.Id;
                        //if (orders[i].IfUpdate == true && orders[i].Ids != 0)
                        //{

                            OrdersSer.Update(orders[i], objCommand);
                            
                        //}
                        //else if (orders[i].Ids == 0)
                        //{
                        //    OrdersSer.Add(orders[i], objCommand);
                          
                        //}
                    }
                    if (IDS != "")
                    {
                        IDS  = IDS.Substring(0, IDS.Length - 1);
                        OrdersSer.DeleteByIds(IDS, objCommand);
                    }



                    #region 采购
                    for (int i = 0; i < Cais.Count; i++)
                    {
                        Cais[i].Id = model.Id;
                        if (Cais[i].IfUpdate == true && Cais[i].Ids != 0)
                        {

                            CaiSer.Update(Cais[i], objCommand);

                        }
                        else if (Cais[i].Ids == 0)
                        {
                            CaiSer.Add(Cais[i], objCommand);

                        }
                    }
                    if (CAI_IDS != "")
                    {
                        CAI_IDS = CAI_IDS.Substring(0, CAI_IDS.Length - 1);
                        CaiSer.DeleteByIds(CAI_IDS, objCommand);
                    } 
                    #endregion


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
        public int addTran(VAN_OA.Model.EFrom.TB_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, List<TB_POOrders> orders, List<TB_POCai> caiOrders,out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                TB_POOrdersService OrdersSer = new TB_POOrdersService();

                TB_POCaiService caiSer = new TB_POCaiService();
                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("TB_POOrder", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;


                    id = Add(model, objCommand);
                    MainId = id;
                 
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].Id = id;
                         
                        OrdersSer.Add(orders[i], objCommand);

                        TB_POCai cai = new TB_POCai();
                        cai.GuestName = orders[i].GuestName;
                        cai.Num = orders[i].Num;
                        cai.InvName = orders[i].InvName;
                        cai.Id = id;
                        caiSer.Add(cai, objCommand);
                       
                    }


                    //for (int i = 0; i < caiOrders.Count; i++)
                    //{
                    //    caiOrders[i].Id = id;

                    //    caiSer.Add(caiOrders[i], objCommand);

                      
                    //}


                    
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
        public int Add(VAN_OA.Model.EFrom.TB_POOrder model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AppName != null)
            {
                strSql1.Append("AppName,");
                strSql2.Append("" + model.AppName + ",");
            }

            if (model.CaiGou != null)
            {
                strSql1.Append("CaiGou,");
                strSql2.Append("'" + model.CaiGou + "',");
            }
            if (model.cRemark != null)
            {
                strSql1.Append("cRemark,");
                strSql2.Append("'" + model.cRemark + "',");
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

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            strSql.Append("insert into TB_POOrder(");
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
        public void Update(VAN_OA.Model.EFrom.TB_POOrder model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_POOrder set ");
            //if (model.AppName != null)
            //{
            //    strSql.Append("AppName=" + model.AppName + ",");
            //}

            if (model.CaiGou != null)
            {
                strSql.Append("CaiGou='" + model.CaiGou + "',");
            }

            if (model.cRemark != null)
            {
                strSql.Append("cRemark='" + model.cRemark + "',");
            }
            else
            {
                strSql.Append("cRemark= null ,");
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
            strSql.Append("delete from TB_POOrder ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.TB_POOrder GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_POOrder.Id,AppName,loginName,CaiGou,cRemark ,fileName,fileType ,proNo");
            strSql.Append(" from TB_POOrder left join tb_User on tb_User.id=TB_POOrder.AppName");
            strSql.Append(" where TB_POOrder.Id=" + id + "");

            VAN_OA.Model.EFrom.TB_POOrder model = null;
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
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.TB_POOrder GetModel_File(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" fileName,fileType,fileNo ");
            strSql.Append(" from TB_POOrder ");
            strSql.Append(" where TB_POOrder.Id=" + id + "");

            VAN_OA.Model.EFrom.TB_POOrder model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = new TB_POOrder();
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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.TB_POOrder> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_POOrder.Id,AppName,loginName,CaiGou ,cRemark,fileName,fileType,proNo  ");
            strSql.Append(" from TB_POOrder left join tb_User on tb_User.id=TB_POOrder.AppName");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.TB_POOrder> list = new List<VAN_OA.Model.EFrom.TB_POOrder>();

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
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.TB_POOrder ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.TB_POOrder model = new VAN_OA.Model.EFrom.TB_POOrder();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["AppName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppName = (int)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }

            ojb = dataReader["CaiGou"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiGou = ojb.ToString();
            }
            try
            {
                model.cRemark = dataReader["cRemark"].ToString();
            }
            catch (Exception)
            {
                
                 
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

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            return model;
        }



    }
}
