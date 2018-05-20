namespace VAN_OA
{
    /// <summary>
    /// 在此描述IJobManager的说明
    /// </summary>
    /// <summary>
    /// Job控制器接口
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// 启动Job
        /// </summary>
        void Start();
        /// <summary>
        /// 停止Job
        /// </summary>
        void ShutDown();
        /// <summary>
        /// 挂起Job
        /// </summary>
        // void Suspend();
        /// <summary>
        /// 挂起Job
        /// </summary>
        void Suspend(string jobName, string groupName);
        /// <summary>
        /// 删除Job
        /// </summary>
        void DeleteJob(string jobName, string groupName);
        /// <summary>
        /// 恢复Job
        /// </summary>
        // void UnSuspend();
        /// <summary>
        /// 恢复Job
        /// </summary>
        void UnSuspend(string jobName, string groupName);
        /// <summary>
        /// 当前Job运行状态
        /// </summary>
        /// <returns></returns>
        JobStatus GetJobStatus();
        /// <summary>
        /// 获得job
        /// </summary>
        /// <param name="groupName">job所在的组</param>
        /// <returns></returns>
        string[] GetJobNames(string groupName);
    }

    /// <summary>
    /// Job状态
    /// </summary>
    public enum JobStatus
    {
        None,
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 关闭
        /// </summary>
        ShutDown,
        /// <summary>
        /// 挂起
        /// </summary>
        Suspend,
        /// <summary>
        /// job关闭
        /// </summary>
        DeleteJob
    }
}
