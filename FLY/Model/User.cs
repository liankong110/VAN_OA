namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class User
    {
        private int id;
        private string loginAddress;
        private string loginCPosition;
        private DateTime loginCreateTime;
        private string loginId;
        private bool loginIFXCBM;
        private string loginIPosition;
        private string loginMemo;
        private string loginName;
        private string loginPhone;
        private string loginPwd;
        private string loginRemark;
        private string loginStatus;
        private string loginTmpPwd;
        private string loginUserNO;

        private string zhiwu;
        public string Zhiwu
        {
            get { return zhiwu; }
            set { zhiwu = value; }
        }
        private int reportTo;
        public int ReportTo
        {
            get { return reportTo; }
            set { reportTo = value; }
        }

        private string reportToName;
        public string ReportToName
        {
            get { return reportToName; }
            set { reportToName = value; }
        }
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string LoginAddress
        {
            get
            {
                return this.loginAddress;
            }
            set
            {
                this.loginAddress = value;
            }
        }

        public string LoginCPosition
        {
            get
            {
                return this.loginCPosition;
            }
            set
            {
                this.loginCPosition = value;
            }
        }

        public DateTime LoginCreateTime
        {
            get
            {
                return this.loginCreateTime;
            }
            set
            {
                this.loginCreateTime = value;
            }
        }

        public string LoginId
        {
            get
            {
                return this.loginId;
            }
            set
            {
                this.loginId = value;
            }
        }

        public bool LoginIFXCBM
        {
            get
            {
                return this.loginIFXCBM;
            }
            set
            {
                this.loginIFXCBM = value;
            }
        }

        public string LoginIPosition
        {
            get
            {
                return this.loginIPosition;
            }
            set
            {
                this.loginIPosition = value;
            }
        }

        public string LoginMemo
        {
            get
            {
                return this.loginMemo;
            }
            set
            {
                this.loginMemo = value;
            }
        }

        public string LoginName
        {
            get
            {
                return this.loginName;
            }
            set
            {
                this.loginName = value;
            }
        }

        public string LoginPhone
        {
            get
            {
                return this.loginPhone;
            }
            set
            {
                this.loginPhone = value;
            }
        }

        public string LoginPwd
        {
            get
            {
                return this.loginPwd;
            }
            set
            {
                this.loginPwd = value;
            }
        }

        public string LoginRemark
        {
            get
            {
                return this.loginRemark;
            }
            set
            {
                this.loginRemark = value;
            }
        }

        public string LoginStatus
        {
            get
            {
                return this.loginStatus;
            }
            set
            {
                this.loginStatus = value;
            }
        }

        public string LoginTmpPwd
        {
            get
            {
                return this.loginTmpPwd;
            }
            set
            {
                this.loginTmpPwd = value;
            }
        }

        public string LoginUserNO
        {
            get
            {
                return this.loginUserNO;
            }
            set
            {
                this.loginUserNO = value;
            }
        }

        public string CompanyCode { get; set; }
        public int CompanyId { get; set; }

        /// <summary>
        /// AE总成本
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// 是否是特殊用户
        /// </summary>
        public bool IsSpecialUser { get; set; }

        public string LoginName_ID { get { return loginId +"-"+ LoginName; } }

        public string Mobile { get; set; }
        public string CardNO { get; set; }
        public string CityNo { get; set; }
        public string Education { get; set; }
        public string School { get; set; }
        public string SchoolDate { get; set; }
        public string Title { get; set; }
        public string Political { get; set; }
        public string HomeAdd { get; set; }
        public string WorkDate { get; set; }

        public string SheBaoCode { get; set; }
    }
}

