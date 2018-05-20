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
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA
{
    public partial class WFUserCmd : BasePage
    {
        private RoleService roleSer = new RoleService();
        private SysUserService UserSer = new SysUserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    User user = new User();
                    user.LoginUserNO = this.txtNo.Text;
                    //user.LoginAddress = this.txtAddress.Text;
                    //user.LoginRemark = this.txtCardNo.Text;
                    user.LoginIPosition = this.ddlDeptment.Text;
                    user.LoginPwd = MD5Util.Encrypt(this.txtPwd.Text);
                    user.LoginPhone = this.txtTel.Text;
                    user.LoginId = this.txtUserid.Text;
                    user.LoginName = this.txtUserName.Text;
                    //user.LoginStatus = this.ddlState.Text;
                    //user.LoginTmpPwd = this.ddlState.Text;
                    user.LoginStatus = this.ddlState.Text;
                    user.LoginTmpPwd = this.ddlSex.Text;
                    user.ReportTo = Convert.ToInt32(ddlReportTo.SelectedItem.Value);
                    user.Zhiwu = txtZhiwu.Text;
                    user.LoginMemo = txtEMail.Text;
                    user.CompanyCode = ddlCompany.Text;
                    user.SheBaoCode = ddlSheBao.Text;
                    user.IsSpecialUser = cbIsSpecialUser.Checked;

                    user.Mobile = txtMobile.Text;
                    user.CardNO = txtCardNO.Text;
                    user.CityNo = txtCityNo.Text;
                    user.Education = ddlEducation.Text;
                    user.School = txtSchool.Text;
                    user.SchoolDate = txtSchoolDate.Text;
                    user.Title = txtTitle.Text;
                    user.Political = ddlPolitical.Text;
                    user.HomeAdd = txtHomeAdd.Text;
                    user.WorkDate = txtWorkDate.Text;
                    int userId = this.UserSer.addUser(user);
                    if (userId > 0)
                    {
                        List<RoleUser> roleUserList = new List<RoleUser>();
                        for (int i = 0; i < this.gvList.Rows.Count; i++)
                        {
                            CheckBox cbSelect = this.gvList.Rows[i].Cells[0].FindControl("IfSelected") as CheckBox;
                            if (cbSelect.Checked)
                            {
                                RoleUser roleUser = new RoleUser();
                                roleUser.UserId = userId;
                                roleUser.RoleId = Convert.ToInt32(this.gvList.DataKeys[i].Value);
                                roleUserList.Add(roleUser);
                            }
                        }
                        new RoleUserService().addRole_User(0, userId, roleUserList);
                        //28	被派工人
                        //32	请假代理人 自动加载到审批流程中
                        string sql = string.Format(@"insert into A_Role_User values(28,{0},0,1);insert into A_Role_User values(32,{0},0,1);", userId);
                        DBHelp.ExeCommand(sql);

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        this.clear();
                        IniData();
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
            base.Response.Redirect("~/WFUserList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    User user = new User();
                    user.LoginUserNO = this.txtNo.Text;
                    user.LoginAddress = "";
                    user.LoginRemark = "";
                    user.LoginIPosition = this.ddlDeptment.Text;

                    if (txtPwd.Enabled == false || txtPwd.Text == lblPwd.Text)
                    {
                        user.LoginPwd = lblPwd.Text;
                    }
                    else
                    {
                        user.LoginPwd = MD5Util.Encrypt(this.txtPwd.Text);
                    }
                    user.LoginPhone = this.txtTel.Text;
                    user.LoginId = this.txtUserid.Text;
                    user.LoginName = this.txtUserName.Text;
                    user.Id = Convert.ToInt32(base.Request["UserId"]);
                    user.LoginStatus = this.ddlState.Text;
                    user.LoginTmpPwd = this.ddlSex.Text;
                    user.ReportTo = Convert.ToInt32(ddlReportTo.SelectedItem.Value);
                    user.Zhiwu = txtZhiwu.Text;
                    user.CompanyCode = ddlCompany.Text;
                    user.SheBaoCode = ddlSheBao.Text;
                    user.LoginMemo = txtEMail.Text;
                    user.IsSpecialUser = cbIsSpecialUser.Checked;
                    user.Mobile = txtMobile.Text;
                    user.CardNO = txtCardNO.Text;
                    user.CityNo = txtCityNo.Text;
                    user.Education = ddlEducation.Text;
                    user.School = txtSchool.Text;
                    user.SchoolDate = txtSchoolDate.Text;
                    user.Title = txtTitle.Text;
                    user.Political = ddlPolitical.Text;
                    user.HomeAdd = txtHomeAdd.Text;
                    user.WorkDate = txtWorkDate.Text;
                    if (this.UserSer.modifyUser(user))
                    {
                        List<RoleUser> roleUserList = new List<RoleUser>();
                        for (int i = 0; i < this.gvList.Rows.Count; i++)
                        {
                            CheckBox cbSelect = this.gvList.Rows[i].Cells[0].FindControl("IfSelected") as CheckBox;
                            if (cbSelect.Checked)
                            {
                                RoleUser roleUser = new RoleUser();
                                roleUser.UserId = user.Id;
                                roleUser.RoleId = Convert.ToInt32(this.gvList.DataKeys[i].Value);
                                roleUserList.Add(roleUser);
                            }
                        }
                        new RoleUserService().addRole_User(0, user.Id, roleUserList);
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

        private void clear()
        {
            this.txtUserName.Text = "";
            this.txtUserid.Text = "";
            this.txtTel.Text = "";
            this.txtPwd.Text = "";
            this.txtNo.Text = "";
            //this.txtDeptment.Text = "";
            this.txtCreateTime.Text = "";
            //this.txtCardNo.Text = "";
            //this.txtAddress.Text = "";
            txtZhiwu.Text = "";
            txtEMail.Text = "";
            cbIsSpecialUser.Checked = false;

            txtMobile.Text = "";
            txtCardNO.Text = "";
            txtCityNo.Text = "";
            ddlEducation.Text = "";
            txtSchool.Text = "";
            txtSchoolDate.Text = "";
            txtTitle.Text = "";
            ddlPolitical.Text = "";
            txtHomeAdd.Text = "";
            txtWorkDate.Text = "";

            this.txtNo.Focus();
        }

        public bool FormCheck()
        {          
            if (this.txtUserid.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写用户账号！');</script>");
                this.txtUserid.Focus();
                return false;
            }
            if (this.txtUserName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写用户名称！');</script>");
                this.txtUserName.Focus();
                return false;
            }
            if (txtPwd.Enabled != false && this.txtPwd.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写用户密码！');</script>");
                this.txtPwd.Focus();
                return false;
            }
            if (this.txtZhiwu.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写职务！');</script>");
                this.txtZhiwu.Focus();
                return false;
            }
            if (this.ddlReportTo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写所属上级！');</script>");
                this.ddlReportTo.Focus();
                return false;
            }
            if (this.txtTel.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写电话！');</script>");
                this.txtTel.Focus();
                return false;
            }
            if (this.txtEMail.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写E-Mail！');</script>");
                this.txtEMail.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(txtSchoolDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtSchoolDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('毕业时间 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtWorkDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtWorkDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('参加工作时间 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtCreateTime.Text))
            {
                if (CommHelp.VerifesToDateTime(txtCreateTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('建档时间 格式错误！');</script>");
                    return false;
                }
            }
            if (base.Request["UserId"] != null)
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_User where loginId='{0}' and id <>{1}", this.txtUserid.Text, base.Request["UserId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户账号" + this.txtUserid.Text + "已存在，请重新填写！');</script>");
                    this.txtUserid.Focus();
                    return false;
                }
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_User where loginName='{0}' and id <>{1}", this.txtUserName.Text, base.Request["UserId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户名" + this.txtUserName.Text + "已存在，请重新填写！');</script>");
                    this.txtUserName.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_User where loginId='{0}'", this.txtUserid.Text))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户账号" + this.txtUserid.Text + "已存在，请重新填写！');</script>");
                    this.txtUserid.Focus();
                    return false;
                }
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_User where loginName='{0}'", this.txtUserName.Text))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户名" + this.txtUserName.Text + "已存在，请重新填写！');</script>");
                    this.txtUserName.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }


        private void IniData()
        {
            try
            {
                string sql = string.Format("select loginName,ID from tb_User where loginName <>'{0}'", txtUserName.Text);

                List<User> getUsers = UserSer.getUserReportTos(sql);
                ddlReportTo.DataSource = getUsers;
                ddlReportTo.DataBind();

                ddlReportTo.DataTextField = "LoginName";
                ddlReportTo.DataValueField = "Id";
            }
            catch (Exception)
            {


            }
        }

        public bool SetEnable()
        {            
            return Convert.ToBoolean(ViewState["ScanSpecGuests"]);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (NewShowAll_textName("用户信息", "可记录权限"))
                {
                    ViewState["ScanSpecGuests"] = true;
                }
                else
                {
                    ViewState["ScanSpecGuests"] = false;                     
                }    

                List<Role> roles1 = this.roleSer.getAllRoles("");
                roles1.Insert(0, new Role { RoleName=""});
                this.ddlDeptment.DataTextField = "RoleName";
                this.ddlDeptment.DataValueField = "RoleName";
                this.ddlDeptment.DataSource = roles1;
                this.ddlDeptment.DataBind();

                TB_CompanyService comSer = new TB_CompanyService();

                var comList = comSer.GetListArray("");
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "", ComName = "" });
                ddlCompany.DataSource = comList;

                ddlCompany.DataBind();


                ddlSheBao.DataSource = comList;
                ddlSheBao.DataBind();

                List<Role> roles = new List<Role>();
                if (base.Request["UserId"] != null)
                {
                    this.btnAdd.Visible = false;
                    User user = this.UserSer.getUserByUserId(Convert.ToInt32(base.Request["UserId"]));
                    this.txtNo.Text = user.LoginUserNO;
                    //this.txtAddress.Text = user.LoginAddress;
                    //this.txtCardNo.Text = user.LoginRemark;
                    this.txtCreateTime.Text = user.LoginCreateTime.ToShortDateString();
                    this.ddlDeptment.Text = user.LoginIPosition;

                    txtPwd.Attributes.Add("Value", user.LoginPwd);
                    lblPwd.Text = user.LoginPwd;
                    this.txtPwd.Text = user.LoginPwd;
                    this.txtTel.Text = user.LoginPhone;
                    this.txtUserid.Text = user.LoginId;
                    this.txtUserName.Text = user.LoginName;
                    this.ddlState.Text = user.LoginStatus;
                    this.ddlSex.Text = user.LoginTmpPwd;
                    ddlCompany.Text = user.CompanyCode;
                    try
                    {
                        ddlSheBao.Text = user.SheBaoCode;
                    }
                    catch (Exception)
                    {
                        
                        
                    }
                    txtZhiwu.Text = user.Zhiwu;
                    cbIsSpecialUser.Checked = user.IsSpecialUser;
                    txtEMail.Text = user.LoginMemo;
                    if (user.ReportTo != 0)
                        ddlReportTo.Text = user.ReportTo.ToString();


                    txtMobile.Text=user.Mobile;
                    txtCardNO.Text=user.CardNO;
                    txtCityNo.Text=user.CityNo ;
                    ddlEducation.Text=user.Education ;
                    txtSchool.Text=user.School ;
                    txtSchoolDate.Text=user.SchoolDate ;
                    txtTitle.Text=user.Title ;
                    ddlPolitical.Text=user.Political ;
                    txtHomeAdd.Text=user.HomeAdd;
                    txtWorkDate.Text= user.WorkDate ;

                    roles = this.roleSer.getAllRolesByUseridExAdmin(user.Id);

                    string checkRole = string.Format("select count(*) from Role_User where roleId=1 and UserId=" + Session["currentUserId"].ToString());

                    bool ifAdmin = Convert.ToInt32(DBHelp.ExeScalar(checkRole)) >= 1 ? true : false;
                    if (ifAdmin)
                    {
                        txtPwd.Enabled = true;
                    }
                    else
                    {
                        txtPwd.Enabled = false;
                    }
                }
                else
                {
                    roles = this.roleSer.getAllRolesByUseridExAdmin(0);
                    this.btnUpdate.Visible = false;

                }
                IniData();
                this.gvList.DataSource = roles;
                this.gvList.DataBind();



            }
        }
    }
}
