using System;
using System.Threading;
using Common.Logging;
using Quartz;
using Quartz.Impl;

namespace VAN_OA
{
    public class MYQuartz : IQuartz
    {
        private IScheduler sched;
        public virtual void Run()
        {
            // First we must get a reference to a scheduler
            //ISchedulerFactory sf = new StdSchedulerFactory();
            //sched = sf.GetScheduler();

            //// job 1 will run every 20 seconds

            //IJobDetail job = JobBuilder.Create<MyJob>()
            //    .WithIdentity("job1", "group1")
            //    .Build();

            ////60*60
            //ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
            //                                          .WithIdentity("trigger1", "group1")
            //                                          .WithCronSchedule("0/60 * * * * ?")
            //                                          .Build();
            //DateTimeOffset ft = sched.ScheduleJob(job, trigger);
            //sched.Start();
        }




        public void Close()
        {
            sched.Shutdown(true);
        }
    }
}
