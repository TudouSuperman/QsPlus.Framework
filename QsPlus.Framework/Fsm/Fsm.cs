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
    /// 有限状态机类。
    /// </summary>
    /// <typeparam name="TFsmOwner">有限状态机持有者类型。</typeparam>
    internal sealed class Fsm<TFsmOwner> : FsmBase, IReference, IFsm<TFsmOwner> where TFsmOwner : class
    {
        private TFsmOwner _mCurrentOwner;
        private FsmStateBase<TFsmOwner> _mCurrentState;
        private readonly Dictionary<Type, FsmStateBase<TFsmOwner>> _mFsmStates;
        private bool _mIsCleared;

        /// <summary>
        /// 初始化有限状态机类的新实例。
        /// </summary>
        public Fsm()
        {
            _mCurrentOwner = null;
            _mCurrentState = null;
            _mFsmStates = new Dictionary<Type, FsmStateBase<TFsmOwner>>();
            _mIsCleared = true;
        }

        /// <summary>
        /// 有限状态机轮询。
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
        /// 有限状态机关闭。
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

            foreach (KeyValuePair<Type, FsmStateBase<TFsmOwner>> state in _mFsmStates)
            {
                state.Value.OnClear(this);
            }

            _mCurrentOwner = null;
            _mCurrentState = null;
            _mFsmStates.Clear();
            _mIsCleared = true;
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        public TFsmOwner Owner => _mCurrentOwner;

        /// <summary>
        /// 获取状态机持有者类型。
        /// </summary>
        public override Type OwnerType => typeof(TFsmOwner);

        /// <summary>
        /// 获取状态机中状态的数量。
        /// </summary>
        public override int FsmStateCount => _mFsmStates.Count;

        /// <summary>
        /// 获取当前状态机状态名称。
        /// </summary>
        public override string CurrentStateName => _mCurrentState.GetType().FullName;

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        public FsmStateBase<TFsmOwner> CurrentState => _mCurrentState;

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        public override bool IsRunning => CurrentState != null;

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        public override bool IsCleared => _mIsCleared;

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要开始的有限状态机状态类型。</typeparam>
        public void Start<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>
        {
            if (IsRunning)
            {
                throw new QsPlusFrameworkException("状态正在运行无法再次启动。");
            }

            FsmStateBase<TFsmOwner> state = GetState(typeof(TFsmOwnerState)) ?? throw new QsPlusFrameworkException($"状态机 '{typeof(TFsmOwner).FullName}' 进入状态 '{typeof(TFsmOwnerState).FullName}' 不存在。");
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <param name="stateType">要开始的有限状态机状态类型。</param>
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

            if (!typeof(FsmStateBase<TFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            FsmStateBase<TFsmOwner> state = GetState(stateType) ?? throw new QsPlusFrameworkException($"状态机 '{typeof(TFsmOwner).FullName}' 进入状态 '{stateType.FullName}' 不存在。");
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>
        {
            return _mFsmStates.ContainsKey(typeof(TFsmOwnerState));
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的有限状态机状态类型。</param>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(FsmStateBase<TFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            return _mFsmStates.ContainsKey(stateType);
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        public TFsmOwnerState GetState<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>
        {
            if (_mFsmStates.TryGetValue(typeof(TFsmOwnerState), out FsmStateBase<TFsmOwner> state))
            {
                return (TFsmOwnerState) state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        public FsmStateBase<TFsmOwner> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(FsmStateBase<TFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            if (_mFsmStates.TryGetValue(stateType, out FsmStateBase<TFsmOwner> state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        public FsmStateBase<TFsmOwner>[] GetAllStates()
        {
            int index = 0;
            FsmStateBase<TFsmOwner>[] results = new FsmStateBase<TFsmOwner>[_mFsmStates.Count];
            foreach (KeyValuePair<Type, FsmStateBase<TFsmOwner>> state in _mFsmStates)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<TFsmOwner> Create(TFsmOwner owner, params FsmStateBase<TFsmOwner>[] states)
        {
            if (owner == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            if (states == null || states.Length < 1)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            Fsm<TFsmOwner> fsm = InternalReferencePool.AcquireReference<Fsm<TFsmOwner>>();
            fsm._mCurrentOwner = owner;
            fsm._mIsCleared = false;
            foreach (FsmStateBase<TFsmOwner> state in states)
            {
                if (state == null)
                {
                    throw new QsPlusFrameworkException("状态机状态是无效的。");
                }

                Type stateType = state.GetType();
                if (fsm._mFsmStates.ContainsKey(stateType))
                {
                    throw new QsPlusFrameworkException($"状态机 '{typeof(TFsmOwner).FullName}' 里状态 '{stateType.FullName}' 已存在。");
                }

                fsm._mFsmStates.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<TFsmOwner> Create(TFsmOwner owner, List<FsmStateBase<TFsmOwner>> states)
        {
            if (owner == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            if (states == null || states.Count < 1)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            Fsm<TFsmOwner> fsm = InternalReferencePool.AcquireReference<Fsm<TFsmOwner>>();
            fsm._mCurrentOwner = owner;
            fsm._mIsCleared = false;
            foreach (FsmStateBase<TFsmOwner> state in states)
            {
                if (state == null)
                {
                    throw new QsPlusFrameworkException("状态机状态是无效的。");
                }

                Type stateType = state.GetType();
                if (fsm._mFsmStates.ContainsKey(stateType))
                {
                    throw new QsPlusFrameworkException($"状态机 '{typeof(TFsmOwner).FullName}' 里状态 '{stateType.FullName}' 已存在。");
                }

                fsm._mFsmStates.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要切换到的有限状态机状态类型。</typeparam>
        internal void ChangeState<TFsmOwnerState>() where TFsmOwnerState : FsmStateBase<TFsmOwner>
        {
            ChangeState(typeof(TFsmOwnerState));
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        internal void ChangeState(Type stateType)
        {
            if (_mCurrentState == null)
            {
                throw new QsPlusFrameworkException("状态机状态是无效的。");
            }

            FsmStateBase<TFsmOwner> state = GetState(stateType);
            if (state == null)
            {
                throw new QsPlusFrameworkException($"状态机 '{typeof(TFsmOwner).FullName}' 进入状态 '{stateType.FullName}' 不存在。");
            }

            _mCurrentState.OnLeave(this);
            _mCurrentState = state;
            _mCurrentState.OnEnter(this);
        }
    }
}