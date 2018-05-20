using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class TB_CompanyService
    {
        public string GetAllE_No()
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ComId),4))+1))),4) FROM  TB_Company where ComId like '{0}%';",
                DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TB_Company model)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            
            
                strSql1.Append("ComId,");
                strSql2.Append("'" + GetAllE_No() + "',");
            
            if (model.ComCode != null)
            {
                strSql1.Append("ComCode,");
                strSql2.Append("'" + model.ComCode + "',");
            }
            if (model.ComName != null)
            {
                strSql1.Append("ComName,");
                strSql2.Append("'" + model.ComName + "',");
            }
            if (model.ComSimpName != null)
            {
                strSql1.Append("ComSimpName,");
                strSql2.Append("'" + model.ComSimpName + "',");
            }
            strSql1.Append("OrderByIndex,");
            strSql2.Append("" + model.OrderByIndex + ",");
            if (model.ZhuSuo != null)
            {
                strSql1.Append("ZhuSuo,");
                strSql2.Append("'" + model.ZhuSuo + "',");
            }
            if (model.LeiXing != null)
            {
                strSql1.Append("LeiXing,");
                strSql2.Append("'" + model.LeiXing + "',");
            }
            if (model.DianHua != null)
            {
                strSql1.Append("DianHua,");
                strSql2.Append("'" + model.DianHua + "',");
            }
            if (model.ChuanZhen != null)
            {
                strSql1.Append("ChuanZhen,");
                strSql2.Append("'" + model.ChuanZhen + "',");
            }
            if (model.XinYongCode != null)
            {
                strSql1.Append("XinYongCode,");
                strSql2.Append("'" + model.XinYongCode + "',");
            }
            if (model.FaRen != null)
            {
                strSql1.Append("FaRen,");
                strSql2.Append("'" + model.FaRen + "',");
            }
            if (model.ZhuCeZiBen != null)
            {
                strSql1.Append("ZhuCeZiBen,");
                strSql2.Append("'" + model.ZhuCeZiBen + "',");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.StartTime != null)
            {
                strSql1.Append("StartTime,");
                strSql2.Append("'" + model.StartTime + "',");
            }
            if (model.EndTime != null)
            {
                strSql1.Append("EndTime,");
                strSql2.Append("'" + model.EndTime + "',");
            }
            if (model.FanWei != null)
            {
                strSql1.Append("FanWei,");
                strSql2.Append("'" + model.FanWei + "',");
            }
            if (model.KaiHuHang != null)
            {
                strSql1.Append("KaiHuHang,");
                strSql2.Append("'" + model.KaiHuHang + "',");
            }
            if (model.KaHao != null)
            {
                strSql1.Append("KaHao,");
                strSql2.Append("'" + model.KaHao + "',");
            }
            if (model.Email != null)
            {
                strSql1.Append("Email,");
                strSql2.Append("'" + model.Email + "',");
            }
            if (model.ComUrl != null)
            {
                strSql1.Append("ComUrl,");
                strSql2.Append("'" + model.ComUrl + "',");
            }
            strSql.Append("insert into TB_Company(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TB_Company model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Company set ");
            if (model.ComId != null)
            {
                strSql.Append("ComId='" + model.ComId + "',");
            }
            if (model.ComCode != null)
            {
                strSql.Append("ComCode='" + model.ComCode + "',");
            }
            if (model.ComName != null)
            {
                strSql.Append("ComName='" + model.ComName + "',");
            }
            if (model.ComSimpName != null)
            {
                strSql.Append("ComSimpName='" + model.ComSimpName + "',");
            }
            if (model.ZhuSuo != null)
            {
                strSql.Append("ZhuSuo='" + model.ZhuSuo + "',");
            }
            if (model.LeiXing != null)
            {
                strSql.Append("LeiXing='" + model.LeiXing + "',");
            }
            if (model.DianHua != null)
            {
                strSql.Append("DianHua='" + model.DianHua + "',");
            }
            if (model.ChuanZhen != null)
            {
                strSql.Append("ChuanZhen='" + model.ChuanZhen + "',");
            }
            if (model.XinYongCode != null)
            {
                strSql.Append("XinYongCode='" + model.XinYongCode + "',");
            }
            if (model.FaRen != null)
            {
                strSql.Append("FaRen='" + model.FaRen + "',");
            }
            if (model.ZhuCeZiBen != null)
            {
                strSql.Append("ZhuCeZiBen='" + model.ZhuCeZiBen + "',");
            }
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime='" + model.CreateTime + "',");
            }
            if (model.StartTime != null)
            {
                strSql.Append("StartTime='" + model.StartTime + "',");
            }
            if (model.EndTime != null)
            {
                strSql.Append("EndTime='" + model.EndTime + "',");
            }
            if (model.FanWei != null)
            {
                strSql.Append("FanWei='" + model.FanWei + "',");
            }
            if (model.KaiHuHang != null)
            {
                strSql.Append("KaiHuHang='" + model.KaiHuHang + "',");
            }
            if (model.KaHao != null)
            {
                strSql.Append("KaHao='" + model.KaHao + "',");
            }
            if (model.Email != null)
            {
                strSql.Append("Email='" + model.Email + "',");
            }
            if (model.ComUrl != null)
            {
                strSql.Append("ComUrl='" + model.ComUrl + "',");
            }
            strSql.Append("OrderByIndex=" + model.OrderByIndex + ",");
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
            strSql.Append("delete from TB_Company ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_Company GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,ComId,ComCode,ComName,ComSimpName,OrderByIndex,ZhuSuo,LeiXing,DianHua,ChuanZhen,XinYongCode,FaRen,ZhuCeZiBen,CreateTime,StartTime,EndTime,FanWei,KaiHuHang,KaHao,Email,ComUrl ");
            strSql.Append(" FROM TB_Company ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.TB_Company model = null;
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
        public List<VAN_OA.Model.BaseInfo.TB_Company> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,ComId,ComCode,ComName,ComSimpName,OrderByIndex,ZhuSuo,LeiXing,DianHua,ChuanZhen,XinYongCode,FaRen,ZhuCeZiBen,CreateTime,StartTime,EndTime,FanWei,KaiHuHang,KaHao,Email,ComUrl ");
            strSql.Append(" FROM TB_Company ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by OrderByIndex desc ");
            List<VAN_OA.Model.BaseInfo.TB_Company> list = new List<VAN_OA.Model.BaseInfo.TB_Company>();

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
        public VAN_OA.Model.BaseInfo.TB_Company ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_Company model = new VAN_OA.Model.BaseInfo.TB_Company();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ComId = dataReader["ComId"].ToString();
            model.ComCode = dataReader["ComCode"].ToString();
            model.ComName = dataReader["ComName"].ToString();
            model.ComSimpName = dataReader["ComSimpName"].ToString();
            model.OrderByIndex = (int)dataReader["OrderByIndex"];
            model.ZhuSuo = dataReader["ZhuSuo"].ToString();
            model.LeiXing = dataReader["LeiXing"].ToString();
            model.DianHua = dataReader["DianHua"].ToString();
            model.ChuanZhen = dataReader["ChuanZhen"].ToString();
            model.XinYongCode = dataReader["XinYongCode"].ToString();
            model.FaRen = dataReader["FaRen"].ToString();
            model.ZhuCeZiBen = dataReader["ZhuCeZiBen"].ToString();
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["StartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StartTime = (DateTime)ojb;
            }
            ojb = dataReader["EndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EndTime = (DateTime)ojb;
            }
            model.FanWei = dataReader["FanWei"].ToString();
            model.KaiHuHang = dataReader["KaiHuHang"].ToString();
            model.KaHao = dataReader["KaHao"].ToString();
            model.Email = dataReader["Email"].ToString();
            model.ComUrl = dataReader["ComUrl"].ToString();
            return model;
        }
    }
}
