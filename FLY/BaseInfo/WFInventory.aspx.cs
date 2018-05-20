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
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;


using System.Collections.Generic;
 

namespace VAN_OA.BaseInfo
{
    public partial class WFInventory : System.Web.UI.Page
    {
        private Tb_InventoryService invSer = new Tb_InventoryService();
  

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Tb_Inventory where InvName='{0}'",txtInvName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null,string.Format( "<script>alert('存货[{0}],已经存在！');</script>",txtInvName.Text));
                        return;
                    }
                    Tb_Inventory per = getModel();
                    if (this.invSer.Add(per) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFInventoryList.aspx");
        }

        private void Clear()
        {
            txtInvNo.Text = "";
            txtInvNum.Text = "0";
            txtInvName.Text = "";
            txtInvUnit.Text = "";
            txtUser.Text = "";
            txtInvName.Focus();
        }


        public Tb_Inventory getModel()
        {

            string InvName = this.txtInvName.Text;
            decimal InvNum = decimal.Parse(this.txtInvNum.Text);
            string InvUnit = this.txtInvUnit.Text;
            string InvNo = this.txtInvNo.Text;

            VAN_OA.Model.BaseInfo.Tb_Inventory model = new VAN_OA.Model.BaseInfo.Tb_Inventory();
            model.InvName = InvName;
            model.InvNum = InvNum;
            model.InvUnit = InvUnit;
            model.InvNo = InvNo;
            model.InvUser = txtUser.Text;

            model.GoodNumber = ddlNumber.Text;
            model.GoodCol = ddlCol.Text;
            model.GoodRow = ddlRow.Text;
            model.GoodArea = ddlArea.Text;
            model.GoodAreaNumber = model.GoodArea + model.GoodNumber + "-" + model.GoodRow + "-" + model.GoodCol;

            if (Request["Id"] != null)
            {
                model.ID = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Tb_Inventory where InvName='{0}' and Id<>{1}", txtInvName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('存货[{0}],已经存在！');</script>", txtInvName.Text));
                        return;
                    }

                    Tb_Inventory per = getModel();
                    if (this.invSer.Update(per))
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
            if (this.txtInvName.Text.Trim().Length == 0)
            {
                strErr = "存货名称不能为空！\\n"; 
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtInvName.Focus();
                return false;
            }         

            try
            {
                Convert.ToDecimal(txtInvNum.Text);
            }
            catch (Exception)
            {
                strErr = "数量格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtInvNum.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtInvNum.Text) <= 0)
            {
                strErr += "请填写数量！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtInvNum.Focus();
                return false;
            }

            if (ddlArea.Text == "" || ddlNumber.Text == "" || ddlRow.Text == "" || ddlCol.Text == "")
            {
                strErr = "请选择仓位！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));

                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //加载基本信息
                ddlNumber.Items.Add(new ListItem { Text = "", Value = "" });
                ddlRow.Items.Add(new ListItem { Text = "", Value = "" });
                ddlCol.Items.Add(new ListItem { Text = "", Value = "" });
                //货架号：1.全部  缺省 2….51 1,..50 
                for (int i = 1; i < 51; i++)
                {
                    ddlNumber.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    //层数：1.全部  缺省 2….21 1,2,3…20 
                    //部位：1.全部  缺省 2….21 1,2,3…20
                    if (i <= 21)
                    {
                        ddlRow.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                        ddlCol.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                }
                if (base.Request["Id"] != null)
                {
                    
                     
                    this.btnAdd.Visible = false;
                    Tb_Inventory model = this.invSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    
                  
                    this.txtInvName.Text = model.InvName;
                    this.txtInvNum.Text = model.InvNum.ToString();
                    this.txtInvUnit.Text = model.InvUnit;
                    this.txtInvNo.Text = model.InvNo;
                    txtUser.Text = model.InvUser;

                    ddlArea.Text = model.GoodArea;
                    ddlCol.Text = model.GoodCol;
                    ddlRow.Text = model.GoodRow;
                    ddlNumber.Text = model.GoodNumber;
                }
                else
                {

                    txtInvNum.Text = "0";
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
