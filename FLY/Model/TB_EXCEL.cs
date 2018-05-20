using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model
{
    public class TB_EXCEL
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public string Table_Name { get; set; }

        public string SheetName { get; set; }

        public string UserName { get; set; }

        public int UpState { get; set; }

        public string FileAddress { get; set; }

        public string UpStateString
        {
            get
            {
                if (UpState == 1)
                {
                    return "准备";
                }
                if (UpState == 2)
                {
                    return "执行中";
                }
                if (UpState == 3)
                {
                    return "执行完毕";
                }
                return "";
            }
        }

        public string Remark { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public int IsParent { get; set; }
    }
}