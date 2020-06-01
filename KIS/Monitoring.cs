using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KIS
{

    public partial class Monitoring : Form
    {

        private static string providerName = "System.Data.SqlClient";
        private static string DBConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString(); 
        private static DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

        public Monitoring()
        {
            InitializeComponent();
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

        

        public static bool ExeCommand(string sql, string connectionString)
        {
            bool result = false;
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = connectionString;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandTimeout = 500;
                objCommand.CommandText = sql;
                if (objCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                objConnection.Close();
            }
            return result;
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

        private void Monitoring_Load(object sender, EventArgs e)
        {
            GetAccount();
            //for (int i = 0; i <= 24; i++)
            //{
            //    cboHour.Items.Add(i.ToString("00"));
            //}

            //for (int i = 0; i <= 60; i++)
            //{
            //    cboMins.Items.Add(i.ToString("00"));
            //}
            //cboHour.Text = "20";
            //cboMins.Text = "30";

            dateStart.Value = Convert.ToDateTime("2016-11-01");
            dateEnd.Value = Convert.ToDateTime("2026-3-03");
            dateJOB.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 20:30");
        }
        /// <summary>
        /// 获取账套信息
        /// </summary>
        private void GetAccount()
        {
            string sql = string.Format("select FDBName,FacctName+'['+FDBName+']' as Name from [dbo].[t_ad_kdAccount_gl]");
            var getList = getDataSet(sql, DBConn);
            List<Account> names = new List<Account>();
            using (SqlConnection conn = new SqlConnection(DBConn))
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
            cboAccount.ValueMember = "Value";
            cboAccount.DisplayMember = "Name";
            cboAccount.DataSource = names;



        }

        public void DoWork()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                try
                {
                    //获取选中的数据库连接
                    string newConn = DBConn.Replace("AcctCtl", cboAccount.SelectedValue.ToString());

                    //                    #region 应收款项
                    //                    //找到新增的发票进行插入OA 系统中
                    //                    string addSql = string.Format(@"
                    //INSERT INTO [KIS].[KingdeeInvoice].[dbo].[Invoice]
                    //           ([GuestName]
                    //           ,[InvoiceNumber]
                    //           ,[Total]
                    //           ,[CreateDate]
                    //           ,[IsAccount]
                    //           ,[Received]
                    //           ,[Isorder]
                    //           ,[BillDate])     
                    //select FName as GUESTNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as T0TAL,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
                    //, isnull(Dao_T0TAL,0) as RECEIVED  , NULL,
                    //FTransDate from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
                    //left join t_Organization on t_Organization.FItemID = t_ItemDetail.F1
                    //left join (
                    //select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //where faccountid in (select faccountid from t_Account where fname = '应收账款')
                    //and FDC = 0  group by FTransNo
                    //) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
                    //where faccountid in (select faccountid from t_Account where fname = '应收账款') 
                    //and FDC = 1 and FTransDate>='{0} 00:00:00' and FTransDate<='{1} 23:59:59'        
                    //and t_VoucherEntry.FTransNo not in (
                    //select InvoiceNumber from KIS.KingdeeInvoice.DBO.Invoice
                    //)  ; ", dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"));

                    //                    //找到新增的发票信息 进行更新
                    //                    string updateSql = string.Format(@"update KIS11 set
                    //KIS11.GUESTNAME=KIS10.GUESTNAME,
                    //KIS11.Total=KIS10.T0TAL,
                    //KIS11.CREATEDATE=KIS10.CREATEDATE,
                    //KIS11.ISACCOUNT=KIS10.ISACCOUNT,
                    //KIS11.RECEIVED=KIS10.RECEIVED,
                    //KIS11.BillDate=KIS10.BillDate
                    //from 
                    //(
                    //select FName as GUESTNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as T0TAL,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
                    //, isnull(Dao_T0TAL,0) as RECEIVED  ,
                    //FTransDate as BillDate from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
                    //left join t_Organization on t_Organization.FItemID = t_ItemDetail.F1
                    //left join (
                    //select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //where faccountid in (select faccountid from t_Account where fname = '应收账款')
                    //and FDC = 0  group by FTransNo
                    //) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
                    //where faccountid in (select faccountid from t_Account where fname = '应收账款') 
                    //and FDC = 1 and FTransDate>='{0} 00:00:00' and FTransDate<='{1} 23:59:59'     ) AS KIS10 
                    //left join KIS.KingdeeInvoice.DBO.Invoice as KIS11 ON KIS10.INVOICENUMBER=KIS11.INVOICENUMBER
                    //where  KIS11.INVOICENUMBER is not null", dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"));


                    //                    //删除发票信息,清理下OA 系统中 垃圾发票号，（金蝶把发票删除了 OA 也要删除）
                    //                    string deleteSQL = string.Format("delete from KIS.KingdeeInvoice.DBO.Invoice where invoiceNumber not in (select FTransNo from t_VoucherEntry)");
                    //                    ExeCommand(updateSql, newConn);
                    //                    ExeCommand(addSql, newConn);                 
                    //                    ExeCommand(deleteSQL, newConn);

                    //                    #endregion


                    //                    #region 应付款项
                    //                    //找到新增的发票进行插入OA 系统中
                    //                    addSql = string.Format(@"
                    //INSERT INTO [KIS].[KingdeeInvoice].[dbo].[Payable]
                    //           ([SupplierName]
                    //           ,[InvoiceNumber]
                    //           ,[Total]
                    //           ,[CreateDate]
                    //           ,[IsAccount]
                    //           ,[Received]
                    //           ,[Isorder]
                    //           ,[BillDate])     
                    //select t_Item.FName as SUPPLIERNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as T0TAL,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
                    //, isnull(Dao_T0TAL,0) as RECEIVED  ,NULL as Isorder,
                    //FTransDate as BillDate from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
                    //left join t_Item on t_Item.FItemID = t_ItemDetail.F3001
                    //left join (
                    //select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //where faccountid in (select faccountid from t_Account where fname = '应付账款')
                    //and FDC = 1  group by FTransNo
                    //) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
                    //where faccountid in (select faccountid from t_Account where fname = '应付账款') 
                    //and FDC = 0 and t_VoucherEntry.FTransNo is not null
                    //and FTransDate>='{0} 00:00:00' and FTransDate<='{1} 23:59:59'       
                    //and t_VoucherEntry.FTransNo not in (
                    //select InvoiceNumber from KIS.KingdeeInvoice.DBO.Payable
                    //)  ; ", dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"));

                    //                    //找到新增的发票信息 进行更新
                    //                    updateSql = string.Format(@"update KIS11 set
                    //KIS11.SupplierName=KIS10.SupplierName,
                    //KIS11.Total=KIS10.T0TAL,
                    //KIS11.CREATEDATE=KIS10.CREATEDATE,
                    //KIS11.ISACCOUNT=KIS10.ISACCOUNT,
                    //KIS11.RECEIVED=KIS10.RECEIVED,
                    //KIS11.BillDate=KIS10.BillDate
                    //from 
                    //(
                    //select t_Item.FName as SUPPLIERNAME,t_VoucherEntry.FTransNo as INVOICENUMBER,FAmount as T0TAL,FDate as CREATEDATE ,case when Dao_T0TAL=FAmount then 1 when isnull(Dao_T0TAL,0)=0 then 0 else 2  end as ISACCOUNT 
                    //, isnull(Dao_T0TAL,0) as RECEIVED  ,0 as Isorder,
                    //FTransDate as BillDate from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //left join t_ItemDetail on t_ItemDetail.FdetailId = t_VoucherEntry.FDetailID
                    //left join t_Item on t_Item.FItemID = t_ItemDetail.F3001
                    //left join (
                    //select FTransNo, sum(FAmount) as Dao_T0TAL from t_Voucher
                    //left join t_VoucherEntry on t_Voucher.FVoucherID = t_VoucherEntry.FVoucherID
                    //where faccountid in (select faccountid from t_Account where fname = '应付账款')
                    //and FDC = 1  group by FTransNo
                    //) as DaoKuan on DaoKuan.FTransNo = t_VoucherEntry.FTransNo
                    //where faccountid in (select faccountid from t_Account where fname = '应付账款') 
                    //and FDC = 0 and t_VoucherEntry.FTransNo is not null
                    //and FTransDate>='{0} 00:00:00' and FTransDate<='{1} 23:59:59'     ) AS KIS10 
                    //left join KIS.KingdeeInvoice.DBO.Payable as KIS11 ON KIS10.INVOICENUMBER=KIS11.INVOICENUMBER
                    //where  KIS11.INVOICENUMBER is not null", dateStart.Value.ToString("yyyy-MM-dd"), dateEnd.Value.ToString("yyyy-MM-dd"));

                    //                    //删除发票信息,清理下OA 系统中 垃圾发票号，（金蝶把发票删除了 OA 也要删除）
                    //                    deleteSQL = string.Format("delete from KIS.KingdeeInvoice.DBO.Payable where invoiceNumber not in (select FTransNo from t_VoucherEntry)");


                    //                    ExeCommand(updateSql, newConn);
                    //                    ExeCommand(addSql, newConn);
                    //                    ExeCommand(deleteSQL, newConn);

                    //                    #endregion

                    ExeCommand("Invoice_Proc", newConn, Convert.ToDateTime(dateStart.Value.ToString("yyyy-MM-dd")), Convert.ToDateTime(dateEnd.Value.ToString("yyyy-MM-dd")));
                    ExeCommand("Payable_Proc", newConn, Convert.ToDateTime(dateStart.Value.ToString("yyyy-MM-dd")), Convert.ToDateTime(dateEnd.Value.ToString("yyyy-MM-dd")));
                    lblMessage.Text = string.Format("执行成功！！！时间：{0}", DateTime.Now.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("系统异常！错误信息:" + ex.Message);
                    lblMessage.Text = "执行异常，请检查！";

                }
                finally
                {
                    btnOK.Enabled = true;
                }
            }));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void OK()
        {
            if (dateStart.Value > dateEnd.Value)
            {
                MessageBox.Show("发票开始时间必须小于结束时间");
                return;
            }
            lblMessage.Text = "开始执行...";
            btnOK.Enabled = false;
            Application.DoEvents();
            Thread thread = new Thread(DoWork) { IsBackground = true };
            thread.Start();
        }

        private void timerJOB_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == dateJOB.Value.Hour && DateTime.Now.Minute == dateJOB.Value.Minute)
            {
                OK();
            }
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
