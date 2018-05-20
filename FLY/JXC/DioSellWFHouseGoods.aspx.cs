using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.JXC;
using System.Collections;
using System.Data;

namespace VAN_OA.JXC
{
    public partial class DioSellWFHouseGoods : System.Web.UI.Page
    {
        protected string GetValue(object value)
        {
            return string.Format("{0:n4}", Convert.ToDecimal(value));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DioSellWFHouseGoods"] = new List<Sell_OrderOutHouses>();
                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArray("");
                goodsSmTypeList.Insert(0, new TB_GoodsSmType());
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodSmType.DataTextField = "GoodTypeSmName";
                ddlGoodSmType.DataValueField = "GoodTypeSmName";

                TB_GoodsTypeService typeSer = new TB_GoodsTypeService();
                List<TB_GoodsType> allType = typeSer.GetListArray("");
                allType.Insert(0, new TB_GoodsType());
                ddlGoodType.DataSource = allType;
                ddlGoodType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeName";
                ddlGoodType.DataValueField = "GoodTypeName";


                Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                houseList.Add(new TB_HouseInfo());
                ddlHouse.DataSource = houseList;
                ddlHouse.DataBind();
                ddlHouse.DataTextField = "houseName";
                ddlHouse.DataValueField = "id";



                List<TB_HouseGoods> allGoods = new List<TB_HouseGoods>();
                gvList.DataSource = allGoods;
                gvList.DataBind();
            }

        }
        Dal.JXC.TB_HouseGoodsService houseSer = new VAN_OA.Dal.JXC.TB_HouseGoodsService();
        Dal.BaseInfo.TB_GoodService goodsSer = new VAN_OA.Dal.BaseInfo.TB_GoodService();
        private void Show()
        {
            string sql = " 1=1 ";



            if (txtZhuJi.Text != "")
            {
                //string goodName = txtZhuJi.Text.Replace(@"\", ",");
                string[] allList = txtZhuJi.Text.Split('\\');

                int goodId = goodsSer.GetGoodId(allList[1], allList[3], allList[4], allList[2], allList[5]);
                if (goodId == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的商品不存在！');</script>");

                    return;
                }

                sql += string.Format(" and TB_HouseGoods.GoodId={0}", goodId);

            }


            if (ddlGoodSmType.Text != "" && ddlGoodType.Text != "")
            {
                sql += string.Format(" and GoodTypeSmName ='{0}' ", ddlGoodSmType.SelectedItem.Value);
            }
            else
            {
                if (ddlGoodSmType.Text != "" && ddlGoodType.Text == "")
                {
                    sql += string.Format(" and GoodTypeSmName ='{0}' ", ddlGoodSmType.SelectedItem.Value);
                }
                if (ddlGoodSmType.Text == "" && ddlGoodType.Text != "")
                {
                    sql += string.Format(" and  GoodTypeName ='{0}'", ddlGoodType.SelectedItem.Value);
                }

            }

            if (ddlHouse.Text != "" && ddlHouse.Text != "0")
            {
                sql += string.Format(" and HouseId ={0}", ddlHouse.SelectedItem.Value);

            }

            if (Request["ChuKuPoNo"] != null)
            {
                sql += string.Format(" and TB_Good.GoodId in ( select GoodId from CG_POOrder left join CG_POOrders on CG_POOrder.id=CG_POOrders.id where Status='通过' and PONo='{0}')", Request["ChuKuPoNo"]);
                //sql += string.Format(" and CG_POOrder.PONo='{0}'", Request["ChuKuPoNo"]);
            }

            if (txtGuiGe.Text != "")
            {
                sql += string.Format(" and  GoodSpec like '%{0}%'", txtGuiGe.Text);
            }
            List<TB_HouseGoods> gooQGooddList = this.houseSer.GetListArray_ToDio_1(sql, Request["ChuKuPoNo"]);
            if (gooQGooddList.Count > 0 && Request["ChuKuPoNo"]!=null)
            {
                Sell_OrderOutHouseService sellOutSer = new Sell_OrderOutHouseService();
                var allNums= sellOutSer.GetSellOrderNum(Request["ChuKuPoNo"].ToString());
                System.Text.StringBuilder goodIds = new System.Text.StringBuilder ();
                foreach (var model in gooQGooddList)
                {
                    if (allNums.ContainsKey(model.GoodId))
                    {
                        model.LastNum = allNums[model.GoodId];
                    }
                    goodIds.AppendFormat("{0},", model.GoodId);
                }
                if (goodIds.ToString().Length > 0)
                {
                    var DoingOrderNums=sellOutSer.GetDoingOrderNum(goodIds.ToString().Trim(','));

                    var caiNums = GetDs_New(Request["ChuKuPoNo"], goodIds.ToString().Trim(','));

                    foreach (var model in gooQGooddList)
                    {
                        if (DoingOrderNums.ContainsKey(model.GoodId))
                        {
                            model.DoingNum = DoingOrderNums[model.GoodId];
                        }
                        if (caiNums.ContainsKey(model.GoodId.ToString()))
                        {
                            model.CaiNum = Convert.ToDecimal(caiNums[model.GoodId.ToString()]);
                        } 
                    }
                }
            }
            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
        }



