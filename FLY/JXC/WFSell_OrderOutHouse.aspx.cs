using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.Collections;
using VAN_OA.Model;
 

namespace VAN_OA.JXC
{
    public partial class WFSell_OrderOutHouse : System.Web.UI.Page
    {
        protected string GetValue(object value)
        {
            return string.Format("{0:n4}", Convert.ToDecimal(value));
        }
        private A_RoleService roleSer = new A_RoleService();

        protected void btnClose_Click(object sender, EventArgs e)
        {

            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }

        }

        protected void btnSet_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// 获取订单原始销售价格
        /// </summary>
        /// <param name="POOrders"></param>
        public void GetPOSellPrice(List<Sell_OrderOutHouses> POOrders)
        {
            if (txtPONo.Text != null && POOrders != null)
            {
               var goodSellPrice= new CG_POOrdersService().GetPOGoodInfo(txtPONo.Text);
               foreach (var model in POOrders)
               {
                   if (goodSellPrice.ContainsKey(model.GooId))
                   {
                       model.GoodOriSellPrice = goodSellPrice[model.GooId];
                   }
               }
            }
        }
        
        public bool FormCheck()
        {
            #region 设置自己要判断的信息


            if (txtRuTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写出库日期！');</script>");
                txtRuTime.Focus();
                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtRuTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库日期 格式错误！');</script>");
                    return false;
                }

            }

            try
            {
                Convert.ToDateTime(txtRuTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写出库日期格式有误！');</script>");
                txtRuTime.Focus();
                return false;
                
            }


            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }



            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");                

                return false;
            }

            //if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写检验人不存在！');</script>");            

            //    return false;
            //}
            
            #endregion

            if (Request["allE_id"] == null)
            {
                List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return false;
                }

                if (POOrders.Count == 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择销售商品！');</script>");

                    return false;
                    
                }

                if (CG_POOrderService.IsClosePONO(txtPONo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                            "<script>alert('此项目已经关闭！');</script>");
                    return false;
                }

                Hashtable ht = new Hashtable();
                //foreach (var model in POOrders)
                //{
                //    string key = model.GooId.ToString() + model.HouseID+model.GoodSellPrice;
                //    if (!ht.Contains(key))
                //        ht.Add(key, null);
                //    else
                //    {
                //        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName));
                //        return false;
                //    }
                   
                //}
                Dal.JXC.TB_HouseGoodsService houseSer = new VAN_OA.Dal.JXC.TB_HouseGoodsService();
                //记录总的
                Hashtable htTotal = new Hashtable();
                decimal poTotal = 0;

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    var goodId=POOrders[i].GooId;
                    decimal oldNums = 0;
                    decimal oldSellPriceTotal = 0;
                    if (htTotal.Contains(goodId))
                    { 
                        var m=htTotal[goodId] as HT;
                        oldNums = m.Nums;
                        oldSellPriceTotal = m.PriceTotal;
                    }

                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        if (CommHelp.VerifesToNum(txtNum.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        if (POOrders[i].GoodNum<=0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");

                            return false;
                        }                      
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }


                    TextBox txtGoodSellPrice = gvList.Rows[i].FindControl("txtGoodSellPrice") as TextBox;
                    if (txtGoodSellPrice != null)
                    {
                        if (CommHelp.VerifesToNum(txtGoodSellPrice.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售单价 格式错误！');</script>");
                            return false;
                        }
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtGoodSellPrice.Text);

                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;

                        poTotal += POOrders[i].GoodSellPriceTotal;
                    }
                    

                    if (POOrders[i].GoodSellPrice < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],价格必须大于等于0！');</script>", POOrders[i].GoodName, POOrders[i].GoodSpec, POOrders[i].Good_Model));
                        return false;
                    }
                    #region MyRegion
                    string key = POOrders[i].GooId.ToString() + POOrders[i].HouseID + POOrders[i].GoodSellPrice;
                    if (!ht.Contains(key))
                        ht.Add(key, null);
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}],信息重复！');</script>", POOrders[i].GoodName, POOrders[i].GoodSpec, POOrders[i].Good_Model, POOrders[i].HouseName));
                        return false;
                    }
                    #endregion
                    var model = POOrders[i];
                    string sql = string.Format(" 1=1 and HouseId={0} and TB_HouseGoods.GoodId={1}", model.HouseID, model.GooId);

                    List<TB_HouseGoods> gooQGooddList = houseSer.GetListArray(sql);


                   
                    if (gooQGooddList.Count <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}]，该信息不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName));
                        return false;

                    }

                    //====检查当前商品的成本价格是否和当前库存价格一样
                    decimal avgPrice=Convert.ToDecimal( string.Format("{0:n4}",gooQGooddList[0].GoodAvgPrice));
                    if (avgPrice != model.GoodPrice)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}]，库存均价已经改变请重新做销售单！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName));
                        return false;
                    }
                    //====


                    //需要会中正在申请的单子

                    sql = string.Format("select sum(GoodNum) from Sell_OrderOutHouses left join Sell_OrderOutHouse on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id  where status='执行中' and houseId={0} and gooId={1};",
                        model.HouseID,model.GooId);
                    decimal sellNum = 0;
                    object objSellNum = DBHelp.ExeScalar(sql);
                    if (objSellNum != null && !(objSellNum is DBNull))
                    {
                        sellNum = Convert.ToDecimal(objSellNum);
                    }
                    //修改
                    if ((model.GoodNum+oldNums) > gooQGooddList[0].GoodNum - sellNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}],库存数量剩余[{4}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName, gooQGooddList[0].GoodNum - sellNum));

                        return false;
                    }

                   
                    sql = string.Format("select sum(GoodNum) from Sell_OrderOutHouses left join Sell_OrderOutHouse on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id  where status<>'不通过' and houseId={0} and gooId={1} and Pono='{2}';",
                      model.HouseID, model.GooId, txtPONo.Text);
                    sellNum = 0;
                    objSellNum = DBHelp.ExeScalar(sql);
                    if (objSellNum != null && !(objSellNum is DBNull))
                    {
                        sellNum = Convert.ToDecimal(objSellNum);
                    }

                    CG_POOrderService cgOrder = new CG_POOrderService();
                    var poGoodNums=cgOrder.GetGoodTotalNum(txtPONo.Text, model.GooId);
                    //修改
                    if ((model.GoodNum + oldNums) > poGoodNums - sellNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}],数量剩余[{4}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text, poGoodNums - sellNum));

                        return false;
                    }

                    CAI_POOrderService caiOrder = new CAI_POOrderService();

                    poGoodNums = caiOrder.GetGoodTotalNum_New(txtPONo.Text, model.GooId);
                    //修改
                    if ((model.GoodNum + oldNums) > poGoodNums - sellNum)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}],采购出库数量剩余[{4}],请等待采购！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text, poGoodNums - sellNum));

                        return false;
                    }


                    #region 记录同一个商品的总数量和总销售金额
                    HT h = new HT() { Nums = POOrders[i].GoodNum, PriceTotal = POOrders[i].GoodSellPriceTotal };
                    if (!htTotal.Contains(POOrders[i].GooId))
                        htTotal.Add(POOrders[i].GooId, h);
                    else
                    {
                        var oldH = htTotal[POOrders[i].GooId] as HT;
                        h.Nums += oldH.Nums;
                        h.PriceTotal += oldH.PriceTotal;
                        htTotal[POOrders[i].GooId] = h;
                    } 
                    #endregion
                  //htTotal.Add(POOrders[i]
                }


                string getSql = @"select sum(POTotal) AS POTotal from CG_POOrder 
