//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Resource
{
    /// <summary>
    /// 资源管理器接口。
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// 资源组数量。
        /// </summary>
        int GroupCount { get; }

        /// <summary>
        /// 检查是否存在资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <returns></returns>
        bool HasResourceGroup(int groupId, string groupName);
        
        /// <summary>
        /// 获取资源组内资源信息。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>获取到的资源信息。</returns>
        IResourceInfo GetResourceInfo(int groupId, string groupName, int resourceId, string resourceName);
        
        /// <summary>
        /// 获取资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        /// <returns>获取到的资源组。</returns>
        IResourceGroup GetResourceGroup(int groupId, string groupName);

        /// <summary>
        /// 增加资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        void AddResourceGroup(int groupId, string groupName);

        /// <summary>
        /// 移除资源组。
        /// </summary>
        /// <param name="groupId">资源组编号。</param>
        /// <param name="groupName">资源组名称。</param>
        void RemoveResourceGroup(int groupId, string groupName);

        /// <summary>
        /// 移除所有资源组。
        /// </summary>
        void RemoveAllResourceGroups();
    }
}