        TB_HouseGoods SumOrders = new TB_HouseGoods();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                TB_HouseGoods model = e.Row.DataItem as TB_HouseGoods;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;

                if (model.GoodNum - model.DoingNum < model.LastNum)
                    e.Row.BackColor = System.Drawing.Color.Red;

            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.GoodNum.ToString());//数量                
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }
        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
        }

        protected void gvSelectedWares_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvSelectedWares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");              


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<Sell_OrderOutHouses> selectedList = Session["DioSellWFHouseGoods"] as List<Sell_OrderOutHouses>;
            if (selectedList == null)
            {
                selectedList = new List<Sell_OrderOutHouses>();
            }

            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("chkSelect")) as CheckBox;
                if (cb.Checked)
                {
                    Sell_OrderOutHouses model = new Sell_OrderOutHouses();


                    Label lblGoodName = (gvList.Rows[i].FindControl("lblGoodName")) as Label;

                    if (lblGoodName != null)
                    {
                        model.GoodName = lblGoodName.Text;
                    }
                    Label GoodAreaNumber = (gvList.Rows[i].FindControl("GoodAreaNumber")) as Label;

                    if (GoodAreaNumber != null)
                    {
                        model.GoodAreaNumber = GoodAreaNumber.Text;
                    }
                    

                    Label GoodTypeSmName = (gvList.Rows[i].FindControl("GoodTypeSmName")) as Label;

                    if (GoodTypeSmName != null)
                    {
                        model.GoodTypeSmName = GoodTypeSmName.Text;
                    }


                    Label GoodSpec = (gvList.Rows[i].FindControl("GoodSpec")) as Label;

                    if (GoodSpec != null)
                    {
                        model.GoodSpec = GoodSpec.Text;
                    }


                    Label Good_Model = (gvList.Rows[i].FindControl("Good_Model")) as Label;

                    if (Good_Model != null)
                    {
                        model.Good_Model = Good_Model.Text;
                    }

                    Label GoodUnit = (gvList.Rows[i].FindControl("GoodUnit")) as Label;

                    if (lblGoodName != null)
                    {
                        model.GoodUnit = GoodUnit.Text;
                    }



                    Label lblCheckPrice = (gvList.Rows[i].FindControl("lblCheckPrice")) as Label;

                    if (lblCheckPrice != null)
                    {
                        model.GoodPrice =Convert.ToDecimal(lblCheckPrice.Text);
                    }

                    Label GoodId = (gvList.Rows[i].FindControl("GoodId")) as Label;

                    if (GoodId != null)
                    {
                        model.GooId = Convert.ToInt32(GoodId.Text);
                    }


                    Label HouseId = (gvList.Rows[i].FindControl("HouseId")) as Label;

                    if (HouseId != null)
                    {
                        model.HouseID = Convert.ToInt32(HouseId.Text);
                    }

                    Label HouseName = (gvList.Rows[i].FindControl("HouseName")) as Label;

                    if (HouseName != null)
                    {
                        model.HouseName = HouseName.Text;
                    }

                    Label GoodNo = (gvList.Rows[i].FindControl("GoodNo")) as Label;

                    if (HouseName != null)
                    {
                        model.GoodNo = GoodNo.Text;
                    }

                    Label lblLastNum = (gvList.Rows[i].FindControl("lblLastNum")) as Label;

                    if (lblLastNum != null)
                    {
                        model.GoodNum =Convert.ToDecimal( lblLastNum.Text);
                        model.Total = model.GoodNum * model.GoodPrice;
                        model.LastSellNum = model.GoodNum;
                    }

                    Label lblCaiNum = (gvList.Rows[i].FindControl("lblCaiNum")) as Label;

                    if (lblCaiNum != null)
                    {
                        model.LastCaiNum = Convert.ToDecimal(lblCaiNum.Text);
                    }
                    #region MyRegion
                    Label lblPOGoodPrice = (gvList.Rows[i].FindControl("lblPOGoodPrice")) as Label;

                    if (lblPOGoodPrice != null)
                    {
                        model.GoodSellPrice = Convert.ToDecimal(lblPOGoodPrice.Text);
                        model.GoodOriSellPrice = model.GoodSellPrice;
                    }
                    Label lblPONum = (gvList.Rows[i].FindControl("lblPONum")) as Label;

                    if (lblPONum != null)
                    {
                        var pONum = Convert.ToDecimal(lblPONum.Text);
                        model.PONum = pONum;
                        model.POGoodTotal = pONum * model.GoodSellPrice;
                        if (pONum < model.LastSellNum)
                        {
                            model.GoodNum = pONum;
                        }
                        model.GoodSellPriceTotal = model.GoodSellPrice * model.GoodNum;
                    } 
                    #endregion

                    selectedList.Add(model);
                }
            }

            Session["DioSellWFHouseGoods"] = selectedList;
            gvSelectedWares.DataSource = selectedList;
            gvSelectedWares.DataBind();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Write("<script>window.close();window.opener=null;</script>"); 
        }

        protected void ddlGoodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGoodType.SelectedItem.Text == "")
            {
                ddlGoodSmType.Text = "";

            }
            else
            {
                TB_GoodsSmTypeService goodsSmTypeSer = new TB_GoodsSmTypeService();
                List<TB_GoodsSmType> goodsSmTypeList = goodsSmTypeSer.GetListArrayToddl(" 1=1 and GoodTypeName='" + ddlGoodType.SelectedItem.Value + "'");
                goodsSmTypeList.Insert(0, new TB_GoodsSmType());
                ddlGoodSmType.DataSource = goodsSmTypeList;
                ddlGoodSmType.DataBind();
                ddlGoodType.DataTextField = "GoodTypeSmName";
                ddlGoodType.DataValueField = "GoodTypeSmName";
            }
            Show();
        }

        protected void ddlGoodSmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show();
        }

