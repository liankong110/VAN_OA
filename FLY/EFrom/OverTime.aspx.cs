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
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Dal;
namespace VAN_OA.EFrom
{
    public partial class OverTime : System.Web.UI.Page
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
            if (txtPONo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                txtPONo.Focus();
                return false;
            }

            #region 设置自己要判断的信息
            if (txtDepartName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写部门！');</script>");
                txtDepartName.Focus();

                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return false;
            }


            if (ddlUser.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户代表！');</script>");
                ddlUser.Focus();

                return false;
            }
             

            if (txtForm.Text.Trim() == "" || txtTo.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写请假时间！');</script>");
                txtForm.Focus();

                return false;
            }

          

            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }

            txtForm.Text = txtForm.Text.Trim().Replace('：', ':');
            txtForm.Text = txtForm.Text.Trim().Replace('。', ':');
            txtForm.Text = txtForm.Text.Trim().Replace('.', ':');

            txtTo.Text = txtTo.Text.Trim().Replace('：', ':');
            txtTo.Text = txtTo.Text.Trim().Replace('.', ':');
            txtTo.Text = txtTo.Text.Trim().Replace('。', ':');

            if (CommHelp.VerifesToDateTime(txtForm.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请假时间 格式错误！');</script>");
                return false;
            }
            if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请假时间 格式错误！');</script>");
                return false;
            }
            try
            {

                if (Convert.ToDateTime( txtForm.Text) >= Convert.ToDateTime( txtTo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
                    txtForm.Focus();
                   
                    return false;
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtForm.Focus();

                return false;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtForm.Focus();

                return false;
            }
            #endregion
            if (txtTotal.ReadOnly==false&&txtTotal.Text == ""&&ddlPers.Visible==false&&ddlResult.Text== "通过")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写金额！');</script>");
                txtForm.Focus();
                return false;
            }

            if (ddlResult.Text == "通过"&&txtTotal.Text.Trim() != "")
            {
                try
                {
                    Convert.ToDecimal(txtTotal.Text);
                }
                catch (Exception)
                {
                      base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的金额有误！');</script>");
                txtTotal.Focus();

                return false;
                   
                }
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



        private void setEnable(bool result)
        {
            txtDepartName.ReadOnly = true;
            txtForm.ReadOnly = !result;
            //txtName.ReadOnly = true;
            ddlUser.Enabled = result;
            txtTo.ReadOnly = !result;
            txtreason.ReadOnly = !result;
            txtAddress.ReadOnly = !result;
            //txtSuiXingRen.ReadOnly = !result;
            //txtTime.ReadOnly = !result;
            Image1.Enabled = result;

            ImageButton1.Enabled = result;
            txtTotal.ReadOnly = !result;
            ddlOverTimeType.Enabled = result;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                lbtnSelectPONo.Visible = false;
                //请假单子              
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";

                if (base.Request["ProId"] != null)
                {
                    txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;
                   
                    txtDepartName.Text = use.LoginIPosition;

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        lbtnSelectPONo.Visible = true;
                        txtTotal.ReadOnly = true;
                        lblGuestName.Visible = false;
                        ddlUser.Visible = false;
                        lblPer.Text = "客户代表:";
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
                        tb_OverTimeSerivce timeSer = new tb_OverTimeSerivce();
                        
                        tb_OverTime timeModel = timeSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDepartName.Text = timeModel.DepartName;
                        txtForm.Text = timeModel.formTime.ToString();
                        txtName.Text = timeModel.LoginName;
                        ddlUser.Text = timeModel.guestDai;
                        txtTo.Text = timeModel.toTime.ToString();
                        txtreason.Text = timeModel.reason;

                        //txtSuiXingRen.Text = timeModel.SuixingRen;
                        txtAddress.Text = timeModel.Address;
                        //txtTime.Text = timeModel.Time.Value.ToShortDateString();
                        lblProNo.Text = timeModel.ProNo;
                        if(timeModel.Total!=null)
                        txtTotal.Text = timeModel.Total.ToString();

                        if (timeModel.OverTimeType!=null)
                        ddlOverTimeType.Text = timeModel.OverTimeType;

                        txtPOGuestName.Text = timeModel.POGuestName;
                        txtPOName.Text = timeModel.POName;
                        txtPONo.Text = timeModel.PONo;

                        showTimeSpan();
                        if (timeModel.Time.HasValue)
                            txtAppDate.Text = timeModel.Time.Value.ToString("yyyy-MM-dd hh:mm:ss");
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



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {


                    #region 获取单据基本信息
                    tb_OverTime timeInfo = new tb_OverTime();
                    timeInfo.appUseId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    timeInfo.formTime = Convert.ToDateTime(txtForm.Text);

                    if (Request["allE_id"] == null)//单据增加
                    {
                        timeInfo.guestDai = ddlPers.SelectedItem.Text;
                    }
                    else
                    {
                        timeInfo.guestDai = ddlUser.Text;
                    }


                    timeInfo.reason = txtreason.Text;
                    timeInfo.toTime = Convert.ToDateTime( txtTo.Text);

                    timeInfo.Time = DateTime.Now;
                    timeInfo.Address = txtAddress.Text;
                    timeInfo.SuixingRen = "";


                    timeInfo.OverTimeType = ddlOverTimeType.Text;

                    if (txtTotal.Text.Trim() != "")
                    {
                        timeInfo.Total = Convert.ToDecimal(txtTotal.Text);
                    }

                    timeInfo.POGuestName = txtPOGuestName.Text;
                    timeInfo.POName = txtPOName.Text;
                    timeInfo.PONo = txtPONo.Text;

                    #endregion
                    if (Request["allE_id"] == null)//单据增加
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
                        tb_OverTimeSerivce OverTimeSer = new tb_OverTimeSerivce();
                        if (OverTimeSer.addTran(timeInfo, eform) > 0)
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
                        timeInfo.id = Convert.ToInt32(Request["allE_id"]);
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
                        tb_OverTimeSerivce OverTimeSer = new tb_OverTimeSerivce();
                        if (OverTimeSer.updateTran(timeInfo, eform, forms))
                        {
                            // btnSub.Enabled = true;
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

        protected void txtForm_TextChanged(object sender, EventArgs e)
        {
            showTimeSpan();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            showTimeSpan();
        }

        private void showTimeSpan()
        {
            try
            {
                if (txtForm.Text != "" && txtTo.Text != "")
                {
                    txtForm.Text = txtForm.Text.Trim().Replace('：', ':');
                    txtForm.Text = txtForm.Text.Trim().Replace('。', ':');
                    txtForm.Text = txtForm.Text.Trim().Replace('.', ':');

                    txtTo.Text = txtTo.Text.Trim().Replace('：', ':');
                    txtTo.Text = txtTo.Text.Trim().Replace('.', ':');
                    txtTo.Text = txtTo.Text.Trim().Replace('。', ':');
                    TimeSpan ts = Convert.ToDateTime(txtTo.Text) - Convert.ToDateTime(txtForm.Text);
                    lblTiemSpan.Text = string.Format("{0:N2}", ts.Days * 24 + ts.Hours + Convert.ToDecimal(ts.Minutes) / 60) + "小时";


                    ////首先判断请假的天数
                    //TimeSpan ts = Convert.ToDateTime(txtTo.Text) - Convert.ToDateTime(txtForm.Text);


                    //DateTime fromTime = Convert.ToDateTime(txtForm.Text);
                    //DateTime toTime = Convert.ToDateTime(txtTo.Text);

                    //double lastfromTime = fromTime.TimeOfDay.Hours + Convert.ToDouble(fromTime.TimeOfDay.Minutes) / Convert.ToDouble(60);
                    //double lasttoTime = toTime.TimeOfDay.Hours + Convert.ToDouble(toTime.TimeOfDay.Minutes) / Convert.ToDouble(60);


                    //int days = ts.Days;
                    //double hours = 0;
                    //if ((ts.Hours + Convert.ToDouble(ts.Minutes) / Convert.ToDouble(60)) >= 8.25)
                    //{
                    //    days += 1;
                    //}
                    //else
                    //{
                    //    //计算小时数
                    //    if ((lastfromTime >= 9 && lastfromTime <= 12) &&
                    //        (lasttoTime >= 13.5 && lasttoTime <= 17.25))
                    //    {
                    //        hours = lasttoTime - lastfromTime - 1.5;
                    //    }
                    //    else if ((lastfromTime >= 9 && lastfromTime <= 12) &&
                    //        (lasttoTime >= 9 && lasttoTime <= 12))
                    //    {
                    //        hours = lasttoTime - lastfromTime;
                    //    }

                    //    else if ((lastfromTime >= 13.5 && lastfromTime <= 17.25) &&
                    //        (lasttoTime >= 13.5 && lasttoTime <= 17.25))
                    //    {
                    //        hours = lasttoTime - lastfromTime;
                    //    }
                    //    else
                    //    {
                    //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无效的请假时间！');</script>");
                    //        return;
                    //    }

                    //}
 

                    //lblTiemSpan.Text = days + "天" + hours + "小时" + "(共" + (days * 6.75 + hours) + "小时)";

                }
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");


            }
        }

        protected void txtTime_TextChanged(object sender, EventArgs e)
        {
            showTimeSpan();
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
                #region 需要审批的需要指定该项目AE

                try
                {

                    var user = new SysUserService().getAllUserByLoginName(string.Format(" and loginName='{0}'", model.AE))[0];
                    //ddlUser.Text = user.Id.ToString();
                    //ddlUser.Enabled = false;
                    ddlPers.Text = user.Id.ToString();
                    ddlPers.Enabled = false;
                }
                catch (Exception)
                {                    
                     base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('没有找到对应的AE！');</script>");
                }
                #endregion
            }
        }
    }
}
