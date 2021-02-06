


using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using VAN_OA.Dal.KingdeeInvoice;

namespace VAN_OA.Quartz.net
{
	[DisallowConcurrentExecutionAttribute]
	public class Job1 : IJob
	{
		private static readonly object Tclock = new object();

		private readonly ILog _logger = LogManager.GetLogger(typeof(Job1));
		public void Execute(IJobExecutionContext context)
		{
			lock (Tclock)
			{
				//模拟运行该任务需要15秒
				//增加金蝶发票处理工具

				var kISModel = new KISService().GetKIS();
				var invoiceDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + kISModel.InvoiceDate);
				var payableDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + kISModel.PayableDate);

				if (DateTime.Now.Hour == invoiceDate.Hour && DateTime.Now.Minute == invoiceDate.Minute)
				{
					var invoiceJob = new Thread(delegate ()
					{
						try
						{
							_logger.InfoFormat(string.Format("Invoice_Proc-开始! - {0}", DateTime.Now), "");

							var kisServer = new KISService();
							var invoicesList = kisServer.GetInvoiceErrorInfo(kISModel.AccountName);
							if (invoicesList.Count > 0)
							{
								_logger.InfoFormat(string.Format("Invoice_Proc-存在重复发票号数据，JOB 停止！", DateTime.Now), "");
								return;
							}

						//获取选中的数据库连接
						string newConn = new KISService().GetKISDBConn().Replace("AcctCtl", kISModel.AccountName);
							KISService.ExeCommand("Invoice_Proc", newConn, Convert.ToDateTime(kISModel.InvoiceFrom), Convert.ToDateTime(kISModel.InvoiceTo));
							_logger.InfoFormat(string.Format("Invoice_Proc-结束! - {0}", DateTime.Now), "");

						}
						catch (Exception ex)
						{
							_logger.InfoFormat(string.Format("Invoice_Proc-结束! - {0}，执行异常，请检查！错误信息:{1}", DateTime.Now, ex.Message), "");
						}

					})
					{ IsBackground = true };
					invoiceJob.Start();
				}

				if (DateTime.Now.Hour == payableDate.Hour && DateTime.Now.Minute == payableDate.Minute)
				{
					var payableJob = new Thread(delegate ()
					{
						try
						{
							var kisServer = new KISService();
							var payableList = kisServer.GetPayaleErrorInfo(kISModel.AccountName);
							if (payableList.Count > 0)
							{
								_logger.InfoFormat(string.Format("Payable_Proc-存在重复发票号数据，JOB 停止！", DateTime.Now), "");
								return;
							}

							_logger.InfoFormat(string.Format("Payable_Proc-开始! - {0}", DateTime.Now), "");
						//获取选中的数据库连接
						string newConn = new KISService().GetKISDBConn().Replace("AcctCtl", kISModel.AccountName);
							KISService.ExeCommand("Payable_Proc", newConn, Convert.ToDateTime(kISModel.PayableFrom), Convert.ToDateTime(kISModel.PayableTo));
							_logger.InfoFormat(string.Format("Payable_Proc-结束! - {0}", DateTime.Now), "");
						}
						catch (Exception ex)
						{
							_logger.InfoFormat(string.Format("Payable_Proc-结束! - {0}，执行异常，请检查！错误信息:{1}", DateTime.Now, ex.Message), "");
						}
					})
					{ IsBackground = true };
					payableJob.Start();
				}

				_logger.InfoFormat("第1个Job：" + DateTime.Now.ToString());

				//new Job2().Execute(context);
			}
		}
	}
}