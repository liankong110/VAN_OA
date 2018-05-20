using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;
using System.Data;


namespace VAN_OA.ReportForms
{
    public partial class WFBusCardRecord : System.Web.UI.Page
    {
        private TB_BusCardRecordService busCardSer = new TB_BusCardRecordService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    string BusCardNo = this.ddlCardNo.Text;                   
                    DateTime BusCardDate = DateTime.Parse(this.txtBusCardDate.Text);
                    decimal BusCardTotal = decimal.Parse(this.txtBusCardTotal.Text);
                    string BusCardRemark = this.txtBusCardRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BusCardRecord model = new VAN_OA.Model.ReportForms.TB_BusCardRecord();
                    model.BusCardNo = BusCardNo;
                    model.BusCardPer = "";
                    model.BusCardDate = BusCardDate;
                    model.BusCardTotal = BusCardTotal;
                    model.BusCardRemark = BusCardRemark;


                    if (this.busCardSer.Add(model) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");    
                        this.txtBusCardDate.Text ="";
                        this.txtBusCardTotal.Text ="";
                        this.txtBusCardRemark.Text ="";
                        this.ddlCardNo.Focus();
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
            base.Response.Redirect(Session["BusCard"].ToString());
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string BusCardNo = this.ddlCardNo.Text;
                    DateTime BusCardDate = DateTime.Parse(this.txtBusCardDate.Text);
                    decimal BusCardTotal = decimal.Parse(this.txtBusCardTotal.Text);
                    string BusCardRemark = this.txtBusCardRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BusCardRecord model = new VAN_OA.Model.ReportForms.TB_BusCardRecord();
                    model.BusCardNo = BusCardNo;
                    model.BusCardPer = "";
                    model.BusCardDate = BusCardDate;
                    model.BusCardTotal = BusCardTotal;
                    model.BusCardRemark = BusCardRemark;                   
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    if (this.busCardSer.Update(model))
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

            if (this.txtBusCardDate.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                this.txtBusCardDate.Focus();
                return false;
            }
            if (CommHelp.VerifesToDateTime(txtBusCardDate.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false;
            }
            if (this.txtBusCardTotal.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填金额！');</script>");
                this.txtBusCardTotal.Focus();
                return false;
            }

            
            try
            {
                Convert.ToDecimal(txtBusCardTotal.Text);
              
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的数字格式有误！');</script>");
                txtBusCardTotal.Focus();
                return false;
            }
           
            return true;
        }

        private void ShowInfo(int Id)
        {

            VAN_OA.Model.ReportForms.TB_BusCardRecord model = busCardSer.GetModel(Id);
           
            this.ddlCardNo.Text = model.BusCardNo;
           
            this.txtBusCardDate.Text =string.Format("{0:yyyy-MM-dd}", model.BusCardDate);
            this.txtBusCardTotal.Text = model.BusCardTotal.ToString();
            this.txtBusCardRemark.Text = model.BusCardRemark;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


                DataTable cardInfo= DBHelp.getDataTable("select BusNo as CardNo from Base_BusInfo where IsStop=0");
                ddlCardNo.DataSource = cardInfo;
                ddlCardNo.DataBind();

                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
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
