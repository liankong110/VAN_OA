using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using VAN_OA.Model.ReportForms;
using System.Data.SqlClient;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

namespace VAN_OA
{
    public class MyJob : IJob
    {

        private int GetZhouQi(int month)
        {
            if (1 <= month && month <= 3)
            {
                return 1;
            }
            else if (4 <= month && month <= 6)
            {
                return 2;
            }
            else if (7 <= month && month <= 9)
            {
                return 3;
            }
            else if (10 <= month && month <= 12)
            {
                return 4;
            }
            return 0;
        }
        public void Execute()
        {
            ServiceAppSetting.LoggerHander.Invoke(string.Format("执行一次! - {0}", DateTime.Now), "");
            //每天早上1，2，3点处理最高值
            if ((DateTime.Now.Hour == 1||DateTime.Now.Hour == 2||DateTime.Now.Hour == 3) && DateTime.Now.Minute== 0)
            {
                ServiceAppSetting.LoggerHander.Invoke(string.Format("执行-最高金额! - {0}", DateTime.Now), "");
                  CashFlowService cashFlowService = new CashFlowService();
                  List<CashFlow> cashFlowList = cashFlowService.GetListArray(" AND IsClose=0");

                  using (SqlConnection conn = DBHelp.getConn())
                  {
                      conn.Open();                    
                      SqlCommand objCommand = conn.CreateCommand();                    
                      try
                      {

                          objCommand.Parameters.Clear();
                          foreach (var model in cashFlowList)
                          {
                              cashFlowService.Update(model,objCommand);
                          }
                         
                      }
                      catch (Exception)
                      {

                      }

                  }
                  ServiceAppSetting.LoggerHander.Invoke(string.Format("结束-最高金额! - {0}", DateTime.Now), "");
            }
            if (DateTime.Now.Hour == 23  && DateTime.Now.Minute <= 5)
            {
                ServiceAppSetting.LoggerHander.Invoke(string.Format("执行一次! - {0}", DateTime.Now), "");
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;

                int nextYear = DateTime.Now.AddMonths(1).Year;
                int nextMonth = DateTime.Now.AddMonths(1).Month;

                int currentZhangQi = GetZhouQi(currentMonth);
                int nextZhangQi = GetZhouQi(nextMonth);
                //if (currentZhangQi == nextZhangQi && currentYear == nextYear)
                //{
                //    return;
                //}

                TB_GuestTrackService GuestTrackSer = new TB_GuestTrackService();
                string sql = string.Format(" 1=1 and QuartNo='{1}' and YearNo='{0}' ", currentYear, currentZhangQi);
                sql += string.Format(@" and (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))");
                List<TB_GuestTrack> GuestTracks = GuestTrackSer.GetListArray(sql);

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    try
                    {

                        objCommand.Parameters.Clear();
                        foreach (var model in GuestTracks)
                        {
                            model.QuartNo = nextZhangQi.ToString();
                            model.YearNo = nextYear.ToString();
                            model.CreateTime = DateTime.Now;
                            string update = GuestTrackSer.AddToString(model);

                            string secondUpdate = GuestTrackSer.UpdateToString(model, model.GuestId, nextYear.ToString(), nextZhangQi.ToString());

                            string updateSql = string.Format("if not exists(select id from TB_GuestTrack where guestId='{0}' and yearNo='{1}' and QuartNo='{2}')begin {3} end else begin {4} end",
                                model.GuestId, nextYear.ToString(), nextZhangQi.ToString(), update, secondUpdate);
                            objCommand.CommandText = updateSql;
                            objCommand.ExecuteNonQuery();
                        }

                        tan.Commit();
                        ServiceAppSetting.LoggerHander.Invoke(string.Format("执行成功! - {0}", DateTime.Now), "");
                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                        ServiceAppSetting.LoggerHander.Invoke(string.Format("账期执行失败! - {0}", DateTime.Now), "Error");
                    }

                }
            }

        }

        public void Execute(JobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
