using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.EFrom;
using VAN_OA.Model.JXC;

namespace VAN_OA
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        Pro_JSXDetailInfoService jxcDetailSer = new Pro_JSXDetailInfoService();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DateTime fromDate = Convert.ToDateTime("2000-1-1");
                DateTime toDate = Convert.ToDateTime("2036-1-1");

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();

                    var selectInfo = string.Format(@"select GoodId from TB_Good WHERE GoodStatus<>'不通过' and goodNo='12218'");
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.CommandText = selectInfo;

                    List<int> goodIdList = new List<int>();
                    using (var reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            goodIdList.Add(Convert.ToInt32(reader["GoodId"]));
                        }
                        reader.Close();
                    }

                    foreach (var goodId in goodIdList)
                    {
                        //查找进销存的所有信息
                        List<Pro_JSXDetailInfo> pOOrderList = this.jxcDetailSer.GetListArray(1, goodId, fromDate, toDate);
                        pOOrderList.Sort(delegate (Pro_JSXDetailInfo a, Pro_JSXDetailInfo b) { return a.RuTime.CompareTo(b.RuTime); });

                        jxcDetailSer.ReSetPro_JSXDetailInfo(pOOrderList);
                        decimal iniNum = 0;
                        for (int i = 0; i < pOOrderList.Count; i++)
                        {
                            var model = pOOrderList[i];
                            if (i == 0)
                            {
                                iniNum = model.GoodInNum - model.GoodOutNum;
                                //i++;
                            }
                            else
                            {
                                iniNum = iniNum + model.GoodInNum - model.GoodOutNum;
                            }
                            pOOrderList[i].GoodResultNum = iniNum;
                        }
                        //计算
                     
                        Dictionary<int, decimal> inUpdatePrice = new Dictionary<int, decimal>();
                        Dictionary<int, decimal> outUpdatePrice = new Dictionary<int, decimal>();

                        Dictionary<int, decimal> sell_inUpdatePrice = new Dictionary<int, decimal>();
                        Dictionary<int, decimal> sell_outUpdatePrice = new Dictionary<int, decimal>();

                        Decimal avgPrice = 0;
                        int startIndex = 0;
                        foreach (var jxc in pOOrderList)
                        {
                            if (jxc.GoodResultNum != 0)
                            {
                                if (jxc.ProNo == "20120096")
                                {

                                }
                                decimal topNum = startIndex > 1 ? pOOrderList[startIndex - 1].GoodResultNum : 0;
                                if (jxc.TypeName == "采购入库")
                                {
                                    avgPrice = (jxc.GoodInNum * jxc.avgHousePrice + avgPrice* topNum) / jxc.GoodResultNum;
                                    inUpdatePrice.Add(jxc.Ids, avgPrice);
                                }
                                if (jxc.TypeName == "销售退货")
                                {
                                    avgPrice = (jxc.GoodInNum * jxc.avgHousePrice + avgPrice * topNum) / jxc.GoodResultNum;
                                    sell_outUpdatePrice.Add(jxc.Ids, avgPrice);
                                }


                                if (jxc.TypeName == "采购退货")
                                {  
                                    avgPrice = (avgPrice * topNum-jxc.GoodOutNum * jxc.avgHousePrice) / jxc.GoodResultNum;
                                    outUpdatePrice.Add(jxc.Ids, avgPrice);
                                }
                                if ( jxc.TypeName == "销售出库")
                                {

                                    avgPrice = (avgPrice * topNum - jxc.GoodOutNum * jxc.avgHousePrice) / jxc.GoodResultNum;

                                    sell_inUpdatePrice.Add(jxc.Ids, avgPrice);
                                }
                            }
                            else
                            {
                                avgPrice = 0;
                            }
                            startIndex++;
                        }

                        StringBuilder sbSql = new StringBuilder();
                        foreach (var m in inUpdatePrice)
                        {
                            sbSql.AppendFormat("update CAI_OrderInHouses set TempHousePrice={1} where Ids={0}; ", m.Key, m.Value);
                        }
                        foreach (var m in outUpdatePrice)
                        {
                            sbSql.AppendFormat("update CAI_OrderOutHouses set TempHousePrice={1} where Ids={0}; ", m.Key, m.Value);
                        }

                        foreach (var m in sell_inUpdatePrice)
                        {
                            sbSql.AppendFormat("update Sell_OrderOutHouses set TempHousePrice={1} where Ids={0}; ", m.Key, m.Value);
                        }
                        foreach (var m in sell_outUpdatePrice)
                        {
                            sbSql.AppendFormat("update Sell_OrderInHouses set TempHousePrice={1} where Ids={0}; ", m.Key, m.Value);
                        }
                        if (!string.IsNullOrEmpty(sbSql.ToString()))
                        {
                            objCommand.CommandText = sbSql.ToString();
                            objCommand.ExecuteNonQuery();
                        }
                    }

                }
            }
        }
    }
}