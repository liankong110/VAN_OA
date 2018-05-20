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
using VAN_OA.Dal.HR;
using VAN_OA.Model.HR;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;
 
namespace VAN_OA.HR
{
    public partial class Person : System.Web.UI.Page
    {
        private HR_PERSONService perSer = new HR_PERSONService();
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (ddlUser.SelectedItem != null)
            {   
                SysUserService userSer=new SysUserService ();
                User user = userSer.getUserByUserId(Convert.ToInt32(ddlUser.SelectedItem.Value));

                lblID.Text = user.Id.ToString() ; 
                txtCode.Text = user.LoginUserNO;
                txtDepartment.Text = user.LoginIPosition;
                txtName.Text = user.LoginName;
                txtPosition.Text = user.Zhiwu;
                ddlSex.Text = user.LoginTmpPwd;
                try
                {
                    ddlUser.Text = user.LoginTmpPwd;
                }
                catch (Exception)
                {
                    
                  
                }
                txtMobilePhone.Text = user.LoginPhone;
            }
       }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    HR_PERSON per = getModel();
                    if (this.perSer.Add(per))
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
            base.Response.Redirect("~/HR/PersonList.aspx?pageindex=" + base.Request["pageindex"]);
        }

        private void Clear()
        {
        
           // this.txtCode.Text = "";
            this.txtDepartment.Text = "";
            this.txtPosition.Text = "";
            this.txtName.Text = "";
            this.txtBirthday.Text = "";
           
            this.txtEducationLevel.Text = "";
            this.txtEducationSchool.Text = "";
            this.txtMajor.Text = "";
            this.txtGraduationTime.Text = "";
            this.txtOnBoardTime.Text = "";
            this.txtBeNormalTime.Text = "";
            this.txtContractTime.Text = "";
            this.txtContractCloseTime.Text = "";
            this.txtHuKou.Text = "";
            
            this.txtIDCard.Text = "";
            this.txtMobilePhone.Text = "";
            this.txtHomePhone.Text = "";
            this.txtHomeAddress.Text = "";
            this.txtEmailAddress.Text = ""; 
            this.txtCreateTime.Text = "";
            this.txtCreatePerson.Text = "";
            this.txtUpdateTime.Text = "";
            this.txtUpdatePerson.Text = "";
            this.chkQuit.Checked  =false;
            this.txtQuitTime.Text = "";
            this.txtQuitReason.Text = "";
            txtCode.Focus();
        }

       
        public HR_PERSON getModel()
        {

            string ID = this.lblID.Text; 
            string Code = this.txtCode.Text;
            string Department = this.txtDepartment.Text;
            string Position = this.txtPosition.Text;
            string Name = this.txtName.Text;
            DateTime Birthday = DateTime.Parse(this.txtBirthday.Text);
            string Sex = this.ddlSex.Text;
            string EducationLevel = this.txtEducationLevel.Text;
            string EducationSchool = this.txtEducationSchool.Text;
            string Major = this.txtMajor.Text;
            DateTime? GraduationTime = null;
            if (txtGraduationTime.Text!="")
            {
                GraduationTime = DateTime.Parse(this.txtGraduationTime.Text);
            }

            DateTime? BeNormalTime = null;
            if (txtBeNormalTime.Text != "")
            {
                BeNormalTime = DateTime.Parse(this.txtBeNormalTime.Text);
            }
            DateTime? OnBoardTime = null;
            if (txtOnBoardTime.Text != "")
            {
                OnBoardTime = DateTime.Parse(this.txtOnBoardTime.Text);
            }
            DateTime? ContractTime = null;
            if (txtContractTime.Text != "")
            {
                ContractTime = DateTime.Parse(this.txtContractTime.Text);
            }
            DateTime? ContractCloseTime = null;
            if (txtContractCloseTime.Text != "")
            {
                ContractCloseTime = DateTime.Parse(this.txtContractCloseTime.Text);
            }

            string HuKou = this.txtHuKou .Text;
            string Marriage = this.ddlMarriage.Text;
            string IDCard = this.txtIDCard.Text;
            string MobilePhone = this.txtMobilePhone.Text;
            string HomePhone = this.txtHomePhone.Text;
            string HomeAddress = this.txtHomeAddress.Text;
            string EmailAddress = this.txtEmailAddress.Text;
            bool quit = this.chkQuit.Checked ;
            DateTime? quitTime = null;
            if (txtQuitTime.Text != "")
            {
                quitTime = DateTime.Parse(this.txtQuitTime.Text);
            }
            string quitReason = this.txtQuitReason.Text;

            DateTime CreateTime = DateTime.Now;
            int CreatePerson = Convert.ToInt32(Session["currentUserId"]);
            DateTime UpdateTime = DateTime.Now;
            int UpdatePerson = Convert.ToInt32(Session["currentUserId"]);


            VAN_OA.Model.HR.HR_PERSON model = new VAN_OA.Model.HR.HR_PERSON();
            if (base.Request["Code"] != null)
            {
                model.ID = Convert.ToInt32(base.Request["Code"]);
            }
            else
            {
                model.ID = Convert.ToInt32(lblID.Text);
            }
            model.Code = Code;
            model.Department = Department;
            model.Position = Position;
            model.Name = Name;
            model.Birthday = Birthday;
            model.Sex = Sex;
            model.EducationLevel = EducationLevel;
            model.EducationSchool = EducationSchool;
            model.Major = Major;
            model.GraduationTime = GraduationTime;
            model.OnBoardTime = OnBoardTime;
            model.BeNormalTime = BeNormalTime;
            model.ContractTime = ContractTime;
            model.ContractCloseTime = ContractCloseTime;
            model.HuKou = HuKou;
            model.Marriage = Marriage;
            model.IDCard = IDCard;
            model.MobilePhone = MobilePhone;
            model.HomePhone = HomePhone;
            model.HomeAddress = HomeAddress;
            model.EmailAddress = EmailAddress;
            model.Quit = quit;
            model.QuitTime = quitTime;
            model.QuitReason = quitReason;
            model.CreateTime = CreateTime;
            model.CreatePerson = CreatePerson;
            model.UpdateTime = UpdateTime;
            model.UpdatePerson = UpdatePerson;

            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    HR_PERSON per = getModel();
                    if (this.perSer.Update(per))
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
            if (this.txtCode.Text.Trim().Length == 0)
            {
                strErr = "编号不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>",strErr));
                this.txtCode.Focus();
                return false;
            }
            if (this.txtDepartment.Text.Trim().Length == 0)
            {
                strErr += "部门不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtDepartment.Focus();
                return false;
            }
            if (this.txtPosition.Text.Trim().Length == 0)
            {
                strErr += "岗位不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtPosition.Focus();
                return false;
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "姓名不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtName.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtBirthday.Text);
            }
            catch (Exception)
            {
                strErr += "出生年月日格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtBirthday.Focus();
                return false;
            }

            try
            {
                Convert.ToDateTime(txtOnBoardTime.Text);
            }
            catch (Exception)
            {                
                strErr += "入司日期格式错误！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtOnBoardTime.Focus();
                return false;              
            }
            try
            {
                Convert.ToDateTime(txtContractTime.Text);
            }
            catch (Exception)
            {
               strErr += "签订合同日期格式错误！\\n";
               base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
               this.txtContractTime.Focus();
               return false;
            }
            if (this.txtHuKou.Text.Trim().Length == 0)
            {
                strErr += "户口所在地不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtHuKou.Focus();
                return false;
            }
           
            if (this.txtIDCard.Text.Trim().Length == 0)
            {
                strErr += "身份证号码不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtIDCard.Focus();
                return false;
            }
           
            if (base.Request["Code"] != null)
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from HR_PERSON where Code='{0}' and ID<>{1}", this.txtCode.Text.Trim(), base.Request["Code"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('编号已经存在,请重新填写！');</script>");
                    this.txtCode.Focus();
                    return false;
                }
               
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from HR_PERSON where Code='{0}'", this.txtCode.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('编号已经存在,请重新填写！');</script>");
                    this.txtCode.Focus();
                    return false;
                }               
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Code"] != null)
                {
                    ddlUser.Visible = false;
                    btnSelect.Visible = false;
                    this.btnAdd.Visible = false;
                    HR_PERSON model = this.perSer.GetModel(Convert.ToInt32(base.Request["Code"]));
                   
                    this.txtCode.Text = model.Code;
                    this.txtDepartment.Text = model.Department;
                    this.txtPosition.Text = model.Position;
                    this.txtName.Text = model.Name;
                    if (model.Birthday!=null)
                    this.txtBirthday.Text = model.Birthday.Value.ToShortDateString();
                    this.ddlSex.Text = model.Sex;
                    this.txtEducationLevel.Text = model.EducationLevel;
                    this.txtEducationSchool.Text = model.EducationSchool;
                    this.txtMajor.Text = model.Major;
                    if (model.GraduationTime != null)
                        this.txtGraduationTime.Text = model.GraduationTime.Value.ToShortDateString();
                    if (model.OnBoardTime != null)
                        this.txtOnBoardTime.Text = model.OnBoardTime.Value.ToShortDateString();
                    if (model.BeNormalTime != null)
                        this.txtBeNormalTime.Text = model.BeNormalTime.Value.ToShortDateString();
                    if (model.ContractTime != null)
                        this.txtContractTime.Text = model.ContractTime.Value.ToShortDateString();
                    if (model.ContractCloseTime != null)
                        this.txtContractCloseTime.Text = model.ContractCloseTime.Value.ToShortDateString();
                    this.txtHuKou.Text = model.HuKou;
                    this.ddlMarriage.Text = model.Marriage;
                    this.txtIDCard.Text = model.IDCard;
                    this.txtMobilePhone.Text = model.MobilePhone;
                    this.txtHomePhone.Text = model.HomePhone;
                    this.txtHomeAddress.Text = model.HomeAddress;
                    this.txtEmailAddress.Text = model.EmailAddress;
                    if (model.CreateTime != null)
                    this.txtCreateTime.Text = model.CreateTime.ToString();
                    this.txtCreatePerson.Text = model.CreatePerson.ToString();
                    if (model.UpdateTime != null)
                    this.txtUpdateTime.Text = model.UpdateTime.ToString();
                    this.txtUpdatePerson.Text = model.UpdatePerson.ToString();
                    this.chkQuit.Checked  = bool.Parse(model.Quit.ToString());

                    System.Collections.Generic.List<string> perss = perSer.GetCreateUpdateInfo(Convert.ToInt32(base.Request["Code"]));
                    txtCreatePerson.Text=perss[0];
                    txtUpdatePerson.Text = perss[1];
                }
                else
                {
                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName(" And loginStatus='在职'");
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
