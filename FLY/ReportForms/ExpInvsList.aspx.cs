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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class ExpInvsList : BasePage
    {


        private Tb_ExpInvsService proInvSer = new Tb_ExpInvsService();


        private void Show()
        {
            string sql = "  1=1 ";
            string sqlSum = " 1=1 ";
            //if (txtFrom.Text != "")
            //{
            //    sql += string.Format(" and CreateTime>='{0} 00:00:00'", txtFrom.Text);
            //}

            //if (txtTo.Text != "")
            //{
            //    sql += string.Format(" and CreateTime<='{0} 23:59:59'", txtTo.Text);
            //}

            if (ddlInvs.Text != "0")
            {
                sql += string.Format(" and Tb_Inventory.Id={0} ", ddlInvs.SelectedItem.Value);
                sqlSum += string.Format(" and Tb_Inventory.Id={0} ", ddlInvs.SelectedItem.Value);
            }


            if (txtInvNo.Text != "")
            {
                sql += string.Format(" and Tb_Inventory.InvNo like '%{0}%'", txtInvNo.Text);
                sqlSum += string.Format(" and Tb_Inventory.InvNo like '%{0}%'", txtInvNo.Text);
            }



            //if (txtProNo.Text != "")
            //{
            //    sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text);
            //}




            List<Tb_ExpInvsSumRep> pos = this.proInvSer.GetListArray_NoReurnInvs(sql);
            List<Tb_ExpInvs> SumPos = this.proInvSer.GroupByListArray(sqlSum);


            foreach (var model in pos)
            {
                if (model.InvName == "小计")
                {
                    var selectModel = SumPos.Find(p => p.InvId == model.InvId);
                    if (selectModel != null) model.InvName = selectModel.InvName;
                }
            }

            this.gvList.DataSource = pos;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
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

                if (e.Row.Cells[1].Text.Contains("小计"))
                {
                    
                    //e.Row.BackColor = System.Drawing.Color.FromName("#D7E8FF");
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected string GetDisPlay(object obj)
        {
            if (obj != null)
            {
                if (obj.ToString().Contains("小计"))
                {
                    return "none";
                }
                return "block";
            }
            return "none";
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //this.proInvSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


            //Session["POUrl"] = "~/ReportForms/GusetInfoList.aspx";
            //base.Response.Redirect("~/ReportForms/GusetInfo.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                VAN_OA.Dal.BaseInfo.Tb_InventoryService invSer = new VAN_OA.Dal.BaseInfo.Tb_InventoryService();
                List<VAN_OA.Model.BaseInfo.Tb_Inventory> InventoryList = invSer.GetListArrayToDdl("");
                InventoryList.Insert(0, new VAN_OA.Model.BaseInfo.Tb_Inventory());
                ddlInvs.DataSource = InventoryList;
                ddlInvs.DataBind();
                ddlInvs.DataTextField = "InvName";
                ddlInvs.DataValueField = "ID";

                List<Tb_ExpInvsSumRep> Gusets = new List<Tb_ExpInvsSumRep>();
                this.gvList.DataSource = Gusets;
                this.gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            base.Response.Redirect("~/ReportForms/ExpInvsGoodListHisMan.aspx?GoodId=" + e.CommandArgument);

        }
    }
}
