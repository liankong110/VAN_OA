using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;

namespace VAN_OA.Fin
{
    public partial class WFBandFlow : BasePage
    {
        BankFlowService bandFlowSer = new BankFlowService();
        Out_BankFlowService out_bandFlowSer = new Out_BankFlowService();
        In_BankFlowService in_bandFlowSer = new In_BankFlowService();

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ViewState["QuerySql"] != null)
            {
                Session["QuerySql"] = ViewState["QuerySql"];
            }
            base.Response.Redirect("~/Fin/WFAddBandFlow.aspx");
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

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                ViewState["ReferenceNumber"] = e.CommandArgument;

                MainDetail();
            }
        }

        private void MainDetail()
        {
            var outList = out_bandFlowSer.GetListArray(string.Format("  ReferenceNumber='{0}'", ViewState["ReferenceNumber"]));
            var inList = in_bandFlowSer.GetListArray(string.Format("  ReferenceNumber='{0}'", ViewState["ReferenceNumber"]));
            gv_In.DataSource = inList;
            gv_In.DataBind();
            gv_Out.DataSource = outList;
            gv_Out.DataBind();
            if (outList.Count > 0)
            {
                TabContainer1.ActiveTabIndex = 1;
            }
            if (inList.Count > 0)
            {
                TabContainer1.ActiveTabIndex = 0;
            }
        }

        private List<BankFlow> LoadList(out Dictionary<string, string> queryList)
        {
             queryList = new Dictionary<string, string>();

            string sql = "1=1";
            if (txtTransactionReferenceNumber.Text != "")
            {
                sql += string.Format(" and TransactionReferenceNumber like '%{0}%'", txtTransactionReferenceNumber.Text);
                queryList.Add("txtTransactionReferenceNumber", txtTransactionReferenceNumber.Text);
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('交易日期 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and TransactionDate>='{0} 00:00:00'", txtFrom.Text);
                queryList.Add("txtFrom", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('交易日期 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and TransactionDate<='{0} 23:59:59'", txtTo.Text);
                queryList.Add("txtTo", txtTo.Text);
            }

            if (ddlTransactionType.Text != "全部")
            {
                sql += string.Format("  and TransactionType ='{0}'", ddlTransactionType.Text);
                queryList.Add("ddlTransactionType", ddlTransactionType.Text);
            }

            if (!string.IsNullOrEmpty(txtTradeAmountFrom.Text))
            {
                if (CommHelp.VerifesToNum(txtTradeAmountFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('交易金额 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and {1}{0}TradeAmount", ddlTradeAmountFrom.Text, txtTradeAmountFrom.Text);
                queryList.Add("ddlTradeAmountFrom", ddlTradeAmountFrom.Text);
                queryList.Add("txtTradeAmountFrom", txtTradeAmountFrom.Text);
            }
            if (!string.IsNullOrEmpty(txtTradeAmountTo.Text))
            {
                if (CommHelp.VerifesToNum(txtTradeAmountTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('交易金额 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and TradeAmount{0}{1}", ddlTradeAmountTo.Text, txtTradeAmountTo.Text);
                queryList.Add("ddlTradeAmountTo", ddlTradeAmountTo.Text);
                queryList.Add("txtTradeAmountTo", txtTradeAmountTo.Text);
            }

            if (txtOutPayerName.Text != "")
            {
                sql += string.Format(" and OutPayerName like '%{0}%'", txtOutPayerName.Text);
                queryList.Add("txtOutPayerName", txtOutPayerName.Text);
            }
            if (txtInPayeeName.Text != "")
            {
                sql += string.Format(" and InPayeeName like '%{0}%'", txtInPayeeName.Text);
                queryList.Add("txtInPayeeName", txtInPayeeName.Text);
            }



            //子表信息
            string inSql = string.Format("");
            string out_Sql = string.Format("");
            if (ddlIncomeType.Text != "全部")
            {
                inSql += string.Format(" and InType ='{0}'", ddlIncomeType.Text);
                queryList.Add("ddlIncomeType", ddlIncomeType.Text);
            }
            if (txtFPNo.Text.Trim() != "")
            {
                inSql += string.Format(" and FPNo like '%{0}%'", txtFPNo.Text.Trim());
                queryList.Add("txtFPNo", txtFPNo.Text.Trim());
            }

            if (ddlPaymentType.Text != "全部")
            {
                out_Sql += string.Format(" and OutType ='{0}'", ddlPaymentType.Text);
                queryList.Add("ddlPaymentType", ddlPaymentType.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return null;
                }
                out_Sql += string.Format(" and ProNo ='{0}'", txtProNo.Text.Trim());
                queryList.Add("txtProNo", txtProNo.Text.Trim());
            }

            if (txtNotes.Text != "")
            {
                sql += string.Format(" and (exists(select id from In_BankFlow where In_BankFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber and Remark like '%{0}%')", txtNotes.Text);
                sql += string.Format(" or exists(select id from Out_BankFlow where Out_BankFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber and Remark like '%{0}%'))", txtNotes.Text);
                queryList.Add("txtNotes", txtNotes.Text);
            }
            if (ddlProgress.Text != "-1")
            {
                if (ddlProgress.Text == "0")//开始未完成
                {
                    sql += string.Format(" and ( (SUMFPTotal<TradeAmount AND TransactionType='来账') or (SUMOutTotal<ABS(TradeAmount) AND TransactionType='往账'))");
                }
                else if (ddlProgress.Text == "1")//未开始
                {
                    sql += string.Format(" and ( (SUMFPTotal IS NULL AND TransactionType='来账') or (SUMOutTotal IS NULL AND TransactionType='往账'))");
                }
                else if (ddlProgress.Text == "2")//未完成
                {
                    sql += string.Format(" and ( (ISNULL(SUMFPTotal,0)<TradeAmount AND TransactionType='来账') or (ISNULL(SUMOutTotal,0)<ABS(TradeAmount) AND TransactionType='往账'))");
                }
                else if (ddlProgress.Text == "3")//已完成
                {
                    sql += string.Format(" and ( (SUMFPTotal=TradeAmount AND TransactionType='来账') or (SUMOutTotal =ABS(TradeAmount) AND TransactionType='往账'))");
                }
                queryList.Add("ddlProgress", ddlProgress.Text);
            }
            if (!string.IsNullOrEmpty(inSql))
            {
                inSql = string.Format("  exists(select id from In_BankFlow where In_BankFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber {0})", inSql);
            }
            if (!string.IsNullOrEmpty(out_Sql))
            {
                out_Sql = string.Format(" exists(select id from Out_BankFlow where Out_BankFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber {0})", out_Sql);
            }
            if (!string.IsNullOrEmpty(inSql) && !string.IsNullOrEmpty(out_Sql))
            {
                sql += string.Format(" and ({0} or {1})", inSql, out_Sql);
            }
            else if (!string.IsNullOrEmpty(inSql))
            {
                sql += string.Format(" and {0}", inSql);
            }
            else if (!string.IsNullOrEmpty(out_Sql))
            {
                sql += string.Format(" and {0}", out_Sql);
            }
            ViewState["QuerySql"] = queryList;
            //Session["QuerySql"] = null;
            List<BankFlow> caiList = this.bandFlowSer.GetListArray(sql);
            return caiList;
        }

        private void Show()
        {
            Dictionary<string, string> queryList = new Dictionary<string, string>();
            var caiList = LoadList(out queryList);
            if (caiList == null)
            {
                return;
            }
            AspNetPager1.RecordCount = caiList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = caiList;
            this.gvList.DataBind();
            gv_In.DataSource = new List<VAN_OA.Model.Fin.In_BankFlow>();
            gv_In.DataBind();

            gv_Out.DataSource = new List<VAN_OA.Model.Fin.Out_BankFlow>();
            gv_Out.DataBind();

            queryList.Add("PageIndex", AspNetPager1.CurrentPageIndex.ToString());
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

                //导入后每条记录填入数据以后 如金额相符（就是该交易流水号输入的进账或出账的总金额和银行的取正总金额 相等），
                //该记录的底色显示淡绿色，如金额还没有填满（就是该交易流水号输入的进账或出账的总金额小于银行的取正总金额 ） 该记录的底色显示土黄色。
                //没有输入数据的记录保持底色白色。
                var model = e.Row.DataItem as BankFlow;
                if (model != null)
                {
                    if (model.TransactionType == "来账")
                    {
                        if (model.SUMFPTotal == model.TradeAmount)
                        {
                            e.Row.BackColor = System.Drawing.Color.PaleGreen;
                        }
                        else if (model.SUMFPTotal != 0 && model.SUMFPTotal < model.TradeAmount)
                        {
                            e.Row.BackColor = System.Drawing.Color.Khaki;
                        }

                    }
                    else
                    {
                        if (model.SUMOutTotal == System.Math.Abs(model.TradeAmount))
                        {
                            e.Row.BackColor = System.Drawing.Color.PaleGreen;
                        }
                        else if (model.SUMOutTotal != 0 && model.SUMFPTotal < System.Math.Abs(model.TradeAmount))
                        {
                            e.Row.BackColor = System.Drawing.Color.Khaki;
                        }
                    }
                }
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.bandFlowSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (ViewState["QuerySql"] != null)
            {
                Session["QuerySql"] = ViewState["QuerySql"];
            }
            var model = bandFlowSer.GetModel(Convert.ToInt32(gvList.DataKeys[e.NewEditIndex].Value));
            if (model.TransactionType == "来账")
            {
                base.Response.Redirect("~/Fin/WFIn_BankFlow.aspx?TransactionReferenceNumber=" + model.TransactionReferenceNumber);
            }
            else
            {
                base.Response.Redirect("~/Fin/WFOut_BankFlow.aspx?TransactionReferenceNumber=" + model.TransactionReferenceNumber);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                var year = DateTime.Now.Year;
                for (int i = year; i > (year - 10); i--)
                {
                    ddlYear.Items.Add(i.ToString());
                }

                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(i.ToString());
                }
                ddlMonth.Text = DateTime.Now.Month.ToString();

                List<BankFlow> carList = new List<BankFlow>();
                this.gvList.DataSource = carList;
                this.gvList.DataBind();

                if (NewShowAll_textName("财务审核", "可编辑") == false)
                {
                    gvList.Columns[0].Visible = false;
                    gv_In.Columns[0].Visible = false;
                    gv_In.Columns[1].Visible = false;

                    gv_Out.Columns[0].Visible = false;
                    gv_Out.Columns[1].Visible = false;
                }

                if (Session["QuerySql"] != null)
                {
                    var queryList = Session["QuerySql"] as Dictionary<string, string>;

                    if (queryList.ContainsKey("txtTransactionReferenceNumber"))
                    {
                        txtTransactionReferenceNumber.Text = queryList["txtTransactionReferenceNumber"];
                    }

                    if (queryList.ContainsKey("txtFrom.Text"))
                    {
                        txtFrom.Text = queryList["txtFrom"];
                    }
                    if (queryList.ContainsKey("txtTo.Text"))
                    {
                        txtTo.Text = queryList["txtTo"];
                    }

                    if (queryList.ContainsKey("ddlTransactionType"))
                    {
                        ddlTransactionType.Text = queryList["ddlTransactionType"];
                    }

                    if (queryList.ContainsKey("txtTradeAmountFrom"))
                    {
                        ddlTradeAmountFrom.Text = queryList["ddlTradeAmountFrom"];
                        txtTradeAmountFrom.Text = queryList["txtTradeAmountFrom"];
                    }
                    if (queryList.ContainsKey("txtTradeAmountTo"))
                    {
                        ddlTradeAmountTo.Text = queryList["ddlTradeAmountTo"];
                        txtTradeAmountTo.Text = queryList["txtTradeAmountTo"];
                    }
                    if (queryList.ContainsKey("txtOutPayerName"))
                    {
                        txtOutPayerName.Text = queryList["txtOutPayerName"];
                    }
                    if (queryList.ContainsKey("txtInPayeeName"))
                    {
                        txtInPayeeName.Text = queryList["txtInPayeeName"];
                    }
                    if (queryList.ContainsKey("ddlIncomeType"))
                    {
                        ddlIncomeType.Text = queryList["ddlIncomeType"];
                    }
                    if (queryList.ContainsKey("txtFPNo"))
                    {
                        txtFPNo.Text = queryList["txtFPNo"];
                    }
                    if (queryList.ContainsKey("ddlPaymentType"))
                    {
                        ddlPaymentType.Text = queryList["ddlPaymentType"];
                    }
                    if (queryList.ContainsKey("txtProNo"))
                    {
                        txtProNo.Text = queryList["txtProNo"];
                    }
                    if (queryList.ContainsKey("txtNotes"))
                    {
                        txtNotes.Text = queryList["txtNotes"];
                    }
                    if (queryList.ContainsKey("ddlProgress"))
                    {
                        ddlProgress.Text = queryList["ddlProgress"];
                    }
                    AspNetPager1.PageSize = 10;
                    AspNetPager1.RecordCount = Convert.ToInt32(queryList["PageIndex"]) * 10 + 1;
                    AspNetPager1.CurrentPageIndex = Convert.ToInt32(queryList["PageIndex"]);

                    Show();
                }
            }
        }

        protected void gv_Out_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (ViewState["QuerySql"] != null)
            {
                Session["QuerySql"] = ViewState["QuerySql"];
            }
            base.Response.Redirect("~/Fin/WFOut_BankFlow.aspx?Id=" + this.gv_Out.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void gv_In_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (ViewState["QuerySql"] != null)
            {
                Session["QuerySql"] = ViewState["QuerySql"];
            }
            base.Response.Redirect("~/Fin/WFIn_BankFlow.aspx?Id=" + this.gv_In.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void gv_Out_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.out_bandFlowSer.Delete(Convert.ToInt32(this.gv_Out.DataKeys[e.RowIndex].Value.ToString()));
            MainDetail();
        }

        protected void gv_In_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.in_bandFlowSer.Delete(Convert.ToInt32(this.gv_In.DataKeys[e.RowIndex].Value.ToString()));
            MainDetail();
        }

        protected void cbCompany_CheckedChanged(object sender, EventArgs e)
        {

            if ((sender as CheckBox).Checked)
            {
                txtOutPayerName.Text = "苏州万邦电脑系统有限公司";
            }
            else
            {
                txtOutPayerName.Text = "";
            }
        }

        protected void cbCompany1_CheckedChanged(object sender, EventArgs e)
        {

            if ((sender as CheckBox).Checked)
            {
                txtInPayeeName.Text = "苏州万邦电脑系统有限公司";
            }
            else
            {
                txtInPayeeName.Text = "";
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (ViewState["QuerySql"] != null)
            {
                Session["QuerySql"] = ViewState["QuerySql"];
            }
            base.Response.Redirect("~/Fin/WFReport.aspx?year=" + ddlYear.Text + "&month=" + ddlMonth.Text);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            gvList.AllowPaging = false;
            gvList.Columns[0].Visible = false;
            gvList.Columns[1].Visible = false; Response.Clear();
            gvList.HeaderStyle.Height = 20;
            Show();
            
            foreach (GridViewRow dg in this.gvList.Rows)
            {               
                dg.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");            
                dg.Cells[8].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
                dg.Cells[14].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            }

            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=Band.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
          
            gvList.RenderControl(hw);        
            Response.Write(sw.ToString());
            Response.End();

            AspNetPager1.CurrentPageIndex = 1;
            gvList.AllowPaging = true;
            gvList.Columns[0].Visible = true;
            gvList.Columns[1].Visible = true;
            Show();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}
