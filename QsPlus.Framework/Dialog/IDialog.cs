//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Dialog
{
    /// <summary>
    /// 对话框接口。
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// 对话框编号。
        /// </summary>
        int DialogId { get; }
                                                                     
        /// <summary>
        /// 对话框名称。
        /// </summary>
        int DialogName { get; }

        /// <summary>
        /// 对话框实例。
        /// </summary>
        object DialogHandle { get; }

        /// <summary>
        /// 设置对话框深度。
        /// </summary>
        /// <param name="depth">深度值。</param>
        void SetDialogDepth(int depth);
    }
}