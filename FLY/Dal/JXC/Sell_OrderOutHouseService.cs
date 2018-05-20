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
    public class Sell_OrderOutHouseService
    {
        public bool updateTran(VAN_OA.Model.JXC.Sell_OrderOutHouse model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Sell_OrderOutHouses> orders, string IDS)
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

                Sell_OrderOutHousesService OrdersSer = new Sell_OrderOutHousesService();
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
                        orders[i].id = model.Id;
                        //if (orders[i].IfUpdate == true && orders[i].Ids != 0)
                        //{

                        OrdersSer.Update(orders[i], objCommand);

                        //}
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
                            houseGoodsSer.OutHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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
        public int addTran(VAN_OA.Model.JXC.Sell_OrderOutHouse model, VAN_OA.Model.EFrom.tb_EForm eform, List<Sell_OrderOutHouses> orders, out int MainId)
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
                Sell_OrderOutHousesService OrdersSer = new Sell_OrderOutHousesService();
                
                TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("Sell_OrderOutHouse", objCommand);
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
                            houseGoodsSer.OutHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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
        public int Add(VAN_OA.Model.JXC.Sell_OrderOutHouse model, SqlCommand objCommand)
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
                strSql1.Append("Supplier,");
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

            if (model.SellTotal != null)
            {
                strSql1.Append("SellTotal,");
                strSql2.Append("" + model.SellTotal + ",");
            }

            strSql.Append("insert into Sell_OrderOutHouse(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderOutHouse model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderOutHouse set ");

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
            if (model.SellTotal != null)
            {
                strSql.Append("SellTotal=" + model.SellTotal + ",");
            }
            
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        public void SellOrderUpdatePoStatus(string pono,string status)
        {
            string sql = string.Format("select PONo from SellXiaoShou_View where PONo='{0}'",pono);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();                
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.CommandText = sql;
                object obj= objCommand.ExecuteScalar();
                if ((obj is DBNull) || obj == null)
                {
                    sql = string.Format("update CG_POOrder set POStatue2='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue2, pono);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }

//                if (status == "通过")
//                {
//                    sql = string.Format(@"if exists(select * from Sell_OrderOutHouseBack where Status='通过' and PONo='{1}')
//begin update CG_POOrder set POStatue5='{0}' where PONo='{1}' end", CG_POOrder.ConPOStatue5_1, pono);
//                    objCommand.CommandText = sql;
//                    objCommand.ExecuteNonQuery();
//                }
                //else
                //{
                //    //if (ifUpdate)
                //    //{
                //    //    sql = string.Format("update CG_POOrder set POStatue2='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue2, pono);
                //    //    objCommand.CommandText = sql;
                //    //    objCommand.ExecuteNonQuery();
                //    //}
                //}
            }
        }

        /// <summary>
        /// 获取需出库数量
        /// </summary>
        /// <param name="poNO"></param>
        /// <returns></returns>
        public Dictionary<int, decimal> GetSellOrderNum(string poNO)
        {
            Dictionary<int, decimal> nums = new Dictionary<int, decimal>();
            string sql = string.Format(@"select newTb1.goodId,newTb1.totalNum, newTb2.outNum,newTb3.caiTuiNum,newTb4.sellTuiNum from (
select goodId ,sum(Num) as totalNum from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过'  and  PoNo='{0}'
group by goodId) as newTb1  left join
(
select PoNo,GooId,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
where Status<>'不通过' and PoNo='{0}' group by GooId,PoNo
) as newTb2 on newTb1.goodId=newTb2.GooId left join 
(
select GooId,sum(GoodNum)  as caiTuiNum from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID 
where Status='通过'  and  PoNo='{0}' group by GooId 
) as newTb3 on newTb2.GooId=newTb3.GooId left join 
(
select GooId,sum(GoodNum) as sellTuiNum from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  
where  Status='通过' and  PoNo='{0}' group by GooId
) newTb4 on newTb3.GooId=newTb4.GooId  
", poNO);
           
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int goodId = 0;
                        decimal totalNum = 0;
                        decimal outNum = 0;
                        decimal tuiNum = 0;
                        decimal sellTuiNum = 0;
                        object ojb;
                        ojb = dataReader["goodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            goodId = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["totalNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            totalNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["outNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            outNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["caiTuiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            tuiNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["sellTuiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            sellTuiNum = Convert.ToDecimal(ojb);
                        }

                        decimal lastNum = 0;
                        if (sellTuiNum >= tuiNum)
                        {
                            lastNum = totalNum - outNum;                            
                        }
                        else
                        {
                            lastNum = totalNum - outNum + sellTuiNum - tuiNum;                           
                        }
                        if (!nums.ContainsKey(goodId))
                        {
                            nums.Add(goodId, lastNum);
                        }
                    }

                    dataReader.Close();
                }

                
                conn.Close();
            }
            return nums;
        }

        /// <summary>
        /// 获取所有正在执行商品的总和
        /// </summary>
        /// <param name="goodIds"></param>
        /// <returns></returns>
        public Dictionary<int, decimal> GetDoingOrderNum(string goodIds)
        {
            Dictionary<int, decimal> nums = new Dictionary<int, decimal>();
            string sql = string.Format(@"select sum(GoodNum) as sumGoodNum,GooId from Sell_OrderOutHouses
left join Sell_OrderOutHouse on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id 
where status='执行中' and gooId in ({0})  group by GooId", goodIds);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int goodId = 0;
                        decimal sumGoodNum = 0;
                      
                        object ojb;
                        ojb = dataReader["GooId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            goodId = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["sumGoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            sumGoodNum = Convert.ToDecimal(ojb);
                        }
                       
                        if (!nums.ContainsKey(goodId))
                        {
                            nums.Add(goodId, sumGoodNum);
                        }
                    }

                    dataReader.Close();
                }


                conn.Close();
            }
            return nums;
        }
        public void SellOrderUpdatePoStatus2(string poNO)
        {
            string sql =string.Format(@"select newTb1.totalNum, newTb2.outNum,newTb3.caiTuiNum,newTb4.sellTuiNum from (
select goodId ,sum(Num) as totalNum from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过'  and  PoNo='{0}'
group by goodId) as newTb1  left join
(
select PoNo,GooId,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
where Status='通过' and PoNo='{0}' group by GooId,PoNo
) as newTb2 on newTb1.goodId=newTb2.GooId left join 
(
select GooId,sum(GoodNum)  as caiTuiNum from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID 
where Status='通过'  and  PoNo='{0}' group by GooId 
) as newTb3 on newTb2.GooId=newTb3.GooId left join 
(
select GooId,sum(GoodNum) as sellTuiNum from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  
where  Status='通过' and  PoNo='{0}' group by GooId
) newTb4 on newTb3.GooId=newTb4.GooId  
", poNO);
            bool ifALLOut = true;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        decimal totalNum = 0;
                        decimal outNum = 0;
                        decimal tuiNum = 0;
                        decimal sellTuiNum = 0;
                        object ojb;
                        ojb = dataReader["totalNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            totalNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["outNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            outNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["caiTuiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            tuiNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["sellTuiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            sellTuiNum = Convert.ToDecimal(ojb);                            
                        }

                        if (sellTuiNum >= tuiNum)
                        {
                            if (totalNum - outNum> 0)
                            {
                                ifALLOut = false;
                                break;
                            }
                        }
                        else
                        {
                            if (totalNum - outNum + sellTuiNum - tuiNum > 0)
                            {
                                ifALLOut = false;
                                break;
                            }
                        }
                    } 

                    dataReader.Close();
                }

                if (ifALLOut)
                {
                    sql = string.Format("update CG_POOrder set POStatue2='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue2, poNO);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                else
                {
                    sql = string.Format("update CG_POOrder set POStatue2=null where PONo='{0}'", poNO);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderOutHouse ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderOutHouse GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderOutHouse.Id,CreateUserId,CreateTime,RuTime,Supplier,DoPer,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,IsPrint ,SellTotal");
            strSql.Append(" from Sell_OrderOutHouse left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where Sell_OrderOutHouse.Id=" + id + "");

            VAN_OA.Model.JXC.Sell_OrderOutHouse model = null;
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
        public List<VAN_OA.Model.JXC.Sell_OrderOutHouse> GetListArray_ToBack(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderOutHouse.Id,RuTime,Sell_OrderOutHouse.Supplier,Sell_OrderOutHouse.ProNo,Sell_OrderOutHouse.PONo,Sell_OrderOutHouse.POName,Sell_OrderOutHouse.SellTotal ");
            strSql.Append(" from Sell_OrderOutHouse left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo and Sell_OrderOutHouseBack.Status<>'不通过'");

            strSql.Append(" where Sell_OrderOutHouse.Status='通过' and Sell_OrderOutHouseBack.id is null " + strWhere);
          
            strSql.Append(" order by Sell_OrderOutHouse.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderOutHouse> list = new List<VAN_OA.Model.JXC.Sell_OrderOutHouse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderOutHouse model = new VAN_OA.Model.JXC.Sell_OrderOutHouse();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }                        
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = (DateTime)ojb;
                        }
                        model.Supplier = dataReader["Supplier"].ToString();  
                        model.ProNo = dataReader["ProNo"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["SellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellTotal = Convert.ToDecimal(ojb);
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 获取所有销售出库的单子（已经审核通过 并且在销售退货中没有退完的） 
        /// </summary>
        public List<VAN_OA.Model.JXC.Sell_OrderOutHouse> GetListSell_OrderOutHouse_Sell_OrderInHouse_View(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" [Id] ,[CreateUserId]  ,[CreateTime],[RuTime],[Supplier],[DoPer],[ChcekProNo],[ProNo],[PONo],[POName],[Remark],[Status]");
            strSql.Append(" from Sell_OrderOutHouse_Sell_OrderInHouse_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by [CreateTime] desc ");
            List<VAN_OA.Model.JXC.Sell_OrderOutHouse> list = new List<VAN_OA.Model.JXC.Sell_OrderOutHouse>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.JXC.Sell_OrderOutHouse model = new VAN_OA.Model.JXC.Sell_OrderOutHouse();
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
                        ojb = dataReader["Status"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Status = ojb.ToString();
                        }                      

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 公共费用输入界面
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetAllTotal_ChengBen(string strWhere)
        {
            Dictionary<string, decimal> totalList = new Dictionary<string, decimal>();
            
            StringBuilder strSql = new StringBuilder();
//            strSql.Append(
//                @" select TB_Good.GoodNo,isnull(sum(GoodPrice*GoodNum),0) as priceTotal from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id
//            left join TB_Good on TB_Good.GoodId=Sell_OrderOutHouses.GooId ");

//            strSql.AppendFormat(@" where Sell_OrderOutHouse.Status='通过' and  GoodNo in ('17110','17112','16616','17109','14346','17111','14350',
//'14396','14423','15317','14348','14349','17108','17158') {0} group by TB_Good.GoodNo ", strWhere);

            strSql.AppendFormat(@"select tb.GoodNo,priceTotal-isnull(tui_priceTotal,0) as priceTotal from 
( select TB_Good.GoodNo,isnull(sum(GoodPrice*GoodNum),0) as priceTotal from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id
            left join TB_Good on TB_Good.GoodId=Sell_OrderOutHouses.GooId
            where Sell_OrderOutHouse.Status='通过' and  GoodNo in ('17167','17110','17112','16616','17109','14346','17111','14350',
'14396','14423','15317','14348','14349','17108','17158') {0} group by TB_Good.GoodNo
) as tb
left join 

( select TB_Good.GoodNo,isnull(sum(GoodPrice*GoodNum),0) as tui_priceTotal from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.Id
            left join TB_Good on TB_Good.GoodId=Sell_OrderInHouses.GooId
            where Sell_OrderInHouse.Status='通过' and  GoodNo in ('17167','17110','17112','16616','17109','14346','17111','14350',
'14396','14423','15317','14348','14349','17108','17158') {1} group by TB_Good.GoodNo
) as tb1 on tb.GoodNo=tb1.GoodNo", strWhere, strWhere.Replace("Sell_OrderOutHouse", "Sell_OrderInHouse"));
            var total = DBHelp.getDataTable(strSql.ToString());
            foreach (DataRow dr in total.Rows)
            {
                totalList.Add((string)dr[0], (decimal)dr[1]);            
            }
            //if(total is DBNull||total==null)
            //{
            //    return 0;
            //}

            //return (decimal) total;
            return totalList;
        }

        public List<decimal> GetAllTotal(string strWhere)
        {
            List<decimal> totalList = new List<decimal>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                " select isnull(sum(GoodPrice*GoodNum),0) as priceTotal,isnull(sum(GoodSellPrice*goodNum),0) as sellPriceTotal from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var total= DBHelp.getDataTable(strSql.ToString());
            foreach (DataRow dr in total.Rows)
            {
                totalList.Add((decimal)dr[0]);
                totalList.Add((decimal)dr[1]);
            }
            //if(total is DBNull||total==null)
            //{
            //    return 0;
            //}
            
            //return (decimal) total;
            return totalList;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.Sell_OrderOutHouse> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderOutHouse.Id,CreateUserId,CreateTime,RuTime,Supplier,DoPer,ChcekProNo,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,DaiLi,IsPrint,SellTotal ");
            strSql.Append(" from Sell_OrderOutHouse left join tb_User on tb_User.id=CreateUserId "); 
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderOutHouse.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderOutHouse> list = new List<VAN_OA.Model.JXC.Sell_OrderOutHouse>();

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
        public VAN_OA.Model.JXC.Sell_OrderOutHouse ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderOutHouse model = new VAN_OA.Model.JXC.Sell_OrderOutHouse();
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
            ojb = dataReader["IsPrint"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsPrint =Convert.ToBoolean(ojb);
            }

            ojb = dataReader["SellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellTotal = Convert.ToDecimal(ojb);
            }

            
            return model;
        }


        /// <summary>
        /// 销售出库单的数量逻辑如下：需出库的数量=原项目订单该商品的数量+所有该追加项目订单的该商品数量-（项目所有该商品的销售退货数量）-（项目所有该商品的采购退货数量）-该商品已经销售出库的数量
        ///但是 如果销售退货后 又做了采购退货，两部分的退货数量能够计一次（以销售退货的数量为准）。
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="PoNo"></param>
        /// <param name="notOurId"></param>
        /// <returns></returns>
        public decimal GetActGoosTotalNum(int goodId, string PoNo,int notOurId)
        {
            string sql = "";

            if (notOurId != 0)
            {
                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
declare @TuiTotalNum decimal(18,2);
declare @SellTuiTotalNum decimal(18,2);
set @ALLTotalNum=0;
set @TuiTotalNum=0;
select @AllTotalNum=isnull(sum(Num),0) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}' and Sell_OrderOutHouse.Id<>{2};
select @TuiTotalNum=isnull(sum(GoodNum),0) from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID  where Status='通过' and GooId={1} and  PoNo='{0}';
select @SellTuiTotalNum=isnull(sum(GoodNum),0) from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  where Status='通过' and GooId={1} and  PoNo='{0}';
if(@SellTuiTotalNum<>0 and @SellTuiTotalNum<@TuiTotalNum)
begin 
set @ALLTotalNum=@ALLTotalNum+@SellTuiTotalNum -@TuiTotalNum
end
select @ALLTotalNum", PoNo, goodId,notOurId);
            }
            else
            {
                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
declare @TuiTotalNum decimal(18,2);
declare @SellTuiTotalNum decimal(18,2);
set @ALLTotalNum=0;
set @TuiTotalNum=0;
select @AllTotalNum=isnull(sum(Num),0) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}';
select @TuiTotalNum=isnull(sum(GoodNum),0) from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID  where Status='通过' and GooId={1} and  PoNo='{0}';
select @SellTuiTotalNum=isnull(sum(GoodNum),0) from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  where Status='通过' and GooId={1} and  PoNo='{0}';
if(@SellTuiTotalNum<>0 and @SellTuiTotalNum<@TuiTotalNum)
begin 
set @ALLTotalNum=@ALLTotalNum+@SellTuiTotalNum -@TuiTotalNum
end
select @ALLTotalNum", PoNo, goodId);
            }
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }
    }
}
