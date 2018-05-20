﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFBaseBusList : BasePage
    {

        Base_BusInfoService busService = new Base_BusInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFBaseBus.aspx");
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
            if (txtBusNo.Text != "")
            {
                sql += string.Format("  BusNo like '%{0}%'", txtBusNo.Text.Trim());

            }

            List<Base_BusInfo> busList = this.busService.GetListArray(sql);
            AspNetPager1.RecordCount = busList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = busList;
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
            //判断项目有没有使用
            
            this.busService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();
          
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFBaseBus.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<Base_BusInfo> busList = new List<Base_BusInfo>();
                this.gvList.DataSource = busList;
                this.gvList.DataBind();
            }
        }
    }
}
