using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
 
using System.Text;
using System.Data;


namespace VAN_OA.Dal.JXC
{
    public class CAI_OrderInHousesService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CAI_OrderInHouses model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.id != null)
            {
                strSql1.Append("id,");
                strSql2.Append("" + model.id + ",");
            }
            if (model.GooId != null)
            {
                strSql1.Append("GooId,");
                strSql2.Append("" + model.GooId + ",");
            }
            if (model.GoodNum != null)
            {
                strSql1.Append("GoodNum,");
                strSql2.Append("" + model.GoodNum + ",");
            }

            if (model.GoodPrice != null)
            {
                strSql1.Append("GoodPrice,");
                strSql2.Append("" + model.GoodPrice + ",");
            }
            if (model.GoodRemark != null)
            {
                strSql1.Append("GoodRemark,");
                strSql2.Append("'" + model.GoodRemark + "',");
            }

            if (model.OrderCheckIds != null)
            {
                strSql1.Append("OrderCheckIds,");
                strSql2.Append("'" + model.OrderCheckIds + "',");
            }
            if (model.QingGouPer != null)
            {
                strSql1.Append("QingGouPer,");
                strSql2.Append("'" + model.QingGouPer + "',");
            }

            strSql1.Append("CaiLastTruePrice,");
            strSql2.Append("" + model.CaiLastTruePrice + ",");

            strSql.Append("insert into CAI_OrderInHouses(");
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
        public void Update(VAN_OA.Model.JXC.CAI_OrderInHouses model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_OrderInHouses set ");
            if (model.id != null)
            {
                strSql.Append("id=" + model.id + ",");
            }
            if (model.GooId != null)
            {
                strSql.Append("GooId=" + model.GooId + ",");
            }
            if (model.GoodNum != null)
            {
                strSql.Append("GoodNum=" + model.GoodNum + ",");
            }

            if (model.GoodPrice != null)
            {
                strSql.Append("GoodPrice=" + model.GoodPrice + ",");
            }

            if (model.GoodRemark != null)
            {
                strSql.Append("GoodRemark='" + model.GoodRemark + "',");
            }
            else
            {
                strSql.Append("GoodRemark= null ,");
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
            strSql.Append("delete from CAI_OrderInHouses ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderInHouses ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderInHouses ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderInHouses GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,CAI_OrderInHouses.id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer");
            strSql.Append(" from CAI_OrderInHouses left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CAI_OrderInHouses model = null;
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
        public List<VAN_OA.Model.JXC.CAI_OrderInHouses> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" GoodAreaNumber,Ids,CAI_OrderInHouses.id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer");
            strSql.Append(" from CAI_OrderInHouses left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId ");
           

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderInHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderInHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 销售退货 到 采购退货 
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderOutHouses> GetListArray_SellInToCaiOut_Result(string proNO)
        {
            StringBuilder strSql = new StringBuilder();       
            strSql.AppendFormat(@"select  CAI_OrderOutHouse.ProNo,CAI_OrderOutHouse.Supplier,CAI_OrderOutHouse.PONo,CAI_OrderOutHouse.POName,Ids,CAI_OrderOutHouse.id,
GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer
 from CAI_OrderOutHouse left join CAI_OrderOutHouses  on CAI_OrderOutHouse.id=CAI_OrderOutHouses.Id
 left join TB_Good on TB_Good.GoodId=CAI_OrderOutHouses.GooId  where Remark like '销售退货单号:{0}';",proNO);
         
            List<VAN_OA.Model.JXC.CAI_OrderOutHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CAI_OrderOutHouses model = ReaderBind_Out(dataReader);
                        object ojb;                       
                        ojb = dataReader["PONo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo = Convert.ToString(ojb);
                        }
                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = Convert.ToString(ojb);
                        }
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = Convert.ToString(ojb);
                        }
                        ojb = dataReader["Supplier"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Supplier = Convert.ToString(ojb);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 销售退货 到 采购退货
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderOutHouses> GetListArray_SellInToCaiOut(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CaiLastTruePrice,CAI_OrderInHouse.ProNo,CAI_OrderInHouse.Supplier,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,Ids,CAI_OrderInHouse.id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,totalOrderNum,QingGouPer");
            strSql.Append(" from Cai_POOrderInHouse_Cai_POOrderOutHouse_ListView left join CAI_OrderInHouse on Cai_POOrderInHouse_Cai_POOrderOutHouse_ListView.id=CAI_OrderInHouse.Id");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderOutHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CAI_OrderOutHouses model = ReaderBind_Out(dataReader);
                        object ojb;
                        ojb = dataReader["totalOrderNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum = model.GoodNum - Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["PONo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo = Convert.ToString(ojb);
                        }
                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = Convert.ToString(ojb);
                        }
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = Convert.ToString(ojb);
                        }
                        ojb = dataReader["Supplier"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Supplier = Convert.ToString(ojb);
                        }
                        ojb = dataReader["CaiLastTruePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiLastTruePrice = Convert.ToDecimal(ojb);
                        }
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有入库尚未退货的子信息集合
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderInHouses> GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_ListView(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,totalOrderNum,QingGouPer");
            strSql.Append(" from Cai_POOrderInHouse_Cai_POOrderOutHouse_ListView");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderInHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderInHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CAI_OrderInHouses model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["totalOrderNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum =model.GoodNum- Convert.ToDecimal(ojb);
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
        public VAN_OA.Model.JXC.CAI_OrderInHouses ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderInHouses model = new VAN_OA.Model.JXC.CAI_OrderInHouses();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["GooId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GooId = (int)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }

            model.GoodRemark = dataReader["GoodRemark"].ToString();
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

            ojb = dataReader["OrderCheckIds"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OrderCheckIds =Convert.ToInt32( ojb);
            }

            ojb = dataReader["QingGouPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QingGouPer = Convert.ToString(ojb);
            }
            
          

            return model;
        }

        public VAN_OA.Model.JXC.CAI_OrderOutHouses ReaderBind_Out(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderOutHouses model = new VAN_OA.Model.JXC.CAI_OrderOutHouses();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
                model.OrderCheckIds = model.Ids;
            }
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
                
            }
            ojb = dataReader["GooId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GooId = (int)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }

            model.GoodRemark = dataReader["GoodRemark"].ToString();
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

            //ojb = dataReader["OrderCheckIds"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.OrderCheckIds = Convert.ToInt32(ojb);
            //}

            ojb = dataReader["QingGouPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QingGouPer = Convert.ToString(ojb);
            }



            return model;
        }


    }
}
