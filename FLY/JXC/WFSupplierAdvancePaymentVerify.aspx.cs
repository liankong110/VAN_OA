using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace VAN_OA.JXC
{
    public partial class WFSupplierAdvancePaymentVerify : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {

        }

        public bool FormCheck()
        {
            #region 设置自己要判断的信息

            if (txtRuTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写日期！');</script>");
                txtRuTime.Focus();
                return false;
            }
            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写日期格式有误！');</script>");
                txtRuTime.Focus();
                return false;
            }
            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }
            #endregion

            if (Request["allE_id"] == null)
            {
                List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
                if (POOrders == null || POOrders.Count <= 0 || POOrders.FindAll(t => t.IfCheck).Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }

                if (Request["ids"] == null)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明细！');</script>");
                    return false;
                }
                //判断改供应商是否有在支付中的单子
                if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(POOrders[0].GuestName, 2))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商【支付单】有在执行中的【抵扣支付单】，请重新选择');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + POOrders[0].GuestName + "'</script>");

                    return false;
                }
                //--在创建/编辑 预付单时 判断是否已经有入库记录(包含正在执行的单子)
                string checkSql = string.Format(@"select proNo,SupplierName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId 
where Status<>'不通过' and  CaiId in ({0})", Request["ids"]);//--增加采购订单的ID ( and CaiId=?)

                var errorText = new StringBuilder();
                DataTable dt = DBHelp.getDataTable(checkSql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2}\\n", dr["proNo"], dr["SupplierName"], dr["GoodNo"] + @"\" + dr["GoodName"] + @"\" + dr["GoodTypeSmName"] + @"\" + dr["GoodSpec"]);
                    }
                    errorText.Append("数据已经存在入库数据，或正在入库的单子！");
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + errorText.ToString() + "');</script>");
                    return false;
                }

                //--获取采购单一共开了多少预付款单
                // LastTruePrice=lastPrice 修改
                checkSql = string.Format(@"select CAI_POCai.ids,Num,LastTruePrice as lastPrice ,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
from CAI_POCai
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status<>'不通过' and CaiIds in ({0})
group by CaiIds
)
as tb1 on CAI_POCai.IDs=tb1.CaiIds
where CAI_POOrder.status='通过' and CAI_POCai.ids in ({0}) ", Request["ids"]);

                dt = DBHelp.getDataTable(checkSql);
                if (dt.Rows.Count != Request["ids"].ToString().Split(',').Length)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('部分数据不存在，请重新选择数据提交');</script>");
                    return false;
                }


                //CAI_POCaiService POSer = new CAI_POCaiService();
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    var model = POOrders[i];
                    if (model.IfCheck == false)
                    {
                        continue;
                    }
                    TextBox txtSupplierInvoiceNum = gvList.Rows[i].FindControl("txtSupplierInvoiceNum") as TextBox;
                    if (txtSupplierInvoiceNum == null || txtSupplierInvoiceNum.Text == "")
                    {
                        try
                        {
                            model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付数量必须大于0');</script>");
                            return false;
                        }
                        catch (Exception)
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付数量格式有误');</script>");
                            return false;
                        }

                    }
                    else
                    {
                        model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
                        //TODO  需要判断支付数量《= 入库数量
                        if (model.SupplierInvoiceNum > model.GoodNum || model.SupplierInvoiceNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付数量必须大于0小于等于采购数量！');</script>"));
                            return false;
                        }
                    }

                    TextBox supplierFPNo = gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox;
                    if (supplierFPNo != null)
                    {
                        model.SupplierFPNo = supplierFPNo.Text;
                    }
                    TextBox txtSupplierInvoicePrice = gvList.Rows[i].FindControl("txtSupplierInvoicePrice") as TextBox;
                    if (txtSupplierInvoicePrice != null)
                    {
                        model.SupplierInvoicePrice = Convert.ToDecimal(txtSupplierInvoicePrice.Text);
                        if (model.SupplierInvoicePrice <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付单价必须大于0！');</script>"));
                            return false;
                        }
                        //model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.lastGoodNum;
                        model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.SupplierInvoiceNum;

                        if (model.SupplierInvoiceTotal > model.LastTotal)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付金额必须小于等于金额！');</script>"));
                            return false;
                        }

                        bool isExist = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["ids"].ToString() == model.Ids.ToString())
                            {
                                isExist = true;
                                decimal resultTotal = Convert.ToDecimal(dr["lastPrice"]) * Convert.ToDecimal(dr["Num"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
                                if (model.SupplierInvoiceTotal > resultTotal)
                                {

                                    errorText = new StringBuilder();
                                    errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 剩余总金额为：{3}", model.ProNo, model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec, resultTotal);
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                        string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                                    return false;
                                }
                                break;
                            }
                        }
                        if (isExist == false)
                        {
                            errorText = new StringBuilder();
                            errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 数据不存在", model.ProNo, model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec);
                            base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                            return false;
                        }
                    }
                    TextBox supplierInvoiceDate = gvList.Rows[i].FindControl("txtSupplierInvoiceDate") as TextBox;
                    if (supplierInvoiceDate != null)
                    {
                        model.SupplierInvoiceDate = Convert.ToDateTime(supplierInvoiceDate.Text);
                    }
                }

                var fushuList = POOrders.FindAll(t => t.IfCheck == false);
                if (fushuList.Count > 0)
                {
                    var result = POOrders.Sum(t => t.SupplierInvoiceTotal);
                    if (result < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('本次付款总合计必须大于0！');</script>"));
                        return false;
                    }
                }

                bool resultFuShu = true;
                //检查负数合计 防止并发
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();
                    foreach (var m in POOrders)
                    {
                        if (m.IfCheck == false)
                        {
                            continue;
                        }
                        objCommand.CommandText = string.Format(@"select Sum(SupplierInvoiceTotal) as  FuShuTotal from TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过' and  SupplierInvoiceTotal<0 and RuIds={0} ", m.Ids);
                        decimal currentTotal = 0;
                        var obj = objCommand.ExecuteScalar();
                        if (obj != null && obj != DBNull.Value)
                        {
                            currentTotal = Convert.ToDecimal(obj);
                        }

                        if (currentTotal != m.FuShuTotal)
                        {
                            resultFuShu = false;
                            break;
                        }
                    }
                    conn.Close();
                }

                if (resultFuShu == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据发送改变，请重新选择');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + POOrders[0].GuestName + "'</script>");

                    return false;
                }

                ViewState["Orders"] = POOrders;
            }
            else
            {

                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    //注意票据号需要校验：不能和所有以前输过的票据号重复！
                    string checkFPNo = "";
                    string checkFPNo1 = "";
                    if (txtFristFPNo.Enabled)
                    {
                        if (txtFristFPNo.Text.Trim() == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('原始票据号必填！');</script>");
                            return false;
                        }
                        string fristFpNo = txtFristFPNo.Text.Trim() == "" ? Guid.NewGuid().ToString() : txtFristFPNo.Text;

                        if (fristFpNo == "12345678")
                        {
                            fristFpNo = "-";
                        }
                      

                        checkFPNo = string.Format("select COUNT(*) from [dbo].[TB_SupplierAdvancePayment]  where (FristFPNo='{0}' or SecondFPNo='{0}') AND ID<>{1}  AND Status<>'不通过' and (FristFPNo<>'12345678' or SecondFPNo<>'12345678')", fristFpNo, Request["allE_id"]);
                        checkFPNo1 = string.Format("select COUNT(*) from [dbo].[TB_SupplierInvoice]  where (FristFPNo='{0}' or SecondFPNo='{0}') AND ID<>{1} AND Status<>'不通过' and CreateName<>'admin' and (FristFPNo<>'12345678' or SecondFPNo<>'12345678')", fristFpNo, Request["allE_id"]);

                    }
                    if (txtSecondFPNo.Enabled)
                    {
                        if (txtSecondFPNo.Text.Trim() == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('新票据号必填！');</script>");
                            return false;
                        }
                        if (txtSecondFPNo.Text.Trim() == txtFristFPNo.Text.Trim())
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('原票据号和新票据号不能一样！');</script>");
                            return false;
                        }
                        string fristFpNo = txtFristFPNo.Text.Trim() == "" ? Guid.NewGuid().ToString() : txtFristFPNo.Text;
                        string secondFpNo = txtSecondFPNo.Text.Trim() == "" ? Guid.NewGuid().ToString() : txtSecondFPNo.Text;

                        if (fristFpNo == "12345678")
                        {
                            fristFpNo = "-";
                        }
                        if (secondFpNo == "12345678")
                        {
                            secondFpNo = "-";
                        }
                        checkFPNo = string.Format("select COUNT(*) from [dbo].[TB_SupplierAdvancePayment]  where  (FristFPNo in ('{0}','{1}') or SecondFPNo in ('{0}','{1}')) AND ID<>{2} AND Status<>'不通过' and (FristFPNo<>'12345678' or SecondFPNo<>'12345678')", fristFpNo, secondFpNo, Request["allE_id"]);
                        checkFPNo1 = string.Format("select COUNT(*) from [dbo].[TB_SupplierInvoice]  where (FristFPNo in ('{0}','{1}') or SecondFPNo in ('{0}','{1}')) AND ID<>{2} AND Status<>'不通过' and CreateName<>'admin' and (FristFPNo<>'12345678' or SecondFPNo<>'12345678')", fristFpNo, secondFpNo, Request["allE_id"]);
                    }
                    if (checkFPNo != "")
                    {                         
                        if (Convert.ToInt32(DBHelp.ExeScalar(checkFPNo)) > 0 || Convert.ToInt32(DBHelp.ExeScalar(checkFPNo1)) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('票据号码有误（重复），请重新输入');</script>");
                            return false;
                        }                         
                    }
                    
                    List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
                    if (POOrders == null || POOrders.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                        return false;
                    }
                    var myids = new StringBuilder();
                    var myPayIds = new StringBuilder();
                    foreach (var m in POOrders)
                    {
                        if (m.IfCheck == false)
                        {
                            continue;
                        }
                        myids.AppendFormat("{0},", m.Ids);
                        myPayIds.AppendFormat("{0},", m.payIds);
                    }
                    var ids = myids.ToString().Substring(0, myids.ToString().Length - 1);
                    var payIds = myPayIds.ToString().Substring(0, myPayIds.ToString().Length - 1);

                    if (ids == "")
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明细！');</script>");
                        return false;
                    }
                    //--在创建/编辑 预付单时 判断是否已经有入库记录(包含正在执行的单子)
