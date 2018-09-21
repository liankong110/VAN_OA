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
using System.ComponentModel;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.EFrom
{
    public partial class MyEForms : BasePage
    {
        protected List<string> EFormNames = new List<string> {"项目订单","采购订单","采购订单检验","采购入库","采购退货","供应商付款单","供应商付款单（预付单转支付单）","供应商预付款单","销售出库","销售退货","销售发票","销售发票修改","销售发票删除" }; 
        protected string GetDemo(object allE_id, object proId)
        {
            var tempWform = allAllWform.FindAll(t => t.Id == (int)allE_id && t.myProId == (int)proId);
            Hashtable hs = new Hashtable();           
            string str = "";
            for (int i = 0; i < tempWform.Count; i++)
            {
                string pono = tempWform[i].PONo;
                if (!hs.Contains(pono))
                {
                    hs.Add(pono, pono);
                    if (i != tempWform.Count - 1)
                    {
                        pono += ",";
                    }
                    if (tempWform[i].IsSpecial)
                    {

                        str += "<span style='color:Red;'>" + pono.Trim(',') + "</span>,";

                    }
                    else
                    {
                        str += pono;

                    }
                   
                }
            }
            return str.Trim(',');

        }
        private A_ProInfoService proSer = new A_ProInfoService();

        protected List<View_AllEform> allAllWform = new List<View_AllEform>();

        private List<SupplierInvoice_Name> SupplierInvoice_Names = new List<SupplierInvoice_Name>();

        private void select()
        {
            string sql = " 1=1 ";
            //QueryEForms
            QuerySession.QueryEForm QEForm = new VAN_OA.QuerySession.QueryEForm();


            if (ddlProType.SelectedItem != null && ddlProType.SelectedItem.Text != "")
            {
                if (ddlProType.SelectedItem.Value == "-2")
                {
                    sql += string.Format(" and proId in (26,34)");
                }
                else if (ddlProType.SelectedItem.Value == "-3")
                {
                    sql += string.Format(" and proId in (31,32)");
                }
                else
                {
                    sql += string.Format(" and proId={0}", ddlProType.SelectedItem.Value);
                }

                QEForm.ProTypeId = Convert.ToInt32(ddlProType.SelectedItem.Value);
            }
            else
            {
                sql += " and pro_Type is not null"; 
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
                sql += string.Format(" and isnull(maxDoTime,tb_EForm_View.createTime)>='{0} 00:00:00' and isnull(maxDoTime,tb_EForm_View.createTime)<='{1} 23:59:59' ",
                    txtSPForm.Text, txtSPTo.Text);

            }
            if (txtSPForm.Text == "" && txtSPTo.Text != "")
            {
                sql += string.Format(" and isnull(maxDoTime,tb_EForm_View.createTime)<='{0} 23:59:59'",
                   txtSPTo.Text);
            }
            if (txtSPForm.Text != "" && txtSPTo.Text == "")
            {
                sql += string.Format(" and isnull(maxDoTime,tb_EForm_View.createTime)>='{0} 00:00:00' ",
                    txtSPForm.Text);
            }
            QEForm.SPForm = txtSPForm.Text;
            QEForm.SPTo = txtSPTo.Text;


            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and appPer_Name like '%{0}%'", ddlUser.SelectedItem.Text);

                QEForm.Apper = ddlUser.SelectedItem.Text;
            }

            if (txtAuper.Text != "")
            {
                sql += string.Format(" and id in (select e_Id  from EForms_View where audPer_Name like '%{0}%' or consignor_Name like '%{0}%')", txtAuper.Text);
                QEForm.Auper = txtAuper.Text;
            }

            if (txtWating.Text != "")
            {
                sql += string.Format(" and toPer_Name like '%{0}%'", txtWating.Text);
                QEForm.WatingAuper = txtWating.Text;
            }


            if (txtNo.Text.Trim() != "")
            {
                if (CheckProNo(txtNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and e_No like '%{0}%'", txtNo.Text.Trim());
                QEForm.E_No = txtNo.Text;
            }
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
) as A where A.myProId=tb_EForm_View.proId and A.Id=tb_EForm_View.allE_id and LastSupplier='{0}' )", txtPOSupplier.Text.Trim());
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
) as A where A.myProId=tb_EForm_View.proId and A.Id=tb_EForm_View.allE_id and LastSupplier like '%{0}%' )", txtPOSupplier.Text.Trim());
                }
            }
            #region 项目查询
            string ponoSql = "";
            if (ddlAEUsers.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.AE='{0}'", ddlAEUsers.SelectedItem.Text);
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text)==false)
                {
                    return;
                }
                ponoSql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
                QEForm.PONO = txtPONo.Text;
               
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                ponoSql += string.Format(" and CG_POOrder.AE IN(select LOGINNAME from tb_User where {0})", where);
            }
            if (txtGuestName.Text.Trim() != "")
            {
                ponoSql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
                QEForm.GuestName = txtGuestName.Text.Trim();
            }
            if (CheckBox1.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue='{0}'", CheckBox1.Text);
            }
            if (CheckBox2.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue2='{0}'", CheckBox2.Text);
            }
            if (CheckBox3.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue3='{0}'", CheckBox3.Text);
            }
            if (CheckBox4.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue4='{0}'", CheckBox4.Text);
            }

            if (CheckBox5.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue5='{0}'", CheckBox5.Text);
            }

            if (CheckBox6.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue6='{0}'", CheckBox6.Text);
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
            if (ponoSql != "")
            {
                sql += string.Format(" and EXISTS (SELECT 1 FROM CG_CAI_POOrder_View as CG_POOrder left join View_AllEform on CG_POOrder.pono=View_AllEform.pono  ");
                if (ddlPrice.Text != "-1")
                {
                    sql += " left join Order_ToInvoice_1 on CG_POOrder.PONo=Order_ToInvoice_1.PONo ";
                }

                sql += string.Format("   WHERE View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id {0})", ponoSql);

            }

            #endregion
       
            Session["QueryEForms"] = QEForm;
            tb_EFormService eformSer = new tb_EFormService();
            PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
          
            List<tb_EForm> allEForms = eformSer.GetListArray_2_Page(sql , page);
            AspNetPager1.RecordCount = page.TotalCount;
           
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
            //this.gvList.PageIndex = e.NewPageIndex;
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
                        var supplierNames = SupplierInvoice_Names.Where(t => t.Type == "1" && t.Id == ef.allE_id).Select(t => t.LastSupplier).ToList<string>();
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
                }
                if (gvList.Columns[1].Visible)
                {
                    if (EFormNames.Contains(ef.ProTyleName))
                    {
                        ImageButton btnDel = e.Row.FindControl("btnDel") as ImageButton;
                        btnDel.Enabled = false;
                        btnDel.BackColor = System.Drawing.Color.Red;
                    }
                }

            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEForms.aspx";
                string type = eform.ProTyleName.ToString();
                if (eformSer.DeleteAllEFormData(eform.proId.ToString(), eform.allE_id.ToString(), gvList.DataKeys[e.RowIndex].Value.ToString(), type, eform.E_No))
                {
                    select();
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败！');</script>");
                }


            }


        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.gvList.DataKeys[e.NewEditIndex].Value.ToString()));


            if (eform != null)
            {
                Session["backurl"] = "/EFrom/MyEForms.aspx";
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), gvList.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    Response.Redirect(url);
                    // Response.Redirect(string.Format("<script language='javascript'>window.open('EFrom/A_ProInfoList.aspx','_blank');</script>"));
                    // Response.Write("<script language='javascript'>window.open(\"" +url+ "\",\"_blank\");</script>");
                    //Page.RegisterStartupScript("ServiceManHistoryButtonClick", "<script>window.open('" + url + "');</script>"); 


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
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User { Id = -1, LoginName = "全部" });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
                var aeUsers = userSer.getAllUserByPOList();
                aeUsers.Insert(0, new VAN_OA.Model.User { Id = -1, LoginName = "全部" });
                ddlAEUsers.DataSource = aeUsers;
                ddlAEUsers.DataBind();
                ddlAEUsers.DataTextField = "LoginName";
                ddlAEUsers.DataValueField = "Id";

                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {
                        gvList.Columns[1].Visible = false;
                    }
                }
                #endregion


                WebQuerySessin Sess = new WebQuerySessin("QueryEForms");

                gvList.PagerSettings.Mode = PagerButtons.NumericFirstLast;


                A_ProInfoService proSer = new A_ProInfoService();
                List<A_ProInfo> pros = this.proSer.GetListArray("");
                pros.Insert(0, new A_ProInfo());

                pros.Insert(pros.FindIndex(t => t.pro_Type == "销售发票") , new A_ProInfo { pro_Id=-2, pro_Type= "销售发票+销售发票修改" });
                pros.Insert(pros.FindIndex(t => t.pro_Type == "销售发票") - 1, new A_ProInfo { pro_Id = -3, pro_Type = "供应商付款单+供应商预付款单" });
                ddlProType.DataSource = pros;
                ddlProType.DataBind();

                ddlProType.DataTextField = "pro_Type";
                ddlProType.DataValueField = "pro_Id";

                //加载SESSION中的数据
                if (Session["QueryEForms"] != null)
                {
                    //QueryEForms
                    QuerySession.QueryEForm QEForm = Session["QueryEForms"] as QuerySession.QueryEForm;
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

                    if (QEForm.Apper != "")
                    {
                        foreach (ListItem m in ddlUser.Items)
                        {
                            if (m.Text == QEForm.Apper)
                            {
                                m.Selected = true;
                                break;
                            }
                        }
                        //ddlUser.SelectedItem.Text = QEForm.Apper;
                    }

                    if (QEForm.Auper != "")
                    {
                        txtAuper.Text = QEForm.Auper;
                    }

                    if (QEForm.WatingAuper != "")
                    {
                        txtWating.Text = QEForm.WatingAuper;
                    }

                    if (QEForm.E_No != "")
                    {
                        txtNo.Text = QEForm.E_No;
                    }
                    if (QEForm.PONO != "")
                    {
                        txtPONo.Text = QEForm.PONO;
                    }
                    if (QEForm.GuestName != "")
                    {
                        txtGuestName.Text = QEForm.GuestName;
                    }


                    txtSPForm.Text = QEForm.SPForm;
                    txtSPTo.Text = QEForm.SPTo;
                    select();


                }
                else
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    List<tb_EForm> allEForms = new List<tb_EForm>();// eformSer.GetListArray_1("");

                    this.gvList.DataSource = allEForms;
                    this.gvList.DataBind();

                }

            }
        }


    }
}
