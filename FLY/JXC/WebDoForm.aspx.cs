using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace VAN_OA.JXC
{
    public partial class WebDoForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();              
                SqlCommand objCommand = conn.CreateCommand();

                string plSql = "select PONO from CG_POOrder where ifzhui=0";
                var dt=DBHelp.getDataTable(plSql);

                List<VAN_OA.Model.JXC.Sell_OrderFP> list = new List<VAN_OA.Model.JXC.Sell_OrderFP>();
                string fpSql = "SELECT PONo,[FPNo]FROM [Sell_OrderFP] where Status='通过'";
                objCommand.CommandText = fpSql;
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        model.PONo = dataReader["PONo"].ToString();
                        model.FPNo = dataReader["FPNo"].ToString();
                        list.Add(model);
                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string pono = dr[0].ToString();
                    var findList=list.FindAll(t => t.PONo == pono);
                    string fpNo = "";
                    for (int i = 0; i < findList.Count; i++)
                    {
                        fpNo += findList[i].FPNo+"/";
                    }
                   
                    if (!string.IsNullOrEmpty(fpNo))
                    {
                        //更改项目订单的发票号
                        string sql = string.Format("update CG_POOrder set FPTotal='{0}' where PONo='{1}' and ifzhui=0 ", fpNo, pono);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                }   
               

                conn.Close();
            }
        }
    }
}