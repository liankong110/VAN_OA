using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;



namespace VAN_OA.Fin
{
    public partial class SpecCostList : BasePage
    {
        protected List<FIN_Property> propertyList = new List<FIN_Property>();
        FIN_PropertyService FIN_PropertyService = new FIN_PropertyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
            btnSave.Enabled = true;
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        private void Show()
        {
            var comModel = new Dal.BaseInfo.TB_CompanyService().GetModel(Convert.ToInt32(ddlCompany.Text));
            lblCompanyName.Text = comModel.ComName;
            lblSimpName.Text = comModel.ComSimpName;
            lblDate.Text = ddlYear.Text + ddlMonth.Text;
            lblAE.Text = ddlUser.SelectedItem.Text;
            propertyList = this.FIN_PropertyService.GetListArray_SpecCost(ddlYear.Text + ddlMonth.Text, ddlCompany.Text,ddlUser.Text);


            //DateTime fristDate = Convert.ToDateTime(ddlYear.Text+"-"+ddlMonth.Text+"-1");
            //DateTime fristDate_1 = Convert.ToDateTime(ddlYear.Text + "-1-1");
            //DateTime endDate = Convert.ToDateTime(ddlYear.Text + "-" + ddlMonth.Text + "-" + DateTime.DaysInMonth(fristDate.Year, fristDate.Month));
            //显示系统值
            var sql = string.Format(@"---显示=右面界面 加班记录管理中 日期从XX---YY(财年跨度) ，某一AE 的加班费 合计数
select  isnull(sum(Total),0) from tb_OverTime 
where   year(formTime)={0} 
and MONTH(formTime)<={1}
and guestDai ='{2}' and tb_OverTime.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='加班单') and state='通过')", ddlYear.Text, ddlMonth.Text, ddlUser.SelectedItem.Text);

            ViewState["OverTime"] = DBHelp.ExeScalar(sql);

            sql = string.Format(@"---显示=右面界面 加班记录管理中 日期从XX---YY(财年跨度) ，某一AE 的加班费 合计数
select  isnull(sum(Total),0) from tb_OverTime 
where   year(formTime)={0} 
and MONTH(formTime)={1}
and guestDai ='{2}' and tb_OverTime.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='加班单') and state='通过')", ddlYear.Text, ddlMonth.Text, ddlUser.SelectedItem.Text);

            ViewState["OverTimeMonth"] = DBHelp.ExeScalar(sql);
         
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }


        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            if (ddlCompany.Text != "-1")
            {
                where = string.Format(" and tb_User.CompanyCode =(select ComCode from TB_Company where id={0})", ddlCompany.Text);
            }
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
            user = userSer.getAllUserByPOList(where);
            user.Insert(0, new VAN_OA.Model.User() { LoginName = "选择", Id = -1 });

            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "Id";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
                TB_CompanyService comSer = new TB_CompanyService();

                var comList = comSer.GetListArray("");
                comList.Insert(0, new TB_Company() { ComName = "选择", Id = -1 });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='个性费用-可修改保存'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "个性费用-可修改保存") == false)
                {
                    btnSave.Visible = false;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var propertyList = this.FIN_PropertyService.GetListArray(" MyProperty='个性'");
           List<FIN_SpecCost> commCostList = new List<FIN_SpecCost>();
           foreach (var m in propertyList)
           {
               if (!string.IsNullOrEmpty(Request["pro_" + m.Id]))
               {
                   var newM = new FIN_SpecCost();
                   newM.CaiYear = ddlYear.Text + ddlMonth.Text;
                   newM.CompId = Convert.ToInt32(ddlCompany.Text);
                   newM.CostTypeId = m.Id;

                    if (CommHelp.VerifesToNum(Request["pro_" + m.Id].ToString()) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数值 格式错误！');</script>");
                        return;
                    }

                    newM.Total = Convert.ToDecimal(Request["pro_" + m.Id]);
                   newM.UserID =Convert.ToInt32(ddlUser.Text);
                   commCostList.Add(newM);
               }
           }
            FIN_SpecCostService commSer=new FIN_SpecCostService ();
            commSer.AddList(commCostList);

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.Text != "-1")
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }
    }
}
