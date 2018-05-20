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

namespace VAN_OA.EFrom
{
    public partial class Dispatching : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();

        protected void btnClose_Click(object sender, EventArgs e)
        {

            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }
            else
            {
                base.Response.Redirect("~/EFrom/EFormApp.aspx");
            }

        }

        protected void btnSet_Click(object sender, EventArgs e)
        {


        }



        public bool FormCheck()
        {
            #region 设置自己要判断的信息


            if (ddlUser.SelectedItem == null || ddlUser.SelectedItem.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写被派工人！');</script>");
                ddlUser.Focus();

                return false;
            }

            if (txtDisDate.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写派工日期！');</script>");
                txtDisDate.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtDisDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return false;
                }
            }
            if (txtNiDate.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写拟派工日期！');</script>");
                return false;
            }
            if (!string.IsNullOrEmpty(txtNiDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtNiDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return false;
                }
            }
            if (txtNiHours.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写拟派工时间！');</script>");
                return false;
            }
            if (txtTel.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写联系电话！');</script>");
                return false;
            }
            if (txtContacter.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写联系人！');</script>");
                return false;
            }
            if (!string.IsNullOrEmpty(txtNiHours.Text))
            {
                if (CommHelp.VerifesToNum(txtNiHours.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('拟派工时间 格式错误！');</script>");
                    return false;
                }
            }
            if (txtGueName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称    ！');</script>");
                txtGueName.Focus();

                return false;
            }
            if (txtPONo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目！');</script>");
                txtPONo.Focus();

                return false;
            }
            if (txtAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写具体地址！');</script>");
                txtAddress.Focus();

                return false;
            }
            if (txtQuestion.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写工作描述及故障现象！');</script>");
                txtQuestion.Focus();

                return false;
            }

            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                if (txtGoDate.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外派出去时间！');</script>");
                    txtGoDate.Focus();

                    return false;
                }

                if (CommHelp.VerifesToDateTime(txtGoDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外派出去时间 格式错误！');</script>");
                    return false;
                }

                if (txtBackDate.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外派回来时间！');</script>");
                    txtGoDate.Focus();

                    return false;
                }

                if (CommHelp.VerifesToDateTime(txtBackDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外派回来时间 格式错误！');</script>");
                    return false;
                }
            }


            try
            {
                if (txtGoDate.Text != "")
                {
                    Convert.ToDateTime(txtGoDate.Text.Trim());
                }
                if (txtBackDate.Text != "")
                {
                    Convert.ToDateTime(txtBackDate.Text.Trim());
                }
                if (txtGoDate.Text != "" && txtBackDate.Text != "")
                {
                    if (Convert.ToDateTime(txtGoDate.Text) >= Convert.ToDateTime(txtBackDate.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
                        txtGoDate.Focus();
                        btnSub.Enabled = true;
                        return false;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtGoDate.Focus();

                return false;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写派工人用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion

            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }
            if (ddlPers.Visible == false)
            {
                if (CommHelp.VerifesToNum(txtMyValue.Text) == false || (Convert.ToDecimal(txtMyValue.Text) < 0 || Convert.ToDecimal(txtMyValue.Text) > 1))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('考核评分必须>=0 且 <=1的数值！');</script>");
                    ddlPers.Focus();
                    return false;
                }
                //，最后一步，如界面上 技能考核系数=0，
                //考核评分 <> 0 时，提示“如该员工在试用期中，请考核评分填写0”
                if (Convert.ToDecimal(txtMyXiShu.Text) == 0 && Convert.ToDecimal(txtMyValue.Text) != 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('如该员工在试用期中，请考核评分填写0！');</script>");
                    
                    return false;
                }

            }
            if (ViewState["eformsCount"] != null &&Convert.ToInt32(ViewState["eformsCount"]) == 1)
            {
                if (string.IsNullOrEmpty(txtGoDate.Text) || string.IsNullOrEmpty(txtBackDate.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外派时间不能为空！');</script>");                  
                    return false;

                }
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtNiDate.ReadOnly = !result;
            txtNiHours.ReadOnly = !result;

            txtAddress.ReadOnly = !result;
            txtBackDate.ReadOnly = !result;
            txtContacter.ReadOnly = !result;
            txtDisDate.ReadOnly = true;
            txtGoDate.ReadOnly = !result;
            txtGueName.ReadOnly = !result;
            //txtName.ReadOnly = true;
            ddlUser.Enabled = result;
            txtQuestion.ReadOnly = !result;
            txtQuestionRemark.ReadOnly = !result;
            txtTel.ReadOnly = !result;
            txtSuiTongRen.ReadOnly = !result;
            Image1.Enabled = result;
            lbtnSelectPONo.Visible = false;
            if (result == true)
            {
                btnEdit.Visible = true;
            }

            if (result == true)
            {
                if (txtGoDate.Text == "")
                {
                    txtGoDate.ReadOnly = false;
                    imggoTime.Enabled = true;
                    txtBackDate.ReadOnly = true;
                    imgendTime.Enabled = false;
                }
                else if (txtBackDate.Text == "")
                {
                    txtGoDate.ReadOnly = true;
                    imggoTime.Enabled = false;
                    txtBackDate.ReadOnly = false;
                    imgendTime.Enabled = true;
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              

                btnEdit.Visible = false;
                if (base.Request["ProId"] != null)
                {


                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                    user.Insert(0,new Model.User { LoginName="",Id=-1 });
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";


                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    if (use == null)
                    {
                        use = new VAN_OA.Model.User();
                    }
                    txtName.Text = use.LoginName;

                    txtGoDate.ReadOnly = true;
                    txtBackDate.ReadOnly = true;

                    imggoTime.Enabled = false;
                    imgendTime.Enabled = false;

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {

                        txtDisDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

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

                                try
                                {

                                    for (int i = 0; i < ddlPers.Items.Count; i++)
                                    {
                                        if (ddlPers.Items[i].Text.Trim() == "李琍")
                                        {
                                            ddlPers.SelectedIndex = i;
                                            break;
                                        }
                                    }
                                }
                                catch (Exception)
                                {


                                }
                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false; fuAttach.Visible = true;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;

                            fuAttach.Visible = true;
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
                        tb_DispatchingService disSer = new tb_DispatchingService();

                        tb_Dispatching Dispatrmodel = disSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = Dispatrmodel.Dispatcher;
                        txtDisDate.Text = Dispatrmodel.DisDate.Value.ToShortDateString().ToString();

                        if (Dispatrmodel.NiDate != null)
                        {
                            txtNiDate.Text = Dispatrmodel.NiDate.Value.ToShortDateString().ToString();
                        }
                        if (Dispatrmodel.NiHours != null)
                        {
                            txtNiHours.Text = Dispatrmodel.NiHours.ToString();
                        }
                        try
                        {
                            ddlUser.Text = Dispatrmodel.OutDispater.ToString();
                        }
                        catch (Exception)
                        {


                        }
                        txtAddress.Text = Dispatrmodel.Address;
                        if (Dispatrmodel.BackDate != null)
                        {
                            txtBackDate.Text = Dispatrmodel.BackDate.Value.ToString();
                        }

                        txtContacter.Text = Dispatrmodel.Contacter;
                        if (Dispatrmodel.GoDate != null)
                        {
                            txtGoDate.Text = Dispatrmodel.GoDate.Value.ToString();
                        }
                        lblProNo.Text = Dispatrmodel.ProNo;
                        txtMyXiShu.Text = Dispatrmodel.MyXiShu.ToString();
                        txtGueName.Text = Dispatrmodel.GueName;
                        //txtOutDispater.Text = Dispatrmodel.OutDispater;
                        txtQuestion.Text = Dispatrmodel.Question;
                        txtQuestionRemark.Text = Dispatrmodel.QuestionRemark;
                        txtTel.Text = Dispatrmodel.Tel;
                        txtSuiTongRen.Text = Dispatrmodel.SuiTongRen;
                        txtPONo.Text = Dispatrmodel.MyPoNo;
                        txtMyValue.Text = Dispatrmodel.MyValue.ToString();
                        if (Dispatrmodel.fileName != null && Dispatrmodel.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = Dispatrmodel.fileName;
                            lblAttName_Vis.Text = Dispatrmodel.fileName.Substring(0, Dispatrmodel.fileName.LastIndexOf('.')) + "_" + Dispatrmodel.Id + Dispatrmodel.fileName.Substring(Dispatrmodel.fileName.LastIndexOf('.'));
                        }

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
                                    txtMyValue.Enabled = true;
                                    txtMyValue.Text = "";
                                    fuAttach.Visible = true;
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
                                        try
                                        {
                                            for (int i = 0; i < ddlPers.Items.Count; i++)
                                            {
                                                if (ddlPers.Items[i].Text.Trim() == "李琍")
                                                {
                                                    ddlPers.SelectedIndex = i;
                                                    break;
                                                }
                                            }
                                            ddlPers.Text = Dispatrmodel.OutDispater.ToString();

                                            int eformsCount = eformSer.GetToAduCount(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                                            ViewState["eformsCount"] = eformsCount;
                                            if (eformsCount == 1)
                                            {
                                                
                                                for (int i = 0; i < ddlPers.Items.Count; i++)
                                                {
                                                    if (ddlPers.Items[i].Text.Trim() == txtName.Text)
                                                    {
                                                        ddlPers.SelectedIndex = i;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {


                                        }
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
                                        txtMyValue.Enabled = true;
                                        txtMyValue.Text = "";
                                        lblPer.Visible = false;
                                        ddlPers.Visible = false;
                                        fuAttach.Visible = true;
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

                                            try
                                            {
                                                for (int i = 0; i < ddlPers.Items.Count; i++)
                                                {
                                                    if (ddlPers.Items[i].Text.Trim() == "李琍")
                                                    {
                                                        ddlPers.SelectedIndex = i;
                                                        break;
                                                    }
                                                }
                                                ddlPers.Text = Dispatrmodel.OutDispater.ToString();

                                                int eformsCount = eformSer.GetToAduCount(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                                                ViewState["eformsCount"] = eformsCount;
                                                if (eformsCount == 1)
                                                {
                                                    for (int i = 0; i < ddlPers.Items.Count; i++)
                                                    {
                                                        if (ddlPers.Items[i].Text.Trim() == txtName.Text)
                                                        {
                                                            ddlPers.SelectedIndex = i;
                                                            break;
                                                        }
                                                    }

                                                }
                                            }
                                            catch (Exception)
                                            {


                                            }


                                        }

                                    }
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                        //是否是修改 如果修改
                        if (Request["Type"] != null && Request["Type"] == "Edit")
                        {
                            txtNiHours.Enabled = true;
                            txtTel.Enabled = true;
                            txtContacter.Enabled = true;
                            txtBackDate.Enabled = true;
                            imgendTime.Visible = true;
                            imgendTime.Enabled = true;
                            txtGoDate.Enabled = true;
                            imggoTime.Visible = true;
                            txtQuestion.Enabled = true;
                            txtQuestionRemark.Enabled = true;
                            txtMyValue.Enabled = true;

                            txtNiHours.ReadOnly = false;
                            txtTel.ReadOnly = false;
                            txtContacter.ReadOnly = false;
                            txtBackDate.ReadOnly = false;
                          
                            txtGoDate.ReadOnly = false;
                            imggoTime.Enabled = true;
                            txtQuestion.ReadOnly = false;
                            txtQuestionRemark.ReadOnly = false;
                            txtMyValue.ReadOnly = false;

                            btnUpdate.Visible = true;
                        }
                    }
                }
                LoadPoInfo(txtPONo.Text);
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
                    tb_Dispatching disInfo = new tb_Dispatching();
                    disInfo.Address = txtAddress.Text;
                    if (txtBackDate.Text != "")
                    {
                        disInfo.BackDate = Convert.ToDateTime(txtBackDate.Text);
                    }
                    disInfo.Contacter = txtContacter.Text;
                    disInfo.DisDate = Convert.ToDateTime(txtDisDate.Text);
                    disInfo.Dispatcher = txtName.Text;
                    if (txtGoDate.Text != "")
                        disInfo.GoDate = Convert.ToDateTime(txtGoDate.Text);

                    disInfo.GueName = txtGueName.Text;
                    disInfo.OutDispater = Convert.ToInt32(ddlUser.SelectedItem.Value);
                    disInfo.Question = txtQuestion.Text;
                    disInfo.QuestionRemark = txtQuestionRemark.Text;
                    disInfo.Tel = txtTel.Text;
                    disInfo.SuiTongRen = txtSuiTongRen.Text;
                    if (!string.IsNullOrEmpty(txtNiDate.Text))
                    {
                        disInfo.NiDate = Convert.ToDateTime(txtNiDate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtNiHours.Text))
                    {
                        disInfo.NiHours = Convert.ToDecimal(txtNiHours.Text);
                    }
                    disInfo.MyPoNo = txtPONo.Text;
                    if (txtMyValue.Text != "")
                    {
                        disInfo.MyValue = Convert.ToDecimal(txtMyValue.Text);
                    }
                    if (txtMyXiShu.Text != "")
                    {
                        disInfo.MyXiShu = Convert.ToDecimal(txtMyXiShu.Text);
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
                            HttpFileCollection files = HttpContext.Current.Request.Files;
                            //查找是否有文件
                            string fileName, fileExtension;
                            fileExtension = "";
                            HttpPostedFile postedFile = null;
                            string file = "";
                            for (int iFile = 0; iFile < files.Count; iFile++)
                            {


                                ///'检查文件扩展名字
                                postedFile = files[iFile];

                                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                                if (fileName != "")
                                {
                                    disInfo.fileName = fileName;
                                    fileExtension = System.IO.Path.GetExtension(fileName);
                                    string fileType = postedFile.ContentType.ToString();//文件类型
                                    disInfo.fileType = fileType;
                                    System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                                    int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                                    byte[] fileData = new Byte[fileLength];//新建一个数组
                                    streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中                                 

                                    file = System.IO.Path.GetFileNameWithoutExtension(fileName);

                                }
                            }


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

                        //需要修改
                        tb_DispatchingService disInfoSer = new tb_DispatchingService();

                        if (disInfoSer.addTran(disInfo, eform) > 0)
                        {
                            if (ViewState["guestId"] != null)
                            {
                                string sql = string.Format("UPDATE TB_GuestTrack SET GuestAddress='{0}' WHERE ID={1}",txtAddress.Text, ViewState["guestId"]);
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
                    else//审核
                    {




                        #region 本单据的ID
                        disInfo.Id = Convert.ToInt32(Request["allE_id"]);
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
                        string file = "";
                        string fileName = "", fileExtension = "";
                        HttpPostedFile postedFile = null;
                        if (ddlPers.Visible == false)//说明为最后一次审核
                        {
                            //查找是否有文件                           
                            fileExtension = "";

                            HttpFileCollection files = HttpContext.Current.Request.Files;
                            for (int iFile = 0; iFile < files.Count; iFile++)
                            {


                                ///'检查文件扩展名字
                                postedFile = files[iFile];

                                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                                if (fileName != "")
                                {
                                    disInfo.fileName = fileName;
                                    fileExtension = System.IO.Path.GetExtension(fileName);
                                    string fileType = postedFile.ContentType.ToString();//文件类型
                                    disInfo.fileType = fileType;
                                    System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                                    int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                                    byte[] fileData = new Byte[fileLength];//新建一个数组
                                    streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中                                 

                                    file = System.IO.Path.GetFileNameWithoutExtension(fileName);

                                }
                            }

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
                        tb_DispatchingService disInfoSer = new tb_DispatchingService();
                        if (disInfoSer.updateTran(disInfo, eform, forms))
                        {
                            //提交文件
                            if (disInfo.Id > 0)
                            {

                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    string qizhui = System.Web.HttpContext.Current.Request.MapPath("Dispatching/") + file + "_" + disInfo.Id;
                                    postedFile.SaveAs(qizhui + fileExtension);

                                }
                            }

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

        protected void btnEdit_Click(object sender, EventArgs e)
        {


            if (ddlUser.SelectedItem == null || ddlUser.SelectedItem.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写被派工人！');</script>");
                ddlUser.Focus();

                return;
            }

            if (txtDisDate.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写派工日期！');</script>");
                txtDisDate.Focus();

                return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtDisDate.Text.Trim());
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                    txtDisDate.Focus();

                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtNiDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtNiDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtNiHours.Text))
            {
                if (CommHelp.VerifesToNum(txtNiHours.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('拟派工时间 格式错误！');</script>");
                    return;
                }
            }

            if (txtGueName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称    ！');</script>");
                txtGueName.Focus();

                return;
            }

            if (txtAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写具体地址！');</script>");
                txtAddress.Focus();

                return;
            }
            if (txtQuestion.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写工作描述及故障现象！');</script>");
                txtQuestion.Focus();

                return;
            }

            //if (ddlPers.Visible == false)
            //{
            //    if (txtGoDate.Text.Trim() == "")
            //    {
            //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外派出去时间！');</script>");
            //        txtGoDate.Focus();

            //        return false;
            //    }

            //    if (txtBackDate.Text.Trim() == "")
            //    {
            //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外派回来时间！');</script>");
            //        txtGoDate.Focus();

            //        return false;
            //    }
            //}


            try
            {
                if (txtGoDate.Text != "")
                {
                    Convert.ToDateTime(txtGoDate.Text.Trim());
                }
                if (txtBackDate.Text != "")
                {
                    Convert.ToDateTime(txtBackDate.Text.Trim());
                }
                if (txtGoDate.Text != "" && txtBackDate.Text != "")
                {
                    if (Convert.ToDateTime(txtGoDate.Text) >= Convert.ToDateTime(txtBackDate.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
                        txtGoDate.Focus();
                        btnSub.Enabled = true;
                        return;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtGoDate.Focus();
                return;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写派工人用户不存在！');</script>");
                txtName.Focus();

                return;
            }




            #region 获取单据基本信息
            tb_Dispatching disInfo = new tb_Dispatching();
            disInfo.Address = txtAddress.Text;
            if (txtBackDate.Text != "")
            {
                disInfo.BackDate = Convert.ToDateTime(txtBackDate.Text);
            }
            disInfo.Contacter = txtContacter.Text;
            disInfo.DisDate = Convert.ToDateTime(txtDisDate.Text);
            disInfo.Dispatcher = txtName.Text;
            if (txtGoDate.Text != "")
                disInfo.GoDate = Convert.ToDateTime(txtGoDate.Text);

            disInfo.GueName = txtGueName.Text;
            //disInfo.OutDispater = txtOutDispater.Text;
            disInfo.OutDispater = Convert.ToInt32(ddlUser.SelectedItem.Value);
            disInfo.Question = txtQuestion.Text;
            disInfo.QuestionRemark = txtQuestionRemark.Text;
            disInfo.Tel = txtTel.Text;
            disInfo.SuiTongRen = txtSuiTongRen.Text;

            if (!string.IsNullOrEmpty(txtNiDate.Text))
            {
                disInfo.NiDate = Convert.ToDateTime(txtNiDate.Text);
            }
            if (!string.IsNullOrEmpty(txtNiHours.Text))
            {
                disInfo.NiHours = Convert.ToDecimal(txtNiHours.Text);
            }


            #region 本单据的ID
            disInfo.Id = Convert.ToInt32(Request["allE_id"]);
            #endregion

            tb_DispatchingService dispachSer = new tb_DispatchingService();
            dispachSer.Update(disInfo, "");


            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");


            #endregion
        }


        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("Dispatching/") + lblAttName_Vis.Text;
            down1(lblAttName.Text, url);
        }

        private void down1(string fileName, string url)
        {
            string filePath = url;//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 1024 * 500;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
            }


        }

        private void LoadPoInfo(string pono)
        {
            if (!string.IsNullOrEmpty(pono))
            {
                string sql = string.Format("select PONo,POName,POType, GuestType,GuestPro,GuestName from CG_POOrder where IFZhui=0 and PONo='{0}';", pono);
                var dt = DBHelp.getDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var PONo = row["PONo"].ToString();
                    var POName = row["POName"].ToString();
                    var POType = row["POType"].ToString();
                    var GuestName = row["GuestName"].ToString();
                    var POTypeString = "";
                    //1	零售
                    //2 工程
                    //3 系统
                    if (POType == "1")
                    {
                        POTypeString = "零售";
                    }
                    if (POType == "2")
                    {
                        POTypeString = "工程";
                    }
                    if (POType == "3")
                    {
                        POTypeString = "系统";
                    }
                    var GuestType = row["GuestType"].ToString();
                    var GuestPro = VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(row["GuestPro"].ToString());
                    txtPOName.Text = POName;
                    txtPONo.Text = PONo;
                    txtGuestType.Text = GuestType;
                    txtGuestPro.Text = GuestPro;
                    txtPOType.Text = POTypeString;
                    txtGueName.Text = GuestName;
                    VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                    List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and GuestName='{0}'", GuestName));
                    if (guestTrackLists.Count > 0)
                    {
                        if (Request["allE_id"] == null)//单据增加
                        {
                            txtAddress.Text = guestTrackLists[0].GuestAddress;
                            ViewState["guestId"] = guestTrackLists[0].Id;
                        }                      

                       
                    }

                    try
                    {
                        lblBaseSkillValue.Text = DBHelp.ExeScalar(string.Format("select XiShu from TB_BaseSkill where MyPoType='{0}'", POTypeString)).ToString();
                    }
                    catch (Exception)
                    { 
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
                LoadPoInfo(model.PONo);
                Session["Comm_CGPONo"] = null;
            }
        }

        private void Show(string userId)
        {
            string sql = string.Format(@"select h.ShouldPayment
 FROM HR_Payment H right join HR_Person P on H.ID=P.ID 
 where  P.ID='{0}' and H.YearMonth='{1}'", userId, DateTime.Now.AddMonths(-2).Year + "-" + DateTime.Now.AddMonths(-2).Month.ToString("00"));
            var val = DBHelp.ExeScalar(sql);
            if (val != null && !(val is DBNull))
            {
                txtMyXiShu.Text = string.Format("{0:n2}",Convert
                    .ToDecimal(val)/3000);
            }
            else
            {
                txtMyXiShu.Text = "0";
            }
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show(ddlUser.Text);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlUser.SelectedItem == null || ddlUser.SelectedItem.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写被派工人！');</script>");
                ddlUser.Focus();

                return;
            }

            if (txtDisDate.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写派工日期！');</script>");
                txtDisDate.Focus();

                return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtDisDate.Text.Trim());
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                    txtDisDate.Focus();

                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtNiDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtNiDate.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtNiHours.Text))
            {
                if (CommHelp.VerifesToNum(txtNiHours.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('拟派工时间 格式错误！');</script>");
                    return;
                }
            }

            if (txtGueName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称    ！');</script>");
                txtGueName.Focus();

                return;
            }

            if (txtAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写具体地址！');</script>");
                txtAddress.Focus();

                return;
            }
            if (txtQuestion.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写工作描述及故障现象！');</script>");
                txtQuestion.Focus();

                return;
            }

         


            try
            {
                if (txtGoDate.Text != "")
                {
                    Convert.ToDateTime(txtGoDate.Text.Trim());
                }
                if (txtBackDate.Text != "")
                {
                    Convert.ToDateTime(txtBackDate.Text.Trim());
                }
                if (txtGoDate.Text != "" && txtBackDate.Text != "")
                {
                    if (Convert.ToDateTime(txtGoDate.Text) >= Convert.ToDateTime(txtBackDate.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
                        txtGoDate.Focus();
                        btnSub.Enabled = true;
                        return;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtGoDate.Focus();
                return;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写派工人用户不存在！');</script>");
                txtName.Focus();

                return;
            }

        
            tb_Dispatching disInfo = new tb_Dispatching();
            disInfo.Address = txtAddress.Text;
            if (txtBackDate.Text != "")
            {
                disInfo.BackDate = Convert.ToDateTime(txtBackDate.Text);
            }
            disInfo.Contacter = txtContacter.Text;
            disInfo.DisDate = Convert.ToDateTime(txtDisDate.Text);
            disInfo.Dispatcher = txtName.Text;
            if (txtGoDate.Text != "")
                disInfo.GoDate = Convert.ToDateTime(txtGoDate.Text);

            disInfo.GueName = txtGueName.Text;
            //disInfo.OutDispater = txtOutDispater.Text;
            disInfo.OutDispater = Convert.ToInt32(ddlUser.SelectedItem.Value);
            disInfo.Question = txtQuestion.Text;
            disInfo.QuestionRemark = txtQuestionRemark.Text;
            disInfo.Tel = txtTel.Text;
            disInfo.SuiTongRen = txtSuiTongRen.Text;

            if (!string.IsNullOrEmpty(txtNiDate.Text))
            {
                disInfo.NiDate = Convert.ToDateTime(txtNiDate.Text);
            }
            if (!string.IsNullOrEmpty(txtNiHours.Text))
            {
                disInfo.NiHours = Convert.ToDecimal(txtNiHours.Text);
            }
            if (!string.IsNullOrEmpty(txtMyValue.Text))
            {
                disInfo.MyValue = Convert.ToDecimal(txtMyValue.Text);
            }
               
            #region 本单据的ID
            disInfo.Id = Convert.ToInt32(Request["allE_id"]);
            #endregion

            tb_DispatchingService dispachSer = new tb_DispatchingService();
            dispachSer.Update(disInfo, "通过");

            Response.Write("<script>alert('保存成功');window.close();window.opener=null;</script>");
        }
    }
}
