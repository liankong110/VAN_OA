using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;

using System.Text;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using System.Data;



namespace VAN_OA.Dal.JXC
{
    public class CAI_OrderCheckService
    {
        /// <summary>
        /// 获取特殊商品最后一次入库的编码
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="GoodNo"></param>
        /// <param name="GoodName"></param>
        /// <param name="GoodType"></param>
        /// <param name="GoodSpec"></param>
        /// <returns></returns>
        public string GetPONoInfo(int goodId,string GoodNo,string GoodName,string GoodType,string GoodSpec)
        {
            string sql = string.Format(@"select top 1 GoodNum,CG_POOrder.PONo,CG_POOrder.AE from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.id=CAI_OrderInHouses.Id
left join CG_POOrder on CAI_OrderInHouse.PONO=CG_POOrder.PONo
where CAI_OrderInHouse.status='通过' and GooId={0} AND CG_POOrder.status='通过'
order by CAI_OrderInHouse.ProNo desc", goodId);
            DataTable dt = DBHelp.getDataTable(sql);
            if (dt.Rows.Count > 0)
            {
            //    return string.Format("“项目编号{0},AE{1} 需要出库该商品编码{2}, 名称{3}, 小类 {4},规格 {5} 数量 {6}”",
            //        dt.Rows[0]["PONo"], dt.Rows[0]["AE"], GoodNo, GoodName, GoodType, GoodSpec, dt.Rows[0]["GoodNum"]);

                return string.Format("“项目编号{0}是按最晚一个入库单或 销售退货单 对应的 项目编号来,AE{1} ,商品编码{2}, 名称{3}, 小类 {4},规格 {5} 数量 {6}”",
                   dt.Rows[0]["PONo"], dt.Rows[0]["AE"], GoodNo, GoodName, GoodType, GoodSpec, dt.Rows[0]["GoodNum"]); 
            }

            return "";
        }
        public bool updateTran(VAN_OA.Model.JXC.CAI_OrderCheck model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<CAI_OrderChecks> orders, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                CG_POOrdersService OrdersSer = new CG_POOrdersService();
                CG_POCaiService CaiSer = new CG_POCaiService();
                try
                {

                    objCommand.Parameters.Clear();

                    model.Status = eform.state;
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);

                    CAI_OrderChecksService myOrderChecks = new CAI_OrderChecksService();
                    for (int i = 0; i < orders.Count; i++)
                    {
                       // orders[i].CheckId = model.Id;
                        //if (orders[i].IfUpdate == true && orders[i].Ids != 0)
                        //{

                        myOrderChecks.Update(orders[i], objCommand);

                        //}
                        //else if (orders[i].Ids == 0)
                        //{
                        //    OrdersSer.Add(orders[i], objCommand);

                        //}
                    }

                    //入库
                    if (eform.state == "通过")
                    {
                        tb_EForm eformMain = new tb_EForm();
                        int proId = 0;
                        string sql = "select Pro_Id from A_ProInfo where pro_Type='采购入库'";
                        objCommand.CommandText=sql;

                        proId=Convert.ToInt32( objCommand.ExecuteScalar());

                        eformMain.appPer = eform.appPer;
                        eformMain.appTime = DateTime.Now;
                        eformMain.createPer = eform.appPer;
                        eformMain.createTime = DateTime.Now;
                        eformMain.proId = proId;
                        eformMain.state = "通过";
                        eformMain.toPer = 0;
                        eformMain.toProsId = 0;

                        CAI_OrderInHouseService orderInHouse = new CAI_OrderInHouseService();
                        sql = "select top 1 id from TB_HouseInfo where ifdefault=1 ";
                        objCommand.CommandText=sql;
                        int objhouseId = Convert.ToInt32(objCommand.ExecuteScalar());
                        CAI_OrderInHouse orderInModel = new CAI_OrderInHouse()
                        {
                            ChcekProNo = model.ProNo,
                            CreateTime = DateTime.Now,
                            CreateUserId = eform.appPer,
                            GuestName = orders[0].GuestName,
                            HouseID = objhouseId,
                            POName = orders[0].POName,
                            PONo = orders[0].PONo,
                            RuTime = DateTime.Now,
                            Status = "通过",
                            Supplier = orders[0].SupplierName,
                            DoPer = orders[0].CaiGouPer,
                            DaiLi = orders[0].CaiGouPer,
                            FPNo = "",
                            Remark = ""
                        };
                        List<CAI_OrderInHouses> ordersInHouses = new List<CAI_OrderInHouses>();
                        foreach (var m in orders)
                        {
                            CAI_OrderInHouses orderM = new CAI_OrderInHouses() { 
                                GooId=m.CheckGoodId,
                                GoodNum=m.CheckNum,
                                GoodPrice=m.CheckPrice,
                                OrderCheckIds=m.Ids,
                                QingGouPer=m.QingGou,
                                CaiLastTruePrice = m.CheckLastTruePrice
                            };
                            ordersInHouses.Add(orderM);
                        }

                        orderInHouse.addTran(orderInModel, eformMain, ordersInHouses, objCommand);



                    }
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.JXC.CAI_OrderCheck model, VAN_OA.Model.EFrom.tb_EForm eform, List<CAI_OrderChecks> orders, out int MainId, List<string> ids = null)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                CAI_OrderChecksService OrdersSer = new CAI_OrderChecksService();


                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("CAI_OrderCheck", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    model.Status = eform.state;
                    id = Add(model, objCommand);
                    model.Id = id;
                    MainId = id;

                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].CheckId = id;
                       orders[i].Ids= OrdersSer.Add(orders[i], objCommand);

                    }

