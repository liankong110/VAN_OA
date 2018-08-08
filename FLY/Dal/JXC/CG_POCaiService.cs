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
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace VAN_OA.Dal.JXC
{
    public class CG_POCaiService
    {   /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CG_POCai model, SqlCommand objCommand)
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
           
            strSql.Append("insert into CG_POCai(");
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
        public void Update(VAN_OA.Model.JXC.CG_POCai model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CG_POCai set ");
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
            if (!string.IsNullOrEmpty(model.Supplier1))
            {
                strSql.Append("Supplier1='" + model.Supplier1 + "',");
            }
            //else
            //{
            //    strSql.Append("Supplier1= null ,");
            //}
            if (model.SupperPrice1 != null)
            {
                strSql.Append("SupperPrice1=" + model.SupperPrice1 + ",");
            }
            //else
            //{
            //    strSql.Append("SupperPrice1= null ,");
            //}
            if (!string.IsNullOrEmpty(model.Supplier2))
            {
                strSql.Append("Supplier2='" + model.Supplier2 + "',");
            }
            //else
            //{
            //    strSql.Append("Supplier2= null ,");
            //}
            if (model.SupperPrice2 != null)
            {
                strSql.Append("SupperPrice2=" + model.SupperPrice2 + ",");
            }
            //else
            //{
            //    strSql.Append("SupperPrice2= null ,");
            //}

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
            //if (model.Num != null && model.Num>0)
            //{
            //    strSql.Append("Num=" + model.Num + ",");
            //}
            //else
            //{
            //    strSql.Append("Num= null ,");
            //}
            if (model.FinPrice1 != null )
            {
                strSql.Append("FinPrice1=" + model.FinPrice1 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice1= null ,");
            //}
            if (model.FinPrice2 != null )
            {
                strSql.Append("FinPrice2=" + model.FinPrice2 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice2= null ,");
            //}
            if (model.FinPrice3 != null )
            {
                strSql.Append("FinPrice3=" + model.FinPrice3 + ",");
            }
            //else
            //{
            //    strSql.Append("FinPrice3= null ,");
            //}
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
            strSql.Append("delete from CG_POCai ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CG_POCai ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CG_POCai ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CG_POCai GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,CG_POCai.Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,GuestName,InvName,Num ,FinPrice1,FinPrice2,FinPrice3 ,CG_POCai.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName");
            strSql.Append(" from CG_POCai left join TB_Good on TB_Good.GoodId=CG_POCai.GoodId ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CG_POCai model = null;
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
        public List<VAN_OA.Model.JXC.CG_POCai> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select Ids,Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,GuestName,InvName,Num,FinPrice1,FinPrice2,FinPrice3");
            //strSql.Append(" FROM CG_POCai ");
            strSql.Append("select   ");
            strSql.Append(@" Ids,CG_POCai.Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,GuestName,InvName,Num ,
FinPrice1,FinPrice2,FinPrice3 ,CG_POCai.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,CaiKuXuNumView.SumKuXuCai,TB_HouseGoods.housegoodnum");
            strSql.Append(" from CG_POCai left join TB_Good on TB_Good.GoodId=CG_POCai.GoodId ");
            strSql.Append(@" left join CaiKuXuNumView on CaiKuXuNumView.GoodId=CG_POCai.GoodId
left join (select goodid,sum(goodnum) as housegoodnum from TB_HouseGoods group by goodid ) as TB_HouseGoods on TB_HouseGoods.goodid=CG_POCai.GoodId");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POCai> list = new List<VAN_OA.Model.JXC.CG_POCai>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["SumKuXuCai"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumKuXuCai =Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["housegoodnum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum = Convert.ToDecimal(ojb);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 根据项目单据号来查询相应的商品信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POCai> GetCaiList(string proNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,CG_POCai.Id,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,CG_POCai.GuestName,InvName,Num ,FinPrice1,FinPrice2,FinPrice3 ,CG_POCai.GoodId");
            strSql.Append(" from CG_POCai  left join CG_POOrder on CG_POCai.id=CG_POOrder.id  ");



            strSql.AppendFormat(" where ProNo='{0}'" ,proNo);
             

            List<VAN_OA.Model.JXC.CG_POCai> list = new List<VAN_OA.Model.JXC.CG_POCai>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POCai model = new VAN_OA.Model.JXC.CG_POCai();
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
                         
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POCai> GetListArrayToList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();           
            strSql.Append("select   ");
            strSql.Append(" IFZhui,Ids,CG_POCai.Id,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,CG_POCai.GuestName,InvName,Num ,FinPrice1,FinPrice2,FinPrice3 ,CG_POCai.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,Status");
            strSql.Append(" from CG_POCai  left join CG_POOrder on CG_POCai.id=CG_POOrder.id left join TB_Good on TB_Good.GoodId=CG_POCai.GoodId ");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            } strSql.Append(" order by ProNo desc");
           
            List<VAN_OA.Model.JXC.CG_POCai> list = new List<VAN_OA.Model.JXC.CG_POCai>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        if (Convert.ToInt32(dataReader["IFZhui"]) == 0)
                        {
                            model.Type = "原";
                        }
                        else
                        {
                            model.Type = "追";
                        }
                        model.States = dataReader["Status"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.CG_POCai ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CG_POCai model = new VAN_OA.Model.JXC.CG_POCai();
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
                    model.Total3= model.SupperPrice2.Value * model.Num.Value;
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
            return model;
        }


    }
}
