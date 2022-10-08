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
    /// 资源管理器类。
    /// </summary>
    internal sealed class ResourceManager : IQsPlusFrameworkModule, IResourceManager
    {
        private readonly IDictionary<KeyValuePair<int, string>, ResourceGroup> _resourceGroups;

        /// <summary>
        /// 资源组数量。
        /// </summary>
        public int GroupCount => _resourceGroups.Count;

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.ResourceManager;

        /// <summary>
        /// 初始化资源管理器类的新实例。
        /// </summary>
        public ResourceManager()
        {
            _resourceGroups = new Dictionary<KeyValuePair<int, string>, ResourceGroup>();
        }

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            _resourceGroups.Clear();
        }

        /// <summary>
        /// 检查是否存在资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <returns>是否存在资源组。</returns>
        public bool HasResourceGroup(int groupId, string groupName)
        {
            return _resourceGroups.ContainsKey(new KeyValuePair<int, string>(groupId, groupName));
        }

        /// <summary>
        /// 获取资源组内资源信息。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>获取到的资源信息。</returns>
        public IResourceInfo GetResourceInfo(int groupId, string groupName, int resourceId, string resourceName)
        {
            if (_resourceGroups.TryGetValue(new KeyValuePair<int, string>(groupId, groupName), out ResourceGroup resourceGroup))
            {
                return resourceGroup.GetResourceInfo(resourceId, resourceName);
            }

            return null;
        }

        /// <summary>
        /// 获取资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <returns>获取到的资源组。</returns>
        public IResourceGroup GetResourceGroup(int groupId, string groupName)
        {
            if (_resourceGroups.TryGetValue(new KeyValuePair<int, string>(groupId, groupName), out ResourceGroup resourceGroup))
            {
                return resourceGroup;
            }

            return null;
        }

        /// <summary>
        /// 增加资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        public void AddResourceGroup(int groupId, string groupName)
        {
            KeyValuePair<int, string> info = new KeyValuePair<int, string>(groupId, groupName);
            if (_resourceGroups.ContainsKey(info))
            {
                throw new QsPlusFrameworkException($"已存在资源组编号为：{groupId}，资源组名称为：{groupName} 的资源组。");
            }

            _resourceGroups.Add(info, ResourceGroup.Create(groupId, groupName));
        }

        /// <summary>
        /// 移除资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        public void RemoveResourceGroup(int groupId, string groupName)
        {
            KeyValuePair<int, string> info = new KeyValuePair<int, string>(groupId, groupName);
            if (_resourceGroups.ContainsKey(info))
            {
                InternalReferencePool.ReleaseReference(_resourceGroups[info]);
                _resourceGroups.Remove(info);
            }
        }

        /// <summary>
        /// 移除所有资源组。
        /// </summary>
        public void RemoveAllResourceGroups()
        {
            foreach (KeyValuePair<KeyValuePair<int, string>, ResourceGroup> itemResourceInfo in _resourceGroups)
            {
                InternalReferencePool.ReleaseReference(itemResourceInfo.Value);
                _resourceGroups.Remove(itemResourceInfo);
            }
        }
    }
}