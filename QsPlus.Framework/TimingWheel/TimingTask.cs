//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.TimingWheel
{
    /// <summary>
    /// 定时任务类。
    /// </summary>
    internal sealed class TimingTask : ITimingTask, IReference
    {
        /// <summary>
        /// 定时任务编号。
        /// </summary>
        public int TimingTaskId { get; set; }

        /// <summary>
        /// 预定的定时开始到结束的时间。
        /// </summary>
        public float TimingTime { get; set; }

        /// <summary>
        /// 是否使用预定的时间循环定时任务。
        /// </summary>
        public bool IsLoopTimingTask { get; set; }

        /// <summary>
        /// 是否处理定时任务。
        /// </summary>
        public bool IsHandleTimingTask { get; set; }

        /// <summary>
        /// 是否取消定时任务。
        /// </summary>
        public bool IsCancelTimingTask { get; set; }

        /// <summary>
        /// 定时待执行任务。
        /// </summary>
        public QsPlusFrameworkAction OnTimingTask { get; set; }

        /// <summary>
        /// 计时器。
        /// </summary>
        private float _timer;

        /// <summary>
        /// 初始化定时任务类的新实例。
        /// </summary>
        public TimingTask()
        {
            TimingTaskId = 0;
            TimingTime = 0;
            IsLoopTimingTask = false;
            IsHandleTimingTask = false;
            IsCancelTimingTask = false;
            OnTimingTask = null;
            _timer = 0;
        }

        /// <summary>
        /// 创建定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <param name="timingTime">预定的定时开始到结束的时间。</param>
        /// <param name="isLoopTimingTask">是否使用预定的时间循环定时任务。</param>
        /// <param name="isHandleTimingTask">是否处理定时任务。</param>
        /// <param name="isCancelTimingTask">是否取消定时任务。</param>
        /// <param name="onTimingTask">定时待执行任务。</param>
        public static TimingTask CreateTimingTask(int timingTaskId, float timingTime, bool isLoopTimingTask, bool isHandleTimingTask, bool isCancelTimingTask, QsPlusFrameworkAction onTimingTask)
        {
            TimingTask tempTimingTask = InternalReferencePool.AcquireReference<TimingTask>();
            tempTimingTask.TimingTaskId = timingTaskId;
            tempTimingTask.TimingTime = timingTime;
            tempTimingTask.IsLoopTimingTask = isLoopTimingTask;
            tempTimingTask.IsHandleTimingTask = isHandleTimingTask;
            tempTimingTask.IsCancelTimingTask = isCancelTimingTask;
            tempTimingTask.OnTimingTask = onTimingTask;
            return tempTimingTask;
        }

        /// <summary>
        /// 轮询定时任务。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void TimingTaskUpdate(float logicTime, float actualTime)
        {
            if (IsCancelTimingTask)
            {
                return;
            }

            IsHandleTimingTask = (_timer += logicTime) >= TimingTime;
        }

        /// <summary>
        /// 定时任务重置。
        /// </summary>
        public void TimingTaskReset()
        {
            _timer = 0;
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public void ClearReference()
        {
            TimingTaskId = 0;
            TimingTime = 0;
            IsLoopTimingTask = false;
            IsHandleTimingTask = false;
            IsCancelTimingTask = false;
            OnTimingTask = null;
            _timer = 0;
        }
    }
}