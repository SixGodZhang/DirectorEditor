using System;
using System.Globalization;

namespace MVPFramework
{
    /// <summary>
    /// 框架中使用的一些字符串
    /// </summary>
    internal static class StringResources
    {
        /// <summary>
        /// 不能在一个ViewLogic实例上找到一个[PresenterBinding]特性
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public static string NotFoundPresenterByAttribute(Type viewLogicType) => Format(
            "could not find a [PresenterBinding] attribute on viewlogic instance {0}", viewLogicType.FullName);

        /// <summary>
        /// 根据类型名不能找到Presenter
        /// </summary>
        /// <param name="presenterTypeName"></param>
        /// <returns></returns>
        public static string NotFoundPresenterByType(string presenterTypeName) => Format(
            "could not find a presenter with type name {0}", presenterTypeName);

        /// <summary>
        /// 没有实现Ipresenter接口
        /// </summary>
        /// <param name="presenterTypeName"></param>
        /// <returns></returns>
        public static string NotImplementIPresenter(string presenterTypeName) => Format(
            "found, but ignored, potential presenter with type name {0} because it does not implement IPresenter", presenterTypeName);

        /// <summary>
        /// 找到了Presenter
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FoundPresenter(string presenterTypeName) =>
            Format("found presenter with type name {0}", presenterTypeName);

        /// <summary>
        /// 参数不能为null
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ParamIsNull(string param) =>
            Format("{0} shouldn't be null, please check it!", param);

        /// <summary>
        /// 调用CompositePresenterDiscoveryStrategy构造函数时, 参数不能为null
        /// </summary>
        /// <returns></returns>
        public static string StrategiesEqualNullInCompositeConstructor() =>
            "strategies shouldn't be null when call composite's constructor";

        /// <summary>
        /// 在组合策略中,找不到任何可用的策略
        /// </summary>
        /// <returns></returns>
        public static string NotFoundAnyStrategyInCompositeStrategy() =>
            "not found any strategy in composite strategy";

        /// <summary>
        /// 反射时, 该属性不可访问
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string TypeIsNotPublic(string typeName) =>
            Format("{0} does not meet accessibility requirements", typeName);

        /// <summary>
        /// 在创建Presenter实例的时候, 找不到指定参数的Presenter的构造函数
        /// </summary>
        /// <param name="presenterType"></param>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public static string NotFoundExpectedConstructorWhenCreatePresenter(Type presenterType, Type viewLogicType) =>
            Format("not found expected constructor -{0} when create {1} Presenter", presenterType.FullName, viewLogicType.FullName);


        /// <summary>
        /// 创建Presenter时,抛出异常
        /// </summary>
        /// <param name="presenterTypeName"></param>
        /// <returns></returns>
        public static string ThrowInvalidOperationExceptionWhenCreatePresenter(string presenterTypeName) =>
            Format("An exception was thrown while trying to create an instance of {0}. Check the InnerException for more information."
                , presenterTypeName);

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string Format(string format, params object[] args) =>
            string.Format(CultureInfo.InvariantCulture, format, args);
    }
}
