//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Config
{
    /// <summary>
    /// 配置管理器接口。
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// 获取配置文件信息数量。
        /// </summary>
        int GetConfigFileInfoCount { get; }

        /// <summary>
        /// 是否存在配置文件信息。
        /// </summary>
        /// <param name="configName">配置文件名称。</param>
        /// <returns>是否存在配置文件信息。</returns>
        bool HasConfigFileInfo(string configName);

        /// <summary>
        /// 获取配置文件信息。
        /// </summary>
        /// <param name="configName">配置文件名称。</param>
        /// <returns>获取到的配置文件信息。</returns>
        string GetConfigFileInfo(string configName);

        /// <summary>
        /// 获取配置文件信息。
        /// </summary>
        /// <param name="configName">配置文件名称。</param>
        /// <param name="defaultValue">当无法获取到配置文件信息时，返回此默认值。</param>
        /// <returns>获取到的配置文件信息。</returns>
        string GetConfigFileInfo(string configName, string defaultValue);

        /// <summary>
        /// 添加配置文件信息。
        /// </summary>
        /// <param name="configName">配置文件名称。</param>
        /// <param name="configInfo">配置文件信息。</param>
        /// <returns>是否添加配置文件信息成功。</returns>
        bool AddConfigFileInfo(string configName, string configInfo);

        /// <summary>
        /// 移除配置文件信息。
        /// </summary>
        /// <param name="configName">配置文件名称。</param>
        /// <returns>是否移除配置文件信息成功。</returns>
        bool RemoveConfigFileInfo(string configName);

        /// <summary>
        /// 移除所有配置文件信息。
        /// </summary>
        void RemoveAllConfigFileInfos();
    }
}