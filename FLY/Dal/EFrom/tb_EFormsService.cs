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

using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;



namespace VAN_OA.Dal.EFrom
{
    public class tb_EFormsService
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(VAN_OA.Model.EFrom.tb_EForms model,SqlCommand  objCommand)
		{

            //try
            //{
            //    string sql = string.Format("select count(*) from tb_EForms where e_Id={0} and prosIds={1}", model.e_Id, model.prosIds);
            //    objCommand.CommandText = sql;
            //    if (Convert.ToInt32(objCommand.ExecuteScalar()) > 0)
            //    {
            //        return 0;
            //    }
            //}
            //catch (Exception)
            //{
                
            //     return 0;
            //}

			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_EForms(");
			strSql.Append("e_Id,audPer,consignor,doTime,idea,resultState,prosIds,roleName");
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(""+model.e_Id+",");
			strSql.Append(""+model.audPer+",");
			strSql.Append(""+model.consignor+",");
			strSql.Append("'"+model.doTime+"',");
			strSql.Append("'"+model.idea+"',");
			strSql.Append("'"+model.resultState+"',");
			strSql.Append(""+model.prosIds+",");
            strSql.Append("'" + model.RoleName + "'");
			strSql.Append(")");
			strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
			int result;
            object obj = objCommand.ExecuteScalar();
			if(!int.TryParse(obj.ToString(),out result))
			{
				return 0;
			}
			return result;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(VAN_OA.Model.EFrom.tb_EForms model)
		{
		 
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_EForms set ");
			strSql.Append("e_Id="+model.e_Id+",");
			strSql.Append("audPer="+model.audPer+",");
			strSql.Append("consignor="+model.consignor+",");
			strSql.Append("doTime='"+model.doTime+"',");
			strSql.Append("idea='"+model.idea+"',");
			strSql.Append("resultState='"+model.resultState+"',");
			strSql.Append("prosIds="+model.prosIds+",");

            strSql.Append("RoleName='" + model.RoleName + "'");
			strSql.Append(" where id="+ model.id+"");
            DBHelp.ExeCommand(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			 
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_EForms ");
			strSql.Append(" where id="+id+"" );
            DBHelp.ExeCommand(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public VAN_OA.Model.EFrom.tb_EForms GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select   ");
            strSql.Append(" id,e_Id,audPer,consignor,doTime,idea,resultState,prosIds,audPer_Name,consignor_Name,consignor_Name ");
            strSql.Append(" from EForms_View ");
			strSql.Append(" where id="+id+"" );
			 
			VAN_OA.Model.EFrom.tb_EForms model=null;
			 using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
			}
			return model;
		}

		 

		/// <summary>
		/// 获得数据列表（比DataSet效率高，推荐使用）
		/// </summary>
		public List<VAN_OA.Model.EFrom.tb_EForms> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM EForms_View ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}

            strSql.Append(" order by id ");
			List<VAN_OA.Model.EFrom.tb_EForms> list = new List<VAN_OA.Model.EFrom.tb_EForms>();
			 using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind_1(dataReader));
                    }
                }
			}
			return list;
		}
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_EForms ReaderBind_1(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_EForms model = new VAN_OA.Model.EFrom.tb_EForms();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["e_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.e_Id = (int)ojb;
            }
            model.audPer =Convert.ToInt32( dataReader["audPer"].ToString());
            model.consignor =Convert.ToInt32( dataReader["consignor"].ToString());
            model.doTime =Convert.ToDateTime( dataReader["doTime"].ToString());
            model.idea = dataReader["idea"].ToString();
            model.resultState = dataReader["resultState"].ToString();
            ojb = dataReader["prosIds"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.prosIds = (int)ojb;
            }


            ojb = dataReader["Audper_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Audper_Name = ojb.ToString();
            }

            ojb = dataReader["Consignor_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Consignor_Name = ojb.ToString();
            }

            ojb = dataReader["roleName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RoleName = ojb.ToString();
            }
            


            //model.A_IFEdit = Convert.ToBoolean(dataReader["A_IFEdit"]);
            //model.A_Index = Convert.ToInt32(dataReader["a_Index"]);
            //model.A_RoleName = dataReader["A_RoleName"].ToString();
            return model;
        }


		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public VAN_OA.Model.EFrom.tb_EForms ReaderBind(IDataReader dataReader)
		{
			VAN_OA.Model.EFrom.tb_EForms model=new VAN_OA.Model.EFrom.tb_EForms();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id=(int)ojb;
			}
			ojb = dataReader["e_Id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.e_Id=(int)ojb;
			}
            model.audPer = Convert.ToInt32(dataReader["audPer"].ToString());
            model.consignor = Convert.ToInt32(dataReader["consignor"].ToString());
			model.doTime=Convert.ToDateTime(dataReader["doTime"].ToString());
			model.idea=dataReader["idea"].ToString();
			model.resultState=dataReader["resultState"].ToString();
			ojb = dataReader["prosIds"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.prosIds=(int)ojb;
			}
            ojb = dataReader["Audper_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Audper_Name = ojb.ToString();
            }

            ojb = dataReader["Consignor_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Consignor_Name = ojb.ToString();
            }
			return model;
		}

	 
	
    }
}
