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
namespace VAN_OA.EFrom
{
    public partial class WFPost : System.Web.UI.Page
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

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtDateTime.Focus();

                return false;
            }

            if (CommHelp.VerifesToDateTime(txtDateTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false; 
            }

            if (Request["allE_id"] == null)//单据增加
            {
                if (txtPONo.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                    txtPONo.Focus();
                    return false;
                }
                if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                            "<script>alert('此项目已经关闭！');</script>");
                    return false;
                }
            }
            if (txtPostAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写邮寄地址！');</script>");
                txtPostAddress.Focus();
                return false;
            }

            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {

                if (txtWuliuName.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写物流名称！');</script>");
                    txtPostCode.Focus();

                    return false;
                }
                if (txtPostCode.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写邮件编码！');</script>");
                    txtPostCode.Focus();

                    return false;
                }
            }

            if (txtToPer.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写收件人！');</script>");
                txtToPer.Focus();

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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的姓名在用户中不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion


            if (plEmail.Visible == true&& ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                if (txtPostCode.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写物流编号！');</script>");
                    txtPostCode.Focus();

                    return false;
                }

                if (txtPostTotal.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写金额！');</script>");
                    txtPostTotal.Focus();

                    return false;
                }
                if (txtPostTotal.Text != "")
                {
                    try
                    {
                        Convert.ToDecimal(txtPostTotal.Text);
                    }
                    catch (Exception)
                    {
                        
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的金额格式有误！');</script>");
                        txtPostTotal.Focus();

                        return false;
                    }
                }
            }
            return true;
        }



        private void setEnable(bool result)
        {
            //txtName.ReadOnly = true;
            txtDateTime.ReadOnly = true;
            txtPostAddress.ReadOnly = !result;
            txtPostCode.ReadOnly = !result;
            txtremark.ReadOnly = !result;
            txtTel.ReadOnly = !result;
            txtToPer.ReadOnly = !result;
            txtWuliuName.ReadOnly = !result;
            txtFromPer.ReadOnly = !result;
            Image1.Enabled = false;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              
                btnBaoXiao.Visible = false;
                lbtnAddFiles.Visible = false;
                plEmail.Enabled = false;
                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                  
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtDateTime.Text = DateTime.Now.ToString();
                        lbtnAddFiles.Visible = true;
                        //隐藏邮寄信息
                        plEmail.Visible = false;

                        txtPostCode.Enabled = false;

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

                                try
                                {
                                    ddlPers.Text = use.ReportTo.ToString();
                                }
                                catch (Exception)
                                {


                                }
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





                        #region MyRegion
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

                      
                        tb_PostService postSer = new tb_PostService();

                        tb_Post postModel = postSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDateTime.Text = postModel.AppTime.ToShortDateString();
                        txtName.Text = postModel.LoginName;

                        txtPostCode.Text = postModel.PostCode;
                        txtremark.Text = postModel.Remark;
                        txtTel.Text = postModel.Tel;
                        txtToPer.Text = postModel.ToPer;
                        txtWuliuName.Text = postModel.WuliuName;
                        txtPostAddress.Text = postModel.PostAddress;

                        lblProNo.Text = postModel.ProNo;

                        txtFromPer.Text = postModel.FromPer;




                        if (postModel.PostContext!=null)
                        txtPostContext.Text = postModel.PostContext;
                        if (postModel.PostFrom!=null)
                        cbPostFrom.Checked=Convert.ToBoolean( postModel.PostFrom) ;

                        if (postModel.PostFromAddress!=null)
                         txtPostFromAddress.Text=postModel.PostFromAddress;

                        if (cbPostTo.Checked!=null)
                         cbPostTo.Checked=Convert.ToBoolean(postModel.PostTo);

                        if (txtPostToAddress.Text!=null)
                         txtPostToAddress.Text=postModel.PostToAddress ;


                        if (postModel.PostTotal != null)
                            txtPostTotal.Text=postModel.PostTotal.ToString();

                        if (postModel.PostRemark!=null)
                            txtPostRemark.Text= postModel.PostRemark;


                        txtPONo.Text = postModel.PONo;
                        txtPOName.Text = postModel.POName;
                        txtPOGuestName.Text = postModel.POGuestName;
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
                            string sql = "SELECT count(1) from Tb_DispatchList where Post_Id=" + Request["allE_id"] + " and State<>'不通过'";
                            if ((int)DBHelp.ExeScalar(sql) == 0)
                            {
                                btnBaoXiao.Visible = true;
                            }
                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                plEmail.Enabled = true;
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
                                        //List<A_Role_User> newList = new List<A_Role_User>();
                                        //for (int i = 0; i < roleUserList.Count; i++)
                                        //{
                                        //    if (roleUserList[i].UserId == use.ReportTo)
                                        //    {
                                        //        A_Role_User a = roleUserList[i];
                                        //        newList.Add(a);
                                        //        break;
                                        //    }
                                        //}

                                        //if (newList.Count > 0)
                                        //{
                                        //    ddlPers.DataSource = newList;
                                        //}
                                        //else
                                        //{
                                        //    ddlPers.DataSource = roleUserList;
                                        //}

                                        ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";

                                        try
                                        {
                                            ddlPers.Text = use.ReportTo.ToString();
                                        }
                                        catch (Exception)
                                        {


                                        }
                                    }

                                } setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    plEmail.Enabled = true;
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
                                            //List<A_Role_User> newList = new List<A_Role_User>();
                                            //for (int i = 0; i < roleUserList.Count; i++)
                                            //{
                                            //    if (roleUserList[i].UserId == use.ReportTo)
                                            //    {
                                            //        A_Role_User a = roleUserList[i];
                                            //        newList.Add(a);
                                            //        break;
                                            //    }
                                            //}

                                            //if (newList.Count > 0)
                                            //{
                                            //    ddlPers.DataSource = newList;
                                            //}
                                            //else
                                            //{
                                            //    ddlPers.DataSource = roleUserList;
                                            //}
                                            //ddlPers.DataSource = roleUserList;
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";

                                            try
                                            {
                                                ddlPers.Text = use.ReportTo.ToString();
                                            }
                                            catch (Exception)
                                            {


                                            }
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

                        #endregion



                        
                        //判断该单据是否为自己申请
                        //string sql = string.Format("select  appPer from tb_EForm where proId={0} and allE_id={1}", Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        //if (Session["currentUserId"].ToString() == DBHelp.ExeScalar(sql).ToString())
                        //{
                        //    setEnable(false);
                            
                        //}
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
                    tb_Post postModel = new tb_Post();
                    postModel.AppName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    postModel.AppTime =Convert.ToDateTime( txtDateTime.Text);
                    postModel.PostAddress = txtPostAddress.Text;
                    postModel.PostCode = txtPostCode.Text;
                    postModel.Remark = txtremark.Text;
                    postModel.Tel = txtTel.Text;
                    postModel.ToPer = txtToPer.Text;
                    postModel.WuliuName = txtWuliuName.Text;

                    postModel.FromPer = txtFromPer.Text;

                    postModel.PONo=txtPONo.Text;
                    postModel.POName=txtPOName.Text;
                    postModel.POGuestName=txtPOGuestName.Text ;

                    if (plEmail.Visible == true)
                    {

                        postModel.ProNo = lblProNo.Text;
                        postModel.PostContext = txtPostContext.Text;
                        postModel.PostFrom = cbPostFrom.Checked;
                        postModel.PostFromAddress = txtPostFromAddress.Text;
                        postModel.PostTo = cbPostTo.Checked;
                        postModel.PostToAddress = txtPostToAddress.Text;
                        if(txtPostTotal.Text!="")
                        postModel.PostTotal=Convert.ToDecimal(txtPostTotal.Text);
                        postModel.PostRemark = txtPostRemark.Text;

                    }
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
                        tb_PostService PostSer = new tb_PostService();


                        if (PostSer.addTran(postModel, eform) > 0)
                        {

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                            if (ddlPers.Visible == false && ddlResult.Text == "通过")//说明为最后一次审核
                            {
                                Session["ToDispatchList"] = postModel;
                                string url = url = "~/EFrom/DispatchList.aspx?ProId=" + DBHelp.ExeScalar("select pro_Id from A_ProInfo where pro_Type='预期报销单'") + "&Post=true";
                                base.Response.Redirect(url);//跳转到报销单中，将信息带入

                                return;
                            }
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
                        postModel.Id = Convert.ToInt32(Request["allE_id"]);
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
                        tb_PostService PostSer = new tb_PostService();
                        if (PostSer.updateTran(postModel, eform, forms))
                        {
                            // btnSub.Enabled = true;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

                            if (ddlPers.Visible == false && ddlResult.Text=="通过")//说明为最后一次审核
                            {
                                Session["ToDispatchList"] = postModel;
                                string url = url = "~/EFrom/DispatchList.aspx?ProId=" + DBHelp.ExeScalar("select pro_Id from A_ProInfo where pro_Type='预期报销单'")+"&Post=true";
                                base.Response.Redirect(url);//跳转到报销单中，将信息带入
                               
                                return;
                            }
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

        protected void btnBaoXiao_Click(object sender, EventArgs e)
        {
            #region 获取单据基本信息
            tb_Post postModel = new tb_Post();
            postModel.AppName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
            postModel.AppTime = Convert.ToDateTime(txtDateTime.Text);
            postModel.PostAddress = txtPostAddress.Text;
            postModel.PostCode = txtPostCode.Text;
            postModel.Remark = txtremark.Text;
            postModel.Tel = txtTel.Text;
            postModel.ToPer = txtToPer.Text;
            postModel.WuliuName = txtWuliuName.Text;

            postModel.FromPer = txtFromPer.Text;
            postModel.PONo = txtPONo.Text;
            postModel.POName = txtPOName.Text;
            postModel.POGuestName = txtPOGuestName.Text;

            if (plEmail.Visible == true)
            {

                postModel.ProNo = lblProNo.Text;
                postModel.PostContext = txtPostContext.Text;
                postModel.PostFrom = cbPostFrom.Checked;
                postModel.PostFromAddress = txtPostFromAddress.Text;
                postModel.PostTo = cbPostTo.Checked;
                postModel.PostToAddress = txtPostToAddress.Text;
                if (txtPostTotal.Text != "")
                    postModel.PostTotal = Convert.ToDecimal(txtPostTotal.Text);
                postModel.PostRemark = txtPostRemark.Text;

            }
            #endregion

            #region 本单据的ID
            postModel.Id = Convert.ToInt32(Request["allE_id"]);
            #endregion

            Session["ToDispatchList"] = postModel;
            string url = url = "~/EFrom/DispatchList.aspx?ProId=" + DBHelp.ExeScalar("select pro_Id from A_ProInfo where pro_Type='预期报销单'") + "&Post=true";
            base.Response.Redirect(url);//跳转到报销单中，将信息带入


        }
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                var model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtPOGuestName.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
            }
        }
         

    }
}
