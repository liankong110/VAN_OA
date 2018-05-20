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
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Dal.BaseInfo;
namespace VAN_OA.EFrom
{
    public partial class MyEFormsTodo : BasePage
    {

        private A_ProInfoService proSer = new A_ProInfoService();

        private List<View_AllEform> allAllWform = new List<View_AllEform>();

        private List<SupplierInvoice_Name> SupplierInvoice_Names = new List<SupplierInvoice_Name>();


        public string Query = "QueryEFormToDo";

        private void select()
        {
            string sql = " 1=1 ";
            //QueryEForms
            QuerySession.QueryEForm QEForm = new VAN_OA.QuerySession.QueryEForm();

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtSPForm.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtSPForm.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtSPTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtSPTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批时间 格式错误！');</script>");
                    return;
                }
            }

            if (txtSPForm.Text != "" && txtSPTo.Text != "")
            {
                sql += string.Format(" and isnull(maxDoTime,table2.createTime)>='{0} 00:00:00' and isnull(maxDoTime,table2.createTime)<='{1} 23:59:59' ",
                    txtSPForm.Text, txtSPTo.Text);

            }
            if (txtSPForm.Text == "" && txtSPTo.Text != "")
            {
                sql += string.Format(" and isnull(maxDoTime,table2.createTime)<='{0} 23:59:59'",
                   txtSPTo.Text);
            }
            if (txtSPForm.Text != "" && txtSPTo.Text == "")
            {
                sql += string.Format(" and isnull(maxDoTime,table2.createTime)>='{0} 00:00:00' ",
                    txtSPForm.Text);
            }
            QEForm.SPForm = txtSPForm.Text;
            QEForm.SPTo = txtSPTo.Text;

            if (ddlProType.SelectedItem!=null&&ddlProType.SelectedItem.Text != "")
            {
                sql += string.Format(" and proId={0}", ddlProType.SelectedItem.Value);
                QEForm.ProTypeId = Convert.ToInt32(ddlProType.SelectedItem.Value);
            }
            if (txtFrom.Text != "")
            {
                sql += string.Format(" and appTime>='{0} 00:00:00'", txtFrom.Text);
                QEForm.FromTime = txtFrom.Text;
            }

            if (txtTo.Text != "")
            {
                sql += string.Format(" and appTime<='{0} 23:59:59'", txtTo.Text);
                QEForm.ToTime = txtTo.Text;
            }
            

            if (txtApper.Text != "")
            {
                sql += string.Format(" and AppPer_Name like '%{0}%'", txtApper.Text);
                QEForm.Apper = txtApper.Text;
            }


            //sql += string.Format(" and toPer='{0}'", Session["LoginName"].ToString());



            if (txtWeiTuo.Text != "")
            {
                sql += string.Format(" and (toPer like '%{0}%' and type1='委托')", txtWeiTuo.Text);
                QEForm.WeiTuo = txtWeiTuo.Text;
            }
            sql += string.Format(" and state='执行中'");

