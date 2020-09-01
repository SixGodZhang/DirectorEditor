using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    // Internal helper methods on System.Type
    public static class Types
    {
        private static readonly Type[] AmbiguousTypes = new[] { typeof(Type), typeof(string), typeof(Scope), typeof(Container) };

        // 完全限定名拼接
        private static readonly Func<Type[], string> FullyQualifiedNameArgumentsFormatter =
            args => string.Join(", ", args.Select(a => a.ToFriendlyName(fullQualifiedName: true)).ToArray());

        // 限定名拼接
        private static readonly Func<Type[], string> SimpleNameArgumentsFormatter =
            args => string.Join(", ", args.Select(a => a.ToFriendlyName(fullQualifiedName: false)).ToArray());

        // 简写限定名拼接
        private static readonly Func<Type[], string> CSharpFriendlyNameArgumentFormatter =
            args => string.Join(",", args.Select(argument => string.Empty).ToArray());

        // 该类型是否是为实例化的泛型 比如List<>这种
        internal static bool ContainsGenericParameter(this Type type) =>
            type.IsGenericParameter ||
                (type.IsGenericType() && type.GetGenericArguments().Any(ContainsGenericParameter));

        // 该类型是否是泛型类型参数 
        internal static bool IsGenericArgument(this Type type) =>
            type.IsGenericParameter || 
            (type.IsGenericType() || type.GetGenericArguments().Any(ContainsGenericParameter));

        // genericTypeDefinition 是否可以用 typeToCheck 替代, 是否存在继承、或者说相等关系
        internal static bool IsGenericTypeDefinitionOf(this Type genericTypeDefinition, Type typeToCheck) =>
            typeToCheck.IsGenericType() && typeToCheck.GetGenericTypeDefinition() == genericTypeDefinition;

        // 歧义类型 or 值类型
        internal static bool IsAmbiguousOrValueType(Type type) =>
            IsAmbiguousType(type) || type.IsValueType;

        //歧义类型
        internal static bool IsAmbiguousType(Type type) => AmbiguousTypes.Contains(type);

        // 此类型是泛型类型 && 未被指定具体类型
        internal static bool IsPartiallyClosed(this Type type) =>
            type.IsGenericType()
            && type.ContainsGenericParameters()
            && type.GetGenericTypeDefinition() != type;

        public static string ToCSharpFriendlyName(this Type genericTypeDefinition) =>
            ToCSharpFriendlyName(genericTypeDefinition, fullyQualifiedName: false);

        // 未被指定具体类型的泛型 获取限定名时, 用空代替
        public static string ToCSharpFriendlyName(this Type genericTypeDefinition, bool fullyQualifiedName)
        {
            Requires.IsNotNull(genericTypeDefinition, nameof(genericTypeDefinition));
            return genericTypeDefinition.ToFriendlyName(fullyQualifiedName, CSharpFriendlyNameArgumentFormatter);
        }

        // 递归去拼接一个类型的限定名
        // 例子:
        // Dictionary<int,string> -> Dictrionary`2
        // ---> Dcitionary<int, string>
        private static string ToFriendlyName(this Type type, bool fullyQualifiedName, Func<Type[], string> argumentsFormatter)
        {
            // 如果是数组
            if (type.IsArray)
            {
                return type.GetElementType().ToFriendlyName(fullyQualifiedName, argumentsFormatter) + "[]";
            }

            // 获取该类型的完全限定名称，包括其命名空间 - FullName
            // 获取当前成员的名称 - Name
            string name = fullyQualifiedName ? (type.FullName ?? type.Name) : type.Name;

            // 
            if (type.IsNested && !type.IsGenericParameter)
            {
                // 获取用来声明当前的嵌套类型或泛型类型参数的类型。
                name = type.DeclaringType.ToFriendlyName(fullyQualifiedName, argumentsFormatter) + "." + name;
            }

            // 获取泛型参数类型
            var genericArguments = GetGenericArguments(type);

            if (!genericArguments.Any())
            {
                return name;
            }

            name = name.Contains("`") ? name.Substring(0, name.LastIndexOf('`')) : name;
            return name + "<" + argumentsFormatter(genericArguments) + ">";
        }

        private static Type[] GetGenericArguments(Type type) =>
            type.IsNested
            ? type.GetGenericArguments().Skip(type.DeclaringType.GetGenericArguments().Length).ToArray()
            : type.GetGenericArguments();

        /// <summary>
        /// 获取一个类型直接获取限定名,是否是完全限定名可以公共fullQualifiedName来控制；
        /// 此方法对泛型同样适用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fullQualifiedName"></param>
        /// <returns></returns>
        public static string ToFriendlyName(this Type type, bool fullQualifiedName)
        {
            Requires.IsNotNull(type, nameof(type));

            return type.ToFriendlyName(
                fullQualifiedName,
                fullQualifiedName ? FullyQualifiedNameArgumentsFormatter : SimpleNameArgumentsFormatter);
        }

        // 判断一个类型是否是具体类型
        internal static bool IsConcreteType(Type serviceType) =>
            !serviceType.IsAbstract() && !serviceType.IsArray && serviceType != typeof(object) &&
            !typeof(Delegate).IsAssignableFrom(serviceType);

        // 是否是 具体&&可构造的类型
        internal static bool IsConcreteConstructableType(Type serviceType) =>
            !serviceType.ContainsGenericParameters() && IsConcreteType(serviceType);

        //internal static bool IsComposite(Type serviceType, ConstructorInfo implementationConstructor) =>
        //    CompositeHelpers.ComposesServiceType(serviceType, implementationConstructor);

        // 是否是泛型集合类型
        internal static bool IsGenericCollectionType(Type serviceType)
        {
            if (!serviceType.IsGenericType())
            {
                return false;
            }

            Type serviceTypeDefinition = serviceType.GetGenericTypeDefinition();
            return serviceTypeDefinition == typeof(IReadOnlyList<>) ||
                serviceTypeDefinition == typeof(IReadOnlyCollection<>) ||
                serviceTypeDefinition == typeof(IEnumerable<>) ||
                serviceTypeDefinition == typeof(ICollection<>) ||
                serviceTypeDefinition == typeof(IList<>);
        }

        // 获取一个类型的继承的所有类和实现的所有接口
        internal static ICollection<Type> GetTypeHierarchyFor(Type type)
        {
            var types = new List<Type>(4);
            types.Add(type);

            //types.AddRange(GetBaseTypes(type));
            //types.AddRange(type.GetInterfaces());
            types.AddRange(type.GetBaseTypesAndInterfaces());

            return types;
        }

        // 获取一个类型 继承链 上的所有基类型
        private static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Type baseType = type.BaseType() ?? (type != typeof(object) ? typeof(object) : null);
            while (baseType != null)
            {
                yield return baseType;
                baseType = baseType.BaseType();
            }
        }

        // 获取继承链上的所有类型和接口
        internal static IEnumerable<Type> GetBaseTypesAndInterfaces(this Type type) =>
            type.GetInterfaces().Concat(type.GetBaseTypes());

        // 判断serviceType 是否在 implementationType 继承链上的所有类型集合中
        internal static IEnumerable<Type> GetBaseTypeCandidates(Type serviceType, Type implementationType) =>
            from baseType in implementationType.GetBaseTypesAndInterfaces()
            where baseType == serviceType || (
            baseType.IsGenericType() && serviceType.IsGenericType() &&
            baseType.GetGenericTypeDefinition() == serviceType.GetGenericTypeDefinition())
            select baseType;


        //type 是否 是 otherType的变体(仅适用于泛型)
        private static bool IsVariantVersionOf(this Type type, Type otherType) =>
            type.IsGenericType()
            && otherType.IsGenericType()
            && type.GetGenericTypeDefinition() == otherType.GetGenericTypeDefinition()
            && type.IsAssignableFrom(otherType);

        // type 是否是serviceType的具体实现，或者type和serviceType的类型相同
        private static bool IsGenericImplementationOf(Type type, Type serviceType) =>
            type == serviceType
            || serviceType.IsVariantVersionOf(type)
            || (type.IsGenericType() && type.GetGenericTypeDefinition() == serviceType);

        // service 是否 是implemantation 的实现
        internal static bool ServiceIsAssignableFromImplementation(Type service, Type implementation)
        {
            // 1. 处理service不是泛型的情况
            if (!service.IsGenericType())
            {
                // 直接判断service是否可以从implementation构造
                return service.IsAssignableFrom(implementation);
            }

            // 2. 如果service是泛型, implemantation是泛型, 且 implementation 的泛型具体的类型是service
            // service --> ITestA<int> , implementation --> TestB<T> where T: ITestA<int>
            if (implementation.IsGenericType() && implementation.GetGenericTypeDefinition() == service)
            {
                return true;
            }

            // 下面这是处理 service是泛型, implementation不是泛型的情况

            // 这里不使用LINQ是为了避免一些不必要的内存分配
            // 不幸的是, 我们在调用GetInterfaces()的时候避免不了这个问题的
            // implementation实现的所有接口中, 是否有一个接口是等于service或者继承了service
            foreach (Type interfaceType in implementation.GetInterfaces())
            {
                if(IsGenericImplementationOf(interfaceType,service))
                {
                    return true;
                }
            }

            //implementation 的基类型中, 是否有对service的实现
            Type baseType = implementation.BaseType() ?? (implementation != typeof(object) ? typeof(object) : null);

            while(baseType != null)
            {
                if (IsGenericImplementationOf(baseType, service))
                {
                    return true;
                }

                baseType = baseType.BaseType();
            }

            return false;

        }



    }
}
