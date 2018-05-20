using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Hosting;
using System.IO;

namespace VAN_OA
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 日志句柄
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IQuartz demo;

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="strLogMsg"></param>
        /// <param name="type"></param>
        private static void WriteLog(string strLogMsg, string type)
        {
            //switch (type)
            //{
            //    case "Error":
            //        log.Error(strLogMsg);
            //        break;
            //    default:
            //        log.Info(strLogMsg);
            //        break;
            //}

            var filepath =
                  HostingEnvironment.MapPath("~/Logs/");
            var filename = filepath + "Job_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            File.AppendAllText(filename, strLogMsg);
            if (
                File.Exists(filepath + "Job_" +
                            DateTime.Now.AddMonths(-1).ToString() + ".txt"))
            {
                File.Delete(filepath + "Job_" +
                            DateTime.Now.AddMonths(-1).ToString() + ".txt");
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            //启动JOB
            try
            {
                ServiceAppSetting.LoggerHander = WriteLog;

                //demo = new MYQuartz();
                //demo.Run();

                MinuteJobManager.Instance.SetMinuteJob(typeof(MinuteJob));
                MinuteJobManager.Instance.Start();

                ServiceAppSetting.LoggerHander.Invoke("启动成功", "Error");

            }
            catch (Exception ex)
            {
                ServiceAppSetting.LoggerHander.Invoke(ex.Message, "Error");
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            var objErr = Server.GetLastError().GetBaseException();
            var error = "发生异常页: " + Request.Url + "\r\n";
            error += "异常信息: " + objErr.Message;
            //log.Error(error);
            ServiceAppSetting.LoggerHander.Invoke(error, "Error");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            demo.Close();
        }
    }
}