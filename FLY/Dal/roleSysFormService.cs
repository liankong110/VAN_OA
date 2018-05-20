namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class roleSysFormService
    {
        public bool addRole_sysform(int roleId, int sysFormId)
        {
            return DBHelp.ExeCommand(string.Format("insert into role_sys_form values({0},{1})", roleId, sysFormId));
        }

        public bool deleteRole_SysfromByFormId(int formId)
        {
            return DBHelp.ExeCommand(string.Format("delete from role_sys_form where sys_form_id={0}", formId));
        }

        public bool depeteRole_SysForm(int roleId, int sysFormId)
        {
            return DBHelp.ExeCommand(string.Format("delete from role_sys_form where sys_form_id={0} and role_Id={1}", sysFormId, roleId));
        }

        public List<roleSysform> getRightsByRoleId(string roleId)
        {
            List<roleSysform> role_sysForms = new List<roleSysform>();
           // DataTable dt = DBHelp.getDataTaeble();
            Role r = null;
            sysFormService froms = new sysFormService();

            List<SysForm> allForms = froms.GetListArray(string.Format(" formID in (select sys_form_Id from role_sys_form where role_id in ({0}))", roleId));
            foreach (SysForm form in allForms)
            {
                roleSysform rsf = new roleSysform();
                //int sys_form_Id = Convert.ToInt32(dr["sys_form_Id"]);
                //SysForm form = new sysFormService().getFormsByFormId(sys_form_Id);
                if (form != null)
                {
                    rsf.Sysform = form;
                    rsf.Role = r;
                    role_sysForms.Add(rsf);
                }
            }
            return role_sysForms;
        }

        public List<roleSysform> getRightsByRoleId_1(int roleId)
        {
            List<roleSysform> role_sysForms = new List<roleSysform>();
            role_sysForms.Clear();
            string sql = string.Format("select role_Id,sys_form_Id,upperID from role_sys_form left join sys_form on sys_Form_Id=formid  where role_id={0} and upperID is not null", roleId);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        roleSysform RR = new roleSysform();
                        RR.Sysform.FormID = Convert.ToInt32(objReader[1].ToString());
                        RR.Role.RID = Convert.ToInt32(objReader[0].ToString());
                        if (objReader[2] != null)
                        {
                            RR.Sysform.UpperID = Convert.ToInt32(objReader[2].ToString());
                        }
                        role_sysForms.Add(RR);
                    }
                    objReader.Close();
                }
            }
            return role_sysForms;
        }
    }
}