where Status='通过' and pono='{0}';";

                getSql += "select sum(SellTotal) as SellTotal from Sell_OrderOutHouse where Status<>'不通过' and pono='{0}' ;";

                getSql = string.Format(getSql, txtPONo.Text);


                var  getAllDB=DBHelp.getDataSet(getSql);


//                该项目编号的之前开出的出库单金额（所有审批单通过的+系统中审批正执行中的）+此出库单金额>该项目订单编号金额+该项目追加订单金额  提示“出库金额超过项目金额” 提交不成功！

//这样可以有效规避，连续开出出库单金额已超出的单子通过。
                decimal poLastTotal = 0;

                object ojb;

                if (getAllDB.Tables[0].Rows.Count>0 )
                {
                    ojb=getAllDB.Tables[0].Rows[0][0];
                    if (ojb != null && ojb != DBNull.Value)
                    { 
                        poLastTotal= Convert.ToDecimal(ojb);
                    }                    
                }

                if (getAllDB.Tables[1].Rows.Count > 0)
                {
                    ojb = getAllDB.Tables[1].Rows[0][0];
                    if (ojb != null && ojb != DBNull.Value)
                    {
                        poLastTotal =poLastTotal- Convert.ToDecimal(ojb);
                    }
                }

               

                if (poLastTotal < poTotal)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单金额超过了项目总金额！');</script>"));
                    return false;
                }


                Sell_OrderOutHouseService orderOut = new Sell_OrderOutHouseService();
                
                //修改
                foreach (var key in htTotal.Keys)
                {
                    var mo = htTotal[key] as HT;
                    int goodId = Convert.ToInt32(key);
                    var actTotal = orderOut.GetActGoosTotalNum(goodId, txtPONo.Text, 0);
                    if (actTotal <= 0)
                    {
                        var m=POOrders.FindAll(t => t.GooId == goodId)[0];

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],不需要在进行出库！');</script>",
                            m.GoodName, m.GoodSpec, m.Good_Model));
                        return false;
                    }
                    var chaTotal = mo.Nums - actTotal;
                  
                    if (chaTotal > 0)
                    {
                        var m = POOrders.FindAll(t => t.GooId == goodId)[0];
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],需出库的数量剩余[{3}]！');</script>",
                            m.GoodName, m.GoodSpec, m.Good_Model, actTotal));
                        return false;
                    }
                   
                }
                Session["Orders"] = POOrders;

            }
            else
            {
                if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
                {
                    //记录总的
                    Hashtable htTotal = new Hashtable();
                    decimal poTotal = 0;
                    Dal.JXC.TB_HouseGoodsService houseSer = new VAN_OA.Dal.JXC.TB_HouseGoodsService();
                    List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        var model = POOrders[i];

                        //var goodId = model.GooId;
                        //decimal oldNums = 0;
                        //decimal oldSellPriceTotal = 0;
                        //if (htTotal.Contains(goodId))
                        //{
                        //    var m = htTotal[goodId] as HT;
                        //    oldNums = m.Nums;
                        //    oldSellPriceTotal = m.PriceTotal;
                        //}

                        TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                        if (txtNum != null && txtNum.Text != "")
                        {
                            POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                            if (POOrders[i].GoodNum <= 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");
                                return false;
                            }
                            if (POOrders[i].GoodSellPrice < 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售价格必须大于0！');</script>");
                                return false;
                            }

                            POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                            POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;


                            model = POOrders[i];
                            string sql = string.Format(" 1=1 and HouseId={0} and TB_HouseGoods.GoodId={1}", model.HouseID, model.GooId);

                            List<TB_HouseGoods> gooQGooddList = houseSer.GetListArray(sql);



                            if (gooQGooddList.Count <= 0)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}]，该信息不存在！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName));
                                return false;

                            }
                            //需要会中正在申请的单子

                            sql = string.Format("select sum(GoodNum) from Sell_OrderOutHouses left join Sell_OrderOutHouse on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id  where status='执行中' and houseId={0} and gooId={1} and ids<>{2};",
                                model.HouseID, model.GooId, model.Ids);
                            decimal sellNum = 0;
                            object objSellNum = DBHelp.ExeScalar(sql);
                            if (objSellNum != null && !(objSellNum is DBNull))
                            {
                                sellNum = Convert.ToDecimal(objSellNum);
                            }

                            //修改
                            if (model.GoodNum  > gooQGooddList[0].GoodNum - sellNum)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('仓库[{3}]-商品[{0}]\规格[{1}]\型号[{2}],库存数量剩余[{4}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, model.HouseName, gooQGooddList[0].GoodNum - sellNum));

                                return false;
                            }
                            sql = string.Format("select sum(GoodNum) from Sell_OrderOutHouses left join Sell_OrderOutHouse on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id  where status<>'不通过' and houseId={0} and gooId={1} and ids<>{2} and Pono='{3}';",
                                model.HouseID, model.GooId, model.Ids, txtPONo.Text);
                            sellNum = 0;
                            objSellNum = DBHelp.ExeScalar(sql);
                            if (objSellNum != null && !(objSellNum is DBNull))
                            {
                                sellNum = Convert.ToDecimal(objSellNum);
                            }

                            CG_POOrderService cgOrder = new CG_POOrderService();
                            var poGoodNums = cgOrder.GetGoodTotalNum(txtPONo.Text, model.GooId);
                            //修改
                            if (model.GoodNum  > poGoodNums - sellNum)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}],数量剩余[{4}]！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text, poGoodNums - sellNum));

                                return false;
                            }

                            CAI_POOrderService caiOrder = new CAI_POOrderService();
                            poGoodNums = caiOrder.GetGoodTotalNum_New(txtPONo.Text, model.GooId);
                            //修改
                            if (model.GoodNum > poGoodNums - sellNum)
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('项目[{3}]-商品[{0}]\规格[{1}]\型号[{2}],采购出库数量剩余[{4}],请等待采购！');</script>", model.GoodName, model.GoodSpec, model.Good_Model, txtPONo.Text, poGoodNums - sellNum));

                                return false;
                            }
                           
                        }
                        poTotal += model.GoodSellPriceTotal;
                        #region 记录同一个商品的总数量和总销售金额
                        HT h = new HT() { Nums = model.GoodNum, PriceTotal = model.GoodSellPriceTotal };
                        if (!htTotal.Contains(model.GooId))
                            htTotal.Add(model.GooId, h);
                        else
                        {
                            var oldH = htTotal[POOrders[i].GooId] as HT;
                            h.Nums += oldH.Nums;
                            h.PriceTotal += oldH.PriceTotal;
                            htTotal[POOrders[i].GooId] = h;
                        }
                        #endregion
                    }


                    string getSql = @"select sum(POTotal) AS POTotal from CG_POOrder 
