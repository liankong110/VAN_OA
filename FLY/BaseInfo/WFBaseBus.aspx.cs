using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFBaseBus : System.Web.UI.Page
    {
        private Base_BusInfoService busInfoService = new Base_BusInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Base_BusInfo where BusNo='{0}'",
                txtBusNo.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公交卡号[{0}]，已经存在！');</script>", txtBusNo.Text));
                        return;
                    }
                    Base_BusInfo model = getModel();
                    if (this.busInfoService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFBaseBusList.aspx");
        }

        private void Clear()
        {
            txtBusNo.Text = "";
            txtBusNo.Focus();
        }


        public Base_BusInfo getModel()
        {
            VAN_OA.Model.BaseInfo.Base_BusInfo model = new VAN_OA.Model.BaseInfo.Base_BusInfo();
            model.BusNo = txtBusNo.Text;
            model.IsStop = ddlIsStop.Text == "0" ? false : true;
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
                    string sqlCheck = string.Format("select count(*) from Base_BusInfo where BusNo='{0}' and id<>{1}",
                  txtBusNo.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公交卡号[{0}]，已经存在！');</script>", txtBusNo.Text));
                        return;
                    }
                    Base_BusInfo model = getModel();
                    if (this.busInfoService.Update(model))
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
            if (this.txtBusNo.Text.Trim().Length == 0)
            {
                strErr += "公交卡号不能为空！\\n";
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
                    Base_BusInfo model = this.busInfoService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtBusNo.Text = model.BusNo;
                    this.ddlIsStop.Text = model.IsStop ? "1" : "0";
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
