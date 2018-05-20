namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    [Serializable]
    public class RoleUserService
    {
        public bool addRole_User(int roleId, int userId)
        {
            return DBHelp.ExeCommand(string.Format("insert into role_user values({0},{1})", roleId, userId));
        }

        public bool addRole_User(int roleId, int userId, List<RoleUser> roleUsers)
        {
            bool result = false;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    string sql = string.Format("delete from role_user where userId={0}", userId);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    for (int i = 0; i < roleUsers.Count; i++)
                    {
                        sql = string.Format("insert into role_user values({0},{1})", roleUsers[i].RoleId, userId);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                    tan.Commit();
                    result = true;
                }
                catch (Exception)
                {
                    tan.Rollback();
                }
                conn.Close();
            }
            return result;
        }

        public bool deleteRole_UserByUserId(int userId)
        {
            return DBHelp.ExeCommand(string.Format("delete from role_user where userId={0}", userId));
        }

        public List<RoleUser> getAllRole_Users()
        {
            string sql = string.Format("select * from role_user ", new object[0]);
            return this.getRole_UsersBySql(sql);
        }

        private List<RoleUser> getRole_UsersBySql(string sql)
        {
            List<RoleUser> role_Users = new List<RoleUser>();
            DataTable dt = DBHelp.getDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                RoleUser role_user = new RoleUser();
                int roleId = Convert.ToInt32(dr["RoleID"]);
                role_user.Role = new RoleService().getRoleById(roleId);
                role_user._User = new SysUserService().getUserByUserId(Convert.ToInt32(dr["userId"]));
                role_Users.Add(role_user);
            }
            return role_Users;
        }

        public List<RoleUser> getRoleIdByUserId(int user_id)
        {
            List<RoleUser> RoleList = new List<RoleUser>();
            string sql = string.Format("select roleid from role_user where userId={0}", user_id);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        RoleUser role_user = new RoleUser();
                        role_user.RoleId = Convert.ToInt32(objReader["roleid"]);
                        RoleList.Add(role_user);
                    }
                    objReader.Close();
                }
            }
            return RoleList;
        }

        public List<RoleUser> getSomeRole_UsersByRoleId(int roleId)
        {
            string sql = string.Format("select * from role_user where roleId={0}", roleId);
            return this.getRole_UsersBySql(sql);
        }

        public bool modifyRole_UserByUserId(int roleId, int userId)
        {
            return DBHelp.ExeCommand(string.Format("update role_user set roleId={0} where userId={1} ", roleId, userId));
        }
    }
}

