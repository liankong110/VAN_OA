﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
 
using System.Text;
using System.Data;


namespace VAN_OA.Dal.JXC
{
    public class Sell_OrderInHousesService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.Sell_OrderInHouses model, SqlCommand objCommand)
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

            if (model.GoodSellPrice != null)
            {
                strSql1.Append("GoodSellPrice,");
                strSql2.Append("" + model.GoodSellPrice + ",");
            }

            if (model.HouseID != null)
            {
                strSql1.Append("HouseID,");
                strSql2.Append("" + model.HouseID + ",");
            }


            if (model.SellOutOrderId != null)
            {
                strSql1.Append("SellOutOrderId,");
                strSql2.Append("" + model.SellOutOrderId + ",");
            }

            if (model.GoodPriceSecond != null)
            {
                strSql1.Append("GoodPriceSecond,");
                strSql2.Append("" + model.GoodPriceSecond + ",");
            }

            if (model.GoodTotalCha != null)
            {
                strSql1.Append("GoodTotalCha,");
                strSql2.Append("" + model.GoodTotalCha + ",");
            }


            
            strSql.Append("insert into Sell_OrderInHouses(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderInHouses model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderInHouses set ");
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

            if (model.GoodSellPrice != null)
            {
                strSql.Append("GoodSellPrice=" + model.GoodSellPrice + ",");
            }


            if (model.GoodTotalCha != null)
            {
                strSql.Append("GoodTotalCha=" + model.GoodTotalCha + ",");
            }

            if (model.GoodPriceSecond != null)
            {
                strSql.Append("GoodPriceSecond=" + model.GoodPriceSecond + ",");
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
            strSql.Append("delete from Sell_OrderInHouses ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderInHouses ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderInHouses ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderInHouses GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,Sell_OrderInHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,HouseID,SellOutOrderId,GoodPriceSecond,GoodTotalCha");
            strSql.Append(" from Sell_OrderInHouses left join TB_Good on TB_Good.GoodId=Sell_OrderInHouses.GooId ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.Sell_OrderInHouses model = null;
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

        public List<VAN_OA.Model.JXC.Sell_OrderInHouses> GetListArrayToList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" RuTime,Status,Ids,Sell_OrderInHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,HouseID,houseName,SellOutOrderId,GoodPriceSecond,GoodTotalCha");
            strSql.Append(" from Sell_OrderInHouses left join Sell_OrderInHouse on Sell_OrderInHouse.id=Sell_OrderInHouses.id  left join TB_Good on TB_Good.GoodId=Sell_OrderInHouses.GooId  left join TB_HouseInfo on TB_HouseInfo.id=HouseID");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }


            List<VAN_OA.Model.JXC.Sell_OrderInHouses> list = new List<VAN_OA.Model.JXC.Sell_OrderInHouses>();

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
                        ojb = dataReader["Status"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Status = ojb.ToString();
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = Convert.ToDateTime(ojb);
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
        public List<VAN_OA.Model.JXC.Sell_OrderInHouses> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" GoodAreaNumber,Ids,Sell_OrderInHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,HouseID,houseName,SellOutOrderId,GoodPriceSecond,GoodTotalCha");
            strSql.Append(" from Sell_OrderInHouses left join TB_Good on TB_Good.GoodId=Sell_OrderInHouses.GooId  left join TB_HouseInfo on TB_HouseInfo.id=HouseID");
           

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.Sell_OrderInHouses> list = new List<VAN_OA.Model.JXC.Sell_OrderInHouses>();

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
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderInHouses ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderInHouses model = new VAN_OA.Model.JXC.Sell_OrderInHouses();
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
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice =Convert.ToDecimal( ojb);
            }
            model.GoodSellPriceTotal = model.GoodSellPrice * model.GoodNum;
            ojb = dataReader["HouseID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseID = (int)ojb;
            }
            ojb = dataReader["houseName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseName = ojb.ToString();
            }

            ojb = dataReader["SellOutOrderId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutOrderId = (int)ojb;
            }

            ojb = dataReader["GoodPriceSecond"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPriceSecond =Convert.ToDecimal( ojb);
            }

            ojb = dataReader["GoodTotalCha"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTotalCha = Convert.ToDecimal( ojb);
            }

            ojb = dataReader["GoodTotalCha"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTotalCha = Convert.ToDecimal(ojb);
            }


            return model;
        }



    }
}
