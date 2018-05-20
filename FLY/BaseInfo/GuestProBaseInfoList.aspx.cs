using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class GuestProBaseInfoList : BasePage
    {
        public string GetInfo(object obj)
        {
            if (obj.ToString() == "1")
                return "可变";

            return "不可变";
        }

        public static  string GetGestProInfo(object obj)
        {
            if (obj.ToString() == "1")
            {
                return "自我开拓";
            }
            else if (obj.ToString() == "0")
            {
                return "公司资源";
            }
            else if (obj.ToString() == "2")
            {
                return "原有资源";
            }
            
            return "";
        }

        public static string GetGestProInfo_1(object obj)
        {
            if (obj.ToString() == "1")
            {
                return "自我";
            }
            else if (obj.ToString() == "0")
            {
                return "公司";
            }
            else if (obj.ToString() == "2")
            {
                return "原有";
            }

            return "";
        }

        public static int GetGestProInfo_2(object obj)
        {
            if (obj.ToString() == "自我")
            {
                return 1;
            }
            else if (obj.ToString() == "公司")
            {
                return 0;
            }
            else if (obj.ToString() == "原有")
            {
                return 2;
            }

            return 3;
        }

        GuestProBaseInfoService guestProBaseInfoService = new GuestProBaseInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFGuestProBaseInfo.aspx");
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

        private void Show()
        {

            string sql = "";
            

            List<GuestProBaseInfo> gooQGooddList = this.guestProBaseInfoService.GetListArray(sql);
            AspNetPager1.RecordCount = gooQGooddList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
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

            this.guestProBaseInfoService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFGuestProBaseInfo.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<GuestProBaseInfo> goodList = new List<GuestProBaseInfo>();
                this.gvList.DataSource = goodList;
                this.gvList.DataBind();
            }
        }
    }
}
