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
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class DispatchList : BasePage
    {
        private Tb_DispatchListService dispatchSer = new Tb_DispatchListService();

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        public string GetLink(object obj)
        {
            if (obj == null)
            {
                return "ProId=12";
            }
            return "ProId=13";
        }

        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return ;
                }
                sql += string.Format(" and Tb_DispatchList.CreateTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if ( CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Tb_DispatchList.CreateTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Tb_DispatchList.GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Tb_DispatchList.POName like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtPONO.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONO.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and Tb_DispatchList.PONo like '%{0}%'", txtPONO.Text.Trim());
            } 
            if (ddlFuHao.Text != "-1" && !string.IsNullOrEmpty(txtTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('报销金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and ISNULL(BusTotal,0)+ISNULL(RepastTotal,0)+ISNULL(HotelTotal,0)+ISNULL(OilTotal,0)+ISNULL(GuoTotal,0)+ISNULL(PostTotal,0)+ISNULL(Tb_DispatchList.PoTotal,0)+ISNULL(OtherTotal,0) {0}{1}", ddlFuHao.Text, txtTotal.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and Tb_DispatchList.CardNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (ddlFundType.Text != "-1")
            {
                
                if (ddlFundType.Text == "0")//公交费
                {
                    sql += string.Format(" and BusTotal is not null and BusTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "1")//餐饮费
                {
                    sql += string.Format(" and RepastTotal is not null and RepastTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "2")//住宿费
                {
                    sql += string.Format(" and HotelTotal is not null and HotelTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "3")//汽油补贴
                {
                    sql += string.Format(" and OilTotal is not null and OilTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "4")//过路费
                {
                    sql += string.Format(" and GuoTotal is not null and GuoTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "5")//邮寄费
                {
                    sql += string.Format(" and PostTotal is not null and PostTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "6")//小额采购
                {
                    sql += string.Format(" and Tb_DispatchList.PoTotal is not null and Tb_DispatchList.PoTotal>0", ddlFundType.Text);
                }
                else if (ddlFundType.Text == "7")//其他费用
                {
                    sql += string.Format(" and OtherTotal is not null and OtherTotal>0", ddlFundType.Text);
                }
            }
            if (ddlCompany.Text != "-1")
            {
                sql += string.Format(" and TB_Company.ComCode='{0}'", ddlCompany.Text.Split(',')[2]);                
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }
            if (ddlState.Text != "全部")
            {
                sql += string.Format(" and Tb_DispatchList.state='{0}'", ddlState.Text);
            }
            if (!string.IsNullOrEmpty(txtKeyWords.Text))
            {
                sql += string.Format(@" and ([BusFromAddress] like '%{0}%'
or [BusFromAddress] like '%{0}%' or [BusToAddress] like '%{0}%' or  CONVERT(varchar(100), BusFromTime, 120) like '%{0}%' or CONVERT(varchar(100), BusToTime, 120) like '%{0}%'
or [RepastAddress] like '%{0}%' or [RepastTotal] like '%{0}%' or [RepastPerNum] like '%{0}%' or [RepastPers] like '%{0}%'
or [HotelAddress] like '%{0}%' or [HotelName] like '%{0}%' or [OilFromAddress] like '%{0}%' or [OilToAddress] like '%{0}%'
or [OilLiCheng] like '%{0}%' or [GuoBeginAddress] like '%{0}%' or [GuoToAddress] like '%{0}%' or [PostFromAddress] like '%{0}%'
or [PostToAddress] like '%{0}%' or [PoContext] like '%{0}%' or [OtherContext] like '%{0}%' or [BusRemark] like '%{0}%'
or [RepastRemark] like '%{0}%' or [HotelRemark] like '%{0}%' or [OilRemark] like '%{0}%' or [GuoRemark] like '%{0}%'
or [PostRemark] like '%{0}%' or Tb_DispatchList.[PoRemark] like '%{0}%' or [OtherRemark] like '%{0}%' or [PostNo] like '%{0}%'
or [PostCompany] like '%{0}%' or [PostContext] like '%{0}%' or [PostToPer] like '%{0}%' or [Post_No] like '%{0}%')", txtKeyWords.Text);
            }

            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            List<Tb_DispatchList> dispatchList = this.dispatchSer.GetListArrayReport(sql);

            lblTotal.Text = dispatchList.Sum(t => t.Total).ToString();

            AspNetPager1.RecordCount = dispatchList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = dispatchList;
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

           

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
           

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

                var user = new List<Model.User>();
                var userSer = new Dal.SysUserService();
                if (QuanXian_ShowAll("预期报销单列表") == false)
                {
                    ViewState["showAll"] = false;
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
                else
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }

                if (!NewShowAll_textName("预期报销单列表", "编辑"))
                {
                    gvList.Columns[0].Visible = false;
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                var dispatchList = new List<Tb_DispatchList>();
                gvList.DataSource = dispatchList;
                gvList.DataBind();
                
            }
        }
    }
}
