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
    public partial class INSIDEGuestTrack : System.Web.UI.Page
    {
        private TB_GuestTrackService guestTrackSer = new TB_GuestTrackService();
      

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
                    
                    string INSIDERemark = this.txtINSIDERemark.Text;

                    int CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    DateTime CreateTime = DateTime.Now;

                    VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();

                    if (txtGuestTotal.Text.Trim()!="")
                        model.GuestTotal = decimal.Parse(this.txtGuestTotal.Text.Trim());

                    if (txtGuestLiRun.Text.Trim() != "")
                        model.GuestLiRun = decimal.Parse(this.txtGuestLiRun.Text);

                    if (txtGuestDays.Text.Trim() != "")
                        model.GuestDays = decimal.Parse(this.txtGuestDays.Text.Trim());
                   
                    model.INSIDERemark = INSIDERemark;




                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    if (this.guestTrackSer.UpdateINSIDE(model))
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

            lblGuestName.Text = model.GuestName;
            lblAE.Text = model.AEName;

            if (model.AEPer != null)
            {
                lblAE.Text += "---"+ model.AEPer.ToString() + "%";
            }
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
            txtINSIDERemark.Text = model.INSIDERemark;
            lblProNo.Text = model.ProNo;

            lblContrPer.Text = model.LikeMan;
            lblEMail.Text = model.FoxOrEmail;
            lblQQ.Text = model.QQMsn;
            lblTel.Text = model.Phone;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                

                if (base.Request["Id"] != null)
                {
                                     

                    ShowInfo(Convert.ToInt32(base.Request["Id"]));


                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }


    }
}
