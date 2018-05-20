using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// tb_ComInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_ComInfo
    {
        public tb_ComInfo()
        { }
        #region Model
        private int _id;
        private string _comname;
        private string _nashuino;
        private string _address;
        private string _combrand;
        private string _invoheader;
        private string _invcontactper;
        private string _invaddress;
        private string _invtel;
        private string _nashuiper;
        private string _brandno;
        private string _comtel;
        private string _comchuanzhen;
        private string _combustel;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string ComName
        {
            set { _comname = value; }
            get { return _comname; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string NaShuiNo
        {
            set { _nashuino = value; }
            get { return _nashuino; }
        }
        /// <summary>
        /// 地址/电话
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 地址/电话
        /// </summary>
        public string ComBrand
        {
            set { _combrand = value; }
            get { return _combrand; }
        }
        /// <summary>
        /// 开票抬头
        /// </summary>
        public string InvoHeader
        {
            set { _invoheader = value; }
            get { return _invoheader; }
        }
        /// <summary>
        /// 联系人-开票信息
        /// </summary>
        public string InvContactPer
        {
            set { _invcontactper = value; }
            get { return _invcontactper; }
        }
        /// <summary>
        /// 注册地址-开票信息
        /// </summary>
        public string InvAddress
        {
            set { _invaddress = value; }
            get { return _invaddress; }
        }
        /// <summary>
        /// 注册电话-开票信息
        /// </summary>
        public string InvTel
        {
            set { _invtel = value; }
            get { return _invtel; }
        }
        /// <summary>
        /// 纳税人登记号
        /// </summary>
        public string NaShuiPer
        {
            set { _nashuiper = value; }
            get { return _nashuiper; }
        }
        /// <summary>
        /// 开户行/帐号
        /// </summary>
        public string brandNo
        {
            set { _brandno = value; }
            get { return _brandno; }
        }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string ComTel
        {
            set { _comtel = value; }
            get { return _comtel; }
        }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string ComChuanZhen
        {
            set { _comchuanzhen = value; }
            get { return _comchuanzhen; }
        }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string ComBusTel
        {
            set { _combustel = value; }
            get { return _combustel; }
        }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Brand { get; set; }
        #endregion Model

    }
}
