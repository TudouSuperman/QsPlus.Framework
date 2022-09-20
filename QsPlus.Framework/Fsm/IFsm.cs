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
    /// 有限状态机接口。
    /// </summary>
    /// <typeparam name="TFsmOwner">有限状态机持有者类型。</typeparam>
    public interface IFsm<TFsmOwner> where TFsmOwner : class
    {
        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        TFsmOwner Owner { get; }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        int FsmStateCount { get; }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        FsmStateBase<TFsmOwner> CurrentState { get; }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要开始的有限状态机状态类型。</typeparam>
        void Start<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>;

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <param name="stateType">要开始的有限状态机状态类型。</param>
        void Start(Type stateType);

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        bool HasState<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>;

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的有限状态机状态类型。</param>
        /// <returns>是否存在有限状态机状态。</returns>
        bool HasState(Type stateType);

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        TFsmOwnerState GetState<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>;

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        FsmStateBase<TFsmOwner> GetState(Type stateType);

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        FsmStateBase<TFsmOwner>[] GetAllStates();
    }
}