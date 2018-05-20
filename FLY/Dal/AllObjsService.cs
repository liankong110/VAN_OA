namespace VAN_OA.Dal
{
    using VAN_OA;
    using VAN_OA.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using Microsoft.Office.Interop.Excel;
    using System.Reflection;

    public class AllObjsService
    {
        public List<AllObjs> getAllobjs()
        {
            List<AllObjs> objs = new List<AllObjs>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("select * from Tb_Objs", conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        AllObjs o = new AllObjs();
                        o.Chinese = objReader["ChineseName"].ToString();
                        o.English = objReader["EnglishName"].ToString();
                        o.FormId = Convert.ToInt32(objReader["FormID"]);
                        objs.Add(o);
                    }
                    objReader.Close();
                }
            }
            return objs;
        }

         public void GridViewtoExcel(GridView MyGridView,String FileName)
         {
            Object[,] MyData=new object[MyGridView.Rows.Count + 1, MyGridView.Columns.Count];
            Application MyExcel = null;
            Workbook MyWorkBook= null;
            Worksheet MyWorkSheet= null;
            Range MyRange= null;
            bool output;
            System.Globalization.CultureInfo CurrentCI= System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {
            MyExcel = new Microsoft.Office.Interop.Excel.Application();
            MyWorkBook = MyExcel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            MyWorkSheet = (Worksheet)MyWorkBook.Worksheets[1];
            for (int i = 0 ;i<MyGridView.Columns.Count;i++)
            {
                if (MyGridView.Columns[i].Visible == true)
                {
                    MyData[0, i] = MyGridView.Columns[i].HeaderText;
                }
            }
            for (int i = 0 ;i<MyGridView.Rows.Count;i++)
            {
                for (int j = 0 ;i<MyGridView.Columns.Count;j++)
                {
                    if (MyGridView.Columns[j].Visible == true)
                    {
                        if (bool.TryParse(MyGridView.Rows[i].Cells[j].Text.ToString(),out output))
                        {
                            MyData[i + 1, j] = bool.Parse(MyGridView.Rows[i].Cells[j].Text.ToString()) == true?"是":"否";
                        }
                        else
                        {
                            MyData[i + 1, j] = "'" + MyGridView.Rows[i].Cells[j].Text.ToString().Trim();
                        }
                    }
                }
            }
            MyRange = MyWorkSheet.get_Range(MyExcel.Cells[1, 1], MyExcel.Cells[MyGridView.Rows.Count + 1, MyGridView.Columns.Count]);
            MyRange.Value2 = MyData;
            System.Windows.Forms.Application.DoEvents();
            MyExcel.get_Range(MyExcel.Cells[1, 1], MyExcel.Cells[1, MyGridView.Columns.Count]).Font.Bold = true;
            MyExcel.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            MyExcel.Cells.EntireColumn.AutoFit();
            MyExcel.Visible = false;
            MyWorkSheet.SaveAs(FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            MyWorkBook.Close(false, null, null);
        }
        catch (Exception ex)
             {
            MyWorkBook.Saved = true;
            throw ex;
             }
        finally
        {
            MyExcel.Quit();
            MyExcel = null;
        }
         }
    }
}

