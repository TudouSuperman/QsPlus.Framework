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
    /// 有限状态机状态基类。
    /// </summary>
    /// <typeparam name="TFsmOwner">有限状态机持有者类型。</typeparam>
    public abstract class FsmStateBase<TFsmOwner> where TFsmOwner : class
    {
        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal abstract void OnInit(IFsm<TFsmOwner> fsm);

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal abstract void OnEnter(IFsm<TFsmOwner> fsm);

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal abstract void OnUpdate(IFsm<TFsmOwner> fsm, float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal abstract void OnLeave(IFsm<TFsmOwner> fsm);

        /// <summary>
        /// 有限状态机状态被清理时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal abstract void OnClear(IFsm<TFsmOwner> fsm);

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TFsmOwnerState">要切换到的有限状态机状态类型。</typeparam>
        /// <param name="fsm">有限状态机引用。</param>
        protected void ChangeState<TFsmOwnerState>(IFsm<TFsmOwner> fsm) where TFsmOwnerState : FsmStateBase<TFsmOwner>
        {
            Fsm<TFsmOwner> fsmImplement = (Fsm<TFsmOwner>) fsm;
            if (fsmImplement == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            fsmImplement.ChangeState<TFsmOwnerState>();
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        protected void ChangeState(IFsm<TFsmOwner> fsm, Type stateType)
        {
            Fsm<TFsmOwner> fsmImplement = (Fsm<TFsmOwner>) fsm;
            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (stateType == null)
            {
                throw new QsPlusFrameworkException("状态类型是无效的。");
            }

            if (!typeof(FsmStateBase<TFsmOwner>).IsAssignableFrom(stateType))
            {
                throw new QsPlusFrameworkException($"状态类型 '{stateType.FullName}' 是无效的。");
            }

            fsmImplement.ChangeState(stateType);
        }
    }
}