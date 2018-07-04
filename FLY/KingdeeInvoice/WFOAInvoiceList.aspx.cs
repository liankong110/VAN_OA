using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.KingdeeInvoice;
using VAN_OA.Model.KingdeeInvoice;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model;



namespace VAN_OA.KingdeeInvoice
{
    public partial class WFOAInvoiceList : BasePage
    {


        Sell_OrderFPService invoiceSer = new Sell_OrderFPService();

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        private void Show()
        {

            string sql = "";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                } 
                 
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderFP.GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (cbInvoiceNo.Checked)
            {
                sql += string.Format(" and (FPNo is null or FPNo='')");
            }
            else if (txtInvoiceNo.Text.Trim() != "")
            {
                sql += string.Format(" and FPNo like '%{0}%'", txtInvoiceNo.Text.Trim());
            }
            if (txtInvoice.Text != "")
            {
                if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Total {0} {1} ", ddlInvTotal.Text, txtInvoice.Text);
            }
            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and Sell_OrderFP.CreateUserId={0}", ddlUser.Text);
            }
           
            if (ddlIsorder.Text != "-1")
            {
                sql += string.Format(" and Isorder={0} ", ddlIsorder.Text);
            }
             
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderFP.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (txtPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderFP.POName like '%{0}%'", txtPOName.Text.Trim());
            }

            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            List<Sell_OrderFP> invoiceList = this.invoiceSer.GetOAInvoiceList(sql);
            AspNetPager1.RecordCount = invoiceList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = invoiceList;
            this.gvList.DataBind();

            lblAllTotal.Text = invoiceList.Sum(t => t.Total).ToString();
          

        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
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
            this.invoiceSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                List<Invoice> invoiceList = new List<Invoice>();
                this.gvList.DataSource = invoiceList;
                this.gvList.DataBind();

                if (VAN_OA.JXC.SysObj.NewShowAll_textName("项目发票销帐", Session["currentUserId"], "销帐可编辑") == false)
                { 
                    ViewState["cbIsIsorder"] = false;
                    btnIsSelected.Visible = false;
                    gvList.Columns[0].Visible = false;
                }
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                if (QuanXian_ShowAll("项目发票销帐") == false)
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

                if (Request["InvoiceNo"] != null)
                {
                    txtInvoiceNo.Text = Request["InvoiceNo"].ToString();
                    Show();
                }
            }
        }

        protected void cbIsSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsIsorder")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }


        protected bool IsSelectedEdit()
        {
            if (ViewState["cbIsIsorder"] != null)
            {
                return false;
            }
            return true;
        }     

        protected void btnIsSelected_Click(object sender, EventArgs e)
        {
            if (ViewState["cbIsIsorder"] == null)
            {
                string where = " Id  in (";
                string expWhere = " Id  in (";
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {
                    CheckBox cb = (gvList.Rows[i].FindControl("cbIsIsorder")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvList.Rows[i].FindControl("Id")) as Label;
                        where += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        Label lblIds = (gvList.Rows[i].FindControl("Id")) as Label;
                        expWhere += "'" + lblIds.Text + "',";
                    }
                }

                if (where != " Id  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";
                    var sql = "update [dbo].[Sell_OrderFP] set Isorder=1 where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (expWhere != " Id  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update [dbo].[Sell_OrderFP] set Isorder=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);
                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            //AspNetPager1.CurrentPageIndex = 1;
            //Show();
        }

      
    }
}
