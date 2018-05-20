using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFBaseSkill : System.Web.UI.Page
    {
        private TB_BaseSkillService baseSkillService = new TB_BaseSkillService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_BaseSkill where MyPoType='{0}'",
                txtMyPoType.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}]，已经存在！');</script>", txtMyPoType.Text));
                        return;
                    }
                    TB_BaseSkill model = getModel();
                    if (this.baseSkillService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFBaseSkillList.aspx");
        }

        private void Clear()
        {
            
            txtMyPoType.Text = "";
            txtXiShu.Text = "";
            txtMyPoType.Focus();
        }


        public TB_BaseSkill getModel()
        {
            VAN_OA.Model.BaseInfo.TB_BaseSkill model = new VAN_OA.Model.BaseInfo.TB_BaseSkill();
            model.MyPoType = txtMyPoType.Text;
            model.XiShu = Convert.ToDecimal(txtXiShu.Text);
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
                    string sqlCheck = string.Format("select count(*) from TB_BaseSkill where MyPoType='{0}' and id<>{1}",
                  txtMyPoType.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目类别[{0}]，已经存在！');</script>",
                            txtMyPoType.Text));
                        return;
                    }
                    TB_BaseSkill model = getModel();
                    if (this.baseSkillService.Update(model))
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
            if (this.txtMyPoType.Text.Trim().Length == 0)
            {
                strErr += "项目类别不能为空！\\n";
            }
            
            if (CommHelp.VerifesToNum(txtXiShu.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('考核系数 格式错误！');</script>");
                return false;
            }
            try
            {
                Convert.ToDecimal(txtXiShu.Text);
            }
            catch (Exception)
            {
                strErr += "考核系数格式错误！\\n";
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
                    TB_BaseSkill model = this.baseSkillService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtMyPoType.Text = model.MyPoType;
                    this.txtXiShu.Text = model.XiShu.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }


    }
}
