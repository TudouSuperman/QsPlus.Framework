//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;

namespace QsPlus.Framework.Fsm
{
    /// <summary>
    /// 状态机基类。
    /// </summary>
    public abstract class FsmBase
    {
        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        public abstract Type OwnerType { get; }

        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        public abstract int FsmStateCount { get; }

        /// <summary>
        /// 获取当前状态机状态名称。
        /// </summary>
        public abstract string CurrentStateName { get; }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        public abstract bool IsRunning { get; }

        /// <summary>
        /// 获取有限状态机是否被清理。
        /// </summary>
        public abstract bool IsCleared { get; }

        /// <summary>
        /// 状态机轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public abstract void FsmUpdate(float logicTime, float actualTime);

        /// <summary>
        /// 状态机关闭。
        /// </summary>
        public abstract void FsmShutdown();
    }
}