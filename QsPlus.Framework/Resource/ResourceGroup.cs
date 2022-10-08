//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Resource
{
    /// <summary>
    /// 资源组类。
    /// </summary>
    internal sealed class ResourceGroup : IReference, IResourceGroup
    {
        private readonly IDictionary<KeyValuePair<int, string>, ResourceInfo> _resourceInfos;

        /// <summary>
        /// 资源组编号。
        /// </summary>
        public int GroupId { get; private set; }

        /// <summary>
        /// 资源组名称。
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 资源组内资源数量。
        /// </summary>
        public int ResourceCount => _resourceInfos.Count;

        /// <summary>
        /// 初始化资源组类的新实例。
        /// </summary>
        public ResourceGroup()
        {
            _resourceInfos = new Dictionary<KeyValuePair<int, string>, ResourceInfo>();
            GroupId = 0;
            GroupName = null;
        }

        /// <summary>
        /// 创建资源组类。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <returns>创建资源组类。</returns>
        public static ResourceGroup Create(int groupId, string groupName)
        {
            ResourceGroup tempResourceGroup = InternalReferencePool.AcquireReference<ResourceGroup>();
            tempResourceGroup.GroupId = groupId;
            tempResourceGroup.GroupName = groupName;
            return tempResourceGroup;
        }

        /// <summary>
        /// 检查资源组内是否存在资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>资源组内是否存在资源信息。</returns>
        public bool HasResourceInfo(int resourceId, string resourceName)
        {
            return _resourceInfos.ContainsKey(new KeyValuePair<int, string>(resourceId, resourceName));
        }

        /// <summary>
        /// 获取资源组内资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>获取到的资源信息。</returns>
        public IResourceInfo GetResourceInfo(int resourceId, string resourceName)
        {
            if (_resourceInfos.TryGetValue(new KeyValuePair<int, string>(resourceId, resourceName), out ResourceInfo resourceInfo))
            {
                return resourceInfo;
            }

            return null;
        }

        /// <summary>
        /// 获取资源组内所有资源信息。
        /// </summary>
        /// <param name="resourceInfos">获取到的所有资源信息。</param>
        public void GetAllResourceInfos(List<IResourceInfo> resourceInfos)
        {
            foreach (KeyValuePair<KeyValuePair<int, string>, ResourceInfo> itemResourceInfo in _resourceInfos)
            {
                resourceInfos.Add(itemResourceInfo.Value);
            }
        }

        /// <summary>
        /// 获取资源组内所有资源信息。
        /// </summary>
        /// <returns>获取到的所有资源信息。</returns>
        public IResourceInfo[] GetAllResourceInfos()
        {
            int pointer = 0;
            IResourceInfo[] tempResourceInfos = new IResourceInfo[_resourceInfos.Count];
            foreach (KeyValuePair<KeyValuePair<int, string>, ResourceInfo> itemResourceInfo in _resourceInfos)
            {
                tempResourceInfos[pointer] = itemResourceInfo.Value;
                pointer++;
            }

            return tempResourceInfos;
        }

        /// <summary>
        /// 添加资源信息到资源组。
        /// </summary>
        /// <param name="resourceInfo">资源信息。</param>
        public void AddResourceInfo(IResourceInfo resourceInfo)
        {
            if (resourceInfo == null)
            {
                throw new QsPlusFrameworkException("类型为空的要添加的资源信息是无效的。");
            }

            KeyValuePair<int, string> info = new KeyValuePair<int, string>(resourceInfo.ResourceId, resourceInfo.ResourceName);
            if (_resourceInfos.ContainsKey(info))
            {
                throw new QsPlusFrameworkException($"已存在资源编号为：{resourceInfo.ResourceId}，资源名称为：{resourceInfo.ResourceName} 的资源。");
            }

            _resourceInfos.Add(info, (ResourceInfo) resourceInfo);
        }

        /// <summary>
        /// 添加资源信息到资源组。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="resourceHandle">资源实例。</param>
        public void AddResourceInfo(int resourceId, string resourceName, object resourceHandle)
        {
            if (_resourceInfos.ContainsKey(new KeyValuePair<int, string>(resourceId, resourceName)))
            {
                throw new QsPlusFrameworkException($"已存在资源编号为：{resourceId}，资源名称为：{resourceName} 的资源。");
            }

            ResourceInfo.Create(resourceId, resourceName, resourceHandle, this);
        }

        /// <summary>
        /// 从资源组移除资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        public void RemoveResourceInfo(int resourceId, string resourceName)
        {
            if (_resourceInfos.ContainsKey(new KeyValuePair<int, string>(resourceId, resourceName)))
            {
                InternalReferencePool.ReleaseReference(_resourceInfos[new KeyValuePair<int, string>(resourceId, resourceName)]);
                _resourceInfos.Remove(new KeyValuePair<int, string>(resourceId, resourceName));
            }
        }

        /// <summary>
        /// 移除资源组内所有资源信息。
        /// </summary>
        public void RemoveAllResourceInfos()
        {
            foreach (KeyValuePair<KeyValuePair<int, string>, ResourceInfo> itemResourceInfo in _resourceInfos)
            {
                InternalReferencePool.ReleaseReference(itemResourceInfo.Value);
                _resourceInfos.Remove(itemResourceInfo);
            }
        }

        /// <summary>
        /// 清理 ResourceGroup 引用。
        /// </summary>
        public void ClearReference()
        {
            GroupId = 0;
            GroupName = null;
            _resourceInfos.Clear();
        }
    }
}