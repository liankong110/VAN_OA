using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Quartz.net
{
	public class QuartzManage
	{
		public static string jobName = "";
		public static string jobGroupName = "";
		public static string triggerName = "";
		public static string triggerGroupName = "";

		//创建一个调度工厂
		public static IScheduler scheduler = GetScheduler();

		private static IScheduler GetScheduler()
		{
			StdSchedulerFactory stdSchedulerFactory = new StdSchedulerFactory();
			IScheduler scheduler = stdSchedulerFactory.GetScheduler();
			return scheduler;
		}
	}
}