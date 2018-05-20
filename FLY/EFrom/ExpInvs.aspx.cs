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
    public partial class ExpInvs : System.Web.UI.Page
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


            

            if (txtCreateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写领用日期！');</script>");
                txtCreateTime.Focus();
                return false;
            }

            if (CommHelp.VerifesToDateTime(txtCreateTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('领用日期 格式错误！');</script>");
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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion


            return true;
        }



        private void setEnable(bool result)
        {          
            //txtName.ReadOnly = true;
            txtDepartMent.ReadOnly = true;
            txtCreateTime.ReadOnly = !result;
            Image1.Enabled = result;

           
            if (result)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = true;
                plProInvs.Visible = true;
                gvList.Columns[0].Visible = true;
                lblReturnTime.Visible = true;
                txtReturnTime.Visible = true;
                imgReturnTime.Visible = true;


                ddlInvs.Enabled = false;
                ddlExpState.Enabled = false;
                txtExpUse.ReadOnly= true;
                txtExpNum.ReadOnly = true;
                //txtExpRemark.Enabled = false;
            }
        }

         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                btnOk.Visible = false;

                VAN_OA.Dal.BaseInfo.Tb_InventoryService invSer = new VAN_OA.Dal.BaseInfo.Tb_InventoryService();
                List<VAN_OA.Model.BaseInfo.Tb_Inventory> InventoryList = invSer.GetListArrayToDdl("");
                ddlInvs.DataSource = InventoryList;
                ddlInvs.DataBind();
                ddlInvs.DataTextField = "InvName";
                ddlInvs.DataValueField = "ID";
                

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
                    txtDepartMent.Text = use.LoginIPosition;


                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {

                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        plProInvs.Visible = true;

                        lblReturnTime.Visible = false;
                        txtReturnTime.Visible = false;
                        imgReturnTime.Visible = false;

                        

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;

                        //加载初始数据

                        List<Tb_ExpInvs> proInvsList = new List<Tb_ExpInvs>();
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
                        Tb_ExpInvService paProInvSer = new Tb_ExpInvService();
                        Tb_ExpInvsService sonProInvSer = new Tb_ExpInvsService();

                        Tb_ExpInv pProModel = paProInvSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pProModel.LoginName;
                        txtCreateTime.Text = pProModel.ExpTime.ToString("yyyy-MM-dd");
                        txtDepartMent.Text = pProModel.DepartMent;                      
                        lblProNo.Text = pProModel.ProNo;

                        if (pProModel.IfOutGoods)
                        {
                            lblOutState.Text = "已发货";
                        }
                        else
                        {
                            lblOutState.Text = "尚未发货";
                        }

                        if (pProModel.OutTime != null)
                        {
                            lblOutTime.Text = pProModel.OutTime.ToString();
                        }
                        lblEventNo.Text = pProModel.EventNo;
                     
                        List<Tb_ExpInvs> proInvsList = sonProInvSer.GetListArray(" 1=1 and PId=" + Request["allE_id"]);
                        
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
                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {

                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {

                                    if (pProModel.IfOutGoods==false)
                                    {
                                        btnOk.Visible = true;
                                    }

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
                                        if (pProModel.IfOutGoods == false)
                                        {
                                            btnOk.Visible = true;
                                        }

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
                            //if (pProModel.CreateUserId.ToString() == base.Session["currentUserId"].ToString())
                            //{
                            //    //gvList.Columns[0].Visible = true;
                            //    //gvList.Columns[1].Visible = true;
                               
                            //    btnEdit.Visible = true;
                            //    plProInvs.Visible = true;
                            //    //setEnable(true);
                            //}
                        }
                    }





                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {
                if (ddlPers.Visible == false&&lblOutState.Text!="已发货")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须先确认发货才能通过！');</script>");
                    return;
                }

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {
                    List<Tb_ExpInvs> projectList_1 = Session["ProInvs"] as List<Tb_ExpInvs>;
                   
                    if (Request["allE_id"] == null)//单据增加
                    {
                        if (projectList_1.Count <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写领料信息！');</script>");
                            btnSub.Enabled = true;
                            return;
                        }

                         Tb_ExpInvsService mainSer = new Tb_ExpInvsService();
                         string ids = "";
                        foreach (var model1 in projectList_1)
                        {
                            ids += model1.InvId + ",";
                           
                            
                        }
                        if (ids.Length > 0)
                        {
                            ids = ids.Substring(0, ids.Length - 1);
                            List<Tb_ExpInvs> sumRep = mainSer.GroupByListArray(string.Format(" 1=1 and Tb_Inventory.Id in({0})",ids));

                            foreach (var model1 in sumRep)
                            {
                                var currentModel= projectList_1.Find(p => p.InvId == model1.InvId);
                                if (model1.ExpNum - currentModel .ExpNum< 0)
                                {
                                    List<Model.ReportForms.Tb_ExpInvsSumRep> detailInv = mainSer.GetListArray_NoReurnInvs(" Tb_Inventory.Id=" + model1.InvId);
                                    string mess = string.Format("货品[{0}]剩余数量不足\\n", currentModel.InvName);
                                    foreach (var detailModel in detailInv)
                                    {
                                        if (detailModel.InvName == "小计" || detailModel.LoginName == null || detailModel.LoginName == "") continue;
                                        mess += string.Format("借出人:{0},数量{1}\\n", detailModel.LoginName, detailModel.ExpNum);
                                    }
                                    mess += model1.InvName + " 你预计借出数量 " + currentModel.ExpNum ;
                                    btnSub.Enabled = true;
                                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + mess + "！');</script>");
                                    return;
                                }
                            }
                        }
                    }
                    if (ddlPers.Visible==false)
                    {
                        
                        int count = projectList_1.FindAll(p => p.ReturnTime == null).Count;
                        if (count > 0)
                        {
                            btnSub.Enabled = true;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('单据存在尚未归还的货品信息,无法提交！');</script>");
                            return;
                        }
                      
                    }


                    #region 获取单据基本信息
                    Tb_ExpInv model = new Tb_ExpInv();
                    model.CreateUserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    model.ExpTime = Convert.ToDateTime(txtCreateTime.Text);                  
                

                    List<Tb_ExpInvs> projectList = Session["ProInvs"] as List<Tb_ExpInvs>;
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
                        Tb_ExpInvService proMainSer = new Tb_ExpInvService();
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
                        Tb_ExpInvService proMainSer = new Tb_ExpInvService();
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

            ddlInvs.SelectedIndex = -1;
            txtExpNum.Text = "0";
            txtExpRemark.Text = "";
            txtExpUse.Text = "";
            ddlInvs.Focus();
            
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


          

            if (txtCreateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写领用日期！');</script>");
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
             
            #endregion


            Tb_ExpInv model = new Tb_ExpInv();            

          
            model.ExpTime = Convert.ToDateTime(txtCreateTime.Text);
             
            List<Tb_ExpInvs> projectList = Session["ProInvs"] as List<Tb_ExpInvs>;
            model.Id = Convert.ToInt32(Request["allE_id"]);

            string IDS = ViewState["ProInvsIds"].ToString();
            Tb_ExpInvService proMainSer = new Tb_ExpInvService();
            if (proMainSer.updateTran(model,projectList, IDS))
            {                
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

            if (ddlInvs.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择货品！');</script>");
                ddlInvs.Focus();
                return;
            }

            
            try
            {
                Convert.ToDecimal(txtExpNum.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量格式错误！');</script>");
                txtExpNum.Focus();
                return;
            }

            if (Convert.ToDecimal(txtExpNum.Text) <= 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写数量！');</script>");
                txtExpNum.Focus();
                return;
            }


            int InvId = int.Parse(ddlInvs.SelectedItem.Value);
            decimal ExpNum = decimal.Parse(this.txtExpNum.Text);
            string ExpUse = this.txtExpUse.Text;
            string ExpState = this.ddlExpState.Text;
            string ExpRemark = this.txtExpRemark.Text;
     

            VAN_OA.Model.EFrom.Tb_ExpInvs model = new VAN_OA.Model.EFrom.Tb_ExpInvs();
          
            model.InvId = InvId;
            model.ExpNum = ExpNum;
            model.ExpUse = ExpUse;
            model.ExpState = ExpState;
            model.ExpRemark = ExpRemark;
            model.InvName = ddlInvs.SelectedItem.Text;
            if (txtResultRemark.Visible == true && txtReturnTime.Text != "")
            {

                try
                {
                    model.ReturnTime = Convert.ToDateTime(txtReturnTime.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('归还时间格式有误！');</script>");
                    txtResultRemark.Focus();
                    return;
                }
            }
            else
            {
                model.ReturnTime = null;
            }

            if (ViewState["state"] != null)
            {
                if (ViewState["state"].ToString() == "add")
                {
                    List<Tb_ExpInvs> POOrders = Session["ProInvs"] as List<Tb_ExpInvs>;
                    POOrders.Add(model);
                    Session["ProInvs"] = POOrders;
                   
                    gvList.DataSource = POOrders;
                    gvList.DataBind();

                    ddlInvs.SelectedIndex = -1;
                    txtExpNum.Text = "0";
                    txtExpRemark.Text = "";
                    txtExpUse.Text = "";
                    ddlInvs.Focus();
                    ViewState["state"] = "add";
                    return;

                }
                if (ViewState["state"].ToString() == "edit" && ViewState["index"] != null)
                {
                    int index = Convert.ToInt32(ViewState["index"]);
                    if (Session["ProInvs"] != null)
                    {

                        List<Tb_ExpInvs> POOrders = Session["ProInvs"] as List<Tb_ExpInvs>;

                        Tb_ExpInvs modelPro = POOrders[index];
                         
                        model.Id = modelPro.Id;
                        model.IfUpdate = true;
                        POOrders[index] = model;
                        Session["ProInvs"] = POOrders;
                     
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
            if (ddlPers.Visible == false && lblOutState.Text != "已发货")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须先确认发货才能修改！');</script>");
                return;
            }

            int index = e.NewEditIndex;
            ViewState["index"] = index;

            if (Session["ProInvs"] != null)
            {

                List<Tb_ExpInvs> projectInvsList = Session["ProInvs"] as List<Tb_ExpInvs>;
                Tb_ExpInvs model = projectInvsList[index];
                setValue(model);

                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                ViewState["state"] = "edit";
            }
        }

        private void setValue(Tb_ExpInvs model)
        {

            ddlInvs.SelectedValue = model.InvId.ToString();
            ddlExpState.SelectedValue = model.ExpState;
            txtExpNum.Text = model.ExpNum.ToString();
            txtExpRemark.Text = model.ExpRemark;
            txtExpUse.Text = model.ExpUse;

            if (model.ReturnTime != null)
            {

                txtReturnTime.Text = model.ReturnTime.Value.ToString("yyyy-MM-dd");
            }

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

                List<Tb_ExpInvs> projectInvsList = Session["ProInvs"] as List<Tb_ExpInvs>;
                projectInvsList.RemoveAt(e.RowIndex);
                ViewState["ProInvsCount"] = projectInvsList.Count;
               
                gvList.DataSource = projectInvsList;
                gvList.DataBind();
                btnAdd.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sql = "update Tb_ExpInv set IfOutGoods=1,OutTime=getdate() where id=" + Request["allE_id"];
            if (DBHelp.ExeCommand(sql))
            {
                lblOutState.Text = "已发货";
                lblOutTime.Text = DateTime.Now.ToString();
                btnOk.Visible = false;
            }

        }


    }
}
