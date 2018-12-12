using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.Fin;

namespace VAN_OA.Fin
{
    public partial class WFAEPromiseTotal : System.Web.UI.Page
    {
        private AEPromiseTotalService aEPromiseTotalService = new AEPromiseTotalService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    //    string sqlCheck = string.Format("select count(*) from Base_BusInfo where BusNo='{0}'",
                    //txtBusNo.Text);
                    //    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    //    {
                    //        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公交卡号[{0}]，已经存在！');</script>", txtBusNo.Text));
                    //        return;
                    //    }
                    AEPromiseTotal model = getModel();
                    if (this.aEPromiseTotalService.Add(model) > 0)
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
            base.Response.Redirect("~/Fin/WFAEPromiseTotalList.aspx");
        }

        private void Clear()
        {           
         
            txtAddGuestProfit.Text = "";
            txtAddGuetSellTotal.Text = "";
            txtPromiseProfit.Text = "";
            txtPromiseSellTotal.Text = "";
        }


        public AEPromiseTotal getModel()
        {
            AEPromiseTotal model = new AEPromiseTotal();

            model.AE = ddlUser.Text;
            model.YearNo =Convert.ToInt32( ddlYear.Text);
            model.PromiseSellTotal = Convert.ToDecimal(txtPromiseSellTotal.Text);
            model.PromiseProfit = Convert.ToDecimal(txtPromiseProfit.Text);
            model.AddGuestProfit = Convert.ToDecimal(txtAddGuestProfit.Text);
            model.AddGuetSellTotal = Convert.ToDecimal(txtAddGuetSellTotal.Text);

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
                  //  string sqlCheck = string.Format("select count(*) from Base_BusInfo where BusNo='{0}' and id<>{1}",
                  //txtBusNo.Text, Request["Id"]);
                  //  if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                  //  {

                  //      base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公交卡号[{0}]，已经存在！');</script>", txtBusNo.Text));
                  //      return;
                  //  }
                    AEPromiseTotal model = getModel();
                    if (this.aEPromiseTotalService.Update(model))
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
            
            
            if (this.txtPromiseSellTotal.Text.Trim().Length == 0|| CommHelp.VerifesToNum(txtPromiseSellTotal.Text) == false)
            {
                strErr = "承诺销售额指标 格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtPromiseSellTotal.Focus();
                return false;
            }
          

            if (this.txtPromiseProfit.Text.Trim().Length == 0 || CommHelp.VerifesToNum(txtPromiseProfit.Text) == false)
            {
                strErr = "承诺利润 格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtPromiseProfit.Focus();
                return false;
            }
            if (this.txtAddGuetSellTotal.Text.Trim().Length == 0 || CommHelp.VerifesToNum(txtAddGuetSellTotal.Text) == false)
            {
                strErr = "新客户承诺销售额 格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAddGuetSellTotal.Focus();
                return false;
            }
            if (this.txtAddGuestProfit.Text.Trim().Length == 0 || CommHelp.VerifesToNum(txtAddGuestProfit.Text) == false)
            {
                strErr = "新客户承诺利润 格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtAddGuestProfit.Focus();
                return false;
            }         
             
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year).ToString();

                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByPOList("");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";

                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    AEPromiseTotal model = this.aEPromiseTotalService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    ddlUser.Text = model.AE;
                    ddlYear.Text = model.YearNo.ToString();
                    txtAddGuestProfit.Text = model.AddGuestProfit.ToString();
                    txtAddGuetSellTotal.Text = model.AddGuetSellTotal.ToString();
                    txtPromiseProfit.Text = model.PromiseProfit.ToString();
                    txtPromiseSellTotal.Text = model.PromiseSellTotal.ToString();

                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
