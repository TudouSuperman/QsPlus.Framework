//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using System.Collections.Generic;
using QsPlus.Framework.Common;
using QsPlus.Framework.Ui;

namespace QsPlus.Framework.UI
{
    /// <summary>
    /// 用户界面管理器类。
    /// </summary>
    internal sealed class UiManager : IQsPlusFrameworkModule, IUiManager
    {
        private readonly IDictionary<int, IUiForm> _mUiForms;
        private readonly Stack<IUiForm> _mOpenUiForms;
        private readonly Queue<IUiForm> _mRecycleUiForms;
        private QsPlusFrameworkEventHandler<OpenUiFormSuccessEventArgs> _mOpenUiFormSuccessEventHandler;
        private QsPlusFrameworkEventHandler<OpenUiFormFailureEventArgs> _mOpenUiFormFailureEventHandler;
        private QsPlusFrameworkEventHandler<CloseUiFormCompleteEventArgs> _mCloseUiFormCompleteEventHandler;

        /// <summary>
        /// 初始化用户界面管理器类的新实例。
        /// </summary>
        public UiManager()
        {
            _mUiForms = new Dictionary<int, IUiForm>();
            _mOpenUiForms = new Stack<IUiForm>();
            _mRecycleUiForms = new Queue<IUiForm>();
            _mOpenUiFormSuccessEventHandler = null;
            _mOpenUiFormFailureEventHandler = null;
            _mCloseUiFormCompleteEventHandler = null;
        }

        /// <summary>
        /// 框架模块优先级。
        /// </summary>
        public QsPlusFrameworkModulePriority QsPlusFrameworkModulePriority => QsPlusFrameworkModulePriority.UiManager;

        /// <summary>
        /// 框架模块轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        public void QsPlusFrameworkModuleUpdate(float logicTime, float actualTime)
        {
            while (_mRecycleUiForms.Count > 0)
            {
                IUiForm uiForm = _mRecycleUiForms.Dequeue();
                uiForm.OnRecycle();
            }

            foreach (KeyValuePair<int, IUiForm> uiGroup in _mUiForms)
            {
                uiGroup.Value.OnUpdate(logicTime, actualTime);
            }
        }

        /// <summary>
        /// 框架模块关闭。
        /// </summary>
        public void QsPlusFrameworkModuleShutdown()
        {
            _mOpenUiFormSuccessEventHandler = null;
            _mOpenUiFormFailureEventHandler = null;
            _mCloseUiFormCompleteEventHandler = null;
            _mRecycleUiForms.Clear();
            _mUiForms.Clear();
        }

        /// <summary>
        /// 获取当前所有用户界面数量。
        /// </summary>
        public int GetAllUiFromCount => _mUiForms.Count;

        /// <summary>
        /// 打开界面成功事件。
        /// </summary>
        public event QsPlusFrameworkEventHandler<OpenUiFormSuccessEventArgs> OpenUiFormSuccess
        {
            add => _mOpenUiFormSuccessEventHandler += value;
            remove => _mOpenUiFormSuccessEventHandler -= value;
        }

        /// <summary>
        /// 打开界面失败事件。
        /// </summary>
        public event QsPlusFrameworkEventHandler<OpenUiFormFailureEventArgs> OpenUiFormFailure
        {
            add => _mOpenUiFormFailureEventHandler += value;
            remove => _mOpenUiFormFailureEventHandler -= value;
        }

        /// <summary>
        /// 关闭界面完成事件。
        /// </summary>
        public event QsPlusFrameworkEventHandler<CloseUiFormCompleteEventArgs> CloseUiFormComplete
        {
            add => _mCloseUiFormCompleteEventHandler += value;
            remove => _mCloseUiFormCompleteEventHandler -= value;
        }

        /// <summary>
        /// 是否存在用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        public bool HasUiForm(int uiFormId)
        {
            return InternalGetUiForm(uiFormId) == null;
        }

        /// <summary>
        /// 获取用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        public IUiForm GetUiForm(int uiFormId)
        {
            return InternalGetUiForm(uiFormId);
        }

        /// <summary>
        /// 打开用户界面。
        /// </summary>
        /// <param name="uiForm">要打开的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void OpenUiForm(IUiForm uiForm, object userData)
        {
            InternalGetUiForm(uiForm.UiFormId).OnOpen(userData);
        }

        /// <summary>
        /// 打开用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void OpenUiForm(int uiFormId, object userData)
        {
            InternalGetUiForm(uiFormId).OnOpen(userData);
        }

        /// <summary>
        /// 关闭用户界面。
        /// </summary>
        /// <param name="uiForm">要关闭的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUiForm(IUiForm uiForm, object userData)
        {
            InternalGetUiForm(uiForm.UiFormId).OnClose(userData);
        }

        /// <summary>
        /// 关闭用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUiForm(int uiFormId, object userData)
        {
            InternalGetUiForm(uiFormId).OnClose(userData);
        }

        /// <summary>
        /// 激活用户界面。
        /// </summary>
        /// <param name="uiForm">要激活的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUiForm(IUiForm uiForm, object userData)
        {
            InternalGetUiForm(uiForm.UiFormId).OnRefocus(userData);
        }

        /// <summary>
        /// 激活用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUiForm(int uiFormId, object userData)
        {
            InternalGetUiForm(uiFormId).OnRefocus(userData);
        }

        /// <summary>
        /// 内部获取用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <returns></returns>
        private IUiForm InternalGetUiForm(int uiFormId)
        {
            if (_mUiForms.TryGetValue(uiFormId, out IUiForm uiForm))
            {
                return uiForm;
            }
            else
            {
                return null;
            }
        }
    }
}