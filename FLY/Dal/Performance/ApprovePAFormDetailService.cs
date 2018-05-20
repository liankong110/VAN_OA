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
    public class ApprovePAFormDetailService
    {
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(string PAFormID,PAFormDetail model,string UserID)
        {
            try
            { 
                string strSql3 = "";
                if (model.PAItem.Count > 0)
                {
                    for (int i = 0; i < model.PAItem.Count; i++)
                    {
                        if (model.PAFirstReviewUserID[i].ToString() == UserID && model.PASecondReviewUserID[i].ToString() == UserID)
                        {
                            strSql3 = "update tb_UserMonthPAForm set FirstReviewScore=" + model.PAFirstReviewScore[i].ToString() + ",FirstReviewTime=getdate(),SecondReviewScore=" + model.PASecondReviewScore[i].ToString() + ",SecondReviewTime=getdate(),ReviewAmount=" + model.PAAmount[i].ToString() + ",Note='" + model.PANote[i].ToString() + "' where PAFormID=" + PAFormID.ToString() + " and PAItemID=" + model.PAItem[i].ToString();
                        }
                        else
                        {
                            if (model.Status == 1)
                            {
                                strSql3 = "update tb_UserMonthPAForm set FirstReviewScore=" + model.PAFirstReviewScore[i].ToString() + ",";
                                if (model.PAFirstReviewUserID[i].ToString() == UserID)
                                {
                                    strSql3 += "FirstReviewTime=getdate(),";
                                }
                                strSql3 += "ReviewAmount=" + model.PAAmount[i].ToString() + ",Note='" + model.PANote[i].ToString() + "' where PAFormID=" + PAFormID.ToString() + " and PAItemID=" + model.PAItem[i].ToString();
                            }
                            else if (model.Status == 2)
                            {
                                strSql3 = "update tb_UserMonthPAForm set SecondReviewScore=" + model.PASecondReviewScore[i].ToString() + ",";
                                if (model.PASecondReviewUserID[i].ToString() == UserID)
                                {
                                    strSql3 += "SecondReviewTime=getdate(),";
                                }
                                strSql3 += "ReviewAmount=" + model.PAAmount[i].ToString() + ",Note='" + model.PANote[i].ToString() + "' where PAFormID=" + PAFormID.ToString() + " and PAItemID=" + model.PAItem[i].ToString();
                            }
                        }
                        object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                    }
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update tb_UserMonthPAFormHead set ");
                strSql.Append("AttendDays=" + model.AttendDays.ToString() + ",");
                strSql.Append("LeaveDays=" + model.LeaveDays.ToString() + ",");
                strSql.Append("FullAttendBonus=" + model.FullAttendBonus.ToString() + ",");
                strSql.Append("Status=dbo.set_PAForm_Status(" + PAFormID + "),"); 
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where PAFormID=" + PAFormID + "");
                DBHelp.ExeCommand(strSql.ToString());
                return 0;
            }
            catch (Exception ex)
            { 
                return 1;
            }        
        }
        /// <summary>
        /// 得到一个对象实体
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
        /// 得到初评和复评的明细
        /// </summary>
        public DataTable GetPAFormDetail(string PAFormID)
        {
            return DBHelp.getDataTable("select P.PAFormID,P.PASectionID,P.PAItemID,S.A_PASectionName as PASectionName,I.A_PAItemName as PAItemName,PAItemScore,IsFirstReview,P.FirstReviewUserID,U1.loginName as FirstReviewUserName,FirstReviewScore,IsSecondReview,P.SecondReviewUserID,U2.loginName as SecondReviewUserName,SecondReviewScore,ReviewAmount,IsMultiReview,dbo.getPAMultiProgress(P.PAFormID,P.PAItemID) as MultiProgress,dbo.getPAMultiScore(P.PAFormID,P.PAItemID) as MultiScore,Note FROM tb_UserMonthPAForm P left join A_PASection S on P.PASectionID=S.A_PASectionID left join A_PAItem I on P.PAItemID=I.A_PAItemID left join tb_user U1 on P.FirstReviewUserID=U1.id left join tb_user U2 on P.SecondReviewUserID=U2.id where P.PAFormID=" + PAFormID);
        }
        /// <summary>
        /// 得到科目的数量
        /// </summary>
        public List<string> GetPAFormSection(string PAFormID)
        {
            List<string> Section = new List<string>();
            string strSql = "select distinct S.A_PASectionName as PASectionName FROM tb_UserMonthPAForm P left join A_PASection S on P.PASectionID=S.A_PASectionID where P.PAFormID=" + PAFormID;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Section.Add(dataReader["PASectionName"].ToString());
                    }
                }
            }
            return Section;
        }
    }
}
