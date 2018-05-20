using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class PetitionsService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.Petitions model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            string prefix = "Q";
           
            string Number = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Number),4))+1))),4) FROM  Petitions where Number like '{0}{1}%'", prefix, model.L_Year);


            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                Number = prefix + model.L_Year.ToString() + objMax.ToString();
            }
            else
            {
                Number = prefix + model.L_Year.ToString() + "0001";
            }

            strSql1.Append("IsRequire,");
            strSql2.Append("" + (model.IsRequire ? 1 : 0) + ",");

            strSql1.Append("Number,");
            strSql2.Append("'" + Number + "',");
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + model.Type + "',");
            }

            if (model.OldIndex != null)
            {
                strSql1.Append("OldIndex,");
                strSql2.Append("'" + model.OldIndex + "',");
            }

            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.SalesUnit != null)
            {
                strSql1.Append("SalesUnit,");
                strSql2.Append("'" + model.SalesUnit + "',");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.Summary != null)
            {
                strSql1.Append("Summary,");
                strSql2.Append("'" + model.Summary + "',");
            }
            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }
            if (model.SignDate != null)
            {
                strSql1.Append("SignDate,");
                strSql2.Append("'" + model.SignDate + "',");
            }
            if (model.SumPages != null)
            {
                strSql1.Append("SumPages,");
                strSql2.Append("" + model.SumPages + ",");
            }
            if (model.SumCount != null)
            {
                strSql1.Append("SumCount,");
                strSql2.Append("" + model.SumCount + ",");
            }
            if (model.BCount != null)
            {
                strSql1.Append("BCount,");
                strSql2.Append("" + model.BCount + ",");
            }
            if (model.PoNo != null)
            {
                strSql1.Append("PoNo,");
                strSql2.Append("'" + model.PoNo + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }
            if (model.Handler != null)
            {
                strSql1.Append("Handler,");
                strSql2.Append("'" + model.Handler + "',");
            }
            if (model.IsColse != null)
            {
                strSql1.Append("IsColse,");
                strSql2.Append("" + (model.IsColse ? 1 : 0) + ",");
            }
            if (model.Local != null)
            {
                strSql1.Append("Local,");
                strSql2.Append("'" + model.Local + "',");
            }
            if (model.L_Year != null)
            {
                strSql1.Append("L_Year,");
                strSql2.Append("" + model.L_Year + ",");
            }
            if (model.L_Month != null)
            {
                strSql1.Append("L_Month,");
                strSql2.Append("" + model.L_Month + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into Petitions(");
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

        public int Add(VAN_OA.Model.BaseInfo.Petitions model, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            string prefix = "Q";

            string Number = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Number),4))+1))),4) FROM  Petitions where Number like '{0}{1}%'", prefix, model.L_Year);


            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                Number = prefix + model.L_Year.ToString() + objMax.ToString();
            }
            else
            {
                Number = prefix + model.L_Year.ToString() + "0001";
            }

            strSql1.Append("IsRequire,");
            strSql2.Append("" + (model.IsRequire ? 1 : 0) + ",");

            strSql1.Append("Number,");
            strSql2.Append("'" + model.Number + "',");
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + model.Type + "',");
            }

            if (model.OldIndex != null)
            {
                strSql1.Append("OldIndex,");
                strSql2.Append("'" + model.OldIndex + "',");
            }

            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.SalesUnit != null)
            {
                strSql1.Append("SalesUnit,");
                strSql2.Append("'" + model.SalesUnit + "',");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.Summary != null)
            {
                strSql1.Append("Summary,");
                strSql2.Append("'" + model.Summary + "',");
            }
            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }
            if (model.SignDate != null)
            {
                strSql1.Append("SignDate,");
                strSql2.Append("'" + model.SignDate + "',");
            }
            if (model.SumPages != null)
            {
                strSql1.Append("SumPages,");
                strSql2.Append("" + model.SumPages + ",");
            }
            if (model.SumCount != null)
            {
                strSql1.Append("SumCount,");
                strSql2.Append("" + model.SumCount + ",");
            }
            if (model.BCount != null)
            {
                strSql1.Append("BCount,");
                strSql2.Append("" + model.BCount + ",");
            }
            if (model.PoNo != null)
            {
                strSql1.Append("PoNo,");
                strSql2.Append("'" + model.PoNo + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }
            if (model.Handler != null)
            {
                strSql1.Append("Handler,");
                strSql2.Append("'" + model.Handler + "',");
            }
            if (model.IsColse != null)
            {
                strSql1.Append("IsColse,");
                strSql2.Append("" + (model.IsColse ? 1 : 0) + ",");
            }
            if (model.Local != null)
            {
                strSql1.Append("Local,");
                strSql2.Append("'" + model.Local + "',");
            }
            if (model.L_Year != null)
            {
                strSql1.Append("L_Year,");
                strSql2.Append("" + model.L_Year + ",");
            }
            if (model.L_Month != null)
            {
                strSql1.Append("L_Month,");
                strSql2.Append("" + model.L_Month + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }

            strSql.Append("insert into Petitions(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            sqlCommand.CommandText = strSql.ToString();
            object obj = sqlCommand.ExecuteScalar();
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
        public bool Update(VAN_OA.Model.BaseInfo.Petitions model,string oldYear)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Petitions set ");

            if (oldYear != model.L_Year.ToString())
            {
                string prefix = "Q";

                string Number = "";
                string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Number),4))+1))),4) FROM  Petitions where Number like '{0}{1}%'", prefix, model.L_Year);

                object objMax = DBHelp.ExeScalar(sql);
                if (objMax != null && objMax.ToString() != "")
                {
                    Number = prefix + model.L_Year.ToString() + objMax.ToString();
                }
                else
                {
                    Number = prefix + model.L_Year.ToString() + "0001";
                }                 
                strSql.Append("Number='" + Number + "',");
            }

            strSql.Append("IsRequire=" + (model.IsRequire ? 1 : 0) + ",");

            if (model.Type != null)
            {
                strSql.Append("Type='" + model.Type + "',");
            }
           
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.SalesUnit != null)
            {
                strSql.Append("SalesUnit='" + model.SalesUnit + "',");
            }
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            if (model.Summary != null)
            {
                strSql.Append("Summary='" + model.Summary + "',");
            }
            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            if (model.SignDate != null)
            {
                strSql.Append("SignDate='" + model.SignDate + "',");
            }
            if (model.SumPages != null)
            {
                strSql.Append("SumPages=" + model.SumPages + ",");
            }
            if (model.SumCount != null)
            {
                strSql.Append("SumCount=" + model.SumCount + ",");
            }
            if (model.BCount != null)
            {
                strSql.Append("BCount=" + model.BCount + ",");
            }
            if (model.PoNo != null)
            {
                strSql.Append("PoNo='" + model.PoNo + "',");
            }
            if (model.AE != null)
            {
                strSql.Append("AE='" + model.AE + "',");
            }
            if (model.Handler != null)
            {
                strSql.Append("Handler='" + model.Handler + "',");
            }
            if (model.IsColse != null)
            {
                strSql.Append("IsColse=" + (model.IsColse ? 1 : 0) + ",");
            }
            if (model.Local != null)
            {
                strSql.Append("Local='" + model.Local + "',");
            }
            if (model.L_Year != null)
            {
                strSql.Append("L_Year=" + model.L_Year + ",");
            }
            if (model.L_Month != null)
            {
                strSql.Append("L_Month=" + model.L_Month + ",");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
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
            strSql.Append("delete from Petitions ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.Petitions GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select OldIndex,IsRequire,Id,Type,Number,GuestName,SalesUnit,Name,Summary,Total,SignDate,SumPages,SumCount,BCount,PoNo,AE,Handler,IsColse,Local,L_Year,L_Month,Remark ");
            strSql.Append(" from Petitions ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.Petitions model = null;
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
        public List<VAN_OA.Model.BaseInfo.Petitions> GetListArray(string strWhere,string totalWhere,string supplier)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select OldIndex,IsRequire,Id,Type,Number,GuestName,SalesUnit,Name,Summary,Total,SignDate,SumPages,SumCount,BCount,PoNo,AE,Handler,IsColse,Local,L_Year,L_Month,Remark ");
            strSql.Append(" from Petitions ");

            if (!string.IsNullOrEmpty(totalWhere)&&totalWhere!= "1=1")
            {
                strWhere += string.Format(@" and PoNo in(select PONo from[CAI_POOrder] left join[dbo].[CAI_POCai]
on[CAI_POOrder].Id =[CAI_POCai].Id where[CAI_POOrder].Status = '通过' group by PONo having {0})",totalWhere);
            }
            if (!string.IsNullOrEmpty(supplier))
            {
                strWhere += string.Format(@" and PoNo in(select PONo from[CAI_POOrder] left join[dbo].[CAI_POCai]
on[CAI_POOrder].Id =[CAI_POCai].Id where[CAI_POOrder].Status = '通过' and lastSupplier like '%{0}%')", supplier);
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.Petitions> list = new List<VAN_OA.Model.BaseInfo.Petitions>();
            int No = 1;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);
                        model.No = No++;
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public DataTable GetDataTables(string strWhere, string totalWhere, string supplier)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select OldIndex,IsRequire,Id,Type,Number,GuestName,SalesUnit,Name,Summary,Total,SignDate,SumPages,SumCount,BCount,PoNo,AE,Handler,IsColse,Local,L_Year,L_Month,Remark ");
            strSql.Append(" from Petitions ");

            if (!string.IsNullOrEmpty(totalWhere) && totalWhere != "1=1")
            {
                strWhere += string.Format(@" and PoNo in(select PONo from[CAI_POOrder] left join[dbo].[CAI_POCai]
on[CAI_POOrder].Id =[CAI_POCai].Id where[CAI_POOrder].Status = '通过' group by PONo having {0})", totalWhere);
            }
            if (!string.IsNullOrEmpty(supplier))
            {
                strWhere += string.Format(@" and PoNo in(select PONo from[CAI_POOrder] left join[dbo].[CAI_POCai]
on[CAI_POOrder].Id =[CAI_POCai].Id where[CAI_POOrder].Status = '通过' and lastSupplier like '%{0}%')", supplier);
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
             
            return DBHelp.getDataTable(strSql.ToString());
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.Petitions ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.Petitions model = new VAN_OA.Model.BaseInfo.Petitions();
            object ojb; 
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.Type = dataReader["Type"].ToString();
            model.Number = dataReader["Number"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            model.SalesUnit = dataReader["SalesUnit"].ToString();
            model.Name = dataReader["Name"].ToString();
            model.Summary = dataReader["Summary"].ToString();
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            ojb = dataReader["SignDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SignDate = (DateTime)ojb;
            }
            ojb = dataReader["SumPages"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SumPages = (int)ojb;
            }
            ojb = dataReader["SumCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SumCount = (int)ojb;
            }
            ojb = dataReader["BCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BCount = (int)ojb;
            }
            model.PoNo = dataReader["PoNo"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.Handler = dataReader["Handler"].ToString();
            ojb = dataReader["IsColse"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsColse = (bool)ojb;
            }
            model.Local = dataReader["Local"].ToString();
            ojb = dataReader["L_Year"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.L_Year = (int)ojb;
            }
            ojb = dataReader["L_Month"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.L_Month = (int)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            model.IsRequire = Convert.ToBoolean(dataReader["IsRequire"]);
            ojb = dataReader["OldIndex"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OldIndex = ojb.ToString();
            }            
            return model;
        }



    }
}