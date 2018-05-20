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

namespace VAN_OA.Model.EFrom
{
    /// <summary>
    /// Tb_ExpInvs:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tb_ExpInvs
    {
        public Tb_ExpInvs()
        { }
        public bool IfUpdate { get; set; }
        #region Model
        private int _id;
        private int _pid;
        private int _invid;
        private decimal _expnum;
        private string _expuse;
        private string _expstate;
        private string _expremark;
        private DateTime? _returntime;
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
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public int InvId
        {
            set { _invid = value; }
            get { return _invid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal ExpNum
        {
            set { _expnum = value; }
            get { return _expnum; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string ExpUse
        {
            set { _expuse = value; }
            get { return _expuse; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string ExpState
        {
            set { _expstate = value; }
            get { return _expstate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string ExpRemark
        {
            set { _expremark = value; }
            get { return _expremark; }
        }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime
        {
            set { _returntime = value; }
            get { return _returntime; }
        }
        #endregion Model

        /// <summary>
        /// 货品名称
        /// </summary>
        public string InvName { get; set; }



        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime? ExpTime { get; set; }

          /// <summary>
        /// 编号
        /// </summary>
        public string ProNo { get; set; }

        public string LoginName { get; set; }

        public string ExpInvState { get; set; }
    }
}
