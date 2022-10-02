//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.StateMachine;
using UserInterfaceOwner = QsPlus.Framework.StateMachine.IPushDownStateMachine<QsPlus.Framework.UserInterface.IUserInterfaceManager>;

namespace QsPlus.Framework.UserInterface
{
    /// <summary>
    /// 用户界面状态基类。
    /// </summary>
    public abstract class UserInterfaceState : IPushDownStateMachineState<IUserInterfaceManager>
    {
        /// <summary>
        /// 进入用户界面状态。
        /// </summary>
        /// <param name="userInterface">用户界面持有者。</param>
        public abstract void OnEnterState(UserInterfaceOwner userInterface);

        /// <summary>
        /// 遮挡用户界面状态。
        /// </summary>
        /// <param name="userInterface">用户界面持有者。</param>
        public abstract void OnPauseState(UserInterfaceOwner userInterface);

        /// <summary>
        /// 轮询用户界面状态。
        /// </summary>
        /// <param name="userInterface">用户界面持有者。</param>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public abstract void OnUpdateState(UserInterfaceOwner userInterface, float logicTime, float actualTime);

        /// <summary>
        /// 恢复用户界面状态。
        /// </summary>
        /// <param name="userInterface">用户界面持有者。</param>
        public abstract void OnResumeState(UserInterfaceOwner userInterface);

        /// <summary>
        /// 离开用户界面状态。
        /// </summary>
        /// <param name="userInterface">用户界面持有者。</param>
        public abstract void OnLeaveState(UserInterfaceOwner userInterface);
    }
}