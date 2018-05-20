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
    public partial class BusContact : System.Web.UI.Page
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

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
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

            if (txtGotime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外出时间！');</script>");
                txtGotime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtDateTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间 格式错误！');</script>");
                    return false;
                }
            }

            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                if (txtBackTime.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写返回时间！');</script>");
                    txtBackTime.Focus();

                    return false;
                }
                else
                {
                    if (CommHelp.VerifesToDateTime(txtBackTime.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('返回时间 格式错误！');</script>");
                        return false;
                    }
                }
            }

            if (txtTel.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写电话号码！');</script>");
                txtTel.Focus();

                return false;
            
            }
            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtDateTime.Text);
                //Convert.ToDateTime(txtBackTime.Text);
                //Convert.ToDateTime(txtGotime.Text);
                if (txtBackTime.Text != "")
                {
                    if (Convert.ToDateTime(txtDateTime.Text + " " + txtGotime.Text) >= Convert.ToDateTime(txtDateTime.Text + " " + txtBackTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 外出时间不能大于等于返回时间！');</script>");
                    }
                }
                else
                {
                    Convert.ToDateTime(txtDateTime.Text + " " + txtGotime.Text);
                }
             

            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtDateTime.Focus();

                return false;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的姓名在用户中不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion


            return true;
        }



        private void setEnable(bool result)
        {
            txtTel.ReadOnly = !result;
            //txtName.ReadOnly = !result;
            txtDepartName.ReadOnly = !result;
            txtDateTime.ReadOnly = !result;
            txtContactUnit.ReadOnly = !result;
            txtContacer.ReadOnly = !result;
            txtBackTime.ReadOnly = !result;
            txtGotime.ReadOnly = !result;
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
                    if (use == null)
                    {
                        use = new VAN_OA.Model.User();
                    }
                    txtName.Text = use.LoginName;
                   
                    txtDepartName.Text = use.LoginIPosition;
                    btnFinSub.Visible = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        txtBackTime.ReadOnly = true;


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
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                                try
                                {
                                    ddlPers.SelectedIndex = roleUserList.FindIndex(t => t.UserName == use.LoginName);
                                }
                                catch (Exception)
                                {


                                }
                            }
                            else
                            {
                                txtBackTime.Enabled = true;
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                                fuAttach.Visible = true;
                            }
                        }
                        else
                        {
                            txtBackTime.Enabled = true;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                            fuAttach.Visible = true;

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

                        tb_BusContactService busSer = new tb_BusContactService();


                        tb_BusContact busModel = busSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                        txtDepartName.Text = busModel.DepartName;
                        txtContacer.Text = busModel.Contacer;
                        txtContactUnit.Text = busModel.ContactUnit;
                        txtDateTime.Text = Convert.ToDateTime(busModel.DateTime).ToShortDateString().ToString();
                        txtName.Text = busModel.Name;
                        txtTel.Text = busModel.Tel;
                        if (busModel.Gotime != null)
                            txtGotime.Text = busModel.Gotime.Value.ToShortTimeString();

                        if (busModel.BackTime != null)
                            txtBackTime.Text = busModel.BackTime.Value.ToShortTimeString();

                        if (busModel.AppDate != null)
                            txtAppDate.Text = busModel.AppDate.Value.ToString("yyyy-MM-dd hh:mm:ss"); ;
                        
                        lblProNo.Text = busModel.ProNo;
                        if (busModel.fileName != null && busModel.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = busModel.fileName;
                            lblAttName_Vis.Text = busModel.fileName.Substring(0, busModel.fileName.LastIndexOf('.')) + "_" + busModel.id + busModel.fileName.Substring(busModel.fileName.LastIndexOf('.'));
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
                                    fuAttach.Visible = true;
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

                                            ddlPers.SelectedIndex = roleUserList.FindIndex(t => t.UserName == busModel.Name);
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
                                    ViewState["ifConsignor"] = true;
                                    if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                    {
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
                                                ddlPers.SelectedIndex = roleUserList.FindIndex(t => t.UserName == busModel.Name);
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



                        txtGotime.ReadOnly=true;
                        //判断该单据是否为自己申请
                        string sql = string.Format("select  appPer from tb_EForm where proId={0} and allE_id={1}", Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        //if (Session["currentUserId"].ToString() == DBHelp.ExeScalar(sql).ToString())
                        //{
                        //   // setEnable(false);
                        //    //txtBackTime.Enabled = true;
                        //    //btnFinSub.Visible = true;

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

                    tb_BusContact busInfo = new tb_BusContact();
                    busInfo.Tel = txtTel.Text;
                    busInfo.Name = txtName.Text;
                    busInfo.DepartName = txtDepartName.Text;
                    busInfo.DateTime = Convert.ToDateTime(txtDateTime.Text);
                    busInfo.ContactUnit = txtContactUnit.Text;
                    busInfo.Contacer = txtContacer.Text;
                    busInfo.Gotime = Convert.ToDateTime(txtDateTime.Text+" "+txtGotime.Text);
                    if(txtBackTime.Text!="")
                    busInfo.BackTime = Convert.ToDateTime(txtDateTime.Text + " " +txtBackTime.Text);

                    busInfo.AppDate = DateTime.Now;
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
                        tb_BusContactService busInfoSer = new tb_BusContactService();
                        if (busInfoSer.addTran(busInfo, eform) > 0)
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
                        busInfo.id = Convert.ToInt32(Request["allE_id"]);
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
                        string fileName="", fileExtension=""; 
                        HttpPostedFile postedFile = null;
                        string file = "";
                        if (ddlPers.Visible == false)//说明为最后一次审核
                        {


                            HttpFileCollection files = HttpContext.Current.Request.Files;
                            //查找是否有文件
                            
                           
                            for (int iFile = 0; iFile < files.Count; iFile++)
                            {


                                ///'检查文件扩展名字
                                postedFile = files[iFile];

                                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                                if (fileName != "")
                                {
                                    busInfo.fileName = fileName;
                                    fileExtension = System.IO.Path.GetExtension(fileName);
                                    string fileType = postedFile.ContentType.ToString();//文件类型
                                    busInfo.fileType = fileType;
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
                        tb_BusContactService busInfoSer = new tb_BusContactService();
                        if (busInfoSer.updateTran(busInfo, eform, forms))
                        {  //提交文件
                            if (busInfo.id > 0)
                            {

                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    string qizhui = System.Web.HttpContext.Current.Request.MapPath("BusContact/") + file + "_" + busInfo.id;
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

        /// <summary>
        /// 提交本次单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinSub_Click(object sender, EventArgs e)
        {
            if (txtDepartName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写部门！');</script>");
                txtDepartName.Focus();

                return ;
            }
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return ;
            }

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtDateTime.Focus();

                return ;
            }

            if (txtGotime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外出时间！');</script>");
                txtGotime.Focus();
                return ;
            }


            if (txtBackTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写返回时间！');</script>");
                txtBackTime.Focus();

                return;
            }

            if (txtTel.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写电话号码！');</script>");
                txtTel.Focus();

                return ;

            }
            //if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
            //    ddlPers.Focus();
            //    return;
            //}

            try
            {
                Convert.ToDateTime(txtDateTime.Text);
                //Convert.ToDateTime(txtBackTime.Text);
                //Convert.ToDateTime(txtGotime.Text);
                if (txtBackTime.Text != "")
                {
                    if (Convert.ToDateTime(txtDateTime.Text + " " + txtGotime.Text) >= Convert.ToDateTime(txtDateTime.Text + " " + txtBackTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 外出时间不能大于等于返回时间！');</script>");
                    }
                }
                else
                {
                    Convert.ToDateTime(txtDateTime.Text + " " + txtGotime.Text);
                }


            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtDateTime.Focus();

                return ;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的姓名在用户中不存在！');</script>");
                txtName.Focus();

                return ;
            }
            string sql = string.Format(" update tb_BusContact set BackTime='{0}' where id={1};", txtDateTime.Text + " " + txtBackTime.Text,
                Convert.ToInt32(Request["allE_id"]));

            sql += string.Format("update tb_EForm set state='通过',toPer=0,toProsId=0 where proId={0} and allE_id={1};",
               Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"] ));
                        
            
            if (DBHelp.ExeCommand(sql))
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

                btnFinSub.Enabled = false;
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

            }

        }

        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("BusContact/") + lblAttName_Vis.Text;
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
    }
}
