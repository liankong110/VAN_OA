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
    public class A_Role_UserService
    {




        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.A_Role_User model)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into A_Role_User(");
            strSql.Append("A_RoleId,User_Id,Role_User_Index,RowState");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.A_RoleId + ",");
            strSql.Append("" + model.User_Id + ",");
            strSql.Append("" + model.Role_User_Index + ",");
            strSql.Append("" + model.RowState + "");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            int result;
            object obj = DBHelp.ExeScalar(strSql.ToString());
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.A_Role_User model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_Role_User set ");
            strSql.Append("A_RoleId=" + model.A_RoleId + ",");
            strSql.Append("User_Id=" + model.User_Id + ",");
            strSql.Append("Role_User_Index=" + model.Role_User_Index + ",");
            strSql.Append("RowState=" + model.RowState + "");
            strSql.Append(" where ID=" + model.ID + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteGetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_Role_User ");
            strSql.Append(" where ID=" + ID + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.A_Role_User GetModelGetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" * ");
            strSql.Append(" from A_Role_User_View ");
            strSql.Append(" where ID=" + ID + " order by Role_User_Index,loginName ");


            VAN_OA.Model.EFrom.A_Role_User model = null;
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
        public List<VAN_OA.Model.EFrom.A_Role_User> GetListArray(string strWhere, int RowState=1)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * ");
            strSql.Append(" FROM A_Role_User_View ");
            if (RowState == 1)
            {
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where RowState=1 and " + strWhere);
                }
            }
            else
            {
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            strSql.Append("  order by Role_User_Index,loginName  ");
            List<VAN_OA.Model.EFrom.A_Role_User> list = new List<VAN_OA.Model.EFrom.A_Role_User>();

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
        public VAN_OA.Model.EFrom.A_Role_User ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.A_Role_User model = new VAN_OA.Model.EFrom.A_Role_User();
            object ojb;
            ojb = dataReader["iD"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            model.UserName = dataReader["loginName"].ToString();
            model.RoleName = dataReader["A_RoleName"].ToString();
            model.LoginName_ID = dataReader["LoginName_ID"].ToString();

            model.Role_User_Index = Convert.ToInt32(dataReader["Role_User_Index"]);
            model.RowState = Convert.ToInt32(dataReader["RowState"]);
            try
            {
                model.UserId = Convert.ToInt32(dataReader["userId"].ToString());
            }
            catch (Exception)
            {
            }
            return model;
        }


    }
}
