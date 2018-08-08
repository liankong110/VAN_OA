using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.Dal.BaseInfo
{
    public class TB_GoodService
    {
        public bool updateTran(TB_Good model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(TB_Good model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("TB_Good", "GoodProNo", objCommand);
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
        public int Add(VAN_OA.Model.BaseInfo.TB_Good model)
        {

            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(GoodNo),5))+1))),5) FROM  TB_Good");
            string MaxNo = "";
            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxNo = objMax.ToString();
            }
            else
            {
                MaxNo = "10001";
            }

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            
            strSql1.Append("GoodNo,");
            strSql2.Append("'" + MaxNo + "',");
            
            if (model.GoodName != null)
            {
                strSql1.Append("GoodName,");
                strSql2.Append("'" + model.GoodName + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql1.Append("ZhuJi,");
                strSql2.Append("'" + model.ZhuJi + "',");
            }
            if (model.GoodTypeName != null)
            {
                strSql1.Append("GoodTypeName,");
                strSql2.Append("'" + model.GoodTypeName + "',");
            }
            if (model.GoodTypeSmName!= null)
            {
                strSql1.Append("GoodTypeSmName,");
                strSql2.Append("'" + model.GoodTypeSmName + "',");
            }
            if (model.GoodSpec != null)
            {
                strSql1.Append("GoodSpec,");
                strSql2.Append("'" + model.GoodSpec + "',");
            }
            if (model.GoodModel != null)
            {
                strSql1.Append("GoodModel,");
                strSql2.Append("'" + model.GoodModel + "',");
            }
            if (model.GoodUnit != null)
            {
                strSql1.Append("GoodUnit,");
                strSql2.Append("'" + model.GoodUnit + "',");
            }
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("getdate(),");
            }

            if (model.IfSpec != null)
            {
                strSql1.Append("IfSpec,");
                strSql2.Append("" + (model.IfSpec ? 1 : 0) + ",");
            }
            if (model.GoodBrand != null)
            {
                strSql1.Append("GoodBrand,");
                strSql2.Append("'" + model.GoodBrand + "',");
            }
            if (model.Product != null)
            {
                strSql1.Append("Product,");
                strSql2.Append("'" + model.Product + "',");
            }
            if (model.GoodCol != null)
            {
                strSql1.Append("GoodCol,");
                strSql2.Append("'" + model.GoodCol + "',");
            }
            if (model.GoodRow != null)
            {
                strSql1.Append("GoodRow,");
                strSql2.Append("'" + model.GoodRow + "',");
            }
            if (model.GoodNumber != null)
            {
                strSql1.Append("GoodNumber,");
                strSql2.Append("'" + model.GoodNumber + "',");
            }
            if (model.GoodArea != null)
            {
                strSql1.Append("GoodArea,");
                strSql2.Append("'" + model.GoodArea + "',");
            }
            if (model.GoodAreaNumber != null)
            {
                strSql1.Append("GoodAreaNumber,");
                strSql2.Append("'" + model.GoodAreaNumber + "',");
            }
            strSql.Append("insert into TB_Good(");
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
        public int Add(VAN_OA.Model.BaseInfo.TB_Good model, SqlCommand objCommand)
        {

            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(GoodNo),5))+1))),5) FROM  TB_Good");
            string MaxNo = "";
            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxNo = objMax.ToString();
            }
            else
            {
                MaxNo = "10001";
            }

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            strSql1.Append("GoodNo,");
            strSql2.Append("'" + MaxNo + "',");

            if (model.GoodName != null)
            {
                strSql1.Append("GoodName,");
                strSql2.Append("'" + model.GoodName + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql1.Append("ZhuJi,");
                strSql2.Append("'" + model.ZhuJi + "',");
            }
            if (model.GoodTypeName != null)
            {
                strSql1.Append("GoodTypeName,");
                strSql2.Append("'" + model.GoodTypeName + "',");
            }
            if (model.GoodTypeSmName != null)
            {
                strSql1.Append("GoodTypeSmName,");
                strSql2.Append("'" + model.GoodTypeSmName + "',");
            }
            if (model.GoodSpec != null)
            {
                strSql1.Append("GoodSpec,");
                strSql2.Append("'" + model.GoodSpec + "',");
            }
            if (model.GoodModel != null)
            {
                strSql1.Append("GoodModel,");
                strSql2.Append("'" + model.GoodModel + "',");
            }
            if (model.GoodUnit != null)
            {
                strSql1.Append("GoodUnit,");
                strSql2.Append("'" + model.GoodUnit + "',");
            }
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("getdate(),");
            }

            if (model.IfSpec != null)
            {
                strSql1.Append("IfSpec,");
                strSql2.Append("" + (model.IfSpec ? 1 : 0) + ",");
            }
            if (model.GoodBrand != null)
            {
                strSql1.Append("GoodBrand,");
                strSql2.Append("'" + model.GoodBrand + "',");
            }
            strSql1.Append("GoodProNo,");
            strSql2.Append("'" + model.ProNo + "',");

            if (model.Status != null)
            {
                strSql1.Append("GoodStatus,");
                strSql2.Append("'" + model.Status + "',");
            }
            if (model.Product != null)
            {
                strSql1.Append("Product,");
                strSql2.Append("'" + model.Product + "',");
            }
            if (model.GoodCol != null)
            {
                strSql1.Append("GoodCol,");
                strSql2.Append("'" + model.GoodCol + "',");
            }
            if (model.GoodRow != null)
            {
                strSql1.Append("GoodRow,");
                strSql2.Append("'" + model.GoodRow + "',");
            }
            if (model.GoodNumber != null)
            {
                strSql1.Append("GoodNumber,");
                strSql2.Append("'" + model.GoodNumber + "',");
            }
            if (model.GoodArea != null)
            {
                strSql1.Append("GoodArea,");
                strSql2.Append("'" + model.GoodArea + "',");
            }
            if (model.GoodAreaNumber != null)
            {
                strSql1.Append("GoodAreaNumber,");
                strSql2.Append("'" + model.GoodAreaNumber + "',");
            }
            strSql.Append("insert into TB_Good(");
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

        public int GetGoodId(string goodName, string spec, string model, string GoodTypeSmName, string GoodUnit)
        {
            int goodId = 0;

            string sql = string.Format("select GoodId from TB_Good where GoodName='{0}' and GoodSpec='{1}' and GoodModel='{2}' and GoodTypeSmName='{3}' and GoodUnit='{4}'",
                goodName, spec, model, GoodTypeSmName, GoodUnit);

            object obj = DBHelp.ExeScalar(sql);
            if (obj != null && obj.ToString() != "")
            {
                goodId = Convert.ToInt32(obj);
            }
            return goodId;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.BaseInfo.TB_Good model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Good set ");
           
            if (model.GoodName != null)
            {
                strSql.Append("GoodName='" + model.GoodName + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql.Append("ZhuJi='" + model.ZhuJi + "',");
            }
            else
            {
                strSql.Append("ZhuJi= null ,");
            }
            if (model.GoodTypeName != null)
            {
                strSql.Append("GoodTypeName='" + model.GoodTypeName + "',");
            }
            else
            {
                strSql.Append("GoodTypeId= null ,");
            }

                if (model.GoodTypeSmName != null)
                {
                    strSql.Append("GoodTypeSmName='" + model.GoodTypeSmName + "',");
                }
                else
                {
                    strSql.Append("GoodTypeSmName= null ,");
                }
                if (model.GoodSpec != null)
                {
                    strSql.Append("GoodSpec='" + model.GoodSpec + "',");
                }
                else
                {
                    strSql.Append("GoodSpec= null ,");
                }
                if (model.GoodModel != null)
                {
                    strSql.Append("GoodModel='" + model.GoodModel + "',");
                }
                else
                {
                    strSql.Append("GoodModel= null ,");
                }
                if (model.GoodUnit != null)
                {
                    strSql.Append("GoodUnit='" + model.GoodUnit + "',");
                }
                else
                {
                    strSql.Append("GoodUnit= null ,");
                }
                if (model.CreateUserId != null)
                {
                    strSql.Append("CreateUserId=" + model.CreateUserId + ",");
                }
                if (model.CreateTime != null)
                {
                    strSql.Append("CreateTime=getdate(),");
                }
                if (model.IfSpec != null)
                {
                    strSql.Append("IfSpec=" + (model.IfSpec ? 1 : 0) + ",");
                }
                if (model.GoodBrand != null)
                {
                    strSql.Append("GoodBrand='" + model.GoodBrand + "',");
                }
                if (model.Product != null)
                {
                    strSql.Append("Product='" + model.Product + "',");
                }

                if (model.GoodAreaNumber != null)
                {
                    strSql.Append("GoodAreaNumber='" + model.GoodAreaNumber + "',");
                }
                if (model.GoodArea != null)
                {
                    strSql.Append("GoodArea='" + model.GoodArea + "',");
                }
                if (model.GoodNumber != null)
                {
                    strSql.Append("GoodNumber='" + model.GoodNumber + "',");
                }
                if (model.GoodRow != null)
                {
                    strSql.Append("GoodRow='" + model.GoodRow + "',");
                }
                if (model.GoodCol != null)
                {
                    strSql.Append("GoodCol='" + model.GoodCol + "',");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where GoodId=" + model.GoodId + "");
                bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
                return rowsAffected;
             
        }
        public bool Update(VAN_OA.Model.BaseInfo.TB_Good model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Good set ");

            if (model.GoodName != null)
            {
                strSql.Append("GoodName='" + model.GoodName + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql.Append("ZhuJi='" + model.ZhuJi + "',");
            }
            else
            {
                strSql.Append("ZhuJi= null ,");
            }
            if (model.GoodTypeName != null)
            {
                strSql.Append("GoodTypeName='" + model.GoodTypeName + "',");
            }
            else
            {
                strSql.Append("GoodTypeId= null ,");
            }

            if (model.GoodTypeSmName != null)
            {
                strSql.Append("GoodTypeSmName='" + model.GoodTypeSmName + "',");
            }
            else
            {
                strSql.Append("GoodTypeSmName= null ,");
            }
            if (model.GoodSpec != null)
            {
                strSql.Append("GoodSpec='" + model.GoodSpec + "',");
            }
            else
            {
                strSql.Append("GoodSpec= null ,");
            }
            if (model.GoodModel != null)
            {
                strSql.Append("GoodModel='" + model.GoodModel + "',");
            }
            else
            {
                strSql.Append("GoodModel= null ,");
            }
            if (model.GoodUnit != null)
            {
                strSql.Append("GoodUnit='" + model.GoodUnit + "',");
            }
            else
            {
                strSql.Append("GoodUnit= null ,");
            }
            if (model.CreateUserId != null)
            {
                strSql.Append("CreateUserId=" + model.CreateUserId + ",");
            }
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime=getdate(),");
            }
            if (model.IfSpec != null)
            {
                strSql.Append("IfSpec=" + (model.IfSpec ? 1 : 0) + ",");
            }
            if (model.GoodBrand != null)
            {
                strSql.Append("GoodBrand='" + model.GoodBrand + "',");
            }
            if (model.Product != null)
            {
                strSql.Append("Product='" + model.Product + "',");
            }
            if (model.GoodAreaNumber != null)
            {
                strSql.Append("GoodAreaNumber='" + model.GoodAreaNumber + "',");
            }
            if (model.GoodArea != null)
            {
                strSql.Append("GoodArea='" + model.GoodArea + "',");
            }
            if (model.GoodNumber != null)
            {
                strSql.Append("GoodNumber='" + model.GoodNumber + "',");
            }
            if (model.GoodRow != null)
            {
                strSql.Append("GoodRow='" + model.GoodRow + "',");
            }
            if (model.GoodCol != null)
            {
                strSql.Append("GoodCol='" + model.GoodCol + "',");
            }
            strSql.Append("GoodStatus='" + model.Status + "',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where GoodId=" + model.GoodId + "");
            objCommand.CommandText = strSql.ToString();
            return objCommand.ExecuteNonQuery() > 0 ? true : false;

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Good ");
            strSql.Append(" where GoodId=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_Good GetModel(int GoodId)
        {
            StringBuilder strSql = new StringBuilder();

          
            strSql.Append("select   ");
            strSql.Append(" GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber, Product,GoodProNo,GoodStatus,GoodBrand,GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec ");
            strSql.Append(" from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId");
          
            strSql.Append(" where GoodId=" + GoodId + "");            

            VAN_OA.Model.BaseInfo.TB_Good model = null;
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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.TB_Good> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("   GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber,Product,GoodProNo,GoodStatus,GoodBrand,GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec ");
            strSql.Append(" from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            //strSql.Append(" order by GoodName,GoodSpec,GoodModel ");
            List<VAN_OA.Model.BaseInfo.TB_Good> list = new List<VAN_OA.Model.BaseInfo.TB_Good>();

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
        /// 获取显示当前库存：XX,采库需出:YY ，滞留库存：XX-YY
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public List<decimal> GetGoodNum(int goodId)
        {
            List<decimal> nums = new List<decimal>();
            string sql = string.Format(@"select GoodNum,SumKuXuCai from TB_HouseGoods INNER join CaiKuXuNumView 
on CaiKuXuNumView.GoodId=TB_HouseGoods.goodId
WHERE TB_HouseGoods.goodId={0}",goodId);
            List<VAN_OA.Model.BaseInfo.TB_Good> list = new List<VAN_OA.Model.BaseInfo.TB_Good>();
            decimal GoodNum = 0;
            decimal SumKuXuCai = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        var ojb = objReader["GoodNum"];
                      
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            GoodNum = (decimal)ojb;
                        }
                       
                        ojb = objReader["SumKuXuCai"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            SumKuXuCai = (decimal)ojb;
                        }
                       
                    }
                }
            }
            nums.Add(GoodNum);
            nums.Add(SumKuXuCai);
            return nums;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.TB_Good>  GetListArray_New(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select * from (select GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber,Product,TB_HouseGoods.HouseId, GoodAvgPrice,GoodNum,GoodProNo,GoodStatus,GoodBrand,TB_Good.GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec
from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId
 left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId where TB_HouseGoods.id is not null
 union all
 select GoodArea,GoodNumber,GoodRow,GoodCol,GoodAreaNumber,Product,TB_HouseGoods.HouseId,GoodPrice as GoodAvgPrice,GoodNum,GoodProNo,GoodStatus,GoodBrand,TB_Good.GoodId,GoodNo,GoodName,ZhuJi,GoodSpec,GoodModel,GoodUnit,CreateUserId,CreateTime,tb_User.loginName,GoodTypeSmName,GoodTypeName,IfSpec
from TB_Good left join tb_User on tb_User.ID=TB_Good.CreateUserId
 left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId
 left join 
 ( select * from (select GooId,GoodPrice,ROW_NUMBER() over (partition  by GooId order by id desc ) as RowNum from CAI_OrderInHouses ) A WHERE RowNum=1
 ) as B on B.GooId=TB_Good.GoodId  where TB_HouseGoods.id is null) as Temp ");

            strSql.Append(@" 
 LEFT JOIN (
 select CAI_OrderInHouses.GooId,--支付单价/实采金额 *采购单价
sum(GoodNum*GoodPrice)-isnull(sum(OutTotal),0)-isnull(sum(SupplierInvoiceTotal),0) as NoInvoice,
isnull(sum(SupplierInvoiceTotal),0) AS  HadInvoice
 from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 
left join 
(
--单支付的单子
select RuIds,
--sum((GoodNum*GoodPrice)*SupplierInvoiceTotal/(GoodNum*CaiLastTruePrice))  as SupplierInvoiceTotal
sum(SupplierInvoiceTotal*GoodPrice/CaiLastTruePrice)  as SupplierInvoiceTotal
from TB_SupplierInvoice
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
where (Status='通过' or (IsYuFu=1 and Status<>'不通过'))  and (
(ActPay>0 ) or
(IsHeBing=1 and SupplierInvoiceTotal<0)  
OR
 (exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds ))
 ) group by RuIds
) as FuInvoice on FuInvoice.RuIds=CAI_OrderInHouses.Ids
left join 
(
select OrderCheckIds,SUM(GoodPrice*GoodNum) AS OutTotal from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id
where Status='通过' GROUP BY OrderCheckIds
) as caiOut ON caiOut.OrderCheckIds=CAI_OrderInHouses.Ids
where status='通过' 
group by CAI_OrderInHouses.GooId
 ) AS Invoice on Invoice.GooId=Temp.GoodId  left join CaiKuXuNumView on CaiKuXuNumView.GoodId=Temp.GoodId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by GoodNum desc,GoodProNo desc ");
            List<VAN_OA.Model.BaseInfo.TB_Good> list = new List<VAN_OA.Model.BaseInfo.TB_Good>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);
                        var ojb = objReader["GoodAvgPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodPrice = (decimal)ojb;
                        }
                        ojb = objReader["GoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum = (decimal)ojb;
                        }
                        ojb = objReader["NoInvoice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NoInvoice = (decimal)ojb;
                        }
                        ojb = objReader["HadInvoice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HadInvoice = (decimal)ojb;
                        }

                        ojb = objReader["SumKuXuCai"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumKuXuCai = Convert.ToDecimal(ojb);
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
        public List<VAN_OA.Model.BaseInfo.TB_Good> GetListToQuery_WebSer(string strWhere, int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select     ");
            strSql.Append(" TB_Good.GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,isnull(GoodNum,0) as GoodNum ");
            strSql.Append(" from TB_Good left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            //strSql.Append(" order by GoodName,GoodSpec,GoodModel ");
            List<VAN_OA.Model.BaseInfo.TB_Good> list = new List<VAN_OA.Model.BaseInfo.TB_Good>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.BaseInfo.TB_Good model = new VAN_OA.Model.BaseInfo.TB_Good();
                        object ojb;
                        model.GoodNo = dataReader["GoodNo"].ToString();
                        model.GoodName = dataReader["GoodName"].ToString();                        
                        model.GoodSpec = dataReader["GoodSpec"].ToString();
                        model.GoodModel = dataReader["GoodModel"].ToString();
                        model.GoodUnit = dataReader["GoodUnit"].ToString();
                        
                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();

                        }
                        model.HouseNum = Convert.ToDecimal(dataReader["GoodNum"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllGoodUnits()
        {
            List<string> GoodUnitList = new List<string>();
            string sql = string.Format("select GoodUnit from TB_Good group by GoodUnit order by GoodUnit");
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {


                        GoodUnitList.Add(dataReader["GoodUnit"].ToString());
                        
                    }
                }
            }
            return GoodUnitList;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_Good ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_Good model = new VAN_OA.Model.BaseInfo.TB_Good();
            object ojb;
            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.ZhuJi = dataReader["ZhuJi"].ToString();
            ojb = dataReader["GoodTypeName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeName = ojb.ToString();
            }
            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            model.GoodModel = dataReader["GoodModel"].ToString();
            model.GoodUnit = dataReader["GoodUnit"].ToString();
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserName = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();

            }

            ojb = dataReader["GoodTypeName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeName = ojb.ToString();
            }
            model.IfSpec = Convert.ToBoolean(dataReader["IfSpec"]);
            ojb = dataReader["GoodBrand"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodBrand = ojb.ToString();
            }
            model.ProNo = Convert.ToString(dataReader["GoodProNo"]);
            model.Status = Convert.ToString(dataReader["GoodStatus"]);
            model.Product = Convert.ToString(dataReader["Product"]);

            model.GoodArea = Convert.ToString(dataReader["GoodArea"]);
            model.GoodNumber = Convert.ToString(dataReader["GoodNumber"]);
            model.GoodRow = Convert.ToString(dataReader["GoodRow"]);
            model.GoodCol = Convert.ToString(dataReader["GoodCol"]);
            model.GoodAreaNumber = Convert.ToString(dataReader["GoodAreaNumber"]);


            return model;
        }



    }
}
