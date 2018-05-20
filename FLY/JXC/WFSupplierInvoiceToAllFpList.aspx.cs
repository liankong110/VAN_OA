using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using System.Data;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
 

namespace VAN_OA.JXC
{
    public partial class WFSupplierInvoiceToAllFpList : BasePage
    {
        SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看所有'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='支付单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (QuanXian_ShowAll("预付款发票列表") == false)               
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
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='不能编辑'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='预付款发票列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("预付款发票列表", "不能编辑")==false)
                {
                    gvMain.Columns[0].Visible = false;
                }

                if (Request["Ids"] != null)
                {
                    ddlStatue.Text = "通过";
                     
                    Show();
                }
                else
                {
                    if (Request["PayIds"] != null)
                    {
                        ddlStatue.Text = "通过";
                        
                        Show();
                    }
                    else
                    {
                        if (Request["PONo"] != null)
                        {
                            txtPONo.Text = Request["PONo"];
                            ddlFpState.Text = "1";
                            Show();
                        }
                        else
                        {
                            //主单
                            List<SupplierToInvoiceView> pOOrderList = new List<SupplierToInvoiceView>();
                            this.gvMain.DataSource = pOOrderList;
                            this.gvMain.DataBind();
                        }
                    }
                }
            }
        }
        #region 入库后付款
        private void Show()
        {

            string sql = " IsYuFu=1  and CAI_OrderInHouses.Ids is not null  ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text.Trim());
            }

            if (Request["Ids"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["Ids"]);
            }
            if (Request["PayIds"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["PayIds"]);
            }
            

            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {

                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }

                
                sql += string.Format(" and SupplierInvoiceDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                
                sql += string.Format(" and SupplierInvoiceDate<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and TB_SupplierInvoice.Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and TB_SupplierInvoice.Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and TB_SupplierInvoice.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }


            if (txtRuCaiProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtRuCaiProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CAI_OrderInHouse.ProNo like '%{0}%'", txtRuCaiProNo.Text.Trim());
            }

            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked == false)
                {
                    sql += string.Format(" and CAI_OrderInHouse.Supplier  like '%{0}%'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and CAI_OrderInHouse.Supplier='{0}'", txtSupplier.Text.Trim());
                }
            }
            

            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlFpState.Text != "-1")
            {
                //含税已到票
                if (ddlFpState.Text == "0")
                {
                    sql += string.Format(" and IsHanShui=1 and SupplierFPNo<>'' ");
                }
                //含税未到票
                if (ddlFpState.Text == "1")
                {
                    sql += string.Format(" and IsHanShui=1 and SupplierFPNo='' ");
                }
                //不含税
                if (ddlFpState.Text == "2")
                {
                    sql += string.Format(" and IsHanShui=0 ");
                }   
            }

            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AppName={1}))", Session["LoginName"], Session["currentUserId"]);
            //}
              if (ddlUser.Text != "-1")
              {
                  sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AE='{1}'))", ddlUser.SelectedItem.Text, ddlUser.SelectedItem.Text);
              }

              if (ddlCompany.Text != "-1")
              {
                  string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                  sql += string.Format(" and (DoPer IN(select loginName from tb_User where {0})  or exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=CAI_OrderInHouse.PONo and AE IN(select LOGINNAME from tb_User where {0})))", where);

              }
            List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetSupplierInvoiceListToAllFp(sql);

            lblAllTotal.Text = pOOrderList.Sum(t => t.SupplierInvoiceTotal).ToString();
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();
           
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
          
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }
        decimal total = 0;
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SupplierToInvoiceView model = e.Row.DataItem as SupplierToInvoiceView;
                if (model.IsHanShui == false)
                {
                   var edit= e.Row.FindControl("lbtnReEdit") as LinkButton;
                   if (edit != null)
                   {
                       edit.Visible = false;
                   }
                }
                if (model != null)
                {
                    total += model.SupplierInvoiceTotal;
                    lblTotal.Text = total.ToString();
                }
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");               
            }
            
        }

       

   
        #endregion

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ReEdit")
            {
                 string[] payType_Id = e.CommandArgument.ToString().Split('_');
                 string sql = "";
                 string type = "";
                 if (payType_Id[0] == "支")
                 {
                     type = "供应商付款单";                     
                     var checkSql = string.Format(@"select count(*) from TB_SupplierInvoice
left join TB_SupplierInvoices on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过'  and TB_SupplierInvoice.id={0} and SupplierInvoiceTotal<0", payType_Id[1]);
                     if (Convert.ToInt32(DBHelp.ExeScalar(checkSql)) > 0)
                     {
                         base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据由退货单生成，无法修改！');</script>");
                         return;
                     }
                     sql = "select CreateName,Status,ProNo,TB_SupplierInvoices.Id,IsYuFu from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoices.id=TB_SupplierInvoice.id where TB_SupplierInvoice.id=" + payType_Id[1];
                 }              

                 DataTable tb = DBHelp.getDataTable(sql);
                 if (tb.Rows.Count <= 0)
                 {
                     base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('信息不存在！');</script>");
                     return;
                 }
                   //是否是此单据的申请人


                 //if (Session["LoginName"].ToString() != tb.Rows[0]["CreateName"].ToString())
                 // {

                 //     base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                 //     return;
                 // }

                //首先单子要先通过               

                if (tb.Rows[0]["Status"] != null && tb.Rows[0]["Status"].ToString()!= "通过")
                {

                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                    return;
                }

                if (Convert.ToBoolean(tb.Rows[0]["IsYuFu"]))
                {
                    type = "供应商付款单（预付单转支付单）";
                }
                

                sql = "select pro_Id from A_ProInfo where pro_Type='" + type + "'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='" + type + "')", payType_Id[1]);

                string url = "";
                if (payType_Id[0] == "支")
                {
                    url = "~/JXC/WFSupplierInvoiceVerify.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + payType_Id[1] + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                }
              
                
                Response.Redirect(url);
                

                //没有做过检验单

                
            }
        }

        

    }
}
