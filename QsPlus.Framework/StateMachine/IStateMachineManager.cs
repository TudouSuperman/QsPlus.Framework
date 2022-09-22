//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 状态机管理器接口。
    /// </summary>
    public interface IStateMachineManager
    {
        /// <summary>
        /// 获取所有有限状态机的数量。
        /// </summary>
        int GetFiniteStateMachineCount { get; }

        /// <summary>
        /// 获取所有下推状态机的数量。
        /// </summary>
        int GetPushDownStateMachineCount { get; }

        /// <summary>
        /// 检查是否存在有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要检查的有限状态机持有者类型。</typeparam>
        /// <returns>是否存在有限状态机。</returns>
        bool HasFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class;

        /// <summary>
        /// 检查是否存在下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要检查的下推状态机持有者类型。</typeparam>
        /// <returns>是否存在下推状态机。</returns>
        bool HasPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class;

        /// <summary>
        /// 获取有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要获取的有限状态机持有者类型。</typeparam>
        /// <returns>获取到的有限状态机。</returns>
        IFiniteStateMachine<TFiniteStateMachineOwner> GetFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class;

        /// <summary>
        /// 获取下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要获取的下推状态机持有者类型。</typeparam>
        /// <returns>获取到的下推状态机。</returns>
        IPushDownStateMachine<TPushDownStateMachineOwner> GetPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class;

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="finiteStateMachineOwner">有限状态机持有者。</param>
        /// <param name="finiteStateMachineStates">有限状态机状态集合。</param>
        /// <typeparam name="TFiniteStateMachineOwner">要创建的有限状态机持有者类型。</typeparam>
        /// <returns>创建的有限状态机。</returns>
        IFiniteStateMachine<TFiniteStateMachineOwner> CreateFiniteStateMachine<TFiniteStateMachineOwner>(TFiniteStateMachineOwner finiteStateMachineOwner, HashSet<IFiniteStateMachineState<TFiniteStateMachineOwner>> finiteStateMachineStates) where TFiniteStateMachineOwner : class;

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <param name="pushDownStateMachineOwner">下推状态机持有者。</param>
        /// <param name="pushDownStateMachineStates">下推状态机状态集合。</param>
        /// <typeparam name="TPushDownStateMachineOwner">要创建的下推状态机持有者类型。</typeparam>
        /// <returns>创建的下推状态机。</returns>
        IPushDownStateMachine<TPushDownStateMachineOwner> CreatePushDownStateMachine<TPushDownStateMachineOwner>(TPushDownStateMachineOwner pushDownStateMachineOwner, HashSet<IPushDownStateMachineState<TPushDownStateMachineOwner>> pushDownStateMachineStates) where TPushDownStateMachineOwner : class;

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要销毁的有限状态机持有者类型。</typeparam>
        /// <returns>是否销毁有限状态机成功。</returns>
        bool DestroyFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class;

        /// <summary>
        /// 销毁下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要销毁的下推状态机持有者类型。</typeparam>
        /// <returns>是否销毁下推状态机成功。</returns>
        bool DestroyPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class;
    }
}