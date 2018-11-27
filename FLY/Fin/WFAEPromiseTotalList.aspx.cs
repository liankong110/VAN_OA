using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.Fin
{
    public partial class WFAEPromiseTotalList : BasePage
    {

        Base_BusInfoService busService = new Base_BusInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAEPromiseTotal.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        private void Show()
        {
            string sql = "1=1";

            if (ddlCompany.Text != "-1")
            {
                sql += string.Format("  and CompanyCode='{0}'", ddlCompany.Text.Trim());
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format("  and AE='{0}'", ddlUser.Text.Trim());
            }

            sql += string.Format("  and YearNo={0}", ddlYear.Text.Trim());

            List<Base_BusInfo> busList = this.busService.GetListArray(sql);
            AspNetPager1.RecordCount = busList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = busList;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //判断项目有没有使用

           

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAEPromiseTotal.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<User> user = new List<User>();
                List<WFAEPromiseTotal> _list = new List<WFAEPromiseTotal>();
                this.gvList.DataSource = _list;
                this.gvList.DataBind();

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");

                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "-1", ComName = "全部" });

                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year - 1).ToString();

                if (NewShowAll_textName("销售指标", "结算明细表-查询全部") == false)
                {

                    var model = Session["userInfo"] as User;
                    var m = comList.Find(t => t.ComCode == model.CompanyCode);
                    comList = new List<TB_Company>();
                    comList.Add(m);

                    user.Insert(0, model);
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "LoginName";
                }
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            if (ddlCompany.Text != "-1")
            {
                where = string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text.Split(',')[2]);
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByPOList(where);
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
            }
            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "LoginName";
        }
    }
}
