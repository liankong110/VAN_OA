namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    [Serializable]
    public class sysMenuService
    {
        public bool addSysMenus(SysMenu sysmenu)
        {
            int count = (int) DBHelp.ExeScalar(string.Format("select count(*) from sys_menu where displayName='{0}'", sysmenu.DisplayName));
            if (count > 0)
            {
                return false;
            }
            string sql = "select max(menuId) from sys_menu";
            int menuId = ((int) DBHelp.ExeScalar(sql)) + 1;
            string sql1 = "select max(menuindex) from sys_menu ";
            int menuindex = ((int) DBHelp.ExeScalar(sql1)) + 1;
            return DBHelp.ExeCommand(string.Format("insert into sys_menu values({0},'{1}',{2},{3})", new object[] { menuId, sysmenu.DisplayName, sysmenu.IconIndex, menuindex }));
        }

        public bool deleteSysMenuByMenuId(int menuId)
        {
            return DBHelp.ExeCommand(string.Format("delete from sys_menu where menuId={0}", menuId));
        }

        public List<SysMenu> getAllSysMenus()
        {
            string sql = string.Format("select * from sys_menu order by menuindex", new object[0]);
            List<SysMenu> sysMenus = new List<SysMenu>();
            DataTable dt = DBHelp.getDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                SysMenu sysMenu = new SysMenu();
                sysMenu.MenuID = Convert.ToInt32(dr[0]);
                sysMenu.DisplayName = dr[1].ToString();
                sysMenu.IconIndex = Convert.ToInt32(dr[2]);
                sysMenu.MenuIndex = Convert.ToInt32(dr[3]);
                sysMenus.Add(sysMenu);
            }
            return sysMenus;
        }

        public SysMenu getSysMenuByMenuId(int menuId)
        {
            SysMenu sysmenu = new SysMenu();
            string sql = string.Format("select * from sys_menu where menuId={0}", menuId);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        sysmenu.MenuID = objReader.GetInt32(0);
                        sysmenu.DisplayName = objReader.GetString(1);
                        sysmenu.IconIndex = objReader.GetInt32(2);
                        sysmenu.MenuIndex = objReader.GetInt32(3);
                    }
                    objReader.Close();
                }
            }
            return sysmenu;
        }

        public void modifySysMensById(List<SysMenu> sysMeuns)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                try
                {
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;
                    for (int i = 0; i < sysMeuns.Count; i++)
                    {
                        string sql = string.Format("update sys_menu set menuindex={0} where menuId={1}", i, sysMeuns[i].MenuID);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                }
                conn.Close();
            }
        }

        public bool modifySysMenusById(int id, string displayName)
        {
            return DBHelp.ExeCommand(string.Format("update sys_menu set displayName='{0}' where menuId={1}", displayName, id));
        }
    }
}

