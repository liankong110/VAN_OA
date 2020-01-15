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
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class GuestTrackList : BasePage
    {
        private TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();

        public string Query = "QueryGuestTrack";


        protected void btnCopy_Click(object sender, EventArgs e)
        {
          

            if (ddlYears.Text+ddlOrl.Text ==ddlNextYears.Text+ ddlBar.Text)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('原季度不能等于目标季度！');</script>");
                return;
            }
            List<GuestProBaseInfo> guestProBaseInfos = new GuestProBaseInfoService().GetListArray("GuestPro=1");
            string sql = string.Format(" 1=1 and QuartNo='{1}' and YearNo='{0}' ", ddlYears.Text, ddlOrl.Text);
            sql += string.Format(@" and (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))");
            List<TB_GuestTrack> GuestTracks = this.GuestTrackSer.GetListArray(sql);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                try
                {

                    objCommand.Parameters.Clear();
                    foreach (var model in GuestTracks)
                    {
                        //客户属性 每个季度需要复制到下一季度，但有一个是需要 特别关注的，就是每年的四季度 如果某个客户的客户属性是 自我开拓，
                        //来年的一季度，这个客户的客户属性 必须变成 原有资源！！！
                        //if (ddlOrl.Text == "4"&& ddlBar.Text=="1")
                        //{
                        //    if (model.MyGuestProString == "自我开拓")
                        //    {
                        //        model.MyGuestPro = 2;
                        //    }
                        //}
                        model.QuartNo = ddlBar.Text;
                        model.YearNo = ddlNextYears.Text;
                      
                        string update = GuestTrackSer.UpdateToString(model, model.GuestId, ddlNextYears.Text, ddlBar.Text);
                        if (model.MyGuestProString == "自我开拓")
                        {
                            //我们的系统在每个季度的最后一天的晚上12点会自动同步客户信息到下一个季度，我们定义
                            //．  如该客户目前的属性是自我开拓，该客户从登记之日起，加上自我开拓的有效期月数数字对应的日期，
                            //如小于（<） 新季度第一天，新季度的客户的属性变成 原有资源，否则 还是自我开拓
                            if (DateTime.Now.Month == 3 || DateTime.Now.Month == 6 || DateTime.Now.Month == 9 || DateTime.Now.Month == 12)
                            {
                                if (DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
                                    && model.Time.AddMonths(guestProBaseInfos[0].GuestMonth) < Convert.ToDateTime(ddlNextYears.Text + "-" + ddlBar.Text + "-01"))
                                {
                                    model.MyGuestPro = 2;
                                }
                            }
                        }
                        string add = GuestTrackSer.AddToString(model);

                        string updateSql =
                            string.Format(
                                "if not exists(select id from TB_GuestTrack where guestId='{0}' and yearNo='{1}' and QuartNo='{2}') begin {3} end else begin {4} end ",
                                model.GuestId, ddlNextYears.Text, ddlBar.Text, add,update);
                        objCommand.CommandText = updateSql;
                        objCommand.ExecuteNonQuery();
                        
                    }

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制失败！');</script>");
                }

            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制成功！');</script>");

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/GuestTrackList.aspx";
            base.Response.Redirect("~/ReportForms/GuestTrack.aspx?type=man");
        }


        private void Show()
        {

            
            string sql = " 1=1 ";

            QuerySession.QueryGuestTrack QGuestTrack = new VAN_OA.QuerySession.QueryGuestTrack();
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text)==false)
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
                sql += string.Format(" and QuartNo='{1}' and YearNo='{0}' ",ddlSelectYears.Text, ddlJidu.Text);
            }

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

            this.GuestTrackSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

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
                string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                    gvList.DataKeys[e.NewEditIndex].Value, DBHelp.ExeScalar(sql));
                DBHelp.ExeCommand(insertEform);
                efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表')", this.gvList.DataKeys[e.NewEditIndex].Value);
                eformIdObj = DBHelp.ExeScalar(efromId);
            }

            string url = "~/ReportForms/GuestTrack.aspx?ProId=" + proId + "&allE_id=" + this.gvList.DataKeys[e.NewEditIndex].Value + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
            Response.Redirect(url);

            //Session["POUrl"] = "~/ReportForms/GuestTrackList.aspx";
            //base.Response.Redirect("~/ReportForms/GuestTrackEdit.aspx?type=man&Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

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

                ddlYears.DataSource = allYears;
                ddlYears.DataBind();

                ddlNextYears.DataSource = allYears;
                ddlNextYears.DataBind();



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



                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {
                        gvList.Columns[1].Visible = false;
                    }
                }
                #endregion


                List<TB_GuestTrack> TB_GuestTracks1 = new List<TB_GuestTrack>();
                this.gvOld.DataSource = TB_GuestTracks1;
                this.gvOld.DataBind();

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




        //===
        private void ShowOld()
        {
            string sql = " 1=1 ";


            if (txtOldDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtOldDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time>='{0} 00:00:00'", txtOldDateFrom.Text);

            }

            if (txtOldTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtOldTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Time<='{0} 23:59:59'", txtOldTo.Text);

            }

            if (txtOldGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtOldGuestName.Text.Trim());

            }


            if (txtLoginName.Text != "")
            {
                sql += string.Format(" and tb_User.loginName like '%{0}%'", txtLoginName.Text);

            }



            List<TB_GuestTrack> GuestTracks = this.GuestTrackSer.GetListArrayOld(sql);


            this.gvOld.DataSource = GuestTracks;
            this.gvOld.DataBind();
        }
        protected void btnOldSelect_Click(object sender, EventArgs e)
        {
            ShowOld();
        }

        protected void gvOld_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOld.PageIndex = e.NewPageIndex;
            ShowOld();
        }

        protected void gvOld_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }


    }
}
