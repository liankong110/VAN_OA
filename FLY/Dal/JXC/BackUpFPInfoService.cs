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
using System.Data.SqlClient;
using VAN_OA.Model.JXC;
using System.Collections.Generic;

namespace VAN_OA.Dal.JXC
{
    public class BackUpFPInfoService
    {
        public string BackUp(int FPId, SqlCommand objCommand)
        {
            string tempGuid = Guid.NewGuid().ToString();
            string tempInvoiceGuid = Guid.NewGuid().ToString();

            //记录修改的部分
            string sql = string.Format("update Sell_OrderFP set NowGuid='{1}',InvoiceNowGuid='{2}' where ID={0};", FPId, tempGuid, tempInvoiceGuid);
            objCommand.CommandText = sql;
            objCommand.ExecuteNonQuery();
            //备份发票数据
            sql = string.Format(@"INSERT INTO [Sell_OrderFP_History]
([Id],[CreateUserId],[CreateTime],[RuTime],[GuestNAME],[DoPer],[ProNo],[PONo],[POName],[Remark],[Status],[FPNo],[FPNoStyle],[Total],[TopFPNo],[TopTotal],[IsTuiHuo],[TempGuid])
SELECT [Id],[CreateUserId],[CreateTime],[RuTime],[GuestNAME],[DoPer],[ProNo],[PONo],[POName],[Remark],[Status],[FPNo],[FPNoStyle],[Total],[TopFPNo],[TopTotal]
,[IsTuiHuo],'{1}' from Sell_OrderFP where ID={0};", FPId, tempGuid);
            sql += string.Format(@"INSERT INTO [Sell_OrderFPs_History]([Ids],[id],[GooId],[GoodNum],[GoodPrice],[GoodRemark],[GoodSellPrice],[SellOutPONO],[SellOutOrderId]
,[TempGuid])
SELECT [Ids],[id],[GooId],[GoodNum],[GoodPrice],[GoodRemark],[GoodSellPrice],[SellOutPONO],[SellOutOrderId],'{1}' FROM [Sell_OrderFPs] where ID={0};", FPId, tempGuid);

            objCommand.CommandText = sql;
            objCommand.ExecuteNonQuery();


            return "";
        }


        public string BackUpOthers(int FPId, SqlCommand objCommand, string tempInvoiceGuid)
        {
            //string tempGuid = Guid.NewGuid().ToString();
            //string tempInvoiceGuid = Guid.NewGuid().ToString();



            //备份 到款单数据
            string sql = string.Format(@"INSERT INTO [TB_ToInvoice_History]([Id],[ProNo],[CreateUser],[AppleDate],[DaoKuanDate],[Total],[UpAccount],[PoNo],[PoName]
,[GuestName],[Remark],[State],[ZhangQi],[FPNo],[FPId],[BusType],[TempGuid])
SELECT [Id],[ProNo],[CreateUser],[AppleDate],[DaoKuanDate],[Total],[UpAccount],[PoNo],[PoName],[GuestName],[Remark],[State],[ZhangQi],[FPNo],[FPId],[BusType]
,'{1}' FROM [TB_ToInvoice] where FPId={0} and State<>'不通过';", FPId, tempInvoiceGuid);
            objCommand.CommandText = sql;
            objCommand.ExecuteNonQuery();


            //删除发票签回单
            sql = string.Format(" select id from Sell_OrderFPBack where PID={0}", FPId);

            var backId = DBHelp.ExeScalar(sql);
            if (backId != null)
            {
                string DeleteAll = string.Format(" declare @oldPONo  varchar(500);select @oldPONo=PONo from Sell_OrderFPBack where id={0}; ", backId);
                DeleteAll += string.Format("delete from Sell_OrderFPBack where id={0};delete from Sell_OrderFPBacks where id={0};", backId);
                //判断状态
                DeleteAll += string.Format(@"if exists(select Sell_OrderFP.Id from Sell_OrderFP  left join Sell_OrderFPBack on  Sell_OrderFP.FPNo=Sell_OrderFPBack.FPNo  and Sell_OrderFPBack.Status='通过'  where
 Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Id is null and Sell_OrderFP.PONo= @oldPONo)
begin update CG_POOrder set POStatue6='{0}' where PONo=@oldPONo end
else begin update CG_POOrder set POStatue6='{1}' where PONo=@oldPONo end;", CG_POOrder.ConPOStatue6_1, CG_POOrder.ConPOStatue6);

                //删除 申请单子
                DeleteAll += string.Format("delete from tb_EForms where e_Id in (select id from tb_EForm where proId=29 and allE_id={0} ) ;delete from tb_EForm where proId=29 and allE_id={0};", backId);
                objCommand.CommandText = DeleteAll;
                objCommand.ExecuteNonQuery();
            }

            //删除到款单 数据
            DataTable dt = DBHelp.getDataTable("select id,State,PoNo from TB_ToInvoice where State<>'不通过' and FPId=" + FPId);

            string InvoicePoNos = "";
            foreach (DataRow dr in dt.Rows)
            {
                var id = dr[0];
                var state = dr[1];
                InvoicePoNos += dr[2].ToString() + ",";

                string DeleteAll = string.Format(@"declare @oldPONo  varchar(500);select @oldPONo=PONo from TB_ToInvoice where id={0};
                    delete from TB_ToInvoice where id={0};", id);
                if (state.ToString() == "通过")
                {
                    DeleteAll += "update CG_POOrder set POStatue4='' where PONo=@oldPONo;";
                }

                //删除 申请单子
                DeleteAll += string.Format("delete from tb_EForms where e_Id in (select id from tb_EForm where proId=27 and allE_id={0} ) ;delete from tb_EForm where proId=27 and allE_id={0};", id);
                objCommand.CommandText = DeleteAll;
                objCommand.ExecuteNonQuery();
            }
            return InvoicePoNos;
        }




        public void BackDown(string tempGuid, int FPId, SqlCommand objCommand,out Sell_OrderFP model,out List<Sell_OrderFPs> fpList)
        {
            model = new Sell_OrderFP();
            fpList = new List<Sell_OrderFPs>();
            //删除上一条数据

            string sql = string.Format("select * from Sell_OrderFP_History where TempGuid='{0}' and id={1} order by UpdateTime DESC ", tempGuid, FPId);
            objCommand.CommandText = sql;

            using (SqlDataReader dataReader = objCommand.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                    model.NowGuid = dataReader["TempGuid"].ToString();
                    //model.Status = "通过";
                }
            }


            if (model != null)
            {                
                sql = string.Format(@"select   
Ids,Sell_OrderFPs_History.id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,SellOutPONO,SellOutOrderId
from Sell_OrderFPs_History left join TB_Good on TB_Good.GoodId=Sell_OrderFPs_History.GooId   where TempGuid='{0}' and Sell_OrderFPs_History.id={1}  ", model.NowGuid, model.Id);
                objCommand.CommandText = sql;

                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        fpList.Add(ReaderBindList(dataReader));

                    }
                }
            }


        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFPs ReaderBindList(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFPs model = new VAN_OA.Model.JXC.Sell_OrderFPs();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["GooId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GooId = (int)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }

            model.GoodRemark = dataReader["GoodRemark"].ToString();
            model.Total = model.GoodNum * model.GoodPrice;

            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo = ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice = Convert.ToDecimal(ojb);
            }


            model.GoodSellPriceTotal = model.GoodSellPrice * model.GoodNum;

            model.SellOutPONO = ojb.ToString();
            ojb = dataReader["SellOutPONO"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutPONO = ojb.ToString();
            }

            ojb = dataReader["SellOutOrderId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutOrderId = (int)ojb;
            }


            return model;
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFP ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestNAME"].ToString();
            model.DoPer = dataReader["DoPer"].ToString();


            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            //ojb = dataReader["CreateName"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.CreateName = ojb.ToString();
            //}

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }
            ojb = dataReader["FPNoStyle"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNoStyle = ojb.ToString();
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = Convert.ToDecimal(ojb);
            }
            model.TopFPNo = dataReader["TopFPNo"].ToString();
            model.TopTotal = Convert.ToDecimal(dataReader["TopTotal"]);

            return model;
        }
    }
}
