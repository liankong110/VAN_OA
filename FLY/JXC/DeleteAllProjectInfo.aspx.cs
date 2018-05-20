using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Data;

namespace VAN_OA.JXC
{
    public partial class DeleteAllProjectInfo : BasePage
    {
        CG_POOrderService POSer = new CG_POOrderService();
        CG_POOrdersService ordersSer = new CG_POOrdersService();

        /// <summary>
        /// 检查项目信息是否有关联
        /// </summary>
        /// <param name="pono"></param>
        /// <returns></returns>
        private bool CheckPONOGuanLian(string pono)
        {
            string sql = string.Format(@"declare  @PONO varchar(50);
set @PONO='{0}';
--1    采购退货
select '采购退货单' as myProId,count(*) as cou
from CAI_OrderOutHouse 
where CAI_OrderOutHouse.PONO=@PONO
union all
--2    销售发票
select '销售发票' as myProId,count(*) as cou  from Sell_OrderFP where NowGuid='' and PONO=@PONO
union all
--3    销售发票
select '销售发票修改' as myProId,count(*) as cou  from Sell_OrderFP where NowGuid<>''  and PONO=@PONO
union all
--4    发票单签回
select '发票单签回' as myProId,count(*) as cou  from Sell_OrderFPBack where   PONO=@PONO
union all
--5    销售退货
select '销售退货单' as myProId,count(*) as cou from Sell_OrderInHouse  where   PONO=@PONO
union all
--6    销售出库
select '销售出库单' as myProId,count(*) as cou  from Sell_OrderOutHouse  where   PONO=@PONO
union all
--7    出库单签回
select '出库单签回' as myProId,count(*) as cou  from Sell_OrderOutHouseBack  where   PONO=@PONO
union all
--8    到款单
select '到款单' as myProId,count(*) as cou from TB_ToInvoice  where   PONO=@PONO
union all
--9 申请请款单
select  '申请请款单' as myProId,count(*) as cou from tb_FundsUse where   PONO=@PONO
union all
--10 预期报销单
select  '预期报销单' as myProId,count(*) as cou from Tb_DispatchList where   PONO=@PONO
union all
--11 预期报销单(油费报销)
select  '预期报销单(油费报销)' as myProId,count(*) as cou from Tb_DispatchList where   PONO=@PONO
union all
--12
select   '供应商预付款单' as myProId,count(*) as cou
 from TB_SupplierAdvancePayments
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id   where   CAI_POOrder.PONO=@PONO
union all
--13
select  '供应商付款单' as myProId,count(*) as cou from TB_SupplierInvoices 
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
where IsYuFu=0 and CAI_OrderInHouse.PONO=@PONO
union all
--14
select  '供应商付款单（预付单转支付单）' as myProId,count(*) as cou from TB_SupplierInvoices 
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id where IsYuFu=1 and CAI_OrderInHouse.PONO=@PONO
union all
--15
--私车公用申请单
select  '私车公用申请单' as myProId,count(*) as cou from tb_UseCar where   PONO=@PONO
union all
--16
--用车明细表
select  '用车明细单' as myProId,count(*) as cou  from TB_UseCarDetail where   PONO=@PONO
union all
--17 加班单
select  '加班单' as myProId,count(*) as cou from tb_OverTime where   PONO=@PONO
union all
--18 邮寄文档快递表
select  '邮寄文档快递表' as myProId,count(*) as cou from tb_Post where   PONO=@PONO
union all
--19 公交车使用
select '公交卡使用' as myProId,count(*) as cou from TB_BusCardUse where   PONO=@PONO
union ALL
SELECT '采购入库'as myProId,count(*) as cou FROM CAI_OrderInHouse  where   PONO=@PONO", pono);
           var dt= DBHelp.getDataTable(sql);
           string mess = "";
           foreach (DataRow m in dt.Rows)
           { 
                if(Convert.ToInt32(m[1])>0)
                {
                    mess += m[0]+",";                   
                }
           }
           if (mess != "")
           {
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除[" + mess.Trim(',') + "]信息！');</script>");
               return false;
           }
           return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                List<CG_POOrder> pOOrderList = new List<CG_POOrder>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CG_POOrders> orders = new List<CG_POOrders>();
                gvList.DataSource = orders;
                gvList.DataBind();



                //子单
                List<CAI_POOrders> orderCais = new List<CAI_POOrders>();
                gvCaiMain.DataSource = orderCais;
                gvCaiMain.DataBind();
                
                

                List<CAI_POCai> caiXiaoShouList = new List<CAI_POCai>();
                gvCaiXiaoShou.DataSource = caiXiaoShouList;
                gvCaiXiaoShou.DataBind();
                
            }
        }
        /// <summary>
        /// 项目号删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSub_Click(object sender, EventArgs e)
        {
            if (txtPONo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目编码！');</script>");
                return;
            }
            if (CheckPoNO(txtPONo.Text) == false)
            {
                return;
            }
            //查询项目信息是否存在
            string sql = string.Format("select count(*) from CG_POOrder where PONo='{0}'", txtPONo.Text.Trim());
            var obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return;
            }
            if (CheckPONOGuanLian(txtPONo.Text.Trim()) == false)
            {
                return;
            }
            ////删除采购信息
            ////检查该项目信息有没有入库信息
            //sql = string.Format("select count(*) from CAI_OrderInHouse where poNo='{0}'", txtPONo.Text);
            //obj = DBHelp.ExeScalar(sql);
            //if (Convert.ToInt32(obj) > 0)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该项目，已经完成入库，请按采购退货流程！');</script>");
            //    return;
            //}

