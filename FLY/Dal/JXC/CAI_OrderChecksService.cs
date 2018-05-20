using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace VAN_OA.Dal.JXC
{
    public class CAI_OrderChecksService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CAI_OrderChecks model, SqlCommand objCommand)
        {


            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CheckId != null)
            {
                strSql1.Append("CheckId,");
                strSql2.Append("" + model.CheckId + ",");
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

            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }

            if (model.CaiProNo != null)
            {
                strSql1.Append("CaiProNo,");
                strSql2.Append("'" + model.CaiProNo + "',");
            }


            if (model.SupplierName != null)
            {
                strSql1.Append("SupplierName,");
                strSql2.Append("'" + model.SupplierName + "',");
            }
            if (model.CheckGoodId != null)
            {
                strSql1.Append("CheckGoodId,");
                strSql2.Append("" + model.CheckGoodId + ",");
            }
            if (model.CheckNum != null)
            {
                strSql1.Append("CheckNum,");
                strSql2.Append("" + model.CheckNum + ",");
            }
            if (model.CheckPrice != null)
            {
                strSql1.Append("CheckPrice,");
                strSql2.Append("" + model.CheckPrice + ",");
            }


            
            if (model.CaiId != null)
            {
                strSql1.Append("CaiId,");
                strSql2.Append("" + model.CaiId + ",");
            }

            if (model.QingGou != null)
            {
                strSql1.Append("QingGou,");
                strSql2.Append("'" + model.QingGou + "',");
            }

            if (model.CaiGouPer != null)
            {
                strSql1.Append("CaiGouPer,");
                strSql2.Append("'" + model.CaiGouPer + "',");
            }
            strSql1.Append("CheckLastTruePrice,");
            strSql2.Append("" + model.CheckLastTruePrice + ",");

            strSql.Append("insert into CAI_OrderChecks(");
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
        public void Update(VAN_OA.Model.JXC.CAI_OrderChecks model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_OrderChecks set ");
            //if (model.CheckId != null)
            //{
            //    strSql.Append("CheckId=" + model.CheckId + ",");
            //}
            //if (model.PONo != null)
            //{
            //    strSql.Append("PONo='" + model.PONo + "',");
            //}
            //if (model.SupplierName != null)
            //{
            //    strSql.Append("SupplierName='" + model.SupplierName + "',");
            //}
            //if (model.CheckGoodId != null)
            //{
            //    strSql.Append("CheckGoodId=" + model.CheckGoodId + ",");
            //}
            if (model.CheckNum != null)
            {
                strSql.Append("CheckNum=" + model.CheckNum + ",");
            }
            //if (model.CheckPrice != null)
            //{
            //    strSql.Append("CheckPrice=" + model.CheckPrice + ",");
            //}
            
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Ids=" + model.Ids + "");

            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderChecks ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderChecks ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderChecks ");
            strSql.Append(" where CheckId=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderChecks GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CheckLastTruePrice,Ids,CheckId,CAI_OrderChecks.PONo,SupplierName,CheckGoodId,CheckNum,CheckPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,POName,GUESTName,CaiId,CaiProNo,QingGou,CaiGouPer");
            strSql.Append(" from CAI_OrderChecks  left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId  ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CAI_OrderChecks model = null;
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
        /// 获得 主表 子表数据
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderChecks> GetAllListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();          
            strSql.Append("select   ");
            strSql.Append(" CheckTime,CAI_OrderCheck.CreateTime,CheckRemark ,tb_User.loginName as CreateName,CheckUser.loginName as CheckUserName,ProNo,Status,Ids,CheckId,CAI_OrderChecks.PONo,SupplierName,CheckGoodId,CheckNum,CheckPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,POName,GUESTName,CaiId,CaiProNo,QingGou,CaiGouPer");
            strSql.Append(" from CAI_OrderCheck left join CAI_OrderChecks on CAI_OrderCheck.Id= CAI_OrderChecks.CheckId left join tb_User on tb_User.id=CreatePer left join tb_User as CheckUser on CheckUser.id=CheckPer  left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId  ");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderChecks> list = new List<VAN_OA.Model.JXC.CAI_OrderChecks>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.JXC.CAI_OrderChecks model = new VAN_OA.Model.JXC.CAI_OrderChecks();
                        object ojb;

                         
                        
                        ojb = dataReader["CheckTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckTime = (DateTime)ojb;
                        }
                        
                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        model.CheckRemark = dataReader["CheckRemark"].ToString();
                        ojb = dataReader["CreateName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateName = ojb.ToString();
                        }

                        ojb = dataReader["CheckUserName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckUserName = ojb.ToString();
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


                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = (int)ojb;
                        }
                        ojb = dataReader["CheckId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckId = (int)ojb;
                        }
                        model.PONo = dataReader["PONo"].ToString();
                        model.SupplierName = dataReader["SupplierName"].ToString();
                        ojb = dataReader["CheckGoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckGoodId = (int)ojb;
                        }
                        ojb = dataReader["CheckNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckNum = (decimal)ojb;
                        }
                        ojb = dataReader["CheckPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckPrice = (decimal)ojb;
                        }


                        model.Total = model.CheckNum * model.CheckPrice;



                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }

                        ojb = dataReader["GUESTName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }

                        ojb = dataReader["CaiId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["CaiProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiProNo = ojb.ToString();
                        }

                        ojb = dataReader["QingGou"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.QingGou = ojb.ToString();
                        }
                        ojb = dataReader["CaiGouPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiGouPer = ojb.ToString();
                        }

                        
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderChecks> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
          
            strSql.Append("select   ");
            strSql.Append(" GoodAreaNumber,CheckLastTruePrice,Ids,CheckId,CAI_OrderChecks.PONo,SupplierName,CheckGoodId,CheckNum,CheckPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,POName,GUESTName,CaiId,CaiProNo,QingGou,CaiGouPer");
            strSql.Append(" from CAI_OrderChecks  left join TB_Good on TB_Good.GoodId=CAI_OrderChecks.CheckGoodId   ");
             

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderChecks> list = new List<VAN_OA.Model.JXC.CAI_OrderChecks>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 查询所有未入库的检验单List
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderChecks> GetListArrayPOOrderChecks_Cai_POOrderInHouse_ListView(string strWhere)
        {          

            StringBuilder strSql = new StringBuilder();
             
            strSql.Append("select   ");
            strSql.Append(" Ids,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,CheckNum,CheckPrice,totalOrderNum,CheckGoodId,QingGou");
            strSql.Append(" from Cai_POOrderChecks_Cai_POOrderInHouse_ListView");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderChecks> list = new List<VAN_OA.Model.JXC.CAI_OrderChecks>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CAI_OrderChecks model = new VAN_OA.Model.JXC.CAI_OrderChecks();
                        object ojb;


                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["CheckNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckNum = Convert.ToInt32(ojb);
                        }


                        ojb = dataReader["CheckPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckPrice = Convert.ToDecimal(ojb);
                        }


                        ojb = dataReader["totalOrderNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckNum = model.CheckNum-Convert.ToInt32(ojb);
                        }

                        model.Total = model.CheckNum * model.CheckPrice;


                        ojb = dataReader["CheckGoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckGoodId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                        ojb = dataReader["QingGou"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.QingGou = ojb.ToString();
                        }

                        
                        list.Add(model);

                       
                    }
                }
            }
            return list;
        }


        /// <summary>
        ///查询所有未入库的检验单
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderChecks> GetListArrayCai_POOrderChecks_Cai_POOrderInHouse_View(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();          

            strSql.Append("select   ");
            strSql.Append(" ProNo, PONo,POName,GUESTName,SupplierName,CheckTime,CaiGouPer");
            strSql.Append(" from Cai_POOrderChecks_Cai_POOrderInHouse_View");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderChecks> list = new List<VAN_OA.Model.JXC.CAI_OrderChecks>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.JXC.CAI_OrderChecks model = new VAN_OA.Model.JXC.CAI_OrderChecks();
                        object ojb;
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiProNo = ojb.ToString();
                        } 
                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }
                        ojb = dataReader["GUESTName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }
                        model.PONo = dataReader["PONo"].ToString();
                        model.SupplierName = dataReader["SupplierName"].ToString();

                        ojb = dataReader["CheckTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckTime = Convert.ToDateTime(ojb);
                        }

                        ojb = dataReader["CaiGouPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiGouPer = Convert.ToString(ojb);
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
        public VAN_OA.Model.JXC.CAI_OrderChecks ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderChecks model = new VAN_OA.Model.JXC.CAI_OrderChecks();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["CheckId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckId = (int)ojb;
            }
            model.PONo = dataReader["PONo"].ToString();
            model.SupplierName = dataReader["SupplierName"].ToString();
            ojb = dataReader["CheckGoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckGoodId = (int)ojb;
            }
            ojb = dataReader["CheckNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckNum = (decimal)ojb;
            }
            ojb = dataReader["CheckPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckPrice = (decimal)ojb;
            }
            ojb = dataReader["CheckLastTruePrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckLastTruePrice = (decimal)ojb;
            }

            model.Total = model.CheckNum * model.CheckPrice;

            

            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo = ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }

            ojb = dataReader["GUESTName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestName = ojb.ToString();
            }

            ojb = dataReader["CaiId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiId =Convert.ToInt32(ojb);
            }

            ojb = dataReader["CaiProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiProNo = ojb.ToString();
            }

            ojb = dataReader["QingGou"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QingGou = ojb.ToString();
            }

            ojb = dataReader["CaiGouPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiGouPer = Convert.ToString(ojb);
            }

            
            return model;
        }



    }
}
