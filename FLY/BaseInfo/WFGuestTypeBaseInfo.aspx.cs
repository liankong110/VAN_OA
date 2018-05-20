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
    public partial class WFGuestTypeBaseInfo : System.Web.UI.Page
    {
        private GuestTypeBaseInfoService goodSer = new GuestTypeBaseInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from GuestTypeBaseInfo where GuestType='{0}'",
                txtGuestType.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('客户类型名称[{0}]，已经存在！');</script>", txtGuestType.Text));
                        return;
                    }
                    GuestTypeBaseInfo model = getModel();
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
            base.Response.Redirect("~/BaseInfo/GuestTypeBaseInfoList.aspx");
        }

        private void Clear()
        {            
            txtGuestType.Text = "";
            txtPayXiShu.Text = "";
            txtGuestType.Focus();
        }


        public GuestTypeBaseInfo getModel()
        {
            VAN_OA.Model.BaseInfo.GuestTypeBaseInfo model = new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo();
            model.GuestType = txtGuestType.Text;
            model.PayXiShu = Convert.ToDecimal(txtPayXiShu.Text);
            model.XiShu = Convert.ToInt32(ddlXiShu.SelectedItem.Value);
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
                    string sqlCheck = string.Format("select count(*) from GuestTypeBaseInfo where GuestType='{0}' and id<>{1}",
                  txtGuestType.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('客户类型名称[{0}]，已经存在！');</script>", txtGuestType.Text));
                        return;
                    }
                    GuestTypeBaseInfo model = getModel();
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

            if (this.txtGuestType.Text.Trim().Length == 0)
            {
                strErr += "客户类型不能为空！\\n";
            }
            if (CommHelp.VerifesToNum(txtPayXiShu.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('结算系数 格式错误！');</script>");
                return false;
            }
            try
            {
                Convert.ToDecimal(txtPayXiShu.Text);
            }
            catch (Exception)
            {
                strErr += "结算系数格式错误！\\n";
            }
            if (Convert.ToDecimal(txtPayXiShu.Text) > 1 || Convert.ToDecimal(txtPayXiShu.Text) < 0)
            {
                strErr += "结算系数格式在数值0-1之间！\\n";
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
                    GuestTypeBaseInfo model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtGuestType.Text = model.GuestType;
                    this.txtPayXiShu.Text = model.PayXiShu.ToString();
                    ddlXiShu.Text = model.XiShu.ToString();
                   
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
