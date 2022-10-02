//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Config
{
    /// <summary>
    /// 配置管理器类。
    /// </summary>
    internal sealed class ConfigManager : IQsPlusFrameworkModule, IConfigManager
    {
        private readonly IDictionary<string, Config> _configs;

        /// <summary>
        /// 初始化配置管理器类的新实例。
        /// </summary>
        public ConfigManager()
        {
            _configs = new Dictionary<string, Config>();
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.ConfigManager;

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
            _configs.Clear();
        }

        /// <summary>
        /// 获取配置文件信息数量。
        /// </summary>
        public int GetConfigFileInfoCount => _configs.Count;

        /// <summary>
        /// 是否存在配置文件信息。
        /// </summary>
        /// <param name="configFileName">配置文件名称。</param>
        /// <returns>是否存在配置文件信息。</returns>
        public bool HasConfigFileInfo(string configFileName)
        {
            return _configs.ContainsKey(configFileName);
        }

        /// <summary>
        /// 获取配置文件信息。
        /// </summary>
        /// <param name="configFileName">配置文件名称。</param>
        /// <returns>获取到的配置文件信息。</returns>
        public IConfig GetConfigFileInfo(string configFileName)
        {
            if (_configs.TryGetValue(configFileName, out Config config))
            {
                return config;
            }

            return null;
        }

        /// <summary>
        /// 获取配置文件信息。
        /// </summary>
        /// <param name="configFileName">配置文件名称。</param>
        /// <param name="defaultValue">当无法获取到配置文件信息时，返回此默认值。</param>
        /// <returns>获取到的配置文件信息。</returns>
        public IConfig GetConfigFileInfo(string configFileName, string defaultValue)
        {
            if (_configs.TryGetValue(configFileName, out Config tempConfig))
            {
                return tempConfig;
            }

            tempConfig = Config.Create(configFileName, defaultValue);
            _configs.Add(configFileName, tempConfig);
            return tempConfig;
        }

        /// <summary>
        /// 添加配置文件信息。
        /// </summary>
        /// <param name="configFileName">配置文件名称。</param>
        /// <param name="configFileInfo">配置文件信息。</param>
        /// <returns>是否添加配置文件信息成功。</returns>
        public bool AddConfigFileInfo(string configFileName, string configFileInfo)
        {
            if (_configs.ContainsKey(configFileName))
            {
                return false;
            }

            Config tempConfig = Config.Create(configFileName, configFileInfo);
            _configs.Add(configFileName, tempConfig);
            return true;
        }

        /// <summary>
        /// 移除配置文件信息。
        /// </summary>
        /// <param name="configFileName">配置文件名称。</param>
        /// <returns>是否移除配置文件信息成功。</returns>
        public bool RemoveConfigFileInfo(string configFileName)
        {
            if (_configs.TryGetValue(configFileName, out Config tempConfig))
            {
                InternalReferencePool.ReleaseReference(tempConfig);
                _configs.Remove(configFileName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 移除所有配置文件信息。
        /// </summary>
        public void RemoveAllConfigFileInfos()
        {
            foreach (KeyValuePair<string, Config> itemConfig in _configs)
            {
                InternalReferencePool.ReleaseReference(itemConfig.Value);
            }

            _configs.Clear();
        }
    }
}