            ////检查有没有出库信息
            //sql = string.Format("select count(*) from Sell_OrderOutHouse where poNo='{0}'", txtPONo.Text);
            //obj = DBHelp.ExeScalar(sql);
            //if (Convert.ToInt32(obj) > 0)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该项目，已经完成出库，请按销售退货流程！');</script>");
            //    return;
            //}
//            //检查项目预付款单
//            sql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
//on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id left join 
//CAI_OrderChecks on CAI_OrderChecks.CaiId=TB_SupplierAdvancePayments.CaiIds  
//where TB_SupplierAdvancePayment.status<>'不通过' and CAI_OrderCheckS.PONo='{0}'", txtPONo.Text);
//            obj = DBHelp.ExeScalar(sql);
//            if (Convert.ToInt32(obj) > 0)
//            {
//                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除项目预付款信息！');</script>");
//                return;
//            }
//            //如果已经产生了 基于这个项目编号的请款单，预期报销单，私车公用车单，预期报销单油费，
//            //申请用车单，公共交通费单。建议就不要去动 这个项目的所有关联 单，而是提示“请采用采购退货等方式”，退出
//            sql = string.Format(@"declare @allCount int;
//set @allCount=0;
//select @allCount=@allCount+count(*) from TB_BusCardUse where poNo='{0}';--公交车费
//select @allCount=@allCount+count(*) from tb_UseCar where poNo='{0}';--私车油耗费
//select @allCount=@allCount+count(*) from TB_UseCarDetail where poNo='{0}';--用车申请油耗费
//select @allCount=@allCount+count(*) from tb_FundsUse where poNo='{0}';--会务费用  人工费
//select @allCount=@allCount+count(*) from Tb_DispatchList where poNo='{0}';--非材料报销 
//select isnull(@allCount,0)", txtPONo.Text);
//            obj = DBHelp.ExeScalar(sql);
//            if (Convert.ToInt32(obj) > 0)
//            {
//                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('已经产生了 基于这个项目编号的请款单 或 预期报销单 或 私车公用车单 或 预期报销单油费 或 申请用车单 或 公共交通费单 ！请采用采购退货等方式！');</script>");
//                return;
//            }

