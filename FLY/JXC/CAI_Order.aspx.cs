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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.Collections.Generic;
using System.IO;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using System.Drawing;

namespace VAN_OA.JXC
{
    public partial class CAI_Order : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();
        public string GetTopPrice(object obj)
        {
            if (obj != null && Convert.ToDecimal(obj) != 0)
            {
                return string.Format("({0})", obj);
            }
            return "";

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


            if (txtCaiGou.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写请购人！');</script>");
                txtCaiGou.Focus();

                return false;
            }

            //if (txtGuestNo.Text == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户ID！');</script>");
            //    txtGuestNo.Focus();

            //    return false;
            //}


            if (txtPOName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目名称！');</script>");
                txtPOName.Focus();

                return false;
            }

            if (txtPODate.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目日期！');</script>");
                txtPODate.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtPODate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return false;
                }

            }



            if (txtPOTotal.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目金额！');</script>");
                txtPOTotal.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtPOTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return false;
                }

            }

            if (txtPOPayStype.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写结算方式！');</script>");
                txtPOPayStype.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtPOPayStype.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结算方式 格式错误！');</script>");
                    return false;
                }
            }


            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }



            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                // txtForm.Focus();

                return false;
            }

            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtCaiGou.Text.Trim())) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写请购人不存在！');</script>");
                // txtForm.Focus();

                return false;
            }


            #endregion

            if (ddlBusType.Text == "0")
            {
                if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                    return false;
                }
            }
            else
            {
                if (Request["allE_id"] == null && ddlUser.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择相应的AE！');</script>");
                    ddlUser.Focus();
                    return false;
                }
            }
            if (Request["allE_id"] == null)
            {
                if (txtRemark.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写备注！');</script>");
                    return false;
                }
                List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }
                decimal alltotal = 0;
                if (ddlBusType.Text == "0")//项目订单采购
                {
                    if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                                "<script>alert('此项目已经关闭！');</script>");
                        return false;
                    }
                }

                Hashtable ht = new Hashtable();
                foreach (var model in POOrders)
                {
                    alltotal += model.SellTotal;
                    if (!ht.Contains(model.GoodId))
                        ht.Add(model.GoodId, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.Num <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.SellPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                }
                if (ddlBusType.SelectedValue == "1")
                {
                    if (alltotal != Convert.ToDecimal(txtPOTotal.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的销售明显总金额和本次项目总金额不一致！');</script>");
                        return false;

                    }
                    foreach (var model in POOrders)
                    {
                        model.CG_POOrdersId = 0;
                    }
                }
                else if (ddlBusType.SelectedValue == "0")
                {
                    foreach (var model in POOrders)
                    {
                        List<Model.JXC.CG_POOrders> POOrdersList = poOrderSer.GetListCG_POOrders_Cai_POOrders_View(string.Format(" 1=1 and CG_POOrders_Cai_POOrders_View.id in (select id from CG_POOrder where PONo='{0}') and ids={1}", txtPONo.Text, model.CG_POOrdersId));
                        if (POOrdersList.Count <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],在项目订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                            return false;

                        }
                        if (model.Num > POOrdersList[0].ResultTotalNum)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, POOrdersList[0].ResultTotalNum));

                            return false;
                        }
                    }

                    if (ddlBusType.Text == "0")
                    {
                        //需采购的数量=原项目订单该商品的数量+所有该追加项目订单的该商品数量-（项目所有该商品的销售退货数量）-（项目所有该商品的采购退货数量）-该商品已经采购入库的数量
                        //但是 如果销售退货后 又做了采购退货，两部分的退货数量能够计一次（以销售退货的数量为准）。
                        CAI_POOrderService caiOrderSer = new CAI_POOrderService();
                        foreach (var model in POOrders)
                        {
                            var actTotal = caiOrderSer.GetActGoosTotalNum(model.GoodId, txtPONo.Text, 0);
                            if (actTotal <= 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],不需要在进行采购！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                                return false;
                            }
                            var chaTotal = model.Num - actTotal;
                            if (chaTotal > 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],需采购的数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, actTotal));
                                return false;
                            }
                        }
                    }
                }



            }

            else
            {
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    List<CAI_POCai> orderCais = ViewState["Cais"] as List<CAI_POCai>;
                    //if (ddlPers.Visible == false)//说明为最后一次审核
                    if (ViewState["RoleCount"] != null && (ViewState["RoleCount"].ToString() == "1"))
                    {
                        foreach (var m in orderCais)
                        {
                            if (m.cbifDefault1 == false && m.cbifDefault2 == false && m.cbifDefault3 == false)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('请为商品[{0}]\规格[{1}]\型号[{2}],指定最终供应商！');</script>", m.GoodName, m.GoodSpec, m.Good_Model));
                                return false;
                            }
                        }
                        if (ddlBusType.Text == "0")
                        {
                            //需采购的数量=原项目订单该商品的数量+所有该追加项目订单的该商品数量-（项目所有该商品的销售退货数量）-（项目所有该商品的采购退货数量）-该商品已经采购入库的数量
                            //但是 如果销售退货后 又做了采购退货，两部分的退货数量能够计一次（以销售退货的数量为准）。
                            CAI_POOrderService caiOrderSer = new CAI_POOrderService();
                            foreach (var model in orderCais)
                            {
                                var actTotal = caiOrderSer.GetActGoosTotalNum(model.GoodId, txtPONo.Text, 0);
                                if (actTotal <= 0)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],不需要在进行采购！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                                    return false;
                                }
                                var chaTotal = model.Num - actTotal;
                                if (chaTotal > 0)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],需采购的数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, actTotal));
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ViewState["RoleCount"] != null && ViewState["RoleCount"].ToString() == "2")
                        {
                            foreach (var m in orderCais)
                            {
                                if (m.Supplier.Trim() == "" && m.Supplier1.Trim() == "" && m.Supplier2.Trim() == "")
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('请为商品[{0}]\规格[{1}]\型号[{2}],至少指定一个供应商！');</script>", m.GoodName, m.GoodSpec, m.Good_Model));

                                    return false;
                                }

                                List<TB_Good> goodList = new TB_GoodService().GetListArray_New(string.Format(" Temp.GoodId={0}", m.GoodId));
                                decimal ZhiLiuKuCun = 0;
                                if (goodList.Count > 0)
                                {
                                    lblZhiDaiNum.Text = string.Format("滞留库存数量:{0}", goodList[0].ZhiLiuKuCun);
                                    ZhiLiuKuCun = goodList[0].ZhiLiuKuCun;
                                }
                                string sqlZhiLiu = string.Format("select isnull(sum(Num),0) as CaiIng FROM CAI_POOrder " +
                                    "left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.Id where Status='执行中' and GoodId={0} and BusType=1 ", m.GoodId);
                                var kcIng = Convert.ToDecimal(DBHelp.ExeScalar(sqlZhiLiu));
                                lblKCIng.Text = string.Format("KC采购中数量:{0}", kcIng);

                                sqlZhiLiu = string.Format(@"select 
isnull(sum(Num - isnull(totalOrderNum, 0)),0) as CaiNotCheckNum
from CAI_POCai
left join CAI_POOrder on CAI_POCai.Id = CAI_POOrder.Id
left
join
(
select CaiId, SUM(CheckNum) as totalOrderNum from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderCheck.id = CAI_OrderChecks.CheckId
where CaiId <> 0  and CAI_OrderCheck.status = '通过'
group by CaiId
)
as newtable on CAI_POCai.Ids = newtable.CaiId
where(CAI_POCai.Num > newtable.totalOrderNum or totalOrderNum is null)
and status = '通过' and lastSupplier<>'库存' and BusType = 1 and CAI_POCai.GoodId={0}", m.GoodId);
                                var KCNotRuKu = Convert.ToDecimal(DBHelp.ExeScalar(sqlZhiLiu));
                                lblKCNotRuKu.Text = string.Format("KC采购完成未入库数量:{0}", KCNotRuKu);

                                //滞留库存数量+KC采购中数量+KC采购完成未入库数量=0
                               var sumZhiTotal = (ZhiLiuKuCun + kcIng + KCNotRuKu);

                                if (ddlBusType.Text == "0" && Convert.ToDecimal(sumZhiTotal) == 0 && m.Supplier == "库存")
                                { 
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('滞留库存数量+KC采购中数量+KC采购完成未入库数量=0，该商品允许外采！！');</script>"));
                                    return false;
                                }
                                else if (ddlBusType.Text == "0" && Convert.ToDecimal(sumZhiTotal) != 0 && m.Supplier != "库存")
                                { 
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('滞留库存数量+KC采购中数量+KC采购完成未入库数量<>0，该商品只能出库存！');</script>"));
                                    return false;
                                }

                                if (ddlBusType.Text == "1" && (m.Supplier.Trim() == "库存" || m.Supplier1.Trim() == "库存" || m.Supplier2.Trim() == "库存"))
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('业务类型为库存采购时，采购供应商则 严格禁止 输入或选择 库存！');</script>"));

                                    return false;
                                }
                                if (m.Supplier != "" && m.Supplier != "库存")
                                {
                                    if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", m.Supplier)) > 0)
                                    {
                                        if (Convert.ToDecimal(m.SupperPrice) <= Convert.ToDecimal(m.TruePrice1))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", m.Supplier), true);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(m.SupperPrice) < Convert.ToDecimal(m.TruePrice1))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<=采购单价，请核对！');", m.Supplier), true);
                                            return false;
                                        }
                                    }
                                }
                                if (m.Supplier1 != "" && m.Supplier1 != "库存")
                                {
                                    if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", m.Supplier1)) > 0)
                                    {
                                        if (Convert.ToDecimal(m.SupperPrice1) <= Convert.ToDecimal(m.TruePrice2))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", m.Supplier1), true);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(m.SupperPrice1) < Convert.ToDecimal(m.TruePrice2))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<=采购单价，请核对！');", m.Supplier1), true);
                                            return false;
                                        }
                                    }
                                }
                                if (m.Supplier2 != "" && m.Supplier2 != "库存")
                                {
                                    if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", m.Supplier2)) > 0)
                                    {
                                        if (Convert.ToDecimal(m.SupperPrice2) <= Convert.ToDecimal(m.TruePrice3))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", m.Supplier2), true);
                                            return false;
                                        }
                                    }
                                    else
                                    {

                                        if (Convert.ToDecimal(m.SupperPrice2) < Convert.ToDecimal(m.TruePrice3))
                                        {
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<=采购单价，请核对！');", m.Supplier2), true);
                                            return false;
                                        }
                                    }
                                }
                                if (m.Supplier.Trim() == "库存" || m.Supplier1.Trim() == "库存" || m.Supplier2.Trim() == "库存")
                                {
                                    //检查库存是否存在
                                    string checkHouse = "select GoodNum from TB_HouseGoods where GoodId=" + m.GoodId;
                                    var goodHouseCount = DBHelp.ExeScalar(checkHouse);
                                    if ((goodHouseCount is DBNull) || goodHouseCount == null)
                                    {
                                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('库存不存在 商品[{0}]\规格[{1}]\型号[{2}]！');</script>",
                                            m.GoodName, m.GoodSpec, m.Good_Model));
                                        return false;

                                    }
                                    else
                                    {

                                        if (m.Num > Convert.ToDecimal(goodHouseCount))
                                        {
                                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('当选 库存采购时，采购数量应该小于等于库存数量 商品[{0}]\规格[{1}]\型号[{2}]！');</script>",
                                            m.GoodName, m.GoodSpec, m.Good_Model));
                                            return false;

                                        }
                                    }
                                }
                                else
                                {
                                    if (ddlBusType.Text == "0")
                                    {
                                        //当采购在第二步选择供应商时，如果他采购的商品 在库存中的数量>0 , 而采购在选择供应商时没有 选库存，
                                        //则 你需要提示一个窗口，提示 “库存内有该商品，请选择库存”，退回原界面
                                        //string checkHouse = "select GoodNum from TB_HouseGoods where GoodId=" + m.GoodId;
                                        //var goodHouseCount = DBHelp.ExeScalar(checkHouse);
                                        //if (!((goodHouseCount is DBNull) || goodHouseCount == null) && Convert.ToInt32(goodHouseCount) > 0)
                                        //{
                                        //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('库存内有该商品，请选择库存 商品[{0}]\规格[{1}]\型号[{2}]！');</script>",
                                        //      m.GoodName, m.GoodSpec, m.Good_Model));
                                        //    return false;
                                        //}
                                    }

                                }
                            }

                            #region 判断是否为特殊商品 并查询库存是否有库存 且 不等于 库存 供应商
                            TB_HouseGoodsService goodHouseSer = new TB_HouseGoodsService();
                            CAI_OrderCheckService checkSer = new CAI_OrderCheckService();
                            foreach (var m in orderCais)
                            {
                                if (goodHouseSer.CheckGoodInHouse(m.GoodId))
                                {
                                    if ((m.Supplier != "" && m.Supplier != "库存")
                                        || (m.Supplier1 != "" && m.Supplier1 != "库存")
                                        || (m.Supplier2 != "" && m.Supplier2 != "库存"))
                                    {
                                        string mess = checkSer.GetPONoInfo(m.GoodId, m.GoodNo, m.GoodName, m.GoodTypeSmName, m.GoodSpec);
                                        if (mess == "")
                                        {
                                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('数据异常！');</script>"));
                                            return false;
                                        }

                                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                            string.Format(@"<script>alert('{0}');</script>",
                                            mess));
                                        return false;
                                    }
                                }
                            }
                            #endregion
                        }
                    }

                    //if (ddlPers.Visible == false)//说明为最后一次审核
                    if (ViewState["RoleCount"] != null && (ViewState["RoleCount"].ToString() == "1"))
                    {
                        TB_HouseGoodsService goodHouseSer = new TB_HouseGoodsService();
                        CAI_OrderCheckService checkSer = new CAI_OrderCheckService();
                        foreach (var m in orderCais)
                        {
                            string resultSupplier = "";
                            int count = 0;
                            if (m.cbifDefault1 && m.Supplier != "")
                            {
                                resultSupplier = m.Supplier;
                                count++;
                            }
                            if (m.cbifDefault2 && m.Supplier1 != "")
                            {
                                resultSupplier = m.Supplier1;
                                count++;
                            }
                            if (m.cbifDefault3 && m.Supplier2 != "")
                            {
                                resultSupplier = m.Supplier2;
                                count++;
                            }

                            if (count != 1)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                    string.Format("<script>alert('商品[{0}]请设置一个最终供应商！');</script>", m.GoodNo));
                                return false;
                            }
                            if (resultSupplier != "库存" && goodHouseSer.CheckGoodInHouse(m.GoodId))
                            {

                                string mess = checkSer.GetPONoInfo(m.GoodId, m.GoodNo, m.GoodName, m.GoodTypeSmName, m.GoodSpec);
                                if (mess == "")
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('数据异常！');</script>"));
                                    return false;
                                }

                                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                    string.Format(@"<script>alert('{0}');</script>",
                                    mess));
                                return false;
                            }
                        }
                        System.Collections.Hashtable hsCai = new Hashtable();
                        foreach (var m in orderCais)
                        {

                            if (m.cbifDefault1 && (m.FinPrice1 != null || m.SupperPrice != null))
                            {

                                if (!hsCai.Contains(m.Supplier))
                                {
                                    hsCai.Add(m.Supplier, m.IsHanShui);
                                }
                            }
                            if (m.cbifDefault2 && (m.FinPrice2 != null || m.SupperPrice1 != null))
                            {
                                if (!hsCai.Contains(m.Supplier1))
                                {
                                    hsCai.Add(m.Supplier1, m.IsHanShui);
                                }
                            }
                            if (m.cbifDefault3 && (m.FinPrice3 != null || m.SupperPrice2 != null))
                            {
                                if (!hsCai.Contains(m.Supplier2))
                                {
                                    hsCai.Add(m.Supplier2, m.IsHanShui);
                                }
                            }
                            //if (!hsCai.Contains(m.Supplier1))
                            //{
                            //    hsCai.Add(m.Supplier1, m.IsHanShui);
                            //}
                            //if (!hsCai.Contains(m.Supplier2))
                            //{
                            //    hsCai.Add(m.Supplier2, m.IsHanShui);
                            //}

                        }
                        foreach (var key in hsCai.Keys)
                        {
                            string sql = "";
                            if (key != "")
                            {

                                sql = string.Format("select top 1 IsSpecial from TB_SupplierInfo where SupplieSimpeName='{0}' and Status='通过' and IsUse=1 ", key);
                                var IsSpecial = DBHelp.ExeScalar(sql);
                                if (IsSpecial is DBNull || IsSpecial == null)
                                {

                                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                        string.Format(@"<script>alert('供应商信息不存在！');</script>"));
                                    return false;
                                }
                                else
                                {
                                    if (Convert.ToBoolean(IsSpecial) && Convert.ToBoolean(hsCai[key]) == true)
                                    {
                                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                        string.Format(@"<script>alert('{0} 为不含税供应商！');</script>", key));
                                        return false;
                                    }
                                    //if (Convert.ToBoolean(IsSpecial)==false && Convert.ToBoolean(hsCai[key])==false)
                                    //{
                                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                    //   string.Format(@"<script>alert('{0} 是含税供应商！');</script>",key));
                                    //    return false;
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }



        private void setEnable(bool result)
        {
            //txtName.Enabled = false;
            //txtCaiGou.Enabled = false;

            //txtCG_ProNo.Enabled = false;

            //txtGuestNo.Enabled = false;
            //txtPODate.Enabled = false;
            //txtPOName.Enabled = false;
            //txtPOPayStype.Enabled = false;
            //txtPOTotal.Enabled = false;

            //txtGuestName.Enabled = false;
            //txtGuestNo.Enabled = false;
            //txtINSIDE.Enabled = false;
            //txtAE.Enabled = false;

            //Image1.Enabled = false;
            //txtPOName.Enabled = false;
            //ddlBusType.Enabled = false;

            //txtPONo.Enabled = false;


            txtName.ReadOnly = true;
            txtCaiGou.ReadOnly = true;

            txtCG_ProNo.ReadOnly = true;

            txtGuestNo.ReadOnly = true;
            txtPODate.ReadOnly = true;
            txtPOName.ReadOnly = true;
            txtPOPayStype.ReadOnly = true;
            txtPOTotal.ReadOnly = true;

            txtGuestName.ReadOnly = true;
            txtGuestNo.ReadOnly = true;
            txtINSIDE.ReadOnly = true;
            txtAE.ReadOnly = true;

            Image1.Enabled = false;
            txtPOName.ReadOnly = true;
            ddlBusType.Enabled = false;

            txtPONo.ReadOnly = true;

            cbMingYiCaiGou.Enabled = false;
            //gvList.Columns[0].Visible = result;
            //gvList.Columns[1].Visible = result;
            //lbtnAddFiles.Visible = result;


            //gvCai.Columns[0].Visible = result;
            //gvCai.Columns[1].Visible = result;


            txtRemark.ReadOnly = true;
            ddlUser.Enabled = false;
            //txtDepartName.ReadOnly = !result;
            //txtForm.Enabled = result;
            //txtName.ReadOnly = !result;
            //txtRemark.ReadOnly = !result;
            //txtTo.Enabled = result;
            //txtZhiwu.ReadOnly = !result;
            //txtZhiwu.ReadOnly = !result;
            //Panel1.Enabled = result;

        }


        private void SetRole(int Count)
        {
            //开始---采购---合同审计—副总经理---合同复核
            ViewState["RoleCount"] = Count;
            //开始
            if (Count == 0)
            {
                gvCai.Visible = false;
            }
            //采购
            if (Count == 2)
            {
                //权限3（采购）

                gvCai.Columns[0].Visible = true;

                plCiGou.Visible = true;

                txtIdea.Visible = false;
                lblIdea.Visible = false;

                txtFinPrice1.Visible = false;
                txtFinPrice2.Visible = false;
                txtFinPrice3.Visible = false;

                cbifDefault1.Visible = false;
                cbifDefault2.Visible = false;
                cbifDefault3.Visible = false;


                kucun1.Visible = true;
                kucun2.Visible = true;
                kucun3.Visible = true;

            }
            //合同审计+副总经理         
            if (Count == 1)
            {
                //权限4（经理再次确认）
                plCiGou.Visible = true;

                gvCai.Columns[0].Visible = true;

                txtPrice2.ReadOnly = true;
                txtPrice3.ReadOnly = true;
                txtSupperPrice.ReadOnly = true;
                //txtSupper2.ReadOnly = true;
                //txtSupper3.ReadOnly = true;
                //txtSupplier.ReadOnly = true;

                cbifDefault1.Visible = true;
                cbifDefault2.Visible = true;
                cbifDefault3.Visible = true;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";

                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                List<FpTypeBaseInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList.Insert(0, new FpTypeBaseInfo { FpType = "", Id = -1 });
                dllFPstye.DataSource = gooQGooddList;
                dllFPstye.DataBind();
                dllFPstye.DataTextField = "FpType";
                dllFPstye.DataValueField = "Id";
                try
                {

                    //dllFPstye.SelectedIndex = gooQGooddList.Select(t => t.FpType).ToList().IndexOf("增值税发票"); ;
                }
                catch (Exception)
                {

                }


                kucun1.Visible = false;
                kucun2.Visible = false;
                kucun3.Visible = false;
                //请假单子              
                Session["Orders"] = null;

                ViewState["Cais"] = null;
                lbtnAddFiles.Visible = false;

                gvList.Columns[0].Visible = false;
                gvList.Columns[1].Visible = false;
                gvCai.Columns[0].Visible = false;

                lblAttName.Visible = false;
                fuAttach.Visible = false;

                // gvCai.Visible = false;
                plCiGou.Visible = false;
                lbtnAddFiles.Visible = false;
                //再次编辑
                // btnReSubEdit.Visible = false;

                if (base.Request["ProId"] != null)
                {

                    lbtnAddFiles.Visible = false;

                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    lbtnPoList.Visible = false;
                    btnSave.Enabled = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtPONo.ReadOnly = true;
                        fuAttach.Visible = true;
                        //权限1（销售）
                        lbtnAddFiles.Visible = false;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;
                        lbtnPoList.Visible = true;
                        //加载初始数据

                        List<CAI_POOrders> orders = new List<CAI_POOrders>();
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();


                        List<CAI_POCai> orderCais = new List<CAI_POCai>();
                        ViewState["Cais"] = orderCais;
                        ViewState["CaisCount"] = orderCais.Count;
                        gvCai.DataSource = orderCais;
                        gvCai.DataBind();

                        if (Request["Copy"] != null)
                        {
                            #region  加载数据
                            gvCai.Visible = true;
                            gvList.Columns[0].Visible = false;
                            gvList.Columns[1].Visible = false;
                            lbtnPoList.Visible = false;
                            setEnable(false);
                            txtRemark.ReadOnly = false;

                            CAI_POOrderService mainSer = new CAI_POOrderService();
                            CAI_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["Id"]));

                            cbMingYiCaiGou.Checked = pp.IsMingYiCaiGou;
                            txtCaiGou.Text = pp.CaiGou;
                            txtRemark.Text = pp.cRemark;

                            txtAE.Text = pp.AE;
                            txtGuestName.Text = pp.GuestName;
                            txtGuestNo.Text = pp.GuestNo;
                            txtINSIDE.Text = pp.INSIDE;
                            txtPODate.Text = pp.PODate.ToString();
                            txtPOName.Text = pp.POName;
                            txtPONo.Text = pp.PONo;
                            txtPOPayStype.Text = pp.POPayStype;
                            txtPOTotal.Text = pp.POTotal.ToString();
                            txtCG_ProNo.Text = pp.CG_ProNo;
                            ddlBusType.Text = pp.BusType;


                            if (pp.ProNo != null)
                                lblProNo.Text = pp.ProNo;
                            CAI_POOrdersService ordersSer = new CAI_POOrdersService();
                            orders = ordersSer.GetListArray(" 1=1 and CAI_POOrders.id=" + Request["Id"]);
                            Session["Orders"] = orders;
                            ViewState["OrdersCount"] = orders.Count;

                            gvList.DataSource = orders;
                            gvList.DataBind();

                            if (pp.fileName != null && pp.fileName != "")
                            {
                                lblAttName.Visible = true;
                                lblAttName.Text = pp.fileName;

                                if (pp.fileName.LastIndexOf('.') != -1)
                                {
                                    lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                                }
                                else
                                {
                                    lblAttName_Vis.Text = pp.fileName;
                                }
                            }

                            lblZhui.Text = new CG_POOrderService().IfZhuiByProNo(txtCG_ProNo.Text);
                            lblSpecial.Text = new CG_POOrderService().IfSpecialByProNo(txtCG_ProNo.Text);
                            CAI_POCaiService CaiSer = new CAI_POCaiService();
                            List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and CAI_POCai.id=" + Request["Id"]);
                            ViewState["Cais"] = caiList;
                            ViewState["CaisCount"] = caiList.Count;

                            gvCai.DataSource = caiList;
                            gvCai.DataBind();
                            #endregion
                        }

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人
                            int pro_IDs = 0;
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                                try
                                {
                                    ddlPers.Text = use.Id.ToString();
                                }
                                catch (Exception)
                                {

                                }

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

                    else if (Request["ReEdit"] != null)//再次编辑
                    {


                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;

                        //加载初始数据

                        //List<CAI_POOrders> orders = new List<CAI_POOrders>();
                        //Session["Orders"] = orders;
                        //ViewState["OrdersCount"] = orders.Count;

                        //gvList.DataSource = orders;
                        //gvList.DataBind();


                        List<CAI_POCai> orderCais = new List<CAI_POCai>();
                        ViewState["Cais"] = orderCais;
                        ViewState["CaisCount"] = orderCais.Count;
                        gvCai.DataSource = orderCais;
                        gvCai.DataBind();

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人
                            int pro_IDs = 0;
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

                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";
                        #region  加载 请假单数据

                        CAI_POOrderService mainSer = new CAI_POOrderService();
                        CAI_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        cbMingYiCaiGou.Checked = pp.IsMingYiCaiGou;
                        lblProNo.Text = pp.ProNo;
                        txtCaiGou.Text = pp.CaiGou;

                        txtAE.Text = pp.AE;
                        try
                        {
                            ddlUser.Text = pp.AE;
                        }
                        catch (Exception)
                        {


                        }

                        txtGuestName.Text = pp.GuestName;
                        txtGuestNo.Text = pp.GuestNo;
                        txtINSIDE.Text = pp.INSIDE;
                        txtPODate.Text = pp.PODate.ToString();
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        txtPOPayStype.Text = pp.POPayStype;
                        txtPOTotal.Text = pp.POTotal.ToString();
                        txtCG_ProNo.Text = pp.CG_ProNo;
                        ddlBusType.Text = pp.BusType;
                        lblZhui.Text = new CG_POOrderService().IfZhuiByProNo(txtCG_ProNo.Text);
                        lblSpecial.Text = new CG_POOrderService().IfSpecialByProNo(txtCG_ProNo.Text);
                        txtRemark.Text = pp.cRemark;
                        CAI_POOrdersService ordersSer = new CAI_POOrdersService();
                        List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and CAI_POOrders.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;


                        gvList.DataSource = orders;
                        gvList.DataBind();

                        if (pp.fileName != null && pp.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = pp.fileName;
                            lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                        }
                        else
                        {
                            fuAttach.Visible = true;
                        }
                        //CAI_POCaiService CaiSer = new CAI_POCaiService();
                        //List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and id=" + Request["allE_id"]);
                        //ViewState["Cais"] = caiList;
                        //ViewState["CaisCount"] = caiList.Count;

                        //gvCai.DataSource = caiList;
                        //gvCai.DataBind();

                        #endregion





                    }
                    else if (Request["ReAudit"] != null)//重新提交编辑
                    {
                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";

                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        ViewState["currentEforms"] = eforms.Count;
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

                        int eformsCount = 0;
                        ViewState["EformsCount"] = eformsCount;

                        #region  加载 请假单数据
                        CAI_POOrderService mainSer = new CAI_POOrderService();
                        CAI_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        cbMingYiCaiGou.Checked = pp.IsMingYiCaiGou;
                        txtName.Text = pp.LoginName;
                        txtCaiGou.Text = pp.CaiGou;
                        txtRemark.Text = pp.cRemark;
                        try
                        {
                            ddlUser.Text = pp.AE;
                        }
                        catch (Exception)
                        {


                        }
                        txtAE.Text = pp.AE;
                        txtGuestName.Text = pp.GuestName;
                        txtGuestNo.Text = pp.GuestNo;
                        txtINSIDE.Text = pp.INSIDE;
                        txtPODate.Text = pp.PODate.ToString();
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        txtPOPayStype.Text = pp.POPayStype;
                        txtPOTotal.Text = pp.POTotal.ToString();
                        txtCG_ProNo.Text = pp.CG_ProNo;
                        ddlBusType.Text = pp.BusType;


                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        CAI_POOrdersService ordersSer = new CAI_POOrdersService();
                        List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and CAI_POOrders.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        if (pp.fileName != null && pp.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = pp.fileName;

                            if (pp.fileName.LastIndexOf('.') != -1)
                            {
                                lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                            }
                            else
                            {
                                lblAttName_Vis.Text = pp.fileName;
                            }
                        }

                        lblZhui.Text = new CG_POOrderService().IfZhuiByProNo(txtCG_ProNo.Text);
                        lblSpecial.Text = new CG_POOrderService().IfSpecialByProNo(txtCG_ProNo.Text);
                        CAI_POCaiService CaiSer = new CAI_POCaiService();
                        List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and CAI_POCai.id=" + Request["allE_id"]);
                        ViewState["Cais"] = caiList;
                        ViewState["CaisCount"] = caiList.Count;

                        gvCai.DataSource = caiList;
                        gvCai.DataBind();




                        #endregion

                        setEnable(false);

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {
                            //获取审批人
                            int pro_IDs = 0;
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

                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";
                        //string POOrdersIds = "";
                        //if (ViewState["POOrdersIds"] != null)
                        //{
                        //    POOrdersIds = ViewState["POOrdersIds"].ToString();
                        //}
                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        ViewState["currentEforms"] = eforms.Count;
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

                        int eformsCount = eformSer.GetToAduCount(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));


                        //开始---采购---合同审计—副总经理---合同复核
                        if (eformsCount == 0)
                        {
                            eformsCount = 3;
                        }
                        else if (eformsCount == 1 || eformsCount == 2)//合同审计—副总经理
                        {
                            eformsCount = 1;
                        }
                        else if (eformsCount == 3)//采购
                        {
                            eformsCount = 2;
                        }
                        else
                        {
                            eformsCount = 0;
                        }
                        //else
                        //{
                        //    eformsCount = 0;
                        //}
                        //if (eformsCount > 3)
                        //{
                        //    eformsCount = eformsCount - 3;
                        //    if (eformsCount % 4 == 1)
                        //    {
                        //        eformsCount = 0;
                        //    }
                        //    if (eformsCount % 4 == 2)
                        //    {
                        //        eformsCount = 1;
                        //    }
                        //    if (eformsCount % 4 == 3)
                        //    {
                        //        eformsCount = 2;
                        //    }
                        //    if (eformsCount % 4 == 4)
                        //    {
                        //        eformsCount = 3;
                        //    }
                        //}

                        ViewState["EformsCount"] = eformsCount;




                        #region  加载 请假单数据

                        CAI_POOrderService mainSer = new CAI_POOrderService();
                        CAI_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        cbMingYiCaiGou.Checked = pp.IsMingYiCaiGou;
                        txtName.Text = pp.LoginName;
                        txtCaiGou.Text = pp.CaiGou;
                        txtRemark.Text = pp.cRemark;
                        try
                        {
                            ddlUser.Text = pp.AE;
                        }
                        catch (Exception)
                        {


                        }
                        txtAE.Text = pp.AE;
                        txtGuestName.Text = pp.GuestName;
                        txtGuestNo.Text = pp.GuestNo;
                        txtINSIDE.Text = pp.INSIDE;
                        txtPODate.Text = pp.PODate.ToString();
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        txtPOPayStype.Text = pp.POPayStype;
                        txtPOTotal.Text = pp.POTotal.ToString();
                        txtCG_ProNo.Text = pp.CG_ProNo;
                        ddlBusType.Text = pp.BusType;

                        lblZhui.Text = new CG_POOrderService().IfZhuiByProNo(txtCG_ProNo.Text);
                        lblSpecial.Text = new CG_POOrderService().IfSpecialByProNo(txtCG_ProNo.Text);
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        CAI_POOrdersService ordersSer = new CAI_POOrdersService();
                        List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and CAI_POOrders.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        if (pp.fileName != null && pp.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = pp.fileName;

                            if (pp.fileName.LastIndexOf('.') != -1)
                            {
                                lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                            }
                            else
                            {
                                lblAttName_Vis.Text = pp.fileName;
                            }
                        }



                        CAI_POCaiService CaiSer = new CAI_POCaiService();
                        List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and CAI_POCai.id=" + Request["allE_id"]);
                        ViewState["Cais"] = caiList;
                        ViewState["CaisCount"] = caiList.Count;

                        gvCai.DataSource = caiList;
                        gvCai.DataBind();




                        #endregion
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            //ViewState["EformsCount"] = 2;
                            btnSub.Visible = false;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                            lblResult.Visible = false;
                            lblYiJian.Visible = false;
                            ddlResult.Visible = false;
                            txtResultRemark.Visible = false;

                            setEnable(false);
                            //再次编辑
                            // btnReSubEdit.Visible = true;

                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                SetRole(eformsCount);
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

                                        try
                                        {
                                            for (int i = 0; i < ddlPers.Items.Count; i++)
                                            {

                                                if (ddlPers.Items[i].Text == txtName.Text)
                                                {
                                                    ddlPers.Text = ddlPers.Items[i].Value;
                                                    break;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {


                                        }


                                    }

                                }
                                setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    SetRole(eformsCount);
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
                                            //从获取出的审核中 获取上级信息
                                            //List<A_Role_User> newList = new List<A_Role_User>();
                                            //for (int i = 0; i < roleUserList.Count; i++)
                                            //{
                                            //    if (roleUserList[i].UserId == use.ReportTo)
                                            //    {
                                            //        A_Role_User a = roleUserList[i];
                                            //        newList.Add(a);
                                            //        break;
                                            //    }
                                            //}

                                            //if (newList.Count > 0)
                                            //{
                                            //    ddlPers.DataSource = newList;
                                            //}
                                            //else
                                            //{
                                            //    ddlPers.DataSource = roleUserList;
                                            //}
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";

                                            try
                                            {

                                                for (int i = 0; i < ddlPers.Items.Count; i++)
                                                {
                                                    if (ddlPers.Items[i].Text == txtName.Text)
                                                    {
                                                        ddlPers.Text = ddlPers.Items[i].Value;


                                                        break;
                                                    }
                                                }

                                            }
                                            catch (Exception)
                                            {


                                            }
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
                List<CAI_POCai> caiOrders = ViewState["Cais"] as List<CAI_POCai>;

                if (cbMingYiCaiGou.Checked && ddlResult.Text == "通过")
                {
                    string tempSupplier = "";
                    //检查是否有选中的值
                    foreach (var m in caiOrders)
                    {
                        string resultSupplier = "";
                        int count = 0;
                        if (m.cbifDefault1 && m.Supplier != "")
                        {
                            resultSupplier = m.Supplier;
                            count++;
                        }
                        if (m.cbifDefault2 && m.Supplier1 != "")
                        {
                            resultSupplier = m.Supplier1;
                            count++;
                        }
                        if (m.cbifDefault3 && m.Supplier2 != "")
                        {
                            resultSupplier = m.Supplier2;
                            count++;
                        }

                        if (count > 0)
                        {
                            if (tempSupplier == "")
                            {
                                tempSupplier = resultSupplier;
                            }
                            if (tempSupplier != resultSupplier)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('名义采购中所有最终供应商必须一样！');</script>");
                                return;
                            }
                        }
                    }


                    if (ddlPers.Visible == false)
                    {
                        string supplier = caiOrders[0].lastSupplier;
                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.checkSupplierDoing(supplier))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请重新选择');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + supplier + "'</script>");

                            return;
                        }
                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(supplier, 1))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');window.location.href='/JXC/WFSupplierInvoice.aspx?error=" + supplier + "'</script>");

                            return;
                        }
                    }
                }
                #region 获取单据基本信息

                CAI_POOrder order = new CAI_POOrder();
                order.AppName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                order.CaiGou = txtCaiGou.Text.Trim();
                if (txtAE.Visible)
                {
                    order.AE = txtAE.Text;
                }
                else
                {
                    order.AE = ddlUser.Text;
                }

                order.GuestName = txtGuestName.Text;
                order.GuestNo = txtGuestNo.Text;
                order.INSIDE = txtINSIDE.Text;
                order.PODate = Convert.ToDateTime(txtPODate.Text);
                order.POName = txtPOName.Text.Trim();
                order.POPayStype = txtPOPayStype.Text;
                order.POTotal = Convert.ToDecimal(txtPOTotal.Text);

                order.PONo = txtPONo.Text;
                order.BusType = ddlBusType.SelectedItem.Value.ToString();
                order.CG_ProNo = txtCG_ProNo.Text;
                order.cRemark = txtRemark.Text;
                order.IsMingYiCaiGou = cbMingYiCaiGou.Checked;
                List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;


                #endregion
                if (Request["allE_id"] == null || Request["ReEdit"] != null)//单据增加+//再次编辑)
                {
                    VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                    int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
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

                    CAI_POOrderService POOrderSer = new CAI_POOrderService();
                    if (!string.IsNullOrEmpty(lblAttName_Vis.Text) && Request["Copy"] != null)
                    {
                        CAI_POOrder File = POOrderSer.GetModel_File(Convert.ToInt32(Request["Id"]));
                        if (File != null)
                        {
                            order.fileName = File.fileName;
                            order.fileNo = File.fileNo;
                            order.fileType = File.fileType;
                        }
                    }
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    //查找是否有文件
                    string fileName, fileExtension;
                    fileExtension = "";
                    HttpPostedFile postedFile = null;
                    string file = "";
                    for (int iFile = 0; iFile < files.Count; iFile++)
                    {


                        ///'检查文件扩展名字
                        postedFile = files[iFile];

                        fileName = System.IO.Path.GetFileName(postedFile.FileName);
                        if (fileName != "")
                        {
                            order.fileName = fileName;
                            fileExtension = System.IO.Path.GetExtension(fileName);
                            string fileType = postedFile.ContentType.ToString();//文件类型
                            order.fileType = fileType;
                            System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                            int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                            byte[] fileData = new Byte[fileLength];//新建一个数组
                            streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                            order.fileNo = fileData;

                            file = System.IO.Path.GetFileNameWithoutExtension(fileName);

                        }
                    }

                    if (fuAttach.Visible == false && Request["allE_id"] != null)
                    {
                        CAI_POOrder File = POOrderSer.GetModel_File(Convert.ToInt32(Request["allE_id"]));
                        if (File != null)
                        {
                            order.fileName = File.fileName;
                            order.fileNo = File.fileNo;
                            order.fileType = File.fileType;
                        }
                    }
                    int MainId = 0;
                    bool isCopy = false;
                    if (Request["Copy"] != null)
                    {
                        isCopy = true;
                    }

                    if (POOrderSer.addTran(order, eform, POOrders, caiOrders, out MainId, isCopy) > 0)
                    {
                        //提交文件
                        if (MainId > 0)
                        {

                            if (order.fileNo != null && fileExtension != "")
                            {
                                try
                                {
                                    string qizhui = System.Web.HttpContext.Current.Request.MapPath("POCAI/") + file + "_" + MainId;
                                    postedFile.SaveAs(qizhui + fileExtension);
                                }
                                catch (Exception)
                                {
                                }

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
                else//审核
                {


                    if (Request["ReAudit"] != null)
                    {
                        CAI_POOrderService POSer = new CAI_POOrderService();
                        var model = POSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        if (Session["currentUserId"].ToString() != model.AppName.ToString())
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                            return;
                        }

                        if (model != null && model.Status == "通过")
                        {

                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                            return;
                        }

                        string check = string.Format("select count(*) from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id where Status in ('通过','执行中') and caiProNo='{0}'  ", model.ProNo);


                        //string check = string.Format("select count(*) from CAI_OrderChecks  where caiProNo='{0}'", model.ProNo);
                        if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据已经被检验单使用！');</script>");
                            return;
                        }
                        check = string.Format(@"select count(*) from TB_SupplierAdvancePayments left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayments.Id=TB_SupplierAdvancePayment.Id
where caiIds in (select Ids from CAI_POCai where ID={0}) and Status<>'不通过' ", model.Id);
                        if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据已经被预付款单使用！');</script>");
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
                    int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
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
                    CAI_POOrderService POOrderSer = new CAI_POOrderService();
                    string IDS = ViewState["POOrdersIds"].ToString();

                    string cai_IDS = ViewState["CaisIds"].ToString();
                    if (Request["ReAudit"] != null)
                    {
                        //更新价格
                        string sql = string.Format("update CAI_POCai set TopPrice=lastPrice where id={0}", Request["allE_id"]);
                        DBHelp.ExeCommand(sql);

                    }

                    if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS, caiOrders, cai_IDS))
                    {
                        //如果是名义采购 并且 审核通过时  执行
                        //最后一步 自动以admin 发起一个采购单检验 ，采购入库，和支付单 流程，自动按审批流程全部走完。
                        if (cbMingYiCaiGou.Checked && ddlPers.Visible == false)
                        {
                            BatchSave(caiOrders);
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

        private void BatchSave(List<CAI_POCai> caiOrders)
        {
            DateTime createTime = DateTime.Now;
            CAI_OrderCheck orderCheck = new CAI_OrderCheck();

            orderCheck.CreatePer = 1;
            orderCheck.CheckTime = createTime;
            orderCheck.CheckRemark = "名义采购";

            VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();
            eform.appPer = 1;
            eform.appTime = createTime;
            eform.createPer = 1;
            eform.createTime = createTime;
            eform.proId = 21;//采购订单检验
            eform.state = "通过";
            eform.toPer = 0;
            eform.toProsId = 0;
            eform.E_LastTime = createTime;

            List<CAI_OrderChecks> orderChecksList = new List<CAI_OrderChecks>();
            foreach (var model in caiOrders)
            {
                CAI_OrderChecks orderChecks = new CAI_OrderChecks();
                orderChecks.PONo = txtPONo.Text;
                orderChecks.POName = txtPOName.Text.Trim();
                orderChecks.GuestName = txtGuestName.Text;
                orderChecks.CaiProNo = lblProNo.Text;
                orderChecks.SupplierName = model.lastSupplier;
                orderChecks.CheckGoodId = model.GoodId;
                orderChecks.CheckNum = model.Num.Value;
                orderChecks.CheckPrice = Convert.ToDecimal(model.lastPrice);
                orderChecks.CaiId = model.Ids;
                orderChecks.QingGou = txtCaiGou.Text.Trim();
                orderChecks.CaiGouPer = txtName.Text;
                orderChecks.CheckLastTruePrice = model.LastTruePrice;
                orderChecksList.Add(orderChecks);
            }

            CAI_OrderCheckService POOrderSer = new CAI_OrderCheckService();

            int MainId = 0;
            List<string> inhouseIds = new List<string>();

            createTime = DateTime.Now.AddSeconds(2);
            //新增检验单
            if (POOrderSer.addTran(orderCheck, eform, orderChecksList, out MainId, inhouseIds) > 0)
            {
                string ids = string.Join(",", inhouseIds.ToArray<string>());
                SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
                var orders = supplierToInvoiceSer.GetListArrayToVerify(string.Format(" CAI_OrderInHouses.ids in ({0}) and CAI_OrderInHouse.Status='通过' ", ids), ids);
                foreach (var supplierToInvoiceView in orders)
                {
                    supplierToInvoiceView.SupplierInvoiceDate = createTime;
                    supplierToInvoiceView.SupplierInvoicePrice = supplierToInvoiceView.GoodPrice;
                    supplierToInvoiceView.SupplierInvoiceTotal = supplierToInvoiceView.SupplierInvoicePrice * (supplierToInvoiceView.GoodNum ?? 0);
                    supplierToInvoiceView.ActPay = supplierToInvoiceView.SupplierInvoiceTotal;
                    //发票号
                    supplierToInvoiceView.SupplierFPNo = "00000000";
                    //发票日期
                    supplierToInvoiceView.SupplierFpDate = createTime;
                }


                TB_SupplierInvoice supplierOrder = new TB_SupplierInvoice();
                supplierOrder.CreateName = "admin";
                supplierOrder.CreteTime = createTime;
                supplierOrder.Remark = "";
                supplierOrder.SumActPay = orders.Sum(t => t.ActPay);

                eform = new tb_EForm();
                eform.appPer = 1;
                eform.appTime = createTime;
                eform.createPer = 1;
                eform.createTime = createTime;
                eform.proId = 31;//供应商付款单
                eform.state = "通过";
                eform.toPer = 0;
                eform.toProsId = 0;
                eform.E_LastTime = createTime;
                TB_SupplierInvoiceService supplierOrderSer = new TB_SupplierInvoiceService();

                tb_EForms forms = new tb_EForms();
                forms.audPer = 1;
                forms.consignor = 0;
                forms.doTime = createTime;
                forms.e_Id = 0;
                forms.idea = "无需支付，名义采购";
                forms.prosIds = 0;
                forms.resultState = ddlResult.Text;
                forms.RoleName = "系统自动审核";

                if (supplierOrderSer.addTran(supplierOrder, eform, orders, out MainId, forms) > 0)
                {

                }

            }
        }






        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


            if (Session["Orders"] != null)
            {
                List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;
                ViewState["POOrdersCount"] = POOrders.Count;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["POOrdersIds"] == null)
                {
                    ViewState["POOrdersIds"] = this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["POOrdersIds"].ToString();
                    ids += this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["POOrdersIds"] = ids;
                }
            }

            if (Session["Orders"] != null)
            {
                List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["POOrdersCount"] = POOrders.Count;

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        CAI_POOrders SumOrders = new CAI_POOrders();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");


                CAI_POOrders model = e.Row.DataItem as CAI_POOrders;


                SumOrders.CostTotal += model.CostTotal;
                SumOrders.Num += model.Num;
                SumOrders.OtherCost += model.OtherCost;
                SumOrders.SellTotal += model.SellTotal;
                SumOrders.YiLiTotal += model.YiLiTotal;
                if (string.IsNullOrEmpty(model.GoodAreaNumber))
                {
                    //var lblGoodAreaNumber = e.Row.FindControl("lblGoodAreaNumber") as Label;
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                }

            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                string val = string.Format("javascript:window.showModalDialog('CAI_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {







                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.Num.ToString());//数量




                //e.Row.Cells[7].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString();//成本单价
                setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString());//成本单价

                // e.Row.Cells[8].Text = SumOrders.CostTotal.ToString();//成本总额
                setValue(e.Row.FindControl("lblCostTotal") as Label, SumOrders.CostTotal.ToString());//成本总额


                // e.Row.Cells[9].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString();//销售单价
                setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString());//销售单价


                //e.Row.Cells[10].Text = SumOrders.SellTotal.ToString();//销售总额
                setValue(e.Row.FindControl("lblSellTotal") as Label, SumOrders.SellTotal.ToString());//销售总额


                //e.Row.Cells[11].Text = SumOrders.OtherCost.ToString();//管理费
                setValue(e.Row.FindControl("lblOtherCost") as Label, SumOrders.OtherCost.ToString());//管理费


                // e.Row.Cells[12].Text = SumOrders.YiLiTotal.ToString();//管理费
                setValue(e.Row.FindControl("lblYiLiTotal") as Label, SumOrders.YiLiTotal.ToString());//盈利总额

                if (SumOrders.SellTotal != 0)
                {
                    SumOrders.Profit = SumOrders.YiLiTotal / SumOrders.SellTotal * 100;
                }
                else if (SumOrders.YiLiTotal != 0)
                {
                    SumOrders.Profit = -100;
                }
                else
                {
                    SumOrders.Profit = 0;
                }

                //e.Row.Cells[14].Text =ConvertToObj(SumOrders.Profit).ToString();//利润
                setValue(e.Row.FindControl("lblProfit") as Label, ConvertToObj(SumOrders.Profit).ToString());//利润


            }

        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        decimal IniProfit = 0;
        decimal SumSellTotal = 0;
        CAI_POCai SumPOCai = new CAI_POCai();

        List<CAI_POOrders> POOrders = new List<CAI_POOrders>();
        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                CAI_POCai model = e.Row.DataItem as CAI_POCai;

                //获取商品的销售信息
                if (POOrders == null || POOrders.Count == 0)
                {
                    POOrders = Session["Orders"] as List<CAI_POOrders>;

                }

                if (POOrders != null)
                {
                    model.SellPrice = POOrders.Find(p => p.GoodId == model.GoodId).SellPrice;
                    (e.Row.FindControl("lblSellPrice") as Label).Text = model.SellPrice.ToString();
                }

                if (model.Total1 != null)
                {
                    if (SumPOCai.Total1 == null) SumPOCai.Total1 = 0;
                    SumPOCai.Total1 += model.Total1;
                }
                if (model.Total2 != null)
                {
                    if (SumPOCai.Total2 == null) SumPOCai.Total2 = 0;
                    SumPOCai.Total2 += model.Total2;
                }
                if (model.Total3 != null)
                {
                    if (SumPOCai.Total3 == null) SumPOCai.Total3 = 0;
                    SumPOCai.Total3 += model.Total3;
                }

                if (model.Num != null)
                {
                    if (SumPOCai.Num == null) SumPOCai.Num = 0;
                    SumPOCai.Num += model.Num;
                }

                //小计1，小计2，小计3 ，我想 还是在每一行的 最便宜的小计的 询价和小计 这两列 帮我 背景变成 淡蓝色 ，这样就能看清楚价格优势
                List<decimal?> totals = new List<decimal?>();
                totals.Add(model.Total1);
                totals.Add(model.Total2);
                totals.Add(model.Total3);
                var minTotal = totals.Min();

                if (model.Total1 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice ?? 0;// (minTotal ?? 0);
                    //(e.Row.FindControl("lblSupperPrice") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[9].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total2 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice1 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[12].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[13].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total3 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice2 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal3") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[16].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }

                //利润%放在最右面背景为淡绿色=初步利润/（销售单价*数量）
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                if (lblCaiLiRun != null && model.SellPrice != 0)
                {
                    lblCaiLiRun.Text = string.Format("{0:n2}", (model.IniProfit / (model.SellPrice * model.Num) * 100));
                }
                else
                {
                    lblCaiLiRun.Text = "0";
                }

                IniProfit = IniProfit + (model.IniProfit ?? 0);
                SumSellTotal = SumSellTotal + (model.Num ?? 0) * model.SellPrice;

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(model.IniProfit).ToString());//数量
            }
            Label lblPrice1 = e.Row.FindControl("lblSupperPrice") as Label;

            Label lblFinPrice1 = e.Row.FindControl("lblFinPrice1") as Label;


            if (lblPrice1 != null && lblFinPrice1 != null)
            {
                if (lblPrice1.Text != "" && lblFinPrice1.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice1.Text) != Convert.ToDecimal(lblFinPrice1.Text))
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }


            Label lblPrice2 = e.Row.FindControl("lblSupperPrice1") as Label;

            Label lblFinPrice2 = e.Row.FindControl("FinPrice2") as Label;


            if (lblPrice2 != null && lblFinPrice2 != null)
            {
                if (lblPrice2.Text != "" && lblFinPrice2.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice2.Text) != Convert.ToDecimal(lblFinPrice2.Text))
                    {
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }




            Label lblPrice3 = e.Row.FindControl("lblSupperPrice2") as Label;

            Label lblFinPrice3 = e.Row.FindControl("FinPrice3") as Label;


            if (lblPrice3 != null && lblFinPrice3 != null)
            {
                if (lblPrice3.Text != "" && lblFinPrice3.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice3.Text) != Convert.ToDecimal(lblFinPrice3.Text))
                    {
                        e.Row.Cells[14].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            //List<decimal> pricelMax = new List<decimal>();
            //if (lblPrice1 != null && lblPrice1.Text != "")
            //{
            //    pricelMax.Add(Convert.ToDecimal(lblPrice1.Text));
            //}

            //if (lblPrice2 != null && lblPrice2.Text != "")
            //{
            //    pricelMax.Add(Convert.ToDecimal(lblPrice2.Text));
            //}

            //if (lblPrice3 != null && lblPrice3.Text != "")
            //{
            //    pricelMax.Add(Convert.ToDecimal(lblPrice3.Text));
            //}


            //if (pricelMax.Count > 0)
            //{
            //    decimal minPrice = pricelMax.Min();
            //    decimal lirun = 0;
            //    List<CAI_POOrders> POOrders = Session["Orders"] as List<CAI_POOrders>;

            //    Label lblGoodId = e.Row.FindControl("lblGoodId") as Label;
            //    CAI_POOrders po = null;
            //    if (POOrders != null && lblGoodId != null)
            //    {
            //        po = POOrders.Find(p => p.GoodId.ToString() == lblGoodId.Text);
            //        if (po != null && po.SellTotal != 0)
            //        {
            //            lirun = ((po.SellTotal - minPrice * po.Num - po.OtherCost) / po.SellTotal) * 100;
            //        }

            //        else if (po != null)
            //        {
            //            decimal yiLiTotal = po.SellTotal - minPrice * po.Num - po.OtherCost;

            //            if (yiLiTotal != 0)
            //            {
            //                lirun = -100;
            //            }



            //        }
            //    }
            //    Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
            //    if (lblCaiLiRun != null)
            //    {
            //        lblCaiLiRun.Text = string.Format("{0:n2}", lirun);
            //        if (po.Profit != null && lirun < po.Profit.Value)
            //        {
            //            lblCaiLiRun.ForeColor = System.Drawing.Color.Red;
            //        }
            //    }

            //}
            //ImageButton btnEdit = e.Row.FindControl("lblFinPrice1") as ImageButton;
            //if (btnEdit != null)
            //{
            //    //    string val = string.Format("javascript:window.showModalDialog('WFPOCai.aspx?indexcai={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
            //    //    btnEdit.Attributes.Add("onclick", val);
            //}


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, NumHelp.FormatFour(SumPOCai.Total1 == null ? 0 : SumPOCai.Total1));//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, NumHelp.FormatFour(SumPOCai.Total2 == null ? 0 : SumPOCai.Total2));//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, NumHelp.FormatFour(SumPOCai.Total3 == null ? 0 : SumPOCai.Total3));//小计3

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(IniProfit).ToString());//数量

                if (SumSellTotal != 0)
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(IniProfit / SumSellTotal * 100).ToString());//数量
                }
                else
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(0).ToString());//数量
                }
            }
        }

        protected void gvCai_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            ViewState["index"] = index;

            if (ViewState["Cais"] != null)
            {

                List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;
                CAI_POCai model = POOrders[index];

                setValue(model);
                kucun1.Checked = false;
                kucun2.Checked = false;
                kucun3.Checked = false;

                btnSave.Enabled = true;


               
                List<TB_Good> goodList = new TB_GoodService().GetListArray_New(string.Format(" Temp.GoodId={0}", model.GoodId));
                decimal ZhiLiuKuCun = 0;
                if (goodList.Count > 0)
                {
                    lblZhiDaiNum.Text = string.Format("滞留库存数量:{0}", goodList[0].ZhiLiuKuCun);
                    ZhiLiuKuCun = goodList[0].ZhiLiuKuCun;
                }
                string sqlZhiLiu = string.Format("select isnull(sum(Num),0) as CaiIng FROM CAI_POOrder " +
                    "left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.Id where Status='执行中' and GoodId={0} and BusType=1 ", model.GoodId);
                var kcIng = Convert.ToDecimal(DBHelp.ExeScalar(sqlZhiLiu));
                lblKCIng.Text = string.Format("KC采购中数量:{0}", kcIng);

                sqlZhiLiu = string.Format(@"select 
isnull(sum(Num - isnull(totalOrderNum, 0)),0) as CaiNotCheckNum
from CAI_POCai
left join CAI_POOrder on CAI_POCai.Id = CAI_POOrder.Id
left
join
(
select CaiId, SUM(CheckNum) as totalOrderNum from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderCheck.id = CAI_OrderChecks.CheckId
where CaiId <> 0  and CAI_OrderCheck.status = '通过'
group by CaiId
)
as newtable on CAI_POCai.Ids = newtable.CaiId
where(CAI_POCai.Num > newtable.totalOrderNum or totalOrderNum is null)
and status = '通过' and lastSupplier<>'库存' and BusType = 1 and CAI_POCai.GoodId={0}", model.GoodId);
                var KCNotRuKu = Convert.ToDecimal(DBHelp.ExeScalar(sqlZhiLiu));
                lblKCNotRuKu.Text = string.Format("KC采购完成未入库数量:{0}", KCNotRuKu);

                //滞留库存数量+KC采购中数量+KC采购完成未入库数量=0
                lblSumZhiTotal.Text=( ZhiLiuKuCun + kcIng + KCNotRuKu).ToString();
                if (model.TopPrice != 0)
                {
                    lblTopPrice.Text = string.Format("上次提交价格:{0}", model.TopPrice);
                }
                var goodModel = new TB_HouseGoodsService().GetModelInfo(string.Format(" GoodId={0} ", model.GoodId));

                decimal GoodAvgPrice = 0;
                if (goodModel != null && goodModel.Count > 0)
                {
                    lblGoodMess.Text = string.Format("库存数量:{0} 库存均价:", goodModel[0].GoodNum);
                    lblGoodAvgPrice.Text = string.Format("{0:n2}", goodModel[0].GoodAvgPrice);
                    GoodAvgPrice = goodModel[0].GoodAvgPrice;
                }
                else
                {
                    lblGoodMess.Text = "库存数量:0 库存均价:";
                    lblGoodAvgPrice.Text = "0";
                }
                lblAvgPrice.Text = GoodAvgPrice.ToString();

                string sql = string.Format(@"select TOP 1 lastPrice from CAI_POOrder left join CAI_POCai on CAI_POOrder.id=CAI_POCai.id
where Status='通过' and lastPrice is not null and GoodId={0}
order by TrueCaiDate DESC", model.GoodId);
                object lastPrice = DBHelp.ExeScalar(sql);

                decimal lastPriceBijiao = 0;
                if (lastPrice != null)
                {
                    lblNextCaiPrice.Text = string.Format("采购近期价:{0}", lastPrice);
                    lblNextCaiPrice.OnClientClick = string.Format("javascript:window.open('Pro_JSXDetailInfoList.aspx?goodNo={0}','_blank'); return false;", model.GoodNo);

                    lastPriceBijiao = Convert.ToDecimal(lastPrice);
                }
                else
                {
                    lblNextCaiPrice.Text = string.Format("采购近期价:无");
                }

                lbllastPrice.Text = lastPriceBijiao.ToString();

                //XX数量  所有已经打印但是没有 最终核定的数量
                sql = string.Format(@"select sum(GoodNum) as SumGoodNum from Sell_OrderOutHouse left join 
Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
where Status='执行中'  and IsPrint=1 and GooId={0}
group by GooId", model.GoodId);
                object SumGoodNum = DBHelp.ExeScalar(sql);
                if (SumGoodNum != null)
                {
                    lblXX.Text = string.Format("锁定数量:{0}", SumGoodNum);
                }
                else
                {
                    lblXX.Text = string.Format("锁定数量:0");
                }
                //--yy=该商品所有采购单通过的总数量（非KC开头的）-该商品所有出库通过核定的总数量
                sql = string.Format(@"declare @SumGoodNum decimal(18,2);
declare @SumNum decimal(18,2); 
set @SumGoodNum=0;
set @SumNum=0; 
--全部采购完成的
select @SumNum=sum(Num)  from CAI_POCai left join CAI_POOrder
on CAI_POCai.Id=CAI_POOrder.Id 
where Status='通过' and GoodId={0} and BusType=0
group by GoodId;
--已经出库的
select @SumGoodNum=sum(GoodNum)  from Sell_OrderOutHouse left join 
Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
where Status='通过' and GooId={0}
group by GooId
select @SumNum-@SumGoodNum", model.GoodId);
                object SumGoodNum1 = DBHelp.ExeScalar(sql);
                if (SumGoodNum1 != null)
                {
                    lblYY.Text = string.Format("非KC采购在途及入库未出数:{0}", SumGoodNum1);
                }
                else
                {
                    lblXX.Text = string.Format("非KC采购在途及入库未出数:0");
                }

                //采购中数量 =正在执行的采购单的数量（含KC和非KC） ，放在 原来的在采数量 的位置
                sql = string.Format(@"declare @SumNum decimal(18,2); 
set @SumNum=0; 
--全部采购完成的
select @SumNum=sum(Num)  from CAI_POCai left join CAI_POOrder
on CAI_POCai.Id=CAI_POOrder.Id 
where Status='执行中' and GoodId={0}
group by GoodId;select @SumNum", model.GoodId);
                SumGoodNum1 = DBHelp.ExeScalar(sql);
                if (SumGoodNum1 != null)
                {
                    lblAA.Text = string.Format("采购中数量:{0}", SumGoodNum1);
                }
                else
                {
                    lblAA.Text = string.Format("采购中数量:0");
                }

                sql = string.Format(@"declare @caiTotalNum decimal(18,2);
declare @checkTotalNum decimal(18,2);
set @caiTotalNum=0;
set @checkTotalNum=0;
select @caiTotalNum=sum(Num)  
from CAI_POCai 
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
where status='通过' and GoodId={0} and lastSupplier<>'库存'
group by GoodId
select  @checkTotalNum=SUM(CheckNum) from CAI_OrderChecks left join CAI_OrderCheck on 
CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where status='通过' and CheckGoodId={0}
group by CheckGoodId
select @caiTotalNum-@checkTotalNum", model.GoodId);
                SumGoodNum1 = DBHelp.ExeScalar(sql);
                if (SumGoodNum1 != null)
                {
                    lblZZ.Text = string.Format("在途库存:{0}", SumGoodNum1);
                }
                else
                {
                    lblZZ.Text = string.Format("在途库存:0");
                }
                if (cbifDefault1.Checked == false && txtSupperPrice.Text != "" && txtFinPrice1.Text != "")
                {
                    if (txtSupperPrice.Text != txtFinPrice1.Text)
                    {
                        txtFinPrice1.Text = txtSupperPrice.Text;
                    }
                }
                //List<CAI_POOrders> Orders = Session["Orders"] as List<CAI_POOrders>;
                //gvList.DataSource = Orders;
                //gvList.DataBind();
                if (ViewState["EformsCount"] != null && ViewState["EformsCount"].ToString() == "1")//合同审核
                {
                    string checkPrice = string.Format("select Supplier,Supplier1,Supplier2 from CAI_POCai where Ids={0}", lblCaiId.Text);

                    var dt = DBHelp.getDataTable(checkPrice);
                    string Supplier = dt.Rows[0]["Supplier"].ToString();
                    string Supplier1 = dt.Rows[0]["Supplier1"].ToString();
                    string Supplier2 = dt.Rows[0]["Supplier2"].ToString();
                    if (string.IsNullOrEmpty(Supplier1))
                    {
                        txtPrice2.ReadOnly = false;
                    }
                    else
                    {
                        txtPrice2.ReadOnly = true;
                    }

                    if (string.IsNullOrEmpty(Supplier2))
                    {
                        txtPrice3.ReadOnly = false;
                    }
                    else
                    {
                        txtPrice3.ReadOnly = true;
                    }
                }


            }
            //if (Session["a"] != null)
            //{ 
            //    Session["a"] 
            //}
            //if (ViewState["Cais"] != null)
            //{
            //    List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;


            //    ViewState["CaisCount"] = POOrders.Count;
            //    gvCai.DataSource = POOrders;
            //    gvCai.DataBind();
            //}
        }

        protected void gvCai_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvCai.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["CaisIds"] == null)
                {
                    ViewState["CaisIds"] = this.gvCai.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["CaisIds"].ToString();
                    ids += this.gvCai.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["CaisIds"] = ids;
                }
            }

            if (ViewState["Cais"] != null)
            {
                List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["CaisCount"] = POOrders.Count;

                gvCai.DataSource = POOrders;
                gvCai.DataBind();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (ViewState["Cais"] != null)
            {
                List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;
                gvCai.DataSource = POOrders;
                gvCai.DataBind();
            }

        }

        protected void gvCai_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {




        }

        protected object getDatetime(object time)
        {
            if (time != null)
            {
                return Convert.ToDateTime(time).ToShortDateString();
            }
            return time;
        }
        protected void gvCai_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


        }

        protected void txtSupperPrice_TextChanged(object sender, EventArgs e)
        {




        }


        public bool check()
        {
            txtSupplier.Text = txtSupplier.Text.Trim();
            if (txtSupplier.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写采购询供应商1！');"), true);
                return false;
            }
            if (txtSupperPrice.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写采购询价1！');"), true);
                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtSupperPrice.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购询价1 格式错误！');</script>");
                    return false;
                }
            }
            txtTureSupperPrice1.Text = txtTureSupperPrice1.Text.Trim();
            txtSupperPrice.Text = txtSupperPrice.Text.Trim();
            txtFinPrice1.Text = txtFinPrice1.Text.Trim();

            txtTureSupperPrice2.Text = txtTureSupperPrice2.Text.Trim();
            txtPrice2.Text = txtPrice2.Text.Trim();
            txtFinPrice2.Text = txtFinPrice2.Text.Trim();

            txtTureSupperPrice3.Text = txtTureSupperPrice3.Text.Trim();
            txtPrice3.Text = txtPrice3.Text.Trim();
            txtFinPrice3.Text = txtFinPrice3.Text.Trim();

            if (txtTureSupperPrice1.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写实采单价1！');"), true);
                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtTureSupperPrice1.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实采单价1 格式错误！');</script>");
                    return false;
                }
            }

            int index = Convert.ToInt32(ViewState["index"]);
            List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;

            CAI_POCai model = POOrders[index];

            try
            {
                if (Convert.ToDecimal(txtSupperPrice.Text) > 0)
                {


                }
                if (Convert.ToDecimal(txtTureSupperPrice1.Text.Trim()) > 0)
                {


                }
                if (txtSupperPrice.Text != "")
                {
                    if (txtSupplier.Text != "")
                    {
                        if (ddlBusType.Text == "1" && txtSupplier.Text == "库存")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('库存采购 ，供应商就不能用库存！');"), true);
                            return false;
                        }
                        if (ddlBusType.Text == "0"&&Convert.ToDecimal(lblSumZhiTotal.Text)==0&&txtSupplier.Text=="库存")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('滞留库存数量+KC采购中数量+KC采购完成未入库数量=0，该商品允许外采！');"), true);
                            return false;
                        }
                        else if(ddlBusType.Text == "0" && Convert.ToDecimal(lblSumZhiTotal.Text) != 0 && txtSupplier.Text != "库存")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('滞留库存数量+KC采购中数量+KC采购完成未入库数量<>0，该商品只能出库存！');"), true);
                            return false;
                        }
                    }
                    if (txtSupplier.Text == "库存" && kucun1.Visible && kucun1.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('库存请选择 打勾！');"), true);
                        return false;
                    }
                    //2.     如果供应商是 库存，实采单价 和采购单价 提交时，自动 都= 库存单价
                    //if (txtSupplier.Text == "库存")
                    //{
                    //    //检查库存是否存在
                    //    //string goodAvgPriceSQL = "select GoodAvgPrice from TB_HouseGoods where GoodId=" + model.GoodId;
                    //    //var goodHousePrice = DBHelp.ExeScalar(goodAvgPriceSQL);
                    //    //if ((goodHousePrice is DBNull) || goodHousePrice == null)
                    //    //{
                    //    //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('当前商品库存不存在 ！');</script>"));
                    //    //    return false;
                    //    //}
                    //    //txtSupperPrice.Text = goodHousePrice.ToString();
                    //    //txtTureSupperPrice1.Text = goodHousePrice.ToString();
                    //}
                    //else
                    //{
                    //    //3.如果该产品 有库存数量(>=1)，填入的供应商只要有 不是 库存的，提交时，提示“该产品有库存，请重新填写”，返回原界面
                    //    //检查库存是否存在
                    //    string goodNumSQL = "select GoodNum from TB_HouseGoods where GoodId=" + model.GoodId;
                    //    var goodHouseNum = DBHelp.ExeScalar(goodNumSQL);
                    //    if (!((goodHouseNum is DBNull) || goodHouseNum == null))
                    //    {
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该产品有库存，请重新填写!');"), true);
                    //        if (ddlBusType.Text == "0")
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //}

                    //需要判断 如果 采购商品是不含税的，实采单价 和采购询价的值 一旦一样，汉中和我 在提交时 都需要 提示 “价格有可能有问题，请核对”，返回原界面。
                    if (txtSupplier.Text != "库存")
                    {  //1.     如果供应商是 特殊供应商（非 库存），
                        //你需要判断：如果实采单价>= 采购单价 ， 提交时，出现弹出窗口，显示“xx供应商是不含税供应商，实采单价 一定<采购单价，请核对！”，返回原界面

                        if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupplier.Text)) > 0)
                        {
                            if (Convert.ToDecimal(txtSupperPrice.Text) <= Convert.ToDecimal(txtTureSupperPrice1.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupplier.Text), true);
                                return false;
                            }
                            if (txtFinPrice1.Visible && !string.IsNullOrEmpty(txtFinPrice1.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice1.Text) <= Convert.ToDecimal(txtTureSupperPrice1.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupplier.Text), true);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtSupperPrice.Text) < Convert.ToDecimal(txtTureSupperPrice1.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                return false;
                            }
                            if (txtFinPrice1.Visible && !string.IsNullOrEmpty(txtFinPrice1.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice1.Text) < Convert.ToDecimal(txtTureSupperPrice1.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                    return false;
                                }
                            }
                        }
                    }
                    if (cbIsHanShui.Checked == false && txtSupplier.Text != "库存")
                    {
                        //if (ddlPers.Visible)
                        //{

                        //    if (Convert.ToDecimal(txtSupperPrice.Text) == Convert.ToDecimal(txtTureSupperPrice1.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }

                        //}
                        //else
                        //{
                        //    if (Convert.ToDecimal(txtFinPrice1.Text) == Convert.ToDecimal(txtTureSupperPrice1.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }
                        //}
                    }
                }

                if (txtPrice2.Text != "")
                {
                    Convert.ToDecimal(txtPrice2.Text);
                    Convert.ToDecimal(txtTureSupperPrice2.Text);
                    if (txtSupper2.Text == "库存" && kucun2.Visible && kucun2.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('库存请选择 打勾！');"), true);
                        return false;
                    }
                    //2.     如果供应商是 库存，实采单价 和采购单价 提交时，自动 都= 库存单价
                    if (txtSupper2.Text == "库存")
                    {
                        ////检查库存是否存在
                        //string goodAvgPriceSQL = "select GoodAvgPrice from TB_HouseGoods where GoodId=" + model.GoodId;
                        //var goodHousePrice = DBHelp.ExeScalar(goodAvgPriceSQL);
                        //if ((goodHousePrice is DBNull) || goodHousePrice == null)
                        //{
                        //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('当前商品库存不存在 ！');</script>"));
                        //    return false;
                        //}
                        //txtPrice2.Text = goodHousePrice.ToString();
                        //txtTureSupperPrice2.Text = goodHousePrice.ToString();
                    }
                    else
                    {
                        //3.如果该产品 有库存数量(>=1)，填入的供应商只要有 不是 库存的，提交时，提示“该产品有库存，请重新填写”，返回原界面
                        //检查库存是否存在
                        //string goodNumSQL = "select GoodNum from TB_HouseGoods where GoodId=" + model.GoodId;
                        //var goodHouseNum = DBHelp.ExeScalar(goodNumSQL);
                        //if (!((goodHouseNum is DBNull) || goodHouseNum == null))
                        //{
                        //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该产品有库存，请重新填写!');"), true);
                        //    if (ddlBusType.Text == "0")
                        //    {
                        //        return false;
                        //    }
                        //}
                    }
                    //需要判断 如果 采购商品是不含税的，实采单价 和采购询价的值 一旦一样，汉中和我 在提交时 都需要 提示 “价格有可能有问题，请核对”，返回原界面。
                    if (txtSupper2.Text != "库存")
                    {
                        //1.     如果供应商是 特殊供应商（非 库存），
                        //你需要判断：如果实采单价>= 采购单价 ， 提交时，出现弹出窗口，显示“xx供应商是不含税供应商，实采单价 一定<采购单价，请核对！”，返回原界面
                        if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper2.Text)) > 0)
                        {
                            if (Convert.ToDecimal(txtPrice2.Text) <= Convert.ToDecimal(txtTureSupperPrice2.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupper2.Text), true);
                                return false;
                            }
                            if (txtFinPrice2.Visible && !string.IsNullOrEmpty(txtFinPrice2.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice2.Text) <= Convert.ToDecimal(txtTureSupperPrice2.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupper2.Text), true);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtPrice2.Text) < Convert.ToDecimal(txtTureSupperPrice2.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                return false;
                            }
                            if (txtFinPrice2.Visible && !string.IsNullOrEmpty(txtFinPrice2.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice2.Text) < Convert.ToDecimal(txtTureSupperPrice2.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                    return false;
                                }
                            }
                        }
                    }
                    if (cbIsHanShui.Checked == false && txtSupper2.Text != "库存")
                    {
                        //if (ddlPers.Visible)
                        //{
                        //    if (Convert.ToDecimal(txtPrice2.Text) == Convert.ToDecimal(txtTureSupperPrice2.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }

                        //}
                        //else
                        //{
                        //    if (Convert.ToDecimal(txtFinPrice2.Text) == Convert.ToDecimal(txtTureSupperPrice2.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }
                        //}
                    }
                }

                if (txtPrice3.Text != "")
                {
                    Convert.ToDecimal(txtPrice3.Text);
                    Convert.ToDecimal(txtTureSupperPrice3.Text);
                    if (txtSupper3.Text == "库存" && kucun3.Visible && kucun3.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('库存请选择 打勾！');"), true);
                        return false;
                    }
                    //2.     如果供应商是 库存，实采单价 和采购单价 提交时，自动 都= 库存单价
                    if (txtSupper3.Text == "库存")
                    {
                        ////检查库存是否存在
                        //string goodAvgPriceSQL = "select GoodAvgPrice from TB_HouseGoods where GoodId=" + model.GoodId;
                        //var goodHousePrice = DBHelp.ExeScalar(goodAvgPriceSQL);
                        //if ((goodHousePrice is DBNull) || goodHousePrice == null)
                        //{
                        //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('当前商品库存不存在 ！');</script>"));
                        //    return false;
                        //}
                        //txtPrice3.Text = goodHousePrice.ToString();
                        //txtTureSupperPrice3.Text = goodHousePrice.ToString();
                    }
                    else
                    {
                        //3.如果该产品 有库存数量(>=1)，填入的供应商只要有 不是 库存的，提交时，提示“该产品有库存，请重新填写”，返回原界面
                        //检查库存是否存在
                        //string goodNumSQL = "select GoodNum from TB_HouseGoods where GoodId=" + model.GoodId;
                        //var goodHouseNum = DBHelp.ExeScalar(goodNumSQL);
                        //if (!((goodHouseNum is DBNull) || goodHouseNum == null))
                        //{
                        //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该产品有库存，请重新填写!');"), true);
                        //    if (ddlBusType.Text == "0")
                        //    {
                        //        return false;
                        //    }
                        //}
                    }
                    //需要判断 如果 采购商品是不含税的，实采单价 和采购询价的值 一旦一样，汉中和我 在提交时 都需要 提示 “价格有可能有问题，请核对”，返回原界面。
                    if (txtSupper3.Text != "库存")
                    {
                        //1.     如果供应商是 特殊供应商（非 库存），
                        //你需要判断：如果实采单价>= 采购单价 ， 提交时，出现弹出窗口，显示“xx供应商是不含税供应商，实采单价 一定<采购单价，请核对！”，返回原界面
                        if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper3.Text)) > 0)
                        {
                            if (Convert.ToDecimal(txtPrice3.Text) <= Convert.ToDecimal(txtTureSupperPrice3.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupper3.Text), true);
                                return false;
                            }
                            if (txtFinPrice3.Visible && !string.IsNullOrEmpty(txtFinPrice3.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice3.Text) <= Convert.ToDecimal(txtTureSupperPrice3.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('[{0}]供应商是不含税供应商，实采单价 一定<采购单价，请核对！');", txtSupper3.Text), true);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtPrice3.Text) < Convert.ToDecimal(txtTureSupperPrice3.Text))
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                return false;
                            }
                            if (txtFinPrice3.Visible && !string.IsNullOrEmpty(txtFinPrice3.Text))
                            {
                                if (Convert.ToDecimal(txtFinPrice3.Text) < Convert.ToDecimal(txtTureSupperPrice3.Text))
                                {
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('实采单价 一定<=采购单价，请核对！');"), true);
                                    return false;
                                }
                            }
                        }

                    }
                    if (cbIsHanShui.Checked == false && txtSupper3.Text != "库存")
                    {
                        //if (ddlPers.Visible)
                        //{

                        //    if (Convert.ToDecimal(txtPrice3.Text) == Convert.ToDecimal(txtTureSupperPrice3.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }

                        //}
                        //else
                        //{
                        //    if (Convert.ToDecimal(txtFinPrice3.Text) == Convert.ToDecimal(txtTureSupperPrice3.Text))
                        //    {
                        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有可能有问题，请核对！');"), true);
                        //        return false;
                        //    }
                        //}
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('你填写的单价格式有误！');"), true);
                return false;
            }

            if (cbifDefault1.Visible == true)
            {
                int count = 0;
                if (txtSupplier.Text != "" && cbifDefault1.Checked)
                {
                    count++;
                }
                if (txtSupper2.Text != "" && cbifDefault2.Checked)
                {
                    count++;
                }
                if (txtSupper3.Text != "" && cbifDefault3.Checked)
                {
                    count++;
                }

                if (count != 1)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请设置一个最终供应商！');"), true);
                    return false;
                }
                if (cbifDefault1.Checked)
                {
                    if (txtSupplier.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称！');"), true);
                        return false;
                    }

                    if (txtSupperPrice.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称商品单价！');"), true);
                        return false;
                    }
                }
                if (cbifDefault2.Checked)
                {

                    if (txtSupper2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称！');"), true);
                        return false;
                    }

                    if (txtPrice2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称商品单据！');"), true);
                        return false;
                    }
                }
                if (cbifDefault3.Checked)
                {
                    if (txtSupper3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称！');"), true);
                        return false;
                    }

                    if (txtPrice3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写最终供应商名称商品单据！');"), true);
                        return false;
                    }
                }
            }
            if (cbIsHanShui.Checked && dllFPstye.SelectedItem.Text == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择发票类型！');"), true);
                return false;
            }
            return true;
        }
        private void setValue(CAI_POCai model)
        {
            lblCaiId.Text = model.Ids.ToString();

            cbifDefault1.Checked = model.cbifDefault1;
            cbifDefault2.Checked = model.cbifDefault2;
            cbifDefault3.Checked = model.cbifDefault3;


            if (model.SupperPrice != null)
            {
                txtSupperPrice.Text = string.Format("{0:f3}", model.SupperPrice.Value);
                if (model.FinPrice1 != null && model.FinPrice1.Value > 0)
                {
                    txtFinPrice1.Text = string.Format("{0:f3}", model.FinPrice1.Value);
                }
                if (model.TruePrice1 == 0)
                {
                    txtTureSupperPrice1.Text = string.Format("{0:f3}", model.SupperPrice.Value);
                }
                else
                {
                    txtTureSupperPrice1.Text = string.Format("{0:f3}", model.TruePrice1);
                }
            }
            else
            {
                txtSupperPrice.Text = "";
            }

            if (model.Total1 != null)
                txtTotal1.Text = string.Format("{0:f4}", model.Total1);
            else
            {
                txtTotal1.Text = "";
            }




            if (model.SupperPrice1 != null)
            {
                txtPrice2.Text = string.Format("{0:f3}", model.SupperPrice1.Value);

                if (model.FinPrice2 != null && model.FinPrice2.Value > 0)
                {
                    txtFinPrice2.Text = string.Format("{0:f3}", model.FinPrice2.Value);
                }
                if (model.TruePrice2 == 0)
                {
                    txtTureSupperPrice2.Text = string.Format("{0:f3}", model.SupperPrice1.Value);
                }
                else
                {
                    txtTureSupperPrice2.Text = string.Format("{0:f3}", model.TruePrice2);
                }
            }
            else
            {
                txtPrice2.Text = "";
            }

            if (model.Total2 != null)
                txtTotal2.Text = string.Format("{0:f4}", model.Total2);
            else
            {
                txtTotal2.Text = "";
            }

            if (model.SupperPrice2 != null)
            {
                txtPrice3.Text = string.Format("{0:f3}", model.SupperPrice2.Value);
                if (model.FinPrice3 != null && model.FinPrice3.Value > 0)
                {
                    txtFinPrice3.Text = string.Format("{0:f3}", model.FinPrice3.Value);
                }
                if (model.TruePrice3 == 0)
                {
                    txtTureSupperPrice3.Text = string.Format("{0:f3}", model.SupperPrice2.Value);
                }
                else
                {
                    txtTureSupperPrice3.Text = string.Format("{0:f3}", model.TruePrice3);
                }
            }
            else
            {
                txtPrice3.Text = "";
            }
            if (model.Total3 != null)
                txtTotal3.Text = string.Format("{0:f4}", model.Total3);
            else
            {
                txtTotal3.Text = "";
            }

            txtSupper2.Text = model.Supplier1;
            txtSupper3.Text = model.Supplier2;
            //txtInvName.Text = model.InvName;
            //txtGuestName.Text = model.GuestName;
            txtNum.Text = model.Num.Value.ToString();

            //if (model.CaiTime != null)
            //    txtCaiTime.Text = model.CaiTime.Value.ToShortDateString();

            txtIdea.Text = model.Idea;
            //txtUpdateUser.Text = Session["LoginName"].ToString();

            txtSupplier.Text = model.Supplier;

            //“特殊”供应商
            if (txtSupplier.Text != "" && (int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupplier.Text)) > 0)
            {
                //cbIsHanShui.Enabled = false;
                txtSupplier.BackColor = System.Drawing.Color.LightGray;
            }
            else
            {
                //cbIsHanShui.Enabled = true;
                txtSupplier.BackColor = System.Drawing.Color.White;
            }
            if (txtSupper2.Text != "" && (int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper2.Text)) > 0)
            {

                //cbIsHanShui.Enabled = false;
                txtSupper2.BackColor = System.Drawing.Color.LightGray;

            }
            else
            {
                //if (txtSupper2.Text != "")
                //{
                //    cbIsHanShui.Enabled = true;
                //}
                txtSupper2.BackColor = System.Drawing.Color.White;
            }
            if (txtSupper3.Text != "" && (int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper3.Text)) > 0)
            {

                //cbIsHanShui.Enabled = false;
                txtSupper3.BackColor = System.Drawing.Color.LightGray;
            }
            else
            {
                //if (txtSupper3.Text != "")
                //{
                //    cbIsHanShui.Enabled = true;
                //}
                txtSupper3.BackColor = System.Drawing.Color.White;
            }

            cbIsHanShui.Checked = model.IsHanShui;
            dllFPstye.Enabled = model.IsHanShui;
            //dllFPstye.SelectedItem.Text = model.CaiFpType;
            foreach (ListItem m in dllFPstye.Items)
            {
                if (m.Text == model.CaiFpType)
                {
                    dllFPstye.Text = m.Value;
                    break;
                }
            }

            lblInvDetail.Text = string.Format(@"{0}\{1}\{2}\{3}\{4} ", model.GoodName, model.GoodTypeSmName, model.GoodSpec, model.Good_Model, model.GoodUnit);

            if (ViewState["EformsCount"] != null)
            {
                if (ViewState["EformsCount"].ToString() == "2")
                {
                    if (model.FinPrice1 == null)
                        txtFinPrice2.Text = txtPrice2.Text;
                    else
                    {
                        txtFinPrice1.Text = string.Format("{0:f3}", model.FinPrice1);
                    }

                    if (model.FinPrice2 == null)
                        txtFinPrice3.Text = txtPrice3.Text;
                    else
                    {
                        txtFinPrice2.Text = string.Format("{0:f3}", model.FinPrice2);
                    }

                    if (model.FinPrice1 == null)
                        txtFinPrice1.Text = txtSupperPrice.Text;
                    else
                    {
                        txtFinPrice3.Text = string.Format("{0:f3}", model.FinPrice3);
                    }
                }
            }
            if (ddlPers.Visible == false)
            {
                if (txtSupplier.Text == "库存")
                {
                    txtFinPrice1.Enabled = false;
                    txtTureSupperPrice1.Enabled = false;
                    txtSupperPrice.Enabled = false;
                }
                else
                {
                    txtFinPrice1.Enabled = true;
                    txtTureSupperPrice1.Enabled = true;
                    txtSupperPrice.Enabled = true;
                }
                if (txtSupper2.Text == "库存")
                {
                    txtFinPrice2.Enabled = false;
                    txtTureSupperPrice2.Enabled = false;
                    txtPrice2.Enabled = false;
                }
                else
                {
                    txtFinPrice2.Enabled = true;
                    txtTureSupperPrice2.Enabled = true;
                    txtPrice2.Enabled = true;
                }
                if (txtSupper3.Text == "库存")
                {
                    txtFinPrice2.Enabled = false;
                    txtTureSupperPrice2.Enabled = false;
                    txtPrice3.Enabled = false;
                }
                else
                {
                    txtFinPrice2.Enabled = true;
                    txtTureSupperPrice2.Enabled = true;
                    txtPrice3.Enabled = true;
                }
            }
        }

        private void Check(decimal price)
        {
            decimal avgPrice = Convert.ToDecimal(lblAvgPrice.Text);
            decimal lastPrice = Convert.ToDecimal(lbllastPrice.Text);
            if (avgPrice != 0 && (price < avgPrice * Convert.ToDecimal(0.9) || price > avgPrice * Convert.ToDecimal(1.1)))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('警告:采购单价 可能有误！');"), true);

                //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('警告:采购单价 可能有误！');</script>");
            }
            else if (lastPrice != 0 && (price < lastPrice * Convert.ToDecimal(0.9) || price > lastPrice * Convert.ToDecimal(1.1)))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('警告:采购单价 可能有误！');"), true);

                //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('警告:采购单价 可能有误！');</script>");
            }


        }

        private int ExistsSupplier(string name)
        {
            string sql = string.Format("select top 1 IsUse from TB_SupplierInfo where  Status='通过' and SupplieSimpeName='{0}'",name);
            var result= DBHelp.ExeScalar(sql);
            if (result == null|| result is DBNull)
            {
                return -1;
            }
            if (!Convert.ToBoolean(result))
            {
                return -2;
            }
            return 0;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (check() == false)
            {
                return;
            }

            //判断供应商 是否存在
            if (txtSupplier.Text != "" && txtSupplier.Text != "库存")
            {
                var result = ExistsSupplier(txtSupplier.Text);
                if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('无此供应商，不能保存！');"), true);
                    return;
                }
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('不显示供应商，不能保存！');"), true);
                    return;
                }
            }
            
            if (txtSupper2.Text != "" && txtSupper2.Text != "库存")
            {
                var result = ExistsSupplier(txtSupper2.Text);
                if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('无此供应商，不能保存！');"), true);
                    return;
                }
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('不显示供应商，不能保存！');"), true);
                    return;
                }
            }
            if (txtSupper3.Text != "" && txtSupper3.Text != "库存")
            {
                var result = ExistsSupplier(txtSupper3.Text);
                if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('无此供应商，不能保存！');"), true);
                    return;
                }
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                   string.Format("alert('不显示供应商，不能保存！');"), true);
                    return;
                }
            }
            if (ViewState["currentEforms"] != null && ViewState["currentEforms"].ToString() == "0")
            {
                if (txtFinPrice1.Text != "")
                {
                    txtFinPrice1.Text = txtSupperPrice.Text;
                }
                if (txtFinPrice2.Text != "")
                {
                    txtFinPrice2.Text = txtPrice2.Text;
                }
                if (txtFinPrice3.Text != "")
                {
                    txtFinPrice3.Text = txtPrice3.Text;
                }
            }
            CAI_POCai s = new CAI_POCai();
            s.CaiTime = null;
            s.Idea = txtIdea.Text;
            s.SupperPrice = Convert.ToDecimal(txtSupperPrice.Text);
            s.TruePrice1 = Convert.ToDecimal(txtTureSupperPrice1.Text);

            s.Supplier = txtSupplier.Text;
            s.UpdateUser = Session["LoginName"].ToString();
            s.Num = Convert.ToDecimal(txtNum.Text);
            s.Total1 = s.SupperPrice * s.Num.Value;

            s.Num = Convert.ToDecimal(txtNum.Text);
            //s.InvName = txtInvName.Text;
            s.GuestName = txtGuestName.Text;


            s.Supplier1 = txtSupper2.Text;
            s.Supplier2 = txtSupper3.Text;

            s.cbifDefault1 = cbifDefault1.Checked;
            s.cbifDefault2 = cbifDefault2.Checked;
            s.cbifDefault3 = cbifDefault3.Checked;



            s.IsHanShui = cbIsHanShui.Checked;
            if (cbIsHanShui.Checked)
            {
                s.CaiFpType = dllFPstye.SelectedItem.Text;
            }
            if (txtPrice2.Text != "")
            {
                s.SupperPrice1 = Convert.ToDecimal(txtPrice2.Text);

                s.TruePrice2 = Convert.ToDecimal(txtTureSupperPrice2.Text);
                s.Total2 = s.SupperPrice1.Value * s.Num.Value;
            }


            if (txtPrice3.Text != "")
            {
                s.SupperPrice2 = Convert.ToDecimal(txtPrice3.Text);
                s.TruePrice3 = Convert.ToDecimal(txtTureSupperPrice3.Text);
                s.Total3 = s.SupperPrice2.Value * s.Num.Value;
            }


            if (txtFinPrice1.Text != "")
            {
                s.FinPrice1 = Convert.ToDecimal(txtFinPrice1.Text);
                s.Total1 = s.FinPrice1.Value * s.Num.Value;
            }

            if (txtFinPrice2.Text != "")
            {
                s.FinPrice2 = Convert.ToDecimal(txtFinPrice2.Text);
                s.Total2 = s.FinPrice2.Value * s.Num.Value;
            }

            if (txtFinPrice3.Text != "")
            {
                s.FinPrice3 = Convert.ToDecimal(txtFinPrice3.Text);
                s.Total3 = s.FinPrice3.Value * s.Num.Value;
            }
            if (s.cbifDefault1 && s.FinPrice1 == null || s.cbifDefault2 && s.FinPrice2 == null ||
                s.cbifDefault3 && s.FinPrice3 == null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                 string.Format("alert('请填写确认价！');"), true);
                return;
            }
            if (txtFinPrice1.Visible && s.SupperPrice < s.FinPrice1)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                    string.Format("alert('确认价格高于询价，请改正！');"), true);
                return;
            }
            if (txtFinPrice2.Visible && s.SupperPrice1 < s.FinPrice2)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                    string.Format("alert('确认价格高于询价，请改正！');"), true);
                return;
            }
            if (txtFinPrice3.Visible && s.SupperPrice2 < s.FinPrice3)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a",
                    string.Format("alert('确认价格高于询价，请改正！');"), true);
                return;
            }
            if (ViewState["EformsCount"] != null && ViewState["EformsCount"].ToString() == "1")//合同审核
            {
                decimal finPrice = 0;
                if (txtFinPrice1.Text != "")
                {
                    finPrice = Convert.ToDecimal(txtFinPrice1.Text);
                }
                //1.彩蓉可以修改采购询价和实采单价，但价格只能比汉中提上来的价格
                //低，否则，系统提示 “价格有误，请修正”

                //2.彩蓉也可以新增供应商，填写采购询价和实采单价，但这两个价格也
                //必须比原来的汉中提上来的价格要低，否则，系统提示 “价格有误，请修正”
                string checkPrice = string.Format("select Supplier,Supplier1,Supplier2,SupperPrice,TruePrice1 from CAI_POCai where Ids={0}", lblCaiId.Text);

                var dt = DBHelp.getDataTable(checkPrice);
                string Supplier1 = dt.Rows[0]["Supplier1"].ToString();
                string Supplier2 = dt.Rows[0]["Supplier2"].ToString();

                if (txtSupplier.Text != "" && txtSupper3.Text == "" && txtSupper2.Text == "")
                {
                    decimal TopTruePrice = Convert.ToDecimal(dt.Rows[0]["TruePrice1"]);
                    decimal TopSupplierPrice = Convert.ToDecimal(dt.Rows[0]["SupperPrice"]);
                    if (finPrice > TopSupplierPrice || s.TruePrice1 > TopTruePrice)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有误，请修正,必须比原来的提上来的价格({0}/{1})要低！');", TopTruePrice, TopSupplierPrice), true);
                        return;
                    }
                    if (finPrice < TopSupplierPrice || s.TruePrice1 < TopTruePrice)
                    {
                        s.TopSupplierPrice = TopSupplierPrice;
                        s.TopTruePrice = TopTruePrice;
                    }
                    else
                    {
                        s.TopSupplierPrice = 0;
                        s.TopTruePrice = 0;
                    }
                }
                //蔡蓉 新增了供应商2
                if (Supplier1 == "" && txtSupper2.Text != "")
                {
                    if (s.FinPrice2 > s.FinPrice1 || s.TruePrice2 > s.TruePrice1)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有误，请修正,必须比小于最低价格({0}/{1})！');",
                            s.TruePrice1, s.FinPrice1), true);
                        return;
                    }
                }
                //彩容新增了供应商3
                if (Supplier2 == "" && txtSupper3.Text != "")
                {
                    decimal minTruePrice = s.TruePrice1 > s.TruePrice2 ? s.TruePrice2 : s.TruePrice1;
                    decimal? minFinPrice = s.FinPrice1 > s.FinPrice2 ? s.FinPrice1 : s.FinPrice2;

                    if (s.FinPrice3 > minFinPrice || s.TruePrice3 > minTruePrice)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('价格有误，请修正,必须比小于最低价格({0}/{1})！');",
                            minTruePrice, minFinPrice), true);
                        return;
                    }
                }

            }

            //如果此次最低的采购单价  和 库存 单价 相差 超过10%，或  和 采购近期价 相差 超过 10%，
            //在含税  边上  用红色字体（比正常字 大一号）显示：采购单价 可能有误！
            if (cbifDefault1.Checked && s.FinPrice1.HasValue)
            {
                Check(s.FinPrice1.Value);
            }
            if (cbifDefault2.Checked && s.FinPrice2.HasValue)
            {
                Check(s.FinPrice2.Value);
            }
            if (cbifDefault3.Checked && s.FinPrice3.HasValue)
            {
                Check(s.FinPrice3.Value);
            }

            if (ddlPers.Visible)
            {
                if (txtSupperPrice.Text != "")
                {
                    Check(Convert.ToDecimal(txtSupperPrice.Text));
                }

                if (txtPrice2.Text != "")
                {
                    Check(Convert.ToDecimal(txtPrice2.Text));
                }

                if (txtPrice3.Text != "")
                {
                    Check(Convert.ToDecimal(txtPrice3.Text));
                }
            }

            //修改
            if (ViewState["index"] != null)
            {
                int index = Convert.ToInt32(ViewState["index"]);
                if (ViewState["Cais"] != null)
                {
                    s.UpdateUser = Session["LoginName"].ToString();
                    List<CAI_POCai> POOrders = ViewState["Cais"] as List<CAI_POCai>;

                    CAI_POCai model = POOrders[index];
                    CAI_POCai newSche = s;
                    s.Ids = model.Ids;
                    s.GoodId = model.GoodId;
                    s.GoodName = model.GoodName;
                    s.GoodNo = model.GoodNo;
                    s.GoodSpec = model.GoodSpec;
                    s.GoodTypeSmName = model.GoodTypeSmName;
                    s.GoodUnit = model.GoodUnit;

                    newSche.IfUpdate = true;
                    POOrders[index] = newSche;
                    ViewState["Cais"] = POOrders;
                    gvCai.DataSource = POOrders;
                    gvCai.DataBind();
                    btnSave.Enabled = false;
                }
            }
        }

        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("POCAI/") + lblAttName_Vis.Text;
            down1(lblAttName.Text, url);
        }

        private void down1(string fileName, string url)
        {
            if (System.IO.File.Exists(url) == false)
            {
                try
                {
                    #region 文件恢复
                    int Id = Convert.ToInt32(Request["allE_id"]);
                    CAI_POOrderService mainSer = new CAI_POOrderService();
                    CAI_POOrder model = mainSer.GetModel_File(Id);

                    MemoryStream ms = new MemoryStream(model.fileNo);

                    using (FileStream fs = new FileStream(url, FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(ms.ToArray());
                        bw.Close();
                    }
                    #endregion
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                    return;
                }

            }
            string filePath = url;//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 1024 * 500;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
            }


        }

        protected void btnReSubEdit_Click(object sender, EventArgs e)
        {
            if (Request["ProId"] != null && Request["allE_id"] != null && Request["EForm_Id"] != null)
            {
                string url = "~/JXC/CAI_Order.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" + Request["EForm_Id"] + "&&ReEdit=true";
                Response.Redirect(url);
            }
        }

        private void BusTypeChanged()
        {
            if (ddlBusType.SelectedValue == "1")//库存采购
            {
                ddlUser.Visible = true;
                txtAE.Visible = false;
                txtCG_ProNo.Text = "";
                txtPONo.Text = "";
                txtPONo.Enabled = false;

                txtAE.Enabled = false;
                txtGuestName.Enabled = true;
                txtGuestNo.Enabled = false;
                txtINSIDE.Enabled = false;
                txtAE.Text = "";
                txtGuestName.Text = "";
                txtGuestNo.Text = "";
                txtINSIDE.Text = "";

                txtPODate.Enabled = true;

                txtPOTotal.Text = "0";
                txtPOPayStype.Text = "0";

                txtPOName.Enabled = true;
                txtPOPayStype.Enabled = true;
                txtPOTotal.Enabled = true;
                txtPODate.Enabled = true;

                txtCG_ProNo.Enabled = false;

                lbtnAddFiles.Visible = true;
                lbtnPoList.Visible = false;

                txtPODate.ReadOnly = false;
                txtPOName.ReadOnly = false;
                txtPOTotal.ReadOnly = false;
                txtPOPayStype.ReadOnly = false;


            }
            else
            {
                ddlUser.Visible = false;
                txtAE.Visible = true;
                txtCG_ProNo.Text = "";
                txtPONo.Text = "";
                txtAE.Text = "";
                txtGuestName.Text = "";
                txtGuestNo.Text = "";
                txtINSIDE.Text = "";
                txtPOTotal.Text = "0";
                txtPOPayStype.Text = "0";
                List<CAI_POOrders> POOrders = new List<CAI_POOrders>();
                Session["Orders"] = POOrders;
                gvCai.DataSource = POOrders;
                gvCai.DataBind();
                txtPONo.Enabled = true;

                txtCG_ProNo.Enabled = true;

                txtAE.Enabled = false;
                txtGuestName.Enabled = false;
                txtGuestNo.Enabled = false;
                txtINSIDE.Enabled = false;
                txtPOName.Enabled = false;
                txtPOPayStype.Enabled = false;
                txtPOTotal.Enabled = false;
                txtPODate.Enabled = false;

                lbtnAddFiles.Visible = false;
                lbtnPoList.Visible = true;
            }
        }
        protected void ddlBusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusTypeChanged();
        }

        Dal.JXC.CG_POOrdersService poOrderSer = new CG_POOrdersService();
        Dal.JXC.CG_POOrderService MyPoOrderSer = new CG_POOrderService();
        protected void txtPONo_TextChanged(object sender, EventArgs e)
        {
            GetPOInfo();
        }

        public void GetPOInfo()
        {
            if (ddlBusType.SelectedValue == "0")
            {

                List<Model.JXC.CG_POOrder> poModel = MyPoOrderSer.GetListArrayToQuery(string.Format(" 1=1 AND  proNo='{0}'", txtCG_ProNo.Text));
                if (poModel.Count > 0)
                {
                    txtAE.Text = poModel[0].AE;
                    txtGuestName.Text = poModel[0].GuestName;
                    txtGuestNo.Text = poModel[0].GuestNo;
                    txtINSIDE.Text = poModel[0].INSIDE;
                    txtPOName.Text = poModel[0].POName;
                    txtPOPayStype.Text = poModel[0].POPayStype;
                    txtPOTotal.Text = poModel[0].POTotal.ToString();
                    txtPODate.Text = poModel[0].PODate.ToString();
                    txtPONo.Text = poModel[0].PONo;

                }
                else
                {
                    txtAE.Text = "";
                    txtGuestName.Text = "";
                    txtGuestNo.Text = "";
                    txtINSIDE.Text = "";
                    txtPOName.Text = "";
                    txtPOPayStype.Text = "";
                    txtPOTotal.Text = "";
                    txtPODate.Text = "";
                }

                List<Model.JXC.CG_POOrders> POOrdersList = poOrderSer.GetListCG_POOrders_Cai_POOrders_View(string.Format(" 1=1 and CG_POOrders_Cai_POOrders_View.id in (select id from CG_POOrder where ProNo='{0}')", txtCG_ProNo.Text));

                List<Model.JXC.CAI_POOrders> MyOrdersList = new List<CAI_POOrders>();
                foreach (var m in POOrdersList)
                {
                    CAI_POOrders model = new CAI_POOrders();
                    model.CostPrice = m.CostPrice;
                    model.CostTotal = m.CostTotal;
                    model.Good_Model = m.Good_Model;
                    model.GoodId = m.GoodId;
                    model.GoodName = m.GoodName;
                    model.GoodNo = m.GoodNo;
                    model.GoodSpec = m.GoodSpec;
                    model.GoodTypeSmName = m.GoodTypeSmName;
                    model.GoodUnit = m.GoodUnit;
                    model.GuestName = m.GuestName;
                    model.InvName = m.InvName;
                    model.Num = m.ResultTotalNum;
                    model.OtherCost = m.OtherCost;
                    model.Profit = m.Profit;
                    model.SellPrice = m.SellPrice;
                    model.SellTotal = m.SellTotal;
                    model.Time = m.Time;
                    model.ToTime = m.ToTime;
                    model.Unit = m.Unit;
                    model.YiLiTotal = m.YiLiTotal;
                    model.CG_POOrdersId = m.Ids;
                    model.GoodAreaNumber = m.GoodAreaNumber;
                    MyOrdersList.Add(model);

                }

                Session["Orders"] = MyOrdersList;
                gvList.DataSource = MyOrdersList;
                gvList.DataBind();
            }
        }

        protected void lbtnPoList_Click(object sender, EventArgs e)
        {
            if (Session["CGPONo"] != null)
            {

                txtCG_ProNo.Text = Session["CGPONo"].ToString();
                lblZhui.Text = new CG_POOrderService().IfZhuiByProNo(txtCG_ProNo.Text);

                lblSpecial.Text = new CG_POOrderService().IfSpecialByProNo(txtCG_ProNo.Text);
                GetPOInfo();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["PoNo"] = txtPONo.Text;
            Page.RegisterStartupScript("ServiceManHistoryButtonClick", "<script>window.open('CAI_OrderList.aspx');</script>");
        }

        protected void cbIsHanShui_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsHanShui.Checked)
            {
                dllFPstye.Enabled = true;
            }
            else
            {
                dllFPstye.SelectedItem.Text = "";
                dllFPstye.Enabled = false;
            }
        }

        protected void lblNextCaiPrice_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>window.open('Pro_JSXDetailInfoList.aspx','_blank');</script>");
        }

        protected void cbifDefault1_CheckedChanged(object sender, EventArgs e)
        {
            SetSpecial();
        }

        private void SetSpecial()
        {
            //“特殊”供应商
            if (cbifDefault1.Checked && txtSupplier.Text != "")
            {

                if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupplier.Text)) > 0)
                {
                    //特殊---不含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = false;
                    dllFPstye.Enabled = false;
                    dllFPstye.SelectedItem.Text = "";
                }
                else
                {
                    //非特殊---含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = true;
                    dllFPstye.Enabled = true;
                }
            }
            if (cbifDefault2.Checked && txtSupper2.Text != "")
            {
                if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper2.Text)) > 0)
                {
                    //特殊---不含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = false;
                    dllFPstye.Enabled = false;
                    dllFPstye.SelectedItem.Text = "";
                }
                else
                {
                    //非特殊---含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = true;
                    dllFPstye.Enabled = true;
                }
            }
            if (cbifDefault3.Checked && txtSupper3.Text != "")
            {
                if ((int)DBHelp.ExeScalar(string.Format("select COUNT(*) from TB_SupplierInfo where Status='通过' and IsSpecial=1 and SupplieSimpeName='{0}'", txtSupper3.Text)) > 0)
                {
                    //特殊---不含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = false;
                    dllFPstye.Enabled = false;
                    dllFPstye.SelectedItem.Text = "";
                }
                else
                {
                    //非特殊---含税
                    cbIsHanShui.Enabled = false;
                    cbIsHanShui.Checked = true;
                    dllFPstye.Enabled = true;
                }
            }
        }

        protected void cbifDefault2_CheckedChanged(object sender, EventArgs e)
        {
            SetSpecial();
        }

        protected void cbifDefault3_CheckedChanged(object sender, EventArgs e)
        {
            SetSpecial();
        }

        protected void cbMingYiCaiGou_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMingYiCaiGou.Checked)
            {
                ddlBusType.Text = "1";
                BusTypeChanged();
                ddlBusType.Enabled = false;

                txtRemark.Text = "合并库存或拆分组件，联动入库和名义支付";
            }
            else
            {
                ddlBusType.Text = "0";
                BusTypeChanged();
                ddlBusType.Enabled = true;
                txtRemark.Text = "";
            }
        }
    }
}
