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
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;
using System.Data;


namespace VAN_OA.JXC
{
    public partial class WFSell_OrderFP : System.Web.UI.Page
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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写发票日期！');</script>");
                txtRuTime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtRuTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return false;
                }
            }

            if (txtFPNo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写发票号！');</script>");
                txtFPNo.Focus();
                return false;
            }

            if (txtDoPer.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('经手人不能为空！');</script>");
                txtDoPer.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写发票日期格式有误！');</script>");
                txtRuTime.Focus();
                return false;

            }

            if (dllFPstye.Text == "" || dllFPstye.Text == "0")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你选择发票类型！');</script>");
                dllFPstye.Focus();
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

            //if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验人不存在！');</script>");            

            //    return false;
            //}

            #endregion
            if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return false;
            }

            if (Request["ReAudit"] != null&&txtRemark.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写修改备注！');</script>");
                return false;
            }

         

            if (Request["allE_id"] == null)
            {

                string isFax = string.Format("select top 1  IsPoFax from CG_POOrder where pono='{0}' and IFZhui=0",txtPONo.Text);
                if (Convert.ToBoolean(DBHelp.ExeScalar(isFax)) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息为不含税项目！');</script>");
                    return false;
                }
                //判断项目是否有预付到款单 ，如果有需要排队新增发票单，防止 预付转支付的时候 出现金额转错误。
                string checkSumSql = string.Format("select isnull(sum(Total),0) from TB_ToInvoice where BusType=1 and PoNo='{0}' and State='通过'", txtPONo.Text);
                var payTotal = Convert.ToDecimal(DBHelp.ExeScalar(checkSumSql));
                if (payTotal > 0)
                {
                    checkSumSql = string.Format("select count(*) from Sell_OrderFP where Status='执行中' and PoNo='{0}'", txtPONo.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(checkSumSql))>0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('同一项目因为有预付款，且有他项发票流程在进行中，请审批结束后开启本流程！');</script>");
                        return false;
                    }
                }

                List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }
                string sqlCheck = string.Format("select FpLength from FpTypeBaseInfo where FpType='{0}'",
               dllFPstye.Text);
                int FpLength = Convert.ToInt32(DBHelp.ExeScalar(sqlCheck));
                if (FpLength != txtFPNo.Text.Trim().Length)
                { 
                     base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票长度必须为{0}！');</script>",FpLength));
                     return false;
                }

                string sqlFPNOCheck = string.Format("SELECT count(*) FROM [Sell_OrderFP] where PONo='{0}' and FPNo='{1}' and Status<>'不通过'",txtPONo.Text,txtFPNo.Text.Trim());
                if (Convert.ToInt32(DBHelp.ExeScalar(sqlFPNOCheck))>0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('同一项目重复发票记录！');</script>");
                    return false;
                }

                string checkFPType = string.Format("select FpType from CG_POOrder WHERE IFZhui=0 and PONo='{0}' AND Status='通过'",txtPONo.Text);
                if (DBHelp.ExeScalar(checkFPType).ToString() != dllFPstye.Text)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该发票类型和项目的发票类型不一致！');</script>");
                    return false;
                }
//                string sqlFpStyleCheck = string.Format(@"if exists(select id from Sell_OrderFP where pono='{0}' and Status<>'不通过')
//begin
//SELECT count(*) FROM [Sell_OrderFP] where PONo='{0}' and FPNoStyle='{1}' and Status<>'不通过'
//end 
//else
//begin
//select -1
//end", txtPONo.Text, dllFPstye.Text);
//                var result =Convert.ToInt32(DBHelp.ExeScalar(sqlFpStyleCheck));
//                if (result==0)
//                {
//                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票编号有误！');</script>");
//                    return false;
//                }
                //新增
                //如果项目编号 不一样，客户和发票类型 要一致这个条件是基于 填写的发票号一样的情况下
                var dt = DBHelp.getDataTable(string.Format("SELECT PONO,GuestNAME,FPNoStyle FROM [Sell_OrderFP] where FPNo='{0}' and Status<>'不通过'",txtFPNo.Text.Trim()));
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[1].ToString() != txtSupplier.Text || dr[2].ToString() != dllFPstye.Text)
                    {
                        
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票编号有误！');</script>");
                        return false;
                    }
                }