            //删除采购检验单信息
            //删除采购订单信息
            //删除项目信息
            sql = "";

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where PONO='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where PONO='{0}');";//删除审批流程单据
            sql += "delete from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where PONO='{0}'); ";//删除检验单主单据
            sql += "delete from CAI_OrderChecks where PONO='{0}'; ";//删除检验单子单据

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where PONO='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where PONO='{0}');";//删除审批流程单据
            sql += "delete from  CAI_POCai where Id in (select Id from CAI_POOrder where PONO='{0}'); ";//删除采购订单 采购子单据
            sql += "delete from  CAI_POOrders where Id in (select Id from CAI_POOrder where PONO='{0}'); ";//删除采购订单 项目子单据
            sql += "delete from CAI_POOrder where PONO='{0}'; ";//删除采购订单主单据

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where PONO='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where PONO='{0}');";//删除审批流程单据
            sql += "delete from CG_POCai where Id in (select Id from CG_POOrder where PONO='{0}'); ";//删除项目订单 采购子单据
            sql += "delete from CG_POOrders where Id in (select Id from CG_POOrder where PONO='{0}'); ";//删除项目订单 项目子单据
            sql += "delete from CG_POOrder where PONO='{0}'; ";//删除项目订单主单据

            sql = string.Format(sql, txtPONo.Text.Trim());
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();

                }
                catch (Exception)
                {

                    tan.Rollback();
                    conn.Close();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                    return;
                }
                conn.Close();
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
            }


        }

        /// <summary>
        /// 项目单据号+商品代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePro_GoodNo_Click(object sender, EventArgs e)
        {
            txtPOProNo1.Text = txtPOProNo1.Text.Trim();
            if (txtPOProNo1.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目单据号！');</script>");
                return;
            }
            if (CheckPoNO(txtPOProNo1.Text) == false)
            {
                return;
            }

            if (txtGoodNo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写商品号码！');</script>");
                return;
            }

            string check = string.Format(@"select top 1 PONo from CG_POOrder where ProNo='{0}' and exists (select 1 from CG_POOrders left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
where TB_Good.GoodNo='{1}' and CG_POOrders.Id=CG_POOrder.Id)", txtPOProNo1.Text, txtGoodNo.Text);
            var myObj = DBHelp.ExeScalar(check);
            if (myObj == null || myObj is DBNull)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('未找到任务项目信息！');</script>");
                return;
            }

            var sql = string.Format("select GoodId from TB_Good where GoodNo='{0}'", txtGoodNo.Text);
            var objGood = DBHelp.ExeScalar(sql);
            if (objGood is DBNull || Convert.ToInt32(objGood) == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品信息不存在！');</script>");
                return;
            }

            //删除采购信息
            //检查该项目信息有没有入库信息    

            //删除采购信息
            //检查该项目信息有没有入库信息          
            sql = string.Format(@"select count(*) from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
where ChcekProNo in (select proNo from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}'))) and CAI_OrderInHouses.GooId={1}", txtPOProNo1.Text, objGood);

            var obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品已入库，无法删除，请按销售退货处理！');</script>");
                return;
            }

            sql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
where  CAI_POOrder.CG_ProNo='{0}' and CAI_POCai.GoodId={1}", txtPOProNo1.Text, objGood);

            obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除项目预付款信息！');</script>");
                return;
            }

