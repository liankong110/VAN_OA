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
using VAN_OA.Dal;

namespace ImportExcel
{
    public partial class FrmMain : Form
    {
        private static string providerName = "System.Data.SqlClient";
        private static string DBConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        private static string FileName = ConfigurationManager.ConnectionStrings["File"].ToString();
        private static DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
        Dictionary<int, string> addSql = new Dictionary<int, string>();
        private DataTable GetTableSchema()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Ids") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ExcelJosnContent") });
            return dataTable;
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

    


        string sqlTableName = "";
        private void btnImport_Click(object sender, EventArgs e)
        {
            //lblMess.Text = "准备导入......";
            //Application.DoEvents();

            //var start = DateTime.Now;
            //Save();
            //var end = DateTime.Now;
            //var a = end - start;
            //lblMess.Text = string.Format("导入成功!");
            //Application.DoEvents();
            //MessageBox.Show(string.Format("导入成功:开始时间:{0},结束时间：{1},总时间：{2}", start.ToString("yyyy-MM-dd HH:MM:ss"), end.ToString("yyyy-MM-dd HH:MM:ss"), a.TotalMinutes));
        }

        private void Save(VAN_OA.Model.TB_EXCEL model)
        {
            lblMess.Text = "正在读取Excel sheet信息......";
            Application.DoEvents();
            string FileAddress = FileName + model.FileType;
          
            string sheetName = "";

            int Com = 0;
            ArrayList al = new ArrayList();
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FileAddress + "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable sheetNames = conn.GetOleDbSchemaTable
                (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            int allSheets = 0;
            foreach (DataRow rowTable in sheetNames.Rows)
            {
                var start = DateTime.Now;
                sqlTableName = DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + "_" + allSheets;
                sheetName = rowTable["TABLE_NAME"].ToString();
                CreateTable(sheetName,model,allSheets);             


                lblMess.Text = string.Format("正在导入sheet[{0}] 数据......", sheetName);
                Application.DoEvents();
                int allCount = 0;
                if (allSheets != 0)
                {
                    conn.Close();
                    conn = new OleDbConnection(strConn);
                    conn.Open();
                }

                OleDbCommand objCommand = new OleDbCommand(string.Format("select * from [" + sheetName + "]"), conn);
                using (OleDbDataReader dataReader = objCommand.ExecuteReader())
                {
                    DataTable dataTable = GetTableSchema();
                    while (dataReader.Read())
                    {
                        Com = dataReader.FieldCount;
                        var EXCEL = new MyExcel();
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

                     
                        var dataRow = dataTable.NewRow();
                        dataRow[1] = JsonConvert.SerializeObject(EXCEL);
                        dataTable.Rows.Add(dataRow);

                        if (allCount % 10000 == 0)
                        {
                            SaveData(dataTable);
                            dataTable = GetTableSchema();
                        }
                        allCount++;
                        lblMess.Text = string.Format("正在导入sheet[{0}] 数据 第{1}行......", sheetName, allCount);
                        Application.DoEvents();
                    }
                    if (allCount % 10000 != 0)
                    {
                        SaveData(dataTable);
                        dataTable = GetTableSchema();
                    }

                }
                string updateSql = string.Format("update TB_EXCEL set UpState=3 where Table_Name='{0}';",sqlTableName);
                ExeCommand(updateSql);
                allSheets++;

            }
            conn.Close();
        }

        public void SaveData(DataTable dataTable)
        {
            using (var sqlBulkCopy = new SqlBulkCopy(DBConn))
            {
                sqlBulkCopy.DestinationTableName = "[" + sqlTableName + "]";
                sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                using (var sqlConnection = new SqlConnection(DBConn))
                {
                    sqlConnection.Open();
                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    sqlBulkCopy.Close();
                    sqlConnection.Close();
                }

            }
            dataTable = null;
            GC.Collect();
        }


        private void CreateTable(string SheetName,VAN_OA.Model.TB_EXCEL model, int allSheets)
        {
            if (allSheets == 0)
            {
                string updateSql = "update TB_EXCEL set Table_Name='" + sqlTableName + "',SheetName='" + SheetName.Replace("'", "").Replace("$", "") + "' where id=" + model.Id;
                ExeCommand(updateSql);
            }
            else
            {
                model.SheetName = SheetName.Replace("'", "").Replace("$", "");
                model.Table_Name = sqlTableName;
                excelSer.Add(model);
            }
            lblMess.Text = "创建主表......";
            Application.DoEvents();
            string sql = string.Format(@"CREATE TABLE [dbo].[{0}](
	[Ids] [int] IDENTITY(1,1) NOT NULL,	
	[ExcelJosnContent] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[Ids] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];", sqlTableName);

            //sql += string.Format("insert TB_EXCEL values('{0}',getdate(),'{1}','{2}') ", txtName.Text, sqlTableName, SheetName.Replace("'", "").Replace("$",""));
             ExeCommand(sql);
        }

        public bool Doing = false;
        MyExcelService excelSer = new MyExcelService();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Doing == false)
            {
                var model = excelSer.TopQueryMainExcel(" IsParent=1 and UpState=1");
                if (model.Id > 0)
                {
                    DoWork(model);
                }
            }
        }

        private void DoWork(VAN_OA.Model.TB_EXCEL model)
        {
            try
            {
                Doing = true;
                lblMess.Text = "准备导入......";
                Application.DoEvents();

                var start = DateTime.Now;
                Save(model);
                var end = DateTime.Now;
                var a = end - start;
                lblMess.Text = string.Format("导入成功!");
                Application.DoEvents();
                //MessageBox.Show(string.Format("导入成功:开始时间:{0},结束时间：{1},总时间：{2}", start.ToString("yyyy-MM-dd HH:MM:ss"), end.ToString("yyyy-MM-dd HH:MM:ss"), a.TotalMinutes));

            }
            catch (Exception)
            {


            }
            finally
            {
                Doing = false;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