                    //入库
                    if (eform.state == "通过")
                    {
                        tb_EForm eformMain = new tb_EForm();
                        int proId = 0;
                        string sql = "select Pro_Id from A_ProInfo where pro_Type='采购入库'";
                        objCommand.CommandText = sql;

                        proId = Convert.ToInt32(objCommand.ExecuteScalar());

                        eformMain.appPer = eform.appPer;
                        eformMain.appTime = DateTime.Now.AddSeconds(1);
                        eformMain.createPer = eform.appPer;
                        eformMain.createTime = DateTime.Now.AddSeconds(1);
                        eformMain.proId = proId;
                        eformMain.state = "通过";
                        eformMain.toPer = 0;
                        eformMain.toProsId = 0;
                        eform.E_LastTime = DateTime.Now.AddSeconds(1);
                        CAI_OrderInHouseService orderInHouse = new CAI_OrderInHouseService();
                        sql = "select top 1 id from TB_HouseInfo where ifdefault=1 ";
                        objCommand.CommandText = sql;
                        int objhouseId = Convert.ToInt32(objCommand.ExecuteScalar());
                        CAI_OrderInHouse orderInModel = new CAI_OrderInHouse()
                        {
                            ChcekProNo = model.ProNo,
                            CreateTime = DateTime.Now.AddSeconds(1),
                            CreateUserId = eform.appPer,
                            GuestName = orders[0].GuestName,
                            HouseID = objhouseId,
                            POName = orders[0].POName,
                            PONo = orders[0].PONo,
                            RuTime = DateTime.Now.AddSeconds(1),
                            Status = "通过",
                            Supplier = orders[0].SupplierName,
                            DoPer = orders[0].CaiGouPer,
                            DaiLi = orders[0].CaiGouPer,
                            FPNo = "",
                            Remark = ""
                        };
                        List<CAI_OrderInHouses> ordersInHouses = new List<CAI_OrderInHouses>();
                        foreach (var m in orders)
                        {
                            CAI_OrderInHouses orderM = new CAI_OrderInHouses()
                            {
                                GooId = m.CheckGoodId,
                                GoodNum = m.CheckNum,
                                GoodPrice = m.CheckPrice,
                                OrderCheckIds = m.Ids,
                                QingGouPer = m.QingGou,
                                CaiLastTruePrice = m.CheckLastTruePrice
                            };
                            ordersInHouses.Add(orderM);
                        }
                        orderInHouse.addTran(orderInModel, eformMain, ordersInHouses, objCommand);
                        if (ids != null)
                        {
                            foreach (var ordersIn in ordersInHouses)
                            {
                                ids.Add(ordersIn.Ids.ToString());
                            }
                        }
                      

                    }

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CAI_OrderCheck model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CheckPer != null)
            {
                strSql1.Append("CheckPer,");
                strSql2.Append("" + model.CheckPer + ",");
            }
            if (model.CheckTime != null)
            {
                strSql1.Append("CheckTime,");
                strSql2.Append("'" + model.CheckTime + "',");
            }
            if (model.CreatePer != null)
            {
                strSql1.Append("CreatePer,");
                strSql2.Append("" + model.CreatePer + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("getdate(),");
            }
            if (model.CheckRemark != null)
            {
                strSql1.Append("CheckRemark,");
                strSql2.Append("'" + model.CheckRemark + "',");
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            strSql.Append("insert into CAI_OrderCheck(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.JXC.CAI_OrderCheck model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_OrderCheck set ");

            if (model.CheckRemark != null)
            {
                strSql.Append("CheckRemark='" + model.CheckRemark + "',");
            }
            else
            {
                strSql.Append("CheckRemark= null ,");
            }

            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }

            if (model.Status != null && model.Status != "执行中")
            {
                strSql.Append("CheckTime=getdate(),");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderCheck ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderCheck GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CAI_OrderCheck.Id,CheckPer,CheckTime,CreatePer,CreateTime,CheckRemark ,tb_User.loginName as CreateName,CheckUser.loginName as CheckUserName,ProNo,Status");
            strSql.Append(" from CAI_OrderCheck left join tb_User on tb_User.id=CreatePer left join tb_User as CheckUser on CheckUser.id=CheckPer ");
            strSql.Append(" where CAI_OrderCheck.Id=" + id + "");

            VAN_OA.Model.JXC.CAI_OrderCheck model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_OrderCheck> GetListArray(string strWhere, PagerDomain page)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append("select   ");
            strSql.Append("distinct tb_EForm.e_LastTime,IsHanShui,CAI_OrderCheck.Id,CheckPer,CAI_OrderCheck.CheckTime,CAI_OrderCheck.CreatePer,CAI_OrderCheck.CreateTime,CheckRemark ,CheckUser.loginName as CheckUserName,ProNo,Status,AE");
            strSql.Append(",PONo,POName,CAI_OrderChecks.GUESTName,SupplierName from CAI_OrderCheck left join CAI_OrderChecks on CAI_OrderChecks.CheckId=CAI_OrderCheck.id left join tb_User as CheckUser on CheckUser.id=CheckPer ");
            strSql.Append(" left join (select distinct PONo as CGPONO,AE from  CAI_POOrder) as tb on tb.CGPONO=CAI_OrderChecks.PONO  left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId ");
            strSql.Append(" left join tb_EForm on tb_EForm.allE_id=CAI_OrderCheck.id and proId=21 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
          
            //strSql.Append(" order by CAI_OrderCheck.Id desc");
            strSql.Append(") AS TEMP");
            strSql = new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), "", "  Id desc "));

          
           
