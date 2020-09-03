using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 绑定Presenter到一个View上
    /// 此装饰器在View上使用。 同一个View可以使用多个不同的装饰器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public sealed class PresenterBindingAttribute:Attribute
    {
        public PresenterBindingAttribute(Type presenterType)
        {
            PresenterType = presenterType;

        }
        public Type PresenterType { get; private set; }
        public Type ViewLogicType { get; set; }
    }
}
