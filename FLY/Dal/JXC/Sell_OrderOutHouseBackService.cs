using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient; 
using System.Text;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using System.Data;



namespace VAN_OA.Dal.JXC
{
    public class Sell_OrderOutHouseBackService
    {
        public bool updateTran(VAN_OA.Model.JXC.Sell_OrderOutHouseBack model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Sell_OrderOutHouseBacks> orders, string IDS)
        {
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                total += orders[i].GoodSellPriceTotal;
            }
            model.SellTotal = total;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                Sell_OrderOutHouseBacksService OrdersSer = new Sell_OrderOutHouseBacksService();                 
                try
                {

                    objCommand.Parameters.Clear();
                    model.Status = eform.state;
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);

                    //====删除所有子单据
                    string delete = "delete from Sell_OrderOutHouseBacks where id=" + model.Id;
                    objCommand.CommandText = delete;
                    objCommand.ExecuteNonQuery();
                    //====

                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].id = model.Id;
                        OrdersSer.Add(orders[i], objCommand);
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
        public int addTran(VAN_OA.Model.JXC.Sell_OrderOutHouseBack model, VAN_OA.Model.EFrom.tb_EForm eform, List<Sell_OrderOutHouseBacks> orders, out int MainId)
        {
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                total += orders[i].GoodSellPriceTotal;
            }
            model.SellTotal = total;

            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Sell_OrderOutHouseBacksService OrdersSer = new Sell_OrderOutHouseBacksService();                
                TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
                try
                {
                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("Sell_OrderOutHouseBack", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    model.Status = eform.state;
                    id = Add(model, objCommand);
                    MainId = id;

                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].id = id;
                        OrdersSer.Add(orders[i], objCommand);
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
        public int Add(VAN_OA.Model.JXC.Sell_OrderOutHouseBack model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            
            strSql1.Append("CreateTime,");
            strSql2.Append("getdate(),");
           
            if (model.BackTime != null)
            {
                strSql1.Append("BackTime,");
                strSql2.Append("'" + model.BackTime + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.BackType != null)
            {
                strSql1.Append("BackType,");
                strSql2.Append("" + model.BackType + ",");
            }
          
            if (model.SellProNo != null)
            {
                strSql1.Append("SellProNo,");
                strSql2.Append("'" + model.SellProNo + "',");
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
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }            

            if (model.SellTotal != null)
            {
                strSql1.Append("SellTotal,");
                strSql2.Append("" + model.SellTotal + ",");
            }

            strSql.Append("insert into Sell_OrderOutHouseBack(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderOutHouseBack model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderOutHouseBack set ");

            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }


            strSql.Append("BackTime='" + model.BackTime + "',");
            strSql.Append("SellProNo='" + model.SellProNo + "',");
            strSql.Append("BackType=" + model.BackType + ",");

            strSql.Append("SellTotal=" + model.SellTotal + ",");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        public void SellOrderBackUpdatePoStatus(string pono)
        {
            string sql = string.Format(@"select top 1 Sell_OrderOutHouse.Id
from Sell_OrderOutHouse left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo and  Sell_OrderOutHouseBack.Status='通过' where 
Sell_OrderOutHouse.Status='通过' and Sell_OrderOutHouseBack.id is null and Sell_OrderOutHouse.PoNo='{0}';", pono);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();                
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.CommandText = sql;
                object obj= objCommand.ExecuteScalar();
                if ((obj is DBNull) || obj == null)
                {
                    sql = string.Format("update CG_POOrder set POStatue5='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue5, pono);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                else
                {
                    sql = string.Format("update CG_POOrder set POStatue5='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue5_1, pono);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderOutHouseBack ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderOutHouseBack GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderOutHouseBack.Id,CreateUserId,CreateTime,BackTime,GuestName,BackType,SellProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,SellTotal");
            strSql.Append(" from Sell_OrderOutHouseBack left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where Sell_OrderOutHouseBack.Id=" + id + "");

            VAN_OA.Model.JXC.Sell_OrderOutHouseBack model = null;
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
        public List<VAN_OA.Model.JXC.Sell_OrderOutHouseBack> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderOutHouseBack.Id,CreateUserId,CreateTime,BackTime,GuestName,BackType,SellProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,SellTotal ");
            strSql.Append(" from Sell_OrderOutHouseBack left join tb_User on tb_User.id=CreateUserId "); 
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderOutHouseBack.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderOutHouseBack> list = new List<VAN_OA.Model.JXC.Sell_OrderOutHouseBack>();

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


        public List<VAN_OA.Model.JXC.Sell_OrderOutHouseView> GetListArrayAndOutList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sell_OrderOutHouse.Id,Sell_OrderOutHouse.CreateUserId,Sell_OrderOutHouse.CreateTime,Sell_OrderOutHouse.RuTime,Sell_OrderOutHouse.Supplier,   ");
            strSql.Append(" Sell_OrderOutHouse.DoPer,Sell_OrderOutHouse.ChcekProNo,Sell_OrderOutHouse.ProNo,Sell_OrderOutHouse.PONo,Sell_OrderOutHouse.POName,Sell_OrderOutHouseBack.Remark, ");
            strSql.Append(" tb_User.loginName as CreateName,Sell_OrderOutHouse.FPNo,Sell_OrderOutHouse.SellTotal ");
            strSql.Append(" ,Sell_OrderOutHouseBack.Id as BackId,BackTime,BackType,Sell_OrderOutHouse.Status, Sell_OrderOutHouseBack.ProNO as BackProNo  from Sell_OrderOutHouse ");
            strSql.Append(" left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo");
            strSql.Append(" left join tb_User on tb_User.id=Sell_OrderOutHouseBack.CreateUserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderOutHouse.ProNo desc,BackTime desc ");
            List<VAN_OA.Model.JXC.Sell_OrderOutHouseView> list = new List<VAN_OA.Model.JXC.Sell_OrderOutHouseView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBindBack(dataReader));
                    }
                }
            }
            return list;
        }


        public VAN_OA.Model.JXC.Sell_OrderOutHouseView ReaderBindBack(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderOutHouseView model = new VAN_OA.Model.JXC.Sell_OrderOutHouseView();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.Supplier = dataReader["Supplier"].ToString();
            model.DoPer = dataReader["DoPer"].ToString();

            model.ChcekProNo = dataReader["ChcekProNo"].ToString();
            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["CreateName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateName = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }        

            ojb = dataReader["SellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellTotal = Convert.ToDecimal(ojb);
            }
         
            ojb = dataReader["BackId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackId = Convert.ToInt32(ojb);
            }

            ojb = dataReader["BackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackTime = Convert.ToDateTime(ojb);
            }
            model.BackType = 2;
            ojb = dataReader["BackType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackType = Convert.ToInt32(ojb);
            }

            ojb = dataReader["BackProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackProNo = ojb.ToString();
            }

            

            return model;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderOutHouseBack ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderOutHouseBack model = new VAN_OA.Model.JXC.Sell_OrderOutHouseBack();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["BackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.BackType =Convert.ToInt32(dataReader["BackType"]);
           
            model.SellProNo = dataReader["SellProNo"].ToString();
            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["CreateName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateName = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }           

            ojb = dataReader["SellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellTotal = Convert.ToDecimal(ojb);
            }

            
            return model;
        }



    }
}
