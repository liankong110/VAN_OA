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
using VAN_OA.Model.BaseInfo;
using System.IO;

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
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                var dalList = dal.GetListArray("");
                dalList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "全部" });
                ddlGuestTypeList.DataSource = dalList;
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";

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
                        user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
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
            if (ddlPoType.Text == "0")
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
                userId = ddlUser.SelectedItem.Text;
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
            string poNoSql = "";
            if (ddlGuestTypeList.SelectedValue != "全部" || ddlIsSpecial.Text != "-1" || ddlModel.Text != "全部")
            {
                poNoSql = " and exists (select id from CG_POOrder where CG_POOrder.pono=NoSellAndCaiGoods.PONO AND IFZhui=0 ";
                if (ddlGuestTypeList.SelectedValue != "全部")
                {
                    poNoSql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
                }
                if (ddlIsSpecial.Text != "-1")
                {
                    poNoSql += string.Format(" and IsSpecial={0} ", ddlIsSpecial.Text);
                }
                if (ddlModel.Text != "全部")
                {
                    poNoSql += string.Format(" and Model='{0}'", ddlModel.Text);
                }
                poNoSql += ")";
            }

            var resultList = _dal.GetListNoSellAndCaiGoods(ponoWhere, userId, goodNoWhere, guestWhere, ruTimeWhere, poTimeWhere, where, company
                , (ddlKCType.Text == "1" ? true : false), poNoSql);

            if (cbZero.Checked)
            {
                resultList = resultList.FindAll(t => t.RuChuNum != 0 || t.CaIKuNum != 0 || t.CaiGouNum != 0);
            }

            if (cbRuZero.Checked)
            {
                resultList = resultList.FindAll(t => t.RuChuNum != 0);
            }

            if (cbCaiKu.Checked)
            {
                resultList = resultList.FindAll(t => t.CaIKuNum != 0);
            }

            if (cbCaiGou.Checked)
            {
                resultList = resultList.FindAll(t => t.CaiGouNum != 0);
            }

            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                //resultList = resultList.FindAll(t => t. != 0);
            }
            lbltotalNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.totalNum * t.avgGoodPrice));
            lblRuChuNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.RuChuNum * t.avgGoodPrice));
            lblCaIKuNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaIKuNum * t.avgGoodPrice));
            lblCaiGouNum.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaiGouNum * t.avgGoodPrice));

            lbltotalNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.totalNum * t.avgSellPrice));
            lblRuChuNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.RuChuNum * t.avgSellPrice));
            lblCaIKuNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaIKuNum * t.avgSellPrice));
            lblCaiGouNum_Sell.Text = string.Format("{0:n2}", resultList.Sum(t => t.CaiGouNum * t.avgSellPrice));

            //供应商进行筛选
            if (!string.IsNullOrEmpty(txtSupplier.Text) && resultList.Count > 0)
            {
                var allSupplier = _dal.GetCaiPoNo("'" + string.Join("','", resultList.GroupBy(t => t.PONo).Select(t => t.Key).ToArray()).Trim("','".ToCharArray()) + "'",
                   string.Join(",", resultList.GroupBy(t => t.GoodId).Select(t => t.Key.ToString()).ToArray()));
                foreach (var model in resultList)
                {
                    var tempWform = allSupplier.FindAll(t => t.PONo == model.PONo && t.GoodId == model.GoodId.ToString()).Select(t => t.lastSupplier).Distinct().ToArray();

                    model.Supplier = string.Join(",", tempWform);
                }
                if (cbPiPei.Checked)
                {
                    resultList = resultList.FindAll(t => t.Supplier == txtSupplier.Text);
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
            if ((gvMain.PageIndex + 1) * 10 <= lastRows)
            {
                lastRows = (gvMain.PageIndex + 1) * 10;
            }
            for (int i = gvMain.PageIndex * 10; i < lastRows; i++)
            {
                goodId += list[i].GoodId + ",";
                pONos += "'" + list[i].PONo + "',";
            }

            caiPonoList = _dal.GetCaiPoNo(pONos.Trim(','), goodId.Trim(','));

            gvMain.DataSource = list;
            gvMain.DataBind();

        }

        protected string GetlastSupplier(object pono, object goodId)
        {
            var tempWform = caiPonoList.FindAll(t => t.PONo == pono.ToString() && t.GoodId == goodId.ToString()).Select(t => t.lastSupplier).Distinct().ToArray();


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
        int num = 1;
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

                e.Row.Attributes.Add("style", "border:1px solid #DCDCDC ");

                num++;
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //override VerifyRenderingInServerForm.
        }
        int AllCount = 0;
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
            }
            gvMain.AllowPaging = false;
            var data = GetList();
            gvMain.DataSource = data;//取消分页后重新绑定数据集,ds为数据集dataset
            gvMain.DataBind();
            //ExportGridViewForUTF8(gvMain, "NoSellAndCaiGoods.xlsx");
            toExcel(gvMain);
            //gvMain.AllowPaging = true;//取消分页，便于导出所有数据，不然只能导出当前页面的几条数据


        }


        void toExcel(GridView gv)
        {
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string fileName = "export.xls";
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="GridView"></param>
        /// <param name="filename">保存的文件名称</param>
        private void ExportGridViewForUTF8(GridView GridView, string filename)
        {

            string attachment = "attachment; filename=" + filename;

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", attachment);

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvMain.RenderControl(htw);
            
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
    }
}
