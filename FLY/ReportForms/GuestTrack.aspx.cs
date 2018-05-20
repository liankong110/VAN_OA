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
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class GuestTrack : System.Web.UI.Page
    {
        private TB_GuestTrackService guestTrackSer = new TB_GuestTrackService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                     
                    
                    
                  
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string GuestName = this.txtGuestName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string GuestId = this.txtGuestId.Text;
                    string GuestAddress = this.txtGuestAddress.Text;
                    string GuestHttp = this.txtGuestHttp.Text;
                    string GuestShui = this.txtGuestShui.Text.Trim();
                    string GuestGong = this.txtGuestGong.Text.Trim();
                    string GuestBrandNo = this.txtGuestBrandNo.Text.Trim();
                    string GuestBrandName = this.txtGuestBrandName.Text.Trim();
                    int AE = int.Parse(ddlAE.SelectedItem.Value);
                    int INSIDE = int.Parse(this.ddlINSIDE.SelectedItem.Value);
                    string Remark = this.txtRemark.Text;

                    

                    //string FristMeet = this.txtFristMeet.Text;
                    //string SecondMeet = this.txtSecondMeet.Text;
                    //string FaceMeet = this.txtFaceMeet.Text;
                    //decimal Price = 0;
                    //if (txtPrice.Text != "")
                    //    Price = decimal.Parse(this.txtPrice.Text);
                    //bool IfSuccess = this.chkIfSuccess.Checked;
                    //string MyAppraise = this.txtMyAppraise.Text;
                    //string ManAppraise = this.txtManAppraise.Text;
                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    DateTime CreateTime = DateTime.Now;

                    VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                    model.Time = Time;
                    model.GuestName = GuestName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;
                    //model.FristMeet = FristMeet;
                    //model.SecondMeet = SecondMeet;
                    //model.FaceMeet = FaceMeet;
                    //model.Price = Price;
                    //model.IfSuccess = IfSuccess;
                    //model.MyAppraise = MyAppraise;
                    //model.ManAppraise = ManAppraise;
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;


                    model.GuestId = GuestId;
                    model.GuestAddress = GuestAddress;
                    model.GuestHttp = GuestHttp;
                    model.GuestShui = GuestShui;
                    model.GuestGong = GuestGong;
                    model.GuestBrandNo = GuestBrandNo;
                    model.GuestBrandName = GuestBrandName;
                    model.AE = AE;
                    model.INSIDE = INSIDE;
                    model.Remark = Remark;

                    if (txtGuestTotal.Text.Trim() != "")
                        model.GuestTotal = decimal.Parse(this.txtGuestTotal.Text.Trim());

                    if (txtGuestLiRun.Text.Trim() != "")
                        model.GuestLiRun = decimal.Parse(this.txtGuestLiRun.Text);

                    if (txtGuestDays.Text.Trim() != "")
                        model.GuestDays = decimal.Parse(this.txtGuestDays.Text.Trim());
                    string INSIDERemark = this.txtINSIDERemark.Text;
                    model.INSIDERemark = INSIDERemark;

                    model.SimpGuestName = txtSimpGuestName.Text;

                    model.YearNo = txtYear.Text;
                    model.QuartNo = txtMouth.Text;


                    if (ddlINSIDEPre.Text != "")
                    {
                        model.INSIDEPer = Convert.ToDecimal(ddlINSIDEPre.Text);
                    }



                    if (ddlAEPre.Text != "")
                    {
                        model.AEPer = Convert.ToDecimal(ddlAEPre.Text);
                    }

                    model.IsSpecial = cbIsSpecial.Checked;
                    model.MyGuestType = ddlGuestTypeList.SelectedItem.Text;
                    model.MyGuestPro = Convert.ToInt32(ddlGuestTypeList.SelectedItem.Text);
                    if (this.guestTrackSer.Add(model) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        
                        this.txtTime.Text ="";
                        this.txtGuestName.Text = "";
                        this.txtPhone.Text = "";
                        this.txtLikeMan.Text = "";
                        this.txtJob.Text = "";
                        this.txtFoxOrEmail.Text = "";
                        this.chkIfSave.Checked = true;
                        this.txtQQMsn.Text = "";


                        //this.txtFristMeet.Text = "";
                        //this.txtSecondMeet.Text = "";
                        //this.txtFaceMeet.Text = "";
                        //this.txtPrice.Text = "";
                        //this.chkIfSuccess.Checked = true;
                        //this.txtMyAppraise.Text = "";
                        //this.txtManAppraise.Text = "";


                        this.txtGuestId.Text = "";
                        this.txtGuestAddress.Text = "";
                        this.txtGuestHttp.Text  ="";
                        this.txtGuestShui.Text = "";
                        this.txtGuestGong.Text = "";
                        this.txtGuestBrandNo.Text = "";
                        this.txtGuestBrandName.Text = "";
                        txtRemark.Text = "";


                        txtGuestTotal.Text = "";
                        txtGuestLiRun.Text = "";
                        txtGuestDays.Text = "";
                        txtINSIDERemark.Text = "";
                        txtYear.Text = "";
                        txtMouth.Text = "";

                        ddlAEPre.Text = "";
                        ddlINSIDEPre.Text = "";
                        model.INSIDERemark = INSIDERemark;
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
            //base.Response.Redirect(Session["POUrl"].ToString());


            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }

        }




        private void setEnable(bool result)
        {
            txtTime.ReadOnly = !result;
            txtGuestName.ReadOnly = !result;
            txtPhone.ReadOnly = !result;
            txtLikeMan.ReadOnly = !result;
            txtFoxOrEmail.ReadOnly = !result;
            chkIfSave.Enabled = result; 
           
            txtGuestAddress.ReadOnly = !result;
            txtGuestHttp.ReadOnly = !result;
            txtGuestShui.ReadOnly = !result;
            txtGuestGong.ReadOnly = !result;
            txtGuestBrandNo.ReadOnly = !result;
            txtGuestBrandName.ReadOnly = !result;
            ddlAE.Enabled = result;
            ddlINSIDE.Enabled = result; 
            txtRemark.ReadOnly = !result; 
            txtGuestTotal.ReadOnly = !result; 
            txtGuestLiRun.ReadOnly = !result; 
            txtGuestDays.ReadOnly = !result; 
             txtINSIDERemark.ReadOnly = !result; 
             //txtYear.ReadOnly = !result; 
             //txtMouth.ReadOnly = !result; 
             ddlINSIDEPre.Enabled = result;
             ddlAEPre.Enabled = result; 
             //txtMouth.ReadOnly = !result; 
             //txtMouth.ReadOnly = !result;
             Image1.Enabled = result;
             txtQQMsn.ReadOnly = !result;
              cbIsSpecial.Enabled=result;
              ddlGuestTypeList.Enabled = result;
              ddlGuestProList.Enabled = result;
              txtSimpGuestName.ReadOnly = !result; 
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {


                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string GuestName = this.txtGuestName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;
                    //string FristMeet = this.txtFristMeet.Text;
                    //string SecondMeet = this.txtSecondMeet.Text;
                    //string FaceMeet = this.txtFaceMeet.Text;
                    //  decimal Price=0;
                    //if(txtPrice.Text!="")
                    // Price = decimal.Parse(this.txtPrice.Text);
                    //bool IfSuccess = this.chkIfSuccess.Checked;
                    //string MyAppraise = this.txtMyAppraise.Text;
                    //string ManAppraise = this.txtManAppraise.Text;

                    string GuestId = this.txtGuestId.Text;
                    string GuestAddress = this.txtGuestAddress.Text;
                    string GuestHttp = this.txtGuestHttp.Text;
                    string GuestShui = this.txtGuestShui.Text.Trim();
                    string GuestGong = this.txtGuestGong.Text.Trim();
                    string GuestBrandNo = this.txtGuestBrandNo.Text.Trim();
                    string GuestBrandName = this.txtGuestBrandName.Text.Trim();
                    int AE = int.Parse(ddlAE.SelectedItem.Value);
                    int INSIDE = int.Parse(this.ddlINSIDE.SelectedItem.Value);
                    string Remark = this.txtRemark.Text;



                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    DateTime CreateTime = DateTime.Now;

                    VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                    model.Time = Time;
                    model.GuestName = GuestName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;
                    //model.FristMeet = FristMeet;
                    //model.SecondMeet = SecondMeet;
                    //model.FaceMeet = FaceMeet;
                    //model.Price = Price;
                    //model.IfSuccess = IfSuccess;
                    //model.MyAppraise = MyAppraise;
                    //model.ManAppraise = ManAppraise;
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;

                    model.GuestId = GuestId;
                    model.GuestAddress = GuestAddress;
                    model.GuestHttp = GuestHttp;
                    model.GuestShui = GuestShui;
                    model.GuestGong = GuestGong;
                    model.GuestBrandNo = GuestBrandNo;
                    model.GuestBrandName = GuestBrandName;
                    model.AE = AE;
                    model.INSIDE = INSIDE;
                    model.Remark = Remark;

                    if (txtGuestTotal.Text.Trim() != "")
                        model.GuestTotal = decimal.Parse(this.txtGuestTotal.Text.Trim());

                    if (txtGuestLiRun.Text.Trim() != "")
                        model.GuestLiRun = decimal.Parse(this.txtGuestLiRun.Text);

                    if (txtGuestDays.Text.Trim() != "")
                        model.GuestDays = decimal.Parse(this.txtGuestDays.Text.Trim());
                    string INSIDERemark = this.txtINSIDERemark.Text;
                    model.INSIDERemark = INSIDERemark;


                    model.YearNo = txtYear.Text;
                    model.QuartNo = txtMouth.Text;
                    model.SimpGuestName = txtSimpGuestName.Text;
                    if (ddlINSIDEPre.Text != "")
                    {
                        model.INSIDEPer = Convert.ToDecimal(ddlINSIDEPre.Text);
                    }

                    if (ddlAEPre.Text != "")
                    {
                        model.AEPer = Convert.ToDecimal(ddlAEPre.Text);
                    }
                    model.Id = Convert.ToInt32(base.Request["Id"]);

                    model.IsSpecial = cbIsSpecial.Checked;
                    model.MyGuestType = ddlGuestTypeList.SelectedItem.Text;
                    model.MyGuestPro =Convert.ToInt32( ddlGuestProList.SelectedItem.Text);
                    if (this.guestTrackSer.Update(model))
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
           
             

             
            if (this.txtGuestName.Text.Trim().Length == 0)
            {
                
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户名称不能为空！');</script>");
                this.txtGuestName.Focus();
                return false;
            }

            if (this.txtSimpGuestName.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户简称不能为空！');</script>");
                this.txtSimpGuestName.Focus();
                return false;
            }

            if (this.txtPhone.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('电话/手机不能为空！');</script>");
                this.txtPhone.Focus();
                return false;
            }

            if (this.txtGuestShui.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户税务登记号不能为空！');</script>");
                this.txtGuestShui.Focus();
                return false;
            }

            if (this.txtGuestGong.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户工商注册号不能为空！');</script>");
                this.txtGuestGong.Focus();
                return false;
            }

            if (this.txtGuestBrandNo.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('银行账号不能为空！');</script>");
                this.txtGuestBrandNo.Focus();
                return false;
            }

            if (this.txtGuestBrandName.Text.Trim().Length == 0)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开户行不能为空！');</script>");
                this.txtGuestBrandName.Focus();
                return false;
            }

            if (txtGuestAddress.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('联系人地址不能为空！');</script>");
                this.txtGuestAddress.Focus();
                return false;
            }

            //if (this.txtPhone.Text.Trim().Length == 0)
            //{
                 
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('电话/手机不能为空！！');</script>");
            //    this.txtPhone.Focus();
            //    return false;
            //}
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
            //if (this.txtFoxOrEmail.Text.Trim().Length == 0)
            //{
                
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('传真或邮箱不能为空！');</script>");
            //    this.txtFoxOrEmail.Focus();
            //    return false;
            //}
            //if (lblCreateUser.Text != ddlAE.SelectedItem.Text)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('AE必须和申请人一样！');</script>");
            //    this.txtJob.Focus();
            //    return false;
            //}


            if (this.txtGuestTotal.Text.Trim().Length != 0)
            {
                try
                {
                    Convert.ToDecimal(txtGuestTotal.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('上季度销售额格式错误！');</script>");
                    return false;
                }


            }

            if (this.txtGuestLiRun.Text.Trim().Length != 0)
            {
                try
                {
                    Convert.ToDecimal(txtGuestLiRun.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('上季度利润额格式错误！');</script>");
                    return false;
                }


            }

            if (this.txtGuestDays.Text.Trim().Length != 0)
            {
                try
                {
                    Convert.ToDecimal(txtGuestDays.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('上季度收款期额格式错误！');</script>");
                    return false;
                }


            }



            if (Request["allE_id"] == null)//单据增加
            { 
                //查看客户信息是否存在
                string sql = string.Format(@"select COUNT(*) from TB_GuestTrack
where  (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state<>'不通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') )) and GuestName='{0}'",txtGuestName.Text.Trim());
                object obj=DBHelp.ExeScalar(sql);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('客户[{0}]已经存在 或正在审批中，无法申请新客户！');</script>",txtGuestName.Text));
                    return false;
                }
            }
            return true;
        }
        private void ShowInfo(int Id)
        {

            VAN_OA.Model.ReportForms.TB_GuestTrack model = guestTrackSer.GetModel(Id);
            txtSimpGuestName.Text = model.SimpGuestName;
            this.txtTime.Text = model.Time.ToString();
            this.txtGuestName.Text = model.GuestName;
            this.txtPhone.Text = model.Phone;
            this.txtLikeMan.Text = model.LikeMan;
            this.txtJob.Text = model.Job;
            this.txtFoxOrEmail.Text = model.FoxOrEmail;
            this.chkIfSave.Checked = model.IfSave;
            this.txtQQMsn.Text = model.QQMsn;

            this.txtGuestId.Text = model.GuestId;
            this.txtGuestAddress.Text = model.GuestAddress;
            this.txtGuestHttp.Text = model.GuestHttp;
            this.txtGuestShui.Text = model.GuestShui;
            this.txtGuestGong.Text = model.GuestGong;
            this.txtGuestBrandNo.Text = model.GuestBrandNo;
            this.txtGuestBrandName.Text = model.GuestBrandName;

            cbIsSpecial.Checked = model.IsSpecial;
            ddlGuestTypeList.SelectedValue = model.MyGuestType;
            ddlGuestProList.SelectedValue = model.MyGuestPro.ToString();
            try
            {
                this.ddlAE.SelectedValue = model.AE.ToString();
            }
            catch (Exception)
            {
                 
            }
            try
            {
                this.ddlINSIDE.SelectedValue = model.INSIDE.ToString();
            }
            catch (Exception)
            {
                
                
            }
            txtRemark.Text = model.Remark;

            //this.txtFristMeet.Text = model.FristMeet;
            //this.txtSecondMeet.Text = model.SecondMeet;
            //this.txtFaceMeet.Text = model.FaceMeet;
            //this.txtPrice.Text = model.Price.ToString();
            //this.chkIfSuccess.Checked = model.IfSuccess;
            //this.txtMyAppraise.Text = model.MyAppraise;
            //this.txtManAppraise.Text = model.ManAppraise;

            if (model.GuestLiRun != null)
            {
                txtGuestLiRun.Text = model.GuestLiRun.ToString();
            }

            if (model.GuestTotal != null)
            {
                txtGuestTotal.Text = model.GuestTotal.ToString();
            }

            if (model.GuestDays != null)
            {
                txtGuestDays.Text = model.GuestDays.ToString();
            }


            if (model.AEPer != null)
            {
                ddlAEPre.Text = model.AEPer.ToString();
            }

            if (model.INSIDEPer != null)
            {

                ddlINSIDEPre.Text = model.INSIDEPer.ToString();
            }

            txtINSIDERemark.Text = model.INSIDERemark;
            lblProNo.Text = model.ProNo;

            txtMouth.Text = model.QuartNo;
            txtYear.Text = model.YearNo;

            lblCreateTime.Text = model.CreateTime.ToShortDateString().ToString();
            lblCreateUser.Text = model.UserName;

            if (model.MyGuestType == "企业用户")
            {
                ddlGuestTypeList.BackColor = System.Drawing.Color.Red;
            }
            
        }

        private void setBaiFenBi()
        {

            List<ListItem> allItems = new List<ListItem>();
            ListItem item = new ListItem("", "");
            allItems.Add(item);
            for (int i = 5; i <= 100; i += 5)
            {
                ListItem li = new ListItem(i.ToString(), i.ToString());
                allItems.Add(li);
            }
            ddlAEPre.DataSource = allItems;
            ddlAEPre.DataBind();

            ddlINSIDEPre.DataSource = allItems;
            ddlINSIDEPre.DataBind();


            ddlAEPre.Text = "95";
            ddlINSIDEPre.Text = "5";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                setBaiFenBi();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");

                ddlAE.DataSource = user;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";

                ddlINSIDE.DataSource = user;
                ddlINSIDE.DataBind();
                ddlINSIDE.DataTextField = "LoginName";
                ddlINSIDE.DataValueField = "Id";

                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                ddlGuestTypeList.DataSource = dal.GetListArray("");
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";


                GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();
                ddlGuestProList.DataSource = guestProBaseInfodal.GetListArray("");
                ddlGuestProList.DataBind();
                ddlGuestProList.DataTextField = "GuestProString";
                ddlGuestProList.DataValueField = "GuestPro";


                

                if (Request["type"] != null)
                {
                    ddlAE.Enabled = true;
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

                         
                            ddlGuestTypeList.BackColor = System.Drawing.Color.Red;
                       

                        txtTime.Text = DateTime.Now.ToString();
                        int month = DateTime.Now.Month;
                      
                        int year = DateTime.Now.Year;
                        txtYear.Text = year.ToString();

                        if (1 <= month && month <= 3)
                        {
                            txtMouth.Text = "1";

                        }
                        else if (4 <= month && month <= 6)
                        {
                            txtMouth.Text = "2";
                        }
                        else if (7 <= month && month <= 9)
                        {
                            txtMouth.Text = "3";
                        }
                        else if (10 <= month && month <= 12)
                        {
                            txtMouth.Text = "4";
                        }


                        try
                        {
                            ddlAE.SelectedValue = use.Id.ToString();
                            ddlINSIDE.SelectedValue = use.Id.ToString();
                        }
                        catch (Exception)
                        {                            
                            
                        }
                        txtGuestTotal.Enabled = false;
                        txtGuestLiRun.Enabled = false;
                        txtGuestDays.Enabled = false;

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
                        ddlGuestTypeList.Enabled = false;
                        txtGuestName.Enabled = false;

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
                        lblCreateUser.Text = use.LoginName;
                        lblCreateTime.Text = DateTime.Now.ToShortDateString().ToString();

                        #endregion
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
                                    {//从获取出的审核中 获取上级信息
                                        //    List<A_Role_User> newList = new List<A_Role_User>();
                                        //    for (int i = 0; i < roleUserList.Count; i++)
                                        //    {
                                        //        if (roleUserList[i].UserId == use.ReportTo)
                                        //        {
                                        //            A_Role_User a = roleUserList[i];
                                        //            newList.Add(a);
                                        //            break;
                                        //        }
                                        //    }

                                        //    if (newList.Count > 0)
                                        //    {
                                        //        ddlPers.DataSource = newList;
                                        //    }
                                        //    else
                                        //    {
                                        ddlPers.DataSource = roleUserList;
                                        // }
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
                                        setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                    }
                                    else
                                    {
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                        ViewState["ids"] = ids;
                                        if (roleUserList != null)
                                        {

                                            ////从获取出的审核中 获取上级信息
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
                                            //    ddlPers.DataSource = newList;}
                                            // else
                                            //{
                                            ddlPers.DataSource = roleUserList;
                                            //}
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

                //if (base.Request["Id"] != null)
                //{
                //    this.btnAdd.Visible = false;                     

                //    ShowInfo(Convert.ToInt32(base.Request["Id"]));

                  
                //}
                //else
                //{
                //    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                //    try
                //    {
                //        ddlAE.SelectedValue = CreateUser.ToString();
                //    }
                //    catch (Exception)
                //    {


                //    }
                //    this.btnUpdate.Visible = false;
                //    btnCopy.Visible = false;
                //}
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Visible = false;
            btnCopy.Visible = false;
            btnAdd.Visible = true;

            txtGuestId.Text = "";
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
                    string GuestName = this.txtGuestName.Text.Trim();
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string GuestId = this.txtGuestId.Text;
                    string GuestAddress = this.txtGuestAddress.Text;
                    string GuestHttp = this.txtGuestHttp.Text;
                    string GuestShui = this.txtGuestShui.Text.Trim();
                    string GuestGong = this.txtGuestGong.Text.Trim();
                    string GuestBrandNo = this.txtGuestBrandNo.Text.Trim();
                    string GuestBrandName = this.txtGuestBrandName.Text.Trim();
                    int AE = int.Parse(ddlAE.SelectedItem.Value);
                    int INSIDE = int.Parse(this.ddlINSIDE.SelectedItem.Value);
                    string Remark = this.txtRemark.Text;




                    int CreateUser = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", lblCreateUser.Text)));
                    DateTime CreateTime = DateTime.Now;

                    VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                    model.Time = Time;
                    model.GuestName = GuestName;
                    model.Phone = Phone;
                    model.LikeMan = LikeMan;
                    model.Job = Job;
                    model.FoxOrEmail = FoxOrEmail;
                    model.IfSave = IfSave;
                    model.QQMsn = QQMsn;
                   
                    model.CreateUser = CreateUser;
                    model.CreateTime = CreateTime;


                    model.GuestId = GuestId;
                    model.GuestAddress = GuestAddress;
                    model.GuestHttp = GuestHttp;
                    model.GuestShui = GuestShui;
                    model.GuestGong = GuestGong;
                    model.GuestBrandNo = GuestBrandNo;
                    model.GuestBrandName = GuestBrandName;
                    model.AE = AE;
                    model.INSIDE = INSIDE;
                    model.Remark = Remark;

                    if (txtGuestTotal.Text.Trim() != "")
                        model.GuestTotal = decimal.Parse(this.txtGuestTotal.Text.Trim());

                    if (txtGuestLiRun.Text.Trim() != "")
                        model.GuestLiRun = decimal.Parse(this.txtGuestLiRun.Text);

                    if (txtGuestDays.Text.Trim() != "")
                        model.GuestDays = decimal.Parse(this.txtGuestDays.Text.Trim());
                    string INSIDERemark = this.txtINSIDERemark.Text;
                    model.INSIDERemark = INSIDERemark;



                    model.YearNo = txtYear.Text;
                    model.QuartNo = txtMouth.Text;
                    model.SimpGuestName = txtSimpGuestName.Text;

                    if (ddlINSIDEPre.Text != "")
                    {
                        model.INSIDEPer = Convert.ToDecimal(ddlINSIDEPre.Text);
                    }



                    if (ddlAEPre.Text != "")
                    {
                        model.AEPer = Convert.ToDecimal(ddlAEPre.Text);
                    }



                    model.IsSpecial = cbIsSpecial.Checked;
                    model.MyGuestType = ddlGuestTypeList.SelectedItem.Text;
                    model.MyGuestPro = Convert.ToInt32(ddlGuestProList.SelectedItem.Value);
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

                        TB_GuestTrackService OverTimeSer = new TB_GuestTrackService();
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

                            }
                        }
                        TB_GuestTrackService OverTimeSer = new TB_GuestTrackService();
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


    }
}
