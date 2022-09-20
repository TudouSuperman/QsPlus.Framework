//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Ui;

namespace QsPlus.Framework.UI
{
    /// <summary>
    /// 用户界面管理器接口。
    /// </summary>
    public interface IUiManager
    {
        /// <summary>
        /// 是否存在用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        bool HasUiForm(int uiFormId);

        /// <summary>
        /// 获取用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        IUiForm GetUiForm(int uiFormId);

        /// <summary>
        /// 打开用户界面。
        /// </summary>
        /// <param name="uiForm">要打开的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        void OpenUiForm(IUiForm uiForm, object userData);

        /// <summary>
        /// 打开用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void OpenUiForm(int uiFormId, object userData);

        /// <summary>
        /// 关闭用户界面。
        /// </summary>
        /// <param name="uiForm">要关闭的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        void CloseUiForm(IUiForm uiForm, object userData);

        /// <summary>
        /// 关闭用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void CloseUiForm(int uiFormId, object userData);

        /// <summary>
        /// 激活用户界面。
        /// </summary>
        /// <param name="uiForm">要激活的用户界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        void RefocusUiForm(IUiForm uiForm, object userData);

        /// <summary>
        /// 激活用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void RefocusUiForm(int uiFormId, object userData);
    }
}