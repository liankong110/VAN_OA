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
using System.Text;
using VAN_OA.Model.ReportForms;
using Newtonsoft.Json;

namespace VAN_OA.EFrom
{
    public partial class WFQuotePrice : System.Web.UI.Page
    {


        private tb_QuotePriceService QuotePriSer = new tb_QuotePriceService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    string QuoteNo = this.lblQuoteNo.Text;
                    string GuestName = txtGuestName.Text;
                    string GuestNo = this.lblGuestNo.Text;
                    DateTime QuoteDate = DateTime.Now;
                    string ResultGuestName = this.txtResultGuestName.Text;
                    string ResultGuestNo = this.txtResultGuestNo.Text;
                    string PayStyle = this.txtPayStyle.Text;
                    string GuestNameToInv = this.txtGuestNameToInv.Text;
                    string ContactPerToInv = this.txtContactPerToInv.Text;
                    string telToInv = this.lbltelToInv.Text;
                    string AddressToInv = this.txtAddressToInv.Text;
                    string InvoHeader = this.txtInvoHeader.Text;
                    string InvContactPer = this.txtInvContactPer.Text;
                    string InvAddress = this.txtInvAddress.Text;
                    string InvTel = this.txtInvTel.Text;
                    string NaShuiPer = this.lblNaShuiPer.Text;
                    string brandNo = this.lblbrandNo.Text;
                    string AddressTofa = this.txtAddressTofa.Text;
                    string BuessName = this.txtBuessName.Text;
                    string BuessEmail = this.txtBuessEmail.Text;
                    string ComTel = this.txtComTel.Text;
                    string ComChuanZhen = this.txtComChuanZhen.Text;
                    string ComBusTel = this.txtComBusTel.Text;
                    string ComName = this.txtComName.Text;
                    string NaShuiNo = this.txtNaShuiNo.Text;
                    string ComBrand = this.txtComBrand.Text;

                    VAN_OA.Model.EFrom.tb_QuotePrice model = new VAN_OA.Model.EFrom.tb_QuotePrice();
                    model.QuoteNo = QuoteNo;
                    model.GuestName = GuestName;
                    model.GuestNo = GuestNo;
                    model.QuoteDate = QuoteDate;
                    model.ResultGuestName = ResultGuestName;
                    model.ResultGuestNo = ResultGuestNo;
                    model.PayStyle = PayStyle;
                    model.GuestNameToInv = GuestNameToInv;
                    model.ContactPerToInv = ContactPerToInv;
                    model.telToInv = telToInv;
                    model.AddressToInv = AddressToInv;
                    model.InvoHeader = InvoHeader;
                    model.InvContactPer = InvContactPer;
                    model.InvAddress = InvAddress;
                    model.InvTel = InvTel;
                    model.NaShuiPer = NaShuiPer;
                    model.brandNo = brandNo;
                    model.AddressTofa = AddressTofa;
                    model.BuessName = BuessName;
                    model.BuessEmail = BuessEmail;
                    model.ComTel = ComTel;
                    model.ComChuanZhen = ComChuanZhen;
                    model.ComBusTel = ComBusTel;
                    model.ComName = ComName;
                    model.NaShuiNo = NaShuiNo;

                    model.ComBrand = ComBrand;

                    model.CreateTime = DateTime.Now;
                    model.CreateUser = Convert.ToInt32(Session["currentUserId"]);

                    model.ComBrand = ComBrand;
                    model.Address = lblZhanghao.Text ;
                    model.ZLBZ = txtZLBZ.Text;
                    model.YSBJ = txtYSBJ.Text;
                    model.FWBXDJ = txtFWBXDJ.Text;
                    model.JFQ = txtJFQ.Text;
                    model.Remark = txtRemark.Text;
                    model.IsYH = cbYH.Checked;
                    if (cbYH.Checked)
                    {
                        model.LastYH =Convert.ToDecimal(txtResultYH.Text);
                    }
                    List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
                    if (rbtnType3.Checked)
                    {
                        model.LaborCost =Convert.ToDecimal( txtLaborCost.Text);
                        model.EngineeringTax = Convert.ToDecimal(txtEngineeringTax.Text);
                        model.LIlv = Convert.ToDecimal(txtXX.Text);
                        model.QPType = 3;

                        //YY=设备材料价格+人工费 
                        txtRenGJS.Text = (invDetails.Sum(t => t.Total) + Convert.ToDecimal(txtLaborCost.Text)).ToString();
                    }
                    if (rbtnType2.Checked)
                    {
                        model.QPType = 2;
                    }
                    if (rbtnType1.Checked)
                    {
                        model.QPType = 1;
                    }
                    model.IsBrand = cbIsBrand.Checked;
                    model.IsProduct = cbIsProduct.Checked;
                    model.IsRemark = cbRemark.Checked;
                    model.IsGaiZhang = cbIsGaiZhang.Checked;
                    model.IsShuiYin = cbIsShuiYin.Checked;

