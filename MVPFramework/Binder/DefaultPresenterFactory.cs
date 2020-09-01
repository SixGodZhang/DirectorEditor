using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// default presenter factory
    /// 在presenterType 中查找一个viewType参数类型的构造函数, 然后将viewInstance作为实例传递进去， 创建一个实例对象返回
    /// </summary>
    public class DefaultPresenterFactory : IPresenterFactory
    {
        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            if (presenterType == null)
                throw new ArgumentNullException("presenterType");
            if (viewType == null)
                throw new ArgumentNullException("viewType");
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            var buildMethod = GetBuildMethod(presenterType, viewType);// 获取构造函数

            try
            {
                // 调用构造函数 创建实例对象
                return (IPresenter)buildMethod.Invoke(null, new[] { viewInstance });
            }catch(Exception ex)
            {
                var orginException = ex;
                if (ex is TargetInvocationException && ex.InnerException != null)
                    orginException = ex.InnerException;

                throw new InvalidOperationException
                    (
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "An exception was thrown whilst trying to create an instance of {0}. Check the InnerException for more information.",
                        presenterType.FullName),
                       orginException
                    );
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
        /// <param name="viewType"></param>
        /// <returns></returns>
        internal static DynamicMethod GetBuildMethod(Type presenterType ,Type viewType)
        {
            var cacheKey = string.Join("__:__", new[]
            {
                presenterType.AssemblyQualifiedName,
                viewType.AssemblyQualifiedName
            });

            return buildMethodCache.GetOrCreateValue(cacheKey,
                () => GetBuildMethodInternal(presenterType, viewType));
        }

        /// <summary>
        /// 创建一个动态方法
        /// presenterType: 返回类型
        /// viewType: 参数类型
        /// </summary>
        /// <param name="presenterType"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        internal static DynamicMethod GetBuildMethodInternal(Type presenterType, Type viewType)
        {
            if(presenterType.IsNotPublic)
            {
                throw new ArgumentException(string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} does not meet accessibility requirements. For the WebFormsMvp framework to be able to call it, it must be public. Make the type public, or set PresenterBinder.Factory to an implementation that can access this type.",
                    presenterType.FullName),
                    "presenterType");
            }

            //查找presenterType中指定参数类型viewType的构造函数
            var constructor = presenterType.GetConstructor(new[] { viewType });
            if (constructor == null)
            {
                throw new ArgumentException(string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} is missing an expected constructor, or the constructor is not accessible. We tried to execute code equivalent to: new {0}({1} view). Add a public constructor with a compatible signature, or set PresenterBinder.Factory to an implementation that can supply constructor dependencies.",
                    presenterType.FullName,
                    viewType.FullName),
                    "presenterType");
            }

            // 根据构造函数创建一个动态方法
            var dynamicMethod = new DynamicMethod("DynamicConstructor", presenterType, new[] { viewType }, presenterType.Module, false);
            var ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Newobj, constructor);
            ilGenerator.Emit(OpCodes.Ret);

            return dynamicMethod;
        }
    }
}
