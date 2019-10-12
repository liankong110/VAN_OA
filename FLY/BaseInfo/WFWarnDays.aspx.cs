using System;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.EFrom;

namespace VAN_OA.BaseInfo
{
    public partial class WFWarnDays : System.Web.UI.Page
    {
        private Base_WarnDaysService warnDaysService = new Base_WarnDaysService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Base_WarnDays where Name='{0}'",
                ddlName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", ddlName.Text));
                        return;
                    }
                    Base_WarnDays model = getModel();
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
            base.Response.Redirect("~/BaseInfo/WFWarnDaysList.aspx");
        }

        private void Clear()
        {
            txtWarnDays.Text = "";
            txtWarnDays.Focus();
        }


        public Base_WarnDays getModel()
        {
            VAN_OA.Model.BaseInfo.Base_WarnDays model = new VAN_OA.Model.BaseInfo.Base_WarnDays();
            model.Name = ddlName.Text;
            model.WarnDays = Convert.ToSingle(txtWarnDays.Text);
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
                    string sqlCheck = string.Format("select count(*) from Base_WarnDays where Name='{0}' and id<>{1}",
                  ddlName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", ddlName.Text));
                        return;
                    }
                    Base_WarnDays model = getModel();
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
            if (this.txtWarnDays.Text.Trim().Length == 0)
            {
                strErr += "警示天数不能为空！\\n";
            }
            else
            {
                if (CommHelp.VerifesToNum(txtWarnDays.Text) == false)
                {
                    strErr += "警示天数格式错误！\\n";
                }
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
                List<A_ProInfo> pros = proSer.GetListArray("");
                ddlName.DataSource = pros;
                ddlName.DataBind();

                ddlName.DataTextField = "pro_Type";
                ddlName.DataValueField = "pro_Type";

                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    Base_WarnDays model = this.warnDaysService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.ddlName.Text = model.Name;
                    this.txtWarnDays.Text = model.WarnDays.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
