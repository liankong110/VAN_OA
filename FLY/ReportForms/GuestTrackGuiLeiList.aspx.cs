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
using System.Data.SqlClient;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using VAN_OA.Dal.BaseInfo;


namespace VAN_OA.ReportForms
{
    public partial class GuestTrackGuiLeiList : BasePage
    {
        private TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();

        public string Query = "QueryGuestTrack";



        List<VAN_OA.Model.BaseInfo.GuestTypeBaseInfo> GetListArray;
        private List<string> allGuestTypes = new List<string>();

        List<VAN_OA.Model.BaseInfo.GuestProBaseInfo> GetGuestProListArray;
        private List<string> allGuestPros = new List<string>();

        private void Show()
        {
            GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
            GetListArray = dal.GetListArray("");
            allGuestTypes = GetListArray.Select(t => t.GuestType).ToList();


            GuestProBaseInfoService guestProDal = new GuestProBaseInfoService();
            GetGuestProListArray = guestProDal.GetListArray("");
            allGuestPros = GetGuestProListArray.Select(t => t.GuestPro.ToString()).ToList();

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

            if (ddlINSIDE.Text != "" && ddlINSIDE.Text != "0")
            {
                sql += string.Format(" and INSIDE={0}", ddlINSIDE.SelectedItem.Value);
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
            if (ddlJidu.Text != "")
            {
                sql += string.Format(" and QuartNo='{1}' and YearNo='{0}' ", ddlSelectYears.Text, ddlJidu.Text);
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial ={0}", ddlSpecial.Text);
            }
            if (ddlGuestTypeList.Text != "-1")
            {
                sql += string.Format(" and MyGuestType ='{0}'", ddlGuestTypeList.Text);
            }
            if (ddlGuestProList.Text != "-1")
            {
                sql += string.Format(" and MyGuestPro like '%{0}%'", ddlGuestProList.Text);
            }
            if (txtPhone.Text != "")
            {
                sql += string.Format(" and Phone like '%{0}%'", txtPhone.Text);
            }
            if (txtLikeMan.Text != "")
            {
                sql += string.Format(" and LikeMan like '%{0}%'", txtLikeMan.Text);
            }
            sql += string.Format(@" and (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))");

            List<TB_GuestTrack> GuestTracks = this.GuestTrackSer.GetListArrayGuilei(sql, dllAddGuest.Text, ddlDiffMyGuestType.Text, dllDiffMyGuestPro.Text,
                ddlSelectYears.Text, ddlJidu.Text);
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

                DropDownList drp = (DropDownList)e.Row.FindControl("dllMyGuestType");
                drp.DataSource = GetListArray;
                drp.DataBind();
                drp.DataTextField = "GuestType";
                drp.DataValueField = "GuestType";
                //  选中 DropDownList
                try
                {
                    var hidTxt = ((HiddenField)e.Row.FindControl("hidtxt")).Value;
                    drp.SelectedIndex = allGuestTypes.IndexOf(hidTxt);
                }
                catch (Exception)
                { }


                DropDownList guestProDrp = (DropDownList)e.Row.FindControl("dllMyGuestPro");
                guestProDrp.DataSource = GetGuestProListArray;
                guestProDrp.DataBind();
                guestProDrp.DataTextField = "GuestProString";
                guestProDrp.DataValueField = "GuestPro";
                //  选中 DropDownList
                try
                {
                    var hidMyGuestProTxt = ((HiddenField)e.Row.FindControl("hidMyGuestProTxt")).Value;
                    guestProDrp.SelectedIndex = allGuestPros.IndexOf(hidMyGuestProTxt);

                }
                catch (Exception)
                { }

            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            this.GuestTrackSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


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

                ddlAE.DataSource = user;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";

                ddlINSIDE.DataSource = user;
                ddlINSIDE.DataBind();
                ddlINSIDE.DataTextField = "LoginName";
                ddlINSIDE.DataValueField = "Id";


                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                ddlGuestTypeList.DataSource = dal.GetListArray("");
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";
                ddlGuestTypeList.Items.Insert(0, new ListItem { Text = "全部", Value = "-1" });

                GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();

                ddlGuestProList.DataSource = guestProBaseInfodal.GetListArray("");
                ddlGuestProList.DataBind();
                ddlGuestProList.DataTextField = "GuestProString";
                ddlGuestProList.DataValueField = "GuestPro";
                ddlGuestProList.Items.Insert(0, new ListItem { Text = "全部", Value = "-1" });

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

                    //if (QGuestTrack.Apper != "")
                    //{
                    //     txtLoginName.Text = QGuestTrack.Apper;
                    //}





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

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            string where = " ID  in (";
            string expWhere = " ID  in (";
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsSpecial")) as CheckBox;
                if (cb.Checked)
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    where += "'" + lblIds.Text + "',";
                }
                else
                {
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    expWhere += "'" + lblIds.Text + "',";
                }
            }

            if (where != " ID  in (")
            {
                where = where.Substring(0, where.Length - 1) + ")";
                var sql = "update TB_GuestTrack set IsSpecial=1 where " + where;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }

            if (expWhere != " ID  in (")
            {
                expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                var sql = "update TB_GuestTrack set IsSpecial=0 where " + expWhere;
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
        }

        protected void btnGuestType_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {

                conn.Open();
                SqlCommand objCommand = conn.CreateCommand();
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {

                    DropDownList drp = ((DropDownList)gvList.Rows[i].FindControl("dllMyGuestType"));
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    objCommand.CommandText = string.Format("update TB_GuestTrack set MyGuestType='{0}' where id={1}",
                           drp.Text, lblIds.Text);
                    objCommand.ExecuteNonQuery();

                }
                conn.Close();
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }

        protected void btnCopyGuestType_Click(object sender, EventArgs e)
        {
            if (DBHelp.ExeCommand("update [tempGuestType] set guestType=MyGuestType,GuestXiShu=payXiShu"))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
            }
        }

        protected void btnSaveGuestPro_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {

                conn.Open();
                SqlCommand objCommand = conn.CreateCommand();
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {

                    DropDownList drp = ((DropDownList)gvList.Rows[i].FindControl("dllMyGuestPro"));
                    Label lblIds = (gvList.Rows[i].FindControl("myId")) as Label;
                    objCommand.CommandText = string.Format("update TB_GuestTrack set MyGuestPro={0} where id={1}",
                           drp.Text, lblIds.Text);
                    objCommand.ExecuteNonQuery();

                }
                conn.Close();
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }

        protected void btnCopyGuestPro_Click(object sender, EventArgs e)
        {
            if (DBHelp.ExeCommand("update [tempGuestPro] set GuestPro=oldGuestPro,JiLiXiShu=oldJiLiXiShu;"))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
            }
        }





    }
}