//            //检查项目预付款单
//            sql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
//on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id left join 
//CAI_OrderChecks on CAI_OrderChecks.CaiId=TB_SupplierAdvancePayments.CaiIds  
//left join CAI_OrderCheck on CAI_OrderCheck.Id=CAI_OrderChecks.CheckId
//where TB_SupplierAdvancePayment.status<>'不通过' and CAI_OrderCheckS.CheckGoodId={1}
//AND  CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')", txtPOProNo1.Text, objGood);
//            obj = DBHelp.ExeScalar(sql);
//            if (Convert.ToInt32(obj) > 0)
//            {
//                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除项目预付款信息！');</script>");
//                return;
//            }
            //删除采购检验单信息
            //删除采购订单信息
            //删除项目信息
            sql = "";

            //sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')));";//删除审批流程单据
            //sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}'));";//删除审批流程单据
            //sql += "delete from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')); ";//删除检验单主单据
            sql += "delete from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}') and CheckGoodId={1}; ";//删除检验单子单据

            //sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where CG_ProNo='{0}'));";//删除审批流程单据
            //sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where CG_ProNo='{0}');";//删除审批流程单据
            sql += "delete from  CAI_POCai where Id in (select Id from CAI_POOrder where CG_ProNo='{0}') and GoodId={1}; ";//删除采购订单 采购子单据
            sql += "delete from  CAI_POOrders where Id in (select Id from CAI_POOrder where CG_ProNo='{0}') and GoodId={1}; ";//删除采购订单 项目子单据
            //sql += "delete from CAI_POOrder where CG_ProNo='{0}'; ";//删除采购订单主单据

            //sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where ProNo='{0}'));";//删除审批流程单据
            //sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where ProNo='{0}');";//删除审批流程单据
            sql += "delete from CG_POCai where Id in (select Id from CG_POOrder where ProNo='{0}')  and GoodId={1}; ";//删除项目订单 采购子单据
            sql += "delete from CG_POOrders where Id in (select Id from CG_POOrder where ProNo='{0}')  and GoodId={1}; ";//删除项目订单 项目子单据
            
            //sql += "delete from CG_POOrder where ProNo='{0}'; ";//删除项目订单主单据

            sql = string.Format(sql, txtPOProNo1.Text, objGood);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    conn.Close();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                    return;
                }
                conn.Close();
            }
            //更新下订单的销售金额
            string updatePOTotal = string.Format(@"declare @Pototal decimal(18, 6)
set @Pototal=0
select @Pototal=SUM(SellPrice*Num) from CG_POOrders where ID=(select ID from CG_POOrder where ProNo='{0}')
update CG_POOrder set POTotal=@Pototal where ProNo='{0}'", txtPOProNo1.Text);
            DBHelp.ExeCommand(updatePOTotal);

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
            //销售状态
            new Sell_OrderOutHouseService().SellOrderUpdatePoStatus2(myObj.ToString());

            //发票状态
            new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(myObj.ToString(), "");

            //到款单
            new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(myObj.ToString());
        }

        /// <summary>
        /// 根据项目编号 和 单据号 来删除制定的商品信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteProNo_Click(object sender, EventArgs e)
        {
            if (txtPOProNo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目单据号！');</script>");
                return;
            }
            string check = string.Format("select top 1 PONo from CG_POOrder where ProNo='{0}' and IFZhui=1", txtPOProNo.Text);
            var myObj = DBHelp.ExeScalar(check);
            if (myObj == null || myObj is DBNull)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('未找到任务项目信息！');</script>");
                return;
            }


            //查询项目信息是否存在
            string sql = string.Format("select count(*) from CG_POOrder where PONo='{0}'", myObj.ToString());
            var obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return;
            }

            //删除采购信息
            //检查该项目信息有没有入库信息          
            sql = string.Format(@"select count(*) from CAI_OrderInHouse
where ChcekProNo in (select proNo from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')))", txtPOProNo.Text);

            obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('商品已入库，无法删除此追加订单，请按销售退货处理！');</script>");
                return;
            }
            //检查项目预付款单
            //检查项目预付款单
            sql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
where  CAI_POOrder.CG_ProNo='{0}'", txtPOProNo.Text);
            obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除项目预付款信息！');</script>");
                return;
            }
            //            //检查有没有出库信息
            //            sql = string.Format("select count(*) from Sell_OrderOutHouse where poNo='{0}'", myObj.ToString());
            //            obj = DBHelp.ExeScalar(sql);
            //            if (Convert.ToInt32(obj) > 0)
            //            {
            //                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该项目，已经完成出库，请按销售退货流程！');</script>");
            //                return;
            //            }
            //            //如果已经产生了 基于这个项目编号的请款单，预期报销单，私车公用车单，预期报销单油费，
            //            //申请用车单，公共交通费单。建议就不要去动 这个项目的所有关联 单，而是提示“请采用采购退货等方式”，退出
            //            sql = string.Format(@"declare @allCount int;
            //set @allCount=0;
            //select @allCount=@allCount+count(*) from TB_BusCardUse where poNo='{0}';--公交车费
            //select @allCount=@allCount+count(*) from tb_UseCar where poNo='{0}';--私车油耗费
            //select @allCount=@allCount+count(*) from TB_UseCarDetail where poNo='{0}';--用车申请油耗费
            //select @allCount=@allCount+count(*) from tb_FundsUse where poNo='{0}';--会务费用  人工费
            //select @allCount=@allCount+count(*) from Tb_DispatchList where poNo='{0}';--非材料报销 
            //select isnull(@allCount,0)", myObj.ToString());
            //            obj = DBHelp.ExeScalar(sql);
            //            if (Convert.ToInt32(obj) > 0)
            //            {
            //                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('已经产生了 基于这个项目编号的请款单 或 预期报销单 或 私车公用车单 或 预期报销单油费 或 申请用车单 或 公共交通费单 ！请采用采购退货等方式！');</script>");
            //                return;
            //            }

            //删除采购检验单信息
            //删除采购订单信息
            //删除项目信息
            sql = "";

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}'));";//删除审批流程单据
            sql += "delete from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}')); ";//删除检验单主单据
            sql += "delete from CAI_OrderChecks where CaiProNo in (select proNo from CAI_POOrder where CG_ProNo='{0}'); ";//删除检验单子单据

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where CG_ProNo='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where CG_ProNo='{0}');";//删除审批流程单据
            sql += "delete from  CAI_POCai where Id in (select Id from CAI_POOrder where CG_ProNo='{0}'); ";//删除采购订单 采购子单据
            sql += "delete from  CAI_POOrders where Id in (select Id from CAI_POOrder where CG_ProNo='{0}'); ";//删除采购订单 项目子单据
            sql += "delete from CAI_POOrder where CG_ProNo='{0}'; ";//删除采购订单主单据

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where ProNo='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='项目订单') and allE_Id in (select Id from CG_POOrder where ProNo='{0}');";//删除审批流程单据
            sql += "delete from CG_POCai where Id in (select Id from CG_POOrder where ProNo='{0}'); ";//删除项目订单 采购子单据
            sql += "delete from CG_POOrders where Id in (select Id from CG_POOrder where ProNo='{0}'); ";//删除项目订单 项目子单据
            sql += "delete from CG_POOrder where ProNo='{0}'; ";//删除项目订单主单据

            sql = string.Format(sql, txtPOProNo.Text);


            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();

                }
                catch (Exception)
                {

                    tan.Rollback();
                    conn.Close();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                    return;
                }
                conn.Close();


            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
            //销售状态
            new Sell_OrderOutHouseService().SellOrderUpdatePoStatus2(myObj.ToString());

            //发票状态
            new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(myObj.ToString(), "");

            //到款单
            new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(myObj.ToString());


        }

        private void Show(int type)
        {
            string sql = " 1=1 ";

            if (type == 0)
            {
                if (txtPONo.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目编码！');</script>");
                    return;
                }
                if (txtPONo.Text.Trim() != "")
                {
                    if (CheckPoNO(txtPONo.Text.Trim()) == false)
                    {
                        return;
                    }
                    sql += string.Format(" and IFZhui=0 and CG_POOrder.PONo='{0}'", txtPONo.Text.Trim());
                }
            }

            if (type == 1)
            {
                if (txtPOProNo.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目单据号！');</script>");
                    return;
                }
                if (txtPOProNo.Text.Trim() != "")
                {
                    if (CheckProNo(txtPOProNo.Text.Trim()) == false)
                    {
                        return;
                    }
                    sql += string.Format(" and ProNo='{0}'", txtPOProNo.Text.Trim());
                }
            }

            if (type == 2)
            {
                if (txtPOProNo1.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写项目单据号！');</script>");
                    return;
                }
                if (txtPOProNo1.Text.Trim() != "")
                {
                    if (CheckProNo(txtPOProNo1.Text.Trim()) == false)
                    {
                        return;
                    }
                    sql += string.Format(" and ProNo='{0}'", txtPOProNo1.Text.Trim());
                }

                if (txtGoodNo.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写商品号码！');</script>");
                    return;
                }
                if (txtGoodNo.Text != "")
                {
                    sql += string.Format(@" and exists (select 1 from CG_POOrders left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
where TB_Good.GoodNo='{0}' and CG_POOrders.Id=CG_POOrder.Id)", txtGoodNo.Text);
                }
            }

            List<CG_POOrder> pOOrderList = this.POSer.GetListArray(sql);
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();
            List<CG_POOrders> orders = ordersSer.GetListArrayToList(sql);
            if (type == 2 && orders.Count <= 1)
            {
                Button6.Enabled = false;
            }
            else
            {
                Button6.Enabled = true;
            }
            ViewState["Orders"] = orders;
            gvList.DataSource = orders;
            gvList.DataBind();

        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                if (!string.IsNullOrEmpty(txtGoodNo.Text))
                {
                    var model = e.Row.DataItem as CG_POOrders;
                    if (model != null&&model.GoodNo==txtGoodNo.Text)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                }
            
            
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Show(0);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Show(1);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Show(2);
        }

        public string GetStateType(object obj)
        {
            if (CG_POOrder.ConPOStatue5_1 == obj.ToString() || CG_POOrder.ConPOStatue5_1 == obj.ToString())
            {
                return "1";
            }
            return "0";
        
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        /// <summary>
        /// 库存采购删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (txtKCPOno.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购项目编码！');</script>");
                return;
            }
            if (CheckPoNO(txtKCPOno.Text)==false)
            {
                return;
            }
            //查询项目信息是否存在
            string sql = string.Format("select count(*) from CAI_POOrder where PONo='{0}'", txtKCPOno.Text.Trim());
            var obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购项目信息不存在！');</script>");
                return;
            }

            //删除采购信息
            //检查该项目信息有没有入库信息
            sql = string.Format("select count(*) from CAI_OrderInHouse where poNo='{0}'", txtKCPOno.Text.Trim());
            obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该采购项目，已经完成入库，请按采购退货流程！');</script>");
                return;
            }
            //检查项目预付款单
            sql = string.Format(@"select COUNT(*) from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
where  CAI_POOrder.PONo='{0}'", txtKCPOno.Text.Trim());
            obj = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无法删除，请先删除项目预付款信息！');</script>");
                return;
            }

            //删除采购检验单信息
            //删除采购订单信息
            //删除项目信息
            sql = "";

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where PONO='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单检验') and allE_Id in (select CheckId from CAI_OrderChecks where PONO='{0}');";//删除审批流程单据
            sql += "delete from CAI_OrderCheck where Id in (select CheckId from CAI_OrderChecks where PONO='{0}'); ";//删除检验单主单据
            sql += "delete from CAI_OrderChecks where PONO='{0}'; ";//删除检验单子单据

            sql += "delete from tb_EForms where e_Id in (select id from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where PONO='{0}'));";//删除审批流程单据
            sql += "delete from tb_EForm  where proId=(select pro_Id from A_ProInfo where pro_Type='采购订单') and allE_Id in (select Id from CAI_POOrder where PONO='{0}');";//删除审批流程单据
            sql += "delete from  CAI_POCai where Id in (select Id from CAI_POOrder where PONO='{0}'); ";//删除采购订单 采购子单据
            sql += "delete from  CAI_POOrders where Id in (select Id from CAI_POOrder where PONO='{0}'); ";//删除采购订单 项目子单据
            sql += "delete from CAI_POOrder where PONO='{0}'; ";//删除采购订单主单据



            sql = string.Format(sql, txtKCPOno.Text.Trim());
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();

                }
                catch (Exception)
                {

                    tan.Rollback();
                    conn.Close();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                    return;
                }
                conn.Close();
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
            }

        }

        protected void gvCaiMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }


        protected void gvCaiXiaoShou_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckPoNO(txtKCPOno.Text) == false)
            {
                return;
            }
            CAI_POOrderService POSer = new CAI_POOrderService();
            string sql = " 1=1 ";


            sql += string.Format(" and PONo= '{0}'", txtKCPOno.Text.Trim());
           
            List<CAI_POOrder> pOOrderList = POSer.GetListArray(sql);
            foreach (var model in pOOrderList)
            {
                if (model.BusType == "0")
                {
                    model.BusType = "项目订单采购";
                }
                else if (model.BusType == "1")
                {
                    model.BusType = "库存采购";
                }
            }

            this.gvCaiMain.DataSource = pOOrderList;
            this.gvCaiMain.DataBind();

            if (pOOrderList.Count > 0)
            {
                CAI_POOrdersService ordersSer = new CAI_POOrdersService();
                List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and Cai_POOrders.id=" + pOOrderList[0].Id);


                gvCaiXiaoShou.DataSource = orders;
                gvCaiXiaoShou.DataBind();
            }

        }
    }
}
