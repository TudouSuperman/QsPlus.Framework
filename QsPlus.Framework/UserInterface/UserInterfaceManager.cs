//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Asset;
using QsPlus.Framework.Common;
using QsPlus.Framework.StateMachine;

namespace QsPlus.Framework.UserInterface
{
    /// <summary>
    /// 用户界面管理器类。
    /// </summary>
    internal sealed class UserInterfaceManager : IQsPlusFrameworkModule, IUserInterfaceManager
    {
        private IStateMachineManager _stateMachineManager;
        private IPushDownStateMachine<IUserInterfaceManager> _pushDownStateMachine;
        private IDictionary<Type, IUserInterface> _userInterfaces;


        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.UserInterfaceManager;

        /// <summary>
        /// 获取当前用户界面缓存数量。
        /// </summary>
        public int GetCurrentUserInterfaceCount
        {
            get
            {
                if (_userInterfaces == null)
                {
                    return 0;
                }

                return _userInterfaces.Count;
            }
        }

        /// <summary>
        /// 获取当前用户界面状态逻辑。
        /// </summary>
        public UserInterfaceState GetCurrentUserInterfaceStateLogic
        {
            get
            {
                if (_pushDownStateMachine == null)
                {
                    throw new QsPlusFrameworkException("请先初始化用户界面管理器。");
                }

                return (UserInterfaceState) _pushDownStateMachine.GetPushDownStateMachineCurrentState;
            }
        }

        /// <summary>
        /// 初始化用户界面管理器类的新实例。
        /// </summary>
        public UserInterfaceManager()
        {
            _stateMachineManager = null;
            _pushDownStateMachine = null;
            _userInterfaces = null;
        }

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
            _userInterfaces.Clear();

            if (_stateMachineManager == null)
            {
                return;
            }

            if (_pushDownStateMachine == null)
            {
                return;
            }

            _stateMachineManager.DestroyFiniteStateMachine<UserInterfaceManager>();
            _pushDownStateMachine = null;
            _stateMachineManager = null;
        }

        /// <summary>
        /// 初始化用户界面管理器。
        /// </summary>
        /// <param name="stateMachineManager">状态机管理器。</param>
        /// <param name="uiStates">用户界面管理器包含的界面状态逻辑。</param>
        public void Initialize(IStateMachineManager stateMachineManager, HashSet<UserInterfaceState> uiStates)
        {
            if (uiStates == null || uiStates.Count <= 0)
            {
                throw new QsPlusFrameworkException("类型为空的用户界面管理器包含的界面状态逻辑是无效的。");
            }

            _stateMachineManager = stateMachineManager ?? throw new QsPlusFrameworkException("类型为空的状态机管理器是无效的。");

            HashSet<IPushDownStateMachineState<IUserInterfaceManager>> tempPushDownStateMachineStates = new HashSet<IPushDownStateMachineState<IUserInterfaceManager>>();
            foreach (UserInterfaceState itemUserInterfaceState in uiStates)
            {
                tempPushDownStateMachineStates.Add(itemUserInterfaceState);
            }

            _pushDownStateMachine = _stateMachineManager.CreatePushDownStateMachine(this, tempPushDownStateMachineStates);
            _userInterfaces = new Dictionary<Type, IUserInterface>(uiStates.Count);
        }

        /// <summary>
        /// 启动用户界面状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要启动的用户界面持有者用户界面状态逻辑。</typeparam>
        public void StartUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化用户界面管理器。");
            }

            _pushDownStateMachine.StartPushDownStateMachineState<TUserInterfaceState>();
        }

        /// <summary>
        /// 检查是否存在用户界面状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要检查的用户界面持有者用户界面状态逻辑。</typeparam>
        /// <returns>是否存在用户界面状态状态逻辑。</returns>
        public bool HasUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化用户界面管理器。");
            }

            return _pushDownStateMachine.HasPushDownStateMachineState<TUserInterfaceState>();
        }

        /// <summary>
        /// 获取用户界面状态状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要获取的用户界面持有者用户界面状态逻辑。</typeparam>
        /// <returns>获取到的用户界面状态状态逻辑。</returns>
        public UserInterfaceState GetUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化用户界面管理器。");
            }

            return _pushDownStateMachine.GetPushDownStateMachineState<TUserInterfaceState>();
        }

        /// <summary>
        /// 获取所有用户界面状态状态逻辑。
        /// </summary>
        /// <returns>获取到的所有用户界面状态状态逻辑。</returns>
        public UserInterfaceState[] GetUiStateLogics()
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化用户界面管理器。");
            }

            IPushDownStateMachineState<IUserInterfaceManager>[] tempPushDownStateMachineStates = _pushDownStateMachine.GetPushDownStateMachineStates();
            UserInterfaceState[] tempUserInterfaceStates = new UserInterfaceState[tempPushDownStateMachineStates.Length];
            for (int i = 0; i < tempPushDownStateMachineStates.Length; i++)
            {
                tempUserInterfaceStates[i] = (UserInterfaceState) tempPushDownStateMachineStates[i];
            }

            return tempUserInterfaceStates;
        }

        /// <summary>
        /// 获取所有用户界面状态状态逻辑。
        /// </summary>
        /// <param name="uiStates">获取到的所有用户界面状态状态逻辑。</param>
        public void GetUiStateLogics(List<UserInterfaceState> uiStates)
        {
            foreach (IPushDownStateMachineState<IUserInterfaceManager> itemUserInterfaceState in _pushDownStateMachine.GetPushDownStateMachineStates())
            {
                uiStates.Add((UserInterfaceState) itemUserInterfaceState);
            }
        }
    }
}