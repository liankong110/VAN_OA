using System;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using System.Web.UI.WebControls;

namespace VAN_OA.JXC
{
    public partial class WFDeleteSupplierAdvancePayment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_SupplierAdvancePaymentService mainSer = new TB_SupplierAdvancePaymentService();
                List<TB_SupplierAdvancePayment> pp = new List<TB_SupplierAdvancePayment>();
                gvMain.DataSource = pp;
                gvMain.DataBind();


                TB_SupplierAdvancePaymentsService ordersSer = new TB_SupplierAdvancePaymentsService();
                List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                gvList.DataSource = orders;
                gvList.DataBind();


            }
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            if (CheckProNo(txtProNo.Text) == false)
            {
                return;
            }
            txtProNo.Text = txtProNo.Text.Trim();
            string deleteSql;
            var sql = string.Format("select Id from TB_SupplierAdvancePayment where ProNo='{0}'",
                txtProNo.Text.Trim());

            var tb = DBHelp.getDataTable(sql);
            if (tb.Rows.Count != 1)
            {
                ClientScript.RegisterStartupScript(GetType(), null, "<script>alert('信息不存在！');</script>");
                return;
            }

            var checksql = string.Format(@"select count(*) from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
where Status<>'不通过' and  CaiId in (select  caiIds from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
where status='通过' and TB_SupplierAdvancePayment.ProNo='{0}' )", txtProNo.Text.Trim());
            if (Convert.ToInt32(DBHelp.ExeScalar(checksql)) > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), null, "<script>alert('数据已经存在入库数据，或正在入库的单子，无法修改！');</script>");
                return;
            }
            deleteSql = string.Format("delete from TB_SupplierAdvancePayment where id={0};delete from TB_SupplierAdvancePayments where id={0};", tb.Rows[0][0]);


            var efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='供应商预付款单')", tb.Rows[0][0]);
            var DeleteAll = string.Format("delete from tb_EForms where e_Id={0};delete from tb_EForm where id={0};", DBHelp.ExeScalar(efromId));
            deleteSql = deleteSql + DeleteAll;
            DBHelp.ExeCommand(deleteSql);
            ClientScript.RegisterStartupScript(GetType(), null, "<script>alert('删除成功！');</script>");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckProNo(txtProNo.Text) == false)
            {
                return;
            }
            TB_SupplierAdvancePaymentService mainSer = new TB_SupplierAdvancePaymentService();
            List<TB_SupplierAdvancePayment> pp = mainSer.GetListArray(" proNo='" + txtProNo.Text.Trim() + "'");
            gvMain.DataSource = pp;
            gvMain.DataBind();

            if (pp.Count > 0)
            {
                TB_SupplierAdvancePaymentsService ordersSer = new TB_SupplierAdvancePaymentsService();
                List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" 1=1 and TB_SupplierAdvancePayments.id=" + pp[0].Id);
                gvList.DataSource = orders;
                gvList.DataBind();

            }
            else
            {
                List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                gvList.DataSource = orders;
                gvList.DataBind();
            }

        }


        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        SupplierToInvoiceView SumOrders = new SupplierToInvoiceView();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                SupplierToInvoiceView model = e.Row.DataItem as SupplierToInvoiceView;
                SumOrders.SupplierInvoiceTotal += model.SupplierInvoiceTotal;
                SumOrders.LastTotal += model.LastTotal;

            }

            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblSupplierInvoiceTotal") as Label, SumOrders.SupplierInvoiceTotal.ToString());//数量                
                setValue(e.Row.FindControl("lblLastTotal") as Label, SumOrders.LastTotal.ToString());//数量  
            }

        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }



        protected object getDatetime(object time)
        {
            if (time != null)
            {
                return Convert.ToDateTime(time).ToShortDateString();
            }
            return time;
        }
    }
}
