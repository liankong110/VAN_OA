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
    public partial class WFSell_OrderOutHouseBack : System.Web.UI.Page
    {
        protected string GetValue(object value)
        {
            return string.Format("{0:n4}", Convert.ToDecimal(value));
        }
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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写出库日期！');</script>");
                txtRuTime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtRuTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库日期 格式错误！');</script>");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtSellProNo.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你选择出库单号！');</script>");
                txtSellProNo.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写出库日期格式有误！');</script>");
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

            //if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验人不存在！');</script>");            

            //    return false;
            //}
            
            #endregion


            if (rdoA.Checked == false && rdoB.Checked == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择签收状态！');</script>");
                return false;
            }
            if (Request["allE_id"] == null)
            {
                //List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;

                //if (POOrders == null || POOrders.Count <= 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                //    return false;
                //}

                //if (POOrders.Count == 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择销售商品！');</script>");

                //    return false;
                    
                //}                

            }
            else
            {

               
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;
            Panel1.Enabled = false;               
            txtRemark.ReadOnly = true;
            txtRuTime.ReadOnly = true;             
            Image1.Enabled = false;
            txtPOName.ReadOnly = true;
            txtPONo.ReadOnly = true;
            txtSellProNo.ReadOnly = true;
        }

        private void SetRole(int Count)
        {
            //打印
            //if (Count == 0)
            //{
            //    btnSub.Text = "打印";
            //    gvList.Columns[7].Visible = false;
            //    gvList.Columns[8].Visible = true;
            //}
        }
       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
               
                //请假单子              
                Session["Orders"] = null;              
                lbtnAddFiles.Visible = false;  
               
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

                        //加载初始数据                        
                        List<Sell_OrderOutHouseBacks> orders = new List<Sell_OrderOutHouseBacks>();
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
                        ViewState["POOrdersIds"] = "";

                        //权限1（销售）
                        lbtnAddFiles.Visible = true;     
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

                        Sell_OrderOutHouseBackService mainSer = new Sell_OrderOutHouseBackService();
                        Sell_OrderOutHouseBack pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtSellProNo.Text = pp.SellProNo;
                        if (pp.BackType == 0)
                        {
                            rdoA.Checked = true;
                        }
                        else
                        {
                            rdoB.Checked = true;
                        }

                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.BackTime.ToString();
                        txtSupplier.Text = pp.GuestName;
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;


                        Sell_OrderOutHouseBacksService ordersSer = new Sell_OrderOutHouseBacksService();
                        List<Sell_OrderOutHouseBacks> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderOutHouseBacks.id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion

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

                        Sell_OrderOutHouseBackService mainSer = new Sell_OrderOutHouseBackService();
                        Sell_OrderOutHouseBack pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;
                        txtSellProNo.Text = pp.SellProNo;
                        if (pp.BackType == 0)
                        {
                            rdoA.Checked = true;
                        }
                        else
                        {
                            rdoB.Checked = true;
                        }

                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.BackTime.ToString();
                        txtSupplier.Text = pp.GuestName;
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;


                        Sell_OrderOutHouseBacksService ordersSer = new Sell_OrderOutHouseBacksService();
                        List<Sell_OrderOutHouseBacks> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderOutHouseBacks.id=" + Request["allE_id"]);
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
                                SetRole(eforms.Count);
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
                                    SetRole(eforms.Count);
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

                    Sell_OrderOutHouseBack order = new Sell_OrderOutHouseBack();

                    int CreatePer=Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateUserId = CreatePer;
                 
                    order.SellProNo =txtSellProNo.Text;
                    //order.HouseID = Convert.ToInt32(ddlHouse.SelectedValue);
                   
                    order.BackTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.GuestName = txtSupplier.Text;
                  
                    order.POName = txtPOName.Text;
                    order.PONo = txtPONo.Text;

                    order.SellTotal = Convert.ToDecimal(lblTotal.Text); 
                    if (rdoA.Checked)
                    {
                        order.BackType = 0;
                    }
                    if (rdoB.Checked)
                    {
                        order.BackType = 1;
                    }

                  
                    List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;

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


                        Sell_OrderOutHouseBackService POOrderSer = new Sell_OrderOutHouseBackService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {

                            if (ddlPers.Visible == false)
                            {
                                POOrderSer.SellOrderBackUpdatePoStatus(txtPONo.Text);
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
                            Sell_OrderOutHouseBackService POSer = new Sell_OrderOutHouseBackService();
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
                        Sell_OrderOutHouseBackService POOrderSer = new Sell_OrderOutHouseBackService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        string url="";
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            if (ddlPers.Visible == false)
                            {
                                 POOrderSer.SellOrderBackUpdatePoStatus(txtPONo.Text);
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
                if (Session["OrderInHouseSession"] != null)
                {

                    object selectedId = Session["OrderInHouseSession"];
                    if (selectedId != null)
                    {

                        Sell_OrderOutHousesService sellOrderServer = new Sell_OrderOutHousesService();
                        Sell_OrderOutHouseService sellOrderMainServer = new Sell_OrderOutHouseService();
                        Sell_OrderOutHouse sell_OrderOutHouseModel = sellOrderMainServer.GetModel(Convert.ToInt32(selectedId));                     
                        txtSupplier.Text = sell_OrderOutHouseModel.Supplier;
                        txtSellProNo.Text = sell_OrderOutHouseModel.ProNo;                        
                        txtPOName.Text = sell_OrderOutHouseModel.POName;
                        txtPONo.Text = sell_OrderOutHouseModel.PONo;
                        lblTotal.Text = sell_OrderOutHouseModel.SellTotal.ToString();

                        List<Sell_OrderOutHouses> POOrders = sellOrderServer.Sell_OrderOutHouse_Sell_OrderInHouse_ListView(" id=" + selectedId); ;

                        List<Sell_OrderOutHouseBacks> sellOrderOutList = new List<Sell_OrderOutHouseBacks>();
                        foreach (var model in POOrders)
                        {
                            Sell_OrderOutHouseBacks sellOrderOutModel = new Sell_OrderOutHouseBacks()
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
                                SellId = model.Ids,                                
                                Total = model.Total,       
                                


                            };
                            sellOrderOutList.Add(sellOrderOutModel);
                        }
                        Session["Orders"] = sellOrderOutList;

                        Session["OrderInHouseSession"] = null;
                        gvList.DataSource = sellOrderOutList;
                        gvList.DataBind();
                    }
                //}
 
                    //List<Sell_OrderOutHouseBacks> selectedList = Session["DioSellWFHouseGoods"] as List<Sell_OrderOutHouseBacks>;
                    //if (selectedList.Count > 0)
                    //{
                    //    List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;
                    //    if (POOrders == null) POOrders = new List<Sell_OrderOutHouseBacks>();
                    //    POOrders.AddRange(selectedList);
                    //    Session["DioSellWFHouseGoods"] = null;
                    //    gvList.DataSource = POOrders;
                    //    gvList.DataBind();
                    //    Session["Orders"] = POOrders;
                    //}
                }      
            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;
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
                List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;

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

        Sell_OrderOutHouseBacks SumOrders = new Sell_OrderOutHouseBacks();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderOutHouseBacks model = e.Row.DataItem as Sell_OrderOutHouseBacks;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;
                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;

            }
            //ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            //if (btnEdit != null)
            //{
            //    string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
            //    btnEdit.Attributes.Add("onclick", val);
            //}


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.GoodNum.ToString());//数量                
           
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
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtSupplier.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string url = string.Format("WFSell_OrderOutHousePrint.aspx?Id={0}",Request["allE_id"]);

            Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
        }

        protected void btnReCala_Click(object sender, EventArgs e)
        {
            if (Request["allE_id"] == null)
            {
                List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return ;
                }

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        if (POOrders[i].GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");

                            return;
                        }
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }


                    TextBox txtGoodSellPrice = gvList.Rows[i].FindControl("txtGoodSellPrice") as TextBox;
                    if (txtGoodSellPrice != null)
                    {
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtGoodSellPrice.Text);

                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;

                    }
                }

                gvList.DataSource = POOrders;
                gvList.DataBind();

            }
            else
            {

                
                List<Sell_OrderOutHouseBacks> POOrders = Session["Orders"] as List<Sell_OrderOutHouseBacks>;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null && txtNum.Text != "")
                    {
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        if (POOrders[i].GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");
                            return ;
                        }

                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;                     
                       

                    }
                }

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }
   

      

     
    }
}
