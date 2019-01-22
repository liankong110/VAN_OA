using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFFin_JieDate : System.Web.UI.Page
    {        
        
        private Fin_JieDateService modelService = new Fin_JieDateService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Fin_JieDate where JYear='{0}'",
                txtYear.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('年份[{0}]，已经存在！');</script>", txtYear.Text));
                        return;
                    }
                    Fin_JieDate model = getModel();
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
            base.Response.Redirect("~/BaseInfo/WFFin_JieDatelList.aspx");
        }

        private void Clear()
        {            
             
        }


        public Fin_JieDate getModel()
        {
            VAN_OA.Model.BaseInfo.Fin_JieDate model = new VAN_OA.Model.BaseInfo.Fin_JieDate();
            model.JYear = Convert.ToInt32(txtYear.Text);
            model.JDate = Convert.ToDateTime(txtJDate.Text);
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
                    string sqlCheck = string.Format("select count(*) from Fin_JieDate where JYear='{0}' and id<>{1}",
                 txtYear.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('年份[{0}]，已经存在！');</script>",
                            txtYear.Text));
                        return;
                    }
                    Fin_JieDate model = getModel();
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
            if (this.txtJDate.Text.Trim().Length == 0||CommHelp.VerifesToDateTime(txtJDate.Text)==false)
            {
                strErr += "日期格式不正确！\\n";
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
                var i = 2012;
                while (i < 2099)
                {
                    txtYear.Items.Add(i.ToString());
                    i++;
                }
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    Fin_JieDate model = this.modelService.GetModel(Convert.ToInt32(base.Request["Id"]));

                    txtYear.Text = model.JYear.ToString();
                    txtJDate.Text = model.JDate.ToString("yyyy-MM-dd");                   
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }


    }
}
