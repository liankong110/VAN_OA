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
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;

namespace VAN_OA.ReportForms
{
    public partial class CarOilMaintenance : System.Web.UI.Page
    {
        private TB_CarOilMaintenanceService carMaintenanceSer = new TB_CarOilMaintenanceService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    TB_CarOilMaintenance carModel = new TB_CarOilMaintenance();
                    carModel.CardNo = ddlCarNo.Text;
                    carModel.CreateTime = DateTime.Now;
                    carModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                  
                    carModel.MaintenanceTime = Convert.ToDateTime(txtMaintenanceTime.Text);
                    carModel.Remark = txtRemark.Text;


                    if (txtChongZhiTime.Text != "")
                    {
                        carModel.ChongZhiDate = Convert.ToDateTime(txtChongZhiTime.Text);
                    }

                    if (txtOilTotal.Text != "")
                    {
                        carModel.OilTotal = Convert.ToDecimal(txtOilTotal.Text);
                    }

                    if (txtAddTotal.Text != "")
                    {
                        carModel.AddTotal = Convert.ToDecimal(txtAddTotal.Text);
                    }



                    if (txtTotal.Text != "")
                    {
                        carModel.Total = Convert.ToDecimal(txtTotal.Text);
                    }
                    if (txtUpTotal.Text != "")
                    {
                        carModel.UpTotal = Convert.ToDecimal(txtUpTotal.Text);
                    }

                     
                    if (this.carMaintenanceSer.Add(carModel) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        ddlCarNo.Text = "";
                        
                        txtMaintenanceTime.Text = "";
                        txtRemark.Text = "";
                        
                        txtAddTotal.Text = "";
                        txtTotal.Text = "";
                        txtUpTotal.Text = "";
                        txtOilTotal.Text = "";
                        txtChongZhiTime.Text = "";

                        this.ddlCarNo.Focus();
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
            base.Response.Redirect(Session["POUrl"].ToString());
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {


                    TB_CarOilMaintenance carModel = new TB_CarOilMaintenance();
                    carModel.CardNo = ddlCarNo.Text;
                    carModel.CreateTime = DateTime.Now;
                    carModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                     
                    carModel.MaintenanceTime = Convert.ToDateTime(txtMaintenanceTime.Text);
                    carModel.Remark = txtRemark.Text;
                    if (txtChongZhiTime.Text != "")
                    {
                        carModel.ChongZhiDate = Convert.ToDateTime(txtChongZhiTime.Text);
                    }

                    if (txtOilTotal.Text != "")
                    {
                        carModel.OilTotal = Convert.ToDecimal(txtOilTotal.Text);
                    }

                    if (txtAddTotal.Text != "")
                    {
                        carModel.AddTotal = Convert.ToDecimal(txtAddTotal.Text);                        
                    }



                    if (txtTotal.Text != "")
                    {
                        carModel.Total = Convert.ToDecimal(txtTotal.Text);
                    }
                    if (txtUpTotal.Text != "")
                    {
                        carModel.UpTotal = Convert.ToDecimal(txtUpTotal.Text);
                    }

                    
                    carModel.Id = Convert.ToInt32(base.Request["Id"]);
                    if (this.carMaintenanceSer.Update(carModel))
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


            if (this.ddlCarNo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择车牌号！');</script>");
                this.ddlCarNo.Focus();
                return false;
            }

            if (this.txtMaintenanceTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写保养/加油时间！');</script>");
                this.txtMaintenanceTime.Focus();
                return false;
            }
            if (txtMaintenanceTime.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtMaintenanceTime.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的日期格式有误！');</script>");
                    txtMaintenanceTime.Focus();
                    return false;
                }
            }



            if (txtChongZhiTime.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtChongZhiTime.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的充值时间格式有误！');</script>");
                    txtChongZhiTime.Focus();
                    return false;
                }
            }

                if (this.txtOilTotal.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写加油费用！');</script>");
                    this.txtOilTotal.Focus();
                    return false;
                }
                try
                {
                    Convert.ToDecimal(txtOilTotal.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的金额格式错误！');</script>");
                    this.txtOilTotal.Focus();
                }
            if (Request["Type"] == null)
            {
                //if (base.Request["Id"] != null)//
                //{
                //根据车牌获取最后一次的的加油时间
                object obj = DBHelp.ExeScalar(string.Format("select max(MaintenanceTime) from TB_CarOilMaintenance where CardNo='{0}'", ddlCarNo.Text));
                if (obj != null && !(obj is DBNull))
                {
                    if (Convert.ToDateTime(txtMaintenanceTime.Text) <= Convert.ToDateTime(obj))
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('上次加油时间为{0},你填写的时间必须大于上次加油时间！');</script>", obj));
                        this.txtMaintenanceTime.Focus();
                        return false;
                    }
                }
                //}

                //检查上期金额是否正确

                //获取最后一次的本期金额，作为这次的上期金额 
                string sql = "select Total from TB_CarOilMaintenance where MaintenanceTime=(";
                sql += string.Format("select max(MaintenanceTime) from TB_CarOilMaintenance where CardNo='{0}' ", ddlCarNo.Text);
                sql += string.Format(" ) and CardNo='{0}'", ddlCarNo.Text);

                object objTotal = DBHelp.ExeScalar(sql);

                string objTo = "";
                if (objTotal != null && !(objTotal is DBNull))
                {

                    objTo = objTotal.ToString();
                }
                else
                {
                    objTo = "0";
                }
                if (txtUpTotal.Text != objTo)
                {
                    txtUpTotal.Text = objTo;


                    SetTotal();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('系统检测到上期金额值已发生改变，请在核对信息后提交！');</script>", obj));
                    this.txtUpTotal.Focus();
                    return false;

                }
            }
            
            

