using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;

namespace VAN_OA.Dal.EFrom
{
    public class tb_EFormService
    {
        public string GetAllE_NoByGoods(string tableName)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(GoodProNo),4))+1))),4) FROM  {0} where GoodProNo like '{1}%';",
                tableName, DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        public string GetAllE_No(string tableName)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  {0} where ProNo like '{1}%';",
                tableName, DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        public string GetAllE_No(string tableName, SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  {0} where ProNo like '{1}%';",
                tableName, DateTime.Now.Year);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        public string GetMaxE_No()
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(e_No),4))+1))),4) FROM  tb_EForm where proId=37 AND e_No like '{0}%';",
                 DateTime.Now.Year);
           
            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        public string GetAllE_No(string tableName,string ziduan, SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(" + ziduan + "),4))+1))),4) FROM  {0} where " + ziduan + " like '{1}%';",
                tableName, DateTime.Now.Year);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        public string GetAllE_No1(string tableName, SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select max(ProNo)+1 FROM  {0} where ProNo like '{1}%';",
                tableName, DateTime.Now.Year);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }


        public bool DeleteAllEFormData(string proId, string allE_id, string EForm_Id, string type, string E_No)
        {
            string DeleteAll = string.Format("delete from tb_EForms where e_Id={0};delete from tb_EForm where id={0};", EForm_Id);

            if (type == "员工请假单")
            {
                DeleteAll += "delete from tb_LeverInfo where id=" + allE_id + ";";

            }

            if (type == "派工单")
            {

                DeleteAll += "delete from tb_Dispatching where id=" + allE_id + ";";
            }


            if (type == "外出业务联系单")
            {

                DeleteAll += "delete from tb_BusContact where id=" + allE_id + ";";
            }

            if (type == "外出送货单")
            {

                DeleteAll += "delete from tb_DeliverGoods  where id=" + allE_id + ";";
            }

            if (type == "工具申请单")
            {


                DeleteAll += "delete from tb_DeliverGoods  where id=" + allE_id + ";";
            }

            if (type == "私车公用申请单")
            {


                DeleteAll += "delete from tb_UseCar  where id=" + allE_id + ";";
            }

            if (type == "内部投诉单")
            {


                DeleteAll += "delete from tb_Complaint where id=" + allE_id + ";";
            }

            if (type == "用车申请单")
            {


                DeleteAll += "delete from tb_AppCar where id=" + allE_id + ";";
            }

            if (type == "加班单")
            {
                DeleteAll += "delete from tb_OverTime where id=" + allE_id + ";";
            }
            if (type == "公交卡使用")
            {
                DeleteAll += "delete from TB_BusCardUse where id=" + allE_id + ";";
            }
            if (type == "商品档案申请")
            {
                DeleteAll += "delete from TB_Good where GoodId=" + allE_id + ";";
            }
            if (type == "申请请款单")
            {


                DeleteAll += "delete from tb_FundsUse where id=" + allE_id + ";";
            }

            if (type == "用车明细表")
            {


                DeleteAll += "delete from TB_UseCarDetail where id=" + allE_id + ";";
            }

            if (type == "订单报批表")
            {


                DeleteAll += string.Format("delete from TB_POOrder where id={0};delete from TB_POOrders where id={0};delete from TB_POCai where id={0};", allE_id);
            }
            if (type == "邮寄文档快递表")
            {
                DeleteAll += "delete from tb_Post where id=" + allE_id + ";";
            }

            if (type == "仓库借货单")
            {
                DeleteAll += "delete from TB_BorrowInvName where id=" + allE_id + ";";
            }

            if (type == "工作事务型请假单")
            {
                DeleteAll += "delete from Tb_LeaveTask where id=" + allE_id + ";";
            }

            if (type == "预期报销单")
            {
                DeleteAll += "delete from Tb_DispatchList where id=" + allE_id + ";";
            }

            if (type == "预期报销单(油费报销)")
            {
                DeleteAll += "delete from Tb_DispatchList where id=" + allE_id + ";";
            }

            if (type == "工程材料审计清单")
            {
                DeleteAll += string.Format("delete from Tb_ProjectInv where id={0};delete from Tb_ProjectInvs where PId={0};", allE_id);
            }
            if (type == "部门领料单")
            {
                DeleteAll += string.Format("delete from Tb_ExpInv where id={0};delete from Tb_ExpInvs where PId={0};", allE_id);
            }
            if (type == "客户联系跟踪表")
            {
                DeleteAll += "delete from TB_GuestTrack where id=" + allE_id + ";";
            }

            if (type == "供应商申请表")
            {
                DeleteAll += "delete from TB_SupplierInfo where id=" + allE_id + ";";
            }



            if (type == "车辆保养申请表")
            {
                DeleteAll += "delete from TB_CarMaintenance where id=" + allE_id + ";";
            }


            if (type == "项目订单")
            {
                string check = string.Format("select count(*) from CAI_POOrder where CG_ProNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                return false;
                //DeleteAll += string.Format("delete from CG_POOrder where id={0};delete from CG_POOrders where id={0};delete from CG_POCai where id={0};", allE_id);
            }

            if (type == "采购订单")
            {

                string check = string.Format("select count(*) from CAI_OrderChecks  where caiProNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                return false;
                //DeleteAll += string.Format("delete from CAI_POOrder where id={0};delete from CAI_POOrders where id={0};delete from CAI_POCai where id={0};", allE_id);
            }

            if (type == "采购订单检验")
            {
                string check = string.Format("select count(*) from CAI_OrderInHouse  where chcekProNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                //DeleteAll += string.Format("delete from CAI_OrderCheck where id={0};delete from CAI_OrderChecks where CheckId={0};", allE_id);
                return false;
            }


            if (type == "采购入库")
            {
                string check = string.Format("select count(*) from CAI_OrderOutHouse  where chcekProNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                return false;
                //DeleteAll += string.Format("delete from CAI_OrderInHouse where id={0};delete from CAI_OrderInHouses where id={0};", allE_id);

            }
            if (type == "采购退货")
            {
                return false;
                //DeleteAll += string.Format("delete from CAI_OrderOutHouse where id={0};delete from CAI_OrderOutHouses where id={0};", allE_id);

            }
            if (type == "供应商付款单" || type == "供应商付款单（预付单转支付单）")
            {
                return false;
            }
            if (type == "供应商预付款单")
            {
                return false;
            }


            if (type == "销售出库")
            {
                string check = string.Format("select count(*) from Sell_OrderInHouse  where chcekProNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                check = string.Format("select count(*) from Sell_OrderFPs  where sellOutPoNo='{0}'", E_No);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                return false;
                //DeleteAll += string.Format("delete from Sell_OrderOutHouse where id={0};delete from Sell_OrderOutHouses where id={0};", allE_id);
            }


            if (type == "销售退货")
            {
                return false;
                // DeleteAll += string.Format("delete from Sell_OrderInHouse where id={0};delete from Sell_OrderInHouses where id={0};", allE_id);
            }

            var poNo = "";

            if (type == "销售发票" || type == "销售发票修改")
            {
                string check = string.Format("select count(*) from TB_ToInvoice  where FPId={0}", allE_id);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                check = string.Format("select  count(*) from Sell_OrderFPBack  where PId={0} ", allE_id);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    return false;
                }
                //var obj = DBHelp.ExeScalar(check);
                //if (obj is DBNull)
                //{
                //    return false;
                //}

                //poNo = obj.ToString();


                DeleteAll += string.Format(@"declare @oldFPNo  varchar(500);declare @oldPONo  varchar(500);
select top 1  @oldFPNo=FPNo,@oldPONo=PONo from Sell_OrderFP where id={0}
update  CG_POOrder set FPTotal=replace( FPTotal, @oldFPNo+'/','')
where PONo  in (select PONo from Sell_OrderFP where id={0}) and ifzhui=0;", allE_id);

                Dal.EFrom.tb_EFormService efromSer = new VAN_OA.Dal.EFrom.tb_EFormService();
                var efromModel = efromSer.GetModel(Convert.ToInt32(EForm_Id));
                if (efromModel.state == "通过")
                {
                    DeleteAll += "update CG_POOrder set POStatue3='' where PONo=@oldPONo;";
                }

                DeleteAll += string.Format("delete from Sell_OrderFP where id={0};delete from Sell_OrderFPs where id={0}; ", allE_id);
            }

            if (type == "到款单")
            {

                string check = string.Format("select top 1 PONo from TB_ToInvoice where id={0}", allE_id);
                var obj = DBHelp.ExeScalar(check);
                if (obj is DBNull)
                {
                    return false;
                }

                poNo = obj.ToString();


                DeleteAll += string.Format(@"declare @oldPONo  varchar(500);select @oldPONo=PONo from TB_ToInvoice where id={0};
                    delete from TB_ToInvoice where id={0};", allE_id);
                Dal.EFrom.tb_EFormService efromSer = new VAN_OA.Dal.EFrom.tb_EFormService();
                var efromModel = efromSer.GetModel(Convert.ToInt32(EForm_Id));
                if (efromModel.state == "通过")
                {
                    DeleteAll += "update CG_POOrder set POStatue4='' where PONo=@oldPONo;";
                }
            }


            if (type == "出库单签回")
            {
                DeleteAll += string.Format(" declare @oldPONo  varchar(500);select @oldPONo=PONo from Sell_OrderOutHouseBack where id={0}; ", allE_id);
                DeleteAll += string.Format("delete from Sell_OrderOutHouseBack where id={0};delete from Sell_OrderOutHouseBacks where id={0};", allE_id);
                //判断状态
                DeleteAll += string.Format(@"if exists(select Sell_OrderOutHouseBack.Id
from Sell_OrderOutHouse left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo and  Sell_OrderOutHouseBack.Status='通过' where 
Sell_OrderOutHouse.Status='通过' and Sell_OrderOutHouseBack.id is null and Sell_OrderOutHouse.PoNo= @oldPONo)
begin update CG_POOrder set POStatue5='{0}' where PONo=@oldPONo end
else begin update CG_POOrder set POStatue5='{1}' where PONo=@oldPONo end", CG_POOrder.ConPOStatue5_1, CG_POOrder.ConPOStatue5);


            }

            if (type == "发票单签回")
            {
                DeleteAll += string.Format(" declare @oldPONo  varchar(500);select @oldPONo=PONo from Sell_OrderFPBack where id={0}; ", allE_id);
                DeleteAll += string.Format("delete from Sell_OrderFPBack where id={0};delete from Sell_OrderFPBacks where id={0};", allE_id);
                //判断状态
                DeleteAll += string.Format(@"if exists(select Sell_OrderFP.Id from Sell_OrderFP  left join Sell_OrderFPBack on  Sell_OrderFP.FPNo=Sell_OrderFPBack.FPNo  and Sell_OrderFPBack.Status='通过'  where
 Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Id is null and Sell_OrderFP.PONo= @oldPONo)
begin update CG_POOrder set POStatue6='{0}' where PONo=@oldPONo end
else begin update CG_POOrder set POStatue6='{1}' where PONo=@oldPONo end", CG_POOrder.ConPOStatue6_1, CG_POOrder.ConPOStatue6);
            }


            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.Parameters.Clear();
                    objCommand.CommandText = DeleteAll;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();


                    if (type == "销售发票" || type == "销售发票修改")
                    {
                        new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(poNo);
                        new VAN_OA.Dal.JXC.CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(poNo, "通过");
                    }
                    if (type == "到款单")
                    {
                        new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(poNo);
                    }

                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;

        }

        public string getUrl(string proId, string allE_id, string EForm_Id, string type)
        {
            string url = "";
            if (type == "员工请假单")
            {
                url = "~/EFrom/LeaveSingle.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;

            }

            if (type == "派工单")
            {
                url = "~/EFrom/Dispatching.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;

            }


            if (type == "外出业务联系单")
            {
                url = "~/EFrom/BusContact.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;

            }
            if (type == "商品档案申请")
            {
                url = "~/BaseInfo/WFGoods.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "外出送货单")
            {
                url = "~/EFrom/DeliverGoods.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "工具申请单")
            {
                url = "~/EFrom/ToolsApp.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "私车公用申请单")
            {
                url = "~/EFrom/UseCar.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "内部投诉单")
            {
                url = "~/EFrom/Complaint.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "用车申请单")
            {
                url = "~/EFrom/AppCar.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "加班单")
            {
                url = "~/EFrom/OverTime.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "公交卡使用")
            {
                url = "~/ReportForms/WFBusCardUse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "申请请款单")
            {
                url = "~/EFrom/FundsUse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }


            if (type == "用车明细表")
            {
                url = "~/EFrom/UseCarDetail.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "订单报批表")
            {
                url = "~/EFrom/PO_Order.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "邮寄文档快递表")
            {
                url = "~/EFrom/WFPost.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "仓库借货单")
            {
                url = "~/EFrom/BorrowInvName.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "工作事务型请假单")
            {
                url = "~/EFrom/LeaveTask.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "预期报销单")
            {
                url = "~/EFrom/DispatchList.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "预期报销单(油费报销)")
            {
                url = "~/EFrom/DispatchList.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "工程材料审计清单")
            {
                url = "~/EFrom/ProjectInvs.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "部门领料单")
            {
                url = "~/EFrom/ExpInvs.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "客户联系跟踪表")
            {
                url = "~/ReportForms/GuestTrack.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "供应商申请表")
            {
                url = "~/ReportForms/WFSupplierInfo.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "车辆保养申请表")
            {
                url = "~/ReportForms/CarMaintenance.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "项目订单")
            {
                url = "~/JXC/CG_Order.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "采购订单")
            {
                url = "~/JXC/CAI_Order.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }


            if (type == "采购订单检验")
            {
                url = "~/JXC/WFCAI_OrderCheck.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "采购入库")
            {
                url = "~/JXC/WFCAI_OrderInHouse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "销售出库")
            {
                url = "~/JXC/WFSell_OrderOutHouse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "销售退货")
            {
                url = "~/JXC/WFSell_OrderInHouse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "采购退货")
            {
                url = "~/JXC/WFCAI_OrderOutHouse.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "销售发票"||type == "销售发票修改"|| type == "销售发票删除")
            {
                url = "~/JXC/WFSell_OrderFP.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }

            if (type == "到款单")
            {
                url = "~/EFrom/WFToInvoice.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "出库单签回")
            {
                url = "~/JXC/WFSell_OrderOutHouseBack.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "发票单签回")
            {
                url = "~/JXC/WFSell_OrderFPBack.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "供应商付款单" || type == "供应商付款单（预付单转支付单）")
            {
                url = "~/JXC/WFSupplierInvoiceVerify.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
            if (type == "供应商预付款单")
            {
                url = "~/JXC/WFSupplierAdvancePaymentVerify.aspx?ProId=" + proId + "&allE_id=" + allE_id + "&EForm_Id=" + EForm_Id;
            }
             
            return url;
        }

        public string getUrlToAdd(string proId, string type)
        {
            string url = "";
            if (type == "员工请假单")
            {
                url = "~/EFrom/LeaveSingle.aspx?ProId=" + proId;

            }
            if (type == "派工单")
            {
                url = "~/EFrom/Dispatching.aspx?ProId=" + proId;

            }

            if (type == "外出业务联系单")
            {
                url = "~/EFrom/BusContact.aspx?ProId=" + proId;

            }
            if (type == "外出送货单")
            {
                url = "~/EFrom/DeliverGoods.aspx?ProId=" + proId;
            }

            if (type == "工具申请单")
            {
                url = "~/EFrom/ToolsApp.aspx?ProId=" + proId;
            }

            if (type == "私车公用申请单")
            {
                url = "~/EFrom/UseCar.aspx?ProId=" + proId;
            }
            if (type == "内部投诉单")
            {
                url = "~/EFrom/Complaint.aspx?ProId=" + proId;
            }
            if (type == "商品档案申请")
            {
                url = "~/BaseInfo/WFGoods.aspx?ProId=" + proId ;
            }
            if (type == "用车申请单")
            {

                url = "~/EFrom/AppCar.aspx?ProId=" + proId;
            }

            if (type == "加班单")
            {
                url = "~/EFrom/OverTime.aspx?ProId=" + proId;

            }

            if (type == "公交卡使用")
            {
                url = "~/ReportForms/WFBusCardUse.aspx?ProId=" + proId;
            }
            if (type == "申请请款单")
            {
                url = "~/EFrom/FundsUse.aspx?ProId=" + proId;

            }

            if (type == "用车明细表")
            {
                url = "~/EFrom/UseCarDetail.aspx?ProId=" + proId;
            }
            if (type == "订单报批表")
            {

                url = "~/EFrom/PO_Order.aspx?ProId=" + proId;
            }
            if (type == "邮寄文档快递表")
            {
                url = "~/EFrom/WFPost.aspx?ProId=" + proId;
            }
            if (type == "仓库借货单")
            {
                url = "~/EFrom/BorrowInvName.aspx?ProId=" + proId;
            }

            if (type == "工作事务型请假单")
            {
                url = "~/EFrom/LeaveTask.aspx?ProId=" + proId;
            }

            if (type == "预期报销单")
            {
                url = "~/EFrom/DispatchList.aspx?ProId=" + proId;
            }
            if (type == "预期报销单(油费报销)")
            {
                url = "~/EFrom/DispatchList.aspx?ProId=" + proId;
            }

            if (type == "工程材料审计清单")
            {
                url = "~/EFrom/ProjectInvs.aspx?ProId=" + proId;
            }
            if (type == "部门领料单")
            {
                url = "~/EFrom/ExpInvs.aspx?ProId=" + proId;
            }

            if (type == "客户联系跟踪表")
            {
                url = "~/ReportForms/GuestTrack.aspx?ProId=" + proId;
            }
            if (type == "供应商申请表")
            {
                url = "~/ReportForms/WFSupplierInfo.aspx?ProId=" + proId;
            }

            if (type == "车辆保养申请表")
            {
                url = "~/ReportForms/CarMaintenance.aspx?ProId=" + proId;
            }

            if (type == "项目订单")
            {
                url = "~/JXC/CG_Order.aspx?ProId=" + proId;
            }
            if (type == "采购订单")
            {
                url = "~/JXC/CAI_Order.aspx?ProId=" + proId;
            }

            if (type == "采购订单检验")
            {
                url = "~/JXC/WFCAI_OrderCheck.aspx?ProId=" + proId;
            }

            if (type == "采购入库")
            {
                url = "~/JXC/WFCAI_OrderInHouse.aspx?ProId=" + proId;
            }

            if (type == "销售出库")
            {
                url = "~/JXC/WFSell_OrderOutHouse.aspx?ProId=" + proId;
            }

            if (type == "销售退货")
            {
                url = "~/JXC/WFSell_OrderInHouse.aspx?ProId=" + proId;
            }

            if (type == "采购退货")
            {
                url = "~/JXC/WFCAI_OrderOutHouse.aspx?ProId=" + proId;
            }

            if (type == "销售发票")
            {
                url = "~/JXC/WFSell_OrderFP.aspx?ProId=" + proId;
            }

            if (type == "到款单")
            {
                url = "~/JXC/WFToInvoice.aspx?ProId=" + proId;
            }
            if (type == "出库单签回")
            {
                url = "~/JXC/WFSell_OrderOutHouseBack.aspx?ProId=" + proId;
            }
            if (type == "发票单签回")
            {
                url = "~/JXC/WFSell_OrderFPBack.aspx?ProId=" + proId;
            }
            if (type == "供应商付款单" || type == "供应商付款单（预付单转支付单）")
            {
                url = "~/JXC/WFSupplierInvoiceVerify.aspx?ProId=" + proId;
            }
            if (type == "供应商预付款单")
            {
                url = "~/JXC/WFSupplierAdvancePaymentVerify.aspx?ProId=" + proId ;
            }
            return url;
        }
        /// <summary>
        /// 获取当前要审核单据的人
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public int getCurrentAuPer(int pro_Id, int allE_id)
        {
            string sql = string.Format("select toPer from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return -1;
        }

        /// <summary>
        /// 获取当前要审核单据的toProsId
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public int getCurrenttoProsId(int pro_Id, int allE_id)
        {
            string sql = string.Format("select toProsId from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                return Convert.ToInt32(obj.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 获取当前要审核单据的RoleName
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public string getCurrenttoRoleName(int pro_Id, int allE_id)
        {
            string sql = string.Format(" select A_RoleName from A_Role where A_RoleId in (select a_Role_Id from  A_ProInfos  where ids in(select toProsId from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id) + "))";
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                return Convert.ToString(obj.ToString());
            }
            return "";
        }



        /// <summary>
        /// 获取当前ID
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public int getCurrentid(int pro_Id, int allE_id)
        {
            string sql = string.Format("select id from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 判断单据是否已经完成
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifFinish(int pro_Id, int allE_id)
        {
            string sql = string.Format("select count(*) from tb_EForm where state<>'执行中' and allE_id=" + allE_id + " and proId=" + pro_Id);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回单据的状态
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public string GetState(int pro_Id, int allE_id)
        {
            string sql = string.Format("select state from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object state = DBHelp.ExeScalar(sql);
            if (state != null)
            {
                return state.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取还有多少个审批流程
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public int GetToAduCount(int pro_Id, int allE_id)
        {
            string sql = string.Format("select toProsId from tb_EForm where allE_id=" + allE_id + " and proId=" + pro_Id);
            string getIDS = "select count(*) from A_ProInfos where pro_Id=" + pro_Id + " and a_index>( select a_index from A_ProInfos where ids=( " + sql + ")) ";
            return Convert.ToInt32(DBHelp.ExeScalar(getIDS));
        }
        /// <summary>
        /// 获取下一个审核人
        /// </summary>
        /// <param name="currentloginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getUserToAdu(int pro_Id, int allE_id, out int ids)
        {

            A_Role_UserService RoleUserSer = new A_Role_UserService();
            string sql = string.Format("select toProsId from tb_EForm where allE_id=" + allE_id + " and proId=" + pro_Id);

            string getIDS = "select top 1 IDS from A_ProInfos where pro_Id=" + pro_Id + " and a_index>( select a_index from A_ProInfos where ids=( " + sql + "))  order by a_index";
            ids = Convert.ToInt32(DBHelp.ExeScalar(getIDS));


            sql = "select top 1 a_Role_Id from A_ProInfos where pro_Id=" + pro_Id + " and a_index>( select a_index from A_ProInfos where ids=( " + sql + "))  order by a_index";





            return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId in ({0}))", sql));

        }

        /// <summary>
        /// 获取当前审批审核人（用来修改：重新指定当前 下一步审批人）
        /// </summary>
        /// <param name="currentloginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getUserToCurrnetEform(int pro_Id, int allE_id, out int ids)
        {


            A_Role_UserService RoleUserSer = new A_Role_UserService();
            string sql = string.Format("select toProsId from tb_EForm where allE_id=" + allE_id + " and proId=" + pro_Id);

            // string getIDS = "select top 1 IDS from A_ProInfos where pro_Id=" + pro_Id + " and  ids=( " + sql + ")  order by a_index";
            ids = Convert.ToInt32(DBHelp.ExeScalar(sql));


            sql = "select top 1 a_Role_Id from A_ProInfos where pro_Id=" + pro_Id + " and  ids=( " + sql + ")  order by a_index";









            return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId in ({0}))", sql));

        }

        /// <summary>
        /// 判断 当前登录人是否为审批人
        /// </summary>
        /// <param name="curentLoginName"></param>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifAudiPer(int curentLoginID, int pro_Id, int allE_id)
        {
            //先查本次审批人是谁
            string sql = string.Format("select toPer from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                if (Convert.ToInt32(obj) == curentLoginID)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 判断 当前登录人是否为审批人或代理人
        /// </summary>
        /// <param name="curentLoginName"></param>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifAudiPerAndCon(int curentLoginID, int pro_Id, int allE_id)
        {
            //先查本次审批人是谁
            string sql = string.Format("select toPer from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                if (Convert.ToInt32(obj) == curentLoginID)
                {
                    return true;
                }
            }

            //是否为代理人
            sql = string.Format(@"select count(*) from tb_EForm left join tb_Consignor on tb_EForm.proId =tb_Consignor.proId
and tb_EForm.toPer=tb_Consignor.appPer
where  consignor={0} and conState='开启' and(ifYouXiao=1  or 
appTime between fromTime and toTime  or appTime>=fromTime  or appTime<=toTime)

and tb_EForm.proId={1} and allE_id={2}", curentLoginID, pro_Id, allE_id);

            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 判断 当前登录人是能编辑审批文件
        /// </summary>
        /// <param name="curentLoginName"></param>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifEdit(int pro_Id, int allE_id)
        {
            //先查本次审批人是谁
            string sql = string.Format("select toProsId from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);
            sql = string.Format("SELECT a_Role_ID FROM A_ProInfos where ids in ({0})", sql);

            sql = string.Format("SELECT A_IFEdit FROM A_Role WHERE A_RoleId IN({0})", sql);
            object obj = DBHelp.ExeScalar(sql);
            if (obj != null)
            {
                return Convert.ToBoolean(obj);
            }
            return false;
        }

        /// <summary>
        /// 判断 当前登录人是否为 审批代理人
        /// </summary>
        /// <param name="curentLoginName"></param>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifAudiPerByUserName(int curentLoginID, int pro_Id, int allE_id)
        {


            string sql = string.Format(@"select count(*) from tb_EForm left join tb_Consignor on tb_EForm.proId =tb_Consignor.proId
and tb_EForm.toPer=tb_Consignor.appPer
where  consignor={0} and conState='开启' and(ifYouXiao=1  or 
appTime between fromTime and toTime  or appTime>=fromTime  or appTime<=toTime)

and tb_EForm.proId={1} and allE_id={2}", curentLoginID, pro_Id, allE_id);

            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            //            string sql = string.Format(@"select count(*)  from  tb_Consignor where appPer in (select toPer from tb_EForm where  allE_id={1} and proId={0})and   proId={0} and ifyouxiao=1 and  consignor='{2}'",
            //                 pro_Id, allE_id, curentLoginName);
            //            using (SqlConnection conn = DBHelp.getConn())
            //            {
            //                conn.Open();
            //                SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);

            //                if (Convert.ToInt32(objCommand.ExecuteScalar()) > 0)
            //                {
            //                    return true;
            //                }
            //                sql = string.Format("select count(*)  from  tb_Consignor where appPer in (select toPer from tb_EForm where  allE_id={1} and proId={0})and   proId={0} and fromTime is not null and  toTime is null and getdate()>=fromTime and  consignor='{2}'", pro_Id, allE_id, curentLoginName);
            //                objCommand.CommandText = sql;

            //                if (Convert.ToInt32(objCommand.ExecuteScalar()) > 0)
            //                {
            //                    return true;
            //                }


            //                sql = string.Format("select count(*)   from  tb_Consignor where appPer in (select toPer from tb_EForm where  allE_id={1} and proId={0})and   proId={0} and fromTime is  null and  toTime is not null and getdate()<toTime and  consignor='{2}'", pro_Id, allE_id, curentLoginName);
            //                objCommand.CommandText = sql;

            //                if (Convert.ToInt32(objCommand.ExecuteScalar()) > 0)
            //                {
            //                    return true;
            //                }



            //                sql = string.Format(@"select count(*)   from  tb_Consignor where appPer in (select toPer from tb_EForm where  allE_id={1} and proId={0})
            //and   proId={0} and fromTime is not null and  toTime is not null 
            //and getdate()>=fromTime  and getdate()<toTime and  consignor='{2}'", pro_Id, allE_id, curentLoginName);
            //                objCommand.CommandText = sql;

            //                if (Convert.ToInt32(objCommand.ExecuteScalar()) > 0)
            //                {
            //                    return true;
            //                }



            //                conn.Close();

            //            }
            return false;
        }
        /// <summary>
        /// 判断是否有审批流程
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <returns></returns>
        public bool ifHasNodes(int pro_Id)
        {


            string sql1 = string.Format("select count(*) from A_ProInfos where pro_Id=" + pro_Id + " ");

            object obj = DBHelp.ExeScalar(sql1);

            if (obj != null && Convert.ToInt32(obj) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;

        }


        /// <summary>
        /// 判断 审批流程是否为最后一个流程
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="allE_id"></param>
        /// <returns></returns>
        public bool ifLastNode(int pro_Id, int allE_id)
        {
            string sql = string.Format("select toProsId from tb_EForm where  allE_id=" + allE_id + " and proId=" + pro_Id);

            object obj = DBHelp.ExeScalar(sql);

            string sql1 = string.Format("select top 1 ids from A_ProInfos where pro_Id=" + pro_Id + " order by a_Index desc");

            object obj1 = DBHelp.ExeScalar(sql1);

            if (obj == null)
            {
                return true;
            }
            else
            {
                if (obj.ToString() == obj1.ToString())
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// (申请单据 增加时判断)根据用户名和 审批单据类型，来获取在次单据中该用户是否有审批权限，如果有则返回该用户最大审批权限ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getUsers(int loginID, int pro_Id, out int pro_IDs)
        {
            int a_Role_Id = 0;
            pro_IDs = 0;


            int id = 0;
            string sql = string.Format(@"select top 1 a_Index from A_ProInfos where pro_Id={1} and a_role_Id in 
(select A_RoleId from A_Role where A_RoleID IN (select A_ROLEid from A_Role_User where USER_ID={0})) 
order by a_Index desc ", loginID, pro_Id);

            //            string sql = string.Format(@"select top 1 ids from A_ProInfos where pro_Id={1} and a_role_Id in 
            //(select A_RoleId from A_Role where A_RoleID IN (select A_ROLEid from A_Role_User where USER_ID={0})) 
            //order by a_Index desc ", loginID, pro_Id);
            object obj = DBHelp.ExeScalar(sql);

            A_Role_UserService RoleUserSer = new A_Role_UserService();
            if (obj != null)
            {
                id = Convert.ToInt32(obj);

                //先获取流程下一个节点， 然后根据节点获取下一个要审批的人
                sql = string.Format("select top 1 * from A_ProInfos where a_index>{1} and pro_Id={0} order by a_index", pro_Id, id);

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            pro_IDs = Convert.ToInt32(dataReader["ids"]);
                            a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);

                        }

                    }
                }

                if (a_Role_Id != 0)
                {
                    return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0})", a_Role_Id));
                }
            }

            else
            {
                //获取流程第一个节点，
                //然后根据节点获取下一个要审批的人
                sql = string.Format("select top 1 * from A_ProInfos where  pro_Id={0} order by a_index", pro_Id);

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            pro_IDs = Convert.ToInt32(dataReader["ids"]);
                            a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);

                        }

                    }
                }

                if (a_Role_Id != 0)
                {
                    return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0})", a_Role_Id));
                }
            }
            return null;

        }


        /// <summary>
        /// (申请单据 增加时判断)根据用户名和 审批单据类型，获取第一级审批人
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getFristNodeUsers(int loginID, int pro_Id, out int pro_IDs)
        {
            int a_Role_Id = 0;
            pro_IDs = 0;


            A_Role_UserService RoleUserSer = new A_Role_UserService();
            string sql = "";
            //获取流程第一个节点，
            //然后根据节点获取下一个要审批的人
            sql = string.Format("select top 1 * from A_ProInfos where  pro_Id={0} order by a_index", pro_Id);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        pro_IDs = Convert.ToInt32(dataReader["ids"]);
                        a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);

                    }

                }
            }

            if (a_Role_Id != 0)
            {
                return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0})", a_Role_Id));
            }

            return null;

        }

        /// <summary>
        /// (申请单据 增加时判断)根据用户名和 审批单据类型，获取第一级审批人
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getFristNodeUsers_New(int loginID, int pro_Id, string LoginIPosition, out int pro_IDs)
        {
            int a_Role_Id = 0;
            pro_IDs = 0;


            A_Role_UserService RoleUserSer = new A_Role_UserService();
            string sql = "";
            //获取流程第一个节点，
            //然后根据节点获取下一个要审批的人
            sql = string.Format("select top 1 * from A_ProInfos where  pro_Id={0} order by a_index", pro_Id);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        pro_IDs = Convert.ToInt32(dataReader["ids"]);
                        a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);
                    }
                }
            }

            if (a_Role_Id != 0)
            {
                return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0}) and userId in (select id from tb_User where loginIPosition='{1}' and id<>{2})", a_Role_Id,LoginIPosition,loginID));
            }

            return null;

        }


        /// <summary>
        /// 返回第二级别 审核人 (请假单)
        /// </summary>
        /// <param name="pro_Id"></param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public List<A_Role_User> getSecondRoleUsers(int pro_Id, int Ids)
        {
            string sql = string.Format("select top 1 a_Role_Id from A_ProInfos where pro_Id={0}  and ids>{1} order by a_index ", pro_Id, Ids);
            A_Role_UserService RoleUserSer = new A_Role_UserService();
            return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId in({0})) ", sql));
        }

        /// <summary>
        /// (请假申请单据 增加时判断)根据用户名和 审批单据类型，来获取在次单据中该用户是否有审批权限，如果有则返回该用户最大审批权限ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<A_Role_User> getUsers_Qingjia(int loginID, int pro_Id, out int pro_IDs, out int pros_Id)
        {
            int a_Role_Id = 0;
            pro_IDs = 0;
            pros_Id = 0;

            int id = 0;
            //查找当前用户的级别 （null,普通人，0.代理人，1.2.3...其他级别）
            string sql = string.Format(@"select top 1 a_Index from A_ProInfos where pro_Id={1} and a_role_Id in 
(select A_RoleId from A_Role where A_RoleID IN (select A_ROLEid from A_Role_User where USER_ID={0})) 
order by a_Index desc ", loginID, pro_Id);
            object obj = DBHelp.ExeScalar(sql);

            A_Role_UserService RoleUserSer = new A_Role_UserService();
            if (obj != null && Convert.ToInt32(obj) != 0)
            {
                id = Convert.ToInt32(obj);

                //先获取流程下一个节点， 然后根据节点获取下一个要审批的人
                sql = string.Format("select top 1 * from A_ProInfos where a_index>{1} and pro_Id={0} order by a_index", pro_Id, id);

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            pro_IDs = Convert.ToInt32(dataReader["ids"]);
                            a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);

                        }

                    }
                }

                if (a_Role_Id != 0)
                {
                    return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0}) ", a_Role_Id));
                }
            }

            else
            {
                //获取流程第一个节点，
                //然后根据节点获取下一个要审批的人
                sql = string.Format("select top 1 * from A_ProInfos where  pro_Id={0} order by a_index", pro_Id);

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            pro_IDs = Convert.ToInt32(dataReader["ids"]);
                            a_Role_Id = Convert.ToInt32(dataReader["a_Role_Id"]);
                            pros_Id = pro_IDs;

                        }

                    }
                }

                if (a_Role_Id != 0)
                {
                    return RoleUserSer.GetListArray(string.Format(" A_RoleName in (select A_RoleName from A_Role where A_RoleId={0}) and userID in (select id from tb_User where loginIPosition in (select loginIPosition from tb_User where id={1})) and userID<>{1}", a_Role_Id, loginID));
                }
            }
            return null;

        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_EForm model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_EForm(");
            strSql.Append("proId,createPer,createTime,appPer,appTime,state,allE_id,toPer,toProsId,e_No,e_Remark,e_lastTime");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.proId + ",");
            strSql.Append("" + model.createPer + ",");
            strSql.Append("'" + model.createTime + "',");
            strSql.Append("" + model.appPer + ",");
            strSql.Append("'" + model.appTime + "',");
            strSql.Append("'" + model.state + "',");
            strSql.Append("" + model.allE_id + ",");
            strSql.Append("" + model.toPer + ",");
            strSql.Append("" + model.toProsId + ",");
            strSql.Append("'" + model.E_No + "',");
            strSql.Append("'" + model.E_Remark + "',");
            if (model.E_LastTime == null)
            {
                strSql.Append("null");
            }
            else
            {
                strSql.Append("'" + model.E_LastTime + "'");                
            }
          
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
            int result;
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        public int Add(VAN_OA.Model.EFrom.tb_EForm model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_EForm(");
            strSql.Append("proId,createPer,createTime,appPer,appTime,state,allE_id,toPer,toProsId,e_No,e_Remark,e_lastTime");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.proId + ",");
            strSql.Append("" + model.createPer + ",");
            strSql.Append("'" + model.createTime + "',");
            strSql.Append("" + model.appPer + ",");
            strSql.Append("'" + model.appTime + "',");
            strSql.Append("'" + model.state + "',");
            strSql.Append("" + model.allE_id + ",");
            strSql.Append("" + model.toPer + ",");
            strSql.Append("" + model.toProsId + ",");
            strSql.Append("'" + model.E_No + "',");
            strSql.Append("'" + model.E_Remark + "',");
            if (model.E_LastTime == null)
            {
                strSql.Append("null");
            }
            else
            {
                strSql.Append("'" + model.E_LastTime + "'");
            }

            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");           
            int result;
            object obj =DBHelp.ExeScalar(strSql.ToString());
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_EForm model, SqlCommand objCommand,bool isUpdateTime=false)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_EForm set ");
            strSql.Append("proId=" + model.proId + ",");
            strSql.Append("state='" + model.state + "',");
            strSql.Append("allE_id=" + model.allE_id + ",");
            strSql.Append("toPer=" + model.toPer + ",");
            strSql.Append("toProsId=" + model.toProsId + ",");


            strSql.Append("appPer=" + model.appPer + ",");

            if (isUpdateTime)
            {
                strSql.Append("appTime='" + model.appTime + "',");
                strSql.Append("createTime='" + model.createTime + "',");
            }

            if (!string.IsNullOrEmpty(model.E_Remark))
            {
                strSql.Append("E_Remark='" + model.E_Remark + "',");
            }
            if (model.state != "执行中")
            {
                strSql.Append("e_LastTime=getdate(),");
            }


            var sql = strSql.ToString().Remove(strSql.Length - 1) + " where id=" + model.id ;
           
            objCommand.CommandText =sql;
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_EForm ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_EForm GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_EForm_View.*,pro_Type ");
            strSql.Append(" from tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            strSql.Append(" where tb_EForm_View.id=" + id + "");

            VAN_OA.Model.EFrom.tb_EForm model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind_1(dataReader);
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_EForm GetModel(int ALLE_id, string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_EForm_View.*,pro_Type ");
            strSql.Append(" from tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            strSql.Append(string.Format(" where tb_EForm_View.AllE_id=" + ALLE_id + " and pro_Type='{0}'", type));

            VAN_OA.Model.EFrom.tb_EForm model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind_1(dataReader);
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,proId,createPer,createTime,appPer,appTime,state,allE_id,toPer,toProsId,createPer_Name,appPer_Name,toPer_Name,e_No,e_Remark ");
            strSql.Append(" FROM tb_EForm_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }

        /// 我所要审核的单子  包括 委托的
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_ToDo(string strWhere, int UserId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(string.Format(@" select table2.*,pro_Type,maxDoTime from (
select *,'' as type1 from tb_EForm_View where  state='执行中' and toPer={0}
union
select * ,'委托' as type1 from (
select tb_EForm_View.* from tb_EForm_View left join tb_Consignor on tb_EForm_View.proId =tb_Consignor.proId
and tb_EForm_View.toPer=tb_Consignor.appPer
where conState='开启' and  consignor={0} and (ifYouXiao=1  or 
appTime between fromTime and toTime  or appTime>=fromTime  or appTime<=toTime)
) as table1
) as table2 left join A_ProInfo on A_ProInfo.pro_Id=table2.proId ", UserId));

            strSql.AppendFormat(" left join (select e_Id,max(dotime) as maxDoTime from tb_EForms   group by e_id) as newTb on newTb.E_Id=table2.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by appTime desc");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
                        object ojb;
                       
                     


                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["proId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.proId = (int)ojb;
                        }

                        ojb = dataReader["createPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createPer = (int)ojb;
                        }

                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }


                        ojb = dataReader["appPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appPer = (int)ojb;
                        }


                        ojb = dataReader["appTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appTime = (DateTime)ojb;
                        }
                        model.state = dataReader["state"].ToString();
                        ojb = dataReader["allE_id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allE_id = (int)ojb;
                        }
                        // model.toPer = dataReader["toPer"].ToString();
                        ojb = dataReader["toProsId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.toProsId = (int)ojb;
                        }

                        model.ProTyleName = dataReader["pro_Type"].ToString();


                        model.Type1 = dataReader["Type1"].ToString();
                        if (model.Type1 != "")
                        {
                            //  model.toPer = dataReader["toPer"].ToString();
                            model.ToPer_Name = dataReader["ToPer_Name"].ToString();
                        }



                        ojb = dataReader["createPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreatePer_Name = ojb.ToString();
                        }

                        ojb = dataReader["appPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AppPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["e_No"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_No = ojb.ToString();
                        }

                        ojb = dataReader["e_Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_Remark = ojb.ToString();
                        }



                        //ojb = dataReader["toPer_Name"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.ToPer_Name = ojb.ToString();
                        //}

                        ojb = dataReader["maxDoTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maxDoTime = ojb.ToString();
                        }
                        else
                        {
                            model.maxDoTime = model.createTime.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// 我所要审核的单子  包括 委托的 返回条数信息
        /// </summary>
        public int GetListArray_ToDo_Count(string strWhere, int UserId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(string.Format(@" select count(*) from (
select *,'' as type1 from tb_EForm_View where  state='执行中' and toPer={0}
union
select * ,'委托' as type1 from (
select tb_EForm_View.* from tb_EForm_View left join tb_Consignor on tb_EForm_View.proId =tb_Consignor.proId
and tb_EForm_View.toPer=tb_Consignor.appPer
where conState='开启' and  consignor={0} and (ifYouXiao=1  or 
appTime between fromTime and toTime  or appTime>=fromTime  or appTime<=toTime)
) as table1
) as table2 left join A_ProInfo on A_ProInfo.pro_Id=table2.proId ", UserId));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            //  strSql.Append(" order by appTime");
            return Convert.ToInt32(DBHelp.ExeScalar(strSql.ToString()));
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_MyApps_Consignor(string strWhere, int userId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(string.Format(@"select * from (
select e_Id ,consignor_Name as type1,doTime  from EForms_View where audPer={0} and consignor<>0
) as table1 left join tb_EForm_View on tb_EForm_View.id=table1.e_Id
left join  A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ", userId));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by doTime desc ");

            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }

                        ojb = dataReader["doTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.DoTime = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["proId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.proId = (int)ojb;
                        }
                        model.createPer = Convert.ToInt32(dataReader["createPer"]);
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        model.appPer = Convert.ToInt32(dataReader["appPer"]);
                        ojb = dataReader["appTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appTime = (DateTime)ojb;
                        }
                        model.state = dataReader["state"].ToString();
                        ojb = dataReader["allE_id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allE_id = (int)ojb;
                        }
                        // model.toPer = dataReader["toPer"].ToString();
                        ojb = dataReader["toProsId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.toProsId = (int)ojb;
                        }

                        model.ProTyleName = dataReader["pro_Type"].ToString();


                        model.Type1 = dataReader["Type1"].ToString();
                        model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());

                        ojb = dataReader["createPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreatePer_Name = ojb.ToString();
                        }

                        ojb = dataReader["appPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AppPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["toPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["e_No"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_No = ojb.ToString();
                        }

                        ojb = dataReader["e_Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_Remark = ojb.ToString();
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_MyApps(string strWhere, int userId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(string.Format(@"select * from (
select e_Id,'' as type1,max(doTime) as doTime from EForms_View where audPer={0}  and consignor!={0}
group by  e_Id 
union 
select e_Id ,audPer_Name as type1,max(doTime) as doTime  from EForms_View where consignor={0} and audPer!={0}
group by  e_Id ,audPer_Name
) as table1 left join tb_EForm_View on tb_EForm_View.id=table1.e_Id
left join  A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ", userId));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by doTime desc");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }

                        ojb = dataReader["doTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.DoTime = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["proId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.proId = (int)ojb;
                        }
                        try
                        {
                            model.createPer = Convert.ToInt32(dataReader["createPer"].ToString());
                        }
                        catch (Exception)
                        {


                        }
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        try
                        {
                            model.appPer = Convert.ToInt32(dataReader["appPer"].ToString());
                        }
                        catch (Exception)
                        {


                        }
                        ojb = dataReader["appTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appTime = (DateTime)ojb;
                        }
                        model.state = dataReader["state"].ToString();
                        ojb = dataReader["allE_id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allE_id = (int)ojb;
                        }
                        // model.toPer = dataReader["toPer"].ToString();
                        ojb = dataReader["toProsId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.toProsId = (int)ojb;
                        }

                        model.ProTyleName = dataReader["pro_Type"].ToString();


                        model.Type1 = dataReader["Type1"].ToString();
                        try
                        {

                            model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());
                        }
                        catch (Exception)
                        {


                        }

                        ojb = dataReader["createPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreatePer_Name = ojb.ToString();
                        }

                        ojb = dataReader["appPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AppPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["toPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["e_No"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_No = ojb.ToString();
                        }

                        ojb = dataReader["e_Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_Remark = ojb.ToString();
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_2_Page(string strWhere,PagerDomain page)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_EForm_View.*,pro_Type,maxDoTime ");
            strSql.Append(" FROM tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            //strSql.Append(" left join View_AllEform on View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id");
            strSql.AppendFormat(" left join (select e_Id,max(dotime) as maxDoTime from tb_EForms   group by e_id) as newTb on newTb.E_Id=tb_EForm_View.ID ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //strSql.Append(" order by appTime desc ");

            strSql=new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), strWhere, " appTime desc "));
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind_1(dataReader);
                        object ojb;
                        ojb = dataReader["maxDoTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maxDoTime = ojb.ToString();
                        }
                        else
                        {
                            model.maxDoTime = model.createTime.ToString();
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_2(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_EForm_View.*,pro_Type,maxDoTime ");
            strSql.Append(" FROM tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            //strSql.Append(" left join View_AllEform on View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id");
            strSql.AppendFormat(" left join (select e_Id,max(dotime) as maxDoTime from tb_EForms   group by e_id) as newTb on newTb.E_Id=tb_EForm_View.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by appTime desc ");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind_1(dataReader);
                        object ojb;
                        ojb = dataReader["maxDoTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maxDoTime = ojb.ToString();
                        }
                        else {
                            model.maxDoTime = model.createTime.ToString();
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        #region 分页重构
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_1(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_EForm_View.*,pro_Type,maxDoTime ");
            strSql.Append(" FROM tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");

            strSql.AppendFormat(" left join (select e_Id,max(dotime) as maxDoTime from tb_EForms   group by e_id) as newTb on newTb.E_Id=tb_EForm_View.ID ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by appTime desc ");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind_1(dataReader);
                        object ojb;

                        ojb = dataReader["maxDoTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maxDoTime = ojb.ToString();
                        }
                        else
                        {
                            model.maxDoTime = model.createTime.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_1(string strWhere,PagerDomain pagerDomain)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_EForm_View.*,pro_Type ");
            strSql.Append(" FROM tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //strSql.Append(" order by appTime desc ");


            strSql = new StringBuilder(DBHelp.GetPagerSql(pagerDomain, strSql.ToString(), strWhere, "appTime desc"));

            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind_1(dataReader));
                    }
                }
            }
            return list;
        } 
        #endregion


        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_AE(string strWhere, string AE, string AEString)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT tb_EForm_View.*,pro_Type,maxDoTime ");
            strSql.Append(" FROM tb_EForm_View left join A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ");
            //strSql.Append(" left join View_AllEform on View_AllEform.myProId=tb_EForm_View.proId and View_AllEform.Id=tb_EForm_View.allE_id");
            strSql.AppendFormat(@" LEFT JOIN
(
select 21 as proId,CAI_OrderCheck.id from CAI_OrderCheck
left join CAI_OrderChecks on CAI_OrderChecks.CheckId=CAI_OrderCheck.id
where
exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderChecks.PONo and AppName={0})
union all
select 20 as proId,CAI_POOrder.id from CAI_POOrder
where
 exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_POOrder.PONo and AppName={0})
union all
select 22 as proId,CAI_OrderInHouse.id from CAI_OrderInHouse
 where
exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderInHouse.PONo and AppName={0})
union all
select 24 as proId,CAI_OrderOutHouse.id from CAI_OrderOutHouse
where
exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_OrderOutHouse.PONo and AppName={0})
union all
select 12 as proId,Tb_DispatchList.id from Tb_DispatchList
where
exists(select id from CG_POOrder where CG_POOrder.PONo=Tb_DispatchList.PONo and AE='{1}')
union all
select 32 as proId,TB_SupplierAdvancePayments.id from TB_SupplierAdvancePayments
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
left join CG_POOrder on CG_POOrder.PONO=CAI_POOrder.PONO
WHERE CG_POOrder.AppName={0}
UNION ALL
select 27 as proId,Id from TB_ToInvoice 
where
exists(select id from CG_POOrder where CG_POOrder.PONo=TB_ToInvoice.PONo and AppName={0})
union all
select 31 as proId,TB_SupplierInvoices.id from TB_SupplierInvoices 
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO
WHERE CG_POOrder.AppName={0}
union all
select 26 as proId,Sell_OrderFP.Id from Sell_OrderFP 
where id in ( select allE_id from tb_EForm where proId=26)
 and
 exists(select id from CG_POOrder where CG_POOrder.PONo=Sell_OrderFP.PONo and AppName={0})
 union all
 select 34 as proId,Sell_OrderFP.Id from Sell_OrderFP 
where id in ( select allE_id from tb_EForm where proId=34)
 and
 exists(select id from CG_POOrder where CG_POOrder.PONo=Sell_OrderFP.PONo and AppName={0})
)
AS TB1 ON  TB1.proId=tb_EForm_View.proId and TB1.Id=tb_EForm_View.allE_id", AE,AEString);

            strSql.Append(" left join (select e_Id,max(dotime) as maxDoTime from tb_EForms   group by e_id) as newTb on newTb.E_Id=tb_EForm_View.ID");
            strSql.Append("  where TB1.Id is not null ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by appTime desc ");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind_1(dataReader);
                        object ojb;
                        ojb = dataReader["maxDoTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maxDoTime = ojb.ToString();
                        }
                        else
                        {
                            model.maxDoTime = model.createTime.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_Eform_DeliverGoods_Dispatch(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Eform_DeliverGoods_Dispatch.*,pro_Type ");
            strSql.Append(" FROM Eform_DeliverGoods_Dispatch left join A_ProInfo on A_ProInfo.pro_Id=Eform_DeliverGoods_Dispatch.proId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by appTime desc ");
            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["proId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.proId = (int)ojb;
                        }
                        model.createPer = Convert.ToInt32(dataReader["createPer"].ToString());
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        model.appPer = Convert.ToInt32(dataReader["appPer"].ToString());
                        ojb = dataReader["appTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appTime = (DateTime)ojb;
                        }

                        ojb = dataReader["allE_id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allE_id = (int)ojb;
                        }
                        model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());
                        ojb = dataReader["toProsId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.toProsId = (int)ojb;
                        }

                        model.ProTyleName = dataReader["pro_Type"].ToString();

                        ojb = dataReader["createPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreatePer_Name = ojb.ToString();
                        }

                        ojb = dataReader["appPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AppPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["toPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToPer_Name = ojb.ToString();
                        }

                        ojb = dataReader["OutDispater"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.state = ojb.ToString();
                        }

                        ojb = dataReader["SongHuo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.state = ojb.ToString();
                        }


                        ojb = dataReader["e_No"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_No = ojb.ToString();
                        }

                        ojb = dataReader["e_Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_Remark = ojb.ToString();
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<VAN_OA.Model.EFrom.tb_EForm> GetListArray_Consignor(string strWhere, int UserID)
        {
            StringBuilder strSql = new StringBuilder();

            //            strSql.Append(string.Format(@"select * from (
            //select e_Id ,audPer as type1  from tb_EForms where consignor={0} and audPer!={0}
            //) as table1 left join tb_EForm_View on tb_EForm_View.id=table1.e_Id
            //left join  A_ProInfo on A_ProInfo.pro_Id=tb_EForm_View.proId ", UserID));


            strSql.Append(string.Format(@"select * from (
select tb_EForm_View.* from tb_EForm_View left join tb_Consignor on tb_EForm_View.proId =tb_Consignor.proId
and tb_EForm_View.toPer=tb_Consignor.appPer
where conState='开启' and  consignor={0} and (ifYouXiao=1  or 
appTime between fromTime and toTime  or appTime>=fromTime  or appTime<=toTime)
) as table1 
left join  A_ProInfo on A_ProInfo.pro_Id=table1.proId ", UserID));



            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append("  order by appTime desc");


            List<VAN_OA.Model.EFrom.tb_EForm> list = new List<VAN_OA.Model.EFrom.tb_EForm>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["proId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.proId = (int)ojb;
                        }
                        model.createPer = Convert.ToInt32(dataReader["createPer"].ToString());
                        ojb = dataReader["createTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.createTime = (DateTime)ojb;
                        }
                        model.appPer = Convert.ToInt32(dataReader["appPer"].ToString());
                        ojb = dataReader["appTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.appTime = (DateTime)ojb;
                        }
                        model.state = dataReader["state"].ToString();
                        ojb = dataReader["allE_id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allE_id = (int)ojb;
                        }
                        // model.toPer = dataReader["toPer"].ToString();
                        ojb = dataReader["toProsId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.toProsId = (int)ojb;
                        }

                        model.ProTyleName = dataReader["pro_Type"].ToString();


                        //model.Type1 = dataReader["Type1"].ToString();
                        model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());

                        ojb = dataReader["createPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreatePer_Name = ojb.ToString();
                        }

                        ojb = dataReader["appPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AppPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["toPer_Name"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToPer_Name = ojb.ToString();
                        }


                        ojb = dataReader["e_No"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_No = ojb.ToString();
                        }

                        ojb = dataReader["e_Remark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.E_Remark = ojb.ToString();
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_EForm ReaderBind_1(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["proId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.proId = (int)ojb;
            }
            model.createPer = Convert.ToInt32(dataReader["createPer"].ToString());
            ojb = dataReader["createTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.createTime = (DateTime)ojb;
            }
            model.appPer = Convert.ToInt32(dataReader["appPer"].ToString());
            ojb = dataReader["appTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.appTime = (DateTime)ojb;
            }
            model.state = dataReader["state"].ToString();
            ojb = dataReader["allE_id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.allE_id = (int)ojb;
            }
            model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());
            ojb = dataReader["toProsId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.toProsId = (int)ojb;
            }

            model.ProTyleName = dataReader["pro_Type"].ToString();

            ojb = dataReader["createPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreatePer_Name = ojb.ToString();
            }

            ojb = dataReader["appPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppPer_Name = ojb.ToString();
            }


            ojb = dataReader["toPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToPer_Name = ojb.ToString();
            }

            ojb = dataReader["e_No"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.E_No = ojb.ToString();
            }

            ojb = dataReader["e_Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.E_Remark = ojb.ToString();
            }
            return model;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_EForm ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_EForm model = new VAN_OA.Model.EFrom.tb_EForm();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["proId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.proId = (int)ojb;
            }
            model.createPer = Convert.ToInt32(dataReader["createPer"].ToString());
            ojb = dataReader["createTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.createTime = (DateTime)ojb;
            }
            model.appPer = Convert.ToInt32(dataReader["appPer"].ToString());
            ojb = dataReader["appTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.appTime = (DateTime)ojb;
            }
            model.state = dataReader["state"].ToString();
            ojb = dataReader["allE_id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.allE_id = (int)ojb;
            }
            model.toPer = Convert.ToInt32(dataReader["toPer"].ToString());
            ojb = dataReader["toProsId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.toProsId = (int)ojb;
            }

            ojb = dataReader["createPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreatePer_Name = ojb.ToString();
            }

            ojb = dataReader["appPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppPer_Name = ojb.ToString();
            }


            ojb = dataReader["toPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToPer_Name = ojb.ToString();
            }
            ojb = dataReader["e_No"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.E_No = ojb.ToString();
            }

            ojb = dataReader["e_Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.E_Remark = ojb.ToString();
            }

            return model;
        }

        internal void Add(tb_EFormService eformSer, SqlCommand objCommand)
        {
            throw new NotImplementedException();
        }

   
        public List<View_AllEform> GetView_AllEformList(string MyProIds, string ids)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.Append(string.Format(@"select View_AllEform.*,CG_POOrder.IsSpecial,CG_POOrder.AE from View_AllEform left join CG_POOrder on CG_POOrder.pono=View_AllEform.pono and IFZhui=0 where MyProId in ({0}) and View_AllEform.Id in ({1}) ", MyProIds, ids));


            List<View_AllEform> list = new List<View_AllEform>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        View_AllEform model = new View_AllEform();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["myProId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.myProId = (int)ojb;
                        }
                        ojb = dataReader["IsSpecial"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsSpecial = (bool)ojb;
                        }
                        model.PONo = dataReader["PONO"].ToString();
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo += "("+ojb.ToString()+")";                            
                        }
                       
                  
                        list.Add(model);
                    }
                }
            }
            return list;
        }
    }
}
