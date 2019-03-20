using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class PVChat
    {
        public string name { get; set; }
        public string type { get; set; }

        public string barHeight { get; set; }
        //public List<Point> data = new List<Point>();
        //public List<decimal> data = new List<decimal>();
        public List<List<decimal>> data = new List<List<decimal>>();

        public MarkLine markLine { get; set; }

    }



    public class MarkLine
    {
        public List<MarkLineData> data = new List<MarkLineData>();


    }

    public class MarkLineData
    {
        public string type { get; set; }
        public string name { get; set; }
       
        public int? valueIndex { get; set; }
    }
}