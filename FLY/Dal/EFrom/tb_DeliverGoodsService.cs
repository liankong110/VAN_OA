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
    public class tb_DeliverGoodsService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_DeliverGoods model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_DeliverGoods model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("tb_DeliverGoods", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;
                    


                    objCommand.Parameters.Clear();
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
        public int Add(VAN_OA.Model.EFrom.tb_DeliverGoods model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DepartName != null)
            {
                strSql1.Append("DepartName,");
                strSql2.Append("'" + model.DepartName + "',");
            }
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.dateTime != null)
            {
                strSql1.Append("dateTime,");
                strSql2.Append("'" + model.dateTime + "',");
            }
            if (model.SongHuo != null)
            {
                strSql1.Append("SongHuo,");
                strSql2.Append("'" + model.SongHuo + "',");
            }
            if (model.CompName != null)
            {
                strSql1.Append("CompName,");
                strSql2.Append("'" + model.CompName + "',");
            }
            if (model.GoTime != null)
            {
                strSql1.Append("GoTime,");
                strSql2.Append("'" + model.GoTime + "',");
            }
            if (model.BackTime != null)
            {
                strSql1.Append("BackTime,");
                strSql2.Append("'" + model.BackTime + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }


            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
 

            strSql.Append("insert into tb_DeliverGoods(");
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
        public void Update(VAN_OA.Model.EFrom.tb_DeliverGoods model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_DeliverGoods set ");
            if (model.DepartName != null)
            {
                strSql.Append("DepartName='" + model.DepartName + "',");
            }
            else
            {
                strSql.Append("DepartName= null ,");
            }
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            if (model.dateTime != null)
            {
                strSql.Append("dateTime='" + model.dateTime + "',");
            }
            if (model.SongHuo != null)
            {
                strSql.Append("SongHuo='" + model.SongHuo + "',");
            }
            else
            {
                strSql.Append("SongHuo= null ,");
            }
            if (model.CompName != null)
            {
                strSql.Append("CompName='" + model.CompName + "',");
            }
            if (model.GoTime != null)
            {
                strSql.Append("GoTime='" + model.GoTime + "',");
            }
            else
            {
                strSql.Append("GoTime= null ,");
            }
            if (model.BackTime != null)
            {
                strSql.Append("BackTime='" + model.BackTime + "',");
            }
            else
            {
                strSql.Append("BackTime= null ,");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            else
            {
                strSql.Append("InvName= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_DeliverGoods model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_DeliverGoods set ");
            if (model.DepartName != null)
            {
                strSql.Append("DepartName='" + model.DepartName + "',");
            }
            else
            {
                strSql.Append("DepartName= null ,");
            }
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            if (model.dateTime != null)
            {
                strSql.Append("dateTime='" + model.dateTime + "',");
            }
            if (model.SongHuo != null)
            {
                strSql.Append("SongHuo='" + model.SongHuo + "',");
            }
            else
            {
                strSql.Append("SongHuo= null ,");
            }
            if (model.CompName != null)
            {
                strSql.Append("CompName='" + model.CompName + "',");
            }
            if (model.GoTime != null)
            {
                strSql.Append("GoTime='" + model.GoTime + "',");
            }
            else
            {
                strSql.Append("GoTime= null ,");
            }
            if (model.BackTime != null)
            {
                strSql.Append("BackTime='" + model.BackTime + "',");
            }
            else
            {
                strSql.Append("BackTime= null ,");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            else
            {
                strSql.Append("InvName= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_DeliverGoods ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_DeliverGoods GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" id,DepartName,Name,dateTime,SongHuo,CompName,GoTime,BackTime,InvName,Address,proNo ");
            strSql.Append(" from tb_DeliverGoods ");
            strSql.Append(" where id=" + id + "");

            VAN_OA.Model.EFrom.tb_DeliverGoods model = null;
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
        public List<VAN_OA.Model.EFrom.tb_DeliverGoods> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,DepartName,Name,dateTime,SongHuo,CompName,GoTime,BackTime,InvName,Address,proNo ");
            strSql.Append(" FROM tb_DeliverGoods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_DeliverGoods> list = new List<VAN_OA.Model.EFrom.tb_DeliverGoods>();

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
		public VAN_OA.Model.EFrom.tb_DeliverGoods ReaderBind(IDataReader dataReader)
		{
			VAN_OA.Model.EFrom.tb_DeliverGoods model=new VAN_OA.Model.EFrom.tb_DeliverGoods();
			object ojb; 
			ojb = dataReader["id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.id=(int)ojb;
			}
			model.DepartName=dataReader["DepartName"].ToString();
			model.Name=dataReader["Name"].ToString();
			ojb = dataReader["dateTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.dateTime=(DateTime)ojb;
			}
			model.SongHuo=dataReader["SongHuo"].ToString();
			model.CompName=dataReader["CompName"].ToString();
			ojb = dataReader["GoTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.GoTime=(DateTime)ojb;
			}
			ojb = dataReader["BackTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BackTime=(DateTime)ojb;
			}
			model.InvName=dataReader["InvName"].ToString();
			model.Address=dataReader["Address"].ToString();

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

			return model;
		}


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.DeliverGoodsRep> GetListArrayReport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  select * from tb_DeliverGoods");
        
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by dateTime desc");
            List<VAN_OA.Model.ReportForms.DeliverGoodsRep> list = new List<VAN_OA.Model.ReportForms.DeliverGoodsRep>();

            int result = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        object ojb;
                        VAN_OA.Model.ReportForms.DeliverGoodsRep model = new VAN_OA.Model.ReportForms.DeliverGoodsRep();
                        if (list.Count == 0)
                        {
                            result = 0;
                        }
                        else
                        {   ojb = dataReader["dateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                                Model.ReportForms.DeliverGoodsRep rep = list[list.Count - 1];
                                if (rep.Desc.Contains('-') && rep.Desc != model.Desc)
                                {
                                    result = 0;
                                }
                                else
                                {
                                    result = 1;
                                }
                            }
                           
                        }
                       


                       
                        if (result != 0)
                        {
                            

                            model.Address = dataReader["Address"].ToString();                          
                            
                            ojb = dataReader["backTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            ojb = dataReader["GoTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            model.InvName = dataReader["InvName"].ToString();
                            model.SouhuoName = dataReader["SongHuo"].ToString();
                            model.Waipairen = dataReader["Name"].ToString();

                              ojb = dataReader["dateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                            }

                        }
                        else
                        {

                            VAN_OA.Model.ReportForms.DeliverGoodsRep model1 = new VAN_OA.Model.ReportForms.DeliverGoodsRep();

                            model1.Address = "送货地址";
                            model1.BackTime = "回来时间";
                            model1.GoTime = "外出时间";
                            model1.InvName = "货物名称";
                            model1.Mouth = "送货地址";
                            model1.SouhuoName = "送货人";
                            model1.Waipairen = "外派人";
                            ojb = dataReader["dateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model1.Mouth = dateTime.Year + "-" + dateTime.Month;
                            }

                            list.Add(model1);
                            model.Address = dataReader["Address"].ToString();

                            ojb = dataReader["backTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            ojb = dataReader["GoTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            model.InvName = dataReader["InvName"].ToString();
                            model.SouhuoName = dataReader["SongHuo"].ToString();
                            model.Waipairen = dataReader["Name"].ToString();
                            ojb = dataReader["dateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();

                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                            }
                        }

                       
                        list.Add(model);
                    }
                }
            }
            return list;
        }


    }
}
