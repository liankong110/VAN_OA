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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.Collections.Generic;

namespace VAN_OA.EFrom
{
    public partial class FundsUsePrint : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();

        protected void btnClose_Click(object sender, EventArgs e)
        {

           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              


                if (base.Request["ProId"] != null)
                {


                    if (Request["allE_id"] == null)//单据增加
                    {

                    }
                    else//单据审批
                    {





                        #region  加载 请假单数据
                        tb_FundsUseService fundsSer = new tb_FundsUseService();

                        tb_FundsUse fundsModel = fundsSer.GetModel(Convert.ToInt32(Request["allE_id"]));


                        lblDepartName.Text = fundsModel.DepartName;
                        lblDateTime.Text = fundsModel.datetiem.ToString();
                        lblName.Text = fundsModel.LoginName;
                        lblTotal.Text = fundsModel.total.ToString();
                        lblUseTo.Text = fundsModel.useTo;
                        lblType.Text=fundsModel.type ;   
                        lblTotalDa.Text = ConvertMoney(fundsModel.total);
                        lblIdea.Text = fundsModel.Idea;
                        lblInvoce.Text = fundsModel.Invoce;
                        lblHouseNo.Text = fundsModel.HouseNo;

                        
                        lblDepartName1.Text = fundsModel.DepartName;
                        lblDate1.Text = fundsModel.datetiem.ToString();
                        lblName1.Text = fundsModel.LoginName;
                        lblTotal1.Text = fundsModel.total.ToString();
                        lblUseTo1.Text = fundsModel.useTo;
                        lblTyp1.Text = fundsModel.type;
                        lblDaTotal1.Text = ConvertMoney(fundsModel.total);
                        lblIdea1.Text = fundsModel.Idea;
                        lblInvoce1.Text = fundsModel.Invoce;
                        lblHouseN01.Text = fundsModel.HouseNo;
                        

                        lblDepartName2.Text = fundsModel.DepartName;
                        lblDate2.Text = fundsModel.datetiem.ToString();
                        lblName2.Text = fundsModel.LoginName;
                        lblTotal2.Text = fundsModel.total.ToString();
                        lblUserTo2.Text = fundsModel.useTo;
                        
                        lblType2.Text = fundsModel.type;
                        lblDaTotal2.Text = ConvertMoney(fundsModel.total);

                        lblIdea2.Text = fundsModel.Idea;
                        lblInvoce2.Text = fundsModel.Invoce;
                        lblHouseNo2.Text = fundsModel.HouseNo;
                        #endregion
                    }
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
