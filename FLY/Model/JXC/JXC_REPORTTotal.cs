using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// JXC_REPORT:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class JXC_REPORTTotal
    {
        private string _pono;
        private DateTime _podate;
        private string _guestname;
        private decimal _goodselltotal;
        private decimal _goodtotal;
        private decimal _maolitotal;
        private string _fptotal;
        private decimal _zhangqitotal;
        private int _truezhangqi;
        private string _ae;
        private string _inside;
        private decimal _aeper;
        private decimal _insideper;
        /// <summary>
        /// 
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PODate
        {
            set { _podate = value; }
            get { return _podate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal goodSellTotal
        {
            set { _goodselltotal = value; }
            get { return _goodselltotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal goodTotal
        {
            set { _goodtotal = value; }
            get { return _goodtotal; }
        }
        /// <summary>
        /// 项目净利
        /// </summary>
        public decimal maoliTotal
        {
            set { _maolitotal = value; }
            get { return _maolitotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FPTotal
        {
            set { _fptotal = value; }
            get { return _fptotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ZhangQiTotal
        {
            set { _zhangqitotal = value; }
            get { return _zhangqitotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int trueZhangQi
        {
            set { _truezhangqi = value; }
            get { return _truezhangqi; }
        }

        /// <summary>
        /// 是否全到款
        /// </summary>
        public bool IsQuanDao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AE
        {
            set { _ae = value; }
            get { return _ae; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string INSIDE
        {
            set { _inside = value; }
            get { return _inside; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AEPer
        {
            set { _aeper = value; }
            get { return _aeper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal INSIDEPer
        {
            set { _insideper = value; }
            get { return _insideper; }
        }


        public decimal AETotal { get; set; }

        public decimal InsidTotal { get; set; }

        public decimal InvoiceTotal { get; set; }

        public decimal SellFPTotal { get; set; }

        public decimal TrueLiRun { get; set; }

        public string POName { get; set; }

        public bool IsClose { get; set; }


        public string potype { get; set; }
        public string potypeString { get; set; }

        public decimal itemTotal { get; set; }

        public decimal HuiWuTotal { get; set; }

        public decimal allItemTotal { get; set; }

        public decimal MantTotal { get; set; }

        public string GuestType { get; set; }

        public string GuestProString { get; set; }
        public string PONO_ProType { get; set; }

        public decimal SumPOTotal { get; set; }

        public decimal AllSellTotal { get; set; }

        public string PoNo_FpTotal { get { return PONo + "_" + SellFPTotal.ToString() + "_" + TrueLiRun + "_" + allItemTotal; } }
        /// <summary>
        /// 帐内到款（就是在帐期截止期内到款的金额）
        /// </summary>
        public decimal ZhangNeiDaoTotal { get; set; }

        public bool isNeiDaoTotal = true;

        /// <summary>
        /// 帐内欠款（项目金额-帐内到款）       
        /// </summary>
        public decimal ZhangNeiQianTotal
        {
            get
            {
                if (isNeiDaoTotal)
                {
                    return SumPOTotal - ZhangNeiDaoTotal;
                }
                return 0;

            }
        }

        /// <summary>
        /// 利润扣除（项目净利×帐内欠款/项目总金额）
        /// </summary>
        public decimal LiRunKouChu
        {
            get
            {
                if (SumPOTotal != 0)
                {
                    return maoliTotal * ZhangNeiQianTotal / SumPOTotal;
                }
                return 0;

            }
        }


        /// <summary>
        /// 帐内到款
        /// </summary>
        public decimal? WaiInvoTotal { get; set; }

        /// <summary>
        /// 帐内欠款（项目金额-帐内欠款）
        /// </summary>
        public decimal WaiQianKuan
        {
            get
            {
                //if (WaiInvoTotal == null)
                //{
                //    return 0;
                //}
                return goodSellTotal - (WaiInvoTotal ?? 0);

            }
        }

        //public decimal WaiQianKuan1
        //{
        //    get
        //    {

        //        return goodSellTotal - (WaiInvoTotal ?? 0);

        //    }
        //}



        /// <summary>
        /// 利润扣除（项目净利×帐内欠款/项目总金额）  需要判断是考核内的项目类别
        /// </summary>
        public decimal KouLiRun
        {
            get
            {
                var tiaojian = false;
                if (zhangQI == 1 && trueZhangQi >= BaseKeyValue)
                {
                    tiaojian = true;
                }
                else if (zhangQI == 2 && trueZhangQi > BaseKeyValue)
                {
                    tiaojian = true;
                }
                if (tiaojian && PODate >= QueryDateTime)
                {
                    if (PoTypeList != "-10" && (PoTypeList == "-1" || PoTypeList.Contains(potype)))
                    {
                        decimal kouLiRun = 0;
                        if (goodSellTotal != 0)
                        {
                            if (maoliTotal > 0)
                            {
                                kouLiRun = maoliTotal * (goodSellTotal - (WaiInvoTotal ?? 0)) / goodSellTotal;
                            }
                            else
                            {
                                kouLiRun = -maoliTotal * (goodSellTotal - (WaiInvoTotal ?? 0)) / goodSellTotal;
                            }
                        }
                        return kouLiRun;
                    }
                }
                return 0;

            }
        }


        /// 帐内利润（项目净利×帐内到款/项目总金额） 需要判断是考核内的项目类别
        /// </summary>
        public decimal No_KouLiRun
        {
            get
            {
                if (PODate >= QueryDateTime)
                {
                    if (PoTypeList != "-10" && (PoTypeList == "-1" || PoTypeList.Contains(potype)))
                    {
                        decimal kouLiRun = 0;
                        if (goodSellTotal != 0)
                        {
                            kouLiRun = maoliTotal * (WaiInvoTotal ?? 0) / goodSellTotal;
                        }
                        return kouLiRun;
                    }
                }
                return 0;
            }
        }

        public string PoTypeList { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime QueryDateTime { get; set; }

        /// <summary>
        ///  净利润率=项目净利/销售额 ，如果销售额=0 ，净利润率=0 
        /// </summary>
        public decimal JingLi
        {
            get
            {

                if (goodSellTotal == 0)
                {
                    return 0;
                }
                return maoliTotal / goodSellTotal;
            }
        }

        public DateTime? DisDate { get; set; }

        public string loginName { get; set; }

        public decimal NiHours { get; set; }

        public decimal MyValue { get; set; }

        public decimal allScore { get; set; }
        /// <summary>
        /// 到款率=实到账/项目金额；如项目金额=0,，该项按空白
        /// </summary>
        public decimal? DaoKuanLv
        {
            get
            {
                if (SumPOTotal != 0)
                {
                    return InvoiceTotal / SumPOTotal;
                }
                return null;
            }
        }

        public int Id { get; set; }

        public int BaseKeyValue { get; set; }
        /// <summary>
        /// 账期条件      
        /// 1 实际帐期>=帐期截止期
        /// 2 实际帐期 >帐期截止期      
        /// </summary>
        public int zhangQI { get; set; }


        /// <summary>
        /// 最近开票日
        /// </summary>
        public DateTime? BillDate { get; set; }
        /// <summary>
        /// 计算开票日
        /// </summary>
        public DateTime? JSKaiPiaoDate { get; set; }

        /// <summary>
        /// 经验账期
        /// </summary>
        public int Avg_ZQ { get; set; }

        /// <summary>
        /// 模型
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 预估到款日
        /// </summary>
        public DateTime? YuGuDaoKuanDate { get; set; }

        /// <summary>
        /// 预估到款 （= 发票额-实到账）
        /// </summary>
        public decimal YuGuDaoKuanTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 实际到款日
        /// </summary>
        public DateTime? MaxDaoKuanDate { get; set; }

        /// <summary>
        /// 实际账期
        /// </summary>
        public int ZQ { get; set; }

        /// <summary>
        /// 第一次到款日期
        /// </summary>
        public DateTime? MinDaoKuanTime_ZQ { get; set; }

        /// <summary>
        /// 第一次到款日期
        /// </summary>
        public DateTime? MinBillDate { get; set; }

        public DateTime? MinFPTime { get; set; }
        /// <summary>
        /// 第二次到款
        /// </summary>
        public DateTime? SecondDaoKuanDate { get; set; }

        /// <summary>
        /// 到款次数 --第几次到款
        /// </summary>
        public int? DaoKuanNumber { get; set; }

        /// <summary>
        /// 财务成本
        /// </summary>
        public decimal CaiWuChengBen { get; set; }
        /// <summary>
        /// 第一次出库时间
        /// </summary>
        public DateTime? MinOutDate { get; internal set; }

        /// <summary>
        /// 新利润扣除=利润扣除+财务成本
        /// </summary>
        public decimal NewKouLiRun { get; set; }

        /// <summary>
        /// 新帐内利润=帐内利润-财务成本
        /// </summary>
        public decimal NewNo_KouLiRun { get; set; }

        /// <summary>
        /// 财务成本计量
        /// </summary>
        public string ChengBenJiLiangString { get; set; }
    }

    public class InvoiceSimpDetail
    {
        public DateTime DaoKuanDate { get; set; }

        public string PONO { get; set; }
    }
}
