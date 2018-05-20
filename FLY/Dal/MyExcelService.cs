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
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Model;
using System.Text;
using Newtonsoft.Json;

namespace VAN_OA.Dal
{
    public class MyExcelService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TB_EXCEL model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.Table_Name != null)
            {
                strSql1.Append("Table_Name,");
                strSql2.Append("'" + model.Table_Name + "',");
            }
            if (model.SheetName != null)
            {
                strSql1.Append("SheetName,");
                strSql2.Append("'" + model.SheetName + "',");
            }
            if (model.UserName != null)
            {
                strSql1.Append("UserName,");
                strSql2.Append("'" + model.UserName + "',");
            }

            strSql1.Append("CreateTime,");
            strSql2.Append("getdate(),");

            if (model.UpState != null)
            {
                strSql1.Append("UpState,");
                strSql2.Append("1,");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.FileName != null)
            {
                strSql1.Append("FileName,");
                strSql2.Append("'" + model.FileName + "',");
            }
            if (model.FileType != null)
            {
                strSql1.Append("FileType,");
                strSql2.Append("'" + model.FileType + "',");
            }
            strSql1.Append("IsParent,");
            strSql2.Append("" + model.IsParent + ",");
            strSql.Append("insert into [ExcelData].[dbo].TB_EXCEL(");
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
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            var tableName = DBHelp.ExeScalar("select top 1 Table_Name from [ExcelData].[dbo].[TB_EXCEL] where id=" + id).ToString();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [ExcelData].[dbo].[TB_EXCEL]  ");
            strSql.Append(" where Id=" + id + ";");
            if (!string.IsNullOrEmpty(tableName))
            {
                strSql.Append(" truncate table [ExcelData].[dbo].[" + tableName + "];");
            }

            DBHelp.ExeCommand(strSql.ToString());
        }

        public List<TB_EXCEL> QueryMainExcel(string strWhere)
        {
            List<TB_EXCEL> excels = new List<TB_EXCEL>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name,Id,CreateTime,Table_Name,SheetName,UpState,Remark,FileName,UserName");
            strSql.Append(" FROM [ExcelData].[dbo].[TB_EXCEL] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc ");

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        TB_EXCEL model = new TB_EXCEL();
                        object ojb;
                        ojb = objReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        model.Name = objReader["Name"].ToString();
                        model.Table_Name = objReader["Table_Name"].ToString();
                        model.UserName = objReader["UserName"].ToString();
                        model.UpState = (int)objReader["UpState"];
                        ojb = objReader["SheetName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SheetName = (string)ojb;
                        }

                        ojb = objReader["FileName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FileName = (string)ojb;
                        }
                        ojb = objReader["Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Remark = (string)ojb;
                        }

                        ojb = objReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }

                        excels.Add(model);
                    }
                }
            }

            return excels;
        }

        public TB_EXCEL TopQueryMainExcel(string strWhere)
        {
            TB_EXCEL model = new TB_EXCEL();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Top 1 Name,Id,CreateTime,Table_Name,SheetName,UpState,Remark,FileName,UserName,FileType");
            strSql.Append(" FROM [ExcelData].[dbo].[TB_EXCEL] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc ");

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {

                        object ojb;
                        ojb = objReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        model.Name = objReader["Name"].ToString();
                        model.Table_Name = objReader["Table_Name"].ToString();
                        model.UserName = objReader["UserName"].ToString();
                        model.UpState = (int)objReader["UpState"];
                        ojb = objReader["SheetName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SheetName = (string)ojb;
                        }

                        ojb = objReader["FileName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FileName = (string)ojb;
                        }
                        ojb = objReader["FileType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FileType = (string)ojb;
                        }
                        ojb = objReader["Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Remark = (string)ojb;
                        }

                        ojb = objReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }


                    }
                }
            }

            return model;
        }

        public List<TB_EXCEL> GetMainExcel(string strWhere)
        {
            List<TB_EXCEL> excels = new List<TB_EXCEL>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Name+'-['+[SheetName]+']' AS Name,Id,CreateTime,Table_Name ");
            strSql.Append(" FROM [ExcelData].[dbo].[TB_EXCEL] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc ");

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        excels.Add(ReaderBind(objReader));
                    }
                }
            }

            return excels;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public TB_EXCEL ReaderBind(IDataReader dataReader)
        {
            TB_EXCEL model = new TB_EXCEL();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.Name = dataReader["Name"].ToString();
            model.Table_Name = dataReader["Table_Name"].ToString();

            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
                model.Name += "-[" + model.CreateTime.ToShortDateString() + "]";
            }

            return model;
        }

        public List<MyExcel> GetSonExcel(string table, string strWhere, PagerDomain page)
        {
            List<MyExcel> excels = new List<MyExcel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Ids,ExcelJosnContent ");
            strSql.Append(" FROM [ExcelData].[dbo].[" + table + "] ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}

            strSql = new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), strWhere, " Ids  "));
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();

                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                objCommand.CommandTimeout = 120;
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        MyExcel model = JsonConvert.DeserializeObject<MyExcel>(objReader["ExcelJosnContent"].ToString());
                        object ojb;
                        ojb = objReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        model.Excel = table;
                        excels.Add(model);
                    }
                }
            }

            return excels;
        }


        public List<MyExcel> GetSonExcel_PiPei(string table, string strWhere, PagerDomain page)
        {
            List<MyExcel> excels = new List<MyExcel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Ids ");
            strSql.Append(" FROM [ExcelData].[dbo].[" + table + "] ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}

            strSql = new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), strWhere, " Ids  "));
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();

                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                objCommand.CommandTimeout = 120;
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        MyExcel model = new MyExcel();
                        object ojb;
                        ojb = objReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        excels.Add(model);
                    }
                }
            }

            return excels;
        }
    }
}