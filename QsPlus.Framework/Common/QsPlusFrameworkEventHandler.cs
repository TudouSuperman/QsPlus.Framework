//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 事件处理者。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkEventArgs">事件参数类型。</typeparam>
    /// <param name="sender">事件发送者。</param>
    /// <param name="e">事件参数。</param>
    public delegate void QsPlusFrameworkEventHandler<in TQsPlusFrameworkEventArgs>(object sender, TQsPlusFrameworkEventArgs e) where TQsPlusFrameworkEventArgs : QsPlusFrameworkEventArgs;

    /// <summary>
    /// 事件处理者 - 允许附带一个奇怪的参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkEventArgs">事件参数类型。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs">奇怪的参数类型。</typeparam>
    /// <param name="sender">事件发送者。</param>
    /// <param name="e">事件参数。</param>
    /// <param name="args">奇怪的参数。</param>
    public delegate void QsPlusFrameworkEventHandler<in TQsPlusFrameworkEventArgs, in TQsPlusFrameworkArgs>(object sender, TQsPlusFrameworkEventArgs e, object args) where TQsPlusFrameworkEventArgs : QsPlusFrameworkEventArgs where TQsPlusFrameworkArgs : class, new();
}