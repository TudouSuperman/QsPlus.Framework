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
    /// 下推状态机接口。
    /// </summary>
    /// <typeparam name="TPushDownFsmOwner">下推状态机持有者类型。</typeparam>
    public interface IPushDownFsm<TPushDownFsmOwner> where TPushDownFsmOwner : class
    {
        /// <summary>
        /// 获取下推状态机持有者。
        /// </summary>
        TPushDownFsmOwner Owner { get; }

        /// <summary>
        /// 获取下推状态机中状态的数量。
        /// </summary>
        int FsmStateCount { get; }

        /// <summary>
        /// 获取当前下推状态机状态名称。
        /// </summary>
        string CurrentStateName { get; }

        /// <summary>
        /// 获取下推状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取下推状态机是否被销毁。
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// 开始下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要开始的下推状态机状态类型。</typeparam>
        void Start<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>;

        /// <summary>
        /// 开始下推状态机。
        /// </summary>
        /// <param name="stateType">要开始的下推状态机状态类型。</param>
        void Start(Type stateType);

        /// <summary>
        /// 是否存在下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要检查的下推状态机状态类型。</typeparam>
        /// <returns>是否存在下推状态机状态。</returns>
        bool HasState<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>;

        /// <summary>
        /// 是否存在下推状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的下推状态机状态类型。</param>
        /// <returns>是否存在下推状态机状态。</returns>
        bool HasState(Type stateType);

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要获取的下推状态机状态类型。</typeparam>
        /// <returns>要获取的下推状态机状态。</returns>
        TPushDownFsmOwnerState GetState<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>;

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的下推状态机状态类型。</param>
        /// <returns>要获取的下推状态机状态。</returns>
        PushDownFsmStateBase<TPushDownFsmOwner> GetState(Type stateType);

        /// <summary>
        /// 获取下推状态机的所有状态。
        /// </summary>
        /// <returns>下推状态机的所有状态。</returns>
        PushDownFsmStateBase<TPushDownFsmOwner>[] GetAllStates();
    }
}