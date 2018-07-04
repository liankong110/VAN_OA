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
    public partial class Sell_Cai_OrderInHouseList : BasePage
    {
        Sell_OrderInHouseService POSer = new Sell_OrderInHouseService();
        Sell_OrderInHousesService ordersSer = new Sell_OrderInHousesService();


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

                //主单
                List<Sell_Cai_OrderInHouseListModel> pOOrderList = new List<Sell_Cai_OrderInHouseListModel>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();


                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll(SysObj.Sell_OrderInHouseList) == false)
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
            string sql = "";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and tb.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销退时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销退时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestNAME  like '%{0}%'", txtGuestName.Text.Trim());
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and AE IN(select loginName from tb_User where {0})", where);
            }
            if (txtGoodNo.Text != "" || txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {

                if (txtGoodNo.Text != "")
                {
                    sql += string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
                }
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
            }

            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial={0} ", ddlIsSpecial.Text);
            }
            if (ddlIsClose.Text != "-1")
            {
                sql += string.Format(" and IsClose={0} ", ddlIsClose.Text);
            }
            if (ddlIsSelect.Text != "-1")
            {
                sql += string.Format(" and IsSelected={0} ", ddlIsSelect.Text);
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += string.Format(" and JieIsSelected={0} ", ddlJieIsSelected.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            if (ddlTui.Text == "2")
            {
                sql += string.Format(" and tb.GoodNum=tb1.CaiGoodNum ", ddlJieIsSelected.Text);
            }
            if (ddlTui.Text == "1")
            {
                sql += string.Format(" and (tb.GoodNum<>tb1.CaiGoodNum or tb1.CaiGoodNum is null )", ddlJieIsSelected.Text);
            }
            List<Sell_Cai_OrderInHouseListModel> pOOrderList = this.POSer.GetSell_Cai_OrderInHouseListArray(sql);

            //商品数量合计》=（销售退货合计-采购退货合计）×2-----浅绿色-正常
            if (ddlFenXi.Text == "1")
            {
                pOOrderList = pOOrderList.FindAll(t => t.NeedNums <= 0 && (t.GoodNum - (t.CaiGoodNum ?? 0) >= 0));
            }
            //商品销售退货合计-采购退货合计<0----浅灰色-不正常
            if (ddlFenXi.Text == "2")
            {
                pOOrderList = pOOrderList.FindAll(model => (model.GoodNum - (model.CaiGoodNum ?? 0) < 0));
            }
            //商品数量合计<（销售退货合计-采购退货合计）×2------红色-不正常
            if (ddlFenXi.Text == "3")
            {
                pOOrderList = pOOrderList.FindAll(t => t.NeedNums > 0);
            }

            ////商品销售退货合计-采购退货合计<0--不正常（浅灰色）
            //if (ddlFenXi.Text == "1")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => model.GoodNum - (model.CaiGoodNum ?? 0) < 0);
            //}

            ////商品销售退货合计-采购退货合计=0--正常（浅黄色）
            //if (ddlFenXi.Text == "2")
            //{
            //    pOOrderList = pOOrderList.FindAll(model =>model.GoodNum - (model.CaiGoodNum ?? 0) == 0);
            //}

            ////项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量>=销售退货合计-采购退货合计-正常（浅绿色）
            //if (ddlFenXi.Text == "3")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => model.NeedNums <= 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)));
            //}

            ////项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -正常库存消零（土黄色）
            //if (ddlFenXi.Text == "4")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => model.NeedNums > 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)));
            //}

            ////项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量<销售退货合计-采购退货合计--不正常有库存（淡红色）
            //if (ddlFenXi.Text == "5")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => model.NeedNums <= 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)));
            //}

            ////项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -不正常库存不为零（红色）
            //if (ddlFenXi.Text == "6")
            //{
            //    pOOrderList = pOOrderList.FindAll(model =>model.NeedNums > 0);
            //}
            // //正常
            //if (ddlFenXi.Text == "7")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => (model.GoodNum - (model.CaiGoodNum ?? 0) == 0) 
            //        ||   (model.NeedNums <= 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)))
            //        || (model.NeedNums > 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0))));
            //}
            ////不正常
            //if (ddlFenXi.Text == "8")
            //{
            //    pOOrderList = pOOrderList.FindAll(model => (model.GoodNum - (model.CaiGoodNum ?? 0) < 0)
            //        || (model.NeedNums > 0)
            //        || (model.NeedNums <= 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0))));
            //}

            lblSellTuiTotal_Sum.Text = pOOrderList.Sum(t => t.GoodPriceSecond).ToString();
            lblCaiTuiTotal_Sum.Text = pOOrderList.Sum(t => t.CAIGoodPriceTotal).ToString();
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();
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

        Sell_Cai_OrderInHouseListModel SumOrders = new Sell_Cai_OrderInHouseListModel();
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                Sell_Cai_OrderInHouseListModel model = e.Row.DataItem as Sell_Cai_OrderInHouseListModel;
                ////商品销售退货合计-采购退货合计<0--不正常（浅灰色）
                // if (model.GoodNum - (model.CaiGoodNum ?? 0) < 0)
                // {
                //     e.Row.BackColor = System.Drawing.Color.LightGreen;
                // }
                ////商品销售退货合计-采购退货合计=0--正常（浅黄色）
                // else if (model.GoodNum - (model.CaiGoodNum ?? 0) == 0)
                // {
                //     e.Row.BackColor = System.Drawing.Color.LightYellow;
                // }
                ////项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量>=销售退货合计-采购退货合计-正常（浅绿色）
                // else if (model.NeedNums <= 0&&model.HouseGoodNum>(model.GoodNum-(model.CaiGoodNum??0)))
                // {
                //     e.Row.BackColor = System.Drawing.Color.LightGreen;
                // }
                ////项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -正常库存消零（土黄色）
                // else if (model.NeedNums > 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)))
                // {
                //     e.Row.BackColor = System.Drawing.Color.LightYellow;
                // }
                ////项目中该商品数量合计-采购退货合计>=（销售退货合计-采购退货合计）×2 且采购单为库存的数量<销售退货合计-采购退货合计--不正常有库存（淡红色）
                // else if (model.NeedNums <= 0 && model.HouseGoodNum > (model.GoodNum - (model.CaiGoodNum ?? 0)))
                // {
                //     e.Row.BackColor = System.Drawing.Color.OrangeRed;
                // }
                ////项目中该商品数量合计-采购退货合计<（销售退货合计-采购退货合计）×2 -不正常库存不为零（红色）
                // else if (model.NeedNums > 0)
                // {
                //     e.Row.BackColor = System.Drawing.Color.Red;
                // }



                if (model.NeedNums <= 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
                if ((model.GoodNum - (model.CaiGoodNum ?? 0)) < 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
                if (model.NeedNums > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                SumOrders.CAIGoodPriceTotal = SumOrders.CAIGoodPriceTotal ?? 0 + model.CAIGoodPriceTotal ?? 0;
                SumOrders.GoodSellPriceTotal = SumOrders.GoodSellPriceTotal + model.GoodSellPriceTotal;

            }
            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                lblSellTuiTotal.Text = SumOrders.GoodSellPriceTotal.ToString();
                lblCaiTuiTotal.Text = (SumOrders.CAIGoodPriceTotal ?? 0).ToString();
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

    }
}
