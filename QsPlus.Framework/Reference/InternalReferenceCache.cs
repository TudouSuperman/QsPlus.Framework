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
    /// 内部引用缓存类。
    /// </summary>
    internal sealed class InternalReferenceCache
    {
        /// <summary>
        /// 引用缓存类型。
        /// </summary>
        private readonly Type _mReferenceType;

        /// <summary>
        /// 引用缓存队列。
        /// </summary>
        private readonly Queue<IReference> _mReferences;

        /// <summary>
        /// 初始化内部引用缓存类的新实例。
        /// </summary>
        /// <param name="referenceType">引用缓存类型。</param>
        public InternalReferenceCache(Type referenceType)
        {
            _mReferenceType = referenceType;
            _mReferences = new Queue<IReference>();
        }

        /// <summary>
        /// 检查是否存在引用。
        /// </summary>
        /// <returns>是否存在引用。</returns>
        public bool CheckReference()
        {
            return _mReferences != null && _mReferences.Count > 0;
        }

        /// <summary>
        /// 获取引用。
        /// </summary>
        /// <returns>要获取的引用。</returns>
        public IReference AcquireReference()
        {
            IReference reference;

            if (CheckReference())
            {
                reference = _mReferences.Dequeue();
            }
            else
            {
                reference = (IReference) Activator.CreateInstance(_mReferenceType);
            }

            return reference;
        }

        /// <summary>
        /// 释放引用。
        /// </summary>
        /// <param name="reference">引用。</param>
        public void ReleaseReference(IReference reference)
        {
            reference.ClearReference();
            _mReferences.Enqueue(reference);
        }

        /// <summary>
        /// 清理内部引用缓存。
        /// </summary>
        public void ClearInternalReferenceCache()
        {
            _mReferences.Clear();
        }
    }
}