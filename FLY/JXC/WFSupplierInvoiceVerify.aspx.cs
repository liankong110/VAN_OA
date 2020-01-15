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
    public partial class WFSupplierInvoiceVerify : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            string topOneFPNo = "";
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    topOneFPNo = (gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox).Text;
                }
                else
                {
                    if (cb1.Checked)
                    {
                        (gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox).Text = topOneFPNo;
                    }
                    else
                    {
                        (gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox).Text = "";
                    }
                }


            }
        }

        protected void cbAllDate_CheckedChanged(object sender, EventArgs e)
        {
            string topOneFPDate = "";
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                if (i == 0)
                {
                    topOneFPDate = (gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox).Text;
                }
                else
                {
                    if (cb1.Checked)
                    {
                        (gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox).Text = topOneFPDate;
                    }
                    else
                    {
                        (gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox).Text = "";
                    }
                }


            }
        }

        protected string GetDate(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
                }
                return "";
            }
            catch (Exception)
            {

                return "";
            }
        }

        protected string GetNum(object obj)
        {
            try
            {
                return string.Format("{0:n2}", obj);
            }
            catch (Exception)
            {

                return "";
            }
        }

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


                if (Session["SupplierInvoiceIds"] == null)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明细！');</script>");
                    return false;
                }

                var htSupplier = new Hashtable();
                foreach (var model in POOrders)
                {
                    string key = model.GuestName;
                    if (!htSupplier.Contains(key))
                    {
                        htSupplier.Add(key, null);

                        if (htSupplier.Keys.Count > 1)
                        {
                            ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('供应商必选完全一样！');</script>"));
                            return false;
                        }
                    }

                }

                //判断改供应商是否有在支付中的单子
                if (TB_SupplierInvoiceService.checkSupplierDoing(POOrders[0].GuestName))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请重新选择');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + POOrders[0].GuestName + "'</script>");

                    return false;
                }
                //判断改供应商是否有在支付中的单子
                if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(POOrders[0].GuestName, 1))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + POOrders[0].GuestName + "'</script>");

                    return false;
                }

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    var model = POOrders[i];
                    if (model.IfCheck == false)
                    {
                        continue;
                    }
                    TextBox supplierFPNo = gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox;

                    //在需要逻辑判断的情况先 才检查发票是否为空
                    if (supplierFPNo != null)
                    {
                        if (supplierFPNo.Text == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票号不能为空');</script>");
                            return false;
                        }
                        model.SupplierFPNo = supplierFPNo.Text;
                    }

                    TextBox supplierFpDate = gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox;

                    //在需要逻辑判断的情况先 才检查发票是否为空
                    if (supplierFpDate != null)
                    {
                        if (supplierFpDate.Text == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期不能为空');</script>");
                            return false;
                        }
                        try
                        {
                            Convert.ToDateTime(supplierFpDate.Text);
                        }
                        catch (Exception)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有误');</script>");
                            return false;
                        }
                        model.SupplierFpDate = Convert.ToDateTime(supplierFpDate.Text);
                    }

                    TextBox supplierInvoiceDate = gvList.Rows[i].FindControl("txtSupplierInvoiceDate") as TextBox;
                    if (supplierInvoiceDate == null || supplierInvoiceDate.Text == "")
                    {
                        try
                        {
                            Convert.ToDateTime(supplierInvoiceDate.Text);
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间不能为空');</script>");
                            return false;
                        }
                        catch (Exception)
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有误');</script>");
                            return false;
                        }

                    }
                    TextBox txtSupplierInvoicePrice = gvList.Rows[i].FindControl("txtSupplierInvoicePrice") as TextBox;
                    if (txtSupplierInvoicePrice == null || txtSupplierInvoicePrice.Text == "")
                    {
                        try
                        {
                            Convert.ToDecimal(txtSupplierInvoicePrice.Text);
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('价格必须大于0');</script>");
                            return false;
                        }
                        catch (Exception)
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('价格格式有误');</script>");
                            return false;
                        }

                    }
                    TextBox txtSupplierInvoiceNum = gvList.Rows[i].FindControl("txtSupplierInvoiceNum") as TextBox;
                    if (txtSupplierInvoiceNum == null || txtSupplierInvoiceNum.Text == "")
                    {
                        try
                        {
                            Convert.ToDecimal(txtSupplierInvoiceNum.Text);
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('支付数量必须大于0');</script>");
                            return false;
                        }
                        catch (Exception)
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('支付数量格式有误');</script>");
                            return false;
                        }

                    }

                }

                //--获取采购单一共开了多少预付款单
                //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额<0
                //CaiLastTruePrice = GoodPrice
                //                string checkSql = string.Format(@"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice as GoodPrice,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal,
                //isnull(FuShuSupplierInvoiceTotal,0) as FuShuSupplierInvoiceTotal
                //from CAI_OrderInHouses 
                //left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
                //left join 
                //(
                //select RuIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
                //TB_SupplierInvoices 
                //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                //where status<>'不通过' and SupplierInvoiceTotal>0 and RuIds in ({0})
                //group by RuIds
                //)
                //as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds
                //left join 
                //(
                //select RuIds,Sum(SupplierInvoiceTotal) as  FuShuSupplierInvoiceTotal from 
                //TB_SupplierInvoices 
                //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                //where status='通过' and SupplierInvoiceTotal<0 and RuIds in ({0})
                //group by RuIds
                //)
                //as tb2 on CAI_OrderInHouses.IDs=tb2.RuIds
                //where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0})  ", Request["ids"]);

                string checkSql = string.Format(@"select CAI_POCai.IDS as CaiIds, CAI_OrderInHouses.ids,CAI_OrderInHouses.GoodNum as GoodNum,CAI_OrderInHouses.CaiLastTruePrice as GoodPrice ,
isnull(HadSupplierInvoiceTotal,0) as allSupplierInvoiceTotal,
 isnull(HadFuShuTotal,0) as FuShuSupplierInvoiceTotal from
 CAI_OrderInHouse  
left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join 
(
select TB_SupplierInvoices.RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id

where ((status<>'不通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0
group by TB_SupplierInvoices.RuIds
)
as tb2 on tb2.RuIds=CAI_OrderInHouses.ids
left join
(
select  CAI_OrderInHouses.Ids,Sum(CAI_OrderOutHouses.GoodNum*CAI_OrderInHouses.CaiLastTruePrice) as  HadFuShuTotal from 
CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=CAI_OrderOutHouses.OrderCheckIds
where CAI_OrderOutHouse.status='通过' 
group by CAI_OrderInHouses.Ids
)
as temp3 on temp3.Ids=CAI_OrderInHouses.ids
where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0})  ", Session["SupplierInvoiceIds"]);

                var dt = DBHelp.getDataTable(checkSql);
                if (dt.Rows.Count != Session["SupplierInvoiceIds"].ToString().Split(',').Length)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('部分数据不存在，请重新选择数据提交');</script>");
                    return false;
                }
                Hashtable htHadTotal = new Hashtable();
                var errorText = new StringBuilder();
                CAI_POCaiService POSer = new CAI_POCaiService();
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    //var CBIfCheck = gvList.Rows[i].FindControl("CBIfCheck") as CheckBox;
                    //if(CBIfCheck==null||CBIfCheck.Checked==false)
                    //{
                    //    continue;
                    //}
                    var model = POOrders[i];
                    if (model.IfCheck == false)
                    {
                        continue;
                    }
                    TextBox supplierFPNo = gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox;
                    if (supplierFPNo != null)
                    {
                        if (supplierFPNo.Text == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票号不能为空');</script>");
                            return false;
                        }
                        model.SupplierFPNo = supplierFPNo.Text;
                    }

                    TextBox supplierFpDate = gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox;

                    //在需要逻辑判断的情况先 才检查发票是否为空
                    if (supplierFpDate != null)
                    {
                        if (supplierFpDate.Text == "")
                        {
                            try
                            {
                                Convert.ToDateTime(supplierFpDate.Text);
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期不能为空');</script>");
                                return false;
                            }
                            catch (Exception)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有误');</script>");
                                return false;
                            }
                        }
                        model.SupplierFpDate = Convert.ToDateTime(supplierFpDate.Text);
                    }
                    //判断当前的入库信息有没有退货单执行 如果有不能做支付单
                    string CAI_OrderOutHouseSql = string.Format(@"select count(*) from CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id 
where CAI_OrderOutHouse.status='执行中' and 
 CAI_OrderOutHouses.GooId={0} and CAI_OrderOutHouse.Supplier='{1}' ", model.GoodId, model.GuestName);
                    if (Convert.ToInt32(DBHelp.ExeScalar(CAI_OrderOutHouseSql)) > 0)
                    {
                        errorText = new StringBuilder();
                        errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 存在正在执行的【采购退货信息】，不能提交本次支付单！", model.ProNo, model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec);
                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                            string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                        return false;
                    }

                    TextBox txtSupplierInvoicePrice = gvList.Rows[i].FindControl("txtSupplierInvoicePrice") as TextBox;

                    //获取支付数量
                    TextBox txtSupplierInvoiceNum = gvList.Rows[i].FindControl("txtSupplierInvoiceNum") as TextBox;
                    model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
                    //TODO  需要判断支付数量《= 入库数量
                    if (model.SupplierInvoiceNum > model.GoodNum || model.SupplierInvoiceNum <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款数量必须大于0小于等于入库数量！');</script>"));
                        return false;
                    }
                    if (txtSupplierInvoicePrice != null)
                    {
                        model.SupplierInvoicePrice = Convert.ToDecimal(txtSupplierInvoicePrice.Text);
                        if (model.SupplierInvoicePrice <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款单价必须大于0！');</script>"));
                            return false;
                        }
                        //强调 支付金额 按照入库数量*金额来计算 不要扣减 退款数量
                        //model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.lastGoodNum;
                        if (model.SupplierInvoiceNum != null)
                        {
                            model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.SupplierInvoiceNum;
                            if (model.SupplierInvoiceTotal > model.LastTotal)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款金额必须小于等于金额！');</script>"));
                                return false;
                            }
                        }

                        bool isExist = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["ids"].ToString() == model.Ids.ToString())
                            {
                                isExist = true;
                                //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额<0
                                decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]) -
                                               Convert.ToDecimal(dr["FuShuSupplierInvoiceTotal"]);


                                decimal hadTotal = 0;
                                //if (htHadTotal.ContainsKey(dr["CaiIds"].ToString()))
                                //{
                                //    hadTotal = Convert.ToDecimal(htHadTotal[dr["CaiIds"].ToString()]);
                                //}
                                //3.剩余金额=入库数量*实采单价-此次入库已经付款金额 （ 就是2的描述）

                                if (model.SupplierInvoiceTotal + hadTotal > resultTotal)
                                {
                                    errorText = new StringBuilder();
                                    errorText.AppendFormat(" {0}供应商:{1},商品:{2} 本次最大支付金额：{3}", "", model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec, resultTotal);
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                        string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                                    return false;
                                }
                                //if (htHadTotal.ContainsKey(dr["CaiIds"].ToString()))
                                //{
                                //    htHadTotal[dr["CaiIds"].ToString()]=model.SupplierInvoiceTotal+hadTotal;
                                //}
                                //else
                                //{
                                //    htHadTotal.Add(dr["CaiIds"].ToString(), model.SupplierInvoiceTotal);
                                //}
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

                    model.ActPay = model.FuShuTotal + model.SupplierInvoiceTotal;
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
                if (Request["ReAudit"] != null)//重新提交编辑
                {
                    List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
                    //判断改供应商是否有在支付中的单子
                    if (TB_SupplierInvoiceService.checkSupplierDoing(POOrders[0].GuestName))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请排队等候');</script>");
                        return false;
                    }

                    //判断改供应商是否有在支付中的单子
                    if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(POOrders[0].GuestName, 1))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + POOrders[0].GuestName + "'</script>");

                        return false;
                    }
                }
                //供应商付款单（预付单转支付单）检验
                //凡是含税的项，发票一栏 是个 输入框，不含税就没有输入框。

                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过"&& Request["ProId"] != "33")
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
                }
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && ddlPers.Visible && Convert.ToInt32(Request["ProId"]) == 33 && gvList.Columns[22].Visible)//说明为供应商付款单（预付单转支付单）
                {
                    for (int i = 0; i < this.gvList.Rows.Count; i++)
                    {
                        TextBox supplierFPNo = (gvList.Rows[i].FindControl("txtSupplierFPNo")) as TextBox;
                        //var CBIfCheck = gvList.Rows[i].FindControl("CBIfCheck") as CheckBox;
                        //在需要逻辑判断的情况先 才检查发票是否为空
                        if (supplierFPNo.Enabled && supplierFPNo.Text == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加发票号！');</script>");
                            return false;
                        }

                        TextBox supplierFpDate = gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox;

                        //在需要逻辑判断的情况先 才检查发票是否为空
                        if (supplierFpDate != null)
                        {
                            if (supplierFpDate.Text == "")
                            {
                                try
                                {
                                    Convert.ToDateTime(supplierFpDate.Text);
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期不能为空');</script>");
                                    return false;
                                }
                                catch (Exception)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有误');</script>");
                                    return false;
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && Request["ReAudit"] == null)
                //    { 

                //    }
                //}

                if (
                    (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && ddlPers.Visible && Convert.ToInt32(Request["ProId"]) == 33 && gvList.Columns[24].Visible)
                    || (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && Request["ReAudit"] != null))//说明为供应商付款单（预付单转支付单）
                //if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过" && ddlPers.Visible)
                {
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

                    //--获取采购单一共开了多少预付款单
                    //CaiLastTruePrice = GoodPrice
                    //                    string checkSql = string.Format(@"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice as GoodPrice,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
                    //from CAI_OrderInHouses 
                    //left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
                    //left join 
                    //(
                    //select RuIds,Sum(ABS(SupplierInvoiceTotal)) as  SupplierInvoiceTotal from 
                    //TB_SupplierInvoices 
                    //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                    //where status<>'不通过' and RuIds in ({0}) and ids not in ({1})
                    //group by RuIds
                    //)
                    //as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds
                    //where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0})  ", ids, payIds);
                    string checkSql = string.Format(@"select CAI_POCai.IDS as CaiIds, CAI_OrderInHouses.ids,CAI_OrderInHouses.GoodNum as GoodNum,CAI_OrderInHouses.CaiLastTruePrice as GoodPrice ,
isnull(HadSupplierInvoiceTotal,0) as allSupplierInvoiceTotal,
 isnull(HadFuShuTotal,0) as FuShuSupplierInvoiceTotal from
 CAI_OrderInHouse  
left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join 
(
select TB_SupplierInvoices.RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status<>'不通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0 and TB_SupplierInvoices.ids not in ({1})
group by TB_SupplierInvoices.RuIds
)
as tb2 on tb2.RuIds=CAI_OrderInHouses.ids
left join
(
select CAI_OrderInHouses.Ids,Sum(CAI_OrderOutHouses.GoodNum*CAI_OrderInHouses.CaiLastTruePrice) as  HadFuShuTotal from 
CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=CAI_OrderOutHouses.OrderCheckIds
where CAI_OrderOutHouse.status='通过' 
group by CAI_OrderInHouses.Ids
)
as temp3 on temp3.Ids=CAI_OrderInHouses.ids
where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0})  ", ids, payIds);
                    var dt = DBHelp.getDataTable(checkSql);
                    if (dt.Rows.Count != ids.ToString().Split(',').Length)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('部分数据不存在，请重新选择数据提交');</script>");
                        return false;
                    }
                    Hashtable htHadTotal = new Hashtable();
                    var errorText = new StringBuilder();
                    CAI_POCaiService POSer = new CAI_POCaiService();
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        var model = POOrders[i];
                        if (model.IfCheck == false)
                        {
                            continue;
                        }
                        TextBox supplierFPNo = gvList.Rows[i].FindControl("txtSupplierFPNo") as TextBox;
                        if (supplierFPNo != null)
                        {
                            model.SupplierFPNo = supplierFPNo.Text;
                        }
                        TextBox supplierFpDate = gvList.Rows[i].FindControl("txtSupplierFpDate") as TextBox;

                        //在需要逻辑判断的情况先 才检查发票是否为空
                        if (supplierFpDate != null)
                        {
                            if (supplierFpDate.Text == "")
                            {
                                try
                                {
                                    Convert.ToDateTime(supplierFpDate.Text);
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期不能为空');</script>");
                                    return false;
                                }
                                catch (Exception)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式有误');</script>");
                                    return false;
                                }
                            }
                            model.SupplierFpDate = Convert.ToDateTime(supplierFpDate.Text);
                        }
                        TextBox txtSupplierInvoicePrice = gvList.Rows[i].FindControl("txtSupplierInvoicePrice") as TextBox;

                        //获取支付数量
                        TextBox txtSupplierInvoiceNum = gvList.Rows[i].FindControl("txtSupplierInvoiceNum") as TextBox;
                        if (txtSupplierInvoiceNum != null && txtSupplierInvoiceNum.Text != "")
                        {
                            model.SupplierInvoiceNum = Convert.ToDecimal(txtSupplierInvoiceNum.Text);
                            //TODO  需要判断支付数量《= 入库数量
                            if (model.SupplierInvoiceNum > model.GoodNum || model.SupplierInvoiceNum <= 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款数量必须大于0小于等于入库数量！');</script>"));
                                return false;
                            }
                        }

                        if (txtSupplierInvoicePrice != null && txtSupplierInvoicePrice.Text != "")
                        {
                            model.SupplierInvoicePrice = Convert.ToDecimal(txtSupplierInvoicePrice.Text);
                            if (model.SupplierInvoicePrice <= 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款单价必须大于0！');</script>"));
                                return false;
                            }
                            if (model.SupplierInvoiceNum != null)
                                model.SupplierInvoiceTotal = model.SupplierInvoicePrice * model.SupplierInvoiceNum;
                            if (model.SupplierInvoiceTotal > model.LastTotal)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('付款金额必须小于等于金额！');</script>"));
                                return false;
                            }

                            bool isExist = false;
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["ids"].ToString() == model.Ids.ToString())
                                {
                                    isExist = true;
                                    decimal hadTotal = 0;
                                    if (htHadTotal.ContainsKey(dr["CaiIds"].ToString()))
                                    {
                                        hadTotal = Convert.ToDecimal(htHadTotal[dr["CaiIds"].ToString()]);
                                    }

                                    //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额<0
                                    //decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
                                    decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]) -
                                                  Convert.ToDecimal(dr["FuShuSupplierInvoiceTotal"]);
                                    if (model.SupplierInvoiceTotal + hadTotal > resultTotal)
                                    {
                                        errorText = new StringBuilder();
                                        errorText.AppendFormat("{0}供应商:{1},商品:{2} 本次最大支付金额：{3}", "", model.GuestName, model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec, resultTotal);
                                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                            string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                                        return false;
                                    }
                                    if (htHadTotal.ContainsKey(dr["CaiIds"].ToString()))
                                    {
                                        htHadTotal[dr["CaiIds"].ToString()] = model.SupplierInvoiceTotal + hadTotal;
                                    }
                                    else
                                    {
                                        htHadTotal.Add(dr["CaiIds"].ToString(), model.SupplierInvoiceTotal);
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
                        if (supplierInvoiceDate != null && supplierInvoiceDate.Text != "")
                        {
                            model.SupplierInvoiceDate = Convert.ToDateTime(supplierInvoiceDate.Text);
                        }
                    }
                    ViewState["Orders"] = POOrders;
                }

            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            txtRuTime.ReadOnly = true;
            Image1.Enabled = false;
            txtRemark.ReadOnly = true;

            if (ViewState["EformsCount"] != null&& btnSub.Visible)
            {
                var count = Convert.ToInt32(ViewState["EformsCount"]) + 1;
                //供应商付款单
                if (Request["ProId"] == "31")
                {
                    if (count >= 1 && (count - 1) % 6 != 0)
                    {
                        if (count <= 6)
                        {
                            txtFristFPNo.Enabled = true;
                        }
                        else
                        {
                            txtSecondFPNo.Enabled = true;
                        }
                    }
                }
                //供应商付款单（预付单转支付单）
                //if (Request["ProId"] == "33")
                //{
                //    if (count >= 1 && (count - 1) % 4 != 0)
                //    {
                //        if ((count - 1) <= 4)
                //        {
                //            txtFristFPNo.Enabled = true;
                //        }
                //        else
                //        {
                //            txtSecondFPNo.Enabled = true;
                //        }
                //    }
                //}
            }
        }


        private void SetColumnsVis(bool isShow)
        {

            //23-22
            //gvList.Columns[19].Visible = isShow;
            //gvList.Columns[20].Visible = isShow;
            //gvList.Columns[21].Visible = isShow;



            //gvList.Columns[23].Visible = !isShow;
            //gvList.Columns[24].Visible = !isShow;
            //gvList.Columns[25].Visible = !isShow;
            //gvList.Columns[26].Visible = !isShow;
            //gvList.Columns[27].Visible = isShow;
            gvList.Columns[12].Visible = isShow;
            gvList.Columns[13].Visible = !isShow;

            gvList.Columns[20].Visible = isShow;
            gvList.Columns[21].Visible = isShow;
            gvList.Columns[22].Visible = isShow;

            gvList.Columns[24].Visible = !isShow;
            gvList.Columns[25].Visible = !isShow;
            gvList.Columns[26].Visible = !isShow;
            gvList.Columns[27].Visible = !isShow;
            gvList.Columns[28].Visible = isShow;

        }

        private void SetSpecialInvocie()
        {
            if (Convert.ToInt32(Request["ProId"]) == 33)//说明为供应商付款单（预付单转支付单）
            {
                //gvList.Columns[19].Visible = false;
                //gvList.Columns[20].Visible = false;

                //gvList.Columns[23].Visible = true;//发票显示
                //gvList.Columns[24].Visible = true;//发票显示

                gvList.Columns[20].Visible = false;
                gvList.Columns[21].Visible = false;

                gvList.Columns[24].Visible = true;//发票显示
                gvList.Columns[25].Visible = true;//发票显示

            }

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
                        if (Session["SupplierInvoiceIds"] != null)
                        {
                            SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
                            orders = supplierToInvoiceSer.GetListArrayToVerify(string.Format(" CAI_OrderInHouses.ids in ({0}) and CAI_OrderInHouse.Status='通过' ", Session["SupplierInvoiceIds"]), Session["SupplierInvoiceIds"].ToString());

                            //List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetListArray_New_Do(string.Format("CAI_OrderInHouses.Ids in ({0})", Session["SupplierInvoiceIds"]));
                            //foreach (var oo in orders)
                            //{
                            //    var foo= pOOrderList.Find(t => t.Ids == oo.Ids);
                            //    if (foo!=null&&foo.Ids>0)
                            //    {
                            //        oo.new_ShengYuZhiJia = foo.ShengYuZhiJia;
                            //    }
                            //}
                            //获取供应商名称
                            string lastSupplier = "";
                            string allRuids = "";
                            foreach (var supplierToInvoiceView in orders)
                            {
                                //将支付数量=入库数量
                                supplierToInvoiceView.SupplierInvoiceNum = (supplierToInvoiceView.GoodNum ?? 0);

                                supplierToInvoiceView.SupplierInvoiceDate = DateTime.Now;
                                supplierToInvoiceView.SupplierInvoicePrice = supplierToInvoiceView.GoodPrice;



                                lastSupplier = supplierToInvoiceView.GuestName;
                                allRuids += supplierToInvoiceView.Ids + ",";
                                //测试
                                //supplierToInvoiceView.SupplierFpDate = DateTime.Now;
                                //supplierToInvoiceView.SupplierFPNo = "17108511";
                            }

                            #region  需要设置下SupplierInvoiceDate
                            string ruIds = "";
                            foreach (var m in orders)
                            {
                                ruIds += m.Ids + ",";
                            }
                            ruIds = ruIds.Trim(',');
                            string caiOutSql = string.Format(@"select OrderCheckIds,RuTime from CAI_OrderOutHouse LEFT JOIN CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
                         where CAI_OrderOutHouses.OrderCheckIds in (" + ruIds + ")");
                            var dt = DBHelp.getDataTable(caiOutSql);
                            foreach (DataRow dr in dt.Rows)
                            {
                                var OrderCheckIds = (int)dr[0];
                                var ruTime = (DateTime)dr[1];
                                var model = orders.Find(t => t.Ids == OrderCheckIds);
                                if (model != null && model.SupplierInvoiceDate != null && ruTime > model.SupplierInvoiceDate)
                                {
                                    model.IsHouTuiKui = true;
                                }
                            }
                            #endregion



                            //根据供公司名称判断 是否 基于同一供应商 的负数支付单，且结清状态=2的记录存在，如果有，就分别罗列在 这条正数的记录下方
                            var ordersSer = new TB_SupplierInvoicesService();
                            var noCheckOrders =
                                ordersSer.GetListArray_ToAdd(string.Format(" Supplier='{0}' AND IsHeBing=1 and SupplierInvoiceTotal<0 and TB_SupplierInvoice.status='通过' ",
                                                                     lastSupplier));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                                ruIds += supplierToInvoiceView.Ids + ",";
                            }
                            ruIds = ruIds.Trim(',');
                            orders.AddRange(noCheckOrders);

                            //查询
                            List<PoRemarkInfo> list = new SupplierToInvoiceViewService().GetPoRemark(ruIds);
                            foreach (var remark in list)
                            {
                                var model = orders.Find(t => t.Ids == remark.Ids);

                                model.PORemark = remark.PORemark;
                            }
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
                        SetColumnsVis(false);
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

                        TB_SupplierInvoiceService mainSer = new TB_SupplierInvoiceService();
                        TB_SupplierInvoice pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.CreteTime.ToString();
                        txtFristFPNo.Text = pp.FristFPNo;
                        txtSecondFPNo.Text = pp.SecondFPNo;

                       
                        TB_SupplierInvoicesService ordersSer = new TB_SupplierInvoicesService();
                        List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" 1=1 and TB_SupplierInvoices.id=" + Request["allE_id"]);
                       
                        //SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
                        //List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetListArray_New_Do(string.Format("CAI_OrderInHouses.Ids in ({0})",
                        //    string.Join(",", orders.Select(t => t.Ids.ToString()).ToArray())));
                        //foreach (var oo in orders)
                        //{
                        //    var foo = pOOrderList.Find(t => t.Ids == oo.Ids);
                        //    if (foo != null && foo.Ids > 0)
                        //    {
                        //        oo.new_ShengYuZhiJia = foo.ShengYuZhiJia;
                        //    }
                        //}
                        #region  需要设置下SupplierInvoiceDate
                        string ruIds = "";
                        foreach (var m in orders)
                        {
                            ruIds += m.Ids + ",";
                        }
                        ruIds = ruIds.Trim(',');
                        string caiOutSql = string.Format(@"select OrderCheckIds,RuTime from CAI_OrderOutHouse LEFT JOIN CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
                         where CAI_OrderOutHouses.OrderCheckIds in (" + ruIds + ")");
                        var dt = DBHelp.getDataTable(caiOutSql);
                        foreach (DataRow dr in dt.Rows)
                        {
                            var OrderCheckIds = (int)dr[0];
                            var ruTime = (DateTime)dr[1];
                            var model = orders.Find(t => t.Ids == OrderCheckIds);
                            if (model != null && model.SupplierInvoiceDate != null && ruTime > model.SupplierInvoiceDate)
                            {
                                model.IsHouTuiKui = true;
                            }
                        }
                        if (pp.CreateName == "admin")
                        {
                            var list=new SupplierAdvancePaymentsToPayService().GetFPInfo_View(ruIds);
                            txtFristFPNo.Text = list[0];
                            txtSecondFPNo.Text = list[2];
                        }
                        #endregion
                        if (orders.Count > 0)
                        {
                            //根据供公司名称 来查看 是否要加载 有实际支付 为负数并且结清字段为0的 记录存在
                            var noCheckOrders =
                                ordersSer.GetListArray_ToAdd(
                                    string.Format(" exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds and SupplierInvoiceId={0})",
                                                  Request["allE_id"]));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                                ruIds += supplierToInvoiceView.Ids + ",";
                            }
                            ruIds = ruIds.Trim(',');
                            orders.AddRange(noCheckOrders);

                            //查询
                            List<PoRemarkInfo> list = new SupplierToInvoiceViewService().GetPoRemark(ruIds);
                            foreach (var remark in list)
                            {
                                var model = orders.Find(t => t.Ids == remark.Ids);

                                model.PORemark = remark.PORemark;
                            }
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

                        TB_SupplierInvoiceService mainSer = new TB_SupplierInvoiceService();
                        TB_SupplierInvoice pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtRuTime.Text = pp.CreteTime.ToString();
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;

                        txtRemark.Text = pp.Remark;
                        txtFristFPNo.Text = pp.FristFPNo;
                        txtSecondFPNo.Text = pp.SecondFPNo;
                        TB_SupplierInvoicesService ordersSer = new TB_SupplierInvoicesService();
                        List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" 1=1 and TB_SupplierInvoices.id=" + Request["allE_id"]);

                        //SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
                        //List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetListArray_New_Do(string.Format("CAI_OrderInHouses.Ids in ({0})",
                        //    string.Join(",", orders.Select(t => t.Ids.ToString()).ToArray())));
                        //foreach (var oo in orders)
                        //{
                        //    var foo = pOOrderList.Find(t => t.Ids == oo.Ids);
                        //    if (foo != null && foo.Ids > 0)
                        //    {
                        //        oo.new_ShengYuZhiJia = foo.ShengYuZhiJia;
                        //    }
                        //}

                        #region  需要设置下SupplierInvoiceDate
                        string ruIds = "";
                        foreach (var m in orders)
                        {
                            ruIds += m.Ids + ",";
                        }
                        ruIds = ruIds.Trim(',');
                        string caiOutSql = string.Format(@"select OrderCheckIds,RuTime from CAI_OrderOutHouse LEFT JOIN CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
                         where CAI_OrderOutHouses.OrderCheckIds in (" + ruIds + ")");
                        var dt = DBHelp.getDataTable(caiOutSql);
                        foreach (DataRow dr in dt.Rows)
                        {
                            var OrderCheckIds = (int)dr[0];
                            var ruTime = (DateTime)dr[1];
                            var model = orders.Find(t => t.Ids == OrderCheckIds);
                            if (model != null && model.SupplierInvoiceDate != null && ruTime > model.SupplierInvoiceDate)
                            {
                                model.IsHouTuiKui = true;
                            }
                        }

                        if (pp.CreateName == "admin"&&pp.FristFPNo=="")
                        {
                            var list = new SupplierAdvancePaymentsToPayService().GetFPInfo_View(ruIds);
                            txtFristFPNo.Text = list[0];
                            txtSecondFPNo.Text = list[2];
                        }
                        #endregion

                        if (orders.Count > 0)
                        {
                            //根据供公司名称 来查看 是否要加载 有实际支付 为负数并且结清字段为0的 记录存在
                            var noCheckOrders =
                                ordersSer.GetListArray_ToAdd(
                                    string.Format(" exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds and SupplierInvoiceId={0})",
                                                  Request["allE_id"]));
                            foreach (var supplierToInvoiceView in noCheckOrders)
                            {
                                supplierToInvoiceView.IfCheck = false;
                                ruIds += supplierToInvoiceView.Ids + ",";
                            }
                            ruIds = ruIds.Trim(',');
                            orders.AddRange(noCheckOrders);

                            //查询
                            List<PoRemarkInfo> list = new SupplierToInvoiceViewService().GetPoRemark(ruIds);
                            foreach (var remark in list)
                            {
                                var model = orders.Find(t => t.Ids == remark.Ids);

                                model.PORemark = remark.PORemark;
                            }
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

                            if (Convert.ToInt32(Request["ProId"]) == 33 && pp.Status == "不通过")
                            {
                                btnReStart.Visible = true;
                            }

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
                                    SetSpecialInvocie();

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
                                        SetSpecialInvocie();

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

                    TB_SupplierInvoice order = new TB_SupplierInvoice();

                    int CreatePer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateName = txtName.Text;
                    order.CreteTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.FristFPNo = txtFristFPNo.Text;
                    order.SecondFPNo = txtSecondFPNo.Text;
                    List<SupplierToInvoiceView> POOrders = ViewState["Orders"] as List<SupplierToInvoiceView>;
                    order.SumActPay = POOrders.Sum(t => t.ActPay);
                    foreach (var m in POOrders)
                    {
                        if (m.IfCheck == false)
                        {
                            continue;
                        }
                        if (m.SupplierInvoicePrice <= 0 || m.SupplierInvoiceTotal <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('单价不能为负数！');</script>");
                            return;
                        }
                    }
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


                        TB_SupplierInvoiceService POOrderSer = new TB_SupplierInvoiceService();


                        foreach (var m in POOrders)
                        {
                            m.lastGoodNum = m.SupplierInvoiceNum;
                            m.ActPay = m.SupplierInvoiceTotal;
                        }

                        //foreach (var m in POOrders)
                        //{
                        //    m.lastGoodNum = m.GoodNum.Value;
                        //}
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {
                            //new TB_SupplierInvoiceService().SetRuKuPayStatus_ToAdd(POOrders);
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
                            TB_SupplierInvoiceService POSer = new TB_SupplierInvoiceService();
                            var model = POSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                            //if (Session["currentUserId"].ToString() != model.CreateUserId.ToString())
                            //{

                            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                            //    return;
                            //}

                            if (model != null && model.Status == "执行中")
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据在执行中不能重新编辑！');</script>");
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

                                if (Convert.ToInt32(Request["ProId"]) == 33)
                                {
                                    forms.resultState += " 被打回重新填写!";
                                    eform.E_Remark = "被打回重新填写";
                                }

                            }
                            else
                            {
                                eform.state = "执行中";
                                eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                                eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                            }
                        }
                        TB_SupplierInvoiceService POOrderSer = new TB_SupplierInvoiceService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        foreach (var m in POOrders)
                        {
                            m.lastGoodNum = m.SupplierInvoiceNum;
                        }
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {

                            if (ddlPers.Visible == false && ddlResult.Text == "通过")
                            {
                                if (POOrders.FindAll(t => t.IsYuFu).Count > 0)
                                {
                                    //new TB_SupplierInvoiceService().SetRuKuPayStatus_YuFu(POOrders);
                                }
                                else
                                {
                                    new TB_SupplierInvoiceService().SetRuKuPayStatus(POOrders);
                                }
                            }
                            UpdateEForm();
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

        private void UpdateEForm()
        {
            //判断是否是预付款转支付单 并且不通过时, 单子的状态变回最原始状态，因为此类单据不允许出现不通过的显现
            if (Convert.ToInt32(Request["ProId"]) == 33 && ddlResult.Text == "不通过")
            {
                tb_EForm eform = new TB_SupplierInvoiceService().GetToSupplierInvoice();

                var sql = string.Format(" update tb_EForm set state='执行中',toPer={0},toProsId={1},e_Remark='驳回-重填' where id={2};update TB_SupplierInvoice set Status='执行中' where id={3};",
                    eform.toPer, eform.toProsId, Request["EForm_Id"], Request["allE_id"]);
                DBHelp.ExeCommand(sql);
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
                SumOrders.ResultTotal += model.ResultTotal;
                SumOrders.ActPay += model.ActPay;
                SumOrders.FuShuTotal += model.FuShuTotal;
                SumOrders.HadSupplierInvoiceTotal += model.HadSupplierInvoiceTotal;

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
                    if (model.IsHouTuiKui)
                    {
                        var lblSupplierTuiGoodNum = e.Row.FindControl("lblSupplierTuiGoodNum") as Label;
                        lblSupplierTuiGoodNum.BackColor = System.Drawing.Color.Red;
                    }
                }

                if (model.HuiKuanLiLv == 0)
                {
                    var lblHuiKuanLiLv = e.Row.FindControl("lblHuiKuanLiLv") as Label;
                    lblHuiKuanLiLv.BackColor = System.Drawing.Color.Red;
                }


            }

            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblSupplierInvoiceTotal") as Label, SumOrders.SupplierInvoiceTotal.ToString());//数量  
                setValue(e.Row.FindControl("lblSupplierInvoiceTotal1") as Label, SumOrders.SupplierInvoiceTotal.ToString());//数量  
                setValue(e.Row.FindControl("lblLastTotal") as Label, SumOrders.ResultTotal.ToString());//数量  
                setValue(e.Row.FindControl("lblActPay") as Label, SumOrders.ActPay.ToString());//数量  
                setValue(e.Row.FindControl("lblActPay2") as Label, SumOrders.ActPay.ToString());//数量  
                setValue(e.Row.FindControl("lblFuShuTotal") as Label, SumOrders.FuShuTotal.ToString());//数量 
                setValue(e.Row.FindControl("lblHadSupplierInvoiceTotal") as Label, SumOrders.HadSupplierInvoiceTotal.ToString());//数量                 

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

        protected void btnReStart_Click(object sender, EventArgs e)
        {
            ddlResult.Text = "不通过";
            UpdateEForm();
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





    }
}
