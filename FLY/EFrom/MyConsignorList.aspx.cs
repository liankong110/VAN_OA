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
using VAN_OA.Dal.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.EFrom;



namespace VAN_OA.EFrom
{
    public partial class MyConsignorList : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/EFrom/MyConsignor.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            show();
        }


        private void show()
        {
            if (this.ddlProType.SelectedItem.Text.Trim() != "")
            {

                tb_ConsignorService conSer = new tb_ConsignorService();
                List<tb_Consignor> allCons = conSer.GetListArray(string.Format(" appPer={0} and proId={1}", Session["currentUserId"].ToString(), ddlProType.SelectedItem.Value));

                this.gvList.DataSource = allCons;
                this.gvList.DataBind();


            }
            else
            {
                tb_ConsignorService conSer = new tb_ConsignorService();
                List<tb_Consignor> allCons = conSer.GetListArray(string.Format(" appPer={0}", Session["currentUserId"].ToString()));

                this.gvList.DataSource = allCons;
                this.gvList.DataBind();
            }

        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            show();
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
            tb_ConsignorService conSer = new tb_ConsignorService();
            conSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/EFrom/MyConsignor.aspx?ID=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                tb_ConsignorService conSer = new tb_ConsignorService();
                List<tb_Consignor> allCons = conSer.GetListArray(string.Format(" appPer={0}", Session["currentUserId"].ToString()));
              
                this.gvList.DataSource = allCons;
                this.gvList.DataBind();

                string sql = string.Format(" consignor={0} and conState='开启'", Session["currentUserId"].ToString());
                List<tb_Consignor> allCons_Bei = conSer.GetListArray(sql);

                this.GvBeiWei.DataSource = allCons_Bei;
                this.GvBeiWei.DataBind();

                A_ProInfoService proSer = new A_ProInfoService();
                List<A_ProInfo> pros = proSer.GetListArray("");
                pros.Insert(0, new A_ProInfo());
                ddlProType.DataSource = pros;
                ddlProType.DataBind();


                DropDownList1.DataSource = pros;
                DropDownList1.DataBind();
                ddlProType.DataTextField = "pro_Type";
                ddlProType.DataValueField = "pro_Id";

                DropDownList1.DataTextField = "pro_Type";
                DropDownList1.DataValueField = "pro_Id";
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "state")
            {
                object obj = DBHelp.ExeScalar("select conState from tb_Consignor where con_Id="+e.CommandArgument);
                if (obj != null)
                {
                    string state = "";
                    if (obj.ToString() == "开启")
                    {
                        state = "关闭";
                    }
                    else
                    {
                        state = "开启";
                    }
                    DBHelp.ExeCommand(string.Format("update  tb_Consignor set conState='{0}' where con_Id={1}",state,e.CommandArgument));
                    show();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            show2();
        }

        private void show2()
        {
            tb_ConsignorService conSer = new tb_ConsignorService();
            string sql = string.Format(" consignor={0} and conState='开启'", Session["currentUserId"].ToString());

            if (DropDownList1.SelectedItem.Text != "")
            {
                sql += " and proId=" + DropDownList1.SelectedItem.Value;
            }
            List<tb_Consignor> allCons_Bei = conSer.GetListArray(sql);

            this.GvBeiWei.DataSource = allCons_Bei;
            this.GvBeiWei.DataBind();
        }

        protected void GvBeiWei_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvBeiWei.PageIndex = e.NewPageIndex;
            show2();
        }

        protected void GvBeiWei_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }
    }
}
