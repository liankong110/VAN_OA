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
    public class A_ProInfosService
    {

       

        ///// <summary>
        ///// 获取用户提交下一级审批人
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="pro_Id"></param>
        ///// <returns></returns>
        //public List<A_Role_User> getUsers(int id, int pro_Id, out int pro_IDs)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select * from A_ProInfos where pro_Id=1  ");
        //    strSql.Append(" where pro_Id=" + pro_Id);  
        //    strSql.Append(" order by a_Index desc  ");

        //    List<VAN_OA.Model.EFrom.A_ProInfos> list = new List<VAN_OA.Model.EFrom.A_ProInfos>();
        //    using (SqlConnection conn = DBHelp.getConn())
        //    {
        //        conn.Open();
        //        SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
        //        using (SqlDataReader dataReader = objCommand.ExecuteReader())
        //        {
        //            while (dataReader.Read())
        //            {
        //                int ids = Convert.ToInt32(dataReader["ids"]);
        //                int a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);
        //                A_ProInfos pro = new A_ProInfos();
        //                pro.a_Role_Id = a_Role_Id;
        //                pro.ids = ids;
        //                list.Add(pro);
        //            }
        //        }
        //    }

        //    int roleId = 0;
        //    pro_IDs = 0;
        //    if (list.Count > 0)
        //    {
        //        roleId = list[list.Count - 1].a_Role_Id;
        //        pro_IDs = list[list.Count - 1].ids;
        //        if (id != 0)
        //        {

        //            for (int i = 0; i < list.Count; i++)
        //            {
        //                if (list[i].ids == id)
        //                {
        //                    if (i == 0)
        //                    {
        //                        roleId = 0;
        //                        pro_IDs = 0;
        //                    }
        //                    else
        //                    {
        //                        roleId = list[i-1].a_Role_Id;
        //                        pro_IDs = list[i - 1].ids;
        //                    }

        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    A_Role_UserService RoleUserSer = new A_Role_UserService();
        //    if (roleId == 0)
        //    {
        //        return null;
        //    }
        //    return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0})",roleId));
        //}

        public int addList(List<A_ProInfos> allPros)
        {
            int id = 1;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {



                    objCommand.Parameters.Clear();
                    for (int i = 0; i < allPros.Count; i++)
                    {
                        if (allPros[i].ids == 0)
                        {
                            allPros[i].a_Index = i;
                           id= Add(allPros[i],objCommand);
                        }
                        else
                        {
                            string sql = string.Format("update A_ProInfos set a_Index={0} where ids={1}",i,allPros[i].ids);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }
                    }                     



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
        public int Add(VAN_OA.Model.EFrom.A_ProInfos model,SqlCommand objCommand)
        {
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into A_ProInfos(");
            strSql.Append("pro_Id,a_Role_Id,a_Index");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.pro_Id + ",");
            strSql.Append("" + model.a_Role_Id + ",");
            strSql.Append("" + model.a_Index + "");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
            int result;
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
        public void Update(VAN_OA.Model.EFrom.A_ProInfos model)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_ProInfos set ");
            strSql.Append("pro_Id=" + model.pro_Id + ",");
            strSql.Append("a_Role_Id=" + model.a_Role_Id + ",");
            strSql.Append("a_Index=" + model.a_Index + "");
            strSql.Append(" where ids=" + model.ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids,SqlCommand objCommand)
        {
          
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_ProInfos ");
            strSql.Append(" where ids=" + ids + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();


        }

        public int deleCommand(int ids, int mainId)
        {
            int id = 1;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    Delete(ids,objCommand);

                    List<A_ProInfos> allPros = GetListArray(" pro_Id=" + mainId + " and ids<>"+ids);
                    for (int i = 0; i < allPros.Count; i++)
                    {
                       
                            string sql = string.Format("update A_ProInfos set a_Index={0} where ids={1}", i, allPros[i].ids);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        
                    }



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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.A_ProInfos> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A_ProInfos.*,A_RoleName ");
            strSql.Append(" FROM A_ProInfos left join A_Role on A_Role.A_RoleId=A_ProInfos.a_Role_Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by a_Index ");
            List<VAN_OA.Model.EFrom.A_ProInfos> list = new List<VAN_OA.Model.EFrom.A_ProInfos>();
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
        public VAN_OA.Model.EFrom.A_ProInfos ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.A_ProInfos model = new VAN_OA.Model.EFrom.A_ProInfos();
            object ojb;
            ojb = dataReader["ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ids = (int)ojb;
            }
            ojb = dataReader["pro_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.pro_Id = (int)ojb;
            }
            ojb = dataReader["a_Role_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.a_Role_Id = (int)ojb;
            }
            ojb = dataReader["a_Index"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.a_Index = (int)ojb;
            }

            ojb = dataReader["A_RoleName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RoleName = (string)ojb;
            }

            
            return model;
        }

    }
}
