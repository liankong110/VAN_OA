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
using VAN_OA.Dal.ReportForms;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;

namespace VAN_OA.ReportForms
{
    public partial class MyGuestTracks : BasePage
    {
        private TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();

        public string Query = "QueryMyGuestTrack";


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/MyGuestTracks.aspx";
            base.Response.Redirect("~/ReportForms/GuestTrack.aspx");
        }


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

            if (ddlINSIDE.Text != "" && ddlINSIDE.Text != "0")
            {
                sql += string.Format(" and INSIDE={0}", ddlINSIDE.SelectedItem.Value);
                //QGuestTrack.Apper = txtLoginName.Text;
            } 
            if (ddlJidu.Text != "")
            {
                sql += string.Format(" and QuartNo='{1}' and YearNo='{0}' ", ddlSelectYears.Text, ddlJidu.Text);
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            sql += string.Format(" and AE={0}", Session["currentUserId"].ToString());

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
            string sql = "select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表'";

            string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表')", this.gvList.DataKeys[e.NewEditIndex].Value);
            object eformIdObj = DBHelp.ExeScalar(efromId);

            object proId = DBHelp.ExeScalar(sql);
            if ((eformIdObj is DBNull) || eformIdObj == null)
            {
                sql = "select ProNo from TB_GuestTrack where id=" + gvList.DataKeys[e.NewEditIndex].Value;
                string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',getdate())", proId,
                    gvList.DataKeys[e.NewEditIndex].Value, DBHelp.ExeScalar(sql));
                DBHelp.ExeCommand(insertEform);
                efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表')", this.gvList.DataKeys[e.NewEditIndex].Value);
                eformIdObj = DBHelp.ExeScalar(efromId);
            }

            string url = "~/ReportForms/GuestTrack.aspx?ProId=" + proId + "&allE_id=" + this.gvList.DataKeys[e.NewEditIndex].Value + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
            Response.Redirect(url);
            //Session["POUrl"] = "~/ReportForms/MyGuestTracks.aspx";
            //base.Response.Redirect("~/ReportForms/GuestTrackEdit.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                List<ListItem> allYears = new List<ListItem>();
                allYears.Add(new ListItem { Text = (2011).ToString(), Value = (2011).ToString() });
                allYears.Add(new ListItem { Text = (2012).ToString(), Value = (2012).ToString() });
                allYears.Add(new ListItem { Text = (2013).ToString(), Value = (2013).ToString() });
                allYears.Add(new ListItem { Text = (2014).ToString(), Value = (2014).ToString() });
                allYears.Add(new ListItem { Text = (2015).ToString(), Value = (2015).ToString() });

                allYears.Add(new ListItem { Text = (2016).ToString(), Value = (2016).ToString() });
                allYears.Add(new ListItem { Text = (2017).ToString(), Value = (2017).ToString() });
                allYears.Add(new ListItem { Text = (2018).ToString(), Value = (2018).ToString() });
                allYears.Add(new ListItem { Text = (2019).ToString(), Value = (2019).ToString() });
                allYears.Add(new ListItem { Text = (2020).ToString(), Value = (2020).ToString() });
                allYears.Add(new ListItem { Text = (2021).ToString(), Value = (2021).ToString() });
                allYears.Add(new ListItem { Text = (2022).ToString(), Value = (2022).ToString() });




                ddlSelectYears.DataSource = allYears;
                ddlSelectYears.DataBind();
                ddlSelectYears.SelectedValue = year.ToString();

            



                if (1 <= month && month <= 3)
                {
                    ddlJidu.Text = "1";

                }
                else if (4 <= month && month <= 6)
                {
                    ddlJidu.Text = "2";
                }
                else if (7 <= month && month <= 9)
                {
                    ddlJidu.Text = "3";
                }
                else if (10 <= month && month <= 12)
                {
                    ddlJidu.Text = "4";
                }

                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User());

                ddlINSIDE.DataSource = user;
                ddlINSIDE.DataBind();
                ddlINSIDE.DataTextField = "LoginName";
                ddlINSIDE.DataValueField = "Id";


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
