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
    public partial class MyEFormsTodo_New : BasePage
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
                sql += string.Format(" and isnull(e_LastTime,table2.createTime)>='{0} 00:00:00' and isnull(maxDoTime,table2.createTime)<='{1} 23:59:59' ",
                    txtSPForm.Text, txtSPTo.Text);

            }
            if (txtSPForm.Text == "" && txtSPTo.Text != "")
            {
                sql += string.Format(" and isnull(e_LastTime,table2.createTime)<='{0} 23:59:59'",
                   txtSPTo.Text);
            }
            if (txtSPForm.Text != "" && txtSPTo.Text == "")
            {
                sql += string.Format(" and isnull(e_LastTime,table2.createTime)>='{0} 00:00:00' ",
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
            if (!string.IsNullOrEmpty(txtPOSupplier.Text.Trim()))
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(@" and proId in (31,32,33) and exists(select 1 from (select CAI_POCai.LastSupplier,TB_SupplierAdvancePayments.Id,32 as myProId  from TB_SupplierAdvancePayments
left join CAI_POCai on TB_SupplierAdvancePayments.CaiIds=CAI_POCai.Ids
union all
select TB_SupplierInvoice.LastSupplier,TB_SupplierInvoice.Id,31 as myProId from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
where IsYuFu=0
union all
select  TB_SupplierInvoice.LastSupplier,TB_SupplierInvoice.Id,33 as myProId  from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
where IsYuFu=1
) as A where A.myProId=table2.proId and A.Id=table2.allE_id and LastSupplier='{0}' )", txtPOSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(@" and proId in (31,32,33) and exists(select 1 from (select CAI_POCai.LastSupplier,TB_SupplierAdvancePayments.Id,32 as myProId  from TB_SupplierAdvancePayments
left join CAI_POCai on TB_SupplierAdvancePayments.CaiIds=CAI_POCai.Ids
union all
select TB_SupplierInvoice.LastSupplier,TB_SupplierInvoice.Id,31 as myProId from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
where IsYuFu=0
union all
select  TB_SupplierInvoice.LastSupplier,TB_SupplierInvoice.Id,33 as myProId  from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
where IsYuFu=1
) as A where A.myProId=table2.proId and A.Id=table2.allE_id and LastSupplier like '%{0}%' )", txtPOSupplier.Text.Trim());
                }
            }
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
            if (ddlClose.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsClose={0} ", ddlClose.Text);
            }
            if (ddlIsSelect.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsSelected={0} ", ddlIsSelect.Text);
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.JieIsSelected={0} ", ddlJieIsSelected.Text);
            }
            if (ddlIsSpecial.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsSpecial={0} ", ddlIsSpecial.Text);
            }

            if (ddlHanShui.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsPoFax={0} ", ddlHanShui.Text);
            }
            if (ddlPrice.Text == "1")
            {
                ponoSql += " and Order_ToInvoice_1.POTotal-isnull(TuiTotal,0)>0";
            }
            if (ddlPrice.Text == "0")
            {
                ponoSql += " and Order_ToInvoice_1.POTotal-isnull(TuiTotal,0)=0";
            }

            string poSQL = "";
            if (ponoSql != "")
            {
                poSQL += string.Format(" and EXISTS (SELECT 1 FROM CG_CAI_POOrder_View as CG_POOrder left join View_AllEform on CG_POOrder.pono=View_AllEform.pono  ");
                if (ddlPrice.Text != "-1")
                {
                    poSQL += " left join Order_ToInvoice_1 on CG_POOrder.PONo=Order_ToInvoice_1.PONo ";
                }

                poSQL += string.Format("   WHERE View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id {0})", ponoSql);

            }

            #endregion
            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms = eformSer.GetListArray_ToDo(sql, poSQL, Convert.ToInt32(Session["currentUserId"]));
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


            //proId 进行分类管理
            // 1：常用，（就是除 项目订单、采购订单、预付款转支付单、销售发票修改、销售发票删除、用车明细表之外的所有单据），查询条件按原来 + 上述增加的一行条件。
            var changyong_allEForms = allEForms.FindAll(t => t.proId != 19 && t.proId != 20 && t.proId != 26 && t.proId != 33 && t.proId != 34 && t.proId != 37 && t.proId != 5);
            //2：项目订单及采购，包含项目订单和采购订单，查询条件，查询条件按原来 + 上述增加的一行条件。。
            var pono_Cai_allEForms = allEForms.FindAll(t => t.proId == 19 || t.proId == 20);
            //3：预付款转支付单，如图3，查询条件按原来 + 上述增加的一行条件。。
            var yuZhuan_allEForms = allEForms.FindAll(t => t.proId == 33);
            //4：销售发票，包含销售发票，销售发票修改、销售发票删除，查询条件按原来 + 上述增加的一行条件。。
            var xiaoShou_allEForms = allEForms.FindAll(t => t.proId == 26 || t.proId == 34 || t.proId == 37);
            //5：用车明细表，查询条件按原来 + 上述增加的一行条件。
            var yongChe_allEForms = allEForms.FindAll(t => t.proId == 5);

            Session[Query] = QEForm;
             
         
            this.gvList.DataSource = changyong_allEForms;
            this.gvList.DataBind();

            this.GridView2.DataSource = pono_Cai_allEForms;
            this.GridView2.DataBind();

            this.GridView3.DataSource = yuZhuan_allEForms;
            this.GridView3.DataBind();

            this.GridView4.DataSource = xiaoShou_allEForms;
            this.GridView4.DataBind();

            this.GridView5.DataSource = yongChe_allEForms;
            this.GridView5.DataBind();

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
           
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
                Session["backurl"] = "/EFrom/MyEFormsTodo_New.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), gvList.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    GetTabIndex();
                    Response.Redirect(url);
                }
            }
        }

        private void GetTabIndex()
        {
            Session["TabIndex"]= TabContainer1.ActiveTabIndex;
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.GridView2.DataKeys[e.NewEditIndex].Value.ToString()));
            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEFormsTodo_New.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), GridView2.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    GetTabIndex();
                    Response.Redirect(url);
                }
            }
        }

        protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.GridView3.DataKeys[e.NewEditIndex].Value.ToString()));
            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEFormsTodo_New.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), GridView3.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    GetTabIndex();
                    Response.Redirect(url);
                }
            }
        }

        protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.GridView4.DataKeys[e.NewEditIndex].Value.ToString()));
            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEFormsTodo_New.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), GridView4.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    GetTabIndex();
                    Response.Redirect(url);
                }
            }
        }
        protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.GridView5.DataKeys[e.NewEditIndex].Value.ToString()));
            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEFormsTodo.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), GridView5.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    GetTabIndex();
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

                if (Session["TabIndex"] != null)
                {
                    TabContainer1.ActiveTabIndex = int.Parse( Session["TabIndex"].ToString());
                }
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
                    //tb_EFormService eformSer = new tb_EFormService();
                    //List<tb_EForm> allEForms = eformSer.GetListArray_ToDo("", Convert.ToInt32(Session["currentUserId"]));

                    //string allIds = "", proIds = "", fukuandan = "0,", yufukuandan = "0,";
                    //for (int i = 0; i < allEForms.Count; i++)
                    //{
                    //    allIds += allEForms[i].allE_id.ToString() + ",";
                    //    proIds += allEForms[i].proId.ToString() + ",";

                    //    if (allEForms[i].ProTyleName == "供应商付款单")
                    //    {
                    //        fukuandan += allEForms[i].allE_id + ",";
                    //    }
                    //    if (allEForms[i].ProTyleName == "供应商预付款单")
                    //    {
                    //        yufukuandan += allEForms[i].allE_id + ",";
                    //    }
                    //}

                    //allIds = allIds.Trim(',');
                    //proIds = proIds.Trim(',');

                    //fukuandan = fukuandan.Trim(',');
                    //yufukuandan = yufukuandan.Trim(',');


                    //if (fukuandan != "" || yufukuandan != "")
                    //{
                    //    var superSer = new TB_SupplierAdvancePaymentService();
                    //    SupplierInvoice_Names = superSer.GetSupplierName(yufukuandan, fukuandan);
                    //}


                    //if (allIds != "")
                    //{
                    //    allAllWform = eformSer.GetView_AllEformList(proIds, allIds);
                    //}

                    //AspNetPager1.RecordCount = allEForms.Count;
                    //this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
                    //this.gvList.DataSource = allEForms;
                    //this.gvList.DataBind();

                    select();
                }

            }
        }
    }
}
