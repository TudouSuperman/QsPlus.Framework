//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;
using QsPlus.Framework.Ui;

namespace QsPlus.Framework.UI
{
    /// <summary>
    /// 打开用户界面成功事件参数类。
    /// </summary>
    public sealed class OpenUiFormSuccessEventArgs : QsPlusFrameworkEventArgs
    {
        /// <summary>
        /// 初始化打开用户界面成功事件参数类的新实例。
        /// </summary>
        public OpenUiFormSuccessEventArgs()
        {
            UiForm = null;
            UserData = null;
        }

        /// <summary>
        /// 获取打开成功的界面。
        /// </summary>
        public IUiForm UiForm { get; private set; }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData { get; private set; }

        /// <summary>
        /// 创建打开界面成功事件。
        /// </summary>
        /// <param name="uiForm">加载成功的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的打开界面成功事件。</returns>
        public static OpenUiFormSuccessEventArgs Create(IUiForm uiForm, object userData)
        {
            OpenUiFormSuccessEventArgs e = InternalReferencePool.AcquireReference<OpenUiFormSuccessEventArgs>();
            e.UiForm = uiForm;
            e.UserData = userData;
            return e;
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public override void ClearReference()
        {
            UiForm = null;
            UserData = null;
        }
    }
}