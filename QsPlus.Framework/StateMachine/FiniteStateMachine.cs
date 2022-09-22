//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 有限状态机类。
    /// </summary>
    /// <typeparam name="TFiniteStateMachineOwner">有限状态机持有者类型。</typeparam>
    internal sealed class FiniteStateMachine<TFiniteStateMachineOwner> : IStateMachineModule, IReference, IFiniteStateMachine<TFiniteStateMachineOwner> where TFiniteStateMachineOwner : class
    {
        private TFiniteStateMachineOwner _finiteStateMachineOwner;
        private IFiniteStateMachineState<TFiniteStateMachineOwner> _finiteStateMachineCurrentState;
        private readonly IDictionary<Type, IFiniteStateMachineState<TFiniteStateMachineOwner>> _finiteStateMachineStates;
        private bool _isCleared;

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        public TFiniteStateMachineOwner GetFiniteStateMachineOwner => _finiteStateMachineOwner;

        /// <summary>
        /// 获取有限状态机当前状态。
        /// </summary>
        public IFiniteStateMachineState<TFiniteStateMachineOwner> GetFiniteStateMachineCurrentState => _finiteStateMachineCurrentState;

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        public Type GetStateMachineOwnerType => typeof(TFiniteStateMachineOwner);

        /// <summary>
        /// 获取当前状态名称。
        /// </summary>
        public string GetCurrentStateName => _finiteStateMachineCurrentState.GetType().FullName;
        
        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        public int GetStateMachineStateCount => _finiteStateMachineStates.Count;

        /// <summary>
        /// 获取状态机是否正在运行。
        /// </summary>
        public bool IsRunning => _finiteStateMachineCurrentState != null;

        /// <summary>
        /// 获取状态机是否被清理。
        /// </summary>
        public bool IsCleared => _isCleared;

        /// <summary>
        /// 初始化有限状态机类的新实例。
        /// </summary>
        public FiniteStateMachine()
        {
            _finiteStateMachineOwner = null;
            _finiteStateMachineCurrentState = null;
            _finiteStateMachineStates = new Dictionary<Type, IFiniteStateMachineState<TFiniteStateMachineOwner>>();
            _isCleared = true;
        }

        /// <summary>
        /// 状态机模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void StateMachineModuleUpdate(float logicTime, float actualTime)
        {
            if (_finiteStateMachineCurrentState == null)
            {
                return;
            }

            _finiteStateMachineCurrentState.OnUpdateState(this, logicTime, actualTime);
        }

        /// <summary>
        /// 状态机模块关闭。
        /// </summary>
        public void StateMachineModuleShutdown()
        {
            InternalReferencePool.ReleaseReference(this);
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public void ClearReference()
        {
            if (_finiteStateMachineCurrentState != null)
            {
                _finiteStateMachineCurrentState.OnLeaveState(this);
            }

            foreach (KeyValuePair<Type, IFiniteStateMachineState<TFiniteStateMachineOwner>> finiteStateMachineStateItem in _finiteStateMachineStates)
            {
                finiteStateMachineStateItem.Value.OnLeaveState(this);
            }

            _finiteStateMachineOwner = null;
            _finiteStateMachineCurrentState = null;
            _finiteStateMachineStates.Clear();
            _isCleared = true;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="finiteStateMachineOwner">有限状态机持有者。</param>
        /// <param name="finiteStateMachineStates">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static FiniteStateMachine<TFiniteStateMachineOwner> CreateFiniteStateMachine(TFiniteStateMachineOwner finiteStateMachineOwner, HashSet<IFiniteStateMachineState<TFiniteStateMachineOwner>> finiteStateMachineStates)
        {
            if (finiteStateMachineOwner == null)
            {
                throw new QsPlusFrameworkException("类型为空的有限状态机持有者是无效的。");
            }

            if (finiteStateMachineStates == null || finiteStateMachineStates.Count < 1)
            {
                throw new QsPlusFrameworkException("类型为空的有限状态机状态集合是无效的。");
            }

            FiniteStateMachine<TFiniteStateMachineOwner> tempFiniteStateMachine = InternalReferencePool.AcquireReference<FiniteStateMachine<TFiniteStateMachineOwner>>();
            tempFiniteStateMachine._finiteStateMachineOwner = finiteStateMachineOwner;
            tempFiniteStateMachine._isCleared = false;
            foreach (IFiniteStateMachineState<TFiniteStateMachineOwner> finiteStateMachineStateItem in finiteStateMachineStates)
            {
                if (finiteStateMachineStateItem == null)
                {
                    throw new QsPlusFrameworkException("类型为空的有限状态机状态是无效的。");
                }

                tempFiniteStateMachine._finiteStateMachineStates.Add(finiteStateMachineStateItem.GetType(), finiteStateMachineStateItem);
            }

            return tempFiniteStateMachine;
        }

        /// <summary>
        /// 启动有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要启动的有限状态机持有者状态类型。</typeparam>
        public void StartFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>
        {
            if (IsRunning)
            {
                throw new QsPlusFrameworkException("当前有限状态机正在运行无法再次启动。");
            }

            IFiniteStateMachineState<TFiniteStateMachineOwner> tempFiniteStateMachineState = GetFiniteStateMachineState<TFiniteStateMachineOwnerState>() ?? throw new QsPlusFrameworkException("不存在的要启动的有限状态机状态是无效的。");
            _finiteStateMachineCurrentState = tempFiniteStateMachineState;
            _finiteStateMachineCurrentState.OnEnterState(this);
        }

        /// <summary>
        /// 检查是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要检查的有限状态机持有者状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>
        {
            return _finiteStateMachineStates.ContainsKey(typeof(TFiniteStateMachineOwnerState));
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要获取的有限状态机持有者状态类型。</typeparam>
        /// <returns>获取到的有限状态机状态。</returns>
        public TFiniteStateMachineOwnerState GetFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>
        {
            if (_finiteStateMachineStates.TryGetValue(typeof(TFiniteStateMachineOwnerState), out IFiniteStateMachineState<TFiniteStateMachineOwner> finiteStateMachineOwnerState))
            {
                return (TFiniteStateMachineOwnerState) finiteStateMachineOwnerState;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>获取到的有限状态机所有状态。</returns>
        public IFiniteStateMachineState<TFiniteStateMachineOwner>[] GetFiniteStateMachineStates()
        {
            int pointer = 0;
            IFiniteStateMachineState<TFiniteStateMachineOwner>[] tempStates = new IFiniteStateMachineState<TFiniteStateMachineOwner>[_finiteStateMachineStates.Count];
            foreach (KeyValuePair<Type, IFiniteStateMachineState<TFiniteStateMachineOwner>> stateItem in _finiteStateMachineStates)
            {
                tempStates[pointer++] = stateItem.Value;
            }

            return tempStates;
        }

        /// <summary>
        /// 切换有限状态机状态。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwnerState">要切换的有限状态机持有者状态类型。</typeparam>
        /// <exception cref="QsPlusFrameworkException"></exception>
        public void ChangeFiniteStateMachineState<TFiniteStateMachineOwnerState>() where TFiniteStateMachineOwnerState : class, IFiniteStateMachineState<TFiniteStateMachineOwner>
        {
            if (_finiteStateMachineCurrentState == null)
            {
                throw new QsPlusFrameworkException("类型为空的当前有限状态机状态是无效的。");
            }

            IFiniteStateMachineState<TFiniteStateMachineOwner> tempFiniteStateMachineState = GetFiniteStateMachineState<TFiniteStateMachineOwnerState>();
            if (tempFiniteStateMachineState == null)
            {
                throw new QsPlusFrameworkException("不存在的要切换的有限状态机状态是无效的。");
            }

            _finiteStateMachineCurrentState.OnLeaveState(this);
            _finiteStateMachineCurrentState = tempFiniteStateMachineState;
            _finiteStateMachineCurrentState.OnEnterState(this);
        }
    }
}