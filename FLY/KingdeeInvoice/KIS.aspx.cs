using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.KingdeeInvoice;

namespace VAN_OA.KingdeeInvoice
{
	public partial class KIS : BasePage
	{
		private static string providerName = "System.Data.SqlClient";
		private static DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

		public string GetValue(object num)
		{
			return string.Format("{0:n2}", num);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetAccount();
				var kISModel = new KISService().GetKIS();
				ddlAccount.Text = kISModel.AccountName;
				txtFrom.Text = kISModel.InvoiceFrom;
				txtTo.Text = kISModel.InvoiceTo;
				txtpayableFrom.Text = kISModel.PayableFrom;
				txtpayableTo.Text = kISModel.PayableTo;
				txtInvoiceDate.Text = kISModel.InvoiceDate;
				txtpayableDate.Text = kISModel.PayableDate;
				txtIP.Text = kISModel.IP;
				txtUserId.Text = kISModel.UserID;
				txtPwd.Text = kISModel.Pwd;

				if (VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶发票处理工具", Session["currentUserId"], "修改时间") == false)
				{
					btnAdd.Visible = false;
					ddlAccount.Enabled = false;
					txtFrom.Enabled = false;
					txtTo.Enabled = false;
					txtpayableFrom.Enabled = false;
					txtpayableTo.Enabled = false;
					txtInvoiceDate.Enabled = false;
					txtpayableDate.Enabled = false;
					Image1.Visible = false;
					ImageButton1.Visible = false;
					ImageButton2.Visible = false;
					ImageButton3.Visible = false;
				}
				plConnString.Visible = VAN_OA.JXC.SysObj.NewShowAll_textName("金蝶发票处理工具", Session["currentUserId"], "可定义服务器");




			}
		}

		/// <summary>
		/// 获取账套信息
		/// </summary>
		private void GetAccount()
		{
			string sql = string.Format("select FDBName,FacctName+'['+FDBName+']' as Name from [dbo].[t_ad_kdAccount_gl]");
			var connString = new KISService().GetKISDBConn();
			var getList = getDataSet(sql, connString);
			List<Account> names = new List<Account>();
			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();
				SqlCommand objCommand = new SqlCommand(sql, conn);
				using (SqlDataReader objReader = objCommand.ExecuteReader())
				{
					while (objReader.Read())
					{
						Account model = new Account();
						model.Value = objReader["FDBName"].ToString();
						model.Name = objReader["Name"].ToString();
						names.Add(model);
					}
					objReader.Close();
				}
			}
			ddlAccount.DataValueField = "Value";
			ddlAccount.DataTextField = "Name";
			ddlAccount.DataSource = names;
			ddlAccount.DataBind();

		}

		public void DoWork()
		{
			try
			{
				//获取选中的数据库连接
				string newConn = new KISService().GetKISDBConn().Replace("AcctCtl", ddlAccount.SelectedValue);
				KISService.ExeCommand("Invoice_Proc", newConn, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text));
				KISService.ExeCommand("Payable_Proc", newConn, Convert.ToDateTime(txtpayableFrom.Text), Convert.ToDateTime(txtpayableTo.Text));
				lblMessage.Text = string.Format("执行成功！！！时间：{0}", DateTime.Now.ToString());
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('执行成功！');</script>");
			}
			catch (Exception ex)
			{
				lblMessage.Text = "执行异常，请检查！错误信息:" + ex.Message;
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('运行时间 格式错误！');</script>");
			}

		}

		public static DataSet getDataSet(string sql, string connectionString)
		{
			DataSet ds = new DataSet();
			using (DbConnection objConnection = provider.CreateConnection())
			{
				objConnection.ConnectionString = connectionString;
				objConnection.Open();
				DbCommand objCommand = objConnection.CreateCommand();
				objCommand.CommandText = sql;
				DbDataAdapter objApater = provider.CreateDataAdapter();
				objApater.SelectCommand = objCommand;
				objApater.Fill(ds);
				objConnection.Close();
			}
			return ds;
		}
		public class Account
		{
			public string Name { get; set; }
			public string Value { get; set; }
		}

		private bool Check()
		{
			if (!CommHelp.VerifesToDateTime(txtFrom.Text) || !CommHelp.VerifesToDateTime(txtTo.Text) ||
				!CommHelp.VerifesToDateTime(txtpayableFrom.Text) || !CommHelp.VerifesToDateTime(txtpayableTo.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间格式错误！');</script>");
				return false;
			}
			if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票开始时间 必须小于等于 结束时间！');</script>");
				return false;
			}
			if (Convert.ToDateTime(txtpayableFrom.Text) > Convert.ToDateTime(txtpayableTo.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票开始时间 必须小于 结束时间！');</script>");
				return false;
			}
			if (txtInvoiceDate.Text.Trim() == "" || txtpayableDate.Text.Trim() == "")
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('运行时间不能为空！');</script>");
				return false;
			}

			if (CommHelp.VerifesToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtInvoiceDate.Text.Trim()) == false
				|| CommHelp.VerifesToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + txtpayableDate.Text.Trim()) == false)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('运行时间 格式错误！');</script>");
				return false;
			}
			
			if (string.IsNullOrEmpty(txtIP.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('IP地址不能为空！');</script>");
				return false;
			}
			if (string.IsNullOrEmpty(txtUserId.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户名不能为空！');</script>");
				return false;
			}
			if (string.IsNullOrEmpty(txtPwd.Text))
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('密码不能为空！');</script>");
				return false;
			}
			return true;
		}
		private void Save()
		{
			KISModel kISModel = new KISModel();
			kISModel.AccountName = ddlAccount.Text;
			kISModel.InvoiceFrom = txtFrom.Text;
			kISModel.InvoiceTo = txtTo.Text;
			kISModel.PayableFrom = txtpayableFrom.Text;
			kISModel.PayableTo = txtpayableTo.Text;
			kISModel.InvoiceDate = txtInvoiceDate.Text;
			kISModel.PayableDate = txtpayableDate.Text;

			kISModel.IP = txtIP.Text;
			kISModel.UserID = txtUserId.Text;
			kISModel.Pwd = txtPwd.Text;
			new KISService().Update(kISModel);
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			if (Check() == false)
			{
				return;
			}
			Save();
			base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
		}

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
				e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
			}
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			if (Check() == false)
			{
				return;
			}
			Save();
			//检查是否有重复的数据
			var kisServer = new KISService();
			var invoicesList = kisServer.GetInvoiceErrorInfo(ddlAccount.SelectedValue);
			var payableList = kisServer.GetPayaleErrorInfo(ddlAccount.SelectedValue);
			gvList.DataSource = invoicesList;
			gvList.DataBind();

			gvPayable.DataSource = payableList;
			gvPayable.DataBind();
			if (invoicesList.Count > 0 || payableList.Count > 0)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('存在发票号重复数据，请检查！');</script>");
				return;
			}
			DoWork();
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			if (Check() == false)
			{
				return;
			}
			Save();
			//检查是否有重复的数据
			var kisServer = new KISService();
			var invoicesList = kisServer.GetInvoiceErrorInfo(ddlAccount.SelectedValue);
			var payableList = kisServer.GetPayaleErrorInfo(ddlAccount.SelectedValue);
			gvList.DataSource = invoicesList;
			gvList.DataBind();

			gvPayable.DataSource = payableList;
			gvPayable.DataBind();
		}
	}
}