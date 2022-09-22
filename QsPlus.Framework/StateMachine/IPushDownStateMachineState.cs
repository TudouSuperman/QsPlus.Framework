//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 下推状态机状态接口。
    /// </summary>
    /// <typeparam name="TPushDownStateMachineOwner">下推状态机持有者类型。</typeparam>
    public interface IPushDownStateMachineState<TPushDownStateMachineOwner> where TPushDownStateMachineOwner : class
    {
        /// <summary>
        /// 下推状态机进入状态。
        /// </summary>
        /// <param name="pushDownStateMachine">下推状态机持有者。</param>
        void OnEnterState(IPushDownStateMachine<TPushDownStateMachineOwner> pushDownStateMachine);

        /// <summary>
        /// 下推状态机下推状态。
        /// </summary>
        /// <param name="pushDownStateMachine">下推状态机持有者。</param>
        void OnPauseState(IPushDownStateMachine<TPushDownStateMachineOwner> pushDownStateMachine);

        /// <summary>
        /// 下推状态机轮询状态。
        /// </summary>
        /// <param name="pushDownStateMachine">下推状态机持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        void OnUpdateState(IPushDownStateMachine<TPushDownStateMachineOwner> pushDownStateMachine, float logicTime, float actualTime);

        /// <summary>
        /// 下推状态机回滚状态。
        /// </summary>
        /// <param name="pushDownStateMachine">下推状态机持有者。</param>
        void OnResumeState(IPushDownStateMachine<TPushDownStateMachineOwner> pushDownStateMachine);

        /// <summary>
        /// 下推状态机离开状态。
        /// </summary>
        /// <param name="pushDownStateMachine">下推状态机持有者。</param>
        void OnLeaveState(IPushDownStateMachine<TPushDownStateMachineOwner> pushDownStateMachine);
    }
}