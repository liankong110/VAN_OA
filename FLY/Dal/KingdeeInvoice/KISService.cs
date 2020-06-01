using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VAN_OA.Model.KingdeeInvoice;

namespace VAN_OA.Dal.KingdeeInvoice
{
	public class KISService
	{
		//public static string KISDBConn = GetKisDBConn();// ConfigurationManager.ConnectionStrings["KISDBConn"].ToString();
		private static string DBConn = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

		public string GetKISDBConn()
		{
			var model = GetKIS();
			return string.Format("Data Source={0};Initial Catalog=AcctCtl;User ID={1};Pwd={2}", model.IP, model.UserID, model.Pwd);
		}
		public KISModel GetKIS()
		{
			var kis = new KISModel();
			string sql = "select * from KISData;";
			using (SqlConnection conn = new SqlConnection(DBConn))
			{
				conn.Open();
				SqlCommand objCommand = new SqlCommand(sql, conn);
				using (SqlDataReader objReader = objCommand.ExecuteReader())
				{
					if (objReader.Read())
					{
						kis = ReaderBind(objReader);
					}
					objReader.Close();
				}
				conn.Close();
			}
			return kis;
		}

		public int Update(KISModel model)
		{
			string sql = string.Format("update KISData set AccountName='{0}',InvoiceFrom='{1}',InvoiceTo='{2}',PayableTo='{3}'," +
				"PayableFrom='{4}',InvoiceDate='{5}',PayableDate='{6}',IP='{7}',UserID='{8}',Pwd='{9}'", model.AccountName,
				model.InvoiceFrom, model.InvoiceTo, model.PayableTo, model.PayableFrom, model.InvoiceDate, model.PayableDate, model.IP, model.UserID, model.Pwd);
			using (SqlConnection conn = new SqlConnection(DBConn))
			{
				conn.Open();
				SqlCommand objCommand = new SqlCommand(sql, conn);
				objCommand.ExecuteNonQuery();
				conn.Close();
			}
			return 1;
		}
		public KISModel ReaderBind(IDataReader dataReader)
		{
			KISModel model = new KISModel();
			model.AccountName = dataReader["AccountName"].ToString();
			model.InvoiceFrom = dataReader["InvoiceFrom"].ToString();
			model.InvoiceTo = dataReader["InvoiceTo"].ToString();
			model.PayableFrom = dataReader["PayableFrom"].ToString();
			model.PayableTo = dataReader["PayableTo"].ToString();
			model.InvoiceDate = dataReader["InvoiceDate"].ToString();
			model.PayableDate = dataReader["PayableDate"].ToString();
			model.Message = dataReader["Message"].ToString();
			model.IP = dataReader["IP"].ToString();
			model.UserID = dataReader["UserID"].ToString();
			model.Pwd = dataReader["Pwd"].ToString();
			return model;
		}
		public static bool ExeCommand(string sql, string connectionString, DateTime beginDate, DateTime endDate)
		{
			bool result = false;
			using (SqlConnection objConnection = new SqlConnection(connectionString))
			{
				try
				{
					objConnection.Open();
					SqlCommand objCommand = objConnection.CreateCommand();
					objCommand.CommandType = CommandType.StoredProcedure;
					objCommand.CommandTimeout = 500;
					objCommand.CommandText = sql;
					objCommand.Parameters.Add("@beginDate", SqlDbType.DateTime).Value = beginDate;
					objCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;

					if (objCommand.ExecuteNonQuery() > 0)
					{
						result = true;
					}
				}
				catch (Exception)
				{


				}
				finally
				{
					objConnection.Close();
				}

			}
			return result;
		}

		/// <summary>
		/// 获取所有应收款 发票重复的内容
		/// </summary>
		/// <returns></returns>
		public List<Invoice> GetInvoiceErrorInfo(string account)
		{
			//获取选中的数据库连接
			string newConn = GetKISDBConn().Replace("AcctCtl", account);

			string sql = @"select FName as GUESTNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as T0TAL,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
, isnull(Dao_T0TAL,0) as RECEIVED  , NULL,
FTransDate from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
left join t_Organization on t_Organization.FItemID = t_ItemDetail.F1
left join (
select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
where faccountid in (select faccountid from t_Account where fname = '应收账款')
and FDC = 0  group by FTransNo
) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
where faccountid in (select faccountid from t_Account where fname = '应收账款') 
and FDC = 1      
and t_VoucherEntry.FTransNo  in (
select t_VoucherEntry.FTransNo as INVOICENUMBER from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID 
left join (
select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
where faccountid in (select faccountid from t_Account where fname = '应收账款')
and FDC = 0  group by FTransNo
) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
where faccountid in (select faccountid from t_Account where fname = '应收账款') 
and FDC = 1 
--and FTransDate>='' and FTransDate<=''        
 group by t_VoucherEntry.FTransNo having count(*)>1) order BY t_VoucherEntry.FTransNo;";
			List<VAN_OA.Model.KingdeeInvoice.Invoice> list = new List<VAN_OA.Model.KingdeeInvoice.Invoice>();
			using (SqlConnection conn = new SqlConnection(newConn))
			{
				conn.Open();
				SqlCommand objCommand = new SqlCommand(sql, conn);
				using (SqlDataReader objReader = objCommand.ExecuteReader())
				{
					while (objReader.Read())
					{
						var model = ReaderBind_Invoice(objReader);
						list.Add(model);
					}
				}
			}
			return list;

		}