where Status='通过' and pono='{0}';";

                    getSql += "select sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status<>'不通过' and pono='{0}' ;";

                    getSql += @"SELECT sum(GoodSellPrice*GoodNum) as sumTotal FROM Sell_OrderOutHouses
left join Sell_OrderOutHouse on Sell_OrderOutHouse.id=Sell_OrderOutHouses.Id
WHERE Status<>'不通过' and Sell_OrderOutHouses.ids not in (select SellOutOrderId from Sell_OrderInHouses left join Sell_OrderInHouse on Sell_OrderInHouses.id=Sell_OrderInHouse.id where
Status<>'不通过' and pono='{0}') and pono='{0}';";

                    getSql += @"select sum( (Sell_OrderOutHouses.GoodNum-isnull(TuiTotals,0))*Sell_OrderOutHouses.GoodSellPrice)  FROM Sell_OrderOutHouses 
left join Sell_OrderOutHouse on Sell_OrderOutHouse.id=Sell_OrderOutHouses.Id
left join 
(
select SellOutOrderId,sum(GoodNum) as TuiTotals from Sell_OrderInHouses 
left join Sell_OrderInHouse on Sell_OrderInHouses.id=Sell_OrderInHouse.id where
Status<>'不通过' and pono='{0}'
group by SellOutOrderId
) as tb1 on Sell_OrderOutHouses.ids=tb1.SellOutOrderId where pono='{0}' and tb1.SellOutOrderId is not null;";

                    getSql = string.Format(getSql, txtPONo.Text);


                    var getAllDB = DBHelp.getDataSet(getSql);


                    //可开出金额== 项目总金额（含追加）-销售退货单中的金额-已开出出库单不含有销售退货单中对应的出库单商品的他项商品的出库金额
                    //-已开出出库单 含有销售退货单中对应的出库单商品的剩余未退数量对应的剩余出库金额
                    decimal poLastTotal = 0;

                    object ojb;

                    if (getAllDB.Tables[0].Rows.Count > 0)
                    {
                        ojb = getAllDB.Tables[0].Rows[0][0];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            poLastTotal = Convert.ToDecimal(ojb);
                        }
                    }

                    if (getAllDB.Tables[1].Rows.Count > 0)
                    {
                        ojb = getAllDB.Tables[1].Rows[0][0];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                        }
                    }

                    if (getAllDB.Tables[2].Rows.Count > 0)
                    {
                        ojb = getAllDB.Tables[2].Rows[0][0];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                        }
                    }

                    if (getAllDB.Tables[3].Rows.Count > 0)
                    {
                        ojb = getAllDB.Tables[3].Rows[0][0];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                        }
                    }

                    if (poLastTotal < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('出库单金额超过了项目总金额！');</script>"));
                        return false;
                    }

                    Sell_OrderOutHouseService orderOut = new Sell_OrderOutHouseService();
                    //修改
                    foreach (var key in htTotal.Keys)
                    {
                        var mo = htTotal[key] as HT;
                        int goodId = Convert.ToInt32(key);

                        var actTotal = orderOut.GetActGoosTotalNum(goodId, txtPONo.Text, Convert.ToInt32(Request["allE_id"]));
                        if (actTotal < 0)
                        {
                            var m = POOrders.FindAll(t => t.GooId == goodId)[0];
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],不需要在进行出库！');</script>", m.GoodName, m.GoodSpec, m.Good_Model));
                            return false;
                        }
                        var chaTotal = mo.Nums - actTotal;


                        if (chaTotal > 0)
                        {
                            var m = POOrders.FindAll(t => t.GooId == goodId)[0];
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format(@"<script>alert('商品[{0}]\规格[{1}]\型号[{2}],需出库的数量剩余[{3}]！');</script>",
                                m.GoodName, m.GoodSpec, m.Good_Model, actTotal));
                            return false;
                        }

                    }

                    Session["Orders"] = POOrders;
                }
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtName.ReadOnly = true;            
            txtDoPer.ReadOnly = true;           
            txtRemark.ReadOnly = true;
            txtRuTime.ReadOnly = true;
            txtSupplier.ReadOnly = true;          
            Image1.Enabled = false;
            txtPOName.ReadOnly = true;
            txtPONo.ReadOnly = true;
            txtFPNo.ReadOnly =! result;
            txtDaiLi.ReadOnly = !result;
            
        }

        private void SetRole(int Count)
        {
            //打印
            if (Count == 0)
            {
                btnSub.Text = "打印";
                gvList.Columns[7].Visible = false;
                gvList.Columns[8].Visible = true;
            }
        }
       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                btnReCala.Visible = false;
                //请假单子              
                Session["Orders"] = null;              
                lbtnAddFiles.Visible = false;
                gvList.Columns[9].Visible = false;

                gvList.Columns[0].Visible = false;
                //gvList.Columns[1].Visible = false;
                gvList.Columns[8].Visible = false;
                gvList.Columns[14].Visible = false;
                gvList.Columns[17].Visible = false;

                //Dal.BaseInfo.TB_HouseInfoService houseSer = new VAN_OA.Dal.BaseInfo.TB_HouseInfoService();
                //List<Model.BaseInfo.TB_HouseInfo> houseList = houseSer.GetListArray("");
                //ddlHouse.DataSource = houseList;
                //ddlHouse.DataBind();
                //ddlHouse.DataTextField = "houseName";
                //ddlHouse.DataValueField = "id";

               
                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;
                    txtDoPer.Text = use.LoginName;

                   
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        gvList.Columns[9].Visible = true;
                        btnReCala.Visible = true;
                        LinkButton2.Visible = true;
                        txtRuTime.Text = DateTime.Now.ToString();
                        //权限1（销售）
                        lbtnAddFiles.Visible = true;
                        gvList.Columns[0].Visible = true;
                        gvList.Columns[1].Visible = true;


                        gvList.Columns[8].Visible = true;
                        gvList.Columns[14].Visible = true;
                        gvList.Columns[17].Visible = true;

                        gvList.Columns[7].Visible = false;
                        gvList.Columns[13].Visible = false;
                        gvList.Columns[16].Visible = false;

                        //加载初始数据

                        
                        List<Sell_OrderOutHouses> orders = new List<Sell_OrderOutHouses>();
                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();


                      
                       

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人
                            int pro_IDs = 0;
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;


                        }
                    }
                   
                    else//单据审批
                    {

                        ViewState["POOrdersIds"] = "";
                        

                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        if (eforms.Count > 0)
                        {
                            string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                            for (int i = 0; i < eforms.Count; i++)
                            {
                                string per = "";
                                if (eforms[i].consignor != null && eforms[i].consignor != 0)
                                {
                                    per = eforms[i].Consignor_Name + "(委托人：" + eforms[i].Audper_Name + ")";
                                }
                                else
                                {
                                    per = eforms[i].Audper_Name;
                                }
                                mess += string.Format("<tr><td align='center'>第{0}步</td><td>序号{0}：{1}</td><td><span style='color:red;'>{2}</span>[<span style='color:blue;'>{3} {4}</span>]<br/>意见：{5}</td></tr>",
                                    i + 1, eforms[i].RoleName
, per, eforms[i].resultState, eforms[i].doTime, eforms[i].idea);
                            }
                            mess += "</table>";
                            lblMess.Text = mess;
                        }

                        ViewState["EformsCount"] = eforms.Count;




                        #region  加载 请假单数据

                        Sell_OrderOutHouseService mainSer = new Sell_OrderOutHouseService();
                        Sell_OrderOutHouse pp = mainSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtName.Text = pp.CreateName;

                         
                        txtDoPer.Text = pp.DoPer;
                    
                        txtRemark.Text = pp.Remark;
                        txtRuTime.Text = pp.RuTime.ToString();
                        txtSupplier.Text = pp.Supplier;

                        //ddlHouse.Text = pp.HouseID.ToString();
                        txtDaiLi.Text = pp.DaiLi;
                        txtFPNo.Text = pp.FPNo;
                        txtPOName.Text = pp.POName;
                        txtPONo.Text = pp.PONo;
                        if (pp.ProNo != null)
                            lblProNo.Text = pp.ProNo;

                        if (pp.IsPrint)
                        {
                            btnPrint.Visible = true;
                        }
                        Sell_OrderOutHousesService ordersSer = new Sell_OrderOutHousesService();
                        List<Sell_OrderOutHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderOutHouses.id=" + Request["allE_id"]);

                        GetPOSellPrice(orders);

                        Session["Orders"] = orders;
                        ViewState["OrdersCount"] = orders.Count;

                        gvList.DataSource = orders;
                        gvList.DataBind();

                        #endregion
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            btnSub.Visible = false;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                            lblResult.Visible = false;
                            lblYiJian.Visible = false;
                            ddlResult.Visible = false;
                            txtResultRemark.Visible = false;

                            setEnable(false);
                           

                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {
                                SetRole(eforms.Count);
                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;
                                }
                                else
                                {
                                    int ids = 0;

                                    List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);

                                    ViewState["ids"] = ids;
                                    if (roleUserList != null)
                                    {
                                        ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";   

                                    }

                                }
                                setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    SetRole(eforms.Count);
                                    ViewState["ifConsignor"] = true;
                                    if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                    {
                                        lblPer.Visible = false;
                                        ddlPers.Visible = false;
                                    }
                                    else
                                    {
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                        ViewState["ids"] = ids;
                                        if (roleUserList != null)
                                        {
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId"; 
                                        }

                                    } setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                }
                                else
                                {
                                    btnSub.Visible = false;
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;

                                    lblResult.Visible = false;
                                    lblYiJian.Visible = false;
                                    ddlResult.Visible = false;
                                    txtResultRemark.Visible = false;
                                    setEnable(false);
                                }
                            }

                        }
                    }





                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {

                    
                    #region 获取单据基本信息

                    Sell_OrderOutHouse order = new Sell_OrderOutHouse();

                    int CreatePer=Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    order.CreateUserId = CreatePer;
                 
                    order.DoPer = txtDoPer.Text;
                    //order.HouseID = Convert.ToInt32(ddlHouse.SelectedValue);
                   
                    order.RuTime = Convert.ToDateTime(txtRuTime.Text);
                    order.Remark = txtRemark.Text;
                    order.Supplier = txtSupplier.Text;
                    order.FPNo = txtFPNo.Text;
                    order.DaiLi = txtDaiLi.Text;
                    order.POName = txtPOName.Text;
                    order.PONo = txtPONo.Text;
                    
                  
                    List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

                    #endregion
                    if (Request["allE_id"] == null)//单据增加+//再次编辑)
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = CreatePer;
                        eform.appPer = userId;
                        eform.appTime = DateTime.Now;
                        eform.createPer = Convert.ToInt32(Session["currentUserId"].ToString());
                        eform.createTime = DateTime.Now;
                        eform.proId = Convert.ToInt32(Request["ProId"]);

                        if (ddlPers.Visible == false)
                        {
                            eform.state = "通过";
                            eform.toPer = 0;
                            eform.toProsId = 0;
                        }
                        else
                        {

                            eform.state = "执行中";
                            eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                            eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                        }
                       
                       
                        int MainId = 0;


                        Sell_OrderOutHouseService POOrderSer = new Sell_OrderOutHouseService();
                        if (POOrderSer.addTran(order, eform, POOrders, out MainId) > 0)
                        {

                            if (ddlPers.Visible == false)
                            {
                                POOrderSer.SellOrderUpdatePoStatus2(txtPONo.Text);
                                //POOrderSer.SellOrderUpdatePoStatus(txtPONo.Text,eform.state);
                                new Sell_OrderOutHouseBackService().SellOrderBackUpdatePoStatus(txtPONo.Text);

                            }
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");
                        }
                    }
                    else//审核
                    {




                        #region 本单据的ID
                        order.Id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();


                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = CreatePer;
                        eform.appPer = userId;




                        tb_EFormService fromSer = new tb_EFormService();
                        if (ViewState["ifConsignor"] != null && Convert.ToBoolean(ViewState["ifConsignor"]) == true)
                        {
                            forms.audPer = fromSer.getCurrentAuPer(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                            forms.consignor = Convert.ToInt32(Session["currentUserId"]);
                        }
                        else
                        {
                            forms.audPer = Convert.ToInt32(Session["currentUserId"]);
                            forms.consignor = 0;
                        }
                        if (fromSer.ifAudiPerAndCon(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])) == false)
                        {
                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                            return;
                        }
                        forms.doTime = DateTime.Now;
                        forms.e_Id = Convert.ToInt32(Request["EForm_Id"]); //fromSer.getCurrentid(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.idea = txtResultRemark.Text;
                        forms.prosIds = fromSer.getCurrenttoProsId(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.resultState = ddlResult.Text;
                        forms.RoleName = fromSer.getCurrenttoRoleName(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        if (ddlPers.Visible == false)//说明为最后一次审核
                        {
                            eform.state = ddlResult.Text;
                            eform.toPer = 0;
                            eform.toProsId = 0;

                        }
                        else
                        {
                            if (ddlResult.Text == "不通过")
                            {
                                eform.state = ddlResult.Text;
                                eform.toPer = 0;
                                eform.toProsId = 0;
                            }
                            else
                            {
                                eform.state = "执行中";
                                eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                                eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                            }
                        }
                        Sell_OrderOutHouseService POOrderSer = new Sell_OrderOutHouseService();
                        string IDS = ViewState["POOrdersIds"].ToString();

                        string url="";
                        if (POOrderSer.updateTran(order, eform, forms, POOrders, IDS))
                        {
                            if (ddlPers.Visible == false)
                            {
                                POOrderSer.SellOrderUpdatePoStatus2(txtPONo.Text);
                                //POOrderSer.SellOrderUpdatePoStatus(txtPONo.Text, eform.state);
                                new Sell_OrderOutHouseBackService().SellOrderBackUpdatePoStatus(txtPONo.Text);
                            }

                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                            if (btnSub.Text == "打印")
                            {
                                //打印销售单子
                                string sql = "update Sell_OrderOutHouse set IsPrint=1,RuTime=getdate() where id=" + order.Id;
                                DBHelp.ExeCommand(sql);
                                //string url = string.Format("WFSell_OrderOutHousePrint.aspx?Id={0}", Request["allE_id"]);

                                //Response.Redirect("~/JXC/" + url);
                                btnSub.Visible = false;



                                url = string.Format("../JXC/NewWFSell_OrderOutHousePrint.aspx?Id={0}", Request["allE_id"]);
                                 Session["printUrl"] = url;


                               
                               

                            }
                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {   
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

                        }
                    }
                }
            }
        }





        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Orders"] != null)
            {
                if (Session["DioSellWFHouseGoods"] != null)
                {
                    List<Sell_OrderOutHouses> selectedList = Session["DioSellWFHouseGoods"] as List<Sell_OrderOutHouses>;
                    if (selectedList.Count > 0)
                    {
                        List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;
                        if (POOrders == null) POOrders = new List<Sell_OrderOutHouses>();
                        POOrders.AddRange(selectedList);

                       
                        //GetPOSellPrice(POOrders);
                        foreach (var m in POOrders)
                        {
                            m.GoodSellPrice = m.GoodOriSellPrice;
                        }
                        Session["DioSellWFHouseGoods"] = null;
                        gvList.DataSource = POOrders;
                        gvList.DataBind();
                        Session["Orders"] = POOrders;
                    }
                }      
            }

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Orders"] != null)
            {
                List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;
                ViewState["POOrdersCount"] = POOrders.Count;
                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "0")
            {
                if (ViewState["POOrdersIds"] == null)
                {
                    ViewState["POOrdersIds"] = this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                }
                else
                {
                    string ids = ViewState["POOrdersIds"].ToString();
                    ids += this.gvList.DataKeys[e.RowIndex].Value.ToString() + ",";
                    ViewState["POOrdersIds"] = ids;
                }
            }

            if (Session["Orders"] != null)
            {
                List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

                POOrders.RemoveAt(e.RowIndex);


                ViewState["POOrdersCount"] = POOrders.Count;

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        Sell_OrderOutHouses SumOrders = new Sell_OrderOutHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderOutHouses model = e.Row.DataItem as Sell_OrderOutHouses;
                SumOrders.GoodNum += model.GoodNum;
                SumOrders.Total += model.Total;
                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;

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
                setValue(e.Row.FindControl("lblTotal1") as Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额    
            }

        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

     

        protected object getDatetime(object time)
        {
            if (time != null)
            {
                return Convert.ToDateTime(time).ToShortDateString();
            }
            return time;
        }


        private void ShowData()
        { 
            
        }
        protected void txtChcekProNo_TextChanged(object sender, EventArgs e)
        {

            //CAI_OrderInHousesService checksSer = new CAI_OrderInHousesService();
            //List<CAI_OrderInHouses> allChecks = checksSer.GetListArray(string.Format(" CAI_OrderInHouses.id=( select id from CAI_OrderInHouse where ProNo='{0}')", txtChcekProNo.Text));
            //List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

            //foreach (var model in allChecks)
            //{
            //    Sell_OrderOutHouses houseModel = new Sell_OrderOutHouses();
            //    houseModel.Good_Model = model.Good_Model;
            //    houseModel.GoodName = model.GoodName;
            //    houseModel.GoodNo = model.GoodNo;
            //    houseModel.GoodNum = model.GoodNum;
            //    houseModel.GoodPrice = model.GoodPrice;
            //    houseModel.GoodSpec = model.GoodSpec;
            //    houseModel.GoodTypeSmName = model.GoodTypeSmName;
            //    houseModel.GoodUnit = model.GoodUnit;
            //    houseModel.GooId = model.GooId;
            //    houseModel.Total = model.Total;

            //    houseModel.GoodSellPrice = 0;
            //    houseModel.GoodSellPriceTotal = 0;

            //    POOrders.Add(houseModel);
            //}
            //Session["Orders"] = POOrders;
            //gvList.DataSource = POOrders;
            //gvList.DataBind();
        }
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));

                txtSupplier.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;




                string getSql = @"select sum(POTotal) AS POTotal from CG_POOrder 
