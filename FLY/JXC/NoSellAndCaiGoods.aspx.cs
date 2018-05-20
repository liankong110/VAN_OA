using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model;
using System.Text;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class NoSellAndCaiGoods : BasePage
    {
        RuSellReportService _dal = new RuSellReportService();
        List<CaiPoNo> caiPonoList = new List<CaiPoNo>();
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
                if (Session["currentUserId"] != null)
                {
                    List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    if (VAN_OA.JXC.SysObj.IfShowAll("采库需出清单", Session["currentUserId"], "ShowAll") == false)
                    {
                        ViewState["showAll"] = false;
                        var model = Session["userInfo"] as User;
                        user.Insert(0, model);
                    }
                    else
                    {
                        user = userSer.getAllUserByPOList();
                        user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
                    }
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";

                    List<RuSellReport> list = new List<RuSellReport>();
                    gvMain.DataSource = list;
                    gvMain.DataBind();


                    if (Request["PONo"] != null)
                    {
                        txtPONo.Text = Request["PONo"].ToString();
                        ddlPoType.Text = "2";
                        cbRuZero.Checked = true;
                        Show();
                    }
                    if (Request["PONo1"] != null)
                    {
                        ddlPoType.Text = "2";
                        txtPONo.Text = Request["PONo1"].ToString();                         
                        Show();
                    }
                }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        public List<Model.JXC.NoSellAndCaiGoods> GetList()
        {
            txtSupplier.Text = txtSupplier.Text.Trim();
        string userId = "", goodNoWhere = "", guestWhere = "", ruTimeWhere = "", poTimeWhere = "", ponoWhere = "";

            string where = "";
            if (txtPONo.Text.Trim() != "")
            {
                 
                //ponoWhere = string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text);
                ponoWhere = string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ddlPoType.Text=="0")
            {
                ponoWhere += " and PONo like 'P%'";
            }
            if (ddlPoType.Text == "1")
            {
                ponoWhere += " and PONo like 'KC%'";
            }
            if (!string.IsNullOrEmpty(txtPOName.Text.Trim()))
            {
                ponoWhere += string.Format(" and PONAME like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                //guestWhere = string.Format(" and CG_POOrder.GuestName like '%{0}%'", txtGuestName.Text);
                guestWhere = string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtGoodNo.Text != "")
            {
                goodNoWhere = string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            
            else
            {
                userId =  ddlUser.SelectedItem.Text;
            }

            if (txtFrom.Text != "")
            {
                ruTimeWhere += string.Format(" and minRuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                ruTimeWhere += string.Format(" and minRuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtPOTimeFrom.Text != "")
            {
                poTimeWhere += string.Format(" and minPODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }


            if (txtPOTimeTo.Text != "")
            {
                poTimeWhere += string.Format(" and minPODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }
           
           
            if (ddlWeiType.Text == "0")//未开具出库单
            {
                where += " and NoOutNum is null ";
            }
            else if (ddlWeiType.Text == "1")//出库单执行中
            {
                where += " and doingOutNum=0 ";
            }
            else if (ddlWeiType.Text == "2")//已出又销退
            {
                where += " and sellTuiNum>0";
            }
            else if (ddlWeiType.Text == "3")//出库单未通过
            {
                where += " and WeiOutNum=0 ";
            }
            //if (cbZero.Checked)
            //{
            //    where += " and LastNum>0 ";
            //}
            //if (cbRuZero.Checked)
            //{
            //    where += " and ruChuNum>0";
            //}
            if (ddlIsHanShui.Text != "-1")
            {
                ponoWhere += string.Format(" and HanShui={0} ", ddlIsHanShui.Text);
            }
            string company = "";
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                company = string.Format("and AE IN(select loginName from tb_User where {0})", where1);
            }
            var resultList = _dal.GetListNoSellAndCaiGoods(ponoWhere, userId, goodNoWhere, guestWhere, ruTimeWhere, poTimeWhere, where, company
                ,(ddlKCType.Text=="1"?true:false));

            if (cbZero.Checked)
            {
                resultList = resultList.FindAll(t=>t.RuChuNum!=0||t.CaIKuNum!=0||t.CaiGouNum!=0);
            }

            if (cbRuZero.Checked)
            {
                resultList = resultList.FindAll(t => t.RuChuNum != 0);
            }

            if (cbCaiKu.Checked)
            {
                resultList = resultList.FindAll( t=>t.CaIKuNum != 0);
            }

            if (cbCaiGou.Checked)
            {
                resultList = resultList.FindAll(t=> t.CaiGouNum != 0);
            }

            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                //resultList = resultList.FindAll(t => t. != 0);
            }
            lbltotalNum.Text =string.Format("{0:n2}" ,resultList.Sum(t => t.totalNum * t.avgGoodPrice));
            lblRuChuNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.RuChuNum * t.avgGoodPrice));
            lblCaIKuNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaIKuNum * t.avgGoodPrice));
            lblCaiGouNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaiGouNum * t.avgGoodPrice));

            lbltotalNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.totalNum * t.avgSellPrice));
            lblRuChuNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.RuChuNum * t.avgSellPrice));
            lblCaIKuNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaIKuNum * t.avgSellPrice));
            lblCaiGouNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaiGouNum * t.avgSellPrice));

            //供应商进行筛选
            if (!string.IsNullOrEmpty(txtSupplier.Text)&& resultList.Count>0)
            {
                var allSupplier = _dal.GetCaiPoNo("'"+string.Join("','",resultList.GroupBy(t=>t.PONo).Select(t=>t.Key).ToArray()).Trim("','".ToCharArray())+"'",
                   string.Join(",", resultList.GroupBy(t => t.GoodId).Select(t => t.Key.ToString()).ToArray()));
                foreach (var model in resultList)
                {
                    var tempWform = allSupplier.FindAll(t => t.PONo == model.PONo && t.GoodId == model.GoodId.ToString()).Select(t => t.lastSupplier).Distinct().ToArray();

                    model.Supplier= string.Join(",", tempWform);
                }
                if (cbPiPei.Checked)
                {
                    resultList = resultList.FindAll(t => t.Supplier==txtSupplier.Text);
                }
                else
                {
                    resultList = resultList.FindAll(t => t.Supplier.Contains(txtSupplier.Text));
                }
            }

            return resultList;
        }
        private void Show()
        {
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtPOTimeFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
            }
            if (txtPOTimeTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }                
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
            }
                var list = GetList(); 
            AspNetPager1.RecordCount = list.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            string goodId = "", pONos = "";

            int lastRows = list.Count;
            if ((gvMain.PageIndex+1) * 10 <= lastRows)
            {
                lastRows = (gvMain.PageIndex+1) * 10;
            }
            for (int i = gvMain.PageIndex  * 10; i < lastRows; i++)
            {
                goodId +=list[i].GoodId +",";
                pONos += "'"+list[i].PONo + "',";
            }

            caiPonoList = _dal.GetCaiPoNo(pONos.Trim(','), goodId.Trim(','));

            gvMain.DataSource = list;
            gvMain.DataBind();

        }

        protected string GetlastSupplier(object pono, object goodId)
        {
            var tempWform = caiPonoList.FindAll(t => t.PONo == pono.ToString() && t.GoodId == goodId.ToString()).Select(t=>t.lastSupplier).Distinct().ToArray();
           

            return string.Join(",", tempWform);

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                VAN_OA.Model.JXC.NoSellAndCaiGoods model = e.Row.DataItem as VAN_OA.Model.JXC.NoSellAndCaiGoods;
                System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                if (lblIsHanShui != null)
                {
                    lblIsHanShui.Text = model.IsHanShui == 1 ? "含税" : "不含税";
                }
                if (model.IsHanShui == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //override VerifyRenderingInServerForm.
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
            }
            gvMain.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
            gvMain.AllowPaging = false;//取消分页，便于导出所有数据，不然只能导出当前页面的几条数据

            gvMain.DataSource = GetList();//取消分页后重新绑定数据集,ds为数据集dataset
            gvMain.DataBind();


            //DateTime dt = DateTime.Now;//给导出后的Excel表命名，结合表的用途以及系统时间来命名
            //string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            /*如导出的表中有某些列为编号、身份证号之类的纯数字字符串，如不进行处理，则导出的数据会默认为数字，例如原字符串"0010"则会变为数字10，字符串"1245787888"则会变为科学计数法1.236+E9，这样便达不到我们想要的结果，所以需要在导出前对相应列添加格式化的数据类型，以下为格式化为字符串型*/

            //foreach (GridViewRow dg in this.gvMain.Rows)
            //{
            //    dg.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //    dg.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //    dg.Cells[6].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //    dg.Cells[8].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //}

            Response.Clear();

            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=NoSellAndCaiGoods.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文          
 
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            Panel1.RenderControl(oHtmlTextWriter);//Add the Panel into the output Stream.
            Response.Write(oStringWriter.ToString());//Output the stream.
            Response.Flush();
            Response.End();
             
        }
    }
}
