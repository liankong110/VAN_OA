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
    /// Tb_Inventory:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tb_Inventory
    {
        public Tb_Inventory()
        { }
        #region Model
        private int _id;
        private string _invname;
        private decimal _invnum;
        private string _invunit;
        private string _invno;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 存货名称
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal InvNum
        {
            set { _invnum = value; }
            get { return _invnum; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string InvUnit
        {
            set { _invunit = value; }
            get { return _invunit; }
        }
        /// <summary>
        /// 序列号
        /// </summary>
        public string InvNo
        {
            set { _invno = value; }
            get { return _invno; }
        }
        #endregion Model


        /// <summary>
        /// 区域：1.全部  缺省 2….27 A,B,C…Z  
        /// </summary>
        public string GoodArea { get; set; }
        /// <summary>
        /// 货架号：1.全部  缺省 2….51 1,..50
        /// </summary>
        public string GoodNumber { get; set; }
        /// <summary>
        /// 层数：1.全部  缺省 2….21 1,2,3…20 
        /// </summary>
        public string GoodRow { get; set; }
        /// <summary>
        /// 部位：1.全部  缺省 2….21 1,2,3…20
        /// </summary>
        public string GoodCol { get; set; }
        /// <summary>
        /// 仓位 =区域+货架号+层数+部位
        /// </summary>
        public string GoodAreaNumber { get; set; }

        public string InvUser { get; set; }

    }
}
