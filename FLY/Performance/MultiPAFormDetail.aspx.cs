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
    public partial class MultiPAFormDetail : BasePage
    {
        private ApprovePAFormMultiService PAFormMultiSer = new ApprovePAFormMultiService();
        private PAFormMulti thisPAFormMulti = new PAFormMulti();
        private SysUserService UserSer = new SysUserService();
        private DataTable PAMultiDetail;
        protected PAFormMulti getSelectedMulti()
        {
            List<int> PAFormID = new List<int>(); 
            List<int> PAItem = new List<int>();
            List<decimal> MultiReviewScore = new List<decimal>();
            List<bool> IsReview = new List<bool>();
            List<string> PANote = new List<string>();
            for (int i=0; i < gvList.Rows.Count; i++)
            {

                PAFormID.Add(int.Parse(gvList.DataKeys[i][0].ToString()));
                PAItem.Add(int.Parse(gvList.DataKeys[i][1].ToString()));
                TextBox txtMultiReviewScore = gvList.Rows[i].FindControl("txtMultiReviewScore") as TextBox;
                MultiReviewScore.Add(decimal.Parse(txtMultiReviewScore.Text.Trim().ToString()));
                if (MultiReviewScore[i] != 0)
                {
                    IsReview.Add(true);
                }
                else
                {
                    IsReview.Add(false);
                }
                TextBox textPANote = gvList.Rows[i].FindControl("txtNote") as TextBox;
                PANote.Add(textPANote.Text.Trim().ToString());
            }
            thisPAFormMulti.PAFormID = PAFormID;
            thisPAFormMulti.PAItem = PAItem;
            thisPAFormMulti.PAIsReview = IsReview;
            thisPAFormMulti.PAMultiReviewScore = MultiReviewScore;
            thisPAFormMulti.PANote = PANote;
            return thisPAFormMulti;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/MultiPAFormDetail.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
        }

        public bool FormCheck()
        {        
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             PAMultiDetail=PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(),"",DateTime.Today.AddMonths(-1).ToString("yyyy-MM"));
            if (!base.IsPostBack)
            {
                ViewState["SortField"] = "loginName"; 
                ViewState["OrderDirection"] = "Desc"; 
                ddlUser.DataSource = UserSer.getAllUserByLoginName(null);
                ddlUser.DataTextField = "loginName";
                ddlUser.DataValueField = "ID";
                ddlUser.DataBind();
                gvList.DataSource = PAMultiDetail;
                gvList.DataBind();
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if (NewShowAll_textName("我要打分的绩效考核表", "预设"))
                {
                    btnYuShe.Visible = true;                    
                }
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && PAMultiDetail.Rows.Count>0)
            {
                Label lblMultiReviewScore = e.Row.FindControl("lblMultiReviewScore") as Label;
                TextBox txtMultiReviewScore = e.Row.FindControl("txtMultiReviewScore") as TextBox;
                Label lblNote = e.Row.FindControl("lblNote") as Label;
                TextBox txtNote = e.Row.FindControl("txtNote") as TextBox;
                //if (int.Parse(PAMultiDetail.Rows[e.Row.RowIndex]["Status"].ToString()) < 3)
                //{
                lblMultiReviewScore.Visible = false;
                txtMultiReviewScore.Visible = true;
                lblNote.Visible = false;
                txtNote.Visible = true;
                //}
                //else
                //{
                //    lblMultiReviewScore.Visible = true;
                //    txtMultiReviewScore.Visible = false;
                //    lblNote.Visible = true;
                //    txtNote.Visible = false;
                //}
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    if (PAFormMultiSer.Update(Session["currentUserId"].ToString(), getSelectedMulti()) == 0)
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

        protected void txtMultiReviewScore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result;
                TextBox thisMultiScore = sender as TextBox;
                if (decimal.TryParse(thisMultiScore.Text.ToString(), out result) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('输入数字错误！');</script>");
                    thisMultiScore.Text = "0";
                }
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (Chk_All.Checked)
            {
                PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), ddlUser.SelectedValue,  "");
            }
            else
            {
                if (ddlYear.SelectedValue == "" || ddlMonth.SelectedValue == "")
                {
                    PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), "", DateTime.Today.AddMonths(-1).ToString("yyyy-MM"));
                }
                else
                {
                    PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), ddlUser.SelectedValue, ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue);
                }

            }
           
            gvList.DataSource = PAMultiDetail;
            gvList.DataBind(); 
        }

        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string SortField = e.SortExpression;
            if (ViewState["SortField"].ToString() == SortField)
            {
                if (ViewState["OrderDirection"].ToString() == "Desc")
                {
                    ViewState["OrderDirection"] = "Asc";
                }
                else
                {
                    ViewState["OrderDirection"] = "Desc";
                }
            }
            else 
            {
                ViewState["SortField"] = e.SortExpression;
                if (Chk_All.Checked)
                {
                    PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), ddlUser.SelectedValue, "");
                }
                else
                {
                    if (ddlYear.SelectedValue == "" || ddlMonth.SelectedValue == "")
                    {
                        PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), "", DateTime.Today.AddMonths(-1).ToString("yyyy-MM"));
                    }
                    else
                    {
                    PAMultiDetail = PAFormMultiSer.GetPAFormMulti(this.Session["currentUserId"].ToString(), ddlUser.SelectedValue, ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue);
                    }

                }
                DataView dv = PAMultiDetail.DefaultView;
                dv.Sort = (string)ViewState["SortField"] + " " + (string)ViewState["OrderDirection"]; 
                gvList .DataSource = dv;
                gvList.DataBind(); 
            } 
        }

        protected void btnYuShe_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                TextBox txtMultiReviewScore = gvList.Rows[i].FindControl("txtMultiReviewScore") as TextBox;
                Label lblPAItemScore = gvList.Rows[i].FindControl("lblPAItemScore") as Label;
                if (txtMultiReviewScore != null && lblPAItemScore != null)
                {
                    txtMultiReviewScore.Text = lblPAItemScore.Text;
                }
            }
        }
    }
}
