//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 框架事件参数基类。
    /// </summary>
    public abstract class QsPlusFrameworkEventArgs : EventArgs, IReference
    {
        /// <summary>
        /// 初始化框架事件参数基类的新实例。
        /// </summary>
        protected QsPlusFrameworkEventArgs()
        {
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public abstract void ClearReference();
    }
}