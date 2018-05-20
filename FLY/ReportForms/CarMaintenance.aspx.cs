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
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.EFrom;
using System.Collections.Generic;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class CarMaintenance : System.Web.UI.Page
    {
        private TB_CarMaintenanceService carMaintenanceSer = new TB_CarMaintenanceService();

        protected void btnClose_Click(object sender, EventArgs e)
        {

            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    TB_CarMaintenance carModel = new TB_CarMaintenance();
                    carModel.CardNo = ddlCarNo.Text;
                    carModel.CreateTime = DateTime.Now;
                    carModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    if (txtDistance.Text != "")
                        carModel.Distance = Convert.ToDecimal(txtDistance.Text);
                    carModel.MaintenanceTime = Convert.ToDateTime(txtMaintenanceTime.Text);
                    carModel.Remark = txtRemark.Text;
                    carModel.ReplaceRemark = txtReplaceRemark.Text;
                    carModel.ReplaceStatus = txtReplaceStatus.Text;



                    //if (this.carMaintenanceSer.Add(carModel) > 0)
                    //{
                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                    //    ddlCarNo.Text = "";
                    //    txtDistance.Text = "";
                    //    txtMaintenanceTime.Text = "";
                    //    txtRemark.Text = "";
                    //    txtReplaceRemark.Text = "";
                    //    txtReplaceStatus.Text = "";



                    //    this.ddlCarNo.Focus();
                    //}
                    //else
                    //{
                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    //}
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    base.Response.Redirect(Session["POUrl"].ToString());
        //}


        private void setEnable(bool result)
        {
            ddlCarNo.Enabled = result;
            txtDistance.ReadOnly = !result;
            txtMaintenanceTime.ReadOnly = !result;
            txtRemark.ReadOnly = !result;
            txtReplaceRemark.ReadOnly = !result;
            txtReplaceStatus.ReadOnly = !result;
            Image1.Enabled = result;
            txtTotal.ReadOnly = !result;
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {


                    TB_CarMaintenance carModel = new TB_CarMaintenance();
                    carModel.CardNo = ddlCarNo.Text;
                    carModel.CreateTime = DateTime.Now;
                    carModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    if (txtDistance.Text != "")
                        carModel.Distance = Convert.ToDecimal(txtDistance.Text);
                    carModel.MaintenanceTime = Convert.ToDateTime(txtMaintenanceTime.Text);
                    carModel.Remark = txtRemark.Text;
                    carModel.ReplaceRemark = txtReplaceRemark.Text;
                    carModel.ReplaceStatus = txtReplaceStatus.Text;

                    carModel.Id = Convert.ToInt32(base.Request["allE_id"]);
                    if (this.carMaintenanceSer.Update(carModel))
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


            if (this.ddlCarNo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择车牌号！');</script>");
                this.ddlCarNo.Focus();
                return false;
            }

            if (this.txtMaintenanceTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写保养/加油时间！');</script>");
                this.txtMaintenanceTime.Focus();
                return false;
            }
            if (txtMaintenanceTime.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtMaintenanceTime.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的日期格式有误！');</script>");
                    txtMaintenanceTime.Focus();
                    return false;
                }
            }


            if (this.txtDistance.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写公里数！');</script>");
                this.txtDistance.Focus();
                return false;
            }
            if (CommHelp.VerifesToNum(txtDistance.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('公里数 格式错误！');</script>");
                this.txtDistance.Focus();
                return false;
            }

            if (txtTotal.Text != "")
            {
                try
                {
                    Convert.ToDecimal(txtTotal.Text);
                }
                catch (Exception)
                {
                     base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('金额格式有误！');</script>");
                    this.txtTotal.Focus();
                    return false;
                     
                }
            }


            return true;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.Text = "";

                if (base.Request["ProId"] != null)
                {
                   

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        //加载基本数据
                        VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                        lblName.Text = use.LoginName;

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

                        #region  加载数据
                        TB_CarMaintenance CarMaiModel = this.carMaintenanceSer.GetModel(Convert.ToInt32(base.Request["allE_id"]));
                        ddlCarNo.Text = CarMaiModel.CardNo;
                        if (CarMaiModel.Distance != null)
                            txtDistance.Text = CarMaiModel.Distance.ToString();
                        txtMaintenanceTime.Text = CarMaiModel.MaintenanceTime.ToString();
                        txtRemark.Text = CarMaiModel.Remark;
                        if (CarMaiModel.ReplaceRemark != null)
                            txtReplaceRemark.Text = CarMaiModel.ReplaceRemark;
                        if (CarMaiModel.ReplaceStatus != null)
                            txtReplaceStatus.Text = CarMaiModel.ReplaceStatus;

                        lblName.Text = CarMaiModel.UserName;
                        lblProNo.Text = CarMaiModel.UseState;
                        txtAppDate.Text = CarMaiModel.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                        if (CarMaiModel.Total != null) txtTotal.Text = CarMaiModel.Total.ToString();
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
                //保养
                if (Request["Type"] != null && Request["Type"] == "Edit")
                {
                    btnUpdate.Visible = true;
                    txtMaintenanceTime.ReadOnly = false;
                    txtDistance.ReadOnly = false;
                    txtTotal.ReadOnly = false;
                    txtReplaceRemark.ReadOnly = false;
                    txtReplaceStatus.ReadOnly = false;
                    txtRemark.ReadOnly = false;

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
                    TB_CarMaintenance carModel = new TB_CarMaintenance();
                    carModel.CardNo = ddlCarNo.Text;
                    carModel.CreateTime = DateTime.Now;
                    carModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    if (txtDistance.Text != "")
                        carModel.Distance = Convert.ToDecimal(txtDistance.Text);
                    carModel.MaintenanceTime = Convert.ToDateTime(txtMaintenanceTime.Text);
                    carModel.Remark = txtRemark.Text;
                    carModel.ReplaceRemark = txtReplaceRemark.Text;
                    carModel.ReplaceStatus = txtReplaceStatus.Text;

                    if (txtTotal.Text != "") carModel.Total = Convert.ToDecimal(txtTotal.Text);
                    #endregion


                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", lblName.Text)));
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

                        if (carMaintenanceSer.addTran(carModel, eform) > 0)
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
                        carModel.Id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();


                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", lblName.Text)));
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
                        
                        if (carMaintenanceSer.updateTran(carModel, eform, forms))
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
