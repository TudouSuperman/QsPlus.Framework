//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Event
{
    /// <summary>
    /// 框架事件类。
    /// </summary>
    internal sealed class Event : IReference
    {
        /// <summary>
        /// 事件发送者。
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// 框架事件参数。
        /// </summary>
        public GameEventArgs EventArgs { get; private set; }

        /// <summary>
        /// 奇怪的参数。
        /// </summary>
        public object Args { get; private set; }

        /// <summary>
        /// 初始化框架事件类的新实例。
        /// </summary>
        public Event()
        {
            Sender = null;
            EventArgs = null;
            Args = null;
        }

        /// <summary>
        /// 创建框架事件类的新实例。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="eventArgs">框架事件参数。</param>
        /// <param name="args">奇怪的参数。</param>
        /// <returns>创建的框架事件类。</returns>
        public static Event Create(object sender, GameEventArgs eventArgs, object args)
        {
            Event e = InternalReferencePool.AcquireReference<Event>();
            e.Sender = sender;
            e.EventArgs = eventArgs;
            e.Args = args;
            return e;
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public void ClearReference()
        {
            Sender = null;
            EventArgs = null;
            Args = null;
        }
    }
}