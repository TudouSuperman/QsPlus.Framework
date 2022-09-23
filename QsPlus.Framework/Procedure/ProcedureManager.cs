//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.StateMachine;

namespace QsPlus.Framework.Procedure
{
    /// <summary>
    /// 流程管理器类。
    /// </summary>
    internal sealed class ProcedureManager : IQsPlusFrameworkModule, IProcedureManager
    {
        private IStateMachineManager _stateMachineManager;
        private IFiniteStateMachine<IProcedureManager> _finiteStateMachine;

        /// <summary>
        /// 获取当前流程状态。
        /// </summary>
        public ProcedureState GetCurrentProcedureState
        {
            get
            {
                if (_finiteStateMachine == null)
                {
                    throw new QsPlusFrameworkException("请先初始化流程管理器。");
                }

                return (ProcedureState) _finiteStateMachine.GetFiniteStateMachineCurrentState;
            }
        }

        /// <summary>
        /// 初始化流程管理器类的新实例。
        /// </summary>
        public ProcedureManager()
        {
            _stateMachineManager = null;
            _finiteStateMachine = null;
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.ProcedureManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            if (_stateMachineManager == null)
            {
                return;
            }

            if (_finiteStateMachine == null)
            {
                return;
            }

            _stateMachineManager.DestroyFiniteStateMachine<ProcedureManager>();
            _finiteStateMachine = null;
            _stateMachineManager = null;
        }

        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="stateMachineManager">状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        public void Initialize(IStateMachineManager stateMachineManager, HashSet<ProcedureState> procedures)
        {
            if (procedures == null || procedures.Count <= 0)
            {
                throw new QsPlusFrameworkException("类型为空的流程管理器包含的流程是无效的。");
            }

            _stateMachineManager = stateMachineManager ?? throw new QsPlusFrameworkException("类型为空的状态机管理器是无效的。");

            HashSet<IFiniteStateMachineState<IProcedureManager>> tempFiniteStateMachineStates = new HashSet<IFiniteStateMachineState<IProcedureManager>>();
            foreach (ProcedureState itemProcedureState in procedures)
            {
                tempFiniteStateMachineStates.Add(itemProcedureState);
            }

            _finiteStateMachine = _stateMachineManager.CreateFiniteStateMachine(this, tempFiniteStateMachineStates);
        }

        /// <summary>
        /// 启动流程状态。
        /// </summary>
        /// <typeparam name="TProcedureState">要启动的流程持有者状态类型。</typeparam>
        public void StartProcedure<TProcedureState>() where TProcedureState : ProcedureState
        {
            if (_finiteStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程管理器。");
            }

            _finiteStateMachine.StartFiniteStateMachineState<TProcedureState>();
        }

        /// <summary>
        /// 检查是否存在流程状态。
        /// </summary>
        /// <typeparam name="TProcedureState">要启动的流程持有者状态类型。</typeparam>
        /// <returns>是否存在流程状态。</returns>
        public bool HasProcedure<TProcedureState>() where TProcedureState : ProcedureState
        {
            if (_finiteStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程管理器。");
            }

            return _finiteStateMachine.HasFiniteStateMachineState<TProcedureState>();
        }

        /// <summary>
        /// 获取流程状态。
        /// </summary>
        /// <typeparam name="TProcedureState">要获取的流程持有者状态类型。</typeparam>
        /// <returns>获取到的流程状态。</returns>
        public ProcedureState GetProcedure<TProcedureState>() where TProcedureState : ProcedureState
        {
            if (_finiteStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程管理器。");
            }

            return _finiteStateMachine.GetFiniteStateMachineState<TProcedureState>();
        }

        /// <summary>
        /// 获取所有流程状态。
        /// </summary>
        /// <returns>获取到的所有流程状态。</returns>
        public ProcedureState[] GetProcedures()
        {
            if (_finiteStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程管理器。");
            }

            IFiniteStateMachineState<IProcedureManager>[] tempFiniteStateMachineStates = _finiteStateMachine.GetFiniteStateMachineStates();
            ProcedureState[] tempProcedureStates = new ProcedureState[tempFiniteStateMachineStates.Length];
            for (int i = 0; i < tempFiniteStateMachineStates.Length; i++)
            {
                tempProcedureStates[i] = (ProcedureState) tempFiniteStateMachineStates[i];
            }

            return tempProcedureStates;
        }

        /// <summary>
        /// 获取所有流程状态。
        /// </summary>
        /// <param name="procedureStates">获取到的所有流程状态。</param>
        public void GetProcedures(List<ProcedureState> procedureStates)
        {
            foreach (IFiniteStateMachineState<IProcedureManager> itemProcedureState in _finiteStateMachine.GetFiniteStateMachineStates())
            {
                procedureStates.Add((ProcedureState) itemProcedureState);
            }
        }
    }
}