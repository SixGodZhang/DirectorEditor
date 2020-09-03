﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    /// <summary>
    /// 此特性用于将UI和其对应的Loggic绑定起来
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public sealed class CustomLogicAttribute : Attribute
    {
        public Type uiType { get; set; }

        public CustomLogicAttribute(Type uiType)
        {
            this.uiType = uiType;
        }
    }
}
