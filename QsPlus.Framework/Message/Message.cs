//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Message
{
    /// <summary>
    /// 框架消息类。
    /// </summary>
    public sealed class Message : IReference
    {
        /// <summary>
        /// 用户消息编号。
        /// </summary>
        public int MessageId { get; private set; }

        /// <summary>
        /// 用户消息信息。
        /// </summary>
        public object UserMessageInfo { get; private set; }

        /// <summary>
        /// 初始化框架消息类的新实例。
        /// </summary>
        public Message()
        {
            MessageId = 0;
            UserMessageInfo = null;
        }

        /// <summary>
        /// 创建框架消息类的新实例。
        /// </summary>
        /// <param name="messageId">消息编号。</param>
        /// <param name="messageInfo">消息信息。</param>
        /// <returns>创建的框架消息类。</returns>
        public static Message Create(int messageId, object messageInfo)
        {
            Message msg = (Message) InternalReferencePool.AcquireReference(typeof(Message));
            msg.MessageId = messageId;
            msg.UserMessageInfo = messageInfo;
            return msg;
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public void ClearReference()
        {
            MessageId = 0;
            UserMessageInfo = null;
        }
    }
}