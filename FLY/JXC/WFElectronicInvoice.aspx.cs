using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.JXC;

namespace VAN_OA.JXC
{
    public partial class WFElectronicInvoice : BasePage
    {

        ElectronicInvoiceService electronicInvoiceSer = new ElectronicInvoiceService();
        List<Invoice_BillType> billTypeList = new List<Invoice_BillType>();
        List<Invoice_Person> personList = new List<Invoice_Person>();
        System.Collections.Hashtable hb = new System.Collections.Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var user = new List<Model.User>();
                var userSer = new Dal.SysUserService();

                //主单
                var pOOrderList = new List<CG_POOrderService>();
                gvMain.DataSource = pOOrderList;
                gvMain.DataBind();

                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                billTypeList = new Invoice_BillTypeService().GetListArray(" IsStop=0");
                personList = new Invoice_PersonService().GetListArray(" IsStop=0");
            }
        }

        private List<string> allFpTypes = new List<string>();

        private List<ElectronicInvoice> GetData()
        {
            billTypeList = new Invoice_BillTypeService().GetListArray(" IsStop=0");
            personList = new Invoice_PersonService().GetListArray(" IsStop=0");
            var sql = "";
            var sumWhere = "having 1=1";
            if (txtSmallTotal.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtSmallTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('金额 格式错误！');</script>");
                    return null;
                }
                sumWhere += string.Format(" and {1}{0}isnull(sum(ActPay),0) ", ddlLeftTotal.Text, txtSmallTotal.Text.Trim());
            }
            if (txtBigTotal.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtBigTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('金额 格式错误！');</script>");
                    return null;
                }
                sumWhere += string.Format(" and isnull(sum(ActPay),0){0} {1}", ddlTotal.Text, txtBigTotal.Text.Trim());
            }

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return null;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return null;
                }
                sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return null;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }

            if (!string.IsNullOrEmpty(txtSupplierName.Text))
            {
                if (cbSupplierName.Checked == false)
                {
                    sql += string.Format(" and SupplierName like '%{0}%'", txtSupplierName.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and SupplierName='{0}'", txtSupplierName.Text.Trim());
                }
            }
            if (!string.IsNullOrEmpty(txtSupplierSimpName.Text))
            {
                if (cbSupplierSimpName.Checked == false)
                {
                    sql += string.Format(" and SupplieSimpeName like '%{0}%'", txtSupplierSimpName.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and SupplieSimpeName='{0}'", txtSupplierSimpName.Text.Trim());
                }
            }
            if (ddlPayType.Text != "-1")
            {
                sql += string.Format(" and busType='{0}'", ddlPayType.Text);
            }

            var list = this.electronicInvoiceSer.GetReport(sql,sumWhere);

            var countList = list.GroupBy(t => t.busType + t.ProNo).Where(t => t.Count() > 1);


            foreach (var m in countList)
            {
                hb.Add(m.Key, null);
            }
            return list;
        }
        private void Show()
        {
            var list = GetData();
            if (list == null)
            {
                return;
            }
            if (list.Count > 0&&txtProNo.Text.Trim().Length==8)
            {
                btbPrint.Enabled = true;
                btnYuLan.Enabled = true;
            }
            else
            {
                btbPrint.Enabled = false;
                btnYuLan.Enabled = false;
            }
            AspNetPager1.RecordCount = list.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = list;
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

        List<ElectronicInvoice> dataList = new List<ElectronicInvoice>();

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");


                DropDownList dllBillType = (DropDownList)e.Row.FindControl("dllBillType");
                dllBillType.DataSource = billTypeList;
                dllBillType.DataTextField = "BillName";
                dllBillType.DataValueField = "Id";
                dllBillType.DataBind();

                DropDownList dllPerson = (DropDownList)e.Row.FindControl("dllPerson");
                dllPerson.DataSource = personList;
                dllPerson.DataTextField = "Name";
                dllPerson.DataValueField = "Id";
                dllPerson.DataBind();



                var model = e.Row.DataItem as ElectronicInvoice;
                if (hb.ContainsKey(model.busType + model.ProNo))
                {
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    ImageButton btnJinZhangDan = (ImageButton)e.Row.FindControl("btnJinZhangDan");
                    btnEdit.Visible = false;
                    btnJinZhangDan.Visible = false;
                }
                if (model.SupplierName != null && (model.SupplierName.Contains("本部门") || model.SupplierName.Contains("淘宝")))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
                if (model.City != null &&
                    (model.City.Contains("苏州") || model.City.Contains("太仓") || model.City.Contains("相城")
                    || model.City.Contains("吴中") || model.City.Contains("张家港")
                    || model.City.Contains("常熟") || model.City.Contains("昆山")))
                {
                    dllBillType.Text = billTypeList.Find(t => t.BillName == "支票").Id.ToString();
                }
                else
                {
                    dllBillType.Text = billTypeList.Find(t => t.BillName == "银行申请单").Id.ToString();

                }
                dataList.Add(model);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ViewState["DataList"] = dataList;
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "YuLan")
            {

                //gvMain.SelectedRow
            }
            if (e.CommandName == "JinZhangDan")
            {

            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //判断项目有没有使用
            var data = ViewState["DataList"] as List<ElectronicInvoice>;
            if (data != null)
            {
                var model = data[e.RowIndex];
                var dllBillType = gvMain.Rows[e.RowIndex].FindControl("dllBillType") as DropDownList;
                var dllPerson = gvMain.Rows[e.RowIndex].FindControl("dllPerson") as DropDownList;
                var dllUse = gvMain.Rows[e.RowIndex].FindControl("dllUse") as DropDownList;
                var dllCompany = gvMain.Rows[e.RowIndex].FindControl("dllCompany") as DropDownList;
                model.BillType = dllBillType.SelectedItem.Text;
                model.Person = dllPerson.SelectedItem.Text;
                model.Use = dllUse.SelectedItem.Text;
                model.Company = dllCompany.SelectedItem.Text;
                Session["ElectronicInvoice"] = model;
                Response.Write("<script>window.open('/Fin/EI_InCome.aspx','_blank')</script>");
            }


        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //var data = gvMain.DataSource as List<ElectronicInvoice>;
            var data = ViewState["DataList"] as List<ElectronicInvoice>;
            if (data != null)
            {
                var model = data[e.NewEditIndex];
                var dllBillType = gvMain.Rows[e.NewEditIndex].FindControl("dllBillType") as DropDownList;
                var dllPerson = gvMain.Rows[e.NewEditIndex].FindControl("dllPerson") as DropDownList;
                var dllUse = gvMain.Rows[e.NewEditIndex].FindControl("dllUse") as DropDownList;
                var dllCompany = gvMain.Rows[e.NewEditIndex].FindControl("dllCompany") as DropDownList;
                model.BillType = dllBillType.SelectedItem.Text;
                model.Person = dllPerson.SelectedItem.Text;
                model.Use = dllUse.SelectedItem.Text;
                model.Company = dllCompany.SelectedItem.Text;
                Session["ElectronicInvoice"] = model;
                if (model.BillType == "银行申请单")
                {
                    Response.Write("<script>window.open('/Fin/EI_BankBill.aspx','_blank')</script>");
                }
                else if (model.BillType == "支票")
                {
                    Response.Write("<script>window.open('/Fin/EI_BlankCheck.aspx','_blank')</script>");
                }
                //Response.Write(" <script>document.location=document.location; </script>");
                return;
            }


        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYuLan_Click(object sender, EventArgs e)
        {
            var list = GetData();
            if (list == null || list.Count == 0)
            {
                return;
            }
            //查询结果既有支付单又有预付款单且供应商简称不同，
            //点击合并预览 或 合并打印进账单 提示“请选择支付类型”，返回界面
            if (list.FindAll(t => t.busType == "支").Count > 0 && list.FindAll(t => t.busType == "预").Count > 0 && list.GroupBy(t => t.SupplieSimpeName).Count() > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('请选择支付类型！');</script>"));
                return;
            }
            var model = list[0];
            var dllBillType = gvMain.Rows[0].FindControl("dllBillType") as DropDownList;
            var dllPerson = gvMain.Rows[0].FindControl("dllPerson") as DropDownList;
            var dllUse = gvMain.Rows[0].FindControl("dllUse") as DropDownList;
            var dllCompany = gvMain.Rows[0].FindControl("dllCompany") as DropDownList;
            model.BillType = dllBillType.SelectedItem.Text;
            model.Person = dllPerson.SelectedItem.Text;
            model.Use = dllUse.SelectedItem.Text;
            model.Company = dllCompany.SelectedItem.Text;
            model.ActPay = list.Sum(t => t.ActPay);
            Session["ElectronicInvoice"] = model;
            if (model.BillType == "银行申请单")
            {
                Response.Write("<script>window.open('/Fin/EI_BankBill.aspx','_blank')</script>");
            }
            else if (model.BillType == "支票")
            {
                Response.Write("<script>window.open('/Fin/EI_BlankCheck.aspx','_blank')</script>");
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btbPrint_Click(object sender, EventArgs e)
        {
            var list = GetData();
            if (list == null || list.Count == 0)
            {
                return;
            }
            //查询结果既有支付单又有预付款单且供应商简称不同，
            //点击合并预览 或 合并打印进账单 提示“请选择支付类型”，返回界面
            if (list.FindAll(t => t.busType == "支").Count > 0 && list.FindAll(t => t.busType == "预").Count > 0 && list.GroupBy(t => t.SupplieSimpeName).Count() > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('请选择支付类型！');</script>"));
                return;
            }
            var model = list[0];
            var dllBillType = gvMain.Rows[0].FindControl("dllBillType") as DropDownList;
            var dllPerson = gvMain.Rows[0].FindControl("dllPerson") as DropDownList;
            var dllUse = gvMain.Rows[0].FindControl("dllUse") as DropDownList;
            var dllCompany = gvMain.Rows[0].FindControl("dllCompany") as DropDownList;
            model.BillType = dllBillType.SelectedItem.Text;
            model.Person = dllPerson.SelectedItem.Text;
            model.Use = dllUse.SelectedItem.Text;
            model.Company = dllCompany.SelectedItem.Text;
            model.ActPay = list.Sum(t => t.ActPay);
            Session["ElectronicInvoice"] = model;
            Response.Write("<script>window.open('/Fin/EI_InCome.aspx','_blank')</script>");
        }
    }
}