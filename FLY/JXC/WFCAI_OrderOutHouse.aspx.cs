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
using System.Text;
using System.Data.SqlClient;

namespace VAN_OA.JXC
{
    public partial class WFCAI_OrderOutHouse : System.Web.UI.Page
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


        private decimal CheckWeiRuKuNums_1(int goodId)
        {
            try
            {
                string sql = string.Format(@"select isnull(sum(GoodNum),0) as sellTuiNum from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  
where  Status='通过' and PONo='{0}' and gooId={1} ", txtPONo.Text, goodId);
                var result = (decimal)DBHelp.ExeScalar(sql);
                sql = string.Format(@"select isnull(sum(GoodNum),0)  as caiTuiNum from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID 
where Status<>'不通过' and PONo='{0}' and gooId={1} ", txtPONo.Text, goodId);

                var result1 = (decimal)DBHelp.ExeScalar(sql);
                return result - result1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private decimal CheckWeiRuKuNums(int goodId)
        {
            try
            {
                string sql = string.Format(@"select 
isnull(ruAllNums,0)- isnull(newTb2.outNum,0)+isnull(newTb4.sellTuiNum,0)-isnull(newTb3.caiTuiNum,0) as ruChuNum
from 
(
select CAI_OrderInHouse.PONo,GooId,sum(GoodNum) as ruAllNums
from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
where Status='通过' 
and CAI_OrderInHouse.PONo='{0}' and gooId={1}
group by CAI_OrderInHouse.PONo,GooId
)
as newTb5
left join
(
select PoNo,GooId,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
 where Status<>'不通过'  group by GooId,PoNo
) as newTb2 on newTb5.GooId=newTb2.GooId  and  newTb5.Pono=newTb2.Pono  left join 
(
select PoNo,GooId,sum(GoodNum)  as caiTuiNum from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID 
where Status<>'不通过'   group by GooId ,PoNo
) as newTb3 on newTb5.GooId=newTb3.GooId and newTb5.Pono=newTb3.Pono  left join 
(
select PoNo,GooId,sum(GoodNum) as sellTuiNum from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  
where  Status<>'不通过'  group by GooId,PoNo
) newTb4 on newTb5.GooId=newTb4.GooId and newTb5.POno=newTb4.pono 
", txtPONo.Text, goodId);
                var result =(decimal)DBHelp.ExeScalar(sql);
                return result;
            }
            catch (Exception)
            {
                
                return 0;
            }
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

            if (txtChcekProNo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写入库单号！');</script>");
                txtChcekProNo.Focus();
                return false;
            }


            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写退货时间有误！');</script>");
                txtRuTime.Focus();
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

            //if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
            //    return false;
            //}
            if (Request["allE_id"] == null)
            {
                List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }

                Hashtable ht = new Hashtable();
                foreach (var model in POOrders)
                {
                    if (!ht.Contains(model.OrderCheckIds))
                        ht.Add(model.OrderCheckIds, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                }

                int i = 0;

                CAI_OrderInHousesService orderInHouseSer = new CAI_OrderInHousesService();
                CAI_OrderChecksService checksSer = new CAI_OrderChecksService();
                TB_HouseGoodsService houseGoodSer = new TB_HouseGoodsService();
                foreach (var model in POOrders)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }
                        model.GoodNum = Convert.ToDecimal(txtNum.Text);
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        model.GoodRemark = txtGoodRemark.Text;
                    }
                    if (model.GoodNum <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.GoodPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }

                    //List<CAI_OrderChecks> allChecks = checksSer.GetListArrayPOOrderChecks_Cai_POOrderInHouse_ListView(string.Format(" 1=1 and ProNo='{0}' and PONo='{1}' and ids={2}", txtChcekProNo.Text, txtPONo.Text, model.OrderCheckIds));
                    List<CAI_OrderInHouses> allChecks = orderInHouseSer.GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_ListView(string.Format(" 1=1 and ProNo='{0}' and ids={1}", txtChcekProNo.Text, model.OrderCheckIds));
                    //List<Model.JXC.CG_POOrders> POOrdersList = poOrderSer.GetListCG_POOrders_Cai_POOrders_View(string.Format(" 1=1 and CG_POOrders_Cai_POOrders_View.id in (select id from CG_POOrder where PONo='{0}') and ids={1}", txtPONo.Text, model.CG_POOrdersId));
                    if (allChecks.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],在订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                        return false;
                    }
                    if (model.GoodNum > allChecks[0].GoodNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],入库数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, allChecks[0].GoodNum));

                        return false;
                    }
                    //检测库存是否足
                    decimal goodNum = houseGoodSer.GetGoodNum(Convert.ToInt32(ddlHouse.SelectedValue), model.GooId);
                    //获取正在执行的采购退货单
                    string sql = string.Format(@"select sum(GoodNum) from CAI_OrderOutHouse
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id
where status='执行中' and GooId={0}", model.GooId);
                    decimal doingGoodNums = 0;
                    var objdoingGoodNums = DBHelp.ExeScalar(sql);
                    if (!(objdoingGoodNums is DBNull) && objdoingGoodNums != null)
                    {
                        doingGoodNums = Convert.ToDecimal(objdoingGoodNums);
                    }
                    if (model.GoodNum > goodNum - doingGoodNums)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],库存剩余数量[{3}],正在审批的数量{4}！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, goodNum, doingGoodNums));

                        return false;
                    }

                    //判断当前的入库信息有没有退货单执行 如果有不能做支付单
                    string CAI_OrderOutHouseSql = string.Format(@" select count(*) from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
LEFT JOIN CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
where TB_SupplierInvoice.status='执行中' and IsYuFu=0 and 
 CAI_OrderInHouses.GooId={0} and CAI_OrderInHouse.Supplier='{1}' ", model.GooId, txtSupplier.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(CAI_OrderOutHouseSql)) > 0)
                    {
                        var errorText = new StringBuilder();
                        errorText.AppendFormat(" 单号:{0},供应商:{1},商品:{2} 存在正在执行的【供应商支付单】，不能提交本次采购退货单！", model.ProNo,txtSupplier.Text , model.GoodNo + @"\" + model.GoodName + @"\" + model.GoodTypeSmName + @"\" + model.GoodSpec);
                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                            string.Format(@"<script>alert('" + errorText.ToString() + "');</script>"));
                        return false;
                    }
//                    1.采购退货提交时必须判断   入库未出数>=采购退货数 即可提交成功，否则 提示：
                    //需要做销售退货，再进行采购退货。提交不成功（销售退货只含通过的数量(审批全部完成的)
                    //if (CheckWeiRuKuNums_1(model.GooId) <model.GoodNum)
                    //{
                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                    //        string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],需要做销售退货，再进行采购退货！');</script>",
                    //        model.GoodName, model.GoodSpec, model.Good_Model));

                    //    return false;
                    //}

                    i++;
                }
                Session["Orders"] = POOrders;
            }
            else
            {
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;

                    if (POOrders == null || POOrders.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                        return false;
                    }

                    Hashtable ht = new Hashtable();
                    foreach (var model in POOrders)
                    {
                        if (!ht.Contains(model.OrderCheckIds))
                            ht.Add(model.OrderCheckIds, null);
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                            return false;
                        }
                    }

                    int i = 0;

                    CAI_OrderInHousesService orderInHouseSer = new CAI_OrderInHousesService();
                    CAI_OrderChecksService checksSer = new CAI_OrderChecksService();
                    TB_HouseGoodsService houseGoodSer = new TB_HouseGoodsService();
                    foreach (var model in POOrders)
                    {                        
                        if (model.GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                            return false;
                        }
                        if (model.GoodPrice < 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                            return false;
                        }

                       
                        //List<CAI_OrderInHouses> allChecks = orderInHouseSer.GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_ListView(string.Format(" 1=1 and ProNo='{0}' and ids={1}", txtChcekProNo.Text, model.OrderCheckIds));
                      
                        //if (allChecks.Count <= 0)
                        //{
                        //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],在订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                        //    return false;

                        //}
                        //if (model.GoodNum > allChecks[0].GoodNum)
                        //{
                        //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],入库数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, allChecks[0].GoodNum));

                        //    return false;
                        //}

                        //检测库存是否足
                        decimal goodNum = houseGoodSer.GetGoodNum(Convert.ToInt32(ddlHouse.SelectedValue), model.GooId);
                        //获取正在执行的采购退货单
                        string sql = string.Format(@"select sum(GoodNum) from CAI_OrderOutHouse
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id
where status='执行中' and GooId={0} and ids<>{1}", model.GooId,model.Ids);
                        decimal doingGoodNums = 0;
                        var objdoingGoodNums = DBHelp.ExeScalar(sql);
                        if (!(objdoingGoodNums is DBNull) && objdoingGoodNums != null)
                        {
                            doingGoodNums = Convert.ToDecimal(objdoingGoodNums);
                        }
                        if (model.GoodNum > goodNum - doingGoodNums)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],库存剩余数量[{3}],正在审批的数量{4}！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, goodNum, doingGoodNums));

                            return false;
                        }

                        i++;
                    }
                    
                }
            }
         
            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            txtChcekProNo.ReadOnly = true;
            
            txtPOName.ReadOnly = true;
            txtPONo.ReadOnly = true;
            txtRemark.ReadOnly = !result;
            txtRuTime.ReadOnly = true;
            txtSupplier.ReadOnly = true;
            ddlHouse.Enabled = false;
            Image1.Enabled = false;
            
        }


       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (Session["LoginName"].ToString() != "admin")
                {
                    Button1.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                }
                //请假单子              
                Session["Orders"] = null;              
                lbtnAddFiles.Visible = false;

                gvList.Columns[0].Visible = false;
                gvList.Columns[6].Visible = false;
                gvList.Columns[10].Visible = false;


                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";

               
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
                        gvList.Columns[6].Visible = true;
                        gvList.Columns[7].Visible = false;
                        gvList.Columns[10].Visible = true;
                        gvList.Columns[11].Visible = false;

                        //加载初始数据

                        List<CAI_OrderOutHouses> orders = new List<CAI_OrderOutHouses>();
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

                        CAI_OrderOutHouseService mainSer = new CAI_OrderOutHouseService();
                        CAI_OrderOutHouse pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;

                        txtChcekProNo.Text = pp.ChcekProNo;
                        
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.RuTime.ToString();
                        txtSupplier.Text = pp.Supplier;

                        ddlHouse.Text = pp.HouseID.ToString();
                        txtCaiGou.Text = pp.DaiLi;
                        
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        CAI_OrderOutHousesService ordersSer = new CAI_OrderOutHousesService();
                        List<CAI_OrderOutHouses> orders = ordersSer.GetListArray(" 1=1 and CAI_OrderOutHouses.id=" + Request["allE_id"]);
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

                    CAI_OrderOutHouse order = new CAI_OrderOutHouse();

                    int CreatePer=Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateUserId = CreatePer;
                    order.ChcekProNo = txtChcekProNo.Text;
                    order.DoPer = "";
                    order.HouseID = Convert.ToInt32(ddlHouse.SelectedValue);
                    order.POName = txtPOName.Text;
                    order.PONo = txtPONo.Text;
                    order.RuTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.Supplier = txtSupplier.Text;
                    order.FPNo = "";
                    order.DaiLi = txtCaiGou.Text;
                    order.GuestName = lblGuestName.Text;
                  
                    List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;

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


                        CAI_OrderOutHouseService POOrderSer = new CAI_OrderOutHouseService();

                        if (ddlPers.Visible == false && ddlResult.SelectedItem.Text == "通过")
                        {
                            //判断改供应商是否有在支付中的单子
                            if (TB_SupplierInvoiceService.checkSupplierDoing(txtSupplier.Text))
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请排队等候');</script>");
                                return;
                            }

                            
                            //判断改供应商是否有在支付中的单子
                            if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(txtSupplier.Text, 1))
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');</script>");

                                return;
                            }
                        }
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {
                            if (ddlPers.Visible == false)
                            {
                                Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                                SellOutSer.SellOrderUpdatePoStatus2(txtPONo.Text);

                             
                            }
                            if (ddlPers.Visible == false&&ddlResult.Text=="通过")
                            {
                                new TB_SupplierInvoiceService().AddSupplierInvoice(POOrders, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()),txtSupplier.Text,lblProNo.Text);

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
                        CAI_OrderOutHouseService POOrderSer = new CAI_OrderOutHouseService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        if (ddlPers.Visible == false && ddlResult.SelectedItem.Text == "通过")
                        {
                            //判断改供应商是否有在支付中的单子
                            if (TB_SupplierInvoiceService.checkSupplierDoing(txtSupplier.Text))
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请排队等候');</script>");
                                return;
                            }
                            //判断改供应商是否有在支付中的单子
                            if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(txtSupplier.Text, 1))
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');</script>");

                                return;
                            }
                        }
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            if (ddlPers.Visible == false)
                            {
                                Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                                SellOutSer.SellOrderUpdatePoStatus2(txtPONo.Text);

                                //new TB_SupplierInvoiceService().AddSupplierInvoice(POOrders,Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()));
                            }
                            if (ddlPers.Visible == false && ddlResult.Text == "通过")
                            {
                                new TB_SupplierInvoiceService().AddSupplierInvoice(POOrders, Session["LoginName"].ToString(),
                                    Convert.ToInt32(Session["currentUserId"].ToString()), txtSupplier.Text, lblProNo.Text);

                                //更新库存价格字段
                                string sql = string.Format("update UPDATE_CAIOUTHOUSE__PRICE set TempHousePrice=isnull(GoodAvgPrice,0) where id={0}", order.Id);
                                DBHelp.ExeCommand(sql);
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



        private void aaa()
        {
            List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;          
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();

                foreach (var ids in POOrders)
                {
                    var selectInfo = string.Format(@"select avg(SupplierInvoiceNum) as avgSupplierInvoiceNum, sum(SupplierInvoiceTotal) as sumSupplierInvoiceTotal from TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where RuIds={0} and SupplierInvoiceTotal>0 and ((status='通过' and IsYuFu=0) or (Status<>'不通过' and IsYuFu=1)) ", ids.OrderCheckIds);

                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.CommandText = selectInfo;
                    decimal avgSupplierInvoiceNum = 0;
                    decimal sumSupplierInvoiceTotal = 0;
                    using (var reader = objCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object ojb;
                            ojb = reader["avgSupplierInvoiceNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                avgSupplierInvoiceNum = Convert.ToDecimal(ojb);
                            }
                            ojb = reader["sumSupplierInvoiceTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                sumSupplierInvoiceTotal = Convert.ToDecimal(ojb);
                            }
                        }
                        reader.Close();
                    }

                    TB_SupplierInvoice model = new TB_SupplierInvoice();
                    model.Status = "通过";
                    model.CreteTime = DateTime.Now;
                    model.CreateName = "admin";
                    model.CaiTuiProNo = "";
                    model.LastSupplier = txtSupplier.Text;
                    model.Remark = "全部退货";
                    var eform = new tb_EForm();
                    eform.appPer = 1;
                    eform.appTime = DateTime.Now;
                    eform.createPer = 1;
                    eform.createTime = DateTime.Now;
                    eform.proId = 31;
                    eform.state = "通过";
                    eform.toPer = 0;
                    eform.toProsId = 0;
                    var orders = new List<SupplierToInvoiceView>();
                    var invoiceModel = new SupplierToInvoiceView();
                    invoiceModel.Ids = ids.OrderCheckIds;

                    //invoiceModel.IsYuFu = true;
                    invoiceModel.SupplierFPNo = "";
                    invoiceModel.SupplierInvoiceDate = DateTime.Now;
                    var AvgSupplierInvoicePrice = -(sumSupplierInvoiceTotal / avgSupplierInvoiceNum);
                    invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                    invoiceModel.lastGoodNum = avgSupplierInvoiceNum;
                    invoiceModel.SupplierInvoiceTotal = -sumSupplierInvoiceTotal;

                    invoiceModel.FuShuTotal = invoiceModel.SupplierInvoiceTotal;
                    invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;

                    invoiceModel.GuestName = txtSupplier.Text;
                    //invoiceModel.CaiTuiProNo = caiTuiProNo;
                    invoiceModel.RePayClear = 2;
                    invoiceModel.IsPayStatus = 0;//未支付
                    invoiceModel.IsHeBing = 1;
                    orders.Add(invoiceModel);
                    int mainId = 0;
                    new TB_SupplierInvoiceService().addTran(model, eform, orders, out mainId);
                }
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成功！');</script>");
        }


        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["DioPOOrderInHouse"] != null)
            {
                CAI_OrderInHouse model = Session["DioPOOrderInHouse"] as CAI_OrderInHouse;
                txtChcekProNo.Text = model.ProNo;
                txtPONo.Text = model.PONo;
                txtPOName.Text = model.POName;
                txtSupplier.Text = model.Supplier;
                ddlHouse.SelectedValue = model.HouseID.ToString();
                lblGuestName.Text = model.GuestName;
                txtCaiGou.Text = model.DoPer;
                ShowData();
            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;
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
                List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;

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

        CAI_OrderOutHouses SumOrders = new CAI_OrderOutHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderOutHouses model = e.Row.DataItem as CAI_OrderOutHouses;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;               


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
            CAI_OrderInHousesService orderInHouseSer = new CAI_OrderInHousesService();


            List<CAI_OrderInHouses> allChecks = orderInHouseSer.GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_ListView(string.Format(" 1=1 and ProNo='{0}'", txtChcekProNo.Text));
            List<CAI_OrderOutHouses> POOrders = new List<CAI_OrderOutHouses>();// Session["Orders"] as List<CAI_OrderOutHouses>;

            foreach (var model in allChecks)
            {
                CAI_OrderOutHouses houseModel = new CAI_OrderOutHouses();
                houseModel.Good_Model = model.Good_Model;
                houseModel.GoodName = model.GoodName;
                houseModel.GoodNo = model.GoodNo;
                houseModel.GoodNum = model.GoodNum;
                houseModel.GoodPrice = model.GoodPrice;
                houseModel.GoodSpec = model.GoodSpec;
                houseModel.GoodTypeSmName = model.GoodTypeSmName;
                houseModel.GoodUnit = model.GoodUnit;
                houseModel.GooId = model.GooId;
                houseModel.Total = model.Total;
                houseModel.OrderCheckIds = model.Ids;
                houseModel.QingGouPer = model.QingGouPer;
                POOrders.Add(houseModel);
            }
            Session["Orders"] = POOrders;
            gvList.DataSource = POOrders;
            gvList.DataBind();

        }
        protected void txtChcekProNo_TextChanged(object sender, EventArgs e)
        {

            

       
        }

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click1(object sender, EventArgs e)
        {
            List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;
            new TB_SupplierInvoiceService().AddSupplierInvoice(POOrders, "admin",
               1, txtSupplier.Text, lblProNo.Text);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成功！');</script>");
        }

        /// <summary>
        /// 抵扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            aaa();
        }

        /// <summary>
        /// 补单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
           
            List<CAI_OrderOutHouses> POOrders = Session["Orders"] as List<CAI_OrderOutHouses>;
            new TB_SupplierInvoiceService().AddSupplierInvoice_01(POOrders, "admin",
               1, txtSupplier.Text, lblProNo.Text);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成功！');</script>");
        }
    }
}
