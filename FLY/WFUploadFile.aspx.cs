using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using VAN_OA.Model.Fin;
using System.Data.SqlClient;
using VAN_OA.Dal.Fin;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

namespace VAN_OA
{
    public partial class WFUploadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear(); //清除所有之前生成的Response内容
            Response.Write(PicUpload("Excel"));
            Response.End(); //停止Response后续写入动作，保证Response内只有我们写入内容
        }


        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="folderName">对应的文件夹</param>
        /// <returns></returns>
        public string PicUpload(string folderName)
        {
           
            var urlpath = string.Format(@"/Attachment/{0}/{1}/", folderName, DateTime.Now.ToString("yyyyMM"));
            var dirpath = Server.MapPath(urlpath);
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            var postedFile = System.Web.HttpContext.Current.Request.Files[0];
            if (postedFile == null)
            {
                return "";
            }
            var filename = GetFileName(postedFile.FileName.ToLower());           
            postedFile.SaveAs(dirpath + filename);

            //开始解析Excel
            LoadExcel(dirpath + filename);
          

            return urlpath + filename;
        }

        private void LoadExcel(string fileAddress)
        {
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileAddress + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable sheetNames = conn.GetOleDbSchemaTable
                (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            int allSheets = 0;

            foreach (DataRow rowTable in sheetNames.Rows)
            {
                string sheetName = rowTable["TABLE_NAME"].ToString();

                conn = new OleDbConnection(strConn);
                conn.Open();
                OleDbCommand objCommand = new OleDbCommand(string.Format("select * from [" + sheetName + "]"), conn);
                int rowIndex = 1;
                BankFlowService bandFlowService=new BankFlowService();
                PetitionsService petSer = new PetitionsService();
                CG_POOrderService POSer = new CG_POOrderService();
                CAI_POCaiService pocaiSer=new CAI_POCaiService();
                using (SqlConnection sqlconn = DBHelp.getConn())
                {
                    sqlconn.Open();
                   
                    SqlCommand sqlCommand = sqlconn.CreateCommand();


                    using (OleDbDataReader dataReader = objCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            //if (rowIndex > 1)
                            //{
                            //    Petitions model = new Petitions();
                            //    model.Number = dataReader[0].ToString();
                            //    model.Type = dataReader[1].ToString();
                            //    model.OldIndex = dataReader[2].ToString();                               
                            //    var ojb = dataReader[3];
                            //    if (ojb != null && ojb != DBNull.Value)
                            //    {
                            //        model.SignDate = Convert.ToDateTime(ojb);
                            //    }

                            //    model.PoNo = dataReader[4].ToString();
                            //    model.Summary = dataReader[5].ToString();
                            //    ojb = dataReader[6];
                            //    if (ojb != null && ojb != DBNull.Value)
                            //    {
                            //        model.SumPages = Convert.ToInt32(ojb);
                            //    }
                            //    model.Local = dataReader[7].ToString();
                            //    model.L_Year = Convert.ToInt32(dataReader[8]);
                            //    model.L_Month = Convert.ToInt32(dataReader[9]);
                            //    model.Remark = dataReader[10].ToString();
                            //    model.SumCount = 1;
                            //    model.BCount = 1;
                            //    model.Handler = "王汉中";

                            //    var poList = POSer.GetSimpListArray(string.Format("and pono='{0}'", model.PoNo), sqlCommand);
                            //    if (poList.Count > 0)
                            //    {
                            //        CG_POOrder poorderModel = poList[0];

                            //        model.AE = poorderModel.AE;
                            //        model.GuestName = poorderModel.GuestName;
                            //        model.Name = poorderModel.POName;

                            //        //var supplierList = pocaiSer.GetLastSupplier(string.Format("'{0}'", model.PoNo), sqlCommand);
                            //        //model.SalesUnit = string.Join(",", supplierList.Select(t => t.Supplier).ToArray());
                            //        //model.Total = supplierList.Sum(t => t.Total);
                            //    }
                            //    petSer.Add(model,sqlCommand);
                            //}
                            if (rowIndex > 8)
                            {
                                BankFlow model = new BankFlow();
                                model.TransactionType = dataReader[0].ToString();
                                if (model.TransactionType == "")
                                {
                                    continue;
                                }
                                model.BusinessType = dataReader[1].ToString();
                                model.PayerAccountBank = dataReader[3].ToString();
                                model.DebitAccountNo = dataReader[4].ToString();
                                model.OutPayerName = dataReader[5].ToString();
                                model.BeneficiaryAccountBank = dataReader[7].ToString();
                                model.PayeeAccountNumber = dataReader[8].ToString();
                                model.InPayeeName = dataReader[9].ToString();
                                DateTime dt = DateTime.ParseExact(dataReader[10].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                model.TransactionDate = Convert.ToDateTime(dt.ToString("yyyy-MM-dd") + " " + dataReader[11]);
                                model.TradeCurrency = dataReader[12].ToString();
                                model.TradeAmount = Convert.ToDecimal(dataReader[13]);
                                model.AfterTransactionBalance = Convert.ToDecimal(dataReader[14]);
                                model.TransactionReferenceNumber = dataReader[17].ToString();
                                model.VoucherType = dataReader[20].ToString();
                                model.Reference = dataReader[23].ToString();
                                model.Purpose = dataReader[24].ToString();
                                model.Remark = dataReader[25].ToString();
                                model.Remarks = dataReader[26].ToString();
                                //判断是否重复
                                string sql = string.Format("SELECT top 1 ID FROM [BankFlow] WHERE TransactionReferenceNumber='{0}'",model.TransactionReferenceNumber);
                                sqlCommand.CommandText = sql;
                                var resultId = sqlCommand.ExecuteScalar();
                                if (!(resultId is DBNull) && resultId != null)
                                {
                                    model.Id = Convert.ToInt32(resultId);
                                    bandFlowService.Update(model, sqlCommand);
                                }
                                else
                                {
                                    bandFlowService.Add(model, sqlCommand);
                                }
                            }
                            rowIndex++;
                        }
                    }
                    sqlconn.Close();
                }
            }
        }

        private static string GetFileName(string filename)
        {
            var random = new Random();
            var extension = ".jpg";
            if (filename.Contains("."))
            {
                extension = filename.Substring(filename.IndexOf('.')).Length > 0
                                ? filename.Substring(filename.IndexOf('.'))
                                : ".jpg";
            }
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + random.Next(10, 99) + extension;
        }

    }
}