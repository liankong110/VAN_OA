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
using System.Collections.Generic;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using Newtonsoft.Json;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class WFSupplierInfo : System.Web.UI.Page
    {
        private TB_SupplierInfoService supplierSer = new TB_SupplierInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string SupplierName = this.txtSupplierName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string SupplierId = this.txtSupplierId.Text;
                    string SupplierAddress = this.txtSupplierAddress.Text;
                    string SupplierHttp = this.txtSupplierHttp.Text;
                    string SupplierShui = this.txtSupplierShui.Text.Trim();
                    string SupplierGong = this.txtSupplierGong.Text.Trim();
                    string SupplierBrandNo = this.txtSupplierBrandNo.Text.Trim();
                    string SupplierBrandName = this.txtSupplierBrandName.Text.Trim();                   
                    string Remark = this.txtRemark.Text;
                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    DateTime CreateTime = DateTime.Now;
                    VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
                    model.Time = Time;
                    model.SupplierName = SupplierName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;                  
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;
                    model.SupplierId = SupplierId;
                    model.SupplierAddress = SupplierAddress;
                    model.SupplierHttp = SupplierHttp;
                    model.SupplierShui = SupplierShui;
                    model.SupplierGong = SupplierGong;
                    model.SupplierBrandNo = SupplierBrandNo;
                    model.SupplierBrandName = SupplierBrandName;                   
                    model.Remark = Remark;
                    model.ZhuJi = txtZhuJi.Text;
                    model.IsUse = cbIsUse.Checked;

                    model.IsSpecial = cbIsSpecial.Checked;
                    if (this.supplierSer.Add(model) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");                        
                        this.txtTime.Text ="";
                        this.txtSupplierName.Text = "";
                        this.txtPhone.Text = "";
                        this.txtLikeMan.Text = "";
                        this.txtJob.Text = "";
                        this.txtFoxOrEmail.Text = "";
                        this.chkIfSave.Checked = true;
                        this.txtQQMsn.Text = "";
                        this.txtSupplierId.Text = "";
                        this.txtSupplierAddress.Text = "";
                        this.txtSupplierHttp.Text  ="";
                        this.txtSupplierShui.Text = "";
                        this.txtSupplierGong.Text = "";
                        this.txtSupplierBrandNo.Text = "";
                        this.txtSupplierBrandName.Text = "";
                        txtRemark.Text = "";                       
 
                        this.txtTime.Focus();
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
        }




        private void setEnable(bool result)
        {
            txtTime.ReadOnly = !result;
            txtSupplierName.ReadOnly = !result;
            txtPhone.ReadOnly = !result;
            txtLikeMan.ReadOnly = !result;
            txtFoxOrEmail.ReadOnly = !result;
            chkIfSave.Enabled = result;            
            txtSupplierAddress.ReadOnly = !result;
            txtSupplierHttp.ReadOnly = !result;
            txtSupplierShui.ReadOnly = !result;
            txtSupplierGong.ReadOnly = !result;
            txtSupplierBrandNo.ReadOnly = !result;
            txtSupplierBrandName.ReadOnly = !result;           
            txtRemark.ReadOnly = !result;             
             Image1.Enabled = result;
             txtQQMsn.ReadOnly = !result;
             txtSupplierSimpleName.ReadOnly = !result;
             txtMainRange.ReadOnly = !result;
             txtZhuJi.ReadOnly = !result;
             cbIsUse.Enabled = result;
             cbIsSpecial.Enabled = result;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {


                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string SupplierName = this.txtSupplierName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;
                   
                    string SupplierId = this.txtSupplierId.Text;
                    string SupplierAddress = this.txtSupplierAddress.Text;
                    string SupplierHttp = this.txtSupplierHttp.Text;
                    string SupplierShui = this.txtSupplierShui.Text.Trim();
                    string SupplierGong = this.txtSupplierGong.Text.Trim();
                    string SupplierBrandNo = this.txtSupplierBrandNo.Text.Trim();
                    string SupplierBrandName = this.txtSupplierBrandName.Text.Trim();

                    string Remark = this.txtRemark.Text;



                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    DateTime CreateTime = DateTime.Now;

                    VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
                    model.Time = Time;
                    model.SupplierName = SupplierName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;
                    
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;

                    model.SupplierId = SupplierId;
                    model.SupplierAddress = SupplierAddress;
                    model.SupplierHttp = SupplierHttp;
                    model.SupplierShui = SupplierShui;
                    model.SupplierGong = SupplierGong;
                    model.SupplierBrandNo = SupplierBrandNo;
                    model.SupplierBrandName = SupplierBrandName;                    
                    model.Remark = Remark;
                    model.MainRange = txtMainRange.Text;    
                    model.SupplieSimpeName = txtSupplierSimpleName.Text.Trim();
                    model.ZhuJi = txtZhuJi.Text;
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    model.IsUse = cbIsUse.Checked;
                    model.IsSpecial = cbIsSpecial.Checked;
                    if (this.supplierSer.Update(model))
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
         

            if (this.txtTime.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期不能为空！');</script>");
                this.txtTime.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtTime.Text);
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期格式错误！');</script>");
                this.txtTime.Focus();
                return false;
            }
           
             

             
            if (this.txtSupplierName.Text.Trim().Length == 0)
            {
                
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商名称不能为空！');</script>");
                this.txtSupplierName.Focus();
                return false;
            }

            if (this.txtSupplierSimpleName.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商简称不能为空！');</script>");
                this.txtSupplierSimpleName.Focus();
                return false;
            }


            if (this.txtPhone.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('电话/手机不能为空！');</script>");
                this.txtPhone.Focus();
                return false;
            }

            if (this.txtSupplierShui.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商税务登记号不能为空！');</script>");
                this.txtSupplierShui.Focus();
                return false;
            }

            if (this.txtSupplierGong.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商工商注册号不能为空！');</script>");
                this.txtSupplierGong.Focus();
                return false;
            }

            if (this.txtSupplierBrandNo.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('银行账号不能为空！');</script>");
                this.txtSupplierBrandNo.Focus();
                return false;
            }

            if (this.txtSupplierBrandName.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开户行不能为空！');</script>");
                this.txtSupplierBrandName.Focus();
                return false;
            } 
            if (this.txtLikeMan.Text.Trim().Length == 0)
            {
               
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('联系人不能为空！');</script>");
                this.txtLikeMan.Focus();
                return false;
            }
            if (this.txtJob.Text.Trim().Length == 0)
            {
                
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('职务不能为空！');</script>");
                this.txtJob.Focus();
                return false;
            }

            if (this.txtZhuJi.Text.Trim().Length == 0)
            {                
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('助记词不能为空！');</script>");
                this.txtZhuJi.Focus();
                return false;
            }
            if (ddlResult.Text=="通过"&&(this.ddlProvince.Text.Trim().Length == 0|| this.ddlCity.Text.Trim().Length == 0))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('省份城市不能为空！');</script>");
                this.ddlProvince.Focus();
                return false;
            }
            if (Request["allE_id"] == null || Request["ReAudit"] != null)//单据增加
            {
                string noSql = "";
                if (Request["ReAudit"] != null)
                {
                    noSql = " and Id<>" + Request["allE_id"];
                }
                //查看供应商信息是否存在
                string sql = string.Format(@"select COUNT(*) from TB_SupplierInfo
where  Status<>'不通过' and SupplieSimpeName='{0}' {1}", txtSupplierSimpleName.Text.Trim(), noSql);
                object obj=DBHelp.ExeScalar(sql);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('供应商简称[{0}]有雷同，请修改数据并保存！');</script>", txtSupplierSimpleName.Text.Trim()));
                    return false;
                }
                sql = string.Format(@"select COUNT(*) from TB_SupplierInfo
where  Status<>'不通过' and SupplierName='{0}' {1}", txtSupplierName.Text.Trim(), noSql);
                obj = DBHelp.ExeScalar(sql);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('供应商[{0}]有雷同，请修改数据并保存！');</script>", txtSupplierName.Text));
                    return false;
                }
                sql = string.Format(@"select COUNT(*) from TB_SupplierInfo
where Status<>'不通过' and ZhuJi='{0}' {1}", txtZhuJi.Text, noSql);
                obj = DBHelp.ExeScalar(sql);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert(' 助记词[{0}]有雷同，请修改数据并保存！');</script>", txtZhuJi.Text));
                    return false;
                }
            }
            return true;
        }
        private void ShowInfo(int Id)
        {

            VAN_OA.Model.ReportForms.TB_SupplierInfo model = supplierSer.GetModel(Id);
            
            this.txtTime.Text = model.Time.ToString();
            this.txtSupplierName.Text = model.SupplierName;
            this.txtPhone.Text = model.Phone;
            this.txtLikeMan.Text = model.LikeMan;
            this.txtJob.Text = model.Job;
            this.txtFoxOrEmail.Text = model.FoxOrEmail;
            this.chkIfSave.Checked = model.IfSave;
            this.txtQQMsn.Text = model.QQMsn;

            this.txtSupplierId.Text = model.SupplierId;
            this.txtSupplierAddress.Text = model.SupplierAddress;
            this.txtSupplierHttp.Text = model.SupplierHttp;
            this.txtSupplierShui.Text = model.SupplierShui;
            this.txtSupplierGong.Text = model.SupplierGong;
            this.txtSupplierBrandNo.Text = model.SupplierBrandNo;
            this.txtSupplierBrandName.Text = model.SupplierBrandName;
            txtSupplierSimpleName.Text = model.SupplieSimpeName;            
            txtRemark.Text = model.Remark;             
            lblProNo.Text = model.ProNo;
            lblCreateTime.Text = model.CreateTime.ToShortDateString().ToString();
            lblCreateUser.Text = model.UserName;
            txtMainRange.Text = model.MainRange;
            txtZhuJi.Text = model.ZhuJi;
            cbIsUse.Checked = model.IsUse;
            cbIsSpecial.Checked = model.IsSpecial;
            ddlProvince.Text = model.Province;

            if (!string.IsNullOrEmpty(model.Province))
            {
                Province_CityService proList = new Province_CityService();
                var cityList = proList.CityList(model.Province);
                cityList.Insert(0, "");
                ddlCity.DataSource =cityList;
                ddlCity.DataBind();
                ddlCity.Text = model.City;
            }
           
        }

        private void setBaiFenBi()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Province_CityService proList = new Province_CityService();
                var comList = proList.ProvinceList();
                comList.Insert(0, "");
                ddlProvince.DataSource = comList;
                ddlProvince.DataBind();

                setBaiFenBi();
                if (Request["type"] != null)
                {
                   
                }
                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    lblCreateUser.Text = use.LoginName;
                    lblCreateTime.Text = DateTime.Now.ToShortDateString().ToString();
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtTime.Text = DateTime.Now.ToString();      
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

                    else if (Request["ReAudit"] != null)//再次编辑
                    {
                        txtSupplierName.Enabled = true;
                        txtZhuJi.Enabled = true;
                        txtSupplierSimpleName.Enabled = false;

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


                        txtSupplierName.Enabled = true;
                        txtZhuJi.Enabled = true;
                        txtSupplierSimpleName.Enabled = false;
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

                        }
                    }

                }
 
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Visible = false;
            btnCopy.Visible = false;
            btnAdd.Visible = true;

            txtSupplierId.Text = "";
            lblProNo.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {


                    #region 获取单据基本信息

                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string SupplierName = this.txtSupplierName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string SupplierId = this.txtSupplierId.Text;
                    string SupplierAddress = this.txtSupplierAddress.Text;
                    string SupplierHttp = this.txtSupplierHttp.Text;
                    string SupplierShui = this.txtSupplierShui.Text.Trim();
                    string SupplierGong = this.txtSupplierGong.Text.Trim();
                    string SupplierBrandNo = this.txtSupplierBrandNo.Text.Trim();
                    string SupplierBrandName = this.txtSupplierBrandName.Text.Trim();
                    string Remark = this.txtRemark.Text;
                    int CreateUser = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", lblCreateUser.Text)));
                    DateTime CreateTime = DateTime.Now;
                    VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
                    model.SupplieSimpeName = txtSupplierSimpleName.Text.Trim();
                    model.Time = Time;
                    model.SupplierName = SupplierName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;                   
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;
                    model.SupplierId = SupplierId;
                    model.SupplierAddress = SupplierAddress;
                    model.SupplierHttp = SupplierHttp;
                    model.SupplierShui = SupplierShui;
                    model.SupplierGong = SupplierGong;
                    model.SupplierBrandNo = SupplierBrandNo;
                    model.SupplierBrandName = SupplierBrandName;                  
                    model.Remark = Remark;
                    model.MainRange = txtMainRange.Text;
                    model.ZhuJi = txtZhuJi.Text;
                    model.IsUse = cbIsUse.Checked;
                    model.IsSpecial = cbIsSpecial.Checked;
                    model.City = ddlCity.Text;
                    model.Province = ddlProvince.Text;
                    #endregion
                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = CreateUser;// Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
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

                        TB_SupplierInfoService OverTimeSer = new TB_SupplierInfoService();
                        if (OverTimeSer.addTran(model, eform) > 0)
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


                                if (Request["ReAudit"] != null)//再次编辑
                                {
                                    VAN_OA.Model.ReportForms.TB_SupplierInfo oldModel = supplierSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                                    var m = JsonConvert.SerializeObject(oldModel);
                                    var sql=string.Format("insert INTO TB_SupplierInfo_History  ([SupplierInfoJosn],[SupplierId]) VALUES ('{0}',{1})", m, Request["allE_id"]);
                                    DBHelp.ExeCommand(sql);                                    
                                }
                            }
                        }
                        if (ddlResult.Text == "不通过")
                        {
                            string sql = string.Format("select top 1 SupplierInfoJosn from TB_SupplierInfo_History where SupplierId={0} order by id desc", Request["allE_id"]);
                            var value = DBHelp.ExeScalar(sql);
                            if (value != null && !(value is DBNull))
                            {
                                model = JsonConvert.DeserializeObject<TB_SupplierInfo>(value.ToString());
                                eform.state = "通过";
                                eform.E_Remark = "驳回";
                            }                           
                        }
                        TB_SupplierInfoService OverTimeSer = new TB_SupplierInfoService();
                        if (OverTimeSer.updateTran(model, eform, forms))
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

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {          
            Province_CityService proList = new Province_CityService();
            var cityList = proList.CityList(ddlProvince.Text);
            cityList.Insert(0, "");
            ddlCity.DataSource = cityList;
            ddlCity.DataBind();
        }
    }
}
