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
    public partial class WFAllotAccountCompare : BasePage
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
                ddlModel.DataValueField = "ModelName";

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
                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";



//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='记录'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='到款对比') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
//                {
//                    lblQuanXian.Text = "1";
//                    gvList.Columns[6].Visible = false;
//                }

            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        public void Show()
        {

            string sql = " where Status='通过' ";

            string sql_1 = "where 1=1 ";
            txtGuestName.Text = txtGuestName.Text.Trim();
            txtInvoiceNo.Text = txtInvoiceNo.Text.Trim();
            if (txtGuestName.Text.Trim() != "")
            {
                sql_1 += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtInvoiceNo.Text != "")
            {
                sql_1 += string.Format(" and FPNo like '%{0}%'", txtInvoiceNo.Text);
            }
            
            if (cbZhengShu.Checked == false && txtInvoice.Text != "")
            {
                if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                    return;
                }
                if (ddlInvTotal.Text == "=")
                {
                    sql_1 += string.Format(" and Sell_OrderFP.Total in ({0},{1})", Convert.ToDecimal(txtInvoice.Text) ,- Convert.ToDecimal(txtInvoice.Text));
                }
                else
                {
                    sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlInvTotal.Text, txtInvoice.Text);
                }
            }
            if (cbZhengShu.Checked && txtInvoice.Text != "")
            {
                if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                    return;
                }
                if (ddlInvTotal.Text == "=")
                {
                    sql_1 += string.Format(" and Sell_OrderFP.Total ={0} ", txtInvoice.Text);
                }
                else
                {
                    sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlInvTotal.Text, txtInvoice.Text);
                }
            }

            string param = "Status='通过'";

            if (ddlClose.Text != "-1" || ddlIsSelect.Text != "-1" || ddlHanShui.Text != "-1" || cbIsSpecial.Checked || cbJiaoFu.Checked || ddlUser.Text != "-1"
                || txtPOFrom.Text != "" || txtPOTo.Text != "" || txtPONo.Text != "" || ddlCompany.Text != "-1"||ddlModel.Text != "全部")
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
                if (ddlModel.Text != "-1")
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
                    param += string.Format(" and AppName IN(select id from tb_User where {0}) ", where);
                }
                if (txtPONo.Text.Trim() != "")
                {
                    if (CheckPoNO(txtPONo.Text) == false)
                    {
                        return;
                    }
                    param += string.Format(" and PONO like '%{0}%' ", txtPONo.Text.Trim());
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
            if (string.IsNullOrEmpty(lblQuanXian.Text))
            {
                gvList.Columns[6].Visible = true;
            }
            else
            {
                gvList.Columns[6].Visible = false;
               
            }
            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtTo.Text))
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
                  ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "1", cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate, cbZhengShu.Checked);

                if (ddlCompareList.Text == "-1")
                {
                    invoiceList.AddRange(this.invoiceSer.GetAllAccountReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                     ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "0", cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate, cbZhengShu.Checked));
                }
            }
            else
            {
                invoiceList = this.invoiceSer.GetAllAccountReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                    ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, ddlCompare.Text, cbInvoTotalToge.Checked, ddlIsorder.Text, cbSameFP.Checked, diffDate, cbZhengShu.Checked, ddlCompareList.Text);
            }

            //if (ddlCompare.Text == "1" || ddlCompare.Text == "2")
            //{               
                
            //    if (ddlCompareList.Text == "0")
            //    {
            //        invoiceList = invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);
            //        invoiceList = invoiceList.FindAll(accountReport => accountReport.All_OATotal == accountReport.All_InvoiceTotal
            //            && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal);


            //        var invoiceList2=  invoiceList.FindAll(accountReport => accountReport.All_InvoiceTotal != null);

            //        invoiceList2 = invoiceList2.FindAll(accountReport => !(accountReport.All_OATotal < accountReport.All_InvoiceTotal
            //            && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal));

            //        invoiceList2 = invoiceList2.FindAll(accountReport => !((accountReport.All_OATotal == accountReport.All_InvoiceTotal
            //           && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal) || (accountReport.All_OATotal < accountReport.All_InvoiceTotal
            //               && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal)));

            //        invoiceList2 = invoiceList2.FindAll(accountReport => !(accountReport.All_OATotal == accountReport.All_InvoiceTotal
            //           && accountReport.All_AccountTotal < accountReport.All_InvoiceTotal));

            //        invoiceList.AddRange(invoiceList2);
            //    }
            //}
            invoiceList = (from items in invoiceList orderby items.MaxDate descending select items).ToList();
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
                string ponos = string.Format("SELECT FPNO,PONO,GuestNAME from Sell_OrderFP where Status='通过'  and FPNO in ({0})GROUP BY FPNO,PONO,GuestNAME ", allFPNo);
                DataTable dt = DBHelp.getDataTable(ponos);
                list = new List<BaseModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new BaseModel { key = dr[0].ToString(), value = dr[1].ToString(), value1 = dr[2].ToString() });
                }
              

            }
            lblSumTotal.Text = "";
            List<AccountReport> resultInvoiceList = new List<AccountReport>();
            resultInvoiceList = invoiceList.FindAll(t => t.All_WeiInvoiceTotal > 0);
            
         
            //int[] b = { 53, 575, 55, 78, 340, 9, 121, 338, 39, 15 };
          

            ViewState["result"] = resultInvoiceList;
            if (resultInvoiceList.Count <= 50)
            {
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
            lblSumTotal.Text = "";
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
            var All_OATotal= invoiceList.Sum(t => t.All_OATotal ?? 0);
            lblAllOATotal.Text = All_OATotal.ToString();
            var All_AccountTotal=invoiceList.Sum(t => t.All_AccountTotal ?? 0);
            lblAllKinDaoTotal.Text = All_AccountTotal.ToString();
            lblAllFPTotal.Text = invoiceList.Sum(t => t.All_InvoiceTotal ?? 0).ToString();
            lblALLDiffTotal.Text = (All_OATotal - All_AccountTotal).ToString();
            lblWeiAllOATotal.Text = invoiceList.Sum(t => t.All_WeiOATotal ?? 0).ToString();
            lblWeiAllKinDaoTotal.Text = invoiceList.Sum(t => t.All_WeiInvoiceTotal).ToString();

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
                        foreach (var m in l)
                        {
                            pono += m.value + ",";
                             if (!hs.ContainsKey(m.value1))
                            {
                                guestName += m.value1 + ","; 
                                hs.Add(m.value1, null);
                            }
                        }
                        lblAll_PONO.Text = pono.Trim(',');
                        Label lblAll_OAGuestName = e.Row.FindControl("lblAll_OAGuestName") as Label;
                        lblAll_OAGuestName.Text = guestName.Trim(',');
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
                                hs.Add(m.value1,null);
                            }
                        }
                        Label lblOA_PONO = e.Row.FindControl("lblOA_PONO") as Label;
                        lblOA_PONO.Text = pono.Trim(',');

                        Label lblOA_GuestName = e.Row.FindControl("lblOA_GuestName") as Label;
                        lblOA_GuestName.Text = guestName.Trim(',');
                    }

                    if (accountReport.All_InvoiceTotal != null && string.IsNullOrEmpty(lblQuanXian.Text))
                    {
                        LinkButton lblRecord = e.Row.FindControl("lblRecord") as LinkButton;

                        lblRecord.Enabled = false;

                        accountReport.All_OATotal = accountReport.All_OATotal ?? 0;
                        accountReport.All_AccountTotal = accountReport.All_AccountTotal ?? 0;
                        if (accountReport.All_OATotal < accountReport.All_InvoiceTotal && accountReport.All_AccountTotal == accountReport.All_InvoiceTotal)
                        {
                            lblRecord.Enabled = true;
                            string pono = "";
                            string linkPono = "";
                            string sql = string.Format(" FPNo='{0}'", model.All_InvoiceNo); ;
                            List<Sell_OrderFP> cars = this.POSer.GetFPtoInvoiceView(sql);
                            foreach (var m in cars)
                            {
                                pono += m.PONo + ",";
                                linkPono += string.Format("window.open('../EFrom/WFToInvoice.aspx?ProId=27&NewPONO={0}&weiDao={1}&GuestName={2}&POName={3}&FPNo={4}&FPId={5}','_blank');", m.PONo,
                                    m.chaTotals, m.GuestName, m.POName, m.FPNo, m.Id);
                            }
                            pono = pono.Trim(',');

                            accountReport.Record = pono;

                            lblRecord.OnClientClick = string.Format("javascript:" + linkPono + " return false;");

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
                            accountReport.Record = "勿,OA到帐需要核对";
                            lblRecord.BackColor = System.Drawing.Color.Blue;
                            lblRecord.ForeColor = System.Drawing.Color.White;
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
                    cells[0].Text = @"</th><th colspan=9>OA系统+金蝶系统</th></tr><tr style='height: 20px; background-color: #336699; color: White;'>
<th>
发票号
</th><th>发票日期</th> <th>
项目编码
</th>
<th>
OA客户名称
</th>
<th>
金额
</th> 
<th>
OA到帐金额
</th>
<th>
金蝶到帐金额</th>
<th>
OA发票未到账金额
</th> 
<th>
金蝶发票未到帐金额
";
                }
                else
                {
                    cells[0].Text = @"</th><th colspan=10>OA系统+金蝶系统</th></tr><tr style='height: 20px; background-color: #336699; color: White;'>
<th>
发票号
</th><th>发票日期</th> <th>
项目编码
</th>
<th>
OA客户名称
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
OA发票未到账金额
</th> 
<th>
金蝶发票未到帐金额</th>
<th>
记录";
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

        //public void Calculate(int[] source, int index, int sum, string result,int sumTotal)
        //{
        //    for (int i = index; i < source.Length; i++)
        //    {
        //        if (sum + source[i] < sumTotal)
        //        {
        //            Calculate(source, i + 1, sum + source[i], result + "," + source[i], sumTotal);
        //        }
        //        else if (sum + source[i] == sumTotal)
        //        {
        //            lblSumTotal.Text += result + "," + source[i];
        //            return;
        //        }
        //    }
        //}

        int myIndex = 1;
        public void Calculate(List<AccountReport> invoice, int index, decimal sum, string result, decimal sumTotal, List<AccountReport> resultInvoice,DateTime dt)
        {

            if ((DateTime.Now - dt).TotalSeconds > 240)
            {
                return;
            }
            for (int i = index; i < invoice.Count; i++)
            {
                var mess = result + "," + invoice[i].All_InvoiceNo + ":" + invoice[i].All_WeiInvoiceTotal;
                if (sum + invoice[i].All_WeiInvoiceTotal < sumTotal)
                {
                    resultInvoice.Add(invoice[i]);
                   // Calculate(invoice, (i + 1), sum + invoice[i].All_WeiInvoiceTotal, result + "," + invoice[i].All_InvoiceNo + ":" + invoice[i].All_WeiInvoiceTotal, sumTotal, resultInvoice, dt);
                    Calculate(invoice, (i + 1), sum + invoice[i].All_WeiInvoiceTotal, mess, sumTotal, resultInvoice, dt);
                }
                else if (sum + invoice[i].All_WeiInvoiceTotal == sumTotal)
                {
                    resultInvoice.Add(invoice[i]);
                    lblSumTotal.Text += "方案" + myIndex + ":" + Environment.NewLine + mess.Trim(',') + Environment.NewLine;
                    myIndex++;
                   // Calculate(invoice, (i + 1), 0, mess, sumTotal, resultInvoice, dt);

                    //break;
                }
            }

            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtDaoTotal.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额请输入金额！');</script>");
                return ;
            }
            myIndex = 1;
            lblSumTotal.Text = "";
            var invoiceList =  ViewState["result"] as  List<AccountReport>;
            invoiceList = invoiceList.FindAll(t => t.All_WeiInvoiceTotal>0);
            var resultInvoiceList=new List<AccountReport> ();        
            Calculate(invoiceList, 0, 0, "", Convert.ToDecimal(txtDaoTotal.Text), resultInvoiceList, DateTime.Now);
            lblSumTotal.Text = lblSumTotal.Text.Trim(',');
            if (lblSumTotal.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('没有找到合适的方案！');</script>");
                return;
            }
            if (lblSumTotal.Text != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计算成功！');</script>");
                return;
            }
        }


    }
}
