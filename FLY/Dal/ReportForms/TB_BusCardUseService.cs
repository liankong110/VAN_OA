using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.Dal.ReportForms
{
    public class TB_BusCardUseService
    {
        public bool updateTran(VAN_OA.Model.ReportForms.TB_BusCardUse model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.Parameters.Clear();
                    model.Status = eform.state;
                    Update(model, objCommand);


                    tb_EFormService eformSer = new tb_EFormService();

                    eformSer.Update(eform, objCommand);


                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.ReportForms.TB_BusCardUse model, VAN_OA.Model.EFrom.tb_EForm eform)
        {
            int id = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    objCommand.Parameters.Clear();
                    model.Status = eform.state;
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("TB_BusCardUse", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;
                    id = Add(model, objCommand);
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_BusCardUse model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.BusCardNo != null)
            {
                strSql1.Append("BusCardNo,");
                strSql2.Append("'" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql1.Append("BusCardPer,");
                strSql2.Append("'" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql1.Append("BusCardDate,");
                strSql2.Append("'" + model.BusCardDate + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.UseTotal != null)
            {
                strSql1.Append("UseTotal,");
                strSql2.Append("" + model.UseTotal + ",");
            }
            if (model.BusUseRemark != null)
            {
                strSql1.Append("BusUseRemark,");
                strSql2.Append("'" + model.BusUseRemark + "',");
            }

            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            if (model.POGuestName != null)
            {
                strSql1.Append("POGuestName,");
                strSql2.Append("'" + model.POGuestName + "',");
            }
            if (model.PONo != null)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            if (model.POName != null)
            {
                strSql1.Append("POName,");
                strSql2.Append("'" + model.POName + "',");
            }

            strSql1.Append("ProNo,");
            strSql2.Append("'" + model.ProNo + "',");

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            strSql1.Append("CreateTime,");
            strSql2.Append("getdate(),");

            strSql1.Append("useName,");
            strSql2.Append("'" + model.UseName + "',");

            strSql.Append("insert into TB_BusCardUse(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            
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
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.ReportForms.TB_BusCardUse model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BusCardUse set ");
            if (model.BusCardNo != null)
            {
                strSql.Append("BusCardNo='" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql.Append("BusCardPer='" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql.Append("BusCardDate='" + model.BusCardDate + "',");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.UseTotal != null)
            {
                strSql.Append("UseTotal=" + model.UseTotal + ",");
            }
            if (model.BusUseRemark != null)
            {
                strSql.Append("BusUseRemark='" + model.BusUseRemark + "',");
            }
            else
            {
                strSql.Append("BusUseRemark= null ,");
            }

            if (model.POGuestName != null)
            {
                strSql.Append("POGuestName='" + model.POGuestName + "',");
            }
            else
            {
                strSql.Append("POGuestName= null ,");
            }
            if (model.PONo != null)
            {
                strSql.Append("PONo='" + model.PONo + "',");
            }
            else
            {
                strSql.Append("PONo= null ,");
            }
            if (model.POName != null)
            {
                strSql.Append("POName='" + model.POName + "',");
            }
            else
            {
                strSql.Append("POName= null ,");
            }
            if (model.UseName != null)
            {
                strSql.Append("UseName='" + model.UseName + "',");
            }
            else
            {
                strSql.Append("UseName= null ,");
            }
            strSql.Append("CreateTime=getdate(),");
            strSql.Append("Status='" + model.Status + "',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            return objCommand.ExecuteNonQuery() > 0 ? true : false;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_BusCardUse model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.BusCardNo != null)
            {
                strSql1.Append("BusCardNo,");
                strSql2.Append("'" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql1.Append("BusCardPer,");
                strSql2.Append("'" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql1.Append("BusCardDate,");
                strSql2.Append("'" + model.BusCardDate + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.UseTotal != null)
            {
                strSql1.Append("UseTotal,");
                strSql2.Append("" + model.UseTotal + ",");
            }
            if (model.BusUseRemark != null)
            {
                strSql1.Append("BusUseRemark,");
                strSql2.Append("'" + model.BusUseRemark + "',");
            }

            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            if (model.POGuestName != null)
            {
                strSql1.Append("POGuestName,");
                strSql2.Append("'" + model.POGuestName + "',");
            }
            if (model.PONo != null)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            if (model.POName != null)
            {
                strSql1.Append("POName,");
                strSql2.Append("'" + model.POName + "',");
            }


             
                strSql1.Append("CreateTime,");
                strSql2.Append("getdate(),");


            strSql1.Append("useName,");
            strSql2.Append("'" + model.UseName + "',");
            strSql.Append("insert into TB_BusCardUse(");
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
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.ReportForms.TB_BusCardUse model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BusCardUse set ");
            if (model.BusCardNo != null)
            {
                strSql.Append("BusCardNo='" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql.Append("BusCardPer='" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql.Append("BusCardDate='" + model.BusCardDate + "',");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.UseTotal != null)
            {
                strSql.Append("UseTotal=" + model.UseTotal + ",");
            }
            if (model.BusUseRemark != null)
            {
                strSql.Append("BusUseRemark='" + model.BusUseRemark + "',");
            }
            else
            {
                strSql.Append("BusUseRemark= null ,");
            }

            if (model.POGuestName != null)
            {
                strSql.Append("POGuestName='" + model.POGuestName + "',");
            }
            else
            {
                strSql.Append("POGuestName= null ,");
            }
            if (model.PONo != null)
            {
                strSql.Append("PONo='" + model.PONo + "',");
            }
            else
            {
                strSql.Append("PONo= null ,");
            }
            if (model.POName != null)
            {
                strSql.Append("POName='" + model.POName + "',");
            }
            else
            {
                strSql.Append("POName= null ,");
            }
            strSql.Append("CreateTime=getdate(),");

            if (model.UseName != null)
            {
                strSql.Append("UseName='" + model.UseName + "',");
            }
            else
            {
                strSql.Append("UseName= null ,");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
              return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_BusCardUse ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }		
        
        /// <summary>
		/// 获得数据列表（比DataSet效率高，推荐使用）
		/// </summary>
		public List<VAN_OA.Model.ReportForms.TB_BusCardUse> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select UseName,AE,ProNo,Status,TB_BusCardUse.Id,BusCardNo,BusCardPer,BusCardDate,Address,GuestName,UseTotal,BusUseRemark,CreateTime,loginName as CreateUserName,CreateUserId,POGuestName,PONo,POName  ");
            strSql.Append(" FROM TB_BusCardUse left join tb_User on tb_User.ID=TB_BusCardUse.CreateUserId ");
            strSql.Append("  LEFT JOIN ( SELECT PONo AS T_PONO,AE,GuestType,GuestPro,IsClose,IsSelected,JieIsSelected,IsSpecial FROM CG_POOrder where IFZhui=0 ) AS T_POOrder on T_POOrder.T_PONO=TB_BusCardUse.PONo ");
            if (strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by TB_BusCardUse.id desc ");

			List<VAN_OA.Model.ReportForms.TB_BusCardUse> list = new List<VAN_OA.Model.ReportForms.TB_BusCardUse>();
			using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);
                        object ojb;
                        ojb = objReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
			return list;
		}

		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public VAN_OA.Model.ReportForms.TB_BusCardUse ReaderBind(IDataReader dataReader)
		{
			VAN_OA.Model.ReportForms.TB_BusCardUse model=new VAN_OA.Model.ReportForms.TB_BusCardUse();
			object ojb; 
			ojb = dataReader["Id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Id=(int)ojb;
			}
			model.BusCardNo=dataReader["BusCardNo"].ToString();
			model.BusCardPer=dataReader["BusCardPer"].ToString();
			ojb = dataReader["BusCardDate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BusCardDate=(DateTime)ojb;
			}
			model.Address=dataReader["Address"].ToString();
			model.GuestName=dataReader["GuestName"].ToString();
			ojb = dataReader["UseTotal"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UseTotal=(decimal)ojb;
			}
			model.BusUseRemark=dataReader["BusUseRemark"].ToString();

            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime =Convert.ToDateTime( ojb);
            }

            ojb = dataReader["CreateUserName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserName =ojb.ToString();
            }

            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = Convert.ToInt32(ojb);
            }




            ojb = dataReader["POGuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POGuestName = ojb.ToString();
            }

            ojb = dataReader["PONo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PONo = ojb.ToString();
            }

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }

            model.UseName = dataReader["UseName"].ToString();
			return model;
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_BusCardUse GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" UseName,ProNo,Status,TB_BusCardUse.Id,BusCardNo,BusCardPer,BusCardDate,Address,GuestName,UseTotal,BusUseRemark,CreateTime,'' as CreateUserName,CreateUserId ,POGuestName,PONo,POName ");
            strSql.Append(" from TB_BusCardUse ");
            strSql.Append(" where Id=" + Id + "");
           
            VAN_OA.Model.ReportForms.TB_BusCardUse model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                       model=ReaderBind(objReader);
                    }
                }
            }
            return model;
        }
		
    }
}
