//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;

namespace QsPlus.Framework.StateMachine
{
    /// <summary>
    /// 状态机管理器类。
    /// </summary>
    internal sealed class StateMachineManager : IQsPlusFrameworkModule, IStateMachineManager
    {
        private readonly IDictionary<int, IStateMachineModule> _finiteStateMachineModules;
        private readonly IDictionary<int, IStateMachineModule> _pushDownStateMachineModules;
        private readonly IList<IStateMachineModule> _tempStateMachineModules;

        /// <summary>
        /// 获取所有有限状态机的数量。
        /// </summary>
        public int GetFiniteStateMachineCount => _finiteStateMachineModules.Count;

        /// <summary>
        /// 获取所有下推状态机的数量。
        /// </summary>
        public int GetPushDownStateMachineCount => _pushDownStateMachineModules.Count;

        /// <summary>
        /// 初始化状态机管理器类的新实例。
        /// </summary>
        public StateMachineManager()
        {
            _finiteStateMachineModules = new Dictionary<int, IStateMachineModule>();
            _pushDownStateMachineModules = new Dictionary<int, IStateMachineModule>();
            _tempStateMachineModules = new List<IStateMachineModule>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.StateMachineManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            _tempStateMachineModules.Clear();

            if (_finiteStateMachineModules.Count >= 0)
            {
                foreach (KeyValuePair<int, IStateMachineModule> stateMachineModule in _finiteStateMachineModules)
                {
                    _tempStateMachineModules.Add(stateMachineModule.Value);
                }
            }

            if (_pushDownStateMachineModules.Count >= 0)
            {
                foreach (KeyValuePair<int, IStateMachineModule> stateMachineModule in _pushDownStateMachineModules)
                {
                    _tempStateMachineModules.Add(stateMachineModule.Value);
                }
            }

            if (_tempStateMachineModules.Count > 0)
            {
                foreach (IStateMachineModule stateMachineModule in _tempStateMachineModules)
                {
                    if (stateMachineModule.IsCleared)
                    {
                        continue;
                    }

                    stateMachineModule.StateMachineModuleUpdate(logicTime, actualTime);
                }
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            foreach (KeyValuePair<int, IStateMachineModule> stateMachineModule in _finiteStateMachineModules)
            {
                stateMachineModule.Value.StateMachineModuleShutdown();
            }

            foreach (KeyValuePair<int, IStateMachineModule> stateMachineModule in _pushDownStateMachineModules)
            {
                stateMachineModule.Value.StateMachineModuleShutdown();
            }

            _finiteStateMachineModules.Clear();
            _pushDownStateMachineModules.Clear();
            _tempStateMachineModules.Clear();
        }

        /// <summary>
        /// 检查是否存在有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要检查的有限状态机持有者类型。</typeparam>
        /// <returns>是否存在有限状态机。</returns>
        public bool HasFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class
        {
            return _finiteStateMachineModules.ContainsKey(typeof(TFiniteStateMachineOwner).GetHashCode());
        }

        /// <summary>
        /// 检查是否存在下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要检查的下推状态机持有者类型。</typeparam>
        /// <returns>是否存在下推状态机。</returns>
        public bool HasPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class
        {
            return _pushDownStateMachineModules.ContainsKey(typeof(TPushDownStateMachineOwner).GetHashCode());
        }

        /// <summary>
        /// 获取有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要获取的有限状态机持有者类型。</typeparam>
        /// <returns>获取到的有限状态机。</returns>
        public IFiniteStateMachine<TFiniteStateMachineOwner> GetFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class
        {
            if (_finiteStateMachineModules.TryGetValue(typeof(TFiniteStateMachineOwner).GetHashCode(), out IStateMachineModule stateMachineModule))
            {
                return (IFiniteStateMachine<TFiniteStateMachineOwner>) stateMachineModule;
            }

            return null;
        }

        /// <summary>
        /// 获取下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要获取的下推状态机持有者类型。</typeparam>
        /// <returns>获取到的下推状态机。</returns>
        public IPushDownStateMachine<TPushDownStateMachineOwner> GetPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class
        {
            if (_pushDownStateMachineModules.TryGetValue(typeof(TPushDownStateMachineOwner).GetHashCode(), out IStateMachineModule stateMachineModule))
            {
                return (IPushDownStateMachine<TPushDownStateMachineOwner>) stateMachineModule;
            }

            return null;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="finiteStateMachineOwner">有限状态机持有者。</param>
        /// <param name="finiteStateMachineStates">有限状态机状态集合。</param>
        /// <typeparam name="TFiniteStateMachineOwner">要创建的有限状态机持有者类型。</typeparam>
        /// <returns>创建的有限状态机。</returns>
        public IFiniteStateMachine<TFiniteStateMachineOwner> CreateFiniteStateMachine<TFiniteStateMachineOwner>(TFiniteStateMachineOwner finiteStateMachineOwner, HashSet<IFiniteStateMachineState<TFiniteStateMachineOwner>> finiteStateMachineStates) where TFiniteStateMachineOwner : class
        {
            if (HasFiniteStateMachine<TFiniteStateMachineOwner>())
            {
                throw new QsPlusFrameworkException($"已存在此有限状态机持有者类型的有限状态机 ：{typeof(TFiniteStateMachineOwner).FullName}");
            }

            FiniteStateMachine<TFiniteStateMachineOwner> tempFiniteStateMachine = FiniteStateMachine<TFiniteStateMachineOwner>.CreateFiniteStateMachine(finiteStateMachineOwner, finiteStateMachineStates);
            _finiteStateMachineModules.Add(typeof(TFiniteStateMachineOwner).GetHashCode(), tempFiniteStateMachine);
            return tempFiniteStateMachine;
        }

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <param name="pushDownStateMachineOwner">下推状态机持有者。</param>
        /// <param name="pushDownStateMachineStates">下推状态机状态集合。</param>
        /// <typeparam name="TPushDownStateMachineOwner">要创建的下推状态机持有者类型。</typeparam>
        /// <returns>创建的下推状态机。</returns>
        public IPushDownStateMachine<TPushDownStateMachineOwner> CreatePushDownStateMachine<TPushDownStateMachineOwner>(TPushDownStateMachineOwner pushDownStateMachineOwner, HashSet<IPushDownStateMachineState<TPushDownStateMachineOwner>> pushDownStateMachineStates) where TPushDownStateMachineOwner : class
        {
            if (HasPushDownStateMachine<TPushDownStateMachineOwner>())
            {
                throw new QsPlusFrameworkException($"已存在此下推状态机持有者类型的下推状态机 ：{typeof(TPushDownStateMachineOwner).FullName}");
            }

            PushDownStateMachine<TPushDownStateMachineOwner> tempPushDownStateMachine = PushDownStateMachine<TPushDownStateMachineOwner>.CreatePushDownStateMachine(pushDownStateMachineOwner, pushDownStateMachineStates);
            _pushDownStateMachineModules.Add(typeof(TPushDownStateMachineOwner).GetHashCode(), tempPushDownStateMachine);
            return tempPushDownStateMachine;
        }

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="TFiniteStateMachineOwner">要销毁的有限状态机持有者类型。</typeparam>
        /// <returns>是否销毁有限状态机成功。</returns>
        public bool DestroyFiniteStateMachine<TFiniteStateMachineOwner>() where TFiniteStateMachineOwner : class
        {
            int id = typeof(TFiniteStateMachineOwner).GetHashCode();
            if (_finiteStateMachineModules.TryGetValue(id, out IStateMachineModule stateMachineModule))
            {
                stateMachineModule.StateMachineModuleShutdown();
                return _finiteStateMachineModules.Remove(id);
            }

            return false;
        }

        /// <summary>
        /// 销毁下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownStateMachineOwner">要销毁的下推状态机持有者类型。</typeparam>
        /// <returns>是否销毁下推状态机成功。</returns>
        public bool DestroyPushDownStateMachine<TPushDownStateMachineOwner>() where TPushDownStateMachineOwner : class
        {
            int id = typeof(TPushDownStateMachineOwner).GetHashCode();
            if (_pushDownStateMachineModules.TryGetValue(id, out IStateMachineModule stateMachineModule))
            {
                stateMachineModule.StateMachineModuleShutdown();
                return _pushDownStateMachineModules.Remove(id);
            }

            return false;
        }
    }
}