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
    public partial class WFHouseInfo : System.Web.UI.Page
    {


        private TB_HouseInfoService goodSer = new TB_HouseInfoService();


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_HouseInfo where houseName='{0}' ",
                    txtGoodName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('仓库名称[{0}]，已经存在！');</script>", txtGoodName.Text ));
                        return;
                    }
                    TB_HouseInfo model = getModel();
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
            base.Response.Redirect("~/BaseInfo/WFHouserInfoList.aspx");
        }

        private void Clear()
        {

            txtGoodNo.Text = "";
            txtRemark.Text = "";
            txtGoodName.Text = "";           
            txtGoodName.Focus();
        }


        public TB_HouseInfo getModel()
        {
            
            VAN_OA.Model.BaseInfo.TB_HouseInfo model = new VAN_OA.Model.BaseInfo.TB_HouseInfo();
            model.CreateUserId = Convert.ToInt32(Session["currentUserId"]);
           
            model.houseName = txtGoodName.Text;
          
            model.houseRemark =txtRemark.Text;
            model.IfDefault = cbDefault.Checked;
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

                    string sqlCheck = string.Format("select count(*) from TB_HouseInfo where houseName='{0}' and id<>{1}",
                  txtGoodName.Text, Request["Id"]);

                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('仓库名称[{0}]，已经存在！');</script>", txtGoodName.Text));
                        return;
                    }
                    TB_HouseInfo model = getModel();
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
            if (this.txtGoodName.Text.Trim().Length == 0)
            {
                strErr = "仓库名称不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtGoodName.Focus();
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
                    TB_HouseInfo model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));

                    txtGoodName.Text = model.houseName;
                    txtGoodNo.Text = model.houseNo;
                    txtRemark.Text = model.houseRemark;
                    cbDefault.Checked = model.IfDefault;
                }
                else
                {

                    
                    this.btnUpdate.Visible = false;
                }

              
            }
        }

        
    }
}
