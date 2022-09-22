//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 框架模块优先级。
    /// </summary>
    internal enum QsPlusFrameworkModulePriority : byte
    {
        /// <summary>
        /// 引用管理器。
        /// </summary>
        ReferenceManager = 0,

        /// <summary>
        /// 事件管理器。
        /// </summary>
        EventManager,

        /// <summary>
        /// 消息管理器。
        /// </summary>
        MessageManager,

        /// <summary>
        /// 状态机管理器。
        /// </summary>
        FsmManager,

        /// <summary>
        /// 界面管理器。
        /// </summary>
        UiManager,
        
        /// <summary>
        /// 配置管理器。
        /// </summary>
        ConfigManager,
        
        /// <summary>
        /// 流程管理器。
        /// </summary>
        Procedure,
    }
}