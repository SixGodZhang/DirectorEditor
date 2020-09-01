using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// 这个cs文件保存一些还没有来得及实现的类
namespace FastInjector
{

    // InstanceProducer
    public class InstanceProducer
    {
        // ToFriendlyName
        public Registration Registration { get; private set; }
    }

    public class TransientLifestyle : Lifestyle
    { }

    public abstract class ScopedLifestyle : Lifestyle
    { }

    public class ScopedProxyLifestyle: ScopedLifestyle
    { }

    public class SingletonLifestyle : Lifestyle
    { }

    public sealed class UnknownLifestyle : Lifestyle
    { }

  



    // InjectionTargetInfo
    public class InjectionTargetInfo
    {
        public string Name;

        public InjectionTargetInfo(ParameterInfo parameter)
        {

        }

        public InjectionTargetInfo(PropertyInfo property)
        {

        }
    }

}