            if (base.Request["Id"] != null)
            {
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleCode='{0}' and RID<>{1}", this.txtRoleCode.Text.Trim(), base.Request["RoleId"]))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                //    this.txtRoleCode.Focus();
                //    return false;
                //}
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}' and RID<>{1}", this.txtRoleName.Text.Trim(), base.Request["RoleId"]))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                //    this.txtRoleName.Focus();
                //    return false;
                //}
            }
            else
            {
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from TB_PO where RoleCode='{0}'", this.txtRoleCode.Text.Trim()))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                //    this.txtRoleCode.Focus();
                //    return false;
                //}
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}'", this.txtRoleName.Text.Trim()))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                //    this.txtRoleName.Focus();
                //    return false;
                //}
            }
            return true;
        }


   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo where IsStop=0");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.Text = "";
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    TB_CarOilMaintenance CarMaiModel = this.carMaintenanceSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    ddlCarNo.Text = CarMaiModel.CardNo;
                 
                    txtMaintenanceTime.Text = CarMaiModel.MaintenanceTime.ToString();
                    txtRemark.Text = CarMaiModel.Remark;                
                    
                    if (CarMaiModel.ChongZhiDate != null)
                        txtChongZhiTime.Text = CarMaiModel.ChongZhiDate.ToString();

                    if (CarMaiModel.Total != null)
                        txtTotal.Text = CarMaiModel.Total.ToString();
                    
                    if (CarMaiModel.UpTotal != null)
                        txtUpTotal.Text = CarMaiModel.UpTotal.ToString();

                    
                    if (CarMaiModel.AddTotal != null)
                        txtAddTotal.Text = CarMaiModel.AddTotal.ToString();
                    
                    if (CarMaiModel.OilTotal != null)
                        txtOilTotal.Text = CarMaiModel.OilTotal.ToString();




                    //ddlCarNo.Enabled = false;
                    //判断是否是最后一次填写的信息
                    //object obj = DBHelp.ExeScalar(string.Format("select max(MaintenanceTime) from TB_CarOilMaintenance where CardNo='{0}' and UseState='加油'", ddlCarNo.Text));
                    //if (obj != null)
                    //{ 

                    //}
                    //txtAddTotal.Enabled = false;
                    //txtOilTotal.Enabled = false;
                    //txtTotal.Enabled = false;
                    //txtUpTotal.Enabled = false;
                    //txtMaintenanceTime.Enabled = false;
                    //txtRemark.Enabled = false;

                    //txtChongZhiTime.Enabled = false;

                    //this.btnUpdate.Visible = false;

                    if (Request["Type"] != null)
                    {
                        this.btnUpdate.Visible = true;
                    }

                    
                    
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

        

        protected void ddlCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                SetIolTotal();
             
        }

        private void SetIolTotal()
        {
            //获取最后一次的本期金额，作为这次的上期金额 
            string sql = "select Total from TB_CarOilMaintenance where MaintenanceTime=(";
            sql += string.Format("select max(MaintenanceTime) from TB_CarOilMaintenance where CardNo='{0}'", ddlCarNo.Text);
            sql += string.Format(" ) and CardNo='{0}'", ddlCarNo.Text);

            object obj = DBHelp.ExeScalar(sql);
            if (obj != null && !(obj is DBNull))
                txtUpTotal.Text = obj.ToString();
            else
            {
                txtUpTotal.Text = "0";
            }

            SetTotal();
        }

        public void SetTotal()
        {
            try
            {
                //上期余额-加油+充值=本期余额
                decimal upTotal = 0;
                decimal oilTotal = 0;
                decimal addTotal = 0;
                if (txtUpTotal.Text != "")
                {
                    upTotal = Convert.ToDecimal(txtUpTotal.Text);
                }
                if (txtOilTotal.Text != "")
                {
                    oilTotal = Convert.ToDecimal(txtOilTotal.Text);
                }
                if (txtAddTotal.Text != "")
                {
                    addTotal = Convert.ToDecimal(txtAddTotal.Text);
                }
                txtTotal.Text = (upTotal - oilTotal +addTotal).ToString();
            }
            catch (Exception)
            {
                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的金额格式错误！');</script>");
                 this.txtOilTotal.Focus();
               
            }

        }

        protected void txtOilTotal_TextChanged(object sender, EventArgs e)
        {
            SetTotal();
        }


    }
}
