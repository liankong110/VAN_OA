using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    public class Contract
    {
        public Contract()
        { }
        #region Model
        private int _id;
        private int _contract_type = 0;
        private string _contract_use = "";
        private string _contract_no = "";
        private string _contract_unit = "";
        private string _contract_name = "";
        private string _contract_summary = "";
        private decimal _contract_total = 0M;
        private DateTime _contract_date;
        private int _contract_pagecount = 2;
        private int _contract_bcount = 1;
        private string _pono = "";
        private string _ae = "";
        private string _contract_brokerage = "";
        private bool _contract_issign = false;
        private string _contract_local = "";
        private int _contract_year = 0;
        private int _contract_month = 0;
        private string _contract_remark = "";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public string Contract_Type_String
        {
            get { if (Contract_Type == 1) { return "采购合同"; } return "销售合同"; }
        }
        /// <summary>
        /// 合同类别（下拉）：采购合同，销售合同
        /// </summary>
        public int Contract_Type
        {
            set { _contract_type = value; }
            get { return _contract_type; }
        }
        /// <summary>
        /// 合同类型（下拉）：供货；系统集成；人工费；工程；咨询；设计；服务
        /// </summary>
        public string Contract_Use
        {
            set { _contract_use = value; }
            get { return _contract_use; }
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Contract_No
        {
            set { _contract_no = value; }
            get { return _contract_no; }
        }
        /// <summary>
        /// 合约单位
        /// </summary>
        public string Contract_Unit
        {
            set { _contract_unit = value; }
            get { return _contract_unit; }
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Contract_Name
        {
            set { _contract_name = value; }
            get { return _contract_name; }
        }
        /// <summary>
        /// 合同摘要
        /// </summary>
        public string Contract_Summary
        {
            set { _contract_summary = value; }
            get { return _contract_summary; }
        }
        /// <summary>
        /// 总金额 （小数点2位）
        /// </summary>
        public decimal Contract_Total
        {
            set { _contract_total = value; }
            get { return _contract_total; }
        }
        /// <summary>
        /// 签订日期时间
        /// </summary>
        public DateTime Contract_Date
        {
            set { _contract_date = value; }
            get { return _contract_date; }
        }
        /// <summary>
        /// 合同总份数
        /// </summary>
        public int Contract_PageCount
        {
            set { _contract_pagecount = value; }
            get { return _contract_pagecount; }
        }
        /// <summary>
        /// 己方份数（缺省1）
        /// </summary>
        public int Contract_BCount
        {
            set { _contract_bcount = value; }
            get { return _contract_bcount; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// AE
        /// </summary>
        public string AE
        {
            set { _ae = value; }
            get { return _ae; }
        }
        /// <summary>
        /// 经手人
        /// </summary>
        public string Contract_Brokerage
        {
            set { _contract_brokerage = value; }
            get { return _contract_brokerage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Contract_IsSign
        {
            set { _contract_issign = value; }
            get { return _contract_issign; }
        }

        public string Contract_IsSign_String
        {
            get { if (Contract_IsSign) { return "是"; } return "否"; }
        }

        /// <summary>
        /// 存放位置 [A,B...Z(下拉货架号) 
        /// </summary>
        public string Contract_Local
        {
            set { _contract_local = value; }
            get { return _contract_local; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        public int Contract_Year
        {
            set { _contract_year = value; }
            get { return _contract_year; }
        }
        /// <summary>
        /// 月份
        /// </summary>
        public int Contract_Month
        {
            set { _contract_month = value; }
            get { return _contract_month; }
        }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string Contract_Local_String
        {
            get { return Contract_Local + "-" + Contract_Year + "-" + Contract_Month; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Contract_Remark
        {
            set { _contract_remark = value; }
            get { return _contract_remark; }
        }

        /// <summary>
        /// 合同总份数
        /// </summary>
        public int Contract_AllCount { get; set; }

        /// <summary>
        /// 总份数/己份数
        /// </summary>
        public string Contract_AllCount_BCount
        {
            get { return Contract_AllCount+"/" + Contract_BCount; }
        }

        /// <summary>
        /// 合同编号 采购合同C开头 销售合同X 开头
        /// </summary>
        public string Contract_ProNo { get; set; }
 
    /// <summary>
    /// 是否需要填写项目
    /// </summary>
    public bool Contract_IsRequire { get; set; }

        public string Contract_IsRequireString { get { if (Contract_IsRequire) { return "是"; } return "否"; } }


      
        #endregion Model
    }
}