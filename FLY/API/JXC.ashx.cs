using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.API
{
    /// <summary>
    /// JXC 的摘要说明
    /// </summary>
    public class JXC : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["name"] == "getfptype"&&context.Request["PONO"]!="")
            {
                string checkFPType = string.Format("select FpType from CG_POOrder WHERE IFZhui=0 and PONo='{0}' AND Status='通过'", context.Request["PONO"]);
                context.Response.Write( DBHelp.ExeScalar(checkFPType).ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}