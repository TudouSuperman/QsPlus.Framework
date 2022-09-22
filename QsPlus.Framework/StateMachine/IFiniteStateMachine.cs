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
    /// 有限状态机接口。
    /// </summary>
    /// <typeparam name="TFiniteStateMachineOwner">有限状态机持有者类型。</typeparam>
    public interface IFiniteStateMachine<TFiniteStateMachineOwner> where TFiniteStateMachineOwner : class
    {
        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        TFiniteStateMachineOwner GetFiniteStateMachineOwner { get; }
        
        /// <summary>
        /// 获取有限状态机当前状态。
        /// </summary>
        IFiniteStateMachineState<TFiniteStateMachineOwner> GetFiniteStateMachineCurrentState { get; }

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        Type GetStateMachineOwnerType { get; }
        
        /// <summary>
        /// 获取当前状态名称。
        /// </summary>
        string GetCurrentStateName { get; }
        
        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        int GetStateMachineStateCount { get; }

        /// <summary>
        /// 获取状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取状态机是否被清理。
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// 启动有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要启动的有限状态机持有者状态类型。</typeparam>
        void StartFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>;

        /// <summary>
        /// 检查是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要检查的有限状态机持有者状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        bool HasFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>;

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要获取的有限状态机持有者状态类型。</typeparam>
        /// <returns>获取到的有限状态机状态。</returns>
        TFiniteStateMachineOwnerState GetFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>;

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>获取到的有限状态机所有状态。</returns>
        IFiniteStateMachineState<TFiniteStateMachineOwner>[] GetFiniteStateMachineStates();

        /// <summary>
        /// 切换有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要切换的有限状态机持有者状态类型。</typeparam>
        void ChangeFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>;
    }
}