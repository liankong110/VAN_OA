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
using System.Text;

namespace VAN_OA.KingdeeInvoice
{
    public partial class WFInvoiceList : BasePage
    {
        InvoiceService invoiceSer = new InvoiceService();

        public string GetValue(object num)
        {
            return string.Format("{0:n2}", num);
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

        private void Show()
        {

            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and CreateDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CreateDate<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtBillDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtBillDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and BillDate>='{0} 00:00:00'", txtBillDateFrom.Text);
            }

            if (txtBillDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtBillDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BillDate<='{0} 23:59:59'", txtBillDateTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (cbInvoiceNo.Checked)
            {
                sql += string.Format(" and (InvoiceNumber is null or InvoiceNumber='')");
            }
            else if (txtInvoiceNo.Text.Trim() != "")
            {
                sql += string.Format(" and InvoiceNumber like '%{0}%'", txtInvoiceNo.Text.Trim());
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
            if (ddlDaoZhang.Text != "-1")
            {
                sql += string.Format(" and IsAccount={0} ", ddlDaoZhang.Text);
            }
            if (txtDaoKuanTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtDaoKuanTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Received {0} {1} ", ddlDaoKuanTotal.Text, txtDaoKuanTotal.Text);
            }

            if (ddlIsorder.Text == "1")
            {
                sql += string.Format(" and Isorder={0} ", ddlIsorder.Text);
            }
            if (ddlIsorder.Text == "0")
            {
                sql += " and Isorder is null ";
            }

            string invoiceServer = "";
            if (ddlIsDeleted.Text == "0")
            {
                sql += " and IsDeleted=0";
            }

            if (ddlIsDeleted.Text == "1")//已经删除
            {
                sql += " and IsDeleted=1";
                //invoiceServer = "KIS.";

            }

            if (ddlEQTotal.Text != "-1")
            {
                sql += string.Format(" AND Received{0}Total", ddlEQTotal.Text);
            }
            List<Invoice> invoiceList = new List<Invoice>();
            //全部
            if (ddlIsDeleted.Text == "-1")
            {
                invoiceList = this.invoiceSer.GetListArray(sql, invoiceServer);
                //sql += " and IsDeleted=1";
                //invoiceServer = "KIS.";
                //invoiceList.AddRange(this.invoiceSer.GetListArray(sql, invoiceServer));
            }
            else
            {
                invoiceList = this.invoiceSer.GetListArray(sql, invoiceServer);
            }

            AspNetPager1.RecordCount = invoiceList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = invoiceList;
            this.gvList.DataBind();
            var sumTotal = invoiceList.Sum(t => t.Total);
            var sumReceived = invoiceList.Sum(t => t.Received);
            if (sumTotal != 0)
            {
                lblDaoKuanBLTotal.Text = string.Format("{0:n2}", sumReceived / sumTotal * 100);
                lblWeiDaoKuanBLTotal.Text = string.Format("{0:n2}", (sumTotal - sumReceived) / sumTotal * 100);
            }
            else
            {
                lblDaoKuanBLTotal.Text = "0";
                lblWeiDaoKuanBLTotal.Text = "0";
            }
            lblAllTotal.Text = sumTotal.ToString();
            lblDaoTotal.Text = sumReceived.ToString();
            lblWeiDaoTotal.Text = (invoiceList.Sum(t => t.Total) - invoiceList.Sum(t => t.Received)).ToString();
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
                Label Total = (e.Row.FindControl("Total")) as Label;
                TextBox EditReceived = (e.Row.FindControl("EditReceived")) as TextBox;
                Label DaoKuanBL = (e.Row.FindControl("DaoKuanBL")) as Label;
                Label NoReceived = (e.Row.FindControl("NoReceived")) as Label;
                Label WeiDaoKuanBL = (e.Row.FindControl("WeiDaoKuanBL")) as Label;


                EditReceived.Attributes.Add("onblur", string.Format("GetNoReceived('{0}','{1}','{2}','{3}','{4}')",
                    EditReceived.ClientID, Total.Text, DaoKuanBL.ClientID, NoReceived.ClientID, WeiDaoKuanBL.ClientID));


            }
        }



        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.invoiceSer.TrueDelete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<Invoice> invoiceList = new List<Invoice>();
                this.gvList.DataSource = invoiceList;
                this.gvList.DataBind();

                string sql = "";
                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    //                    sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='删除'
                    //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='金蝶发票清单') and sys_Object.AutoID is not null", Session["currentUserId"]);
                    if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "删除") == false)
                    {
                        gvList.Columns[0].Visible = false;
                    }
                }
                #endregion


                //                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='Isorder可编辑'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='金蝶发票清单') and sys_Object.AutoID is not null", Session["currentUserId"]);

                if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "Isorder可编辑") == false)
                {
                    ViewState["cbIsIsorder"] = false;
                    gvList.Columns[2].Visible = false;
                }

                if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "删除标记") == false)
                {
                    ViewState["cbIsDeleted"] = false;
                    btnDeleted.Visible = false;
                }

                if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "可编辑") == false)
                {
                    btnEdit.Visible = false;
                }
                if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "Isorder可编辑") == false &&
                    VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶应收发票清单", Session["currentUserId"], "可编辑") == false)
                {
                    btnIsSelected.Visible = false;
                }

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

        protected void cbIsDeleted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsDeleted")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }



        protected bool IsDeletedEdit()
        {
            if (ViewState["cbIsDeleted"] != null)
            {
                return false;
            }
            return true;
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
            if (btnEdit.Text == "取消编辑")
            {
                StringBuilder where = new StringBuilder();
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {
                    Label lblIds = (gvList.Rows[i].FindControl("Id")) as Label;
                    TextBox EditGuestName = (gvList.Rows[i].FindControl("EditGuestName")) as TextBox;
                    TextBox EditReceived = (gvList.Rows[i].FindControl("EditReceived")) as TextBox;
                    if (CommHelp.VerifesToNum(EditReceived.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额格式错误！');</script>");
                        return;
                    }
                    //where.AppendFormat("update KIS.[KingdeeInvoice].[dbo].[Invoice] set GuestName='{1}',Received={2} where Id={0};", lblIds.Text, EditGuestName.Text, EditReceived.Text);
                    where.AppendFormat("update [KingdeeInvoice].[dbo].[Invoice] set GuestName='{1}',Received={2} where Id={0};", lblIds.Text, EditGuestName.Text, EditReceived.Text);

                }
                if (where.ToString() != "")
                {
                    DBHelp.ExeCommand(where.ToString());
                }
            }
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
                    //var sql = "update KIS.[KingdeeInvoice].[dbo].[Invoice] set Isorder=1 where " + where;
                    var sql = "update [KingdeeInvoice].[dbo].[Invoice] set Isorder=1 where " + where;
                    DBHelp.ExeCommand(sql);
                }

                if (expWhere != " Id  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    //var sql = "update KIS.[KingdeeInvoice].[dbo].[Invoice] set Isorder=null where " + expWhere;
                    var sql = "update [KingdeeInvoice].[dbo].[Invoice] set Isorder=null where " + expWhere;
                    DBHelp.ExeCommand(sql);

                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
			string checkSql = @"SELECT COUNT(*) FROM (
SELECT ROW_NUMBER() over(partition by GuestName, InvoiceNumber, Total, BillDate, received order by InvoiceNumber) rowNum
      ,ID
  FROM[KingdeeInvoice].[dbo].[Invoice] ) AS newInvoice WHERE ROWNUM>1";
			if (Convert.ToInt32(DBHelp.ExeScalar(checkSql)) ==0)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('没有发现重复发票记录!');</script>");
				return;
			}

			string sql = @"delete from [KingdeeInvoice].[dbo].[Invoice] where id in (
