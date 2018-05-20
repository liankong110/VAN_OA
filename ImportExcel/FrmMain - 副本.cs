using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ImportExcel
{
    public partial class FrmMain : Form
    {
        private static string providerName = "System.Data.SqlClient";
        private static string DBConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        private static DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
        Dictionary<int, string> addSql = new Dictionary<int, string>();
        private static DataTable GetTableSchema()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ExcelJosnContent") });

            return dataTable;
        }
        /// <summary>
        /// 使用SqlBulkCopy方式插入数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private  long SqlBulkCopyInsert()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DataTable dataTable = GetTableSchema();
            string passportKey;
            for (int i = 0; i < 1000000; i++)
            {
                passportKey = Guid.NewGuid().ToString();
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = passportKey;
                dataTable.Rows.Add(dataRow);
            }

            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(DBConn);
            sqlBulkCopy.DestinationTableName = "Passport";
            sqlBulkCopy.BatchSize = dataTable.Rows.Count;
            SqlConnection sqlConnection = new SqlConnection(DBConn);
            sqlConnection.Open();
            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                sqlBulkCopy.WriteToServer(dataTable);
            }
            sqlBulkCopy.Close();
            sqlConnection.Close();

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static DbProviderFactory GetProviderFactory()
        {
            return provider;
        }

        public static bool ExeCommand(string sql)
        {

            bool result = false;
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = DBConn;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandText = sql;
                if (objCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                objConnection.Close();
            }
            return result;
        }

        public void ExeCommandParams()
        {
            lblMess.Text = string.Format("正在保存数据......");
            //Application.DoEvents();
            //StringBuilder strSql = new StringBuilder();
            //strSql.AppendFormat("insert into [{0}](", sqlTableName);
            //strSql.Append("Id,ExcelJosnContent)");
            //strSql.Append(" values (");
            //strSql.Append("@Id,@ExcelJosnContent)");            

            //using (DbConnection objConnection = provider.CreateConnection())
            //{
            //    objConnection.ConnectionString = DBConn;
            //    objConnection.Open();
            //    DbCommand objCommand = objConnection.CreateCommand();
            //    foreach (var sql in addSql.Keys)
            //    {
            //        objCommand.CommandText = strSql.ToString();

            //        objCommand.Parameters.Clear();
            //        objCommand.Parameters.AddRange(addSql[sql]);
            //        objCommand.ExecuteNonQuery();
            //    }             

            //    objConnection.Close();
            //}
            addSql = new Dictionary<int, string>();

        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                txtName.Text = ofdMain.SafeFileName;
                txtExcel.Text = ofdMain.FileName;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //            Excel2000-2003:

            //OleDbConnection ExcelConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + FilePath + "; Extended Properties='Excel 12.0;HDR=YES;IMEX=1'");

            //Excel2007:
            //OleDbConnection ExcelConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FilePath + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'"); 

            //string tableName = "";
            //ArrayList al = new ArrayList();
            //string strConn;
            //strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + txtExcel.Text + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            //OleDbConnection conn = new OleDbConnection(strConn);
            //conn.Open();
            //DataTable sheetNames = conn.GetOleDbSchemaTable
            //    (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //conn.Close();
            //tableName = sheetNames.Rows[0]["TABLE_NAME"].ToString();
            ////foreach (DataRow dr in sheetNames.Rows)
            ////{

            ////    al.Add(dr[2]);
            ////} 
            //DataSet ds = new DataSet();
            //OleDbDataAdapter oada = new OleDbDataAdapter("select top 100 * from [" + tableName + "]", strConn);
            //oada.Fill(ds);

            //dgvMain.DataSource = ds.Tables[0];
        }


        string sqlTableName = "";
        private void btnImport_Click(object sender, EventArgs e)
        {
            lblMess.Text = "准备导入......";
            Application.DoEvents();

            var start = DateTime.Now;
            Save();
            var end = DateTime.Now;
            var a = end - start;
            lblMess.Text = string.Format("导入成功!");
            Application.DoEvents();
            MessageBox.Show(string.Format("导入成功:开始时间:{0},结束时间：{1},总时间：{2}", start.ToString("yyyy-MM-dd HH:MM:ss"), end.ToString("yyyy-MM-dd HH:MM:ss"), a.TotalMinutes));
        }

        private void Save()
        {
            lblMess.Text = "正在读取Excel sheet信息......";
            Application.DoEvents();

            string sheetName = "";

            int Com = 0;
            ArrayList al = new ArrayList();
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + txtExcel.Text + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable sheetNames = conn.GetOleDbSchemaTable
                (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
          
            foreach (DataRow rowTable in sheetNames.Rows)
            {
                var start = DateTime.Now;
                sqlTableName = DateTime.Now.ToString("yyyyMMddHHmmss");
                CreateTable();

                sheetName = rowTable["TABLE_NAME"].ToString();
               

                lblMess.Text = string.Format("正在导入sheet[{0}] 数据......", sheetName);
                Application.DoEvents();
                int allCount = 0;
                //using (conn = new OleDbConnection(strConn))
                //{
                //    conn.Open();
                    OleDbCommand objCommand = new OleDbCommand(string.Format("select * from [" + sheetName + "]"), conn);
                    using (OleDbDataReader dataReader = objCommand.ExecuteReader())
                    {
                        using (SqlConnection objConnection = new SqlConnection(DBConn))
                        {
                            objConnection.Open();
                            SqlCommand sqlCommand = objConnection.CreateCommand();
                            while (dataReader.Read())
                            {
                                Com = dataReader.FieldCount;
                                MyExcel EXCEL = new MyExcel();
                                #region 列赋值
                                if (Com > 0)
                                {
                                    EXCEL.A = dataReader[0].ToString();
                                    if (Com > 1)
                                    {
                                        EXCEL.B = dataReader[1].ToString();
                                        if (Com > 2)
                                        {
                                            EXCEL.C = dataReader[2].ToString();
                                            if (Com > 3)
                                            {
                                                EXCEL.D = dataReader[3].ToString();
                                                if (Com > 4)
                                                {
                                                    EXCEL.E = dataReader[4].ToString();
                                                    if (Com > 5)
                                                    {
                                                        EXCEL.F = dataReader[5].ToString();
                                                        if (Com > 6)
                                                        {
                                                            EXCEL.G = dataReader[6].ToString();
                                                            if (Com > 7)
                                                            {
                                                                EXCEL.H = dataReader[7].ToString();
                                                                if (Com > 8)
                                                                {
                                                                    EXCEL.I = dataReader[8].ToString();
                                                                    if (Com > 9)
                                                                    {
                                                                        EXCEL.J = dataReader[9].ToString();
                                                                        if (Com > 10)
                                                                        {
                                                                            EXCEL.K = dataReader[11].ToString();
                                                                            if (Com > 12)
                                                                            {
                                                                                EXCEL.L = dataReader[12].ToString();
                                                                                if (Com > 13)
                                                                                {
                                                                                    EXCEL.M = dataReader[13].ToString();
                                                                                    if (Com > 14)
                                                                                    {
                                                                                        EXCEL.N = dataReader[14].ToString();
                                                                                        if (Com > 15)
                                                                                        {
                                                                                            EXCEL.O = dataReader[15].ToString();
                                                                                            if (Com > 16)
                                                                                            {
                                                                                                EXCEL.P = dataReader[16].ToString();
                                                                                                if (Com > 17)
                                                                                                {
                                                                                                    EXCEL.Q = dataReader[17].ToString();
                                                                                                    if (Com > 18)
                                                                                                    {
                                                                                                        EXCEL.R = dataReader[18].ToString();
                                                                                                        if (Com > 19)
                                                                                                        {
                                                                                                            EXCEL.S = dataReader[19].ToString();
                                                                                                            if (Com > 20)
                                                                                                            {
                                                                                                                EXCEL.T = dataReader[20].ToString();
                                                                                                                if (Com > 21)
                                                                                                                {
                                                                                                                    EXCEL.U = dataReader[21].ToString();
                                                                                                                    if (Com > 22)
                                                                                                                    {
                                                                                                                        EXCEL.V = dataReader[22].ToString();
                                                                                                                        if (Com > 23)
                                                                                                                        {
                                                                                                                            EXCEL.W = dataReader[23].ToString();
                                                                                                                            if (Com > 24)
                                                                                                                            {
                                                                                                                                EXCEL.X = dataReader[24].ToString();
                                                                                                                                if (Com > 25)
                                                                                                                                {
                                                                                                                                    EXCEL.Y = dataReader[25].ToString();
                                                                                                                                    if (Com > 26)
                                                                                                                                    {
                                                                                                                                        EXCEL.Z = dataReader[26].ToString();
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                //var josnModel = JsonConvert.SerializeObject(EXCEL);

                                Add(sqlCommand, JsonConvert.SerializeObject(EXCEL));
                                //addSql.Add(allCount, josnModel);
                                //if (allCount % 20000 == 0)
                                //{
                                //    ExeCommandParams();
                                //}
                                allCount++;
                                lblMess.Text = string.Format("正在导入sheet[{0}] 数据 第{1}行......", sheetName,allCount);
                                Application.DoEvents();
                            }
                            //if (allCount % 20000 != 0)
                            //{
                            //    ExeCommandParams();
                            //}
                            objConnection.Close();
                        }
                    //}
                    //conn.Close();
                }

            }
            conn.Close();
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SqlCommand objCommand, string ExcelJosnContent)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat("insert into [{0}](", sqlTableName);
            strSql.Append("ExcelJosnContent)");
            strSql.Append(" values (");
            strSql.Append("@ExcelJosnContent)");
            SqlParameter[] parameters = {                  
                    new SqlParameter("@ExcelJosnContent", SqlDbType.NVarChar,-1)};
           
            parameters[0].Value = ExcelJosnContent;
            objCommand.CommandText = strSql.ToString();

            objCommand.Parameters.Clear();
            objCommand.Parameters.AddRange(parameters);
            objCommand.ExecuteNonQuery();
        }

        private bool CreateTable()
        {
            lblMess.Text = "创建主表......";
            Application.DoEvents();
            string sql = string.Format(@"CREATE TABLE [dbo].[{0}](
	[Ids] [int] IDENTITY(1,1) NOT NULL,	
	[ExcelJosnContent] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[Ids] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]", sqlTableName);
            return ExeCommand(sql);
        }
    }
}
