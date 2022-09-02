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
    /// 引用缓存。
    /// </summary>
    internal sealed class ReferenceCache
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
        /// 初始化引用缓存类的新实例。
        /// </summary>
        /// <param name="referenceType">引用缓存类型。</param>
        public ReferenceCache(Type referenceType)
        {
            _mReferenceType = referenceType;
            _mReferences = new Queue<IReference>();
        }

        /// <summary>
        /// 获取引用的数量。
        /// </summary>
        /// <returns>引用的数量。</returns>
        public int ReferenceCount => _mReferences.Count;

        /// <summary>
        /// 引用缓存轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void ReferenceCacheUpdate(float logicTime, float actualTime)
        {
        }

        /// <summary>
        /// 引用缓存关闭。
        /// </summary>
        public void ReferenceCacheShutdown()
        {
            _mReferences.Clear();
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
        /// 创建并缓存多个引用。
        /// </summary>
        /// <param name="count">数量。</param>
        public void AddReference(int count)
        {
            while (count-- > 0)
            {
                _mReferences.Enqueue((IReference) Activator.CreateInstance(_mReferenceType));
            }
        }

        /// <summary>
        /// 移除多个引用。
        /// </summary>
        /// <param name="count">数量。</param>
        public void RemoveReference(int count)
        {
            if (_mReferences.Count <= count)
            {
                _mReferences.Clear();
            }
            else
            {
                while (count-- > 0)
                {
                    _mReferences.Dequeue();
                }
            }
        }
    }
}