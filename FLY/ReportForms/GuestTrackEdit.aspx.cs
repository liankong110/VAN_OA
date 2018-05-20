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

namespace VAN_OA.ReportForms
{
    public partial class GuestTrackEdit : System.Web.UI.Page
    {
        private TB_GuestTrackService guestTrackSer = new TB_GuestTrackService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {                  
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string GuestName = this.txtGuestName.Text;
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string GuestId = this.txtGuestId.Text;
                    string GuestAddress = this.txtGuestAddress.Text;
                    string GuestHttp = this.txtGuestHttp.Text;
                    string GuestShui = this.txtGuestShui.Text;
                    string GuestGong = this.txtGuestGong.Text;
                    string GuestBrandNo = this.txtGuestBrandNo.Text;
                    string GuestBrandName = this.txtGuestBrandName.Text;
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
                    model.SimpGuestName = txtSimpGuestName.Text;
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


                    if (ddlINSIDEPre.Text != "")
                    {
                        model.INSIDEPer = Convert.ToDecimal(ddlINSIDEPre.Text);
                    }



                    if (ddlAEPre.Text != "")
                    {
                        model.AEPer = Convert.ToDecimal(ddlAEPre.Text);
                    }

                    if (this.guestTrackSer.Add(model) > 0)
                    {
                        base.Response.Redirect(Session["POUrl"].ToString());
                        //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        
                        //this.txtTime.Text ="";
                        //this.txtGuestName.Text = "";
                        //this.txtPhone.Text = "";
                        //this.txtLikeMan.Text = "";
                        //this.txtJob.Text = "";
                        //this.txtFoxOrEmail.Text = "";
                        //this.chkIfSave.Checked = true;
                        //this.txtQQMsn.Text = "";


                        ////this.txtFristMeet.Text = "";
                        ////this.txtSecondMeet.Text = "";
                        ////this.txtFaceMeet.Text = "";
                        ////this.txtPrice.Text = "";
                        ////this.chkIfSuccess.Checked = true;
                        ////this.txtMyAppraise.Text = "";
                        ////this.txtManAppraise.Text = "";


                        //this.txtGuestId.Text = "";
                        //this.txtGuestAddress.Text = "";
                        //this.txtGuestHttp.Text  ="";
                        //this.txtGuestShui.Text = "";
                        //this.txtGuestGong.Text = "";
                        //this.txtGuestBrandNo.Text = "";
                        //this.txtGuestBrandName.Text = "";
                        //txtRemark.Text = "";


                        //txtGuestTotal.Text = "";
                        //txtGuestLiRun.Text = "";
                        //txtGuestDays.Text = "";
                        //txtINSIDERemark.Text = "";
                        //txtYear.Text = "";
                        //txtMouth.Text = "";

                        //ddlAEPre.Text = "";
                        //ddlINSIDEPre.Text = "";
                        //model.INSIDERemark = INSIDERemark;
                        //this.txtTime.Focus();
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
            base.Response.Redirect(Session["POUrl"].ToString());
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    string sql = string.Format("select COUNT(*) from TB_GuestTrack where GuestName='{1}' and YearNo='{2}' and QuartNo='{3}' and Id<>{0}", Request["Id"], txtGuestName.Text,
                        txtYear.Text, txtMouth.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('客户名称已经存在！');</script>");
                    }
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string GuestName = this.txtGuestName.Text;
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
                    string GuestShui = this.txtGuestShui.Text;
                    string GuestGong = this.txtGuestGong.Text;
                    string GuestBrandNo = this.txtGuestBrandNo.Text;
                    string GuestBrandName = this.txtGuestBrandName.Text;
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
                    model.SimpGuestName = txtSimpGuestName.Text;
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

                    if (ddlINSIDEPre.Text != "")
                    {
                        model.INSIDEPer = Convert.ToDecimal(ddlINSIDEPre.Text);
                    }

                    if (ddlAEPre.Text != "")
                    {
                        model.AEPer = Convert.ToDecimal(ddlAEPre.Text);
                    }
                    model.Id = Convert.ToInt32(base.Request["Id"]);
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



           
            return true;
        }
        private void ShowInfo(int Id)
        {

            VAN_OA.Model.ReportForms.TB_GuestTrack model = guestTrackSer.GetModel(Id);
            
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
            txtSimpGuestName.Text = model.SimpGuestName;
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



                if (Request["type"] != null)
                {
                    ddlAE.Enabled = true;
                     VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;

                     if (use.LoginName == "王鸣")
                     {
                         txtGuestName.ReadOnly = false;
                     }
                }


                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;                     

                    ShowInfo(Convert.ToInt32(base.Request["Id"]));

                  
                }
                else
                {
                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    try
                    {
                        ddlAE.SelectedValue = CreateUser.ToString();
                    }
                    catch (Exception)
                    {


                    }
                    this.btnUpdate.Visible = false;
                    btnCopy.Visible = false;
                }
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Visible = false;
            btnCopy.Visible = false;
            btnAdd.Visible = true;

            //txtGuestId.Text = "";
            lblProNo.Text = "";
        }


    }
}
