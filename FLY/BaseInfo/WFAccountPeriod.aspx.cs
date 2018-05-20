using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

 
 
namespace VAN_OA.BaseInfo
{
    public partial class WFAccountPeriod : System.Web.UI.Page
    {
        private TB_AccountPeriodService accSer = new TB_AccountPeriodService();


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_AccountPeriod where AccountName={0}", txtAccountName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('账期[{0}],已经存在！');</script>", txtAccountName.Text));
                        return;
                    }
                    TB_AccountPeriod per = getModel();
                    if (this.accSer.Add(per) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFAccountPeriodList.aspx");
        }

        private void Clear()
        {

            txtAccountName.Text = "";
            txtAccountXiShu.Text = "";
            txtRemark.Text = "";

            txtAccountName.Focus();
        }


        public TB_AccountPeriod getModel()
        {
            
            TB_AccountPeriod model = new TB_AccountPeriod();
            model.AccountName =Convert.ToInt32(txtAccountName.Text);
            model.AccountXiShu = Convert.ToInt32(txtAccountXiShu.Text);
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
                    string sqlCheck = string.Format("select count(*) from TB_AccountPeriod where AccountName={0} and Id<>{1}", txtAccountName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('账期[{0}],已经存在！');</script>", txtAccountName.Text));
                        return;
                    }

                    TB_AccountPeriod per = getModel();
                    if (this.accSer.Update(per))
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


            if (this.txtAccountName.Text.Trim().Length == 0)
            {
                strErr = "请填写账期！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAccountName.Focus();
                return false;
            }
            try
            {
                Convert.ToInt32(txtAccountXiShu.Text);
            }
            catch (Exception)
            {
                strErr = "你填写的账期格式有误 必须为整数！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAccountXiShu.Focus();
                return false;
            }
            if (this.txtAccountXiShu.Text.Trim().Length == 0)
            {
                strErr = "请填写账期系数！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAccountXiShu.Focus();
                return false;
            }

            try
            {
                Convert.ToInt32(txtAccountXiShu.Text);
            }
            catch (Exception)
            {
                strErr = "你填写的系数格式有误 必须为整数！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAccountXiShu.Focus();
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
                    TB_AccountPeriod model = this.accSer.GetModel(Convert.ToInt32(base.Request["Id"]));


                    txtAccountName.Text = model.AccountName.ToString();
                    txtAccountXiShu.Text = model.AccountXiShu.ToString();
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
