using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.ReportForms
{ 
    /// <summary>
    /// 车辆加油记录表
    /// </summary>
    [Serializable]
    public class TB_CarOilMaintenance
    {
        public TB_CarOilMaintenance()
        { }
        #region Model
        private int _id;
        private string _cardno;
        private DateTime _maintenancetime;
         
        private string _remark;
        private int _createuser;
        private DateTime _createtime;

       

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 保养时间点
        /// </summary>
        public DateTime MaintenanceTime
        {
            set { _maintenancetime = value; }
            get { return _maintenancetime; }
        }
        
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
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

        private decimal? _uptotal;
        private decimal? _oiltotal;
        private decimal? _addtotal;
        private decimal? _total;
        /// <summary>
		/// 
		/// </summary>
		public decimal? UpTotal
		{
			set{ _uptotal=value;}
			get{return _uptotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OilTotal
		{
			set{ _oiltotal=value;}
			get{return _oiltotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AddTotal
		{
			set{ _addtotal=value;}
			get{return _addtotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Total
		{
			set{ _total=value;}
			get{return _total;}
		}


        private DateTime? chongZhiDate;
        public DateTime? ChongZhiDate
        {
            get { return chongZhiDate; }
            set { chongZhiDate = value; }
        }

    }
}
