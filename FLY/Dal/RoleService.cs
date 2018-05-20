namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    [Serializable]
    public class RoleService
    {
        public int addRole(Role role)
        {
            string maxId = "select max(RID) from tb_Role";
            int id = 1;
            try
            {
                id = 1 + ((int) DBHelp.ExeScalar(maxId));
            }
            catch (Exception)
            {
            }
            DBHelp.ExeCommand(string.Format("insert into tb_Role values({5},'{0}','{1}','{2}','{3}','{4}')", new object[] { role.RoleName, role.RoleCode, role.RoleDepCode, role.RoleStatus, role.RoleMemo, id }));
            return id;
        }

        public bool deleteRoleByRoleId(int roleId)
        {
            return DBHelp.ExeCommand(string.Format("delete from tb_Role where rid={0} ", roleId));
        }

        public DataTable getAllRole_SysForms(string dataType)
        {
            string sql = "select * from role  ";
            return DBHelp.getDataTable(sql);
        }

        public List<Role> getAllRoles(string strWhere)
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("select * from tb_Role where 1=1 " + strWhere, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        Role role = new Role();
                        role.RID = Convert.ToInt32(objReader["RID"].ToString());
                        role.RoleName = Convert.ToString(objReader["RoleName"].ToString());
                        role.RoleCode = Convert.ToString(objReader["RoleCode"].ToString());
                        role.RoleDepCode = Convert.ToString(objReader["RoleDepCode"].ToString());
                        role.RoleStatus = Convert.ToString(objReader["RoleStatus"].ToString());
                        role.RoleMemo = Convert.ToString(objReader["RoleMemo"].ToString());
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        public List<Role> getAllRolesByUserid(int userId)
        {
            List<Role> roles = new List<Role>();
            string sql = string.Format(" select RID,RoleCode,roleName,ifSelectId=\r\ncase\r\nwhen (roleId is null) then convert(bit,0)\r\nwhen (roleId is not null) then convert(bit,1) \r\nend from tb_Role left join (select * from Role_User where UserId={0}) as newRole_User \r\non  newRole_User.RoleId=tb_Role.rid", userId);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        Role role = new Role();
                        role.RID = Convert.ToInt32(objReader["RID"].ToString());
                        role.RoleName = Convert.ToString(objReader["RoleName"].ToString());
                        role.RoleCode = Convert.ToString(objReader["RoleCode"].ToString());
                        role.IfSelected = Convert.ToBoolean(objReader["ifSelectId"].ToString());
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        public List<Role> getAllRolesByUseridExAdmin(int userId)
        {
            List<Role> roles = new List<Role>();
            string sql = string.Format(" select RID,RoleCode,roleName,ifSelectId=\r\ncase\r\nwhen (roleId is null) then convert(bit,0)\r\nwhen (roleId is not null) then convert(bit,1) \r\nend from tb_Role left join (select * from Role_User where UserId={0}) as newRole_User \r\non  newRole_User.RoleId=tb_Role.rid where RID<>1", userId);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        Role role = new Role();
                        role.RID = Convert.ToInt32(objReader["RID"].ToString());
                        role.RoleName = Convert.ToString(objReader["RoleName"].ToString());
                        role.RoleCode = Convert.ToString(objReader["RoleCode"].ToString());
                        role.IfSelected = Convert.ToBoolean(objReader["ifSelectId"].ToString());
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        public Role getRoleById(int roleId)
        {
            string sql = string.Format("select * from tb_Role where RID={0}", roleId);
            Role role = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        role = new Role();
                        role.RID = objReader.GetInt32(0);
                        role.RoleName = objReader.GetString(1);
                        role.RoleCode = objReader["RoleCode"].ToString();
                    }
                    objReader.Close();
                }
                conn.Close();
            }
            return role;
        }

        public Role getRoleByRoleName(string roleName)
        {
            string sql = string.Format("select * from role where roleName='{0}'", roleName);
            Role role = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        role = new Role();
                        role.RID = Convert.ToInt32(objReader["RID"].ToString());
                        role.RoleName = Convert.ToString(objReader["RoleName"].ToString());
                        role.RoleCode = Convert.ToString(objReader["RoleCode"].ToString());
                        role.RoleDepCode = Convert.ToString(objReader["RoleDepCode"].ToString());
                        role.RoleStatus = Convert.ToString(objReader["RoleStatus"].ToString());
                        role.RoleMemo = Convert.ToString(objReader["RoleMemo"].ToString());
                    }
                    objReader.Close();
                }
            }
            return role;
        }

        public bool modityRole(Role role)
        {
            return DBHelp.ExeCommand(string.Format("update tb_Role set roleName='{0}',roleCode='{1}',roleDepCode='{2}',roleStatus='{3}',roleMemo='{4}'  where RID={5}", new object[] { role.RoleName, role.RoleCode, role.RoleDepCode, role.RoleStatus, role.RoleMemo, role.RID }));
        }
    }
}

