using System;
using System.Collections.Generic;
using System.Data;
using VAN_OA.Model;
using  System.Text;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Data.Common;
namespace VAN_OA.JXC
{
    public partial class WFEFormAssess : System.Web.UI.Page
    {

        protected DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool showAll = true;
                if (SysObj.IfShowAll("人事考核", Session["currentUserId"], "") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                var user = new List<VAN_OA.Model.User>();
                var userSer = new VAN_OA.Dal.SysUserService();
                if (showAll == true)
                {
                    user = userSer.getAllUserByLoginName("");
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

                }

                else
                {
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                ds=new DataSet();
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
              
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            ds = DBHelp.getDataSet(GetSql(ddlUser.Text,ddlUser.SelectedItem.Text).ToString());
        }

        private StringBuilder GetSql(string selectUserId,string selectUserName)
        {
            string t1 = "";
            string t2 = "";
            if (!string.IsNullOrEmpty(selectUserId)&&selectUserId!="-1")
            {
                t1 += string.Format(" and tb_EForm.createPer="+selectUserId);
                t2 += string.Format(" and tb_EForm.createPer=" + selectUserId);
            }
            
           
            
            var sqlInfo = new StringBuilder();
                   
            //派工单
            sqlInfo.AppendFormat(@"select tb_EForm.e_no,tb_User.loginName,tb_EForm.allE_id , tb_EForm.Id from  tb_EForm 
left join tb_EFormS on tb_EForm.id=tb_EFormS.e_id
left join tb_User on tb_User.id=tb_EForm.createPer
where proId=1 and state='执行中' and tb_EForms.id is not null "+t1);

            //客户联系单子
            sqlInfo.AppendFormat(@"select tb_EForm.e_no,tb_User.loginName,tb_EForm.allE_id , tb_EForm.Id from  tb_EForm 
left join tb_EFormS on tb_EForm.id=tb_EFormS.e_id
left join tb_User on tb_User.id=tb_EForm.createPer
where proId=2 and state='执行中' and tb_EForms.id is not null "+t2);
            return sqlInfo;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            OutExcelToList("");
        }

        /// <summary>
        /// 导出售票明细报表
        /// 添加人：冯建
        /// 添加时间：2012-12-26
        /// </summary>
        /// <param name="ticketSalesRecordList">数据源</param>
        /// <param name="sheetName">EXCEL标题名称</param>
        private void OutExcelToList(string sheetName)
        {
            var hssfworkbook = new HSSFWorkbook();
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;

            //超链接的单元格风格
            //超链接默认的是蓝色底边框
            var hlink_style = hssfworkbook.CreateCellStyle();
            var hlink_font = hssfworkbook.CreateFont();
            hlink_font.Color = HSSFColor.BLUE.index;
            hlink_font.Underline = 0;
            hlink_style.SetFont(hlink_font);

            //设置单元格式-居中
            var stylecenter = hssfworkbook.CreateCellStyle();
            stylecenter.Alignment = HorizontalAlignment.CENTER;
            stylecenter.VerticalAlignment = VerticalAlignment.CENTER;
            stylecenter.BorderTop = CellBorderType.THIN;
            stylecenter.BorderBottom = CellBorderType.THIN;
            stylecenter.BorderLeft = CellBorderType.THIN;
            stylecenter.BorderRight = CellBorderType.THIN;
            stylecenter.TopBorderColor = IndexedColors.BLACK.Index;
            stylecenter.BottomBorderColor = IndexedColors.BLACK.Index;
            stylecenter.LeftBorderColor = IndexedColors.BLACK.Index;
            stylecenter.RightBorderColor = IndexedColors.BLACK.Index;

            string url1 = Request.Url.Scheme + "://" + Request.Url.Authority + "/EFrom/Dispatching.aspx?ProId=1&allE_id={0}&EForm_Id={1}";
            string url2 = Request.Url.Scheme + "://" + Request.Url.Authority + "/EFrom/BusContact.aspx?ProId=2&allE_id={0}&EForm_Id={1}";
          
           
            DataSet ds = new DataSet();
            using (DbConnection objConnection = DBHelp.getConn())
            {

                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();            
               

                for (int ii = 0; ii < ddlUser.Items.Count; ii++)
                {
                    var name = ddlUser.Items[ii].Text;
                    var userId = ddlUser.Items[ii].Value;
                    if(userId=="-1")
                    {
                        continue;
                    }

                    #region 创建工作簿
                    var sheet = hssfworkbook.CreateSheet(name);
                    sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2));
                    sheet.CreateRow(0).CreateCell(0).SetCellValue(name + " 人事考核");
                    var cell = sheet.GetRow(0).GetCell(0);
                    cell.CellStyle = stylecenter;
                    var columns = new List<string>
                              {
                                  "派工单列表",                                 
                                  "外出业务联系单列表",
                              };
                    //添加列名
                    sheet.CreateRow(2);

                    objCommand.CommandText = GetSql(userId, name).ToString();
                    DbDataAdapter objApater = DBHelp.GetProviderFactory().CreateDataAdapter();
                    var myDS = new DataSet();
                    objApater.SelectCommand = objCommand;
                    objApater.Fill(myDS);

                    //var myDS = DBHelp.getDataSet(GetSql(userId, name).ToString());
                    var dataTable1 = myDS.Tables[0];
                    var dataTable2 = myDS.Tables[1];
                   
                    int maxRows = dataTable1.Rows.Count;
                    if (dataTable2.Rows.Count > maxRows)
                    {
                        maxRows = dataTable2.Rows.Count;
                    }
                    
                    var dt1Count = dataTable1.Rows.Count;
                    var dt2Count = dataTable2.Rows.Count;
                   
                    for (var i = 0; i < columns.Count; i++)
                    {
                        sheet.GetRow(2).CreateCell(i).SetCellValue(columns[i]);
                        cell = sheet.GetRow(2).GetCell(i);
                        cell.CellStyle = stylecenter;
                    }
                    for (var i = 0; i < maxRows; i++)
                    {
                        sheet.CreateRow(3 + i);

                        var text1 = dt1Count > i ? dataTable1.Rows[i][0].ToString() : "";
                        var text2 = dt2Count > i ? dataTable2.Rows[i][0].ToString() : "";
                        sheet.GetRow(3 + i).CreateCell(0).SetCellValue(text1);
                        if (text1 != "")
                        {
                            var link1 = new HSSFHyperlink(HyperlinkType.URL) { Address = string.Format(url1, dataTable1.Rows[i][2], dataTable1.Rows[i][3]) };
                            sheet.GetRow(3 + i).GetCell(0).Hyperlink = link1;
                            sheet.GetRow(3 + i).GetCell(0).CellStyle = hlink_style;
                        }

                        //sheet.GetRow(3 + i).CreateCell(1).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(1).SetCellValue(text2);
                        if (text2 != "")
                        {
                            var link2 = new HSSFHyperlink(HyperlinkType.URL) { Address = string.Format(url2, dataTable2.Rows[i][2], dataTable2.Rows[i][3]) };
                            sheet.GetRow(3 + i).GetCell(1).Hyperlink = link2;
                            sheet.GetRow(3 + i).GetCell(1).CellStyle = hlink_style;

                        }

                        for (var j = 0; j < columns.Count; j++)
                        {
                            cell = sheet.GetRow(3 + i).GetCell(j);
                            cell.CellStyle = stylecenter;
                        }
                    }
                   
                    for (var i = 0; i < columns.Count; i++)
                    {
                        
                        sheet.SetColumnWidth(i, 25 * 256);
                    }
                    sheet.ForceFormulaRecalculation = true;
                    #endregion
                }
                objConnection.Close();
            }
            WriteToFile(hssfworkbook, "人事考核");
        }

        /// <summary>
        /// 向页面输出excel文件
        /// 添加人：冯建
        /// 添加时间：2012-12-26
        /// </summary>
        /// <param name="hssfworkbook"></param>
        /// <param name="excelName">Excel的名称</param>
        private void WriteToFile(HSSFWorkbook hssfworkbook, string excelName)
        {
            var rootPath = Server.MapPath("~/UploadFiles/");
            var _title = "/" + excelName + ".xls";
            if (!string.IsNullOrEmpty(Request.Browser.Browser))
            {
                if (Request.Browser.Browser.ToLower().IndexOf("ie") >= 0)
                {
                    _title = Server.UrlEncode(_title);
                }
            }
            //Write the stream data of workbook to the root directory
            var fs = new FileStream(rootPath + "/" + excelName + ".xls", FileMode.Create, FileAccess.ReadWrite);
            hssfworkbook.Write(fs);
            hssfworkbook.Dispose();
            fs.Close();
            //把文件以流方式指定xls格式提供下载
            fs = System.IO.File.OpenRead(rootPath + "/" + excelName + ".xls");
            var FileArray = new byte[fs.Length];
            fs.Read(FileArray, 0, FileArray.Length);
            fs.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + _title);
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Length", FileArray.Length.ToString());
            Response.BinaryWrite(FileArray);
            Response.Flush();
            Response.End();
            Response.Clear();
        }

    }
}
