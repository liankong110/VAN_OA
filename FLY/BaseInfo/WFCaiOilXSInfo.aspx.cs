using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFCaiOilXSInfo : System.Web.UI.Page
    {
        private CaiOilXSInfoService caiOilXSInfoService = new CaiOilXSInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from CaiOilXSInfo where CarNo='{0}'",
                txtCarNo.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，已经存在！');</script>", txtCarNo.Text));
                        return;
                    }
                    CaiOilXSInfo model = getModel();
                    if (this.caiOilXSInfoService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/CaiOilXSInfoList.aspx");
        }

        private void Clear()
        {
            txtCarNo.Text = "";
            txtOilXs.Text = "";
            txtCarNo.Focus();
        }


        public CaiOilXSInfo getModel()
        {
            VAN_OA.Model.BaseInfo.CaiOilXSInfo model = new VAN_OA.Model.BaseInfo.CaiOilXSInfo();
            model.CarNo = txtCarNo.Text;
            model.OilXs = Convert.ToDecimal(txtOilXs.Text);
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from CaiOilXSInfo where CarNo='{0}' and id<>{1}",
                  txtCarNo.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('车牌号[{0}]，已经存在！');</script>", txtCarNo.Text));
                        return;
                    }
                    CaiOilXSInfo model = getModel();
                    if (this.caiOilXSInfoService.Update(model))
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

            try
            {
                Convert.ToDecimal(txtOilXs.Text);
            }
            catch (Exception)
            {
                strErr += "系数格式错误！\\n";
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
                    CaiOilXSInfo model = this.caiOilXSInfoService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtCarNo.Text = model.CarNo;
                    this.txtOilXs.Text = model.OilXs.ToString();
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