//                    string checkSql = string.Format(@"select proNo,SupplierName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
//left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId 
//where Status<>'不通过' and  CaiId in ({0})", ids);//--增加采购订单的ID ( and CaiId=?)

//                    var errorText = new StringBuilder();
//                    DataTable dt = DBHelp.getDataTable(checkSql);
//                    if (dt.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in dt.Rows)
//                        {
//                            errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2}\\n", dr["proNo"], dr["SupplierName"], dr["GoodNo"] + @"\" + dr["GoodName"] + @"\" + dr["GoodTypeSmName"] + @"\" + dr["GoodSpec"]);
//                        }
//                        errorText.Append("数据已经存在入库数据，或正在入库的单子！");
//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + errorText.ToString() + "');</script>");
//                        return false;
//                    }
                }
//                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && Request["ReAudit"] != null)
//                {
//                    List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
//                    if (POOrders == null || POOrders.Count <= 0)
//                    {
//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
//                        return false;
//                    }
//                    var myids = new StringBuilder();
//                    var myPayIds = new StringBuilder();
//                    foreach (var m in POOrders)
//                    {
//                        if (m.IfCheck == false)
//                        {
//                            continue;
//                        }
//                        myids.AppendFormat("{0},", m.Ids);
//                        myPayIds.AppendFormat("{0},", m.payIds);
//                    }
//                    var ids = myids.ToString().Substring(0, myids.ToString().Length - 1);
//                    var payIds = myPayIds.ToString().Substring(0, myPayIds.ToString().Length - 1);