            if (txtNo.Text.Trim() != "")
            {
                if (CheckProNo(txtNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and e_No like '%{0}%'", txtNo.Text.Trim());
                QEForm.E_No = txtNo.Text.Trim();
            }


            //if (txtNo.Text != "")
            //{
            //    if (CheckProNo(txtNo.Text) == false)
            //    {
            //        return;
            //    }
            //    sql += string.Format(" and e_No like '%{0}%'", txtNo.Text);
            //    QEForm.E_No = txtNo.Text;
            //}
            #region 项目查询
            string ponoSql = "";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                ponoSql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
                QEForm.PONO = txtPONo.Text;
            }
            if (ddlAEUsers.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.AE='{0}'", ddlAEUsers.SelectedItem.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                ponoSql += string.Format(" and CG_POOrder.AE IN(select LOGINNAME from tb_User where {0})", where);
            }
            if (ponoSql != "")
            {
                sql += string.Format(" and EXISTS (SELECT 1 FROM CG_CAI_POOrder_View as CG_POOrder left join View_AllEform on CG_POOrder.pono=View_AllEform.pono  ");


                sql += string.Format("   WHERE View_AllEform.myProId=table2.proId and View_AllEform.Id=table2.allE_id {0})", ponoSql);

            }

            #endregion
            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms = eformSer.GetListArray_ToDo(sql, Convert.ToInt32(Session["currentUserId"]));
            string allIds = "", proIds = "",fukuandan="0,",yufukuandan="0,";
            for (int i = 0; i < allEForms.Count ; i++)
            {
                allIds += allEForms[i].allE_id.ToString() + ",";
                proIds += allEForms[i].proId.ToString() + ",";

                if (allEForms[i].ProTyleName == "供应商付款单")
                {
                    fukuandan += allEForms[i].allE_id +",";
                }
                if (allEForms[i].ProTyleName == "供应商预付款单")
                {
                    yufukuandan += allEForms[i].allE_id + ",";
                }
            }

            allIds = allIds.Trim(',');
            proIds = proIds.Trim(',');

            fukuandan = fukuandan.Trim(',');
            yufukuandan = yufukuandan.Trim(',');


            if (fukuandan != "" || yufukuandan!="")
            {
                var superSer = new TB_SupplierAdvancePaymentService();
                SupplierInvoice_Names = superSer.GetSupplierName(yufukuandan, fukuandan);
            }


            if (allIds != "")
            {
                allAllWform = eformSer.GetView_AllEformList(proIds, allIds);
            }


          
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

                var ef = e.Row.DataItem as tb_EForm;
                if (ef != null)
                {

                    if (ef.ProTyleName == "供应商付款单")
                    {
                        var supplierNames= SupplierInvoice_Names.Where(t=>t.Type=="1"&&t.Id==ef.allE_id).Select(t=>t.LastSupplier).ToList<string>();
                        if (supplierNames.Count > 0)
                        {
                            Label lblSupplierName = e.Row.FindControl("lblSupplierName") as Label;
                            lblSupplierName.Text = supplierNames[0];
                        }
                    }
                    else if (ef.ProTyleName == "供应商预付款单")
                    {
                        var supplierNames = SupplierInvoice_Names.Where(t => t.Type == "0" && t.Id == ef.allE_id).Select(t => t.LastSupplier).ToList<string>();
                        if (supplierNames.Count > 0)
                        {
                            Label lblSupplierName = e.Row.FindControl("lblSupplierName") as Label;
                            lblSupplierName.Text = supplierNames[0];
                        }
                    }

                    Hashtable hs = new Hashtable();
                    var ponoString = "";
                    var PONOs = allAllWform.Where(t => t.Id == ef.allE_id && t.myProId == ef.proId).Select(t => t.PONo).ToList<string>();
                    foreach (var p in PONOs)
                    {
                        if (!hs.Contains(p))
                        {
                            ponoString += p + "/";
                            hs.Add(p, p);
                        }

                    }
                    ef.E_Remark = ponoString.Trim('/');
                    Label lblPrice2 = e.Row.FindControl("lblE_Remark") as Label;
                    lblPrice2.Text = ponoString.Trim('/');
                }


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
                Session["backurl"] = "/EFrom/MyEFormsTodo.aspx";
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
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                var aeUsers = userSer.getAllUserByPOList();
                aeUsers.Insert(0, new VAN_OA.Model.User { Id = -1, LoginName = "全部" });
                ddlAEUsers.DataSource = aeUsers;
                ddlAEUsers.DataBind();
                ddlAEUsers.DataTextField = "LoginName";
                ddlAEUsers.DataValueField = "Id";

                if (Session["printUrl"] != null)
                {
                    string url = Session["printUrl"].ToString();
                    Session["printUrl"] = null;
                    Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
                }

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
                    

                    if (QEForm.Apper != "")
                    {

                        txtApper.Text = QEForm.Apper;
                    }

                    if (QEForm.WeiTuo != "")
                    {
                        txtWeiTuo.Text = QEForm.WeiTuo;
                    }
                    if (QEForm.E_No != "")
                    {
                        txtNo.Text = QEForm.E_No;
                    }

                    if (QEForm.E_No != "")
                    {
                        txtNo.Text = QEForm.E_No;
                    }

                    select();


                }
                else
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    List<tb_EForm> allEForms = eformSer.GetListArray_ToDo("", Convert.ToInt32(Session["currentUserId"]));

                    string allIds = "", proIds = "", fukuandan = "0,", yufukuandan = "0,";
                    for (int i = 0; i < allEForms.Count; i++)
                    {
                        allIds += allEForms[i].allE_id.ToString() + ",";
                        proIds += allEForms[i].proId.ToString() + ",";

                        if (allEForms[i].ProTyleName == "供应商付款单")
                        {
                            fukuandan += allEForms[i].allE_id + ",";
                        }
                        if (allEForms[i].ProTyleName == "供应商预付款单")
                        {
                            yufukuandan += allEForms[i].allE_id + ",";
                        }
                    }

                    allIds = allIds.Trim(',');
                    proIds = proIds.Trim(',');

                    fukuandan = fukuandan.Trim(',');
                    yufukuandan = yufukuandan.Trim(',');


                    if (fukuandan != "" || yufukuandan != "")
                    {
                        var superSer = new TB_SupplierAdvancePaymentService();
                        SupplierInvoice_Names = superSer.GetSupplierName(yufukuandan, fukuandan);
                    }


                    if (allIds != "")
                    {
                        allAllWform = eformSer.GetView_AllEformList(proIds, allIds);
                    }

                    AspNetPager1.RecordCount = allEForms.Count;
                    this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
                    this.gvList.DataSource = allEForms;
                    this.gvList.DataBind();

                  
                }

            }
        }
    }
}
