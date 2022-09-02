//------------------------------------------------------------
// QsPlus.Framework ??? ...最垃圾的框架...Fuck..!
// Copyright © 2022-2035 Shi Qi. All rights reserved.
// GitHub : https://github.com/ShiQi2022/QsPlus.Framework
// E-mail : www.shiqi.com@gmail.com
// QQ : 2581424471@qq.com
//------------------------------------------------------------

namespace QsPlus.Framework.Common
{
    /// <summary>
    /// 封装一个方法 - 该方法没有参数。
    /// </summary>
    public delegate void QsPlusFrameworkAction();

    /// <summary>
    /// 封装一个方法 - 该方法具有一个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs">参数类型。</typeparam>
    /// <param name="args">参数。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs>(TQsPlusFrameworkArgs args);

    /// <summary>
    /// 封装一个方法 - 该方法具有两个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2);

    /// <summary>
    /// 封装一个方法 - 该方法具有三个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs3">参数类型3。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    /// <param name="args3">参数3。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2, in TQsPlusFrameworkArgs3>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2, TQsPlusFrameworkArgs3 args3);

    /// <summary>
    /// 封装一个方法 - 该方法具有四个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs3">参数类型3。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs4">参数类型4。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    /// <param name="args3">参数3。</param>
    /// <param name="args4">参数4。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2, in TQsPlusFrameworkArgs3, in TQsPlusFrameworkArgs4>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2, TQsPlusFrameworkArgs3 args3, TQsPlusFrameworkArgs4 args4);

    /// <summary>
    /// 封装一个方法 - 该方法具有五个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs3">参数类型3。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs4">参数类型4。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs5">参数类型5。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    /// <param name="args3">参数3。</param>
    /// <param name="args4">参数4。</param>
    /// <param name="args5">参数5。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2, in TQsPlusFrameworkArgs3, in TQsPlusFrameworkArgs4, in TQsPlusFrameworkArgs5>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2, TQsPlusFrameworkArgs3 args3, TQsPlusFrameworkArgs4 args4, TQsPlusFrameworkArgs5 args5);

    /// <summary>
    /// 封装一个方法 - 该方法具有六个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs3">参数类型3。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs4">参数类型4。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs5">参数类型5。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs6">参数类型6。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    /// <param name="args3">参数3。</param>
    /// <param name="args4">参数4。</param>
    /// <param name="args5">参数5。</param>
    /// <param name="args6">参数6。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2, in TQsPlusFrameworkArgs3, in TQsPlusFrameworkArgs4, in TQsPlusFrameworkArgs5, in TQsPlusFrameworkArgs6>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2, TQsPlusFrameworkArgs3 args3, TQsPlusFrameworkArgs4 args4, TQsPlusFrameworkArgs5 args5, TQsPlusFrameworkArgs6 args6);

    /// <summary>
    /// 封装一个方法 - 该方法具有七个个参数。
    /// </summary>
    /// <typeparam name="TQsPlusFrameworkArgs1">参数类型1。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs2">参数类型2。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs3">参数类型3。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs4">参数类型4。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs5">参数类型5。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs6">参数类型6。</typeparam>
    /// <typeparam name="TQsPlusFrameworkArgs7">参数类型7。</typeparam>
    /// <param name="args1">参数1。</param>
    /// <param name="args2">参数2。</param>
    /// <param name="args3">参数3。</param>
    /// <param name="args4">参数4。</param>
    /// <param name="args5">参数5。</param>
    /// <param name="args6">参数6。</param>
    /// <param name="args7">参数7。</param>
    public delegate void QsPlusFrameworkAction<in TQsPlusFrameworkArgs1, in TQsPlusFrameworkArgs2, in TQsPlusFrameworkArgs3, in TQsPlusFrameworkArgs4, in TQsPlusFrameworkArgs5, in TQsPlusFrameworkArgs6, in TQsPlusFrameworkArgs7>(TQsPlusFrameworkArgs1 args1, TQsPlusFrameworkArgs2 args2, TQsPlusFrameworkArgs3 args3, TQsPlusFrameworkArgs4 args4, TQsPlusFrameworkArgs5 args5, TQsPlusFrameworkArgs6 args6, TQsPlusFrameworkArgs7 args7);
}