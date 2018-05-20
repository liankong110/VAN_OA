using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Dal.HR;
using VAN_OA.Model.HR;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;
 
namespace VAN_OA.HR
{
    public partial class SalaryInfo : System.Web.UI.Page
    {
        private HR_PERSONService perSer = new HR_PERSONService();
        
        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/HR/PersonList.aspx?pageindex=" + base.Request["pageindex"]);
        }

        private void Clear()
        {      
            this.txtBasicSalary.Text = "0";
            this.txtGonglin.Text = "0";
            this.txtMobileFee.Text = "0";
            this.txtPositionFee.Text = "0";
            this.txtYangLaoJin.Text = "0";
            this.txtUnionFee.Text = "0";
            this.txtDefaultWorkDays.Text = "22";
            this.txtUpdateTime.Text = "0";
            this.txtUpdatePerson.Text = "0";
  
            txtBasicSalary.Focus();
        }

        public HR_PERSON getModel()
        {
            DateTime UpdateTime = DateTime.Now;
            int UpdatePerson = Convert.ToInt32(Session["currentUserId"]);

            VAN_OA.Model.HR.HR_PERSON model = new VAN_OA.Model.HR.HR_PERSON();
            if (base.Request["Code"] != null)
            {
                model.ID = Convert.ToInt32(base.Request["Code"]);
            }
            model.BasicSalary = decimal.Parse(txtBasicSalary.Text);
            model.GongLin = decimal.Parse(txtGonglin.Text);;
            model.MobileFee =decimal.Parse(txtMobileFee.Text);;
            model.PositionFee = decimal.Parse(txtPositionFee.Text);
            model.YangLaoJin = decimal.Parse(txtYangLaoJin.Text);
            model.UnionFee  = decimal.Parse(txtUnionFee.Text);
            model.DefaultWorkDays = decimal.Parse(txtDefaultWorkDays.Text);
            model.IsRetailed   = this.ChkRetail.Checked ;
            model.IsQuit   = this.ChkQuit.Checked ;
            model.UpdateTime = UpdateTime;
            model.UpdatePerson = UpdatePerson;
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    HR_PERSON per = getModel();
                    if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from HR_Salary where ID='{0}'", base.Request["Code"]))) > 0)
                    {
                        this.perSer.UpdateSalary(per);
                    }
                    else
                    { 
                        this.perSer.AddSalary(per);
                    }
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
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
            if (this.txtBasicSalary.Text.Trim().Length == 0)
            {
                strErr = "基本工资不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>",strErr));
                this.txtBasicSalary.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal (txtBasicSalary.Text);
            }
            catch (Exception)
            {
                strErr += "基本工资格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtBasicSalary.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal(txtGonglin.Text);
            }
            catch (Exception)
            {                
                strErr += "工龄工资格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtGonglin.Focus();
                return false;              
            }
            try
            {
                Convert.ToDecimal(txtMobileFee.Text);
            }
            catch (Exception)
            {
               strErr += "通讯费格式错误！\\n";
               base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
               this.txtMobileFee.Focus();
               return false;
            }
            try
            {
                Convert.ToDecimal(txtPositionFee.Text);
            }
            catch (Exception)
            {
                strErr += "职务津贴格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtPositionFee.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal(txtUnionFee.Text);
            }
            catch (Exception)
            {
                strErr += "工会费格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtUnionFee.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal(txtYangLaoJin.Text);
            }
            catch (Exception)
            {
                strErr += "养老金格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtYangLaoJin.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal(txtDefaultWorkDays.Text);
            }
            catch (Exception)
            {
                strErr += "默认工作天数格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtDefaultWorkDays.Focus();
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Code"] != null)
                {
                    HR_PERSON model = this.perSer.GetSalary(Convert.ToInt32(base.Request["Code"]));  
                    if (model!=null)
                    {
                    this.lblName.Text = model.Name;
                    this.txtBasicSalary.Text = model.BasicSalary.ToString();
                    this.txtGonglin.Text = model.GongLin.ToString();
                    this.txtMobileFee.Text = model.MobileFee.ToString();
                    this.txtPositionFee.Text = model.PositionFee.ToString();
                    this.txtUnionFee.Text = model.UnionFee.ToString();
                    this.txtYangLaoJin.Text = model.YangLaoJin.ToString();
                    this.txtDefaultWorkDays.Text = model.DefaultWorkDays.ToString();
                    this.ChkRetail.Checked = model.IsRetailed;
                    this.ChkQuit.Checked = model.IsQuit;
                        if (model.UpdateTime != null)
                        {
                            this.txtUpdateTime.Text = model.UpdateTime.ToString();
                        }
                        if (model.UpdatePersonName != null)
                        {
                            txtUpdatePerson.Text = model.UpdatePersonName.ToString();
                        }
                    }
                }
            }
        }
    }
}
