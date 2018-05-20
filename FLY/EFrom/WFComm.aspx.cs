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
    public partial class WFComm : BasePage
    {

        private A_ProInfoService proSer = new A_ProInfoService();
        public string Query = "QueryWFComm";

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
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and appTime>='{0} 00:00:00'", txtFrom.Text);
                QEForm.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and appTime<='{0} 23:59:59'", txtTo.Text);
                QEForm.ToTime = txtTo.Text;
            }
            
            if (txtWating.Text != "")
            {
                sql += string.Format(" and toPer_Name like '%{0}%'", txtWating.Text);
                QEForm.WatingAuper = txtWating.Text;
            }

            if (txtSouhuo.Text.Trim() != "")
            {
                sql += string.Format(" and SongHuo like '%{0}%'", txtSouhuo.Text);
                //送货人
                QEForm.WeiTuo = txtSouhuo.Text;
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

            //if (txtWaiPai.Text.Trim() != "")
            //{
            //    sql += string.Format(" and OutDispater like '%{0}%'", txtWaiPai.Text);
            //}
            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms = eformSer.GetListArray_Eform_DeliverGoods_Dispatch(sql);
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
                Session["backurl"] = "/EFrom/WFComm.aspx";
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
                List<A_ProInfo> pros_1 = new List<A_ProInfo>();
                for (int i = 0; i < pros.Count; i++)
                {
                    if (pros[i].pro_Type == "外出送货单")
                    {
                        pros_1.Add(pros[i]);
                    }
                }

                pros_1.Insert(0, new A_ProInfo());
                ddlProType.DataSource = pros_1;
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
                   

                    if (QEForm.Auper != "")
                    {

                        txtWating.Text = QEForm.WatingAuper;
                    }

                    if (QEForm.WeiTuo != "")
                    {
                        //送货人
                        txtSouhuo.Text= QEForm.WeiTuo;
                      
                    }


                    if (QEForm.E_No != "")
                    { 
                        txtProNo.Text = QEForm.E_No;
                    }
                    select();


                }
                else
                {
                    txtFrom.Text = DateTime.Now.ToShortDateString();
                    txtTo.Text = DateTime.Now.ToShortDateString();


                    tb_EFormService eformSer = new tb_EFormService();
                    List<tb_EForm> allEForms = eformSer.GetListArray_Eform_DeliverGoods_Dispatch(string.Format(" 1=1 and appTime between '{0} 00:00:00' and '{0} 23:59:59'", DateTime.Now.ToShortDateString()));

                    this.gvList.DataSource = allEForms;
                    this.gvList.DataBind();
                }
               

            }
        }
    }
}
