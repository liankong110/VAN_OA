using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VAN_OA.Model.KingdeeInvoice
{

    /// <summary>
    /// Invoice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Invoice
    {
        public Invoice()
        { }
        #region Model
        private int _id;
        private string _guestname;
        private string _invoicenumber;
        private decimal _total;
        private DateTime _createdate;
        private int _isaccount = 0;
        private decimal _received = 0M;
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNumber
        {
            set { _invoicenumber = value; }
            get { return _invoicenumber; }
        }
        /// <summary>
        /// 金额 
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 开具日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 到账否 1：到账   0：未到账（缺省是0）
        /// </summary>
        public int IsAccount
        {
            set { _isaccount = value; }
            get { return _isaccount; }
        }
        /// <summary>
        /// 0是未到账,1是全到帐 2是为未全到帐
        /// </summary>
        public virtual string IsAccountString
        {
            get
            {
                if (IsAccount == 0)
                {
                    return "未到账";
                }
                if (IsAccount == 1)
                {
                    return "全到帐";
                }
                if (IsAccount == 2)
                {
                    return "未全到帐";
                }
                return "";
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public decimal Received
        {
            set { _received = value; }
            get { return _received; }
        }

        /// <summary>
        /// 表明是真实订单，如果赋值为1 时，表明此发票是过账用的
        /// </summary>
        public bool Isorder { get; set; }

        public bool IsDeleted { get; set; }

        public decimal NoReceived
        {
            get
            {
                return Total - Received;
            }
        }
        #endregion Model

        /// <summary>
        /// 到款比例=到款金额/发票金额
        /// </summary>
        public decimal DaoKuanBL
        {
            get
            {

                if (Total != 0)
                {
                    return Received / Total*100;
                }
                return 0;
            }
        }
        /// <summary>
        /// 未到款比例=未到款金额/发票金额
        /// </summary>
        public decimal WeiDaoKuanBL
        {
            get
            {

                if (Total != 0)
                {
                    return NoReceived / Total*100;
                }
                return 0;
            }
        }

        public DateTime BillDate { get;  set; }
        public string FPNoStyle { get; internal set; }
    }
}
