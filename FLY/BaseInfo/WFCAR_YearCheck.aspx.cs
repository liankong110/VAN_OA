using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFCAR_YearCheck : System.Web.UI.Page
    {
        private CAR_YearCheckService CAR_YearCheckService = new CAR_YearCheckService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from CAR_YearCheck where CarNo='{0}' and YearDate='{1}'",
                ddlCarNo.Text, txtYearDate.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，有效期[{1}]已经存在！');</script>", ddlCarNo.Text,txtYearDate.Text));
                        return;
                    }
                    CAR_YearCheck model = getModel();
                    if (this.CAR_YearCheckService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/CAR_YearCheckList.aspx");
        }

        private void Clear()
        {
            txtRemark.Text = "";
            txtYearDate.Text = "";
            ddlCarNo.Focus();
        }


        public CAR_YearCheck getModel()
        {
            VAN_OA.Model.BaseInfo.CAR_YearCheck model = new VAN_OA.Model.BaseInfo.CAR_YearCheck();
            model.CarNo = ddlCarNo.Text;
            model.YearDate =Convert.ToDateTime(txtYearDate.Text);
            model.Remark = txtRemark.Text;
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
                    string sqlCheck = string.Format("select count(*) from CAR_YearCheck where CarNo='{0}'  and YearDate='{2}' and id<>{1}",
                  ddlCarNo.Text, Request["Id"], txtYearDate.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，有效期[{1}]已经存在！');</script>", ddlCarNo.Text, txtYearDate.Text));
                        return;
                    }
                    CAR_YearCheck model = getModel();
                    if (this.CAR_YearCheckService.Update(model))
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
            if (this.txtYearDate.Text.Trim().Length == 0)
            {
                strErr += "有效期不能为空！\\n";
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
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    CAR_YearCheck model = this.CAR_YearCheckService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.ddlCarNo.Text = model.CarNo;
                    this.txtYearDate.Text = model.YearDate.ToString("yyyy-MM-dd");
                    txtRemark.Text = model.Remark;
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
