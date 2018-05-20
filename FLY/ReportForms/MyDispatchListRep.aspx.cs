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
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
namespace VAN_OA.ReportForms
{
    public partial class MyDispatchListRep : BasePage
    {
        private Tb_DispatchListService dispathSer = new Tb_DispatchListService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/BreakRulesCarList.aspx";
            //base.Response.Redirect("~/ReportForms/BreakRulesCar.aspx");
        }

        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发生时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and EvTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发生时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and EvTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtNo.Text.Trim() != "")
            {
                if (CheckProNo(txtNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CardNo like '%{0}%'", txtNo.Text.Trim());
            }



            sql += string.Format(" and (UserId={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=Tb_DispatchList.PONo and IFZhui=0  and AE='{1}'))", Session["currentUserId"], Session["LoginName"].ToString());



            sql += string.Format(@" and Tb_DispatchList.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type in ('预期报销单(油费报销)','预期报销单')) and state='通过')");



            //List<Tb_DispatchList> dispaths = this.dispathSer.GetListArray(sql);
            //decimal total = 0;
            //foreach (Tb_DispatchList model in dispaths)
            //{
            //    total += model.Total;
            //}

            string type = ddlType.Text;



            if (type == "")
            {
                gvList.Attributes.Add("Width", "300%");
                gvList.Width = Unit.Parse("300%");
                for (int i = 4; i <= 48; i++)
                {
                    gvList.Columns[i].Visible = true;
                }
            }
            else
            {

                gvList.Attributes.Add("Width", "100%");
                gvList.Width = Unit.Parse("100%");
                for (int i = 4; i <= 48; i++)
                {
                    gvList.Columns[i].Visible = false;
                }
                if (type == "公交费")
                {
                    for (int i = 4; i <= 11; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }

                    sql += string.Format(" and BusTotal is not null");

                }
                else if (type == "餐饮费")
                {
                    for (int i = 12; i <= 17; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and RepastTotal is not null");
                }
                else if (type == "住宿费")
                {
                    for (int i = 18; i <= 22; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and HotelTotal is not null");
                }
                else if (type == "汽油补贴")
                {
                    for (int i = 23; i <= 27; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and OilTotal is not null");
                }
                else if (type == "过路费")
                {
                    for (int i = 28; i <= 31; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and GuoTotal is not null");
                }
                else if (type == "邮寄费")
                {
                    for (int i = 32; i <= 41; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and PostTotal is not null");
                }
                else if (type == "小额采购")
                {
                    for (int i = 42; i <= 44; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and PoTotal is not null");
                }
                else if (type == "其它费用")
                {
                    for (int i = 45; i <= 48; i++)
                    {
                        gvList.Columns[i].Visible = true;
                    }
                    sql += string.Format(" and OtherTotal is not null");
                }
            }

            List<Tb_DispatchList> dispaths = this.dispathSer.GetListArray(sql);
            decimal total = 0;


            if (type == "")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.Total;
                }
            }
            else if (type == "公交费")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.BusTotal.Value;
                }

            }
            else if (type == "餐饮费")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.RepastTotal.Value;
                }
            }
            else if (type == "住宿费")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.HotelTotal.Value;
                }
            }
            else if (type == "汽油补贴")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.OilTotal.Value;
                }
            }
            else if (type == "过路费")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.GuoTotal.Value;
                }
            }
            else if (type == "邮寄费")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.PostTotal.Value;
                }
            }
            else if (type == "小额采购")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.PoTotal.Value;
                }
            }
            else if (type == "其它费用")
            {
                foreach (Tb_DispatchList model in dispaths)
                {
                    total += model.OtherTotal.Value;
                }

            }

            lblTotal.Text = total.ToString();
            AspNetPager1.RecordCount = dispaths.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = dispaths;
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

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //this.braRuleCarSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/BreakRulesCarList.aspx";
            //base.Response.Redirect("~/ReportForms/BreakRulesCar.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
               
                List<Tb_DispatchList> poseModels = new List<Tb_DispatchList>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();
            }
        }
    }
}
