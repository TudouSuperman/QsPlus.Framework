//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
//------------------------------------------------------------

namespace QsPlus.Framework.Ui
{
    /// <summary>
    /// 用户界面接口。
    /// </summary>
    public interface IUiForm
    {
        /// <summary>
        /// 获取用户界面序列编号。
        /// </summary>
        int UiFormId { get; }

        /// <summary>
        /// 获取用户界面实例。
        /// </summary>
        object Handle { get; }

        /// <summary>
        /// 获取用户界面的深度。
        /// </summary>
        int DepthInUiForm { get; }

        /// <summary>
        /// 初始化用户界面。
        /// </summary>
        /// <param name="uiFormId">用户界面序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void OnInit(int uiFormId, object userData);

        /// <summary>
        /// 用户界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        void OnOpen(object userData);

        /// <summary>
        /// 用户界面轮询。
        /// </summary>
        /// <param name="logicTime">逻辑时间。</param>
        /// <param name="actualTime">真实时间。</param>
        void OnUpdate(float logicTime, float actualTime);

        /// <summary>
        /// 用户界面暂停。
        /// </summary>
        void OnPause();

        /// <summary>
        /// 用户界面暂停恢复。
        /// </summary>
        void OnResume();

        /// <summary>
        /// 用户界面遮挡。
        /// </summary>
        void OnCover();

        /// <summary>
        /// 用户界面遮挡恢复。
        /// </summary>
        void OnReveal();

        /// <summary>
        /// 用户界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        void OnRefocus(object userData);

        /// <summary>
        /// 用户界面关闭。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        void OnClose(object userData);

        /// <summary>
        /// 用户界面回收。
        /// </summary>
        void OnRecycle();

        /// <summary>
        /// 用户界面深度改变。
        /// </summary>
        /// <param name="uiFormDepth">用户界面深度。</param>
        void OnDepthChanged(int uiFormDepth);
    }
}