﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.JXC;

namespace VAN_OA.Fin
{
    public partial class EI_BlankCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegistryKey pregkey;
            //pregkey = Registry.CurrentUser.OpenSubKey("Software//Microsoft//Internet Explorer//PageSetup//", false);
            if (!IsPostBack)
            {
                if (Session["ElectronicInvoice"] != null)
                {
                    var model = Session["ElectronicInvoice"] as ElectronicInvoice;
                    txtPrNo.Text = model.ProNo;

                    txtDate.Text = string.Format("{0:yyyy    MM    dd}", DateTime.Now);
                    txtShouKuanRen.Text = model.SupplieSimpeName;
                    txtTotal.Text = string.Format("¥{0:n2}", model.ActPay);
                    txtUse.Text = model.Use;

                    txtDaShouKuan.Text = model.SupplierName;
                    txtDaTotal.Text = RMBCapitalization.RMBAmount(Convert.ToDouble(model.ActPay.ToString("f2"))); 
                    txtDaUse.Text = model.Use;
                    txtDaNum.Text = "¥" + model.ActPay.ToString("f2").Replace(".", "");
                    txtDaDate.Text = ConvertNum(DateTime.Now.Year.ToString()) + "      " +
                      (DateTime.Now.Month > 9 ? "" : "零") + ConvertMoney(DateTime.Now.Month).Replace("圆整", "") + "       "
                        + (DateTime.Now.Day > 9 ? "" : "零") + ConvertMoney(DateTime.Now.Day).Replace("圆整", "");
                    if (!string.IsNullOrEmpty(model.Person))
                    {
                        var person = new Invoice_PersonService().GetListArray(string.Format(" name='{0}'", model.Person))[0];
                        txtDaRemark.Text = person.CardNo;
                    }
                }
            }
        }


        public string ConvertNum(string Num)
        {
            string result = "";
            int i = 0;
            foreach (char c in Num)
            {
                if (i > 0 && Convert.ToInt32(Num) > 9)
                {

                }
                string da = "";
                if (c.ToString() == "0")
                {
                    da = "零";
                }
                else if (c.ToString() == "1")
                {
                    da = "壹";
                }
                else if (c.ToString() == "2")
                {
                    da = "贰";
                }
                else if (c.ToString() == "3")
                {
                    da = "叁";
                }
                else if (c.ToString() == "4")
                {
                    da = "肆";
                }
                else if (c.ToString() == "5")
                {
                    da = "伍";
                }
                else if (c.ToString() == "6")
                {
                    da = "陆";
                }
                else if (c.ToString() == "7")
                {
                    da = "柒";
                }
                else if (c.ToString() == "8")
                {
                    da = "捌";
                }
                else if (c.ToString() == "9")
                {
                    da = "玖";
                }


                result += da;
                i++;

            }
            return result;
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var model = Session["ElectronicInvoice"] as ElectronicInvoice;
            model.ProNo = txtPrNo.Text;
            model.SupplieSimpeName = txtShouKuanRen.Text;
            model.Use = txtUse.Text;
            model.SupplierName = txtDaShouKuan.Text;
            model.Use = txtDaUse.Text;
            Session["ElectronicInvoice"] = model;
            base.Response.Redirect("~/Fin/EI_BlankCheckPrint.aspx");
        }
    }
}