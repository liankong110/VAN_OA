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
    public partial class WFToInvoice : System.Web.UI.Page
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
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写申请人！');</script>");
                txtName.Focus();

                return false;
            }




            if (txtDaoKuanDate.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写到款日期！');</script>");
                txtDaoKuanDate.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtDaoKuanDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款日期 格式错误！');</script>");
                    return false;
                }                 
            }

            if (txtTotal.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写到款金额！');</script>");
                txtDaoKuanDate.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额 格式错误！');</script>");
                    return false;
                }
                if (Convert.ToDecimal(txtTotal.Text) <= 0 && ddlStyle.SelectedItem.Value == "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写的到款金额格式有误！');</script>");
                    txtDaoKuanDate.Focus();

                    return false;
                }
                if (Convert.ToDecimal(txtTotal.Text) < 0 && ddlStyle.SelectedItem.Value == "1")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写的到款金额格式有误！');</script>");
                    txtDaoKuanDate.Focus();

                    return false;
                }
            }

            if (txtZhangQi.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('账期不能为空！');</script>");
                txtZhangQi.Focus();

                return false;
            }

            if (txtPONo.Text == "")
            {

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
            if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return false;
            }
            if (Request["allE_id"] == null)
            {
                //实际发票到款 
                if (ddlStyle.SelectedItem.Value == "0")
                {
                    if (lblFPNo.Text == "")
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票号不能为空！');</script>");
                        return false;
                    }
                    CG_POOrderService sellPOSer1 = new CG_POOrderService();
                    string sql = " 1=1 ";
                    sql += string.Format(" and PONo='{0}'", txtPONo.Text);
                    List<CG_POOrder> cars1 = sellPOSer1.GetOrder_ToInvoice(sql);
                    if (cars1.Count > 0)
                    {
                        if (Convert.ToDecimal(txtTotal.Text) > cars1[0].WeiTotal)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", txtPONo.Text, cars1[0].WeiTotal));
                            return false;
                        }
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('此项目{0}已完全到款');</script>", txtPONo.Text));
                        return false;
                    }


                    Sell_OrderFPService sellPOSer = new Sell_OrderFPService();
                    sql = " 1=1 ";
                    sql += string.Format(" and id={0}", lblFPId.Text);
                    List<Sell_OrderFP> cars = sellPOSer.GetFPtoInvoiceView(sql);
                    if (cars.Count > 0)
                    {
                        if (Convert.ToDecimal(txtTotal.Text) > cars[0].chaTotals)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", lblFPNo.Text, cars[0].chaTotals));
                            return false;
                        }
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('此项目{0}已完全到款');</script>", txtPONo.Text));
                        return false;
                    }

                }
                else if (ddlStyle.SelectedItem.Value == "1")//预付款
                {
                    //--发票总到款金额  含 执行中的，发票总金额 不含执行中的
                    //--发票总到款金额  是指 系统中 该项目编号的  按发票条件 录入到款金额的 合计数字！
                    //如果 发票总到款金额<发票总金额，提示 “需要按发票到款来执行” 返回原来界面
                    string ToInvoiceSql = string.Format("select isnull(sum(Total),0) from TB_ToInvoice where BusType=0 and PoNo='{0}' and State<>'不通过'", txtPONo.Text);
                    decimal Invoice =Convert.ToDecimal( DBHelp.ExeScalar(ToInvoiceSql));
                    ToInvoiceSql = string.Format("select isnull(sum(Total),0)  from Sell_OrderFP  where PoNo='{0}' and Status='通过'", txtPONo.Text);
                    decimal OrderFP = Convert.ToDecimal(DBHelp.ExeScalar(ToInvoiceSql));
                    if (Invoice < OrderFP)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('此项目{0},需要按发票到款来执行');</script>", txtPONo.Text));
                        return false;
                    }
                    CG_POOrderService sellPOSer = new CG_POOrderService();
                    string sql = " 1=1 ";
                    sql += string.Format(" and PONo='{0}'", txtPONo.Text);
                    List<CG_POOrder> cars = sellPOSer.GetOrder_ToInvoice(sql);
                    if (cars.Count > 0)
                    {
                        if (Convert.ToDecimal(txtTotal.Text) == 0)
                        {
                            if (cars[0].ifZenoPoInfo != 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('到款金额超过项目金额');</script>"));
                                return false;
                            }                            
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtTotal.Text) > cars[0].WeiTotal)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", txtPONo.Text, cars[0].WeiTotal));
                                return false;
                            }
                        }
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}不存在');</script>", txtPONo.Text));
                        return false;
                    }
                }
            }
            else  if (Request["ReAudit"] != null)                        
            {
                //实际发票到款 
                if (ddlStyle.SelectedItem.Value == "0")
                {

                    CG_POOrderService sellPOSer1 = new CG_POOrderService();
                    string sql = " 1=1 ";
                    sql += string.Format(" and PONo='{0}'", txtPONo.Text);
                    List<CG_POOrder> cars1 = sellPOSer1.GetOrder_ToInvoice_1(sql);
                   
                    TB_ToInvoiceService ToInvoiceSer = new TB_ToInvoiceService();
                    var oldModel= ToInvoiceSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                   
                    if (cars1.Count > 0)
                    {
                        if (Convert.ToDecimal(txtTotal.Text) > cars1[0].WeiTotal +oldModel.Total)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", txtPONo.Text, cars1[0].WeiTotal+oldModel.Total));
                            return false;
                        }
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('此项目{0}已完全到款');</script>", txtPONo.Text));
                        return false;
                    }

                    sql = "select Total from Sell_OrderFP where id= "+lblFPId.Text;
                    object obj = DBHelp.ExeScalar(sql);
                    if (!(obj is DBNull))
                    {
                        sql = "SELECT sum(Total) as sumTotal FROM TB_ToInvoice WHERE State<>'不通过' and id<>" + Request["allE_id"] + " and  FPId=" + lblFPId.Text;
                        object sumTotal = DBHelp.ExeScalar(sql);
                        if (sumTotal is DBNull)
                        {
                            sumTotal = 0;
                        }
                        if (Convert.ToDecimal(txtTotal.Text) > Convert.ToDecimal(obj)-Convert.ToDecimal(sumTotal))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", lblFPNo.Text, Convert.ToDecimal(obj) - Convert.ToDecimal(sumTotal)));
                            return false;
                        }

                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票不存在！');</script>"));
                        return false;
                    }
                }
                else if (ddlStyle.SelectedItem.Value == "1")//预付款
                {

                    if (Convert.ToDecimal(txtTotal.Text) <= 0 )
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写的到款金额格式有误！');</script>");
                        txtDaoKuanDate.Focus();

                        return false;
                    }

                    CG_POOrderService sellPOSer = new CG_POOrderService();
                    string sql = " 1=1 ";
                    sql += string.Format(" and PONo='{0}'", txtPONo.Text);
                    List<CG_POOrder> cars = sellPOSer.GetOrder_ToInvoice_1(sql);
                    TB_ToInvoiceService ToInvoiceSer = new TB_ToInvoiceService();
                    var oldModel = ToInvoiceSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                    if (cars.Count > 0)
                    {

                        if (Convert.ToDecimal(txtTotal.Text) > cars[0].WeiTotal + oldModel.Total)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}，金额剩余{1}，你填写的金额大于剩余金额！');</script>", txtPONo.Text, cars[0].WeiTotal + oldModel.Total));
                            return false;
                        }
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目{0}不存在');</script>", txtPONo.Text));
                        return false;
                    }
                }
            }

            return true;
        }



        private void setEnable(bool result)
        {
            result = false;
            txtDaoKuanDate.ReadOnly = !result;

            txtRemark.ReadOnly = !result;
            txtTotal.ReadOnly = !result;
            txtUpAccount.ReadOnly = !result;
            txtName.ReadOnly = !result;


            btnFp.Visible = false;
            btnYuFu.Visible = false;

            ImageButton1.Visible = false;

            ddlStyle.Enabled = false;
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



                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人
                        txtDaoKuanDate.Text = DateTime.Now.ToString();
                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;

                        if (Request["NewPONO"] != null)
                        {
                            //判断相同的项目是否有在执行中
                            string samePOno = string.Format("select count(*) from Sell_OrderFP where Status='执行中' and pono='{0}'", Request["NewPONO"]);
                            if (Convert.ToInt32(DBHelp.ExeScalar(samePOno)) > 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此项目存在发票单正在执行中，请稍后再试！');</script>");
                                return;
                            }
                            samePOno = string.Format("select count(*) from TB_ToInvoice  WHERE State='执行中' and PoNo='{0}'", Request["NewPONO"]);
                            if (Convert.ToInt32(DBHelp.ExeScalar(samePOno)) > 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此项目存在到款单正在执行中，请稍后再试！');</script>");
                                return;
                            }
                          
                            txtPOName.Text = HttpUtility.UrlDecode(Request["POName"]);
                            txtPONo.Text = Request["NewPONO"];
                            lblFPNo.Text = Request["FPNo"];
                            lblFPId.Text = Request["FPId"];
                            txtTotal.Text = Request["weiDao"];
                            txtSupplier.Text = HttpUtility.UrlDecode( Request["GuestName"]);

                            string sql = string.Format("select top 1 guestDays from TB_GuestTrack where guestName='{0}'", Request["GuestName"]);
                            object ob = DBHelp.ExeScalar(sql);
                            txtZhangQi.Text = ob is DBNull ? "0" : ob.ToString();                          
                            GetTotal();

                            TB_ToInvoiceService invoiceSer = new TB_ToInvoiceService();
                            var ZhuanPayTotal = invoiceSer.GetPayTotal(txtPONo.Text, Convert.ToDecimal(txtTotal.Text));

                            if (ZhuanPayTotal > 0)
                            {
                                sql = string.Format("select SUM(Total) as total from TB_ToInvoice where BusType=1 and PONO='{0}'", txtPONo.Text);
                                ob = DBHelp.ExeScalar(sql);
                                var yufuTotal = ob is DBNull ? 0 : Convert.ToDecimal(ob);
                                if (yufuTotal > 0)
                                {
                                    Sell_OrderFP pp = new Sell_OrderFPService().GetModel(Convert.ToInt32(lblFPId.Text));
                                    if (invoiceSer.YuPay_CreateInvoice(pp, Convert.ToDecimal(txtTotal.Text)))
                                    {
                                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结转成功！');window.opener=null;window.close();</script>");
                                        return;
                                    }
                                    else
                                    {
                                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结转失败！');window.opener=null;window.close();</script>");
                                        return;
                                    }
                                }
                            }
                            //else
                            //{
                            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据异常！');window.opener=null;window.close();</script>");
                            //    return;
                            //}
                        }
                        

                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人

                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                //从获取出的审核中 获取上级信息
                                List<A_Role_User> newList = new List<A_Role_User>();
                                for (int i = 0; i < roleUserList.Count; i++)
                                {
                                    if (roleUserList[i].UserId == use.ReportTo)
                                    {
                                        A_Role_User a = roleUserList[i];
                                        newList.Add(a);
                                        break;
                                    }
                                }

                                if (newList.Count > 0)
                                {
                                    ddlPers.DataSource = newList;
                                }
                                else
                                {
                                    ddlPers.DataSource = roleUserList;
                                }
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
                    else if (Request["ReAudit"] != null)//重新提交编辑
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

                        #region 到款单


                        TB_ToInvoiceService ToInvoiceSer = new TB_ToInvoiceService();



                        TB_ToInvoice toInvoiceModel = ToInvoiceSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDaoKuanDate.Text = toInvoiceModel.DaoKuanDate.ToString();
                        //txtDateTime.Text = toInvoiceModel.AppleDate.ToString();
                        txtName.Text = toInvoiceModel.CreateUser;
                        txtPOName.Text = toInvoiceModel.PoName;
                        txtPONo.Text = toInvoiceModel.PoNo;
                        txtRemark.Text = toInvoiceModel.Remark;
                        txtSupplier.Text = toInvoiceModel.GuestName;
                        txtTotal.Text = toInvoiceModel.Total.ToString();
                        txtUpAccount.Text = toInvoiceModel.UpAccount.ToString();
                        lblProNo.Text = toInvoiceModel.ProNo;
                        txtZhangQi.Text = toInvoiceModel.ZhangQi.ToString();
                        lblFPNo.Text = toInvoiceModel.FPNo;
                        lblFPId.Text = toInvoiceModel.FPId.ToString();
                        ddlStyle.Text = toInvoiceModel.BusType.ToString();
                        lblLastPayTotal.Text = toInvoiceModel.LastPayTotal.ToString();
                        GetTotal();
                        #endregion

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
                                //从获取出的审核中 获取上级信息
                                List<A_Role_User> newList = new List<A_Role_User>();
                                for (int i = 0; i < roleUserList.Count; i++)
                                {
                                    if (roleUserList[i].UserId == use.ReportTo)
                                    {
                                        A_Role_User a = roleUserList[i];
                                        newList.Add(a);
                                        break;
                                    }
                                }

                                if (newList.Count > 0)
                                {
                                    ddlPers.DataSource = newList;
                                }
                                else
                                {
                                    ddlPers.DataSource = roleUserList;
                                }
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

                        #region 到款单


                        TB_ToInvoiceService ToInvoiceSer = new TB_ToInvoiceService();

                       

                        TB_ToInvoice toInvoiceModel = ToInvoiceSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                        if (toInvoiceModel.TempGuid != "")
                        {
                            this.gvList.DataSource = new TB_ToInvoiceService().GetListArray_History(string.Format(" TempGuid='{0}'",toInvoiceModel.TempGuid));
                            this.gvList.DataBind();
                        }
                        txtDaoKuanDate.Text = toInvoiceModel.DaoKuanDate.ToString();
                        //txtDateTime.Text = toInvoiceModel.AppleDate.ToString();
                        txtName.Text = toInvoiceModel.CreateUser;
                        txtPOName.Text = toInvoiceModel.PoName;
                        txtPONo.Text = toInvoiceModel.PoNo;
                        txtRemark.Text = toInvoiceModel.Remark;
                        txtSupplier.Text = toInvoiceModel.GuestName;
                        txtTotal.Text = toInvoiceModel.Total.ToString();
                        txtUpAccount.Text = toInvoiceModel.UpAccount.ToString();
                        lblProNo.Text = toInvoiceModel.ProNo;
                        txtZhangQi.Text = toInvoiceModel.ZhangQi.ToString();
                        lblFPNo.Text = toInvoiceModel.FPNo;
                        lblFPId.Text = toInvoiceModel.FPId.ToString();
                        ddlStyle.Text = toInvoiceModel.BusType.ToString();
                        lblLastPayTotal.Text = toInvoiceModel.LastPayTotal.ToString();
                        GetTotal();

                        var sql = string.Format("select Total from [KingdeeInvoice].[dbo].[Invoice]  where InvoiceNumber='{0}'", toInvoiceModel.FPNo);
                        object ob = DBHelp.ExeScalar(sql);
                        lblInvoiceTotal.Text = (ob is DBNull||ob==null) ? "0" : ob.ToString();

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
                                    {//从获取出的审核中 获取上级信息
                                        List<A_Role_User> newList = new List<A_Role_User>();
                                        for (int i = 0; i < roleUserList.Count; i++)
                                        {
                                            if (roleUserList[i].UserId == use.ReportTo)
                                            {
                                                A_Role_User a = roleUserList[i];
                                                newList.Add(a);
                                                break;
                                            }
                                        }

                                        if (newList.Count > 0)
                                        {
                                            ddlPers.DataSource = newList;
                                        }
                                        else
                                        {
                                            ddlPers.DataSource = roleUserList;
                                        }
                                        // ddlPers.DataSource = roleUserList;
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
                                            //从获取出的审核中 获取上级信息
                                            List<A_Role_User> newList = new List<A_Role_User>();
                                            for (int i = 0; i < roleUserList.Count; i++)
                                            {
                                                if (roleUserList[i].UserId == use.ReportTo)
                                                {
                                                    A_Role_User a = roleUserList[i];
                                                    newList.Add(a);
                                                    break;
                                                }
                                            }

                                            if (newList.Count > 0)
                                            {
                                                ddlPers.DataSource = newList;
                                            }
                                            else
                                            {
                                                ddlPers.DataSource = roleUserList;
                                            }
                                            //ddlPers.DataSource = roleUserList;
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



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {


                    #region 获取单据基本信息


                    TB_ToInvoice model = new TB_ToInvoice();
                    model.AppleDate = DateTime.Now;
                    model.CreateUser = txtName.Text;
                    model.DaoKuanDate = Convert.ToDateTime(txtDaoKuanDate.Text);
                    model.GuestName = txtSupplier.Text;
                    model.PoName = txtPOName.Text;
                    model.PoNo = txtPONo.Text;
                    model.ProNo = lblProNo.Text;
                    model.Remark = txtRemark.Text;
                    model.Total = Convert.ToDecimal(txtTotal.Text);
                    if (txtUpAccount.Text != "")
                        model.UpAccount = Convert.ToDecimal(txtUpAccount.Text);
                    model.FPNo = lblFPNo.Text;
                    model.ZhangQi = Convert.ToDecimal(txtZhangQi.Text);
                    model.FPId = Convert.ToInt32(lblFPId.Text);
                    model.BusType = Convert.ToInt32(ddlStyle.SelectedItem.Value);
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
                        TB_ToInvoiceService carSer = new TB_ToInvoiceService();
                        if (ddlStyle.Text == "1")
                        {
                            model.LastPayTotal = Convert.ToDecimal(txtTotal.Text);
                        }
                        if (carSer.addTran(model, eform) > 0)
                        {

                            if (ddlPers.Visible == false)
                            {
                                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(txtPONo.Text);
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
                        if (Request["ReAudit"] != null)
                        {
                            TB_ToInvoiceService toInvoSerSer = new TB_ToInvoiceService();
                            //是否是此单据的申请人
                            var modelInvoice = toInvoSerSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                            //首先单子要先通过               

                            if (modelInvoice != null && modelInvoice.State == "通过")
                            {

                            }
                            else
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                                return;
                            }
                        }
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
                        TB_ToInvoiceService carSer = new TB_ToInvoiceService();
                        if (carSer.updateTran(model, eform, forms))
                        {
                            if (ddlPers.Visible == false)
                            {
                                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(txtPONo.Text);
                            }
                            else if (Request["ReAudit"] != null)
                            {
                                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(txtPONo.Text);
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

        Sell_OrderFPService fpOrder = new Sell_OrderFPService();
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            if (Session["DioSell_OrderFP"] != null)
            {
                Sell_OrderFP model = fpOrder.GetModel(Convert.ToInt32(Session["DioSell_OrderFP"]));
                txtSupplier.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                string sql = string.Format("select top 1 guestDays from TB_GuestTrack where guestName='{0}'", model.GuestName);
                object ob = DBHelp.ExeScalar(sql);
                txtZhangQi.Text = ob is DBNull ? "0" : ob.ToString();
                lblFPNo.Text = model.FPNo;
                //txtTotal.Text = model.Total.ToString();
                lblFPId.Text = model.Id.ToString();
                GetTotal();
                Session["DioSell_OrderFP"] = null;

                sql = string.Format("select Total from [KingdeeInvoice].[dbo].[Invoice] where InvoiceNumber='{0}'", model.FPNo);
                ob = DBHelp.ExeScalar(sql);
                lblInvoiceTotal.Text = (ob is DBNull || ob == null) ? "0" : ob.ToString();
                
                ////获取发票号=
                //string sql = string.Format("select FPNo from Sell_OrderFP where status='通过' and pono='{0}';",txtPONo.Text);
                //DataTable dt = DBHelp.getDataTable(sql);
                //lblFPNo.Text = "";
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    lblFPNo.Text += dt.Rows[i][0].ToString()+"/";
                //}
                //    TotalZhangQi();
            }
        }


        private void TotalZhangQi()
        {
            if (txtPONo.Text != "" && txtDaoKuanDate.Text != "")
            {
                string sql = string.Format("select  top 1 rutime from Sell_OrderOutHouse where pono='{0}' order by rutime ", txtPONo.Text);
                object time = DBHelp.ExeScalar(sql);
                if (time != null)
                {
                    TimeSpan ts = Convert.ToDateTime(txtDaoKuanDate.Text) - Convert.ToDateTime(time);
                    sql = string.Format("select top 1 accountXishu from TB_AccountPeriod where  accountName<={0}  order by accountName desc", ts.Days);
                    object accountXishu = DBHelp.ExeScalar(sql);

                    if (accountXishu != null)
                        txtUpAccount.Text = accountXishu.ToString();
                }
            }
            else
            {
                txtUpAccount.Text = "0";
            }
        }

        protected void txtDaoKuanDate_TextChanged(object sender, EventArgs e)
        {
            TotalZhangQi();

        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFPId.Text = "0";
            txtPOName.Text = "";
            txtPONo.Text = "";
            txtSupplier.Text = "";
            txtUpAccount.Text = "0";
            txtZhangQi.Text = "0";

            //实际发票到款 
            if (ddlStyle.SelectedItem.Value == "0")
            {
                btnFp.Visible = true;
                btnYuFu.Visible = false;


            }
            else if (ddlStyle.SelectedItem.Value == "1")//预付款
            {
                btnFp.Visible = false;
                btnYuFu.Visible = true;
            }
        }
        CG_POOrderService orderSer = new CG_POOrderService();
        protected void btnYuFu_Click(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                List<CG_POOrder> models = orderSer.GetListArray(" CG_POOrder.PONo='" + Session["Comm_CGPONo"] + "' and IFZhui=0");
                if (models.Count > 0)
                {
                    var model = models[0];
                    txtSupplier.Text = model.GuestName;
                    txtPOName.Text = model.POName;
                    txtPONo.Text = model.PONo;
                    string sql = string.Format("select top 1 guestDays from TB_GuestTrack where guestName='{0}'", model.GuestName);
                    object ob = DBHelp.ExeScalar(sql);
                    txtZhangQi.Text = ob is DBNull ? "0" : ob.ToString();
                    lblFPNo.Text = "";
                    lblFPId.Text = "0";// model.Id.ToString();
                    GetTotal();
                }
                Session["Comm_CGPONo"] = null;
            }

        }

        private void GetTotal()
        {
            var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", txtPONo.Text));
            if (list.Count > 0)
            {
                lblTotal.Text = (list[0].POTotal - list[0].TuiTotal).ToString();
            }
        }


    }
}
