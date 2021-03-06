﻿using System;
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
    public partial class A_PAUserTemplateCmd1 : System.Web.UI.Page
    {
        private A_PAUserTemplateService PAUserTemplateSer = new A_PAUserTemplateService();
        private A_PASectionService PASectionSer = new A_PASectionService();
        private A_PAItemService PAItemSer = new A_PAItemService();
        private A_PATemplate PATemplate = new A_PATemplate();
        private List<A_PASection> PASection = new List<A_PASection>();
        private List<A_PAItem> PAItem = new List<A_PAItem>();
        private SysUserService UserSer = new SysUserService();
        private DataTable PAItemDetail;
        private DataTable SingleUserPAForm;
        private DataTable SingleUserPAFormUser;
        private DataTable AllUserTable;
        private List<int> sequence=new List<int>();
        private List<string> Section = new List<string>();
        private List<decimal> ScoreSum = new List<decimal>();
        private List<decimal> AmountSum = new List<decimal>();
        protected A_PAUserTemplate getSelectedDetail()
        {
            List<int> TemplateItem=new List<int>();
            List<int> ItemSequence = new List<int>();
            List<int> TemplateSection = new List<int>();
            List<decimal> TemplateScore = new List<decimal>();
            List<decimal> TemplateAmount = new List<decimal>();
            List<bool> IsFirstReview = new List<bool>();
            List<int> FirstReviewUserID = new List<int>();
            List<bool> IsSecondReview = new List<bool>();
            List<int> SecondReviewUserID = new List<int>();
            List<bool> IsMultiReview = new List<bool>();
            List<List<int>> MultiReviewUserID = new List<List<int>>();
            A_PAUserTemplate PAUserTemplate = new A_PAUserTemplate();

           TemplateItem.Add(int.Parse(ddlPAItem.SelectedValue.Trim()));
           ItemSequence.Add(int.Parse(ddlSequence.SelectedValue.Trim()));
           TemplateSection.Add(int.Parse(ddlPASection.SelectedValue.Trim()));
           TemplateScore.Add(decimal.Parse(txtPAItemScore.Text.Trim()));
           TemplateAmount.Add(decimal.Parse(txtPAItemAmount.Text.Trim()));
           IsFirstReview.Add(cbFirstReview.Checked);
           FirstReviewUserID.Add(int.Parse(ddlFirstReviewUserID.SelectedValue.Trim()));
           IsSecondReview.Add(cbSecondReview.Checked);
           SecondReviewUserID.Add(int.Parse(ddlSecondReviewUserID.SelectedValue.Trim()));
           IsMultiReview.Add(cbMultiReview.Checked);
           MultiReviewUserID.Add(getEachMultiReviewID(cblMultiReviewUserID));
            PAUserTemplate.UserID  = int.Parse(base.Request["UserID"]);
            PAUserTemplate.A_PATemplateItem = TemplateItem;
            PAUserTemplate.A_Sequence = ItemSequence;
            PAUserTemplate.A_PATemplateSection = TemplateSection;
            PAUserTemplate.A_PATemplateScore = TemplateScore;
            PAUserTemplate.A_PATemplateAmount = TemplateAmount;
            PAUserTemplate.A_PATemplateIsFirstReview = IsFirstReview;
            PAUserTemplate.A_PATemplateFirstReviewUserID = FirstReviewUserID;
            PAUserTemplate.A_PATemplateIsSecondReview = IsSecondReview;
            PAUserTemplate.A_PATemplateSecondReviewUserID = SecondReviewUserID;
            PAUserTemplate.A_PATemplateIsMultiReview = IsMultiReview;
            PAUserTemplate.A_PATemplateMultiReviewUserID = MultiReviewUserID;
            return PAUserTemplate;
        }

        protected List<int> getEachMultiReviewID(CheckBoxList listbox)
        {
            List<int> eachReviewID = new List<int>();
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                if (listbox.Items[i].Selected)
                {
                    eachReviewID.Add(int.Parse(listbox.Items[i].Value.Trim()));
                }
            }
            return eachReviewID;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PAUserTemplateList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    PAUserTemplateSer.UserUpdate(getSelectedDetail());
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {        
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 100;i++ )
            {
                sequence.Add(i);
            }
            Section = PAUserTemplateSer.GetPAFormSection(base.Request["UserID"]);
            for (int i = 0; i < Section.Count; i++)
            {
                ScoreSum.Add(0);
                AmountSum.Add(0);
            }  
            if (!base.IsPostBack)
            {
                ddlSequence.DataSource = sequence;
                ddlSequence.DataBind();
                ddlPAItem.DataSource = PAItemSer.GetTablePAItemList("");
                ddlPAItem.DataTextField = "A_PAItemName";
                ddlPAItem.DataValueField = "A_PAItemId";
                ddlPAItem.DataBind();
                PASection = this.PASectionSer.GetModelList("");
                ddlPASection.DataSource = PASection;
                ddlPASection.DataTextField = "A_PASectionName";
                ddlPASection.DataValueField = "A_PASectionId";
                ddlPASection.DataBind();
                AllUserTable = UserSer.getUserTableByLoginName(" and loginStatus='在职'");
                ddlFirstReviewUserID.DataSource = AllUserTable;
                ddlFirstReviewUserID.DataTextField = "LoginName";
                ddlFirstReviewUserID.DataValueField = "Id";
                ddlFirstReviewUserID.DataBind();
                ddlSecondReviewUserID.DataSource = AllUserTable;
                ddlSecondReviewUserID.DataTextField = "LoginName";
                ddlSecondReviewUserID.DataValueField = "Id";
                ddlSecondReviewUserID.DataBind();
                cblMultiReviewUserID.DataSource = AllUserTable;
                cblMultiReviewUserID.DataTextField = "LoginName";
                cblMultiReviewUserID.DataValueField = "Id";
                cblMultiReviewUserID.DataBind();
                if (base.Request["UserID"] != null)
                {
                    User User = UserSer.getUserByUserId(int.Parse(base.Request["UserID"]));
                    lblUserName.Text = User.LoginName ;
                    PAItemDetail = PAUserTemplateSer.GetUserDetailList(base.Request["UserID"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind();
                    #region SingleItemDisplay
                    if (base.Request["PAItemId"] != null)
                    {
                        SingleUserPAForm = PAUserTemplateSer.GetTableUserPAForm(base.Request["UserId"], base.Request["PAItemId"]);
                        ddlSequence.SelectedValue = SingleUserPAForm.Rows[0]["Sequence"].ToString();
                        ddlPASection.SelectedValue = SingleUserPAForm.Rows[0]["PASectionID"].ToString();
                        ddlPAItem.SelectedValue = SingleUserPAForm.Rows[0]["PAItemID"].ToString();
                        txtPAItemAmount.Text = SingleUserPAForm.Rows[0]["PAItemAmount"].ToString();
                        txtPAItemScore.Text = SingleUserPAForm.Rows[0]["PAItemScore"].ToString();
                        cbFirstReview.Checked = bool.Parse(SingleUserPAForm.Rows[0]["IsFirstReview"].ToString());
                        ddlFirstReviewUserID.SelectedValue = SingleUserPAForm.Rows[0]["FirstReviewUserID"].ToString();
                        cbSecondReview.Checked = bool.Parse(SingleUserPAForm.Rows[0]["IsSecondReview"].ToString());
                        ddlSecondReviewUserID.SelectedValue = SingleUserPAForm.Rows[0]["SecondReviewUserID"].ToString();
                        cbMultiReview.Checked = bool.Parse(SingleUserPAForm.Rows[0]["IsMultiReview"].ToString());
                        SingleUserPAFormUser = PAUserTemplateSer.GetTableUserPAFormUser(base.Request["UserId"], base.Request["PAItemId"]);
                        for (int i = 0; i < cblMultiReviewUserID.Items.Count; i++)
                        {
                            for (int j = 0; j < SingleUserPAFormUser.Rows.Count; j++)
                            {
                                if (cblMultiReviewUserID.Items[i].Value.ToString() == SingleUserPAFormUser.Rows[j]["ReviewID"].ToString())
                                {
                                    cblMultiReviewUserID.Items[i].Selected = true;
                                }
                            }
                        }
                        if (bool.Parse(SingleUserPAForm.Rows[0]["IsFirstReview"].ToString()) == true || bool.Parse(SingleUserPAForm.Rows[0]["IsSecondReview"].ToString()) == true)
                        {
                            ddlFirstReviewUserID.Enabled = true;
                            ddlSecondReviewUserID.Enabled = true;
                            cbMultiReview.Checked = false;
                            cblMultiReviewUserID.Enabled = false;
                        }
                        else
                        {
                            ddlFirstReviewUserID.Enabled = false;
                            ddlSecondReviewUserID.Enabled = false;
                            cbFirstReview.Checked = false;
                            cbSecondReview.Checked = false;
                            cblMultiReviewUserID.Enabled = true;
                        }
                    }
                    #endregion
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.ddlSequence.SelectedIndex = 0;
            this.ddlPASection.SelectedIndex = 0;
            this.ddlPAItem.SelectedIndex = 0;
            this.txtPAItemScore.Text = "";
            this.txtPAItemAmount.Text = "";
            cbFirstReview.Checked = false;
            this.ddlFirstReviewUserID.Enabled = true;
            ddlFirstReviewUserID.SelectedIndex = 0;
            cbSecondReview.Checked = false;
            this.ddlSecondReviewUserID.Enabled = true;
            ddlSecondReviewUserID.SelectedIndex = 0;
            cbMultiReview.Checked = false;
            this.cblMultiReviewUserID.Enabled = true;
            for (int i = 0; i < cblMultiReviewUserID.Items.Count; i++)
            {
                cblMultiReviewUserID.Items[i].Selected = false;
            }
        }
        public bool SingleInsertCheck()
        {
            if (ddlSequence.SelectedValue.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择顺序！');</script>");
                ddlSequence.Focus();
                return false;
            }
            if (this.ddlPASection.SelectedValue.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择科目！');</script>");
                ddlPASection.Focus();
                return false;
            }
            if (base.Request["UserID"] != null)
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_UserPAForm where UserID='{0}' and PAItemID={1}", base.Request["UserID"], ddlPAItem.SelectedValue.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该评分项" + ddlPAItem.SelectedValue.Trim() + "已经存在,请重新选择！');</script>");
                    return false;
                }
            }
            return true;
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.SingleInsertCheck())
            {
                try
                {
                    PAUserTemplateSer.SingleInsert(getSelectedDetail());
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('插入成功！');</script>");
                    PAItemDetail = this.PAUserTemplateSer.GetUserDetailList(base.Request["UserId"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind();
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }
        public bool SingleModifyCheck()
        {
            string checksql;
            if (ddlSequence.SelectedValue.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择顺序！');</script>");
                ddlSequence.Focus();
                return false;
            }
            if (this.ddlPASection.SelectedValue.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择科目！');</script>");
                ddlPASection.Focus();
                return false;
            }

            checksql = string.Format("select count(*) from tb_UserPAForm where UserID='{0}' and PAItemID={1}", base.Request["UserId"], ddlPAItem.SelectedValue.Trim());
                if (Convert.ToInt32(DBHelp.ExeScalar(checksql)) == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该评分项" + ddlPAItem.SelectedValue.Trim() + "不存在,请重新选择！');</script>");
                    return false;
                }
            return true;
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (this.SingleModifyCheck())
            {
                try
                {
                    PAUserTemplateSer.SingleInsert(getSelectedDetail());
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");
                    PAItemDetail = this.PAUserTemplateSer.GetUserDetailList(base.Request["UserId"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind();
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PAUserTemplateCmd1.aspx?UserID=" + base.Request["UserID"] + "&PAItemId=" + gvList.DataKeys[e.NewEditIndex]["PAItemID"].ToString());
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                PAUserTemplateSer.SingleDelete(base.Request["UserID"], gvList.DataKeys[e.RowIndex]["PAItemID"].ToString());
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功！');</script>");
                PAItemDetail = this.PAUserTemplateSer.GetUserDetailList(base.Request["UserID"]);
                this.gvList.DataSource = PAItemDetail;
                this.gvList.DataBind();
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }

        protected void cbSecondReview_CheckedChanged(object sender, EventArgs e)
        {
            ddlFirstReviewUserID.Enabled = true;
            ddlSecondReviewUserID.Enabled = true;
            cbMultiReview.Checked = false;
            cblMultiReviewUserID.Enabled = false;
        }

        protected void cbFirstReview_CheckedChanged(object sender, EventArgs e)
        {
            ddlFirstReviewUserID.Enabled = true;
            ddlSecondReviewUserID.Enabled = true;
            cbMultiReview.Checked = false;
            cblMultiReviewUserID.Enabled = false;
        }

        protected void cbMultiReview_CheckedChanged(object sender, EventArgs e)
        {
            ddlFirstReviewUserID.Enabled = false;
            ddlSecondReviewUserID.Enabled = false;
            cbFirstReview.Checked = false;
            cbSecondReview.Checked = false;
            cblMultiReviewUserID.Enabled = true;
        }

        protected void ddlPAItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            PAItem = PAItemSer.GetModelList(" and A_PAItemID=" + ddlPAItem.SelectedValue);
            txtPAItemAmount.Text = PAItem[0].A_PAItemAmount.ToString();
            txtPAItemScore.Text = PAItem[0].A_PAItemScore.ToString();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < Section.Count; i++)
                {
                    if (Section[i] == PAItemDetail.Rows[e.Row.RowIndex]["A_PASectionName"].ToString())
                    {
                        ScoreSum[i] += decimal.Parse(e.Row.Cells[5].Text.ToString());
                        AmountSum[i] += decimal.Parse(e.Row.Cells[6].Text.ToString());
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";
                for (int i = 0; i < Section.Count; i++)
                {
                    e.Row.Cells[5].Text += Section[i] + ":" + ScoreSum[i].ToString()+"<br>";
                    e.Row.Cells[6].Text += Section[i] + ":" + AmountSum[i].ToString() + "<br>";
                }
            }
        }
    }
}
