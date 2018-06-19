using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.EFrom;
using VAN_OA.Model.JXC;

namespace VAN_OA
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();

                    var selectInfo = string.Format(@"select 1 as Type,GooId,Ids,RuTime from CAI_OrderInHouses left join CAI_OrderInHouse on CAI_OrderInHouses.id=CAI_OrderInHouse.Id where Status='通过'
union all 
select -1 as Type,GooId,Ids,RuTime from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where Status='通过'");
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.CommandText = selectInfo;

                    List<CAI_OrderInHouses> idsList = new List<CAI_OrderInHouses>();
                    using (var reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CAI_OrderInHouses model = new CAI_OrderInHouses();
                            model.GooId = Convert.ToInt32(reader["GooId"]);
                            model.Ids = Convert.ToInt32(reader["Ids"]);
                            model.RuTime = Convert.ToDateTime(reader["RuTime"]);
                            model.Type = Convert.ToInt32(reader["Type"]);
                            idsList.Add(model);
                        }
                        reader.Close();
                    }
                    foreach (var m in idsList)
                    {
                        string sql = string.Format(@"select AVG(GoodPrice) AS AVGPrice from (
select RuTime,GoodPrice,GooId from CAI_OrderInHouses left join CAI_OrderInHouse on CAI_OrderInHouses.id=CAI_OrderInHouse.Id where Status='通过'
union all 
select RuTime,GoodPrice,GooId from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where Status='通过'
) as tempTB WHERE GooId={0} and RUTIME <='{1}'", m.GooId, m.RuTime);
                        objCommand.CommandText = sql;
                        var val = objCommand.ExecuteScalar();
                        decimal xishu = 0;
                        if (val != null && !(val is DBNull))
                        {
                            if (m.Type == 1)
                            {
                                objCommand.CommandText = string.Format("update CAI_OrderInHouses set TempHousePrice={1} where Ids={0}", m.Ids, val);
                                objCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                objCommand.CommandText = string.Format("update CAI_OrderOutHouses set TempHousePrice={1} where Ids={0}", m.Ids, val);
                                objCommand.ExecuteNonQuery();
                            }
                         
                        }

                       

                    }
                }
            }
        }
    }
}