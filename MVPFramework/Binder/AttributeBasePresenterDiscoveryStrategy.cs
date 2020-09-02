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
        static readonly IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> typeToAttributeCache
            = new Dictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>>();


        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            var messages = new List<string>();
            var bindings = new List<PresenterBinding>();

            var viewType = viewInstance.GetType();
            var viewDefinedAttributes = GetAttributes(typeToAttributeCache, viewType);

            if (viewDefinedAttributes.Empty())
            {
                messages.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    "could not find a [PresenterBinding] attribute on view instance {0}",
                    viewType.FullName
                                 ));
            }

            if (viewDefinedAttributes.Any())
            {
                foreach (var attribute in viewDefinedAttributes.OrderBy(a=>a.PresenterType.Name))
                {
                    if (!attribute.ViewType.IsAssignableFrom(viewType))
                    {
                        messages.Add(string.Format(
                            CultureInfo.InvariantCulture,
                            "found, but ignored, a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}) because the view type on the attribute is not compatible with the type of the view instance",
                            viewType.FullName,
                            attribute.PresenterType.FullName,
                            attribute.ViewType.FullName
                                         ));
                        continue;
                    }

                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "found a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2})",
                        viewType.FullName,
                        attribute.PresenterType.FullName,
                        attribute.ViewType.FullName
                                     ));

                    bindings.Add(new PresenterBinding(
                                     attribute.PresenterType,
                                     attribute.ViewType,
                                     viewInstance
                                     ));
                }
            }
            else
            {
                return null;
            }

            return new PresenterDiscoveryResult(
                new[] { bindings.Single().ViewInstance },
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
                        ViewType = pba.ViewType ?? sourceType
                    })
                    .ToArray();

                return attributes;
            });

        }
    }
}
