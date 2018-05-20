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
    public partial class Consignoring : BasePage
    {

        private A_ProInfoService proSer = new A_ProInfoService();
        public string Query = "QueryConsignoring";

        private void select()
        {
            string sql = " 1=1 ";
            //QueryEForms
            QuerySession.QueryEForm QEForm = new VAN_OA.QuerySession.QueryEForm();


            if (ddlProType.SelectedItem!=null&&ddlProType.SelectedItem.Text != "")
            {
                sql += string.Format(" and proId={0}", ddlProType.SelectedItem.Value);
                QEForm.ProTypeId = Convert.ToInt32(ddlProType.SelectedItem.Value);
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and appTime>='{0} 00:00:00'", txtFrom.Text);
                QEForm.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and appTime<='{0} 23:59:59'", txtTo.Text);
                QEForm.ToTime = txtTo.Text;
            }
            if (ddlState.SelectedItem.Text != "")
            {
                sql += string.Format(" and state='{0}'", ddlState.SelectedItem.Text);
                QEForm.State = ddlState.SelectedItem.Text;
            }



            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and e_No like '%{0}%'", txtProNo.Text.Trim());
                //送货人
                QEForm.E_No = txtProNo.Text.Trim();
            }

          //  sql += string.Format(" and appPer={0} and state='执行中'", Session["currentUserId"].ToString());
            sql += string.Format(" and state='执行中'");

            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms = eformSer.GetListArray_Consignor(sql, Convert.ToInt32(Session["currentUserId"]));
            Session[Query] = QEForm;
            this.gvList.DataSource = allEForms;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {

            select();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;

            select();

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
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.gvList.DataKeys[e.NewEditIndex].Value.ToString()));


            if (eform != null)
            {
                Session["backurl"] = "/EFrom/Consignoring.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), gvList.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    Response.Redirect(url);
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                WebQuerySessin Sess = new WebQuerySessin(Query);

                A_ProInfoService proSer = new A_ProInfoService();
                List<A_ProInfo> pros = this.proSer.GetListArray("");
                pros.Insert(0, new A_ProInfo());
                ddlProType.DataSource = pros;
                ddlProType.DataBind();

                ddlProType.DataTextField = "pro_Type";
                ddlProType.DataValueField = "pro_Id";

                  //加载SESSION中的数据
                if (Session[Query] != null)
                {
                    //QueryEForms
                    QuerySession.QueryEForm QEForm = Session[Query] as QuerySession.QueryEForm;
                    if (QEForm == null)
                    {
                        return;
                    }

                    if (QEForm.ProTypeId != 0)
                    {
                        try
                        {
                            ddlProType.Text = QEForm.ProTypeId.ToString();
                        }
                        catch (Exception)
                        {


                        }

                    }
                    if (QEForm.FromTime != "")
                    {

                        txtFrom.Text = QEForm.FromTime;

                    }

                    if (QEForm.ToTime != "")
                    {

                        txtTo.Text = QEForm.ToTime;
                    }
                    if (QEForm.State != "")
                    {
                        ddlState.Text = QEForm.State;
                    }

                    if (QEForm.E_No != "")
                    {
                        txtProNo.Text = QEForm.E_No;
                    }
                    select();


                }
                else
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    string sql = string.Format("  appPer={0} and state='执行中'", Session["currentUserId"].ToString());
                    List<tb_EForm> allEForms = eformSer.GetListArray_Consignor(sql, Convert.ToInt32(Session["currentUserId"]));

                    this.gvList.DataSource = allEForms;
                    this.gvList.DataBind();
                }

              

            }
        }
    }
}
