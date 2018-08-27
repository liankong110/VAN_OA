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
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class CAI_OrderCheckList : BasePage
    {
        CAI_OrderCheckService POSer = new CAI_OrderCheckService();
        CAI_OrderChecksService ordersSer = new CAI_OrderChecksService();
        List<string> idsList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                //主单
                List<CAI_OrderCheck> pOOrderList = new List<CAI_OrderCheck>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CAI_OrderChecks> orders = new List<CAI_OrderChecks>();
                gvList.DataSource = orders;
                gvList.DataBind();


                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll(SysObj.CAI_OrderCheckList) == false)
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

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='预付单转支付单'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='采购订单检验列表') and sys_Object.AutoID is not null", Session["currentUserId"]);


                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("采购订单检验列表", "预付单转支付单"))
                {
                    gvMain.Columns[1].Visible = true;
                }
                else
                {
                    gvMain.Columns[1].Visible = false;
                }
            }
        }


        private void Show()
        {
            string sql = " 1=1 ";

            //if (txtPONo.Text != "" || ttxPOName.Text != "" || txtSupplier.Text!="")
            //{
            //    sql += " and exists(select ids from CAI_OrderChecks where 1=1 ";
            if (txtRemark.Text.Trim() != "")
            {
                sql += string.Format(" and CheckRemark like '%{0}%' ", txtRemark.Text.Trim());
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONO like '%{0}%' ", txtPONo.Text.Trim());
            }
            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%' ", ttxPOName.Text.Trim());
            }
            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(" and SupplierName='{0}'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and SupplierName  like '%{0}%'", txtSupplier.Text.Trim());
                }
            }
            //    sql += "and checkId=CAI_OrderCheck.id) ";
            //}



            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CheckTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CheckTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }


            if (txtJianYan.Text != "")
            {
                sql += string.Format(" and CheckUser.loginName  like '%{0}%'", txtJianYan.Text);
            }


            if (txtCaiGou.Text.Trim() != "")
            {
                if (CheckProNo(txtCaiGou.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CaiProNo like '%{0}%'", txtCaiGou.Text.Trim());
            }
            if (ddlUser.Text != "-1")
            {
                //sql += string.Format(" and (CAI_OrderCheck.CreatePer={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderChecks.PONo and AppName={0}))", ddlUser.Text);
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format("and (CAI_OrderCheck.CreatePer IN(select id from tb_User where {0}) or exists(select id from CG_POOrder where IFZhui=0 and CG_POOrder.PONo=CAI_OrderChecks.PONo and AppName IN(select id from tb_User where {0})))", where);
            }

            if (ddlIsHanShui.Text != "-1")
            {
                sql += string.Format(" and  CAI_POCai.IsHanShui={0} ", ddlIsHanShui.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=CAI_OrderChecks.PONO) ", ddlModel.Text);
            }
            PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;

            List<CAI_OrderCheck> pOOrderList = this.POSer.GetListArray(sql, page);


            string ids = "";
            for (int i = 0; i < pOOrderList.Count; i++)
            {
                if (pOOrderList[i].Status == "通过")
                {
                    ids += pOOrderList[i].Id.ToString() + ",";
                }
            }
            ids = ids.Trim(',');

            if (!string.IsNullOrEmpty(ids))
            {
                string InHouserSql = string.Format(@"  select CAI_OrderCheck.Id from CAI_OrderCheck 
 left join CAI_OrderChecks on CAI_OrderCheck.Id=CAI_OrderChecks.CheckId
  left join CAI_OrderInHouses on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids

   left join 
 (
 select  TB_SupplierAdvancePayments.CaiIds from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments  on TB_SupplierAdvancePayments.Id=TB_SupplierAdvancePayment.Id  
 where Status='通过' 
 ) as Yu on Yu.CaiIds=CAI_OrderChecks.CaiId
 left join 
 (
	select TB_SupplierInvoices.RuIds from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoices.Id=TB_SupplierInvoice.Id 
	where Status<>'不通过' and  IsYuFu=1
 ) as Zhi on Zhi.RuIds=CAI_OrderInHouses.Ids

where CAI_OrderCheck.Status='通过'  and  Yu.CaiIds is not null and Zhi.RuIds is null and CAI_OrderCheck.id in ({0})", ids);
                var dt = DBHelp.getDataTable(InHouserSql);
                idsList = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    idsList.Add(dr[0].ToString());
                }

//                InHouserSql = (@" select CAI_OrderCheck.Id from CAI_OrderCheck 
// left join CAI_OrderChecks on CAI_OrderCheck.Id=CAI_OrderChecks.CheckId
//  left join CAI_OrderInHouses on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids
//
//   left join 
// (
// select  TB_SupplierAdvancePayments.CaiIds from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments  on TB_SupplierAdvancePayments.Id=TB_SupplierAdvancePayment.Id  
// where Status='通过' 
// ) as Yu on Yu.CaiIds=CAI_OrderChecks.CaiId
// left join 
// (
//	select TB_SupplierInvoices.RuIds from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoices.Id=TB_SupplierInvoice.Id 
//	where Status<>'不通过' and  IsYuFu=1
// ) as Zhi on Zhi.RuIds=CAI_OrderInHouses.Ids
//
//where CAI_OrderCheck.Status='通过'  and  Yu.CaiIds is not null and Zhi.RuIds is null ");
//                dt = DBHelp.getDataTable(InHouserSql);
//                idsList = new List<string>();
//                foreach (DataRow dr in dt.Rows)
//                {
//                    XiuFu(dr[0].ToString());

//                }
            }
            AspNetPager1.RecordCount = page.TotalCount;

            //AspNetPager1.RecordCount = pOOrderList.Count;
            //this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<CAI_OrderChecks> orders = new List<CAI_OrderChecks>();
            gvList.DataSource = orders;
            gvList.DataBind();





        }
        private void XiuFu(string CheckId)
        {
            //检查是否已经生成支付单
            List<CAI_OrderChecks> POOrders = ordersSer.GetListArray(" 1=1 and CheckId=" + CheckId);
            string checkIds = "";
            string caiIds = "";
            foreach (var model in POOrders)
            {
                checkIds += model.Ids + ",";
                caiIds += model.CaiId + ",";
            }
            if (checkIds.Length > 0)
            {
                checkIds = checkIds.Substring(0, checkIds.Length - 1);
                caiIds = caiIds.Substring(0, caiIds.Length - 1);
            }
            string sql = string.Format("select count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoices.id=TB_SupplierInvoice.id  where Status<>'不通过' and  IsYuFu=1 and RuIds in (select ids from CAI_OrderInHouses where OrderCheckIds IN ({0}))", checkIds);
            int count = Convert.ToInt32(DBHelp.ExeScalar(sql));
            if (count > 0)
            {
                if (POOrders.Count > count)//说明有部分单子没有转支付单
                {
                    checkIds = "";
                    caiIds = "";
                    foreach (var model in POOrders)
                    {
                        sql = string.Format("select count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoices.id=TB_SupplierInvoice.id  where Status<>'不通过' and  IsYuFu=1 and RuIds in (select ids from CAI_OrderInHouses where OrderCheckIds IN ({0}))", model.Ids);
                        count = Convert.ToInt32(DBHelp.ExeScalar(sql));
                        if (count == 0)
                        {
                            checkIds += model.Ids + ",";
                            caiIds += model.CaiId + ",";
                        }
                    }
                    checkIds = checkIds.Trim(',');
                    caiIds = caiIds.Trim(',');
                }
                else
                {
                   
                    return;
                }
            }
            new TB_SupplierInvoiceService().AddSupplierInvoice(checkIds, caiIds, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()), POOrders[0].SupplierName);
            
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

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                CAI_OrderCheck model = e.Row.DataItem as CAI_OrderCheck;
                System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                if (lblIsHanShui != null)
                {
                    lblIsHanShui.Text = model.IsHanShui == 1 ? "含税" : "不含税";
                }
                if (model.IsHanShui == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
                if (model.Status != "通过" || !idsList.Contains(model.Id.ToString()))
                {
                    System.Web.UI.WebControls.LinkButton lbtnDoSupplierInvoice = e.Row.FindControl("lbtnDoSupplierInvoice") as System.Web.UI.WebControls.LinkButton;
                    lbtnDoSupplierInvoice.Visible = false;
                }

            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                List<CAI_OrderChecks> orders = ordersSer.GetListArray(" 1=1 and CheckId=" + e.CommandArgument);

                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();

            }
            else if (e.CommandName == "DoSupplierInvoice")
            {
                //检查是否已经生成支付单
                List<CAI_OrderChecks> POOrders = ordersSer.GetListArray(" 1=1 and CheckId=" + e.CommandArgument);
                string checkIds = "";
                string caiIds = "";
                foreach (var model in POOrders)
                {
                    checkIds += model.Ids + ",";
                    caiIds += model.CaiId + ",";
                }
                if (checkIds.Length > 0)
                {
                    checkIds = checkIds.Substring(0, checkIds.Length - 1);
                    caiIds = caiIds.Substring(0, caiIds.Length - 1);
                }
                string sql = string.Format("select count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoices.id=TB_SupplierInvoice.id  where Status<>'不通过' and  IsYuFu=1 and RuIds in (select ids from CAI_OrderInHouses where OrderCheckIds IN ({0}))", checkIds);
                int count = Convert.ToInt32(DBHelp.ExeScalar(sql));
                if (count > 0)
                {
                    if (POOrders.Count > count)//说明有部分单子没有转支付单
                    {
                        checkIds = "";
                        caiIds = "";
                        foreach (var model in POOrders)
                        {
                            sql = string.Format("select count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoices.id=TB_SupplierInvoice.id  where Status<>'不通过' and  IsYuFu=1 and RuIds in (select ids from CAI_OrderInHouses where OrderCheckIds IN ({0}))", model.Ids);
                            count = Convert.ToInt32(DBHelp.ExeScalar(sql));
                            if (count == 0)
                            {
                                checkIds += model.Ids + ",";
                                caiIds += model.CaiId + ",";
                            }
                        }  
                        checkIds = checkIds.Trim(',');
                        caiIds = caiIds.Trim(',');
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('已经生成，不能重复操作！');</script>");
                        return;
                    }
                }
                new TB_SupplierInvoiceService().AddSupplierInvoice(checkIds, caiIds, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()), POOrders[0].SupplierName);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('操作成功！');</script>");
                Show();
            }
        }



        CAI_OrderChecks SumOrders = new CAI_OrderChecks();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderChecks model = e.Row.DataItem as CAI_OrderChecks;
                SumOrders.CheckNum += model.CheckNum;
                SumOrders.Total += model.Total;
            }
            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblPONo") as Label, "合计");//合计
                //setValue(e.Row.FindControl("lblNum") as Label, SumOrders.CheckNum.ToString());//数量                
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }

        }

        private void setValue(Label control, string value)
        {
            if (control != null)
                control.Text = value;
        }

        //补预付款转支付单的单子
        private void DoSupplierInvoice()
        {

        }
    }
}
