//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 状态机模块接口。
    /// </summary>
    internal interface IStateMachineModule
    {
        /// <summary>
        /// 获取状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取状态机是否被清理。
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// 状态机模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        void StateMachineModuleUpdate(float logicTime, float actualTime);

        /// <summary>
        /// 状态机模块关闭。
        /// </summary>
        void StateMachineModuleShutdown();
    }
}