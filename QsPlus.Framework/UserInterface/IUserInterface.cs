//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.UserInterface
{
    /// <summary>
    /// 用户界面接口。
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// 用户界面编号。
        /// </summary>
        int UserInterfaceId { get; }

        /// <summary>
        /// 用户界面名称。
        /// </summary>
        int UserInterfaceName { get; }

        /// <summary>
        /// 用户界面实例。
        /// </summary>
        object UserInterfaceHandle { get; }

        /// <summary>
        /// 设置用户界面深度。
        /// </summary>
        /// <param name="depth">深度值。</param>
        void SetUserInterfaceDepth(int depth);
    }
}