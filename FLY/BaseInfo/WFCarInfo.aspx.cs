using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;



namespace VAN_OA.BaseInfo
{
    public partial class WFCarInfo : System.Web.UI.Page
    {
        private TB_CarInfoService caiInfoSer = new TB_CarInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_CarInfo where CarNo='{0}'",
                txtCarNo.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，已经存在！');</script>", txtCarNo.Text));
                        return;
                    }
                    TB_CarInfo model = getModel();
                    if (this.caiInfoSer.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFCarInfoList.aspx");
        }

        private void Clear()
        {
            txtBaoxian.Text = "";
            txtCaiXingShiNo.Text = "";
            txtCarEngine.Text = "";
            txtCarJiaNo.Text = "";
            txtCarModel.Text = "";
            txtCarNo.Text = "";
            txtNianJian.Text = "";
            txtCarShiBieNO.Text = "";
            txtOilNumber.Text = "";
            txtCarNo.Focus();
        }


        public TB_CarInfo getModel()
        {
            TB_CarInfo model = new TB_CarInfo();
            if (!string.IsNullOrEmpty(txtBaoxian.Text))
            {
                model.Baoxian = Convert.ToDateTime(txtBaoxian.Text);
            }
            if (!string.IsNullOrEmpty(txtNianJian.Text))
            {
                model.NianJian = Convert.ToDateTime(txtNianJian.Text);
            }
            model.CaiXingShiNo = txtCaiXingShiNo.Text;
            model.CarEngine = txtCarEngine.Text;
            model.CarJiaNo = txtCarJiaNo.Text;
            model.CarModel = txtCarModel.Text;
            model.CarNo = txtCarNo.Text;
            model.CarShiBieNO = txtCarShiBieNO.Text;
            model.OilNumber =Convert.ToDecimal(txtOilNumber.Text);
            if (Request["Id"] != null)
            {
                model.id = Convert.ToInt32(Request["Id"]);
            }         
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_CarInfo where CarNo='{0}' AND ID<>{1}",
              txtCarNo.Text, Request["Id"]);                   
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，已经存在！');</script>", txtCarNo.Text));
                        return;
                    }
                    TB_CarInfo model = getModel();
                    if (this.caiInfoSer.Update(model))
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

            if (this.txtCarNo.Text.Trim().Length == 0)
            {
                strErr += "车牌号不能为空！\\n";
            }

            if (this.txtCarEngine.Text.Trim().Length == 0)
            {
                strErr += "发动机号不能为空！\\n";
            }
            if (this.txtCarShiBieNO.Text.Trim().Length == 0)
            {
                strErr += "车辆识别代号不能为空！\\n";
            }
            if (this.txtCarJiaNo.Text.Trim().Length == 0)
            {
                strErr += "车架号不能为空！\\n";
            }

            if (this.txtCaiXingShiNo.Text.Trim().Length == 0)
            {
                strErr += "行驶证号不能为空！\\n";
            }

            try
            {
                if (!string.IsNullOrEmpty(txtOilNumber.Text))
                {
                    Convert.ToDecimal(txtOilNumber.Text);
                }
            }
            catch (Exception)
            {
                strErr += "油耗系数格式错误！\\n";
            }
            try
            {
                if (!string.IsNullOrEmpty(txtNianJian.Text))
                {
                    Convert.ToDateTime(txtNianJian.Text);
                }
            }
            catch (Exception)
            {
                strErr += "年检时间格式错误！\\n";
            }

            try
            {
                if (!string.IsNullOrEmpty(txtBaoxian.Text))
                {
                    Convert.ToDateTime(txtBaoxian.Text);
                }
            }
            catch (Exception)
            {
                strErr += "保险时间格式错误！\\n";
            }



            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    TB_CarInfo model = this.caiInfoSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    if (model.Baoxian.HasValue)
                    {
                        txtBaoxian.Text = model.Baoxian.Value.ToString("yyyy-MM-dd");
                    }
                    if (model.NianJian.HasValue)
                    {
                        txtNianJian.Text = model.NianJian.Value.ToString("yyyy-MM-dd");
                    }
                    txtCaiXingShiNo.Text = model.CaiXingShiNo;
                    txtCarEngine.Text = model.CarEngine;
                    txtCarJiaNo.Text = model.CarJiaNo;
                    txtCarModel.Text = model.CarModel;
                    txtCarNo.Text = model.CarNo;
                    txtCarShiBieNO.Text = model.CarShiBieNO;
                    txtOilNumber.Text = model.OilNumber.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
