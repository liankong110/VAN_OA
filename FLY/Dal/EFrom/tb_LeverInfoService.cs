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
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
namespace VAN_OA.Dal.EFrom
{
    public class tb_LeverInfoService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_LeverInfo model, VAN_OA.Model.EFrom.tb_EForm eform,tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_LeverInfo model, VAN_OA.Model.EFrom.tb_EForm eform)
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

                    tb_EFormService eformSer = new tb_EFormService();

                    objCommand.Parameters.Clear();
                    string proNo = eformSer.GetAllE_No("tb_LeverInfo", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.tb_LeverInfo model,SqlCommand objCommand)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_LeverInfo(");
            strSql.Append("depart,zhiwu,name,leverType,dateForm,dateTo,remark,ZhuGuan,ProNo,AppDate");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + model.depart + "',");
            strSql.Append("'" + model.zhiwu + "',");
            strSql.Append("'" + model.name + "',");
            strSql.Append("'" + model.leverType + "',");
            strSql.Append("'" + model.dateForm + "',");
            strSql.Append("'" + model.dateTo + "',");
            strSql.Append("'" + model.remark + "',");
            strSql.Append("" + model.ZhuGuan + ",");
            strSql.Append("'" + model.ProNo + "',");
            strSql.Append("'" + model.AppDate + "'");
            

            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            
           
            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_LeverInfo model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_LeverInfo set ");
            strSql.Append("depart='" + model.depart + "',");
            strSql.Append("zhiwu='" + model.zhiwu + "',");
            strSql.Append("name='" + model.name + "',");
            strSql.Append("leverType='" + model.leverType + "',");
            strSql.Append("dateForm='" + model.dateForm + "',");
            strSql.Append("dateTo='" + model.dateTo + "',");
            strSql.Append("remark='" + model.remark + "',");
            strSql.Append("ZhuGuan=" + model.ZhuGuan + "");
            strSql.Append(" where id=" + model.id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
          
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_LeverInfo ");
            strSql.Append(" where id=" + id + "");
             DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_LeverInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" id,depart,zhiwu,name,leverType,dateForm,dateTo,remark,ZhuGuan,proNo,AppDate ");
            strSql.Append(" from tb_LeverInfo ");
            strSql.Append(" where id=" + id + "");
            
            VAN_OA.Model.EFrom.tb_LeverInfo model = null;
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
		public List<VAN_OA.Model.EFrom.tb_LeverInfo> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select id,depart,zhiwu,name,leverType,dateForm,dateTo,remark,ZhuGuan,proNo,AppDate ");
            strSql.Append(" FROM tb_LeverInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
			List<VAN_OA.Model.EFrom.tb_LeverInfo> list = new List<VAN_OA.Model.EFrom.tb_LeverInfo>();
			 
			 using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
				while (dataReader.Read())
				{
					list.Add(ReaderBind(dataReader));
				}
			}}
			return list;
		}
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_LeverInfo> GetListArray_1(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,depart,zhiwu,name,leverType,dateForm,dateTo,remark,ZhuGuan,proNo,AppDate ");
            strSql.Append(" FROM tb_LeverInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by  name");
            List<VAN_OA.Model.EFrom.tb_LeverInfo> list = new List<VAN_OA.Model.EFrom.tb_LeverInfo>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }

		/// <summary>
		/// 对象实体绑定数据
		/// </summary>
		public VAN_OA.Model.EFrom.tb_LeverInfo ReaderBind(IDataReader dataReader)
		{
            VAN_OA.Model.EFrom.tb_LeverInfo model = new VAN_OA.Model.EFrom.tb_LeverInfo();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.depart = dataReader["depart"].ToString();
            model.zhiwu = dataReader["zhiwu"].ToString();
            model.name = dataReader["name"].ToString();
            model.leverType = dataReader["leverType"].ToString();
            ojb = dataReader["dateForm"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.dateForm = (DateTime)ojb;
            }
            ojb = dataReader["dateTo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.dateTo = (DateTime)ojb;
            }
            model.remark = dataReader["remark"].ToString();
            ojb = dataReader["ZhuGuan"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ZhuGuan = (int)ojb;
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            ojb = dataReader["AppDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppDate = (DateTime)ojb;
            }
            return model;
		}

		 

    }
}
