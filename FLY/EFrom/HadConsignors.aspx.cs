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
    public partial class HadConsignors : BasePage
    {

        private A_ProInfoService proSer = new A_ProInfoService();
        public string Query = "QueryHadConsignors";

        private void select()
        {
            string sql = " 1=1 ";
            //QueryEForms
            QuerySession.QueryEForm QEForm = new VAN_OA.QuerySession.QueryEForm();
            if (ddlProType.SelectedItem!=null&& ddlProType.SelectedItem.Text != "")
            {
                sql += string.Format(" and proId={0}", ddlProType.SelectedItem.Value);
                QEForm.ProTypeId = Convert.ToInt32(ddlProType.SelectedItem.Value);
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and DoTime>='{0} 00:00:00'", txtFrom.Text); 
                QEForm.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and DoTime<='{0} 23:59:59'", txtTo.Text);
                QEForm.ToTime = txtTo.Text;
            }
            if (ddlState.SelectedItem.Text != "")
            {
                sql += string.Format(" and state='{0}'", ddlState.SelectedItem.Text);
                QEForm.State = ddlState.SelectedItem.Text;
            }

            if (txtApper.Text != "")
            {
                sql += string.Format(" and appPer_Name like '%{0}%'", txtApper.Text);
                QEForm.Auper = txtApper.Text;
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


            if (txtToPer.Text != "")
            {
                sql += string.Format(" and type1 like '%{0}%'", txtToPer.Text);
                //送货人
                QEForm.WeiTuo = txtToPer.Text;
            }
            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms = eformSer.GetListArray_MyApps_Consignor(sql, Convert.ToInt32(Session["currentUserId"]));
            Session[Query] = QEForm;
            AspNetPager1.RecordCount = allEForms.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = allEForms;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            select();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
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
                Session["backurl"] = "/EFrom/HadConsignors.aspx";
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

                    if (QEForm.Auper != "")
                    {

                        txtApper.Text = QEForm.Auper;
                    }

                    if (QEForm.E_No != "")
                    {
                        txtProNo.Text = QEForm.E_No;
                    }


                    if (QEForm.WeiTuo != "")
                    {
                        txtToPer.Text = QEForm.WeiTuo;
                    }
                    select();


                }
                else
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    List<tb_EForm> allEForms = eformSer.GetListArray_MyApps_Consignor("", Convert.ToInt32(Session["currentUserId"]));
                    AspNetPager1.RecordCount = allEForms.Count;
                    this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
                    this.gvList.DataSource = allEForms;
                    this.gvList.DataBind();
                }

            }
        }
    }
}
