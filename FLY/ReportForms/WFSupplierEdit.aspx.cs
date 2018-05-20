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
    public partial class WFSupplierEdit : System.Web.UI.Page
    {
        private TB_SupplierInfoService supplierSer = new TB_SupplierInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                     
                    
                    
                  
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string SupplierName = this.txtSupplierName.Text;
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text;

                    string SupplierId = this.txtSupplierId.Text;
                    string SupplierAddress = this.txtSupplierAddress.Text;
                    string SupplierHttp = this.txtSupplierHttp.Text;
                    string SupplierShui = this.txtSupplierShui.Text;
                    string SupplierGong = this.txtSupplierGong.Text;
                    string SupplierBrandNo = this.txtSupplierBrandNo.Text;
                    string SupplierBrandName = this.txtSupplierBrandName.Text;                  
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
                    model.SupplieSimpeName = txtSupplierSimpleName.Text;
                    model.ZhuJi = txtZhuJi.Text;
                    if (this.supplierSer.Add(model) > 0)
                    {
                        base.Response.Redirect(Session["POUrl"].ToString());                       
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

                    string sql = string.Format("select COUNT(*) from TB_SupplierInfo where SupplieSimpeName='{1}' and Id<>{0}", Request["Id"], txtSupplierName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商名称已经存在！');</script>");
                    }
                    DateTime Time = DateTime.Parse(this.txtTime.Text);
                    string SupplierName = this.txtSupplierName.Text;
                    string Phone = this.txtPhone.Text;
                    string LikeMan = this.txtLikeMan.Text;
                    string Job = this.txtJob.Text;
                    string FoxOrEmail = this.txtFoxOrEmail.Text;
                    bool IfSave = this.chkIfSave.Checked;
                    string QQMsn = this.txtQQMsn.Text; 
                    string SupplierId = this.txtSupplierId.Text;
                    string SupplierAddress = this.txtSupplierAddress.Text;
                    string SupplierHttp = this.txtSupplierHttp.Text;
                    string SupplierShui = this.txtSupplierShui.Text;
                    string SupplierGong = this.txtSupplierGong.Text;
                    string SupplierBrandNo = this.txtSupplierBrandNo.Text;
                    string SupplierBrandName = this.txtSupplierBrandName.Text;
                   
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
                    model.SupplieSimpeName = txtSupplierSimpleName.Text;
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    model.MainRange = txtMainRange.Text;
                    model.SupplieSimpeName = txtSupplierSimpleName.Text;
                    model.ZhuJi = txtZhuJi.Text;
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
            if (CommHelp.VerifesToDateTime(txtTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
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

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('供应商简称不能为空！！');</script>");
                this.txtSupplierSimpleName.Focus();
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
            txtRemark.Text = model.Remark;             
            txtSupplierSimpleName.Text=model.SupplieSimpeName;
            txtMainRange.Text = model.MainRange;
            txtZhuJi.Text = model.ZhuJi;
            lblProNo.Text = model.ProNo;         
        }

        private void setBaiFenBi()
        {

          
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                setBaiFenBi();
                


                if (Request["type"] != null)
                {
                  
                }


                if (base.Request["Id"] != null)
                {
                    //this.btnAdd.Visible = false;                     

                    ShowInfo(Convert.ToInt32(base.Request["Id"]));

                  
                }
                else
                {
                  
                    this.btnUpdate.Visible = false;
                    //btnCopy.Visible = false;
                }
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Visible = false;
            //btnCopy.Visible = false;
            //btnAdd.Visible = true;

            //txtSupplierId.Text = "";
            lblProNo.Text = "";
        }


    }
}
