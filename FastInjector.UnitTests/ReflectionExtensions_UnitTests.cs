using System;
using FastInjector;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace FastInjector.UnitTests
{
    [TestFixture]
    public class ReflectionExtensions_UnitTests
    {
        /// <summary>
        /// 测试GenericTypeArguments和GenericTypeParameters的差异性
        /// </summary>
        [Test]
        public void GenericTypeArguments_GenericTypeParameters_Test_Diff()
        {
            List<int> list1 = new List<int>();// System.Collections.Generic.List`1<int32>
            list1.Add(1);
            Type listDef = typeof(List<>);// System.Collections.Generic.List`1
            var listStr = listDef.MakeGenericType(typeof(string));// System.Collections.Generic.List`1<string>

            // 当前实例的类型是否是泛型， 即IsGenericType == true, 是否成立
            // list1
            Assert.AreEqual(list1.GetType().GetTypeInfo().IsGenericType, true);
            Assert.AreEqual(list1.GetType().GetTypeInfo().IsGenericTypeDefinition, false);
            Type[] list1_arguments = list1.GetType().GetTypeInfo().GenericTypeArguments;//1
            Assert.AreEqual(list1_arguments.Length, 1);
            Type[] list1_parameters = list1.GetType().GetTypeInfo().GenericTypeParameters;//0
            Assert.AreEqual(list1_parameters.Length, 0);

            //listDef
            Assert.AreEqual(listDef.GetTypeInfo().IsGenericType, true);
            Assert.AreEqual(listDef.GetTypeInfo().IsGenericTypeDefinition, true);
            var type_arguments = listDef.GetTypeInfo().GenericTypeArguments;//0
            Assert.AreEqual(type_arguments.Length, 0);
            var type_parameters = listDef.GetTypeInfo().GenericTypeParameters;//1
            Assert.AreEqual(type_parameters.Length, 1);

            //listStr
            Assert.AreEqual(listStr.GetTypeInfo().IsGenericType, true);
            Assert.AreEqual(listStr.GetTypeInfo().IsGenericTypeDefinition, false);
            



            //GetGenericArguments
            //public virtual Type[] GenericTypeParameters
            //{
            //    get
            //    {
            //        if (IsGenericTypeDefinition)
            //        {
            //            return GetGenericArguments();
            //        }
            //        else
            //        {
            //            return Type.EmptyTypes;
            //        }

            //    }
            //}

        //GenericTypeArguments
        //public virtual Type[] GenericTypeArguments
        //{
        //    get
        //    {
        //        if (IsGenericType && !IsGenericTypeDefinition)
        //        {
        //            return GetGenericArguments();
        //        }
        //        else
        //        {
        //            return Type.EmptyTypes;
        //        }

        //    }
        //}
    }
    }
}
