using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VAN_OA.Dal.Fin;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.Fin;
using VAN_OA.Model.ReportForms;



namespace VAN_OA.Fin
{
    public partial class WFIn_BankFlow : System.Web.UI.Page
    {

        private In_BankFlowService inBankSer = new In_BankFlowService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    //     string sqlCheck = string.Format("select count(*) from In_BankFlow where ReferenceNumber='{0}' and FPNo='{1}'",
                    //lblReferenceNumber.Text, txtFPNo.Text);
                    //     if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    //     {
                    //         base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('流水号[{0}]，发票号[{1}]，已经存在！');</script>", lblReferenceNumber.Text, txtFPNo.Text));
                    //         return;
                    //     }
                    In_BankFlow model = getModel();
                    if (this.inBankSer.Add(model) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFBandFlow.aspx");
        }

        private void Clear()
        {
            this.lblNumber.Text = "";
            this.txtFPNo.Text = "";
            this.txtFPTotal.Text = "";
            this.txtRemark.Text = "";
            txtInvoiceTotal.Text = "";
            txtGuestName.Text = "";
          
        }


        public In_BankFlow getModel()
        {
            string Number = this.lblNumber.Text;
            string ReferenceNumber = this.lblReferenceNumber.Text;
            string InType = this.ddlInType.Text;
            string FPNo = this.txtFPNo.Text.Trim();
            decimal FPTotal = decimal.Parse(this.txtFPTotal.Text);
            string Remark = this.txtRemark.Text;

            In_BankFlow model = new In_BankFlow();
            model.Number = Number;
            model.ReferenceNumber = ReferenceNumber;
            model.InType = InType;
            model.FPNo = FPNo;
            model.FPTotal = FPTotal;
            model.Remark = Remark;

            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    //        string sqlCheck = string.Format("select count(*) from In_BankFlow where ReferenceNumber='{0}' and FPNo='{1}' AND ID<>{2}",
                    //lblReferenceNumber.Text, txtFPNo.Text, Request["Id"]);
                    //        if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    //        {
                    //            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('流水号[{0}]，发票号[{1}]，已经存在！');</script>", lblReferenceNumber.Text, txtFPNo.Text));
                    //            return;
                    //        }
                    In_BankFlow model = getModel();
                    if (this.inBankSer.Update(model))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {
            string strErr = "";
            if (ddlInType.Text == "发票回款" && this.txtInvoiceTotal.Text.Trim().Length == 0)
            {
                strErr += "请重新输入发票编号并点击获取金额 ！\\n";
            }
          
            if (this.txtFPTotal.Text.Trim().Length == 0)
            {
                strErr += "进账金额不能为空！\\n";
            }
            if (CommHelp.VerifesToNum(this.txtFPTotal.Text.Trim()) == false)
            {
                strErr += "进账金额格式错误！\\n";
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }

            if (ddlInType.Text == "发票回款")
            {
                string sumTotal = string.Format("select isnull(sum(FPTotal),0) from In_BankFlow where FPNo='{0}'", txtFPNo.Text.Trim());
                if (base.Request["Id"] != null)
                {
                    sumTotal += " and id<>" + Request["Id"];
                }
                var sqlsumTotal = Convert.ToDecimal(DBHelp.ExeScalar(sumTotal));
                var total = Convert.ToDecimal(txtFPTotal.Text) + sqlsumTotal;
                if (total >Convert.ToDecimal(txtInvoiceTotal.Text))
                {
                    strErr = "该发票编号剩余总金额"+ (Convert.ToDecimal(txtInvoiceTotal.Text)- sqlsumTotal) + " ！\\n";
                }
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }

            //添加按钮需要同时累计当前这个交易流水号所有的进账金额（含当前的数字）的合计
            //是否超过该银行流水号的进账总金额（就是银行记录中的进账金额），如超过则提示金额超出银行进账金额，
            //如没有超过则写记录。  我们发现目前软件没有这个检验机制可以乱输入的，见下面的三笔。   
            string sql = string.Format("select ISNULL(SUM(FPTotal),0) AS SUMFPTotal FROM In_BankFlow where ReferenceNumber='{0}'", lblReferenceNumber.Text);
            if (base.Request["Id"] != null)
            {
                sql += " and id<>" + Request["Id"];
            }
            string mainSql = string.Format("select TradeAmount from BankFlow where TransactionReferenceNumber='{0}'", lblReferenceNumber.Text);
            decimal lastTotal = Convert.ToDecimal(DBHelp.ExeScalar(mainSql)) - Convert.ToDecimal(DBHelp.ExeScalar(sql));

            if (lastTotal < Convert.ToDecimal(txtFPTotal.Text))
            {
                strErr = string.Format("进账剩余总金额：{0}，请重新填写", lastTotal);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    In_BankFlow model = this.inBankSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.lblNumber.Text = model.Number;
                    this.lblReferenceNumber.Text = model.ReferenceNumber;
                    this.ddlInType.Text = model.InType;
                    this.txtFPNo.Text = model.FPNo;
                    this.txtFPTotal.Text = model.FPTotal.ToString();
                    this.txtRemark.Text = model.Remark;

                    if (ddlInType.Text == "发票回款")
                    {

                        txtFPNo.Enabled = true;
                    }
                    else
                    {
                        txtFPNo.Enabled = false;
                    }
                }
                else
                {
                    lblReferenceNumber.Text = Request["TransactionReferenceNumber"].ToString();

                    this.btnUpdate.Visible = false;
                }
                BankFlowService bandFlowSer = new BankFlowService();
                var list = bandFlowSer.GetListArray(string.Format("TransactionReferenceNumber='{0}'", lblReferenceNumber.Text));
                if (list.Count > 0)
                {
                    lblOutPayerName.Text = list[0].OutPayerName;
                }
            }
        }

        protected void btnTotal_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtFPNo.Text.Trim().Length == 0)
            {
                strErr += "发票号码不能为空！\\n";
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return;
            }
            string sql = string.Format("select Total,GuestName FROM [KingdeeInvoice].[dbo].[Invoice] WHERE InvoiceNumber='{0}'", txtFPNo.Text.Trim());
            var total = DBHelp.getDataTable(sql);
            if (total != null && total.Rows.Count > 0)
            {
                txtInvoiceTotal.Text = total.Rows[0][0].ToString();
                txtGuestName.Text = total.Rows[0][1].ToString();
                txtFPNo.Enabled = false;
                var inList = inBankSer.GetListArray(string.Format("  FPNo='{0}' ", txtFPNo.Text.Trim()));
                gv_In.DataSource = inList;
                gv_In.DataBind();
            }
            else
            {
                txtInvoiceTotal.Text = "";
                txtGuestName.Text = "";
                gv_In.DataSource = new List<VAN_OA.Model.Fin.In_BankFlow>();
                gv_In.DataBind();
            }

        }

        protected void ddlInType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFPNo.Text = "";
            txtInvoiceTotal.Text = "";
            txtGuestName.Text = "";
            if (ddlInType.Text == "发票回款")
            {
               
                txtFPNo.Enabled = true;
            }
            else
            {
                txtFPNo.Enabled = false;
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }
    }
}
