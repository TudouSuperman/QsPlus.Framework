//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Event
{
    /// <summary>
    /// 事件管理器类。
    /// </summary>
    internal sealed class EventManager : IQsPlusFrameworkModule, IEventManager
    {
        /// <summary>
        /// 框架事件缓存。
        /// </summary>
        private readonly IDictionary<int, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object>> _mEvents;

        /// <summary>
        /// 框架事件处理队列。
        /// </summary>
        private readonly Queue<Event> _mHandleEventQueue;

        /// <summary>
        /// 初始化事件管理器类的新实例。
        /// </summary>
        public EventManager()
        {
            _mEvents = new Dictionary<int, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object>>();
            _mHandleEventQueue = new Queue<Event>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.EventManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            if (_mHandleEventQueue != null)
            {
                lock (_mHandleEventQueue)
                {
                    if (_mHandleEventQueue != null && _mHandleEventQueue.Count > 0)
                    {
                        while (_mHandleEventQueue.Count > 0)
                        {
                            Event e = _mHandleEventQueue.Dequeue();
                            InternalHandleEvent(e.Sender, e.EventArgs, e.Args);
                            InternalReferencePool.ReleaseReference(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            lock (_mEvents)
            {
                _mHandleEventQueue.Clear();
                _mEvents.Clear();
            }
        }

        /// <summary>
        /// 获取指定框架事件的数量。
        /// </summary>
        /// <param name="id">要获取的框架事件编号。</param>
        /// <returns>指定框架事件的数量。</returns>
        public int EventCount(int id)
        {
            if (_mEvents.TryGetValue(id, out QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandlers))
            {
                return eventHandlers.GetInvocationList().Length;
            }

            return 0;
        }

        /// <summary>
        /// 检查是否存在指定框架事件。
        /// </summary>
        /// <param name="id">要检查的框架事件编号。</param>
        /// <param name="eventHandler">要检查的框架事件处理函数。</param>
        /// <returns>是否存在指定框架事件处理函数。</returns>
        public bool CheckEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new QsPlusFrameworkException("[要检查的框架事件处理函数是无效的 -> null]");
            }

            if (_mEvents.TryGetValue(id, out QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandlers))
            {
                if (eventHandlers == null || eventHandlers.GetInvocationList().Length <= 0)
                {
                    return false;
                }

                foreach (Delegate item in eventHandlers.GetInvocationList())
                {
                    if (item.Equals(eventHandler))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 订阅框架事件。
        /// </summary>
        /// <param name="id">要订阅的框架事件编号。</param>
        /// <param name="eventHandler">要订阅的框架事件处理函数。</param>
        public void SubscribeEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new QsPlusFrameworkException("[要订阅的框架事件处理函数是无效的 -> null]");
            }

            if (_mEvents.TryGetValue(id, out QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandlers))
            {
                eventHandlers += eventHandler;
                _mEvents[id] = eventHandlers;
            }
            else
            {
                _mEvents.Add(id, eventHandler);
            }
        }

        /// <summary>
        /// 取消订阅框架事件。
        /// </summary>
        /// <param name="id">要取消订阅的框架事件编号。</param>
        /// <param name="eventHandler">要取消订阅的框架事件处理函数。</param>
        public void UnSubscribeEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new QsPlusFrameworkException("[要取消订阅的框架事件处理函数是无效的 -> null]");
            }

            if (_mEvents.TryGetValue(id, out QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandlers))
            {
                if (eventHandlers == null)
                {
                    _mEvents.Remove(id);
                }
                else
                {
                    eventHandlers -= eventHandler;
                    _mEvents[id] = eventHandlers;
                }
            }
        }

        /// <summary>
        /// 广播框架事件 - 线程安全模式。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        public void BroadcastEvent(object sender, QsPlusFrameworkEventArgs eventArgs, object args)
        {
            if (eventArgs == null)
            {
                throw new QsPlusFrameworkException("[框架事件参数是无效的 -> null]");
            }

            lock (_mHandleEventQueue)
            {
                Event e = Event.Create(sender, eventArgs, args);
                _mHandleEventQueue.Enqueue(e);
            }
        }

        /// <summary>
        /// 广播框架事件 - 立即广播模式。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        public void BroadcastEventNow(object sender, QsPlusFrameworkEventArgs eventArgs, object args)
        {
            if (eventArgs == null)
            {
                throw new QsPlusFrameworkException("[框架事件参数是无效的 -> null]");
            }

            InternalHandleEvent(sender, eventArgs, args);
        }

        /// <summary>
        /// 内部处理框架事件函数。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        private void InternalHandleEvent(object sender, QsPlusFrameworkEventArgs eventArgs, object args)
        {
            if (_mEvents.TryGetValue(eventArgs.EventArgsId, out QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> e))
            {
                e?.Invoke(sender, eventArgs, args);
            }
            else
            {
                throw new QsPlusFrameworkException($"[此事件未订阅 -> sender : {sender} -> eventArgs : {eventArgs} -> args : {args}]");
            }
        }
    }
}