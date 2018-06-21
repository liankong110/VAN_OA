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


namespace VAN_OA.JXC
{
    public partial class WFCAI_OrderCheck : System.Web.UI.Page
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


            if (txtCheckPer.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验人！');</script>");
                txtCheckPer.Focus();
                return false;
            }


            if (txtCheckTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验时间！');</script>");
                txtCheckTime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtCheckTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('检验时间 格式错误！');</script>");
                    return false;
                }

            }


            try
            {
                Convert.ToDateTime(txtCheckTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验时间有误！');</script>");
                txtCheckTime.Focus();
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

            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtCheckPer.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验人不存在！');</script>");

                return false;
            }

            #endregion


            if (Request["allE_id"] == null)
            {
                List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }


                var mycaiIds = new StringBuilder();
                foreach (var m in POOrders)
                {
                    mycaiIds.AppendFormat("{0},", m.CaiId);
                }
                if (mycaiIds.ToString() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明细！');</script>");
                    return false;
                }
                var ids = mycaiIds.ToString().Substring(0, mycaiIds.ToString().Length - 1);


                //--在创建/编辑 预付单时 判断是否已经有入库记录(包含正在执行的单子)
                string checkSql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments on 
TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id where Status='执行中'
and CaiIds in ({0})", ids);//--增加采购订单的ID ( and CaiId=?)

                var errorText = new StringBuilder();
                var count = DBHelp.ExeScalar(checkSql);
                if (count != null && Convert.ToInt32(count) > 0)
                {
                    errorText.Append("列表中采购数据，存在正在执行中的预付款单，请先将对应预付款单审批通过！");
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + errorText.ToString() + "');</script>");
                    return false;
                }


                Hashtable ht = new Hashtable();

                Hashtable htSupplier = new Hashtable();
                foreach (var model in POOrders)
                {
                    string key = model.CheckGoodId.ToString() + model.CaiProNo;
                    if (!ht.Contains(key))
                        ht.Add(key, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('单号[{3}]-商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.CaiProNo));
                        return false;
                    }

                    string aa = model.SupplierName + model.PONo + model.POName + model.CaiGouPer;

                    if (!htSupplier.Contains(aa))
                    {
                        if ((!model.PONo.Contains("KC")) && new CG_POOrderService().ExistPONO(model.PONo) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                            return false;
                        }
                        htSupplier.Add(aa, null);
                        if (htSupplier.Keys.Count > 1)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('检验单中 项目单号，项目名称,供应商,采购人必选完全一样！');</script>"));
                            return false;
                        }
                    }


                }
                CAI_POCaiService POSer = new CAI_POCaiService();
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].CheckNum = Convert.ToDecimal(txtNum.Text);
                    }
                    var model = POOrders[i];
                    if (model.CheckNum <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.CheckPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }

                    string sql = string.Format(" 1=1 and Ids={0}", model.CaiId);
                    List<CAI_POCaiView> cars = POSer.GetListViewCai_POOrders_Cai_POOrderChecks_View(sql);
                    if (cars.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('单号[{3}]-商品[{0}]\规格[{1}]\型号[{2}],在项目订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.PONo));
                        return false;

                    }
                    if (model.CheckNum > cars[0].ResultNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('单号[{3}]-商品[{0}]\规格[{1}]\型号[{2}],检查数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, cars[0].ResultNum));

                        return false;
                    }
                }


                Session["Orders"] = POOrders;

            }
            else
            {
                List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;
                CAI_POCaiService POSer = new CAI_POCaiService();
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].CheckNum = Convert.ToDecimal(txtNum.Text);
                    }
                    var model = POOrders[i];
                    if (model.CheckNum <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.CheckPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }

                    if (i == 0 && (!model.PONo.Contains("KC")) && new CG_POOrderService().ExistPONO(model.PONo) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                        return false;
                    }


                    //查询订单原来的数量
                    string oldString = string.Format(@"select isnull(Num,0)-ISNULL(totalOrderNum,0)  as ResultNum from CAI_POCai 
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id 
left join 
(
select  CaiId,SUM(CheckNum) as totalOrderNum from CAI_OrderChecks left join CAI_OrderCheck on 
CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where CaiId<>0  and status<>'不通过' and CAI_OrderChecks.Ids<>{0}
group by CaiId
)
as newtable on CAI_POCai.Ids=newtable.CaiId where (CAI_POCai.Num>newtable.totalOrderNum or totalOrderNum is null)
and status='通过' and Ids={1}", model.Ids, model.CaiId);
                    var oldNum = DBHelp.ExeScalar(oldString);


                    if (oldNum is DBNull)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('单号[{3}]-商品[{0}]\规格[{1}]\型号[{2}],在项目订单[{3}]中不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.PONo));
                        return false;

                    }
                    if (model.CheckNum > Convert.ToDecimal(oldNum))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('单号[{3}]-商品[{0}]\规格[{1}]\型号[{2}],检查数量剩余[{3}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, oldNum));

                        return false;
                    }
                }
                Session["Orders"] = POOrders;

            }

            if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;
                //判断是否为特殊商品 并查询库存是否有库存
                TB_HouseGoodsService goodHouseSer = new TB_HouseGoodsService();
                CAI_OrderCheckService checkSer = new CAI_OrderCheckService();
                foreach (var m in POOrders)
                {
                    if (goodHouseSer.CheckGoodInHouse(m.CheckGoodId))
                    {
                        string mess = checkSer.GetPONoInfo(m.CheckGoodId, m.GoodNo, m.GoodName, m.GoodTypeSmName, m.GoodSpec);
                        if (mess == "")
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('数据异常！');</script>"));
                            return false;
                        }
                        base.ClientScript.RegisterStartupScript(base.GetType(), null,
                            string.Format(@"<script>alert('{0}！');</script>",
                           mess));
                        return false;
                    }
                }
            }

            //判断有没有设置默认仓库
            if (ddlPers.Visible == false)
            {
                string sql = "select count(*) from TB_HouseInfo where IfDefault=1";
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "默认仓库设置错误！请检查");
                    return false;
                }

            }

            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            txtCheckTime.ReadOnly = true;
            txtCheckPer.ReadOnly = true;
            txtCheckRemark.ReadOnly = !result;
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (Session["LoginName"].ToString() != "admin")
                {
                    Button1.Visible = false;
                }

                //请假单子              
                Session["Orders"] = null;
                lbtnAddFiles.Visible = false;

                gvList.Columns[0].Visible = false;

                gvList.Columns[12].Visible = false;
                //gvList.Columns[1].Visible = false;



                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    txtCheckTime.Text = DateTime.Now.ToString();

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {


                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        // gvList.Columns[1].Visible = true;

                        gvList.Columns[12].Visible = true;
                        gvList.Columns[13].Visible = false;
                        //加载初始数据

                        List<CAI_OrderChecks> orders = new List<CAI_OrderChecks>();
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

                        CAI_OrderCheckService mainSer = new CAI_OrderCheckService();
                        CAI_OrderCheck pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtCheckPer.Text = pp.CheckUserName;
                        txtCheckTime.Text = pp.CheckTime.ToString();
                        txtCheckRemark.Text = pp.CheckRemark;

                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        CAI_OrderChecksService ordersSer = new CAI_OrderChecksService();
                        List<CAI_OrderChecks> orders = ordersSer.GetListArray(" 1=1 and CAI_OrderChecks.CheckId=" + Request["allE_id"]);
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
                                gvList.Columns[12].Visible = true;
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
                                    gvList.Columns[12].Visible = true;
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

                    CAI_OrderCheck order = new CAI_OrderCheck();

                    int CreatePer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreatePer = CreatePer;
                    order.CheckPer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtCheckPer.Text)));
                    order.CheckTime = Convert.ToDateTime(txtCheckTime.Text);
                    order.CheckRemark = txtCheckRemark.Text;

                    List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;

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


                        CAI_OrderCheckService POOrderSer = new CAI_OrderCheckService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {

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
                        CAI_OrderCheckService POOrderSer = new CAI_OrderCheckService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        order.ProNo = lblProNo.Text;

                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            if (ddlPers.Visible == false && ddlResult.SelectedItem.Text == "通过")
                            {
                                string checkIds = "";
                                string caiIds = "";
                                foreach (var model in POOrders)
                                {
                                    checkIds += model.Ids + ",";
                                    caiIds += model.CaiId + ",";
                                }
                                if (checkIds.Length > 0)
                                {
                                    checkIds = checkIds.Substring(0, checkIds.Length - 1);
                                    caiIds = caiIds.Substring(0, caiIds.Length - 1);
                                }
                                new TB_SupplierInvoiceService().AddSupplierInvoice(checkIds, caiIds, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()), POOrders[0].SupplierName);
                                try
                                {
                                    //更新库存价格字段
                                    string sql = string.Format("update UPDATE_CAIINHOUSE_PRICE set TempHousePrice=isnull(GoodAvgPrice,0) where id={0}", order.Id);
                                    DBHelp.ExeCommand(sql);
                                }
                                catch (Exception)
                                {

                                     
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





        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {

                if (Session["CGPOList"] != null)
                {
                    List<Model.JXC.CAI_POCaiView> modelList = Session["CGPOList"] as List<Model.JXC.CAI_POCaiView>;
                    List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;

                    foreach (var model in modelList)
                    {
                        CAI_OrderChecks checkModel = new CAI_OrderChecks();
                        checkModel.Total = model.Total;
                        checkModel.CaiId = model.POCaiId;
                        checkModel.CheckGoodId = model.GoodId;
                        checkModel.CheckNum = model.Num;
                        checkModel.CheckPrice = model.Price;
                        checkModel.Good_Model = model.Good_Model;
                        checkModel.GoodName = model.GoodName;
                        checkModel.GoodNo = model.GoodNo;
                        checkModel.GoodSpec = model.GoodSpec;
                        checkModel.GoodTypeSmName = model.GoodTypeSmName;
                        checkModel.GoodUnit = model.GoodUnit;
                        checkModel.GuestName = model.GuestName;
                        checkModel.POName = model.POName;
                        checkModel.PONo = model.PONo;
                        checkModel.SupplierName = model.Supplier;

                        checkModel.CaiProNo = model.ProNo;
                        checkModel.QingGou = model.CaiGou;
                        checkModel.CaiGouPer = model.loginName;
                        checkModel.CheckLastTruePrice = model.LastTruePrice;
                        checkModel.GoodAreaNumber = model.GoodAreaNumber;
                        POOrders.Add(checkModel);

                    }

                    Session["CGPOList"] = null;
                    gvList.DataSource = POOrders;
                    gvList.DataBind();


                }

            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;
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
                List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;

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

        CAI_OrderChecks SumOrders = new CAI_OrderChecks();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderChecks model = e.Row.DataItem as CAI_OrderChecks;
                SumOrders.CheckNum += model.CheckNum;
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
                setValue(e.Row.FindControl("lblPONo") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.CheckNum.ToString());//数量                
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

        protected void Button1_Click1(object sender, EventArgs e)
        {
            List<CAI_OrderChecks> POOrders = Session["Orders"] as List<CAI_OrderChecks>;
            string checkIds = "";
            string caiIds = "";
            foreach (var model in POOrders)
            {
                checkIds += model.Ids + ",";
                caiIds += model.CaiId + ",";
            }
            if (checkIds.Length > 0)
            {
                checkIds = checkIds.Substring(0, checkIds.Length - 1);
                caiIds = caiIds.Substring(0, caiIds.Length - 1);
            }
            new TB_SupplierInvoiceService().AddSupplierInvoice(checkIds, caiIds, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()), POOrders[0].SupplierName);

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
        }
    }
}
