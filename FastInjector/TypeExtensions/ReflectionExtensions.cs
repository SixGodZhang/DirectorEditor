using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FastInjector
{
    /// <summary>
    /// 反射类的一些扩展方法
    /// </summary>
    internal static class ReflectionExtensions
    {
        // 获取一个属性的set方法
        // nonPublic： 如果为false,set 方法为非public, 则返回null
        public static MethodInfo GetSetMethod(this PropertyInfo property, bool nonPublic = true) =>
            nonPublic || property.SetMethod?.IsPublic == true ? property.SetMethod : null;

        // 获取一个属性的get方法
        public static MethodInfo GetGetMethod(this PropertyInfo property, bool nonPublic = true) =>
            nonPublic || property.SetMethod?.IsPublic == true ? property.GetMethod : null;

        //获取泛型参数的类型--> 下面这两个属性有啥区别， 具体可以在单元测试中体会一下区别
        public static Type[] GetGenericArguments(this Type type) => type.GetTypeInfo().IsGenericTypeDefinition
            ? type.GetTypeInfo().GenericTypeParameters
            : type.GetTypeInfo().GenericTypeArguments;

        // 查找指定名称的方法, 如果有多个, 则会引发异常
        public static MethodInfo GetMethod(this Type type, string name) => type.GetTypeInfo().DeclaredMethods.Single(m => m.Name == name);

        // 返回Assembly中所有的Type
        public static Type[] GetTypes(this Assembly assembly) => assembly.DefinedTypes.Select(i => i.AsType()).ToArray();

        // 获取指定成员(可以包含多个同名)
        public static MemberInfo[] GetMember(this Type type, string name) => type.GetTypeInfo().DeclaredMembers.Where(m => m.Name == name).ToArray();

        // 获取类型中实现的所有接口
        public static Type[] GetInterfaces(this Type type) => type.GetTypeInfo().ImplementedInterfaces.ToArray();

        // 是否为泛型类型
        public static bool IsGenericType(this Type type) => type.GetTypeInfo().IsGenericType;

        // 是否为值类型
        public static bool IsValueType(this Type type) => type.GetTypeInfo().IsValueType;

        // 是否是抽象类型
        public static bool IsAbstract(this Type type) => type.GetTypeInfo().IsAbstract;

        // other是否可分配给当前的类型
        public static bool IsAssignableFrom(this Type type, Type other) => type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());

        //对象是否具有尚未被特定类型替代的类型参数
        public static bool ContainsGenericParameters(this Type type) => type.GetTypeInfo().ContainsGenericParameters;

        // 父类型
        public static Type BaseType(this Type type) => type.GetTypeInfo().BaseType;

        // 是否是基元类型
        public static bool IsPrimitive(this Type type) => type.GetTypeInfo().IsPrimitive;

        // 是否为嵌套的公共类型
        public static bool IsNestedPublic(this Type type) => type.GetTypeInfo().IsNestedPublic;

        // 是否是公开类型
        public static bool IsPublic(this Type type) => type.GetTypeInfo().IsPublic;

        //返回表示当前泛型类型参数约束的 System.Type 对象的数组
        public static Type[] GetGenericParameterConstraints(this Type type) => type.GetTypeInfo().GetGenericParameterConstraints();

        //获取一个值，通过该值指示 System.Type 是否是一个类或委托；即，不是值类型或接口。
        public static bool IsClass(this Type type) => type.GetTypeInfo().IsClass;

        //获取一个值，通过该值指示 System.Type 是否是一个接口；即，不是类或值类型。
        public static bool IsInterface(this Type type) => type.GetTypeInfo().IsInterface;

        //获取一个值，该值指示当前 System.Type 是否表示泛型类型或方法的定义中的类型参数。
        public static bool IsGenericParameter(this Type type) => type.GetTypeInfo().IsGenericParameter;

        // 获取描述当前泛型类型参数的协变和特殊约束的 System.Reflection.GenericParameterAttributes 标志。
        public static GenericParameterAttributes GetGenericParameterAttributes(this Type type) => type.GetTypeInfo().GenericParameterAttributes;

        // 获取声明该类型的Assembly
        public static Assembly GetAssembly(this Type type) => type.GetTypeInfo().Assembly;

        // 由当前类型定义的属性的集合
        public static PropertyInfo[] GetProperties(this Type type) => type.GetTypeInfo().DeclaredProperties.ToArray();

        //获取与 System.Type 关联的 GUID。
        public static Guid GetGuid(this Type type) => type.GetTypeInfo().GUID;

        // 获取由当前类型声明的构造函数的集合
        public static ConstructorInfo[] GetConstructors(this Type type) =>
            type.GetTypeInfo().DeclaredConstructors.Where(ctor => !ctor.IsStatic && ctor.IsPublic).ToArray();

        // 获取指定序列类型参数的构造函数
        public static ConstructorInfo GetConstructor(this Type type, Type[] types) => (
            from constructor in type.GetTypeInfo().DeclaredConstructors
            where types.SequenceEqual(constructor.GetParameters().Select(p => p.ParameterType))
            select constructor)
            .FirstOrDefault();

        //private static string TypeName(this Type type) => type.ToFri





    }
}
