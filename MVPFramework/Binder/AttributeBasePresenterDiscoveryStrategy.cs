using MVPFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 基于装饰器的查找策略
    /// </summary>
    public class AttributeBasePresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        /// <summary>
        /// 缓存已检索过类型的数据
        /// </summary>
        static readonly IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> typeToAttributeCache
            = new Dictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>>();


        /// <summary>
        /// 获取与View绑定的Presenter
        /// 返回结果是一个固定格式, Presenter可以在PresenterDiscovertResult中获取
        /// </summary>
        /// <param name="viewInstance"></param>
        /// <returns></returns>
        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            var messages = new List<string>();
            var bindings = new List<PresenterBinding>();

            var viewLogicType = viewInstance.GetType();
            // 获取View使用的装饰器组[Decorator group]
            var viewDefinedAttributes = GetAttributes(typeToAttributeCache, viewLogicType);

            // View没有使用装饰器
            if (viewDefinedAttributes.Empty())
            {
                messages.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    "could not find a [PresenterBinding] attribute on view instance {0}",
                    viewLogicType.FullName));
            }

            // 处理装饰器
            if (viewDefinedAttributes.Any())
            {
                foreach (var attribute in viewDefinedAttributes.OrderBy(a=>a.PresenterType.Name))
                {
                    // 如果viewType不是Attribute中的ViewType实例， 取下一个
                    if (!attribute.ViewLogicType.IsAssignableFrom(viewLogicType))
                    {
                        messages.Add(string.Format(
                            CultureInfo.InvariantCulture,
                            "found, but ignored, a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}) because the view type on the attribute is not compatible with the type of the view instance",
                            viewLogicType.FullName,
                            attribute.PresenterType.FullName,
                            attribute.ViewLogicType.FullName
                                         ));
                        continue;
                    }

                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "found a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2})",
                        viewLogicType.FullName,
                        attribute.PresenterType.FullName,
                        attribute.ViewLogicType.FullName
                                     ));

                    bindings.Add(new PresenterBinding(
                                     attribute.PresenterType,
                                     attribute.ViewLogicType,
                                     viewInstance
                                     ));
                }
            }
            else
            {
                return null;
            }

            // bindings 是 多个绑定关系的集合
            return new PresenterDiscoveryResult(
                viewInstance,
                "AttributeBasedPresenterDiscoveryStrategy:\r\n" +
                string.Join("\r\n", messages.Select(m => "- " + m).ToArray()),
                bindings
                );
        }

        /// <summary>
        /// 获取sourceType中的所有PresenterBindingAttribute属性, 并将结果保存在cache中
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        internal static IEnumerable<PresenterBindingAttribute> GetAttributes(IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> cache, Type sourceType)
        {
            var hostTypeHandle = sourceType.TypeHandle;

            return cache.GetOrCreateValue(hostTypeHandle, () =>
            {
                var attributes = sourceType
                    .GetCustomAttributes(typeof(PresenterBindingAttribute), true)
                    .OfType<PresenterBindingAttribute>()
                    .Select(pba =>
                    new PresenterBindingAttribute(pba.PresenterType)
                    {
                        ViewLogicType = pba.ViewLogicType ?? sourceType
                    })
                    .ToArray();

                return attributes;
            });
        }
    }
}
