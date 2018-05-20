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
    public class ApprovePAFormListService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(A_PAUserTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserID  != null)
            {
                strSql1.Append("A_PATemplateName,");
                strSql2.Append("'" + model.UserID  + "',");
            }
            strSql.Append("insert into A_PATemplate(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj =DBHelp.ExeScalar(strSql.ToString());
            
            if (obj == null)
            {
                return 0;
            }
            else
            { 
                int PATemplateID=Convert.ToInt32(obj);
                if (model.A_PATemplateItem.Count > 0)
                {
                   string strSql3="delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID.ToString();
                   object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                   for (int i = 0; i < model.A_PATemplateItem.Count; i++)
                   {
                       strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID) values (" + PATemplateID.ToString() + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + ")";
                       object obj2 = DBHelp.ExeScalar(strSql3.ToString());       
                   }
                }
                return PATemplateID;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(A_PATemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_PATemplate set ");
            if (model.A_PATemplateName != null)
            {
                strSql.Append("A_PATemplateName='" + model.A_PATemplateName + "',");
            }
            else
            {
                strSql.Append("A_PATemplateName= null ,");
            }
            
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where A_PATemplateID=" + model.A_PATemplateID + "");
            DBHelp.ExeCommand(strSql.ToString());
            if (model.A_PATemplateItem.Count > 0)
            {
                string strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + model.A_PATemplateID;
                object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                for (int i = 0; i < model.A_PATemplateItem.Count; i++)
                {
                    strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID) values (" + model.A_PATemplateID + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + ")";
                    object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int PAFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_UserMonthPAFormHead ");
            strSql.Append(" where PAFormID=" + PAFormID + ";");
            strSql.Append("delete from tb_UserMonthPAForm ");
            strSql.Append(" where PAFormID=" + PAFormID + ";");
            strSql.Append("delete from tb_UserMonthPAFormUser ");
            strSql.Append(" where PAFormID=" + PAFormID + "");
           DBHelp.ExeCommand (strSql.ToString());
           
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void BatchDelete(string PAFormIDs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_UserMonthPAFormHead ");
            strSql.Append(" where PAFormID in (" + PAFormIDs + ");");
            strSql.Append("delete from tb_UserMonthPAForm ");
            strSql.Append(" where PAFormID in (" + PAFormIDs + ");");
            strSql.Append("delete from tb_UserMonthPAFormUser ");
            strSql.Append(" where PAFormID in (" + PAFormIDs + ")");
            DBHelp.ExeCommand(strSql.ToString());

        }
        /// <summary>
        /// 复制用户模版到每月表单
        /// </summary>
        public int Copy(string A_PATemplateID,string UserID,string YearMonth)
        {
            try
            {
                if (Convert.ToInt32(DBHelp.ExeScalar("select count(*) from tb_UserPAForm where UserID=" + UserID)) == 0)
                {
                    string strSql = "insert into tb_UserPAForm (UserID,PASectionID,PAItemID,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID) select " + UserID + ",A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID from A_PATemplateItem where A_PATemplateID=" + A_PATemplateID;
                    DBHelp.ExeCommand(strSql.ToString());
                }
                string strSql1 = "insert into tb_UserMonthPAForm (UserID,Month,PASectionID,PAItemID,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID) select " + UserID + ",'" + YearMonth + "',PASectionID,PAItemID,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID from tb_UserPAForm where UserID=" + UserID;
            DBHelp.ExeCommand(strSql1.ToString());
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
        public List<A_PAUserTemplate> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select U.ID,U.loginName,PA.A_PATemplateName ");
            strSql.Append(" FROM tb_User U left join tb_UserPATemplate PU on U.ID=PU.UserID left join A_PATemplate PA on PU.PATemplateID=PA.A_PATemplateID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<A_PAUserTemplate> PAUserTemplate = new List<A_PAUserTemplate>();
             using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        A_PAUserTemplate model = new A_PAUserTemplate();
                        object ojb;
                        ojb = dataReader["ID"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.UserID = (int)ojb;
                        }
                        PAUserTemplate.Add(model);
                    }
                }
             }
             return PAUserTemplate; 
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetMyPAFormList(string UserID)
        {
            return DBHelp.getDataTable("select P.PAFormID,U.loginName,P.Month,dbo.getPAStatus(P.Status) as Status FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID where P.UserID=" + UserID );
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetMyPAFormList(string UserID,string Month)
        {
            return DBHelp.getDataTable("select P.PAFormID,U.loginName,P.Month,dbo.getPAStatus(P.Status) as Status FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID where P.UserID=" + UserID+" and Month='"+Month+"'");
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetMyApprovePAFormList(string ReviewID)
        {
            return DBHelp.getDataTable("select P.PAFormID,U.loginName,P.Month,dbo.getPAStatus(P.Status) as Status FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID where P.PAFormID in (Select PAFormID from tb_UserMonthPAForm where FirstReviewUserID=" + ReviewID + " or SecondReviewUserID=" + ReviewID + ")");
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetMyApprovePAFormList(string ReviewID, string UserID, string Month)
        {
            string sql = "select P.PAFormID,U.loginName,P.Month,dbo.getPAStatus(P.Status) as Status FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID where P.PAFormID in (Select PAFormID from tb_UserMonthPAForm where (FirstReviewUserID=" + ReviewID + " and IsFirstReview = 1) or (SecondReviewUserID=" + ReviewID + " and IsSecondReview = 1))";
            if (Month != "")
            {
                sql+="and P.Month>='"+Month+"'";
            }
            if (UserID != "")
            {
                sql += "and P.UserID=" + UserID ;
            }
            return DBHelp.getDataTable(sql);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetAllPAFormList()
        {
            return DBHelp.getDataTable("select P.PAFormID,U.loginName,U.zhiwu,P.Month,P.AttendDays,P.LeaveDays,P.FullAttendBonus,dbo.getPAStatus(P.Status) as Status,dbo.getPAFirstScoreSum(P.PAFormID) as PAFirstScoreSum,dbo.getPASecondScoreSum(P.PAFormID) as PASecondScoreSum,dbo.getPAMultiScoreSum(P.PAFormID) as PAMultiScoreSum,dbo.getPAAmountSum(P.PAFormID)as PAAmountSum FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID Order by U.loginName,P.Month");
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetAllPAFormList(string UserID, string Month, PagerDomain page)
        {
            string sql = "select P.PAFormID,U.loginName,U.zhiwu,P.Month,P.AttendDays,P.LeaveDays,P.FullAttendBonus,dbo.getPAStatus(P.Status) as Status,dbo.getPAFirstScoreSum(P.PAFormID) as PAFirstScoreSum,dbo.getPASecondScoreSum(P.PAFormID) as PASecondScoreSum,dbo.getPAMultiScoreSum(P.PAFormID) as PAMultiScoreSum,dbo.getPASumAVG(P.PAFormID) as PASumAVG,dbo.getPAAmountSum(P.PAFormID) as PAAmountSum,dbo.getPASubNotes(P.PAFormID) as Note2,Convert(bit,0) as IsDeleted FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID ";
            string strWhere = "  P.PAFormID is not null";
            if (UserID != "")
            {
                strWhere += " and P.PAFormID in (Select PAFormID from tb_UserMonthPAForm where UserID=" + UserID + ")";
            }
            if (Month != "")
            {
                strWhere += " and Month>='" + Month + "'";
            }

            var strSql = new StringBuilder(DBHelp.GetPagerSql(page, sql, strWhere, " U.loginName,P.Month "));

            return DBHelp.getDataTable(strSql.ToString());
        }

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public DataTable GetAllPAFormList(string UserID, string Month, PagerDomain page)
        //{
        //    string sql = "select P.PAFormID,U.loginName,U.zhiwu,P.Month,P.AttendDays,P.LeaveDays,P.FullAttendBonus,dbo.getPAStatus(P.Status) as Status,dbo.getPAFirstScoreSum(P.PAFormID) as PAFirstScoreSum,dbo.getPASecondScoreSum(P.PAFormID) as PASecondScoreSum,dbo.getPAMultiScoreSum(P.PAFormID) as PAMultiScoreSum,dbo.getPASumAVG(P.PAFormID) as PASumAVG,dbo.getPAAmountSum(P.PAFormID) as PAAmountSum,dbo.getPASubNotes(P.PAFormID) as Note2 FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID ";
        //    string strWhere = " where P.PAFormID is not null";
        //    if (UserID != "")
        //    {
        //        strWhere += " and P.PAFormID in (Select PAFormID from tb_UserMonthPAForm where UserID=" + UserID + ")";
        //    }
        //    if (Month != "")
        //    {
        //        strWhere += " and Month>='" + Month + "'";
        //    }

        //    var strSql = new StringBuilder(DBHelp.GetPagerSql(page, sql, strWhere, " U.loginName,P.Month "));

        //    return DBHelp.getDataTable(sql);
        //}  
        /// <summary>
        /// 得到一个用于工资计算的对象实体
        /// </summary>
        public DataTable GetAllPAFormListForPayment(string UserID, string Month)
        {
            string sql="";
            sql = "select dbo.getPAScoreForPosition(P.PAFormID) as PAScoreSumPosition,dbo.getPAScoreForWork(P.PAFormID) as PAScoreSumWork FROM tb_UserMonthPAFormHead P left join tb_User U on P.UserID=U.ID where P.PAFormID is not null and P.PAFormID in (Select PAFormID from tb_UserMonthPAForm where UserID=" + UserID + " and [Month]='" + Month + "')";
            return DBHelp.getDataTable(sql);
        }  
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetDetailList(string PATempateID)
        {
            return DBHelp.getDataTable("select TI.A_PAItemID,TI.A_PASectionID,PA.A_PAItemName,TI.A_PAItemScore,TI.A_PAItemAmount,TI.A_PAIsFirstReview,TI.A_PAFirstReviewUserID,TI.A_PAIsSecondReview,TI.A_PASecondReviewUserID FROM A_PAItem PA left join A_PATemplateITem TI on PA.A_PAItemID=TI.A_PAItemID and TI.A_PATemplateID=" + PATempateID);
        }    
    }
}
