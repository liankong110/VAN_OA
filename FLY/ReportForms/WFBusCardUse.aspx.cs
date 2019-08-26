using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;
using System.Data;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;


namespace VAN_OA.ReportForms
{
    public partial class WFBusCardUse : System.Web.UI.Page
    {
        private TB_BusCardUseService busCardSer = new TB_BusCardUseService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    string BusCardNo = this.ddlCardNo.Text;
                    DateTime BusCardDate = DateTime.Parse(this.txtBusCardDate.Text);
                    string Address = this.txtAddress.Text;
                    string GuestName = this.txtGuestName.Text;
                    decimal UseTotal = decimal.Parse(this.txtUseTotal.Text);
                    string BusUseRemark = this.txtBusUseRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BusCardUse model = new VAN_OA.Model.ReportForms.TB_BusCardUse();
                    model.BusCardNo = BusCardNo;
                    model.BusCardPer = "";
                    model.BusCardDate = BusCardDate;
                    model.Address = Address;
                    model.GuestName = GuestName;
                    model.UseTotal = UseTotal;
                    model.BusUseRemark = BusUseRemark;
                    model.POGuestName = txtPOGuestName.Text;
                    model.POName = txtPOName.Text;
                    model.PONo = txtPONo.Text;

                    model.CreateUserId = Convert.ToInt32(Session["currentUserId"]);
                    if (this.busCardSer.Add(model) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        this.txtBusCardDate.Text = "";
                        this.txtBusCardDate.Text = "";
                        this.txtAddress.Text = "";
                        this.txtGuestName.Text = "";
                        this.txtUseTotal.Text = "";
                        this.txtBusUseRemark.Text = "";
                        txtPONo.Text = "";
                        txtPOName.Text = "";
                        txtPOGuestName.Text = "";
                        this.ddlCardNo.Focus();
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
            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }
            else
            {
                Response.Write("<script>javascript:history.back(-1);</script>");
                //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>javascript:history.back(-1);</script>");               
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string BusCardNo = this.ddlCardNo.Text;
                    DateTime BusCardDate = DateTime.Parse(this.txtBusCardDate.Text);
                    string Address = this.txtAddress.Text;
                    string GuestName = this.txtGuestName.Text;
                    decimal UseTotal = decimal.Parse(this.txtUseTotal.Text);
                    string BusUseRemark = this.txtBusUseRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BusCardUse model = new VAN_OA.Model.ReportForms.TB_BusCardUse();
                    model.BusCardNo = BusCardNo;
                    model.BusCardPer = "";
                    model.BusCardDate = BusCardDate;
                    model.Address = Address;
                    model.GuestName = GuestName;
                    model.UseTotal = UseTotal;
                    model.BusUseRemark = BusUseRemark;
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    model.POGuestName = txtPOGuestName.Text;
                    model.POName = txtPOName.Text;
                    model.PONo = txtPONo.Text;

                    if (this.busCardSer.Update(model))
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

            if (this.txtBusCardDate.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                this.txtBusCardDate.Focus();
                return false;
            }

            if (CommHelp.VerifesToDateTime(txtBusCardDate.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false;
            }
            if (this.txtUseTotal.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填金额！');</script>");
                this.txtUseTotal.Focus();
                return false;
            }


            try
            {
                Convert.ToDecimal(txtUseTotal.Text);
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的数字格式有误！');</script>");
                txtUseTotal.Focus();
                return false;
            }

            if (txtPONo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                txtPONo.Focus();
                return false;
            }



            if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return false;
            }
            if (CG_POOrderService.IsClosePONO(txtPONo.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('此项目已经关闭！');</script>");
                return false;
            }
            if (CG_POOrderService.IsSpecialPONO(txtPONo.Text, txtPOName.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('特殊订单无法计入费用！');</script>");
                return false;
            }

            return true;
        }

        private void ShowInfo(int Id)
        {
            VAN_OA.Model.ReportForms.TB_BusCardUse model = busCardSer.GetModel(Id);
            ddlUser.Text = model.UseName;
            this.ddlCardNo.Text = model.BusCardNo;
            lblProNo.Text = model.ProNo;
            this.txtBusCardDate.Text = string.Format("{0:yyyy-MM-dd}", model.BusCardDate);
            this.txtAddress.Text = model.Address;
            this.txtGuestName.Text = model.GuestName;
            this.txtUseTotal.Text = model.UseTotal.ToString();
            this.txtBusUseRemark.Text = model.BusUseRemark;
            txtPOGuestName.Text = model.POGuestName;
            txtPOName.Text = model.POName;
            txtPONo.Text = model.PONo;

        }
        private void setEnable(bool result)
        {
            this.txtBusCardDate.Enabled = result;
            this.txtAddress.Enabled = result;
            this.txtGuestName.Enabled = result;
            this.txtUseTotal.Enabled = result;
            this.txtBusUseRemark.Enabled = result;
            ddlUser.Enabled = result;
            ddlCardNo.Enabled = result;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (Session["backurl"] != null)
                {
                    Button2.Visible = false;
                }
                else
                {
                    Button1.Visible = false;
                }

                lbtnSelectPONo.Visible = false;
                DataTable cardInfo = DBHelp.getDataTable("select BusNo as CardNo from Base_BusInfo where IsStop=0");
                ddlCardNo.DataSource = cardInfo;
                ddlCardNo.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getZaiZhiList(" and loginStatus='在职' and loginName<>'admin'");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";


                if (base.Request["ProId"] != null)
                {

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        lbtnSelectPONo.Visible = true;
                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {
                            //获取审批人
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getUsers(0, Convert.ToInt32(Request["ProId"]), out ids);

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
                    else if (Request["ReAudit"] != null)//再次编辑
                    {

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

                        #region  加载 请假单数据

                        ShowInfo(Convert.ToInt32(base.Request["allE_id"]));


                        #endregion

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;



                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人

                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

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

                        #region  加载 请假单数据
                        ShowInfo(Convert.ToInt32(base.Request["allE_id"]));
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
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                }

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
                                        setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                    }
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
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtPOGuestName.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
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
                    string BusCardNo = this.ddlCardNo.Text;
                    DateTime BusCardDate = DateTime.Parse(this.txtBusCardDate.Text);
                    string Address = this.txtAddress.Text;
                    string GuestName = this.txtGuestName.Text;
                    decimal UseTotal = decimal.Parse(this.txtUseTotal.Text);
                    string BusUseRemark = this.txtBusUseRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BusCardUse model = new VAN_OA.Model.ReportForms.TB_BusCardUse();
                    model.BusCardNo = BusCardNo;
                    model.BusCardPer = "";
                    model.BusCardDate = BusCardDate;
                    model.Address = Address;
                    model.GuestName = GuestName;
                    model.UseTotal = UseTotal;
                    model.BusUseRemark = BusUseRemark;
                    model.POGuestName = txtPOGuestName.Text;
                    model.POName = txtPOName.Text;
                    model.PONo = txtPONo.Text;
                    model.CreateUserId = Convert.ToInt32(Session["currentUserId"]);
                    model.UseName = ddlUser.Text;
                    #endregion
                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();
                        int userId = CreateUser;
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
                        if (busCardSer.addTran(model, eform) > 0)
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
                        model.Id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();
                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = CreateUser;// Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
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
                        if (busCardSer.updateTran(model, eform, forms))
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
                }
            }
        }
    }
}
