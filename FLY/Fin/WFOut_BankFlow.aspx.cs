using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VAN_OA.Dal.EFrom;
using VAN_OA.Dal.Fin;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.Fin;
using VAN_OA.Model.ReportForms;



namespace VAN_OA.Fin
{
    public partial class WFOut_BankFlow : BasePage
    {

        private Out_BankFlowService outBankSer = new Out_BankFlowService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
               //     string sqlCheck = string.Format("select count(*) from Out_BankFlow where ReferenceNumber='{0}' and ProNo='{1}'",
               //lblReferenceNumber.Text, txtProNo.Text);
               //     if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
               //     {
               //         base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('流水号[{0}]，单据号[{1}]，已经存在！');</script>", lblReferenceNumber.Text, txtProNo.Text));
               //         return;
               //     }
                    Out_BankFlow model = getModel();
                    if (this.outBankSer.Add(model) > 0)
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
            this.txtProNo.Text = "";
            this.txtOutTotal.Text = "";
            this.txtRemark.Text = "";
            txtTotal.Text = "";
            txtName.Text = "";
           
        }


        public Out_BankFlow getModel()
        {
            string Number = this.lblNumber.Text;
            string ReferenceNumber = this.lblReferenceNumber.Text;
            string outType = this.ddlOutType.Text;
            string ProNo = this.txtProNo.Text.Trim();
            decimal OutTotal = decimal.Parse(this.txtOutTotal.Text);
            string Remark = this.txtRemark.Text;

            Out_BankFlow model = new Out_BankFlow();
            model.Number = Number;
            model.ReferenceNumber = ReferenceNumber;
            model.OutType = outType;
            model.ProNo = ProNo;
            model.OutTotal = OutTotal;
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
            //        string sqlCheck = string.Format("select count(*) from Out_BankFlow where ReferenceNumber='{0}' and ProNo='{1}' AND ID<>{2}",
            //lblReferenceNumber.Text, txtProNo.Text, Request["Id"]);
            //        if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
            //        {
            //            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('流水号[{0}]，单据号[{1}]，已经存在！');</script>", lblReferenceNumber.Text, txtProNo.Text));
            //            return;
            //        }
                    Out_BankFlow model = getModel();
                    if (this.outBankSer.Update(model))
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
            if (this.txtProNo.Text.Trim().Length == 0)
            {
                strErr += "单据号不能为空！\\n";
            }
            if (this.txtOutTotal.Text.Trim().Length == 0)
            {
                strErr += "出账金额不能为空！\\n";
            }
            if (CommHelp.VerifesToNum(this.txtOutTotal.Text.Trim()) == false)
            {
                strErr += "出账金额格式错误！\\n";
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }

            if (ddlOutType.Text == "支付单" || ddlOutType.Text == "预付款单" || ddlOutType.Text == "申请请款单" || ddlOutType.Text == "预期报销单")
            {
                if (string.IsNullOrEmpty(txtTotal.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", "请点击获取单据金额"));
                    return false;
                }
                //当下拉框选择 预付款单和支付单 时,点击获取样金额 后，单据的抬头必须严格和收款人名称一（除了单据抬头是淘宝，本部门，本部门（含税），淘宝(含1.33税) ，
                //京东 ，京东商城 可以不一样） ，修改或添加 才能保存数据，否则提示预付款单和支付单 抬头必须一致
                if (ddlOutType.Text == "支付单" || ddlOutType.Text == "预付款单")
                {
                    //中文括号转为英文
                    string name = txtName.Text.Trim().Replace("（", "(").Replace("）", ")");
                    if (lblInPayeeName.Text.Replace("（", "(").Replace("）", ")") != name && (name!= "淘宝"&& name != "本部门" &&name != "本部门（含税）" && name != "淘宝(含1.33税)" 
                        && name != "上海圆迈贸易有限公司"))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", "预付款单和支付单 抬头必须一致"));
                        return false;
                    }
                }
                decimal total = Convert.ToDecimal(txtTotal.Text);

                decimal outTotal = Convert.ToDecimal(txtOutTotal.Text);

                string sqlsumTotal = string.Format("select isnull(sum(OutTotal),0) from Out_BankFlow where ProNo='{0}' and  OutType='{1}'",
                    txtProNo.Text.Trim(),ddlOutType.Text);
                if (base.Request["Id"] != null)
                {
                    sqlsumTotal += " and id<>" + Request["Id"];
                }
                var sumTotal = Convert.ToDecimal(DBHelp.ExeScalar(sqlsumTotal));

                if (sumTotal+ outTotal > total)
                {
                    strErr = "该单据号剩余金额" + (total- sumTotal) + " ！\\n";
                }
                if (strErr != "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                    return false;
                }
            }

            string sql = string.Format("select ISNULL(SUM(OutTotal),0) AS SUMOutTotal FROM Out_BankFlow  where ReferenceNumber='{0}'", lblReferenceNumber.Text);
            if (base.Request["Id"] != null)
            {
                sql += " and id<>" + Request["Id"];
            }
            string mainSql = string.Format("select TradeAmount from BankFlow where TransactionReferenceNumber='{0}'", lblReferenceNumber.Text);
            decimal lastTotal = System.Math.Abs(Convert.ToDecimal(DBHelp.ExeScalar(mainSql))) - Convert.ToDecimal(DBHelp.ExeScalar(sql));

            if (lastTotal < Convert.ToDecimal(txtOutTotal.Text))
            {
                strErr = string.Format("出账剩余总金额：{0}，请重新填写", lastTotal);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }
            if (txtProNo.Text != "")
            {
                txtProNo.Text = txtProNo.Text.Trim();
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return false;
                }
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
                    Out_BankFlow model = this.outBankSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.lblNumber.Text = model.Number;
                    this.lblReferenceNumber.Text = model.ReferenceNumber;
                    this.ddlOutType.Text = model.OutType;
                    this.txtProNo.Text = model.ProNo;
                    this.txtOutTotal.Text = model.OutTotal.ToString();
                    this.txtRemark.Text = model.Remark;
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
                    lblInPayeeName.Text = list[0].InPayeeName;
                    lblTradeAmount.Text =System.Math.Abs(list[0].TradeAmount).ToString();
                    lblTime.Text = list[0].TransactionDate.ToString();

                    string sql = string.Format("select ISNULL(SUM(OutTotal),0) AS SUMOutTotal FROM Out_BankFlow  where ReferenceNumber='{0}'", lblReferenceNumber.Text);
                    //if (base.Request["Id"] != null)
                    //{
                    //    sql += " and id<>" + Request["Id"];
                    //}
                    var total= Convert.ToDecimal(DBHelp.ExeScalar(sql));
                    lblLastTotal.Text= (System.Math.Abs(list[0].TradeAmount)- total).ToString();

                    var outList = outBankSer.GetListArray(string.Format("  ReferenceNumber='{0}'", lblReferenceNumber.Text));
                    var time = Convert.ToDateTime(lblTime.Text);
                    foreach (var m in outList)
                    {
                        m.Time = time;
                    }

                    gvLiuShui.DataSource = outList;
                    gvLiuShui.DataBind();
                }

            }
        }

        protected void btnTotal_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtProNo.Text.Trim().Length == 0)
            {
                strErr += "单据号不能为空！\\n";
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return;
            }
            string total = "";
            string name = "";
            string sql = "";
            if (ddlOutType.Text == "支付单")
            {
                sql = string.Format(@"select SUM(TB_SupplierInvoices.SupplierInvoiceTotal),SupplierName from TB_SupplierInvoice  left join TB_SupplierInfo
on TB_SupplierInfo.SupplieSimpeName = TB_SupplierInvoice.LastSupplier  and TB_SupplierInfo.Status='通过'
LEFT JOIN TB_SupplierInvoices on TB_SupplierInvoices.Id=TB_SupplierInvoice.Id
where TB_SupplierInvoice.ProNo ='{0}'
group BY SupplierName,TB_SupplierInvoice.ProNo", txtProNo.Text.Trim());
               
            }
            else if (ddlOutType.Text == "预付款单")
            {
                sql = string.Format(@"select TOP 1 SumActPay,SupplierName from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName = CAI_POCai.lastSupplier and TB_SupplierInfo.Status='通过'
where TB_SupplierAdvancePayment.ProNo='{0}'", txtProNo.Text.Trim());
            }
            else if (ddlOutType.Text == "申请请款单")
            {
                sql = string.Format(@"select ISNULL(total,0)+ISNULL(Hui,0)+ISNULL(Ren,0)+ISNULL(XingCai,0)as Total,GuestName FROM tb_FundsUse
where ProNo='{0}'", txtProNo.Text.Trim());
            }
            else if (ddlOutType.Text == "预期报销单")
            {
                Tb_DispatchListService dispatchListSer = new Tb_DispatchListService();
                var list=dispatchListSer.GetListArray(string.Format("CardNo='{0}'", txtProNo.Text.Trim()));
                if (list.Count == 1)
                {
                    var model = list[0];
                    total = model.Total.ToString();
                    name = model.GuestName;
                }
            }
            if (!string.IsNullOrEmpty(sql))
            {
                var result = DBHelp.getDataTable(sql);
                if (result != null && result.Rows.Count > 0)
                {
                    total = result.Rows[0][0].ToString();
                    name = result.Rows[0][1].ToString();
                    txtProNo.Enabled = false;
                }
            }

            txtTotal.Text = total;
            txtName.Text = name;

            var outList = outBankSer.GetListArray(string.Format("  ProNo='{0}' and OutType='{1}'", txtProNo.Text, ddlOutType.Text));
            var time = Convert.ToDateTime(lblTime.Text);
            foreach (var m in outList)
            {
                m.Time = time;
            }
            gv_Out.DataSource = outList;
            gv_Out.DataBind();

        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void ddlOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlOutType.Text == "支付单" || ddlOutType.Text == "预付款单" || ddlOutType.Text == "申请请款单" || ddlOutType.Text == "预期报销单")
            //{
            //    txtProNo.Enabled = true;
            //}
            //else
            //{
            //    txtProNo.Enabled = false;
            //}
            txtProNo.Enabled = true;
            txtProNo.Text = "";
            txtTotal.Text = "";
            txtName.Text = "";
        }
    }
}
