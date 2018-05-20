using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;
using System.Data.Common;

namespace VAN_OA.JXC
{
    public partial class DioSellOutOrderToFPList : BasePage
    {
        private CG_POOrdersService POSer = new CG_POOrdersService();
        private System.Data.DataTable myDataTable = new System.Data.DataTable();

        private void Show()
        {
            string sql = " 1=1 ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and SellFP_Out_View.PONO='{0}'", txtPONo.Text.Trim());            
            }
            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderOutHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            sql += string.Format(" and Sell_OrderOutHouse.CreateUserId={0} ", Session["currentUserId"]);
            List<CG_POOrdersFP> cars = this.POSer.GetListArrayToFps_Out(sql);

            var cgIds="";
            for (int i=gvList.PageIndex*10;i< ((gvList.PageIndex+1)*10);i++)
            {
                if (i < cars.Count)
                {
                    cgIds += cars[i].Ids.ToString() + ",";
                }
            }
            if (cgIds.Length > 0)
            {
                cgIds = cgIds.Substring(0,cgIds.Length-1);
            }
            //Label1.Text = "";
            if (cgIds.Length > 0)
            {
                myDataTable = DBHelp.getDataTable(string.Format("select SellOutOrderId from Sell_OrderFPs where SellOutOrderId in  ({0}) ", cgIds));

            }
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
            this.gvList.DataBind();
            
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
            ViewState["ids"] = null;
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


                CG_POOrdersFP model = e.Row.DataItem as CG_POOrdersFP;
                if (model != null)
                {

                    for (int i = 0; i < myDataTable.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(myDataTable.Rows[i][0]) == model.Ids)
                        {
                            //Label1.Text += model.Ids.ToString();
                            e.Row.BackColor = System.Drawing.Color.Gold;
                            break;
                        }
                    }
                    if (ViewState["ids"] != null && ViewState["ids"].ToString().Contains(","+model.Ids+","))
                    {                   
                        CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                        cb.Checked = true;
                    }

                }
                       
                
            }
        }

     

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["Sell_OrderOutHousesViewSession"] = null;
                List<CG_POOrdersFP> cars = new List<CG_POOrdersFP>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
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
                Session["OrderInHouseSession"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>"); 
            }
        }

        private void GetSelectedData()
        {
            if (ViewState["ids"] == null)
            {
                ViewState["ids"] = ",";
            }
            string where = ViewState["ids"].ToString();
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("chkSelect")) as CheckBox;
                Label lblIds = (gvList.Rows[i].FindControl("Ids")) as Label;
                if (cb.Checked)
                {
                  
                    if (!where.Contains("," + lblIds.Text + ","))
                        where += lblIds.Text + ",";
                }
                else
                {
                    if (where.Contains("," + lblIds.Text + ","))
                        where =where.Replace( ","+lblIds.Text + ",",",");
                }
            }

            ViewState["ids"] = where;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetSelectedData();
            if (ViewState["ids"] != null)
            {
                string where = " ids  in (" + ViewState["ids"].ToString().Substring(1);
                //for (int i = 0; i < this.gvList.Rows.Count; i++)
                //{
                //    CheckBox cb = (gvList.Rows[i].FindControl("chkSelect")) as CheckBox;
                //    if (cb.Checked)
                //    {
                //        Label lblIds = (gvList.Rows[i].FindControl("Ids")) as Label;
                //        where += lblIds.Text + ",";
                //    }
                //}

                if (where != " ids  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";

                    var allList = this.POSer.GetListArrayToFps_Out(where);
                    System.Collections.Hashtable hs = new System.Collections.Hashtable();
                    foreach (var model in allList)
                    { 
                        if(!hs.Contains(model.PONo))
                        {
                            hs.Add(model.PONo,null);
                            if (hs.Keys.Count > 1)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('选择的信息只能为同一个项目，请查看选择的信息');</script>"));
                                return;
                            }
                        }
                    }
                    Session["Sell_OrderOutHousesViewSession"] = allList;
                    Response.Write("<script>window.close();window.opener=null;</script>");
                    return;
                }
            }
            Response.Write("<script>window.close();window.opener=null;</script>");

        }

        protected void gvList_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GetSelectedData();
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }
    }
}
