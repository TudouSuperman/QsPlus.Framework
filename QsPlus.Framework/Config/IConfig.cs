//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Config
{
    /// <summary>
    /// 配置接口。
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 配置文件名。
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        /// 配置文件信息。
        /// </summary>
        string ConfigFileInfo { get; }
    }
}