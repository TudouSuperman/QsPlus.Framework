//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.StateMachine;
using DialogOwner = QsPlus.Framework.StateMachine.IPushDownStateMachine<QsPlus.Framework.Dialog.IDialogManager>;

namespace QsPlus.Framework.Dialog
{
    /// <summary>
    /// 对话框状态基类。
    /// </summary>
    public abstract class DialogState : IPushDownStateMachineState<IDialogManager>
    {
        /// <summary>
        /// 进入对话框状态。
        /// </summary>
        /// <param name="dialogOwner">对话框持有者。</param>
        public abstract void OnEnterState(DialogOwner dialogOwner);

        /// <summary>
        /// 遮挡对话框状态。
        /// </summary>
        /// <param name="dialogOwner">对话框持有者。</param>
        public abstract void OnPauseState(DialogOwner dialogOwner);

        /// <summary>
        /// 轮询对话框状态。
        /// </summary>
        /// <param name="dialogOwner">对话框持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public abstract void OnUpdateState(DialogOwner dialogOwner, float logicTime, float actualTime);

        /// <summary>
        /// 恢复对话框状态。
        /// </summary>
        /// <param name="dialogOwner">对话框持有者。</param>
        public abstract void OnResumeState(DialogOwner dialogOwner);

        /// <summary>
        /// 离开对话框状态。
        /// </summary>
        /// <param name="dialogOwner">对话框持有者。</param>
        public abstract void OnLeaveState(DialogOwner dialogOwner);
    }
}