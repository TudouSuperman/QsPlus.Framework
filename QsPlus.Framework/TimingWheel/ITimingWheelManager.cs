//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Common;

namespace QsPlus.Framework.TimingWheel
{
    /// <summary>
    /// 时间轮管理器接口。
    /// </summary>
    public interface ITimingWheelManager
    {
        /// <summary>
        /// 当前定时任务数量。
        /// </summary>
        int GetCurrentTimingTaskCount { get; }

        /// <summary>
        /// 是否存在定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <returns>是否存在定时任务。</returns>
        bool HasTimingTask(int timingTaskId);

        /// <summary>
        /// 获取定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <returns>要获取的定时任务。</returns>
        ITimingTask GetTimingTask(int timingTaskId);

        /// <summary>
        /// 启动定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <param name="timingTime">预定的定时开始到结束的时间。</param>
        /// <param name="isLoopTimingTask">是否使用预定的时间循环定时任务。</param>
        /// <param name="onTimingTask">定时待执行任务。</param>
        void StartTimingTask(int timingTaskId, float timingTime, bool isLoopTimingTask, QsPlusFrameworkAction onTimingTask);

        /// <summary>
        /// 移除定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        void RemoveTimingTask(int timingTaskId);

        /// <summary>
        /// 移除所有定时任务。
        /// </summary>
        void RemoveAllTimingTasks();
    }
}