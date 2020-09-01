using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 绑定Presenter到一个View上
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public sealed class PresenterBindingAttribute:Attribute
    {
        public PresenterBindingAttribute(Type presenterType)
        {
            PresenterType = presenterType;

        }
        public Type PresenterType { get; private set; }
        public Type ViewType { get; set; }
    }
}
