//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QsPlus.Framework.Reference
{
    /// <summary>
    /// 内部引用池类。
    /// </summary>
    internal static class InternalReferencePool
    {
        /// <summary>
        /// 内部引用缓存。
        /// </summary>
        private static readonly IDictionary<Type, InternalReferenceCache> InternalReferences = new Dictionary<Type, InternalReferenceCache>();

        /// <summary>
        /// 检查是否存在指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>是否存在指定类型引用。</returns>
        public static bool CheckReference(Type referenceType)
        {
            return InternalGetReferenceCache(referenceType).CheckReference();
        }

        /// <summary>
        /// 获取指定类型引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>要获取的引用。</returns>
        public static IReference AcquireReference(Type referenceType)
        {
            return InternalGetReferenceCache(referenceType).AcquireReference();
        }

        /// <summary>
        /// 释放指定类型引用。
        /// </summary>
        /// <param name="reference">引用。</param>
        public static void ReleaseReference(IReference reference)
        {
            InternalGetReferenceCache(reference.GetType()).ReleaseReference(reference);
        }

        /// <summary>
        /// 清理所有引用。
        /// </summary>
        public static void ClearReferences()
        {
            foreach (KeyValuePair<Type, InternalReferenceCache> itemInternalReferenceCache in InternalReferences)
            {
                itemInternalReferenceCache.Value.ClearInternalReferenceCache();
            }
            
            InternalReferences.Clear();
        }

        /// <summary>
        /// 内部获取指定类型引用的缓存。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <returns>指定类型引用的缓存。</returns>
        private static InternalReferenceCache InternalGetReferenceCache(Type referenceType)
        {
            if (InternalReferences.TryGetValue(referenceType, out InternalReferenceCache internalReferenceCache))
            {
                return internalReferenceCache;
            }

            internalReferenceCache = new InternalReferenceCache(referenceType);

            if (InternalReferences.ContainsKey(referenceType))
            {
                InternalReferences[referenceType] = internalReferenceCache;
            }
            else
            {
                InternalReferences.Add(referenceType, internalReferenceCache);
            }

            return internalReferenceCache;
        }
    }
}