//                    if (ids == "")
//                    {
//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明细！');</script>");
//                        return false;
//                    }
//                    //--在创建/编辑 预付单时 判断是否已经有入库记录(包含正在执行的单子)
//                    string checkSql = string.Format(@"select proNo,SupplierName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
//left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId 
//where Status<>'不通过' and  CaiId in ({0})", ids);//--增加采购订单的ID ( and CaiId=?)

//                    var errorText = new StringBuilder();
//                    DataTable dt = DBHelp.getDataTable(checkSql);
//                    if (dt.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in dt.Rows)
//                        {
//                            errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2}\\n", dr["proNo"], dr["SupplierName"], dr["GoodNo"] + @"\" + dr["GoodName"] + @"\" + dr["GoodTypeSmName"] + @"\" + dr["GoodSpec"]);
//                        }
//                        errorText.Append("数据已经存在入库数据，或正在入库的单子！");
//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + errorText.ToString() + "');</script>");
//                        return false;
//                    }

//                    //--获取采购单一共开了多少预付款单
//                    // LastTruePrice=lastPrice 修改
//                    checkSql = string.Format(@"select CAI_POCai.ids,Num,LastTruePrice as lastPrice ,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
//from CAI_POCai
//left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
//left join 
//(
//select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
//TB_SupplierAdvancePayments 
//left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
//where status<>'不通过' and CaiIds in ({0}) and ids not in ({1})
//group by CaiIds
//)
//as tb1 on CAI_POCai.IDs=tb1.CaiIds
//where CAI_POOrder.status='通过' and CAI_POCai.ids in ({0}) ", ids, payIds);

