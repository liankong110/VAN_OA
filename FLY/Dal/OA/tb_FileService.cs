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
using System.Text;
using VAN_OA.Model.OA;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VAN_OA.Dal.OA
{
    public class tb_FileService
    {

      /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(VAN_OA.Model.OA.tb_File model)
		{
			 
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_File(");
            strSql.Append("fileName,fileURL,fileFullName,createTime,createPer");
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append("'"+model.fileName+"',");
			strSql.Append("'"+model.fileURL+"',");
            strSql.Append("'" + model.fileFullName + "',");
            strSql.Append("'" + model.createTime + "',");
            strSql.Append("'" + model.createPer + "'");
			strSql.Append(")");
			strSql.Append(";select @@IDENTITY");
		 
			int result;
			object obj =DBHelp.ExeScalar(strSql.ToString());// db.ExecuteScalar(dbCommand);
			if(!int.TryParse(obj.ToString(),out result))
			{
				return 0;
			}
			return result;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(VAN_OA.Model.OA.tb_File model)
		{

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update tb_File set ");
                strSql.Append("fileName='" + model.fileName + "',");
                strSql.Append("fileURL='" + model.fileURL + "',");
                strSql.Append("fileFullName='" + model.fileFullName + "',");
                strSql.Append("createTime='" + model.createTime + "',");
                strSql.Append("createPer='" + model.createPer + "'");
                strSql.Append(" where id=" + model.id + " ");

                DBHelp.ExeCommand(strSql.ToString());
                return true;
            }
            catch (Exception)
            {
                
                 return false;
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			 
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_File ");
			strSql.Append(" where id="+id+" " );
			 DBHelp.ExeCommand(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public VAN_OA.Model.OA.tb_File GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select   ");
			strSql.Append(" * ");
			strSql.Append(" from tb_File ");
			strSql.Append(" where id="+id+" " );
			 
			VAN_OA.Model.OA.tb_File model=null;
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

	 
		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "tb_File");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "System.Collections.Generic.List`1[LTP.CodeHelper.ColumnInfo]");
			db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
			db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
			db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
			return db.ExecuteDataSet(dbCommand);
		}*/

		/// <summary>
		/// 获得数据列表（比DataSet效率高，推荐使用）
		/// </summary>
		public List<VAN_OA.Model.OA.tb_File> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM tb_File ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}

            strSql.Append(" order by createTime desc");
			List<VAN_OA.Model.OA.tb_File> list = new List<VAN_OA.Model.OA.tb_File>();
			 
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
		public VAN_OA.Model.OA.tb_File ReaderBind(IDataReader dataReader)
		{
			VAN_OA.Model.OA.tb_File model=new VAN_OA.Model.OA.tb_File();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id=(int)ojb;
			}
			model.fileName=dataReader["fileName"].ToString();
			model.fileURL=dataReader["fileURL"].ToString();
            model.fileFullName = dataReader["fileFullName"].ToString();
            ojb = dataReader["createTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.createTime = (DateTime)ojb;
            }
            model.createPer = dataReader["createPer"].ToString();
			return model;
		}

		 


    }
}
