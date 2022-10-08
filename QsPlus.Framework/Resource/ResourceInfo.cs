//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Resource
{
    /// <summary>
    /// 资源信息。
    /// </summary>
    internal sealed class ResourceInfo : IReference, IResourceInfo
    {
        /// <summary>
        /// 资源编号。
        /// </summary>
        public int ResourceId { get; private set; }

        /// <summary>
        /// 资源名称。
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// 资源实例。
        /// </summary>
        public object ResourceHandle { get; private set; }

        /// <summary>
        /// 资源所属的资源组。
        /// </summary>
        public IResourceGroup ResourceGroup { get; private set; }

        /// <summary>
        /// 初始化资源信息类的新实例。
        /// </summary>
        public ResourceInfo()
        {
            ResourceId = 0;
            ResourceName = null;
            ResourceHandle = null;
            ResourceGroup = null;
        }

        /// <summary>
        /// 创建资源信息类。
        /// </summary>
        /// <param name="resourceId">资源编号。</param>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="resourceHandle">资源实例。</param>
        /// <param name="resourceGroup">资源所属的资源组。</param>
        /// <returns>创建的资源信息类。</returns>
        public static ResourceInfo Create(int resourceId, string resourceName, object resourceHandle, IResourceGroup resourceGroup)
        {
            ResourceInfo tempResourceInfo = InternalReferencePool.AcquireReference<ResourceInfo>();
            tempResourceInfo.ResourceId = resourceId;
            tempResourceInfo.ResourceName = resourceName;
            tempResourceInfo.ResourceHandle = resourceHandle;
            tempResourceInfo.ResourceGroup = resourceGroup;
            return tempResourceInfo;
        }

        /// <summary>
        /// 清理 ResourceInfo 引用。
        /// </summary>
        public void ClearReference()
        {
            ResourceId = 0;
            ResourceName = null;
            ResourceHandle = null;
            ResourceGroup = null;
        }
    }
}