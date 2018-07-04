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
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class PostList : BasePage
    {
        private tb_PostService postSer = new tb_PostService();     

        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlUser.Text != "全部")
            {
                sql += string.Format(" and loginName ='{0}'", ddlUser.Text);
            }

            if (txtToPer.Text != "")
            {
                sql += string.Format(" and ToPer like '%{0}%'", txtToPer.Text);
            }
            if (txtPONO.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONO.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and tb_Post.PONO like '%{0}%'", txtPONO.Text.Trim());
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=tb_Post.PONo and AE IN(select LOGINNAME from tb_User where {0}))", where);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=tb_Post.PONO) ", ddlModel.Text);
            }
            if (txtWuLiu.Text != "")
            {
                sql += string.Format(" and WuliuName like '%{0}%'", txtWuLiu.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            if (txtAddress.Text != "")
            {
                sql += string.Format(" and PostAddress like '%{0}%'", txtAddress.Text);
            }
            
            if (ddlAE.Text == "-1")//显示所有用户
            {

            }
            else if (ddlAE.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                sql += string.Format(" and AE='{0}' ", ddlAE.SelectedItem.Text);
            }



            sql += string.Format(" and tb_Post.id in (select allE_id from tb_EForm where state='通过' and proid in (select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表') )");
            List<tb_Post> cars = this.postSer.GetListArray(sql);
            decimal total = 0;
            foreach (var model in cars)
            {
                if (model.Total != null)
                {
                    total += model.Total.Value;
                }
            }

            lblTotal.Text = total.ToString();
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
            this.gvList.DataBind();
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

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
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

            //this.CarSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/CarMaintenanceList.aspx";
            //base.Response.Redirect("~/ReportForms/CarMaintenance.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                List<tb_Post> poseModels = new List<tb_Post>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";


                List<VAN_OA.Model.User> aeList = new List<VAN_OA.Model.User>();
                bool showAll = true;
                if (QuanXian_ShowAll("公司邮寄快递汇总明细") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("公司邮寄快递汇总明细", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }
              
                if (showAll == true)
                {
                    aeList = userSer.getAllUserByPOList();
                    aeList.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList == true)
                {
                    aeList = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    aeList.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = 0 });
                }
                else
                {
                    var model = Session["userInfo"] as User;
                    aeList.Insert(0, model);
                }


                ddlAE.DataSource = aeList;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";
            }
        }
    }
}
