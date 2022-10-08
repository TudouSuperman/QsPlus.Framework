//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.StateMachine;

namespace QsPlus.Framework.Dialog
{
    /// <summary>
    /// 对话框管理器接口。
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// 获取当前对话框逻辑。
        /// </summary>
        DialogState GetCurrentDialogStateLogic { get; }

        /// <summary>
        /// 展示对话框。
        /// </summary>
        /// <param name="dialog">要展示的对话框。</param>
        void ShowDialog(IDialog dialog);

        /// <summary>
        /// 隐藏对话框。
        /// </summary>
        /// <param name="dialog">要隐藏的对话框。</param>
        void HideDialog(IDialog dialog);

        /// <summary>
        /// 初始化对话框管理器。
        /// </summary>
        /// <param name="stateMachineManager">状态机管理器。</param>
        /// <param name="dialogStates">对话框管理器包含的界面状态逻辑。</param>
        void Initialize(IStateMachineManager stateMachineManager, HashSet<DialogState> dialogStates);

        /// <summary>
        /// 启动对话框逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要启动的对话框持有者对话框逻辑。</typeparam>
        void StartDialogStateLogic<TDialogState>() where TDialogState : DialogState;

        /// <summary>
        /// 检查是否存在对话框逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要检查的对话框持有者对话框逻辑。</typeparam>
        /// <returns>是否存在对话框状态逻辑。</returns>
        bool HasDialogStateLogic<TDialogState>() where TDialogState : DialogState;

        /// <summary>
        /// 获取对话框状态逻辑。
        /// </summary>
        /// <typeparam name="TDialogState">要获取的对话框持有者对话框逻辑。</typeparam>
        /// <returns>获取到的对话框状态逻辑。</returns>
        DialogState GetDialogStateLogic<TDialogState>() where TDialogState : DialogState;

        /// <summary>
        /// 获取所有对话框状态逻辑。
        /// </summary>
        /// <returns>获取到的所有对话框状态逻辑。</returns>
        DialogState[] GetDialogStateLogics();

        /// <summary>
        /// 获取所有对话框状态逻辑。
        /// </summary>
        /// <param name="dialogStates">获取到的所有对话框状态逻辑。</param>
        void GetDialogStateLogics(List<DialogState> dialogStates);
    }
}