where Status='通过' and pono='{0}';";

                getSql += "select sum(SellTotal) as SellTotal from Sell_OrderOutHouse where Status<>'不通过' and pono='{0}' ;";

//                getSql += "select sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status<>'不通过' and pono='{0}' ;";

//                getSql += @"SELECT sum(GoodSellPrice*GoodNum) as sumTotal FROM Sell_OrderOutHouses
//left join Sell_OrderOutHouse on Sell_OrderOutHouse.id=Sell_OrderOutHouses.Id
//WHERE Status<>'不通过' and Sell_OrderOutHouses.ids not in (select SellOutOrderId from Sell_OrderInHouses left join Sell_OrderInHouse on Sell_OrderInHouses.id=Sell_OrderInHouse.id where
//Status<>'不通过' and pono='{0}') and pono='{0}';";

//                getSql += @"select sum( (Sell_OrderOutHouses.GoodNum-isnull(TuiTotals,0))*Sell_OrderOutHouses.GoodSellPrice)  FROM Sell_OrderOutHouses 
//left join Sell_OrderOutHouse on Sell_OrderOutHouse.id=Sell_OrderOutHouses.Id
//left join 
//(
//select SellOutOrderId,sum(GoodNum) as TuiTotals from Sell_OrderInHouses 
//left join Sell_OrderInHouse on Sell_OrderInHouses.id=Sell_OrderInHouse.id where
//Status<>'不通过' and pono='{0}'
//group by SellOutOrderId
//) as tb1 on Sell_OrderOutHouses.ids=tb1.SellOutOrderId where pono='{0}' and tb1.SellOutOrderId is not null;";

                getSql = string.Format(getSql, txtPONo.Text);


                var getAllDB = DBHelp.getDataSet(getSql);


                //可开出金额== 项目总金额（含追加）-销售退货单中的金额-已开出出库单不含有销售退货单中对应的出库单商品的他项商品的出库金额
                //-已开出出库单 含有销售退货单中对应的出库单商品的剩余未退数量对应的剩余出库金额
                decimal poLastTotal = 0;

                object ojb;

                if (getAllDB.Tables[0].Rows.Count > 0)
                {
                    ojb = getAllDB.Tables[0].Rows[0][0];
                    if (ojb != null && ojb != DBNull.Value)
                    {
                        poLastTotal = Convert.ToDecimal(ojb);
                    }
                }

                if (getAllDB.Tables[1].Rows.Count > 0)
                {
                    ojb = getAllDB.Tables[1].Rows[0][0];
                    if (ojb != null && ojb != DBNull.Value)
                    {
                        poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                    }
                }

                //if (getAllDB.Tables[2].Rows.Count > 0)
                //{
                //    ojb = getAllDB.Tables[2].Rows[0][0];
                //    if (ojb != null && ojb != DBNull.Value)
                //    {
                //        poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                //    }
                //}

                //if (getAllDB.Tables[3].Rows.Count > 0)
                //{
                //    ojb = getAllDB.Tables[3].Rows[0][0];
                //    if (ojb != null && ojb != DBNull.Value)
                //    {
                //        poLastTotal = poLastTotal - Convert.ToDecimal(ojb);
                //    }
                //}

                //该项目订单编号的所有出库单金额+此出库单金额>该项目订单编号金额+该项目追加订单金额-该项目订单编号销售退货单的金额
                //时，提交不成功，提示 “出库单金额超过了项目总金额“
