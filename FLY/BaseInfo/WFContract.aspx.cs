using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC; 

namespace VAN_OA.BaseInfo
{
    public partial class WFContract : System.Web.UI.Page
    {
        private ContractService contractSer = new ContractService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Contract where Contract_No='{0}'",txtContract_No.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('合同编号[{0}]，已经存在！');</script>",txtContract_No.Text));
                        return;
                    }
                    Contract model = getModel();
                    if (this.contractSer.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFContractList.aspx");
        }

        private void Clear()
        {
            this.txtContract_No.Text = "";
            this.txtContract_Unit.Text = "";
            this.txtContract_Name.Text = "";
            this.txtContract_Summary.Text = "";
            this.txtContract_Total.Text = "";
            this.txtContract_Date.Text = "";
            this.txtContract_PageCount.Text = "";
            txtContract_AllCount.Text = "2";
            this.txtContract_BCount.Text = "1";
            this.txtPONo.Text = "";
            this.txtAE.Text = "";
            this.ddlContract_Brokerage.Text = "";           
            rabContract_IsSignA.Checked = true;
            this.txtContract_Remark.Text = "";
           
            
        }


        public Contract getModel()
        {
            int Contract_Type=int.Parse(this.ddlContract_Type.Text);
			string Contract_Use=this.ddlContract_Use.Text.Trim();
			string Contract_No=this.txtContract_No.Text.Trim();
			string Contract_Unit=this.txtContract_Unit.Text.Trim();
			string Contract_Name=this.txtContract_Name.Text.Trim();
			string Contract_Summary=this.txtContract_Summary.Text.Trim();
			decimal Contract_Total=decimal.Parse(this.txtContract_Total.Text);
			DateTime Contract_Date=DateTime.Parse(this.txtContract_Date.Text);
			int Contract_PageCount=int.Parse(this.txtContract_PageCount.Text);
			int Contract_BCount=int.Parse(this.txtContract_BCount.Text);
			string PONo=this.txtPONo.Text.Trim();
			string AE=this.txtAE.Text.Trim();
			string Contract_Brokerage=this.ddlContract_Brokerage.Text.Trim();
            bool Contract_IsSign = rabContract_IsSignA.Checked ? false : true;
			string Contract_Local=this.ddlContract_Local.Text;
			int Contract_Year=int.Parse(this.ddlContract_Year.Text);
			int Contract_Month=int.Parse(this.ddlContract_Month.Text);
			string Contract_Remark=this.txtContract_Remark.Text;

            Contract model = new Contract();
			model.Contract_Type=Contract_Type;
			model.Contract_Use=Contract_Use;
			model.Contract_No=Contract_No;
			model.Contract_Unit=Contract_Unit;
			model.Contract_Name=Contract_Name;
			model.Contract_Summary=Contract_Summary;
			model.Contract_Total=Contract_Total;
			model.Contract_Date=Contract_Date;
			model.Contract_PageCount=Contract_PageCount;
			model.Contract_BCount=Contract_BCount;
			model.PONo=PONo;
			model.AE=AE;
			model.Contract_Brokerage=Contract_Brokerage;
			model.Contract_IsSign=Contract_IsSign;
			model.Contract_Local=Contract_Local;
			model.Contract_Year=Contract_Year;
			model.Contract_Month=Contract_Month;
			model.Contract_Remark=Contract_Remark;
            model.Contract_AllCount =Convert.ToInt32( txtContract_AllCount.Text);
            model.Contract_IsRequire = cbContract_IsRequire.Checked;
           
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
                    string sqlCheck = string.Format("select count(*) from Contract where Contract_No='{0}' AND ID<>{1}",
              txtContract_No.Text, Request["Id"]);                   
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('合同编号[{0}]，已经存在！');</script>",
                           txtContract_No.Text));
                        return;
                    }
                    Contract model = getModel();
                    if (this.contractSer.Update(model,Contract_OldType.Value))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                        base.Response.Redirect("~/BaseInfo/WFContractList.aspx?Contract_No="+ txtContract_No.Text);
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

          
            if (this.txtContract_No.Text.Trim().Length == 0)
            {
                strErr += "合同编号不能为空！\\n";
            }
            if (this.txtContract_Unit.Text.Trim().Length == 0)
            {
                strErr += "合约单位不能为空！\\n";
            }
            if (this.txtContract_Name.Text.Trim().Length == 0)
            {
                strErr += "合同名称不能为空！\\n";
            }
            if (this.txtContract_Summary.Text.Trim().Length == 0)
            {
                strErr += "合同摘要不能为空！\\n";
            }
            if (string.IsNullOrEmpty(txtContract_Total.Text))
            {
                strErr += "总金额 （小数点2位）格式错误！\\n";
            }
            if (string.IsNullOrEmpty(txtContract_Date.Text))
            {
                strErr += "签订日期时间格式错误！\\n";
            }
            if (string.IsNullOrEmpty(txtContract_PageCount.Text))
            {
                strErr += "合同总页数格式错误！\\n";
            }
            if (string.IsNullOrEmpty(txtContract_AllCount.Text))
            {
                strErr += "合同总份数格式错误！\\n";
            }
            if (string.IsNullOrEmpty(txtContract_BCount.Text))
            {
                strErr += "己方份数（缺省1）格式错误！\\n";
            }
            if (cbContract_IsRequire.Checked == false)
            {
                if (this.txtPONo.Text.Trim().Length == 0)
                {
                    strErr += "项目编号不能为空！\\n";
                }
                if (this.txtAE.Text.Trim().Length == 0)
                {
                    strErr += "AE不能为空！\\n";
                }
            }
            if (this.ddlContract_Brokerage.Text.Trim().Length == 0)
            {
                strErr += "经手人不能为空！\\n";
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
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();             
                user = userSer.getAllUserByLoginName("");              
                ddlContract_Brokerage.DataSource = user;
                ddlContract_Brokerage.DataBind();
                ddlContract_Brokerage.DataTextField = "LoginName";
                ddlContract_Brokerage.DataValueField = "LoginName";
                
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    Contract model = this.contractSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    
                    this.ddlContract_Type.Text = model.Contract_Type.ToString();
                    this.ddlContract_Use.Text = model.Contract_Use;
                    this.txtContract_No.Text = model.Contract_No;
                    this.txtContract_Unit.Text = model.Contract_Unit;
                    this.txtContract_Name.Text = model.Contract_Name;
                    this.txtContract_Summary.Text = model.Contract_Summary;
                    this.txtContract_Total.Text = model.Contract_Total.ToString();
                    this.txtContract_Date.Text = model.Contract_Date.ToString();
                    this.txtContract_PageCount.Text = model.Contract_PageCount.ToString();
                    this.txtContract_BCount.Text = model.Contract_BCount.ToString();
                    this.txtPONo.Text = model.PONo;
                    this.txtAE.Text = model.AE;
                    this.ddlContract_Brokerage.Text = model.Contract_Brokerage;
                    if (model.Contract_IsSign)
                    {
                        rabContract_IsSignB.Checked = true;
                    } 
                    this.ddlContract_Local.Text = model.Contract_Local;
                    this.ddlContract_Year.Text = model.Contract_Year.ToString();
                    this.ddlContract_Month.Text = model.Contract_Month.ToString();
                    this.txtContract_Remark.Text = model.Contract_Remark;
                    txtContract_AllCount.Text = model.Contract_AllCount.ToString();
                    lblContract_ProNo.Text = model.Contract_ProNo;
                    cbContract_IsRequire.Checked = model.Contract_IsRequire;
                    if (model.Contract_IsRequire)
                    {
                        lbtnSelectPONo.Visible = false;
                    }
                    Contract_OldType.Value = model.Contract_Type.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }

        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetSimpListArray(string.Format("and pono='{0}'",Session["Comm_CGPONo"]))[0];
                txtAE.Text = model.AE;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
                if (ddlContract_Type.Text == "2")
                {
                    var list = contractSer.GetListArray(string.Format(" PoNo='{0}' AND Contract_Type=2", txtPONo.Text));
                    if (list.Count > 0)
                    {
                        var modelPetition = list[0];
                        lblError.Text = string.Format("该项目已有合同档案，合同编号：{0}，合同名称:{1}, 签订日期：{2}, 存放位置：{3}",
                           modelPetition.Contract_No, modelPetition.Contract_Name, modelPetition.Contract_Date.ToString("yyyy-MM-dd"), modelPetition.Contract_Local_String);
                        btnAdd.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        btnUpdate.Enabled = true;
                        lblError.Text = "";
                    }
                }
            }
        }

        protected void cbContract_IsRequire_CheckedChanged(object sender, EventArgs e)
        {
            if (cbContract_IsRequire.Checked)
            {
                lbtnSelectPONo.Visible = false;
                txtAE.Text = "";
                txtPONo.Text = "";

                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                lblError.Text = "";
            }
            else
            {
                lbtnSelectPONo.Visible = true;
            }
        }
    }
}
