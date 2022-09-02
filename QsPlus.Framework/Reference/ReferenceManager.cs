//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Common;

namespace QsPlus.Framework.Reference
{
    /// <summary>
    /// 引用管理器类。
    /// </summary>
    internal class ReferenceManager : IQsPlusFrameworkModule, IReferenceManager
    {
        /// <summary>
        /// 引用缓存。
        /// </summary>
        private readonly IDictionary<Type, ReferenceCache> _mReferences;

        /// <summary>
        /// 初始化引用管理器类的新实例。
        /// </summary>
        public ReferenceManager()
        {
            _mReferences = new Dictionary<Type, ReferenceCache>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.ReferenceManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            foreach (KeyValuePair<Type, ReferenceCache> itemReferenceCache in _mReferences)
            {
                itemReferenceCache.Value.ReferenceCacheUpdate(logicTime, actualTime);
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            foreach (KeyValuePair<Type, ReferenceCache> itemReferenceCache in _mReferences)
            {
                itemReferenceCache.Value.ReferenceCacheShutdown();
            }

            _mReferences.Clear();
        }

        /// <summary>
        /// 获取指定类型引用的数量。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>指定类型引用的数量。</returns>
        public int ReferenceCount(Type referenceType)
        {
            return InternalGetReferenceCache(referenceType).ReferenceCount;
        }

        /// <summary>
        /// 检查是否存在指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>是否存在指定类型引用。</returns>
        public bool CheckReference(Type referenceType)
        {
            return InternalGetReferenceCache(referenceType).CheckReference();
        }

        /// <summary>
        /// 获取指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>要获取的引用。</returns>
        public IReference AcquireReference(Type referenceType)
        {
            return InternalGetReferenceCache(referenceType).AcquireReference();
        }

        /// <summary>
        /// 释放指定类型引用。
        /// </summary>
        /// <param name="reference">引用。</param>
        public void ReleaseReference(IReference reference)
        {
            InternalGetReferenceCache(reference.GetType()).ReleaseReference(reference);
        }

        /// <summary>
        /// 创建并缓存多个指定类型的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">数量。</param>
        public void AddReference(Type referenceType, int count)
        {
            InternalGetReferenceCache(referenceType).AddReference(count);
        }

        /// <summary>
        /// 移除多个指定类型的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">数量。</param>
        public void RemoveReference(Type referenceType, int count)
        {
            InternalGetReferenceCache(referenceType).RemoveReference(count);
        }

        /// <summary>
        /// 内部获取指定类型引用的缓存。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>指定类型引用的缓存。</returns>
        private ReferenceCache InternalGetReferenceCache(Type referenceType)
        {
            InternalCheckReferenceType(referenceType);

            if (_mReferences.TryGetValue(referenceType, out ReferenceCache referenceCache))
            {
                return referenceCache;
            }

            referenceCache = new ReferenceCache(referenceType);

            if (_mReferences.ContainsKey(referenceType))
            {
                _mReferences[referenceType] = referenceCache;
            }
            else
            {
                _mReferences.Add(referenceType, referenceCache);
            }

            return referenceCache;
        }

        /// <summary>
        /// 内部校验引用类型。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        private void InternalCheckReferenceType(Type referenceType)
        {
            if (referenceType == null)
            {
                throw new QsPlusFrameworkException("[引用类型是无效的]");
            }

            if (!referenceType.IsClass || referenceType.IsAbstract)
            {
                throw new QsPlusFrameworkException("[引用类型不可以为抽象类型]");
            }

            if (!typeof(IReference).IsAssignableFrom(referenceType))
            {
                throw new QsPlusFrameworkException($"[引用类型 '{referenceType.FullName}' 是无效的]");
            }
        }
    }
}