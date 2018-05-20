using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using Microsoft.Win32;


namespace VAN_OA.JXC
{
    public partial class NewWFSell_OrderOutHousePrint : System.Web.UI.Page
    {

        protected Model.JXC.Sell_OrderOutHouse mainModel = new VAN_OA.Model.JXC.Sell_OrderOutHouse();
        protected List<Model.JXC.Sell_OrderOutHouses> modelList = new List<VAN_OA.Model.JXC.Sell_OrderOutHouses>();
        private Sell_OrderOutHouseService mainOutHouseSer = new Sell_OrderOutHouseService();
        private Sell_OrderOutHousesService outHousesSer = new Sell_OrderOutHousesService();
        protected string houseName = "";
        protected decimal total = 0;
        protected string totalDa = "";
        protected int allRows = 0;
        protected int pageSize = 10;

        protected void btnClose_Click(object sender, EventArgs e)
        {


        }
        private void printnull()
        {
            RegistryKey pregkey;
            pregkey = Registry.CurrentUser.OpenSubKey("Software//Microsoft//Internet Explorer//PageSetup//", true);

            //if (pregkey == null)
            //{
            //    Response.Write("<script language='javascript'>alert('键值不存在');</script>");
            //}

            //else
            //{
            //    pregkey.SetValue("footer", "");
            //    pregkey.SetValue("header", "");
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            printnull();//隐藏ie打印的页眉和页脚
            if (!base.IsPostBack)
            {
                //请假单子
                if (base.Request["Id"] != null)
                {
                    mainModel = mainOutHouseSer.GetModel(Convert.ToInt32(Request["Id"]));
                    modelList = outHousesSer.GetListArray(" Sell_OrderOutHouses.id=" + Request["Id"]);
                    if (modelList.Count > 0)
                    {
                        houseName = modelList[0].HouseName;
                    }
                    foreach (var m in modelList)
                    {
                        total += m.GoodSellPriceTotal;
                    }
                    if (modelList.Count % pageSize == 0)
                    {
                        allRows = modelList.Count / pageSize;
                    }
                    else
                    {
                        int cha = 0;
                        if (modelList.Count < pageSize)
                        {
                            cha = pageSize - modelList.Count;
                        }
                        else
                        {
                            cha =pageSize- modelList.Count % pageSize;
                        }
                        for (int i = 0; i < cha; i++)
                        {
                            modelList.Add(new Sell_OrderOutHouses());
                        }
                            allRows = modelList.Count / pageSize ;
                    }
                    totalDa = ConvertMoney(total);
                }

            }
        }


        #region 人民币小写金额转大写金额
        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="Money">接收需要转换的小写金额</param>
        /// <returns>返回大写金额</returns>
        public string ConvertMoney(Decimal Money)
        {
            //金额转换程序
            string MoneyNum = "";//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾圆角分";//模

            MoneyNum = ((long)(Money * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr = MoneyStr + "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零圆")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整") || MoneyStr.Contains("角整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零圆", "圆零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("角整", "角");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            return MoneyStr;
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
