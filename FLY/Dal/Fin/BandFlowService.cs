using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using VAN_OA.Model.Fin;

namespace VAN_OA.Dal.Fin
{
    public class BankFlowService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.BankFlow model, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TransactionType != null)
            {
                strSql1.Append("TransactionType,");
                strSql2.Append("'" + model.TransactionType + "',");
            }
            if (model.BusinessType != null)
            {
                strSql1.Append("BusinessType,");
                strSql2.Append("'" + model.BusinessType + "',");
            }
            if (model.PayerAccountBank != null)
            {
                strSql1.Append("PayerAccountBank,");
                strSql2.Append("'" + model.PayerAccountBank + "',");
            }
            if (model.DebitAccountNo != null)
            {
                strSql1.Append("DebitAccountNo,");
                strSql2.Append("'" + model.DebitAccountNo + "',");
            }
            if (model.OutPayerName != null)
            {
                strSql1.Append("OutPayerName,");
                strSql2.Append("'" + model.OutPayerName + "',");
            }
            if (model.BeneficiaryAccountBank != null)
            {
                strSql1.Append("BeneficiaryAccountBank,");
                strSql2.Append("'" + model.BeneficiaryAccountBank + "',");
            }
            if (model.PayeeAccountNumber != null)
            {
                strSql1.Append("PayeeAccountNumber,");
                strSql2.Append("'" + model.PayeeAccountNumber + "',");
            }
            if (model.InPayeeName != null)
            {
                strSql1.Append("InPayeeName,");
                strSql2.Append("'" + model.InPayeeName + "',");
            }
            if (model.TransactionDate != null)
            {
                strSql1.Append("TransactionDate,");
                strSql2.Append("'" + model.TransactionDate + "',");
            }
            if (model.TradeCurrency != null)
            {
                strSql1.Append("TradeCurrency,");
                strSql2.Append("'" + model.TradeCurrency + "',");
            }
            if (model.TradeAmount != null)
            {
                strSql1.Append("TradeAmount,");
                strSql2.Append("" + model.TradeAmount + ",");
            }
            if (model.AfterTransactionBalance != null)
            {
                strSql1.Append("AfterTransactionBalance,");
                strSql2.Append("" + model.AfterTransactionBalance + ",");
            }
            if (model.TransactionReferenceNumber != null)
            {
                strSql1.Append("TransactionReferenceNumber,");
                strSql2.Append("'" + model.TransactionReferenceNumber + "',");
            }
            if (model.VoucherType != null)
            {
                strSql1.Append("VoucherType,");
                strSql2.Append("'" + model.VoucherType + "',");
            }
            if (model.Reference != null)
            {
                strSql1.Append("Reference,");
                strSql2.Append("'" + model.Reference + "',");
            }
            if (model.Purpose != null)
            {
                strSql1.Append("Purpose,");
                strSql2.Append("'" + model.Purpose + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.Remarks != null)
            {
                strSql1.Append("Remarks,");
                strSql2.Append("'" + model.Remarks + "',");
            }
            if (model.Notes != null)
            {
                strSql1.Append("Notes,");
                strSql2.Append("'" + model.Notes + "',");
            }
            if (model.IncomeType != null)
            {
                strSql1.Append("IncomeType,");
                strSql2.Append("'" + model.IncomeType + "',");
            }
            if (model.PaymentType != null)
            {
                strSql1.Append("PaymentType,");
                strSql2.Append("'" + model.PaymentType + "',");
            }
            if (model.YuRemarks != null)
            {
                strSql1.Append("YuRemarks,");
                strSql2.Append("'" + model.YuRemarks + "',");
            }
            strSql.Append("insert into BankFlow(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            sqlCommand.CommandText = strSql.ToString();

            object obj = sqlCommand.ExecuteScalar();
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
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.BankFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TransactionType != null)
            {
                strSql1.Append("TransactionType,");
                strSql2.Append("'" + model.TransactionType + "',");
            }
            if (model.BusinessType != null)
            {
                strSql1.Append("BusinessType,");
                strSql2.Append("'" + model.BusinessType + "',");
            }
            if (model.PayerAccountBank != null)
            {
                strSql1.Append("PayerAccountBank,");
                strSql2.Append("'" + model.PayerAccountBank + "',");
            }
            if (model.DebitAccountNo != null)
            {
                strSql1.Append("DebitAccountNo,");
                strSql2.Append("'" + model.DebitAccountNo + "',");
            }
            if (model.OutPayerName != null)
            {
                strSql1.Append("OutPayerName,");
                strSql2.Append("'" + model.OutPayerName + "',");
            }
            if (model.BeneficiaryAccountBank != null)
            {
                strSql1.Append("BeneficiaryAccountBank,");
                strSql2.Append("'" + model.BeneficiaryAccountBank + "',");
            }
            if (model.PayeeAccountNumber != null)
            {
                strSql1.Append("PayeeAccountNumber,");
                strSql2.Append("'" + model.PayeeAccountNumber + "',");
            }
            if (model.InPayeeName != null)
            {
                strSql1.Append("InPayeeName,");
                strSql2.Append("'" + model.InPayeeName + "',");
            }
            if (model.TransactionDate != null)
            {
                strSql1.Append("TransactionDate,");
                strSql2.Append("'" + model.TransactionDate + "',");
            }
            if (model.TradeCurrency != null)
            {
                strSql1.Append("TradeCurrency,");
                strSql2.Append("'" + model.TradeCurrency + "',");
            }
            if (model.TradeAmount != null)
            {
                strSql1.Append("TradeAmount,");
                strSql2.Append("" + model.TradeAmount + ",");
            }
            if (model.AfterTransactionBalance != null)
            {
                strSql1.Append("AfterTransactionBalance,");
                strSql2.Append("" + model.AfterTransactionBalance + ",");
            }
            if (model.TransactionReferenceNumber != null)
            {
                strSql1.Append("TransactionReferenceNumber,");
                strSql2.Append("'" + model.TransactionReferenceNumber + "',");
            }
            if (model.VoucherType != null)
            {
                strSql1.Append("VoucherType,");
                strSql2.Append("'" + model.VoucherType + "',");
            }
            if (model.Reference != null)
            {
                strSql1.Append("Reference,");
                strSql2.Append("'" + model.Reference + "',");
            }
            if (model.Purpose != null)
            {
                strSql1.Append("Purpose,");
                strSql2.Append("'" + model.Purpose + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.Remarks != null)
            {
                strSql1.Append("Remarks,");
                strSql2.Append("'" + model.Remarks + "',");
            }
            if (model.Notes != null)
            {
                strSql1.Append("Notes,");
                strSql2.Append("'" + model.Notes + "',");
            }
            if (model.IncomeType != null)
            {
                strSql1.Append("IncomeType,");
                strSql2.Append("'" + model.IncomeType + "',");
            }
            if (model.PaymentType != null)
            {
                strSql1.Append("PaymentType,");
                strSql2.Append("'" + model.PaymentType + "',");
            }
            if (model.YuRemarks != null)
            {
                strSql1.Append("YuRemarks,");
                strSql2.Append("'" + model.YuRemarks + "',");
            }
            strSql.Append("insert into BankFlow(");
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
        public bool Update(VAN_OA.Model.Fin.BankFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BankFlow set ");
            if (model.TransactionType != null)
            {
                strSql.Append("TransactionType='" + model.TransactionType + "',");
            }
            if (model.BusinessType != null)
            {
                strSql.Append("BusinessType='" + model.BusinessType + "',");
            }
            if (model.PayerAccountBank != null)
            {
                strSql.Append("PayerAccountBank='" + model.PayerAccountBank + "',");
            }
            if (model.DebitAccountNo != null)
            {
                strSql.Append("DebitAccountNo='" + model.DebitAccountNo + "',");
            }
            if (model.OutPayerName != null)
            {
                strSql.Append("OutPayerName='" + model.OutPayerName + "',");
            }
            if (model.BeneficiaryAccountBank != null)
            {
                strSql.Append("BeneficiaryAccountBank='" + model.BeneficiaryAccountBank + "',");
            }
            if (model.PayeeAccountNumber != null)
            {
                strSql.Append("PayeeAccountNumber='" + model.PayeeAccountNumber + "',");
            }
            if (model.InPayeeName != null)
            {
                strSql.Append("InPayeeName='" + model.InPayeeName + "',");
            }
            if (model.TransactionDate != null)
            {
                strSql.Append("TransactionDate='" + model.TransactionDate + "',");
            }
            if (model.TradeCurrency != null)
            {
                strSql.Append("TradeCurrency='" + model.TradeCurrency + "',");
            }
            if (model.TradeAmount != null)
            {
                strSql.Append("TradeAmount=" + model.TradeAmount + ",");
            }
            if (model.AfterTransactionBalance != null)
            {
                strSql.Append("AfterTransactionBalance=" + model.AfterTransactionBalance + ",");
            }
            if (model.TransactionReferenceNumber != null)
            {
                strSql.Append("TransactionReferenceNumber='" + model.TransactionReferenceNumber + "',");
            }
            if (model.VoucherType != null)
            {
                strSql.Append("VoucherType='" + model.VoucherType + "',");
            }
            if (model.Reference != null)
            {
                strSql.Append("Reference='" + model.Reference + "',");
            }
            if (model.Purpose != null)
            {
                strSql.Append("Purpose='" + model.Purpose + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            if (model.Remarks != null)
            {
                strSql.Append("Remarks='" + model.Remarks + "',");
            }
            if (model.Notes != null)
            {
                strSql.Append("Notes='" + model.Notes + "',");
            }
            if (model.IncomeType != null)
            {
                strSql.Append("IncomeType='" + model.IncomeType + "',");
            }
            if (model.PaymentType != null)
            {
                strSql.Append("PaymentType='" + model.PaymentType + "',");
            }
            if (model.YuRemarks != null)
            {
                strSql.Append("YuRemarks='" + model.YuRemarks + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        public bool Update(VAN_OA.Model.Fin.BankFlow model, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BankFlow set ");
            if (model.TransactionType != null)
            {
                strSql.Append("TransactionType='" + model.TransactionType + "',");
            }
            if (model.BusinessType != null)
            {
                strSql.Append("BusinessType='" + model.BusinessType + "',");
            }
            if (model.PayerAccountBank != null)
            {
                strSql.Append("PayerAccountBank='" + model.PayerAccountBank + "',");
            }
            if (model.DebitAccountNo != null)
            {
                strSql.Append("DebitAccountNo='" + model.DebitAccountNo + "',");
            }
            if (model.OutPayerName != null)
            {
                strSql.Append("OutPayerName='" + model.OutPayerName + "',");
            }
            if (model.BeneficiaryAccountBank != null)
            {
                strSql.Append("BeneficiaryAccountBank='" + model.BeneficiaryAccountBank + "',");
            }
            if (model.PayeeAccountNumber != null)
            {
                strSql.Append("PayeeAccountNumber='" + model.PayeeAccountNumber + "',");
            }
            if (model.InPayeeName != null)
            {
                strSql.Append("InPayeeName='" + model.InPayeeName + "',");
            }
            if (model.TransactionDate != null)
            {
                strSql.Append("TransactionDate='" + model.TransactionDate + "',");
            }
            if (model.TradeCurrency != null)
            {
                strSql.Append("TradeCurrency='" + model.TradeCurrency + "',");
            }
            if (model.TradeAmount != null)
            {
                strSql.Append("TradeAmount=" + model.TradeAmount + ",");
            }
            if (model.AfterTransactionBalance != null)
            {
                strSql.Append("AfterTransactionBalance=" + model.AfterTransactionBalance + ",");
            }
            if (model.TransactionReferenceNumber != null)
            {
                strSql.Append("TransactionReferenceNumber='" + model.TransactionReferenceNumber + "',");
            }
            if (model.VoucherType != null)
            {
                strSql.Append("VoucherType='" + model.VoucherType + "',");
            }
            if (model.Reference != null)
            {
                strSql.Append("Reference='" + model.Reference + "',");
            }
            if (model.Purpose != null)
            {
                strSql.Append("Purpose='" + model.Purpose + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            if (model.Remarks != null)
            {
                strSql.Append("Remarks='" + model.Remarks + "',");
            }
            if (model.Notes != null)
            {
                strSql.Append("Notes='" + model.Notes + "',");
            }
            if (model.IncomeType != null)
            {
                strSql.Append("IncomeType='" + model.IncomeType + "',");
            }
            if (model.PaymentType != null)
            {
                strSql.Append("PaymentType='" + model.PaymentType + "',");
            }
            if (model.YuRemarks != null)
            {
                strSql.Append("YuRemarks='" + model.YuRemarks + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            sqlCommand.CommandText = strSql.ToString();
            bool rowsAffected = sqlCommand.ExecuteNonQuery() > 0;
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BankFlow ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.Fin.BankFlow GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,TransactionType,BusinessType,PayerAccountBank,DebitAccountNo,OutPayerName,BeneficiaryAccountBank,PayeeAccountNumber,InPayeeName,TransactionDate,TradeCurrency,TradeAmount,AfterTransactionBalance,TransactionReferenceNumber,VoucherType,Reference,Purpose,Remark,Remarks,Notes,IncomeType,PaymentType,YuRemarks ");
            strSql.Append(" from BankFlow ");
            strSql.Append(" where BankFlow.ID=" + ID + "");

            VAN_OA.Model.Fin.BankFlow model = null;
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
        public List<VAN_OA.Model.Fin.BankFlow> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,TransactionType,BusinessType,PayerAccountBank,DebitAccountNo,OutPayerName,BeneficiaryAccountBank,PayeeAccountNumber,InPayeeName,TransactionDate,TradeCurrency,TradeAmount,AfterTransactionBalance,TransactionReferenceNumber,VoucherType,Reference,Purpose,Remark,Remarks,Notes,IncomeType,PaymentType,YuRemarks,SUMFPTotal,SUMOutTotal ");
            strSql.Append(" from BankFlow ");
            strSql.Append(@" left join 
 (
 select ReferenceNumber,SUM(FPTotal) AS SUMFPTotal FROM In_BankFlow GROUP BY ReferenceNumber
 ) AS INFlow ON INFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber
  left join 
 (
 select ReferenceNumber,SUM(OutTotal) AS SUMOutTotal FROM Out_BankFlow GROUP BY ReferenceNumber
 ) AS OUTFlow ON OUTFlow.ReferenceNumber=BankFlow.TransactionReferenceNumber
");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by TransactionDate desc");
            List<VAN_OA.Model.Fin.BankFlow> list = new List<VAN_OA.Model.Fin.BankFlow>();

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
                        ojb = objReader["SUMFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SUMFPTotal = (decimal)ojb;
                        }

                        ojb = objReader["SUMOutTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SUMOutTotal = (decimal)ojb;
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<VAN_OA.Model.Fin.Report> Report(int year,int month)
        {
            string sql = string.Format(@"
select 1 AS 'Type', BandFlowType.TypeName,Total FROM BandFlowType
left join
( 
SELECT In_BankFlow.InType,SUM(FPTotal) AS Total FROM  In_BankFlow 
left join BankFlow on BankFlow.TransactionReferenceNumber=In_BankFlow.ReferenceNumber
where Year(BankFlow.TransactionDate)={0} and Month(BankFlow.TransactionDate)={1} 
GROUP BY In_BankFlow.InType
) as INFlow ON INFlow.InType=BandFlowType.TypeName
where BandFlowType.Type=1   
union all
select 0 AS 'Type', BandFlowType.TypeName,Total FROM BandFlowType
left join
( 
SELECT Out_BankFlow.OutType,SUM(OutTotal) AS Total FROM  Out_BankFlow 
left join BankFlow on BankFlow.TransactionReferenceNumber=Out_BankFlow.ReferenceNumber
where Year(BankFlow.TransactionDate)={0} and Month(BankFlow.TransactionDate)={1}
GROUP BY Out_BankFlow.OutType
) as OutFlow ON OutFlow.OutType=BandFlowType.TypeName
where BandFlowType.Type=0   ",year,month);

            List<VAN_OA.Model.Fin.Report> list = new List<VAN_OA.Model.Fin.Report>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = new Report();
                        model.Type = Convert.ToInt32(objReader["Type"]);
                        model.Name = Convert.ToString(objReader["TypeName"]);
                        object ojb;
                        ojb = objReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Total = (decimal)ojb;
                        }
                        list.Add(model);
                    }
                }
                return list;
            }

        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.Fin.BankFlow ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.Fin.BankFlow model = new VAN_OA.Model.Fin.BankFlow();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.TransactionType = dataReader["TransactionType"].ToString();
            model.BusinessType = dataReader["BusinessType"].ToString();
            model.PayerAccountBank = dataReader["PayerAccountBank"].ToString();
            model.DebitAccountNo = dataReader["DebitAccountNo"].ToString();
            model.OutPayerName = dataReader["OutPayerName"].ToString();
            model.BeneficiaryAccountBank = dataReader["BeneficiaryAccountBank"].ToString();
            model.PayeeAccountNumber = dataReader["PayeeAccountNumber"].ToString();
            model.InPayeeName = dataReader["InPayeeName"].ToString();
            ojb = dataReader["TransactionDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TransactionDate = (DateTime)ojb;
            }
            model.TradeCurrency = dataReader["TradeCurrency"].ToString();
            ojb = dataReader["TradeAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeAmount = (decimal)ojb;
            }
            ojb = dataReader["AfterTransactionBalance"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AfterTransactionBalance = (decimal)ojb;
            }
            model.TransactionReferenceNumber = dataReader["TransactionReferenceNumber"].ToString();
            model.VoucherType = dataReader["VoucherType"].ToString();
            model.Reference = dataReader["Reference"].ToString();
            model.Purpose = dataReader["Purpose"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.Remarks = dataReader["Remarks"].ToString();
            model.Notes = dataReader["Notes"].ToString();
            model.IncomeType = dataReader["IncomeType"].ToString();
            model.PaymentType = dataReader["PaymentType"].ToString();
            model.YuRemarks = dataReader["YuRemarks"].ToString();
            return model;
        }



    }
}
