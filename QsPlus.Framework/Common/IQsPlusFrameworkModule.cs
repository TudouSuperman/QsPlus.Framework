//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 框架模块接口。
    /// </summary>
    internal interface IQsPlusFrameworkModule
    {
        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority { get; }

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime);

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        void QsPlusFrameworkModuleShutdown();
    }
}