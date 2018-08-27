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
using VAN_OA.Model.KingdeeInvoice;
using System.Collections.Generic;
using VAN_OA.Dal.KingdeeInvoice;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.KingdeeInvoice
{
    public partial class WFInvoiceCompare : BasePage
    {
        InvoiceReportService invoiceSer = new InvoiceReportService();

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


                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                List<InvoiceReport> invoiceList = new List<InvoiceReport>();

                this.gvList.DataSource = invoiceList;
                this.gvList.DataBind();

                txtPOFrom.Text = DateTime.Now.AddYears(-1).Year + "-1-1";

                //var user = new List<VAN_OA.Model.User>();
                //var userSer = new VAN_OA.Dal.SysUserService();
                //user = userSer.getAllUserByPOList();
                //user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                //ddlUser.DataSource = user;
                //ddlUser.DataBind();
                //ddlUser.DataTextField = "LoginName";
                //ddlUser.DataValueField = "Id";
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (VAN_OA.JXC.SysObj.NewShowAll_Name("发票比对", Session["currentUserId"], "ShowAll") == false)
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

                if (Request["PONo"] != null)
                {
                    isSelect = true;
                    txtPONo.Text = Request["PONo"];
                    txtPOFrom.Text = "";
                    cbIsSpecial.Checked = false;
                    AspNetPager1.CurrentPageIndex = 1;
                    Show();
                }


            }
        }
        bool isSelect = false;
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            isSelect = true;
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        List<BaseModel> list = new List<BaseModel>();
        public void Show()
        {
            txtGuestName.Text = txtGuestName.Text.Trim();
            txtInvoiceNo.Text = txtInvoiceNo.Text.Trim();
            txtPOName.Text = txtPOName.Text.Trim();
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return;
                }
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                    return;
                }
            }

            List<InvoiceReport> invoiceList = new List<InvoiceReport>();
            if (isSelect)
            {
                string sql = " where Sell_OrderFP.Status='通过'  and  Sell_OrderFP.FPNo<>'' ";

                string sql_1 = "where 1=1 ";

                if (txtGuestName.Text.Trim() != "")
                {
                    sql_1 += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%'", txtGuestName.Text.Trim());
                }
                if (txtInvoiceNo.Text.Trim() != "")
                {
                    sql_1 += string.Format(" and FPNo like '%{0}%'", txtInvoiceNo.Text.Trim());
                }
                if (cbZhengShu.Checked == false && txtInvoice.Text != "")
                {
                    if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                        return;
                    }
                    if (ddlInvTotal.Text == "=")
                    {
                        sql_1 += string.Format(" and Sell_OrderFP.Total in ({0},{1})", Convert.ToDecimal(txtInvoice.Text), -Convert.ToDecimal(txtInvoice.Text));
                    }
                    else
                    {
                        sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlInvTotal.Text, txtInvoice.Text);
                    }
                }
                if (cbZhengShu.Checked && txtInvoice.Text != "")
                {
                    if (CommHelp.VerifesToNum(txtInvoice.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票金额 格式错误！');</script>");
                        return;
                    }
                    if (ddlInvTotal.Text == "=")
                    {
                        sql_1 += string.Format(" and Sell_OrderFP.Total ={0} ", txtInvoice.Text);
                    }
                    else
                    {
                        sql_1 += string.Format(" and Sell_OrderFP.Total {0} {1}", ddlInvTotal.Text, txtInvoice.Text);
                    }
                }
                string param = "CG_POOrder.Status='通过'";
                string temp = "";
                if (ddlClose.Text != "-1" || ddlIsSelect.Text != "-1" || ddlHanShui.Text != "-1" || cbIsSpecial.Checked || cbJiaoFu.Checked || ddlUser.Text != "-1"
                    || txtPOFrom.Text != "" || txtPOTo.Text != "" || txtPONo.Text != "" || ddlCompany.Text != "-1"|| ddlModel.Text != "全部")
                {

                    if (ddlClose.Text != "-1")
                    {
                        param += string.Format(" and IsClose={0}", ddlClose.Text);
                    }
                    if (ddlIsSelect.Text != "-1")
                    {
                        param += string.Format(" and IsSelected={0}", ddlIsSelect.Text);
                    }
                    if (ddlHanShui.Text != "-1")
                    {
                        param += string.Format(" and IsPoFax={0}", ddlHanShui.Text);
                    }
                    if (ddlModel.Text != "全部")
                    {
                        param += string.Format(" and Model='{0}'", ddlModel.Text);
                    }
                    if (cbIsSpecial.Checked)
                    {
                        param += string.Format(" and IsSpecial=0 ");
                    }

                    if (cbJiaoFu.Checked)
                    {
                        param += string.Format(" and POStatue2='已交付' ");
                    }


                    if (txtPOFrom.Text != "")
                    {
                        if (CommHelp.VerifesToDateTime(txtPOFrom.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                            return;
                        }
                        param += string.Format(" and PODate>='{0} 00:00:00'", txtPOFrom.Text);
                    }

                    if (txtPOTo.Text != "")
                    {
                        if (CommHelp.VerifesToDateTime(txtPOTo.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票日期 格式错误！');</script>");
                            return;
                        }
                        param += string.Format(" and PODate<='{0} 23:59:59'", txtPOTo.Text);
                    }
                    if (ddlUser.Text != "-1")
                    {
                        param += string.Format(" and AppName={0} ", ddlUser.Text);
                    }
                    if (ddlCompany.Text != "-1")
                    {
                        string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                        param += string.Format(" and AppName IN(select id from tb_User where {0}) ", where);
                    }
                    if (txtPONo.Text.Trim() != "")
                    {
                        if (CheckPoNO(txtPONo.Text) == false)
                        {
                            return;
                        }

                        param += string.Format(" and Sell_OrderFP.PONO like '%{0}%' ", txtPONo.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtPOName.Text))
                    {
                        param += string.Format(" and Sell_OrderFP.PONAME like '%{0}%'", txtPOName.Text);
                    }
                    
                    sql += string.Format(" and exists(select id from CG_POOrder where {0} and  CG_POOrder.PONO=Sell_OrderFP.PONO  and ifzhui=0)", param);

                    temp = string.Format(" and exists(select id from CG_POOrder where {0} and  CG_POOrder.PONO=Sell_OrderFP.PONO  and ifzhui=0)", param);
                }

                if (ddlIsXiaozhang.Text != "-1")
                { 
                    
                }

                if (ddlCompare.Text == "2")
                {
                    invoiceList = this.invoiceSer.GetAllInvoiceReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                      ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "1", cbInvoTotalToge.Checked, ddlIsorder.Text, cbZhengShu.Checked, "", ddlIsXiaozhang.Text, param, temp);


                    invoiceList.AddRange(this.invoiceSer.GetAllInvoiceReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                     ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, "0", cbInvoTotalToge.Checked, ddlIsorder.Text, cbZhengShu.Checked, "", ddlIsXiaozhang.Text, param, temp));

                }
                else
                {
                    if (Request["PONo"] != null)
                    {
                        invoiceList = this.invoiceSer.GetAllInvoiceReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                            ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, ddlCompare.Text, cbInvoTotalToge.Checked, ddlIsorder.Text, cbZhengShu.Checked, txtPONo.Text, ddlIsXiaozhang.Text, param, temp);
                    }
                    else
                    {
                        invoiceList = this.invoiceSer.GetAllInvoiceReports(txtGuestName.Text, txtInvoiceNo.Text, txtInvoice.Text,
                         ddlInvTotal.Text, txtFrom.Text, txtTo.Text, sql, ddlCompare.Text, cbInvoTotalToge.Checked, ddlIsorder.Text, cbZhengShu.Checked, "", ddlIsXiaozhang.Text, param, temp);
                    }
                }
                isSelect = false;
                ViewState["myList"] = invoiceList;
            }
            else
            {
                invoiceList = ViewState["myList"] as List<InvoiceReport>;
            }
            AspNetPager1.RecordCount = invoiceList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            string allPONOs = "";
            int pageIndex = gvList.PageIndex;
            for (int i = (pageIndex * 10); i < ((pageIndex + 1) * 10); i++)
            {
                if (i >= invoiceList.Count)
                {
                    break;
                }
                if (!string.IsNullOrEmpty(invoiceList[i].OA_PONO))
                {
                    allPONOs += "'" + invoiceList[i].OA_PONO + "',";
                }

            }
            allPONOs = allPONOs.Trim(',');
            if (allPONOs != "")
            {
                string ponos = string.Format(@"SELECT PONO,AE from CG_POOrder  where Status='通过' and  IFZhui=0 AND PONO IN ({0})", allPONOs);
                DataTable dt = DBHelp.getDataTable(ponos);
                list = new List<BaseModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new BaseModel { key = dr[0].ToString(), AE = dr[1].ToString() });
                }
            }
            this.gvList.DataSource = invoiceList;
            this.gvList.DataBind();


            decimal OA_InvoiceTotal = 0;
            decimal Kingdee_InvoiceTotal = 0;

            Hashtable hs = new Hashtable();
            Hashtable hs1 = new Hashtable();
            foreach (var m in invoiceList)
            {
                if (!hs.ContainsKey(m.Kingdee_Id))
                {
                    Kingdee_InvoiceTotal += m.Kingdee_InvoiceTotal ?? 0;
                    hs.Add(m.Kingdee_Id, null);
                }

                if (!hs1.ContainsKey(m.OA_FPId))
                {
                    OA_InvoiceTotal += m.OA_InvoiceTotal ?? 0;
                    hs1.Add(m.OA_FPId, null);
                }
            }
            lblAllTotal.Text = invoiceList.Sum(t => t.All_InvoiceTotal ?? 0).ToString();
            lblOATotal.Text = OA_InvoiceTotal.ToString();
            lblKingdeeTotal.Text = Kingdee_InvoiceTotal.ToString();

        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                InvoiceReport model = e.Row.DataItem as InvoiceReport;
                LinkButton lb1 = e.Row.FindControl("LinkButton1") as LinkButton;
                lb1.OnClientClick = string.Format("javascript:window.open('../JXC/Sell_OrderPFList.aspx?InvoiceNo={0}','_blank'); return false;", model.All_InvoiceNo);

                LinkButton lb2 = e.Row.FindControl("LinkButton2") as LinkButton;
                lb2.OnClientClick = string.Format("javascript:window.open('../JXC/Sell_OrderPFList.aspx?InvoiceNo={0}','_blank'); return false;", model.OA_InvoiceNo);

                LinkButton lb3 = e.Row.FindControl("LinkButton3") as LinkButton;
                lb3.OnClientClick = string.Format("javascript:window.open('WFInvoiceList.aspx?InvoiceNo={0}','_blank'); return false;", model.Kingdee_InvoiceNo);

                if (!string.IsNullOrEmpty(model.OA_PONO))
                {
                    Label lblOA_AE = e.Row.FindControl("lblOA_AE") as Label;
                    var l = list.FindAll(t => t.key == model.OA_PONO);
                    string AE = l.Count == 0 ? "" : l[0].AE;
                    lblOA_AE.Text = AE;
                }

            }

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)   //判断创建的行是不是标题行
            {
                TableCellCollection cells = e.Row.Cells;
                cells.Clear();  //获得标题行,清空标题行的设置
                cells.Add(new TableHeaderCell());  //添加一个标题单元
                cells[0].RowSpan = 12;   //设置跨行.         
                //直接导入html中的table中的元素
                cells[0].Text = @"</th><th colspan=3>OA系统+金蝶系统</th><th colspan=6>OA系统</th><th colspan=4>金蝶系统</th></tr><tr style='height: 20px; background-color: #336699; color: White;'>
                
                <th>
                                    发票号
                                </th>
                                <th>
                                    客户名称
                                </th>
                                <th>
                                    金额
                                </th>
                                <th>
                                    发票号
                                </th>
                                <th>
                                    客户名称
                                </th>
 <th>
                                    AE
                                </th>
                                <th>
                                    金额
                                </th>
                                <th>
                                    日期
                                </th>
                                <th>
                                    项目编号
                                </th>
                                <th>
                                    发票号
                                </th>
                                <th>
                                    客户名称
                                </th>
                                <th>
                                    金额
                                </th>
                                <th>
                                    日期
                                 ";
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "selectAll")
            //{
            //    Response.Redirect("~/JXC/Sell_OrderPFList.aspx?InvoiceNo=" + e.CommandArgument);
            //}
            //if (e.CommandName == "selectOA")
            //{
            //    Response.Redirect("~/JXC/Sell_OrderPFList.aspx?InvoiceNo=" + e.CommandArgument);
            //}
            //if (e.CommandName == "selectKingdee")
            //{
            //    Response.Redirect("~/KingdeeInvoice/WFInvoiceList.aspx?InvoiceNo=" + e.CommandArgument);
            //}

        }

    }
}
