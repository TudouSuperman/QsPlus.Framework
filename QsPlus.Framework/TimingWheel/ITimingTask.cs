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
    /// 定时任务接口。
    /// </summary>
    public interface ITimingTask
    {
        /// <summary>
        /// 定时任务编号。
        /// </summary>
        int TimingTaskId { get; }

        /// <summary>
        /// 预定的定时开始到结束的时间。
        /// </summary>
        float TimingTime { get; }

        /// <summary>
        /// 是否使用预定的时间循环定时任务。
        /// </summary>
        bool IsLoopTimingTask { get; }

        /// <summary>
        /// 是否处理定时任务。
        /// </summary>
        bool IsHandleTimingTask { get; }

        /// <summary>
        /// 是否取消定时任务。
        /// </summary>
        bool IsCancelTimingTask { get; }

        /// <summary>
        /// 定时待执行任务。
        /// </summary>
        QsPlusFrameworkAction OnTimingTask { get; }
    }
}