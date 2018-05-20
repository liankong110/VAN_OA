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


namespace VAN_OA.JXC
{
    public partial class WFSell_OrderInHouse : System.Web.UI.Page
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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写退货日期！');</script>");
                txtRuTime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtRuTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('退货日期 格式错误！');</script>");
                    return false;
                }

            }

            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写退货日期格式有误！');</script>");
                txtRuTime.Focus();
                return false;

            }

            if (txtSellOutOrder.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择销售出库单号！');</script>");
                txtSellOutOrder.Focus();
                return false;
            }
            if (ddlSellInList.SelectedItem.Value == "1")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择销退理由！');</script>");
                ddlSellInList.Focus();
                return false;                
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

                return false;
            }


            #endregion

            if (Request["allE_id"] == null)
            {
                List<Sell_OrderInHouses> POOrders = Session["Orders"] as List<Sell_OrderInHouses>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }
                //Hashtable ht = new Hashtable();
                //foreach (var model in POOrders)
                //{
                //    string key = model.GooId.ToString() + model.HouseID;
                //    if (!ht.Contains(key))
                //        ht.Add(key, null);
                //    else
                //    {
                //        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName));
                //        return false;
                //    }
                //}

                if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                            "<script>alert('此项目已经关闭！');</script>");
                    return false;
                }

                Sell_OrderOutHousesService sell_OrderOutHousesService = new Sell_OrderOutHousesService();

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    TextBox txtGoodSellPrice = gvList.Rows[i].FindControl("txtGoodSellPrice") as TextBox;
                    var model1 = POOrders[i];

                    if (txtGoodSellPrice != null)
                    {
                        if (CommHelp.VerifesToNum(txtGoodSellPrice.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtGoodSellPrice.Text);
                        if (POOrders[i].GoodSellPrice > POOrders[i].OriSellPrice)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单[{3}]中-商品[{0}]\规格[{1}]\型号[{2}]，原始销售价格为{4},你填写的价格大于该价格！');</script>",
                                model1.GoodName, model1.GoodSpec, model1.Good_Model, txtSellOutOrder.Text,model1.OriSellPrice));
                            return false;
                        }
                    }
                    if (txtNum != null)
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;
                        if (POOrders[i].GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", POOrders[i].GoodName, POOrders[i].GoodSpec, POOrders[i].Good_Model));
                            return false;
                        }

                        if (POOrders[i].GoodSellPrice < 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", POOrders[i].GoodName, POOrders[i].GoodSpec, POOrders[i].Good_Model));
                            return false;
                        }
                    }
                    TextBox txtGoodPriceSecond = gvList.Rows[i].FindControl("txtGoodPriceSecond") as TextBox;
                    if (txtGoodPriceSecond != null)
                    {
                        if (txtGoodPriceSecond.Text != "")
                        {
                            if (CommHelp.VerifesToNum(txtGoodPriceSecond.Text) == false)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本单价确认价 格式错误！');</script>");
                                return false;
                            }
                        }
                        POOrders[i].GoodPriceSecond = Convert.ToDecimal(txtGoodPriceSecond.Text == "" ? "0" : txtGoodPriceSecond.Text);
                        if (POOrders[i].GoodPriceSecond > 0)
                        {
                            POOrders[i].GoodTotalCha = (POOrders[i].GoodPriceSecond - POOrders[i].GoodPrice) * POOrders[i].GoodNum;
                        }
                        else
                        {
                            POOrders[i].GoodTotalCha = 0;

                        }
                    }


                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }

                    var model = POOrders[i];

                    string strSpec = string.Format(@"select count(*) from TB_HouseGoods left join TB_Good on TB_Good.GoodId=TB_HouseGoods.GoodId
where TB_Good.GoodId={0} and IfSpec=1",model.GooId);
                    if (Convert.ToInt32(DBHelp.ExeScalar(strSpec)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单[{3}]中-商品[{0}]\规格[{1}]\型号[{2}]，库存里有此特殊商品，请先开出！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtSellOutOrder.Text));
                        return false;
                    }

                    string sql = string.Format(" ids={0}", model.SellOutOrderId);

                    List<Sell_OrderOutHouses> sell_OrderOutHousesList = sell_OrderOutHousesService.Sell_OrderOutHouse_Sell_OrderInHouse_ListView(sql);

                    if (sell_OrderOutHousesList.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单[{3}]中-商品[{0}]\规格[{1}]\型号[{2}]，该信息不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtSellOutOrder.Text));
                        return false;

                    }
                    if (model.GoodNum > sell_OrderOutHousesList[0].GoodNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单[{3}]-商品[{0}]\规格[{1}]\型号[{2}],数量剩余[{4}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtSellOutOrder.Text, sell_OrderOutHousesList[0].GoodNum));

                        return false;
                    }
                }


                Session["Orders"] = POOrders;

            }
            else
            {
               
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    List<Sell_OrderInHouses> POOrders = Session["Orders"] as List<Sell_OrderInHouses>;
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {

                        TextBox txtGoodPriceSecond = gvList.Rows[i].FindControl("txtGoodPriceSecond") as TextBox;
                        if (txtGoodPriceSecond != null)
                        {
                            if (txtGoodPriceSecond.Text != "")
                            {
                                if (CommHelp.VerifesToNum(txtGoodPriceSecond.Text) == false)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本单价确认价 格式错误！');</script>");
                                    return false;
                                }
                            }
                            POOrders[i].GoodPriceSecond = Convert.ToDecimal(txtGoodPriceSecond.Text == "" ? "0" : txtGoodPriceSecond.Text);
                            if (POOrders[i].GoodPriceSecond > POOrders[i].GoodPrice)
                            {

                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert(' 商品[{0}]\规格[{1}]\型号[{2}] 成本单价确认价必须 <= 成本价');</script>", POOrders[i].GoodName, POOrders[i].GoodSpec, POOrders[i].Good_Model));

                                return false;
                            }
                            if (POOrders[i].GoodPriceSecond > 0)
                            {
                                POOrders[i].GoodTotalCha = (POOrders[i].GoodPriceSecond - POOrders[i].GoodPrice) * POOrders[i].GoodNum;
                            }
                            else
                            {
                                POOrders[i].GoodTotalCha = 0;

                            }
                        }
                    }
                    Session["Orders"] = POOrders;
                }

                //销售退货 到 采购退货流程
                if (ddlSellInList.Text == "2" && ddlPers.Visible == false && ddlResult.SelectedItem.Text == "通过")
                {
                    List<Sell_OrderInHouses> sellIn_POOrders = Session["Orders"] as List<Sell_OrderInHouses>;
                    List<CAI_OrderOutHouses> POOrders = ViewState["caiList"] as List<CAI_OrderOutHouses>;
                    if (POOrders == null || POOrders.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加采购入库明显信息！');</script>");
                        return false;
                    }
                    int i = 0;
                    Hashtable ht = new Hashtable();
                    CAI_OrderInHousesService orderInHouseSer = new CAI_OrderInHousesService();
                    Hashtable ht_Supplier = new Hashtable();
                    foreach (var model in POOrders)
                    {
                        TextBox txtNum = gvCai.Rows[i].FindControl("txtNum") as TextBox;
                        if (txtNum != null)
                        {
                            model.GoodNum = Convert.ToDecimal(txtNum.Text);
                        }
                        if (!ht.Contains(model.GooId))
                            ht.Add(model.GooId, model.GoodNum);
                        else
                        {
                            decimal allNums = Convert.ToDecimal(ht[model.GooId]) + model.GoodNum;
                            ht[model.GooId] = allNums;
                        }
                        if (!ht_Supplier.Contains(model.Supplier))
                        {
                            ht_Supplier.Add(model.Supplier, null);
                        }
                        if (model.GoodNum == 0)
                        {
                            i++;
                            continue;
                        }
                       
                        
                        TextBox txtGoodRemark = gvCai.Rows[i].FindControl("txtGoodRemark") as TextBox;
                        if (txtGoodRemark != null)
                        {
                            model.GoodRemark = txtGoodRemark.Text;
                        }
                        List<CAI_OrderInHouses> allChecks = orderInHouseSer.GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_ListView(string.Format(" 1=1 and ProNo='{0}' and ids={1}", model.ProNo, model.OrderCheckIds));

                        if (allChecks.Count <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],在订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                            return false;
                        }
                        if (model.GoodNum > allChecks[0].GoodNum)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],入库数量剩余[{3}],采购退货只能有[{3}]个！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, allChecks[0].GoodNum));

                            return false;
                        }
                        i++;
                    }                    
                   
                    foreach (var key in ht.Keys)
                    {
                        var model=sellIn_POOrders.Find(t => t.GooId.ToString() == key.ToString());
                        if (Convert.ToDecimal(ht[key]) !=model.GoodNum )
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],该商品销售退货数量必须等于该采退数量总和，请修改采退数量！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));

                            return false;
                        }
                    }
                    foreach (var key in ht_Supplier.Keys)
                    {
                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.checkSupplierDoing(key.ToString()))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('[{0}]该供应商有在执行中的支付单，请排队等候');</script>", key));
                            return false;
                        }
                        
                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(key.ToString(), 1))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');</script>");

                            return false;
                        }
                    }
                    //判断退货的信息 需要
                    ViewState["caiList"] = POOrders;
                }
            }
            return true;
        }

        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            txtDoPer.ReadOnly = true;
            txtRemark.ReadOnly = true;
            txtRuTime.ReadOnly = true;
            txtSupplier.ReadOnly = true;
            txtSellOutOrder.ReadOnly = true;
            Image1.Enabled = false;
            ddlSellInList.Enabled = false;
            txtPOName.ReadOnly = true;
            txtPONo.ReadOnly = true;         
            txtDaiLi.ReadOnly = !result;
        }


        private void setRole(int Count)
        {
            if (Count == 1)
            {
                gvList.Columns[11].Visible = false;
                gvList.Columns[12].Visible = true;
            }             
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["Orders"] = null;
                lbtnAddFiles.Visible = false;
                gvList.Columns[0].Visible = false;
                gvList.Columns[8].Visible = false;
                gvList.Columns[12].Visible = false;
                gvList.Columns[18].Visible = false;

                gvList.Columns[15].Visible = false;

                //gvList.Columns[7].Visible = false;
                //gvList.Columns[11].Visible = false;
                //gvList.Columns[12].Visible = false;
                //gvList.Columns[13].Visible = false;
                //gvList.Columns[16].Visible = false;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {

                        txtRuTime.Text = DateTime.Now.ToString();
                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[8].Visible = true;
                        gvList.Columns[18].Visible = true;
                        gvList.Columns[15].Visible = true;

                        gvList.Columns[7].Visible = false;
                        gvList.Columns[11].Visible = false;
                        gvList.Columns[12].Visible = false;
                        gvList.Columns[13].Visible = false;
                        gvList.Columns[17].Visible = false;
                        gvList.Columns[14].Visible = false;




                        //加载初始数据

                        List<Sell_OrderInHouses> orders = new List<Sell_OrderInHouses>();
                        Session["Orders"] = orders;
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

                        Sell_OrderInHouseService mainSer = new Sell_OrderInHouseService();
                        Sell_OrderInHouse pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtSellOutOrder.Text = pp.ChcekProNo;

                        txtDoPer.Text = pp.DoPer;

                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.RuTime.ToString();
                        txtSupplier.Text = pp.Supplier;

                        //ddlHouse.Text = pp.HouseID.ToString();
                        txtDaiLi.Text = pp.DaiLi;
                       
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        ddlSellInList.Text = pp.SellInType.ToString();
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        Sell_OrderInHousesService ordersSer = new Sell_OrderInHousesService();
                        List<Sell_OrderInHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderInHouses.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
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
                            CAI_OrderInHousesService caiInSer = new CAI_OrderInHousesService();
                            lblCaiMess.Text = "采购退货信息";
                            var caiList = caiInSer.GetListArray_SellInToCaiOut_Result(lblProNo.Text);
                            gvCai.Visible = true;
                            gvCai.DataSource = caiList;
                            gvCai.DataBind();

                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                setRole(eforms.Count);
                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;
                                    if (ddlSellInList.Text == "2")
                                    {
                                        gvCai.Visible = true;
                                        CAI_OrderInHousesService caiInSer = new CAI_OrderInHousesService();
                                        string ids = "";
                                        foreach (var m in orders)
                                        {
                                            ids += m.GooId + ",";
                                        }
                                        var caiList = caiInSer.GetListArray_SellInToCaiOut(string.Format(" CAI_OrderInHouse.PONo='{0}' and GooId in ({1})", txtPONo.Text, ids.Trim(',')));

                                        gvCai.DataSource = caiList;
                                        gvCai.DataBind();
                                        ViewState["caiList"] = caiList;
                                    }
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
                                    setRole(eforms.Count);
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

                                    } setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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

                    Sell_OrderInHouse order = new Sell_OrderInHouse();

                    int CreatePer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateUserId = CreatePer;
                    order.ChcekProNo = txtSellOutOrder.Text;
                    order.DoPer = txtDoPer.Text;

                    order.RuTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.Supplier = txtSupplier.Text;

                    order.FPNo = "";
                    order.DaiLi = txtDaiLi.Text;
                    order.POName = txtPOName.Text;
                    order.PONo = txtPONo.Text;
                    order.SellInType = Convert.ToInt32(ddlSellInList.SelectedItem.Value);

                    List<Sell_OrderInHouses> POOrders = Session["Orders"] as List<Sell_OrderInHouses>;

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


                        Sell_OrderInHouseService POOrderSer = new Sell_OrderInHouseService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {
                           
                            if (ddlPers.Visible == false)
                            {
                                Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                                SellOutSer.SellOrderUpdatePoStatus2(txtPONo.Text);
                                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(txtPONo.Text, eform.state);
                                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(txtPONo.Text);
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
                        forms.doTime = DateTime.Now;
                        forms.e_Id = Convert.ToInt32(Request["EForm_Id"]); //fromSer.getCurrentid(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.idea = txtResultRemark.Text;
                        forms.prosIds = fromSer.getCurrenttoProsId(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.resultState = ddlResult.Text;
                        forms.RoleName = fromSer.getCurrenttoRoleName(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
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
                        Sell_OrderInHouseService POOrderSer = new Sell_OrderInHouseService();
                        string IDS = ViewState["POOrdersIds"].ToString();


                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            if (ddlPers.Visible == false)
                            {
                                Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                                SellOutSer.SellOrderUpdatePoStatus2(txtPONo.Text);
                                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(txtPONo.Text, eform.state);
                                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(txtPONo.Text);
                            }
                            //写入-销售退货损差表和销售退货明细损差表  中。
                            if (ddlPers.Visible == false && ddlResult.Text == "通过")
                            {
                                if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                                {
                                    new Sell_TuiSunChaService().addTran(POOrders);
                                }
                            }
                            if (ddlSellInList.Text == "2" && ddlPers.Visible == false && ddlResult.SelectedItem.Text == "通过")
                            {
                                List<CAI_OrderOutHouses> caiOutList = ViewState["caiList"] as List<CAI_OrderOutHouses>;
                                Hashtable ht = new Hashtable();
                                foreach (var m in caiOutList)
                                {                                   
                                    
                                    if (!ht.Contains(m.ProNo))
                                    {
                                        ht.Add(m.ProNo, null);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                    //如果没有天下数量就不要生成
                                    var proCaiList = caiOutList.FindAll(t => t.ProNo == m.ProNo && t.GoodNum > 0);
                                    if (proCaiList.Count > 0)
                                    {
                                        DoCaiOut(proCaiList, order.ProNo);
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
                }
            }
        }

        /// <summary>
        /// 销售退货 生成 采购退货
        /// </summary>
        /// <param name="caiOutList"></param>
        /// <param name="sellOutProNo"></param>
        private void DoCaiOut(List<CAI_OrderOutHouses> caiOutList,string sellOutProNo)
        {
            VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();           
            eform.appPer = 1;
            eform.appTime = DateTime.Now;
            eform.createPer =1;
            eform.createTime = DateTime.Now;
            eform.proId = 24;
            eform.state = "通过";
            eform.toPer = 0;
            eform.toProsId = 0;

            CAI_OrderOutHouse order = new CAI_OrderOutHouse();           
            order.CreateUserId = 1;
            order.ChcekProNo = caiOutList[0].ProNo;
            order.DoPer = "";
            order.HouseID = 1;
            order.POName = txtPOName.Text;
            order.PONo = txtPONo.Text;
            order.RuTime = DateTime.Now;
            order.Remark = txtRemark.Text;
            order.Supplier = caiOutList[0].Supplier;
            order.FPNo = "";
            order.DaiLi = "";
            order.GuestName ="";
            order.Remark = "销售退货单号:"+lblProNo.Text;
            int MainId = 0;
            CAI_OrderOutHouseService POOrderSer = new CAI_OrderOutHouseService();
            if (POOrderSer.addTran(order, eform, caiOutList, out MainId) > 0)
            {
                if (ddlPers.Visible == false)
                {
                    Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                    SellOutSer.SellOrderUpdatePoStatus2(txtPONo.Text);
                }
                if (ddlPers.Visible == false && ddlResult.Text == "通过")
                {
                    new TB_SupplierInvoiceService().AddSupplierInvoice(caiOutList, "admin", 1, order.Supplier, order.ProNo);
                }                
            }

        }


        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                if (Session["OrderInHouseSession"] != null)
                {
                    object selectedId = Session["OrderInHouseSession"];
                    if (selectedId != null)
                    {

                        Sell_OrderOutHousesService sellOrderServer = new Sell_OrderOutHousesService();
                        Sell_OrderOutHouseService sellOrderMainServer = new Sell_OrderOutHouseService();
                        Sell_OrderOutHouse sell_OrderOutHouseModel = sellOrderMainServer.GetModel(Convert.ToInt32(selectedId));
                        //txtDoPer.Text = sell_OrderOutHouseModel.DoPer;
                        txtSupplier.Text = sell_OrderOutHouseModel.Supplier;
                        txtSellOutOrder.Text = sell_OrderOutHouseModel.ProNo;
                        txtPOName.Text = sell_OrderOutHouseModel.POName;
                        txtPONo.Text = sell_OrderOutHouseModel.PONo;

                        List<Sell_OrderOutHouses> POOrders = sellOrderServer.Sell_OrderOutHouse_Sell_OrderInHouse_ListView(" id=" + selectedId); ;

                        List<Sell_OrderInHouses> sellOrderOutList = new List<Sell_OrderInHouses>();
                        foreach (var model in POOrders)
                        {
                            Sell_OrderInHouses sellOrderOutModel = new Sell_OrderInHouses()
                            {
                                Good_Model = model.Good_Model,
                                GoodName = model.GoodName,
                                GoodNo = model.GoodNo,
                                GoodNum = model.GoodNum,
                                GoodPrice = model.GoodPrice,
                                //GoodRemark = model.GoodRemark,
                                GoodSellPrice = model.GoodSellPrice,
                                GoodSellPriceTotal = model.GoodSellPriceTotal,
                                GoodSpec = model.GoodSpec,
                                GoodTypeSmName = model.GoodTypeSmName,
                                GoodUnit = model.GoodUnit,
                                GooId = model.GooId,
                                HouseID = model.HouseID,
                                HouseName = model.HouseName,
                                SellOutOrderId = model.Ids,
                                Total = model.Total,
                                OriSellPrice = model.GoodSellPrice


                            };
                            sellOrderOutList.Add(sellOrderOutModel);
                        }
                        Session["Orders"] = sellOrderOutList;

                        Session["OrderInHouseSession"] = null;
                        gvList.DataSource = sellOrderOutList;
                        gvList.DataBind();
                    }
                }
            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<Sell_OrderInHouses> POOrders = Session["Orders"] as List<Sell_OrderInHouses>;
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
                List<Sell_OrderInHouses> POOrders = Session["Orders"] as List<Sell_OrderInHouses>;

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

        Sell_OrderInHouses SumOrders = new Sell_OrderInHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderInHouses model = e.Row.DataItem as Sell_OrderInHouses;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;

                SumOrders.GoodTotalCha += model.GoodTotalCha;

                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;
            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.GoodNum.ToString());//数量                
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额 
                setValue(e.Row.FindControl("lblTotal1") as Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额 

                setValue(e.Row.FindControl("lblGoodTotalCha") as Label, SumOrders.GoodTotalCha.ToString());//差额总额    
            }

        }

        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
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


        private void ShowData()
        {

        }
        protected void txtChcekProNo_TextChanged(object sender, EventArgs e)
        {

            //CAI_OrderInHousesService checksSer = new CAI_OrderInHousesService();
            //List<CAI_OrderInHouses> allChecks = checksSer.GetListArray(string.Format(" CAI_OrderInHouses.id=( select id from CAI_OrderInHouse where ProNo='{0}')", txtChcekProNo.Text));
            //List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

            //foreach (var model in allChecks)
            //{
            //    Sell_OrderOutHouses houseModel = new Sell_OrderOutHouses();
            //    houseModel.Good_Model = model.Good_Model;
            //    houseModel.GoodName = model.GoodName;
            //    houseModel.GoodNo = model.GoodNo;
            //    houseModel.GoodNum = model.GoodNum;
            //    houseModel.GoodPrice = model.GoodPrice;
            //    houseModel.GoodSpec = model.GoodSpec;
            //    houseModel.GoodTypeSmName = model.GoodTypeSmName;
            //    houseModel.GoodUnit = model.GoodUnit;
            //    houseModel.GooId = model.GooId;
            //    houseModel.Total = model.Total;

            //    houseModel.GoodSellPrice = 0;
            //    houseModel.GoodSellPriceTotal = 0;

            //    POOrders.Add(houseModel);
            //}
            //Session["Orders"] = POOrders;
            //gvList.DataSource = POOrders;
            //gvList.DataBind();
        }





    }
}