                    VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                    List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and GuestId='{0}'", lblGuestNo.Text));
                    if (guestTrackLists.Count > 0)
                    {
                        TB_GuestTrack guest = guestTrackLists[0];
                        //|| txtAE.Text != model.AEName || txtINSIDE.Text != model.INSIDEName
                        if (txtGuestName.Text != guest.GuestName)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                            return;
                        }
                        if (guest.AEName != txtBuessName.Text)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('AE必须和申请人一样！');</script>");
                            return;
                        }

                        model.GuestId = guest.Id;


                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                        return;
                    }

                    if (this.QuotePriSer.addTran(model, invDetails) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        base.Response.Redirect(Session["POUrl"].ToString() + "?QuoteNo=" + model.QuoteNo);
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


        private void ShowInfo(int Id)
        {

            VAN_OA.Model.EFrom.tb_QuotePrice model = QuotePriSer.GetModel(Id);
            lblNo.Text = model.QuoteNo;
            this.lblQuoteNo.Text = model.QuoteNo;
            this.txtGuestName.Text = model.GuestName;
            this.lblGuestNo.Text = model.GuestNo;
            this.lblQuoteDate.Text = model.QuoteDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtResultGuestName.Text = model.ResultGuestName;
            this.txtResultGuestNo.Text = model.ResultGuestNo;
            this.txtPayStyle.Text = model.PayStyle;
            this.txtGuestNameToInv.Text = model.GuestNameToInv;
            this.txtContactPerToInv.Text = model.ContactPerToInv;
            this.lbltelToInv.Text = model.telToInv;
            this.txtAddressToInv.Text = model.AddressToInv;
            this.txtInvoHeader.Text = model.InvoHeader;
            this.txtInvContactPer.Text = model.InvContactPer;
            this.txtInvAddress.Text = model.InvAddress;
            this.txtInvTel.Text = model.InvTel;
            this.lblNaShuiPer.Text = model.NaShuiPer;
            this.lblbrandNo.Text = model.brandNo;

            this.txtAddressTofa.Text = model.AddressTofa;
            this.txtBuessName.Text = model.BuessName;
            this.txtBuessEmail.Text = model.BuessEmail;
            this.txtComTel.Text = model.ComTel;
            this.txtComChuanZhen.Text = model.ComChuanZhen;
            this.txtComBusTel.Text = model.ComBusTel;
            this.txtComName.Text = model.ComName;
            
            this.txtNaShuiNo.Text = model.NaShuiNo;

            this.txtComBrand.Text = model.ComBrand;
            lblZhanghao.Text = model.Address;
            txtGuestName.Text = model.GuestName;
            txtZLBZ.Text = model.ZLBZ;
            txtYSBJ.Text = model.YSBJ;
            txtFWBXDJ.Text = model.FWBXDJ;
            txtJFQ.Text = model.JFQ;
            txtRemark.Text = model.Remark;

            if (model.QPType == 1)
            {
                rbtnType1.Checked = true;
            }
            if (model.QPType == 2)
            {
                rbtnType2.Checked = true;
            }
            if (model.QPType == 3)
            {
                rbtnType3.Checked = true;
                txtLaborCost.Text=model.LaborCost.ToString();
                txtEngineeringTax.Text = model.EngineeringTax.ToString();
                txtXX.Text = model.LIlv.ToString();

               
            }             
            cbIsBrand.Checked=model.IsBrand ;
            cbIsProduct.Checked=model.IsProduct;
            cbRemark.Checked= model.IsRemark;
            cbIsGaiZhang.Checked= model.IsGaiZhang;
            cbIsShuiYin.Checked=model.IsShuiYin;

            cbYH.Checked = model.IsYH;
            if (cbYH.Checked)
            {
                txtResultYH.Text=model.LastYH.ToString();
            }

            GetALLTotal();
            Session["Company"] = model.ComName;

        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(Session["POUrl"].ToString());
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    string QuoteNo = this.lblQuoteNo.Text;
                    string GuestName = this.txtGuestName.Text;
                    string GuestNo = this.lblGuestNo.Text;
                    DateTime QuoteDate = DateTime.Now;
                    string ResultGuestName = this.txtResultGuestName.Text;
                    string ResultGuestNo = this.txtResultGuestNo.Text;
                    string PayStyle = this.txtPayStyle.Text;
                    string GuestNameToInv = this.txtGuestNameToInv.Text;
                    string ContactPerToInv = this.txtContactPerToInv.Text;
                    string telToInv = this.lbltelToInv.Text;
                    string AddressToInv = this.txtAddressToInv.Text;
                    string InvoHeader = this.txtInvoHeader.Text;
                    string InvContactPer = this.txtInvContactPer.Text;
                    string InvAddress = this.txtInvAddress.Text;
                    string InvTel = this.txtInvTel.Text;
                    string NaShuiPer = this.lblNaShuiPer.Text;
                    string brandNo = this.lblbrandNo.Text;

                    string AddressTofa = this.txtAddressTofa.Text;
                    string BuessName = this.txtBuessName.Text;
                    string BuessEmail = this.txtBuessEmail.Text;
                    string ComTel = this.txtComTel.Text;
                    string ComChuanZhen = this.txtComChuanZhen.Text;
                    string ComBusTel = this.txtComBusTel.Text;
                    string ComName = this.txtComName.Text;
                    string NaShuiNo = this.txtNaShuiNo.Text;

                    string ComBrand = this.txtComBrand.Text;


                    VAN_OA.Model.EFrom.tb_QuotePrice model = new VAN_OA.Model.EFrom.tb_QuotePrice();
                    model.QuoteNo = QuoteNo;
                    model.GuestName = GuestName;
                    model.GuestNo = GuestNo;
                    model.QuoteDate = QuoteDate;
                    model.ResultGuestName = ResultGuestName;
                    model.ResultGuestNo = ResultGuestNo;
                    model.PayStyle = PayStyle;
                    model.GuestNameToInv = GuestNameToInv;
                    model.ContactPerToInv = ContactPerToInv;
                    model.telToInv = telToInv;
                    model.AddressToInv = AddressToInv;
                    model.InvoHeader = InvoHeader;
                    model.InvContactPer = InvContactPer;
                    model.InvAddress = InvAddress;
                    model.InvTel = InvTel;
                    model.NaShuiPer = NaShuiPer;
                    model.brandNo = brandNo;

                    model.AddressTofa = AddressTofa;
                    model.BuessName = BuessName;
                    model.BuessEmail = BuessEmail;
                    model.ComTel = ComTel;
                    model.ComChuanZhen = ComChuanZhen;
                    model.ComBusTel = ComBusTel;
                    model.ComName = ComName;
                    model.NaShuiNo = NaShuiNo;

                    model.ComBrand = ComBrand;
                    model.Address = lblZhanghao.Text ;
                    model.ZLBZ = txtZLBZ.Text;
                    model.YSBJ = txtYSBJ.Text;
                    model.FWBXDJ = txtFWBXDJ.Text;
                    model.JFQ = txtJFQ.Text;
                    model.Remark = txtRemark.Text;
                    model.Id = Convert.ToInt32(base.Request["Id"]);

                    model.IsYH = cbYH.Checked;
                    if (cbYH.Checked)
                    {
                        model.LastYH = Convert.ToDecimal(txtResultYH.Text);
                    }

                    if (rbtnType3.Checked)
                    {
                        model.LaborCost = Convert.ToDecimal(txtLaborCost.Text);
                        model.EngineeringTax = Convert.ToDecimal(txtEngineeringTax.Text);
                        model.LIlv = Convert.ToDecimal(txtXX.Text);
                        model.QPType = 3;
                    }
                    if (rbtnType2.Checked)
                    {
                        model.QPType = 2;
                    }
                    if (rbtnType1.Checked)
                    {
                        model.QPType = 1;
                    }
                    model.IsBrand = cbIsBrand.Checked;
                    model.IsProduct = cbIsProduct.Checked;
                    model.IsRemark= cbRemark.Checked ;
                    model.IsGaiZhang = cbIsGaiZhang.Checked;
                    model.IsShuiYin = cbIsShuiYin.Checked;
                    List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;

                    string InvDetailsIds = "";
                    if (ViewState["InvDetailsIds"] != null)
                    {
                        InvDetailsIds = ViewState["InvDetailsIds"].ToString();
                    }
                    VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                    List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and GuestId='{0}'", lblGuestNo.Text));
                    if (guestTrackLists.Count > 0)
                    {
                        TB_GuestTrack guest = guestTrackLists[0];
                        //|| txtAE.Text != model.AEName || txtINSIDE.Text != model.INSIDEName
                        if (txtGuestName.Text != guest.GuestName)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                            return;
                        }
                        if (guest.AEName != txtBuessName.Text)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('AE必须和申请人一样！');</script>");
                            return;
                        }

                        model.GuestId = guest.Id;


                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户信息不存在请重新填写！');</script>");
                        return;
                    }
                    if (this.QuotePriSer.updateTran(model, invDetails, InvDetailsIds))
                    {
                        string url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + lblQuoteNo.Text + ".pdf";

                        url = url.Replace(@"EFrom\", @"ReportForms\");
                        if (System.IO.File.Exists(url))
                        {
                            try
                            {
                                System.IO.File.Delete(url);
                            }
                            catch (Exception)
                            {


                            }
                        }

                        url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + lblQuoteNo.Text + ".doc";

                        url = url.Replace(@"EFrom\", @"ReportForms\");
                        if (System.IO.File.Exists(url))
                        {
                            try
                            {
                                System.IO.File.Delete(url);
                            }
                            catch (Exception)
                            {


                            }
                        }
                        url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + lblQuoteNo.Text + "-word.rar";

                        url = url.Replace(@"EFrom\", @"ReportForms\");
                        if (System.IO.File.Exists(url))
                        {
                            try
                            {
                                System.IO.File.Delete(url);
                            }
                            catch (Exception)
                            {


                            }
                        }

                        url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + lblQuoteNo.Text + "-pdf.rar";

                        url = url.Replace(@"EFrom\", @"ReportForms\");
                        if (System.IO.File.Exists(url))
                        {
                            try
                            {
                                System.IO.File.Delete(url);
                            }
                            catch (Exception)
                            {


                            }
                        }
                        base.Response.Redirect(Session["POUrl"].ToString() + "?QuoteNo=" + model.QuoteNo);
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

            if (this.txtGuestName.Text.Trim() == "" || txtGuestName.Text == "0")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择客户名称！');</script>");
                this.txtGuestName.Focus();
                return false;
            }

            if (this.txtRemark.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写报价概要！');</script>");
                this.txtRemark.Focus();
                return false;
            }
           
            
            List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
            if (invDetails.Count <= 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加报价内容！');</script>");

                return false;
            }
            if (cbYH.Checked&&string.IsNullOrEmpty(txtResultYH.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添填写最终优惠价！');</script>");

                return false;
            }
            //最终优惠价：ZZ，ZZ是一个输入框内的值，缺省=报价单内的所有费用合计，
            //但可以修改，修改的值不能超过 缺省值，否则提交 提示“优惠价格必须小于合计价格”
            if (cbYH.Checked)
            { 
                //计算总价
                //只要小于 计税设备材料人工基数+工程计税 
                var sumTotal = Convert.ToDecimal(lblTotalDetailsXiao.Text) + Convert.ToDecimal(string.IsNullOrEmpty(txtLaborCost.Text) ? "0" : txtLaborCost.Text)+
                    Convert.ToDecimal(string.IsNullOrEmpty(txtEngineeringTax.Text) ? "0" : txtEngineeringTax.Text);             
                if (Convert.ToDecimal(txtResultYH.Text) >= sumTotal)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('优惠价格必须小于合计价格！');</script>");
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtLaborCost.Text))
            {
                if (CommHelp.VerifesToNum(txtLaborCost.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('人工费用 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtXX.Text))
            {
                if (CommHelp.VerifesToNum(txtXX.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工程计税 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtResultYH.Text))
            {
                if (CommHelp.VerifesToNum(txtResultYH.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('最终优惠价 格式错误！');</script>");
                    return false;
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                ViewState["Invs"] = null;
                Session["DataInvDetails"] = null;


                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    tb_QuotePrice_InvDetailsService invDetailSer = new tb_QuotePrice_InvDetailsService();
                    List<tb_QuotePrice_InvDetails> invDetails = invDetailSer.GetListArray(" QuoteId=" + base.Request["Id"]);                   

                    decimal total = 0;
                    for (int i = 0; i < invDetails.Count; i++)
                    {
                        invDetails[i].No = i + 1;
                        total += invDetails[i].Total;
                    }
                    lblTotalDetailsXiao.Text = total.ToString();
                    txtResultYH.Text = total.ToString();
                    lblTotalDetailsDa.Text = ConvertMoney(total);
                   
                    Session["DataInvDetails"] = invDetails;
                    ViewState["InvDetailsCount"] = invDetails.Count;
                    gvInvDetails.DataSource = invDetails;
                    gvInvDetails.DataBind();
                    ShowInfo(Convert.ToInt32(base.Request["Id"]));

                    if (rbtnType3.Checked)
                    {
                        txtRenGJS.Text = (invDetails.Sum(t => t.Total) + Convert.ToDecimal(txtLaborCost.Text)).ToString();
                    }

                    //try
                    //{

                    //    txtRenGJS.Text = (Convert.ToDecimal(string.IsNullOrEmpty(lblTotalDetailsXiao.Text) ? "0" : lblTotalDetailsXiao.Text) + Convert.ToDecimal(string.IsNullOrEmpty(txtLaborCost.Text) ? "0" : txtLaborCost.Text)).ToString();
                    //}
                    //catch (Exception)
                    //{

                    //}

                }
                else
                {

                    //加载初始数据
                    List<tb_QuotePrice_InvDetails> invDetails = new List<tb_QuotePrice_InvDetails>();
                    Session["DataInvDetails"] = invDetails;
                    ViewState["InvDetailsCount"] = invDetails.Count;
                    gvInvDetails.DataSource = invDetails;
                    gvInvDetails.DataBind();




                    //  银行转账 这里 的4行 你显示系统中的企业的信息吧。
                    Dal.BaseInfo.TB_CompanyService comInfoSer = new VAN_OA.Dal.BaseInfo.TB_CompanyService();
                    var comInfoList = comInfoSer.GetListArray(string.Format(" ComName='{0}'", Session["Company"]));
                    if (comInfoList.Count > 0)
                    {
                        Model.BaseInfo.TB_Company model = comInfoList[0];
                        txtComName.Text = model.ComName;
                        txtComBrand.Text = model.KaiHuHang;
                        lblZhanghao.Text = model.KaHao;
                        txtNaShuiNo.Text = model.XinYongCode;

                        txtComChuanZhen.Text = model.ChuanZhen;//公司传真?
                        txtComTel.Text = model.ZhuSuo;//卖方地址?
                  
                    }
                    //卖方业务代表 这里5个输入框 ，缺省帮我把AE的人员信息表中的字段显示在上面，且不可修改！
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtBuessName.Text = use.LoginName;
                    txtComBusTel.Text = use.LoginPhone;
                    txtBuessEmail.Text = use.LoginMemo;


                    lblQuoteDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.btnUpdate.Visible = false;
                }
                if (Request["Copy"] != null)
                {

                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    lblQuoteDate.Text = DateTime.Now.ToString();
                    lblNo.Text = "";
                }
            }
            else
            {
                if (Request.Form["__EVENTARGUMENT"] == "GuestName")
                {
                    TextChange();
                }
            }
        }

        #region 人民币小写金额转大写金额
        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="Money">接收需要转换的小写金额</param>
        /// <returns>返回大写金额</returns>
        public string ConvertMoney(Decimal Money)
        {
            //金额转换程序
            string MoneyNum = "";//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾圆角分";//模

            MoneyNum = ((long)(Money * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr = MoneyStr + "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零圆")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整") || MoneyStr.Contains("角整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零圆", "圆零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("角整", "角");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            return MoneyStr;
        }
        #endregion
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (Session["DataInvDetails"] != null)
            {
                decimal total = 0;
                List<tb_QuotePrice_InvDetails> invDetails = Session["InvDetails"] as List<tb_QuotePrice_InvDetails>;
                if (invDetails == null)
                {
                    invDetails = new List<tb_QuotePrice_InvDetails>();
                }
                List<tb_QuotePrice_InvDetails> invDetails1 = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
                if (invDetails1 == null)
                {
                    invDetails1 = new List<tb_QuotePrice_InvDetails>();
                }
                foreach (var m in invDetails)
                {
                    invDetails1.Add(m);
                }
                Session["DataInvDetails"] = invDetails1;
                Session["InvDetails"] = null;

                for (int i = 0; i < invDetails1.Count; i++)
                {
                    invDetails1[i].No = i + 1;
                    total += invDetails1[i].Total;
                }
                lblTotalDetailsXiao.Text = total.ToString();
                txtResultYH.Text = total.ToString();
                lblTotalDetailsDa.Text = ConvertMoney(total);
                gvInvDetails.DataSource = invDetails1;
                gvInvDetails.DataBind();

                if (!string.IsNullOrEmpty(txtLaborCost.Text))
                {
                    //YY=设备材料价格+人工费 
                    txtRenGJS.Text = (invDetails1.Sum(t => t.Total) + Convert.ToDecimal(txtLaborCost.Text)).ToString();
                }
                else
                {
                    //YY=设备材料价格+人工费 
                    txtRenGJS.Text = invDetails1.Sum(t => t.Total).ToString();
                }
                GetALLTotal();
            }
        }





        protected void gvInvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvInvDetails.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["InvDetailsIds"] == null)
                {
                    ViewState["InvDetailsIds"] = this.gvInvDetails.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["InvDetailsIds"].ToString();
                    ids += this.gvInvDetails.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["InvDetailsIds"] = ids;
                }
            }

            if (Session["DataInvDetails"] != null)
            {
                List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
                invDetails.RemoveAt(e.RowIndex);

                ViewState["InvDetailsCount"] = invDetails.Count;

                decimal total = 0;
                for (int i = 0; i < invDetails.Count; i++)
                {
                    invDetails[i].No = i + 1;
                    total += invDetails[i].Total;
                }
                lblTotalDetailsXiao.Text = total.ToString();
                txtResultYH.Text = total.ToString();
                lblTotalDetailsDa.Text = ConvertMoney(total);

                gvInvDetails.DataSource = invDetails;
                gvInvDetails.DataBind();
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var json = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                tb_QuotePrice_InvDetails model = e.Row.DataItem as tb_QuotePrice_InvDetails;

                json = JsonConvert.SerializeObject(model);
            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {              

                string val = string.Format("javascript:window.showModalDialog('QPInvs.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }
        }

        protected void gvInvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["m"] != null)
            {
                tb_QuotePrice_InvDetails m = Session["m"] as tb_QuotePrice_InvDetails;
                List<tb_QuotePrice_InvDetails> invDetails = Session["DataInvDetails"] as List<tb_QuotePrice_InvDetails>;
                invDetails[m.UpdateIndex] = m;
                Session["m"] = null;
                gvInvDetails.DataSource = invDetails;
                gvInvDetails.DataBind();

                if (!string.IsNullOrEmpty(txtLaborCost.Text))
                {
                    //YY=设备材料价格+人工费 
                    txtRenGJS.Text = (invDetails.Sum(t => t.Total) + Convert.ToDecimal(txtLaborCost.Text)).ToString();
                }
                else
                {
                    //YY=设备材料价格+人工费 
                    txtRenGJS.Text = invDetails.Sum(t => t.Total).ToString();
                }
                GetALLTotal();
            }
        }

        protected void gvInvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var json = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                tb_QuotePrice_InvDetails model = e.Row.DataItem as tb_QuotePrice_InvDetails;
                //json = JsonConvert.SerializeObject(model);
            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                //json= Uri.EscapeUriString(json);
                //json = HttpUtility.UrlEncode(CommHelp.DES3Encrypt(json,"AAA"));

              
                //string val = string.Format("javascript:window.showModalDialog('QPInvDetails.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                //btnEdit.Attributes.Add("onclick", val);

                string val = string.Format("javascript:window.showModalDialog('QPInvDetails.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex + "&m=" + json);
                btnEdit.Attributes.Add("onclick", val);
            }
        }

        protected void txtGuestName_SelectedIndexChanged(object sender, EventArgs e)
        {

            Dal.ReportForms.TB_GuestTrackService guestSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestList = guestSer.GetListArray(string.Format(" GuestName='{0}'", txtGuestName.Text));
            if (guestList.Count > 0)
            {
                lblGuestNo.Text = guestList[0].GuestId.ToString();
                txtResultGuestNo.Text = guestList[0].GuestId.ToString();
                txtContactPerToInv.Text = guestList[0].LikeMan;

                lbltelToInv.Text = guestList[0].Phone;

                txtAddressToInv.Text = guestList[0].GuestAddress;
                txtAddressTofa.Text = guestList[0].GuestAddress;
            }


        }

        protected void txtGuestName_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void TextChange()
        {
            VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
            StringBuilder strSql = new StringBuilder();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (1 <= month && month <= 3)
            {
                strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
            }
            else if (4 <= month && month <= 6)
            {
                strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
            }
            else if (7 <= month && month <= 9)
            {
                strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
            }
            else if (10 <= month && month <= 12)
            {
                strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
            }
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetListArray(string.Format("  GuestName= '{0}' " + strSql.ToString(), txtGuestName.Text));

            if (guestTrackLists.Count > 0)
            {
                var obj = guestTrackLists[0];
                lblGuestNo.Text = obj.GuestId;
                txtContactPerToInv.Text = obj.LikeMan; //联系人
                lbltelToInv.Text = obj.Phone; //电话
                txtGuestNameToInv.Text = obj.FoxOrEmail; //传真
                txtAddressToInv.Text = obj.GuestAddress; //地址
                txtInvoHeader.Text = obj.GuestName; //发票抬头
                txtInvAddress.Text = obj.GuestAddress; //注册地址=地址
                txtInvTel.Text = obj.Phone; //注册电话=电话
                lblNaShuiPer.Text = obj.GuestShui; //社会统一信用代码=税号
                lblbrandNo.Text = obj.GuestBrandNo; //开户行帐号
                txtAddressTofa.Text = obj.GuestAddress; //发票邮寄地址=地址
                //txtComBusTel.Text = obj.Phone; //联系人及电话=电话
                txtInvContactPer.Text = obj.LikeMan + "," + obj.Phone; //联系人
                txtResultGuestName.Text = obj.GuestName; //最终联系人
            }
        }

        protected void btnXX_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtXX.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写税率！');</script>");
                this.txtXX.Focus();
                return;
            }

            try
            {
                if (Convert.ToDecimal(txtXX.Text) > 100 || Convert.ToDecimal(txtXX.Text) < 0)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('工程税率超出范围！');</script>");
                    this.txtXX.Focus();
                    return;
                }
                //， 按（设备材料合计+人工费用）* XX% 计算出 税费
                txtEngineeringTax.Text = ((Convert.ToDecimal(txtXX.Text)/100) * (Convert.ToDecimal(txtLaborCost.Text) + Convert.ToDecimal(lblTotalDetailsXiao.Text))).ToString();
            }
            catch (Exception)
            {
                
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('税率有问题！');</script>");
                this.txtXX.Focus();
            }
            GetALLTotal();
        }

        /// <summary>
        /// 报价单金额合计
        /// </summary>
        public void GetALLTotal()
        {
            if (cbYH.Checked)
            {
                txtAllTotal.Text = txtResultYH.Text;
            }
            else
            {
                txtAllTotal.Text = (Convert.ToDecimal(lblTotalDetailsXiao.Text) 
                    + Convert.ToDecimal(txtLaborCost.Text == "" ? "0" : txtLaborCost.Text)
                    + Convert.ToDecimal(txtEngineeringTax.Text == "" ? "0" : txtEngineeringTax.Text)).ToString();
            }

            try
            {

                txtRenGJS.Text = (Convert.ToDecimal(string.IsNullOrEmpty(lblTotalDetailsXiao.Text) ? "0" : lblTotalDetailsXiao.Text) + Convert.ToDecimal(string.IsNullOrEmpty(txtLaborCost.Text) ? "0" : txtLaborCost.Text)).ToString();
            }
            catch (Exception)
            {

            }
        }
    }
}
