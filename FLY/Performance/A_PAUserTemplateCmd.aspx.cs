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
using DevExpress.Web.ASPxEditors;
using System.Collections.Generic;

namespace VAN_OA.Performance
{
    public partial class A_PAUserTemplateCmd : System.Web.UI.Page
    {
        private A_PAUserTemplateService PAUserTemplateSer = new A_PAUserTemplateService();
        private A_PASectionService PASectionSer = new A_PASectionService();
        private A_PAItemService PAItemSer = new A_PAItemService();
        private A_PATemplate PATemplate = new A_PATemplate();
        private List<A_PASection> PASection = new List<A_PASection>();
        private SysUserService UserSer = new SysUserService();
        private DataTable PAItemDetail;
        private DataTable UserTable;
        private DataTable AllUserTable;
        private DataView UserView;
        private List<int> sequence=new List<int>();
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
            for (int i=0; i < gvList.Rows.Count; i++)
            {
                CheckBox  cbsSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox ;
                if (cbsSelect.Checked)
                {
                   TemplateItem.Add(int.Parse(gvList.DataKeys[i].Value.ToString()));
                   DropDownList ddlItemSequence = gvList.Rows[i].FindControl("ddlSequence") as DropDownList;
                   ItemSequence.Add(int.Parse(ddlItemSequence.SelectedValue.ToString()));
                   DropDownList ddlPASection = gvList.Rows[i].FindControl("ddlPASection") as DropDownList;
                   TemplateSection.Add(int.Parse(ddlPASection.SelectedValue.ToString()));
                   TextBox txtScore = gvList.Rows[i].FindControl("txtPAItemScore") as TextBox;
                   TemplateScore.Add(decimal.Parse(txtScore.Text.Trim()));
                   TextBox txtAmount = gvList.Rows[i].FindControl("txtPAItemAmount") as TextBox;
                   TemplateAmount.Add(decimal.Parse(txtAmount.Text.Trim()));
                   ASPxCheckBox ASPxcbFirstReview = gvList.Rows[i].FindControl("ASPxcbFirstReview") as ASPxCheckBox;
                   IsFirstReview.Add(ASPxcbFirstReview.Checked);
                   DropDownList ddlFirstReviewUserID = gvList.Rows[i].FindControl("ddlFirstReviewUserID") as DropDownList;
                   FirstReviewUserID.Add(int.Parse(ddlFirstReviewUserID.SelectedValue.ToString()));
                   ASPxCheckBox ASPxcbSecondReview = gvList.Rows[i].FindControl("ASPxcbSecondReview") as ASPxCheckBox;
                   IsSecondReview.Add(ASPxcbSecondReview.Checked);
                   DropDownList ddlSecondReviewUserID = gvList.Rows[i].FindControl("ddlSecondReviewUserID") as DropDownList;
                   SecondReviewUserID.Add(int.Parse(ddlSecondReviewUserID.SelectedValue.ToString()));
                   ASPxCheckBox ASPxcbMultiReview = gvList.Rows[i].FindControl("ASPxcbMultiReview") as ASPxCheckBox;
                   IsMultiReview.Add(ASPxcbMultiReview.Checked);
                   ASPxListBox albMultiReview = gvList.Rows[i].FindControl("albMultiReview") as ASPxListBox;
                   MultiReviewUserID.Add(getEachMultiReviewID(albMultiReview));
                }
            }
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