SELECT id FROM (
SELECT ROW_NUMBER() over(partition by GuestName,InvoiceNumber,Total,BillDate,received order by InvoiceNumber) rowNum 
      ,ID
  FROM [KingdeeInvoice].[dbo].[Invoice] ) AS newInvoice WHERE ROWNUM>1)";
			DBHelp.ExeCommand(sql);
			base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('已删除重复发票记录!');</script>");

			//            string sql = string.Format(@"SELECT   [InvoiceNumber],[Total],[CreateDate],[IsAccount],[Received],COUNT(*) as cou    
			//FROM KIS.KingdeeInvoice.[dbo].[Invoice]  group by  [InvoiceNumber],[Total],[CreateDate],[IsAccount],[Received]
			//having COUNT(*)>1");
			//            var oriData = DBHelp.getDataTable(sql);
			//            if (oriData.Rows.Count > 0)
			//            {
			//                using (SqlConnection conn = DBHelp.getConn())
			//                {
			//                    conn.Open();
			//                    SqlTransaction tan = conn.BeginTransaction();
			//                    SqlCommand objCommand = conn.CreateCommand();
			//                    objCommand.Transaction = tan;
			//                    try
			//                    {
			//                        foreach (DataRow dr in oriData.Rows)
			//                        {
			//                            sql = string.Format(@"update KIS.KingdeeInvoice.[dbo].[Invoice]  set IsDeleted=1 where id in (select top {5} id from KIS.KingdeeInvoice.[dbo].[Invoice] where [InvoiceNumber]='{0}' and [Total]={1} and [CreateDate]='{2}' and [IsAccount]={3} and [Received]={4});
			//                            delete from KingdeeInvoice.[dbo].[Invoice] where id in (select top {5} id from KingdeeInvoice.[dbo].[Invoice] where [InvoiceNumber]='{0}' and [Total]={1} and [CreateDate]='{2}' and [IsAccount]={3} and [Received]={4})",
			//                            dr[0], dr[1], dr[2], dr[3], dr[4], (Convert.ToInt32(dr[5]) - 1));
			//                            objCommand.CommandText = sql;
			//                            objCommand.ExecuteNonQuery();
			//                        }
			//                        tan.Commit();

			//                    }
			//                    catch (Exception)
			//                    {

			//                        tan.Rollback();
			//                        conn.Close();
			//                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
			//                        return;
			//                    }
			//                    conn.Close();
			//                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
			//                }
			//            }
			//            else
			//            {
			//                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('没有重复数据!');</script>");
			//            }
		}

        protected void btnDeleted_Click(object sender, EventArgs e)
        {

            if (ViewState["cbIsDeleted"] == null)
            {
                string where = " Id  in (";
                string expWhere = " Id  in (";
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {
                    CheckBox cb = (gvList.Rows[i].FindControl("cbIsDeleted")) as CheckBox;
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
                    var sql = "update [KingdeeInvoice].[dbo].[Invoice] set IsDeleted=1 where " + where;
                    //var sql = "delete [KingdeeInvoice].[dbo].[Invoice]  where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                //                //页面的删除标记 为 未删除的纪录 做如下处理：A.10 上该记录的删除标记=0，B.把10上的该记录拷贝到11上
                if (expWhere != " Id  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update [KingdeeInvoice].[dbo].[Invoice] set IsDeleted=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);

                    //                    string batchSql = string.Format(@"
                    //INSERT INTO KingdeeInvoice.[dbo].[Invoice]
                    //           (ID,[GuestName]
                    //           ,[InvoiceNumber]
                    //           ,[Total]
                    //           ,[CreateDate]
                    //           ,[IsAccount]
                    //           ,[Received]
                    //           ,[Isorder])
                    //SELECT ID,[GuestName]
                    //      ,[InvoiceNumber]
                    //      ,[Total]
                    //      ,[CreateDate]
                    //      ,[IsAccount]
                    //      ,[Received]
                    //      ,[Isorder]
                    //  FROM KIS.[KingdeeInvoice].[dbo].[Invoice] where IsDeleted=0 and {0}", expWhere);
                    //                    DBHelp.ExeCommand(batchSql);
                    //                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                Show();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "编辑")
            {
                btnEdit.Text = "取消编辑";
                gvList.Columns[5].Visible = false;
                gvList.Columns[6].Visible = true;
                gvList.Columns[13].Visible = false;
                gvList.Columns[14].Visible = true;
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {
                    Label GuestName = (gvList.Rows[i].FindControl("GuestName")) as Label;
                    TextBox EditGuestName = (gvList.Rows[i].FindControl("EditGuestName")) as TextBox;
                    EditGuestName.Text = GuestName.Text;

                    Label Received = (gvList.Rows[i].FindControl("Received")) as Label;
                    TextBox EditReceived = (gvList.Rows[i].FindControl("EditReceived")) as TextBox;
                    EditReceived.Text = Received.Text;
                }
            }
            else
            {
                btnEdit.Text = "编辑";
                gvList.Columns[5].Visible = true;
                gvList.Columns[6].Visible = false;
                gvList.Columns[13].Visible = true;
                gvList.Columns[14].Visible = false;
                Show();
            }
        }
    }
}
