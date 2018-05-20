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
using VAN_OA.Dal.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.EFrom;


namespace VAN_OA.EFrom
{
    public partial class MyConsignor : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    tb_Consignor conSer = new tb_Consignor();
                    conSer.appPer =Convert.ToInt32(Session["currentUserId"]);

                   // string sql = string.Format("select id from tb_User where loginName='{0}'",ddlUser.SelectedItem.Value);
                    int userId = Convert.ToInt32(ddlUser.SelectedItem.Value);
                    conSer.consignor = userId;
                    conSer.conState = "开启";
                    if (txtFrom.Text != "")
                    {
                        conSer.fromTime =Convert.ToDateTime(txtFrom.Text+" 00:00:00");
                    }
                    if (txtTo.Text != "")
                    {

                        conSer.toTime = Convert.ToDateTime(txtTo.Text + " 23:59:59");
                    }
                    conSer.proId =Convert.ToInt32( ddlProType.SelectedItem.Value);

                    conSer.ifYouXiao = cbYouXiao.Checked;


                    tb_ConsignorService consiSer = new tb_ConsignorService();

                    if (cbAll.Checked == false)
                    {
                        if (consiSer.Add(conSer) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                            txtFrom.Text = "";
                            txtTo.Text = "";
                            cbYouXiao.Checked = false;
                           // txtconsignor.Text = "";
                            ddlProType.Focus();
                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                        }
                    }
                    else
                    {
                        List<int> conList = new List<int>();
                        for (int i = 0; i <ddlProType.Items.Count; i++)
                        {
                            conList.Add(Convert.ToInt32(ddlProType.Items[i].Value));
                        }
                        if (consiSer.AddSome(conSer, conList) > 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                                txtFrom.Text = "";
                                txtTo.Text = "";
                                cbYouXiao.Checked = false;
                                //txtconsignor.Text = "";
                                ddlProType.Focus();
                            }
                            else
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                            }
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
            base.Response.Redirect("~/EFrom/MyConsignorList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
          

            txtFrom.Text = "";
            txtTo.Text = "";
            cbYouXiao.Checked = false;
            //txtconsignor.Text = "";
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    tb_Consignor conSer = new tb_Consignor();
                    conSer.appPer = Convert.ToInt32(Session["currentUserId"]);

                   // string sql = string.Format("select id from tb_User where loginName='{0}'", ddlUser.SelectedItem.Value);
                    int userId = Convert.ToInt32(ddlUser.SelectedItem.Value);
                    conSer.consignor = userId;
                    conSer.conState = "开启";
                    if (txtFrom.Text != "")
                    {
                        conSer.fromTime = Convert.ToDateTime(txtFrom.Text + " 00:00:00");
                    }
                    if (txtTo.Text != "")
                    {

                        conSer.toTime = Convert.ToDateTime(txtTo.Text + " 23:59:59");
                    }
                    conSer.proId = Convert.ToInt32(ddlProType.SelectedItem.Value);
                    conSer.ifYouXiao = cbYouXiao.Checked;
                    conSer.con_Id = Convert.ToInt32(Request["ID"]);


                    tb_ConsignorService consiSer = new tb_ConsignorService();
                    consiSer.Update(conSer);
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");

                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {

            if (ddlUser.SelectedItem == null || ddlUser.SelectedItem.Value=="")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择被委托人！');</script>");
                this.ddlUser.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('生效日期 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('终止日期 格式错误！');</script>");
                    return false;
                }
            }

            if (txtFrom.Text == "" && txtTo.Text == "" && cbYouXiao.Checked == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请设定一个有效期！');</script>");
                this.txtFrom.Focus();
                return false;
            }


            if (cbYouXiao.Checked == true)
            {
                if (txtTo.Text != "" || txtFrom.Text != "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('当有效期设定为一直有效时,生效时间和终止时间不允许填写内容！');</script>");
                    this.txtTo.Focus();
                    return false;
                }
            }

            if (txtTo.Text != "" && txtFrom.Text != "")
            {
                try
                {
                    if (Convert.ToDateTime(txtTo.Text) < Convert.ToDateTime(txtFrom.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('生效时间不能大于终止时间！');</script>");
                        this.txtTo.Focus();
                        return false;
                         
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式不正确！');</script>");
                    return false;
                }
            }

            if (ddlUser.SelectedItem.Text.Trim() == Session["LoginName"].ToString())
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('被委托人不能为自己！');</script>");
                ddlUser.Focus();
                return false;
            }

           
            //string sql = string.Format("select count(*) from tb_User where loginName='{0}'",txtconsignor.Text);
            //int userId = Convert.ToInt32(DBHelp.ExeScalar(sql));
            //if (userId <= 0)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert(' 你填写的用户名在系统用户列表中不存在！');</script>");
            //    this.txtconsignor.Focus();
            //    return false;
            //}




            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";



                A_ProInfoService proSer = new A_ProInfoService();
                List<A_ProInfo> pros = proSer.GetListArray("");
                ddlProType.DataSource = pros;
                ddlProType.DataBind();

                ddlProType.DataTextField = "pro_Type";
                ddlProType.DataValueField = "pro_Id";


                if (base.Request["ID"] != null)
                {
                    cbAll.Visible = false;
                    this.btnAdd.Visible = false;
                    tb_ConsignorService conSer = new tb_ConsignorService();
                    tb_Consignor consi = conSer.GetListArray(" con_Id=" + base.Request["ID"])[0];


                    ddlProType.SelectedItem.Value = consi.proId.ToString();
                    if (consi.fromTime != null)
                    {

                        txtFrom.Text = Convert.ToDateTime(consi.fromTime).ToShortDateString();
                    }

                    if (consi.toTime != null)
                    {
                        txtTo.Text = Convert.ToDateTime(consi.toTime).ToShortDateString();
                    }
                   // txtconsignor.Text = consi.Consignor_Name;
                    cbYouXiao.Checked = consi.ifYouXiao;

                    ddlUser.Text = consi.consignor.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                ddlProType.Enabled = false;
            }
            else
            {
                ddlProType.Enabled = true;
            }
        }
    }
}
