using System;
using System.Data;
using System.Configuration;
using System.Linq; 
using System.Xml.Linq;
using VAN_OA.Model.OA;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.BaseInfo
{
    public class tb_ComInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.tb_ComInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ComName != null)
            {
                strSql1.Append("ComName,");
                strSql2.Append("'" + model.ComName + "',");
            }
            if (model.NaShuiNo != null)
            {
                strSql1.Append("NaShuiNo,");
                strSql2.Append("'" + model.NaShuiNo + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.ComBrand != null)
            {
                strSql1.Append("ComBrand,");
                strSql2.Append("'" + model.ComBrand + "',");
            }
            if (model.InvoHeader != null)
            {
                strSql1.Append("InvoHeader,");
                strSql2.Append("'" + model.InvoHeader + "',");
            }
            if (model.InvContactPer != null)
            {
                strSql1.Append("InvContactPer,");
                strSql2.Append("'" + model.InvContactPer + "',");
            }
            if (model.InvAddress != null)
            {
                strSql1.Append("InvAddress,");
                strSql2.Append("'" + model.InvAddress + "',");
            }
            if (model.InvTel != null)
            {
                strSql1.Append("InvTel,");
                strSql2.Append("'" + model.InvTel + "',");
            }
            if (model.NaShuiPer != null)
            {
                strSql1.Append("NaShuiPer,");
                strSql2.Append("'" + model.NaShuiPer + "',");
            }
            if (model.brandNo != null)
            {
                strSql1.Append("brandNo,");
                strSql2.Append("'" + model.brandNo + "',");
            }
            if (model.ComTel != null)
            {
                strSql1.Append("ComTel,");
                strSql2.Append("'" + model.ComTel + "',");
            }
            if (model.ComChuanZhen != null)
            {
                strSql1.Append("ComChuanZhen,");
                strSql2.Append("'" + model.ComChuanZhen + "',");
            }
            if (model.ComBusTel != null)
            {
                strSql1.Append("ComBusTel,");
                strSql2.Append("'" + model.ComBusTel + "',");
            }
            if (model.Brand != null)
            {
                strSql1.Append("Brand,");
                strSql2.Append("'" + model.Brand + "',");
            }
            strSql.Append("insert into tb_ComInfo(");
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
        public bool Update(VAN_OA.Model.BaseInfo.tb_ComInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_ComInfo set ");
            if (model.ComName != null)
            {
                strSql.Append("ComName='" + model.ComName + "',");
            }
            else
            {
                strSql.Append("ComName= null ,");
            }
            if (model.NaShuiNo != null)
            {
                strSql.Append("NaShuiNo='" + model.NaShuiNo + "',");
            }
            else
            {
                strSql.Append("NaShuiNo= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.ComBrand != null)
            {
                strSql.Append("ComBrand='" + model.ComBrand + "',");
            }
            else
            {
                strSql.Append("ComBrand= null ,");
            }
            if (model.InvoHeader != null)
            {
                strSql.Append("InvoHeader='" + model.InvoHeader + "',");
            }
            else
            {
                strSql.Append("InvoHeader= null ,");
            }
            if (model.InvContactPer != null)
            {
                strSql.Append("InvContactPer='" + model.InvContactPer + "',");
            }
            else
            {
                strSql.Append("InvContactPer= null ,");
            }
            if (model.InvAddress != null)
            {
                strSql.Append("InvAddress='" + model.InvAddress + "',");
            }
            else
            {
                strSql.Append("InvAddress= null ,");
            }
            if (model.InvTel != null)
            {
                strSql.Append("InvTel='" + model.InvTel + "',");
            }
            else
            {
                strSql.Append("InvTel= null ,");
            }
            if (model.NaShuiPer != null)
            {
                strSql.Append("NaShuiPer='" + model.NaShuiPer + "',");
            }
            else
            {
                strSql.Append("NaShuiPer= null ,");
            }
            if (model.brandNo != null)
            {
                strSql.Append("brandNo='" + model.brandNo + "',");
            }
            else
            {
                strSql.Append("brandNo= null ,");
            }
            if (model.ComTel != null)
            {
                strSql.Append("ComTel='" + model.ComTel + "',");
            }
            else
            {
                strSql.Append("ComTel= null ,");
            }
            if (model.ComChuanZhen != null)
            {
                strSql.Append("ComChuanZhen='" + model.ComChuanZhen + "',");
            }
            else
            {
                strSql.Append("ComChuanZhen= null ,");
            }
            if (model.ComBusTel != null)
            {
                strSql.Append("ComBusTel='" + model.ComBusTel + "',");
            }
            else
            {
                strSql.Append("ComBusTel= null ,");
            }
            strSql.Append("Brand='" + model.Brand + "',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            return DBHelp.ExeCommand(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_ComInfo ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.tb_ComInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" id,ComName,NaShuiNo,Address,ComBrand,InvoHeader,InvContactPer,InvAddress,InvTel,NaShuiPer,brandNo,ComTel,ComChuanZhen,ComBusTel,Brand ");
            strSql.Append(" from tb_ComInfo ");
            strSql.Append(" where id=" + Id + "");


            VAN_OA.Model.BaseInfo.tb_ComInfo model = null;
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
        public List<VAN_OA.Model.BaseInfo.tb_ComInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ComName,NaShuiNo,Address,ComBrand,InvoHeader,InvContactPer,InvAddress,InvTel,NaShuiPer,brandNo,ComTel,ComChuanZhen,ComBusTel,Brand ");
            strSql.Append(" FROM tb_ComInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.BaseInfo.tb_ComInfo> list = new List<VAN_OA.Model.BaseInfo.tb_ComInfo>();
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
        public VAN_OA.Model.BaseInfo.tb_ComInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.tb_ComInfo model = new VAN_OA.Model.BaseInfo.tb_ComInfo();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.ComName = dataReader["ComName"].ToString();
            model.NaShuiNo = dataReader["NaShuiNo"].ToString();
            model.Address = dataReader["Address"].ToString();
            model.ComBrand = dataReader["ComBrand"].ToString();
            model.InvoHeader = dataReader["InvoHeader"].ToString();
            model.InvContactPer = dataReader["InvContactPer"].ToString();
            model.InvAddress = dataReader["InvAddress"].ToString();
            model.InvTel = dataReader["InvTel"].ToString();
            model.NaShuiPer = dataReader["NaShuiPer"].ToString();
            model.brandNo = dataReader["brandNo"].ToString();
            model.ComTel = dataReader["ComTel"].ToString();
            model.ComChuanZhen = dataReader["ComChuanZhen"].ToString();
            model.ComBusTel = dataReader["ComBusTel"].ToString();
            model.Brand = dataReader["Brand"].ToString();
            return model;
        }

    }
}
