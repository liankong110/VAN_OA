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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.BaseInfo
{
    public class Tb_InventoryService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.Tb_Inventory model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.InvNum != null)
            {
                strSql1.Append("InvNum,");
                strSql2.Append("" + model.InvNum + ",");
            }
            if (model.InvUnit != null)
            {
                strSql1.Append("InvUnit,");
                strSql2.Append("'" + model.InvUnit + "',");
            }
            if (model.InvNo != null)
            {
                strSql1.Append("InvNo,");
                strSql2.Append("'" + model.InvNo + "',");
            }
            if (model.GoodCol != null)
            {
                strSql1.Append("GoodCol,");
                strSql2.Append("'" + model.GoodCol + "',");
            }
            if (model.GoodRow != null)
            {
                strSql1.Append("GoodRow,");
                strSql2.Append("'" + model.GoodRow + "',");
            }
            if (model.GoodNumber != null)
            {
                strSql1.Append("GoodNumber,");
                strSql2.Append("'" + model.GoodNumber + "',");
            }
            if (model.GoodArea != null)
            {
                strSql1.Append("GoodArea,");
                strSql2.Append("'" + model.GoodArea + "',");
            }
            if (model.GoodAreaNumber != null)
            {
                strSql1.Append("GoodAreaNumber,");
                strSql2.Append("'" + model.GoodAreaNumber + "',");
            }
            if (model.InvUser != null)
            {
                strSql1.Append("InvUser,");
                strSql2.Append("'" + model.InvUser + "',");
            }
            strSql.Append("insert into Tb_Inventory(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DBHelp.ExeScalar(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.BaseInfo.Tb_Inventory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_Inventory set ");
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.InvNum != null)
            {
                strSql.Append("InvNum=" + model.InvNum + ",");
            }
            if (model.InvUnit != null)
            {
                strSql.Append("InvUnit='" + model.InvUnit + "',");
            }
            if (model.InvNo != null)
            {
                strSql.Append("InvNo='" + model.InvNo + "',");
            }
            if (model.GoodAreaNumber != null)
            {
                strSql.Append("GoodAreaNumber='" + model.GoodAreaNumber + "',");
            }
            if (model.GoodArea != null)
            {
                strSql.Append("GoodArea='" + model.GoodArea + "',");
            }
            if (model.GoodNumber != null)
            {
                strSql.Append("GoodNumber='" + model.GoodNumber + "',");
            }
            if (model.GoodRow != null)
            {
                strSql.Append("GoodRow='" + model.GoodRow + "',");
            }
            if (model.GoodCol != null)
            {
                strSql.Append("GoodCol='" + model.GoodCol + "',");
            }
            if (model.InvUser != null)
            {
                strSql.Append("InvUser='" + model.InvUser + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_Inventory ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());      
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.Tb_Inventory GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber,ID,InvName,InvNum,InvUnit,InvNo,InvUser ");
            strSql.Append(" from Tb_Inventory ");
            strSql.Append(" where ID=" + ID + "");
          
            VAN_OA.Model.BaseInfo.Tb_Inventory model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model = ReaderBind(objReader);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.Tb_Inventory> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber,ID,InvName,InvNum,InvUnit,InvNo,InvUser ");
            strSql.Append(" FROM Tb_Inventory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by InvName ");
            List<VAN_OA.Model.BaseInfo.Tb_Inventory> list = new List<VAN_OA.Model.BaseInfo.Tb_Inventory>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind(objReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.Tb_Inventory> GetListArrayToDdl(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,InvName,InvNo ");
            strSql.Append(" FROM Tb_Inventory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by InvName ");
            List<VAN_OA.Model.BaseInfo.Tb_Inventory> list = new List<VAN_OA.Model.BaseInfo.Tb_Inventory>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.BaseInfo.Tb_Inventory model = new VAN_OA.Model.BaseInfo.Tb_Inventory();
                        object ojb;
                        ojb = dataReader["ID"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ID = (int)ojb;
                        }
                        model.InvName = dataReader["InvName"].ToString();
                      
                        
                        model.InvNo = dataReader["InvNo"].ToString();
                        if (model.InvNo != "")
                        {
                            model.InvName += "---" + model.InvNo;
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
        public VAN_OA.Model.BaseInfo.Tb_Inventory ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.Tb_Inventory model = new VAN_OA.Model.BaseInfo.Tb_Inventory();
            object ojb;
            ojb = dataReader["ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            model.InvName = dataReader["InvName"].ToString();
            ojb = dataReader["InvNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvNum = (decimal)ojb;
            }
            model.InvUnit = dataReader["InvUnit"].ToString();
            model.InvNo = dataReader["InvNo"].ToString();
            model.GoodArea = Convert.ToString(dataReader["GoodArea"]);
            model.GoodNumber = Convert.ToString(dataReader["GoodNumber"]);
            model.GoodRow = Convert.ToString(dataReader["GoodRow"]);
            model.GoodCol = Convert.ToString(dataReader["GoodCol"]);
            model.GoodAreaNumber = Convert.ToString(dataReader["GoodAreaNumber"]);
            model.InvUser = Convert.ToString(dataReader["InvUser"]);
            
            return model;
        }



    }
}
