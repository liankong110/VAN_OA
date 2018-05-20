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
using VAN_OA.Model.Performance;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.Performance
{
    public class ApprovePAFormMultiService
    {
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(string UserID,PAFormMulti model)
        {
            try
            { 
                if (model.PAItem.Count > 0)
                {
                    for (int i = 0; i < model.PAItem.Count; i++)
                    {
                        string strSql3 = "update tb_UserMonthPAFormUser set ReviewScore=" + model.PAMultiReviewScore[i].ToString() + ",IsReview=" + (model.PAIsReview[i]? 1:0) + ",Note='" + model.PANote[i].ToString() + "' where PAFormID=" + model.PAFormID[i].ToString() + " and UserID=" + UserID.ToString() + " and PAItemID=" + model.PAItem[i].ToString();      
                        object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                        string strSql4 = "update tb_UserMonthPAFormHead set Status=dbo.set_PAForm_Status(" + model.PAFormID[i].ToString() + ") where PAFormID=" + model.PAFormID[i].ToString();
                        object obj3 = DBHelp.ExeScalar(strSql4.ToString());
                    }
                }
            return 0;
            }
            catch (Exception ex)
            { return 1; }
            
        }
        /// <summary>
        /// 得到头信息
        /// </summary>
        public PAFormDetail GetPAFormHead(string PAFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.UserID,U.LoginName,U.loginIPosition,P.Month,P.AttendDays,P.LeaveDays,P.FullAttendBonus,P.Status FROM tb_UserMonthPAFormHead P left join tb_user U on P.UserID=U.id where P.PAFormID=" + PAFormID);
            PAFormDetail thisPAFormDetail = new PAFormDetail();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        thisPAFormDetail.UserID = int.Parse(dataReader["UserID"].ToString());
                        thisPAFormDetail.UserName = dataReader["LoginName"].ToString();
                        thisPAFormDetail.UserIPosition = dataReader["loginIPosition"].ToString();
                        thisPAFormDetail.Month = dataReader["Month"].ToString();
                        thisPAFormDetail.AttendDays = decimal.Parse(dataReader["AttendDays"].ToString());
                        thisPAFormDetail.LeaveDays = decimal.Parse(dataReader["LeaveDays"].ToString());
                        thisPAFormDetail.FullAttendBonus = decimal.Parse(dataReader["FullAttendBonus"].ToString());
                        thisPAFormDetail.Status = int.Parse(dataReader["Status"].ToString());
                    }
                }
            }
            return thisPAFormDetail; 
        }   
        /// <summary>
        /// 得到众批的明细
        /// </summary>
        public DataTable GetPAFormMulti(string ReviewID,string PAUserID,string Month)
        {
            string sql = "select H.Month,H.Status,P.PAFormID,P.PAItemID,S.A_PASectionName as PASectionName,I.A_PAItemName as PAItemName,UI.PAItemScore as PAItemScore,U1.loginName,P.ReviewScore,dbo.getPAMultiProgress(P.PAFormID,P.PAItemID) as MultiProgress,P.Note FROM tb_UserMonthPAFormUser P inner join tb_UserMonthPAFormHead H on P.PAFormID=H.PAFormID left join A_PASection S on P.PASectionID=S.A_PASectionID left join A_PAItem I on P.PAItemID=I.A_PAItemID inner join tb_UserPAForm AS UI ON P.PAItemID = UI.PAItemID and UI.USERID=H.USERID left join tb_user U1 on H.UserID=U1.id where H.Status<>3 and P.UserID=" + ReviewID;
            if (PAUserID!="")
            {
                sql+=" and H.UserID="+PAUserID;
            }
            if (Month!="")
            {
                sql+=" and H.Month>='"+Month+"'";
            }
            sql += " and loginName is not null Order by loginName,Month,PASectionName,PAItemName";
            return DBHelp.getDataTable(sql);
        }
        /// <summary>
        /// 得到众批的明细
        /// </summary>
        public DataTable GetPAFormMulti(string PAFormID,string PAItemID)
        {
            string sql = "select P.PAItemID,S.A_PASectionName as PASectionName,I.A_PAItemName as PAItemName,U1.loginName,P.ReviewScore,P.Note FROM tb_UserMonthPAFormUser P left join A_PASection S on P.PASectionID=S.A_PASectionID left join A_PAItem I on P.PAItemID=I.A_PAItemID left join tb_user U1 on P.UserID=U1.id where P.PAFormID=" + PAFormID+" and P.PAItemID="+PAItemID ;
            return DBHelp.getDataTable(sql);
        }
    }
}
