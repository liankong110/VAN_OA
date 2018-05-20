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
    public class Sell_OrderInHouseService
    {
        public bool updateTran(VAN_OA.Model.JXC.Sell_OrderInHouse model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Sell_OrderInHouses> orders, string IDS)
        {
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                total += orders[i].GoodSellPriceTotal;
            }
            model.TuiTotal = total;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Sell_OrderInHousesService OrdersSer = new Sell_OrderInHousesService();
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

                    for (int i = 0; i < orders.Count; i++)
                    {

                        if (orders[i].GoodPriceSecond != 0)
                        {
                            orders[i].id = model.Id;


                            OrdersSer.Update(orders[i], objCommand);
                        }
                        
                        //else if (orders[i].Ids == 0)
                        //{
                        //    OrdersSer.Add(orders[i], objCommand);

                        //}
                    }
                    //if (IDS != "")
                    //{
                    //    IDS  = IDS.Substring(0, IDS.Length - 1);
                    //    OrdersSer.DeleteByIds(IDS, objCommand);
                    //}
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (eform.state == "通过")
                        {
                            if (orders[i].GoodPriceSecond != 0)
                            {
                                houseGoodsSer.InHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPriceSecond, objCommand);
                            }
                            else
                            {
                                houseGoodsSer.InHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
                            }
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
        public int addTran(VAN_OA.Model.JXC.Sell_OrderInHouse model, VAN_OA.Model.EFrom.tb_EForm eform, List<Sell_OrderInHouses> orders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                total += orders[i].GoodSellPriceTotal;
            }
            model.TuiTotal = total;


            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Sell_OrderInHousesService OrdersSer = new Sell_OrderInHousesService();
                
                TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
                try
                {

                   
                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("Sell_OrderInHouse", objCommand);
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
                        orders[i].GoodPriceSecond = orders[i].GoodPrice;
                        OrdersSer.Add(orders[i], objCommand);

                        if (eform.state == "通过")
                        {
                            if (orders[i].GoodPriceSecond != 0)
                            {
                                houseGoodsSer.InHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPriceSecond, objCommand);
                            }
                            else
                            {
                                houseGoodsSer.InHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
                            }

                            //houseGoodsSer.InHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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
        public int Add(VAN_OA.Model.JXC.Sell_OrderInHouse model, SqlCommand objCommand)
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
           
            if (model.RuTime != null)
            {
                strSql1.Append("RuTime,");
                strSql2.Append("'" + model.RuTime + "',");
            }
            if (model.Supplier != null)
            {
                strSql1.Append("GuestNAME,");
                strSql2.Append("'" + model.Supplier + "',");
            }
            if (model.DoPer != null)
            {
                strSql1.Append("DoPer,");
                strSql2.Append("'" + model.DoPer + "',");
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

            if (model.TuiTotal != null)
            {
                strSql1.Append("TuiTotal,");
                strSql2.Append("" + model.TuiTotal + ",");
            }
            if (model.SellInType != null)
            {
                strSql1.Append("SellInType,");
                strSql2.Append("" + model.SellInType + ",");
            }

            strSql.Append("insert into Sell_OrderInHouse(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderInHouse model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderInHouse set ");

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

            if (model.TuiTotal != null)
            {
                strSql.Append("TuiTotal=" + model.TuiTotal + ",");
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
            strSql.Append("delete from Sell_OrderInHouse ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderInHouse GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SellInType,Sell_OrderInHouse.Id,CreateUserId,CreateTime,RuTime,GuestNAME,DoPer,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,TuiTotal ");
            strSql.Append(" from Sell_OrderInHouse left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where Sell_OrderInHouse.Id=" + id + "");

            VAN_OA.Model.JXC.Sell_OrderInHouse model = null;
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
        public List<VAN_OA.Model.JXC.Sell_OrderInHouse> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SellInType,Sell_OrderInHouse.Id,CreateUserId,CreateTime,RuTime,GuestNAME,DoPer,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,TuiTotal ");
            strSql.Append(" from Sell_OrderInHouse left join tb_User on tb_User.id=CreateUserId "); 
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderInHouse.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderInHouse> list = new List<VAN_OA.Model.JXC.Sell_OrderInHouse>();

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
        public VAN_OA.Model.JXC.Sell_OrderInHouse ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderInHouse model = new VAN_OA.Model.JXC.Sell_OrderInHouse();
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
            model.Supplier = dataReader["GuestNAME"].ToString();
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

            ojb = dataReader["TuiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TuiTotal =Convert.ToDecimal( ojb);
            }
            ojb = dataReader["SellInType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellInType = Convert.ToInt32(ojb);
            }
            
            return model;
        }

        public List<VAN_OA.Model.JXC.Sell_Cai_OrderInHouseListModel> GetSell_Cai_OrderInHouseListArray(string strWhere)
        {
            List<VAN_OA.Model.JXC.Sell_Cai_OrderInHouseListModel> list = new List<VAN_OA.Model.JXC.Sell_Cai_OrderInHouseListModel>();
            var sql = string.Format(@" select GoodAreaNumber,IsNormal,CaiNums,TB_HouseGoods.GoodNum as HouseGoodNum,PONums,AE,POName,GuestNAME,tb.*,CaiGoodNum,CAIRuTime,CAIGoodPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName from 
 (
 select IsNormal,MAX(RuTime) as RuTime,PONo,GooId,sum(GoodNum) as GoodNum,avg(GoodPrice) as GoodPrice,
 avg(GoodSellPrice) as GoodSellPrice,AVG(GoodPriceSecond) as GoodPriceSecond,SUM(GoodTotalCha) as GoodTotalCha
 from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id
 where Status='通过'
 group by PONo,GooId,IsNormal
 ) as tb left join 
 (
	select PONo,GooId,SUM(GoodNum) as CaiGoodNum,MAX(RuTime) as CAIRuTime,AVG(GoodPrice) as CAIGoodPrice 
	
	from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id
	where Status<>'不通过'
	group by PONo,GooId
 ) as  tb1 on tb.PONo=tb1.PONo and tb.GooId=tb1.GooId
 left join TB_Good on TB_Good.GoodId=tb.GooId 
 left join TB_HouseGoods on TB_HouseGoods.GoodId=tb.GooId
left join 
(
select PONo,GoodId,sum(CAI_POCai.Num) as CaiNums from CAI_POOrder left join CAI_POCai on CAI_POCai.Id=CAI_POOrder.Id
where Status='通过' and lastSupplier='库存'
group by PONo,GoodId
)
as tempCai on tb.PONo=tempCai.PONo and tb.GooId=tempCai.GoodId 
 lefT JOIN 
 (
	SELECT  CG_POOrder.PONo,CG_POOrders.GoodId,sum(CG_POOrders.Num) as PONums 
	FROM CG_POOrder left join CG_POOrders on CG_POOrder.Id=CG_POOrders.Id
	 WHERE Status='通过' GROUP BY CG_POOrders.GoodId,CG_POOrder.PONo
 )as poTable on poTable.PONo=tb.PONo and poTable.GoodId=tb.GooId left join CG_POOrder
on CG_POOrder.Status='通过' and CG_POOrder.IFZhui=0 and CG_POOrder.PONO=tb.PONo where 1=1 {0} order by tb.PONO desc ", strWhere);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new Sell_Cai_OrderInHouseListModel();                        
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.GuestName = dataReader["GuestNAME"].ToString();
                        model.CreateName = dataReader["AE"].ToString();
                        model.RuTime =Convert.ToDateTime(dataReader["RuTime"]);
                        var ojb = dataReader["GoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum = (decimal)ojb;
                        }
                        ojb = dataReader["GoodPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodPrice = (decimal)ojb;
                        }                      
                        model.Total = model.GoodNum * model.GoodPrice;
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
                        ojb = dataReader["GoodSellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSellPrice = Convert.ToDecimal(ojb);
                        }


                        model.GoodSellPriceTotal = model.GoodSellPrice * model.GoodNum;

                        ojb = dataReader["GoodPriceSecond"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodPriceSecond = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["GoodTotalCha"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTotalCha = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["CaiGoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiGoodNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["CAIRuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CAIRuTime = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["CAIGoodPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CAIGoodPrice = Convert.ToDecimal(ojb);
                            model.CAIGoodPriceTotal=model.CAIGoodPrice *  model.CaiGoodNum ;
                        }
                        ojb = dataReader["PONums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONums = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["HouseGoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HouseGoodNum = Convert.ToDecimal(ojb);
                        } 
                        ojb = dataReader["CaiNums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiNums = Convert.ToDecimal(ojb);
                        }
                        model.GooId = Convert.ToInt32(dataReader["GooId"]);
                        ojb = dataReader["IsNormal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsNormal = Convert.ToBoolean(ojb);
                        }

                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(model);
                    }
                }
            }

            return list;
        }

    }
}
