using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;

namespace VAN_OA
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        List<CAI_OrderInHouses> orders_CaiIn = new List<CAI_OrderInHouses>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int AddCai()
        {
            CAI_OrderInHousesService ordersSer = new CAI_OrderInHousesService();
            orders_CaiIn = ordersSer.GetListArray(" 1=1 and CAI_OrderInHouses.id=1");
            //采购单
            CAI_POOrder order = new CAI_POOrder();
            order.AppName = 1;
            order.CaiGou = "admin";
            order.AE = "";
            order.GuestName = "";
            order.GuestNo = "";
            order.INSIDE = "";
            order.PODate = Convert.ToDateTime("2012-06-19");
            order.POName = "库存初始化";
            order.POPayStype = "1";
            order.POTotal = Convert.ToDecimal(628650.72000);
            order.PONo = "";
            order.BusType = "1";
            order.CG_ProNo = "";
            order.cRemark = "";

            List<CAI_POOrders> POOrders = new List<CAI_POOrders>();
            foreach (var m in orders_CaiIn)
            {
                CAI_POOrders cai = new CAI_POOrders();
                cai.InvName = m.GoodName;
                cai.Num = m.GoodNum;
                cai.CostPrice = m.GoodPrice;
                cai.OtherCost = 0;
                cai.ToTime = Convert.ToDateTime("2012-06-26 00:00:00.000");
                cai.Profit = 0;
                cai.GoodId = m.GooId;
                cai.CG_POOrdersId = 0;
                POOrders.Add(cai);
            }

            List<CAI_POCai> caiOrders = new List<CAI_POCai>();
            foreach (var m in orders_CaiIn)
            {
                CAI_POCai cai = new CAI_POCai();
                cai.Supplier = "本部门（含税）";
                cai.SupperPrice = m.GoodPrice;
                cai.UpdateUser = "admin";
                cai.Idea = "";
                cai.Num = m.GoodNum;
                cai.FinPrice1 = m.GoodPrice;
                cai.GoodId = m.GooId;
                cai.cbifDefault1 = true;
                cai.lastSupplier = "本部门（含税）";
                cai.IsHanShui = true;
                cai.LastTruePrice = m.GoodPrice;
                caiOrders.Add(cai);
            }

            tb_EForm eform = new tb_EForm();
            eform.appPer = 1;
            eform.appTime = DateTime.Now;
            eform.createPer = 1;
            eform.createTime = DateTime.Now;
            eform.proId = 20;
            eform.state = "通过";
            eform.toPer = 0;
            eform.toProsId = 0;

            CAI_POOrderService POOrderSer = new CAI_POOrderService();
            int MainId = 0;
            return POOrderSer.addTran(order, eform, POOrders, caiOrders, out MainId, false);
        }


        public int AddCAI_OrderCheck(int id)
        {
            CAI_OrderCheck order = new CAI_OrderCheck();          
            order.CreatePer = 1;
            order.CheckPer = 1;
            order.CheckTime = DateTime.Now;
            order.CheckRemark = "库存初始化";

            VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

            int userId = 1;
            eform.appPer = userId;
            eform.appTime = DateTime.Now;
            eform.createPer =1;
            eform.createTime = DateTime.Now;
            eform.proId =21;           
            eform.state = "不通过";
            eform.toPer = 0;
            eform.toProsId = 0;
            CAI_POCaiService POSer = new CAI_POCaiService();
            List<CAI_POCaiView> modelList = POSer.GetListViewCai_POOrders_Cai_POOrderChecks_View(" id=" + id);
            List<CAI_OrderChecks> POOrders =new List<CAI_OrderChecks>();
            foreach (var model in modelList)
            {
                CAI_OrderChecks checkModel = new CAI_OrderChecks();
                checkModel.Total = model.Total;
                checkModel.CaiId = model.POCaiId;
                checkModel.CheckGoodId = model.GoodId;
                checkModel.CheckNum = model.Num;
                checkModel.CheckPrice = model.Price;
                checkModel.Good_Model = model.Good_Model;
                checkModel.GoodName = model.GoodName;
                checkModel.GoodNo = model.GoodNo;
                checkModel.GoodSpec = model.GoodSpec;
                checkModel.GoodTypeSmName = model.GoodTypeSmName;
                checkModel.GoodUnit = model.GoodUnit;
                checkModel.GuestName = model.GuestName;
                checkModel.POName = model.POName;
                checkModel.PONo = model.PONo;
                checkModel.SupplierName = model.Supplier;

                checkModel.CaiProNo = model.ProNo;
                checkModel.QingGou = model.CaiGou;
                checkModel.CaiGouPer = model.loginName;
                checkModel.CheckLastTruePrice = model.LastTruePrice;
                checkModel.GoodAreaNumber = model.GoodAreaNumber;
                POOrders.Add(checkModel);
            }

             CAI_OrderCheckService POOrderSer = new CAI_OrderCheckService();
             int MainId = 0;
             return POOrderSer.addTran(order, eform, POOrders, out MainId);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int id=AddCai();

            CAI_POOrderService mainSer = new CAI_POOrderService();

            CAI_POOrder pp = mainSer.GetModel(id);


            CAI_POOrdersService ordersSer = new CAI_POOrdersService();
            List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and CAI_POOrders.id=" + id);


            CAI_POCaiService CaiSer = new CAI_POCaiService();
            List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and CAI_POCai.id=" + id);

            foreach (var cai in caiList)
            {
                var m = orders_CaiIn.Single(t => t.GooId == cai.GoodId);
                
                cai.Supplier = "本部门（含税）";
                cai.SupperPrice = m.GoodPrice;
                cai.UpdateUser = "admin";
                cai.Idea = "";
                cai.Num = m.GoodNum;
                cai.FinPrice1 = m.GoodPrice;
                cai.GoodId = m.GooId;
                cai.cbifDefault1 = true;
                cai.lastSupplier = "本部门（含税）";
                cai.IsHanShui = true;
                cai.LastTruePrice = m.GoodPrice;
                cai.IfUpdate = true;
                
            }

               CAI_POOrderService POOrderSer = new CAI_POOrderService();
               POOrderSer.UpdataCai(caiList);


              int checkId= AddCAI_OrderCheck(id);

              LastUpdate(checkId);

               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成功！');</script>");



        }

        private void LastUpdate(int id)
        {
            CAI_OrderInHousesService ordersSer = new CAI_OrderInHousesService();
            List<CAI_OrderInHouses> orders = ordersSer.GetListArray(" 1=1 and CAI_OrderInHouses.id=1" );

            CAI_OrderChecksService ordersSer111 = new CAI_OrderChecksService();
            List<CAI_OrderChecks> orders1111 = ordersSer111.GetListArray(" 1=1 and CAI_OrderChecks.CheckId=" + id);



            CAI_OrderCheckService mainSer = new CAI_OrderCheckService();
            CAI_OrderCheck pp = mainSer.GetModel(id);

            DBHelp.ExeCommand(string.Format(@" update CAI_OrderInHouse SET ChcEkProNo='{0}',pono='{1}',poname='{2}' where id=1;
            update CAI_OrderOutHouse set pono='{1}',poname='{2}' where ChcEkProNo='20120000';
update CAI_OrderCheck set status='通过' where id={3} ;update tb_EForm set state='通过',e_LastTime=getdate() where allE_id={3} and proId=21;", pp.ProNo,
                orders1111[0].PONo, orders1111[0].POName, id));

            
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                CAI_POCaiService CaiSer = new CAI_POCaiService();
                try
                {
                    objCommand.Parameters.Clear();

                    foreach (var caiIn in orders)
                    {
                       var check= orders1111.Single(t => t.CheckGoodId == caiIn.GooId);

                       string sql = string.Format(" update CAI_OrderInHouses set OrderCheckIds={1} where ids={0}",caiIn.Ids,check.Ids);
                       objCommand.CommandText = sql;
                       objCommand.ExecuteNonQuery();
                    }


                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                     

                }

            }

            
        }
    }
}