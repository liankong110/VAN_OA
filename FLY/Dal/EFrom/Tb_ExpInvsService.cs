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
    public class Tb_ExpInvsService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.Tb_ExpInvs model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.PId != null)
            {
                strSql1.Append("PId,");
                strSql2.Append("" + model.PId + ",");
            }
            if (model.InvId != null)
            {
                strSql1.Append("InvId,");
                strSql2.Append("" + model.InvId + ",");
            }
            if (model.ExpNum != null)
            {
                strSql1.Append("ExpNum,");
                strSql2.Append("" + model.ExpNum + ",");
            }
            if (model.ExpUse != null)
            {
                strSql1.Append("ExpUse,");
                strSql2.Append("'" + model.ExpUse + "',");
            }
            if (model.ExpState != null)
            {
                strSql1.Append("ExpState,");
                strSql2.Append("'" + model.ExpState + "',");
            }
            if (model.ExpRemark != null)
            {
                strSql1.Append("ExpRemark,");
                strSql2.Append("'" + model.ExpRemark + "',");
            }
            if (model.ReturnTime != null)
            {
                strSql1.Append("ReturnTime,");
                strSql2.Append("'" + model.ReturnTime + "',");
            }
            strSql.Append("insert into Tb_ExpInvs(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
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
        public void Update(VAN_OA.Model.EFrom.Tb_ExpInvs model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_ExpInvs set ");
            if (model.PId != null)
            {
                strSql.Append("PId=" + model.PId + ",");
            }
            if (model.InvId != null)
            {
                strSql.Append("InvId=" + model.InvId + ",");
            }
            if (model.ExpNum != null)
            {
                strSql.Append("ExpNum=" + model.ExpNum + ",");
            }
            if (model.ExpUse != null)
            {
                strSql.Append("ExpUse='" + model.ExpUse + "',");
            }
            else
            {
                strSql.Append("ExpUse= null ,");
            }
            if (model.ExpState != null)
            {
                strSql.Append("ExpState='" + model.ExpState + "',");
            }
            else
            {
                strSql.Append("ExpState= null ,");
            }
            if (model.ExpRemark != null)
            {
                strSql.Append("ExpRemark='" + model.ExpRemark + "',");
            }
            else
            {
                strSql.Append("ExpRemark= null ,");
            }
            if (model.ReturnTime != null)
            {
                strSql.Append("ReturnTime='" + model.ReturnTime + "',");
            }
            else
            {
                strSql.Append("ReturnTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ExpInvs ");
            strSql.Append(" where Id=" + Id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string Id, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ExpInvs ");
            strSql.Append(" where Id in(" + Id + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int pid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ExpInvs ");
            strSql.Append(" where pid=" + pid + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_ExpInvs GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_ExpInvs.Id,PId,InvId,ExpNum,ExpUse,ExpState,ExpRemark,ReturnTime,InvName");
            strSql.Append(" from Tb_ExpInvs left join Tb_Inventory on Tb_Inventory.Id=Tb_ExpInvs.InvId");
            strSql.Append(" where Tb_ExpInvs.Id=" + Ids + "");
            VAN_OA.Model.EFrom.Tb_ExpInvs model = null;
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
        public List<ExpInvInfoView> GetListExpInvInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  EventNo,loginName ,ExpTime,OutTime,InvName,ExpUse,ExpState,ReturnTime,ExpRemark
from Tb_ExpInvs left join Tb_ExpInv on  Tb_ExpInvs.PId=Tb_ExpInv.Id  left join Tb_Inventory on Tb_Inventory.Id=Tb_ExpInvs.InvId
 left join tb_User on tb_User.id=Tb_ExpInv.CreateUserId ");            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by EventNo desc ");
            List<ExpInvInfoView> list = new List<ExpInvInfoView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ExpInvInfoView model = new ExpInvInfoView();
                        object ojb;
                        model.EventNo = dataReader["EventNo"].ToString();
                        model.loginName = dataReader["loginName"].ToString();
                        ojb = dataReader["ExpTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpTime = (DateTime)ojb;
                        }
                        ojb = dataReader["OutTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OutTime = (DateTime)ojb;
                        }
                        model.InvName = dataReader["InvName"].ToString();
                        model.ExpUse = dataReader["ExpUse"].ToString();
                        model.ExpState = dataReader["ExpState"].ToString();
                        ojb = dataReader["ReturnTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ReturnTime = (DateTime)ojb;
                        }
                        model.ExpRemark = dataReader["ExpRemark"].ToString();

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.Tb_ExpInvs> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Tb_ExpInvs.Id,PId,InvId,ExpNum,ExpUse,ExpState,ExpRemark,ReturnTime,InvName,InvNo");
            strSql.Append(" from Tb_ExpInvs left join Tb_Inventory on Tb_Inventory.Id=Tb_ExpInvs.InvId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
               
            }
             
            List<VAN_OA.Model.EFrom.Tb_ExpInvs> list = new List<VAN_OA.Model.EFrom.Tb_ExpInvs>();

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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.Tb_ExpInvs> GroupByListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select InvNum , isnull(SumExpNum,0)as SumExpNum , InvNum-isnull(SumExpNum,0) as lastNum ,Tb_Inventory.Id from Tb_Inventory left join ");
            strSql.Append(" ( select InvId,sum(isnull(expNum,0))as SumExpNum from Tb_ExpInv_NoReurnInvView group BY InvId) ");
            strSql.Append("as newGroupExpInvNums on Tb_Inventory.Id=newGroupExpInvNums.InvId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);

            }

            List<VAN_OA.Model.EFrom.Tb_ExpInvs> list = new List<VAN_OA.Model.EFrom.Tb_ExpInvs>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Tb_ExpInvs model = new Tb_ExpInvs();
                        model.InvId = Convert.ToInt32(dataReader["Id"]);
                        model.InvName = string.Format("小计:总数 {0} 借出 {1} 剩余 {2}", dataReader["InvNum"] + "  ", dataReader["SumExpNum"] + "  ", dataReader["lastNum"] + "  ");
                        model.ExpNum = Convert.ToDecimal(dataReader["lastNum"]);
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep> GetListArray_NoReurnInvs(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();             

            strSql.Append("select Tb_ExpInv_NoReurnInvView.*,Tb_Inventory.InvName as baseInvName,InvNo,Tb_Inventory.Id as baseInvId from Tb_Inventory left join  Tb_ExpInv_NoReurnInvView");
            strSql.Append(" on Tb_Inventory.Id=Tb_ExpInv_NoReurnInvView.InvId");
            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Tb_Inventory.InvName");

            List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep> list = new List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep>();
            System.Collections.Hashtable HsInvS = new System.Collections.Hashtable();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep model = new VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep();

                      
                        object ojb;
                        ojb = dataReader["baseInvName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvName = Convert.ToString(ojb);
                        }
                        ojb = dataReader["ExpNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpNum = (decimal)ojb;
                        }

                        if (!HsInvS.Contains(model.InvName))
                        {
                            if (HsInvS.Keys.Count > 0)
                            {
                                foreach (string invName in HsInvS.Keys)
                                {
                                    //decimal num = Convert.ToDecimal(HsInvS[invName]);                                 

                                    VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep modelTotal = new VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep();
                                    modelTotal.InvId = list[list.Count-1].InvId;
                                    modelTotal.InvName = "小计";
                                    //modelTotal.ExpNum = num;
                                    list.Add(modelTotal);
                                }
                                HsInvS.Clear();
                            }
                            HsInvS.Add(model.InvName,"");
                        }
                        //else
                        //{
                        //    decimal num = 0;
                        //    string key = "";
                        //    foreach(string invName in HsInvS.Keys)
                        //    {
                        //        key = invName;
                        //       num=Convert.ToDecimal(HsInvS[invName]) + model.ExpNum;
                                
                        //    }
                        //    HsInvS[key] = num;
                        //}

                        
                       


                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["baseInvId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvId = (int)ojb;
                        }
                        ojb = dataReader["InvNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvNo = ojb.ToString();
                        }
                        
                        model.ExpUse = dataReader["ExpUse"].ToString();
                        model.ExpState = dataReader["ExpState"].ToString();
                        model.ExpRemark = dataReader["ExpRemark"].ToString();
                        ojb = dataReader["ReturnTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ReturnTime = (DateTime)ojb;
                        }

                      

                        model.ProNo = dataReader["ProNo"].ToString();

                        ojb = dataReader["ExpTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpTime = (DateTime)ojb;
                        }

                        ojb = dataReader["LoginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LoginName = ojb.ToString();
                        }

                        ojb = dataReader["ExpInvState"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpInvState = ojb.ToString();
                        }

                        

                        list.Add(model);
                    }
                }
            }

            if (list.Count > 0)
            {
                if (HsInvS.Keys.Count > 0)
                {
                    foreach (string invName in HsInvS.Keys)
                    {


                        VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep modelTotal = new VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep();
                        modelTotal.InvId = list[list.Count - 1].InvId;
                        modelTotal.InvName = "小计";
                       
                        list.Add(modelTotal);
                    }
                    HsInvS.Clear();
                }
                
            }
            return list;
        }






        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep> GetListArray_Histroy(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Tb_ExpInv_View.* from Tb_ExpInv_View ");
         

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Tb_ExpInv_View.InvName");

            List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep> list = new List<VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep>();
           
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep model = new VAN_OA.Model.ReportForms.Tb_ExpInvsSumRep();


                        object ojb;
                        ojb = dataReader["InvName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvName = Convert.ToString(ojb);
                        }
                        ojb = dataReader["ExpNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpNum = (decimal)ojb;
                        }

                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["InvId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvId = (int)ojb;
                        }
                        ojb = dataReader["InvNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvNo = ojb.ToString();
                        }

                        model.ExpUse = dataReader["ExpUse"].ToString();
                        model.ExpState = dataReader["ExpState"].ToString();
                        model.ExpRemark = dataReader["ExpRemark"].ToString();
                        ojb = dataReader["ReturnTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ReturnTime = (DateTime)ojb;
                        }



                        model.ProNo = dataReader["ProNo"].ToString();
                        model.ExpRemark = dataReader["ExpRemark"].ToString();

                        ojb = dataReader["ExpTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpTime = (DateTime)ojb;
                        }

                        ojb = dataReader["LoginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LoginName = ojb.ToString();
                        }

                        ojb = dataReader["ExpInvState"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ExpInvState = ojb.ToString();
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
        public VAN_OA.Model.EFrom.Tb_ExpInvs ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.Tb_ExpInvs model = new VAN_OA.Model.EFrom.Tb_ExpInvs();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["PId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PId = (int)ojb;
            }
            ojb = dataReader["InvId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvId = (int)ojb;
            }
            ojb = dataReader["ExpNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ExpNum = (decimal)ojb;
            }
            model.ExpUse = dataReader["ExpUse"].ToString();
            model.ExpState = dataReader["ExpState"].ToString();
            model.ExpRemark = dataReader["ExpRemark"].ToString();
            ojb = dataReader["ReturnTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ReturnTime = (DateTime)ojb;
            }

            ojb = dataReader["InvName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvName = Convert.ToString(ojb);
            }


            ojb = dataReader["InvNo"];
            if (ojb != null && ojb != DBNull.Value&&ojb.ToString()!="")
            {
                model.InvName += "---"+ojb.ToString();
            }
            return model;
        }



        

    }
}
