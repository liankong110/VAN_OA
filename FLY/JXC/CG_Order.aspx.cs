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
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;


namespace VAN_OA.JXC
{
    public partial class CG_Order : System.Web.UI.Page
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


            if (txtGuestNo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户ID！');</script>");
                txtGuestNo.Focus();

                return false;
            }

            if (txtPOName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目名称！');</script>");
                txtPOName.Focus();

                return false;
            }

            if (txtPODate.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目日期！');</script>");
                txtPODate.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtPODate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return false;
                }
            }

            if (txtPOTotal.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目金额！');</script>");
                txtPOTotal.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtPOTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return false;
                }
            }
            if (cbIsPoFax.Checked && dllFPstye.SelectedItem.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('含税项目，需要选择发票类型！');</script>");
                return false;
            }
            if (txtPOPayStype.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写结算方式！');</script>");
                txtPOPayStype.Focus();

                return false;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPOPayStype.Text) < 0 || Convert.ToDecimal(txtPOPayStype.Text) > 900)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结算方式只能在0-900取值！');</script>");
                        txtPOPayStype.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结算方式只能在0-900取值！');</script>");
                    txtPOPayStype.Focus();

                    return false;

                }
            }

            if (txtAE.Text != txtName.Text)
            {
                VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                if (guestTrackSer.GuestIsSpecial(string.Format(" and GuestId='{0}'", txtGuestNo.Text)) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('AE必须和申请人一样！');</script>");
                    return false;
                }
                if (Request["allE_id"] == null && (bool)ViewState["DoSpecGuest"] == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你不是所属AE 或 特殊人员无法 对特殊客户操作！');</script>");
                    return false;
                }
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

            //if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'",txtCaiGou.Text)) == null)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写采购人不存在！');</script>");
            //    // txtForm.Focus();

            //    return false;
            //}
            #endregion


            if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return false;
            }
            if (Request["allE_id"] == null)
            {
                if (txtPORemark.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写备注！');</script>");
                    txtPORemark.Focus();
                    return false;
                }

                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加项目明显信息！');</script>");
                    return false;
                }

                if (ddlIfZhui.Text == "1")//追加
                {
                    if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此项目已经关闭！');</script>");
                        return false;
                    }
                }

                Hashtable ht = new Hashtable();
                decimal alltotal = 0;
                decimal profitTotal = 0;
                foreach (var model in POOrders)
                {
                    profitTotal += model.YiLiTotal;
                    alltotal += model.SellTotal;
                    if (!ht.Contains(model.GoodId))
                        ht.Add(model.GoodId, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }

                    if (model.Num <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],数量必须大于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                    if (model.SellPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", model.GoodName, model.GoodSpec, model.Good_Model));
                        return false;
                    }
                }

                if (ddlIfZhui.Text == "0")//追加
                {
                    if (profitTotal < 0)
                    {
                        if (string.IsNullOrEmpty(fuAttach.FileName))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单没有附上，请附订单文件！');</script>");
                            return false;
                        }
                    }
                }

                if (alltotal != Convert.ToDecimal(txtPOTotal.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的销售明显总金额和本次项目总金额不一致！');</script>");
                    return false;

                }

            }
            else
            {
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    List<CG_POCai> orderCais = ViewState["Cais"] as List<CG_POCai>;
                    if (ViewState["RoleCount"] != null && ViewState["RoleCount"].ToString() == "0")
                    {
                        foreach (var m in orderCais)
                        {
                            if (m.Supplier.Trim() == "" && m.Supplier1.Trim() == "" && m.Supplier2.Trim() == "")
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('请为商品[{0}]\规格[{1}]\型号[{2}],至少指定一个供应商！');</script>", m.GoodName, m.GoodSpec, m.Good_Model));

                                return false;
                            }
                        }
                    }


                    System.Collections.Hashtable hsCai = new Hashtable();
                    foreach (var m in orderCais)
                    {
                        if (!hsCai.Contains(m.Supplier))
                        {
                            hsCai.Add(m.Supplier, null);
                        }
                        if (!hsCai.Contains(m.Supplier1))
                        {
                            hsCai.Add(m.Supplier1, null);
                        }
                        if (!hsCai.Contains(m.Supplier2))
                        {
                            hsCai.Add(m.Supplier2, null);
                        }



                    }
                    foreach (var key in hsCai.Keys)
                    {
                        string sql = "";
                        if (key != "")
                        {
                            sql = string.Format("select count(*) from TB_SupplierInfo where SupplieSimpeName='{0}' and Status='通过'  and IsUse=1 ", key);
                            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                            {

                                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                    string.Format(@"<script>alert('供应商信息不存在！');</script>"));
                                return false;
                            }
                        }
                    }
                }
            }


            return true;
        }



        private void setEnable(bool result)
        {
            //txtName.Enabled = false;

            //txtGuestNo.Enabled = false;
            //txtPODate.Enabled = false;
            //txtPOName.Enabled = false;
            //txtPOPayStype.Enabled = false;
            //txtPOTotal.Enabled = false;
            //txtGuestName.ReadOnly = true;
            ////txtGuestName.Enabled = false;
            //txtGuestNo.Enabled = false;
            //txtINSIDE.Enabled = false;
            //txtAE.Enabled = false;
            cbSpecial.Enabled = false;
            txtName.ReadOnly = true;
            txtPORemark.ReadOnly = true;
            txtGuestNo.ReadOnly = true;
            txtPODate.ReadOnly = true;
            txtPOName.ReadOnly = true;
            ddlPOTyle.Enabled = false;
            txtPOPayStype.ReadOnly = true;
            txtPOTotal.ReadOnly = true;
            txtGuestName.ReadOnly = true;
            //txtGuestName.Enabled = false;
            txtGuestNo.ReadOnly = true;
            txtINSIDE.ReadOnly = true;
            txtAE.ReadOnly = true;

            Image1.Enabled = false;
            cbIsPoFax.Enabled = false;
            dllFPstye.Enabled = false;
        }

        private void SetColor(int value)
        {
            if (value == 2)
            {
                ddlPOTyle.BackColor = Color.Red;
            }
            if (value == 3)
            {
                ddlPOTyle.BackColor = Color.Green;
            }
        }

        private void SetRole(int Count)
        {
            ViewState["RoleCount"] = Count;
            //if (Count == 0)
            //{
            //    //权限2（经理过目）
            //    //lbtnAddFiles.Visible = false;
            //    //gvCai.Columns[0].Visible = false;
            //    //gvCai.Columns[1].Visible = false;
            //    //lbtnAddFiles.Visible = false;
            //    gvCai.Visible = false;
            //    //plCiGou.Visible = false;

            //    //try
            //    //{
            //    //    ddlPers.Text = txtCaiGou.Text;
            //    //}
            //    //catch (Exception)
            //    //{


            //    //}
            //}

            if (Count == 0)
            {
                //权限3（采购）

                gvCai.Columns[0].Visible = true;

                plCiGou.Visible = true;

                txtIdea.Visible = false;
                lblIdea.Visible = false;

                //txtFinPrice1.Visible = false;
                //txtFinPrice2.Visible = false;
                //txtFinPrice3.Visible = false;
            }

            //if (Count == 1)
            //{
            //    //权限4（经理再次确认）
            //    plCiGou.Visible = true;

            //    gvCai.Columns[0].Visible = true;

            //    txtPrice2.ReadOnly = true;
            //    txtPrice3.ReadOnly = true;
            //    txtSupper2.ReadOnly = true;
            //    txtSupper3.ReadOnly = true;
            //    txtSupperPrice.ReadOnly = true;
            //    txtSupplier.ReadOnly = true;


            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可操作特殊客户'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目订单') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                {
                    ViewState["DoSpecGuest"] = true;
                }
                else
                {
                    ViewState["DoSpecGuest"] = false;
                }

                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                // basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                List<FpTypeBaseInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList.Insert(0, new FpTypeBaseInfo { Id = -1, FpType = "" });
                dllFPstye.DataSource = gooQGooddList;
                dllFPstye.DataBind();
                dllFPstye.DataTextField = "FpType";
                dllFPstye.DataValueField = "Id";
                try
                {

                    //dllFPstye.SelectedIndex = gooQGooddList.Select(t => t.FpType).ToList().IndexOf("增值税发票"); ;
                }
                catch (Exception)
                {

                }

                txtGuestNo.Attributes.Add("onkeydown", "if(event.keyCode==13){event.keyCode=9;}");

                //AutoCompleteExtender2.ContextKey = Session["currentUserId"].ToString();
                //请假单子              
                ViewState["Orders"] = null;

                ViewState["Cais"] = null;
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


                ddlIfZhui.Enabled = false;
                lblSelect.Visible = false;
                if (Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;


                    btnSave.Enabled = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtPODate.Text = DateTime.Now.ToString();
                        ddlIfZhui.Enabled = true;

                        fuAttach.Visible = true;
                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;

                        //加载初始数据

                        List<CG_POOrders> orders = new List<CG_POOrders>();
                        ViewState["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();


                        List<CG_POCai> orderCais = new List<CG_POCai>();
                        ViewState["Cais"] = orderCais;
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
                    }

                    else if (Request["ReEdit"] != null)//再次编辑
                    {

                        ddlIfZhui.Enabled = true;

                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;
                        gvCai.Visible = false;

                        //加载初始数据
                        List<CG_POCai> orderCais = new List<CG_POCai>();
                        ViewState["Cais"] = orderCais;
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

                        CG_POOrderService mainSer = new CG_POOrderService();
                        CG_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        cbIsPoFax.Checked = pp.IsPoFax;
                        dllFPstye.Enabled = pp.IsPoFax;
                        dllFPstye.SelectedItem.Text = pp.FpType;
                        lblProNo.Text = pp.ProNo;
                        //txtCaiGou.Text = pp.CaiGou;
                        txtAE.Text = pp.AE;
                        txtGuestName.Text = pp.GuestName;
                        txtGuestNo.Text = pp.GuestNo;
                        txtINSIDE.Text = pp.INSIDE;
                        txtPODate.Text = pp.PODate.ToString();
                        txtPOName.Text = pp.POName;
                        if (pp.IFZhui == 0)
                        {
                            txtPONo.Text = "";
                        }
                        else
                        {
                            txtPONo.Text = pp.PONo;
                        }

                        cbSpecial.Checked = pp.IsSpecial;
                        txtPOPayStype.Text = pp.POPayStype;
                        txtPOTotal.Text = pp.POTotal.ToString();
                        ddlIfZhui.SelectedValue = pp.IFZhui.ToString();
                        txtPORemark.Text = pp.PORemark;
                        ddlPOTyle.Text = pp.POType.ToString();
                        SetColor(pp.POType);
                        CG_POOrdersService ordersSer = new CG_POOrdersService();
                        List<CG_POOrders> orders = ordersSer.GetListArray(" 1=1 and CG_POOrders.id=" + Request["allE_id"]);
                        ViewState["Orders"] = orders;
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

                        #endregion

                    }
                    else//单据审批
                    {

                        ViewState["POOrdersIds"] = "";
                        ViewState["CaisIds"] = "";

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

                        CG_POOrderService mainSer = new CG_POOrderService();
                        CG_POOrder pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        cbIsPoFax.Checked = pp.IsPoFax;
                        dllFPstye.Enabled = pp.IsPoFax;
                        dllFPstye.SelectedItem.Text = pp.FpType;

                        txtName.Text = pp.LoginName;
                        //txtCaiGou.Text = pp.CaiGou;
                        cbSpecial.Checked = pp.IsSpecial;
                        txtAE.Text = pp.AE;
                        txtGuestName.Text = pp.GuestName;
                        txtGuestNo.Text = pp.GuestNo;
                        txtINSIDE.Text = pp.INSIDE;
                        txtPODate.Text = pp.PODate.ToString();
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        txtPOPayStype.Text = pp.POPayStype;
                        //txtPOTotal.Text = pp.POTotal.ToString();

                        txtPOTotal.Text = NumHelp.FormatTwo(pp.POTotal);

                        ddlIfZhui.SelectedValue = pp.IFZhui.ToString();
                        txtPORemark.Text = pp.PORemark;
                        ddlPOTyle.Text = pp.POType.ToString();
                        SetColor(pp.POType);
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;
                        CG_POOrdersService ordersSer = new CG_POOrdersService();
                        List<CG_POOrders> orders = ordersSer.GetListArray(" 1=1 and CG_POOrders.id=" + Request["allE_id"]);
                        ViewState["Orders"] = orders;
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



                        CG_POCaiService CaiSer = new CG_POCaiService();
                        List<CG_POCai> caiList = CaiSer.GetListArray(" 1=1 and CG_POCai.id=" + Request["allE_id"]);
                        ViewState["Cais"] = caiList;
                        ViewState["CaisCount"] = caiList.Count;

                       

                        ViewState["Orders"] = orders;

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
                            if (ddlPOTyle.Text != "1")
                            {
                                ddlPOTyle.ForeColor = Color.White;
                            }
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

                                    if (ddlIfZhui.Text == "1" && orders.Sum(T => T.YiLiTotal) < 0)//追加
                                    {
                                        lblWarn.Visible = true;
                                    }
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

                                        //try
                                        //{
                                        //    //for (int i = 0; i < ddlPers.Items.Count; i++)
                                        //    //{
                                        //    //    if (ddlPers.Items[i].Text == txtCaiGou.Text)
                                        //    //    {
                                        //    //        ddlPers.SelectedValue = ddlPers.Items[i].Value;
                                        //    //        break;
                                        //    //    }
                                        //    //}
                                        //}
                                        //catch (Exception)
                                        //{


                                        //}


                                    }

                                }
                                setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                                if (ddlIfZhui.Text == "0")
                                {
                                    ddlPOTyle.Enabled = true;
                                }
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
                                        if (ddlIfZhui.Text == "1" && orders.Sum(T => T.YiLiTotal) < 0)//追加
                                        {
                                            lblWarn.Visible = true;
                                        }
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

                                            //try
                                            //{

                                            //    //for (int i = 0; i < ddlPers.Items.Count; i++)
                                            //    //{
                                            //    //    if (ddlPers.Items[i].Text == txtCaiGou.Text)
                                            //    //    {
                                            //    //        ddlPers.SelectedItem.Value = ddlPers.Items[i].Value;
                                            //    //        break;
                                            //    //    }
                                            //    //}

                                            //}
                                            //catch (Exception)
                                            //{


                                            //}
                                        }

                                    }
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                    if (ddlIfZhui.Text == "0")
                                    {
                                        ddlPOTyle.Enabled = true;
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
                                    if (ddlPOTyle.Text != "1")
                                    {
                                        ddlPOTyle.ForeColor = Color.White;
                                    }
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

                    int guestId = 0;
                    CG_POOrder order = new CG_POOrder();
                    if (Request["allE_id"] == null || Request["ReEdit"] != null)
                    {
                        VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                        List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and GuestId='{0}'", txtGuestNo.Text));
                        if (guestTrackLists.Count > 0)
                        {
                            TB_GuestTrack model = guestTrackLists[0];
                            //|| txtAE.Text != model.AEName || txtINSIDE.Text != model.INSIDEName
                            if (txtGuestName.Text != model.GuestName)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                                return;
                            }
                            var guestType = new GuestTypeBaseInfoService().GetListArray(" GuestType='" + model.MyGuestType + "'");
                            if (guestType.Count == 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户类型不存在检查客户信息！');</script>");
                                return;
                            }

                            order.GuestType = guestType[0].GuestType;
                            order.GuestXiShu = guestType[0].PayXiShu;

                            var guestPro = new GuestProBaseInfoService().GetListArray(" GuestPro=" + model.MyGuestPro + "");
                            if (guestPro.Count == 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户属性不存在检查客户信息！');</script>");
                                return;
                            }
                            order.GuestPro = guestPro[0].GuestPro;
                            order.JiLiXiShu = guestPro[0].JiLiXiShu;

                            guestId = model.Id;

                            order.AEPer = model.AEPer != null ? model.AEPer.Value : 0;
                            order.INSIDEPer = model.INSIDEPer != null ? model.INSIDEPer.Value : 0;
                            order.ZhangQiTotal = model.GuestDays != null ? model.GuestDays.Value : 0;
                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                            return;
                        }

                    }
                    #region 获取单据基本信息


                    order.AppName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    //order.CaiGou = txtCaiGou.Text;

                    order.AE = txtAE.Text;
                    order.GuestName = txtGuestName.Text;
                    order.GuestNo = txtGuestNo.Text;
                    order.INSIDE = txtINSIDE.Text;
                    order.PODate = Convert.ToDateTime(txtPODate.Text);
                    order.POName = txtPOName.Text.Trim();
                    order.PONo = txtPONo.Text;
                    order.POPayStype = txtPOPayStype.Text;
                    order.POTotal = Convert.ToDecimal(txtPOTotal.Text);
                    order.GuestId = guestId;
                    order.cRemark = "";
                    order.IFZhui = Convert.ToInt32(ddlIfZhui.SelectedItem.Value);
                    order.PORemark = txtPORemark.Text;
                    order.IsSpecial = cbSpecial.Checked;
                    order.POType = Convert.ToInt32(ddlPOTyle.Text);

                    List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                    List<CG_POCai> caiOrders = ViewState["Cais"] as List<CG_POCai>;


                    order.IsPoFax = cbIsPoFax.Checked;
                    if (cbIsPoFax.Checked)
                    {
                        order.FpType = dllFPstye.SelectedItem.Text;

                        var strSql = new StringBuilder();
                        strSql.Append("select Tax ");
                        strSql.Append(" FROM FpTypeBaseInfo where id=" + dllFPstye.SelectedItem.Value);

                        order.FpTax = Convert.ToDecimal(DBHelp.ExeScalar(strSql.ToString()));
                    }

                    #endregion
                    if (Request["allE_id"] == null || Request["ReEdit"] != null)//单据增加+//再次编辑)
                    {
                        if (POOrders.Sum(t => t.SellTotal) != Convert.ToDecimal(txtPOTotal.Text))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的销售明显总金额和本次项目总金额不一致！');</script>");
                            return;

                        }


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
                        CG_POOrderService POOrderSer = new CG_POOrderService();
                        if (fuAttach.Visible == false && Request["allE_id"] != null)
                        {
                            //CG_POOrder File = POOrderSer.GetModel_File(Convert.ToInt32(Request["allE_id"]));
                            //if (File != null)
                            //{
                            //    order.fileName = File.fileName;
                            //    //order.fileNo = File.fileNo;
                            //    order.fileType = File.fileType;
                            //}
                        }
                        int MainId = 0;
                        if (ddlIfZhui.Text == "1")
                        {
                            string getIsSelected = string.Format("select IsSelected from CG_POOrder where PONo='{0}' and Status='通过' and IFZhui=0 ", txtPONo.Text);
                            order.IsSelected = Convert.ToBoolean(DBHelp.ExeScalar(getIsSelected));

                        }
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
                        CG_POOrderService POOrderSer = new CG_POOrderService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        string cai_IDS = ViewState["CaisIds"].ToString();
                        order.ProNo = lblProNo.Text;
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS, caiOrders, cai_IDS))
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





        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<CG_POOrders> POOrders = Session["Orders"] as List<CG_POOrders>;
                List<CG_POOrders> POOrders1 = ViewState["Orders"] as List<CG_POOrders>;
                foreach (var m in POOrders)
                {
                    POOrders1.Add(m);
                }
                ViewState["Orders"] = POOrders1;
                Session["Orders"] = null;
                gvList.DataSource = POOrders1;
                gvList.DataBind();
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


            if (Session["m"] != null)
            {
                CG_POOrders m = Session["m"] as CG_POOrders;
                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                POOrders[m.UpdateIndex] = m;
                Session["m"] = null;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }


        //protected string ViewToSession()
        //{
        //    if (IsPostBack)
        //    {
        //        Session["Orders"] = ViewState["Orders"] as List<CG_POOrders>;
        //    }
        //    return "";
        //}
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

            if (ViewState["Orders"] != null)
            {
                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["POOrdersCount"] = POOrders.Count;

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        CG_POOrders SumOrders = new CG_POOrders();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var json = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");


                CG_POOrders model = e.Row.DataItem as CG_POOrders;


                SumOrders.CostTotal += model.CostTotal;
                SumOrders.Num += model.Num;
                SumOrders.OtherCost += model.OtherCost;
                SumOrders.SellTotal += model.SellTotal;
                SumOrders.YiLiTotal += model.YiLiTotal;

                json = JsonConvert.SerializeObject(model);

            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {

                string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex + "&m=" + HttpUtility.UrlEncode(json));
                btnEdit.Attributes.Add("onclick", val);
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {







                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.Num.ToString());//数量




                //e.Row.Cells[7].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString();//成本单价
                //setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString());//成本单价

                // e.Row.Cells[8].Text = SumOrders.CostTotal.ToString();//成本总额
                setValue(e.Row.FindControl("lblCostTotal") as Label, NumHelp.FormatTwo(SumOrders.CostTotal));//成本总额


                // e.Row.Cells[9].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString();//销售单价
                //setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString());//销售单价


                //e.Row.Cells[10].Text = SumOrders.SellTotal.ToString();//销售总额
                setValue(e.Row.FindControl("lblSellTotal") as Label, NumHelp.FormatTwo(SumOrders.SellTotal));//销售总额


                //e.Row.Cells[11].Text = SumOrders.OtherCost.ToString();//管理费
                setValue(e.Row.FindControl("lblOtherCost") as Label, SumOrders.OtherCost.ToString());//管理费


                // e.Row.Cells[12].Text = SumOrders.YiLiTotal.ToString();//管理费
                setValue(e.Row.FindControl("lblYiLiTotal") as Label, NumHelp.FormatTwo(SumOrders.YiLiTotal));//盈利总额

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

                setValue(e.Row.FindControl("lblProfit") as Label, NumHelp.FormatFour(SumOrders.Profit).ToString());//利润


            }

        }


        private void setValue(Label control, string value)
        {
            if (control != null)
                control.Text = value;
        }

        CG_POCai SumPOCai = new CG_POCai();
        decimal IniProfit = 0;
        decimal SumSellTotal = 0;
        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                CG_POCai model = e.Row.DataItem as CG_POCai;
                //获取商品的销售信息
                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                if (POOrders != null)
                {
                    model.SellPrice = POOrders.Find(p => p.GoodId == model.GoodId).SellPrice;
                    (e.Row.FindControl("lblSellPrice") as Label).Text = model.SellPrice.ToString();
                }
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

                //小计1，小计2，小计3 ，我想 还是在每一行的 最便宜的小计的 询价和小计 这两列 帮我 背景变成 淡蓝色 ，这样就能看清楚价格优势
                List<decimal?> totals = new List<decimal?>();
                totals.Add(model.Total1);
                totals.Add(model.Total2);
                totals.Add(model.Total3);
                var minTotal = totals.Min();
              
                if (model.Total1 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice ?? 0;// (minTotal ?? 0);
                    //(e.Row.FindControl("lblSupperPrice") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[9].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total2 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice1 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[12].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[13].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total3 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice2 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal3") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[16].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }

                //利润%放在最右面背景为淡绿色=初步利润/（销售单价*数量）
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                if (lblCaiLiRun != null && model.SellPrice != 0)
                {
                    lblCaiLiRun.Text = string.Format("{0:n2}", (model.IniProfit / (model.SellPrice * model.Num) * 100));
                }
                else
                {
                    lblCaiLiRun.Text = "0";
                }

                IniProfit = IniProfit + (model.IniProfit ?? 0);
                SumSellTotal = SumSellTotal+(model.Num ?? 0) * model.SellPrice;

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(model.IniProfit).ToString());//数量
                //(e.Row.FindControl("lblCaiLiRun") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, NumHelp.FormatFour(SumPOCai.Total1 == null ? 0 : SumPOCai.Total1));//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, NumHelp.FormatFour(SumPOCai.Total2 == null ? 0 : SumPOCai.Total2));//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, NumHelp.FormatFour(SumPOCai.Total3 == null ? 0 : SumPOCai.Total3));//小计3

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(IniProfit).ToString());//数量

                if (SumSellTotal != 0)
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(IniProfit / SumSellTotal * 100).ToString());//数量
                }
                else
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(0).ToString());//数量
                }                
            }
        }

        protected void gvCai_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            ViewState["index"] = index;

            if (ViewState["Cais"] != null)
            {

                List<CG_POCai> POOrders = ViewState["Cais"] as List<CG_POCai>;
                CG_POCai model = POOrders[index];
                setValue(model);
                btnSave.Enabled = true;
            }

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

            if (ViewState["Cais"] != null)
            {
                List<CG_POCai> POOrders = ViewState["Cais"] as List<CG_POCai>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["CaisCount"] = POOrders.Count;

                gvCai.DataSource = POOrders;
                gvCai.DataBind();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (ViewState["Cais"] != null)
            {
                List<CG_POCai> POOrders = ViewState["Cais"] as List<CG_POCai>;
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
        private void setValue(CG_POCai model)
        {

            hfGoodId.Value = model.GoodId.ToString();
            if (model.SupperPrice != null)
                txtSupperPrice.Text = string.Format("{0:f3}", model.SupperPrice.Value);
            else
            {
                txtSupperPrice.Text = "";
            }

            if (model.Total1 != null)
                txtTotal1.Text = string.Format("{0:f4}", model.Total1);
            else
            {
                txtTotal1.Text = "";
            }
            if (model.SupperPrice1 != null)
            {
                txtPrice2.Text = string.Format("{0:f3}", model.SupperPrice1.Value);
            }
            else
            {
                txtPrice2.Text = "";
            }

            if (model.Total2 != null)
                txtTotal2.Text = string.Format("{0:f4}", model.Total2);
            else
            {
                txtTotal2.Text = "";
            }

            if (model.SupperPrice2 != null)
                txtPrice3.Text = string.Format("{0:f3}", model.SupperPrice2.Value);
            else
            {
                txtPrice3.Text = "";
            }
            if (model.Total3 != null)
                txtTotal3.Text = string.Format("{0:f4}", model.Total3);
            else
            {
                txtTotal3.Text = "";
            }

            txtSupper2.Text = model.Supplier1;
            txtSupper3.Text = model.Supplier2;
            //txtInvName.Text = model.InvName;
            //txtGuestName.Text = model.GuestName;
            txtNum.Text = model.Num.Value.ToString();

            lblInvDetail.Text = string.Format(@"{0}\{1}\{2}\{3}\{4} ", model.GoodName, model.GoodTypeSmName, model.GoodSpec, model.Good_Model, model.GoodUnit);

            txtIdea.Text = model.Idea;


            txtSupplier.Text = model.Supplier;

            SetKuCunValueEnabled(cbKuCun1,txtSupplier, txtSupperPrice, txtTotal1);
            SetKuCunValueEnabled(cbKuCun2,txtSupper2, txtPrice2, txtTotal2);
            SetKuCunValueEnabled(cbKuCun3,txtSupper3, txtPrice3, txtTotal3);

            //if (ViewState["EformsCount"] != null)
            //{
            //    if (ViewState["EformsCount"].ToString() == "2")
            //    {
            //        //if (model.FinPrice1 == null)
            //        //    txtFinPrice2.Text = txtPrice2.Text;
            //        //else
            //        //{
            //        //    txtFinPrice1.Text = string.Format("{0:n2}", model.FinPrice1);
            //        //}

            //        //if (model.FinPrice2 == null)
            //        //    txtFinPrice3.Text = txtPrice3.Text;
            //        //else
            //        //{
            //        //    txtFinPrice2.Text = string.Format("{0:n2}", model.FinPrice2);
            //        //}

            //        //if (model.FinPrice1 == null)
            //        //    txtFinPrice1.Text = txtSupperPrice.Text;
            //        //else
            //        //{
            //        //    txtFinPrice3.Text = string.Format("{0:n2}", model.FinPrice3);
            //        //}
            //    }
            //}

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (check() == false)
            {
                return;
            }
            CG_POCai s = new CG_POCai();
            s.CaiTime = null;
            s.Idea = txtIdea.Text;
            s.SupperPrice = Convert.ToDecimal(txtSupperPrice.Text);
            s.Supplier = txtSupplier.Text;
            s.UpdateUser = Session["LoginName"].ToString();
            s.Num = Convert.ToDecimal(txtNum.Text);
            s.Total1 = s.SupperPrice * s.Num.Value;

            s.Num = Convert.ToDecimal(txtNum.Text);
            //s.InvName = txtInvName.Text;
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


            //if (txtFinPrice1.Text != "")
            //{
            //    s.FinPrice1 = Convert.ToDecimal(txtFinPrice1.Text);
            //    s.Total1 = s.FinPrice1.Value * s.Num.Value;
            //}

            //if (txtFinPrice2.Text != "")
            //{
            //    s.FinPrice2 = Convert.ToDecimal(txtFinPrice2.Text);
            //    s.Total2 = s.FinPrice2.Value * s.Num.Value;
            //}

            //if (txtFinPrice3.Text != "")
            //{
            //    s.FinPrice3 = Convert.ToDecimal(txtFinPrice3.Text);
            //    s.Total3 = s.FinPrice3.Value * s.Num.Value;
            //}
            //修改
            if (ViewState["index"] != null)
            {
                int index = Convert.ToInt32(ViewState["index"]);
                if (ViewState["Cais"] != null)
                {
                    s.UpdateUser = Session["LoginName"].ToString();
                    List<CG_POCai> POOrders = ViewState["Cais"] as List<CG_POCai>;

                    CG_POCai model = POOrders[index];
                    CG_POCai newSche = s;
                    s.Ids = model.Ids;
                    s.GoodId = model.GoodId;
                    s.GoodName = model.GoodName;
                    s.GoodNo = model.GoodNo;
                    s.GoodSpec = model.GoodSpec;
                    s.GoodTypeSmName = model.GoodTypeSmName;
                    s.GoodUnit = model.GoodUnit;
                    newSche.IfUpdate = true;
                    POOrders[index] = newSche;
                    ViewState["Cais"] = POOrders;
                    gvCai.DataSource = POOrders;
                    gvCai.DataBind();
                    btnSave.Enabled = false;
                }


            }
        }

        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("PO/") + lblAttName_Vis.Text;
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
                    CG_POOrderService mainSer = new CG_POOrderService();
                    CG_POOrder model = mainSer.GetModel_File(Id);

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
                string url = "~/JXC/CG_Order.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" + Request["EForm_Id"] + "&&ReEdit=true";
                Response.Redirect(url);
            }
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlIfZhui.SelectedItem.Value == "0")
            {
                lblSelect.Visible = false;
                txtAE.Enabled = true;
                txtGuestName.Enabled = true;
                txtGuestNo.Enabled = true;
                txtINSIDE.Enabled = true;
                txtPOName.Enabled = true;
                cbSpecial.Enabled = true;
                cbIsPoFax.Enabled = true;
                dllFPstye.Enabled = true;
                ddlPOTyle.Enabled = true;
            }
            else if (ddlIfZhui.SelectedItem.Value == "1")
            {
                lblSelect.Visible = true;
                txtAE.Enabled = false;
                txtGuestName.Enabled = false;
                txtGuestNo.Enabled = false;
                txtINSIDE.Enabled = false;
                txtPOName.Enabled = false;
                cbSpecial.Enabled = false;
                ddlPOTyle.Enabled = false;
                cbIsPoFax.Enabled = false;
                dllFPstye.Enabled = false;
            }
            txtPONo.Text = "";
            txtAE.Text = "";
            txtGuestName.Text = "";
            txtGuestNo.Text = "";
            txtINSIDE.Text = "";
            txtPOName.Text = "";

            cbIsPoFax.Checked = true;

        }

        CG_POOrderService POSer = new CG_POOrderService();

        protected void lblSelect_Click(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));


                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                txtAE.Text = model.AE;
                txtGuestName.Text = model.GuestName;
                txtGuestNo.Text = model.GuestNo;
                txtINSIDE.Text = model.INSIDE;
                cbSpecial.Checked = model.IsSpecial;
                cbIsPoFax.Checked = model.IsPoFax;
                dllFPstye.SelectedItem.Text = model.FpType;
                Session["Comm_CGPONo"] = null;
                ddlPOTyle.Text = model.POType.ToString();
                SetColor(model.POType);
            }
        }

        protected void cbIsPoFax_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsPoFax.Checked)
            {
                dllFPstye.Enabled = true;

            }
            else
            {
                dllFPstye.Enabled = false;
                dllFPstye.SelectedItem.Text = "";
            }
        }

        private decimal GetHouseGoodPrice(out decimal num)
        {
            num = 0;
            if (!string.IsNullOrEmpty(hfGoodId.Value))
            {
                var sql = string.Format("select count(*) from TB_HouseGoods where goodId={0}", hfGoodId.Value);
                num = Convert.ToDecimal(DBHelp.ExeScalar(sql));
                if (num > 0)
                {
                    sql = string.Format("select GoodAvgPrice from TB_HouseGoods where goodId={0}", hfGoodId.Value);
                    return Convert.ToDecimal(DBHelp.ExeScalar(sql));
                }
            }
            return 0;
        }

        private void SetKuCunValueEnabled(CheckBox isKC, TextBox Supplier, TextBox SupperPrice, TextBox txtTotal)
        {
            if (Supplier.Text == "库存")
            {
                Supplier.Enabled = false;
                SupperPrice.Enabled = false;
                txtTotal.Enabled = false;
                isKC.Checked = true;
            }
            else
            {
                Supplier.Enabled = true;
                SupperPrice.Enabled = true;
                txtTotal.Enabled = true;
                isKC.Checked = false;
            }

        }


        private void SetKuCunValue(CheckBox isKC, TextBox Supplier, TextBox SupperPrice, TextBox txtTotal)
        {
            if (!string.IsNullOrEmpty(hfGoodId.Value))
            {
                if (isKC.Checked)
                {
                    decimal num = 0;
                    var GoodAvgPrice = GetHouseGoodPrice(out num);
                    if (num == 0)
                    {
                        //没有库存提示并且不能打钩
                        isKC.Checked = false;
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品没有库存，不能打钩！');</script>");
                        return;
                    }
                    Supplier.Enabled = false;
                    SupperPrice.Enabled = false;
                    txtTotal.Enabled = false;
                    Supplier.Text = "库存";
                    
                    txtSupperPrice.Text = GoodAvgPrice.ToString();
                    txtTotal.Text = (Convert.ToDecimal(txtNum.Text) * GoodAvgPrice).ToString();
                }
                else
                {
                    Supplier.Enabled = true;
                    SupperPrice.Enabled = true;
                    txtTotal.Enabled = true;
                    Supplier.Text = "";
                    SupperPrice.Text = "";
                    txtTotal.Text = "";
                }
            }
            else
            {
                isKC.Checked = false;
            }
        }

        protected void cbKuCun1_CheckedChanged(object sender, EventArgs e)
        {
            SetKuCunValue(cbKuCun1, txtSupplier, txtSupperPrice, txtTotal1);
        }
        protected void cbKuCun2_CheckedChanged(object sender, EventArgs e)
        {
            SetKuCunValue(cbKuCun2, txtSupper2, txtPrice2, txtTotal2);
        }
        protected void cbKuCun3_CheckedChanged(object sender, EventArgs e)
        {
            SetKuCunValue(cbKuCun3, txtSupper3, txtPrice3, txtTotal3);
        }
    }
}
