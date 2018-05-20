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
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class CAI_OrderOutHouseList : BasePage
    {
        CAI_OrderOutHouseService POSer = new CAI_OrderOutHouseService();
        CAI_OrderOutHousesService ordersSer = new CAI_OrderOutHousesService();
        Dictionary<int, bool> CAIInfo_HanShui = new Dictionary<int, bool>();

        protected void ddlBusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBusType.Text != "0")
            {
                ddlIsSpecial.Text = "-1";
                ddlIsSpecial.Enabled = false;
            }
            else
            {
                ddlIsSpecial.Text = "0";
                ddlIsSpecial.Enabled = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                houseList.Insert(0, new VAN_OA.Model.BaseInfo.TB_HouseInfo());
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";

                //主单
                List<CAI_OrderOutHouse> pOOrderList = new List<CAI_OrderOutHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CAI_OrderOutHouses> orders = new List<CAI_OrderOutHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll(SysObj.CAI_OrderOutHouseList) == false)                 
                {
                    ViewState["showAll"] = false;
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
                else
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

            }
        }      


        private void Show()
        {
            string sql = " 1=1 ";


            if (txtFromDate.Text != "" || txtToDate.Text != "")
            {
                string where = "";
                if (txtFromDate.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtFromDate.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                        return;
                    }
                    where += string.Format(" and PODate>='{0} 00:00:00'", txtFromDate.Text);
                }
                if (txtToDate.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtToDate.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                        return;
                    }
                    where += string.Format(" and PODate<='{0} 23:59:59'", txtToDate.Text);
                }
                sql += string.Format(@" and (EXISTS(SELECT ID FROM CG_POOrder WHERE IFZhui=0 AND CG_POOrder.PONo=CAI_OrderOutHouse.PONo {0} ) 
OR EXISTS  (select id from CAI_POOrder where PONo like 'KC%' AND CAI_POOrder.PONo=CAI_OrderOutHouse.PONo {0} ))", where);
            } 

            if (ddlBusType.Text != "")
            {
                sql += string.Format(" and BusType='{0}'", ddlBusType.SelectedValue);
            }
            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format("and IsSpecial={0}", ddlIsSpecial.Text);
            }

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderOutHouse.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CAI_OrderOutHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('退货时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('退货时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and CAI_OrderOutHouse.Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and CAI_OrderOutHouse.Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderOutHouse.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }


            if (txtChcekProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtChcekProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and ChcekProNo like '%{0}%'", txtChcekProNo.Text.Trim());
            }

            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(" and CAI_OrderOutHouse.Supplier='{0}'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and CAI_OrderOutHouse.Supplier like '%{0}%'", txtSupplier.Text.Trim());
                }
            }
            if (ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseID={0}", ddlHouse.Text);
             }

          

            if (ddlUser.Text != "-1")
            {
                //sql += string.Format(" and (CAI_OrderOutHouse.CreateUserId={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderOutHouse.PONo and AppName={0}))", ddlUser.Text);
                sql += string.Format(" and CAI_POOrder.AE='{0}'",ddlUser.SelectedItem.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and (CAI_OrderOutHouse.CreateUserId IN(select id from tb_User where {0}) or exists(select id from CG_POOrder where IFZhui=0 and CG_POOrder.PONo=CAI_OrderOutHouse.PONo and AppName IN(select id from tb_User where {0})))", where);
            }
            if (ddlIsHanShui.Text != "-1")
            {
                sql += string.Format(" and exists(select 1 from Cai_Info left join CAI_OrderOutHouses on CAI_OrderOutHouses.OrderCheckIds=CaiInHouseIds where CAI_OrderOutHouses.Id=CAI_OrderOutHouse.Id and IsHanShui={0}) ", ddlIsHanShui.Text);
            }
            if (!string.IsNullOrEmpty(txtGoodNo.Text))
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }
            if (!string.IsNullOrEmpty(txtNameOrTypeOrSpec.Text))
            {
                sql += string.Format(" and (GoodSpec like '%{0}%' or GoodName like '%{0}%' or GoodTypeSmName like '%{0}%')", txtNameOrTypeOrSpec.Text);
            }
            decimal sumTotal=0;
            List<CAI_OrderOutHouse> pOOrderList = this.POSer.GetListArray(sql,out sumTotal);
            lblTotal.Text = string.Format("{0:n2}", sumTotal);
            string ids = "";
            for (int i = (AspNetPager1.CurrentPageIndex - 1) * 10; i < AspNetPager1.CurrentPageIndex * 10; i++)
            {
                if (i >= pOOrderList.Count)
                {
                    break;
                }
                ids += string.Format("{0},", pOOrderList[i].Id);
            }
            if (ids != "")
            {
                CAIInfo_HanShui = POSer.GetCAI_OrderOutHouse_HanShui(ids.Trim(','));
            }

            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<CAI_OrderOutHouses> orders = new List<CAI_OrderOutHouses>();
            gvList.DataSource = orders;
            gvList.DataBind();



           

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
                CAI_OrderOutHouse model = e.Row.DataItem as CAI_OrderOutHouse;
                if (CAIInfo_HanShui.ContainsKey(model.Id))
                {
                    bool hanShui = CAIInfo_HanShui[model.Id];
                    System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                    if (lblIsHanShui != null)
                    {
                        lblIsHanShui.Text = hanShui ? "含税" : "不含税";
                    }
                    if (hanShui==false)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                List<CAI_OrderOutHouses> orders = ordersSer.GetListArray(" CAI_OrderOutHouses.id=" + e.CommandArgument);
               
                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();


               
            
            }
        }



        CAI_OrderInHouses SumOrders = new CAI_OrderInHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderOutHouses model = e.Row.DataItem as CAI_OrderOutHouses;
                
                SumOrders.Total += model.Total;


            }
           


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                      
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }

        }

        private void setValue(Label control, string value)
        {
            control.Text = value;
        }
    }
}
