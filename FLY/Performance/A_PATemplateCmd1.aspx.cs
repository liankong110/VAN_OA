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
    public partial class A_PATemplateCmd1 : System.Web.UI.Page
    {
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private A_PASectionService PASectionSer = new A_PASectionService();
        private A_PAItemService PAItemSer = new A_PAItemService();
        private A_PATemplate PATemplate = new A_PATemplate();
        private List<A_PASection> PASection = new List<A_PASection>();
        private List<A_PAItem> PAItem = new List<A_PAItem>();
        private SysUserService UserSer = new SysUserService();
        private DataTable PAItemDetail;
        private DataTable SinglePAItem;
        private DataTable SingleMultiUser;
        private DataTable AllUserTable;
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
           TemplateItem.Add(int.Parse(ddlPAItem.SelectedValue.Trim()));
           ItemSequence.Add(int.Parse(this.ddlSequence .SelectedValue.Trim()));
           TemplateSection.Add(int.Parse(ddlPASection.SelectedValue.Trim()));
           TemplateScore.Add(decimal.Parse(txtPAItemScore.Text.Trim()));
           TemplateAmount.Add(decimal.Parse(txtPAItemAmount.Text.Trim()));
           IsFirstReview.Add(cbFirstReview.Checked);
           FirstReviewUserID.Add(int.Parse(ddlFirstReviewUserID.SelectedValue.Trim()));
           IsSecondReview.Add(cbSecondReview.Checked); 
           SecondReviewUserID.Add(int.Parse(ddlSecondReviewUserID.SelectedValue.Trim()));
           IsMultiReview.Add(cbMultiReview.Checked );
           MultiReviewUserID.Add(getEachMultiReviewID(cblMultiReviewUserID));
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

        protected List<int> getEachMultiReviewID(CheckBoxList listbox)
        { 
            List<int> eachReviewID=new List<int>();
            for(int i=0;i<listbox.Items.Count;i++)
            {
                if (listbox.Items[i].Selected )
                { 
                    eachReviewID.Add(int.Parse(listbox.Items[i].Value.Trim()));
                }
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
                if (base.Request["PATemplateId"] != null)
                {
                    A_PATemplate PATemplate = this.PATemplateSer.GetModelList("A_PATemplateId=" + base.Request["PATemplateId"])[0];
                    this.txtPATemplateName.Text = PATemplate.A_PATemplateName;
                    PAItemDetail = this.PATemplateSer.GetDetailList(base.Request["PATemplateId"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind();
                    #region SingleItemDisplay
                    if (base.Request["PAItemId"] != null)
                    {
                        SinglePAItem = PATemplateSer.GetTableTemplateItem(base.Request["PATemplateId"], base.Request["PAItemId"]);
                        ddlSequence.SelectedValue = SinglePAItem.Rows[0]["Sequence"].ToString();
                        ddlPASection.SelectedValue = SinglePAItem.Rows[0]["A_PASectionID"].ToString();
                        ddlPAItem.SelectedValue = SinglePAItem.Rows[0]["A_PAItemID"].ToString();
                        txtPAItemAmount.Text = SinglePAItem.Rows[0]["A_PAItemAmount"].ToString();
                        txtPAItemScore.Text = SinglePAItem.Rows[0]["A_PAItemScore"].ToString();
                        cbFirstReview.Checked = bool.Parse(SinglePAItem.Rows[0]["A_PAIsFirstReview"].ToString());
                        ddlFirstReviewUserID.SelectedValue = SinglePAItem.Rows[0]["A_PAFirstReviewUserID"].ToString();
                        cbSecondReview.Checked = bool.Parse(SinglePAItem.Rows[0]["A_PAIsSecondReview"].ToString());
                        ddlSecondReviewUserID.SelectedValue = SinglePAItem.Rows[0]["A_PASecondReviewUserID"].ToString();
                        cbMultiReview.Checked = bool.Parse(SinglePAItem.Rows[0]["A_PAIsMultiReview"].ToString());
                        SingleMultiUser = PATemplateSer.GetTableTemplateItemMultiUser(base.Request["PATemplateId"], base.Request["PAItemId"]);
                        for (int i = 0; i < cblMultiReviewUserID.Items.Count; i++)
                        {
                            for (int j = 0; j < SingleMultiUser.Rows.Count; j++)
                            {
                                if (cblMultiReviewUserID.Items[i].Value.ToString() == SingleMultiUser.Rows[j]["User_ID"].ToString())
                                {
                                    cblMultiReviewUserID.Items[i].Selected = true;
                                }
                            }
                        }
                        if (bool.Parse(SinglePAItem.Rows[0]["A_PAIsFirstReview"].ToString()) == true || bool.Parse(SinglePAItem.Rows[0]["A_PAIsSecondReview"].ToString()) == true)
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
                    btnNameUpdate.Enabled = true;
                } 
                else
                {
                    btnNameUpdate.Enabled = false;
                }
            }
        }

        protected void cbFirstReview_CheckedChanged(object sender, EventArgs e)
        {
            ddlFirstReviewUserID.Enabled = true;
            ddlSecondReviewUserID.Enabled = true;
            cbMultiReview.Checked = false;
            cblMultiReviewUserID.Enabled = false;
        }

        protected void ddlPAItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            PAItem = PAItemSer.GetModelList(" and A_PAItemID=" + ddlPAItem.SelectedValue);
            txtPAItemAmount.Text = PAItem[0].A_PAItemAmount.ToString();
            txtPAItemScore.Text = PAItem[0].A_PAItemScore.ToString();
        }

        protected void cbSecondReview_CheckedChanged(object sender, EventArgs e)
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
        public bool SingleInsertCheck()
        {
            if (base.Request["PATemplateId"] == null)
            {
                if (this.txtPATemplateName.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写绩效考核模版名称！');</script>");
                    this.txtPATemplateName.Focus();
                    return false;
                }
                if ( Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PATemplate where A_PATemplateName='{0}'", this.txtPATemplateName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核模版名称已经存在,请重新填写！');</script>");
                    this.txtPATemplateName.Focus();
                    return false;
                }
            }

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
            if (base.Request["PATemplateId"] != null)
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PATemplateItem where A_PATemplateId='{0}' and A_PAItemID={1}", base.Request["PATemplateId"],ddlPAItem.SelectedValue.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该评分项" + ddlPAItem.SelectedValue.Trim() + "已经存在,请重新选择！');</script>");
                    this.txtPATemplateName.Focus();
                    return false;
                }
            }
            return true;
        }
        public bool NameUpdateCheck()
        {
            if (this.txtPATemplateName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写绩效考核模版名称！');</script>");
                this.txtPATemplateName.Focus();
                return false;
            }
            if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PATemplate where A_PATemplateName='{0}'", this.txtPATemplateName.Text.Trim()))) > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核模版名称已经被使用,请重新填写！');</script>");
                this.txtPATemplateName.Focus();
                return false;
            }
            return true;
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
            if (base.Request["PATemplateId"] != null)
            {
               
                    checksql=string.Format("select count(*) from A_PATemplateItem where A_PATemplateId='{0}' and A_PAItemID={1}", base.Request["PATemplateId"], ddlPAItem.SelectedValue.Trim());
                if (Convert.ToInt32(DBHelp.ExeScalar(checksql)) == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该评分项" + ddlPAItem.SelectedValue.Trim() + "不存在,请重新选择！');</script>");
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
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.SingleInsertCheck())
            {
                try
                {
                    int PATemplateId;
                    if (base.Request["PATemplateId"]==null)
                    {
                        PATemplateId=PATemplateSer.NewSingleInsert(getSelectedDetail());
                    }
                    else
                    {
                        PATemplateId=PATemplateSer.SingleInsert(getSelectedDetail(),int.Parse(base.Request["PATemplateId"]));
                    }
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('插入成功！');</script>");
                    base.Response.Redirect("~/Performance/A_PATemplateCmd1.aspx?PATemplateId=" + PATemplateId.ToString());
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PATemplateCmd1.aspx?PATemplateId=" + gvList.DataKeys[e.NewEditIndex]["A_PATemplateID"].ToString() + "&PAItemId=" + gvList.DataKeys[e.NewEditIndex]["A_PAItemID"].ToString());
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (this.SingleModifyCheck())
            {
                try
                {
                    PATemplateSer.SingleModify(getSelectedDetail(), base.Request["PATemplateId"]);
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");
                    PAItemDetail = this.PATemplateSer.GetDetailList(base.Request["PATemplateId"]);
                    this.gvList.DataSource = PAItemDetail;
                    this.gvList.DataBind();
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                PATemplateSer.SingleDelete( gvList.DataKeys[e.RowIndex]["A_PATemplateID"].ToString(), gvList.DataKeys[e.RowIndex]["A_PAItemID"].ToString());
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功！');</script>");
                PAItemDetail = this.PATemplateSer.GetDetailList(gvList.DataKeys[e.RowIndex]["A_PAItemID"].ToString());
                this.gvList.DataSource = PAItemDetail;
                this.gvList.DataBind();
            }
            catch (Exception ex)
            {
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
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

        protected void btnNameUpdate_Click(object sender, EventArgs e)
        {
            if (this.NameUpdateCheck())
            {
                try
                {
                    PATemplateSer.NameModify(this.txtPATemplateName.Text.Trim(), base.Request["PATemplateId"]);
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }
    }
}
