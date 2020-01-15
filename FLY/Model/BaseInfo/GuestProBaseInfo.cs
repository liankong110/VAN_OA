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

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// GuestProBaseInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GuestProBaseInfo
    {
        public GuestProBaseInfo()
        { }
        #region Model
        private int _id;
        private int _guestpro;
        private decimal _jilixishu;
        private int _xishu = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 定义 0=公司资源  1=自我开拓
        /// </summary>
        public int GuestPro
        {
            set { _guestpro = value; }
            get { return _guestpro; }
        }

        public string GuestProString
        {
            get
            {
                
                if (GuestPro == -2)
                {
                    return "全部";
                }
                return VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(GuestPro); ;
                
            }        
    
        }
        /// <summary>
        /// 0---1000 小数取2位
        /// </summary>
        public decimal JiLiXiShu
        {
            set { _jilixishu = value; }
            get { return _jilixishu; }
        }
        /// <summary>
        ///  系数可变（1：可变  0 不可变）
        /// </summary>
        public int XiShu
        {
            set { _xishu = value; }
            get { return _xishu; }
        }
        #endregion Model
        /// <summary>
        /// 有效期
        /// </summary>
        public int GuestMonth { get; set; }

    }
}
