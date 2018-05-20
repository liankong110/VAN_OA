using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFBasePoType : System.Web.UI.Page
    {
        private TB_BasePoTypeService TB_BasePoTypeService = new TB_BasePoTypeService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_BasePoType where BasePoType='{0}'",
                txtBasePoType.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", txtBasePoType.Text));
                        return;
                    }
                    TB_BasePoType model = getModel();
                    if (this.TB_BasePoTypeService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFBasePoTypeList.aspx");
        }

        private void Clear()
        {
            txtBasePoType.Text = "";
            txtReward.Text = "";
            txtBasePoType.Focus();
        }


        public TB_BasePoType getModel()
        {
            VAN_OA.Model.BaseInfo.TB_BasePoType model = new VAN_OA.Model.BaseInfo.TB_BasePoType();
            model.BasePoType = txtBasePoType.Text;
            model.Reward = Convert.ToDecimal(txtReward.Text);
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
                    string sqlCheck = string.Format("select count(*) from TB_BasePoType where BasePoType='{0}' and id<>{1}",
                  txtBasePoType.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", txtBasePoType.Text));
                        return;
                    }
                    TB_BasePoType model = getModel();
                    if (this.TB_BasePoTypeService.Update(model))
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
            if (this.txtBasePoType.Text.Trim().Length == 0)
            {
                strErr += "名称不能为空！\\n";
            }

            if (CommHelp.VerifesToNum(txtReward.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('系数 格式错误！');</script>");
                return false;
            }
            try
            {
                Convert.ToDecimal(txtReward.Text);
            }
            catch (Exception)
            {
                strErr += "系数格式错误！\\n";
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
                    TB_BasePoType model = this.TB_BasePoTypeService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtBasePoType.Text = model.BasePoType;
                    this.txtReward.Text = model.Reward.ToString();
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
