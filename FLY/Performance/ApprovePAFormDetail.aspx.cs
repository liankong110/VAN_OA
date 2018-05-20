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
using VAN_OA.Dal.Performance;
using VAN_OA.Model.Performance;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;

namespace VAN_OA.Performance
{
    public partial class ApprovePAFormDetail :BasePage
    {
        private ApprovePAFormDetailService PAFormDetailSer = new ApprovePAFormDetailService();
        private PAFormDetail thisPAFormDetail = new PAFormDetail();
        private DataTable TablePAFormDetail;
        private List<string> Section = new List<string>();
        private List<decimal> ScoreSum = new List<decimal>();
        private List<decimal> FirstSum = new List<decimal>();
        private List<decimal> SecondSum = new List<decimal>();
        private List<decimal> AmountSum = new List<decimal>();
        protected PAFormDetail getSelectedDetail()
        {
            List<int> PAItem=new List<int>();
            List<int> FirstReviewUserID = new List<int>();
            List<decimal> FirstReviewScore = new List<decimal>();
            List<int> SecondReviewUserID = new List<int>();
            List<decimal> SecondReviewScore = new List<decimal>();
            List<decimal> PAItemAmount = new List<decimal>();
            List<string> PANote = new List<string>();
            for (int i=0; i < gvList.Rows.Count; i++)
            {

                PAItem.Add(int.Parse(gvList.DataKeys[i][1].ToString()));
                FirstReviewUserID.Add(int.Parse(gvList.DataKeys[i][2].ToString()));
                TextBox txtFirstReviewScore = gvList.Rows[i].FindControl("txtFirstReviewScore") as TextBox;
                FirstReviewScore.Add(decimal.Parse(txtFirstReviewScore.Text.Trim().ToString()));
                SecondReviewUserID.Add(int.Parse(gvList.DataKeys[i][3].ToString()));
                TextBox txtSecondReviewScore = gvList.Rows[i].FindControl("txtSecondReviewScore") as TextBox;
                SecondReviewScore.Add(decimal.Parse(txtSecondReviewScore.Text.Trim().ToString()));
                TextBox textPAItemAmount = gvList.Rows[i].FindControl("txtPAItemAmount") as TextBox;
                PAItemAmount.Add(decimal.Parse(textPAItemAmount.Text.Trim().ToString()));
                TextBox textPANote = gvList.Rows[i].FindControl("txtNote") as TextBox;
                PANote.Add(textPANote.Text.Trim().ToString());

            }
            thisPAFormDetail.PAItem = PAItem;
            thisPAFormDetail.Status = int.Parse(lblStatus.Text.ToString());
            thisPAFormDetail.PAFirstReviewUserID = FirstReviewUserID;
            thisPAFormDetail.PAFirstReviewScore = FirstReviewScore;
            thisPAFormDetail.PASecondReviewUserID = SecondReviewUserID;
            thisPAFormDetail.PASecondReviewScore = SecondReviewScore;
            thisPAFormDetail.PAAmount = PAItemAmount;
            thisPAFormDetail.PANote = PANote;
            thisPAFormDetail.AttendDays = decimal.Parse(txtAttendDays.Text.Trim().ToString());
            thisPAFormDetail.LeaveDays = decimal.Parse(txtLeaveDays.Text.Trim().ToString());
            thisPAFormDetail.FullAttendBonus = decimal.Parse(txtFullAttendBonus.Text.Trim().ToString()); 
            return thisPAFormDetail;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/ApprovePAFormList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            txtAttendDays.Text = "";
            txtLeaveDays.Text = "";
            txtFullAttendBonus.Text = "";
        }

