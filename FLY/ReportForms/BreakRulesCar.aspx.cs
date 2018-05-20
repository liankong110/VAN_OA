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
    public partial class breakRulesCar : BasePage
    {

        private void UpdateCarInfo()
        {
            TB_CarInfo model = new TB_CarInfo();
            model.CarNo = ddlCarNo.Text;
            if (txtBaoxian.Text != "")
            {
                model.Baoxian = Convert.ToDateTime(txtBaoxian.Text);
            }

            if (txtNianJian.Text != "")
            {
                model.NianJian = Convert.ToDateTime(txtNianJian.Text);
            }

            TB_CarInfoService carSer = new TB_CarInfoService();
            carSer.UpdateDate(model);
        }
        private TB_BreakRulesCarService breakRulesCarSer = new TB_BreakRulesCarService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string JiGuan = this.txtJiGuan.Text;
                    DateTime BreakTime = DateTime.Parse(this.txtBreakTime.Text);
                    string Address = this.txtAddress.Text;
                    string Dothing = this.txtDothing.Text;
                    decimal Total = decimal.Parse(this.txtTotal.Text);
                    string State = this.ddlState.Text;
                    string CarNo = this.ddlCarNo.Text;
                    string Remark = this.txtRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BreakRulesCar model = new VAN_OA.Model.ReportForms.TB_BreakRulesCar();
                    model.JiGuan = JiGuan;
                    model.BreakTime = BreakTime;
                    model.Address = Address;
                    model.Dothing = Dothing;
                    model.Total = Total;
                    model.State = State;
                    model.CarNo = CarNo;
                    model.Remark = Remark;
                    model.Driver = txtDriver.Text;
                    model.Score = Convert.ToDecimal(txtScore.Text);
                    if (this.breakRulesCarSer.Add(model) > 0)
                    {
                        UpdateCarInfo();
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        txtTotal.Text = "";

                        txtRemark.Text = "";
                        txtJiGuan.Text = "";
                        txtDothing.Text = "";

                        ddlCarNo.Text = "";
                        txtBreakTime.Text = "";
                        txtAddress.Text = "";
                        txtDriver.Text = "";
                        this.txtJiGuan.Focus();
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

                    string JiGuan = this.txtJiGuan.Text;
                    DateTime BreakTime = DateTime.Parse(this.txtBreakTime.Text);
                    string Address = this.txtAddress.Text;
                    string Dothing = this.txtDothing.Text;
                    decimal Total = decimal.Parse(this.txtTotal.Text);
                    string State = this.ddlState.Text;
                    string CarNo = this.ddlCarNo.Text;
                    string Remark = this.txtRemark.Text;

                    VAN_OA.Model.ReportForms.TB_BreakRulesCar model = new VAN_OA.Model.ReportForms.TB_BreakRulesCar();
                    model.JiGuan = JiGuan;
                    model.BreakTime = BreakTime;
                    model.Address = Address;
                    model.Dothing = Dothing;
                    model.Total = Total;
                    model.State = State;
                    model.CarNo = CarNo;
                    model.Remark = Remark;
                    model.Driver = txtDriver.Text;
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    model.Score = Convert.ToDecimal(txtScore.Text);
                    if (this.breakRulesCarSer.Update(model))
                    {
                        UpdateCarInfo();
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
            if (this.txtJiGuan.Text.Trim().Length == 0)
            {
                strErr += "发现机关不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            
            try
            {
                Convert.ToDateTime(txtBreakTime.Text);
            }
            catch (Exception)
            {
                strErr += "违章时间格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;

            }
            if (this.txtAddress.Text.Trim().Length == 0)
            {
                strErr += "违章地点不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            if (this.txtDothing.Text.Trim().Length == 0)
            {
                strErr += "违章行为不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            try
            {
                Convert.ToDecimal(txtTotal.Text);
            }
            catch (Exception)
            {

                strErr += "罚款金额格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }

            if (this.ddlState.Text.Trim().Length == 0)
            {
                strErr += "处理标志不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            if (this.ddlCarNo.Text.Trim().Length == 0)
            {
                strErr += "车牌号不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            try
            {
                if (txtNianJian.Text != "")
                    Convert.ToDateTime(txtNianJian.Text);
            }
            catch (Exception)
            {

                strErr += "年检时间格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }

            try
            {
                if (txtBaoxian.Text != "")
                    Convert.ToDateTime(txtBaoxian.Text);
            }
            catch (Exception)
            {

                strErr += "保险时间格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            if (txtScore.Text == "")
            {
                strErr += "扣分不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                return false;
            }
            else
            {
                if (CommHelp.VerifesToNum(txtScore.Text) == false)
                {
                    strErr += "扣分格式有问题！\\n";
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}！');</script>", strErr));

                    return false;
                }
            }

           
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_CarInfoService carInfoSer = new TB_CarInfoService();
                System.Collections.Generic.List<TB_CarInfo> carInfos = carInfoSer.GetListArray("");
                carInfos.Insert(0, new TB_CarInfo());
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.DataTextField = "CarNo";
                ddlCarNo.DataValueField = "CarNo";
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    TB_BreakRulesCar model = this.breakRulesCarSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtBreakTime.Text = model.BreakTime.ToShortDateString();
                    txtAddress.Text = model.Address;
                    txtDothing.Text = model.Dothing;
                    txtRemark.Text = model.Remark;
                    ddlState.Text = model.State;
                    txtTotal.Text = model.Total.ToString();
                    txtDriver.Text = model.Driver;
                    txtJiGuan.Text = model.JiGuan;
                    ddlCarNo.Text = model.CarNo;
                    txtScore.Text = model.Score.ToString();
                    if (model.CarNo != "")
                    {

                        TB_CarInfoService carSer = new TB_CarInfoService();

                        List<TB_CarInfo> car = carSer.GetListArray(string.Format(" 1=1 and CarNo='{0}'", model.CarNo));
                        if (car.Count > 0)
                        {
                            txtNianJian.Text = "";
                            txtBaoxian.Text = "";
                            if (car[0].NianJian != null)
                                txtNianJian.Text = car[0].NianJian.Value.ToString();

                            if (car[0].Baoxian != null)
                                txtBaoxian.Text = car[0].Baoxian.Value.ToString();
                        }

                    }
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

        protected void ddlCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCarNo.Text != "")
            {
                TB_CarInfoService carSer = new TB_CarInfoService();

                List<TB_CarInfo> car = carSer.GetListArray(string.Format(" 1=1 and CarNo='{0}'", ddlCarNo.Text));
                if (car.Count > 0)
                {
                    txtNianJian.Text = "";
                    txtBaoxian.Text = "";
                    if (car[0].NianJian != null)
                        txtNianJian.Text = car[0].NianJian.Value.ToString();

                    if (car[0].Baoxian != null)
                        txtBaoxian.Text = car[0].Baoxian.Value.ToString();
                }
            }
            else
            {
                txtNianJian.Text = "";
                txtBaoxian.Text = "";
            }
        }


    }
}
