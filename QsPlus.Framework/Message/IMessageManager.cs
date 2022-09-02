//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Common;

namespace QsPlus.Framework.Message
{
    /// <summary>
    /// 消息管理器接口。
    /// </summary>
    public interface IMessageManager
    {
        /// <summary>
        /// 获取指定框架消息的数量。
        /// </summary>
        /// <param name="id">要获取的框架消息编号。</param>
        /// <returns>指定框架消息的数量。</returns>
        int MessageCount(int id);

        /// <summary>
        /// 检查是否存在指定框架消息。
        /// </summary>
        /// <param name="id">要检查的框架消息编号。</param>
        /// <param name="message">要检查的框架消息。</param>
        /// <returns>是否存在指定框架消息。</returns>
        bool CheckMessage(int id, QsPlusFrameworkAction<Message> message);

        /// <summary>
        /// 订阅框架消息。
        /// </summary>
        /// <param name="id">要订阅的框架消息编号。</param>
        /// <param name="message">要订阅的框架消息。</param>
        void SubscribeMessage(int id, QsPlusFrameworkAction<Message> message);

        /// <summary>
        /// 取消订阅框架消息。
        /// </summary>
        /// <param name="id">要取消订阅的消息编号。</param>
        /// <param name="message">要取消订阅的消息。</param>
        void UnSubscribeMessage(int id, QsPlusFrameworkAction<Message> message);

        /// <summary>
        /// 发送框架消息 - 线程安全模式。
        /// </summary>
        /// <param name="id">要发送消息的编号。</param>
        /// <param name="message">要发送的信息。</param>
        void SendMessage(int id, Message message);

        /// <summary>
        /// 发送框架消息 - 立即发送模式。
        /// </summary>
        /// <param name="id">要发送消息的编号。</param>
        /// <param name="message">要发送的信息。</param>
        void SendMessageNow(int id, Message message);
    }
}