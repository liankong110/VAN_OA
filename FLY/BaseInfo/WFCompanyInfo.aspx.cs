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
    public partial class WFCompanyInfo : System.Web.UI.Page
    {
        private TB_CompanyService goodSer = new TB_CompanyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_Company where ComCode='{0}'",
                txtComCode.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公司代码[{0}]，已经存在！');</script>", txtComCode.Text));
                        return;
                    }
                    TB_Company model = getModel();
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
            base.Response.Redirect("~/BaseInfo/CompanyList.aspx");
        }

        private void Clear()
        {
            txtComCode.Text = "";
            txtComId.Text = "";
            txtComName.Text = "";
            txtComSimpName.Text = "";
            txtChuanZhen.Text = "";
            txtComCode.Text = "";
            txtComUrl.Text = "";
            txtCreateTime.Text = "";
            txtDianHua.Text = "";
            txtEmail.Text = "";
            txtEndTime.Text = "";
            txtFanWei.Text = "";
            txtFaRen.Text = "";
            txtKaHao.Text = "";
            txtKaiHuHang.Text = "";
            txtLeiXing.Text = "";
            txtStartTime.Text = "";
            txtXinYongCode.Text = "";
            txtZhuCeZiBen.Text = "";
            txtZhuSuo.Text = "";


        }


        public TB_Company getModel()
        {
            VAN_OA.Model.BaseInfo.TB_Company model = new VAN_OA.Model.BaseInfo.TB_Company();

            model.ComCode = txtComCode.Text;
            model.ComId = txtComId.Text;
            model.ComName = txtComName.Text;
            model.ComSimpName = txtComSimpName.Text;
            model.OrderByIndex = Convert.ToInt32(txtOrderByIndex.Text);
            model.ZhuSuo = txtZhuSuo.Text;
            model.LeiXing = txtLeiXing.Text;
            model.DianHua = txtDianHua.Text;
            model.ChuanZhen = txtChuanZhen.Text;
            model.XinYongCode = txtXinYongCode.Text;
            model.FaRen = txtFaRen.Text;
            model.ZhuCeZiBen = txtZhuCeZiBen.Text;
            model.CreateTime =Convert.ToDateTime( txtCreateTime.Text);
            model.StartTime =Convert.ToDateTime( txtStartTime.Text);
            model.EndTime = Convert.ToDateTime(txtEndTime.Text);
            model.FanWei = txtFanWei.Text;
            model.KaiHuHang = txtKaiHuHang.Text;
            model.KaHao = txtKaHao.Text;
            model.Email = txtEmail.Text;
            model.ComUrl = txtComUrl.Text;
        

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
                    string sqlCheck = string.Format("select count(*) from TB_Company where ComCode='{0}' and id<>{1}",
                  txtComCode.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('公司代码[{0}]，已经存在！');</script>", txtComCode.Text));
                        return;
                    }
                    TB_Company model = getModel();
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
            if (this.txtComCode.Text.Trim().Length == 0)
            {
                strErr += "公司代码不能为空！\\n";
            }
            if (this.txtComName.Text.Trim().Length == 0)
            {
                strErr += "公司名称不能为空！\\n";
            }
            if (this.txtComSimpName.Text.Trim().Length == 0)
            {
                strErr += "公司简称不能为空！\\n";
            }

            if (this.txtZhuSuo.Text.Trim().Length == 0)
            {
                strErr += "住所不能为空！\\n";
            }
            if (this.txtLeiXing.Text.Trim().Length == 0)
            {
                strErr += "类型不能为空！\\n";
            }
            if (this.txtDianHua.Text.Trim().Length == 0)
            {
                strErr += "电话不能为空！\\n";
            }
            if (this.txtChuanZhen.Text.Trim().Length == 0)
            {
                strErr += "传真不能为空！\\n";
            }
            if (this.txtXinYongCode.Text.Trim().Length == 0)
            {
                strErr += "信用代码不能为空！\\n";
            }
            if (this.txtFaRen.Text.Trim().Length == 0)
            {
                strErr += "法人不能为空！\\n";
            }
            if (this.txtZhuCeZiBen.Text.Trim().Length == 0)
            {
                strErr += "注册资本不能为空！\\n";
            }

            DateTime outTime;           

            if (DateTime.TryParse(txtCreateTime.Text, out outTime)==false)
            {
                strErr += "成立日期格式错误！\\n";
            }
           
            if (DateTime.TryParse(txtStartTime.Text, out outTime)==false)
            {
                strErr += "经营期限起始格式错误！\\n";
            }
            if (DateTime.TryParse(txtEndTime.Text, out outTime) == false)
            {
                strErr += "经营期限结束格式错误！\\n";
            }
            if (this.txtFanWei.Text.Trim().Length == 0)
            {
                strErr += "经营范围不能为空！\\n";
            }
            if (this.txtKaiHuHang.Text.Trim().Length == 0)
            {
                strErr += "开户行不能为空！\\n";
            }
            if (this.txtKaHao.Text.Trim().Length == 0)
            {
                strErr += "帐号不能为空！\\n";
            }
            if (this.txtEmail.Text.Trim().Length == 0)
            {
                strErr += "对外邮箱不能为空！\\n";
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
                    TB_Company model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtOrderByIndex.Text = model.OrderByIndex.ToString();
                    this.txtComId.Text = model.ComId;
                    this.txtComCode.Text = model.ComCode;
                    this.txtComName.Text = model.ComName;
                    this.txtComSimpName.Text = model.ComSimpName;
                    this.txtZhuSuo.Text = model.ZhuSuo;
                    this.txtLeiXing.Text = model.LeiXing;
                    this.txtDianHua.Text = model.DianHua;
                    this.txtChuanZhen.Text = model.ChuanZhen;
                    this.txtXinYongCode.Text = model.XinYongCode;
                    this.txtFaRen.Text = model.FaRen;
                    this.txtZhuCeZiBen.Text = model.ZhuCeZiBen;
                    this.txtCreateTime.Text = model.CreateTime.ToString("yyyy-MM-dd");
                    this.txtStartTime.Text = model.StartTime.ToString("yyyy-MM-dd");
                    this.txtEndTime.Text = model.EndTime.ToString("yyyy-MM-dd");
                    this.txtFanWei.Text = model.FanWei;
                    this.txtKaiHuHang.Text = model.KaiHuHang;
                    this.txtKaHao.Text = model.KaHao;
                    this.txtEmail.Text = model.Email;
                    this.txtComUrl.Text = model.ComUrl;
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
