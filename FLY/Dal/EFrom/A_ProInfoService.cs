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
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.EFrom
{
    public class A_ProInfoService
    {
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.A_ProInfo> GetListArrayByRoleIds(string roleIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select distinct A_ProInfo.pro_Id,A_ProInfo.pro_Type,a_Role_Id from A_ProInfo left join A_ProInfos on A_ProInfo.pro_Id=A_ProInfos.pro_Id 
where a_Role_Id in ({0}) ",roleIds);
             
            List<VAN_OA.Model.EFrom.A_ProInfo> list = new List<VAN_OA.Model.EFrom.A_ProInfo>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.A_ProInfo model = new VAN_OA.Model.EFrom.A_ProInfo();
                        object ojb;
                        ojb = dataReader["pro_Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.pro_Id = (int)ojb;
                        }
                        model.pro_Type = dataReader["pro_Type"].ToString();
                        model.RoleId = Convert.ToInt32(dataReader["a_Role_Id"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.A_ProInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select A_ProInfo.* ,cou from A_ProInfo
left join (select count(*) as cou,pro_Id from A_ProInfos group by pro_Id) as A_ProInfos
on  A_ProInfo.pro_Id=A_ProInfos.pro_Id ");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.A_ProInfo> list = new List<VAN_OA.Model.EFrom.A_ProInfo>();
          
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
        public VAN_OA.Model.EFrom.A_ProInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.A_ProInfo model = new VAN_OA.Model.EFrom.A_ProInfo();
            object ojb;
            ojb = dataReader["pro_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.pro_Id = (int)ojb;
            }
            model.pro_Type = dataReader["pro_Type"].ToString();


            ojb = dataReader["cou"];
            if (ojb != null && ojb != DBNull.Value)
            {
                if (Convert.ToInt32(ojb) > 0)
                {
                    model.IfIDS = true;
                }
                else
                {
                    model.IfIDS = false;
                }
            }
            return model;
        }
    }
}
