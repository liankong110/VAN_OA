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
using System.IO;


namespace VAN_OA.EFrom
{
    public partial class PO_Order : System.Web.UI.Page
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


            if (txtCaiGou.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购人！');</script>");
                txtCaiGou.Focus();

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
               // txtForm.Focus();

                return false;
            }

            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'",txtCaiGou.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写采购人不存在！');</script>");
                // txtForm.Focus();

                return false;
            }
            #endregion


            return true;
        }



        private void setEnable(bool result)
        {
            //txtName.Enabled = false;
            txtCaiGou.Enabled = false;

            //gvList.Columns[0].Visible = result;
            //gvList.Columns[1].Visible = result;
            //lbtnAddFiles.Visible = result;


            //gvCai.Columns[0].Visible = result;
            //gvCai.Columns[1].Visible = result;

         
            //txtRemark.ReadOnly = !result;
            //txtDepartName.ReadOnly = !result;
            //txtForm.Enabled = result;
            //txtName.ReadOnly = !result;
            //txtRemark.ReadOnly = !result;
            //txtTo.Enabled = result;
            //txtZhiwu.ReadOnly = !result;
            //txtZhiwu.ReadOnly = !result;
            //Panel1.Enabled = result;

        }


        private void SetRole(int Count)
        {
            if (Count == 0)
            {
                //权限2（经理过目）
                //lbtnAddFiles.Visible = false;
                //gvCai.Columns[0].Visible = false;
                //gvCai.Columns[1].Visible = false;
                //lbtnAddFiles.Visible = false;
                gvCai.Visible = false;
                //plCiGou.Visible = false;

                //try
                //{
                //    ddlPers.Text = txtCaiGou.Text;
                //}
                //catch (Exception)
                //{
                    
                   
                //}
            }

            if (Count == 1)
            {
                //权限3（采购）
                 
                gvCai.Columns[0].Visible = true;

                plCiGou.Visible = true;

                txtIdea.Visible = false;
                lblIdea.Visible = false;

                txtFinPrice1.Visible = false;
                txtFinPrice2.Visible = false;
                txtFinPrice3.Visible = false;
            }

            if (Count == 2)
            {
                //权限4（经理再次确认）
                plCiGou.Visible = true;

                gvCai.Columns[0].Visible = true;

                txtPrice2.ReadOnly = true;
                txtPrice3.ReadOnly = true;
                txtSupper2.ReadOnly = true;
                txtSupper3.ReadOnly = true;
                txtSupperPrice.ReadOnly = true;
                txtSupplier.ReadOnly = true;

              
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              
                Session["Orders"] = null;

                Session["Cais"] = null;
                lbtnAddFiles.Visible = false;

                gvList.Columns[0].Visible = false;
                gvList.Columns[1].Visible = false;
                gvCai.Columns[0].Visible = false;

                lblAttName.Visible = false;
                fuAttach.Visible = false;

               // gvCai.Visible = false;
                plCiGou.Visible = false;

                //再次编辑
                btnReSubEdit.Visible = false;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;


                    btnSave.Enabled = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null )//单据增加
                    {

                        fuAttach.Visible = true;
                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;

                        //加载初始数据

                        List<TB_POOrders> orders = new List<TB_POOrders>();
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();


                        List<TB_POCai> orderCais = new List<TB_POCai>();
                        Session["Cais"] = orderCais;
                        ViewState["CaisCount"] = orderCais.Count;
                        gvCai.DataSource = orderCais;
                        gvCai.DataBind();

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
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

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
                                ddlPers.DataSource = roleUserList;
                                //}
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

                    else if (Request["ReEdit"] != null)//再次编辑
                    {

                        
                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;

                        //加载初始数据

                        //List<TB_POOrders> orders = new List<TB_POOrders>();
                        //Session["Orders"] = orders;
                        //ViewState["OrdersCount"] = orders.Count;

                        //gvList.DataSource = orders;
                        //gvList.DataBind();


                        List<TB_POCai> orderCais = new List<TB_POCai>();
                        Session["Cais"] = orderCais;
                        ViewState["CaisCount"] = orderCais.Count;
                        gvCai.DataSource = orderCais;
                        gvCai.DataBind();

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
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

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

                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";
                        #region  加载 请假单数据

                        TB_POOrderService mainSer = new TB_POOrderService();
                        TB_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        lblProNo.Text = pp.ProNo;
                        txtCaiGou.Text = pp.CaiGou;                       
                        TB_POOrdersService ordersSer = new TB_POOrdersService();
                        List<TB_POOrders> orders = ordersSer.GetListArray(" 1=1 and id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                      
                        gvList.DataSource = orders;
                        gvList.DataBind();

                        if (pp.fileName != null && pp.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = pp.fileName;
                            lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                        }
                        else
                        {
                            fuAttach.Visible = true;
                        }
                        //TB_POCaiService CaiSer = new TB_POCaiService();
                        //List<TB_POCai> caiList = CaiSer.GetListArray(" 1=1 and id=" + Request["allE_id"]);
                        //Session["Cais"] = caiList;
                        //ViewState["CaisCount"] = caiList.Count;

                        //gvCai.DataSource = caiList;
                        //gvCai.DataBind();

                        #endregion





                    }
                    else//单据审批
                    {

                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";
                        //string POOrdersIds = "";
                        //if (ViewState["POOrdersIds"] != null)
                        //{
                        //    POOrdersIds = ViewState["POOrdersIds"].ToString();
                        //}
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

                        ViewState["EformsCount"] = eforms.Count;




                        #region  加载 请假单数据

                        TB_POOrderService mainSer = new TB_POOrderService();
                        TB_POOrder pp=mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.LoginName;
                        txtCaiGou.Text = pp.CaiGou;
                        //txtRemark.Text = pp.cRemark;
                        if (pp.ProNo!=null)
                        lblProNo.Text = pp.ProNo;
                        TB_POOrdersService ordersSer = new TB_POOrdersService();
                        List<TB_POOrders> orders = ordersSer.GetListArray(" 1=1 and id=" + Request["allE_id"]);
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        if (pp.fileName != null && pp.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = pp.fileName;

                            if (pp.fileName.LastIndexOf('.') != -1)
                            {
                                lblAttName_Vis.Text = pp.fileName.Substring(0, pp.fileName.LastIndexOf('.')) + "_" + pp.Id + pp.fileName.Substring(pp.fileName.LastIndexOf('.'));
                            }
                            else
                            {
                                lblAttName_Vis.Text = pp.fileName;
                            }
                        }

                        

                        TB_POCaiService CaiSer = new TB_POCaiService();
                        List<TB_POCai> caiList = CaiSer.GetListArray(" 1=1 and id=" + Request["allE_id"]);
                        Session["Cais"] = caiList;
                        ViewState["CaisCount"] = caiList.Count;

                        gvCai.DataSource = caiList;
                        gvCai.DataBind();




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
                            //再次编辑
                            btnReSubEdit.Visible = true;

                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                SetRole(eforms.Count);
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
                                            for (int i = 0; i < ddlPers.Items.Count; i++)
                                            {
                                                if (ddlPers.Items[i].Text == txtCaiGou.Text)
                                                {
                                                    ddlPers.SelectedValue = ddlPers.Items[i].Value;
                                                    break;
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
                                    SetRole(eforms.Count);
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
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";

                                            try
                                            {

                                                for (int i = 0; i < ddlPers.Items.Count; i++)
                                                {
                                                    if (ddlPers.Items[i].Text == txtCaiGou.Text)
                                                    {
                                                        ddlPers.SelectedItem.Value = ddlPers.Items[i].Value;
                                                        break;
                                                    }
                                                }
                                                    
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
                    
                    TB_POOrder order = new TB_POOrder();
                    order.AppName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CaiGou = txtCaiGou.Text;
                    order.cRemark="";
                    List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;
                    List<TB_POCai> caiOrders = Session["Cais"] as List<TB_POCai>;

                    #endregion
                    if (Request["allE_id"] == null || Request["ReEdit"] != null)//单据增加+//再次编辑)
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
                                order.fileName = fileName;
                                fileExtension = System.IO.Path.GetExtension(fileName);
                                string fileType = postedFile.ContentType.ToString();//文件类型
                                order.fileType = fileType;
                                System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                                int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                                byte[] fileData = new Byte[fileLength];//新建一个数组
                                streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                                order.fileNo = fileData;

                                file = System.IO.Path.GetFileNameWithoutExtension(fileName);

                            }
                        }
                         TB_POOrderService POOrderSer = new TB_POOrderService();
                        if (fuAttach.Visible == false && Request["allE_id"]!=null)
                        {
                            TB_POOrder File = POOrderSer.GetModel_File(Convert.ToInt32(Request["allE_id"]));
                            if (File != null)
                            {
                                order.fileName = File.fileName;
                                order.fileNo = File.fileNo;
                                order.fileType = File.fileType;
                            }
                        }
                        int MainId=0;
                       
                        if (POOrderSer.addTran(order, eform, POOrders, caiOrders, out MainId) > 0)
                        {
                            //提交文件
                            if (MainId > 0)
                            {
                                
                                if (order.fileNo != null && fileExtension != "")
                                {
                                    string qizhui = System.Web.HttpContext.Current.Request.MapPath("PO/") + file + "_" + MainId;
                                    postedFile.SaveAs(qizhui + fileExtension);                                    

                                }
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
                        order.Id = Convert.ToInt32(Request["allE_id"]);
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
                        if (fromSer.ifAudiPerAndCon(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]))==false)
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
                        TB_POOrderService POOrderSer = new TB_POOrderService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        string cai_IDS = ViewState["CaisIds"].ToString();
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS, caiOrders, cai_IDS))
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

       

  

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


            if (Session["Orders"] != null)
            {
                List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;                 
                ViewState["POOrdersCount"] = POOrders.Count;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["POOrdersIds"] == null)
                {
                    ViewState["POOrdersIds"] = this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["POOrdersIds"].ToString();
                    ids += this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["POOrdersIds"] = ids;
                }
            }

            if (Session["Orders"] != null)
            {
                List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;

                POOrders.RemoveAt(e.RowIndex);

               
                ViewState["POOrdersCount"] = POOrders.Count;

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}",Convert.ToDecimal(obj));
            return 0;
        }

        TB_POOrders SumOrders = new TB_POOrders();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");


                TB_POOrders model = e.Row.DataItem as TB_POOrders;

                 
                SumOrders.CostTotal += model.CostTotal;
                SumOrders.Num += model.Num;
                SumOrders.OtherCost += model.OtherCost;
                SumOrders.SellTotal += model.SellTotal;
                SumOrders.YiLiTotal += model.YiLiTotal;
                 

            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                string val = string.Format("javascript:window.showModalDialog('PO_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }


             // 合计
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            
           


        

            setValue(e.Row.FindControl("lblGuestName") as Label, "合计");//合计
            setValue(e.Row.FindControl("lblNum") as Label, SumOrders.Num.ToString());//数量

    


            //e.Row.Cells[7].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString();//成本单价
            setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString());//成本单价

           // e.Row.Cells[8].Text = SumOrders.CostTotal.ToString();//成本总额
            setValue(e.Row.FindControl("lblCostTotal") as Label, SumOrders.CostTotal.ToString());//成本总额


           // e.Row.Cells[9].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString();//销售单价
            setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString());//销售单价


            //e.Row.Cells[10].Text = SumOrders.SellTotal.ToString();//销售总额
            setValue(e.Row.FindControl("lblSellTotal") as Label, SumOrders.SellTotal.ToString());//销售总额


            //e.Row.Cells[11].Text = SumOrders.OtherCost.ToString();//管理费
            setValue(e.Row.FindControl("lblOtherCost") as Label, SumOrders.OtherCost.ToString());//管理费


           // e.Row.Cells[12].Text = SumOrders.YiLiTotal.ToString();//管理费
            setValue(e.Row.FindControl("lblYiLiTotal") as Label, SumOrders.YiLiTotal.ToString());//盈利总额

            if (SumOrders.SellTotal != 0)
            {
                SumOrders.Profit = SumOrders.YiLiTotal / SumOrders.SellTotal * 100;
            }
            else if (SumOrders.YiLiTotal != 0)
            {
                SumOrders.Profit = -100;
            }
            else
            {
                SumOrders.Profit = 0;
            }

            //e.Row.Cells[14].Text =ConvertToObj(SumOrders.Profit).ToString();//利润
            setValue(e.Row.FindControl("lblProfit") as Label, ConvertToObj(SumOrders.Profit).ToString());//利润

 
        }

        }


        private void setValue(Label control,string value)
        {
            control.Text = value;
        }

        TB_POCai SumPOCai = new TB_POCai();
        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                TB_POCai model = e.Row.DataItem as TB_POCai;
                if (model.Total1 != null)
                {
                    if (SumPOCai.Total1 == null) SumPOCai.Total1 = 0;
                    SumPOCai.Total1 += model.Total1;
                }
                if (model.Total2 != null)
                {
                    if (SumPOCai.Total2 == null) SumPOCai.Total2 = 0;
                    SumPOCai.Total2 += model.Total2;
                }
                if (model.Total3 != null)
                {
                    if (SumPOCai.Total3 == null) SumPOCai.Total3 = 0;
                    SumPOCai.Total3 += model.Total3;
                }

                if (model.Num != null)
                {
                    if (SumPOCai.Num == null) SumPOCai.Num = 0;
                    SumPOCai.Num += model.Num;
                }
             

            }
            Label lblPrice1 = e.Row.FindControl("lblSupperPrice") as Label;

            Label lblFinPrice1 = e.Row.FindControl("lblFinPrice1") as Label;
           

            if (lblPrice1 != null && lblFinPrice1 != null)
            {
                if (lblPrice1.Text != "" && lblFinPrice1.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice1.Text) != Convert.ToDecimal(lblFinPrice1.Text))
                    { 
                        e.Row.Cells[6].ForeColor=System.Drawing.Color.Red;
                    }
                }
            }


            Label lblPrice2 = e.Row.FindControl("lblSupperPrice1") as Label;

            Label lblFinPrice2 = e.Row.FindControl("FinPrice2") as Label;


            if (lblPrice2 != null && lblFinPrice2 != null)
            {
                if (lblPrice2.Text != "" && lblFinPrice2.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice2.Text) != Convert.ToDecimal(lblFinPrice2.Text))
                    {
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }




            Label lblPrice3 = e.Row.FindControl("lblSupperPrice2") as Label;

            Label lblFinPrice3 = e.Row.FindControl("FinPrice3") as Label;


            if (lblPrice3 != null && lblFinPrice3 != null)
            {
                if (lblPrice3.Text != "" && lblFinPrice3.Text != "")
                {
                    if (Convert.ToDecimal(lblPrice3.Text) != Convert.ToDecimal(lblFinPrice3.Text))
                    {
                        e.Row.Cells[14].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            List<decimal> pricelMax = new List<decimal>();
            if (lblPrice1 != null && lblPrice1.Text!="")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice1.Text));
            }

            if (lblPrice2 != null && lblPrice2.Text != "")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice2.Text));
            }

            if (lblPrice3 != null && lblPrice3.Text != "")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice3.Text));
            }


            if (pricelMax.Count > 0)
            {
                decimal minPrice = pricelMax.Min();
                decimal lirun = 0;
                List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;

                Label lblInvName = e.Row.FindControl("lblInvName") as Label;
                Label lblGuestName = e.Row.FindControl("lblGuestName") as Label;
                TB_POOrders po=null;
                if (POOrders != null&&lblInvName!=null&&lblGuestName!=null)
                {
                    po = POOrders.Find(p => p.GuestName == lblGuestName.Text && p.InvName == lblInvName.Text);
                    if (po != null && po.SellTotal != 0)
                    {
                        lirun = ((po.SellTotal-minPrice * po.Num - po.OtherCost) / po.SellTotal) * 100;
                    }

                    else if(po!=null)
                    {
                        decimal yiLiTotal = po.SellTotal - minPrice * po.Num - po.OtherCost;

                        if (yiLiTotal != 0)
                        {
                            lirun = -100;
                        }
                         


                    }
                } 
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                if (lblCaiLiRun != null )
                {
                    lblCaiLiRun.Text =string.Format("{0:n2}", lirun);
                    if (po.Profit != null && lirun < po.Profit.Value)
                    {
                        lblCaiLiRun.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            ImageButton btnEdit = e.Row.FindControl("lblFinPrice1") as ImageButton;
            if (btnEdit != null)
            {
            //    string val = string.Format("javascript:window.showModalDialog('WFPOCai.aspx?indexcai={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
            //    btnEdit.Attributes.Add("onclick", val);
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[3].Text = SumPOCai.Num==null?"":SumPOCai.Num.ToString();//数量
                //e.Row.Cells[7].Text = SumPOCai.Total1 == null ? "" : SumPOCai.Total1.ToString();//小计1
                //e.Row.Cells[11].Text = SumPOCai.Total2 == null ? "" : SumPOCai.Total2.ToString();//小计2
                //e.Row.Cells[15].Text = SumPOCai.Total3 == null ? "" : SumPOCai.Total3.ToString();//小计3

                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, SumPOCai.Total1 == null ? "" : SumPOCai.Total1.ToString());//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, SumPOCai.Total2 == null ? "" : SumPOCai.Total2.ToString());//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, SumPOCai.Total3 == null ? "" : SumPOCai.Total3.ToString());//小计3


                List<decimal> totalMax = new List<decimal>();
                if (SumPOCai.Total1 != null )
                {
                    totalMax.Add(SumPOCai.Total1.Value);
                }

                if (SumPOCai.Total2 != null)
                {
                    totalMax.Add(SumPOCai.Total2.Value);
                }

                if (SumPOCai.Total3 != null)
                {
                    totalMax.Add(SumPOCai.Total3.Value);
                }
                if (totalMax.Count > 0)
                {
                    decimal minPrice = totalMax.Min();
                    decimal lirun = 0;
                    decimal sellTotal=0;
                    decimal otherCost=0;
                    List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;
                    foreach (var model in POOrders)
                    {
                        sellTotal += model.SellTotal;
                        otherCost += model.OtherCost;
                    }

                    if (sellTotal != 0)
                    {
                        lirun = ((sellTotal - minPrice - otherCost) / sellTotal) * 100;
                    }

                    else 
                    {
                        decimal yiLiTotal = sellTotal - minPrice - otherCost;

                        if (yiLiTotal != 0)
                        {
                            lirun = -100;
                        }
                    }
                     // e.Row.Cells[18].Text =ConvertToObj(lirun).ToString();//数量
                      setValue(e.Row.FindControl("lblCaiLiRun") as Label, ConvertToObj(lirun).ToString());//数量
                }
            }
        }

        protected void gvCai_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            ViewState["index"] = index;
                
            if (Session["Cais"] != null)
            {

                List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;
                TB_POCai model = POOrders[index];
                setValue(model);
                btnSave.Enabled = true;

                //List<TB_POOrders> Orders = Session["Orders"] as List<TB_POOrders>;
                //gvList.DataSource = Orders;
                //gvList.DataBind();
            }
            //if (Session["a"] != null)
            //{ 
            //    Session["a"] 
            //}
            //if (Session["Cais"] != null)
            //{
            //    List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;

               
            //    ViewState["CaisCount"] = POOrders.Count;
            //    gvCai.DataSource = POOrders;
            //    gvCai.DataBind();
            //}
        }

        protected void gvCai_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvCai.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["CaisIds"] == null)
                {
                    ViewState["CaisIds"] = this.gvCai.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["CaisIds"].ToString();
                    ids += this.gvCai.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["CaisIds"] = ids;
                }
            }

            if (Session["Cais"] != null)
            {
                List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["CaisCount"] = POOrders.Count;

                gvCai.DataSource = POOrders;
                gvCai.DataBind();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (Session["Cais"] != null)
            {
                List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;
                gvCai.DataSource = POOrders;
                gvCai.DataBind();
            }

        }

        protected void gvCai_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

           

          
        }

        protected object getDatetime(object time)
        {
            if (time != null)
            {
                return Convert.ToDateTime(time).ToShortDateString();
            }
            return time;
        }
        protected void gvCai_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            
        }

        protected void txtSupperPrice_TextChanged(object sender, EventArgs e)
        {
           
          

            
        }


        public bool check()
        {
            //if (txtCaiTime.Text == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
            //    return false;
            //}

            if (txtSupplier.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购询供应商1！');</script>");
                return false;
            }
            if (txtSupperPrice.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购询价1！');</script>");
                return false;
            }
            //try
            //{
            //    Convert.ToDateTime(txtCaiTime.Text);
            //}
            //catch (Exception)
            //{

            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写时间格式有误！');</script>");
            //    return false;
            //}
            try
            {
                Convert.ToDecimal(txtSupperPrice.Text);

                if (txtPrice2.Text != "")
                {
                    Convert.ToDecimal(txtPrice2.Text);

                }

                if (txtPrice3.Text != "")
                {
                    Convert.ToDecimal(txtPrice3.Text);

                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                return false;
            }
            return true;
        }
        private void setValue(TB_POCai model)
        {


            if (model.SupperPrice != null)
                txtSupperPrice.Text = string.Format("{0:n2}", model.SupperPrice.Value);
            else
            {
                txtSupperPrice.Text = "";
            }

            if (model.Total1 != null)
                txtTotal1.Text = string.Format("{0:n2}", model.Total1);
            else
            {
                txtTotal1.Text = "";
            }




            if (model.SupperPrice1 != null)
                txtPrice2.Text = string.Format("{0:n2}", model.SupperPrice1.Value);
            else
            {
                txtPrice2.Text = "";
            }

            if (model.Total2 != null)
                txtTotal2.Text = string.Format("{0:n2}", model.Total2);
            else
            {
                txtTotal2.Text = "";
            }

            if (model.SupperPrice2 != null)
                txtPrice3.Text = string.Format("{0:n2}", model.SupperPrice2.Value);
            else
            {
                txtPrice3.Text = "";
            }
            if (model.Total3 != null)
                txtTotal3.Text = string.Format("{0:n2}", model.Total3);
            else
            {
                txtTotal3.Text = "";
            }

            txtSupper2.Text = model.Supplier1;
            txtSupper3.Text = model.Supplier2;
            txtInvName.Text = model.InvName;
            txtGuestName.Text = model.GuestName;
            txtNum.Text = model.Num.Value.ToString();

            //if (model.CaiTime != null)
            //    txtCaiTime.Text = model.CaiTime.Value.ToShortDateString();

            txtIdea.Text = model.Idea;
            //txtUpdateUser.Text = Session["LoginName"].ToString();

            txtSupplier.Text = model.Supplier;



            if (ViewState["EformsCount"] != null)
            {
                if (ViewState["EformsCount"].ToString() == "2")
                {
                    if (model.FinPrice1 == null)
                        txtFinPrice2.Text = txtPrice2.Text;
                    else
                    {
                        txtFinPrice1.Text = string.Format("{0:n2}", model.FinPrice1);
                    }

                    if (model.FinPrice2 == null)
                        txtFinPrice3.Text = txtPrice3.Text;
                    else
                    {
                        txtFinPrice2.Text = string.Format("{0:n2}", model.FinPrice2);
                    }

                    if (model.FinPrice1 == null)
                        txtFinPrice1.Text = txtSupperPrice.Text;
                    else
                    {
                        txtFinPrice3.Text = string.Format("{0:n2}", model.FinPrice3);
                    }
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (check() == false)
            {
                return;
            }
            TB_POCai s = new TB_POCai();
            s.CaiTime = null;
            s.Idea = txtIdea.Text;
            s.SupperPrice = Convert.ToDecimal(txtSupperPrice.Text);
            s.Supplier = txtSupplier.Text;
            s.UpdateUser = Session["LoginName"].ToString();
            s.Num = Convert.ToDecimal(txtNum.Text);
            s.Total1 = s.SupperPrice * s.Num.Value;

            s.Num = Convert.ToDecimal(txtNum.Text);
            s.InvName = txtInvName.Text;
            s.GuestName = txtGuestName.Text;


            s.Supplier1 = txtSupper2.Text;
            s.Supplier2 = txtSupper3.Text;

            if (txtPrice2.Text != "")
            {
                s.SupperPrice1 = Convert.ToDecimal(txtPrice2.Text);
                s.Total2 = s.SupperPrice1.Value * s.Num.Value;
            }


            if (txtPrice3.Text != "")
            {
                s.SupperPrice2 = Convert.ToDecimal(txtPrice3.Text);
                s.Total3 = s.SupperPrice2.Value * s.Num.Value;
            }


            if (txtFinPrice1.Text != "")
            {
                s.FinPrice1 = Convert.ToDecimal(txtFinPrice1.Text);
                s.Total1 = s.FinPrice1.Value * s.Num.Value;
            }

            if (txtFinPrice2.Text != "")
            {
                s.FinPrice2 = Convert.ToDecimal(txtFinPrice2.Text);
                s.Total2 = s.FinPrice2.Value * s.Num.Value;
            }

            if (txtFinPrice3.Text != "")
            {
                s.FinPrice3 = Convert.ToDecimal(txtFinPrice3.Text);
                s.Total3 = s.FinPrice3.Value * s.Num.Value;
            }
            //修改
            if (ViewState["index"] != null)
            {
                int index = Convert.ToInt32(ViewState["index"]);
                if (Session["Cais"] != null)
                {
                    s.UpdateUser = Session["LoginName"].ToString();
                    List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;

                    TB_POCai model = POOrders[index];
                    TB_POCai newSche = s;
                    s.Ids = model.Ids;
                    newSche.IfUpdate = true;
                    POOrders[index] = newSche;
                    Session["Cais"] = POOrders;
                    gvCai.DataSource = POOrders;
                    gvCai.DataBind();
                    btnSave.Enabled = false;
                }
                
                
            }
        }

        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("PO/")+lblAttName_Vis.Text;
            down1(lblAttName.Text, url);
        }

        private void down1(string fileName, string url)
        {
            if (System.IO.File.Exists(url) == false)
            {
                try
                {
                    #region 文件恢复
                    int Id = Convert.ToInt32(Request["allE_id"]);
                    TB_POOrderService mainSer = new TB_POOrderService();
                    TB_POOrder model = mainSer.GetModel_File(Id);

                    MemoryStream ms = new MemoryStream(model.fileNo);

                    using (FileStream fs = new FileStream(url, FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(ms.ToArray());
                        bw.Close();
                    } 
                    #endregion
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                    return;
                }
               
            }
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

        protected void btnReSubEdit_Click(object sender, EventArgs e)
        {
            if (Request["ProId"] != null && Request["allE_id"] != null && Request["EForm_Id"] != null)
            {
                string url = "~/EFrom/PO_Order.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" +Request["EForm_Id"]+"&&ReEdit=true";
                Response.Redirect(url);
            }
        }
    }
}