//                    dt = DBHelp.getDataTable(checkSql);
//                    if (dt.Rows.Count != ids.ToString().Split(',').Length)
//                    {
//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('部分数据不存在，请重新选择数据提交');</script>");
//                        return false;
//                    }

//                    //CAI_POCaiService POSer = new CAI_POCaiService();
//                    for (int i = 0; i < gvList.Rows.Count; i++)
//                    {
//                        var model = POOrders[i];
//                        if (model.IfCheck == false)
//                        {
//                            continue;
//                        }

//                        TextBox txtSupplierInvoiceNum = gvList.Rows[i].FindControl("txtSupplierInvoiceNum") as TextBox;
//                        if (txtSupplierInvoiceNum == null || txtSupplierInvoiceNum.Text == "")
//                        {
//                            try
//                            {
//                                model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付数量必须大于0');</script>");
//                                return false;
//                            }
//                            catch (Exception)
//                            {
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付数量格式有误');</script>");
//                                return false;
//                            }

//                        }
//                        else
//                        {
//                            model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
//                            //TODO  需要判断支付数量《= 入库数量
//                            if (model.SupplierInvoiceNum > model.GoodNum || model.SupplierInvoiceNum <= 0)
//                            {
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付数量必须大于0小于等于采购数量！');</script>"));
//                                return false;
//                            }
//                        }

//                        TextBox supplierFPNo = gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox;
//                        if (supplierFPNo != null)
//                        {
//                            model.SupplierFPNo = supplierFPNo.Text;
//                        }
//                        TextBox txtSupplierInvoicePrice = gvList.Rows[i].FindControl("txtSupplierInvoicePrice") as TextBox;
//                        if (txtSupplierInvoicePrice != null)
//                        {
//                            model.SupplierInvoicePrice = Convert.ToDecimal(txtSupplierInvoicePrice.Text);
//                            if (model.SupplierInvoicePrice <= 0)
//                            {
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付单价必须大于0！');</script>"));
//                                return false;
//                            }
//                            model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.SupplierInvoiceNum;
//                            if (model.SupplierInvoiceTotal > model.LastTotal)
//                            {
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('预付金额必须小于等于金额！');</script>"));
//                                return false;
//                            }

//                            bool isExist = false;
//                            foreach (DataRow dr in dt.Rows)
//                            {
//                                if (dr["ids"].ToString() == model.Ids.ToString())
//                                {
//                                    isExist = true;
//                                    decimal resultTotal = Convert.ToDecimal(dr["lastPrice"]) * Convert.ToDecimal(dr["Num"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
//                                    if (model.SupplierInvoiceTotal > resultTotal)
//                                    {

