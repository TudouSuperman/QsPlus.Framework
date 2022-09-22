//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Fsm;
using ProcedureOwner = QsPlus.Framework.Fsm.IFsm<QsPlus.Framework.Procedure.IProcedureManager>;

namespace QsPlus.Framework.Procedure
{
    /// <summary>
    /// 流程基类。
    /// </summary>
    public abstract class ProcedureBase : FsmStateBase<IProcedureManager>
    {
        /// <summary>
        /// 流程初始化时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
        }

        /// <summary>
        /// 进入流程时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
        }

        /// <summary>
        /// 流程轮询时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        protected internal override void OnUpdate(ProcedureOwner procedureOwner, float logicTime, float actualTime)
        {
        }

        /// <summary>
        /// 流程离开时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnLeave(ProcedureOwner procedureOwner)
        {
        }

        /// <summary>
        /// 流程被清理时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnClear(ProcedureOwner procedureOwner)
        {
        }
    }
}