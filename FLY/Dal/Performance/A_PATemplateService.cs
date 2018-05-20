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
    public class A_PATemplateService
    {
        /// <summary>
        /// 没Template，插入一条Item
        /// </summary>
        public int NewSingleInsert(A_PATemplate model)
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
            object obj =DBHelp.ExeScalar(strSql.ToString());

            if (obj == null)
            {
                return 0;
            }
            else
            {
                int PATemplateID = Convert.ToInt32(obj);
                if (model.A_PATemplateItem.Count > 0)
                {
                    strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID + ";delete from A_PATemplateItemUser where A_PATemplateID=" + PATemplateID;
                    object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                    strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,Sequence,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview) values (" + PATemplateID + "," + model.A_PAItemSequence[0].ToString() + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_PATemplateScore[0].ToString() + "," + model.A_PATemplateAmount[0].ToString() + "," + (model.A_PATemplateIsFirstReview[0] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[0].ToString() + "," + (model.A_PATemplateIsSecondReview[0] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[0].ToString() + "," + (model.A_PATemplateIsMultiReview[0] ? 1 : 0) + ")";
                    object obj3 = DBHelp.ExeScalar(strSql3.ToString());
                    for (int j = 0; j < model.A_PATemplateMultiReviewUserID[0].Count; j++)
                    {
                        strSql3 = "Insert into A_PATemplateItemUser (A_PATemplateID,A_PASection_ID,A_PAItemID,User_ID) values (" + PATemplateID + "," + model.A_PATemplateSection[0].ToString() + "," + model.A_PATemplateItem[0].ToString() + "," + model.A_PATemplateMultiReviewUserID[0][j].ToString() + ")";
                        object obj4 = DBHelp.ExeScalar(strSql3.ToString());
                    }
                }  
                return PATemplateID;
            }
        }
        /// <summary>
        /// 有Template，插入一条Item
        /// </summary>
        public int SingleInsert(A_PATemplate model,int PATemplateID)
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
            return PATemplateID;
        }
        public int NameModify(string PATemplateName, string PATemplateID)
        {
            string strSql3;
            strSql3 = "update A_PATemplate set A_PATemplateName='" + PATemplateName + "' where A_PATemplateID=" + PATemplateID;
            object obj1 = DBHelp.ExeScalar(strSql3.ToString());
            return 1;
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
        public int SingleDelete(string PATemplateID, string PAItemID)
        {
            string strSql3;
                strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID + " and A_PAItemID=" + PAItemID + ";delete from A_PATemplateItemUser where A_PATemplateID=" + PATemplateID + " and A_PAItemID=" + PAItemID;
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
                   strSql3="delete from A_PATemplateItem where A_PATemplateID=" + PATemplateID.ToString()+";delete from A_PATemplateItemUser where A_PATemplateID=" + PATemplateID.ToString();
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
        /// 更新一条模版及子项和众评人
        /// </summary>
        public void Update(A_PATemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            string strSql3;
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
                strSql3 = "delete from A_PATemplateItem where A_PATemplateID=" + model.A_PATemplateID + ";delete from A_PATemplateItemUser where A_PATemplateID=" + model.A_PATemplateID;
                object obj1 = DBHelp.ExeScalar(strSql3.ToString());
                for (int i = 0; i < model.A_PATemplateItem.Count; i++)
                {
                    strSql3 = "Insert into A_PATemplateItem (A_PATemplateID,Sequence,A_PASectionID,A_PAItemID,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview) values (" + model.A_PATemplateID + "," + model.A_PAItemSequence[i].ToString() + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateScore[i].ToString() + "," + model.A_PATemplateAmount[i].ToString() + "," + (model.A_PATemplateIsFirstReview[i] ? 1 : 0) + "," + model.A_PATemplateFirstReviewUserID[i].ToString() + "," + (model.A_PATemplateIsSecondReview[i] ? 1 : 0) + "," + model.A_PATemplateSecondReviewUserID[i].ToString() + "," + (model.A_PATemplateIsMultiReview[i] ? 1 : 0) + ")";
                    object obj2 = DBHelp.ExeScalar(strSql3.ToString());
                    for (int j = 0; j < model.A_PATemplateMultiReviewUserID[i].Count; j++)
                    {
                        strSql3 = "Insert into A_PATemplateItemUser (A_PATemplateID,A_PASection_ID,A_PAItemID,User_ID) values (" + model.A_PATemplateID.ToString() + "," + model.A_PATemplateSection[i].ToString() + "," + model.A_PATemplateItem[i].ToString() + "," + model.A_PATemplateMultiReviewUserID[i][j].ToString() + ")";
                        object obj4 = DBHelp.ExeScalar(strSql3.ToString());
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
            strSql.Append(" where A_PATemplateID=" + A_PATemplateID + "");
           DBHelp.ExeCommand (strSql.ToString());
           
        }
        /// <summary>
        /// 复制模版到用户
        /// </summary>
        public void Copy(int A_PATemplateID,int userID)
        {

            if (Convert.ToInt32(DBHelp.ExeScalar("select count(*) from tb_UserPAForm where UserID=" + userID)) > 0)
            {
                DBHelp.ExeCommand("delete from Temp_UserPAForm;insert into Temp_UserPAForm (UserID,PASectionID,PAItemID,Amount) select UserID,PASectionID,PAItemID,PAItemAmount from tb_UserPAForm where UserID=" + userID); 
                DBHelp.ExeCommand("delete from tb_UserPAForm where UserID=" + userID + ";delete from tb_UserPATemplate where UserID=" + userID + ";delete from tb_UserPAFormUser where UserID=" + userID);
            }
            string strSql = "insert into tb_UserPAForm (UserID,PASectionID,PAItemID,Sequence,PAItemScore,PAItemAmount,IsFirstReview,FirstReviewUserID,IsSecondReview,SecondReviewUserID,IsMultiReview) select " + userID + ",A_PASectionID,A_PAItemID,Sequence,A_PAItemScore,A_PAItemAmount,A_PAIsFirstReview,A_PAFirstReviewUserID,A_PAIsSecondReview,A_PASecondReviewUserID,A_PAIsMultiReview from A_PATemplateItem where A_PATemplateID=" + A_PATemplateID + ";insert into tb_UserPAFormUser (UserID,SectionID,ItemID,ReviewID) select " + userID + ",A_PASection_ID,A_PAItemID,User_ID from A_PATemplateItemUser where A_PATemplateID=" + A_PATemplateID + ";insert into tb_UserPATemplate (UserID,PATemplateID) values (" + userID + "," + A_PATemplateID + ")";
            DBHelp.ExeCommand(strSql.ToString());
            if (Convert.ToInt32(DBHelp.ExeScalar("select count(*) from temp_UserPAForm where UserID=" + userID)) > 0)
            {
                string strSql2 = "update tb_UserPAForm set PAItemAmount=(select Amount from Temp_UserPAForm where UserID=" + userID + " and PAItemID=tb_UserPAForm.PAItemID) where UserID=" + userID;
                DBHelp.ExeCommand(strSql2.ToString());
            }
        }		
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<A_PATemplate> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A_PATemplateID,A_PATemplateName ");
            strSql.Append(" FROM A_PATemplate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<A_PATemplate> PATemplate = new List<A_PATemplate>();
             using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        A_PATemplate model = new A_PATemplate();
                        object ojb;
                        ojb = dataReader["A_PATemplateID"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.A_PATemplateID = (int)ojb;
                        }
                        model.A_PATemplateName = dataReader["A_PATemplateName"].ToString();
                        PATemplate.Add(model);
                    }
                }
             }
             return PATemplate; 
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetDetailList(string PATempateID)
        {
            if (PATempateID != "")
            {
                return DBHelp.getDataTable("select TI.A_PATemplateID,TI.A_PAItemID,S.A_PASectionName,PA.A_PAItemName,TI.Sequence,isnull(TI.A_PAItemScore,PA.A_PAItemScore) AS A_PAItemScore,isnull(TI.A_PAItemAmount,PA.A_PAItemAmount) as A_PAItemAmount,dbo.getChineseBit(TI.A_PAIsFirstReview) as A_PAIsFirstReview,U1.LoginName as A_PAFirstReviewUserID,dbo.getChineseBit(TI.A_PAIsSecondReview) as A_PAIsSecondReview,U2.LoginName as A_PASecondReviewUserID,dbo.getChineseBit(TI.A_PAIsMultiReview) as A_PAIsMultiReview,dbo.getPATemplateMultiReviewName(TI.A_PAIsMultiReview,TI.A_PATemplateID,TI.A_PAItemID) as A_PAMultiReviewUserID FROM A_PATemplateItem TI left join A_PAItem PA on TI.A_PAItemID=PA.A_PAItemID left join A_PASection S on TI.A_PASectionID=S.A_PASectionID left join tb_user U1 on TI.A_PAFirstReviewUserID=U1.ID left join tb_user U2 on TI.A_PASecondReviewUserID=U2.ID Where TI.A_PATemplateID=" + PATempateID + " ORDER BY Sequence");
            }
            else
            {
                return DBHelp.getDataTable("select PA.A_PAItemID as Base_A_PAItemID,null as A_PAItemID,'' as A_PASectionID,PA.A_PAItemName,0 as Sequence,PA.A_PAItemScore,PA.A_PAItemAmount,0 as A_PAIsFirstReview,'' as A_PAFirstReviewUserID,0 as A_PAIsSecondReview,'' as A_PASecondReviewUserID,0 as A_PAIsMultiReview FROM A_PAItem PA");
            }
        }
        public DataTable GetTableTemplateItem(string PATempateID, string PAItemID)
        {
                return DBHelp.getDataTable("select TI.A_PASectionID,TI.A_PAItemID,TI.Sequence,TI.A_PAItemScore,TI.A_PAItemAmount,TI.A_PAIsFirstReview,TI.A_PAFirstReviewUserID,TI.A_PAIsSecondReview,TI.A_PASecondReviewUserID,TI.A_PAIsMultiReview FROM A_PATemplateItem TI Where TI.A_PATemplateID=" + PATempateID + " and TI.A_PAItemID=" + PAItemID);
            
        }
        public DataTable GetTableTemplateItemMultiUser(string PATempateID, string PAItemID)
        {

                return DBHelp.getDataTable("select TU.User_ID FROM A_PATemplateItemUser TU Where TU.A_PATemplateID=" + PATempateID + " and TU.A_PAItemID=" + PAItemID);

           
        }    
    }
}
