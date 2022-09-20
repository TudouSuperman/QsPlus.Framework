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

namespace QsPlus.Framework.Fsm
{
    /// <summary>
    /// 下推状态机类。
    /// </summary>
    /// <typeparam name="TPushDownFsmOwner">下推状态机持有者类型。</typeparam>
    internal sealed class PushDownFsm<TPushDownFsmOwner> : FsmBase, IReference, IPushDownFsm<TPushDownFsmOwner> where TPushDownFsmOwner : class
    {
        private TPushDownFsmOwner _mCurrentOwner;
        private PushDownFsmStateBase<TPushDownFsmOwner> _mCurrentState;
        private readonly Stack<PushDownFsmStateBase<TPushDownFsmOwner>> _mPushDownFsmStack;
        private readonly Dictionary<Type, PushDownFsmStateBase<TPushDownFsmOwner>> _mPushDownFsmStates;
        private bool _mIsCleared;

        /// <summary>
        /// 初始化下推状态机类的新实例。
        /// </summary>
        public PushDownFsm()
        {
            _mCurrentOwner = null;
            _mCurrentState = null;
            _mPushDownFsmStack = new Stack<PushDownFsmStateBase<TPushDownFsmOwner>>();
            _mPushDownFsmStates = new Dictionary<Type, PushDownFsmStateBase<TPushDownFsmOwner>>();
            _mIsCleared = true;
        }

        /// <summary>
        /// 下推状态机轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public override void FsmUpdate(float logicTime, float actualTime)
        {
            if (_mCurrentState == null)
            {
                return;
            }

            _mCurrentState.OnUpdate(this, logicTime, actualTime);
        }

