using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
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
