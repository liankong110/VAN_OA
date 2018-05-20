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
    [Serializable]
    public partial class tb_EForms
    {
        public tb_EForms()
        { }

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        #region Model
        private int _id;
        private int _e_id;
        private string _audper_Name;
        public string Audper_Name
        {
            get { return _audper_Name; }
            set { _audper_Name = value; }
        }

        private int _audper;

        private string _consignor_Name; 
        public string Consignor_Name
        {
            get { return _consignor_Name; }
            set { _consignor_Name = value; }
        }
        
        private int _consignor;
        private DateTime _dotime;
        private string _idea;
        private string _resultstate;
        private int _prosids;
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
        public int e_Id
        {
            set { _e_id = value; }
            get { return _e_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int audPer
        {
            set { _audper = value; }
            get { return _audper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int consignor
        {
            set { _consignor = value; }
            get { return _consignor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime doTime
        {
            set { _dotime = value; }
            get { return _dotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string idea
        {
            set { _idea = value; }
            get { return _idea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string resultState
        {
            set { _resultstate = value; }
            get { return _resultstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int prosIds
        {
            set { _prosids = value; }
            get { return _prosids; }
        }
        #endregion Model


        //private int a_Index;
        //public int A_Index
        //{
        //    get { return a_Index; }
        //    set { a_Index = value; }
        //}
        //private string a_RoleName;
        //public string A_RoleName
        //{
        //    get { return a_RoleName; }
        //    set { a_RoleName = value; }
        //}
        //private bool a_IFEdit;
        //public bool A_IFEdit
        //{
        //    get { return a_IFEdit; }
        //    set { a_IFEdit = value; }
        //}
    }
}
