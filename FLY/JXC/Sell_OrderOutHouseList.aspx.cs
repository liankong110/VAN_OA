using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using Microsoft.Office.Interop.Excel;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class Sell_OrderOutHouseList : BasePage
    {
        Sell_OrderOutHouseService POSer = new Sell_OrderOutHouseService();
        Sell_OrderOutHousesService ordersSer = new Sell_OrderOutHousesService();
     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
                //主单
                List<Sell_OrderOutHouse> pOOrderList = new List<Sell_OrderOutHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<Sell_OrderOutHouses> orders = new List<Sell_OrderOutHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll(SysObj.Sell_OrderOutHouseList) == false)                  
                {
                    ViewState["showAll"] = false;
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
                else
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可导出'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='销售出库列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("销售出库列表", "可导出"))
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }


                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    ddlType.Text = Request["Type"].ToString();
                    Show();
                }
            }
        }      


        private void Show()
        {
            string sql = " 1=1 ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }           

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Supplier  like '%{0}%'", txtGuestName.Text.Trim());
            }

            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and CreateUserId={0}", Session["currentUserId"]);
            //}
            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and CreateUserId={0}", ddlUser.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and CreateUserId IN(select id from tb_User where {0})", where);
            }
            if (ddlType.Text == "0")
            {
                sql += string.Format(" and exists(select id from Sell_OrderOutHouseBack where Status='通过' and SellProNo=Sell_OrderOutHouse.ProNo )");
            }
            if (ddlType.Text == "1")
            {
                sql += string.Format(" and not exists(select id from Sell_OrderOutHouseBack where Status='通过' and SellProNo=Sell_OrderOutHouse.ProNo)");
            }

            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and exists( select id from Sell_OrderOutHouses left join TB_Good on Sell_OrderOutHouses.GooId=GoodId where Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id and GoodNo like '%{0}%')", txtGoodNo.Text);
            }
            #region 项目查询
            string ponoSql = "";
           
            if (CheckBox1.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue='{0}'", CheckBox1.Text);
            }
            if (CheckBox2.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue2='{0}'", CheckBox2.Text);
            }
            if (CheckBox3.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue3='{0}'", CheckBox3.Text);
            }
            if (CheckBox4.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue4='{0}'", CheckBox4.Text);
            }

            if (CheckBox5.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue5='{0}'", CheckBox5.Text);
            }

            if (CheckBox6.Checked)
            {
                ponoSql += string.Format(" and CG_POOrder.POStatue6='{0}'", CheckBox6.Text);
            }
            if (ddlClose.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsClose={0} ", ddlClose.Text);
            }
            if (ddlIsSelect.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsSelected={0} ", ddlIsSelect.Text);
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.JieIsSelected={0} ", ddlJieIsSelected.Text);
            }
            if (ddlIsSpecial.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsSpecial={0} ", ddlIsSpecial.Text);
            }

            if (ddlHanShui.Text != "-1")
            {
                ponoSql += string.Format(" and CG_POOrder.IsPoFax={0} ", ddlHanShui.Text);
            }
            if (ddlPrice.Text == "1")
            {
                ponoSql += " and Order_ToInvoice_1.POTotal-isnull(TuiTotal,0)>0";
            }
            if (ddlPrice.Text == "0")
            {
                ponoSql += " and Order_ToInvoice_1.POTotal-isnull(TuiTotal,0)=0";
            }
            if (ponoSql != "")
            {
                sql += string.Format(" and EXISTS (SELECT 1 FROM CG_POOrder ");
                if (ddlPrice.Text != "-1")
                {
                    sql += " left join Order_ToInvoice_1 on CG_POOrder.PONo=Order_ToInvoice_1.PONo ";
                }

                sql += string.Format("   WHERE  IFZhui=0 and Sell_OrderOutHouse.PONO=CG_POOrder.PONo  {0})", ponoSql);

            }

            #endregion

            List<Sell_OrderOutHouse> pOOrderList = this.POSer.GetListArray(sql);
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();
            var total= POSer.GetAllTotal(sql);
            lblAllTotal.Text = total[0].ToString();
            lblAllSellTotal.Text = total[1].ToString();
            //子单
            List<Sell_OrderOutHouses> orders = new List<Sell_OrderOutHouses>();
            gvList.DataSource = orders;
            gvList.DataBind();


            lblTotal.Text = "0";
            lblPriceTotal.Text = "0";
           

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                List<Sell_OrderOutHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderOutHouses.id=" + e.CommandArgument);               
               
                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();            
            }
            if (e.CommandName == "print")
            {

                var model = new Sell_OrderOutHouseService().GetModel(Convert.ToInt32(e.CommandArgument));
                if (model.Status == "通过")
                {
                    string url = string.Format("NewWFSell_OrderOutHousePrint.aspx?Id={0}", e.CommandArgument);

                    Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('该单据尚未通过!！');</script>"));
                    
                }
            }
        }



        Sell_OrderOutHouses SumOrders = new Sell_OrderOutHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderOutHouses model = e.Row.DataItem as Sell_OrderOutHouses;
                 
                SumOrders.Total += model.Total;
                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;

            }
            


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as System.Web.UI.WebControls.Label, "合计");//合计                      
                setValue(e.Row.FindControl("lblTotal") as System.Web.UI.WebControls.Label, SumOrders.Total.ToString());//成本总额    
                setValue(e.Row.FindControl("lblTotal1") as System.Web.UI.WebControls.Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额    
                lblTotal.Text = SumOrders.Total.ToString();
                lblPriceTotal.Text = SumOrders.GoodSellPriceTotal.ToString();
            
            }

        }


        private void setValue(System.Web.UI.WebControls.Label control, string value)
        {
            control.Text = value;
        }

        public string xlfile = "Sales_OutPut.xls";
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlStatue.Text != "通过")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单状态必须选择通过！');</script>");
                 
                return;
            }
            string sql = @"select  Sell_OrderOutHouse.RuTime as '出库日期',ProNo as '出库单号',Supplier as '客户名称'
