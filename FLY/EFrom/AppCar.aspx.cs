﻿using System;
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
namespace VAN_OA.EFrom
{
    public partial class AppCar : System.Web.UI.Page
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

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写申请日期！');</script>");
                txtDateTime.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtDateTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return false;
                }
            }
            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }

            if (txtCompName.Text.Trim() == "")
            {
            
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写公司名称！');</script>");
                txtName.Focus();

                return false;
            }
            
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion


            return true;
        }



        private void setEnable(bool result)
        {
            txtsuiChePer.ReadOnly = !result;
            txtAddress.ReadOnly = !result;
            txtCompName.ReadOnly = !result;
            txtDateTime.ReadOnly = !result;
            txtDepartName.ReadOnly = !result;
            txtInvName.ReadOnly = !result;
            //txtName.ReadOnly = !result;          


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              


                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;
                   
                    txtDepartName.Text = use.LoginIPosition;

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {

                        rdoWang.Checked = true;

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
                                //从获取出的审核中 获取上级信息
                                List<A_Role_User> newList = new List<A_Role_User>();
                                for (int i = 0; i < roleUserList.Count; i++)
                                {
                                    if (roleUserList[i].UserId == use.ReportTo)
                                    {
                                        A_Role_User a = roleUserList[i];
                                        newList.Add(a);
                                        break;
                                    }
                                }

                                if (newList.Count > 0)
                                {
                                    ddlPers.DataSource = newList;
                                }
                                else
                                {
                                    ddlPers.DataSource = roleUserList;
                                }
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
                        tb_AppCarService AppCarSer = new tb_AppCarService();


                      
                        tb_AppCar appCarModel = AppCarSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtAddress.Text = appCarModel.address;
                        txtCompName.Text = appCarModel.compName;
                        txtDateTime.Text = appCarModel.datetiem.ToString();
                        txtDepartName.Text = appCarModel.DepartName;
                        txtInvName.Text = appCarModel.invName;
                        txtName.Text = appCarModel.LoginName;
                        txtsuiChePer.Text = appCarModel.suiChePer;
                        if (appCarModel.type == "单程")
                        {
                            rdoDan.Checked = true;
                        }
                        else
                        {
                            rdoWang.Enabled = true;
                        }
                        lblProNo.Text = appCarModel.ProNo;
 
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
                                    {//从获取出的审核中 获取上级信息
                                        List<A_Role_User> newList = new List<A_Role_User>();
                                        for (int i = 0; i < roleUserList.Count; i++)
                                        {
                                            if (roleUserList[i].UserId == use.ReportTo)
                                            {
                                                A_Role_User a = roleUserList[i];
                                                newList.Add(a);
                                                break;
                                            }
                                        }

                                        if (newList.Count > 0)
                                        {
                                            ddlPers.DataSource = newList;
                                        }
                                        else
                                        {
                                            ddlPers.DataSource = roleUserList;
                                        }
                                        // ddlPers.DataSource = roleUserList;
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
                                            //从获取出的审核中 获取上级信息
                                            List<A_Role_User> newList = new List<A_Role_User>();
                                            for (int i = 0; i < roleUserList.Count; i++)
                                            {
                                                if (roleUserList[i].UserId == use.ReportTo)
                                                {
                                                    A_Role_User a = roleUserList[i];
                                                    newList.Add(a);
                                                    break;
                                                }
                                            }

                                            if (newList.Count > 0)
                                            {
                                                ddlPers.DataSource = newList;
                                            }
                                            else
                                            {
                                                ddlPers.DataSource = roleUserList;
                                            }
                                            //ddlPers.DataSource = roleUserList;
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
                    tb_AppCar appCarInfo = new tb_AppCar();
                    appCarInfo.address = txtAddress.Text;
                    appCarInfo.appUserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    appCarInfo.compName = txtCompName.Text;
                    appCarInfo.datetiem = Convert.ToDateTime(txtDateTime.Text);
                    appCarInfo.invName = txtInvName.Text;
                    appCarInfo.suiChePer = txtsuiChePer.Text;
                   
                    if (rdoDan.Checked)
                    {
                        appCarInfo.type = "单程";
                    }
                    else
                    {
                        appCarInfo.type = "往返";
                    }
                    

                    #endregion
                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId;
                        eform.appTime = Convert.ToDateTime(txtDateTime.Text);
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
                        tb_AppCarService carSer = new tb_AppCarService();
                        if (carSer.addTran(appCarInfo, eform) > 0)
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
                        appCarInfo.id = Convert.ToInt32(Request["allE_id"]);
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
                        tb_AppCarService carSer = new tb_AppCarService();
                        if (carSer.updateTran(appCarInfo, eform, forms))
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

        
    }
}
