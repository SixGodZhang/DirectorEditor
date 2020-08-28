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


    }
}