            List<VAN_OA.Model.JXC.CAI_OrderCheck> list = new List<VAN_OA.Model.JXC.CAI_OrderCheck>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CAI_OrderCheck model = new VAN_OA.Model.JXC.CAI_OrderCheck();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["CheckPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckPer = (int)ojb;
                        }
                        ojb = dataReader["CheckTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckTime = (DateTime)ojb;
                        }

                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        ojb = dataReader["e_LastTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTime = (DateTime)ojb;
                        }
                        model.CheckRemark = dataReader["CheckRemark"].ToString();
                        //ojb = dataReader["CreateName"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.CreateName = ojb.ToString();
                        //}

                        ojb = dataReader["CheckUserName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckUserName = ojb.ToString();
                        }

                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["Status"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Status = ojb.ToString();
                        }

                        ojb = dataReader["PONo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo = ojb.ToString();
                        }

                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }

                        ojb = dataReader["GUESTName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GUESTName = ojb.ToString();
                        }

                        ojb = dataReader["SupplierName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierName = ojb.ToString();
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = (int)ojb;
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
        public List<VAN_OA.Model.JXC.CAI_OrderCheck> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("distinct tb_EForm.e_LastTime,IsHanShui,CAI_OrderCheck.Id,CheckPer,CAI_OrderCheck.CheckTime,CAI_OrderCheck.CreatePer,CAI_OrderCheck.CreateTime,CheckRemark ,CheckUser.loginName as CheckUserName,ProNo,Status,AE");
            strSql.Append(",PONo,POName,CAI_OrderChecks.GUESTName,SupplierName from CAI_OrderCheck left join CAI_OrderChecks on CAI_OrderChecks.CheckId=CAI_OrderCheck.id left join tb_User as CheckUser on CheckUser.id=CheckPer ");
            strSql.Append(" left join (select AE,PONO as CGPONO from CG_POOrder where IFZhui=0 and Status='通过') as tb on tb.CGPONO=CAI_OrderChecks.PONO  left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId ");
            strSql.Append(" left join tb_EForm on tb_EForm.allE_id=CAI_OrderCheck.id and proId=21 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CAI_OrderCheck.Id desc");
            List<VAN_OA.Model.JXC.CAI_OrderCheck> list = new List<VAN_OA.Model.JXC.CAI_OrderCheck>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CAI_OrderCheck model = new VAN_OA.Model.JXC.CAI_OrderCheck();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["CheckPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckPer = (int)ojb;
                        }
                        ojb = dataReader["CheckTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckTime = (DateTime)ojb;
                        }
                       
                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        ojb = dataReader["e_LastTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTime = (DateTime)ojb;
                        }
                        model.CheckRemark = dataReader["CheckRemark"].ToString();
                        //ojb = dataReader["CreateName"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.CreateName = ojb.ToString();
                        //}

                        ojb = dataReader["CheckUserName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckUserName = ojb.ToString();
                        }

                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["Status"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Status = ojb.ToString();
                        }

                        ojb = dataReader["PONo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONo = ojb.ToString();
                        }

                        ojb = dataReader["POName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POName = ojb.ToString();
                        }

                        ojb = dataReader["GUESTName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GUESTName = ojb.ToString();
                        }

                        ojb = dataReader["SupplierName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierName = ojb.ToString();
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = (int)ojb;
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
        public List<VAN_OA.Model.JXC.CAI_OrderCheck> GetListArrayToQuery(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" CAI_OrderCheck.Id,CheckPer,CheckTime,CreatePer,CreateTime,CheckRemark ,tb_User.loginName as CreateName,CheckUser.loginName as CheckUserName,ProNo,Status");
            strSql.Append(" from CAI_OrderCheck left join tb_User on tb_User.id=CreatePer left join tb_User as CheckUser on CheckUser.id=CheckPer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderCheck> list = new List<VAN_OA.Model.JXC.CAI_OrderCheck>();

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


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderCheck ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderCheck model = new VAN_OA.Model.JXC.CAI_OrderCheck();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CheckPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckPer = (int)ojb;
            }
            ojb = dataReader["CheckTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckTime = (DateTime)ojb;
            }
            ojb = dataReader["CreatePer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreatePer = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            model.CheckRemark = dataReader["CheckRemark"].ToString();
            ojb = dataReader["CreateName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateName = ojb.ToString();
            }

            ojb = dataReader["CheckUserName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckUserName = ojb.ToString();
            }

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }



            return model;
        }



    }
}