//        private System.Collections.Hashtable GetDs(string pono,string goodids)
//        {
//            Hashtable hs = new Hashtable();
//            string sql = string.Format(@"select tb1.GoodId,caiNums-isnull(outNum,0) as caiLastNum from (select GoodId,PoNo,sum(Num) as caiNums from CAI_POOrder 
//left join CAI_POCai on CAI_POCai.id=CAI_POOrder.id
//where Status<>'不通过' and pono='{0}' and  GoodId in ({1}) group by GoodId,PoNo
//) as tb1
//left join 
//(
//select GooId,PoNo,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
//where Status<>'不通过' and pono='{0}' and  GooId in ({1}) group by GooId,PoNo
//)
//as tb2  on tb1.pono=tb2.pono and tb1.GoodId=tb2.GooId ", pono, goodids);
//            var dt= DBHelp.getDataTable(sql);

//            foreach (DataRow m in dt.Rows)
//            {
//                hs.Add(m[0].ToString(),m[1].ToString());
//            }

//            return hs;
//        }

       

        /// <summary>
        /// 非库存+库存采购数量
        /// </summary>
        /// <param name="pono"></param>
        /// <param name="goodids"></param>
        /// <returns></returns>
        private System.Collections.Hashtable GetDs_New(string pono, string goodids)
        {
            Hashtable hs = new Hashtable();
            string sql = string.Format(@"select tb1.GoodId,caiNums-isnull(outNum,0) as caiLastNum from (
select  GoodId,PoNo,sum(caiNums) as caiNums from (
select GooId as GoodId,PoNo,sum(GoodNum) as caiNums from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.id=CAI_OrderInHouses.id
where Status='通过' and Supplier<>'库存' and pono='{0}' and  GooId in ({1}) group by GooId,PoNo
union all
select GoodId,PoNo,sum(Num) as caiNums from CAI_POOrder 
left join CAI_POCai on CAI_POCai.id=CAI_POOrder.id
where Status='通过' and lastSupplier='库存' and pono='{0}' and  GoodId in ({1}) group by GoodId,PoNo) as A group by  GoodId,PoNo

) as tb1
left join 
(
select GooId,PoNo,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
where Status<>'不通过' and pono='{0}' and  GooId in ({1}) group by GooId,PoNo
)
as tb2  on tb1.pono=tb2.pono and tb1.GoodId=tb2.GooId ", pono, goodids);
            var dt = DBHelp.getDataTable(sql);

            foreach (DataRow m in dt.Rows)
            {
                hs.Add(m[0].ToString(), m[1].ToString());
            }

            return hs;
        }
    }   
}
