using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// Petitions:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Petitions
    {
        public int No { get; set; }
        public Petitions()
        { }
        #region Model
        private int _id;
        private string _type = "";
        private string _number = "";
        private string _guestname = "";
        private string _salesunit = "";
        private string _name = "";
        private string _summary = "";
        private decimal _total = 0M;
        private DateTime _signdate = Convert.ToDateTime("1900-1-1");
        private int _sumpages = 0;
        private int _sumcount = 1;
        private int _bcount = 1;
        private string _pono = "";
        private string _ae;
        private string _handler = "";
        private bool _iscolse = false;
        private string _local = "";
        private int _l_year = 0;
        private int _l_month = 0;
        private string _remark = "";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
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
        public string SalesUnit
        {
            set { _salesunit = value; }
            get { return _salesunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SignDate
        {
            set { _signdate = value; }
            get { return _signdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SumPages
        {
            set { _sumpages = value; }
            get { return _sumpages; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SumCount
        {
            set { _sumcount = value; }
            get { return _sumcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BCount
        {
            set { _bcount = value; }
            get { return _bcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PoNo
        {
            set { _pono = value; }
            get { return _pono; }
        }
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
        public string Handler
        {
            set { _handler = value; }
            get { return _handler; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsColse
        {
            set { _iscolse = value; }
            get { return _iscolse; }
        }
        /// <summary>
        /// 存放位置 [A,B...Z(下拉货架号) 
        /// </summary>
        public string Local
        {
            set { _local = value; }
            get { return _local; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        public int L_Year
        {
            set { _l_year = value; }
            get { return _l_year; }
        }
        /// <summary>
        /// 月份
        /// </summary>
        public int L_Month
        {
            set { _l_month = value; }
            get { return _l_month; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

        public bool IsRequire { get; set; }
        public string IsRequireString { get { if (IsRequire) { return "是"; } return "否"; } }

        public string IsColseString { get { if (IsColse) { return "是"; } return "否"; } }

        
        /// <summary>
        /// 存放位置
        /// </summary>
        public string Local_String
        {
            get { return Local + "-" + L_Year + "-" + L_Month; }
        }

        public string OldIndex { get; set; }

    }
}