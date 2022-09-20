//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;
using QsPlus.Framework.Ui;

namespace QsPlus.Framework.UI
{
    /// <summary>
    /// 关闭用户界面完成事件参数类。
    /// </summary>
    public class CloseUiFormCompleteEventArgs : QsPlusFrameworkEventArgs
    {
        /// <summary>
        /// 初始化关闭用户界面完成事件参数类的新实例。
        /// </summary>
        public CloseUiFormCompleteEventArgs()
        {
            UiForm = null;
            UserData = null;
        }

        /// <summary>
        /// 获取关闭成功的界面。
        /// </summary>
        public IUiForm UiForm { get; private set; }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData { get; private set; }

        /// <summary>
        /// 创建关闭用户界面完成事件。
        /// </summary>
        /// <param name="uiForm">加载成功的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的打开界面成功事件。</returns>
        public static CloseUiFormCompleteEventArgs Create(IUiForm uiForm, object userData)
        {
            CloseUiFormCompleteEventArgs e = InternalReferencePool.AcquireReference<CloseUiFormCompleteEventArgs>();
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