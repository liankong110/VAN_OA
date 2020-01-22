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
    public partial class DioCommPOList :BasePage
    {
        private CG_POOrderService POSer = new CG_POOrderService();

        private bool isSell = false;
        private void Show()
        {
            string sql = " 1=1 and IFZhui=0 and Status='通过' ";

            

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.POName like '%{0}%'", ttxPOName.Text.Trim());
            }


            //if (Request["Type"] != null)
            //{
            //    sql += string.Format(" and PONo not in (select PoNo from TB_ToInvoice where State<>'不通过' )");
            //}

            if (Request["CG_Order"] != null)
            {
                var strSql = new System.Text.StringBuilder();
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                if (1 <= month && month <= 3)
                {
                    strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
                }
                else if (4 <= month && month <= 6)
                {
                    strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
                }
                else if (7 <= month && month <= 9)
                {
                    strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
                }
                else if (10 <= month && month <= 12)
                {
                    strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
                }
                //sql += string.Format(" and (AppName={0}  or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 {1} and CG_POOrder.GuestNo=TB_GuestTrack.GuestId) )", Session["currentUserId"], strSql);
                sql += string.Format(" and (AppName={0}", Session["currentUserId"]);
                #region 特殊，AE 用户判断
                if ((bool)ViewState["DoSpecGuest"])
                {
                    sql += string.Format("  or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 {0} and CG_POOrder.GuestNo=TB_GuestTrack.GuestId) ",
                     strSql);
                }
                sql += ")";
                
                #endregion
            }

            if (Request["AE"] != null)
            {
                sql += string.Format(" and (AE='{0}' OR AppName={1})  ", Session["LoginName"], Session["currentUserId"]);
            }

            if (Request["sell"] != null)
            {
                var strSql = new System.Text.StringBuilder();
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                if (1 <= month && month <= 3)
                {
                    strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
                }
                else if (4 <= month && month <= 6)
                {
                    strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
                }
                else if (7 <= month && month <= 9)
                {
                    strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
                }
                else if (10 <= month && month <= 12)
                {
                    strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
                }

                #region 特殊，AE 用户判断
                sql += string.Format(" and (AppName={0}", Session["currentUserId"]);
                if ((bool)ViewState["DoSpecGuest"])
                {
                    sql += string.Format("  or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 {0} and CG_POOrder.GuestNo=TB_GuestTrack.GuestId) ",
                     strSql.ToString());
                }
                sql += ")";
                #endregion

                //sql += string.Format(" and (AppName={0} or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 {1} and CG_POOrder.GuestNo=TB_GuestTrack.GuestId) )", Session["currentUserId"],strSql);
                isSell = true;
            }
            //    List<CG_POOrder> cars = this.POSer.GetSellGoodsPoInfo(sql, Convert.ToInt32(Session["currentUserId"]));
            //    this.gvList.DataSource = cars;
            //    this.gvList.DataBind();

            //}
            //else
            //{
                List<CG_POOrder> cars = this.POSer.GetListArray(sql);
                AspNetPager1.RecordCount = cars.Count;
                this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
            //}


          
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
