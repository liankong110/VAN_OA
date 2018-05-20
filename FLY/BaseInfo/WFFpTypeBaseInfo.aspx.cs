using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFFpTypeBaseInfo : System.Web.UI.Page
    {
        private FpTypeBaseInfoService goodSer = new FpTypeBaseInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from FpTypeBaseInfo where FpType='{0}'",
                txtFpType.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票类型名称[{0}]，已经存在！');</script>", txtFpType.Text));
                        return;
                    }
                    FpTypeBaseInfo model = getModel();
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
            base.Response.Redirect("~/BaseInfo/FpTypeBaseInfoList.aspx");
        }

        private void Clear()
        {
            txtFpType.Text = "";
            txtTax.Text = "";
            txtFpType.Focus();
        }


        public FpTypeBaseInfo getModel()
        {
            VAN_OA.Model.BaseInfo.FpTypeBaseInfo model = new VAN_OA.Model.BaseInfo.FpTypeBaseInfo();
            model.FpType = txtFpType.Text;
            model.Tax = Convert.ToDecimal(txtTax.Text);
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            model.FpLength = Convert.ToInt32(txtFpLength.Text);
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from FpTypeBaseInfo where FpType='{0}' and id<>{1}",
                  txtFpType.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('发票类型名称[{0}]，已经存在！');</script>", txtFpType.Text));
                        return;
                    }
                    FpTypeBaseInfo model = getModel();
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
            if (this.txtFpType.Text.Trim().Length == 0)
            {
                strErr += "发票类型不能为空！\\n";
            }

            try
            {
                Convert.ToDecimal(txtTax.Text);
            }
            catch (Exception)
            {
                strErr += "税率格式错误！\\n";
            }

            try
            {
                Convert.ToDecimal(txtFpLength.Text);
            }
            catch (Exception)
            {
                strErr += "发票长度格式错误！\\n";
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
                    FpTypeBaseInfo model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtFpType.Text = model.FpType;
                    this.txtTax.Text = model.Tax.ToString();
                    txtFpLength.Text = model.FpLength.ToString();
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
