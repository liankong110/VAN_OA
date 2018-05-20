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
using VAN_OA.Model.HR;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.HR
{
    public class HR_PaymentService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(VAN_OA.Model.HR.HR_Payment model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("ID,");
            strSql2.Append("'" + model.ID + "',");
            if (model.YearMonth != null)
            {
                strSql1.Append("YearMonth,");
                strSql2.Append("'" + model.YearMonth + "',");
            }
            if (model.BasicSalary != null)
            {
                strSql1.Append("BasicSalary,");
                strSql2.Append( + model.BasicSalary + ",");
            }
            if (model.FullAttendence != null)
            {
                strSql1.Append("FullAttendence,");
                strSql2.Append( + model.FullAttendence + ",");
            }
            if (model.MobileFee != null)
            {
                strSql1.Append("MobileFee,");
                strSql2.Append(  model.MobileFee + ",");
            }
            if (model.SpecialAward != null)
            {
                strSql1.Append("SpecialAward,");
                strSql2.Append(  model.SpecialAward + ",");
            }
            if (model.SpecialAwardNote != null)
            {
                strSql1.Append("SpecialAwardNote,");
                strSql2.Append("'"+model.SpecialAwardNote + "',");
            }
            if (model.GongLin != null)
            {
                strSql1.Append("Gonglin,");
                strSql2.Append(  model.GongLin + ",");
            }
            if (model.PositionPerformance != null)
            {
                strSql1.Append("PositionPerformance,");
                strSql2.Append( model.PositionPerformance + ",");
            }
            if (model.PositionFee != null)
            {
                strSql1.Append("PositionFee,");
                strSql2.Append( model.PositionFee + ",");
            }
            if (model.WorkPerformance != null)
            {
                strSql1.Append("WorkPerformance,");
                strSql2.Append(  model.WorkPerformance + ",");
            }
            if (model.FullPayment != null)
            {
                strSql1.Append("FullPayment,");
                strSql2.Append( model.FullPayment + ",");
            }
            if (model.DefaultWorkDays != null)
            {
                strSql1.Append("DefaultWorkDays,");
                strSql2.Append(model.DefaultWorkDays + ",");
            }
            if (model.WorkDays != null)
            {
                strSql1.Append("WorkDays,");
                strSql2.Append(model.WorkDays + ",");
            }
            if (model.ShouldPayment != null)
            {
                strSql1.Append("ShouldPayment,");
                strSql2.Append( model.ShouldPayment + ",");
            }
            if (model.UnionFee != null)
            {
                strSql1.Append("UnionFee,");
                strSql2.Append( model.UnionFee + ",");
            }
            if (model.Deduction != null)
            {
                strSql1.Append("Deduction,");
                strSql2.Append(model.Deduction + ",");
            }
            if (model.DeductionNote != null)
            {
                strSql1.Append("DeductionNote,");
                strSql2.Append("'"+model.DeductionNote + "',");
            }
            if (model.YangLaoJin != null)
            {
                strSql1.Append("YangLaoJin,");
                strSql2.Append( model.YangLaoJin + ",");
            }
            if (model.ActualPayment != null)
            {
                strSql1.Append("ActualPayment,");
                strSql2.Append(model.ActualPayment + ",");
            }
            if (model.UpdateTime != null)
            {
                strSql1.Append("UpdateTime,");
                strSql2.Append("'" + model.UpdateTime + "',");
            }
            if (model.UpdatePerson != null)
            {
                strSql1.Append("UpdatePerson,");
                strSql2.Append(model.UpdatePerson + ",");
            }
            strSql.Append("insert into HR_Payment(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 更新一条工资数据
        /// </summary>
        public bool Update(VAN_OA.Model.HR.HR_Payment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HR_Payment set ");
            strSql.Append("YearMonth='" + model.YearMonth + "',");
            strSql.Append("BasicSalary=" + model.BasicSalary + ",");
            strSql.Append("FullAttendence=" + model.FullAttendence + ",");
            strSql.Append("MobileFee=" + model.MobileFee + ",");
            strSql.Append("SpecialAward=" + model.SpecialAward + ",");
            strSql.Append("SpecialAwardNote='" + model.SpecialAwardNote + "',");
            strSql.Append("Gonglin=" + model.GongLin + ",");
            strSql.Append("PositionPerformance=" + model.PositionPerformance + ",");
            strSql.Append("WorkPerformance=" + model.WorkPerformance + ",");
            strSql.Append("FullPayment=" + model.FullPayment + ",");
            strSql.Append("DefaultWorkDays=" + model.DefaultWorkDays + ",");
            strSql.Append("WorkDays=" + model.WorkDays + ",");
            strSql.Append("ShouldPayment=" + model.ShouldPayment + ",");
            strSql.Append("UnionFee=" + model.UnionFee + ",");
            strSql.Append("Deduction=" + model.Deduction + ",");
            strSql.Append("DeductionNote='" + model.DeductionNote + "',");
            strSql.Append("YangLaoJin=" + model.YangLaoJin + ",");
            strSql.Append("ActualPayment=" + model.ActualPayment + ",");
            if (model.UpdateTime != null)
            {
                strSql.Append("UpdateTime='" + model.UpdateTime + "',");
            }
            else
            {
                strSql.Append("UpdateTime= null ,");
            }
            if (model.UpdatePerson != null)
            {
                strSql.Append("UpdatePerson=" + model.UpdatePerson + ",");
            }
            else
            {
                strSql.Append("UpdatePerson= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + " and YearMonth='"+model.YearMonth +"'");
            bool rowsAffected =DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HR_PERSON ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());            
        }

        /// <summary>
        /// 得到一个员工基本信息的对象实体
        /// </summary>
        public VAN_OA.Model.HR.HR_Payment GetModel(int ID,string Yearmonth)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" P.ID,P.YearMonth,H.Name,P.BasicSalary,P.FullAttendence,P.MobileFee,P.SpecialAward,P.SpecialAwardNote,P.Gonglin,P.PositionPerformance,P.PositionFee,P.WorkPerformance,P.FullPayment,P.DefaultWorkDays,P.WorkDays,P.ShouldPayment,P.UnionFee,P.Deduction,P.DeductionNote,P.YangLaoJin,P.ActualPayment,P.UpdateTime,P.UpdatePerson");
            strSql.Append(" from HR_Payment P left join HR_Person H on P.ID=H.ID ");
            strSql.Append(" where P.ID=" + ID + " and P.YearMonth='" + Yearmonth + "'");
        
            VAN_OA.Model.HR.HR_Payment model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model = SalaryReaderBind(objReader);
                    }
                }
            }
            return model;
        }

        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<string> GetCreateUpdateInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select user1.loginName as CreatePer,user2.loginName as UpdatePer from HR_PERSON
 left join tb_User as user1 on user1.id=HR_PERSON.CreatePerson
 left join tb_User as user2 on user2.id=HR_PERSON.UpdatePerson ");

            strSql.Append(" where HR_PERSON.id=" + id);

            List<string> list = new List<string>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                      object ojb = objReader["CreatePer"];
                       if (ojb != null && ojb != DBNull.Value)
                       {
                          
                           list.Add(ojb.ToString());
                       }
                       else
                       {
                           list.Add("");
                       }
                       ojb = objReader["UpdatePer"];
                       if (ojb != null && ojb != DBNull.Value)
                       {

                           list.Add(ojb.ToString());
                       }
                       else
                       {
                           list.Add("");
                       }                       
                    }
                }
            }
            return list;
        }
        
		/// <summary>
		/// 获得数据列表（比DataSet效率高，推荐使用）
		/// </summary>
        public List<VAN_OA.Model.HR.HR_Payment> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select P.ID,H.YearMonth,P.Name,P.Position,H.BasicSalary,H.FullAttendence,H.MobileFee,H.SpecialAward,H.SpecialAwardNote,H.Gonglin,H.PositionPerformance,H.PositionFee,H.WorkPerformance,H.FullPayment,H.WorkDays,H.ShouldPayment,H.UnionFee,H.Deduction,H.DeductionNote,H.YangLaoJin,H.ActualPayment,H.UpdateTime,H.UpdatePerson");
            strSql.Append(" FROM HR_Payment H right join HR_Person P on H.ID=P.ID ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            List<VAN_OA.Model.HR.HR_Payment> list = new List<VAN_OA.Model.HR.HR_Payment>();
		    using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ListReaderBind(objReader));
                    }
                }
			}
			return list;
		}

		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
        public VAN_OA.Model.HR.HR_Payment ListReaderBind(IDataReader dataReader)
		{
            VAN_OA.Model.HR.HR_Payment model = new VAN_OA.Model.HR.HR_Payment();
            model.ID = (int)dataReader["ID"];
            model.YearMonth = dataReader["YearMonth"].ToString();
            model.Name = dataReader["Name"].ToString();
            model.Position = dataReader["Position"].ToString();
            if (dataReader["BasicSalary"] != DBNull.Value)
            {
            model.BasicSalary = (decimal)dataReader["BasicSalary"];
            }
            if (dataReader["FullAttendence"] != DBNull.Value)
            {
                model.FullAttendence = (decimal)dataReader["FullAttendence"];
            }
            if (dataReader["MobileFee"] != DBNull.Value)
            {
                model.MobileFee = (decimal)dataReader["MobileFee"];
            }
            if (dataReader["SpecialAward"] != DBNull.Value)
            {
                model.SpecialAward = (decimal)dataReader["SpecialAward"];
            }
                model.SpecialAwardNote = dataReader["SpecialAwardNote"].ToString();
            if (dataReader["Gonglin"] != DBNull.Value)
            {
                model.GongLin = (decimal)dataReader["Gonglin"];
            }
            if (dataReader["PositionPerformance"] != DBNull.Value)
            {
                model.PositionPerformance = (decimal)dataReader["PositionPerformance"];
            }
            if (dataReader["PositionFee"] != DBNull.Value)
            {
                model.PositionFee = (decimal)dataReader["PositionFee"];
            }
            if (dataReader["WorkPerformance"] != DBNull.Value)
            {
                model.WorkPerformance = (decimal)dataReader["WorkPerformance"];
            }
            if (dataReader["FullPayment"] != DBNull.Value)
            {
                model.FullPayment = (decimal)dataReader["FullPayment"];
            }
            if (dataReader["WorkDays"] != DBNull.Value)
            {
                model.WorkDays = (decimal)dataReader["WorkDays"];
            }
            if (dataReader["ShouldPayment"] != DBNull.Value)
            {
                model.ShouldPayment = (decimal)dataReader["ShouldPayment"];
            }
            if (dataReader["UnionFee"] != DBNull.Value)
            {
                model.UnionFee = (decimal)dataReader["UnionFee"];
            }
            if (dataReader["Deduction"] != DBNull.Value)
            {
                model.Deduction = (decimal)dataReader["Deduction"];
            }
                model.DeductionNote = dataReader["DeductionNote"].ToString();
            if (dataReader["YangLaoJin"] != DBNull.Value)
            {
                model.YangLaoJin = (decimal)dataReader["YangLaoJin"];
            }
            if (dataReader["ActualPayment"] != DBNull.Value)
            {
                model.ActualPayment = (decimal)dataReader["ActualPayment"];
            }
            object ojb;
			ojb = dataReader["UpdateTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UpdateTime=(DateTime)ojb;
			}
			ojb = dataReader["UpdatePerson"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UpdatePerson=(int)ojb;
			}
			return model;
		}
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.HR.HR_Payment SalaryReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.HR.HR_Payment model = new VAN_OA.Model.HR.HR_Payment();
            model.ID = (int)dataReader["ID"];
            model.YearMonth = dataReader["YearMonth"].ToString();
            model.Name  = dataReader["Name"].ToString();
            if (dataReader["BasicSalary"] != DBNull.Value)
            {
                model.BasicSalary = (decimal)dataReader["BasicSalary"];
            }
            if (dataReader["FullAttendence"] != DBNull.Value)
            {
                model.FullAttendence = (decimal)dataReader["FullAttendence"];
            }
            if (dataReader["MobileFee"] != DBNull.Value)
            {
                model.MobileFee = (decimal)dataReader["MobileFee"];
            }
            if (dataReader["SpecialAward"] != DBNull.Value)
            {
                model.SpecialAward = (decimal)dataReader["SpecialAward"];
            }
            model.SpecialAwardNote = dataReader["SpecialAwardNote"].ToString();
            if (dataReader["Gonglin"] != DBNull.Value)
            {
                model.GongLin = (decimal)dataReader["Gonglin"];
            }
            if (dataReader["PositionPerformance"] != DBNull.Value)
            {
                model.PositionPerformance = (decimal)dataReader["PositionPerformance"];
            }
            if (dataReader["PositionFee"] != DBNull.Value)
            {
                model.PositionFee = (decimal)dataReader["PositionFee"];
            }
            if (dataReader["WorkPerformance"] != DBNull.Value)
            {
                model.WorkPerformance = (decimal)dataReader["WorkPerformance"];
            }
            if (dataReader["FullPayment"] != DBNull.Value)
            {
                model.FullPayment = (decimal)dataReader["FullPayment"];
            }
            if (dataReader["DefaultWorkDays"] != DBNull.Value)
            {
                model.DefaultWorkDays = (decimal)dataReader["DefaultWorkDays"];
            }
            if (dataReader["WorkDays"] != DBNull.Value)
            {
                model.WorkDays = (decimal)dataReader["WorkDays"];
            }
            if (dataReader["ShouldPayment"] != DBNull.Value)
            {
                model.ShouldPayment = (decimal)dataReader["ShouldPayment"];
            }
            if (dataReader["UnionFee"] != DBNull.Value)
            {
                model.UnionFee = (decimal)dataReader["UnionFee"];
            }
            if (dataReader["Deduction"] != DBNull.Value)
            {
                model.Deduction = (decimal)dataReader["Deduction"];
            }
            model.DeductionNote = dataReader["DeductionNote"].ToString();
            if (dataReader["YangLaoJin"] != DBNull.Value)
            {
                model.YangLaoJin = (decimal)dataReader["YangLaoJin"];
            }
            if (dataReader["ActualPayment"] != DBNull.Value)
            {
                model.ActualPayment = (decimal)dataReader["ActualPayment"];
            }
            object ojb;
            ojb = dataReader["UpdateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpdateTime = (DateTime)ojb;
            }
            ojb = dataReader["UpdatePerson"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpdatePerson = (int)ojb;
            }
            return model;
        }
    }
}
