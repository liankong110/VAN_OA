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
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;

namespace VAN_OA.JXC
{
    public partial class DioXiao_OrderOutHouseList : System.Web.UI.Page
    {
        Sell_OrderOutHouseService POSer = new Sell_OrderOutHouseService();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["Comm_CGPONo"] = null;
                //主单
                List<Sell_OrderOutHouse> pOOrderList = new List<Sell_OrderOutHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                if (Request["PoNo"] != null)
                {
                    txtPONo.Text = Request["PoNo"].ToString();
                }
                if (Request["Funds"] != null)
                {
                    btnOk.Visible = true;
                    gvMain.Columns[0].Visible = false;
                }
                else
                {
                    btnOk.Visible = false;
                    gvMain.Columns[1].Visible = false;
                }
            }
        }      


        private void Show()
        {
            string sql = " 1=1 ";

            if (txtPONo.Text != "")
            {
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text);
            }


            if (ttxPOName.Text != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text);
            }

            if (txtFrom.Text != "")
            {
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }


           
                sql += string.Format(" and Status='通过'");
            


            if (txtProNo.Text != "")
            {
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text);
            }


            if (Request["Funds"] != null)
            {
                sql += string.Format(" and ProNo not in (select lblNo from TB_CaiXiaoNo where Type='出库') ", txtProNo.Text);
            }


            List<Sell_OrderOutHouse> pOOrderList = this.POSer.GetListArray(sql);
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

             


           

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;

            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                Session["Comm_CGPONo"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>");                
            
            }
        }


 

        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            string AllProNo = "";
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("chkSelect")) as CheckBox;
                if (cb.Checked)
                {
                    Label ProNo = (gvMain.Rows[i].FindControl("ProNo")) as Label;
                    if (ProNo != null)
                    {
                        AllProNo+= ProNo.Text+"/";
                    }                    
                }
            }
            Session["Comm_CGPONo"] = AllProNo;
            Response.Write("<script>window.close();window.opener=null;</script>");   
        }
    }
}
