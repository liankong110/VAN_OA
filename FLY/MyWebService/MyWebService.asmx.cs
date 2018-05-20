using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using VAN_OA.Dal.JXC;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using VAN_OA.Model.ReportForms;
using System.Text;

namespace VAN_OA.MyWebService
{
    /// <summary>
    /// MyWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {

        VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string[] GetUserName(string prefixText,int count)
        {
            List<VAN_OA.Model.User> userLists = userSer.getAllUserByLoginName(string.Format(" and (loginName like '%{0}%' or loginId LIKE '%{0}%') ", prefixText.Trim()));
            string[] strUsers = new string[userLists.Count];
            for (int i = 0; i < userLists.Count; i++)
            {
                strUsers[i] = userLists[i].LoginName;
            }
            return strUsers;
        }


        VAN_OA.Dal.BaseInfo.TB_GoodService goodSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        Sell_OrderOutHouseService sellOutSer = new Sell_OrderOutHouseService();
        [WebMethod]
        public string[] GetGoods(string prefixText, int count)
        {


            List<VAN_OA.Model.BaseInfo.TB_Good> goodsLists = goodSer.GetListToQuery_WebSer(string.Format(" GoodStatus='通过' and (ZhuJi like '%{0}%' or goodNo like '%{0}%')", prefixText), count);

            System.Text.StringBuilder goodIds = new System.Text.StringBuilder();
            foreach (var model in goodsLists)
            {            
                goodIds.AppendFormat("{0},", model.GoodId);
            }
            if (goodIds.ToString().Length > 0)
            {
                var DoingOrderNums = sellOutSer.GetDoingOrderNum(goodIds.ToString().Trim(','));

                foreach (var model in goodsLists)
                {
                    if (DoingOrderNums.ContainsKey(model.GoodId))
                    {
                        model.DoingNum = DoingOrderNums[model.GoodId];
                    }
                }
            }

            string[] strGoods = new string[goodsLists.Count];
            for (int i = 0; i < goodsLists.Count; i++)
            {
                strGoods[i] = goodsLists[i].GoodNo + @"\" + goodsLists[i].GoodName + @"\" + goodsLists[i].GoodTypeSmName + @"\" + goodsLists[i].GoodSpec + @"\" + goodsLists[i].GoodModel + @"\" + goodsLists[i].GoodUnit + @"\" + goodsLists[i].HouseNum + "-" + goodsLists[i].DoingNum;
            }
            return strGoods;
        }


        VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
        [WebMethod]
        public string[] GetGuestList(string prefixText, int count)
        {
          

            List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and (GuestName like '%{0}%' or GuestId like '%{0}%') ", prefixText));
            string[] strGoods = new string[guestTrackLists.Count];
            for (int i = 0; i < guestTrackLists.Count; i++)
            {
                strGoods[i] = guestTrackLists[i].GuestId + @"\" + guestTrackLists[i].GuestName + @"\" + guestTrackLists[i].AEName + @"\" + guestTrackLists[i].INSIDEName;
            }
            return strGoods;
        }

        [WebMethod]

        public TB_GuestTrack GetGuestByGuestName(string prefixText, int count)
        {
            StringBuilder strSql = new StringBuilder();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (1 <= month && month <= 3)
            {
                strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
            }
            else if (4 <= month && month <= 6)
            {
                strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
            }
            else if (7 <= month && month <= 9)
            {
                strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
            }
            else if (10 <= month && month <= 12)
            {
                strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
            }
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetListArray(string.Format("  GuestName= '{0}' " + strSql.ToString(), prefixText));

            if (guestTrackLists.Count > 0)
            {  
                return guestTrackLists[0];
            }
            return new TB_GuestTrack ();
        }


        [WebMethod]
        public string[] GetGuestAllList(string prefixText, int count)
        {
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> guestTrackLists = guestTrackSer.GetGuestListToQuery(string.Format(" and (GuestName like '%{0}%' or GuestId like '%{0}%')", prefixText));
            string[] strGoods = new string[guestTrackLists.Count];
            for (int i = 0; i < guestTrackLists.Count; i++)
            {
                string guestDays=guestTrackLists[i].GuestDays == null ? "0" : guestTrackLists[i].GuestDays.Value.ToString();
                strGoods[i] = guestTrackLists[i].GuestId + @"\" + guestTrackLists[i].GuestName + @"\" + guestTrackLists[i].AEName + @"\" + guestTrackLists[i].INSIDEName + @"\" + guestDays;
            }
            return strGoods;
        }

     

        [WebMethod]
        public string[] GetSuplierList(string prefixText, int count)
        {
            string sql = string.Format("select top {1} SupplieSimpeName from TB_SupplierInfo where (SupplieSimpeName like '%{0}%' or ZhuJi like '%{0}%') and Status='通过'  and IsUse=1 ", prefixText.Trim(), count);
            System.Data.DataTable dt = DBHelp.getDataTable(sql);
            string[] strGoods = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strGoods[i] = dt.Rows[i][0].ToString();
            }
            return strGoods;
        }

        [WebMethod]
        public string[] GetFullSuplierList(string prefixText, int count)
        {
            string sql = string.Format("select top {1} SupplierName from TB_SupplierInfo where (SupplieSimpeName like '%{0}%' or ZhuJi like '%{0}%') and Status='通过'  and IsUse=1 ", prefixText.Trim(), count);
            System.Data.DataTable dt = DBHelp.getDataTable(sql);
            string[] strGoods = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strGoods[i] = dt.Rows[i][0].ToString();
            }
            return strGoods;
        }

    }
}
