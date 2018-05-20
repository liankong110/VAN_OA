using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;

namespace VAN_OA.JXC
{
    public partial class WFSupplierInvoice : BasePage
    {
        //实采总额=金额一列的总和
        decimal TotalZhi = 0;
        //剩余可支付实采金额=剩余一列的总和
        decimal ResultTotal = 0;
        decimal ShengYuZhiJia = 0;

        decimal YuTotal = 0;
        decimal Yu_ResultTotal = 0;
        decimal Yu_ShengYuZhiJia = 0;
        SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                houseList.Insert(0, new VAN_OA.Model.BaseInfo.TB_HouseInfo());
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";

                //主单
                List<SupplierToInvoiceView> pOOrderList = new List<SupplierToInvoiceView>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();


                this.gvAdvancePayment.DataSource = pOOrderList;
                this.gvAdvancePayment.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                ddlAdvUserList.DataSource = user;
                ddlAdvUserList.DataBind();
                ddlAdvUserList.DataTextField = "LoginName";
                ddlAdvUserList.DataValueField = "Id";
                
                if (Request["error"] != null)
                {
                    txtSupplier.Text = Request["error"];
                   // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败,供应商['" + Request["error"] + "']有在执行中的支付单，请排队等候');</script>");
                }
            }
        }
        #region 入库后付款
        private void Show()
        {
            //if(txtSupplier.Text=="")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择供应商');</script>");
            //    return ;
            //}
            string sql = " 1=1 ";

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

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }

            string poDateSql = "";
            if (txtPoDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                poDateSql += string.Format(" and PODate>='{0} 00:00:00'", txtPoDateFrom.Text);
            }

            if (txtPoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                poDateSql += string.Format(" and PODate<='{0} 23:59:59'", txtPoDateTo.Text);
            }

            if (!string.IsNullOrEmpty(poDateSql))
            {
                sql += string.Format(@" and (EXISTS (select ID from CG_POOrder where CG_POOrder.PONO=CAI_OrderInHouse.PONO and IFZhui=0 
{0}) or EXISTS (select ID from CAI_POOrder where CAI_POOrder.PONO = CAI_OrderInHouse.PONO and BusType = 1 {0}))", poDateSql);
            }

            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and CAI_OrderInHouse.Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderInHouse.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }


            if (txtChcekProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtChcekProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ChcekProNo like '%{0}%'", txtChcekProNo.Text.Trim());
            }

            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked == false)
                {
                    sql += string.Format(" and Supplier  like '%{0}%'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and Supplier ='{0}'", txtSupplier.Text.Trim());
                }

            }

           
            if (ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseID={0}", ddlHouse.Text);
            }

            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }
            if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
            }
            if (txtCaiNum.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCaiNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库数 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and GoodNum{0} {1}", ddlCaiNum.Text, txtCaiNum.Text.Trim());
            }
            if (txtCaiPrice.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCaiPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购单价 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and GoodPrice{0} {1}", ddlCaiPrice.Text, txtCaiPrice.Text.Trim());
            }
            if (ddlTemp.Text != "-1")
            {
                sql += " and IsTemp="+ddlTemp.Text;
            }

            if (ddlUser.Text != "-1") 
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where CG_POOrder.PONO=CAI_OrderInHouse.PONO and AE='{0}')",ddlUser.SelectedItem.Text);                 
            }


            List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetListArray_New(sql);

            //过滤库存，库存的信息不要显示出来
            pOOrderList = pOOrderList.FindAll(t => t.GuestName!="库存");

            if (cbGuolv.Checked)
            {
                pOOrderList = pOOrderList.FindAll(t=>t.IsShow);

                //pOOrderList = pOOrderList.FindAll(t => t.LastTotal != t.ResultTotal);
            }
            AspNetPager1.RecordCount = pOOrderList.Count;

            var temp = pOOrderList.Sum(t => t.ResultTotal);

            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            lblTotalZhi.Text = TotalZhi.ToString();
            lblShengYuTotal.Text = ResultTotal.ToString();
            lblShengyuzhijiaTotal.Text = ShengYuZhiJia.ToString();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSupplier.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择供应商');</script>");
                return;
            }
            txtSupplier.Text = txtSupplier.Text.Trim();
            string where = "",notwhere="";
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbTemp")) as CheckBox;
                Label lblIds = (gvMain.Rows[i].FindControl("Ids")) as Label;
                if (cb.Checked)
                {                   
                    where += lblIds.Text + ",";
                }
                else
                {
                    notwhere += lblIds.Text + ",";
                }
            }
            if (where != "" || notwhere!="")
            {
                if (where != "")
                {
                    where = where.Substring(0, where.Length - 1);
                    if (Convert.ToInt32(DBHelp.ExeCommand(string.Format(" update CAI_OrderInHouses set IsTemp=1 where Ids IN (" + where + ")"))) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    }                     
                }
                if (notwhere != "")
                {
                    notwhere = notwhere.Substring(0, notwhere.Length - 1);
                    if (Convert.ToInt32(DBHelp.ExeCommand(string.Format(" update CAI_OrderInHouses set IsTemp=0 where Ids IN (" + notwhere + ")"))) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    }  
                }                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");                  
                 
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择数据！');</script>");
            }
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
            ViewState["ids"] = null;
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            GetSelectedData();
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetSelectedData();
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
                if (model != null)
                {
                    TotalZhi += model.LastTotal;
                    ResultTotal += model.ResultTotal;
                    ShengYuZhiJia += model.ShengYuZhiJia;
                     //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额<=0
                    //var result = model.GoodNum * model.GoodPrice - model.SupplierInvoiceTotal;
                    if (model.ResultTotal <= 0)
                    {                        
                        CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                        cb.Visible = false;                        
                    }
                    else
                    {
                        if (ViewState["ids"] != null && ViewState["ids"].ToString().Contains("," + model.Ids + ","))
                        {
                            CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                            cb.Checked = true;
                        }
                    }

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtSupplier.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择供应商');</script>");
                return;
            }
            txtSupplier.Text = txtSupplier.Text.Trim();
            //判断改供应商是否有在支付中的单子
            if (TB_SupplierInvoiceService.checkSupplierDoing(txtSupplier.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请排队等候');</script>");
                return ;
            }
            if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(txtSupplier.Text, 1))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');</script>");

                return;
            }
            GetSelectedData();
            if (ViewState["ids"] != null)
            {
                string where = ViewState["ids"].ToString().Substring(1);
                if (where != "")
                {
                    where = where.Substring(0, where.Length - 1);
                    Session["backurl"] = "/JXC/WFSupplierInvoice.aspx";
                    Session["SupplierInvoiceIds"] = where;
                    base.Response.Redirect("~/JXC/WFSupplierInvoiceVerify.aspx?ProId=31&ids=" );
                }
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择数据！');</script>");
            }
        }

        private void GetSelectedData()
        {
            if (ViewState["ids"] == null)
            {
                ViewState["ids"] = ",";
            }
            string where = ViewState["ids"].ToString();
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("chkSelect")) as CheckBox;
                Label lblIds = (gvMain.Rows[i].FindControl("Ids")) as Label;
                if (cb.Visible)
                {
                    if (cb.Checked)
                    {

                        if (!where.Contains("," + lblIds.Text + ","))
                            where += lblIds.Text + ",";
                    }
                    else
                    {
                        if (where.Contains("," + lblIds.Text + ","))
                            where = where.Replace("," + lblIds.Text + ",", ",");
                    }
                }
            }

            ViewState["ids"] = where;
        }
        #endregion

        #region 预付款
        private void AdvancePaymentShow()
        {
            string sql = "  CAI_POOrder.Status='通过' ";


            if (txtAdvancePaymentPoNo.Text.Trim() != "")
            {
                if (CheckPoNO(txtAdvancePaymentPoNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtAdvancePaymentPoNo.Text.Trim());
            }

            if (txtAdvancePaymentPoName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", txtAdvancePaymentPoName.Text.Trim());
            }
            if (txtAdvancePaymentProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtAdvancePaymentProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_POOrder.ProNo like '%{0}%'", txtAdvancePaymentProNo.Text.Trim());
            }
            if (txtAdvancePaymentSupplierName.Text.Trim() != "")
            {
                if (cbAdvancePiPei.Checked == false)
                {
                    sql += string.Format(" and lastSupplier  like '%{0}%'", txtAdvancePaymentSupplierName.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and lastSupplier ='{0}'", txtAdvancePaymentSupplierName.Text.Trim());
                }
            }

            if (cbNoKuCun.Checked)
            {
                sql += string.Format(" and lastSupplier<>'库存'");
            }
            if (txtAdvancePaymentGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtAdvancePaymentGoodNo.Text);
            }
            if (txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "")
            {
                if (txtNameOrTypeOrSpec1.Text != "" && txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec1.Text, txtNameOrTypeOrSpecTwo1.Text);
                }
                else if (txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec1.Text;
                    if (txtNameOrTypeOrSpecTwo1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo1.Text;

                    sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
            }
            if (txtCaiNum1.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCaiNum1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购数 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Num{0} {1}", ddlCaiNum1.Text, txtCaiNum1.Text.Trim());
            }
            if (txtCaiPrice1.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCaiPrice1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购单价 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and lastPrice{0} {1}", ddlCaiPrice1.Text, txtCaiPrice1.Text.Trim());
            }
            if (ddlAdvanceTemp.Text != "-1")
            {
                sql += " and IsCAITemp=" + ddlAdvanceTemp.Text;
            }

            if (ddlAdvUserList.Text != "-1")
            {
                sql += string.Format(" and CAI_POOrder.AE='{0}'", ddlAdvUserList.SelectedItem.Text);
            }

            string poDateSql = "";
            if (txtYuPoDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtYuPoDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                poDateSql += string.Format(" and PODate>='{0} 00:00:00'", txtYuPoDateFrom.Text);
            }

            if (txtYuPoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtYuPoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                poDateSql += string.Format(" and PODate<='{0} 23:59:59'", txtYuPoDateTo.Text);
            }

            if (!string.IsNullOrEmpty(poDateSql))
            {
                sql += string.Format(@" and (EXISTS (select ID from CG_POOrder where CG_POOrder.PONO=CAI_POOrder.PONO and IFZhui=0 {0}) or (1=1 {0}))", poDateSql);
            }

            //去除不必要的
            //model.IfRuKu || (model.GoodNum * model.GoodPrice - model.SupplierInvoiceTotal) <= 0

            List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetSupplierAdvancePaymentList(sql);

            if (cbAdvanceGuolv.Checked)
            {
                pOOrderList = pOOrderList.FindAll(t => t.IsShow);
            }
            AspNetPager2.RecordCount = pOOrderList.Count;
            this.gvAdvancePayment.PageIndex = AspNetPager2.CurrentPageIndex - 1;
            this.gvAdvancePayment.DataSource = pOOrderList;
            this.gvAdvancePayment.DataBind();
            lblYuTotal.Text = YuTotal.ToString();
            lblYu_ShengYuTotal.Text = Yu_ResultTotal.ToString();
            lblYu_ShengyuzhijiaTotal.Text = Yu_ShengYuZhiJia.ToString();
        }

        protected void btnSelectAdvancePayment_Click(object sender, EventArgs e)
        {
            AspNetPager2.CurrentPageIndex = 1;
            AdvancePaymentShow();
            ViewState["idsAdvancePayment"] = null;
        }
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        {
            GetSelectedDataAdvancePayment();          
            AdvancePaymentShow();
        }

        protected void gvAdvancePayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetSelectedDataAdvancePayment();
            this.gvAdvancePayment.PageIndex = e.NewPageIndex;
            AdvancePaymentShow();
        }

        protected void gvAdvancePayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                SupplierToInvoiceView model = e.Row.DataItem as SupplierToInvoiceView;
                if (model != null)
                {
                    YuTotal += model.LastTotal;
                    Yu_ResultTotal += model.ResultTotal;
                    Yu_ShengYuZhiJia += model.ShengYuZhiJia;

                    //（采购数量）*采购单价-之前所有支付单金额<=0                 
                    if (model.IfRuKu || (model.LastTotal - model.SupplierInvoiceTotal) <= 0)
                    {
                        CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                        cb.Visible = false;
                    }
                    else
                    {
                        if (ViewState["idsAdvancePayment"] != null && ViewState["idsAdvancePayment"].ToString().Contains("," + model.Ids + ","))
                        {
                            CheckBox cb = (e.Row.FindControl("chkSelect")) as CheckBox;
                            cb.Checked = true;
                        }
                    }

                }
            }
        }

        protected void Btn_Save_AdvancePayment_Click(object sender, EventArgs e)
        {            
            string where = "", notwhere = "";
            for (int i = 0; i < this.gvAdvancePayment.Rows.Count; i++)
            {
                CheckBox cb = (gvAdvancePayment.Rows[i].FindControl("cbTemp")) as CheckBox;
                Label lblIds = (gvAdvancePayment.Rows[i].FindControl("Ids")) as Label;
                if (cb.Checked)
                {
                    where += lblIds.Text + ",";
                }
                else
                {
                    notwhere += lblIds.Text + ",";
                }
            }
            if (where != "" || notwhere != "")
            {
                if (where != "")
                {
                    where = where.Substring(0, where.Length - 1);
                    if (Convert.ToInt32(DBHelp.ExeCommand(string.Format(" update CAI_POCai set IsCAITemp=1 where Ids IN (" + where + ")"))) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    }
                }
                if (notwhere != "")
                {
                    notwhere = notwhere.Substring(0, notwhere.Length - 1);
                    if (Convert.ToInt32(DBHelp.ExeCommand(string.Format(" update CAI_POCai set IsCAITemp=0 where Ids IN (" + notwhere + ")"))) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    }
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择数据！');</script>");
            }           
        }

        protected void Button1AdvancePayment_Click(object sender, EventArgs e)
        {
            GetSelectedDataAdvancePayment();
            if (ViewState["idsAdvancePayment"] != null)
            {
                string where = ViewState["idsAdvancePayment"].ToString().Substring(1);
                if (where != "")
                {
                    where = where.Substring(0, where.Length - 1);
                    Session["backurl"] = "/JXC/WFSupplierInvoice.aspx";
                    base.Response.Redirect("~/JXC/WFSupplierAdvancePaymentVerify.aspx?ProId=32&ids=" + where);
                }
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择数据！');</script>");
            }
        }

        private void GetSelectedDataAdvancePayment()
        {
            
            if (ViewState["idsAdvancePayment"] == null)
            {
                ViewState["idsAdvancePayment"] = ",";
            }
            string where = ViewState["idsAdvancePayment"].ToString();
            for (int i = 0; i < this.gvAdvancePayment.Rows.Count; i++)
            {
                CheckBox cb = (gvAdvancePayment.Rows[i].FindControl("chkSelect")) as CheckBox;
                Label lblIds = (gvAdvancePayment.Rows[i].FindControl("Ids")) as Label;
                if (cb.Visible)
                {
                    if (cb.Checked)
                    {

                        if (!where.Contains("," + lblIds.Text + ","))
                            where += lblIds.Text + ",";
                    }
                    else
                    {
                        if (where.Contains("," + lblIds.Text + ","))
                            where = where.Replace("," + lblIds.Text + ",", ",");
                    }
                }
            }

            ViewState["idsAdvancePayment"] = where;
        }
        #endregion


        protected void cbTempAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbTemp")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("chkSelect")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }

        protected void cbAll_AdvancePayment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvAdvancePayment.Rows.Count; i++)
            {
                CheckBox cb = (gvAdvancePayment.Rows[i].FindControl("chkSelect")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }

        protected void cbTempAll_AdvancePayment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvAdvancePayment.Rows.Count; i++)
            {
                CheckBox cb = (gvAdvancePayment.Rows[i].FindControl("cbTemp")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }
    }
}
