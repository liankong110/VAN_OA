using System;
using System.Configuration;
using System.Linq;
using Quartz;
using Quartz.Impl;

namespace VAN_OA
{
    public class MinuteJobManager : IJobManager
    {
        private static MinuteJobManager _job;
        private static readonly object ObjLock = new object();
        private IScheduler _scheduler;

        public static MinuteJobManager Instance
        {
            get
            {
                if (_job == null)
                {
                    lock (ObjLock)
                    {
                        if (_job == null)
                        {
                            _job = new MinuteJobManager();
                        }
                    }
                }
                return _job;
            }
        }

        /// <summary>
        /// 启动Job
        /// </summary>
        public void Start()
        {
            if (_scheduler != null && !_scheduler.IsStarted)
            {
                _scheduler.Start();
            }
        }
        /// <summary>
        /// 停止Job
        /// </summary>
        public void ShutDown()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                _scheduler.Shutdown(false);
            }
        }

        /// <summary>
        /// 彻底关闭JOB
        /// </summary>
        public void Close()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                _scheduler.Shutdown(true);
            }
        }

        /// <summary>
        /// 挂起Job
        /// </summary>
        public void Suspend(string jobName, string groupName)
        {
            if (_scheduler != null && _scheduler.IsStarted)
            {
                //_scheduler.PauseJob(jobName, groupName);
            }
        }

        /// <summary>
        /// 挂起Job
        /// </summary>
        public void Suspend()
        {
            Suspend("MinuteJob", "MinuteJobGroup");
        }

        /// <summary>
        /// 删除Job
        /// </summary>
        public void DeleteJob(string jobName, string groupName)
        {
            if (_scheduler != null && _scheduler.IsStarted)
            {
                //_scheduler.DeleteJob(jobName, groupName);
            }
        }

        /// <summary>
        /// 删除Job
        /// </summary>
        public void DeleteJob()
        {
            DeleteJob("MinuteJob", "MinuteJobGroup");
        }

        /// <summary>
        /// 恢复Job
        /// </summary>
        public void UnSuspend(string jobName, string groupName)
        {
            if (_scheduler != null && _scheduler.IsStarted)
            {
                //_scheduler.PauseJob(jobName, groupName);
            }
        }

        /// <summary>
        /// 恢复Job
        /// </summary>
        public void UnSuspend()
        {
            UnSuspend("MinuteJob", "MinuteJobGroup");
        }

        /// <summary>
        /// 获得控制器中的job
        /// </summary>
        /// <param name="groupName">job所在的组</param>
        /// <returns></returns>
        public string[] GetJobNames(string groupName)
        {
            var jobNames = new string[100];
            if (_scheduler != null)
            {
                //jobNames = _scheduler.GetJobNames(groupName);
            }
            return jobNames;
        }

        /// <summary>
        /// 创建Job
        /// </summary>
        public void SetMinuteJob(Type type)
        {
            string cronExpr = ConfigurationManager.AppSettings["cronExpr"] ?? "0 * * * * ?";
            object[,] arrJob = {
                                   {
                                       "MinuteJob", "MinuteJobGroup", type, "MinuteJobTrigger", cronExpr
                                   }
                               };
            ISchedulerFactory shFactory = new StdSchedulerFactory();
            _scheduler = shFactory.GetScheduler();

            //var getfaxjob = new JobDetail(arrJob[0, 0].ToString(), arrJob[0, 1].ToString(), (Type)arrJob[0, 2]);
            //var getfaxtrigger = new CronTrigger(arrJob[0, 3].ToString(), arrJob[0, 1].ToString(), arrJob[0, 4].ToString());
            //_scheduler.ScheduleJob(getfaxjob, getfaxtrigger);
        }

        /// <summary>
        /// 当前Job运行状态
        /// </summary>
        /// <returns></returns>
        public JobStatus GetJobStatus()
        {
            JobStatus status = JobStatus.None;
            if (_scheduler != null)
            {
                if (_scheduler.IsStarted)
                    status = JobStatus.Running;
                if (_scheduler.IsShutdown)
                    status = JobStatus.ShutDown;
                if (_scheduler.IsJobGroupPaused("MinuteJobGroup"))
                    status = JobStatus.Suspend;
                //if (!_scheduler.JobGroupNames.Contains("MinuteJobGroup"))
                //{
                //    if (!_scheduler.GetJobNames("MinuteJobGroup").Contains("MinuteJob"))
                //        status = JobStatus.DeleteJob;
                //}
            }
            return status;
        }

        /// <summary>
        /// 获得控制器指定的组是否挂起
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public bool IsSuspend(string groupName)
        {
            bool isSuspend = false;
            if (_scheduler != null)
            {
                isSuspend = _scheduler.IsJobGroupPaused(groupName);
            }
            return isSuspend;
        }
    }
}
