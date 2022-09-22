//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using QsPlus.Framework.Common;
using QsPlus.Framework.Fsm;

namespace QsPlus.Framework.Procedure
{
    /// <summary>
    /// 流程管理器类。
    /// </summary>
    internal sealed class ProcedureManager : IQsPlusFrameworkModule, IProcedureManager
    {
        private IFsmManager _mFsmManager;
        private IFsm<IProcedureManager> _mProcedureFsm;

        /// <summary>
        /// 初始化流程管理器的新实例。
        /// </summary>
        public ProcedureManager()
        {
            _mFsmManager = null;
            _mProcedureFsm = null;
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.Procedure;

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
            if (_mFsmManager != null)
            {
                if (_mProcedureFsm != null)
                {
                    _mFsmManager.DestroyFsm(_mProcedureFsm);
                    _mProcedureFsm = null;
                }

                _mFsmManager = null;
            }
        }

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (_mProcedureFsm == null)
                {
                    throw new QsPlusFrameworkException("请先初始化流程。");
                }

                return (ProcedureBase) _mProcedureFsm.CurrentState;
            }
        }

        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        public void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures)
        {
            _mFsmManager = fsmManager ?? throw new QsPlusFrameworkException("状态机管理器是无效的。");
            _mProcedureFsm = _mFsmManager.CreateFsm(this, procedures);
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        public void StartProcedure<T>() where T : ProcedureBase
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            _mProcedureFsm.Start<T>();
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <param name="procedureType">要开始的流程类型。</param>
        public void StartProcedure(Type procedureType)
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            _mProcedureFsm.Start(procedureType);
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            return _mProcedureFsm.HasState<T>();
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <param name="procedureType">要检查的流程类型。</param>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(Type procedureType)
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            return _mProcedureFsm.HasState(procedureType);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            return _mProcedureFsm.GetState<T>();
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <param name="procedureType">要获取的流程类型。</param>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure(Type procedureType)
        {
            if (_mProcedureFsm == null)
            {
                throw new QsPlusFrameworkException("请先初始化流程。");
            }

            return (ProcedureBase) _mProcedureFsm.GetState(procedureType);
        }
    }
}