//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Common;

namespace QsPlus.Framework.Event
{
    /// <summary>
    /// 事件管理器接口。
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// 获取指定框架事件的数量。
        /// </summary>
        /// <param name="id">要获取的框架事件编号。</param>
        /// <returns>指定框架事件的数量。</returns>
        int EventCount(int id);

        /// <summary>
        /// 检查是否存在指定框架事件。
        /// </summary>
        /// <param name="id">要检查的框架事件编号。</param>
        /// <param name="eventHandler">要检查的框架事件处理函数。</param>
        /// <returns>是否存在指定框架事件处理函数。</returns>
        bool CheckEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler);

        /// <summary>
        /// 订阅框架事件。
        /// </summary>
        /// <param name="id">要订阅的框架事件编号。</param>
        /// <param name="eventHandler">要订阅的框架事件处理函数。</param>
        void SubscribeEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler);

        /// <summary>
        /// 取消订阅框架事件。
        /// </summary>
        /// <param name="id">要取消订阅的框架事件编号。</param>
        /// <param name="eventHandler">要取消订阅的框架事件处理函数。</param>
        void UnSubscribeEvent(int id, QsPlusFrameworkEventHandler<QsPlusFrameworkEventArgs, object> eventHandler);

        /// <summary>
        /// 广播框架事件 - 线程安全模式。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        void BroadcastEvent(object sender, QsPlusFrameworkEventArgs eventArgs, object args);

        /// <summary>
        /// 广播框架事件 - 立即广播模式。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        void BroadcastEventNow(object sender, QsPlusFrameworkEventArgs eventArgs, object args);
    }
}