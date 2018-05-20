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

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// TB_PO:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_PO
    {
        public TB_PO()
        { }

        private string createUser_Name;
        public string CreateUser_Name
        {
            get { return createUser_Name; }
            set { createUser_Name = value; }
        }
        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        #region Model
        private int _id;
        private DateTime _datatime;
        private string _unitname;
        private string _invname;
        private string _unit;
        private decimal _num;
        private decimal _price;
        private string _seller;
        private DateTime _createtime;
        private int _createuser;
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
        public DateTime DataTime
        {
            set { _datatime = value; }
            get { return _datatime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitName
        {
            set { _unitname = value; }
            get { return _unitname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        #endregion Model

    }
}
