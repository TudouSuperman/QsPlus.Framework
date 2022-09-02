//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 框架消息参数基类。
    /// </summary>
    public abstract class QsPlusFrameworkMessageArgs : EventArgs, IReference
    {
        /// <summary>
        /// 框架事件编号。
        /// </summary>
        public abstract int EventArgsId { get; }

        /// <summary>
        /// 用户自定义数据。
        /// </summary>
        public abstract object UserData { get; set; }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public abstract void ClearReference();
    }
}