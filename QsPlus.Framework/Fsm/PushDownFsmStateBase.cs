//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using QsPlus.Framework.Common;

namespace QsPlus.Framework.Fsm
{
    /// <summary>
    /// 下推状态机状态基类。
    /// </summary>
    /// <typeparam name="TPushDownFsmOwner">下推状态机持有者类型。</typeparam>
    public abstract class PushDownFsmStateBase<TPushDownFsmOwner> : FsmStateBase<TPushDownFsmOwner> where TPushDownFsmOwner : class
    {
        /// <summary>
        /// 下推状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnInit(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 下推状态机状态被推入临时位时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnEnter(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 下推状态机状态被压入栈区时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnPause(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 下推状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal abstract void OnUpdate(IPushDownFsm<TPushDownFsmOwner> fsm, float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 下推状态机状态从栈区被回滚到临时位时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnResume(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 下推状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnLeave(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 下推状态机状态被清理时调用。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        protected internal abstract void OnClear(IPushDownFsm<TPushDownFsmOwner> fsm);

        /// <summary>
        /// 切换当前下推状态机状态。
        /// </summary>
        /// <typeparam name="TFsmState">要切换到的下推状态机状态类型。</typeparam>
        /// <param name="fsm">下推状态机引用。</param>
        protected void PushDownState<TFsmState>(IPushDownFsm<TPushDownFsmOwner> fsm) where TFsmState : PushDownFsmStateBase<TPushDownFsmOwner>
        {
            PushDownFsm<TPushDownFsmOwner> fsmImplement = (PushDownFsm<TPushDownFsmOwner>) fsm;
            if (fsmImplement == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            fsmImplement.PushDownState<TFsmState>();
        }

        /// <summary>
        /// 切换当前下推状态机状态。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        /// <param name="stateType">要切换到的下推状态机状态类型。</param>
        protected void PushDownState(IPushDownFsm<TPushDownFsmOwner> fsm, Type stateType)
        {
            PushDownFsm<TPushDownFsmOwner> pushDownFsmImplement = (PushDownFsm<TPushDownFsmOwner>) fsm;
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(PushDownFsmStateBase<TPushDownFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            pushDownFsmImplement.PushDownState(stateType);
        }

        /// <summary>
        /// 上移当前栈区栈顶下推状态机到临时位。
        /// </summary>
        /// <param name="fsm">下推状态机引用。</param>
        internal void PopUpState(IPushDownFsm<TPushDownFsmOwner> fsm)
        {
            PushDownFsm<TPushDownFsmOwner> pushDownFsmImplement = (PushDownFsm<TPushDownFsmOwner>) fsm;
            pushDownFsmImplement.PopUpState();
        }
    }
}