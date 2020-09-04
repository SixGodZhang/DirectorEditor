using MVPFramework.Extensions;
using MVPFramework.Resources;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MVPFramework.Binder
{
    /// <summary>
    /// default presenter factory
    /// 在presenterType 中查找一个viewType参数类型的构造函数, 然后将viewInstance作为实例传递进去， 创建一个实例对象返回
    /// </summary>
    public class DefaultPresenterFactory : IPresenterFactory
    {
        public IPresenter Create(Type presenterType, Type viewLogicType, IViewLogic viewLogicInstance)
        {
            if (presenterType == null)
                throw new ArgumentNullException(StringResources.ParamIsNull("presenterType"));
            if (viewLogicType == null)
                throw new ArgumentNullException(StringResources.ParamIsNull("viewLogicType"));
            if (viewLogicInstance == null)
                throw new ArgumentNullException(StringResources.ParamIsNull("viewLogicInstance"));

            var buildMethod = GetBuildMethod(presenterType, viewLogicType);// 获取构造函数
            try
            {
                // 调用构造函数 创建实例对象
                return (IPresenter)buildMethod.Invoke(null, new[] { viewLogicInstance });
            }catch(Exception ex)
            {
                var orginException = ex;
                if (ex is TargetInvocationException && ex.InnerException != null)
                    orginException = ex.InnerException;

                throw new InvalidOperationException
                    (StringResources.ThrowInvalidOperationExceptionWhenCreatePresenter(presenterType.FullName),orginException);
            }
        }

        public void Release(IPresenter presenter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 构造函数的缓存
        /// </summary>
        static readonly IDictionary<string, DynamicMethod> buildMethodCache = new Dictionary<string, DynamicMethod>();

        /// <summary>
        /// 获取构造函数
        /// </summary>
        /// <param name="presenterType"></param>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        internal static DynamicMethod GetBuildMethod(Type presenterType ,Type viewLogicType)
        {
            var cacheKey = string.Join("__:__", new[]
            {
                presenterType.AssemblyQualifiedName,
                viewLogicType.AssemblyQualifiedName
            });

            return buildMethodCache.GetOrCreateValue(cacheKey,
                () => GetBuildMethodInternal(presenterType, viewLogicType));
        }

        /// <summary>
        /// 创建一个动态方法
        /// presenterType: 返回类型
        /// viewType: 参数类型
        /// </summary>
        /// <param name="presenterType"></param>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        internal static DynamicMethod GetBuildMethodInternal(Type presenterType, Type viewLogicType)
        {
            if(presenterType.IsNotPublic)
            {
                throw new ArgumentException(StringResources.TypeIsNotPublic(presenterType.FullName));
            }

            //查找presenterType中指定参数类型viewType的构造函数
            var constructor = presenterType.GetConstructor(new[] { viewLogicType });
            if (constructor == null)
            {
                throw new ArgumentException(StringResources.NotFoundExpectedConstructorWhenCreatePresenter(presenterType,viewLogicType));
            }

            // 根据构造函数创建一个动态方法
            var dynamicMethod = new DynamicMethod("DynamicConstructor", presenterType, new[] { viewLogicType }, presenterType.Module, false);
            var ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Newobj, constructor);
            ilGenerator.Emit(OpCodes.Ret);

            return dynamicMethod;
        }
    }
}
