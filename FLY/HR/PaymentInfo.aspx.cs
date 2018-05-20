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
using VAN_OA.Dal.Performance;
using VAN_OA.Model.HR;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;

namespace VAN_OA.HR
{
    public partial class PaymentInfo : System.Web.UI.Page
    {
        private HR_PaymentService PaymentSer = new HR_PaymentService();
        private HR_PERSONService perSer = new HR_PERSONService();
        private ApprovePAFormListService PASer = new ApprovePAFormListService();

        private string YearMonth;
        private decimal BasicSalary;
        private decimal FullAttendence;
        private decimal MobileFee;
        private decimal SpecialAward;
        private decimal SpecialAwardNote;
        private decimal GongLin;
        private decimal PositionPerformance;
        private decimal PositionFee;
        private decimal WorkPerformance;
        private decimal FullPayment;
        private decimal DefaultWorkDays;
        private decimal WorkDays;
        private decimal ShouldPayment;
        private decimal UnionFee;
        private decimal Deduction;
        private string DeductionNote;
        private decimal YangLaoJin;
        private decimal ActualPayment;
        private DateTime UpdateTime;
        private DateTime OnBoadTime;
        private DateTime QuitTime;

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/HR/PaymentList.aspx?selectYearMonth=" + base.Request["selectYearMonth"] + "&pageindex=" + base.Request["pageindex"]);
        }

