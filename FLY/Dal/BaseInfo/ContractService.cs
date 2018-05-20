using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class ContractService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.Contract model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            string prefix = "";
            if (model.Contract_Type == 1)
            {
                prefix = "C";
            }
            else
            {
                prefix = "X";
            }
            string Contract_ProNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Contract_ProNo),4))+1))),4) FROM  Contract where Contract_ProNo like '{0}{1}%'", prefix, model.Contract_Year);

            
            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                Contract_ProNo = prefix + model.Contract_Year.ToString() + objMax.ToString();
            }
            else
            {
                Contract_ProNo = prefix + model.Contract_Year.ToString() + "0001";
            }

            strSql1.Append("Contract_ProNo,");
            strSql2.Append("'" + Contract_ProNo + "',");

            strSql1.Append("Contract_IsRequire,");
            strSql2.Append("" + (model.Contract_IsRequire?1:0) + ",");

            if (model.Contract_Type != null)
            {
                strSql1.Append("Contract_Type,");
                strSql2.Append("" + model.Contract_Type + ",");
            }
            if (model.Contract_Use != null)
            {
                strSql1.Append("Contract_Use,");
                strSql2.Append("'" + model.Contract_Use + "',");
            }
            if (model.Contract_No != null)
            {
                strSql1.Append("Contract_No,");
                strSql2.Append("'" + model.Contract_No + "',");
            }
            if (model.Contract_Unit != null)
            {
                strSql1.Append("Contract_Unit,");
                strSql2.Append("'" + model.Contract_Unit + "',");
            }
            if (model.Contract_Name != null)
            {
                strSql1.Append("Contract_Name,");
                strSql2.Append("'" + model.Contract_Name + "',");
            }
            if (model.Contract_Summary != null)
            {
                strSql1.Append("Contract_Summary,");
                strSql2.Append("'" + model.Contract_Summary + "',");
            }
            if (model.Contract_Total != null)
            {
                strSql1.Append("Contract_Total,");
                strSql2.Append("" + model.Contract_Total + ",");
            }
            if (model.Contract_Date != null)
            {
                strSql1.Append("Contract_Date,");
                strSql2.Append("'" + model.Contract_Date + "',");
            }
            if (model.Contract_PageCount != null)
            {
                strSql1.Append("Contract_PageCount,");
                strSql2.Append("" + model.Contract_PageCount + ",");
            }
            if (model.Contract_AllCount != null)
            {
                strSql1.Append("Contract_AllCount,");
                strSql2.Append("" + model.Contract_AllCount + ",");
            }
            if (model.Contract_BCount != null)
            {
                strSql1.Append("Contract_BCount,");
                strSql2.Append("" + model.Contract_BCount + ",");
            }
            if (model.PONo != null)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }
            if (model.Contract_Brokerage != null)
            {
                strSql1.Append("Contract_Brokerage,");
                strSql2.Append("'" + model.Contract_Brokerage + "',");
            }
            if (model.Contract_IsSign != null)
            {
                strSql1.Append("Contract_IsSign,");
                strSql2.Append("" + (model.Contract_IsSign ? 1 : 0) + ",");
            }
            if (model.Contract_Local != null)
            {
                strSql1.Append("Contract_Local,");
                strSql2.Append("'" + model.Contract_Local + "',");
            }
            if (model.Contract_Year != null)
            {
                strSql1.Append("Contract_Year,");
                strSql2.Append("" + model.Contract_Year + ",");
            }
            if (model.Contract_Month != null)
            {
                strSql1.Append("Contract_Month,");
                strSql2.Append("" + model.Contract_Month + ",");
            }
            if (model.Contract_Remark != null)
            {
                strSql1.Append("Contract_Remark,");
                strSql2.Append("'" + model.Contract_Remark + "',");
            }
            strSql.Append("insert into Contract(");
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
        public bool Update(VAN_OA.Model.BaseInfo.Contract model,string oldType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Contract set ");

            if (oldType != model.Contract_Type.ToString())
            {
                string prefix = "";
                if (model.Contract_Type == 1)
                {
                    prefix = "C";
                }
                else
                {
                    prefix = "X";
                }
                string Contract_ProNo = "";
                string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Contract_ProNo),4))+1))),4) FROM  Contract where Contract_ProNo like '{0}{1}%'", prefix, model.Contract_Year);


                object objMax = DBHelp.ExeScalar(sql);
                if (objMax != null && objMax.ToString() != "")
                {
                    Contract_ProNo = prefix + model.Contract_Year.ToString() + objMax.ToString();
                }
                else
                {
                    Contract_ProNo = prefix + model.Contract_Year.ToString() + "0001";
                } 

                strSql.Append("Contract_ProNo='" + Contract_ProNo + "',");
            }
           
            if (model.Contract_Type != null)
            {
                strSql.Append("Contract_Type=" + model.Contract_Type + ",");
            }
            if (model.Contract_Use != null)
            {
                strSql.Append("Contract_Use='" + model.Contract_Use + "',");
            }
            if (model.Contract_No != null)
            {
                strSql.Append("Contract_No='" + model.Contract_No + "',");
            }
            if (model.Contract_Unit != null)
            {
                strSql.Append("Contract_Unit='" + model.Contract_Unit + "',");
            }
            if (model.Contract_Name != null)
            {
                strSql.Append("Contract_Name='" + model.Contract_Name + "',");
            }
            if (model.Contract_Summary != null)
            {
                strSql.Append("Contract_Summary='" + model.Contract_Summary + "',");
            }
            if (model.Contract_Total != null)
            {
                strSql.Append("Contract_Total=" + model.Contract_Total + ",");
            }
            if (model.Contract_Date != null)
            {
                strSql.Append("Contract_Date='" + model.Contract_Date + "',");
            }
            if (model.Contract_PageCount != null)
            {
                strSql.Append("Contract_PageCount=" + model.Contract_PageCount + ",");
            }
            if (model.Contract_AllCount != null)
            {
                strSql.Append("Contract_AllCount=" + model.Contract_AllCount + ",");
            }
            if (model.Contract_BCount != null)
            {
                strSql.Append("Contract_BCount=" + model.Contract_BCount + ",");
            }
            if (model.PONo != null)
            {
                strSql.Append("PONo='" + model.PONo + "',");
            }
            if (model.AE != null)
            {
                strSql.Append("AE='" + model.AE + "',");
            }
            if (model.Contract_Brokerage != null)
            {
                strSql.Append("Contract_Brokerage='" + model.Contract_Brokerage + "',");
            }
            if (model.Contract_IsSign != null)
            {
                strSql.Append("Contract_IsSign=" + (model.Contract_IsSign ? 1 : 0) + ",");
            }
            if (model.Contract_IsRequire != null)
            {
                strSql.Append("Contract_IsRequire=" + (model.Contract_IsRequire ? 1 : 0) + ",");
            }
            if (model.Contract_Local != null)
            {
                strSql.Append("Contract_Local='" + model.Contract_Local + "',");
            }
            if (model.Contract_Year != null)
            {
                strSql.Append("Contract_Year=" + model.Contract_Year + ",");
            }
            if (model.Contract_Month != null)
            {
                strSql.Append("Contract_Month=" + model.Contract_Month + ",");
            }
            if (model.Contract_Remark != null)
            {
                strSql.Append("Contract_Remark='" + model.Contract_Remark + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Contract ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.Contract GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select Contract_ProNo,Contract_IsRequire,Contract_AllCount,Id,Contract_Type,Contract_Use,Contract_No,Contract_Unit,Contract_Name,Contract_Summary,Contract_Total,Contract_Date,Contract_PageCount,Contract_BCount,PONo,AE,Contract_Brokerage,Contract_IsSign,Contract_Local,Contract_Year,Contract_Month,Contract_Remark ");
            strSql.Append(" from Contract ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.Contract model = null;
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
        public List<VAN_OA.Model.BaseInfo.Contract> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Contract_ProNo,Contract_IsRequire,Contract_AllCount,Id,Contract_Type,Contract_Use,Contract_No,Contract_Unit,Contract_Name,Contract_Summary,Contract_Total,Contract_Date,Contract_PageCount,Contract_BCount,PONo,AE,Contract_Brokerage,Contract_IsSign,Contract_Local,Contract_Year,Contract_Month,Contract_Remark ");
            strSql.Append(" from Contract ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.Contract> list = new List<VAN_OA.Model.BaseInfo.Contract>();

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
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.Contract ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.Contract model = new VAN_OA.Model.BaseInfo.Contract();
            object ojb; 
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["Contract_Type"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_Type = (int)ojb;
            }
            model.Contract_Use = dataReader["Contract_Use"].ToString();
            model.Contract_No = dataReader["Contract_No"].ToString();
            model.Contract_Unit = dataReader["Contract_Unit"].ToString();
            model.Contract_Name = dataReader["Contract_Name"].ToString();
            model.Contract_Summary = dataReader["Contract_Summary"].ToString();
            ojb = dataReader["Contract_Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_Total = (decimal)ojb;
            }
            ojb = dataReader["Contract_Date"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_Date = (DateTime)ojb;
            }
            ojb = dataReader["Contract_PageCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_PageCount = (int)ojb;
            }
            ojb = dataReader["Contract_AllCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_AllCount = (int)ojb;
            }
            
            ojb = dataReader["Contract_BCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_BCount = (int)ojb;
            }
            model.PONo = dataReader["PONo"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.Contract_Brokerage = dataReader["Contract_Brokerage"].ToString();
            ojb = dataReader["Contract_IsSign"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_IsSign = (bool)ojb;
            }
            model.Contract_Local = dataReader["Contract_Local"].ToString();
            ojb = dataReader["Contract_Year"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_Year = (int)ojb;
            }
            ojb = dataReader["Contract_Month"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Contract_Month = (int)ojb;
            }
            model.Contract_Remark = dataReader["Contract_Remark"].ToString();
            model.Contract_ProNo = dataReader["Contract_ProNo"].ToString();
            model.Contract_IsRequire = Convert.ToBoolean(dataReader["Contract_IsRequire"]);
            
            return model;
        }



    }
}