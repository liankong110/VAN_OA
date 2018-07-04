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
using VAN_OA.Model.KingdeeInvoice;
using System.Collections.Generic;
using VAN_OA.Dal.KingdeeInvoice;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.KingdeeInvoice
{
    public partial class WFAccountCompare : BasePage
    {
        List<BaseModel> list;
        System.Collections.Generic.Dictionary<string, decimal> invoice_List;
        private Sell_OrderFPService POSer = new Sell_OrderFPService();
        InvoiceReportService invoiceSer = new InvoiceReportService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                List<InvoiceReport> invoiceList = new List<InvoiceReport>();
                this.gvList.DataSource = invoiceList;
                this.gvList.DataBind();

                txtPOFrom.Text = DateTime.Now.AddYears(-1).Year + "-1-1";

                var user = new List<VAN_OA.Model.User>();
                var userSer = new VAN_OA.Dal.SysUserService();
                //user = userSer.getAllUserByPOList();
                //user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

                if (VAN_OA.JXC.SysObj.NewShowAll_Name("到款对比", Session["currentUserId"], "ShowAll") == false)
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



                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='记录'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='到款对比') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)

                if (VAN_OA.JXC.SysObj.NewShowAll_Name("到款对比", Session["currentUserId"], "Compare") == false)
                {
                    lblLianJie.Text = "1";
                    //lblQuanXian.Text = "1";
                    //gvList.Columns[6].Visible = false;
                }

            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        public void Show()
        {
            txtGuestName.Text = txtGuestName.Text.Trim();
            txtInvoiceNo.Text = txtInvoiceNo.Text.Trim();
            txtPOName.Text = txtPOName.Text.Trim();

            string sql = " where Status='通过' ";

            string sql_1 = "where 1=1 ";

            if (txtGuestName.Text .Trim()!= "")
            {
                sql_1 += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtInvoiceNo.Text != "")
            {
                sql_1 += string.Format(" and FPNo like '%{0}%'", txtInvoiceNo.Text);
            }
            if (txtInvoice.Text != "")
            {
                if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                    return;
                }
                sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlInvTotal.Text, txtInvoice.Text);
            }

            if (txtKISDaoKuanTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtKISDaoKuanTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('金蝶到账金额 格式错误！');</script>");
                    return;
                }
                sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlKISDaoKuanTotal.Text, txtKISDaoKuanTotal.Text);
            }
            if (txtOADaoKuanTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtOADaoKuanTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('OA到账金额 格式错误！');</script>");
                    return;
                }
                sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlOADaoKuanTotal.Text, txtOADaoKuanTotal.Text);
            }

            string param = "Status='通过'";

            if (ddlClose.Text != "-1" || ddlIsSelect.Text != "-1" || ddlHanShui.Text != "-1" || cbIsSpecial.Checked || cbJiaoFu.Checked || ddlUser.Text != "-1"
                || txtPOFrom.Text != "" || txtPOTo.Text != "" || txtPONo.Text != "" || ddlCompany.Text != "-1"|| ddlModel.Text != "全部")
            {

                if (ddlClose.Text != "-1")
                {
                    param += string.Format(" and IsClose={0}", ddlClose.Text);
                }
                if (ddlIsSelect.Text != "-1")
                {
                    param += string.Format(" and IsSelected={0}", ddlIsSelect.Text);
                }
                if (ddlHanShui.Text != "-1")
                {
                    param += string.Format(" and IsPoFax={0}", ddlHanShui.Text);
                }
                if (cbIsSpecial.Checked)
                {
                    param += string.Format(" and IsSpecial=0 ");
                }

                if (cbJiaoFu.Checked)
                {
                    param += string.Format(" and POStatue2='已交付' ");
                }
                if (ddlModel.Text != "全部")
                {
                    param += string.Format(" and Model='{0}'", ddlModel.Text);
                }

                if (txtPOFrom.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtPOFrom.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                        return;
                    }
                    param += string.Format(" and PODate>='{0} 00:00:00'", txtPOFrom.Text);
                }

                if (txtPOTo.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtPOTo.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                        return;
                    }
                    param += string.Format(" and PODate<='{0} 23:59:59'", txtPOTo.Text);
                }
                if (ddlUser.Text != "-1")
                {
                    param += string.Format(" and AppName={0} ", ddlUser.Text);
                }
                if (ddlCompany.Text != "-1")
                {
                    string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                    param += string.Format(" and IFZhui=0 and  AppName IN(select id from tb_User where {0}) ", where);
                }
                if (txtPONo.Text.Trim() != "")
                {
                    if (CheckPoNO(txtPONo.Text) == false)
                    {
                        return;
                    }
                    param += string.Format(" and PONO like '%{0}%' ", txtPONo.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPOName.Text))
                {
                    param += string.Format(" and PONAME like '%{0}%'", txtPOName.Text);
                }
                sql += string.Format(" and exists(select id from CG_POOrder where {0} and  CG_POOrder.PONO=Sell_OrderFP.PONO  and ifzhui=0)", param);
            }
            string diffDate = "";
            if (ddlTime.Text == "0")
            {
                diffDate = " DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())<=30 ";
            }
            if (ddlTime.Text == "1")
            {
                diffDate = " DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())>30 and DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())<=60";
            }
            if (ddlTime.Text == "2")
            {
                diffDate = " DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())>60 and DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())<=90";
            }
            if (ddlTime.Text == "3")
            {
                diffDate = " DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())>90 and DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())<=120";
            }
            if (ddlTime.Text == "4")
            {
                diffDate = " DATEDIFF(DAY,MAX(MaxRuTime),GETDATE())>120";
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return;
                }
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return;
                }
            }
            List<AccountReport> invoiceList = new List<AccountReport>();

            if (ddlCompare.Text == "2")
            {
                invoiceList = this.invoiceSer.GetAllAccountReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                  ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "1", cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate,
                  "-1",
 ddlKISDaoKuanTotal.Text, txtKISDaoKuanTotal.Text, ddlOADaoKuanTotal.Text, txtOADaoKuanTotal.Text);

                if (ddlCompareList.Text == "-1")
                {
                    invoiceList.AddRange(this.invoiceSer.GetAllAccountReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                     ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "0", cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate, 
                     "-1",
 ddlKISDaoKuanTotal.Text, txtKISDaoKuanTotal.Text, ddlOADaoKuanTotal.Text, txtOADaoKuanTotal.Text));
                }

            }
            else
            {
                invoiceList = this.invoiceSer.GetAllAccountReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                    ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, ddlCompare.Text, cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate,
                    false,"-1",
 ddlKISDaoKuanTotal.Text, txtKISDaoKuanTotal.Text, ddlOADaoKuanTotal.Text, txtOADaoKuanTotal.Text);
            }

            if (ddlCompare.Text == "1" || ddlCompare.Text == "2")
            {
                if (ddlCompareList.Text == "0")//项目编号
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);

                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal < accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal);
                }
                else if (ddlCompareList.Text == "1")//勿  
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal == accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal);
                }
                else if (ddlCompareList.Text == "2")//OA到帐需要警示和核对
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal == accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal);

                }
                else if (ddlCompareList.Text == "3")//.OA到帐需要核对 勿
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal < accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal && accountReport.All_OATotal >= accountReport.All_AccountTotal);
                }
                else if (ddlCompareList.Text == "3.1")//.OA到帐需要核对 项目编码
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal < accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal && accountReport.All_OATotal < accountReport.All_AccountTotal);
                }
                else if (ddlCompareList.Text == "3.2")//OA到帐金额<>金蝶到帐金额
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal!=accountReport.All_AccountTotal);
                }
                else if (ddlCompareList.Text == "4")//OA到帐需要警示
                {
                    invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);

                    invoiceList = invoiceList.FindAll(accountReport => !(accountReport.All_OATotal < accountReport.All_InvoiceTotal
                        && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal));

                    invoiceList = invoiceList.FindAll(accountReport => !((accountReport.All_OATotal == accountReport.All_InvoiceTotal
                       && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal) || (accountReport.All_OATotal < accountReport.All_InvoiceTotal
                           && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal)));

                    invoiceList = invoiceList.FindAll(accountReport => !(accountReport.All_OATotal == accountReport.All_InvoiceTotal
                       && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal));
                }
            }

            AspNetPager1.RecordCount = invoiceList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            int pageIndex = gvList.PageIndex;
            string allFPNo = "";
            string allPONOs = "";

            for (int i = (pageIndex * 10); i < ((pageIndex + 1) * 10); i++)
            {
                if (i >= invoiceList.Count)
                {
                    break;
                }
                if (!string.IsNullOrEmpty(invoiceList[i].All_InvoiceNo))
                {
                    allFPNo += "'" + invoiceList[i].All_InvoiceNo + "',";
                    allPONOs += "'" + invoiceList[i].All_PONO + "',";
                }
                if (!string.IsNullOrEmpty(invoiceList[i].OA_InvoiceNo) && string.IsNullOrEmpty(invoiceList[i].OA_GuestName))
                {
                    allFPNo += "'" + invoiceList[i].OA_InvoiceNo + "',";
                }
            }
            allFPNo = allFPNo.Trim(',');
            if (allFPNo != "")
            {
                string ponos = string.Format(@"SELECT Sell_OrderFP.FPNO,Sell_OrderFP.PONO,Sell_OrderFP.GuestNAME,CG_POOrder.AE from Sell_OrderFP left join CG_POOrder on CG_POOrder.pono= Sell_OrderFP.pono and IFZhui=0
 where Sell_OrderFP.Status='通过' and FPNO in ({0})
GROUP BY Sell_OrderFP.FPNO,Sell_OrderFP.PONO,Sell_OrderFP.GuestNAME ,CG_POOrder.AE", allFPNo);
                DataTable dt = DBHelp.getDataTable(ponos);
                list = new List<BaseModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new BaseModel { key = dr[0].ToString(), value = dr[1].ToString(), value1 = dr[2].ToString(), AE = dr[3].ToString() });
                }
                //ViewState["LISTPONO"] = list;
                //if (ddlCompare.Text == "1" || ddlCompare.Text == "2")
                //{
                //    invoice_List = new Dictionary<string, decimal>();
                //    string invoice_Sql = string.Format("select PONO,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' AND BusType=1 and PONO in ({0}) group by PONO", allPONOs);
                //    DataTable invoice_dt = DBHelp.getDataTable(invoice_Sql);
                //    foreach (DataRow dr in invoice_dt.Rows)
                //    {
                //        invoice_List.Add(dr[0].ToString(),Convert.ToDecimal(dr[1]));
                //    }
                //}

            }
            this.gvList.DataSource = invoiceList;
            this.gvList.DataBind();


            decimal OA_InvoiceTotal = 0;
            decimal Kingdee_InvoiceTotal = 0;

            Hashtable hs = new Hashtable();
            Hashtable hs1 = new Hashtable();
            foreach (var m in invoiceList)
            {
                if (!hs.ContainsKey(m.Kingdee_Id))
                {
                    Kingdee_InvoiceTotal += m.Kingdee_InvoiceTotal ?? 0;
                    hs.Add(m.Kingdee_Id, null);
                }

                if (!hs1.ContainsKey(m.OA_FPId))
                {
                    OA_InvoiceTotal += m.OA_InvoiceTotal ?? 0;
                    hs1.Add(m.OA_FPId, null);
                }
            }
            lblAllTotal.Text = invoiceList.Sum(t => t.All_InvoiceTotal ?? 0).ToString();
            var All_OATotal = invoiceList.Sum(t => t.All_OATotal ?? 0);
            lblAllOATotal.Text = All_OATotal.ToString();
            var All_AccountTotal = invoiceList.Sum(t => t.All_AccountTotal ?? 0);
            lblAllKinDaoTotal.Text = All_AccountTotal.ToString();
            lblAllFPTotal.Text = invoiceList.Sum(t => t.All_InvoiceTotal ?? 0).ToString();
            lblALLDiffTotal.Text = (All_OATotal - All_AccountTotal).ToString();


            lblOATotal.Text = OA_InvoiceTotal.ToString();
            lblKingdeeTotal.Text = Kingdee_InvoiceTotal.ToString();

        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                AccountReport model = e.Row.DataItem as AccountReport;
                LinkButton lb1 = e.Row.FindControl("LinkButton1") as LinkButton;
                lb1.OnClientClick = string.Format("javascript:window.open('../JXC/Sell_OrderPFList.aspx?InvoiceNo={0}','_blank'); return false;", model.All_InvoiceNo);

                LinkButton lb2 = e.Row.FindControl("LinkButton2") as LinkButton;
                lb2.OnClientClick = string.Format("javascript:window.open('../JXC/Sell_OrderPFList.aspx?InvoiceNo={0}','_blank'); return false;", model.OA_InvoiceNo);


                LinkButton lb3 = e.Row.FindControl("LinkButton3") as LinkButton;
                lb3.OnClientClick = string.Format("javascript:window.open('WFInvoiceList.aspx?InvoiceNo={0}','_blank'); return false;", model.Kingdee_InvoiceNo);

                var accountReport = e.Row.DataItem as AccountReport;
                if (accountReport != null)
                { 
                    if (!string.IsNullOrEmpty(accountReport.All_InvoiceNo))
                    {
                        string pono = "";
                        string guestName = "";

                        Label lblAll_PONO = e.Row.FindControl("lblAll_PONO") as Label;
                        Hashtable hs = new Hashtable();
                        var l = list.FindAll(t => t.key == model.All_InvoiceNo);
                        string ae = "";
                        foreach (var m in l)
                        {
                            pono += m.value + ",";
                            if (!hs.ContainsKey(m.value1))
                            {
                                guestName += m.value1 + ",";
                                ae += m.AE + ",";
                                hs.Add(m.value1, null);
                            }
                        }
                        lblAll_PONO.Text = pono.Trim(',');
                        Label lblAll_OAGuestName = e.Row.FindControl("lblAll_OAGuestName") as Label;
                        lblAll_OAGuestName.Text = guestName.Trim(',');

                        Label lblAll_OAAE = e.Row.FindControl("lblAll_OAAE") as Label;
                        lblAll_OAAE.Text = ae.Trim(',');
                    }

                    if (!string.IsNullOrEmpty(accountReport.OA_InvoiceNo) && cbInvoTotalToge.Checked)
                    {
                        string pono = "";
                        string guestName = "";
                        var l = list.FindAll(t => t.key == model.OA_InvoiceNo);
                        Hashtable hs = new Hashtable();
                        foreach (var m in l)
                        {
                            pono += m.value + ",";
                            if (!hs.ContainsKey(m.value1))
                            {
                                guestName += m.value1 + ",";
                                hs.Add(m.value1, null);
                            }
                        }
                        Label lblOA_PONO = e.Row.FindControl("lblOA_PONO") as Label;
                        lblOA_PONO.Text = pono.Trim(',');

                        Label lblOA_GuestName = e.Row.FindControl("lblOA_GuestName") as Label;
                        lblOA_GuestName.Text = guestName.Trim(',');
                    }
                    if (accountReport.All_OATotal != accountReport.All_AccountTotal)
                    {
                        e.Row.BackColor = System.Drawing.Color.Khaki;
                    }
                    if (accountReport.All_InvoiceTotal != null && string.IsNullOrEmpty(lblQuanXian.Text))
                    {
                        LinkButton lblRecord = e.Row.FindControl("lblRecord") as LinkButton;

                        lblRecord.Enabled = false;

                        accountReport.All_OATotal = accountReport.All_OATotal ?? 0;
                        accountReport.All_AccountTotal = accountReport.All_AccountTotal ?? 0;
                        if (accountReport.All_OATotal < accountReport.All_InvoiceTotal && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal)
                        {
                            e.Row.BackColor = System.Drawing.Color.YellowGreen;
                            lblRecord.Enabled = true;
                            string pono = "";
                            string linkPono = "";
                            string sql = string.Format(" FPNo='{0}'", model.All_InvoiceNo); ;
                            List<Sell_OrderFP> cars = this.POSer.GetFPtoInvoiceView_AccountCom(sql);
                            foreach (var m in cars)
                            {
                                pono += m.PONo + ",";
                                if ((int)DBHelp.ExeScalar(string.Format("select count(*) from TB_ToInvoice  WHERE State='执行中' and PoNo='{0}'", m.PONo)) > 0)
                                {
                                    lblRecord.Enabled = false;
                                }
                                else
                                {
                                    linkPono += string.Format("window.open('../EFrom/WFToInvoice.aspx?ProId=27&NewPONO={0}&weiDao={1}&GuestName={2}&POName={3}&FPNo={4}&FPId={5}','_blank');", m.PONo,
                                        m.chaTotals, HttpUtility.UrlEncode( m.GuestName), HttpUtility.UrlEncode(m.POName), m.FPNo, m.Id);
                                }
                            }
                            pono = pono.Trim(',');

                            accountReport.Record = pono;
                            if (string.IsNullOrEmpty(lblLianJie.Text))
                            {
                                lblRecord.OnClientClick = string.Format("javascript:" + linkPono + " return false;");
                            }
                            var doing = POSer.GetFPtoInvoiceView_Doing(" and " + sql);
                            if (doing.Count > 0)
                            {
                                Label lblRecordString = e.Row.FindControl("lblRecordString") as Label;
                                string ponoDing = "";
                                foreach (var m in doing)
                                {
                                    ponoDing += m + ",";
                                }
                                lblRecordString.Text = ponoDing.Trim(',');
                            }

                        }
                        else if (accountReport.All_OATotal == accountReport.All_InvoiceTotal && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal)
                        {
                            accountReport.Record = "勿";
                            lblRecord.BackColor = System.Drawing.Color.Blue;
                            lblRecord.ForeColor = System.Drawing.Color.White;
                        }
                        else if (accountReport.All_OATotal == accountReport.All_InvoiceTotal && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal)
                        {
                            accountReport.Record = "勿,OA到帐需要警示和核对";
                            lblRecord.BackColor = System.Drawing.Color.Red;
                        }
                        else if (accountReport.All_OATotal < accountReport.All_InvoiceTotal && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal)
                        {
                            e.Row.BackColor = System.Drawing.Color.YellowGreen;
                            //.  OA 到款金额<金蝶到帐金额  ，在记录一栏 ，你就显示 这张发票对应的项目编号，做一个超链接 到 到款单界面（见第二界面）
                            //，传递申请人，项目编号，项目名称，客户名称，发票编号，当天日期时间，和该发票到款金额= （金蝶到帐金额-OA到帐金额）
                            if (accountReport.All_OATotal < accountReport.All_AccountTotal)
                            {
                                lblRecord.Enabled = true;
                                string pono = "";
                                string linkPono = "";
                                string linkPonoFalse = "";
                                string sql = string.Format(" FPNo='{0}'", model.All_InvoiceNo); ;
                                List<Sell_OrderFP> cars = this.POSer.GetFPtoInvoiceView_AccountCom(sql);
                                foreach (var m in cars)
                                {
                                    pono += m.PONo + ",";
                                    if ((int)DBHelp.ExeScalar(string.Format("select count(*) from TB_ToInvoice  WHERE State='执行中' and PoNo='{0}'", m.PONo)) > 0)
                                    {
                                        lblRecord.Enabled = false;
                                    }
                                    else
                                    {
                                        linkPono += string.Format("window.open('../EFrom/WFToInvoice.aspx?ProId=27&NewPONO={0}&weiDao={1}&GuestName={2}&POName={3}&FPNo={4}&FPId={5}','_blank');", m.PONo,
                                            (accountReport.All_AccountTotal - accountReport.All_OATotal), HttpUtility.UrlEncode( m.GuestName), HttpUtility.UrlEncode(m.POName), m.FPNo, m.Id);
                                    }
                                }
                                pono = pono.Trim(',');

                                accountReport.Record = pono;
                                if (string.IsNullOrEmpty(lblLianJie.Text))
                                {
                                    if (!string.IsNullOrEmpty(linkPono))
                                    {
                                        lblRecord.OnClientClick = string.Format("javascript:" + linkPono + " return false;");
                                    }

                                }
                                var doing = POSer.GetFPtoInvoiceView_Doing(" and " + sql);
                                Label lblRecordString = e.Row.FindControl("lblRecordString") as Label;
                                if (doing.Count > 0)
                                {

                                    string ponoDing = "";
                                    foreach (var m in doing)
                                    {
                                        ponoDing += m + ",";
                                    }
                                    lblRecordString.Text = ponoDing.Trim(',');
                                }

                            }
                            else
                            {
                                accountReport.Record = "勿,OA到帐需要核对";
                                lblRecord.BackColor = System.Drawing.Color.Blue;
                                lblRecord.ForeColor = System.Drawing.Color.White;

                            }
                        }
                        else
                        {
                            accountReport.Record = "勿,OA到款需要警示";
                            lblRecord.BackColor = System.Drawing.Color.Blue;
                            lblRecord.ForeColor = System.Drawing.Color.White;
                        }
                        lblRecord.Text = accountReport.Record;

                       
                    }
                }
            }

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)   //判断创建的行是不是标题行
            {
                TableCellCollection cells = e.Row.Cells;
                cells.Clear();  //获得标题行,清空标题行的设置
                cells.Add(new TableHeaderCell());  //添加一个标题单元
                cells[0].RowSpan = 12;   //设置跨行.         
                //直接导入html中的table中的元素

                if (!(cbInvoTotalToge.Checked && string.IsNullOrEmpty(lblQuanXian.Text)))
                {
                    cells[0].Text = @"</th><th colspan=7>OA系统+金蝶系统</th><th colspan=7>OA系统</th><th colspan=5>金蝶系统</th></tr><tr style='height: 20px; background-color: #336699; color: White;'>
<th>
发票号
</th><th>发票日期</th> <th>
项目编码
</th>
<th>
OA客户名称
</th>
<th>
AE
</th>
<th>
金额
</th> 
<th>
OA到帐金额
</th>
<th>
金蝶到帐金额
</th>

<th>
发票号
</th>
<th>
项目编号
</th>
<th>
金额
</th>
<th>
日期
</th>
<th>
客户名称
</th>
<th>
到帐金额
</th>
<th>
发票号
</th>
<th>
客户名称
</th>
<th>
金额
</th>
<th>
日期</th><th>
到帐金额";
                }
                else
                {
                    cells[0].Text = @"</th><th colspan=8>OA系统+金蝶系统</th><th colspan=7>OA系统</th><th colspan=5>金蝶系统</th></tr><tr style='height: 20px; background-color: #336699; color: White;'>
<th>
发票号
</th><th>发票日期</th> <th>
项目编码
</th>
<th>
OA客户名称
</th>
<th>
AE
</th>
<th>
金额
</th> 
<th>
OA到帐金额
</th>
<th>
金蝶到帐金额
</th>
<th>
记录
</th>
<th>
发票号
</th>
<th>
项目编号
</th>
<th>
金额
</th>
<th>
日期
</th>
<th>
客户名称
</th>
<th>
到帐金额
</th>
<th>
发票号
</th>
<th>
客户名称
</th>
<th>
金额
</th>
<th>
日期</th><th>
到帐金额";
                }

            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void cbSameFP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSameFP.Checked)
            {
                cbInvoTotalToge.Enabled = false;
                cbInvoTotalToge.Checked = true;
            }
            else
            {
                cbInvoTotalToge.Enabled = true;
            }
        }

        protected void ddlCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCompareList.Text = "-1";
            if (ddlCompare.Text == "0")
            {
                ddlCompareList.Enabled = false;
            }
            else
            {
                ddlCompareList.Enabled = true;
            }
        }



    }
}
