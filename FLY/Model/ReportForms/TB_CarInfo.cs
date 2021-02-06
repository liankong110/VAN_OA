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
    /// TB_CarInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_CarInfo
    {
        public TB_CarInfo()
        { }
        #region Model
        private int _id;
        private string _carno;
        private DateTime? _baoxian;
        private DateTime? _nianjian;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Baoxian
        {
            set { _baoxian = value; }
            get { return _baoxian; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NianJian
        {
            set { _nianjian = value; }
            get { return _nianjian; }
        }
        #endregion Model
        /// <summary>
        /// 品牌型号
        /// </summary>
        public string CarModel { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string CarEngine { get; set; }
        /// <summary>
        /// 车架号
        /// </summary>
        public string CarJiaNo { get; set; }
        /// <summary>
        /// 行驶证号
        /// </summary>
        public string CaiXingShiNo { get; set; }

        public string CarShiBieNO { get; set; }
        /// <summary>
        /// 油价系数
        /// </summary>
        public decimal OilNumber { get; set; }

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop { get; set; }


    }
}
