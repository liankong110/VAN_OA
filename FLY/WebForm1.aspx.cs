using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.EFrom;

namespace VAN_OA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack)
            {
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();

                    var selectInfo = string.Format(@" select Id,DisDate,OutDispater from tb_Dispatching where MyXiShu=0  and DisDate>='2018-2-22' ");
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.CommandText = selectInfo;

                    List<tb_Dispatching> idsList = new List<tb_Dispatching>();
                    using (var reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tb_Dispatching model = new tb_Dispatching();
                            model.Id = Convert.ToInt32(reader["Id"]);
                            model.DisDate = Convert.ToDateTime(reader["DisDate"]);
                            model.OutDispater = Convert.ToInt32(reader["OutDispater"]);
                            idsList.Add(model);
                        }
                        reader.Close();
                    }
                    foreach (var m in idsList)
                    {
                        string sql = string.Format(@"select h.ShouldPayment
 FROM HR_Payment H right join HR_Person P on H.ID=P.ID 
 where  P.ID='{0}' and H.YearMonth='{1}'", m.OutDispater, m.DisDate.Value.AddMonths(-2).Year + "-" +
 m.DisDate.Value.AddMonths(-2).Month.ToString("00"));
                        objCommand.CommandText = sql;
                        var val = objCommand.ExecuteScalar();
                        decimal xishu = 0;
                        if (val != null && !(val is DBNull))
                        {
                            xishu = Convert.ToDecimal(val) / 3000;
                        }

                        objCommand.CommandText = string.Format("update tb_Dispatching set MyXiShu={1} where id={0}", m.Id, xishu);
                        objCommand.ExecuteNonQuery();

                    }
                }
            }
        }
    }
}