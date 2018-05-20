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
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using System.Collections.Generic;

namespace VAN_OA.BaseInfo
{
    public partial class WFInventoryList : BasePage
    {

        private Tb_InventoryService invSer = new Tb_InventoryService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFInventory.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string sql = " 1=1 ";
            if (txtName.Text != "")
            {
                sql += string.Format(" and InvName like '%{0}%'", txtName.Text);
            }
            if (ddlArea.Text != "")
            {
                sql += string.Format(" and GoodArea='{0}'", ddlArea.Text);
            }
            if (ddlNumber.Text != "")
            {
                sql += string.Format(" and GoodNumber='{0}'", ddlNumber.Text);
            }
            if (ddlRow.Text != "")
            {
                sql += string.Format(" and GoodRow='{0}'", ddlRow.Text);
            }
            if (ddlCol.Text != "")
            {
                sql += string.Format(" and GoodCol='{0}'", ddlCol.Text);
            }
            List<Tb_Inventory> pers = this.invSer.GetListArray(sql);
            this.gvList.DataSource = pers;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
 
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

            this.invSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            

            List<Tb_Inventory> pers = this.invSer.GetListArray("");
            this.gvList.DataSource = pers;
            this.gvList.DataBind();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFInventory.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //加载基本信息
                ddlNumber.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlRow.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlCol.Items.Add(new ListItem { Text = "全部", Value = "" });
                //货架号：1.全部  缺省 2….51 1,..50 
                for (int i = 1; i < 51; i++)
                {
                    ddlNumber.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    //层数：1.全部  缺省 2….21 1,2,3…20 
                    //部位：1.全部  缺省 2….21 1,2,3…20
                    if (i <= 21)
                    {
                        ddlRow.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                        ddlCol.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                }

                List<Tb_Inventory> pers = this.invSer.GetListArray("");
                this.gvList.DataSource = pers;
                this.gvList.DataBind();
            }
        }
    }
}
