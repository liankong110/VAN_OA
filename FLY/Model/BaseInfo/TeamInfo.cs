using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    public class TeamInfo
    {
        public TeamInfo()
        { }
        #region Model
        private int _id;
        private string _teamlever = "";
        private string _sex ;
        private string _cardno = "";
        private int _brithdayyear = 0;
        private int _brithdaymonth = 0;
        private DateTime _contractstarttime;
        private int _teampersoncount = 0;
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
        public string TeamLever
        {
            set { _teamlever = value; }
            get { return _teamlever; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BrithdayYear
        {
            set { _brithdayyear = value; }
            get { return _brithdayyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BrithdayMonth
        {
            set { _brithdaymonth = value; }
            get { return _brithdaymonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ContractStartTime
        {
            set { _contractstarttime = value; }
            get { return _contractstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TeamPersonCount
        {
            set { _teampersoncount = value; }
            get { return _teampersoncount; }
        }
        #endregion Model

        public string Phone { get; set; }
    }
}