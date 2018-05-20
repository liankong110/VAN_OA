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

namespace VAN_OA.EFrom
{
    public partial class ProjectInvs : System.Web.UI.Page
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

            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return false;
            }


            if (txtProName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目名称！');</script>");
                txtName.Focus();

                return false;
            }

            if (txtCreateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtCreateTime.Focus();
                return false;
            }
            if (CommHelp.VerifesToDateTime(txtCreateTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false;
            }


            //if (ddlPers.SelectedItem == null || ddlPers.Text == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择各户代表！');</script>");
            //    ddlPers.Focus();
            //    return false;
            //}


            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();
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
           // txtBuyTime.ReadOnly = !result;
            //txtName.ReadOnly = true;
            txtProName.ReadOnly = !result;
            txtCreateTime.ReadOnly = !result;
            Image1.Enabled = result;
            ddlProState.Enabled = result;
            ddlUser.Enabled = result;
        }

        private void setTotal(List<Tb_ProjectInvs> models)
        {
            decimal total = 0;
            foreach (Tb_ProjectInvs mo in models)
            {
                total += mo.Total == null ? 0 : Convert.ToDecimal(mo.Total);
            }

            lblTotal.Text = total.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


                btnReSet.Visible = false;

                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";


                Session["ProInvs"] = null;
                gvList.Columns[0].Visible = false;
                gvList.Columns[1].Visible = false;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                plProInvs.Visible = false;
                btnEdit.Visible = false;
                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;



                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {

                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        plProInvs.Visible = true;
                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;

                        //加载初始数据

                        List<Tb_ProjectInvs> proInvsList = new List<Tb_ProjectInvs>();
                        Session["ProInvs"] = proInvsList;
                        ViewState["ProInvsCount"] = proInvsList.Count;
                        
                        gvList.DataSource = proInvsList;
                        gvList.DataBind();

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

                        ViewState["ProInvsIds"] = "";

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
                        Tb_ProjectInvService paProInvSer = new Tb_ProjectInvService();
                        Tb_ProjectInvsService sonProInvSer = new Tb_ProjectInvsService();

                        Tb_ProjectInv pProModel = paProInvSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pProModel.LoginName;
                        txtCreateTime.Text = pProModel.CreateTime.Value.ToString("yyyy-MM-dd");
                        txtProName.Text = pProModel.ProName;
                        txtProNo.Text = pProModel.ProNo;
                        ddlProState.Text = pProModel.State;

                        lblProInvState.Text= ddlProState.Text;
                        lblProNo.Text = pProModel.ProNo;
                        if (pProModel.State == "已完工")
                        {
                            lblProInvState.ForeColor = System.Drawing.Color.Red;
                            
                        }
                        if (pProModel.State == "未完工")
                        {

                        }
                        try
                        {
                            ddlUser.SelectedValue = pProModel.GuestId.ToString();
                        }
                        catch (Exception)
                        {


                        }

                     
                        List<Tb_ProjectInvs> proInvsList = sonProInvSer.GetListArray(" 1=1 and PId=" + Request["allE_id"]);
                        setTotal(proInvsList);
                        Session["ProInvs"] = proInvsList;
                        ViewState["ProInvsCount"] = proInvsList.Count;

                        gvList.DataSource = proInvsList;
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
                            btnReSet.Visible = true;
                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {

                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                                        setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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


                            //判断是不是自己填写的单子
                            if (pProModel.UserId.ToString() == base.Session["currentUserId"].ToString())
                            {
                                gvList.Columns[0].Visible = true;
                                gvList.Columns[1].Visible = true;
                               
                                btnEdit.Visible = true;
                                plProInvs.Visible = true;
                                setEnable(true);
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
                    if (Request["allE_id"] != null)
                    {
                        if (ddlProState.Text != "已完工")
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工程尚未完工,无法审核！');</script>");
                            txtName.Focus();

                           
                            return;
                        }
                    }


                    #region 获取单据基本信息
                    Tb_ProjectInv model = new Tb_ProjectInv();
                    model.UserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));


                    model.ProName = txtProName.Text;
                    model.CreateTime = Convert.ToDateTime(txtCreateTime.Text);
                    model.State = ddlProState.Text;
                    model.GuestId = Convert.ToInt32(ddlUser.SelectedValue);

                    List<Tb_ProjectInvs> projectList = Session["ProInvs"] as List<Tb_ProjectInvs>;
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
                        eform.E_Remark = txtProName.Text;
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
                        Tb_ProjectInvService proMainSer = new Tb_ProjectInvService();
                        if (proMainSer.addTran(model, eform, projectList) > 0)
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
                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId;

                        eform.E_Remark = txtProName.Text;


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
                        string IDS = ViewState["ProInvsIds"].ToString();
                        Tb_ProjectInvService proMainSer = new Tb_ProjectInvService();
                        if (proMainSer.updateTran(model, eform, forms, projectList, IDS))
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            btnCancel.Enabled = true;
            
            this.txtBuyTime.Text = "";
            this.txtInvModel.Text = "";
            this.txtInvName.Text = "";
            this.txtInvUnit.Text = "";
            this.txtInvNum.Text = "";
            this.txtInvPrice.Text = "";
            this.txtInvCarPrice.Text = "";
            this.txtInvTaskPrice.Text = "";
            this.txtInvManPrice.Text = "";
            this.txtRemark.Text = "";
            ViewState["state"] = "add";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            #region 设置自己要判断的信息

            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return ;
            }


            if (txtProName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目名称！');</script>");
                txtName.Focus();

                return ;
            }

            if (txtCreateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtCreateTime.Focus();
                return ;
            }


          
            try
            {

                Convert.ToDateTime(txtCreateTime.Text);

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtCreateTime.Focus();

                return ;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return ;
            }
            #endregion


            Tb_ProjectInv model = new Tb_ProjectInv();            

            model.ProName = txtProName.Text;
            model.CreateTime = Convert.ToDateTime(txtCreateTime.Text);
            model.State = ddlProState.Text;
            model.GuestId = Convert.ToInt32(ddlUser.SelectedValue);
            List<Tb_ProjectInvs> projectList = Session["ProInvs"] as List<Tb_ProjectInvs>;
            model.Id = Convert.ToInt32(Request["allE_id"]);

            string IDS = ViewState["ProInvsIds"].ToString();
            Tb_ProjectInvService proMainSer = new Tb_ProjectInvService();
            if (proMainSer.updateTran(model,projectList, IDS))
            {
                string sql =string.Format( "update tb_EForm set e_Remark='{0}' where id={1}" ,txtProName.Text,Request["EForm_Id"]);
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                
            }
            else
            {
                btnSub.Enabled = false;
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtBuyTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写购买日期！');</script>");
                txtBuyTime.Focus();
                return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtBuyTime.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('购买日期格式错误！');</script>");
                    txtBuyTime.Focus();
                    return;
                }
            }
            if (txtInvName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('材料名称不能为空！');</script>");
                txtInvName.Focus();
                return;
            }

            if (txtInvNum.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量不能为空！');</script>");
                txtInvName.Focus();
                return;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txtInvNum.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量格式错误！');</script>");
                    txtBuyTime.Focus();
                    return;
                }


                try
                {
                    if (txtInvPrice.Text != "")
                        Convert.ToDecimal(txtInvPrice.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('材料费格式错误！');</script>");
                    txtInvPrice.Focus();
                    return;
                }



                try
                {
                    if (txtInvCarPrice.Text != "")
                        Convert.ToDecimal(txtInvCarPrice.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('运费格式错误！');</script>");
                    txtInvCarPrice.Focus();
                    return;
                }


                try
                {
                    if (txtInvTaskPrice.Text != "")
                        Convert.ToDecimal(txtInvTaskPrice.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('会务费格式错误！');</script>");
                    txtInvTaskPrice.Focus();
                    return;
                }

                try
                {
                    if (txtInvManPrice.Text != "")
                        Convert.ToDecimal(txtInvManPrice.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理费格式错误！');</script>");
                    txtInvManPrice.Focus();
                    return;
                }


            }
            DateTime BuyTime = DateTime.Parse(this.txtBuyTime.Text);
            decimal total = 0;
            string InvModel = this.txtInvModel.Text;
            string InvName = this.txtInvName.Text;
            string InvUnit = this.txtInvUnit.Text;
            decimal InvNum = decimal.Parse(this.txtInvNum.Text);
            decimal? InvPrice = null;
            if (txtInvPrice.Text != "")
            {
                InvPrice = decimal.Parse(this.txtInvPrice.Text);

                total += decimal.Parse(this.txtInvPrice.Text);
            }

            decimal? InvCarPrice = null;
            if (txtInvCarPrice.Text != "")
            {
                InvCarPrice = decimal.Parse(this.txtInvCarPrice.Text);
                total += decimal.Parse(this.txtInvCarPrice.Text);
            }


            decimal? InvTaskPrice = null;
            if (txtInvTaskPrice.Text != "")
            {
                InvTaskPrice = decimal.Parse(this.txtInvTaskPrice.Text);
                total += decimal.Parse(this.txtInvTaskPrice.Text);
            }


            decimal? InvManPrice = null;
            if (txtInvManPrice.Text != "")
            {
                InvManPrice = decimal.Parse(this.txtInvManPrice.Text);
                total += decimal.Parse(this.txtInvManPrice.Text);
            }


            string Remark = this.txtRemark.Text;


            Model.EFrom.Tb_ProjectInvs model = new Model.EFrom.Tb_ProjectInvs();
            model.Total = total;
            model.BuyTime = BuyTime;
            model.InvModel = InvModel;
            model.InvName = InvName;
            model.InvUnit = InvUnit;
            model.InvNum = InvNum;
            model.InvPrice = InvPrice;
            model.InvCarPrice = InvCarPrice;
            model.InvTaskPrice = InvTaskPrice;
            model.InvManPrice = InvManPrice;
            model.Remark = Remark;
            if (ViewState["state"] != null)
            {
                if (ViewState["state"].ToString() == "add")
                {
                    List<Tb_ProjectInvs> POOrders = Session["ProInvs"] as List<Tb_ProjectInvs>;
                    POOrders.Add(model);
                    Session["ProInvs"] = POOrders;
                    setTotal(POOrders);
                    gvList.DataSource = POOrders;
                    gvList.DataBind();

                    this.txtBuyTime.Text = "";
                    this.txtInvModel.Text = "";
                    this.txtInvName.Text = "";
                    this.txtInvUnit.Text = "";
                    this.txtInvNum.Text = "";
                    this.txtInvPrice.Text = "";
                    this.txtInvCarPrice.Text = "";
                    this.txtInvTaskPrice.Text = "";
                    this.txtInvManPrice.Text = "";
                    this.txtRemark.Text = "";
                    ViewState["state"] = "add";
                    return;

                }
                if (ViewState["state"].ToString() == "edit" && ViewState["index"] != null)
                {
                    int index = Convert.ToInt32(ViewState["index"]);
                    if (Session["ProInvs"] != null)
                    {

                        List<Tb_ProjectInvs> POOrders = Session["ProInvs"] as List<Tb_ProjectInvs>;

                        Tb_ProjectInvs modelPro = POOrders[index];
                         
                        model.Id = modelPro.Id;
                        model.IfUpdate = true;
                        POOrders[index] = model;
                        Session["ProInvs"] = POOrders;
                        setTotal(POOrders);
                        gvList.DataSource = POOrders;
                        gvList.DataBind();

                    }
                }
            }
             
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
             
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            ViewState["index"] = index;

            if (Session["ProInvs"] != null)
            {

                List<Tb_ProjectInvs> projectInvsList = Session["ProInvs"] as List<Tb_ProjectInvs>;
                Tb_ProjectInvs model = projectInvsList[index];
                setValue(model);

                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                ViewState["state"] = "edit";
            }
        }

        private void setValue(Tb_ProjectInvs model)
        {

            this.txtBuyTime.Text = model.BuyTime.ToString("yyyy-MM-dd");
            this.txtInvModel.Text = model.InvModel;
            this.txtInvName.Text = model.InvName;
            this.txtInvUnit.Text = model.InvUnit;
            this.txtInvNum.Text = model.InvNum.ToString();
            if (model.InvPrice != null)
                this.txtInvPrice.Text = model.InvPrice.ToString();

            if (model.InvCarPrice != null)
                this.txtInvCarPrice.Text = model.InvCarPrice.ToString();

            if (model.InvTaskPrice != null)
                this.txtInvTaskPrice.Text = model.InvTaskPrice.ToString();

            if (model.InvManPrice != null)
                this.txtInvManPrice.Text = model.InvManPrice.ToString();
            this.txtRemark.Text = model.Remark;
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["ProInvsIds"] == null)
                {
                    ViewState["ProInvsIds"] = this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["ProInvsIds"].ToString();
                    ids += this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["ProInvsIds"] = ids;
                }
            }

            if (Session["ProInvs"] != null)
            {

                List<Tb_ProjectInvs> projectInvsList = Session["ProInvs"] as List<Tb_ProjectInvs>;
                projectInvsList.RemoveAt(e.RowIndex);
                ViewState["ProInvsCount"] = projectInvsList.Count;
                setTotal(projectInvsList);
                gvList.DataSource = projectInvsList;
                gvList.DataBind();
                btnAdd.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        protected void btnReSet_Click(object sender, EventArgs e)
        {
            if (Request["ProId"] == null || Request["allE_id"] == null || Request["EForm_Id"]==null) return;

            tb_EFormService eformSer = new tb_EFormService();
            //查询单子有没有结束
            if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
            {

                Tb_ProjectInvService proSer = new Tb_ProjectInvService();
                if (proSer.RestatePro(Convert.ToInt32(Request["EForm_Id"]), Convert.ToInt32(Request["allE_id"]), Convert.ToInt32(Session["currentUserId"])))
                {
                    base.Response.Redirect("~" + Session["backurl"]);
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启动失败！');</script>");
                }

            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('单据审批尚未结束，请重新载入本单据信息！');</script>");
            }
            
        }


    }
}
