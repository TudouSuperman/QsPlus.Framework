//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QsPlus.Framework.Fsm
{
    /// <summary>
    /// 状态机管理器接口。
    /// </summary>
    public interface IFsmManager
    {
        /// <summary>
        /// 获取状态机数量。
        /// </summary>
        int FsmAllCount { get; }

        /// <summary>
        /// 检查是否存在状态机。
        /// </summary>
        /// <typeparam name="TFsm">状态机持有者类型。</typeparam>
        /// <returns>是否存在状态机。</returns>
        bool HasFsm<TFsm>() where TFsm : class;

        /// <summary>
        /// 检查是否存在状态机。
        /// </summary>
        /// <param name="id">状态机编号。</param>
        /// <returns>是否存在状态机。</returns>
        bool HasFsm(int id);

        /// <summary>
        /// 获取有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <returns>要获取的有限状态机。</returns>
        IFsm<TFsm> GetFsm<TFsm>() where TFsm : class;

        /// <summary>
        /// 获取下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsm">下推状态机持有者类型。</typeparam>
        /// <returns>要获取的下推状态机。</returns>
        IPushDownFsm<TPushDownFsm> GetPushDownFsm<TPushDownFsm>() where TPushDownFsm : class;

        /// <summary>
        /// 获取状态机。
        /// </summary>
        /// <param name="id">状态机编号。</param>
        /// <returns>要获取的状态机。</returns>
        FsmBase GetFsm(int id);

        /// <summary>
        /// 获取所有状态机。
        /// </summary>
        /// <returns>所有状态机。</returns>
        FsmBase[] GetAllFsm();

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        IFsm<TFsm> CreateFsm<TFsm>(TFsm owner, params FsmStateBase<TFsm>[] states) where TFsm : class;

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        IFsm<TFsm> CreateFsm<TFsm>(TFsm owner, List<FsmStateBase<TFsm>> states) where TFsm : class;

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsm">下推状态机持有者类型。</typeparam>
        /// <param name="owner">下推状态机持有者。</param>
        /// <param name="states">下推状态机状态集合。</param>
        /// <returns>要创建的下推状态机。</returns>
        IPushDownFsm<TPushDownFsm> CreatePushDownFsm<TPushDownFsm>(TPushDownFsm owner, params PushDownFsmStateBase<TPushDownFsm>[] states) where TPushDownFsm : class;

        /// <summary>
        /// 创建下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsm">下推状态机持有者类型。</typeparam>
        /// <param name="owner">下推状态机持有者。</param>
        /// <param name="states">下推状态机状态集合。</param>
        /// <returns>要创建的下推状态机。</returns>
        IPushDownFsm<TPushDownFsm> CreatePushDownFsm<TPushDownFsm>(TPushDownFsm owner, List<PushDownFsmStateBase<TPushDownFsm>> states) where TPushDownFsm : class;

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="TFsm">有限状态机持有者类型。</typeparam>
        /// <param name="fsm">要销毁的有限状态机。</param>
        /// <returns>是否销毁有限状态机成功。</returns>
        bool DestroyFsm<TFsm>(IFsm<TFsm> fsm) where TFsm : class;

        /// <summary>
        /// 销毁下推状态机。
        /// </summary>
        /// <typeparam name="TPushDownFsm">下推状态机持有者类型。</typeparam>
        /// <param name="fsm">要销毁的下推状态机。</param>
        /// <returns>是否销毁下推状态机成功。</returns>
        bool DestroyPushDownFsm<TPushDownFsm>(IPushDownFsm<TPushDownFsm> fsm) where TPushDownFsm : class;

        /// <summary>
        /// 销毁状态机。
        /// </summary>
        /// <param name="id">要销毁的状态机编号。</param>
        /// <returns>是否销毁状态机成功。</returns>
        bool DestroyFsm(int id);
    }
}