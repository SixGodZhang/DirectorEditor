using System;

namespace MVPFramework
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true,Inherited =false)]
    public class ViewLogicBindingAttribute:Attribute
    {
        public ViewLogicBindingAttribute(Type viewLogicType)
        {
            ViewLogicType = viewLogicType;
        }

        public Type ViewLogicType { get; private set; }
    }
}
