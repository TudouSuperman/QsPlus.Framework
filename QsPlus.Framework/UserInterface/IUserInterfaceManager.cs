//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Asset;
using QsPlus.Framework.StateMachine;

namespace QsPlus.Framework.UserInterface
{
    /// <summary>
    /// 用户界面管理器接口。
    /// </summary>
    public interface IUserInterfaceManager
    {
        /// <summary>
        /// 获取当前用户界面缓存数量。
        /// </summary>
        int GetCurrentUserInterfaceCount { get; }

        /// <summary>
        /// 获取当前用户界面状态逻辑。
        /// </summary>
        UserInterfaceState GetCurrentUserInterfaceStateLogic { get; }

        /// <summary>
        /// 初始化用户界面管理器。
        /// </summary>
        /// <param name="stateMachineManager">状态机管理器。</param>
        /// <param name="uiStates">用户界面管理器包含的界面状态逻辑。</param>
        void Initialize(IStateMachineManager stateMachineManager, HashSet<UserInterfaceState> uiStates);

        /// <summary>
        /// 启动用户界面状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要启动的用户界面持有者用户界面状态逻辑。</typeparam>
        void StartUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState;

        /// <summary>
        /// 检查是否存在用户界面状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要检查的用户界面持有者用户界面状态逻辑。</typeparam>
        /// <returns>是否存在用户界面状态状态逻辑。</returns>
        bool HasUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState;

        /// <summary>
        /// 获取用户界面状态状态逻辑。
        /// </summary>
        /// <typeparam name="TUserInterfaceState">要获取的用户界面持有者用户界面状态逻辑。</typeparam>
        /// <returns>获取到的用户界面状态状态逻辑。</returns>
        UserInterfaceState GetUiStateLogic<TUserInterfaceState>() where TUserInterfaceState : UserInterfaceState;

        /// <summary>
        /// 获取所有用户界面状态状态逻辑。
        /// </summary>
        /// <returns>获取到的所有用户界面状态状态逻辑。</returns>
        UserInterfaceState[] GetUiStateLogics();

        /// <summary>
        /// 获取所有用户界面状态状态逻辑。
        /// </summary>
        /// <param name="uiStates">获取到的所有用户界面状态状态逻辑。</param>
        void GetUiStateLogics(List<UserInterfaceState> uiStates);
    }
}