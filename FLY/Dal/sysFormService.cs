namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    [Serializable]
    public class sysFormService
    {
        public int addSysForm(SysForm form)
        {
            string maxId = "select max(formID) from sys_form";
            int id = 1;
            try
            {
                id = 1 + ((int) DBHelp.ExeScalar(maxId));
            }
            catch (Exception)
            {
            }
            string getFormIndex = string.Format("select max(formIndex) from sys_form where UpperID={0}", form.UpperID);
            int maxFormIndex = 0;
            try
            {
                maxFormIndex = ((int) DBHelp.ExeScalar(getFormIndex)) + 1;
            }
            catch (Exception)
            {
            }
            DBHelp.ExeCommand(string.Format("insert into sys_form values({4},'{0}','{1}',{2},{3},'')  \r\n                                       ", new object[] { form.DisplayName, form.AssemblyPath, form.UpperID, maxFormIndex, id }));
            return id;
        }

        public bool deleteSysFormById(int formID)
        {
            return DBHelp.ExeCommand(string.Format("delete from sys_form where formId={0}", formID));
        }

        public bool deleteSysFormByMenuIndex(int MenuIndex)
        {
            return DBHelp.ExeCommand(string.Format("delete from sys_form where upperId={0}", MenuIndex));
        }

        public List<SysForm> getAllForms()
        {
            string sql = string.Format("select * from sys_form", new object[0]);
            return this.getFormsBySql(sql);
        }

        public List<SysForm> getAllForms_1()
        {
            string sql = string.Format("select * from sys_form", new object[0]);
            List<SysForm> SysForms = new List<SysForm>();
            DataTable dt = DBHelp.getDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                SysForm sysForms = new SysForm();
                sysForms.FormID = Convert.ToInt32(dr[0]);
                sysForms.DisplayName = dr[1].ToString();
                sysForms.AssemblyPath = dr[2].ToString();
                sysForms.UpperID = Convert.ToInt32(dr[3]);
                sysForms.FormIndex = Convert.ToInt32(dr[4]);
                if (dr[5] != null)
                {
                    sysForms.FormImgURL = dr[5].ToString();
                }
                SysForms.Add(sysForms);
            }
            return SysForms;
        }

        public SysForm getFormByPath(string path)
        {
            SysForm sysForms = null;
            DataTable dt = DBHelp.getDataTable(string.Format("select * from sys_form where assemblyPath='{0}'", path));
            foreach (DataRow dr in dt.Rows)
            {
                sysForms = new SysForm();
                sysForms.FormID = Convert.ToInt32(dr[0]);
                sysForms.DisplayName = dr[1].ToString();
                sysForms.AssemblyPath = dr[2].ToString();
                sysForms.UpperID = Convert.ToInt32(dr[3]);
                sysForms.FormIndex = Convert.ToInt32(dr[4]);
                if (dr[5] != null)
                {
                    sysForms.FormImgURL = dr[5].ToString();
                }
            }
            return sysForms;
        }

        public SysForm getFormsByFormId(int formId)
        {
            SysForm sysForms = null;
            DataTable dt = DBHelp.getDataTable(string.Format("select * from sys_form where formid={0}", formId));
            foreach (DataRow dr in dt.Rows)
            {
                sysForms = new SysForm();
                sysForms.FormID = Convert.ToInt32(dr[0]);
                sysForms.DisplayName = dr[1].ToString();
                sysForms.AssemblyPath = dr[2].ToString();
                sysForms.UpperID = Convert.ToInt32(dr[3]);
                sysForms.FormIndex = Convert.ToInt32(dr[4]);
                if (dr[5] != null)
                {
                    sysForms.FormImgURL = dr[5].ToString();
                }
            }
            return sysForms;
        }

        private List<SysForm> getFormsBySql(string sql)
        {
            List<SysForm> SysForms = new List<SysForm>();
            DataTable dt = DBHelp.getDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                SysForm sysForms = new SysForm();
                sysForms.FormID = Convert.ToInt32(dr[0]);
                sysForms.DisplayName = dr[1].ToString();
                sysForms.AssemblyPath = dr[2].ToString();
                sysForms.UpperID = Convert.ToInt32(dr[3]);
                sysForms.Menu = new sysMenuService().getSysMenuByMenuId(sysForms.UpperID);
                sysForms.FormIndex = Convert.ToInt32(dr[4]);
                if (dr[5] != null)
                {
                    sysForms.FormImgURL = dr[5].ToString();
                }
                SysForms.Add(sysForms);
            }
            return SysForms;
        }




        public List<SysForm> getFormsByUpperId(int upperId)
        {
            string sql = string.Format(" select * from sys_form where upperId ={0}", upperId);
            return this.getFormsBySql(sql);
        }

        public List<SysForm> GetInfoByConditon(string condition)
        {
            string sql = "select formID, sys_form.displayName,assemblyPath,upperID,formIndex,formImgURL from sys_form left join sys_menu on menuId=upperID  ";
            if (condition != "")
            {
                sql = sql + string.Format(" where {0}", condition);
            }
            return this.getFormsBySql(sql);
        }

        public DataTable getMentFormInfo(string dataType)
        {
            string sql = "select formID, sys_menu.displayName as name,sys_form.displayName,assemblyPath,upperId,formImgURL from sys_form left join sys_menu on upperId=menuId";
            return DBHelp.getDataTable(sql);
        }

        public int modifyFormIndex(List<SysForm> sysForms)
        {
            for (int i = 0; i < sysForms.Count; i++)
            {
                DBHelp.ExeCommand(string.Format("update sys_form set formIndex={0} where formID={1}", i, sysForms[i].FormID));
            }
            return 0;
        }

        public bool modifySysForm(SysForm form)
        {
            if (this.getFormsByFormId(form.FormID).DisplayName != form.DisplayName)
            {
                int count = (int) DBHelp.ExeScalar(string.Format("select count(*) from sys_form where displayName='{0}'", form.DisplayName));
                if (count > 0)
                {
                    return false;
                }
            }
            return DBHelp.ExeCommand(string.Format("update sys_form set displayName='{0}',upperId={1} where formId={2}", form.DisplayName, form.UpperID, form.FormID));
        }



        public List<SysForm> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select formID,displayName,assemblyPath,upperID,formIndex,formImgURL ");
            strSql.Append(" FROM sys_form ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SysForm> list = new List<SysForm>();
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
        public SysForm ReaderBind(IDataReader dataReader)
        {
            SysForm model = new SysForm();
            object ojb;
            ojb = dataReader["formID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FormID = (int)ojb;
            }
            model.DisplayName = dataReader["displayName"].ToString();
            model.AssemblyPath = dataReader["assemblyPath"].ToString();
            ojb = dataReader["upperID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpperID = (int)ojb;
            }
            ojb = dataReader["formIndex"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FormIndex = (int)ojb;
            }

            ojb = dataReader["formImgURL"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FormImgURL = dataReader["formImgURL"].ToString();
            }
         
            return model;
        }

    }
}

