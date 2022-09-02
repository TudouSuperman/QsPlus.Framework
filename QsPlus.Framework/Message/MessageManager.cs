//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Message
{
    /// <summary>
    /// 消息管理器类。
    /// </summary>
    internal sealed class MessageManager : IQsPlusFrameworkModule, IMessageManager
    {
        /// <summary>
        /// 消息缓存。
        /// </summary>
        private readonly IDictionary<int, QsPlusFrameworkAction<Message>> _mMessages;

        /// <summary>
        /// 框架消息处理队列。
        /// </summary>
        private readonly Queue<Message> _mHandleMessageQueue;

        /// <summary>
        /// 初始化消息管理器类的新实例。
        /// </summary>
        public MessageManager()
        {
            _mMessages = new Dictionary<int, QsPlusFrameworkAction<Message>>();
            _mHandleMessageQueue = new Queue<Message>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.MessageManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            if (_mHandleMessageQueue != null)
            {
                lock (_mHandleMessageQueue)
                {
                    if (_mHandleMessageQueue != null && _mHandleMessageQueue.Count > 0)
                    {
                        while (_mHandleMessageQueue.Count > 0)
                        {
                            Message msg = _mHandleMessageQueue.Dequeue();
                            InternalHandleEvent(msg.MessageId, (Message) msg.UserMessageInfo);
                            InternalReferencePool.ReleaseReference(msg);
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
            lock (_mMessages)
            {
                _mHandleMessageQueue.Clear();
                _mMessages.Clear();
            }
        }

        /// <summary>
        /// 获取指定框架消息的数量。
        /// </summary>
        /// <param name="id">要获取的框架消息编号。</param>
        /// <returns>指定框架消息的数量。</returns>
        public int MessageCount(int id)
        {
            if (_mMessages.ContainsKey(id))
            {
                return _mMessages[id].GetInvocationList().Length;
            }

            return 0;
        }

        /// <summary>
        /// 检查是否存在指定框架消息。
        /// </summary>
        /// <param name="id">要检查的框架消息编号。</param>
        /// <param name="message">要检查的框架消息。</param>
        /// <returns>是否存在指定框架消息。</returns>
        public bool CheckMessage(int id, QsPlusFrameworkAction<Message> message)
        {
            if (message == null)
            {
                throw new QsPlusFrameworkException("[要检查的框架消息是无效的 -> null]");
            }

            if (_mMessages.TryGetValue(id, out QsPlusFrameworkAction<Message> messages))
            {
                return messages.GetInvocationList().Contains(messages);
            }

            return false;
        }

        /// <summary>
        /// 订阅框架消息。
        /// </summary>
        /// <param name="id">要订阅的框架消息编号。</param>
        /// <param name="message">要订阅的框架消息。</param>
        public void SubscribeMessage(int id, QsPlusFrameworkAction<Message> message)
        {
            if (message == null)
            {
                throw new QsPlusFrameworkException("[要订阅的框架消息是无效的 -> null]");
            }

            if (_mMessages.ContainsKey(id))
            {
                _mMessages[id] += message;
            }
            else
            {
                _mMessages.Add(id, message);
            }
        }

        /// <summary>
        /// 取消订阅框架消息。
        /// </summary>
        /// <param name="id">要取消订阅的消息编号。</param>
        /// <param name="message">要取消订阅的消息。</param>
        public void UnSubscribeMessage(int id, QsPlusFrameworkAction<Message> message)
        {
            if (message == null)
            {
                throw new QsPlusFrameworkException("[要取消订阅的框架消息是无效的 -> null]");
            }

            if (_mMessages.ContainsKey(id))
            {
                _mMessages[id] -= message;
            }
            else
            {
                _mMessages.Remove(id);
            }
        }

        /// <summary>
        /// 发送框架消息 - 线程安全模式。
        /// </summary>
        /// <param name="id">要发送消息的编号。</param>
        /// <param name="message">要发送的信息。</param>
        public void SendMessage(int id, Message message)
        {
            if (message == null)
            {
                throw new QsPlusFrameworkException("[框架事件参数是无效的 -> null]");
            }

            lock (_mHandleMessageQueue)
            {
                Message msg = Message.Create(id, message);
                _mHandleMessageQueue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 发送框架消息 - 立即发送模式。
        /// </summary>
        /// <param name="id">要发送消息的编号。</param>
        /// <param name="message">要发送的信息。</param>
        public void SendMessageNow(int id, Message message)
        {
            if (message == null)
            {
                throw new QsPlusFrameworkException("[框架要发送的信息是无效的 -> null]");
            }

            InternalHandleEvent(id, message);
        }

        /// <summary>
        /// 内部处理框架事件函数。
        /// </summary>
        private void InternalHandleEvent(int id, Message message)
        {
            if (_mMessages.TryGetValue(id, out QsPlusFrameworkAction<Message> messages))
            {
                messages?.Invoke(message);
            }
            else
            {
                throw new QsPlusFrameworkException($"[此消息未订阅 -> id : {id} -> message : {(message == null ? "null" : message.ToString())}]");
            }
        }
    }
}