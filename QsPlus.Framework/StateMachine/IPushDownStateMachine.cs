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
    /// 下推状态机接口。
    /// </summary>
    /// <typeparam name="TPushDownStateMachineOwner">下推状态机持有者类型。</typeparam>
    public interface IPushDownStateMachine<TPushDownStateMachineOwner> where TPushDownStateMachineOwner : class
    {
        /// <summary>
        /// 获取下推状态机持有者。
        /// </summary>
        TPushDownStateMachineOwner PushDownStateMachineOwner { get; }
        
        /// <summary>
        /// 获取下推状态机当前临时位状态。
        /// </summary>
        IPushDownStateMachineState<TPushDownStateMachineOwner> PushDownStateMachineCurrentState { get; }

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        Type StateMachineOwnerType { get; }
        
        /// <summary>
        /// 获取当前临时位状态名称。
        /// </summary>
        string CurrentStateName { get; }

        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        int StateMachineStateCount { get; }
        
        /// <summary>
        /// 获取状态机中栈区状态的数量。
        /// </summary>
        int StateMachineStackStateCount { get; }

        /// <summary>
        /// 获取状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取状态机是否被清理。
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// 启动下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要启动的下推状态机持有者状态类型。</typeparam>
        void StartPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>;

        /// <summary>
        /// 检查是否存在下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要检查的下推状态机持有者状态类型。</typeparam>
        /// <returns>是否存在下推状态机状态。</returns>
        bool HasPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>;

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要获取的下推状态机持有者状态类型。</typeparam>
        /// <returns>获取到的下推状态机状态。</returns>
        TPushDownStateMachineOwnerState GetPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>;

        /// <summary>
        /// 获取下推状态机的所有状态。
        /// </summary>
        /// <returns>获取到的下推状态机所有状态。</returns>
        IPushDownStateMachineState<TPushDownStateMachineOwner>[] GetPushDownStateMachineStates();

        /// <summary>
        /// 下推下推状态机状态到栈区。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要下推的下推状态机持有者状态类型。</typeparam>
        void PushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>;

        /// <summary>
        /// 弹出下推状态机状态到临时位。
        /// </summary>
        void PopUpStateMachineState();
    }
}