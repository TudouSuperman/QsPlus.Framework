//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using QsPlus.Framework.Reference;

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

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public static void QsPlusFrameworkModulesUpdate(float logicTime, float actualTime)
        {
            foreach (IQsPlusFrameworkModule module in QsPlusFrameworkModules)
            {
                module.QsPlusFrameworkModuleUpdate(logicTime, actualTime);
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public static void QsPlusFrameworkModulesShutdown()
        {
            for (LinkedListNode<IQsPlusFrameworkModule> current = QsPlusFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.QsPlusFrameworkModuleShutdown();
            }

            QsPlusFrameworkModules.Clear();
            InternalReferencePool.ClearReferences();
        }

        /// <summary>
        /// 获取框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的框架模块类型。</typeparam>
        /// <returns>要获取的框架模块。</returns>
        /// <remarks>如果要获取的框架模块不存在，则自动创建该框架模块。</remarks>
        public static T GetQsPlusFrameworkModule<T>() where T : class
        {
            Type interfaceType = typeof(T);

            if (!interfaceType.IsInterface)
            {
                throw new QsPlusFrameworkException($"你必须通过接口获取模块，但 '{interfaceType.FullName}' 不是。");
            }

            if (!interfaceType.FullName.StartsWith("QsPlus.Framework.", StringComparison.Ordinal))
            {
                throw new QsPlusFrameworkException($"你必须获取框架内的模块，但 '{interfaceType.FullName}' 不是。");
            }

            string moduleName = $"{interfaceType.Namespace}.{interfaceType.Name.Substring(1)}";
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new QsPlusFrameworkException($"框架内找不到此模块类型 '{moduleName}' 。");
            }

            return GetQsPlusFrameworkModule(moduleType) as T;
        }

        /// <summary>
        /// 获取框架模块。
        /// </summary>
        /// <param name="moduleType">要获取的框架模块类型。</param>
        /// <returns>要获取的框架模块。</returns>
        /// <remarks>如果要获取的框架模块不存在，则自动创建该框架模块。</remarks>
        private static IQsPlusFrameworkModule GetQsPlusFrameworkModule(Type moduleType)
        {
            foreach (IQsPlusFrameworkModule module in QsPlusFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module;
                }
            }

            return CreateQsPlusFrameworkModule(moduleType);
        }

        /// <summary>
        /// 创建框架模块。
        /// </summary>
        /// <param name="moduleType">要创建的框架模块类型。</param>
        /// <returns>要创建的框架模块。</returns>
        private static IQsPlusFrameworkModule CreateQsPlusFrameworkModule(Type moduleType)
        {
            IQsPlusFrameworkModule module = (IQsPlusFrameworkModule) Activator.CreateInstance(moduleType);
            if (module == null)
            {
                throw new QsPlusFrameworkException($"无法创建此模块 '{moduleType.FullName}' 。");
            }

            LinkedListNode<IQsPlusFrameworkModule> current = QsPlusFrameworkModules.First;
            while (current != null)
            {
                if (module.QsPlusFrameworkModulePriority > current.Value.QsPlusFrameworkModulePriority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                QsPlusFrameworkModules.AddBefore(current, module);
            }
            else
            {
                QsPlusFrameworkModules.AddLast(module);
            }

            return module;
        }
    }
}