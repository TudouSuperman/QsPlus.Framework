//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;

namespace QsPlus.Framework.Resource
{
    /// <summary>
    /// 资源组接口。
    /// </summary>
    public interface IResourceGroup
    {
        /// <summary>
        /// 资源组编号。
        /// </summary>
        int GroupId { get; }

        /// <summary>
        /// 资源组名称。
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// 资源组内资源数量。
        /// </summary>
        int ResourceCount { get; }

        /// <summary>
        /// 检查资源组内是否存在资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>资源组内是否存在资源信息。</returns>
        bool HasResourceInfo(int resourceId, string resourceName);
        
        /// <summary>
        /// 获取资源组内资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <returns>获取到的资源信息。</returns>
        IResourceInfo GetResourceInfo(int resourceId, string resourceName);

        /// <summary>
        /// 获取资源组内所有资源信息。
        /// </summary>
        /// <param name="resourceInfos">获取到的所有资源信息。</param>
        void GetAllResourceInfos(List<IResourceInfo> resourceInfos);

        /// <summary>
        /// 获取资源组内所有资源信息。
        /// </summary>
        /// <returns>获取到的所有资源信息。</returns>
        IResourceInfo[] GetAllResourceInfos();

        /// <summary>
        /// 添加资源信息到资源组。
        /// </summary>
        /// <param name="resourceInfo">资源信息。</param>
        void AddResourceInfo(IResourceInfo resourceInfo);

        /// <summary>
        /// 添加资源信息到资源组。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="resourceHandle">资源实例。</param>
        void AddResourceInfo(int resourceId, string resourceName, object resourceHandle);

        /// <summary>
        /// 从资源组移除资源信息。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        void RemoveResourceInfo(int resourceId, string resourceName);

        /// <summary>
        /// 移除资源组内所有资源信息。
        /// </summary>
        void RemoveAllResourceInfos();
    }
}