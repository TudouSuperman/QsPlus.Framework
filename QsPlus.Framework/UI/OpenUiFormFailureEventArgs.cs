//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

using QsPlus.Framework.Common;
using QsPlus.Framework.Reference;

namespace QsPlus.Framework.UI
{
    /// <summary>
    /// 打开用户界面失败事件参数类。
    /// </summary>
    public sealed class OpenUiFormFailureEventArgs : QsPlusFrameworkEventArgs
    {
        /// <summary>
        /// 初始化打开用户界面失败事件参数类的新实例。
        /// </summary>
        public OpenUiFormFailureEventArgs()
        {
            SerialId = 0;
            UiFormAssetName = null;
            UserData = null;
        }

        /// <summary>
        /// 获取用户界面序列编号。
        /// </summary>
        public int SerialId { get; private set; }

        /// <summary>
        /// 获取用户界面资源名称。
        /// </summary>
        public string UiFormAssetName { get; private set; }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData { get; private set; }

        /// <summary>
        /// 创建打开用户界面失败事件。
        /// </summary>
        /// <param name="serialId">用户界面序列编号。</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的打开用户界面失败事件。</returns>
        public static OpenUiFormFailureEventArgs Create(int serialId, string uiFormAssetName, object userData)
        {
            OpenUiFormFailureEventArgs e = InternalReferencePool.AcquireReference<OpenUiFormFailureEventArgs>();
            e.SerialId = serialId;
            e.UiFormAssetName = uiFormAssetName;
            e.UserData = userData;
            return e;
        }

        /// <summary>
        /// 清理引用(释放时调用)。
        /// </summary>
        public override void ClearReference()
        {
            SerialId = 0;
            UiFormAssetName = null;
            UserData = null;
        }
    }
}