        protected List<int> getEachMultiReviewID(ASPxListBox listbox)
        {
            List<int> eachReviewID = new List<int>();
            for (int i = 0; i < listbox.SelectedItems.Count; i++)
            {
                eachReviewID.Add(int.Parse(listbox.SelectedValues[i].ToString()));
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
            if (!base.IsPostBack)
            {
                PASection = this.PASectionSer.GetModelList("");
                AllUserTable = UserSer.getUserTableByLoginName(null);
                if (base.Request["UserID"] != null)
                {
                    User User = UserSer.getUserByUserId(int.Parse(base.Request["UserID"]));
                    lblUserName.Text = User.LoginName ;
                    PAItemDetail = PAUserTemplateSer.GetUserDetailList(base.Request["UserID"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind(); 
                }
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {            
                DropDownList ddlPASection = e.Row.FindControl("ddlPASection") as DropDownList;
                if (ddlPASection != null)
                {
                    ddlPASection.DataSource = PASection;
                    ddlPASection.DataTextField = "A_PASectionName";
                    ddlPASection.DataValueField = "A_PASectionID";
                    ddlPASection.DataBind();
                }
                DropDownList ddlFirstReviewUserID = e.Row.FindControl("ddlFirstReviewUserID") as DropDownList;
                if (ddlFirstReviewUserID != null)
                {
                    ddlFirstReviewUserID.DataSource = AllUserTable;
                    ddlFirstReviewUserID.DataTextField = "LoginName";
                    ddlFirstReviewUserID.DataValueField = "Id";                
                    ddlFirstReviewUserID.DataBind();
               }
                DropDownList ddlSecondReviewUserID = e.Row.FindControl("ddlSecondReviewUserID") as DropDownList;
                if (ddlSecondReviewUserID != null)
                {
                    ddlSecondReviewUserID.DataSource = AllUserTable;
                    ddlSecondReviewUserID.DataTextField = "LoginName";
                    ddlSecondReviewUserID.DataValueField = "Id";
                    ddlSecondReviewUserID.DataBind();
               }
                DropDownList ddlSequence = e.Row.FindControl("ddlSequence") as DropDownList;
                if (ddlSecondReviewUserID != null)
                {
                    ddlSequence.DataSource = sequence;
                    ddlSequence.DataBind();
                }
                UserTable = UserSer.getUserTableByLoginNameForUserPAForm(base.Request["UserID"],PAItemDetail.Rows[e.Row.RowIndex]["PAItemID"].ToString());  
                ASPxListBox albMultiReview = e.Row.FindControl("albMultiReview") as ASPxListBox;
                if (albMultiReview != null)
                {
                    albMultiReview.DataSource = UserTable;
                    albMultiReview.ValueField = "ID";
                    albMultiReview.TextField = "loginName";
                    albMultiReview.DataBind();
                }
                ASPxCheckBox ASPxcbFirstReview = e.Row.FindControl("ASPxcbFirstReview") as ASPxCheckBox;
                ASPxCheckBox ASPxcbSecondReview = e.Row.FindControl("ASPxcbSecondReview") as ASPxCheckBox;
                ASPxCheckBox ASPxcbMultiReview = e.Row.FindControl("ASPxcbMultiReview") as ASPxCheckBox;
                ASPxcbFirstReview.ClientSideEvents.CheckedChanged = "function(s, e){SingleReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                ASPxcbSecondReview.ClientSideEvents.CheckedChanged = "function(s, e){SingleReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                ASPxcbMultiReview.ClientSideEvents.CheckedChanged = "function(s, e){MultiReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                if (base.Request["UserID"] != null)
                {
                    ddlSequence.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["Sequence"].ToString();
                    ddlPASection.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["PASectionID"].ToString();
                    ddlFirstReviewUserID.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["FirstReviewUserID"].ToString();
                    ddlSecondReviewUserID.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["SecondReviewUserID"].ToString();
                    if (PAItemDetail.Rows[e.Row.RowIndex]["PAItemID"].ToString() != "")
                    {
                        ddlFirstReviewUserID.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["IsMultiReview"].ToString()) == true ? false : true;
                        ddlSecondReviewUserID.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["IsMultiReview"].ToString()) == true ? false : true;
                        albMultiReview.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["IsMultiReview"].ToString()) == true ? true : false;
                    }
                    for (int i=0;i<albMultiReview.Items.Count;i++)
                    {
                        UserView = new DataView(UserTable); 
                        UserView.RowFilter=" ID="+albMultiReview.Items[i].Value.ToString()+" and UserID is not null";
                        if (UserView.Count > 0)
                        {
                            albMultiReview.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled == true)
                {
                    cbSelect.Checked = true;
                }
            }
        }

        protected void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled == true)
                {
                    cbSelect.Checked = false;
                }
            }
        }

        protected void btnDisSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled == true)
                {
                    if (cbSelect.Checked == true)
                    {
                        cbSelect.Checked = false;
                    }
                    else
                    {
                        cbSelect.Checked = true;
                    }
                }
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvList.Rows[index];
            ASPxListBox albMultiReview = row.FindControl("albMultiReview") as ASPxListBox;
            switch (e.CommandName)
            {
                case "MultiSelectAll":
                    for (int i = 0; i < albMultiReview.Items.Count; i++)
                    {
                        albMultiReview.Items[i].Selected = true;
                    }
                    break;
                case "MultiUnSelectAll":
                    for (int i = 0; i < albMultiReview.Items.Count; i++)
                    {
                        albMultiReview.Items[i].Selected = false;
                    }
                    break;
                case "MultiDisSelectAll":
                    for (int i = 0; i < albMultiReview.Items.Count; i++)
                    {
                        if (albMultiReview.Items[i].Selected == true)
                        {
                            albMultiReview.Items[i].Selected = false;
                        }
                        else
                        {
                            albMultiReview.Items[i].Selected = true;
                        }
                    }
                    break;
            }
        }
    }
}
