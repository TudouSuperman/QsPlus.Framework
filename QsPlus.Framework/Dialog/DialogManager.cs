//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.StateMachine;

namespace QsPlus.Framework.Dialog
{
    /// <summary>
    /// 对话框管理器类。
    /// </summary>
    internal sealed class DialogManager : IQsPlusFrameworkModule, IDialogManager
    {
        private IStateMachineManager _stateMachineManager;
        private IPushDownStateMachine<IDialogManager> _pushDownStateMachine;

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.DialogManager;

        /// <summary>
        /// 获取当前对话框状态逻辑。
        /// </summary>
        public DialogState GetCurrentDialogStateLogic
        {
            get
            {
                if (_pushDownStateMachine == null)
                {
                    throw new QsPlusFrameworkException("请先初始化对话框管理器。");
                }

                return (DialogState) _pushDownStateMachine.GetPushDownStateMachineCurrentState;
            }
        }

        /// <summary>
        /// 初始化对话框管理器类的新实例。
        /// </summary>
        public DialogManager()
        {
            _stateMachineManager = null;
            _pushDownStateMachine = null;
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
            if (_stateMachineManager == null)
            {
                return;
            }

            if (_pushDownStateMachine == null)
            {
                return;
            }

            _stateMachineManager.DestroyFiniteStateMachine<DialogManager>();
            _pushDownStateMachine = null;
            _stateMachineManager = null;
        }

        /// <summary>
        /// 展示对话框。
        /// </summary>
        /// <param name="dialog">要展示的对话框。</param>
        public void ShowDialog(IDialog dialog)
        {
        }

        /// <summary>
        /// 隐藏对话框。
        /// </summary>
        /// <param name="dialog">要隐藏的对话框。</param>
        public void HideDialog(IDialog dialog)
        {
        }

        /// <summary>
        /// 初始化对话框管理器。
        /// </summary>
        /// <param name="stateMachineManager">状态机管理器。</param>
        /// <param name="dialogStates">对话框管理器包含的界面状态逻辑。</param>
        public void Initialize(IStateMachineManager stateMachineManager, HashSet<DialogState> dialogStates)
        {
            if (dialogStates == null || dialogStates.Count <= 0)
            {
                throw new QsPlusFrameworkException("类型为空的对话框管理器包含的界面状态逻辑是无效的。");
            }

            _stateMachineManager = stateMachineManager ?? throw new QsPlusFrameworkException("类型为空的状态机管理器是无效的。");

            HashSet<IPushDownStateMachineState<IDialogManager>> tempPushDownStateMachineStates = new HashSet<IPushDownStateMachineState<IDialogManager>>();
            foreach (DialogState itemDialogState in dialogStates)
            {
                tempPushDownStateMachineStates.Add(itemDialogState);
            }

            _pushDownStateMachine = _stateMachineManager.CreatePushDownStateMachine(this, tempPushDownStateMachineStates);
        }

        /// <summary>
        /// 启动对话框状态逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要启动的对话框持有者对话框状态逻辑。</typeparam>
        public void StartDialogStateLogic<TDialogState>() where TDialogState : DialogState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化对话框管理器。");
            }

            _pushDownStateMachine.StartPushDownStateMachineState<TDialogState>();
        }

        /// <summary>
        /// 检查是否存在对话框状态逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要检查的对话框持有者对话框状态逻辑。</typeparam>
        /// <returns>是否存在对话框状态状态逻辑。</returns>
        public bool HasDialogStateLogic<TDialogState>() where TDialogState : DialogState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化对话框管理器。");
            }

            return _pushDownStateMachine.HasPushDownStateMachineState<TDialogState>();
        }

        /// <summary>
        /// 获取对话框状态状态逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要获取的对话框持有者对话框状态逻辑。</typeparam>
        /// <returns>获取到的对话框状态状态逻辑。</returns>
        public DialogState GetDialogStateLogic<TDialogState>() where TDialogState : DialogState
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化对话框管理器。");
            }

            return _pushDownStateMachine.GetPushDownStateMachineState<TDialogState>();
        }

        /// <summary>
        /// 获取所有对话框状态状态逻辑。
        /// </summary>
        /// <returns>获取到的所有对话框状态状态逻辑。</returns>
        public DialogState[] GetDialogStateLogics()
        {
            if (_pushDownStateMachine == null)
            {
                throw new QsPlusFrameworkException("请先初始化对话框管理器。");
            }

            IPushDownStateMachineState<IDialogManager>[] tempPushDownStateMachineStates = _pushDownStateMachine.GetPushDownStateMachineStates();
            DialogState[] tempDialogStates = new DialogState[tempPushDownStateMachineStates.Length];
            for (int i = 0; i < tempPushDownStateMachineStates.Length; i++)
            {
                tempDialogStates[i] = (DialogState) tempPushDownStateMachineStates[i];
            }

            return tempDialogStates;
        }

        /// <summary>
        /// 获取所有对话框状态状态逻辑。
        /// </summary>
        /// <param name="dialogStates">获取到的所有对话框状态状态逻辑。</param>
        public void GetDialogStateLogics(List<DialogState> dialogStates)
        {
            foreach (IPushDownStateMachineState<IDialogManager> itemDialogState in _pushDownStateMachine.GetPushDownStateMachineStates())
            {
                dialogStates.Add((DialogState) itemDialogState);
            }
        }
    }
}