//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Common;

namespace QsPlus.Framework.Event
{
    /// <summary>
    /// 游戏事件参数基类。
    /// </summary>
    public abstract class GameEventArgs : QsPlusFrameworkEventArgs
    {
        /// <summary>
        /// 事件参数编号。
        /// </summary>
        public abstract int EventArgsId { get; }

        /// <summary>
        /// 用户自定义数据。
        /// </summary>
        public abstract object UserData { get; set; }
    }
}