,GoodName+' '+GoodTypeSmName+' '+GoodSpec+' '+GoodModel+' ' as '商品',GoodUnit as '单位',
GoodNum as '数量',GoodSellPrice as '出库单价',GoodNum*GoodSellPrice as '销售额',
 GoodPrice as '单价成本'  ,GoodNum*GoodPrice as '总成本',GoodNum*GoodSellPrice-GoodNum*GoodPrice 
as '毛利润额',FPNo as '发票号',DoPer as '经手人'
 from Sell_OrderOutHouse  left join Sell_OrderOutHouses on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id
left join TB_Good on TB_Good.GoodId=Sell_OrderOutHouses.GooId where 1=1 ";

            if (txtPONo.Text != "")
            {
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text);
            }


            if (ttxPOName.Text != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text);
            }

            if (txtFrom.Text != "")
            {
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }


            if (txtProNo.Text != "")
            {
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text);
            }

            if (txtGuestName.Text != "")
            {
                sql += string.Format(" and Supplier  like '%{0}%'", txtGuestName.Text);
            }

            if (ViewState["showAll"] != null)
            {
                sql += string.Format(" and Sell_OrderOutHouse.CreateUserId={0}", Session["currentUserId"]);
            }
            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and Sell_OrderOutHouse.CreateUserId={0}", ddlUser.Text);
            }
            System.Data.DataTable dt = DBHelp.getDataTable(sql);
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + xlfile);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = dt;
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();

           // OutputExcel(dt.DefaultView, xlfile);

        }

        public void OutputExcel(DataView dv, string str)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            //dv为要输出到Excel的数据，str为标题名称
            GC.Collect();
            Application excel;// = new Application();
            int rowIndex = 2;
            int colIndex = 0;
            _Workbook xBk;
            _Worksheet xSt;
            excel = new ApplicationClass();
            xBk = excel.Workbooks.Add(true);
            xSt = (_Worksheet)xBk.ActiveSheet;
            //
            //取得标题
            //
            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                excel.Cells[2, colIndex] = col.ColumnName;
                xSt.get_Range(excel.Cells[2, colIndex], excel.Cells[4, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置标题格式为居中对齐
            }
            //
            //取得表格中的数据
            //

            decimal total = 0;
            decimal sellTotal = 0;
            decimal lirunTotal = 0;
            foreach (DataRowView row in dv)
            {
                if (!(row["总成本"] is DBNull))
                {
                    total += Convert.ToDecimal(row["总成本"]);
                }
                if (!(row["销售额"] is DBNull))
                {
                    sellTotal += Convert.ToDecimal(row["销售额"]);
                }
                if (!(row["毛利润额"] is DBNull))
                {
                    lirunTotal += Convert.ToDecimal(row["毛利润额"]);
                }
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in dv.Table.Columns)
                {
                    colIndex++;
                    if (col.DataType == System.Type.GetType("System.DateTime"))
                    {
                        excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
                        xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
                    }
                    else
                        if (col.DataType == System.Type.GetType("System.String"))
                        {
                            excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
                            xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
                        }
                        else
                        {
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }
                }
            }
            //
            //加载一个合计行
            //
            int rowSum = rowIndex + 1;
            int colSum = 1;
            excel.Cells[rowSum, 1] = "合计";
            excel.Cells[rowSum, 8] = sellTotal.ToString();
            excel.Cells[rowSum, 10] = total.ToString();
            excel.Cells[rowSum, 11] = lirunTotal.ToString();
            xSt.get_Range(excel.Cells[rowSum, 1], excel.Cells[rowSum, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //
            //设置选中的部分的颜色
            //
            xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Select();
            xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Interior.ColorIndex = 19;//设置为浅黄色，共计有56种
            //
            //取得整个报表的标题
            //
            excel.Cells[1, 1] = str;
            //
            //设置整个报表的标题格式
            //
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Bold = true;
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 16;
            //
            //设置报表表格为最适应宽度
            //
            xSt.get_Range(excel.Cells[2, 1], excel.Cells[rowSum, colIndex]).Select();
            xSt.get_Range(excel.Cells[2, 1], excel.Cells[rowSum, colIndex]).Columns.AutoFit();
            //
            //设置整个报表的标题为跨列居中
            //
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[1, colIndex]).Select();
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
            //
            //绘制边框
            //
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[rowSum, 2]).Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThick;//设置左边线加粗
            //xSt.get_Range(excel.Cells[2, 2], excel.Cells[4, colIndex]).Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThick;//设置上边线加粗
            //xSt.get_Range(excel.Cells[2, colIndex], excel.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThick;//设置右边线加粗
            //xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, colIndex]).Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThick;//设置下边线加粗
            //
            //显示效果
            //
            excel.Visible = true;
            //xSt.Export(Server.MapPath(".")+"\\"+this.xlfile.Text+".xls",SheetExportActionEnum.ssExportActionNone,Microsoft.Office.Interop.OWC.SheetExportFormat.ssExportHTML);
            xBk.SaveCopyAs(Server.MapPath(".") + "\\" + this.xlfile + ".xls");
            dv = null;
            xBk.Close(false, null, null);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
            GC.Collect();
            string path = Server.MapPath(this.xlfile + ".xls");
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            Response.Clear();
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            Response.AddHeader("Content-Length", file.Length.ToString());
            // 指定返回的是一个不能被客户端读取的流，必须被下载
            Response.ContentType = "application/ms-excel";
            // 把文件流发送到客户端
            Response.WriteFile(file.FullName);
            // 停止页面的执行
            Response.End();
        }
    }
}
