using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.ReportForms;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;


namespace VAN_OA.ReportForms
{
    public partial class MyINSIDEGuestTracks :BasePage
    {
        private TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();

        public string Query = "QueryMyGuestTrack";


       

        private void Show()
        {
            string sql = " 1=1 ";
            QuerySession.QueryGuestTrack QGuestTrack = new VAN_OA.QuerySession.QueryGuestTrack();
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('登记日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time>='{0} 00:00:00'", txtFrom.Text);
                QGuestTrack.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('登记日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time<='{0} 23:59:59'", txtTo.Text);
                QGuestTrack.ToTime = txtTo.Text;
            }
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
                QGuestTrack.GuestName = txtGuestName.Text.Trim();
            }

            if (ddlAE.Text != "" && ddlAE.Text != "0")
            {
                sql += string.Format(" and AE={0}", ddlAE.SelectedItem.Value);
                //QGuestTrack.Apper = txtLoginName.Text;
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

           
            sql += string.Format(" and INSIDE={0}", Session["currentUserId"].ToString());

            sql += string.Format(@" and (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))");


            List<TB_GuestTrack> GuestTracks = this.GuestTrackSer.GetListArray(sql);
            AspNetPager1.RecordCount = GuestTracks.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            Session[Query] = QGuestTrack;
            this.gvList.DataSource = GuestTracks;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //this.GuestTrackSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/MyINSIDEGuestTracks.aspx";
            base.Response.Redirect("~/ReportForms/INSIDEGuestTrack.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User());

                ddlAE.DataSource = user;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";




                WebQuerySessin Sess = new WebQuerySessin(Query);
                //加载SESSION中的数据
                if (Session[Query] != null)
                {

                    //QueryEForms
                    QuerySession.QueryGuestTrack QGuestTrack = Session[Query] as QuerySession.QueryGuestTrack;
                    if (QGuestTrack == null)
                    {
                        return;
                    }
                    if (QGuestTrack.FromTime != "")
                    {

                        txtFrom.Text = QGuestTrack.FromTime;
                    }

                    if (QGuestTrack.ToTime != "")
                    {
                        txtTo.Text = QGuestTrack.ToTime;
                    }
                    if (QGuestTrack.GuestName != "")
                    {
                        txtGuestName.Text = QGuestTrack.GuestName;
                    }

                    Show();
                }
                else
                {
                    List<TB_GuestTrack> TB_GuestTracks = new List<TB_GuestTrack>();
                    this.gvList.DataSource = TB_GuestTracks;
                    this.gvList.DataBind();
                }
            }
        }
    }
}
