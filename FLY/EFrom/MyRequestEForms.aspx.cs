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
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.BaseInfo;
namespace VAN_OA.EFrom
{
    public partial class MyRequestEForms : BasePage
    {
        protected List<View_AllEform> allAllWform = new List<View_AllEform>();
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
        private List<SupplierInvoice_Name> SupplierInvoice_Names = new List<SupplierInvoice_Name>();

        private void select()
        {

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

            string sql = " 1=1 ";
            //QueryEForms
            QuerySession.QueryEForm QEForm = new VAN_OA.QuerySession.QueryEForm();
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
            if (ddlState.SelectedItem.Text != "")
            {
                sql += string.Format(" and state='{0}'", ddlState.SelectedItem.Text);
                QEForm.State = ddlState.SelectedItem.Text;
            }

            if (txtNo.Text.Trim() != "")
            {
                if (CheckProNo(txtNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and e_No like '%{0}%'", txtNo.Text.Trim());
                QEForm.E_No = txtNo.Text.Trim();
            }
           


             
            //sql += string.Format(" and appPer={0}",Session["currentUserId"].ToString());

            string SuccessCai = string.Format(@"select id from tb_EForm where  tb_EForm.alle_id in (select id from  TB_POOrder where caigou='{0}')
and  proid=(select pro_Id from A_ProInfo where pro_Type='订单报批表')", base.Session["LoginName"].ToString());

            sql += string.Format(" and (appPer={0}  or id in ({1}))", Session["currentUserId"].ToString(), SuccessCai);

            if (Request["Type"] != null )
            {
                string type = "";
                if (Request["Type"] != null)
                { 
                    type = Request["Type"].ToString();
                }
               
              
                if (type == "Success")
                {
                    string Success = string.Format(@"select id from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='通过' and appPer={0} and MaxTime between '{1} 00:00:00' and '{1} 23:59:59'", Session["currentUserId"], DateTime.Now.ToShortDateString());
                    sql += string.Format(@" and id in ({0})", Success);
                   
                }

                if (type == "SuccessCai")
                {
//                    string Success = string.Format(@"select id from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
//                                          where state='通过' and appPer={0} and MaxTime between '{1} 00:00:00' and '{1} 23:59:59'", Session["currentUserId"], DateTime.Now.ToShortDateString());
                    string Success = string.Format(@"select id from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='通过'  and MaxTime between '{1} 00:00:00' and '{1} 23:59:59' 
                                    and  tb_EForm.alle_id in (select id from  TB_POOrder where caigou='{0}')
and  proid=(select pro_Id from A_ProInfo where pro_Type='订单报批表')", base.Session["LoginName"].ToString(), DateTime.Now.ToShortDateString());


                    sql += string.Format(@" and id in ({0})", Success);

                }


                if (type == "Fail")
                {
                    string Fail = string.Format(@"select id from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='不通过' and appPer={0} and MaxTime between '{1} 00:00:00' and '{1} 23:59:59'", Session["currentUserId"], DateTime.Now.ToShortDateString());
                    sql += string.Format(@" and id in ({0})", Fail);

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
                QEForm.PONO = txtPONo.Text.Trim();
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                ponoSql += string.Format(" and CG_POOrder.AE IN(select LOGINNAME from tb_User where {0})", where);
            }
            if (ponoSql != "")
            {
                sql += string.Format(" and EXISTS (SELECT 1 FROM CG_CAI_POOrder_View as CG_POOrder left join View_AllEform on CG_POOrder.pono=View_AllEform.pono  ");
               

                sql += string.Format("   WHERE View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id {0})", ponoSql);

            }

            #endregion
            tb_EFormService eformSer = new tb_EFormService();
            List<tb_EForm> allEForms;
            //if (AspNetPager1.CurrentPageIndex == 1)
            //{
                allEForms = eformSer.GetListArray_1(sql);
            //    ViewState["myList"] = allEForms;
            //}
            //else
            //{
            //    allEForms=ViewState["myList"] as List<tb_EForm>;
            //}
            Session["QueryRequestEForms"] = QEForm;
            AspNetPager1.RecordCount = allEForms.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            string allIds = "", proIds = "", fukuandan = "0,", yufukuandan = "0,";
            for (int i = gvList.PageIndex * 10; i < (gvList.PageIndex + 1) * 10; i++)
            {
                if (i >= allEForms.Count)
                {
                    break;
                }
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
            }
            Label LBL = e.Row.FindControl("lblstate") as Label;
            if (LBL != null)
            {
                if (LBL.Text.Trim() == "不通过")
                {
                    LBL.ForeColor = System.Drawing.Color.Red;
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
                if (Request["Type"] != null)
                {
                    Session["backurl"] = "/EFrom/MyRequestEForms.aspx?Type=" + Request["Type"];
                }
                else
                {
                    Session["backurl"] = "/EFrom/MyRequestEForms.aspx";
                    
                }
                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(eform.proId.ToString(), eform.allE_id.ToString(), gvList.DataKeys[e.NewEditIndex].Value.ToString(), type);

                if (url != "")
                {
                    Response.Redirect(url);
                    //Response.Redirect(string.Format("<script>window.open('{0}','_blank')</script>",url));
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

                //清除Session
                WebQuerySessin Sess = new WebQuerySessin("QueryRequestEForms");

                gvList.PagerSettings.Mode = PagerButtons.NumericFirstLast;

                A_ProInfoService proSer = new A_ProInfoService();
                List<A_ProInfo> pros = this.proSer.GetListArray("");
                pros.Insert(0, new A_ProInfo());
                ddlProType.DataSource = pros;
                ddlProType.DataBind();

                ddlProType.DataTextField = "pro_Type";
                ddlProType.DataValueField = "pro_Id";


                //加载SESSION中的数据
                if (Session["QueryRequestEForms"] != null)
                {
                 
                    //QueryEForms
                    QuerySession.QueryEForm QEForm = Session["QueryRequestEForms"] as QuerySession.QueryEForm;
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
                        txtNo.Text = QEForm.E_No;
                    }


                }
                if (Request["Type"] != null)
                {
                    string type = Request["Type"].ToString();
                    if (type == "Doing")
                    {
                        ddlState.Text = "执行中";

                    }

                    if (type == "Success")
                    {
                        ddlState.Text = "通过";

                       
                    }

                    if (type == "SuccessCai")
                    {
                        ddlState.Text = "通过";


                    }


                    if (type == "Fail")
                    {
                        ddlState.Text = "不通过";
                     
                    }
                    select();
                }
                else
                {
                    select();
                    // //加载SESSION中的数据
                    //if (Session["QueryRequestEForms"] == null)
                    //{
                    //    tb_EFormService eformSer = new tb_EFormService();
                    //    string sql = string.Format("  appPer={0}", Session["currentUserId"].ToString());
                    //    List<tb_EForm> allEForms = eformSer.GetListArray_1(sql);
                    //    AspNetPager1.RecordCount = allEForms.Count;
                    //    this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
                    //    this.gvList.DataSource = allEForms;
                    //    this.gvList.DataBind();

                    //}
                    //else
                    //{
                    //    select();
                    //}
                }

                

            }
            if (Session["printUrl"] != null)
            {
                string url = Session["printUrl"].ToString();
                Session["printUrl"] = null;
                Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
            }
        }
    }
}