		public VAN_OA.Model.KingdeeInvoice.Invoice ReaderBind_Invoice(IDataReader dataReader)
		{
			VAN_OA.Model.KingdeeInvoice.Invoice model = new VAN_OA.Model.KingdeeInvoice.Invoice();
			object ojb;

			model.GuestName = dataReader["GuestName"].ToString();
			model.InvoiceNumber = dataReader["InvoiceNumber"].ToString();
			ojb = dataReader["T0TAL"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.Total = (decimal)ojb;
			}
			ojb = dataReader["CreateDate"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.CreateDate = (DateTime)ojb;
			}
			ojb = dataReader["IsAccount"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.IsAccount = (int)ojb;
			}
			ojb = dataReader["Received"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.Received = (decimal)ojb;
			}

			ojb = dataReader["FTransDate"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.BillDate = Convert.ToDateTime(ojb);
			}


			return model;
		}


		/// <summary>
		/// 获取所有应付款 发票重复的内容
		/// </summary>
		/// <returns></returns>
		public List<Payable> GetPayaleErrorInfo(string account)
		{
			//获取选中的数据库连接
			string newConn = GetKISDBConn().Replace("AcctCtl", account);
			string sql = @"select t_Item.FName as SUPPLIERNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as Total,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
, isnull(Dao_T0TAL,0) as RECEIVED  ,NULL as Isorder,
FTransDate as BillDate from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
left join t_Item on t_Item.FItemID = t_ItemDetail.F3001
left join (
select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
where faccountid in (select faccountid from t_Account where fname = '应付账款')
and FDC = 1  group by FTransNo
) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
where faccountid in (select faccountid from t_Account where fname = '应付账款') 
and FDC = 0 and t_VoucherEntry.FTransNo is not null    
and t_VoucherEntry.FTransNo  in (
 select t_VoucherEntry.FTransNo as INVOICENUMBER from t_Voucher
left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID 
 
where faccountid in (select faccountid from t_Account where fname = '应付账款') 
and FDC = 0 and t_VoucherEntry.FTransNo is not null 
group BY  t_VoucherEntry.FTransNo having count(*)>1
)  order BY t_VoucherEntry.FTransNo;";
			List<VAN_OA.Model.KingdeeInvoice.Payable> list = new List<VAN_OA.Model.KingdeeInvoice.Payable>();
			using (SqlConnection conn = new SqlConnection(newConn))
			{
				conn.Open();
				SqlCommand objCommand = new SqlCommand(sql, conn);
				using (SqlDataReader objReader = objCommand.ExecuteReader())
				{
					while (objReader.Read())
					{
						var model = ReaderBind_Payable(objReader);
						list.Add(model);
					}
				}
			}
			return list;

		}

		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public VAN_OA.Model.KingdeeInvoice.Payable ReaderBind_Payable(IDataReader dataReader)
		{
			VAN_OA.Model.KingdeeInvoice.Payable model = new VAN_OA.Model.KingdeeInvoice.Payable();
			object ojb;

			model.SupplierName = dataReader["SupplierName"].ToString();
			model.InvoiceNumber = dataReader["InvoiceNumber"].ToString();
			ojb = dataReader["Total"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.Total = (decimal)ojb;
			}
			ojb = dataReader["CreateDate"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.CreateDate = (DateTime)ojb;
			}
			ojb = dataReader["IsAccount"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.IsAccount = (int)ojb;
			}
			ojb = dataReader["Received"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.Received = (decimal)ojb;
			}

			ojb = dataReader["BillDate"];
			if (ojb != null && ojb != DBNull.Value)
			{
				model.BillDate = Convert.ToDateTime(ojb);
			}


			return model;
		}

	}

	public class KISModel
	{
		public string AccountName { get; set; }

		public string InvoiceFrom { get; set; }

		public string InvoiceTo { get; set; }

		public string PayableFrom { get; set; }
		public string PayableTo { get; set; }

		public string InvoiceDate { get; set; }

		public string PayableDate { get; set; }
		public string Message { get; set; }

		public string IP { get; set; }
		public string UserID { get; set; }
		public string Pwd { get; set; }


	}
}