using System;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.EFrom;

namespace VAN_OA.BaseInfo
{
    public partial class WFUseType : System.Web.UI.Page
    {
        private Base_UseTypeService warnDaysService = new Base_UseTypeService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Base_UseType where Name='{0}' AND Type={1}",
                txtName.Text,ddlType.SelectedItem.Value);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", txtName.Text));
                        return;
                    }
                    Base_UseType model = getModel();
                    if (this.warnDaysService.Add(model) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFUseTypeList.aspx");
        }

        private void Clear()
        {
            txtName.Text = "";
            txtName.Focus();
        }


        public Base_UseType getModel()
        {
            VAN_OA.Model.BaseInfo.Base_UseType model = new VAN_OA.Model.BaseInfo.Base_UseType();
            model.Name = txtName.Text;
            model.Type = Convert.ToInt32(ddlType.Text);
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Base_UseType where Name='{0}' and id<>{1} AND Type={2}",
                  txtName.Text, Request["Id"], ddlType.SelectedItem.Value);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", txtName.Text));
                        return;
                    }
                    Base_UseType model = getModel();
                    if (this.warnDaysService.Update(model))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {
            string strErr = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "用途不能为空！\\n";
            }
             
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                A_ProInfoService proSer = new A_ProInfoService();
               

                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    Base_UseType model = this.warnDaysService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtName.Text = model.Name;
                    this.ddlType.Text = model.Type.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
