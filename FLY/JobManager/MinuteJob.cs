using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using Quartz;
using VAN_OA.Dal.ReportForms;
using System.Data.SqlClient;
using VAN_OA.Model.ReportForms;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;


namespace VAN_OA
{
    public class MinuteJob : IJob
    {
        private static readonly object Tclock = new object();

        private static readonly object Tclock1 = new object();

        public void Execute(JobExecutionContext context)
        {          
            try
            {                 
                var dt = DateTime.Now;
                var content = DateTime.Now.ToString() + "执行JOB一次\r\n";
                lock (Tclock)
                {                    
                  

                    //每天早上1，2，3点处理最高值
                  //  if (DateTime.Now.Hour ==10 && DateTime.Now.Minute == 40)
                  if (DateTime.Now.Hour == 2 && DateTime.Now.Minute == 00)
                    {
                        //ServiceAppSetting.LoggerHander.Invoke(string.Format("执行-最高金额! - {0}", DateTime.Now), "");
                        content += "执行最高金额";
                        CashFlowService cashFlowService = new CashFlowService();
                        //List<CashFlow> cashFlowList = cashFlowService.GetListArray(" and IsClose=0");
                        List<CashFlow> cashFlowList = cashFlowService.GetListArray("");

                        using (SqlConnection conn = DBHelp.getConn())
                        {
                            conn.Open();
                            SqlCommand objCommand = conn.CreateCommand();
                            try
                            {
                                objCommand.Parameters.Clear();
                                foreach (var model in cashFlowList)
                                {
                                    cashFlowService.Update(model, objCommand);
                                }

                            }
                            catch (Exception)
                            {
                                content += "【异常】最高金额";
                            }
                        }                      
                        content += "结束-最高金额";
                    }
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 0)
                    {

                        content += "开始-客户联系跟踪表";
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
                                content += "结束-客户联系跟踪表";
                                //ServiceAppSetting.LoggerHander.Invoke(string.Format("执行成功! - {0}", DateTime.Now), "");
                            }
                            catch (Exception)
                            {
                                tan.Rollback();
                                content += "【异常】账期执行失败";
                                //ServiceAppSetting.LoggerHander.Invoke(string.Format("账期执行失败! - {0}", DateTime.Now), "Error");
                            }

                        }
                    }
 
                }

               

                var filepath =
                    HostingEnvironment.MapPath("~/Logs/");
                var filename = filepath + "Job_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.AppendAllText(filename, content);
                if (
                    File.Exists(filepath + "Job_" +
                                DateTime.Now.AddMonths(-1).ToString() + ".txt"))
                {
                    File.Delete(filepath + "Job_" +
                                DateTime.Now.AddMonths(-1).ToString() + ".txt");
                }
            }
            catch (Exception ex)
            {
              
                
            }
        }


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
      
    }
}