//                string allOutIng = string.Format(@"select sum(GoodSellPrice*GoodNum)  from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id
//where status<>'不通过' and PONO='{0}'", txtPONo.Text);
//                var goodSellTotal = DBHelp.ExeScalar(allOutIng);
//                decimal sellTotal = 0;
//                if (goodSellTotal != null && !(goodSellTotal is DBNull))
//                {
//                    sellTotal = Convert.ToDecimal(goodSellTotal);
//                }
                
//                CG_POOrderService orderSer = new CG_POOrderService();
//                var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", txtPONo.Text));
//                if (list.Count > 0)
//                {
//                    lblLastPOTotal.Text = string.Format("可开出金额:{0}",
//                        (list[0].POTotal-sellTotal));                    
//                }
//                else
//                {
//                    lblLastPOTotal.Text = "可开出金额:0.00";                  
//                }
//                //===========================================================
                lblLastPOTotal.Text = string.Format("可开出金额:{0}", poLastTotal);
                
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string url = string.Format("WFSell_OrderOutHousePrint.aspx?Id={0}",Request["allE_id"]);

            Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
        }

        protected void btnReCala_Click(object sender, EventArgs e)
        {
            if (Request["allE_id"] == null)
            {
                List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;

                if (POOrders == null || POOrders.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请添加明显信息！');</script>");
                    return ;
                }

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null)
                    {
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        if (POOrders[i].GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");

                            return;
                        }
                    }

                    TextBox txtGoodRemark = gvList.Rows[i].FindControl("txtGoodRemark") as TextBox;
                    if (txtGoodRemark != null)
                    {
                        POOrders[i].GoodRemark = txtGoodRemark.Text;
                    }


                    TextBox txtGoodSellPrice = gvList.Rows[i].FindControl("txtGoodSellPrice") as TextBox;
                    if (txtGoodSellPrice != null)
                    {
                        POOrders[i].GoodSellPrice = Convert.ToDecimal(txtGoodSellPrice.Text);

                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;

                    }
                }

                gvList.DataSource = POOrders;
                gvList.DataBind();

            }
            else
            {

                
                List<Sell_OrderOutHouses> POOrders = Session["Orders"] as List<Sell_OrderOutHouses>;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    TextBox txtNum = gvList.Rows[i].FindControl("txtNum") as TextBox;
                    if (txtNum != null && txtNum.Text != "")
                    {
                        POOrders[i].GoodNum = Convert.ToDecimal(txtNum.Text);
                        if (POOrders[i].GoodNum <= 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量必须大于0！');</script>");
                            return ;
                        }

                        POOrders[i].GoodSellPriceTotal = POOrders[i].GoodSellPrice * POOrders[i].GoodNum;
                        POOrders[i].Total = POOrders[i].GoodPrice * POOrders[i].GoodNum;                     
                       

                    }
                }

                gvList.DataSource = POOrders;
                gvList.DataBind();
            }
        }
   

      

     
    }
}
