//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Common;

namespace QsPlus.Framework.Fsm
{
    /// <summary>
    /// 状态机管理器类。
    /// </summary>
    internal sealed class FsmManager : IQsPlusFrameworkModule, IFsmManager
    {
        private readonly IDictionary<Type, FsmBase> _mFsmDic;
        private readonly List<FsmBase> _mTempFsmList;

        /// <summary>
        /// 初始化有限状态机管理器的新实例。
        /// </summary>
        public FsmManager()
        {
            _mFsmDic = new Dictionary<Type, FsmBase>();
            _mTempFsmList = new List<FsmBase>();
        }

        /// <summary> 
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.FsmManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            _mTempFsmList.Clear();
            if (_mFsmDic.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<Type, FsmBase> fsm in _mFsmDic)
            {
                _mTempFsmList.Add(fsm.Value);
            }

            foreach (FsmBase fsm in _mTempFsmList)
            {
                if (fsm.IsCleared)
                {
                    continue;
                }

                fsm.FsmUpdate(logicTime, actualTime);
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            foreach (KeyValuePair<Type, FsmBase> fsm in _mFsmDic)
            {
                fsm.Value.FsmShutdown();
            }

            _mTempFsmList.Clear();
            _mFsmDic.Clear();
        }

        /// <summary>
        /// 获取状态机数量。
        /// </summary>
        public int FsmCount => _mFsmDic.Count;

        /// <summary>
        /// 检查是否存在状态机。
        /// </summary>
        /// <typeparam name="TFsm">状态机持有者类型。</typeparam>
        /// <returns>是否存在状态机。</returns>
        public bool HasFsm<TFsm>() where TFsm : class
        {
            return InternalHasFsm(typeof(TFsm));
        }

        /// <summary>
        /// 检查是否存在状态机。
        /// </summary>
        /// <param name="ownerType">状态机持有者类型。</param>
        /// <returns>是否存在状态机。</returns>
        public bool HasFsm(Type ownerType)
        {
            if (ownerType == null)
            {
                throw new QsPlusFrameworkException("状态机持有者类型是无效的。");
            }

            return InternalHasFsm(ownerType);
        }

        /// <summary>
        /// 获取状态机。
        /// </summary>
        /// <typeparam name="TFsm">状态机持有者类型。</typeparam>
        /// <returns>要获取的状态机。</returns>
        public IFsm<TFsm> GetFsm<TFsm>() where TFsm : class
        {
            return (IFsm<TFsm>) InternalGetFsm(typeof(TFsm));
        }

        /// <summary>
        /// 获取状态机。
        /// </summary>
        /// <param name="ownerType">状态机持有者类型。</param>
        /// <returns>要获取的状态机。</returns>
        public FsmBase GetFsm(Type ownerType)
        {
            if (ownerType == null)
            {
                throw new QsPlusFrameworkException("状态机持有者类型是无效的。");
            }

            return InternalGetFsm(ownerType);
        }

        /// <summary>
        /// 获取所有状态机。
        /// </summary>
        /// <returns>所有状态机。</returns>
        public FsmBase[] GetAllFsm()
        {
            int index = 0;
            FsmBase[] results = new FsmBase[_mFsmDic.Count];
            foreach (KeyValuePair<Type, FsmBase> fsm in _mFsmDic)
            {
                results[index++] = fsm.Value;
            }

            return results;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        public IFsm<TFsm> CreateFsm<TFsm>(TFsm owner, params FsmStateBase<TFsm>[] states) where TFsm : class
        {
            if (HasFsm<TFsm>())
            {
                throw new QsPlusFrameworkException($"已存在此类型状态机 ：{typeof(TFsm).FullName}");
            }

            Fsm<TFsm> fsm = Fsm<TFsm>.Create(owner, states);
            _mFsmDic.Add(owner.GetType(), fsm);
            return fsm;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        public IFsm<TFsm> CreateFsm<TFsm>(TFsm owner, List<FsmStateBase<TFsm>> states) where TFsm : class
        {
            if (HasFsm<TFsm>())
            {
                throw new QsPlusFrameworkException($"已存在此类型状态机 ：{typeof(TFsm).FullName}");
            }

            Fsm<TFsm> fsm = Fsm<TFsm>.Create(owner, states);
            _mFsmDic.Add(owner.GetType(), fsm);
            return fsm;
        }

        /// <summary>
        /// 销毁状态机。
        /// </summary>
        /// <typeparam name="TFsm">状态机持有者类型。</typeparam>
        /// <param name="fsm">要销毁的状态机。</param>
        /// <returns>是否销毁状态机成功。</returns>
        public bool DestroyFsm<TFsm>(IFsm<TFsm> fsm) where TFsm : class
        {
            if (fsm == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            return InternalDestroyFsm(typeof(TFsm));
        }

        /// <summary>
        /// 销毁状态机。
        /// </summary>
        /// <param name="fsm">要销毁的状态机。</param>
        /// <returns>是否销毁状态机成功。</returns>
        public bool DestroyFsm(FsmBase fsm)
        {
            if (fsm == null)
            {
                throw new QsPlusFrameworkException("状态机是无效的。");
            }

            return InternalDestroyFsm(fsm.OwnerType);
        }

        /// <summary>
        /// 内部检查是否存在状态机。
        /// </summary>
        /// <param name="fsm">状态机类型。</param>
        /// <returns></returns>
        private bool InternalHasFsm(Type fsm)
        {
            return _mFsmDic.ContainsKey(fsm);
        }

        /// <summary>
        /// 内部获取状态机。
        /// </summary>
        /// <param name="fsm">状态机类型。</param>
        /// <returns></returns>
        private FsmBase InternalGetFsm(Type fsm)
        {
            if (_mFsmDic.TryGetValue(fsm, out FsmBase tempFsm))
            {
                return tempFsm;
            }

            return null;
        }

        /// <summary>
        /// 内部销毁状态机。
        /// </summary>
        /// <param name="fsm">状态机类型。</param>
        /// <returns></returns>
        private bool InternalDestroyFsm(Type fsm)
        {
            if (_mFsmDic.TryGetValue(fsm, out FsmBase tempFsm))
            {
                tempFsm.FsmShutdown();
                return _mFsmDic.Remove(fsm);
            }

            return false;
        }
    }
}