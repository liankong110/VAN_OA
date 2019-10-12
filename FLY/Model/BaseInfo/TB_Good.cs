using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_Good:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Good
    {
        public TB_Good()
        { }

        public string CreateUserName { get; set; }
        /// <summary>
        /// 小类
        /// </summary>
        public string GoodTypeSmName { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string GoodTypeName { get; set; }
      
        #region Model
        private int _goodid;
        private string _goodno;
        private string _goodname;
        private string _zhuji;
        private int? _goodtypeid;
        private int? _goodsmtypeid;
        private string _goodspec;
        private string _goodmodel;
        private string _goodunit;
        private int _createuserid;
        private DateTime _createtime;
        /// <summary>
        /// ID
        /// </summary>
        public int GoodId
        {
            set { _goodid = value; }
            get { return _goodid; }
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string GoodNo
        {
            set { _goodno = value; }
            get { return _goodno; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string GoodName
        {
            set { _goodname = value; }
            get { return _goodname; }
        }
        /// <summary>
        /// 助记词
        /// </summary>
        public string ZhuJi
        {
            set { _zhuji = value; }
            get { return _zhuji; }
        }
        ///// <summary>
        ///// 类别名称
        ///// </summary>
        //public int? GoodTypeId
        //{
        //    set { _goodtypeid = value; }
        //    get { return _goodtypeid; }
        //}
        ///// <summary>
        ///// 小类
        ///// </summary>
        //public int? GoodSmTypeId
        //{
        //    set { _goodsmtypeid = value; }
        //    get { return _goodsmtypeid; }
        //}
        /// <summary>
        /// 规格
        /// </summary>
        public string GoodSpec
        {
            set { _goodspec = value; }
            get { return _goodspec; }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string GoodModel
        {
            set { _goodmodel = value; }
            get { return _goodmodel; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string GoodUnit
        {
            set { _goodunit = value; }
            get { return _goodunit; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

        /// <summary>
        /// 产地
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 是否是特殊商品
        /// </summary>
        public bool IfSpec { get; set; }

        public decimal DoingNum { get; set; }

        public decimal HouseNum { get; set; }

        public string GoodBrand { get; set; }

        public string Status { get; set; }

        public string ProNo { get; set; }

        public decimal GoodNum { get; set; }

        public decimal GoodPrice { get; set; }

        public decimal GoodTotal { get { return GoodNum * GoodPrice; } }

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

        /// <summary>
        /// 未支付
        /// </summary>
        public decimal NoInvoice { get; set; }
        /// <summary>
        /// 已支付
        /// </summary>
        public decimal HadInvoice { get; set; }

        /// <summary>
        /// 采库需出数
        /// </summary>
        public decimal SumKuXuCai { get; set; }

        public decimal CaiNotCheckNum { get; set; }
        /// <summary>
        /// 滞留库存=库存数量-采库需出数
        /// </summary>
        public decimal ZhiLiuKuCun
        {
            get
            {
                return GoodNum - SumKuXuCai+ CaiNotCheckNum;
            }
        }

    }
}