        public bool FormCheck()
        {        
            if (txtAttendDays.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出勤天数！');</script>");
                this.txtAttendDays.Focus();
                return false;
            }
            if (txtLeaveDays.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写假期！');</script>");
                this.txtLeaveDays.Focus();
                return false;
            }
            if (txtFullAttendBonus.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写全勤奖！');</script>");
                this.txtFullAttendBonus.Focus();
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Section = PAFormDetailSer.GetPAFormSection(base.Request["PAFormId"]);
            for (int i=0;i<Section.Count;i++)
            {
                ScoreSum.Add(0);
                FirstSum.Add(0);
                SecondSum.Add(0);
                AmountSum.Add(0);
            }  
            if (!base.IsPostBack)
            {

                thisPAFormDetail = PAFormDetailSer.GetPAFormHead(base.Request["PAFormId"]);
                lblStatus.Text = thisPAFormDetail.Status.ToString();
                lblUserName.Text = thisPAFormDetail.UserName.ToString();
                lblDepartment.Text = thisPAFormDetail.UserIPosition.ToString();
                lblMonth.Text = thisPAFormDetail.Month.ToString();
                txtAttendDays.Text = thisPAFormDetail.AttendDays.ToString();
                txtLeaveDays.Text = thisPAFormDetail.LeaveDays.ToString();
                lblAttendDays.Text = thisPAFormDetail.AttendDays.ToString(); 
                lblLeaveDays.Text = thisPAFormDetail.LeaveDays.ToString(); 
                lblFullAttendBonus.Text = thisPAFormDetail.AttendDays.ToString(); 
                txtFullAttendBonus.Text = thisPAFormDetail.FullAttendBonus.ToString();
                if (thisPAFormDetail.Status==0)
                {
                    string strSql1 = "update tb_UserMonthPAFormHead set Status=1 where PAFormID=" + base.Request["PAFormId"];
                    object obj1 = DBHelp.ExeScalar(strSql1.ToString());
                    lblStatus.Text = "1";
                }
                if  (thisPAFormDetail.Status>2)
                {
                    btnSave.Visible=false;
                    btnSet.Visible=false;
                    lblAttendDays.Visible = true;
                    lblLeaveDays.Visible = true;
                    lblFullAttendBonus.Visible = true;
                    txtAttendDays.Visible = false;
                    txtLeaveDays.Visible = false;
                    txtFullAttendBonus.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnSet.Visible = true;
                    lblAttendDays.Visible = false;
                    lblLeaveDays.Visible = false;
                    lblFullAttendBonus.Visible = false;
                    txtAttendDays.Visible = true;
                    txtLeaveDays.Visible = true;
                    txtFullAttendBonus.Visible = true;
                }
                if (NewShowAll_textName("我要评估的绩效考核表", "预设"))
                {
                    ViewState["Load"] = "1";
                }

                TablePAFormDetail=PAFormDetailSer.GetPAFormDetail(base.Request["PAFormId"]);
                gvList.DataSource = TablePAFormDetail;
                gvList.DataBind();

             
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (TablePAFormDetail.Rows[e.Row.RowIndex]["IsMultiReview"].ToString() != "True")
                {
                    e.Row.Cells[8].Text = "";
                    e.Row.Cells[9].Text = "";
                }
                Label lblFirstReviewScore = e.Row.FindControl("lblFirstReviewScore") as Label;
                TextBox txtFirstReviewScore = e.Row.FindControl("txtFirstReviewScore") as TextBox;
                Label lblSecondReviewScore = e.Row.FindControl("lblSecondReviewScore") as Label;
                TextBox txtSecondReviewScore = e.Row.FindControl("txtSecondReviewScore") as TextBox;
                Label lblPAItemAmount = e.Row.FindControl("lblPAItemAmount") as Label;
                TextBox txtPAItemAmount = e.Row.FindControl("txtPAItemAmount") as TextBox;
                Label lblNote = e.Row.FindControl("lblNote") as Label;
                TextBox txtNote = e.Row.FindControl("txtNote") as TextBox;
                //初评人和复评人相同
                if (base.Session["currentUserId"].ToString() == TablePAFormDetail.Rows[e.Row.RowIndex]["FirstReviewUserID"].ToString() && base.Session["currentUserId"].ToString() == TablePAFormDetail.Rows[e.Row.RowIndex]["SecondReviewUserID"].ToString())
                {
                    if (int.Parse(lblStatus.Text) == 1)
                    {
                        lblFirstReviewScore.Visible = false;
                        txtFirstReviewScore.Visible = true;
                        if (ViewState["Load"] == "1")
                        {
                            txtFirstReviewScore.Text = TablePAFormDetail.Rows[e.Row.RowIndex]["PAItemScore"].ToString();
                        }
                    }
                    else
                    {
                        lblFirstReviewScore.Visible = true;
                        txtFirstReviewScore.Visible = false;
                    }
                    lblPAItemAmount.Visible = false;
                    txtPAItemAmount.Visible = true;
                    lblSecondReviewScore.Visible = false;
                    txtSecondReviewScore.Visible = true;
                    if (ViewState["Load"] == "1")
                    {
                        txtSecondReviewScore.Text = TablePAFormDetail.Rows[e.Row.RowIndex]["PAItemScore"].ToString();
                    }
                    lblNote.Visible = false;
                    txtNote.Visible = true;
                }
                else
                {
                    if (int.Parse(lblStatus.Text) < 2)
                    {
                        if (base.Session["currentUserId"].ToString() == TablePAFormDetail.Rows[e.Row.RowIndex]["FirstReviewUserID"].ToString())
                        {
                            lblFirstReviewScore.Visible = false;
                            txtFirstReviewScore.Visible = true;
                            lblPAItemAmount.Visible = false;
                            txtPAItemAmount.Visible = true;
                            if (ViewState["Load"] == "1")
                            {
                                txtFirstReviewScore.Text = TablePAFormDetail.Rows[e.Row.RowIndex]["PAItemScore"].ToString();
                            }
                        }
                        else
                        {
                            lblFirstReviewScore.Visible = true;
                            txtFirstReviewScore.Visible = false;
                            lblPAItemAmount.Visible = true;
                            txtPAItemAmount.Visible = false;
                        }
                        lblSecondReviewScore.Visible = false;
                        txtSecondReviewScore.Visible = false;
                        lblNote.Visible = false;
                        txtNote.Visible = true;
                    }
                    else if(int.Parse(lblStatus.Text) < 3)
                    {
                        lblFirstReviewScore.Visible = true;
                        txtFirstReviewScore.Visible=false;
                        if (base.Session["currentUserId"].ToString() == TablePAFormDetail.Rows[e.Row.RowIndex]["SecondReviewUserID"].ToString())
                        {
                            lblSecondReviewScore.Visible = false;
                            txtSecondReviewScore.Visible = true;
                            if (ViewState["Load"] == "1")
                            {
                                txtSecondReviewScore.Text = TablePAFormDetail.Rows[e.Row.RowIndex]["PAItemScore"].ToString();
                            }
                            lblPAItemAmount.Visible = false;
                            txtPAItemAmount.Visible = true;
                       }
                        else
                        {
                            lblSecondReviewScore.Visible = true;
                            txtSecondReviewScore.Visible = false;
                            lblPAItemAmount.Visible = true;
                            txtPAItemAmount.Visible = false;
                        }
                        lblNote.Visible = false;
                        txtNote.Visible = true;
                    }
                    else
                    {
                        lblFirstReviewScore.Visible = true;
                        txtFirstReviewScore.Visible=false;
                        lblSecondReviewScore.Visible = true;
                        txtSecondReviewScore.Visible = false;
                        lblPAItemAmount.Visible = true;
                        txtPAItemAmount.Visible = false;
                        lblNote.Visible = true;
                        txtNote.Visible = false;
                    }
                }
                for (int i=0;i<Section.Count;i++)
                {
                    if (Section[i] == TablePAFormDetail.Rows[e.Row.RowIndex]["PASectionName"].ToString())
                    {
                        ScoreSum[i]+=decimal.Parse(e.Row.Cells[2].Text.ToString());
                        if (txtFirstReviewScore.Visible == true)
                        {
                            FirstSum[i] += decimal.Parse(txtFirstReviewScore.Text.ToString());
                        }
                        else
                        { 
                            FirstSum[i] += decimal.Parse(lblFirstReviewScore.Text.ToString());
                        }
                        if (txtSecondReviewScore.Visible==true)
                        {
                            SecondSum[i] += decimal.Parse(txtSecondReviewScore.Text.ToString());
                        }
                        else
                        {
                            SecondSum[i] += decimal.Parse(lblSecondReviewScore.Text.ToString());
                        }
                        if (txtPAItemAmount.Visible == true)
                        {
                            AmountSum[i] += decimal.Parse(txtPAItemAmount.Text.ToString());
                        }
                        else
                        {
                            AmountSum[i] += decimal.Parse(lblPAItemAmount.Text.ToString());
                        }
                   }           
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";
                for (int i = 0; i < Section.Count; i++)
                {
                    e.Row.Cells[2].Text += Section[i] +":"+ScoreSum[i].ToString();
                    e.Row.Cells[4].Text += Section[i] + ":" + FirstSum[i].ToString();
                    e.Row.Cells[6].Text += Section[i] + ":" + SecondSum[i].ToString();
                    e.Row.Cells[7].Text += Section[i] + ":" + AmountSum[i].ToString();
               } 
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    if (PAFormDetailSer.Update(base.Request["PAFormId"], getSelectedDetail(), base.Session["currentUserId"].ToString()) == 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void txtFirstReviewScore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                TextBox thisFirstScore = sender as TextBox;
                if (decimal.TryParse(thisFirstScore.Text.ToString(), out result) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('输入数字错误！');</script>");
                    thisFirstScore.Text = "0";
                }
                else
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        TextBox FirstScore = gvList.Rows[i].FindControl("txtFirstReviewScore") as TextBox;
                        for (int j = 0; j < Section.Count; j++)
                        {
                            if (Section[j] == gvList.Rows[i].Cells[0].Text.ToString())
                            {
                                FirstSum[j] += decimal.Parse(FirstScore.Text.ToString());
                            }
                        }
                    }
                    gvList.FooterRow.Cells[4].Text = "";
                    for (int i = 0; i < Section.Count; i++)
                    {
                        gvList.FooterRow.Cells[4].Text += Section[i] + ":" + FirstSum[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }

        protected void txtSecondReviewScore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                TextBox thisSecondScore = sender as TextBox;
                if (decimal.TryParse(thisSecondScore.Text.ToString(), out result) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('输入数字错误！');</script>");
                    thisSecondScore.Text = "0";
                }
                else
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        TextBox SecondScore = gvList.Rows[i].FindControl("txtSecondReviewScore") as TextBox;
                        for (int j = 0; j < Section.Count; j++)
                        {
                            if (Section[j] == gvList.Rows[i].Cells[0].Text.ToString())
                            {
                                SecondSum[j] += decimal.Parse(SecondScore.Text.ToString());
                            }
                        }
                    }
                    gvList.FooterRow.Cells[6].Text = "";
                    for (int i = 0; i < Section.Count; i++)
                    {
                        gvList.FooterRow.Cells[6].Text += Section[i] + ":" + SecondSum[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }

        protected void txtPAItemAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                TextBox thisAmount = sender as TextBox;
                if (decimal.TryParse(thisAmount.Text.ToString(), out result) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('输入数字错误！');</script>");
                    thisAmount.Text = "0";
                }
                else
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        TextBox Amount = gvList.Rows[i].FindControl("txtPAItemAmount") as TextBox;
                        for (int j = 0; j < Section.Count; j++)
                        {
                            if (Section[j] == gvList.Rows[i].Cells[0].Text.ToString())
                            {
                                AmountSum[j] += decimal.Parse(Amount.Text.ToString());
                            }
                        }
                    }
                    gvList.FooterRow.Cells[7].Text = "";
                    for (int i = 0; i < Section.Count; i++)
                    {
                        gvList.FooterRow.Cells[7].Text += Section[i] + ":" + AmountSum[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }
    }
}
