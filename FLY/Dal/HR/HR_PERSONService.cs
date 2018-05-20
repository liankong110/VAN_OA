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
    public class HR_PERSONService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(VAN_OA.Model.HR.HR_PERSON model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("ID,");
            strSql2.Append(model.ID + ",");
            if (model.Code != null)
            {
                strSql1.Append("Code,");
                strSql2.Append("'" + model.Code + "',");
            }
            if (model.Department != null)
            {
                strSql1.Append("Department,");
                strSql2.Append("'" + model.Department + "',");
            }
            if (model.Position != null)
            {
                strSql1.Append("Position,");
                strSql2.Append("'" + model.Position + "',");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.Birthday != null)
            {
                strSql1.Append("Birthday,");
                strSql2.Append("'" + model.Birthday + "',");
            }
            if (model.Sex != null)
            {
                strSql1.Append("Sex,");
                strSql2.Append("'" + model.Sex + "',");
            }
            if (model.EducationLevel != null)
            {
                strSql1.Append("EducationLevel,");
                strSql2.Append("'" + model.EducationLevel + "',");
            }
            if (model.EducationSchool != null)
            {
                strSql1.Append("EducationSchool,");
                strSql2.Append("'" + model.EducationSchool + "',");
            }
            if (model.Major != null)
            {
                strSql1.Append("Major,");
                strSql2.Append("'" + model.Major + "',");
            }
            if (model.GraduationTime != null)
            {
                strSql1.Append("GraduationTime,");
                strSql2.Append("'" + model.GraduationTime + "',");
            }
            if (model.OnBoardTime != null)
            {
                strSql1.Append("OnBoardTime,");
                strSql2.Append("'" + model.OnBoardTime + "',");
            }
            if (model.BeNormalTime != null)
            {
                strSql1.Append("BeNormalTime,");
                strSql2.Append("'" + model.BeNormalTime + "',");
            }
            if (model.ContractTime != null)
            {
                strSql1.Append("ContractTime,");
                strSql2.Append("'" + model.ContractTime + "',");
            }
            if (model.ContractCloseTime != null)
            {
                strSql1.Append("ContractCloseTime,");
                strSql2.Append("'" + model.ContractCloseTime + "',");
            }
            if (model.HuKou != null)
            {
                strSql1.Append("HuKou,");
                strSql2.Append("'" + model.HuKou + "',");
            }
            if (model.Marriage != null)
            {
                strSql1.Append("Marriage,");
                strSql2.Append("'" + model.Marriage + "',");
            }
            if (model.IDCard != null)
            {
                strSql1.Append("IDCard,");
                strSql2.Append("'" + model.IDCard + "',");
            }
            if (model.MobilePhone != null)
            {
                strSql1.Append("MobilePhone,");
                strSql2.Append("'" + model.MobilePhone + "',");
            }
            if (model.HomePhone != null)
            {
                strSql1.Append("HomePhone,");
                strSql2.Append("'" + model.HomePhone + "',");
            }
            if (model.HomeAddress != null)
            {
                strSql1.Append("HomeAddress,");
                strSql2.Append("'" + model.HomeAddress + "',");
            }
            if (model.EmailAddress != null)
            {
                strSql1.Append("EmailAddress,");
                strSql2.Append("'" + model.EmailAddress + "',");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.CreatePerson != null)
            {
                strSql1.Append("CreatePerson,");
                strSql2.Append("" + model.CreatePerson + ",");
            }
            if (model.Quit != null)
            {
                strSql1.Append("QuitStatus,");
                strSql2.Append("" + (model.Quit == true ? 1 : 0) + ",");
            }
            if (model.QuitTime != null)
            {
                strSql1.Append("QuitTime,");
                strSql2.Append("'" + model.QuitTime + "',");
            }
            if (model.QuitReason != null)
            {
                strSql1.Append("QuitReason,");
                strSql2.Append("'" + model.QuitReason + "',");
            }
            //if (model.UpdateTime != null)
            //{
            //    strSql1.Append("UpdateTime,");
            //    strSql2.Append("'" + model.UpdateTime + "',");
            //}
            //if (model.UpdatePerson != null)
            //{
            //    strSql1.Append("UpdatePerson,");
            //    strSql2.Append("" + model.UpdatePerson + ",");
            //}
            strSql.Append("insert into HR_PERSON(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }
        public int AddSalary(VAN_OA.Model.HR.HR_PERSON model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
                strSql1.Append("ID,");
                strSql2.Append(model.ID + ",");
                strSql1.Append("BasicSalary,");
                strSql2.Append( model.BasicSalary + ",");
                strSql1.Append("GongLin,");
                strSql2.Append( model.GongLin + ",");
                strSql1.Append("MobileFee,");
                strSql2.Append( model.MobileFee + ",");
                strSql1.Append("PositionFee,");
                strSql2.Append( model.PositionFee + ",");
                strSql1.Append("YangLaoJin,");
                strSql2.Append(model.YangLaoJin + ",");
                strSql1.Append("UnionFee,");
                strSql2.Append(model.UnionFee + ",");
                strSql1.Append("DefaultWorkDays,");
                strSql2.Append(model.DefaultWorkDays + ",");
                strSql1.Append("IsRetailed,");
                strSql2.Append((model.IsRetailed== true ? 1 : 0)  + ",");
                strSql1.Append("IsQuit,");
                strSql2.Append((model.IsQuit== true ? 1 : 0) + ",");
            if (model.UpdateTime != null)
            {
                strSql1.Append("UpdateTime,");
                strSql2.Append("'" + model.UpdateTime + "',");
            }
            if (model.UpdatePerson != null)
            {
                strSql1.Append("UpdatePerson,");
                strSql2.Append("" + model.UpdatePerson + ",");
            }
            strSql.Append("insert into HR_Salary(");
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
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条员工数据
        /// </summary>
        public bool Update(VAN_OA.Model.HR.HR_PERSON model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HR_PERSON set ");
            if (model.Code != null)
            {
                strSql.Append("Code='" + model.Code + "',");
            }
            else
            {
                strSql.Append("Code= null ,");
            }
            if (model.Department != null)
            {
                strSql.Append("Department='" + model.Department + "',");
            }
            else
            {
                strSql.Append("Department= null ,");
            }
            if (model.Position != null)
            {
                strSql.Append("Position='" + model.Position + "',");
            }
            else
            {
                strSql.Append("Position= null ,");
            }
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            else
            {
                strSql.Append("Name= null ,");
            }
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            else
            {
                strSql.Append("Birthday= null ,");
            }
            if (model.Sex != null)
            {
                strSql.Append("Sex='" + model.Sex + "',");
            }
            else
            {
                strSql.Append("Sex= null ,");
            }
            if (model.EducationLevel != null)
            {
                strSql.Append("EducationLevel='" + model.EducationLevel + "',");
            }
            else
            {
                strSql.Append("EducationLevel= null ,");
            }
            if (model.EducationSchool != null)
            {
                strSql.Append("EducationSchool='" + model.EducationSchool + "',");
            }
            if (model.Major != null)
            {
                strSql.Append("Major='" + model.Major + "',");
            }
            else
            {
                strSql.Append("Major= null ,");
            }
            if (model.GraduationTime != null)
            {
                strSql.Append("GraduationTime='" + model.GraduationTime + "',");
            }
            else
            {
                strSql.Append("GraduationTime= null ,");
            }
            if (model.OnBoardTime != null)
            {
                strSql.Append("OnBoardTime='" + model.OnBoardTime + "',");
            }
            else
            {
                strSql.Append("OnBoardTime= null ,");
            }
            if (model.BeNormalTime != null)
            {
                strSql.Append("BeNormalTime='" + model.BeNormalTime + "',");
            }
            else
            {
                strSql.Append("BeNormalTime= null ,");
            }

            if (model.ContractTime != null)
            {
                strSql.Append("ContractTime='" + model.ContractTime + "',");
            }
            else
            {
                strSql.Append("ContractTime= null ,");
            }
            if (model.ContractCloseTime != null)
            {
                strSql.Append("ContractCloseTime='" + model.ContractCloseTime + "',");
            }
            else
            {
                strSql.Append("ContractCloseTime= null ,");
            }
            if (model.HuKou != null)
            {
                strSql.Append("HuKou='" + model.HuKou + "',");
            }
            else
            {
                strSql.Append("HuKou= null ,");
            }
            if (model.Marriage != null)
            {
                strSql.Append("Marriage='" + model.Marriage + "',");
            }
            else
            {
                strSql.Append("Marriage= null ,");
            }
            if (model.IDCard != null)
            {
                strSql.Append("IDCard='" + model.IDCard + "',");
            }
            else
            {
                strSql.Append("IDCard= null ,");
            }
            if (model.MobilePhone != null)
            {
                strSql.Append("MobilePhone='" + model.MobilePhone + "',");
            }
            else
            {
                strSql.Append("MobilePhone= null ,");
            }
            if (model.HomePhone != null)
            {
                strSql.Append("HomePhone='" + model.HomePhone + "',");
            }
            else
            {
                strSql.Append("HomePhone= null ,");
            }
            if (model.HomeAddress != null)
            {
                strSql.Append("HomeAddress='" + model.HomeAddress + "',");
            }
            else
            {
                strSql.Append("HomeAddress= null ,");
            }
            if (model.EmailAddress != null)
            {
                strSql.Append("EmailAddress='" + model.EmailAddress + "',");
            }
            else
            {
                strSql.Append("EmailAddress= null ,");
            }
            if (model.Quit != null)
            {
                strSql.Append("QuitStatus=" + (model.Quit == true ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("QuitStatus= 0 ,");
            }
            if (model.QuitTime != null)
            {
                strSql.Append("QuitTime='" + model.QuitTime + "',");
            }
            else
            {
                strSql.Append("QuitTime= null ,");
            }
            if (model.QuitReason != null)
            {
                strSql.Append("QuitReason='" + model.QuitReason + "',");
            }
            else
            {
                strSql.Append("QuitReason= null ,");
            }
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}
            //else
            //{
            //    strSql.Append("CreateTime= null ,");
            //}
            //if (model.CreatePerson != null)
            //{
            //    strSql.Append("CreatePerson=" + model.CreatePerson + ",");
            //}
            //else
            //{
            //    strSql.Append("CreatePerson= null ,");
            //}
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
            strSql.Append(" where ID=" + model.ID + "");
            bool rowsAffected =DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }
        /// <summary>
        /// 更新一条基本工资数据
        /// </summary>
        public bool UpdateSalary(VAN_OA.Model.HR.HR_PERSON model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HR_Salary set ");
            strSql.Append("BasicSalary=" + model.BasicSalary + ",");
            strSql.Append("GongLin=" + model.GongLin + ",");
            strSql.Append("MobileFee=" + model.MobileFee + ",");
            strSql.Append("PositionFee=" + model.PositionFee + ",");
            strSql.Append("YangLaoJin=" + model.YangLaoJin + ",");
            strSql.Append("UnionFee=" + model.UnionFee + ",");
            strSql.Append("DefaultWorkDays=" + model.DefaultWorkDays + ",");
            strSql.Append("IsRetailed=" + (model.IsRetailed == true ? 1 : 0)  + ",");
            strSql.Append("IsQuit=" + (model.IsQuit == true ? 1 : 0)  + ",");
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
            strSql.Append(" where ID=" + model.ID + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
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
        public VAN_OA.Model.HR.HR_PERSON GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" ID,Code,Department,Position,Name,Birthday,Sex,EducationLevel,EducationSchool,Major,GraduationTime,OnBoardTime,BeNormalTime,ContractTime,ContractCloseTime,HuKou,Marriage,IDCard,MobilePhone,HomePhone,HomeAddress,EmailAddress,CreateTime,CreatePerson,UpdateTime,UpdatePerson,QuitStatus,'' as QuitStatusName ");
            strSql.Append(" from HR_PERSON ");
            strSql.Append(" where ID=" + ID + "");
        
            VAN_OA.Model.HR.HR_PERSON model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model = ReaderBind(objReader);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 得到一个员工基本工资的对象实体
        /// </summary>
        public VAN_OA.Model.HR.HR_PERSON GetSalary(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select");
            strSql.Append(" H.ID,U.Name,H.BasicSalary,H.GongLin,H.MobileFee,H.PositionFee,H.YangLaoJin,H.UnionFee,H.DefaultWorkDays,H.IsRetailed,H.IsQuit,H.UpdateTime,H.UpdatePerson,U.OnBoardTime,U.QuitTime,U1.loginName as UpdatePersonName");
            strSql.Append(" from HR_Salary H right join HR_Person U on H.ID=U.ID left join tb_user U1 on H.UpdatePerson=U1.ID");
            strSql.Append(" where U.ID=" + ID + "");

            VAN_OA.Model.HR.HR_PERSON model = null;
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
		public List<VAN_OA.Model.HR.HR_PERSON> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,Code,Department,Position,Name,Birthday,Sex,EducationLevel,EducationSchool,GraduationTime,Major,OnBoardTime,BeNormalTime,ContractTime,ContractCloseTime,HomeAddress,Marriage,HuKou,IDCard,MobilePhone,HomePhone,HomeAddress,EmailAddress,CreateTime,CreatePerson,UpdateTime,UpdatePerson,QuitStatus,dbo.getHRQuitStatus(QuitStatus) as QuitStatusName ");
			strSql.Append(" FROM HR_PERSON ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<VAN_OA.Model.HR.HR_PERSON> list = new List<VAN_OA.Model.HR.HR_PERSON>();
		    using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind(objReader));
                    }
                }
			}
			return list;
		}

		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public VAN_OA.Model.HR.HR_PERSON ReaderBind(IDataReader dataReader)
		{
			VAN_OA.Model.HR.HR_PERSON model=new VAN_OA.Model.HR.HR_PERSON();
			object ojb; 
			ojb = dataReader["ID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ID=(int)ojb;
			}
			model.Code=dataReader["Code"].ToString();
			model.Department=dataReader["Department"].ToString();
			model.Position=dataReader["Position"].ToString();
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["Birthday"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Birthday=(DateTime)ojb;
			}
			model.Sex=dataReader["Sex"].ToString();
			model.EducationLevel=dataReader["EducationLevel"].ToString();
			model.EducationSchool=dataReader["EducationSchool"].ToString();
            model.Major = dataReader["Major"].ToString();
			ojb = dataReader["GraduationTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
                model.GraduationTime = (DateTime)ojb;
			}
            ojb = dataReader["OnBoardTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
                model.OnBoardTime = (DateTime)ojb;
			}
			ojb = dataReader["BeNormalTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
                model.BeNormalTime = (DateTime)ojb;
			}
			ojb = dataReader["ContractTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
                model.ContractTime = (DateTime)ojb;
			}
            ojb = dataReader["ContractCloseTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ContractCloseTime = (DateTime)ojb;
            }
            model.HuKou = dataReader["HuKou"].ToString();
            model.Marriage = dataReader["Marriage"].ToString();
            model.IDCard = dataReader["IDCard"].ToString();
            model.MobilePhone = dataReader["MobilePhone"].ToString();
            model.HomePhone = dataReader["HomePhone"].ToString();
            model.HomeAddress = dataReader["HomeAddress"].ToString();
            model.EmailAddress = dataReader["EmailAddress"].ToString();
            ojb = dataReader["CreateTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CreateTime=(DateTime)ojb;
			}
			ojb = dataReader["CreatePerson"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CreatePerson=(int)ojb;
			}
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
            ojb = dataReader["QuitStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Quit = (bool)ojb;
            }
            model.QuitStatusName = dataReader["QuitStatusName"].ToString();
			return model;
		}
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.HR.HR_PERSON SalaryReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.HR.HR_PERSON model = new VAN_OA.Model.HR.HR_PERSON();
            object ojb;
            ojb = dataReader["ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            model.Name = dataReader["Name"].ToString();
            ojb = dataReader["BasicSalary"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BasicSalary = (Decimal)ojb;
            }
            ojb = dataReader["GongLin"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GongLin = (Decimal)ojb;
            }
            ojb = dataReader["MobileFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MobileFee = (Decimal)ojb;
            }
            ojb = dataReader["PositionFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PositionFee = (Decimal)ojb;
            }
            ojb = dataReader["YangLaoJin"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.YangLaoJin = (Decimal)ojb;
            }
            ojb = dataReader["UnionFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UnionFee = (Decimal)ojb;
            }
            ojb = dataReader["DefaultWorkDays"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DefaultWorkDays = (Decimal)ojb;
            }
            ojb = dataReader["IsRetailed"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsRetailed = (bool)ojb;
            }
            else
            {
                model.IsRetailed = false;
            }
            ojb = dataReader["IsQuit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsQuit = (bool)ojb;
            }
            else
            {
                model.IsQuit = false;
            }
            ojb = dataReader["OnBoardTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OnBoardTime = (DateTime )ojb;
            }
            ojb = dataReader["QuitTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QuitTime = (DateTime)ojb;
            }
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
            ojb = dataReader["UpdatePersonName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpdatePersonName = (string)ojb;
            }
            return model;
        }
    }
}
