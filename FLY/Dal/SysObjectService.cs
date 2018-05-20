namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SysObjectService
    {
        public bool addObject(SysObject sysObj)
        {
            string maxId = "select max(AutoID) from sys_Object";
            int id = 1;
            try
            {
                id = 1 + ((int) DBHelp.ExeScalar(maxId));
            }
            catch (Exception)
            {
            }
            return DBHelp.ExeCommand(string.Format("insert into sys_Object values({6},{0},'{1}','{2}','{3}','{4}',{5})", new object[] { sysObj.From.FormID, sysObj.TxtName, sysObj.Name, sysObj.ObjctPro, sysObj.ObjProValue, sysObj.SysRole.RID, id }));
        }

        public bool addObject(List<roleSysObject> roleObjects, List<SysObject> sysObjs, int roleId)
        {
            bool result = true;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    int i;
                    string sql;
                    string deleteRoleObj = string.Format("delete role_sys_form where role_Id=" + roleId, new object[0]);
                    objCommand.CommandText = deleteRoleObj;
                    objCommand.ExecuteNonQuery();
                    string deleteObjcts = string.Format("delete sys_object where roleId=" + roleId, new object[0]);
                    objCommand.CommandText = deleteObjcts;
                    objCommand.ExecuteNonQuery();
                    for (i = 0; i < sysObjs.Count; i++)
                    {
                        SysObject sysObj = sysObjs[i];
                        string maxId = "select max(AutoID) from sys_Object";
                        int id = 1;
                        try
                        {
                            objCommand.CommandText = maxId;
                            objCommand.ExecuteNonQuery();
                            id = 1 + ((int) objCommand.ExecuteScalar());
                        }
                        catch (Exception)
                        {
                        }
                        sql = string.Format("insert into sys_Object values({6},{0},'{1}','{2}','{3}','{4}',{5})", new object[] { sysObj.FromId, sysObj.TxtName, sysObj.Name, "Enabled", "false", roleId, id });
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                    for (i = 0; i < roleObjects.Count; i++)
                    {
                        sql = string.Format("insert into role_sys_form values({0},{1})", roleId, roleObjects[i]._SysObject.FromId);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                    tan.Commit();
                    conn.Close();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    result = true;
                }
            }
            return result;
        }

        public SysObject checkIfExist(SysObject sysObj)
        {
            SysObject obj = null;
            DataTable dt = DBHelp.getDataTable(string.Format("select * from sys_Object where FormId={0} and name='{1}' and objectPro='{2}' and roleId={3}", new object[] { sysObj.From.FormID, sysObj.Name, sysObj.ObjctPro, sysObj.SysRole.RID }));
            foreach (DataRow dr in dt.Rows)
            {
                obj = new SysObject();
                obj.Obj_ID = Convert.ToInt32(dr["AutoID"]);
                obj.Name = dr["Name"].ToString();
                obj.ObjctPro = dr["ObjectPro"].ToString();
                obj.ObjProValue = dr["objectProVal"].ToString();
                int formId = Convert.ToInt32(dr["FormID"]);
                obj.From = new sysFormService().getFormsByFormId(formId);
                int roleId = Convert.ToInt32(dr["roleId"]);
                obj.TxtName = dr["TextName"].ToString();
                obj.SysRole = new RoleService().getRoleById(roleId);
            }
            return obj;
        }

        public bool deleteObj(int objId)
        {
            return DBHelp.ExeCommand(string.Format("delete from sys_Object where autoId={0}", objId));
        }

        public DataTable getAllInfoByRoleId(int roleId)
        {
            return DBHelp.getDataTable(string.Format("select displayName,TextName,name,objectPro,objectProVal,roleName from sys_Object \r\n                                        left join sys_form on sys_Object.formID=sys_form.formId \r\n                                        left join tb_role on tb_role.RID=sys_Object.roleId where sys_Object.roleId={0}", roleId));
        }

        public List<SysObject> getObjects(string roleStr, SysObject sysObj)
        {
            int count = roleStr.Split(new char[] { ',' }).Length;
            List<SysObject> sysObjs = new List<SysObject>();
            DataTable dt = DBHelp.getDataTable(string.Format("select  [FormID] ,[TextName],[Name],[ObjectPro],[objectProVal] from sys_object where formID={0} and roleId in ({1})\r\ngroup by [FormID] ,[TextName],[Name],[ObjectPro],[objectProVal] having count(*)={2}", sysObj.From.FormID, roleStr, count));
            foreach (DataRow dr in dt.Rows)
            {
                SysObject obj = new SysObject();
                obj.Name = dr["Name"].ToString();
                obj.ObjctPro = dr["ObjectPro"].ToString();
                obj.ObjProValue = dr["objectProVal"].ToString();
                int formId = Convert.ToInt32(dr["FormID"]);
                obj.From = new sysFormService().getFormsByFormId(formId);
                obj.TxtName = dr["TextName"].ToString();
                sysObjs.Add(obj);
            }
            return sysObjs;
        }

        public List<SysObject> getSomeObjects(int roleID)
        {
            List<SysObject> sysObjs = new List<SysObject>();
            string sql = string.Format("select Name,formId from sys_Object where roleId={0}", roleID);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        SysObject obj = new SysObject();
                        obj.Name = objReader[0].ToString();
                        obj.FromId = Convert.ToInt32(objReader[1]);
                        sysObjs.Add(obj);
                    }
                    objReader.Close();
                }
            }
            return sysObjs;
        }

        public List<SysObject> getSomeObjects(string assemblyPath, int roleID)
        {
            List<SysObject> sysObjs = new List<SysObject>();
            string sql = string.Format("select Name from sys_Object where formID in(select formId from sys_form where assemblyPath='{0}') and roleId={1}", assemblyPath, roleID);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        SysObject obj = new SysObject();
                        obj.Name = objReader[0].ToString();
                        sysObjs.Add(obj);
                    }
                    objReader.Close();
                }
            }
            return sysObjs;
        }

        public bool modifyObject(SysObject sysObj)
        {
            return DBHelp.ExeCommand(string.Format("update  sys_Object set objectProVal='{0}' where autoId={1}", sysObj.ObjProValue, sysObj.Obj_ID));
        }

        public bool modifyObjectBySql(string sql)
        {
            return DBHelp.ExeCommand(sql);
        }
    }
}

