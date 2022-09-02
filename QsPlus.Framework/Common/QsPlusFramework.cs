//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using System.Collections.Generic;

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// QsPlusFramework...Go..!
    /// </summary>
    public static class QsPlusFramework
    {
        /// <summary>
        /// 框架模块链表。
        /// </summary>
        private static readonly LinkedList<IQsPlusFrameworkModule> QsPlusFrameworkModules = new LinkedList<IQsPlusFrameworkModule>();
    }
}