//                                        errorText = new StringBuilder();
//                                        errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 剩余总金额为：{3}", model.ProNo, model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec, resultTotal);
//                                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
//                                            string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
//                                        return false;
//                                    }
//                                    break;
//                                }
//                            }
//                            if (isExist == false)
//                            {
//                                errorText = new StringBuilder();
//                                errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 数据不存在", model.ProNo, model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec);
//                                base.ClientScript.RegisterStartupScript(base.GetType(), null,
//                                    string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
//                                return false;
//                            }
//                        }
//                        TextBox supplierInvoiceDate = gvList.Rows[i].FindControl("txtSupplierInvoiceDate") as TextBox;
//                        if (supplierInvoiceDate != null)
//                        {
//                            model.SupplierInvoiceDate = Convert.ToDateTime(supplierInvoiceDate.Text);
//                        }
//                    }
//                    ViewState["Orders"] = POOrders;
//                }



            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            txtRuTime.ReadOnly = true;
            Image1.Enabled = false;
            txtRemark.ReadOnly = true;
            if (ViewState["EformsCount"] != null && btnSub.Visible)
            {
                var count = Convert.ToInt32(ViewState["EformsCount"]) + 1;

                if (count >= 1 && (count - 1) % 6 != 0)
                {
                    if (count <= 6)
                    {
                        txtFristFPNo.Enabled = true;
                    }
                    else
                    {
                        VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                       
                        if (use != null&& VAN_OA.JXC.SysObj.NewShowAll_textName("支付单列表", Session["currentUserId"], "不能修改新票据号")==false)
                        {
                            txtSecondFPNo.Enabled = true;
                        }
                       
                    }
                }
            }
        }


        private void SetColumnsVis(bool isShow)
        {
            gvList.Columns[12].Visible = isShow;
            gvList.Columns[13].Visible = !isShow;

            gvList.Columns[15 + 2].Visible = isShow;
            gvList.Columns[16 + 2].Visible = isShow;
            gvList.Columns[17 + 2].Visible = isShow;

            gvList.Columns[18 + 2].Visible = !isShow;
            gvList.Columns[19 + 2].Visible = !isShow;
            gvList.Columns[20 + 2].Visible = !isShow;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              
                ViewState["Orders"] = null;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;


                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {


                        SetColumnsVis(false);
                        //权限1（销售）                       
                        txtRuTime.Text = DateTime.Now.ToString();

                        //加载初始数据
                        List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                        if (Request["ids"] != null)
                        {
                            var myids = new StringBuilder();
                            var myPayIds = new StringBuilder();
                            SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
                            orders = supplierToInvoiceSer.GetSupplierAdvancePaymentListToLoadVerify(string.Format(" CAI_POCai.ids in ({0}) and CAI_POOrder.Status='通过' ", Request["ids"]), Request["ids"]);
                            foreach (var m in orders)
                            {
                                //将支付数量=入库数量
                                m.SupplierInvoiceNum = (m.GoodNum ?? 0);

                                m.IfCheck = true;

                                m.SupplierInvoiceDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                myids.AppendFormat("{0},", m.Ids);
                                myPayIds.AppendFormat("{0},", m.payIds);
                            }

                            var ids = myids.ToString().Substring(0, myids.ToString().Length - 1);
                            var payIds = myPayIds.ToString().Substring(0, myPayIds.ToString().Length - 1);

                            //增加项目备注一列
                            var remarkList = supplierToInvoiceSer.GetPoRemarkByCaiIds(ids);
                            foreach (var remark in remarkList)
                            {
                                var model = orders.Find(t => t.Ids == remark.Ids);
                                model.PORemark = remark.PORemark;
                            }

                            var checkSql = string.Format(@"select CAI_POCai.ids,Num,LastTruePrice as lastPrice ,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
from CAI_POCai
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status<>'不通过' and CaiIds in ({0}) and ids not in ({1})
group by CaiIds
)
as tb1 on CAI_POCai.IDs=tb1.CaiIds
where CAI_POOrder.status='通过' and CAI_POCai.ids in ({0}) ", ids, payIds);

                            var dt = DBHelp.getDataTable(checkSql);

                            foreach (DataRow dr in dt.Rows)
                            {

                                var model = orders.Find(t => t.Ids.ToString() == dr["ids"].ToString());

                                if (model != null)
                                {
                                    decimal resultTotal = Convert.ToDecimal(dr["lastPrice"]) * Convert.ToDecimal(dr["Num"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);

                                    model.SupplierInvoicePrice = resultTotal / Convert.ToDecimal(dr["Num"]);

                                }
                            }
                        }


                        if (orders.Count > 0)
                        {
                            //根据供公司名称判断 是否 基于同一供应商 的负数支付单，且结清状态=2的记录存在，如果有，就分别罗列在 这条正数的记录下方
                            var ordersSer = new TB_SupplierInvoicesService();
                            var noCheckOrders =
                                ordersSer.GetListArray_ToAdd(string.Format(" Supplier='{0}' AND IsHeBing=1 and SupplierInvoiceTotal<0 and TB_SupplierInvoice.status='通过' ",
                                                                   orders[0].GuestName));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                            }

                            //增加项目备注一列
                            if (noCheckOrders.Count > 0)
                            {
                                List<PoRemarkInfo> list = new SupplierToInvoiceViewService().GetPoRemark(string.Join(",", noCheckOrders.Select(t => t.Ids.ToString()).ToArray()));
                                foreach (var remark in list)
                                {
                                    var model = noCheckOrders.Find(t => t.Ids == remark.Ids);

                                    model.PORemark = remark.PORemark;
                                }
                            }

                            orders.AddRange(noCheckOrders);

                        }
                        ViewState["Orders"] = orders;

                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人
                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人                            
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;


                        }
                    }
                    else if (Request["ReAudit"] != null)//重新提交编辑
                    {
                        SetColumnsVis(true);
                        ViewState["POOrdersIds"] = "";
                        //权限1（销售）

                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        if (eforms.Count > 0)
                        {
                            string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                            for (int i = 0; i < eforms.Count; i++)
                            {
                                string per = "";
                                if (eforms[i].consignor != null && eforms[i].consignor != 0)
                                {
                                    per = eforms[i].Consignor_Name + "(委托人：" + eforms[i].Audper_Name + ")";
                                }
                                else
                                {
                                    per = eforms[i].Audper_Name;
                                }
                                mess += string.Format("<tr><td align='center'>第{0}步</td><td>序号{0}：{1}</td><td><span style='color:red;'>{2}</span>[<span style='color:blue;'>{3} {4}</span>]<br/>意见：{5}</td></tr>",
                                    i + 1, eforms[i].RoleName
, per, eforms[i].resultState, eforms[i].doTime, eforms[i].idea);
                            }
                            mess += "</table>";
                            lblMess.Text = mess;
                        }

                        ViewState["EformsCount"] = eforms.Count;




                        #region  加载 请假单数据

                        TB_SupplierAdvancePaymentService mainSer = new TB_SupplierAdvancePaymentService();
                        TB_SupplierAdvancePayment pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        txtRuTime.Text = pp.CreteTime.ToString();
                        txtRemark.Text = pp.Remark;
                        txtFristFPNo.Text = pp.FristFPNo;
                        txtSecondFPNo.Text = pp.SecondFPNo;
                        TB_SupplierAdvancePaymentsService ordersSer = new TB_SupplierAdvancePaymentsService();
                        List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" 1=1 and TB_SupplierAdvancePayments.id=" + Request["allE_id"]);
                        foreach (var m in orders)
                        {
                            m.IfCheck = true;
                        }

                        if (orders.Count > 0)
                        {
                            //根据供公司名称判断 是否 基于同一供应商 的负数支付单，且结清状态=2的记录存在，如果有，就分别罗列在 这条正数的记录下方
                            var ordersSer1 = new TB_SupplierInvoicesService();

                            var noCheckOrders =
                               ordersSer1.GetListArray_ToAdd(
                                   string.Format(" exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds and SupplierAdvanceId={0})",
                                                 Request["allE_id"]));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                            }
                            orders.AddRange(noCheckOrders);
                        }
                        ViewState["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion

                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人                          
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;


                        }
                    }
                    else//单据审批
                    {
                        SetColumnsVis(true);
                        ViewState["POOrdersIds"] = "";


                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        if (eforms.Count > 0)
                        {
                            string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                            for (int i = 0; i < eforms.Count; i++)
                            {
                                string per = "";
                                if (eforms[i].consignor != null && eforms[i].consignor != 0)
                                {
                                    per = eforms[i].Consignor_Name + "(委托人：" + eforms[i].Audper_Name + ")";
                                }
                                else
                                {
                                    per = eforms[i].Audper_Name;
                                }
                                mess += string.Format("<tr><td align='center'>第{0}步</td><td>序号{0}：{1}</td><td><span style='color:red;'>{2}</span>[<span style='color:blue;'>{3} {4}</span>]<br/>意见：{5}</td></tr>",
                                    i + 1, eforms[i].RoleName
, per, eforms[i].resultState, eforms[i].doTime, eforms[i].idea);
                            }
                            mess += "</table>";
                            lblMess.Text = mess;
                        }

                        ViewState["EformsCount"] = eforms.Count;




                        #region  加载 请假单数据

                        TB_SupplierAdvancePaymentService mainSer = new TB_SupplierAdvancePaymentService();
                        TB_SupplierAdvancePayment pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtRuTime.Text = pp.CreteTime.ToString();
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        txtRemark.Text = pp.Remark;
                        txtFristFPNo.Text = pp.FristFPNo;
                        txtSecondFPNo.Text = pp.SecondFPNo;
                        TB_SupplierAdvancePaymentsService ordersSer = new TB_SupplierAdvancePaymentsService();
                        List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" 1=1 and TB_SupplierAdvancePayments.id=" + Request["allE_id"]);

                        foreach (var m in orders)
                        {
                            m.IfCheck = true;
                        }




                        if (orders.Count > 0)
                        {
                            //增加项目备注一列
                            var remarkList = new SupplierToInvoiceViewService().GetPoRemarkByCaiIds(string.Join(",", orders.Select(t => t.Ids.ToString()).ToArray()));
                            foreach (var remark in remarkList)
                            {
                                var model = orders.Find(t => t.Ids == remark.Ids);
                                model.PORemark = remark.PORemark;
                            }

                            TB_SupplierInvoicesService ordersSer1 = new TB_SupplierInvoicesService();
                            //根据供公司名称 来查看 是否要加载 有实际支付 为负数并且结清字段为0的 记录存在
                            var noCheckOrders =
                                ordersSer1.GetListArray_ToAdd(
                                    string.Format(" exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds and SupplierAdvanceId={0})",
                                                  Request["allE_id"]));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                            }
                            if (noCheckOrders.Count > 0)
                            {
                                List<PoRemarkInfo> list = new SupplierToInvoiceViewService().GetPoRemark(string.Join(",", noCheckOrders.Select(t => t.Ids.ToString()).ToArray()));
                                foreach (var remark in list)
                                {
                                    var model = noCheckOrders.Find(t => t.Ids == remark.Ids);

                                    model.PORemark = remark.PORemark;
                                }
                            }

                            orders.AddRange(noCheckOrders);
                        }

                        ViewState["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            btnSub.Visible = false;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                            lblResult.Visible = false;
                            lblYiJian.Visible = false;
                            ddlResult.Visible = false;
                            txtResultRemark.Visible = false;

                            setEnable(false);


                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;
                                }
                                else
                                {
                                    int ids = 0;

                                    List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);

                                    ViewState["ids"] = ids;
                                    if (roleUserList != null)
                                    {
                                        ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";

                                    }

                                }
                                setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {

                                    ViewState["ifConsignor"] = true;
                                    if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                    {
                                        lblPer.Visible = false;
                                        ddlPers.Visible = false;
                                    }
                                    else
                                    {
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                        ViewState["ids"] = ids;
                                        if (roleUserList != null)
                                        {
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";
                                        }

                                    }
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                }
                                else
                                {
                                    btnSub.Visible = false;
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;

                                    lblResult.Visible = false;
                                    lblYiJian.Visible = false;
                                    ddlResult.Visible = false;
                                    txtResultRemark.Visible = false;
                                    setEnable(false);
                                }
                            }

                        }
                    }
                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {

                    #region 获取单据基本信息

                    TB_SupplierAdvancePayment order = new TB_SupplierAdvancePayment();

                    int CreatePer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateName = txtName.Text;
                    order.CreteTime = Convert.ToDateTime(txtRuTime.Text);
                    List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
                    //foreach (var m in POOrders)
                    //{
                    //    if (m.IfCheck == false)
                    //    {
                    //        continue;
                    //    }
                    //    if (m.SupplierInvoicePrice <= 0 || m.SupplierInvoiceTotal <= 0)
                    //    {
                    //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('单价不能为负数！');</script>");
                    //        return;
                    //    }
                    //}
                    order.Remark = txtRemark.Text;
                    order.SumActPay = POOrders.Sum(t => t.SupplierInvoiceTotal);
                    order.FristFPNo = txtFristFPNo.Text;
                    order.SecondFPNo = txtSecondFPNo.Text;
                    #endregion
                    if (Request["allE_id"] == null)//单据增加+//再次编辑)
                    {

                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = CreatePer;
                        eform.appPer = userId;
                        eform.appTime = DateTime.Now;
                        eform.createPer = Convert.ToInt32(Session["currentUserId"].ToString());
                        eform.createTime = DateTime.Now;
                        eform.proId = Convert.ToInt32(Request["ProId"]);

                        if (ddlPers.Visible == false)
                        {
                            eform.state = "通过";
                            eform.toPer = 0;
                            eform.toProsId = 0;
                        }
                        else
                        {

                            eform.state = "执行中";
                            eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                            eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                        }


                        int MainId = 0;


                        TB_SupplierAdvancePaymentService POOrderSer = new TB_SupplierAdvancePaymentService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {
                            new TB_SupplierAdvancePaymentService().SetCaiPayStatus(POOrders);
                            if (ddlPers.Visible == false)
                            {
                                //POOrderSer.SellFPOrderBackUpdatePoStatus(txtPONo.Text);
                            }



                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");
                        }
                    }
                    else//审核
                    {

                        if (Request["ReAudit"] != null)
                        {
                            TB_SupplierAdvancePaymentService POSer = new TB_SupplierAdvancePaymentService();
                            var model = POSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                            //if (Session["currentUserId"].ToString() != model.CreateUserId.ToString())
                            //{

                            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                            //    return;
                            //}

                            if (model != null && model.Status == "通过")
                            {

                            }
                            else
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                                return;
                            }

                        }


                        #region 本单据的ID
                        order.Id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();


                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = CreatePer;
                        eform.appPer = userId;




                        tb_EFormService fromSer = new tb_EFormService();
                        if (ViewState["ifConsignor"] != null && Convert.ToBoolean(ViewState["ifConsignor"]) == true)
                        {
                            forms.audPer = fromSer.getCurrentAuPer(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                            forms.consignor = Convert.ToInt32(Session["currentUserId"]);
                        }
                        else
                        {
                            forms.audPer = Convert.ToInt32(Session["currentUserId"]);
                            forms.consignor = 0;
                        }
                        if (Request["ReAudit"] == null)
                        {

                            if (fromSer.ifAudiPerAndCon(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])) == false)
                            {
                                if (Session["backurl"] != null)
                                {
                                    base.Response.Redirect("~" + Session["backurl"]);
                                }
                                else
                                {
                                    base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                                }
                                return;
                            }
                        }
                        forms.doTime = DateTime.Now;
                        forms.e_Id = Convert.ToInt32(Request["EForm_Id"]); //fromSer.getCurrentid(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.idea = txtResultRemark.Text;
                        forms.prosIds = fromSer.getCurrenttoProsId(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.resultState = ddlResult.Text;
                        forms.RoleName = fromSer.getCurrenttoRoleName(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        if (Request["ReAudit"] != null)
                        {
                            forms.RoleName = "重新编辑";
                        }
                        if (ddlPers.Visible == false)//说明为最后一次审核
                        {
                            eform.state = ddlResult.Text;
                            eform.toPer = 0;
                            eform.toProsId = 0;

                        }
                        else
                        {
                            if (ddlResult.Text == "不通过")
                            {
                                eform.state = ddlResult.Text;
                                eform.toPer = 0;
                                eform.toProsId = 0;
                            }
                            else
                            {
                                eform.state = "执行中";
                                eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                                eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                            }
                        }
                        TB_SupplierAdvancePaymentService POOrderSer = new TB_SupplierAdvancePaymentService();
                        string IDS = ViewState["POOrdersIds"].ToString();


                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            new TB_SupplierAdvancePaymentService().SetCaiPayStatus(POOrders);
                            if (ddlPers.Visible == false)
                            {
                                //POOrderSer.SellFPOrderBackUpdatePoStatus(txtPONo.Text);
                            }

                            if (ddlPers.Visible == false && ddlResult.Text == "通过")
                            {
                                if (POOrders.FindAll(t => t.IfCheck == false).Count > 0)
                                {
                                    POOrderSer.SetActStatus(POOrders);
                                }

                            }
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

                        }
                    }
                }
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
                if (model.IfCheck == false)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;

                    var invoiceDate = e.Row.FindControl("txtSupplierInvoiceDate") as TextBox;
                    if (invoiceDate != null && invoiceDate.Visible)
                    {
                        invoiceDate.Enabled = false;
                    }
                    var txtSupplierInvoicePrice = e.Row.FindControl("txtSupplierInvoicePrice") as TextBox;
                    if (txtSupplierInvoicePrice != null && txtSupplierInvoicePrice.Visible)
                    {
                        txtSupplierInvoicePrice.Enabled = false;
                    }
                    var txtSupplierFPNo = e.Row.FindControl("txtSupplierFPNo") as TextBox;
                    if (txtSupplierFPNo != null && txtSupplierFPNo.Visible)
                    {
                        txtSupplierFPNo.Enabled = false;
                    }
                }
                else
                {
                    //if (model.IsHouTuiKui)
                    //{
                    //    var lblSupplierTuiGoodNum = e.Row.FindControl("lblSupplierTuiGoodNum") as Label;
                    //    lblSupplierTuiGoodNum.BackColor = System.Drawing.Color.Red;
                    //}
                }
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



        protected void btnReCala_Click(object sender, EventArgs e)
        {
            List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                //TextBox txtCheckPrice1 = gvList.Rows[i].FindControl("txtCheckPrice1") as TextBox;
                //if (txtCheckPrice1 != null)
                //{
                //    POOrders[i].GoodSellPrice = Convert.ToDecimal(txtCheckPrice1.Text);
                //}

                //TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                //if (txtNum != null)
                //{
                //    POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                //    POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                //    POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;

                //}

                //TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                //if (txtGoodRemark != null)
                //{
                //    POOrders[i].GoodRemark = txtGoodRemark.Text;
                //}
            }
            ViewState["Orders"] = POOrders;
            gvList.DataSource = POOrders;
            gvList.DataBind();
        }





    }
}
