using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA
{
    /// <summary>
    /// 系统配置信息，全局静态变量和常量定义
    /// </summary>
    public class ServiceAppSetting
    { /// <summary>
        /// 日志句柄
        /// </summary>
        /// <param name="strLogMessage"></param>
        /// <param name="type"></param>
        public delegate void LoggerHanderDelegate(string strLogMessage, string type);

        public static LoggerHanderDelegate LoggerHander;
    }
}
