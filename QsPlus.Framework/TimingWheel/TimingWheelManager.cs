//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.TimingWheel
{
    /// <summary>
    /// 时间轮管理器类。
    /// </summary>
    internal sealed class TimingWheelManager : IQsPlusFrameworkModule, ITimingWheelManager
    {
        private readonly LinkedList<TimingTask> _timingTasks;
        private readonly IList<int> _timingTaskIds;
        private readonly Queue<TimingTask> _timingTaskHandleQueue;

        /// <summary>
        /// 当前定时任务数量。
        /// </summary>
        public int GetCurrentTimingTaskCount => _timingTasks.Count;

        /// <summary>
        /// 初始化时间轮管理器类的新实例。
        /// </summary>
        public TimingWheelManager()
        {
            _timingTasks = new LinkedList<TimingTask>();
            _timingTaskIds = new List<int>();
            _timingTaskHandleQueue = new Queue<TimingTask>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.TimingWheelManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            if (_timingTasks == null || _timingTasks.Count <= 0)
            {
                return;
            }

            foreach (TimingTask itemTimingTask in _timingTasks)
            {
                itemTimingTask.TimingTaskUpdate(logicTime, actualTime);
            }

            _timingTaskHandleQueue.Clear();

            LinkedListNode<TimingTask> current = _timingTasks.First;
            while (current != null)
            {
                if (current.Value.IsHandleTimingTask && !current.Value.IsCancelTimingTask)
                {
                    _timingTaskHandleQueue.Enqueue(current.Value);
                }

                if (!current.Value.IsHandleTimingTask && current.Value.IsCancelTimingTask)
                {
                    InternalReferencePool.ReleaseReference(current.Value);
                    _timingTaskIds.Remove(current.Value.TimingTaskId);
                    _timingTasks.Remove(current.Value);
                }

                current = current.Next;
            }

            while (_timingTaskHandleQueue != null && _timingTaskHandleQueue.Count > 0)
            {
                TimingTask tempTimingTask = _timingTaskHandleQueue.Dequeue();
                if (tempTimingTask == null)
                {
                    continue;
                }

                if (!tempTimingTask.IsHandleTimingTask)
                {
                    continue;
                }

                tempTimingTask.OnTimingTask?.Invoke();

                if (tempTimingTask.IsLoopTimingTask)
                {
                    tempTimingTask.TimingTaskReset();
                    continue;
                }

                InternalReferencePool.ReleaseReference(tempTimingTask);
                _timingTasks.Remove(tempTimingTask);
                _timingTaskIds.Remove(tempTimingTask.TimingTaskId);
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            _timingTaskHandleQueue.Clear();
            _timingTaskIds.Clear();
            _timingTasks.Clear();
        }

        /// <summary>
        /// 是否存在定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <returns>是否存在定时任务。</returns>
        public bool HasTimingTask(int timingTaskId)
        {
            return _timingTaskIds.Contains(timingTaskId);
        }

        /// <summary>
        /// 获取定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <returns>要获取的定时任务。</returns>
        public ITimingTask GetTimingTask(int timingTaskId)
        {
            LinkedListNode<TimingTask> current = _timingTasks.First;
            while (current != null)
            {
                if (timingTaskId == current.Value.TimingTaskId)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// 启动定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        /// <param name="timingTime">预定的定时开始到结束的时间。</param>
        /// <param name="isLoopTimingTask">是否使用预定的时间循环定时任务。</param>
        /// <param name="onTimingTask">定时待执行任务。</param>
        public void StartTimingTask(int timingTaskId, float timingTime, bool isLoopTimingTask, QsPlusFrameworkAction onTimingTask)
        {
            if (_timingTaskIds.Contains(timingTaskId))
            {
                throw new QsPlusFrameworkException($"已存在此 {timingTaskId} 编号定时任务。");
            }

            TimingTask tempTimingTask = TimingTask.CreateTimingTask(timingTaskId, timingTime, isLoopTimingTask, false, false, onTimingTask);
            if (tempTimingTask == null)
            {
                throw new QsPlusFrameworkException("类型为空的要启动的定时任务是无效的。");
            }

            InternalInsertTimingTask(tempTimingTask);
        }

        /// <summary>
        /// 移除定时任务。
        /// </summary>
        /// <param name="timingTaskId">定时任务编号。</param>
        public void RemoveTimingTask(int timingTaskId)
        {
            LinkedListNode<TimingTask> current = _timingTasks.First;
            while (current != null)
            {
                if (timingTaskId == current.Value.TimingTaskId)
                {
                    current.Value.IsCancelTimingTask = true;
                }

                current = current.Next;
            }
        }

        /// <summary>
        /// 移除所有定时任务。
        /// </summary>
        public void RemoveAllTimingTasks()
        {
            while (_timingTaskHandleQueue != null && _timingTaskHandleQueue.Count > 0)
            {
                TimingTask tempTimingTask = _timingTaskHandleQueue.Dequeue();
                if (tempTimingTask == null)
                {
                    continue;
                }

                InternalReferencePool.ReleaseReference(tempTimingTask);
            }

            LinkedListNode<TimingTask> current = _timingTasks.First;
            while (current != null)
            {
                InternalReferencePool.ReleaseReference(current.Value);
                current = current.Next;
            }

            _timingTaskIds.Clear();
            _timingTasks.Clear();
        }

        /// <summary>
        /// 内部插入定时任务。
        /// </summary>
        /// <param name="timingTask">要插入的定时任务。</param>
        private void InternalInsertTimingTask(TimingTask timingTask)
        {
            LinkedListNode<TimingTask> current = _timingTasks.First;
            while (current != null)
            {
                if (timingTask.TimingTime > current.Value.TimingTime)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                _timingTasks.AddBefore(current, timingTask);
            }
            else
            {
                _timingTasks.AddLast(timingTask);
            }

            _timingTaskIds.Add(timingTask.TimingTaskId);
        }
    }
}