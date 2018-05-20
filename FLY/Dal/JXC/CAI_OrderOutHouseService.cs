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
    public class CAI_OrderOutHouseService
    {
        public bool updateTran(VAN_OA.Model.JXC.CAI_OrderOutHouse model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<CAI_OrderOutHouses> orders, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                //CG_POOrdersService OrdersSer = new CG_POOrdersService();
                //CG_POCaiService CaiSer = new CG_POCaiService();
                try
                {

                    objCommand.Parameters.Clear();
                    model.Status = eform.state;

                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);
                    TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();

                    //for (int i = 0; i < orders.Count; i++)
                    //{
                    //    orders[i].Id = model.Id;
                    //    //if (orders[i].IfUpdate == true && orders[i].Ids != 0)
                    //    //{

                    //        OrdersSer.Update(orders[i], objCommand);

                    //    //}
                    //    //else if (orders[i].Ids == 0)
                    //    //{
                    //    //    OrdersSer.Add(orders[i], objCommand);

                    //    //}
                    //}
                    //if (IDS != "")
                    //{
                    //    IDS  = IDS.Substring(0, IDS.Length - 1);
                    //    OrdersSer.DeleteByIds(IDS, objCommand);
                    //}
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (eform.state == "通过")
                        {
                            houseGoodsSer.OutHouse(model.HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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
        public int addTran(VAN_OA.Model.JXC.CAI_OrderOutHouse model, VAN_OA.Model.EFrom.tb_EForm eform, List<CAI_OrderOutHouses> orders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                CAI_OrderOutHousesService OrdersSer = new CAI_OrderOutHousesService();

                TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("CAI_OrderOutHouse", objCommand);
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

                        if (eform.state == "通过")
                        {
                            houseGoodsSer.OutHouse(model.HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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
        public int Add(VAN_OA.Model.JXC.CAI_OrderOutHouse model, SqlCommand objCommand)
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
            strSql1.Append("RuTime,");
            strSql2.Append("getdate(),");
           
            //if (model.RuTime != null)
            //{
            //    strSql1.Append("RuTime,");
            //    strSql2.Append("'" + model.RuTime + "',");
            //}
            if (model.Supplier != null)
            {
                strSql1.Append("Supplier,");
                strSql2.Append("'" + model.Supplier + "',");
            }
            if (model.DoPer != null)
            {
                strSql1.Append("DoPer,");
                strSql2.Append("'" + model.DoPer + "',");
            }
            if (model.HouseID != null)
            {
                strSql1.Append("HouseID,");
                strSql2.Append("" + model.HouseID + ",");
            }
            if (model.ChcekProNo != null)
            {
                strSql1.Append("ChcekProNo,");
                strSql2.Append("'" + model.ChcekProNo + "',");
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
            if (model.FPNo != null)
            {
                strSql1.Append("FPNo,");
                strSql2.Append("'" + model.FPNo + "',");
            }
            if (model.DaiLi != null)
            {
                strSql1.Append("DaiLi,");
                strSql2.Append("'" + model.DaiLi + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            strSql.Append("insert into CAI_OrderOutHouse(");
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
        public void Update(VAN_OA.Model.JXC.CAI_OrderOutHouse model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_OrderOutHouse set ");

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
            if (model.FPNo != null)
            {
                strSql.Append("FPNo='" + model.FPNo + "',");
            }
            else
            {
                strSql.Append("FPNo= null ,");
            }
            if (model.DaiLi != null)
            {
                strSql.Append("DaiLi='" + model.DaiLi + "',");
            }
            else
            {
                strSql.Append("DaiLi= null ,");
            }

            if (model.Status != null && model.Status != "执行中")
            {
                strSql.Append("RuTime=getdate(),");
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
            strSql.Append("delete from CAI_OrderOutHouse ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderOutHouse GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CAI_OrderOutHouse.Id,CreateUserId,CreateTime,RuTime,Supplier,DoPer,HouseID,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,GuestName ");
            strSql.Append(" from CAI_OrderOutHouse left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where CAI_OrderOutHouse.Id=" + id + "");

            VAN_OA.Model.JXC.CAI_OrderOutHouse model = null;
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
        /// 退库商品是否含税
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<int, bool> GetCAI_OrderOutHouse_HanShui(string id)
        {
            Dictionary<int, bool> list = new Dictionary<int, bool>();
            string sql = string.Format("select IsHanShui,CAI_OrderOutHouses.Id from Cai_Info left join CAI_OrderOutHouses on CAI_OrderOutHouses.OrderCheckIds=Cai_Info.CaiInHouseIds where CAI_OrderOutHouses.id in ({0}) ", id);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int Id = 0;
                        bool IsHanShui = false;

                        object ojb;
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            IsHanShui = ojb.ToString() == "1" ? true : false;
                        }
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            Id = (int)ojb;
                        }
                        if (!list.ContainsKey(Id))
                        {
                            list.Add(Id, IsHanShui);
                        }
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderOutHouse> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CAI_OrderOutHouse.Id,CAI_OrderOutHouse.CreateUserId,CAI_OrderOutHouse.CreateTime,RuTime,Supplier,DoPer,HouseID,houseName,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,GuestName,AE ");
            strSql.Append(" from CAI_OrderOutHouse left join tb_User on tb_User.id=CAI_OrderOutHouse.CreateUserId left join TB_HouseInfo on CAI_OrderOutHouse.HouseID=TB_HouseInfo.id ");
            strSql.Append(" left join (select AE,PONO as CGPONO,IsSpecial from CG_POOrder where IFZhui=0 and Status='通过') as tb on tb.CGPONO=CAI_OrderOutHouse.PONO ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CAI_OrderOutHouse.Id desc");
            List<VAN_OA.Model.JXC.CAI_OrderOutHouse> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CAI_OrderOutHouse model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["houseName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HouseName = ojb.ToString();
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
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
        public List<VAN_OA.Model.JXC.CAI_OrderOutHouse> GetListArray(string strWhere,out decimal sumTotal)
        {
            
                string sqlSum = @"select 
 isnull(SUM(CAI_OrderOutHouses.GoodNum*CAI_OrderOutHouses.GoodPrice),0)
 from CAI_OrderOutHouse left join tb_User on tb_User.id=CAI_OrderOutHouse.CreateUserId left join TB_HouseInfo on CAI_OrderOutHouse.HouseID=TB_HouseInfo.id 
 left join (select AE,PONO as CGPONO,IsSpecial from CG_POOrder where IFZhui=0 and Status='通过') as tb on tb.CGPONO=CAI_OrderOutHouse.PONO
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id 
 left join CAI_OrderInHouses on CAI_OrderOutHouses.OrderCheckIds=CAI_OrderInHouses.Ids
 left join CAI_OrderChecks on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids
 left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
 left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
left join TB_Good ON CAI_OrderOutHouses.GooId=TB_Good.GoodId";
                if (strWhere.Trim() != "")
                {
                    sqlSum += " where " + strWhere;
                }
                sumTotal = Convert.ToDecimal(DBHelp.ExeScalar(sqlSum));
           
           

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  DISTINCT ");
            strSql.Append(@" CAI_OrderOutHouse.Id,CAI_OrderOutHouse.CreateUserId,CAI_OrderOutHouse.CreateTime,RuTime,CAI_OrderOutHouse.Supplier,DoPer,HouseID,houseName,ChcekProNo,CAI_OrderOutHouse.ProNo,CAI_OrderOutHouse.PONo,CAI_OrderOutHouse.POName,Remark ,tb_User.loginName as CreateName,
 CAI_OrderOutHouse.Status,FPNo,DaiLi,CAI_OrderOutHouse.GuestName,CAI_POOrder.AE ");
            strSql.Append(" from CAI_OrderOutHouse left join tb_User on tb_User.id=CAI_OrderOutHouse.CreateUserId left join TB_HouseInfo on CAI_OrderOutHouse.HouseID=TB_HouseInfo.id ");
            strSql.Append(" left join (select AE,PONO as CGPONO,IsSpecial from CG_POOrder where IFZhui=0 and Status='通过') as tb on tb.CGPONO=CAI_OrderOutHouse.PONO ");
            strSql.Append(@"  left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id 
 left join CAI_OrderInHouses on CAI_OrderOutHouses.OrderCheckIds=CAI_OrderInHouses.Ids
 left join CAI_OrderChecks on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids
 left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
 left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
left join TB_Good ON CAI_OrderOutHouses.GooId=TB_Good.GoodId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CAI_OrderOutHouse.Id desc");
            List<VAN_OA.Model.JXC.CAI_OrderOutHouse> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CAI_OrderOutHouse model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["houseName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HouseName = ojb.ToString();
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
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
        public VAN_OA.Model.JXC.CAI_OrderOutHouse ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderOutHouse model = new VAN_OA.Model.JXC.CAI_OrderOutHouse();
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
            ojb = dataReader["HouseID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseID = (int)ojb;
            }
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

            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }
            ojb = dataReader["DaiLi"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DaiLi = ojb.ToString();
            }
            ojb = dataReader["GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestName = ojb.ToString();
            }
           

            return model;
        }



    }
}
