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
    /// 下推状态机类。
    /// </summary>
    /// <typeparam name="TPushDownStateMachineOwner">下推状态机持有者类型。</typeparam>
    internal sealed class PushDownStateMachine<TPushDownStateMachineOwner> : IStateMachineModule, IReference, IPushDownStateMachine<TPushDownStateMachineOwner> where TPushDownStateMachineOwner : class
    {
        private TPushDownStateMachineOwner _pushDownStateMachineOwner;
        private IPushDownStateMachineState<TPushDownStateMachineOwner> _pushDownStateMachineCurrentState;
        private readonly IDictionary<Type, IPushDownStateMachineState<TPushDownStateMachineOwner>> _pushDownStateMachineStates;
        private readonly Stack<IPushDownStateMachineState<TPushDownStateMachineOwner>> _pushDownStateMachineStack;
        private bool _isCleared;

        /// <summary>
        /// 获取下推状态机持有者。
        /// </summary>
        public TPushDownStateMachineOwner GetPushDownStateMachineOwner => _pushDownStateMachineOwner;

        /// <summary>
        /// 获取下推状态机当前临时位状态。
        /// </summary>
        public IPushDownStateMachineState<TPushDownStateMachineOwner> GetPushDownStateMachineCurrentState => _pushDownStateMachineCurrentState;

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        public Type GetStateMachineOwnerType => typeof(TPushDownStateMachineOwner);

        /// <summary>
        /// 获取当前临时位状态机状态名称。
        /// </summary>
        public string GetCurrentStateMachineStateName => _pushDownStateMachineCurrentState.GetType().FullName;

        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        public int GetStateMachineStateCount => _pushDownStateMachineStates.Count;

        /// <summary>
        /// 获取状态机中栈区状态的数量。
        /// </summary>
        public int GetStateMachineStackStateCount => _pushDownStateMachineStack.Count;

        /// <summary>
        /// 获取状态机是否正在运行。
        /// </summary>
        public bool IsRunning => _pushDownStateMachineCurrentState != null;

        /// <summary>
        /// 获取状态机是否被清理。
        /// </summary>
        public bool IsCleared => _isCleared;

        /// <summary>
        /// 初始化下推状态机类的新实例。
        /// </summary>
        public PushDownStateMachine()
        {
            _pushDownStateMachineOwner = null;
            _pushDownStateMachineCurrentState = null;
            _pushDownStateMachineStates = new Dictionary<Type, IPushDownStateMachineState<TPushDownStateMachineOwner>>();
            _pushDownStateMachineStack = new Stack<IPushDownStateMachineState<TPushDownStateMachineOwner>>();
            _isCleared = true;
        }

        /// <summary>
        /// 状态机模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void StateMachineModuleUpdate(float logicTime, float actualTime)
        {
            if (_pushDownStateMachineCurrentState == null)
            {
                return;
            }

            _pushDownStateMachineCurrentState.OnUpdateState(this, logicTime, actualTime);
        }

        /// <summary>
        /// 状态机模块关闭。
        /// </summary>
        public void StateMachineModuleShutdown()
        {
            InternalReferencePool.ReleaseReference(this);
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public void ClearReference()
        {
            _pushDownStateMachineStack.Clear();

            if (_pushDownStateMachineCurrentState != null)
            {
                _pushDownStateMachineCurrentState.OnLeaveState(this);
            }

            foreach (KeyValuePair<Type, IPushDownStateMachineState<TPushDownStateMachineOwner>> pushDownStateMachineStateItem in _pushDownStateMachineStates)
            {
                pushDownStateMachineStateItem.Value.OnLeaveState(this);
            }

            _pushDownStateMachineOwner = null;
            _pushDownStateMachineCurrentState = null;
            _pushDownStateMachineStates.Clear();
            _isCleared = true;
        }

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <param name="pushDownStateMachineOwner">下推状态机持有者。</param>
        /// <param name="pushDownStateMachineStates">下推状态机状态集合。</param>
        /// <returns>创建的下推状态机。</returns>
        public static PushDownStateMachine<TPushDownStateMachineOwner> CreatePushDownStateMachine(TPushDownStateMachineOwner pushDownStateMachineOwner, HashSet<IPushDownStateMachineState<TPushDownStateMachineOwner>> pushDownStateMachineStates)
        {
            if (pushDownStateMachineOwner == null)
            {
                throw new QsPlusFrameworkException("类型为空的下推状态机持有者是无效的。");
            }

            if (pushDownStateMachineStates == null || pushDownStateMachineStates.Count < 1)
            {
                throw new QsPlusFrameworkException("类型为空的下推状态机状态集合是无效的。");
            }

            PushDownStateMachine<TPushDownStateMachineOwner> tempPushDownStateMachine = InternalReferencePool.AcquireReference<PushDownStateMachine<TPushDownStateMachineOwner>>();
            tempPushDownStateMachine._pushDownStateMachineOwner = pushDownStateMachineOwner;
            tempPushDownStateMachine._isCleared = false;
            foreach (IPushDownStateMachineState<TPushDownStateMachineOwner> pushDownStateMachineStateItem in pushDownStateMachineStates)
            {
                if (pushDownStateMachineStateItem == null)
                {
                    throw new QsPlusFrameworkException("类型为空的下推状态机状态是无效的。");
                }

                tempPushDownStateMachine._pushDownStateMachineStates.Add(pushDownStateMachineStateItem.GetType(), pushDownStateMachineStateItem);
            }

            return tempPushDownStateMachine;
        }

        /// <summary>
        /// 启动下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要启动的下推状态机持有者状态类型。</typeparam>
        public void StartPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>
        {
            if (IsRunning)
            {
                throw new QsPlusFrameworkException("当前下推状态机正在运行无法再次启动。");
            }

            IPushDownStateMachineState<TPushDownStateMachineOwner> tempPushDownStateMachineState = GetPushDownStateMachineState<TPushDownStateMachineOwnerState>() ?? throw new QsPlusFrameworkException("不存在的要启动的下推状态机状态是无效的。");
            _pushDownStateMachineCurrentState = tempPushDownStateMachineState;
            _pushDownStateMachineCurrentState.OnEnterState(this);
        }

        /// <summary>
        /// 检查是否存在下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要检查的下推状态机持有者状态类型。</typeparam>
        /// <returns>是否存在下推状态机状态。</returns>
        public bool HasPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>
        {
            return _pushDownStateMachineStates.ContainsKey(typeof(TPushDownStateMachineOwnerState));
        }

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要获取的下推状态机持有者状态类型。</typeparam>
        /// <returns>获取到的下推状态机状态。</returns>
        public TPushDownStateMachineOwnerState GetPushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>
        {
            if (_pushDownStateMachineStates.TryGetValue(typeof(TPushDownStateMachineOwnerState), out IPushDownStateMachineState<TPushDownStateMachineOwner> pushDownStateMachineOwnerState))
            {
                return (TPushDownStateMachineOwnerState) pushDownStateMachineOwnerState;
            }

            return null;
        }

        /// <summary>
        /// 获取下推状态机的所有状态。
        /// </summary>
        /// <returns>获取到的下推状态机所有状态。</returns>
        public IPushDownStateMachineState<TPushDownStateMachineOwner>[] GetPushDownStateMachineStates()
        {
            int pointer = 0;
            IPushDownStateMachineState<TPushDownStateMachineOwner>[] tempStates = new IPushDownStateMachineState<TPushDownStateMachineOwner>[_pushDownStateMachineStates.Count];
            foreach (KeyValuePair<Type, IPushDownStateMachineState<TPushDownStateMachineOwner>> stateItem in _pushDownStateMachineStates)
            {
                tempStates[pointer++] = stateItem.Value;
            }

            return tempStates;
        }

        /// <summary>
        /// 下推下推状态机状态到栈区。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwnerState">要下推的下推状态机持有者状态类型。</typeparam>
        public void PushDownStateMachineState<TPushDownStateMachineOwnerState>() where TPushDownStateMachineOwnerState : class, IPushDownStateMachineState<TPushDownStateMachineOwner>
        {
            if (_pushDownStateMachineCurrentState == null)
            {
                throw new QsPlusFrameworkException("类型为空的当前临时位下推状态机状态是无效的。");
            }

            IPushDownStateMachineState<TPushDownStateMachineOwner> tempPushDownStateMachineState = GetPushDownStateMachineState<TPushDownStateMachineOwnerState>();
            if (tempPushDownStateMachineState == null)
            {
                throw new QsPlusFrameworkException("不存在的要下推的下推状态机状态是无效的。");
            }

            if (_pushDownStateMachineCurrentState.Equals(tempPushDownStateMachineState))
            {
                return;
            }

            if (_pushDownStateMachineStack.Contains(_pushDownStateMachineCurrentState))
            {
                return;
            }

            if (_pushDownStateMachineStack.Contains(tempPushDownStateMachineState))
            {
                return;
            }

            _pushDownStateMachineCurrentState.OnPauseState(this);
            _pushDownStateMachineStack.Push(_pushDownStateMachineCurrentState);
            _pushDownStateMachineCurrentState = tempPushDownStateMachineState;
            _pushDownStateMachineCurrentState.OnEnterState(this);
        }

        /// <summary>
        /// 弹出下推状态机状态到临时位。
        /// </summary>
        public void PopUpStateMachineState()
        {
            if (_pushDownStateMachineCurrentState == null)
            {
                throw new QsPlusFrameworkException("类型为空的当前临时位下推状态机状态是无效的。");
            }

            if (_pushDownStateMachineStack == null || _pushDownStateMachineStack.Count <= 0)
            {
                return;
            }

            IPushDownStateMachineState<TPushDownStateMachineOwner> tempPopUpState = _pushDownStateMachineStack.Pop();
            while (tempPopUpState == null && _pushDownStateMachineStack.Count > 0)
            {
                tempPopUpState = _pushDownStateMachineStack.Pop();
            }

            if (tempPopUpState == null)
            {
                return;
            }

            _pushDownStateMachineCurrentState.OnLeaveState(this);
            _pushDownStateMachineCurrentState = tempPopUpState;
            _pushDownStateMachineCurrentState.OnResumeState(this);
        }
    }
}