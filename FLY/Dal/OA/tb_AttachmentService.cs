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
    public class tb_AttachmentService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(tb_Attachment model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Folder_Id != null)
            {
                strSql1.Append("Folder_Id,");
                strSql2.Append("" + model.Folder_Id + ",");
            }
            if (model.fileName != null)
            {
                strSql1.Append("fileName,");
                strSql2.Append("'" + model.fileName + "',");
            }
            if (model.fileNo != null)
            {
                strSql1.Append("fileNo,");
                strSql2.Append("@fileNo,");

                SqlParameter pp = new System.Data.SqlClient.SqlParameter("@fileNo", System.Data.SqlDbType.Image, model.fileNo.Length);
                pp.Value = model.fileNo;
                objCommand.Parameters.Add(pp);
            }
            if (model.FileType != null)
            {
                strSql1.Append("FileType,");
                strSql2.Append("'" + model.FileType + "',");
            }

            if (model.createTime != null)
            {
                strSql1.Append("createTime,");
                strSql2.Append("'" + model.createTime + "',");
            }
            if (model.userName != null)
            {
                strSql1.Append("userName,");
                strSql2.Append("'" + model.userName + "',");
            }
            if (model.version != null)
            {
                strSql1.Append("version,");
                strSql2.Append("'" + model.version + "',");
            }

            strSql1.Append("Remark,");
            strSql2.Append("'" + model.Remark + "',");

            strSql1.Append("MainName,");
            strSql2.Append("'" + model.MainName + "',");

            strSql.Append("insert into tb_Attachment(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
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
        public void Update(tb_Attachment model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Attachment set ");
            if (model.Folder_Id != null)
            {
                strSql.Append("Folder_Id=" + model.Folder_Id + ",");
            }
            if (model.fileName != null)
            {
                strSql.Append("fileName='" + model.fileName + "',");
            }
            if (model.fileNo != null)
            {
                strSql.Append("fileNo=@fileNo,");

               

                    SqlParameter pp = new System.Data.SqlClient.SqlParameter("@fileNo", System.Data.SqlDbType.Image, model.fileNo.Length);
                    pp.Value = model.fileNo;
                    objCommand.Parameters.Add(pp);
                
            }
            if (model.createTime != null)
            {
                strSql.Append("createTime='" + model.createTime + "',");
            }
            if (model.userName != null)
            {
                strSql.Append("userName='" + model.userName + "',");
            }
            if (model.version != null)
            {
                strSql.Append("version='" + model.version + "',");
            }

            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }

            if (model.MainName != null)
            {
                strSql.Append("MainName='" + model.MainName + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Attachment ");
            strSql.Append(" where id in(" + ids + ")");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<tb_Attachment> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Folder_NAME,MainName,id,tb_Attachment.folder_Id,fileName,fileType,createTime,userName,version,Remark ");
            strSql.Append(" FROM tb_Attachment left join tb_Folder on tb_Folder.Folder_ID=tb_Attachment.folder_Id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by id desc" );
            List<tb_Attachment> list = new List<tb_Attachment>();

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
        /// treeview
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<tb_Attachment> GetListArray_TV(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MainName,id,tb_Attachment.folder_Id, ParentId,createTime");
            strSql.Append(" FROM tb_Attachment left join tb_Folder on tb_Attachment.folder_Id=tb_Folder.folder_Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }

            strSql.Append(" order by  MainName,createTime desc");
            List<tb_Attachment> list = new List<tb_Attachment>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {

                        tb_Attachment model = new tb_Attachment();
                        object ojb;
                        ojb = objReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = objReader["Folder_Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Folder_Id = (int)ojb;
                        }

                        ojb = objReader["ParentId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Parentd = ojb.ToString();
                        }

                        ojb = objReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime =Convert.ToDateTime( ojb);
                        }
                        
                        model.MainName = objReader["MainName"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 下载
        /// </summary>
        public tb_Attachment GetListArrayByParentId_Down(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Folder_NAME,MainName,id,tb_Attachment.folder_Id,fileName,fileType,createTime,userName,version,Remark ");
            strSql.Append(" FROM tb_Attachment left join tb_Folder on tb_Folder.Folder_ID=tb_Attachment.folder_Id ");

            strSql.Append(" where id=" + id);

            tb_Attachment model = new tb_Attachment();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                       
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["folder_Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Folder_Id = (int)ojb;
                        }


                        model.fileName = dataReader["fileName"].ToString();
                        ojb = dataReader["FileType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FileType = ojb.ToString();
                        }
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        model.userName = dataReader["userName"].ToString();
                        model.version = dataReader["version"].ToString();
                        model.Remark = dataReader["Remark"].ToString();
                        model.MainName = dataReader["MainName"].ToString();
                        ojb = dataReader["Folder_NAME"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FolderName = ojb.ToString();
                        }
                        //ojb = dataReader["fileNo"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.fileNo = (byte[])ojb;
                        //}
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 预览
        /// </summary>
        public tb_Attachment GetListArrayByParentId_Pre(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MainName,id,tb_Attachment.folder_Id,fileNo,fileName,fileType,createTime,userName,version,Remark ");
            strSql.Append(" FROM tb_Attachment  ");

            strSql.Append(" where id=" + id);

            tb_Attachment model = new tb_Attachment();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["folder_Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Folder_Id = (int)ojb;
                        }


                        model.fileName = dataReader["fileName"].ToString();
                        ojb = dataReader["FileType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FileType = ojb.ToString();
                        }
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        model.userName = dataReader["userName"].ToString();
                        model.version = dataReader["version"].ToString();
                        model.Remark = dataReader["Remark"].ToString();
                        model.MainName = dataReader["MainName"].ToString();
                        //ojb = dataReader["Folder_NAME"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.FolderName = ojb.ToString();
                        //}
                        ojb = dataReader["fileNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fileNo = (byte[])ojb;
                        }
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public tb_Attachment GetListArrayByParentId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Folder_NAME,MainName,id,tb_Attachment.folder_Id,fileName,fileType,createTime,userName,version,Remark ");
            strSql.Append(" FROM tb_Attachment left join tb_Folder on tb_Folder.Folder_ID=tb_Attachment.folder_Id ");
            strSql.Append(" where id=" + id);

            List<tb_Attachment> list = new List<tb_Attachment>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind_1(objReader));
                    }
                }
            }
            return list[0];
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public tb_Attachment ReaderBind(IDataReader dataReader)
        {
            tb_Attachment model = new tb_Attachment();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["Folder_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Folder_Id = (int)ojb;
            }
            model.fileName = dataReader["fileName"].ToString();
            //ojb = dataReader["fileNo"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.fileNo = (byte[])ojb;
            //}

            ojb = dataReader["FileType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FileType = ojb.ToString();
            }
            ojb = dataReader["createTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.createTime = (DateTime)ojb;
            }
            model.userName = dataReader["userName"].ToString();
            model.version = dataReader["version"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.MainName = dataReader["MainName"].ToString();
            ojb = dataReader["Folder_NAME"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FolderName = ojb.ToString();
            }
            return model;
        }

        /// 对象实体绑定数据
        /// </summary>
        public tb_Attachment ReaderBind_1(IDataReader dataReader)
        {
            tb_Attachment model = new tb_Attachment();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["folder_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Folder_Id = (int)ojb;
            }


            model.fileName = dataReader["fileName"].ToString();
            ojb = dataReader["FileType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FileType = ojb.ToString();
            }
            ojb = dataReader["createTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.createTime = (DateTime)ojb;
            }
            model.userName = dataReader["userName"].ToString();
            model.version = dataReader["version"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.MainName = dataReader["MainName"].ToString();
             ojb = dataReader["Folder_NAME"];
             if (ojb != null && ojb != DBNull.Value)
             {
                 model.FolderName = ojb.ToString();
             }
            return model;
        }




    }
}
