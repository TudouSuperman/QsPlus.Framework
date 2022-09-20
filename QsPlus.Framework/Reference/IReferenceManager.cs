//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System;

namespace QsPlus.Framework.Reference
{
    /// <summary>
    /// 引用管理器接口。
    /// </summary>
    public interface IReferenceManager
    {
        /// <summary>
        /// 获取指定类型引用的数量。
        /// </summary>
        /// <typeparam name="TReference">引用类型。</typeparam>
        /// <returns>指定类型引用的数量。</returns>
        int ReferenceCount<TReference>() where TReference : IReference;

        /// <summary>
        /// 获取指定类型引用的数量。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>指定类型引用的数量。</returns>
        int ReferenceCount(Type referenceType);

        /// <summary>
        /// 检查是否存在指定类型引用。
        /// </summary>
        /// <typeparam name="TReference">引用类型。</typeparam>
        /// <returns>是否存在指定类型引用。</returns>
        bool CheckReference<TReference>() where TReference : IReference;

        /// <summary>
        /// 检查是否存在指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>是否存在指定类型引用。</returns>
        bool CheckReference(Type referenceType);

        /// <summary>
        /// 获取指定类型引用。
        /// </summary>
        /// <typeparam name="TReference">引用类型。</typeparam>
        /// <returns>要获取的引用。</returns>
        TReference AcquireReference<TReference>() where TReference : IReference;

        /// <summary>
        /// 获取指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>要获取的引用。</returns>
        IReference AcquireReference(Type referenceType);

        /// <summary>
        /// 释放指定引用。
        /// </summary>
        /// <param name="reference">引用。</param>
        void ReleaseReference(IReference reference);

        /// <summary>
        /// 创建并缓存多个指定类型的引用。
        /// </summary>
        /// <typeparam name="TReference">引用类型。</typeparam>
        /// <param name="count">数量。</param>
        void AddReference<TReference>(int count) where TReference : IReference;

        /// <summary>
        /// 创建并缓存多个指定类型的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">数量。</param>
        void AddReference(Type referenceType, int count);

        /// <summary>
        /// 移除多个指定类型的引用。
        /// </summary>
        /// <typeparam name="TReference">引用类型。</typeparam>
        /// <param name="count">数量。</param>
        void RemoveReference<TReference>(int count) where TReference : IReference;

        /// <summary>
        /// 移除多个指定类型的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">数量。</param>
        void RemoveReference(Type referenceType, int count);
    }
}