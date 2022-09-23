//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.StateMachine;
using ProcedureOwner = QsPlus.Framework.StateMachine.IFiniteStateMachine<QsPlus.Framework.Procedure.IProcedureManager>;

namespace QsPlus.Framework.Procedure
{
    /// <summary>
    /// 流程状态基类。
    /// </summary>
    public abstract class ProcedureState : IFiniteStateMachineState<IProcedureManager>
    {
        /// <summary>
        /// 进入流程状态。
        /// </summary>
        /// <param name="procedure">流程持有者。</param>
        public abstract void OnEnterState(ProcedureOwner procedure);

        /// <summary>
        /// 轮询流程状态。
        /// </summary>
        /// <param name="procedure">流程持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public abstract void OnUpdateState(ProcedureOwner procedure, float logicTime, float actualTime);

        /// <summary>
        /// 离开流程状态。
        /// </summary>
        /// <param name="procedure">流程持有者。</param>
        public abstract void OnLeaveState(ProcedureOwner procedure);
    }
}