using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    /// <summary>
    /// 此参数阻止Registration用指定的构造参数调用容器中的表达式
    /// </summary>
    internal struct OverriddenParameter
    {
        internal readonly ParameterInfo Parameter;

        internal readonly ConstantExpression PlaceHolder;

        internal readonly Expression Expression;

        internal readonly InstanceProducer Producer;

        internal OverriddenParameter(ParameterInfo parameter, Expression expression, InstanceProducer producer)
        {
            this.Parameter = parameter;
            this.Expression = expression;
            this.Producer = producer;
            this.PlaceHolder = System.Linq.Expressions.Expression.Constant(null, parameter.ParameterType);
        }


    }
}
