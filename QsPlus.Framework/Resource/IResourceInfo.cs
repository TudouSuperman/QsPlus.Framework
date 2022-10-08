//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Resource
{
    /// <summary>
    /// 资源信息接口。
    /// </summary>
    public interface IResourceInfo
    {
        /// <summary>
        /// 资源编号。
        /// </summary>
        int ResourceId { get; }

        /// <summary>
        /// 资源名称。
        /// </summary>
        string ResourceName { get; }

        /// <summary>
        /// 资源实例。
        /// </summary>
        object ResourceHandle { get; }

        /// <summary>
        /// 资源所属的资源组。
        /// </summary>
        IResourceGroup ResourceGroup { get; }
    }
}