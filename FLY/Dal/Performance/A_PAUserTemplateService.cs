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
    public class A_PAUserTemplateService
    {
        /// <summary>
        /// 插入临时一条数据
        /// </summary>
        public void SingleInsert(A_PAUserTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            string strSql3;
                if (model.A_PATemplateItem.Count > 0)
                {
                    strSql3 = "delete from tb_UserPAForm where UserID=" + model.UserID + " and PAItemID=" + model.A_PATemplateItem[0].ToString() + ";delete from tb_UserPAFormUser where UserID=" + model.UserID+" and ItemID=" + model.A_PATemplateItem[0].ToString();
                    object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                    strSql3 = "Insert into tb_UserPAForm (UserID,PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview) values (" + model.UserID + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_Sequence[0].ToString() + "," + model.A_PATemplateScore[0].ToString() + "," + model.A_PATemplateAmount[0].ToString() + "," + (model.A_PATemplateIsFirstReview[0] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[0].ToString() + "," + (model.A_PATemplateIsSecondReview[0] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[0].ToString() + "," + (model.A_PATemplateIsMultiReview[0] ? 1 : 0) + ")";
                    object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                    for (int j = 0; j < model.A_PATemplateMultiReviewUserID[0].Count; j++)
                    {
                        strSql3 = "insert into tb_UserPAFormUser (UserID,SectionID,ItemID,ReviewID) values (" + model.UserID + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_PATemplateMultiReviewUserID[0][j].ToString() + ")";
                        object obj3 = DBHelp.ExeScalar(strSql3.ToString());
                    }
                }
        }
        public int SingleModify(A_PATemplate model, string PATemplateID)
        {
            string strSql3;
            if (model.A_PATemplateItem.Count > 0)
            {
                strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID + " and A_PAItemID=" + model.A_PATemplateItem[0].ToString() + ";delete from A_PATemplateItemUser where A_PATemplateID=" + PATemplateID + " and A_PAItemID=" + model.A_PATemplateItem[0].ToString();
                object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,Sequence,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview) values (" + PATemplateID + "," + model.A_PAItemSequence[0].ToString() + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_PATemplateScore[0].ToString() + "," + model.A_PATemplateAmount[0].ToString() + "," + (model.A_PATemplateIsFirstReview[0] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[0].ToString() + "," + (model.A_PATemplateIsSecondReview[0] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[0].ToString() + "," + (model.A_PATemplateIsMultiReview[0] ? 1 : 0) + ")";
                object obj3 = DBHelp.ExeScalar(strSql3.ToString());
                for (int j = 0; j < model.A_PATemplateMultiReviewUserID[0].Count; j++)
                {
                    strSql3 = "Insert into A_PATemplateItemUser (A_PATemplateID,A_PASection_ID,A_PAItemID,User_ID) values (" + PATemplateID + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_PATemplateMultiReviewUserID[0][j].ToString() + ")";
                    object obj4 = DBHelp.ExeScalar(strSql3.ToString());
                }
            }
            return 1;
        }
        public int SingleDelete(string UserID, string PAItemID)
        {
            string strSql3;
            strSql3 = "delete from tb_UserPAForm where UserID=" + UserID + " and PAItemID=" + PAItemID + ";delete from tb_UserPAFormUser where UserID=" + UserID + " and ItemID=" + PAItemID;
            object obj1 = DBHelp.ExeScalar(strSql3.ToString());
            return 1;
        }
        /// <summary>
        /// 增加一条模版及子项和众评人
        /// </summary>
        public int Add(A_PATemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            string strSql3;
            if (model.A_PATemplateName != null)
            {
                strSql1.Append("A_PATemplateName,");
                strSql2.Append("'" + model.A_PATemplateName + "',");
            }
            strSql.Append("insert into A_PATemplate(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DBHelp.ExeScalar(strSql.ToString());

            if (obj == null)
            {
                return 0;
            }
            else
            {
                int PATemplateID = Convert.ToInt32(obj);
                if (model.A_PATemplateItem.Count > 0)
                {
                    strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID.ToString() + ";delete from A_PATemplateItemUser where A_PATemplateID=" + PATemplateID.ToString();
                    object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                    for (int i = 0; i < model.A_PATemplateItem.Count; i++)
                    {
                        strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,Sequence,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview) values (" + PATemplateID.ToString() + "," + model.A_PAItemSequence[i].ToString() + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + "," + (model.A_PATemplateIsMultiReview[i] ? 1 : 0) + ")";
                        object obj3 = DBHelp.ExeScalar(strSql3.ToString());
                        for (int j = 0; j < model.A_PATemplateMultiReviewUserID[i].Count; j++)
                        {
                            strSql3 = "Insert into A_PATemplateItemUser (A_PATemplateID,A_PASection_ID,A_PAItemID,User_ID) values (" + PATemplateID.ToString() + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateMultiReviewUserID[i][j].ToString() + ")";
                            object obj4 = DBHelp.ExeScalar(strSql3.ToString());
                        }
                    }
                }
                return PATemplateID;
            }
        }
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
                    strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview) values (" + model.A_PATemplateID + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + "," + (model.A_PATemplateIsMultiReview[i] ? 1 : 0) + ")";
                    object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                }
            }
        }

        /// <summary>
        /// 更新一条用户模版数据
        /// </summary>
        public void UserUpdate(A_PAUserTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            string strSql3;
            if (model.A_PATemplateItem.Count > 0)
            {
                strSql3 = "delete from tb_UserPAForm where UserID=" + model.UserID + ";delete from tb_UserPAFormUser where UserID=" + model.UserID;
                object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                for (int i = 0; i < model.A_PATemplateItem.Count; i++)
                {
                    strSql3 = "Insert into tb_UserPAForm (UserID,PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview) values (" + model.UserID + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_Sequence[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + "," + (model.A_PATemplateIsMultiReview[i] ? 1 : 0) + ")";
                    object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                    for (int j=0;j<model.A_PATemplateMultiReviewUserID[i].Count;j++)
                    {
                        strSql3 = "insert into tb_UserPAFormUser (UserID,SectionID,ItemID,ReviewID) values (" + model.UserID + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateMultiReviewUserID[i][j].ToString() + ")";
                        object obj3 = DBHelp.ExeScalar(strSql3.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int A_PATemplateID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_PATemplate ");
            strSql.Append(" where A_PATemplateID=" + A_PATemplateID + ";");
            strSql.Append("delete from A_PATemplateItem ");
            strSql.Append(" where A_PATemplateID=" + A_PATemplateID + ";");
            strSql.Append("delete from A_PATemplateItemUser ");
            strSql.Append(" where A_PATemplateID=" + A_PATemplateID + ";");
           DBHelp.ExeCommand (strSql.ToString());
           
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
                    string strSql = "insert into tb_UserPAForm (UserID,PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview) select " + UserID + ",A_PASectionID,A_PAItemID,Sequence,A_PAItemScore,A_PAItemAmount,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview from A_PATemplateItem where A_PATemplateID=" + A_PATemplateID + ";insert into tb_UserPAFormUser (UserID,SectionID,ItemID,ReviewID) select " + UserID + ",A_PASection_ID,A_PAItemID,User_ID from A_PATemplateItemUser where A_PATemplateID=" + A_PATemplateID;
                    DBHelp.ExeCommand(strSql.ToString());
                }
                if (Convert.ToInt32(DBHelp.ExeScalar("select count(*) from tb_UserMonthPAFormHead where UserID=" + UserID + " and Month='" + YearMonth + "' and Status=0")) != 0)
                {
                    string strSql0 = "delete from tb_UserMonthPAForm where PAFormID=(select PAFormID from tb_UserMonthPAFormHead where UserID=" + UserID + " and Month='" + YearMonth + "');delete from tb_UserMonthPAFormUser where PAFormID=(select PAFormID from tb_UserMonthPAFormHead where UserID=" + UserID + " and Month='" + YearMonth + "')";
                    DBHelp.ExeCommand(strSql0.ToString());
                }
                else
                {
                    string strSql1 = "insert into tb_UserMonthPAFormHead (UserID,Month) values (" + UserID + ",'" + YearMonth + "')";
                    DBHelp.ExeCommand(strSql1.ToString());
                }
                string strSql2 = "insert into tb_UserMonthPAForm (PAFormID,PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview) select (select PAFormID from tb_UserMonthPAFormHead where UserID=" + UserID + " and Month='" + YearMonth + "'),PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview from tb_UserPAForm where UserID=" + UserID + ";insert into tb_UserMonthPAFormUser (PAFormID,PASectionID,PAItemID,UserID) select (select PAFormID from tb_UserMonthPAFormHead where UserID=" + UserID + " and Month='" + YearMonth + "'),PFU.SectionID,PFU.ItemID,PFU.ReviewID from tb_UserPAFormUser PFU inner join tb_UserPAForm PF on PFU.UserID=PF.UserID and PFU.ItemID=PF.PAItemID where PFU.UserID=" + UserID + " and PF.IsMultiReview=1";
            DBHelp.ExeCommand(strSql2.ToString());
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
        public DataTable GetUserTemplateList(string strWhere)
        {
            return DBHelp.getDataTable("select U.ID,U.loginName,PU.PATemplateID,PA.A_PATemplateName FROM tb_User U left join tb_UserPATemplate PU on U.ID=PU.UserID left join A_PATemplate PA on PU.PATemplateID=PA.A_PATemplateID where U.loginID<>'admin' and U.loginStatus='在职'" + strWhere);
        }  
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTemplateDetailList(string PATempateID)
        {
            return DBHelp.getDataTable("select TI.A_PAItemID,TI.A_PASectionID,PA.A_PAItemName,TI.A_PAItemScore,TI.A_PAItemAmount,TI.A_PAIsFirstReview,TI.A_PAFirstReviewUserID,TI.A_PAIsSecondReview,TI.A_PASecondReviewUserID FROM A_PAItem PA left join A_PATemplateITem TI on PA.A_PAItemID=TI.A_PAItemID and TI.A_PATemplateID=" + PATempateID);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetUserDetailList(string UserID)
        {
            return DBHelp.getDataTable("select UP.PAItemID,UP.PASectionID,S.A_PASectionName,PA.A_PAItemName,UP.Sequence,UP.PAItemScore,isnull(UP.PAItemAmount,0) as PAItemAmount,dbo.getChineseBit(UP.IsFirstReview) as IsFirstReview,U1.LoginName as FirstReviewUserID,dbo.getChineseBit(UP.IsSecondReview) as IsSecondReview,U2.LoginName as SecondReviewUserID,dbo.getChineseBit(UP.IsMultiReview) as IsMultiReview,dbo.getUserPAFormMultiReviewName(UP.IsMultiReview,UP.UserID,UP.PAItemID) as MultiReviewUserID FROM tb_UserPAForm UP inner join A_PAItem PA on UP.PAItemID=PA.A_PAItemID inner join A_PASection S on UP.PASectionID=S.A_PASectionID left join tb_user U1 on UP.FirstReviewUserID=U1.ID left join tb_user U2 on UP.SecondReviewUserID=U2.ID where UP.UserID=" + UserID + " Order by UP.Sequence");
        }
        public DataTable GetTableUserPAForm(string UserID, string PAItemID)
        {
            return DBHelp.getDataTable("select TI.PASectionID,TI.PAItemID,TI.Sequence,TI.PAItemScore,TI.PAItemAmount,TI.IsFirstReview,TI.FirstReviewUserID,TI.IsSecondReview,TI.SecondReviewUserID,TI.IsMultiReview FROM tb_UserPAForm TI Where TI.UserID=" + UserID + " and TI.PAItemID=" + PAItemID);

        }
        public DataTable GetTableUserPAFormUser(string UserID, string PAItemID)
        {

            return DBHelp.getDataTable("select TU.ReviewID FROM tb_UserPAFormUser TU Where TU.UserID=" + UserID + " and TU.ItemID=" + PAItemID);


        }
        /// <summary>
        /// 得到科目的数量
        /// </summary>
        public List<string> GetPAFormSection(string UserID)
        {
            List<string> Section = new List<string>();
            string strSql = "select distinct S.A_PASectionName as PASectionName FROM tb_UserPAForm P left join A_PASection S on P.PASectionID=S.A_PASectionID where P.UserID=" + UserID;
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
