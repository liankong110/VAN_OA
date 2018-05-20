using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class TB_HouseInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TB_HouseInfo model)
        {

            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(houseNo),3))+1))),3) FROM  TB_HouseInfo");
            string MaxNo = "";
            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxNo =  objMax.ToString();
            }
            else
            {
                MaxNo = "001";
            }

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            strSql1.Append("houseNo,");
            strSql2.Append("'" + MaxNo + "',");

            
            if (model.houseName != null)
            {
                strSql1.Append("houseName,");
                strSql2.Append("'" + model.houseName + "',");
            }
            if (model.houseRemark != null)
            {
                strSql1.Append("houseRemark,");
                strSql2.Append("'" + model.houseRemark + "',");
            }
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            
            strSql1.Append("CreateTime,");
            strSql2.Append("getdate(),");


            strSql1.Append("IfDefault,");
            strSql2.Append("" + (model.IfDefault?1:0).ToString() + ",");

            
            strSql.Append("insert into TB_HouseInfo(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TB_HouseInfo model)
        {

            StringBuilder strSql = new StringBuilder();

            if (model.IfDefault == true)
            {
                strSql.Append("update TB_HouseInfo set IfDefault=0;");
                
            }
            strSql.Append("update TB_HouseInfo set ");
           
            if (model.houseName != null)
            {
                strSql.Append("houseName='" + model.houseName + "',");
            }
            if (model.houseRemark != null)
            {
                strSql.Append("houseRemark='" + model.houseRemark + "',");
            }
            else
            {
                strSql.Append("houseRemark= null ,");
            }
            if (model.CreateUserId != null)
            {
                strSql.Append("CreateUserId=" + model.CreateUserId + ",");
            }
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime=getdate(),");
            }

            strSql.Append("IfDefault=" + (model.IfDefault ? 1 :0).ToString() + ",");
           
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_HouseInfo ");
            strSql.Append(" where id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_HouseInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();

          
            strSql.Append("select   ");
            strSql.Append("TB_HouseInfo.id,houseNo,houseName,houseRemark,CreateUserId,CreateTime,tb_User.loginName,IfDefault ");
            strSql.Append(" from TB_HouseInfo left join tb_User on tb_User.ID=TB_HouseInfo.CreateUserId ");

            strSql.Append(" where TB_HouseInfo.id=" + id + "");            

            VAN_OA.Model.BaseInfo.TB_HouseInfo model = null;
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
        public List<VAN_OA.Model.BaseInfo.TB_HouseInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("TB_HouseInfo.id,houseNo,houseName,houseRemark,CreateUserId,CreateTime,tb_User.loginName,IfDefault ");
            strSql.Append(" from TB_HouseInfo left join tb_User on tb_User.ID=TB_HouseInfo.CreateUserId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by CreateTime desc");
            List<VAN_OA.Model.BaseInfo.TB_HouseInfo> list = new List<VAN_OA.Model.BaseInfo.TB_HouseInfo>();

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
        public VAN_OA.Model.BaseInfo.TB_HouseInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_HouseInfo model = new VAN_OA.Model.BaseInfo.TB_HouseInfo();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.houseNo = dataReader["houseNo"].ToString();
            model.houseName = dataReader["houseName"].ToString();
            model.houseRemark = dataReader["houseRemark"].ToString();
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

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserName = ojb.ToString();
            }

            ojb = dataReader["IfDefault"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfDefault = Convert.ToBoolean(ojb);
            } 


            return model;
        }



    }
}