        private void Clear()
        {
            this.txtBasicSalary.Text = "0";
            this.txtGonglin.Text = "0";
            this.txtMobileFee.Text = "0";
            this.txtPositionFee.Text = "0";
            this.txtYangLaoJin.Text = "0";
            this.txtUnionFee.Text = "0";
            this.txtUpdateTime.Text = "0";
            this.txtUpdatePerson.Text = "0";
            txtBasicSalary.Focus();
        }
        public HR_Payment getPayment()
        {
            YearMonth = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue;
            UpdateTime = DateTime.Now;
            int UpdatePerson = Convert.ToInt32(Session["currentUserId"]);

            VAN_OA.Model.HR.HR_Payment ENPayment = new VAN_OA.Model.HR.HR_Payment();
            if (base.Request["Code"] != null)
            {
                ENPayment.ID = Convert.ToInt32(base.Request["Code"]);
            }
            ENPayment.YearMonth = YearMonth;            
            ENPayment.BasicSalary = decimal.Parse(txtBasicSalary.Text.Trim());            
            ENPayment.FullAttendence = decimal.Parse(txtFullAttendence.Text.Trim());            
            ENPayment.MobileFee = decimal.Parse(txtMobileFee.Text.Trim());            
            ENPayment.SpecialAward = decimal.Parse(txtSpecialAward.Text.Trim());
            ENPayment.SpecialAwardNote = txtSpecialAwardNote.Text.Trim();             
            ENPayment.GongLin = decimal.Parse(txtGonglin.Text.Trim());            
            ENPayment.PositionPerformance = decimal.Parse(txtPositionPerformance.Text.Trim());             
            ENPayment.PositionFee = decimal.Parse(txtPositionFee.Text.Trim());           
            ENPayment.WorkPerformance = decimal.Parse(txtWorkPerformance.Text.Trim());            
            ENPayment.FullPayment = decimal.Parse(lblFullPayment.Text.Trim());            
            ENPayment.DefaultWorkDays = decimal.Parse(lblDefaultWorkDays.Text.Trim());           
            ENPayment.WorkDays = decimal.Parse(txtWorkDays.Text.Trim());            
            ENPayment.ShouldPayment = decimal.Parse(lblShouldPayment.Text.Trim());            
            ENPayment.UnionFee = decimal.Parse(txtUnionFee.Text);           
            ENPayment.Deduction = decimal.Parse(txtDeduction.Text.Trim());
            ENPayment.DeductionNote = txtDeductionNote.Text.Trim();            
            ENPayment.YangLaoJin = decimal.Parse(txtYangLaoJin.Text);           
            ENPayment.ActualPayment = decimal.Parse(lblActualPayment.Text.Trim());
            ENPayment.UpdateTime = UpdateTime;
            ENPayment.UpdatePerson = UpdatePerson;
            return ENPayment;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    HR_Payment PaymentSer = getPayment();
                    if (Convert.ToInt32(DBHelp.ExeScalar("select count(*) from HR_Payment where ID=" + base.Request["Code"] + " and YearMonth='" + PaymentSer.YearMonth + "'")) > 0)
                    {
                        this.PaymentSer.Update(PaymentSer);
                    }
                    else
                    {
                        this.PaymentSer.Add(PaymentSer);
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
            //string strErr = "";
            //try
            //{
            //    Convert.ToDecimal (txtBasicSalary.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "基本工资格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtBasicSalary.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtFullAttendence.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "全勤奖格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtFullAttendence.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtMobileFee.Text);
            //}
            //catch (Exception)
            //{
            //   strErr += "通讯费格式错误！\\n";
            //   base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //   this.txtMobileFee.Focus();
            //   return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtSpecialAward.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "特殊奖励格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtSpecialAward.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtGonglin.Text);
            //}
            //catch (Exception)
            //{                
            //    strErr += "工龄工资格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtGonglin.Focus();
            //    return false;              
            //}
            //try
            //{
            //    Convert.ToDecimal(txtPositionPerformance.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "岗位考核格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtPositionPerformance.Focus();
            //    return false;
            //} 
            //try
            //{
            //    Convert.ToDecimal(txtPositionFee.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "职务津贴格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtPositionFee.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtWorkPerformance.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "工作绩效格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtWorkPerformance.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtWorkDays.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "出勤天数格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtWorkDays.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtUnionFee.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "工会费格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtUnionFee.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtDeduction.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "扣款格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtDeduction.Focus();
            //    return false;
            //}
            //try
            //{
            //    Convert.ToDecimal(txtYangLaoJin.Text);
            //}
            //catch (Exception)
            //{
            //    strErr += "养老金格式错误！\\n";
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
            //    this.txtYangLaoJin.Focus();
            //    return false;
            //}

            YearMonth = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue;
            DateTime lastMonthDate= DateTime.Now.AddMonths(-1);
            //只能修改当前月的前一月工资，比如现在是2017-10月份 那就计算2017-9月份的工资
            if (lastMonthDate.ToString("yyyy-MM") != YearMonth)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 非本月计算工资 不得修改！');</script>");
                return false;
            }
            if (CommHelp.VerifesToNum(txtBasicSalary.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('基本工资 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtFullAttendence.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('全勤奖 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtMobileFee.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('通讯费 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtSpecialAward.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('特殊奖励 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtGonglin.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工龄工资 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtPositionPerformance.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('岗位考核 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtPositionFee.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('职务津贴 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtWorkPerformance.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工作绩效 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(lblFullPayment.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工资合计 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(lblDefaultWorkDays.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('默认出勤天数 格式错误！');</script>");
                return false;
            }


            if (CommHelp.VerifesToNum(txtWorkDays.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 出勤天数 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(lblShouldPayment.Text.Trim()) == false)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 应发工资 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtUnionFee.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('  工会费 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtDeduction.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 扣款 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtYangLaoJin.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 养老金 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(lblActualPayment.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('  实发工资 格式错误！');</script>");
                return false;
            }
            return true;
        }

        private void IniData(string Yearmonth)
        {
            HR_Payment Payment = this.PaymentSer.GetModel(Convert.ToInt32(base.Request["Code"]),Yearmonth);
            HR_PERSON Salary = this.perSer.GetSalary(Convert.ToInt32(base.Request["Code"]));
            if (Payment != null)
            {
                this.lblName.Text = Payment.Name;
                this.ddlYear.SelectedValue = Payment.YearMonth.Substring(0, 4);
                this.ddlMonth.SelectedValue = Payment.YearMonth.Substring(5, 2);
                this.txtBasicSalary.Text = Payment.BasicSalary.ToString();
                this.txtFullAttendence.Text = Payment.FullAttendence.ToString();
                this.txtMobileFee.Text = Payment.MobileFee.ToString();
                this.txtSpecialAward.Text = Payment.SpecialAward.ToString();
                this.txtSpecialAwardNote.Text = Payment.SpecialAwardNote.ToString();
                this.txtGonglin.Text = Payment.GongLin.ToString();
                this.txtPositionPerformance.Text = Payment.PositionPerformance.ToString();
                this.txtPositionFee.Text = Payment.PositionFee.ToString();
                this.txtWorkPerformance.Text = Payment.WorkPerformance.ToString();
                this.lblFullPayment.Text = Payment.FullPayment.ToString();
                this.txtWorkDays.Text = Payment.WorkDays.ToString();
                this.lblDefaultWorkDays.Text = Payment.DefaultWorkDays.ToString();
                this.lblShouldPayment.Text = Payment.ShouldPayment.ToString();
                this.txtUnionFee.Text = Payment.UnionFee.ToString();
                this.txtDeduction.Text = Payment.Deduction.ToString();
                this.txtDeductionNote.Text = Payment.DeductionNote.ToString();
                this.txtYangLaoJin.Text = Payment.YangLaoJin.ToString();
                this.lblActualPayment.Text = Payment.ActualPayment.ToString();
                if (Payment.UpdateTime != null)
                {
                    this.txtUpdateTime.Text = Payment.UpdateTime.ToString();
                }
                if (Payment.UpdatePersonName != null)
                {
                    txtUpdatePerson.Text = Payment.UpdatePersonName.ToString();
                }
            }
            else
            {
                this.lblName.Text = Salary.Name;
                this.txtBasicSalary.Text = Salary.BasicSalary.ToString();
                this.txtGonglin.Text = Salary.GongLin.ToString();
                this.txtMobileFee.Text = Salary.MobileFee.ToString();
                this.txtPositionFee.Text = Salary.PositionFee.ToString();
                this.txtYangLaoJin.Text = Salary.YangLaoJin.ToString();
                this.txtUnionFee.Text = Salary.UnionFee.ToString();
                this.lblDefaultWorkDays.Text = Salary.DefaultWorkDays.ToString();
            }
            this.ChkRetailed.Checked = Salary.IsRetailed;
            this.ChkQuit.Checked = Salary.IsQuit;
            if (Salary.OnBoardTime != null)
            {
                OnBoadTime = (DateTime)Salary.OnBoardTime;
                this.lblOnBoardTime.Text = OnBoadTime.ToString("yyyy-MM-dd");
            }
            if (Salary.QuitTime != null)
            {
                QuitTime = (DateTime)Salary.OnBoardTime;
                this.lblQuitTime.Text = QuitTime.ToString("yyyy-MM-dd");
            }
            if (this.ChkRetailed.Checked)
            {
                this.txtPositionPerformance.Enabled = false;
                this.txtPositionPerformance.Text = "0";
                this.txtWorkPerformance.Enabled = false;
                this.txtWorkPerformance.Text = "0";
                this.txtYangLaoJin.Enabled = false;
                this.txtYangLaoJin.Text = "0";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                ddlYear.Items.Add(new ListItem("--选择--"));
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if (base.Request["Code"] != null)
                {
                    IniData(base.Request["Yearmonth"]);
                }
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            YearMonth = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue;
            if (ddlYear.SelectedValue != null && ddlMonth.SelectedValue != null && ChkRetailed.Checked == false)
            {
                DataTable PAScore = PASer.GetAllPAFormListForPayment(base.Request["Code"], YearMonth);
                if (PAScore.Rows.Count == 1)
                {
                    this.txtPositionPerformance.Text = Math.Round(decimal.Parse(PAScore.Rows[0]["PAScoreSumPosition"].ToString()), 1, MidpointRounding.AwayFromZero).ToString();
                    this.txtWorkPerformance.Text = Math.Round(decimal.Parse(PAScore.Rows[0]["PAScoreSumWork"].ToString()), 1, MidpointRounding.AwayFromZero).ToString();
                }

                IniData(YearMonth);
            }
        }

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            if (FormCheck() == false)
            {
                return;
            }
            YearMonth = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue;
            BasicSalary = decimal.Parse(txtBasicSalary.Text.Trim());
            FullAttendence = decimal.Parse(txtFullAttendence.Text.Trim());
            MobileFee = decimal.Parse(txtMobileFee.Text.Trim());
            SpecialAward = decimal.Parse(txtSpecialAward.Text.Trim());
            GongLin = decimal.Parse(txtGonglin.Text.Trim());
            PositionPerformance = decimal.Parse(txtPositionPerformance.Text.Trim());
            PositionFee = decimal.Parse(txtPositionFee.Text.Trim());
            WorkPerformance = decimal.Parse(txtWorkPerformance.Text.Trim());
            FullPayment = decimal.Parse(lblFullPayment.Text.Trim());
            DefaultWorkDays = decimal.Parse(this.lblDefaultWorkDays.Text.Trim());
            WorkDays = decimal.Parse(txtWorkDays.Text.Trim());
            UnionFee = decimal.Parse(txtUnionFee.Text);
            Deduction = decimal.Parse(txtDeduction.Text.Trim());
            YangLaoJin = decimal.Parse(txtYangLaoJin.Text);
            ActualPayment = decimal.Parse(lblActualPayment.Text.Trim());
            OnBoadTime = DateTime.Parse(lblOnBoardTime.Text.Trim());
            if (lblQuitTime.Text.Trim() != "")
            {
                QuitTime = DateTime.Parse(lblQuitTime.Text.Trim());
            }
            FullPayment = BasicSalary + FullAttendence + MobileFee + PositionPerformance + PositionFee + WorkPerformance;
            ShouldPayment = Math.Round(FullPayment / DefaultWorkDays * WorkDays, 2, MidpointRounding.AwayFromZero) + SpecialAward + GongLin;
            //if ((DateTime.Parse(YearMonth + "-01") - OnBoadTime.AddMonths(1)).TotalDays / 365 * 12 < 0)
            //{
            //    UnionFee = 0;
            //}
            if (lblQuitTime.Text.Trim() != "")
            {
                if (this.ChkQuit.Checked && (DateTime.Parse(YearMonth + "-01") - QuitTime.AddMonths(-1)).TotalDays / 365 * 12 < 0)
                {
                    UnionFee = 0;
                }
            }
            ActualPayment = Math.Round(ShouldPayment - UnionFee - Deduction - YangLaoJin, 1, MidpointRounding.AwayFromZero);
            lblFullPayment.Text = FullPayment.ToString();
            lblShouldPayment.Text = ShouldPayment.ToString();
            lblActualPayment.Text = ActualPayment.ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/HR/PaymentInfo.aspx?Code=" + base.Request["Code"] + "&Yearmonth=");
        }
    }
}
