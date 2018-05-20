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
    public partial class A_PATemplateCmd : System.Web.UI.Page
    {
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private A_PASectionService PASectionSer = new A_PASectionService();
        private A_PAItemService PAItemSer = new A_PAItemService();
        private A_PATemplate PATemplate = new A_PATemplate();
        private List<A_PASection> PASection = new List<A_PASection>();
        private SysUserService UserSer = new SysUserService();
        private DataTable PAItemDetail;
        private DataTable AllUserTable;
        private DataTable UserTable;
        private DataView UserView;
        private DropDownList AllUserDropDownList=new DropDownList();
        private List<int> sequence=new List<int>();

        protected A_PATemplate getSelectedDetail()
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
            A_PATemplate PATemplate = new A_PATemplate();
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
                   IsMultiReview.Add(ASPxcbMultiReview.Checked );
                   ASPxListBox albMultiReview = gvList.Rows[i].FindControl("albMultiReview") as ASPxListBox;
                   MultiReviewUserID.Add(getEachMultiReviewID(albMultiReview));
                }
            } 
            PATemplate.A_PATemplateItem = TemplateItem;
            PATemplate.A_PAItemSequence = ItemSequence;
            PATemplate.A_PATemplateSection = TemplateSection;
            PATemplate.A_PATemplateScore = TemplateScore;
            PATemplate.A_PATemplateAmount = TemplateAmount;
            PATemplate.A_PATemplateIsFirstReview = IsFirstReview;
            PATemplate.A_PATemplateFirstReviewUserID = FirstReviewUserID;
            PATemplate.A_PATemplateIsSecondReview = IsSecondReview;
            PATemplate.A_PATemplateSecondReviewUserID = SecondReviewUserID;
            PATemplate.A_PATemplateIsMultiReview = IsMultiReview;
            PATemplate.A_PATemplateMultiReviewUserID = MultiReviewUserID;
            PATemplate.A_PATemplateName = this.txtPATemplateName.Text.Trim();
            if (base.Request["PATemplateId"]!=null)
            {
                PATemplate.A_PATemplateID = Convert.ToInt32(base.Request["PATemplateId"]);
            }
            return PATemplate;
        }

        protected List<int> getEachMultiReviewID(ASPxListBox listbox)
        { 
            List<int> eachReviewID=new List<int>();
            for(int i=0;i<listbox.SelectedItems.Count;i++)
            {
                eachReviewID.Add(int.Parse(listbox.SelectedValues[i].ToString()));
            }
            return eachReviewID;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    if (this.PATemplateSer.Add(getSelectedDetail()) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        this.txtPATemplateName.Text = "";
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
            base.Response.Redirect("~/Performance/A_PATemplateList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtPATemplateName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    PATemplateSer.Update(getSelectedDetail());
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
            if (this.txtPATemplateName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写绩效考核模版名称！');</script>");
                this.txtPATemplateName.Focus();
                return false;
            }
            if (base.Request["PATemplateId"] != null)
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PATemplate where A_PATemplateName='{0}' and A_PATemplateId<>{1}", this.txtPATemplateName.Text.Trim(), base.Request["PATemplateId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核模版名称已经存在,请重新填写！');</script>");
                    this.txtPATemplateName.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PATemplate where A_PATemplateName='{0}'", this.txtPATemplateName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核模版名称已经存在,请重新填写！');</script>");
                    this.txtPATemplateName.Focus();
                    return false;
                }
            }
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
                AllUserDropDownList.DataSource = AllUserTable;
                AllUserDropDownList.DataTextField = "LoginName";
                AllUserDropDownList.DataValueField = "Id";
                AllUserDropDownList.DataBind();
                if (base.Request["PATemplateId"] != null)
                {
                    this.btnAdd.Visible = false;
                    A_PATemplate PATemplate = this.PATemplateSer.GetModelList("A_PATemplateId=" + base.Request["PATemplateId"])[0];
                    this.txtPATemplateName.Text = PATemplate.A_PATemplateName;
                    PAItemDetail=this.PATemplateSer.GetDetailList(base.Request["PATemplateId"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind(); 
                }
                else
                {
                    this.btnUpdate.Visible = false;
                    this.gvList.DataSource = this.PATemplateSer.GetDetailList("");
                    this.gvList.DataBind(); 
                }
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {            
                //DropDownList ddlPASection = e.Row.FindControl("ddlPASection") as DropDownList;
                //if (ddlPASection != null)
                //{
                //    ddlPASection.DataSource = PASection;
                //    ddlPASection.DataTextField = "A_PASectionName";
                //    ddlPASection.DataValueField = "A_PASectionID";
                //    ddlPASection.DataBind();
                //}
               // DropDownList ddlFirstReviewUserID = e.Row.FindControl("ddlFirstReviewUserID") as DropDownList;
               // if (ddlFirstReviewUserID != null)
               // {
               //     //ddlFirstReviewUserID = AllUserDropDownList;
               //     //ddlFirstReviewUserID.DataSource = AllUserTable;
               //     //ddlFirstReviewUserID.DataTextField = "LoginName";
               //     //ddlFirstReviewUserID.DataValueField = "Id";
               //     //ddlFirstReviewUserID.DataBind();
               //}
               // DropDownList ddlSecondReviewUserID = e.Row.FindControl("ddlSecondReviewUserID") as DropDownList;
               // if (ddlSecondReviewUserID != null)
               // {
               //     //ddlSecondReviewUserID = AllUserDropDownList;
               //     //ddlSecondReviewUserID.DataSource = AllUserTable;
               //     //ddlSecondReviewUserID.DataTextField = "LoginName";
               //     //ddlSecondReviewUserID.DataValueField = "Id";
               //     //ddlSecondReviewUserID.DataBind();
               //}
               // DropDownList ddlSequence = e.Row.FindControl("ddlSequence") as DropDownList;
               // if (ddlSequence != null)
               // {
               //     ddlSequence.DataSource = sequence;
               //     ddlSequence.DataBind();
               // }
                //if (base.Request["PATemplateId"] != null)
                //{
                //    UserTable = UserSer.getUserTableByLoginNameForPAForm(base.Request["PATemplateId"], PAItemDetail.Rows[e.Row.RowIndex]["Base_A_PAItemID"].ToString());
                //}
                //else
                //{
                //    UserTable = UserSer.getUserTableByLoginName(null);
                //}
                //ASPxListBox albMultiReview = e.Row.FindControl("albMultiReview") as ASPxListBox;
                //if (albMultiReview != null)
                //{
                //    albMultiReview.DataSource = UserTable;
                //    albMultiReview.ValueField  = "ID";
                //    albMultiReview.TextField = "loginName";
                //    albMultiReview.DataBind();
                //}             
                //ASPxCheckBox ASPxcbFirstReview = e.Row.FindControl("ASPxcbFirstReview") as ASPxCheckBox;
                //ASPxCheckBox ASPxcbSecondReview = e.Row.FindControl("ASPxcbSecondReview") as ASPxCheckBox;
                //ASPxCheckBox ASPxcbMultiReview = e.Row.FindControl("ASPxcbMultiReview") as ASPxCheckBox;
                //ASPxcbFirstReview.ClientSideEvents.CheckedChanged = "function(s, e){SingleReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                //ASPxcbSecondReview.ClientSideEvents.CheckedChanged = "function(s, e){SingleReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                //ASPxcbMultiReview.ClientSideEvents.CheckedChanged = "function(s, e){MultiReviewCheck('" + ASPxcbFirstReview.ClientID + "','" + ddlFirstReviewUserID.ClientID + "','" + ASPxcbSecondReview.ClientID + "','" + ddlSecondReviewUserID.ClientID + "','" + ASPxcbMultiReview.ClientID + "','" + albMultiReview.ClientID + "');}";
                if (base.Request["PATemplateId"] != null)
                {
                    //ddlSequence.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["Sequence"].ToString();
                    //ddlPASection.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["A_PASectionID"].ToString();
                    //ddlFirstReviewUserID.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["A_PAFirstReviewUserID"].ToString();
                    
                    //ddlSecondReviewUserID.SelectedValue = PAItemDetail.Rows[e.Row.RowIndex]["A_PASecondReviewUserID"].ToString();
                    //if (PAItemDetail.Rows[e.Row.RowIndex]["A_PAItemID"].ToString() != "")
                    //{
                    //    ddlFirstReviewUserID.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["A_PAIsMultiReview"].ToString()) == true ? false : true;
                    //    ddlSecondReviewUserID.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["A_PAIsMultiReview"].ToString()) == true ? false : true;
                    //    albMultiReview.Enabled = bool.Parse(PAItemDetail.Rows[e.Row.RowIndex]["A_PAIsMultiReview"].ToString()) == true ? true : false;
                    //}
                    //for (int i = 0; i < albMultiReview.Items.Count; i++)
                    //{
                    //    UserView = new DataView(UserTable);
                    //    UserView.RowFilter = " ID=" + albMultiReview.Items[i].Value.ToString() + " and [User_ID] is not null";
                    //    if (UserView.Count > 0)
                    //    {
                    //        albMultiReview.Items[i].Selected = true;
                    //    }
                    //}
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
