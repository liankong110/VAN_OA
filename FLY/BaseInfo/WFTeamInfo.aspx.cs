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
    public partial class WFTeamInfo : System.Web.UI.Page
    {
        private TeamInfoService teamInfoSer = new TeamInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TeamInfo where TeamLever='{0}'", txtTeamLever.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('姓名[{0}]，已经存在！');</script>", txtTeamLever.Text));
                        return;
                    }
                    TeamInfo model = getModel();
                    if (this.teamInfoSer.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFTeamInfoList.aspx");
        }

        private void Clear()
        {
            txtCardNo.Text = "";
            txtContractStartTime.Text = "";
            txtTeamLever.Text = "";
            txtTeamPersonCount.Text = "";
            txtPhone.Text = "";
        }


        public TeamInfo getModel()
        {
            TeamInfo model = new TeamInfo();
            model.TeamLever = txtTeamLever.Text;
            model.Sex = ddlSex.Text;
            model.CardNo = txtCardNo.Text;
            model.BrithdayYear =Convert.ToInt32(ddlBrithdayYear.Text);
            model.BrithdayMonth = Convert.ToInt32(ddlContract_Month.Text);
            model.ContractStartTime =Convert.ToDateTime(txtContractStartTime.Text);
            model.TeamPersonCount = Convert.ToInt32(txtTeamPersonCount.Text);
            model.Phone = txtPhone.Text;
           
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
                    string sqlCheck = string.Format("select count(*) from TeamInfo where TeamLever='{0}' AND ID<>{1}", txtTeamLever.Text, Request["Id"]); 
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('姓名[{0}]，已经存在！');</script>",
                           txtTeamLever.Text));
                        return;
                    }
                    TeamInfo model = getModel();
                    if (this.teamInfoSer.Update(model))
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
            if (this.txtTeamLever.Text.Trim().Length == 0)
            {
                strErr += "姓名不能为空！\\n";
            }
            //if (this.txtCardNo.Text.Trim().Length == 0)
            //{
            //    strErr += "身份证号不能为空！\\n";
            //}
            if (this.txtContractStartTime.Text.Trim().Length == 0)
            {
                strErr += "合作起始时间不能为空！\\n";
            }
            if (CommHelp.VerifesToDateTime(txtContractStartTime.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('合作起始时间 格式错误！');</script>");
                return false;
            }
            if (this.txtTeamPersonCount.Text.Trim().Length == 0)
            {
                strErr += "人员规模不能为空！\\n";
            }
            if (CommHelp.VerifesToNum(txtTeamPersonCount.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('人员规模 格式错误！');</script>");
                return false;
            }
            if (!string.IsNullOrEmpty(txtPhone.Text) && txtPhone.Text.Length != 11)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('手机号码 格式错误！');</script>");
                return false;
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
                for (int i = 1950; i < 2017; i++)
                {
                    ddlBrithdayYear.Items.Add(new ListItem { Text=i.ToString(),Value=i.ToString() });
                } 
                
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    TeamInfo model = this.teamInfoSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtCardNo.Text = model.CardNo;
                    txtContractStartTime.Text = model.ContractStartTime.ToString();
                    txtTeamLever.Text = model.TeamLever;
                    txtTeamPersonCount.Text = model.TeamPersonCount.ToString();
                    ddlSex.Text = model.Sex.ToString();
                    ddlBrithdayYear.Text = model.BrithdayYear.ToString();
                    ddlContract_Month.Text = model.BrithdayMonth.ToString();
                    txtPhone.Text = model.Phone;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        } 
        
    }
}
