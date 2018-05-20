using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA
{
    public class NumHelp
    {
        public static string FormatTwo(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return string.Format("{0:f2}",obj);
            }
            return "";
        }

        public static string FormatFour(object obj)
        {
            if (obj != null&&!string.IsNullOrEmpty(obj.ToString()))
            {
                return string.Format("{0:f4}", obj);
            }
            return "";
        }
    }
}
