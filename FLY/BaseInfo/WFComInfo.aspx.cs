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


namespace VAN_OA.BaseInfo
{
    public partial class WFComInfo : BasePage
    {

        private tb_ComInfoService comInfoSer = new tb_ComInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string ComName = this.txtComName.Text;
                    string NaShuiNo = this.txtNaShuiNo.Text;
                    string Address = this.txtAddress.Text;
                    string ComBrand = this.txtComBrand.Text;
                    string InvoHeader = this.txtInvoHeader.Text;
                    string InvContactPer = this.txtInvContactPer.Text;
                    string InvAddress = this.txtInvAddress.Text;
                    string InvTel = this.txtInvTel.Text;
                    string NaShuiPer = this.txtNaShuiPer.Text;
                    string brandNo = this.txtbrandNo.Text;
                    string ComTel = this.txtComTel.Text;
                    string ComChuanZhen = this.txtComChuanZhen.Text;
                    string ComBusTel = this.txtComBusTel.Text;

                    VAN_OA.Model.BaseInfo.tb_ComInfo model = new VAN_OA.Model.BaseInfo.tb_ComInfo();
                    model.ComName = ComName;
                    model.NaShuiNo = NaShuiNo;
                    model.Address = Address;
                    model.ComBrand = ComBrand;
                    model.InvoHeader = InvoHeader;
                    model.InvContactPer = InvContactPer;
                    model.InvAddress = InvAddress;
                    model.InvTel = InvTel;
                    model.NaShuiPer = NaShuiPer;
                    model.brandNo = brandNo;
                    model.ComTel = ComTel;
                    model.ComChuanZhen = ComChuanZhen;
                    model.ComBusTel = ComBusTel;
                    model.Brand = txtBrand.Text;
                    int id=comInfoSer.Add(model);
                    if (id > 0)
                    {
                        Response.Redirect("~/BaseInfo/WFComInfo.aspx?Id=" + id);
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");                      
                    }
                    else
                    {
                       // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

       



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string ComName = this.txtComName.Text;
                    string NaShuiNo = this.txtNaShuiNo.Text;
                    string Address = this.txtAddress.Text;
                    string ComBrand = this.txtComBrand.Text;
                    string InvoHeader = this.txtInvoHeader.Text;
                    string InvContactPer = this.txtInvContactPer.Text;
                    string InvAddress = this.txtInvAddress.Text;
                    string InvTel = this.txtInvTel.Text;
                    string NaShuiPer = this.txtNaShuiPer.Text;
                    string brandNo = this.txtbrandNo.Text;
                    string ComTel = this.txtComTel.Text;
                    string ComChuanZhen = this.txtComChuanZhen.Text;
                    string ComBusTel = this.txtComBusTel.Text;

                    VAN_OA.Model.BaseInfo.tb_ComInfo model = new VAN_OA.Model.BaseInfo.tb_ComInfo();
                    model.ComName = ComName;
                    model.NaShuiNo = NaShuiNo;
                    model.Address = Address;
                    model.ComBrand = ComBrand;
                    model.InvoHeader = InvoHeader;
                    model.InvContactPer = InvContactPer;
                    model.InvAddress = InvAddress;
                    model.InvTel = InvTel;
                    model.NaShuiPer = NaShuiPer;
                    model.brandNo = brandNo;
                    model.ComTel = ComTel;
                    model.ComChuanZhen = ComChuanZhen;
                    model.ComBusTel = ComBusTel;
                    model.id = Convert.ToInt32(base.Request["Id"]);
                    model.Brand = txtBrand.Text;
                    if (this.comInfoSer.Update(model))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                    }
                    else
                    {
                       // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
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

            if (this.txtComName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写公司名称！');</script>");
                this.txtComName.Focus();
                return false;
            }
           
            return true;
        }

        private void ShowInfo(int id)
        {
            VAN_OA.Model.BaseInfo.tb_ComInfo model = comInfoSer.GetModel(id);             
            this.txtComName.Text = model.ComName;
            this.txtNaShuiNo.Text = model.NaShuiNo;
            this.txtAddress.Text = model.Address;
            this.txtComBrand.Text = model.ComBrand;
            this.txtInvoHeader.Text = model.InvoHeader;
            this.txtInvContactPer.Text = model.InvContactPer;
            this.txtInvAddress.Text = model.InvAddress;
            this.txtInvTel.Text = model.InvTel;
            this.txtNaShuiPer.Text = model.NaShuiPer;
            this.txtbrandNo.Text = model.brandNo;
            this.txtComTel.Text = model.ComTel;
            this.txtComChuanZhen.Text = model.ComChuanZhen;
            this.txtComBusTel.Text = model.ComBusTel;
            txtBrand.Text = model.Brand;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    ShowInfo(Convert.ToInt32(base.Request["Id"]));                     
                }
                else
                {

                    var comInfo=comInfoSer.GetListArray("");
                    if (comInfo.Count > 0)
                    {
                        Response.Redirect("~/BaseInfo/WFComInfo.aspx?Id=" + comInfo[0].id);
                        return;
                    }
                    this.btnUpdate.Visible = false;
                }
            }
        }

        
    }
}
