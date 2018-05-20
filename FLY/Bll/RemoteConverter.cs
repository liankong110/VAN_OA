using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.IO;
using System.Data;

namespace VAN_OA.Bll
{
    public class RemoteConverter
    {
        object paramMissing = Type.Missing;
        public string errormessage;
        private bool wordavailable=false;
        private bool checkedword = false;

        public bool WordIsAvailable()
        {
            //don't check every time.... first time only
            if (!checkedword)
            {

                try
                {
                    ApplicationClass wordApplication = new ApplicationClass();
                    wordApplication.Visible = true;
                    wordApplication.Quit(ref paramMissing, ref paramMissing,
                                         ref paramMissing);
                    wordavailable = true;
                }
                catch
                {
                    wordavailable = false;
                }
                checkedword = true;
            }
            return wordavailable;
          
        }

        public bool convert(string source, string output)
        {
            if (System.IO.File.Exists(source))
            {
                //start conversion
                try
                {
                    ApplicationClass wordApplication = new ApplicationClass();

                    Document wordDocument = null;
                    object paramSourceDocPath = source;

                    string paramExportFilePath = output;

                    //this part is copied from Microsoft MSDN

                    //set exportformat to pdf
                    WdExportFormat paramExportFormat = WdExportFormat.wdExportFormatPDF;
                    bool paramOpenAfterExport = false;
                    WdExportOptimizeFor paramExportOptimizeFor =
                        WdExportOptimizeFor.wdExportOptimizeForPrint;
                    WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
                    int paramStartPage = 0;
                    int paramEndPage = 0;
                    WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
                    bool paramIncludeDocProps = true;
                    bool paramKeepIRM = true;
                    WdExportCreateBookmarks paramCreateBookmarks =
                        WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                    bool paramDocStructureTags = true;
                    bool paramBitmapMissingFonts = true;
                    bool paramUseISO19005_1 = false;

                    try
                    {
                        // Open the source document.
                        wordDocument = wordApplication.Documents.Open(
                            ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing, ref paramMissing, ref paramMissing,
                            ref paramMissing);

                        // Export it in the specified format.
                        if (wordDocument != null)
                            wordDocument.ExportAsFixedFormat(paramExportFilePath,
                                                             paramExportFormat, paramOpenAfterExport,
                                                             paramExportOptimizeFor, paramExportRange, paramStartPage,
                                                             paramEndPage, paramExportItem, paramIncludeDocProps,
                                                             paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                                                             paramBitmapMissingFonts, paramUseISO19005_1,
                                                             ref paramMissing);
                    }
                    catch (Exception ex)
                    {
                        // Respond to the error
                        errormessage=ex.Message;
                    }
                    finally
                    {
                        // Close and release the Document object.
                        if (wordDocument != null)
                        {
                            wordDocument.Close(ref paramMissing, ref paramMissing,
                                               ref paramMissing);
                            wordDocument = null;
                        }

                        // Quit Word and release the ApplicationClass object.
                        if (wordApplication != null)
                        {
                            wordApplication.Quit(ref paramMissing, ref paramMissing,
                                                 ref paramMissing);
                            wordApplication = null;
                        }

                        //i don't know why this is here two times. I just copied it from the MSDN howto interop with word 2007
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
                catch (Exception err)
                {
                    errormessage=err.Message;
                }
                    return true;

            }
            else
            {
                errormessage="ERROR: Inputfile not found";
            }

            return false;

        }

        public string PrintPDF(int PDFId, out string QPNo, out string GuidNO, string modelWordUrl, string wordUrl)
        {
            //ServiceAppSetting.LoggerHander("开始转换", "");
            QPNo = "";
            GuidNO = "";
            //string modelWordUrl = System.Web.HttpContext.Current.Request.MapPath("WordModel/") + "QP.doc";
            //if (!File.Exists(modelWordUrl))
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('模版不存在！');</script>");
            //    return "";
            //}
            tb_QuotePriceService QuotePriSer = new tb_QuotePriceService();

            VAN_OA.Model.EFrom.tb_QuotePrice model =QuotePriSer.GetModel(PDFId);
            //ServiceAppSetting.LoggerHander("获取实体", "");
            if (model.QPType == 2)//不含税 英文模版
            {

                modelWordUrl = modelWordUrl.Replace("QP","EnQP");
            }
            
            GuidNO = model.QuoteNo;
            
            string guidUrl = wordUrl + GuidNO + ".doc";
            if (!File.Exists(guidUrl))
            {
                File.Copy(modelWordUrl, guidUrl);
            }
            else
            {
                return guidUrl;
            }
            //ServiceAppSetting.LoggerHander("复制文件", "");
            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();

            object oMissing = System.Reflection.Missing.Value;
            //打开模板文档，并指定doc的文档类型 
            object objTemplate = guidUrl;
            //路径一定要正确            
            object objDocType = WdDocumentType.wdTypeDocument;
            object objfalse = false;
            object objtrue = true;
            object miss = System.Reflection.Missing.Value;
            object missingValue = Type.Missing;
            object doNotSaveChanges = WdSaveOptions.wdDoNotSaveChanges;
            Microsoft.Office.Interop.Word.Document doc = null;
            try
            {
                try
                {
                    //ServiceAppSetting.LoggerHander("开始创建模版", "");
                    doc = (Document)appWord.Documents.Add(ref objTemplate, ref objfalse, ref objDocType, ref objtrue);
                    //ServiceAppSetting.LoggerHander("结束创建模版", "");
                }
                catch (Exception ex)
                {

                    ServiceAppSetting.LoggerHander("创建模版-》"+ex.ToString(), "");
                }

                //获取模板中所有的书签 
                Bookmarks odf = doc.Bookmarks;


                QPNo = model.QuoteNo;
                Dictionary<string, string> bookMarks = new Dictionary<string, string>();
                bookMarks.Add("QuoteNo", model.QuoteNo);
                bookMarks.Add("QuoteNo1", model.QuoteNo);
                bookMarks.Add("GuestNo", model.GuestNo);
                bookMarks.Add("QuoteDate", model.QuoteDate.ToString("yyyy-MM-dd"));
                bookMarks.Add("ResultGuestName", model.ResultGuestName);
                bookMarks.Add("GuestName2", model.GuestName);
                bookMarks.Add("ResultGuestNo", model.ResultGuestNo);
                bookMarks.Add("PayStyle", model.PayStyle);
                bookMarks.Add("ContactPerToInv", model.ContactPerToInv);
                bookMarks.Add("NaShuiPer", model.NaShuiPer);
                //telToInv
                bookMarks.Add("telToInv", model.telToInv);
                //InvoHeader
                bookMarks.Add("InvoHeader", model.InvoHeader);
                //brandNo
                bookMarks.Add("brandNo", model.brandNo);
                //GuestNameToInv
                bookMarks.Add("GuestNameToInv", model.GuestNameToInv);
                //InvAddress
                bookMarks.Add("InvAddress", model.InvAddress);
                //AddressTofa
                bookMarks.Add("AddressTofa", model.AddressTofa);
                //AddressToInv
                bookMarks.Add("AddressToInv", model.AddressToInv);
                //InvTel
                bookMarks.Add("InvTel", model.InvTel);
                //InvContactPer
                bookMarks.Add("InvContactPer", model.InvContactPer);
                //BuessName
                bookMarks.Add("BuessName", model.BuessName);
                //ComBusTel
                bookMarks.Add("ComBusTel", model.ComBusTel);
                //ComTel
                bookMarks.Add("ComTel", model.ComTel);
                //BuessEmail
                bookMarks.Add("BuessEmail", model.BuessEmail);
                //ComChuanZhen
                bookMarks.Add("ComChuanZhen", model.ComChuanZhen);

                //ZLBZ
                bookMarks.Add("ZLBZ", model.ZLBZ);
                //YSBJ
                bookMarks.Add("YSBJ", model.YSBJ);
                //FWBXDJ
                bookMarks.Add("FWBXDJ", model.FWBXDJ);
                //JFQ
                bookMarks.Add("JFQ", model.JFQ);
                //ComName
                bookMarks.Add("ComName", model.ComName);
                //ComBrand
                bookMarks.Add("ComBrand", model.ComBrand);
                //ComBrand
                bookMarks.Add("Zhanghao", model.Address);
                //NaShuiNo
                bookMarks.Add("NaShuiNo", model.NaShuiNo);

                if (model.IsYH)
                {
                    //NaShuiNo
                    bookMarks.Add("YHPrice", model.LastYH.ToString());
                }
                foreach (var key in bookMarks.Keys)
                {
                    try
                    {
                        object objKey = key;
                        doc.Bookmarks.get_Item(ref objKey).Range.Text = bookMarks[key];
                    }
                    catch (Exception)
                    {


                    }
                }
                //ServiceAppSetting.LoggerHander("书签 赋值", "");
                ////修改图片
                //object YHPrice = "YHPrice";
                //var xShape =doc.Bookmarks.get_Item(ref YHPrice).Range.InlineShapes.AddPicture(@"C:\Users\fj3174\Desktop\1.bmp", false, true ) ;
              
                //doc.Bookmarks.get_Item(ref YHPrice).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight; 
                //报价内容
                Microsoft.Office.Interop.Word.Table nowTable = doc.Tables[2];

               
                Object Nothing = System.Reflection.Missing.Value;

                tb_QuotePrice_InvDetailsService invDetailSer = new tb_QuotePrice_InvDetailsService();
                List<tb_QuotePrice_InvDetails> invDetails = invDetailSer.GetListArray(" QuoteId=" + PDFId);

                //invDetails.Add(new tb_QuotePrice_InvDetails { });
                decimal total = 0;

                for (int i = 0; i < invDetails.Count; i++)
                {
                    invDetails[i].No = i + 1;
                    total += invDetails[i].Total;                   
                    nowTable.Rows.Add(ref Nothing);

                }


            
                int rowsCount = nowTable.Rows.Count;
                int columnCount = nowTable.Columns.Count;
                ServiceAppSetting.LoggerHander("明细总数" + rowsCount, "");
                decimal fax=Convert.ToDecimal(1.17);
                decimal noHanShui = 0;
                if (invDetails.Count > 0)
                {
                    for (int rowPos = 2; rowPos <= rowsCount; rowPos++)
                    {

                        try
                        {
                            if (model.QPType == 2)
                            {
                                nowTable.Cell(rowPos, 1).Range.Text = invDetails[rowPos - 2].No.ToString();
                                nowTable.Cell(rowPos, 2).Range.Text = invDetails[rowPos - 2].InvName.ToString();
                                nowTable.Cell(rowPos, 3).Range.Text = invDetails[rowPos - 2].InvModel.ToString();
                                nowTable.Cell(rowPos, 4).Range.Text = invDetails[rowPos - 2].InvUnit.ToString();
                                nowTable.Cell(rowPos, 5).Range.Text = string.Format("{0:n0}", invDetails[rowPos - 2].InvNum);
                                nowTable.Cell(rowPos, 6).Range.Text = string.Format("{0:n2}", (invDetails[rowPos - 2].InvPrice / fax));
                                noHanShui += (invDetails[rowPos - 2].InvPrice / fax) * invDetails[rowPos - 2].InvNum;
                                nowTable.Cell(rowPos, 7).Range.Text = string.Format("{0:n2}", invDetails[rowPos - 2].InvPrice);
                                nowTable.Cell(rowPos, 8).Range.Text = string.Format("{0:n2}", invDetails[rowPos - 2].Total);
                                if (model.IsBrand)
                                {
                                    nowTable.Cell(rowPos, 9).Range.Text = invDetails[rowPos - 2].GoodBrand.ToString();//品牌
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 9).Range.Text = "";//品牌
                                }
                                if (model.IsProduct)
                                {
                                    nowTable.Cell(rowPos, 10).Range.Text = invDetails[rowPos - 2].Product.ToString();//产地
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 10).Range.Text ="";//产地
                                    
                                }
                                if (model.IsRemark)
                                {
                                    nowTable.Cell(rowPos, 11).Range.Text = invDetails[rowPos - 2].InvRemark.ToString();//备注
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 11).Range.Text = "";//备注

                                }
                            }
                            else
                            {
                                nowTable.Cell(rowPos, 1).Range.Text = invDetails[rowPos - 2].No.ToString();
                                nowTable.Cell(rowPos, 2).Range.Text = invDetails[rowPos - 2].InvName.ToString();
                                nowTable.Cell(rowPos, 3).Range.Text = invDetails[rowPos - 2].InvModel.ToString();
                                nowTable.Cell(rowPos, 4).Range.Text = invDetails[rowPos - 2].InvUnit.ToString();
                                nowTable.Cell(rowPos, 5).Range.Text = string.Format("{0:n0}", invDetails[rowPos - 2].InvNum);
                                nowTable.Cell(rowPos, 6).Range.Text = string.Format("{0:n2}", invDetails[rowPos - 2].InvPrice);
                                nowTable.Cell(rowPos, 7).Range.Text = string.Format("{0:n2}", invDetails[rowPos - 2].Total);
                                if (model.IsBrand)
                                {
                                    nowTable.Cell(rowPos, 8).Range.Text = invDetails[rowPos - 2].GoodBrand.ToString();
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 8).Range.Text = "";//品牌
                                }
                                if (model.IsProduct)
                                {
                                    nowTable.Cell(rowPos, 9).Range.Text = invDetails[rowPos - 2].Product.ToString();
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 9).Range.Text = "";//产地

                                }
                                if (model.IsRemark)
                                {
                                    nowTable.Cell(rowPos, 10).Range.Text = invDetails[rowPos - 2].InvRemark.ToString();
                                }
                                else
                                {
                                    nowTable.Cell(rowPos, 10).Range.Text = "";//备注

                                }
                            }
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

                ServiceAppSetting.LoggerHander("明细赋值", "");
                //合计

                if (model.QPType == 3)//工程 加三行
                {
                    //材料小计
                    nowTable.Rows.Add(ref Nothing);
                  
                    nowTable.Cell(rowsCount, 6).Range.Text = "材料小计";
                    nowTable.Cell(rowsCount, 7).Range.Text = total.ToString();
                    //人工费用
                    nowTable.Rows.Add(ref Nothing);
                    rowsCount++;
                    nowTable.Cell(rowsCount, 6).Range.Text = "人工费用";
                    nowTable.Cell(rowsCount, 7).Range.Text =model.LaborCost.ToString();
                    nowTable.Cell(rowsCount, 7).Range.Shading.ForegroundPatternColor = WdColor.wdColorYellow;//背景颜色
                    total += model.LaborCost;   
                    //工程计税
                    nowTable.Rows.Add(ref Nothing);
                    rowsCount++;
                    nowTable.Cell(rowsCount, 6).Range.Text = "工程计税";
                    nowTable.Cell(rowsCount, 7).Range.Text =model.EngineeringTax.ToString();
                    nowTable.Cell(rowsCount, 7).Range.Shading.ForegroundPatternColor = WdColor.wdColorYellow;//背景颜色
                    total += model.EngineeringTax;   
                }

                if (model.QPType == 2)//不含税 英文格式 加1行
                {                 
                    Microsoft.Office.Interop.Word.Cell cell1 = nowTable.Cell(rowsCount + 1, 2);//2行1列合并2行2列为一起
                    cell1.Merge(nowTable.Cell(rowsCount + 1, 7));
                    nowTable.Cell(rowsCount + 1, 2).Range.Text = "总计人民币（不含税）：  " + string.Format("CNY {0:n2}", noHanShui) + System.Environment.NewLine+
                        "总计人民币增值税：      " + string.Format("CNY {0:n2}", (total-noHanShui));
                    nowTable.Cell(rowsCount + 1, 2).LeftPadding = 2;                    
                    nowTable.Cell(rowsCount + 1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft; 
                    rowsCount++;
                    nowTable.Rows.Add(ref Nothing);
                }
                if (model.QPType != 2)
                {
                    Microsoft.Office.Interop.Word.Cell cell = nowTable.Cell(rowsCount + 1, 2);//2行1列合并2行2列为一起
                    cell.Merge(nowTable.Cell(rowsCount + 1, 6));

                    nowTable.Cell(rowsCount + 1, 2).Range.Text = "总计人民币（含税）：";

                    nowTable.Cell(rowsCount + 1, 2).LeftPadding = 2;
                    float size = 10.5f;
                    //app.Selection.Font.Size
                    nowTable.Cell(rowsCount + 1, 2).Range.Font.Size = size;
                    nowTable.Cell(rowsCount + 1, 3).Range.Font.Size = size;
                    nowTable.Cell(rowsCount + 1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    nowTable.Cell(rowsCount + 1, 3).Range.Text = string.Format("CNY {0:n2}", total);

                    nowTable.Cell(rowsCount + 1, 2).Range.Bold = 2;//设置单元格中字体为粗体
                    nowTable.Cell(rowsCount + 1, 3).Range.Bold = 2;//设置单元格中字体为粗体
                }
                else
                {
                    nowTable.Cell(rowsCount + 1, 2).Range.Text = "总计人民币（含税）：      " + string.Format("CNY {0:n2}", total); 

                    nowTable.Cell(rowsCount + 1, 2).LeftPadding = 2;
                    float size = 10.5f;
                    //app.Selection.Font.Size
                    nowTable.Cell(rowsCount + 1, 2).Range.Font.Size = size;
                    //nowTable.Cell(rowsCount + 1, 3).Range.Font.Size = size;
                    nowTable.Cell(rowsCount + 1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    //nowTable.Cell(rowsCount + 1, 3).Range.Text = string.Format("CNY {0:n2}", total);

                    nowTable.Cell(rowsCount + 1, 2).Range.Bold = 2;//设置单元格中字体为粗体
                    //nowTable.Cell(rowsCount + 1, 3).Range.Bold = 2;//设置单元格中字体为粗体
                }

                if (model.IsYH)//增加最后优惠信息
                {
                    //人工费用
                    nowTable.Rows.Add(ref Nothing);

                    rowsCount++;

                    Microsoft.Office.Interop.Word.Cell cell = nowTable.Cell(rowsCount + 1, 2);//2行1列合并2行2列为一起
                    //cell.Merge(nowTable.Cell(rowsCount + 1, 6));
                    if (model.QPType == 2)
                    {
                        nowTable.Cell(rowsCount + 1, 2).Range.Text = "最终优惠价（含税）：      " + string.Format("CNY {0:n2}", model.LastYH); 
                    }
                    else
                    {
                        nowTable.Cell(rowsCount + 1, 2).Range.Text = "最终优惠价（含税）：";
                    }

                    nowTable.Cell(rowsCount + 1, 2).LeftPadding = 2;
                    float size = 10.5f;
                    //app.Selection.Font.Size
                    nowTable.Cell(rowsCount + 1, 2).Range.Font.Size = size;
                    nowTable.Cell(rowsCount + 1, 3).Range.Font.Size = size;
                    nowTable.Cell(rowsCount + 1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    if (model.QPType != 2)
                    {
                        nowTable.Cell(rowsCount + 1, 3).Range.Text = string.Format("CNY {0:n2}", model.LastYH);
                    }
                    nowTable.Cell(rowsCount + 1, 2).Range.Bold = 2;//设置单元格中字体为粗体
                    nowTable.Cell(rowsCount + 1, 3).Range.Bold = 2;//设置单元格中字体为粗体
                }
                ServiceAppSetting.LoggerHander("合计赋值", "");
                //object objKey_Total = "Total";
                //doc.Bookmarks.get_Item(ref objKey_Total).Range.Text = string.Format("{0:n2}", total);

                doc.SaveAs(ref objTemplate, ref miss, ref miss, ref miss, ref miss, ref miss,
                               ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
                appWord.Application.Quit(ref miss, ref miss, ref miss);
                doc = null;
                appWord = null;

             
            }
            catch (Exception ex)
            {


                // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
                appWord.Application.Quit(ref miss, ref miss, ref miss);
                if (System.IO.File.Exists(objTemplate.ToString()))
                {
                    try
                    {
                        System.IO.File.Delete(objTemplate.ToString());
                    }
                    catch (Exception)
                    {


                    }
                }

                return "";
            }
            return objTemplate.ToString();
        }
        
    }
}
