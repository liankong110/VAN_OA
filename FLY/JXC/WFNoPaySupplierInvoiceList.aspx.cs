using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using System.Data;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;


namespace VAN_OA.JXC
{
    public partial class WFNoPaySupplierInvoiceList : BasePage
    {
        SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
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

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                if (QuanXian_ShowAll("供应商对账系统")==false)
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

                
                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {
                        gvMain.Columns[1].Visible = false;
                    }
                }
                #endregion

                if (Request["Ids"] != null)
                {
                    Show();
                }
                else
                {
                    if (Request["PayIds"] != null)
                    {
                        Show();
                    }
                    else
                    {
                        //主单
                        List<SupplierToInvoiceView> pOOrderList = new List<SupplierToInvoiceView>();
                        this.gvMain.DataSource = pOOrderList;
                        this.gvMain.DataBind();
                    }
                }
            }
        }
        #region 入库后付款
        private void Show()
        {
            string sql = "1=1 ";
            //sql += string.Format("  TB_SupplierInvoice.Status<>'不通过' ");
            if (ddlStatue.Text != "")
            {
                if (ddlStatue.Text == "执行中+通过")
                {
                    sql += string.Format(" and TB_SupplierInvoice.Status<>'不通过'");
                }
                else
                {
                    sql += string.Format(" and TB_SupplierInvoice.Status='{0}'", ddlStatue.Text);
                }
            }  
            if (ddlZhiFu.Text != "-1")
            {
                sql += string.Format(" and IsPayStatus=" + ddlZhiFu.Text);
            }
            if (ddlClear.Text != "-1")
            {
                sql += string.Format(" and RePayClear=" + ddlClear.Text);
            }
            if (txtActTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtActTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际支付金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and ActPay" + ddlActJS.Text + txtActTotal.Text);
            }

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text.Trim());
            }

            if (Request["Ids"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["Ids"]);
            }
            if (Request["PayIds"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["PayIds"]);
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                
                sql += string.Format(" and CreteTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
               
                sql += string.Format(" and CreteTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked==false)
                {
                    sql += string.Format(" and CAI_OrderInHouse.Supplier  like '%{0}%'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and CAI_OrderInHouse.Supplier ='{0}'", txtSupplier.Text.Trim());
                }
            }

            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }
           
            if (ddlUser.Text != "-1")//显示所有用户
            {

                sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AE='{0}'))",
                    ddlUser.SelectedItem.Text);
            }
            if (ddlCompany.Text != "-1")//显示所有用户
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and (DoPer IN(select loginName from tb_User where {0}) or exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=CAI_OrderInHouse.PONo and AE IN(select LOGINNAME from tb_User where {0})))",
                    where);
            }

            if (ddlIsHanShui.Text != "-1")
            {
                sql += string.Format(" and  IsHanShui={0} ", ddlIsHanShui.Text);
            }
            else
            {
                sql += " and  IsHanShui is not null ";               
            }
            List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetSupplierInvoiceListToNoPay(sql);
            lblActPayTotal.Text = pOOrderList.Sum(t => t.ActPay).ToString();
            lblPayTotal.Text = pOOrderList.Sum(t => t.SupplierInvoiceTotal).ToString();

            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

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
                SupplierToInvoiceView model = e.Row.DataItem as SupplierToInvoiceView;
                System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                if (lblIsHanShui != null)
                {
                    lblIsHanShui.Text = model.Good_IsHanShui == 1 ? "含税" : "不含税";
                }
                if (model.Good_IsHanShui == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string sql = "1=1 ";
            //sql += string.Format("  TB_SupplierInvoice.Status<>'不通过' and  RePayClear=2 and SupplierInvoiceTotal<0 ");
            //sql += string.Format("  TB_SupplierInvoice.Status<>'不通过' ");
            if (txtPONo.Text != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text);
            }

            if (Request["Ids"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["Ids"]);
            }
            if (Request["PayIds"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["PayIds"]);
            }
            
            
            if (ddlStatue.Text != "")
            {
                if (ddlStatue.Text == "执行中+通过")
                {
                    sql += string.Format(" and TB_SupplierInvoice.Status<>'不通过'");
                }
                else
                {
                    sql += string.Format(" and TB_SupplierInvoice.Status='{0}'", ddlStatue.Text);
                }
            }  

            if (ttxPOName.Text != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.POName like '%{0}%'", ttxPOName.Text);
            }

            if (txtFrom.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtFrom.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有问题！');</script>");
                    return;
                }
                sql += string.Format(" and CreteTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtTo.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有问题！');</script>");
                    return;
                }
                sql += string.Format(" and CreteTime<='{0} 23:59:59'", txtTo.Text);
            }





            if (txtSupplier.Text.Trim() != "")
            {
                sql += string.Format(" and Supplier  like '%{0}%'", txtSupplier.Text.Trim());
            }


            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlZhiFu.Text != "-1")
            {
                sql += string.Format(" and IsPayStatus=" + ddlZhiFu.Text);
            }
            if (ddlClear.Text != "-1")
            {
                sql += string.Format(" and RePayClear=" + ddlClear.Text);
            }
            if (txtActTotal.Text != "")
            {
                try
                {
                    Convert.ToDecimal(txtActTotal.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际支付金额格式有误！');</script>");
                    return;
                }
                sql += string.Format(" and ActPay" + ddlActJS.Text + txtActTotal.Text);
            }


            if (ddlUser.Text != "-1")//显示所有用户
            {

                sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AppName={1}))",
                    ddlUser.SelectedItem.Text, ddlUser.Text);
            }
            var strSql = new StringBuilder();
            strSql.Append(@"select TB_SupplierInvoice.ProNo as '支付单号',SupplierProNo as '支付流水号',
CAI_OrderInHouse.ProNo as '入库单号',RuTime as '入库时间',Supplier as '供应商',CAI_OrderInHouse.PONo as '项目编号',CAI_OrderInHouse.POName as '项目名称',CG_POOrder.GuestName as '客户',CG_POOrder.AE,
GoodNo as '编码',GoodName as '名称',GoodTypeSmName as '小类',GoodSpec as '规格',GoodUnit as '单位',GoodNum as '数量',supplierTuiGoodNum as '采退数'
,GoodPrice as '采购单价' ,SupplierFPNo as '发票号',SupplierInvoicePrice as '付款单价',SupplierInvoiceDate as '出款日期',SupplierInvoiceTotal as '付款金额',
ActPay as '实际支付',FuShuTotal as '负数合计',TB_SupplierInvoice.CreateName as '制单人',TB_SupplierInvoice.Status as '状态' from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0 ");
            if (sql.Trim() != "")
            {
                strSql.Append(" where " + sql);
            }
            strSql.Append(" order by TB_SupplierInvoices.Ids desc ");
            System.Data.DataTable dt = DBHelp.getDataTable(strSql.ToString());
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + "statement of account.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = dt;
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();


        }

       



    }
}