//                string sqlFpStyleCheck = string.Format(@"if exists(select id from Sell_OrderFP where FPNo='{0}' and Status<>'不通过')
//begin
//SELECT count(*) FROM [Sell_OrderFP] where GuestNAME='{1}' and FPNoStyle='{2}' and Status<>'不通过' and PONO='{3}' 
//end 
//else
//begin
//select -1
//end",txtFPNo.Text,txtSupplier.Text, dllFPstye.Text,txtPONo.Text);
//                 result = Convert.ToInt32(DBHelp.ExeScalar(sqlFpStyleCheck));
//                if (result == 0)
//                {
//                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票编号有误！');</script>");
//                    return false;
//                }


                Hashtable ht = new Hashtable();
                foreach (var model in POOrders)
                {
                    string key = model.SellOutOrderId.ToString();
                    if (!ht.Contains(key))
                        ht.Add(key, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                   
                }
                //Sell_OrderOutHousesService POSer = new Sell_OrderOutHousesService();


                CG_POOrdersService POSer = new CG_POOrdersService();
                decimal totalJin = 0;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtCheckPrice1 = gvList.Rows[i].FindControl("txtCheckPrice1") as TextBox;
                    if (txtCheckPrice1 != null)
                    {
                        if (CommHelp.VerifesToNum(txtCheckPrice1.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtCheckPrice1.Text);
                    }

                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
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
                        totalJin += POOrders[i].GoodSellPriceTotal;
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }


                    var model = POOrders[i];
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

                    string sql = string.Format(" ids={0}", model.SellOutOrderId);
                    List<CG_POOrdersFP> cars = POSer.GetListArrayToFps_Out(sql);

                    if (cars.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}]，该信息不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                        return false;
                    }

                    if (i == gvList.Rows.Count - 1)
                    {
                        if (totalJin <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('销售金额必须大于0！');</script>"));
                            return false;
                        }
                        sql = string.Format(" SellFP_Out_View.PONo='{0}'", txtPONo.Text);
                        cars = POSer.GetListArrayToFps_Out(sql);

                        if (cars[0].POTotal == 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{0}],不允许开发票！');</script>",
                                  txtPONo.Text));

                            return false;
                        }
                        //
                        if (totalJin > cars[0].WEITotals)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{1}],未开发票金额剩余[{0}]！');</script>",
                                 cars[0].WEITotals, txtPONo.Text));

                            return false;
                        }
                    }
                }

                Session["Orders"] = POOrders;

            }
            else
            {
                List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }
                Hashtable ht = new Hashtable();
                foreach (var model in POOrders)
                {
                    string key = model.SellOutOrderId.ToString();
                    if (!ht.Contains(key))
                        ht.Add(key, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                }
                //Sell_OrderOutHousesService POSer = new Sell_OrderOutHousesService();


                CG_POOrdersService POSer = new CG_POOrdersService();
                decimal totalJin = 0;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtCheckPrice1 = gvList.Rows[i].FindControl("txtCheckPrice1") as TextBox;
                    if (txtCheckPrice1 != null && txtCheckPrice1.Text!="")
                    {
                        if (CommHelp.VerifesToNum(txtCheckPrice1.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtCheckPrice1.Text);
                    }

                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null && txtNum.Text != "")
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }

                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;
                        totalJin += POOrders[i].GoodSellPriceTotal;
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null && txtGoodRemark.Text != "")
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }


                    var model = POOrders[i];
                    //string sql = string.Format(" ids={0}", model.SellOutOrderId);
                    //List<CG_POOrdersFP> cars = POSer.GetListArrayToFps(sql);

                    //if (cars.Count <= 0)
                    //{
                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}]，该信息不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text));
                    //    return false;
                    //}

                    if (i == gvList.Rows.Count - 1)
                    {

                        string sql = string.Format(" SellFP_Out_View.PONo='{0}'", txtPONo.Text);
                        decimal allWeiTotal = POSer.GetPOOrder_FPTotal_Out(txtPONo.Text, Request["allE_id"]);

                        //
                       if (totalJin > allWeiTotal)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{1}],未开发票金额剩余[{0}]！');</script>",
                                allWeiTotal, txtPONo.Text));

                            return false;
                        }
                    }
                }

                Session["Orders"] = POOrders;
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
            Image1.Enabled = false;
            txtPOName.ReadOnly = true;
            txtPONo.ReadOnly = true;
            txtFPNo.ReadOnly = !result;
            dllFPstye.Enabled = false;
            
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                FpTypeBaseInfoService fpTypeBaseInfoService = new FpTypeBaseInfoService();
                List<FpTypeBaseInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList.Insert(0, new FpTypeBaseInfo { FpType="" });
                dllFPstye.DataSource = gooQGooddList;
                dllFPstye.DataBind();
                dllFPstye.DataTextField = "FpType";
                dllFPstye.DataValueField = "FpType";
                dllFPstye.Items[gooQGooddList.Count - 1].Attributes.Add("style", "background-color: red");
                //请假单子              
                Session["Orders"] = null;
                lbtnAddFiles.Visible = false;

                gvList.Columns[0].Visible = false;
              
                gvList.Columns[8].Visible = false;

                gvList.Columns[12].Visible = false;

                gvList.Columns[15].Visible = false;

                //Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                //List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                //ddlHouse.DataSource = houseList;
                //ddlHouse.DataBind();
                //ddlHouse.DataTextField = "houseName";
                //ddlHouse.DataValueField = "id";

                btnReCala.Visible = false;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;



                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtRuTime.Text = DateTime.Now.ToString();
                        btnReCala.Visible = true;

                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        


                        gvList.Columns[8].Visible = true;

                        gvList.Columns[15].Visible = true;

                        gvList.Columns[12].Visible = true;

                        gvList.Columns[7].Visible = false;

                        gvList.Columns[14].Visible = false;
                        gvList.Columns[11].Visible = true;

                        //加载初始数据


                        List<Sell_OrderFPs> orders = new List<Sell_OrderFPs>();
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
                    else if (Request["ReAudit"] != null)//重新提交编辑
                    {
                        ddlResult.Enabled = false;
                        btnReCala.Visible = true;

                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = false;



                        gvList.Columns[8].Visible = true;

                        gvList.Columns[15].Visible = true;

                        gvList.Columns[12].Visible = true;

                        gvList.Columns[7].Visible = false;

                        gvList.Columns[14].Visible = false;
                        gvList.Columns[11].Visible = true;

                        ViewState["POOrdersIds"] = "";

                        

                        var proId = 34;
                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(proId), Convert.ToInt32(Request["allE_id"])));
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

                        Sell_OrderFPService mainSer = new Sell_OrderFPService();
                        Sell_OrderFP pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;


                        txtDoPer.Text = pp.DoPer;

                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.RuTime.ToString();


                        txtRuTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        txtSupplier.Text = pp.GuestName;
                        dllFPstye.Text = pp.FPNoStyle;
                        //ddlHouse.Text = pp.HouseID.ToString();

                        txtFPNo.Text = pp.FPNo;
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;

                        if (pp.ZhuanPayTotal > 0)
                        {
                            txtZhuanJie.Text = pp.ZhuanPayTotal.ToString();
                        }
                        Sell_OrderFPsService ordersSer = new Sell_OrderFPsService();
                        List<Sell_OrderFPs> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderFPs.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion

                        if (pp.TopTotal != 0 && pp.TopFPNo != "")
                        {
                            lblTopMess.Text = string.Format("原发票：{0}， 金额：{1}",pp.TopFPNo,pp.TopTotal);
                        }
                        if (Request["ReAudit"] != null)
                        {
                            lblTopFPNo.Text = pp.FPNo;
                            lblTopTotal.Text = pp.Total.ToString();
                        }
                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人
                            int pro_IDs = 0;
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), proId, out ids);

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

                        var ReEdit = false;
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

                            if (eforms[eforms.Count - 1].RoleName == "重新编辑")
                            {
                                ReEdit = true;

                                
                            }
                        }

                        ViewState["EformsCount"] = eforms.Count;




                        #region  加载 请假单数据

                        Sell_OrderFPService mainSer = new Sell_OrderFPService();
                        Sell_OrderFP pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        lblHiddGuid.Text = pp.InvoiceNowGuid;
                        lblHiddFpGuid.Text = pp.NowGuid;
                        ViewState["oriModel"] = pp;
                       
                        if (ReEdit && pp != null && pp.NowGuid != "")
                        { 
                            //查询之前修改的数据
                            string sql = string.Format("select * from Sell_OrderFP_History where TempGuid='{0}'", pp.NowGuid);
                            ViewState["Diff"] = DBHelp.getDataTable(sql);
                        }
                        txtName.Text = pp.CreateName;


                        txtDoPer.Text = pp.DoPer;

                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.RuTime.ToString();
                        txtSupplier.Text = pp.GuestName;
                        dllFPstye.Text = pp.FPNoStyle;
                        //ddlHouse.Text = pp.HouseID.ToString();

                        txtFPNo.Text = pp.FPNo;
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        if (pp.ZhuanPayTotal > 0)
                        {
                            txtZhuanJie.Text = pp.ZhuanPayTotal.ToString();
                        }
                        var sql1 = string.Format("select Total from [KingdeeInvoice].[dbo].[Invoice] where InvoiceNumber='{0}'", pp.FPNo);
                        var ob = DBHelp.ExeScalar(sql1);
                        lblInvoiceTotal.Text = (ob is DBNull || ob == null) ? "0" : ob.ToString();
                        Sell_OrderFPsService ordersSer = new Sell_OrderFPsService();
                        List<Sell_OrderFPs> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderFPs.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion
                        if (pp.TopTotal != 0 && pp.TopFPNo != "")
                        {
                            lblTopMess.Text = string.Format("原发票：{0}， 金额：{1}", pp.TopFPNo, pp.TopTotal);
                        }
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            if (Convert.ToInt32(Request["ProId"]) == 34)
                            {
                                ViewState["isBackUpInvoice"] = true;
                                string sql = string.Format("select count(*) from [TB_ToInvoice] where FPId={0} and State<>'不通过'", pp.Id);
                                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                                {
                                    btnSub.Text = "提交并补到款单"; btnSub.Width = 100;
                                }
                            }
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
                                    if (Convert.ToInt32(Request["ProId"]) == 34)
                                    {
                                        string sql = string.Format("select count(*) from [TB_ToInvoice] where FPId={0} and State<>'不通过'", pp.Id);
                                        if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                                        {
                                            btnSub.Text = "提交并补到款单";
                                            btnSub.Width = 100;
                                        }
                                    }
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
                                        if (Convert.ToInt32(Request["ProId"]) == 34)
                                        {
                                            string sql = string.Format("select count(*) from [TB_ToInvoice] where FPId={0} and State<>'不通过'", pp.Id);
                                            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                                            {
                                                btnSub.Text = "提交并补到款单"; btnSub.Width = 100;
                                            }
                                        }
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

        private void AddFpBack(List<Sell_OrderFPs> POOrders)
        {
            if (ddlPers.Visible == false && ddlResult.Text == "通过")
            {
                //查询数据存在不存在
                string sql = string.Format(" select count(*) from Sell_OrderFPBack where PID={0}", Convert.ToInt32(Request["allE_id"]));
                if ((int)DBHelp.ExeScalar(sql) ==0)
                {
                    var MainId = 0;
                    Sell_OrderFPBack sell_OrderFpBack = new Sell_OrderFPBack();
                    sell_OrderFpBack.FPNo = txtFPNo.Text.Trim();
                    sell_OrderFpBack.POName = txtPOName.Text;
                    sell_OrderFpBack.PONo = txtPONo.Text;
                    sell_OrderFpBack.GuestName = txtSupplier.Text;
                    sell_OrderFpBack.CreateUserId = Convert.ToInt32(Session["currentUserId"].ToString());
                    sell_OrderFpBack.CreateTime = DateTime.Now;
                    sell_OrderFpBack.FPBackTime = DateTime.Now;
                    sell_OrderFpBack.FPBackType = 1;
                    sell_OrderFpBack.Status = "通过";
                    sell_OrderFpBack.PId = Convert.ToInt32(Request["allE_id"]);

                    List<Sell_OrderFPBacks> allPOOrders = new List<Sell_OrderFPBacks>();
                    foreach (var m in POOrders)
                    {
                        Sell_OrderFPBacks fpModel = new Sell_OrderFPBacks();
                        fpModel.GoodSellPriceTotal = m.GoodSellPriceTotal;
                        fpModel.FPId = m.Ids;
                        allPOOrders.Add(fpModel);
                    }
                    var eform = new tb_EForm();
                    eform.state = "通过";
                    eform.toPer = 0;
                    eform.toProsId = 0;
                    eform.appTime = DateTime.Now;
                    eform.createTime = DateTime.Now;
                    eform.createPer = Convert.ToInt32(Session["currentUserId"].ToString());
                    eform.DoTime = DateTime.Now;
                    eform.proId = 29;//发票单签回
                    eform.appPer = Convert.ToInt32(Session["currentUserId"].ToString());
                    Sell_OrderFPBackService sell_OrderFPBackService = new Sell_OrderFPBackService();
                    if (sell_OrderFPBackService.addTran(sell_OrderFpBack, eform, allPOOrders, out MainId) > 0)
                    {

                    }
                }
                else
                {
                    sql = string.Format("update Sell_OrderFPBack set FPNo='{0}' where PID={1}",txtFPNo.Text.Trim(), Convert.ToInt32(Request["allE_id"]));
                    DBHelp.ExeScalar(sql);
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

                    Sell_OrderFP order = new Sell_OrderFP();

                    int CreatePer = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateUserId = CreatePer;

                    order.DoPer = txtDoPer.Text;
                    //order.HouseID = Convert.ToInt32(ddlHouse.SelectedValue);

                    order.RuTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.GuestName = txtSupplier.Text;
                    order.FPNo = txtFPNo.Text.Trim();

                    order.POName = txtPOName.Text;
                    order.PONo = txtPONo.Text;
                    order.FPNoStyle = dllFPstye.Text;

                    List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;

                    #endregion
                    if (Request["ReAudit"] != null)
                    {
                        order.TopFPNo = lblTopFPNo.Text;
                        order.TopTotal = Convert.ToDecimal(lblTopTotal.Text);
                    }
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
                        
                        Sell_OrderFPService POOrderSer = new Sell_OrderFPService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {

                            AddFpBack(POOrders);
                            if (ddlPers.Visible == false)
                            {
                                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(txtPONo.Text, eform.state);
                                new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(txtPONo.Text);
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
                            Sell_OrderFPService POSer = new Sell_OrderFPService();
                            var model = POSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                            if (Session["currentUserId"].ToString() != model.CreateUserId.ToString())
                            {

                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
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
                        if (Request["ReAudit"] != null)
                        {
                            eform.proId = 34;
                            eform.appTime = DateTime.Now;                            
                            eform.createTime = DateTime.Now;
                        }
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
                        var POOrderSer = new Sell_OrderFPService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        bool isBackUp = false;
                        if (Request["ReAudit"] != null)
                        {
                            isBackUp = true;

                            string sql = string.Format(" select isnull(SUM(Total),0) from TB_ToInvoice where State<>'不通过' and FPId={0}", Request["allE_id"]);
                            var total = Convert.ToDecimal(DBHelp.ExeScalar(sql));
                            if (total > 0)
                            {
                                decimal total1 = 0;
                                foreach (var m in POOrders)
                                {
                                    total1 += m.GoodSellPriceTotal;
                                }

                                if (total > total1)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                        string.Format("<script>alert('原到款单金额:{0},现发票金额:{1}，发票金额必须大于等于原到款金额！');</script>", total, total1));
                                    return;
                                }
                            }

                           var    deleteAll = string.Format(@"declare @oldFPNo  varchar(500);declare @oldPONo  varchar(500);
select top 1  @oldFPNo=FPNo,@oldPONo=PONo from Sell_OrderFP where id={0}
update  CG_POOrder set FPTotal=replace( FPTotal, @oldFPNo+'/','')
where PONo  in (select PONo from Sell_OrderFP where id={0}) and ifzhui=0;", Request["allE_id"]);
                            DBHelp.ExeCommand(deleteAll);
                        }
                        bool isBackUpInvoice = false;

                         var sql1 = string.Format("select count(*) from [TB_ToInvoice] where FPId={0} and State<>'不通过'", order.Id);
                         int invoiceHistory =Convert.ToInt32(DBHelp.ExeScalar(sql1));
                         if (invoiceHistory > 0)
                        {
                            isBackUpInvoice = true;

                            order.InvoiceNowGuid = lblHiddGuid.Text;
                        }

                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS, isBackUp, isBackUpInvoice))
                        {
                            AddFpBack(POOrders);
                            if (ddlPers.Visible == false)
                            {
                                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(txtPONo.Text, eform.state);
                                new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(txtPONo.Text);
                                TB_ToInvoiceService invoiceSer = new TB_ToInvoiceService();
                                //是销售发票的时候才生成，修改时不生成
                                if (eform.proId== 26 && !string.IsNullOrEmpty(txtZhuanJie.Text) && forms.resultState == "通过")
                                {
                                    invoiceSer.YuPay_CreateInvoice(order, Convert.ToDecimal(txtZhuanJie.Text));
                                }
                            }
                            else if (Request["ReAudit"] != null)    
                            {
                                //将原来的状态发票号修改修改
                                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(txtPONo.Text, eform.state);
                                new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(txtPONo.Text);
                            }
                            //数据进行还原
                            if (forms.resultState == "不通过" && !string.IsNullOrEmpty(lblHiddFpGuid.Text))
                            {
                                if (POOrderSer.updateTran_BakDown(lblHiddFpGuid.Text, order.Id) == false)
                                {
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票单还原失败！');</script>");
                                }
                            }


                            //项目编号 ，发票类型，发票号 进行还原
                            if ( invoiceHistory > 0 && eform.state == "通过")
                            {

                                if (!string.IsNullOrEmpty(lblHiddGuid.Text))
                                {
                                    var noewUserId = Convert.ToInt32(Session["currentUserId"].ToString());
                                    TB_ToInvoiceService invoiceService = new TB_ToInvoiceService();
                                    var historyList= invoiceService.GetListArray_History(string.Format(" TempGuid='{0}'", lblHiddGuid.Text));
                                    foreach (var model in historyList)
                                    {
                                        //项目编号 ，发票类型，发票号 进行还原
                                        model.State = "执行中";
                                        model.PoNo = txtPONo.Text;
                                        model.PoName = txtPOName.Text;
                                        model.FPNo = txtFPNo.Text.Trim();
                                        model.GuestName = txtSupplier.Text;
                                        model.TempGuid = lblHiddGuid.Text;

                                        eform = new tb_EForm();

                                        eform.toPer = noewUserId;

                                        eform.appPer = noewUserId;
                                        eform.appTime = DateTime.Now;
                                        eform.createPer = noewUserId;
                                        eform.createTime = DateTime.Now;
                                        eform.proId = 27;//到款单
                                        eform.state = "执行中";
                                        tb_EFormService eformSer = new tb_EFormService();
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUsers(0, 27, out ids);


                                        eform.toProsId = ids;
                                        if (invoiceService.addTran(model, eform) < 0)
                                        {
                                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('补到款单 失败，请手动补写！');</script>");
                                        }
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





        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                if (Session["Sell_OrderOutHousesViewSession"] != null)
                {

                    List<CG_POOrdersFP> selectedList = Session["Sell_OrderOutHousesViewSession"] as List<CG_POOrdersFP>;                 
                    if (selectedList.Count > 0)
                    {
                        List<Sell_OrderFPs> POOrders = new List<Sell_OrderFPs>();
                        decimal total = 0;
                        foreach (var model in selectedList)
                        {
                            Sell_OrderFPs fpModel = new Sell_OrderFPs();
                            fpModel.Good_Model = model.Good_Model;
                            fpModel.GoodName = model.GoodName;
                            fpModel.GoodNo = model.GoodNo;
                            fpModel.GoodNum = model.Num;
                            fpModel.GoodPrice = model.CostPrice;
                           // fpModel.GoodRemark = model.GoodRemark;
                            fpModel.GoodSellPrice = model.SellPrice;
                            fpModel.GoodSellPriceTotal = model.SellTotal;
                            fpModel.GoodSpec = model.GoodSpec;
                            fpModel.GoodTypeSmName = model.GoodTypeSmName;
                            fpModel.GoodUnit = model.GoodUnit;
                            fpModel.GooId = model.GoodId;
                            fpModel.SellOutOrderId = model.Ids;
                            fpModel.SellOutPONO = "";
                            fpModel.Total = model.CostTotal;
                            POOrders.Add(fpModel);

                            total += model.SellTotal;
                        }

                        txtPOName.Text = selectedList[0].POName;
                        txtPONo.Text = selectedList[0].PONo;
                        txtSupplier.Text = selectedList[0].GuestName;

                        TB_ToInvoiceService invoiceSer = new TB_ToInvoiceService();                       
                        var ZhuanPayTotal = invoiceSer.GetPayTotal(txtPONo.Text, total);
                        txtZhuanJie.Text = "";
                        if (ZhuanPayTotal > 0)
                        {
                            txtZhuanJie.Text = ZhuanPayTotal.ToString();
                        }

                        Session["Orders"] = POOrders;
                        Session["Sell_OrderOutHousesViewSession"] = null;
                        gvList.DataSource = POOrders;
                        gvList.DataBind();
                    }
                }
            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;
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
                List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;

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

        Sell_OrderFPs SumOrders = new Sell_OrderFPs();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderFPs model = e.Row.DataItem as Sell_OrderFPs;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;

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
        }

        protected void btnReCala_Click(object sender, EventArgs e)
        {
            List<Sell_OrderFPs> POOrders = Session["Orders"] as List<Sell_OrderFPs>;

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                TextBox txtCheckPrice1 = gvList.Rows[i].FindControl("txtCheckPrice1") as TextBox;
                if (txtCheckPrice1 != null)
                {
                    POOrders[i].GoodSellPrice = Convert.ToDecimal(txtCheckPrice1.Text);
                }

                TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                if (txtNum != null)
                {
                    POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                    POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                    POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;
                     
                }

                TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                if (txtGoodRemark != null)
                {
                    POOrders[i].GoodRemark = txtGoodRemark.Text;
                }
            }
            Session["Orders"] = POOrders;
            gvList.DataSource = POOrders;
            gvList.DataBind();
        }

        protected void txtFPNo_TextChanged(object sender, EventArgs e)
        {
            if (txtFPNo.Text.Trim() != "")
            {
                var sql = string.Format("select Total from [KingdeeInvoice].[dbo].[Invoice] where InvoiceNumber='{0}'", txtFPNo.Text.Trim());
                var ob = DBHelp.ExeScalar(sql);
                lblInvoiceTotal.Text = (ob is DBNull || ob == null) ? "0" : ob.ToString();
            }
        }
    }
}
