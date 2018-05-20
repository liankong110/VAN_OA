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
    public partial class WFPetitions : System.Web.UI.Page
    {
        private PetitionsService petitionsSer = new PetitionsService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    Petitions model = getModel();
                    if (this.petitionsSer.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFPetitionsList.aspx");
        }

        private void Clear()
        {
            txtAE.Text = "";
            txtGuestName.Text = "";
            txtName.Text = "";
            txtPONo.Text = "";
            txtRemark.Text = "";
            txtSalesUnit.Text = "";
            txtSignDate.Text = "";
            txtSumCount.Text = "1";
            txtSummary.Text = "";
            txtSumPages.Text = "";
            txtTotal.Text = "";
            txtBCount.Text = "1";
        }


        public Petitions getModel()
        {
            string Type = this.ddlType.Text;

            string GuestName = this.txtGuestName.Text.Trim();
            string SalesUnit = this.txtSalesUnit.Text.Trim();
            string Summary = this.txtSummary.Text.Trim();
            decimal Total = decimal.Parse(this.txtTotal.Text);
            DateTime SignDate = DateTime.Parse(this.txtSignDate.Text);
            int SumPages = int.Parse(this.txtSumPages.Text);
            int SumCount = int.Parse(this.txtSumCount.Text);
            int BCount = int.Parse(this.txtBCount.Text);
            string PoNo = this.txtPONo.Text.Trim();
            string AE = this.txtAE.Text.Trim();
            string Handler = this.ddlHandler.Text;
            bool IsColse = this.rabIsColseB.Checked;
            string Local = this.ddlLocal.Text;
            int L_Year = int.Parse(this.ddlL_Year.Text);
            int L_Month = int.Parse(this.ddlL_Month.Text);
            string Remark = this.txtRemark.Text;

            Petitions model = new Petitions();
            model.Type = Type;
            model.GuestName = GuestName;
            model.SalesUnit = SalesUnit;
            model.Summary = Summary;
            model.Total = Total;
            model.SignDate = SignDate;
            model.SumPages = SumPages;
            model.SumCount = SumCount;
            model.BCount = BCount;
            model.PoNo = PoNo;
            model.AE = AE;
            model.Handler = Handler;
            model.IsColse = IsColse;
            model.Local = Local;
            model.L_Year = L_Year;
            model.L_Month = L_Month;
            model.Remark = Remark;
            model.Name = txtName.Text.Trim();
            model.IsRequire = cbIsRequire.Checked;

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
                    Petitions model = getModel();
                    if (this.petitionsSer.Update(model, hfYear.Value))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                        base.Response.Redirect("~/BaseInfo/WFPetitionsList.aspx?PONO=" +txtPONo.Text);
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

            
            if (this.txtPONo.Text.Trim().Length == 0)
            {
                strErr += "项目编号不能为空！\\n";
            }
            //if (this.txtGuestName.Text.Trim().Length == 0)
            //{
            //    strErr += "客户名称不能为空！\\n";
            //}
            if (this.txtSalesUnit.Text.Trim().Length == 0)
            {
                strErr += "采购单位不能为空！\\n";
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                strErr += "签呈单名称不能为空！\\n";
            }
            if (this.txtSummary.Text.Trim().Length == 0)
            {
                strErr += "签呈单摘要不能为空！\\n";
            }
            if (CommHelp.VerifesToNum(txtTotal.Text) == false)
            {
                strErr += "总金额格式错误！\\n";
            }
            if (CommHelp.VerifesToDateTime(txtSignDate.Text) == false)
            {
                strErr += "签订起始日期时间格式错误！\\n";
            }
            if (CommHelp.VerifesToNum(txtSumPages.Text) == false)
            {
                strErr += "签呈单总页数格式错误！\\n";
            }

            if (CommHelp.VerifesToNum(txtSumCount.Text) == false)
            {
                strErr += "签呈单总份数格式错误！\\n";
            }
            if (CommHelp.VerifesToNum(txtBCount.Text) == false)
            {
                strErr += "己方份数格式错误！\\n";
            }

            //if (cbIsRequire.Checked == false)
            //{
            //    if (this.txtPONo.Text.Trim().Length == 0)
            //    {
            //        strErr += "项目编号不能为空！\\n";
            //    }
            //    if (this.txtAE.Text.Trim().Length == 0)
            //    {
            //        strErr += "AE不能为空！\\n";
            //    }
            //}

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
                ddlHandler.DataSource = user;
                ddlHandler.DataBind();
                ddlHandler.DataTextField = "LoginName";
                ddlHandler.DataValueField = "LoginName";

                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    Petitions model = this.petitionsSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtName.Text = model.Name;
                    this.ddlType.Text = model.Type;
                    this.lblNumber.Text = model.Number;
                    this.txtGuestName.Text = model.GuestName;
                    this.txtSalesUnit.Text = model.SalesUnit;
                    this.txtSummary.Text = model.Summary;
                    this.txtTotal.Text = model.Total.ToString();
                    this.txtSignDate.Text = model.SignDate.ToString();
                    this.txtSumPages.Text = model.SumPages.ToString();
                    this.txtSumCount.Text = model.SumCount.ToString();
                    this.txtBCount.Text = model.BCount.ToString();
                    this.txtPONo.Text = model.PoNo;
                    this.txtAE.Text = model.AE;
                    this.ddlHandler.Text = model.Handler;
                    this.ddlLocal.Text = model.Local;
                    this.ddlL_Year.Text = model.L_Year.ToString();
                    this.ddlL_Month.Text = model.L_Month.ToString();
                    this.txtRemark.Text = model.Remark;

                    cbIsRequire.Checked = model.IsRequire;
                    if (model.IsRequire)
                    {
                        lbtnSelectPONo.Visible = false;
                    }
                    if (model.IsColse)
                    {
                        rabIsColseB.Checked = true;
                    }
                    hfYear.Value = model.L_Year.ToString();


                    var supplierList = new CAI_POCaiService().GetLastSupplier(string.Format("'{0}'", model.PoNo));
                    txtSalesUnit.Text = string.Join(",", supplierList.Select(t => t.Supplier).ToArray());
                    txtTotal.Text = supplierList.Sum(t => t.Total).ToString();
                }
                else
                {
                    ddlHandler.Text = "王汉中";
                    this.btnUpdate.Visible = false;
                }


            }
        }

        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetSimpListArray(string.Format("and pono='{0}'", Session["Comm_CGPONo"]))[0];
                txtAE.Text = model.AE;
                txtPONo.Text = model.PONo;
                txtGuestName.Text = model.GuestName;
                txtName.Text = model.POName;
                //var list = POSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", model.PONo));
                //if (list.Count > 0)
                //{
                //    txtTotal.Text = (list[0].POTotal - list[0].TuiTotal).ToString();
                //}
                var supplierList= new CAI_POCaiService().GetLastSupplier(string.Format("'{0}'", model.PONo));
                txtSalesUnit.Text=  string.Join(",", supplierList.Select(t => t.Supplier).ToArray());
                txtTotal.Text = supplierList.Sum(t=>t.Total).ToString();
                Session["Comm_CGPONo"] = null;

                var list= petitionsSer.GetListArray(string.Format(" PoNo='{0}'", txtPONo.Text),"","");
                if (list.Count > 0)
                {
                    var modelPetition = list[0];
                    lblError.Text = string.Format("该项目已有签呈单，签呈单编号：{0}，签呈单名称:{1}, 签订日期：{2}, 存放位置：{3}",
                       modelPetition.Number, modelPetition.Name, modelPetition.SignDate.ToString("yyyy-MM-dd"), modelPetition.Local_String);
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

        protected void cbIsRequire_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsRequire.Checked)
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
