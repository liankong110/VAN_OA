using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFModel : System.Web.UI.Page
    {        
        private TB_ModelService modelService = new TB_ModelService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_Model where ModelName='{0}'",
                txtModelName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('模型名称[{0}]，已经存在！');</script>", txtModelName.Text));
                        return;
                    }
                    TB_Model model = getModel();
                    if (this.modelService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFModelList.aspx");
        }

        private void Clear()
        {            
            txtModelName.Text = "";
            txtModelRemark.Text = "";
            txtModelName.Focus();
        }


        public TB_Model getModel()
        {
            VAN_OA.Model.BaseInfo.TB_Model model = new VAN_OA.Model.BaseInfo.TB_Model();
            model.ModelName = txtModelName.Text;
            model.ModelRemark = txtModelRemark.Text;            
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
                    string sqlCheck = string.Format("select count(*) from TB_Model where ModelName='{0}' and id<>{1}",
                  txtModelName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('模型名称[{0}]，已经存在！');</script>",
                            txtModelName.Text));
                        return;
                    }
                    TB_Model model = getModel();
                    if (this.modelService.Update(model))
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
            if (this.txtModelName.Text.Trim().Length == 0)
            {
                strErr += "模型名称不能为空！\\n";
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
                    TB_Model model = this.modelService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtModelName.Text = model.ModelName;
                    txtModelRemark.Text = model.ModelRemark;                   
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }


    }
}
