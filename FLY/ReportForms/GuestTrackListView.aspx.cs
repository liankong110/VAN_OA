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
    public partial class GuestTrackListView : BasePage
    {
        private TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();

        public string Query = "QueryGuestTrack";

       

        private void Show()
        {
            string sql = "";

            QuerySession.QueryGuestTrack QGuestTrack = new VAN_OA.QuerySession.QueryGuestTrack();
            

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
                QGuestTrack.GuestName = txtGuestName.Text.Trim();
            }

//            sql += string.Format(@" and (TB_GuestTrack.id in (select allE_id from tb_EForm where proId =17 and state='通过') 
//or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId =17))");

            PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;


            List<TB_GuestTrack> GuestTracks = this.GuestTrackSer.GetListArrayToPage(sql, page).OrderByDescending(t=>t.Time).ToList();
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

       

      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
               


               

                

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

                    
                    if (QGuestTrack.GuestName!= "")
                    {                       
                         txtGuestName.Text=QGuestTrack.GuestName ;
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
