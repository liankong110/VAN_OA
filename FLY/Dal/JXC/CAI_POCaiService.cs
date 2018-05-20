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

namespace VAN_OA.Dal.JXC
{
    public class CAI_POCaiService
    {   /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CAI_POCai model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.CaiTime != null)
            {
                strSql1.Append("CaiTime,");
                strSql2.Append("'" + model.CaiTime + "',");
            }
            if (model.Supplier != null)
            {
                strSql1.Append("Supplier,");
                strSql2.Append("'" + model.Supplier + "',");
            }
            if (model.SupperPrice != null)
            {
                strSql1.Append("SupperPrice,");
                strSql2.Append("" + model.SupperPrice + ",");
            }
            if (model.UpdateUser != null)
            {
                strSql1.Append("UpdateUser,");
                strSql2.Append("'" + model.UpdateUser + "',");
            }
            if (model.Idea != null)
            {
                strSql1.Append("Idea,");
                strSql2.Append("'" + model.Idea + "',");
            }
            if (model.Supplier1 != null)
            {
                strSql1.Append("Supplier1,");
                strSql2.Append("'" + model.Supplier1 + "',");
            }
            if (model.SupperPrice1 != null)
            {
                strSql1.Append("SupperPrice1,");
                strSql2.Append("" + model.SupperPrice1 + ",");
            }
            if (model.Supplier2 != null)
            {
                strSql1.Append("Supplier2,");
                strSql2.Append("'" + model.Supplier2 + "',");
            }
            if (model.SupperPrice2 != null)
            {
                strSql1.Append("SupperPrice2,");
                strSql2.Append("" + model.SupperPrice2 + ",");
            }

            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Num != null)
            {
                strSql1.Append("Num,");
                strSql2.Append("" + model.Num + ",");
            }

            if (model.FinPrice1 != null)
            {
                strSql1.Append("FinPrice1,");
                strSql2.Append("" + model.FinPrice1 + ",");
            }
            if (model.FinPrice2 != null)
            {
                strSql1.Append("FinPrice2,");
                strSql2.Append("" + model.FinPrice2 + ",");
            }
            if (model.FinPrice3 != null)
            {
                strSql1.Append("FinPrice3,");
                strSql2.Append("" + model.FinPrice3 + ",");
            }
            if (model.GoodId != null)
            {
                strSql1.Append("GoodId,");
                strSql2.Append("" + model.GoodId + ",");
            }


            if (model.cbifDefault1 != null)
            {
                strSql1.Append("cbifDefault1,");
                strSql2.Append("" + (model.cbifDefault1 ? 1 : 0) + ",");
            }
            if (model.cbifDefault2 != null)
            {
                strSql1.Append("cbifDefault2,");
                strSql2.Append("" + (model.cbifDefault2 ? 1 : 0) + ",");
            }
            if (model.cbifDefault3 != null)
            {
                strSql1.Append("cbifDefault3,");
                strSql2.Append("" + (model.cbifDefault3 ? 1 : 0) + ",");
            }

            if (model.CaiFpType != null)
            {
                strSql1.Append("CaiFpType,");
                strSql2.Append("'" + model.CaiFpType + "',");
            }
            if (!string.IsNullOrEmpty(model.lastPrice))
            {
                strSql1.Append("lastPrice,");
                strSql2.Append("" + model.lastPrice + ",");
            }
            if (!string.IsNullOrEmpty(model.lastSupplier))
            {
                strSql1.Append("lastSupplier,");
                strSql2.Append("'" + model.lastSupplier + "',");
            }
            strSql.Append("insert into CAI_POCai(");
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
        public void Update(VAN_OA.Model.JXC.CAI_POCai model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_POCai set ");


            //if (model.Id != null)
            //{
            //    strSql.Append("Id=" + model.Id + ",");
            //}
            //else
            //{
            //    strSql.Append("Id= null ,");
            //}
            //if (model.CaiTime != null)
            //{
            //    strSql.Append("CaiTime='" + model.CaiTime + "',");
            //}
            //else
            //{
            //    strSql.Append("CaiTime= null ,");
            //}
            if (!string.IsNullOrEmpty(model.Supplier))
            {
                strSql.Append("Supplier='" + model.Supplier + "',");
            }
            //else
            //{
            //    strSql.Append("Supplier= null ,");
            //}
            if (model.SupperPrice != null)
            {
                strSql.Append("SupperPrice=" + model.SupperPrice + ",");
            }
            strSql.Append("TruePrice1=" + model.TruePrice1 + ",");
            strSql.Append("TruePrice2=" + model.TruePrice2 + ",");
            strSql.Append("TruePrice3=" + model.TruePrice3 + ",");
            //else
            //{
            //    strSql.Append("SupperPrice= null ,");
            //}
            if (!string.IsNullOrEmpty(model.UpdateUser))
            {
                strSql.Append("UpdateUser='" + model.UpdateUser + "',");
            }
            //else
            //{
            //    strSql.Append("UpdateUser= null ,");
            //}
            if (!string.IsNullOrEmpty(model.Idea))
            {
                strSql.Append("Idea='" + model.Idea + "',");
            }
            //else
            //{
            //    strSql.Append("Idea= null ,");
            //}
            //if (!string.IsNullOrEmpty(model.Supplier1))
            //{
            strSql.Append("Supplier1='" + model.Supplier1 + "',");
            //  }

            //else
            //{
            //    strSql.Append("Supplier1= null ,");
            //}
            if (model.SupperPrice1 != null && model.SupperPrice1 > 0)
            {
                strSql.Append("SupperPrice1=" + model.SupperPrice1 + ",");
            }
            else
            {
                strSql.Append("SupperPrice1=null,");
            }
            //else
            //{
            //    strSql.Append("SupperPrice1= null ,");
            //}
            //if (!string.IsNullOrEmpty(model.Supplier2))
            //{
            strSql.Append("Supplier2='" + model.Supplier2 + "',");
            //}
            //else
            //{
            //    strSql.Append("Supplier2= null ,");
            //}
            if (model.SupperPrice2 != null && model.SupperPrice2 > 0)
            {
                strSql.Append("SupperPrice2=" + model.SupperPrice2 + ",");
            }
            else
            {
                strSql.Append("SupperPrice2= null ,");
            }

            if (!string.IsNullOrEmpty(model.GuestName))
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            //else
            //{
            //    strSql.Append("GuestName= null ,");
            //}
            if (!string.IsNullOrEmpty(model.InvName))
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            //else
            //{
            //    strSql.Append("InvName= null ,");
            //}
            //if (model.Num != null&&model.Num>0)
            //{
            //    strSql.Append("Num=" + model.Num + ",");
            //}
            //else
            //{
            //    strSql.Append("Num= null ,");
            //}
            if (model.FinPrice1 != null)
            {
                strSql.Append("FinPrice1=" + model.FinPrice1 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice1= null ,");
            //}
            if (model.FinPrice2 != null && model.FinPrice2 > 0)
            {
                strSql.Append("FinPrice2=" + model.FinPrice2 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice2= null ,");
            //}
            if (model.FinPrice3 != null && model.FinPrice3 > 0)
            {
                strSql.Append("FinPrice3=" + model.FinPrice3 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice3= null ,");
            //}

            if (model.cbifDefault1 != null)
            {
                strSql.Append("cbifDefault1=" + (model.cbifDefault1 ? 1 : 0) + ",");
            }
            //else
            //{
            //    strSql.Append("cbifDefault1= null ,");
            //}
            if (model.cbifDefault2 != null)
            {
                strSql.Append("cbifDefault2=" + (model.cbifDefault2 ? 1 : 0) + ",");
            }
            //else
            //{
            //    strSql.Append("cbifDefault2= null ,");
            //}
            if (model.cbifDefault3 != null)
            {
                strSql.Append("cbifDefault3=" + (model.cbifDefault3 ? 1 : 0) + ",");
            }
            //else
            //{
            //    strSql.Append("cbifDefault3= null ,");
            //}


            if (model.cbifDefault1 && (model.FinPrice1 != null || model.SupperPrice != null))
            {
                if (model.FinPrice1 != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier + "',");
                    strSql.Append("lastPrice=" + model.FinPrice1 + ",");

                }
                else if (model.SupperPrice != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier + "',");
                    strSql.Append("lastPrice=" + model.SupperPrice + ",");
                }

                strSql.Append("LastTruePrice=" + model.TruePrice1 + ",");
                // strSql.Append(string.Format("lastSupplier= '{0}',lastPrice={1}", model.Supplier, model.SupperPrice));

            }

            if (model.cbifDefault2 && (model.FinPrice2 != null || model.SupperPrice1 != null))
            {

                if (model.FinPrice2 != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier1 + "',");
                    strSql.Append("lastPrice=" + model.FinPrice2 + ",");
                }

                else if (model.SupperPrice1 != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier1 + "',");
                    strSql.Append("lastPrice=" + model.SupperPrice1 + ",");
                }

                strSql.Append("LastTruePrice=" + model.TruePrice2 + ",");
                //strSql.Append(string.Format("lastSupplier= '{0}',lastPrice={1}", model.Supplier1, model.SupperPrice1));
            }

            if (model.cbifDefault3 && (model.FinPrice3 != null || model.SupperPrice2 != null))
            {
                if (model.FinPrice3 != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier2 + "',");
                    strSql.Append("lastPrice=" + model.FinPrice3 + ",");

                }
                else if (model.SupperPrice2 != null)
                {
                    strSql.Append("lastSupplier='" + model.Supplier2 + "',");
                    strSql.Append("lastPrice=" + model.SupperPrice2 + ",");
                }
                strSql.Append("LastTruePrice=" + model.TruePrice3 + ",");
                //strSql.Append(string.Format("lastSupplier= '{0}',lastPrice={1}", model.Supplier2, model.SupperPrice2));
            }

            if (model.IsHanShui != null)
            {
                strSql.Append("IsHanShui=" + (model.IsHanShui ? 1 : 0) + ",");
            }

            if (!string.IsNullOrEmpty(model.CaiFpType))
            {
                strSql.Append("CaiFpType='" + model.CaiFpType + "',");
            }
            if (model.TopTruePrice > 0)
            {
                strSql.Append("TopTruePrice=" + model.TopTruePrice + ",");
            }
            if (model.TopSupplierPrice > 0)
            {
                strSql.Append("TopSupplierPrice=" + model.TopSupplierPrice + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Ids=" + model.Ids + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_POCai ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_POCai ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_POCai ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_POCai GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" [LastTruePrice],[TruePrice1],[TruePrice2],[TruePrice3],Ids,CAI_POCai.Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,GuestName,InvName,Num ,FinPrice1,FinPrice2,FinPrice3 ,CAI_POCai.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,cbifDefault1,cbifDefault2,cbifDefault3,TopPrice,IsHanShui");
            strSql.Append(" from CAI_POCai left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId  ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CAI_POCai model = null;
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
        public List<VAN_OA.Model.JXC.CAI_POCai> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TopTruePrice,TopSupplierPrice,[LastTruePrice],[TruePrice1],[TruePrice2],[TruePrice3],lastSupplier,lastPrice,CaiFpType,Ids,CAI_POCai.Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,GuestName,InvName,Num ,FinPrice1,FinPrice2,FinPrice3 ,CAI_POCai.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,cbifDefault1,cbifDefault2,cbifDefault3,TopPrice,IsHanShui");
            strSql.Append(" from CAI_POCai left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_POCai> list = new List<VAN_OA.Model.JXC.CAI_POCai>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb = dataReader["CaiFpType"]; ;
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiFpType = dataReader["CaiFpType"].ToString();
                        }
                        ojb = dataReader["lastSupplier"]; ;
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.lastSupplier = dataReader["lastSupplier"].ToString();
                        }
                        ojb = dataReader["lastPrice"]; ;
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.lastPrice = dataReader["lastPrice"].ToString();
                        }
                        model.TopTruePrice = Convert.ToDecimal(dataReader["TopTruePrice"]);
                        model.TopSupplierPrice = Convert.ToDecimal(dataReader["TopSupplierPrice"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }




        /// <summary>
        /// 查询所有未检验的订单
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_POCaiView> GetListView(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" * ");
            strSql.Append(" from CAI_Order_Supplier_View ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_POCaiView> list = new List<VAN_OA.Model.JXC.CAI_POCaiView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        Model.JXC.CAI_POCaiView model = new VAN_OA.Model.JXC.CAI_POCaiView();
                        object ojb;



                        model.PONo = dataReader["PONo"].ToString();
                        model.BusType = dataReader["BusType"].ToString();

                        ojb = dataReader["GuestNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestNo = ojb.ToString();
                        }

                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["GuestName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }

                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }

                        ojb = dataReader["INSIDE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDE = ojb.ToString();
                        }

                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = Convert.ToDateTime(ojb);
                        }

                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["POPayStype"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POPayStype = ojb.ToString();
                        }


                        model.POCaiId = Convert.ToInt32(dataReader["POCaiId"]);


                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["lastPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Price = Convert.ToDecimal(ojb);
                        }
                        model.Total = model.Price * model.Num;

                        ojb = dataReader["LastSupplier"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Supplier = ojb.ToString();
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }



                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 查询所有未检验的订单
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_POCaiView> GetListViewCai_POOrders_Cai_POOrderChecks_View(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" GoodAreaNumber,[ProNo],[PONo] ,[POName],[PODate],[POPayStype],[POTotal],[GuestName],[AE] ");
            strSql.Append(",[INSIDE],[GuestNo],[Ids],[Id],[Num],[GoodId],[totalOrderNum],[lastSupplier],[lastPrice],[GoodNo],[GoodName],[GoodSpec]");
            strSql.Append(",[GoodModel],[GoodUnit],[GoodTypeSmName],[CaiGou],loginName,LastTruePrice");
            strSql.Append(" from Cai_POOrders_Cai_POOrderChecks_View ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ids desc");
            List<VAN_OA.Model.JXC.CAI_POCaiView> list = new List<VAN_OA.Model.JXC.CAI_POCaiView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        Model.JXC.CAI_POCaiView model = new VAN_OA.Model.JXC.CAI_POCaiView();
                        object ojb;

                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();

                        model.PONo = dataReader["PONo"].ToString();


                        ojb = dataReader["GuestNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestNo = ojb.ToString();
                        }

                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["GuestName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }

                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }

                        ojb = dataReader["INSIDE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDE = ojb.ToString();
                        }

                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = Convert.ToDateTime(ojb);
                        }

                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["POPayStype"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POPayStype = ojb.ToString();
                        }


                        model.POCaiId = Convert.ToInt32(dataReader["Ids"]);


                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["totalOrderNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.totalOrderNum = Convert.ToDecimal(ojb);
                        }

                        model.ResultNum = model.Num - model.totalOrderNum;

                        ojb = dataReader["lastPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Price = Convert.ToDecimal(ojb);
                        }
                        model.Total = model.Price * model.ResultNum;

                        ojb = dataReader["LastSupplier"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Supplier = ojb.ToString();
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.loginName = ojb.ToString();
                        }



                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["LastTruePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTruePrice = Convert.ToDecimal(ojb);
                        }

                        model.CaiGou = dataReader["CaiGou"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }





        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.CAI_POCai ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_POCai model = new VAN_OA.Model.JXC.CAI_POCai();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CaiTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiTime = (DateTime)ojb;
            }
            model.Supplier = dataReader["Supplier"].ToString();


            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            ojb = dataReader["SupperPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice = (decimal)ojb;

                try
                {
                    model.Total1 = model.SupperPrice.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }
            model.UpdateUser = dataReader["UpdateUser"].ToString();
            model.Idea = dataReader["Idea"].ToString();


            try
            {
                model.GuestName = dataReader["GuestName"].ToString();
            }
            catch (Exception)
            {


            }
            try
            {
                model.InvName = dataReader["InvName"].ToString();
            }
            catch (Exception)
            {


            }





            model.Supplier1 = dataReader["Supplier1"].ToString();
            ojb = dataReader["SupperPrice1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice1 = (decimal)ojb;

                try
                {
                    model.Total2 = model.SupperPrice1.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }
            model.Supplier2 = dataReader["Supplier2"].ToString();
            ojb = dataReader["SupperPrice2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice2 = (decimal)ojb;
                try
                {
                    model.Total3 = model.SupperPrice2.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }
            ojb = dataReader["FinPrice1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice1 = (decimal)ojb;

                try
                {
                    model.Total1 = model.FinPrice1.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }
            ojb = dataReader["FinPrice2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice2 = (decimal)ojb;

                try
                {
                    model.Total2 = model.FinPrice2.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }
            ojb = dataReader["FinPrice3"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice3 = (decimal)ojb;

                try
                {
                    model.Total3 = model.FinPrice3.Value * model.Num.Value;
                }
                catch (Exception)
                {


                }
            }

            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = Convert.ToInt32(ojb);
            }

            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo = ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }

            ojb = dataReader["cbifDefault1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault1 = (bool)ojb;
            }

            ojb = dataReader["cbifDefault2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault2 = (bool)ojb;
            }

            ojb = dataReader["cbifDefault3"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault3 = (bool)ojb;
            }

            try
            {
                ojb = dataReader["TopPrice"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.TopPrice = (decimal)ojb;
                }

            }
            catch (Exception)
            {


            }
            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
            }

            ojb = dataReader["TruePrice1"];
            model.TruePrice1 = Convert.ToDecimal(ojb);

            ojb = dataReader["TruePrice2"];
            model.TruePrice2 = Convert.ToDecimal(ojb);

            ojb = dataReader["TruePrice3"];
            model.TruePrice3 = Convert.ToDecimal(ojb);

            ojb = dataReader["LastTruePrice"];
            model.LastTruePrice = Convert.ToDecimal(ojb);
            return model;
        }

        /// <summary>
        /// 根据项目编号 获取对应的供应商信息
        /// </summary>
        /// <param name="pono"></param>
        /// <returns></returns>
        public List<CAI_POCaiView> GetLastSupplier(string pono)
        {

            List<CAI_POCaiView> dicSupplier = new List<CAI_POCaiView>();
            if (pono == "")
            {
                return dicSupplier;
            }
            string sql = string.Format(@"select PONo,lastSupplier,sum(LastTruePrice*Num) as lastTotal from [CAI_POOrder] left join [dbo].[CAI_POCai] 
on [CAI_POOrder].Id=[CAI_POCai].Id where [CAI_POOrder].Status='通过'
and PONo in ({0})
group by PONo,lastSupplier
order by PONo", pono);

            string tempPONo = "";
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Model.JXC.CAI_POCaiView caiView = new Model.JXC.CAI_POCaiView();
                        caiView.PONo = dataReader["PONo"].ToString();
                        caiView.Supplier = dataReader["lastSupplier"].ToString();
                        caiView.Total = Convert.ToDecimal(dataReader["lastTotal"]);
                        dicSupplier.Add(caiView);
                    }
                }
            }
            return dicSupplier;
        }

        public List<CAI_POCaiView> GetLastSupplier(string pono, SqlCommand sqlCommand)
        {

            List<CAI_POCaiView> dicSupplier = new List<CAI_POCaiView>();
            if (pono == "")
            {
                return dicSupplier;
            }
            string sql = string.Format(@"select PONo,lastSupplier,sum(LastTruePrice*Num) as lastTotal from [CAI_POOrder] left join [dbo].[CAI_POCai] 
on [CAI_POOrder].Id=[CAI_POCai].Id where [CAI_POOrder].Status='通过'
and PONo in ({0})
group by PONo,lastSupplier
order by PONo", pono);

         
            sqlCommand.CommandText = sql;
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Model.JXC.CAI_POCaiView caiView = new Model.JXC.CAI_POCaiView();
                    caiView.PONo = dataReader["PONo"].ToString();
                    caiView.Supplier = dataReader["lastSupplier"].ToString();
                    caiView.Total = Convert.ToDecimal(dataReader["lastTotal"]);
                    dicSupplier.Add(caiView);
                }
            }

            return dicSupplier;
        }
    }
}