        /// <summary>
        /// 下推状态机关闭。
        /// </summary>
        public override void FsmShutdown()
        {
            InternalReferencePool.ReleaseReference(this);
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public void ClearReference()
        {
            if (_mCurrentState != null)
            {
                _mCurrentState.OnLeave(this);
            }

            foreach (KeyValuePair<Type, PushDownFsmStateBase<TPushDownFsmOwner>> state in _mPushDownFsmStates)
            {
                state.Value.OnClear(this);
            }

            _mCurrentOwner = null;
            _mCurrentState = null;
            _mPushDownFsmStack.Clear();
            _mPushDownFsmStates.Clear();
            _mIsCleared = true;
        }


        /// <summary>
        /// 获取下推状态机持有者。
        /// </summary>
        public TPushDownFsmOwner Owner => _mCurrentOwner;

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        public override Type OwnerType => typeof(TPushDownFsmOwner);

        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        public override int FsmStateCount => _mPushDownFsmStates.Count;

        /// <summary>
        /// 获取下推状态机中栈区状态的数量。
        /// </summary>
        public int FsmStackStateCount => _mPushDownFsmStack.Count;

        /// <summary>
        /// 获取当前临时位状态机状态名称。
        /// </summary>
        public override string CurrentStateName => _mCurrentState.GetType().FullName;

        /// <summary>
        /// 获取当前临时位下推状态机状态。
        /// </summary>
        public PushDownFsmStateBase<TPushDownFsmOwner> CurrentState => _mCurrentState;

        /// <summary>
        /// 获取下推状态机是否正在运行。
        /// </summary>
        public override bool IsRunning => CurrentState != null;

        /// <summary>
        /// 获取下推状态机是否被销毁。
        /// </summary>
        public override bool IsCleared => _mIsCleared;

        /// <summary>
        /// 开始下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要开始的下推状态机状态类型。</typeparam>
        public void Start<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>
        {
            if (IsRunning)
            {
                throw new QsPlusFrameworkException("状态正在运行无法再次启动。");
            }

            PushDownFsmStateBase<TPushDownFsmOwner> state = GetState(typeof(TPushDownFsmOwnerState)) ?? throw new QsPlusFrameworkException($"状态机 '{typeof(TPushDownFsmOwner).FullName}' 进入状态 '{typeof(TPushDownFsmOwnerState).FullName}' 不存在。");
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }

        /// <summary>
        /// 开始下推状态机。
        /// </summary>
        /// <param name="stateType">要开始的下推状态机状态类型。</param>
        public void Start(Type stateType)
        {
            if (IsRunning)
            {
                throw new QsPlusFrameworkException("状态正在运行无法再次启动。");
            }

            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(PushDownFsmStateBase<TPushDownFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            PushDownFsmStateBase<TPushDownFsmOwner> state = GetState(stateType) ?? throw new QsPlusFrameworkException($"状态机 '{typeof(TPushDownFsmOwner).FullName}' 进入状态 '{stateType.FullName}' 不存在。");
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }

        /// <summary>
        /// 是否存在下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要检查的下推状态机状态类型。</typeparam>
        /// <returns>是否存在下推状态机状态。</returns>
        public bool HasState<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>
        {
            return _mPushDownFsmStates.ContainsKey(typeof(TPushDownFsmOwnerState));
        }

        /// <summary>
        /// 是否存在下推状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的下推状态机状态类型。</param>
        /// <returns>是否存在下推状态机状态。</returns>
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(FsmStateBase<TPushDownFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            return _mPushDownFsmStates.ContainsKey(stateType);
        }

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要获取的下推状态机状态类型。</typeparam>
        /// <returns>要获取的下推状态机状态。</returns>
        public TPushDownFsmOwnerState GetState<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>
        {
            if (_mPushDownFsmStates.TryGetValue(typeof(TPushDownFsmOwnerState), out PushDownFsmStateBase<TPushDownFsmOwner> state))
            {
                return (TPushDownFsmOwnerState) state;
            }

            return null;
        }

        /// <summary>
        /// 获取下推状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的下推状态机状态类型。</param>
        /// <returns>要获取的下推状态机状态。</returns>
        public PushDownFsmStateBase<TPushDownFsmOwner> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(PushDownFsmStateBase<TPushDownFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            if (_mPushDownFsmStates.TryGetValue(stateType, out PushDownFsmStateBase<TPushDownFsmOwner> state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取下推状态机的所有状态。
        /// </summary>
        /// <returns>下推状态机的所有状态。</returns>
        public PushDownFsmStateBase<TPushDownFsmOwner>[] GetAllStates()
        {
            int index = 0;
            PushDownFsmStateBase<TPushDownFsmOwner>[] results = new PushDownFsmStateBase<TPushDownFsmOwner>[_mPushDownFsmStates.Count];
            foreach (KeyValuePair<Type, PushDownFsmStateBase<TPushDownFsmOwner>> state in _mPushDownFsmStates)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <param name="owner">下推状态机持有者。</param>
        /// <param name="states">下推状态机状态集合。</param>
        /// <returns>创建的下推状态机。</returns>
        public static PushDownFsm<TPushDownFsmOwner> Create(TPushDownFsmOwner owner, params PushDownFsmStateBase<TPushDownFsmOwner>[] states)
        {
            if (owner == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            if (states == null || states.Length < 1)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            PushDownFsm<TPushDownFsmOwner> fsm = InternalReferencePool.AcquireReference<PushDownFsm<TPushDownFsmOwner>>();
            fsm._mCurrentOwner = owner;
            fsm._mIsCleared = false;
            foreach (PushDownFsmStateBase<TPushDownFsmOwner> state in states)
            {
                if (state == null)
                {
                    throw new QsPlusFrameworkException("状态机状态是无效的。");
                }

                Type stateType = state.GetType();
                if (fsm._mPushDownFsmStates.ContainsKey(stateType))
                {
                    throw new QsPlusFrameworkException($"状态机 '{typeof(TPushDownFsmOwner).FullName}' 里状态 '{stateType.FullName}' 已存在。");
                }

                fsm._mPushDownFsmStates.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <param name="owner">下推状态机持有者。</param>
        /// <param name="states">下推状态机状态集合。</param>
        /// <returns>创建的下推状态机。</returns>
        public static PushDownFsm<TPushDownFsmOwner> Create(TPushDownFsmOwner owner, List<PushDownFsmStateBase<TPushDownFsmOwner>> states)
        {
            if (owner == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            if (states == null || states.Count < 1)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            PushDownFsm<TPushDownFsmOwner> fsm = InternalReferencePool.AcquireReference<PushDownFsm<TPushDownFsmOwner>>();
            fsm._mCurrentOwner = owner;
            fsm._mIsCleared = false;
            foreach (PushDownFsmStateBase<TPushDownFsmOwner> state in states)
            {
                if (state == null)
                {
                    throw new QsPlusFrameworkException("状态机状态是无效的。");
                }

                Type stateType = state.GetType();
                if (fsm._mPushDownFsmStates.ContainsKey(stateType))
                {
                    throw new QsPlusFrameworkException($"状态机 '{typeof(TPushDownFsmOwner).FullName}' 里状态 '{stateType.FullName}' 已存在。");
                }

                fsm._mPushDownFsmStates.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        /// <summary>
        /// 下推当前临时位的下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsmOwnerState">要压入的下推状态机状态类型。</typeparam>
        internal void PushDownState<TPushDownFsmOwnerState>() where TPushDownFsmOwnerState : PushDownFsmStateBase<TPushDownFsmOwner>
        {
            PushDownState(typeof(TPushDownFsmOwnerState));
        }

        /// <summary>
        /// 下推当前临时位的下推状态机。
        /// </summary>
        /// <param name="stateType">要压入的下推状态机状态类型。</param>
        internal void PushDownState(Type stateType)
        {
            if (_mCurrentState == null)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            PushDownFsmStateBase<TPushDownFsmOwner> state = GetState(stateType);
            if (state == null)
            {
                throw new QsPlusFrameworkException($"状态机 '{typeof(TPushDownFsmOwner).FullName}' 进入状态 '{stateType.FullName}' 不存在。");
            }

            _mCurrentState.OnPause(this);
            _mPushDownFsmStack.Push(_mCurrentState);
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }

        /// <summary>
        /// 上移当前栈区栈顶的下推状态机到临时位。
        /// </summary>
        internal void PopUpState()
        {
            PushDownFsmStateBase<TPushDownFsmOwner> state = _mPushDownFsmStack.Pop();
            while (state == null && _mPushDownFsmStack.Count > 0)
            {
                state = _mPushDownFsmStack.Pop();
            }

            if (state != null)
            {
                _mCurrentState.OnLeave(this);
                _mCurrentState = state;
                _mCurrentState.OnResume(this);
            }
        }
    }
}