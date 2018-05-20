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
    public partial class LeaveSingle : System.Web.UI.Page
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
            if (txtForm.Text.Trim() == "" || txtTo.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写请假时间！');</script>");
                txtForm.Focus();

                return false;
            }

            if (CommHelp.VerifesToDateTime(txtForm.Text.Trim()) == false|| CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请假时间 格式错误！');</script>");
                return false;
            }

            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }

            if (ddlSecondPers.Visible == true)
            {
                if (ddlSecondPers.SelectedItem == null || ddlSecondPers.SelectedItem.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择部门主管！');</script>");
                    ddlPers.Focus();
                    return false;
                    
                }
            }
            try
            {

                if (Convert.ToDateTime(txtForm.Text) >= Convert.ToDateTime(txtTo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
                    txtForm.Focus();
                    btnSub.Enabled = true;
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
           
            
            return true;
        }



        private void setEnable(bool result)
        {
            txtDepartName.ReadOnly = !result;
            txtForm.ReadOnly = !result;
            //txtName.ReadOnly = !result;
            txtRemark.ReadOnly = !result;
            txtTo.ReadOnly = !result;
            txtZhiwu.ReadOnly = !result;
            txtZhiwu.ReadOnly = !result;
            Panel1.Enabled = result;
            Image1.Enabled = result;
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
                    txtZhiwu.Text = use.Zhiwu;
                    txtDepartName.Text = use.LoginIPosition;

                    ddlSecondPers.Visible = false;
                    lblDepartment.Visible = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        rdoShiJia.Checked = true;
                        
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
                            //List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids, out pro_IDs);
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers_New(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), use.LoginIPosition, out ids);
                       
                            ViewState["ids"] = ids;
                            pro_IDs = ids;
                            if (roleUserList != null)
                            {                               
                                ddlPers.DataSource = roleUserList;                               
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                                if (pro_IDs != 0)
                                {
                                    ddlSecondPers.Visible = true;
                                    lblDepartment.Visible = true;
                                    //获取第二级别审核人
                                     List<A_Role_User> roleUserListSecond= eformSer.getSecondRoleUsers(Convert.ToInt32(Request["ProId"]), pro_IDs);
                                     //从获取出的审核中 获取上级信息

                                     ddlSecondPers.DataSource = roleUserListSecond;
                                     ddlSecondPers.DataBind();
                                     ddlSecondPers.DataTextField = "UserName";
                                     ddlSecondPers.DataValueField = "UserId";

                                     try
                                     {
                                         ddlSecondPers.Text = use.ReportTo.ToString();
                                         ddlSecondPers.Enabled = false;
                                     }
                                     catch (Exception)
                                     {


                                     }
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

                        tb_LeverInfoService leverSer = new tb_LeverInfoService();
                        tb_LeverInfo levermodel = leverSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDepartName.Text = levermodel.depart;
                        txtForm.Text = levermodel.dateForm.ToString();
                        txtName.Text = levermodel.name;
                        txtRemark.Text = levermodel.remark;
                        txtZhiwu.Text = levermodel.zhiwu;
                        txtTo.Text = levermodel.dateTo.ToString();
                        if (levermodel.leverType == "病假")
                        {
                            rdoBing.Checked = true;
                        }
                        if (levermodel.leverType == "调休")
                        {

                            rdoDiaoXiu.Checked = true;
                        }
                        if (levermodel.leverType == "年休假")
                        {
                            rdoNianjia.Checked = true;
                        }
                        if (levermodel.leverType == "事假")
                        {
                            rdoShiJia.Checked = true;
                        }

                        if (levermodel.leverType == "产假")
                        {

                            rdoChanJia.Checked = true;
                        }

                        if (levermodel.leverType == "丧假")
                        {

                            rdoCangjia.Checked = true;
                        }

                        if (levermodel.leverType == "婚假")
                        {

                            rdoHunJia.Checked = true;
                        }
                        
                       
                        lblProNo.Text = levermodel.ProNo;
                        lblZhuGuanId.Text = levermodel.ZhuGuan.ToString();
                        showTimeSpan();
                        if (levermodel.AppDate != null)
                            txtAppDate.Text = levermodel.AppDate.Value.ToString("yyyy-MM-dd hh:mm:ss"); ;
                        
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


                            //判断请假单是否是自动过期
                            if (eforms.Count <= 0)
                            {
                                string sql = string.Format("select count(*) from tb_EForm where state='不通过' and allE_id=" + Request["allE_id"] + " and proId=" + Request["ProId"]);
                                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                                {

                                    string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                                    
                                        string per = "系统自动审批";

                                        mess += string.Format("<tr><td align='center'>第1步</td><td>序号1：系统自动审批</td><td><span style='color:red;'>系统自动检测,你的请假时间在{0}前没有得到审批,系统自动将本单据作废！</span></td></tr>",
                                            txtForm.Text);
                                    
                                    mess += "</table>";
                                    lblMess.Text = mess;
                                }
                            }

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

                                        try
                                        {
                                            ddlPers.Text = use.ReportTo.ToString();
                                        }
                                        catch (Exception)
                                        {


                                        }

                                        string sql = string.Format(" select a_Index from A_ProInfos where pro_Id={0} and ids={1}", Convert.ToInt32(Request["ProId"]), ids);

                                        try
                                        {
                                            // 说明 是代理人审核
                                            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 1&&lblZhuGuanId.Text!=""&&Convert.ToInt32(lblZhuGuanId.Text)!=0)
                                            {
                                                //从获取出的审核中 获取主管信息
                                                List<A_Role_User> newList = new List<A_Role_User>();
                                                for (int i = 0; i < roleUserList.Count; i++)
                                                {
                                                    if (roleUserList[i].UserId == Convert.ToInt32(lblZhuGuanId.Text))
                                                    {
                                                        A_Role_User a = roleUserList[i];
                                                        newList.Add(a);
                                                        break;
                                                    }
                                                }

                                                 //重新绑定部门信息
                                                 ddlPers.DataSource = newList;
                                                 ddlPers.DataBind();
                                                 ddlPers.DataTextField = "UserName";
                                                 ddlPers.DataValueField = "UserId";
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            
                                             
                                        }
                                    }
                                
                                }   setEnable( eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

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
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]),out ids);
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
                    tb_LeverInfo leverInfo = new tb_LeverInfo();
                    leverInfo.dateForm = Convert.ToDateTime(txtForm.Text);
                    leverInfo.dateTo = Convert.ToDateTime(txtTo.Text);
                    leverInfo.depart = txtDepartName.Text;
                    leverInfo.leverType = "事假";
                    leverInfo.name = txtName.Text;
                    leverInfo.remark = txtRemark.Text;
                    leverInfo.zhiwu = txtZhiwu.Text;


                    if (rdoBing.Checked == true)
                    {
                        leverInfo.leverType = "病假";
                    }
                    if (rdoDiaoXiu.Checked == true)
                    {
                        leverInfo.leverType = "调休";
                    }
                    if (rdoNianjia.Checked == true)
                    {
                        leverInfo.leverType = "年休假";

                    }
                    if (rdoShiJia.Checked)
                    {
                        leverInfo.leverType = "事假";
                    }



                    if (rdoCangjia.Checked)
                    {
                        leverInfo.leverType = "丧假";
                    }

                    if (rdoChanJia.Checked)
                    {
                        leverInfo.leverType = "产假";
                    }

                    if (rdoHunJia.Checked)
                    {
                        leverInfo.leverType = "婚假";
                    }


                    if (ddlSecondPers.Visible == true)
                    {
                        leverInfo.ZhuGuan = Convert.ToInt32(ddlSecondPers.SelectedItem.Value);
                    }
                    else
                    {
                        if (lblZhuGuanId.Text.Trim() != "" && Convert.ToInt32(lblZhuGuanId.Text) != 0)
                        {
                            leverInfo.ZhuGuan = Convert.ToInt32(lblZhuGuanId.Text);
                        }
                    }

                    leverInfo.AppDate = DateTime.Now;
                    #endregion
                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId =Convert.ToInt32(DBHelp.ExeScalar( string.Format("select ID from tb_User where loginName='{0}'",txtName.Text)));
                        eform.appPer = userId;
                        eform.appTime = DateTime.Now;
                        eform.createPer =Convert.ToInt32(Session["currentUserId"].ToString());
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
                            eform.toPer =Convert.ToInt32( ddlPers.SelectedItem.Value);
                            eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                        }
                        tb_LeverInfoService leverInfoSer=new tb_LeverInfoService ();
                        if (leverInfoSer.addTran(leverInfo, eform) > 0)
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
                        leverInfo.id = Convert.ToInt32(Request["allE_id"]); 
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
                            forms.audPer =Convert.ToInt32( Session["currentUserId"]);
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
                                eform.toPer =Convert.ToInt32( ddlPers.SelectedItem.Value);
                                eform.toProsId =Convert.ToInt32( ViewState["ids"]);
                            
                            }
                        }
                        tb_LeverInfoService leverSer = new tb_LeverInfoService();
                        if (leverSer.updateTran(leverInfo, eform, forms))
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
                    //首先判断请假的天数
                    TimeSpan ts = Convert.ToDateTime(txtTo.Text) - Convert.ToDateTime(txtForm.Text);


                    DateTime  fromTime=Convert.ToDateTime(txtForm.Text);
                    DateTime  toTime=Convert.ToDateTime(txtTo.Text);

                    double lastfromTime=fromTime.TimeOfDay.Hours+ Convert.ToDouble(fromTime.TimeOfDay.Minutes)/Convert.ToDouble(60);
                    double lasttoTime = toTime.TimeOfDay.Hours + Convert.ToDouble(toTime.TimeOfDay.Minutes) / Convert.ToDouble(60);


                    int days = ts.Days;
                    double hours = 0;
                    if ((ts.Hours+Convert.ToDouble(ts.Minutes)/Convert.ToDouble(60)) >=8.25)
                    {
                        days += 1;
                    }
                    else
                    {
                        //计算小时数
                        if ((lastfromTime>=9&&lastfromTime<=12)&&
                            (lasttoTime>=13.5&&lasttoTime<=17.25))
                        {
                            hours = lasttoTime - lastfromTime - 1.5;
                        }
                        else if ((lastfromTime >= 9 && lastfromTime <= 12) &&
                            (lasttoTime >= 9 && lasttoTime <= 12))
                        {
                            hours = lasttoTime - lastfromTime;
                        }

                        else if ((lastfromTime >= 13.5 && lastfromTime <= 17.25) &&
                            (lasttoTime >= 13.5 && lasttoTime <= 17.25))
                        {
                            hours = lasttoTime - lastfromTime;
                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无效的请假时间！');</script>");
                            return;
                        }

                    }

                   



                   // int fromDays=0;
                   // int toDays=0;

                   // //DateTime from = Convert.ToDateTime(txtForm.Text);
                   // //DateTime to = Convert.ToDateTime(txtTo.Text);
                   // //if (from.TimeOfDay.Hours <= 12)
                   // //{
                   // //    fromDays = from.Hour;
                   // //}
                   // //else if(from.Hour>12&&from.Hour*<=)

                   
                   // double LastDays = ts.Days * 8.15 + ts.Hours;


                   //// lblTiemSpan.Text = ts.Days*6.45 + "天" + ts.Hours  + "小时";
                   //// lblTiemSpan.Text = (ts.Days + ts.Hours / 8.15) + "天" + ts.Hours % 8.15 + "小时";

                   // double ds = 0;
                   // double hs = 0;
                   // if (ts.Hours > 8.15)
                   // {
                   //     ds = 1;
                   //     hs = 0;
                   // }
                   // else
                   // {
                   //     hs = ts.Hours;

                   //     //if (ts.TotalDays <= 12)
                   //     //{ 
                   //     //    hs=
                   //     //}
                   // }

                    lblTiemSpan.Text = days + "天" + hours + "小时" + "(共" + (days * 6.75 + hours) + "小时)";
                }
            }
            catch (Exception)
            {
                  base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");

              
            }
        }
    }
}
