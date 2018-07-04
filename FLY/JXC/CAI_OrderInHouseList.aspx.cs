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
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class CAI_OrderInHouseList : BasePage
    {
        CAI_OrderInHouseService POSer = new CAI_OrderInHouseService();
        CAI_OrderInHousesService ordersSer = new CAI_OrderInHousesService();
        Dictionary<int, bool> CAIInfo_HanShui = new Dictionary<int, bool>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                //加载基本信息
                ddlNumber.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlRow.Items.Add(new ListItem { Text = "全部", Value = "" });
                ddlCol.Items.Add(new ListItem { Text = "全部", Value = "" });
                //货架号：1.全部  缺省 2….51 1,..50 
                for (int i = 1; i < 51; i++)
                {
                    ddlNumber.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    //层数：1.全部  缺省 2….21 1,2,3…20 
                    //部位：1.全部  缺省 2….21 1,2,3…20
                    if (i <= 21)
                    {
                        ddlRow.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                        ddlCol.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                }

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                houseList.Insert(0, new VAN_OA.Model.BaseInfo.TB_HouseInfo());
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";

                //主单
                List<CAI_OrderInHouse> pOOrderList = new List<CAI_OrderInHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CAI_OrderInHouses> orders = new List<CAI_OrderInHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll(SysObj.CAI_OrderInHouseList) == false)                
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
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
               
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
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


            if (txtChcekProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtChcekProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and ChcekProNo like '%{0}%'", txtChcekProNo.Text.Trim());
            }

            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(" and Supplier ='{0}'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and Supplier  like '%{0}%'", txtSupplier.Text.Trim());
                }
            }
            if (ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseID={0}", ddlHouse.Text);
             }

            if (txtGoodNo.Text != "" || ddlArea.Text != "" || ddlNumber.Text != ""||
                ddlRow.Text != "" || ddlCol.Text != "")
            {

                sql += " and exists( select id from CAI_OrderInHouses left join TB_Good on CAI_OrderInHouses.GooId=GoodId where CAI_OrderInHouse.Id=CAI_OrderInHouses.Id ";
                if (txtGoodNo.Text != "")
                {
                    sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
                }
                if (ddlArea.Text != "")
                {
                    sql += string.Format(" and GoodArea='{0}'", ddlArea.Text);
                }
                if (ddlNumber.Text != "")
                {
                    sql += string.Format(" and GoodNumber='{0}'", ddlNumber.Text);
                }
                if (ddlRow.Text != "")
                {
                    sql += string.Format(" and GoodRow='{0}'", ddlRow.Text);
                }
                if (ddlCol.Text != "")
                {
                    sql += string.Format(" and GoodCol='{0}'", ddlCol.Text);
                }
                sql += ")";
            }
            
            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and (CAI_OrderInHouse.CreateUserId={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AppName={0}))", ddlUser.Text);
                
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and (CAI_OrderInHouse.CreateUserId IN(select id from tb_User where {0})  or exists(select id from CG_POOrder where IFZhui=0 and CG_POOrder.PONo=CAI_OrderInHouse.PONo and AppName IN(select id from tb_User where {0})))", where);
            }
            if (ddlIsHanShui.Text == "1")
            {
                sql += string.Format(" and tb2.IsHanShui=tb2.allCount");
            }
            if (ddlIsHanShui.Text == "0")
            {
                sql += string.Format(" and tb2.IsHanShui<>tb2.allCount");
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format("and exists(select id from CG_POOrder where Status='通过' and  Model='{0}' and CG_POOrder.PONO=CAI_OrderInHouse.PONO ) ", ddlModel.Text);
            }
            List<CAI_OrderInHouse> pOOrderList = this.POSer.GetListArray(sql);

           
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<CAI_OrderInHouses> orders = new List<CAI_OrderInHouses>();
            gvList.DataSource = orders;
            gvList.DataBind();



           

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
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

                CAI_OrderInHouse model = e.Row.DataItem as CAI_OrderInHouse;
                if (model.Count2!= model.Count1)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                
                 CAIInfo_HanShui = POSer.GetCAI_OrderInHouse_HanShui(e.CommandArgument.ToString());
                 

                List<CAI_OrderInHouses> orders = ordersSer.GetListArray(" CAI_OrderInHouses.id=" + e.CommandArgument);
               
                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();


               
            
            }
        }



        CAI_OrderInHouses SumOrders = new CAI_OrderInHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderInHouses model = e.Row.DataItem as CAI_OrderInHouses;
                
                SumOrders.Total += model.Total;

                
                if (CAIInfo_HanShui.ContainsKey(model.Ids))
                {
                    bool hanShui = CAIInfo_HanShui[model.Ids];
                    System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                    if (lblIsHanShui != null)
                    {
                        lblIsHanShui.Text = hanShui ? "含税" : "不含税";
                    }
                    if (hanShui == false)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
           


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as System.Web.UI.WebControls.Label, "合计");//合计
                      
                setValue(e.Row.FindControl("lblTotal") as System.Web.UI.WebControls.Label, SumOrders.Total.ToString());//成本总额    
            }

        }

        private void setValue(System.Web.UI.WebControls.Label control, string value)
        {
            control.Text = value;
        }

        public string xlfile = "采购入库报表.xls";
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlStatue.Text != "通过")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单状态必须选择通过！');</script>");

                return;
            }

            string sql = @"select CAI_OrderInHouse.RuTime as '日期',ProNo as '采购单号', DoPer as '采购人',Supplier as '供应商',
GoodName+' '+GoodTypeSmName+' '+GoodSpec+' '+GoodModel+' ' as '商品',GoodUnit as '单位',
GoodNum as '数量',GoodPrice as '单价', GoodNum*GoodPrice as '小计',QingGouPer as '请购人'
from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.id=CAI_OrderInHouses.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId
where 1=1 ";

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


            if (txtChcekProNo.Text != "")
            {
                sql += string.Format(" and ChcekProNo like '%{0}%'", txtChcekProNo.Text);
            }

            //if (txtSupplier.Text != "")
            //{
            //    sql += string.Format(" and Supplier  like '%{0}%'", txtSupplier.Text);
            //}
            if (ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseID={0}", ddlHouse.Text);
            }

            if (ViewState["showAll"] != null)
            {
                sql += string.Format(" and CreateUserId={0}", Session["currentUserId"]);
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

            //OutputExcel(dt.DefaultView, xlfile);
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
            foreach (DataRowView row in dv)
            {
                if (!(row["小计"] is DBNull))
                {
                    total += Convert.ToDecimal(row["小计"]);
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
            excel.Cells[rowSum, 9] = total.ToString();
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
            xBk.SaveCopyAs(Server.MapPath(".") + "\\" + this.xlfile+ ".xls");
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
