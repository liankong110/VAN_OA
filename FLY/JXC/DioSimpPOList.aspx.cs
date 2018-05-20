using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
 
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioSimpPOList :BasePage
    {
        private CG_POOrderService POSer = new CG_POOrderService();

        private bool isSell = false;
        private void Show()
        {
            string sql = ""; 
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            List<CG_POOrder> cars = this.POSer.GetSimpListArray(sql);
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
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
                if (isSell)
                     
                {
                    CG_POOrder model = e.Row.DataItem as CG_POOrder;
                    if (model.POStatue2 == CG_POOrder.ConPOStatue2)
                        e.Row.BackColor = System.Drawing.Color.Red;
                }
            }
        }

     

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["Comm_CGPONo"] = null;
                List<CG_POOrder> cars = new List<CG_POOrder>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();

                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可操作特殊客户'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='销售出库') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                {
                    ViewState["DoSpecGuest"] = true;
                }
                else
                {
                    ViewState["DoSpecGuest"] = false;
                }            
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            { 
                Session["Comm_CGPONo"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>"); 
            }
        }
    }
}
