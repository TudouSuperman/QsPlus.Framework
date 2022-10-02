//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Reference;

namespace QsPlus.Framework.Config
{
    /// <summary>
    /// 配置类。
    /// </summary>
    internal sealed class Config : IConfig, IReference
    {
        /// <summary>
        /// 配置文件名。
        /// </summary>
        public string ConfigFileName { get; private set; }

        /// <summary>
        /// 配置文件信息。
        /// </summary>
        public string ConfigFileInfo { get; private set; }

        /// <summary>
        /// 初始化配置类的新实例。
        /// </summary>
        public Config()
        {
            ConfigFileName = null;
            ConfigFileInfo = null;
        }

        /// <summary>
        /// 创建配置类的新实例。
        /// </summary>
        /// <param name="configFileName">配置文件名。</param>
        /// <param name="configFileInfo">配置文件信息。</param>
        /// <returns>创建的配置类。</returns>
        public static Config Create(string configFileName, string configFileInfo)
        {
            Config tempConfig = InternalReferencePool.AcquireReference<Config>();
            tempConfig.ConfigFileName = configFileName;
            tempConfig.ConfigFileInfo = configFileInfo;
            return tempConfig;
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public void ClearReference()
        {
            ConfigFileName = null;
            ConfigFileInfo = null;
        }
    }
}