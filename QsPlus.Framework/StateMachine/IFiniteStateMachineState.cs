//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 有限状态机状态接口。
    /// </summary>
    /// <typeparam name="TFiniteStateMachineOwner">有限状态机持有者类型。</typeparam>
    public interface IFiniteStateMachineState<TFiniteStateMachineOwner> where TFiniteStateMachineOwner : class
    {
        /// <summary>
        /// 有限状态机进入状态。
        /// </summary>
        /// <param name="finiteStateMachine">有限状态机持有者。</param>
        void OnEnterState(IFiniteStateMachine<TFiniteStateMachineOwner> finiteStateMachine);

        /// <summary>
        /// 有限状态机轮询状态。
        /// </summary>
        /// <param name="finiteStateMachine">有限状态机持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        void OnUpdateState(IFiniteStateMachine<TFiniteStateMachineOwner> finiteStateMachine, float logicTime, float actualTime);

        /// <summary>
        /// 有限状态机离开状态。
        /// </summary>
        /// <param name="finiteStateMachine">有限状态机持有者。</param>
        void OnLeaveState(IFiniteStateMachine<TFiniteStateMachineOwner> finiteStateMachine);
    }
}