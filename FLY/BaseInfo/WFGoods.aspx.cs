using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.EFrom;

 
 
namespace VAN_OA.BaseInfo
{
    public partial class WFGoods : System.Web.UI.Page
    {
        private TB_GoodService goodSer = new TB_GoodService();

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_Good where GoodName='{0}' and GoodModel='{1}' and GoodSpec='{2}' and GoodTypeSmName='{3}' and GoodBrand='{4}'",
                    txtGoodName.Text,txtModel.Text,txtSpec.Text,ddlGoodSmType.Text,txtGoodBrand.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}],规格[{1}],型号[{2}],小类[{3}],品牌[{4}]已经存在！');</script>", txtGoodName.Text, txtSpec.Text,txtModel.Text,ddlGoodSmType.Text,txtGoodBrand.Text ));
                        return;
                    }
                    TB_Good model = getModel();
                    if (this.goodSer.Add(model) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
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
            base.Response.Redirect("~/BaseInfo/WFGoodsList.aspx");
        }

        private void Clear()
        {
            txtGoodNo.Text = "";
            txtModel.Text = "";
            txtSpec.Text = "";
            txtZhuJi.Text = "";
            txtGoodName.Text = "";
            txtGoodBrand.Text = "";
            txtProduct.Text = "";
            txtGoodName.Focus();
        }


        public TB_Good getModel()
        {            
            VAN_OA.Model.BaseInfo.TB_Good model = new VAN_OA.Model.BaseInfo.TB_Good();
            model.CreateUserId = Convert.ToInt32(Session["currentUserId"]);
            model.GoodModel = txtModel.Text;
            model.GoodName = txtGoodName.Text;
            model.GoodTypeSmName = ddlGoodSmType.Text;
            model.GoodSpec = txtSpec.Text;
            model.GoodTypeName = ddlGoodType.SelectedItem.Value;
            model.GoodUnit = txtUnit.Text;
            model.ZhuJi = txtZhuJi.Text;
            model.GoodBrand = txtGoodBrand.Text;

            model.GoodNumber = ddlNumber.Text;
            model.GoodCol = ddlCol.Text;
            model.GoodRow = ddlRow.Text;
            model.GoodArea = ddlArea.Text;
            model.GoodAreaNumber = model.GoodArea + model.GoodNumber + "-" + model.GoodRow + "-"+model.GoodCol;
            if (Request["allE_id"] != null)
            {
                model.GoodId = Convert.ToInt32(Request["allE_id"]);
            }
            model.IfSpec = cbSpec.Checked;
            model.Product = txtProduct.Text;
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_Good where GoodName='{0}' and GoodModel='{1}' and GoodSpec='{2}' and GoodTypeSmName='{4}' and GoodBrand='{5}' and GoodId<>{3}",
                    txtGoodName.Text, txtModel.Text, txtSpec.Text, Request["Id"], ddlGoodSmType.Text,txtGoodBrand.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('名称[{0}],规格[{1}],型号[{2}],已经存在！');</script>", txtGoodName.Text, txtSpec.Text, txtModel.Text));
                        return;

                    }
                    TB_Good model = getModel();
                    if (this.goodSer.Update(model))
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

            string strErr = "";


          

            if (this.txtGoodName.Text.Trim().Length == 0)
            {
                strErr = "商品名称不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtGoodName.Focus();
                return false;
            }

            if (this.ddlGoodType.Text.Trim().Length == 0)
            {
                strErr = "请选择商品类别！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.ddlGoodType.Focus();
                return false;
            }

            if (this.ddlGoodSmType.Text.Trim().Length == 0 || ddlGoodSmType.Text.Trim()=="0")
            {
                strErr = "请选择商品小类！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.ddlGoodSmType.Focus();
                return false;
            }


            if (this.txtUnit.Text.Trim().Length == 0)
            {
                strErr = "商品单位不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtUnit.Focus();
                return false;
            }
            if (this.txtGoodBrand.Text.Trim().Length == 0)
            {
                strErr = "商品品牌不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtGoodBrand.Focus();
                return false;
            }
            if (this.txtProduct.Text.Trim().Length == 0)
            {
                strErr = "商品产地不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtProduct.Focus();
                return false;
            }
            
            if (ddlArea.Text == "" || ddlNumber.Text == "" || ddlRow.Text == "" || ddlCol.Text == "")
            {
                strErr = "请选择仓位！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
               
                return false;
            }
            return true;
        }

        private void setEnable(bool result)
        {
            this.txtGoodName.Enabled = result;
            this.txtGoodNo.Enabled = result;
            this.txtModel.Enabled = result;
            this.txtSpec.Enabled = result;
            this.txtUnit.Enabled = result;
            this.txtZhuJi.Enabled = result;
            this.cbSpec.Enabled = result;
            this.txtGoodBrand.Enabled = result;
            ddlGoodType.Enabled = result;
            ddlGoodSmType.Enabled = result;
            txtProduct.Enabled = result;

            ddlArea.Enabled = result;
            ddlCol.Enabled = result;
            ddlRow.Enabled = result;
            ddlNumber.Enabled = result;
        }

        private void ShowInfo(int Id)
        {           
            TB_Good model = goodSer.GetModel(Id);
            txtGoodName.Text = model.GoodName;
            txtGoodNo.Text = model.GoodNo;
            txtModel.Text = model.GoodModel;
            txtSpec.Text = model.GoodSpec;
            txtUnit.Text = model.GoodUnit;
            txtZhuJi.Text = model.ZhuJi;
            cbSpec.Checked = model.IfSpec;
            txtGoodBrand.Text = model.GoodBrand;
            lblProNo.Text = model.ProNo;
            txtProduct.Text = model.Product;
            ddlArea.Text = model.GoodArea;
            ddlCol.Text = model.GoodCol;
            ddlRow.Text = model.GoodRow;
            ddlNumber.Text = model.GoodNumber;

            if (model.GoodTypeName != null)
            {
                ddlGoodType.SelectedValue = model.GoodTypeName.ToString();      
                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArray(" 1=1 and GoodTypeName='" + model.GoodTypeName + "'");
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodSmType.DataTextField = "GoodTypeSmName";
                ddlGoodSmType.DataValueField = "GoodTypeSmName";
            }

            if (model.GoodTypeSmName != null)
                ddlGoodSmType.Text = model.GoodTypeSmName.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //加载基本信息
                ddlNumber.Items.Add(new ListItem { Text = "", Value = "" });
                ddlRow.Items.Add(new ListItem { Text = "", Value = "" });
                ddlCol.Items.Add(new ListItem { Text = "", Value = "" });
                //货架号：1.全部  缺省 2….51 1,..50 
                for (int i = 1; i < 51; i++)
                {
                    ddlNumber.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    //层数：1.全部  缺省 2….21 1,2,3…20 
                    //部位：1.全部  缺省 2….21 1,2,3…20
                    if (i <= 21)
                    { 
                        ddlRow.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                        ddlCol.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                }
                
                TB_GoodsTypeService typeSer=new TB_GoodsTypeService ();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                allType.Insert(0, new TB_GoodsType());
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";

                if (base.Request["ProId"] != null)
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        
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
                        txtGoodBrand.Enabled = false;
                        ddlGoodSmType.Enabled = false;
                        ddlGoodType.Enabled = false;
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

        protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlGoodType.SelectedItem.Text == "")
            {
                List<TB_GoodsSmType> goodsSmTypeList = new List<TB_GoodsSmType>();
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeSmName";
                ddlGoodType.DataValueField = "GoodTypeSmName";
            }
            else
            {
                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArrayToddl(" 1=1 and GoodTypeName='" + ddlGoodType.SelectedItem.Value + "'");
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeSmName";
                ddlGoodType.DataValueField = "GoodTypeSmName";
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (ddlGoodSmType.SelectedItem == null)
                return;
            string sql = string.Format("  GoodBrand like '%{0}%' and GoodTypeSmName ='{1}' ", txtGoodBrand.Text, ddlGoodSmType.Text);
            if (Request["allE_id"] != null)
            {
                sql += " and GoodId<>" + Request["allE_id"];
            }
            List<TB_Good> gooQGooddList = this.goodSer.GetListArray(sql);
            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
            btnSub.Enabled = true;

            //btnAdd.Enabled = true;
            //btnUpdate.Enabled = true;
            
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
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
                    TB_Good model = getModel();
                    #endregion
                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    if (Request["allE_id"] == null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();
                        int userId = CreateUser;
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
                        if (goodSer.addTran(model, eform) > 0)
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

                            }
                        }
                        if (goodSer.updateTran(model, eform, forms))
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
    }
}
