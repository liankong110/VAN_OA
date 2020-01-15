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
    public partial class WFGuestProBaseInfo : System.Web.UI.Page
    {
        public string GetGestProInfo(object obj)
        {
            return GuestProBaseInfoList.GetGestProInfo(obj);            
        }

        private GuestProBaseInfoService goodSer = new GuestProBaseInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from GuestProBaseInfo where GuestPro={0}",
                ddlGuestPro.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('客户属性[{0}]，已经存在！');</script>",GetGestProInfo( ddlGuestPro.Text)));
                        return;
                    }
                    GuestProBaseInfo model = getModel();
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
            base.Response.Redirect("~/BaseInfo/GuestProBaseInfoList.aspx");
        }

        private void Clear()
        {   
            txtPayXiShu.Text = "";
            ddlGuestPro.Focus();
        }


        public GuestProBaseInfo getModel()
        {
            VAN_OA.Model.BaseInfo.GuestProBaseInfo model = new VAN_OA.Model.BaseInfo.GuestProBaseInfo();
            model.GuestPro = Convert.ToInt32(ddlGuestPro.Text);
            model.JiLiXiShu = Convert.ToDecimal(txtPayXiShu.Text);
            model.XiShu = Convert.ToInt32(ddlXiShu.SelectedItem.Value);
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            model.GuestMonth = Convert.ToInt32(ddlMonth.Text);
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from GuestProBaseInfo where GuestPro={0} and id<>{1}",
                  ddlGuestPro.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('客户属性[{0}]，已经存在！');</script>", GetGestProInfo(ddlGuestPro.Text)));
                        return;
                    }
                    GuestProBaseInfo model = getModel();
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
            if (txtPayXiShu.Text == "")
            {
                strErr += "激励系数格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }
            if (CommHelp.VerifesToNum(txtPayXiShu.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('激励系数 格式错误！');</script>");
                return false;
            }
            try
            {
                Convert.ToDecimal(txtPayXiShu.Text);
            }
            catch (Exception)
            {
                strErr += "激励系数格式错误！\\n";
            }
            if (Convert.ToDecimal(txtPayXiShu.Text) > 1000 || Convert.ToDecimal(txtPayXiShu.Text) < 0)
            {
                strErr += "激励系数格式在数值0---1000之间！\\n";
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
                    GuestProBaseInfo model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.ddlGuestPro.Text = model.GuestPro.ToString();
                    this.txtPayXiShu.Text = model.JiLiXiShu.ToString();
                    ddlXiShu.Text = model.XiShu.ToString();
                    ddlMonth.Text = model.GuestMonth.ToString();
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
