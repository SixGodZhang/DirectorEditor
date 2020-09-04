using MVPFramework.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 组合寻找Presenter策略
    /// </summary>
    public class CompositePresenterDiscoveryStrategy:IPresenterDiscoveryStrategy
    {
        /// <summary>
        /// 组合List
        /// </summary>
        readonly IEnumerable<IPresenterDiscoveryStrategy> strategies;

        public CompositePresenterDiscoveryStrategy(params IPresenterDiscoveryStrategy[] strategies)
            :this((IEnumerable<IPresenterDiscoveryStrategy>) strategies)
        {

        }

        public CompositePresenterDiscoveryStrategy(IEnumerable<IPresenterDiscoveryStrategy> strategies)
        {
            if (strategies == null)
            {
                throw new ArgumentException(StringResources.NotFoundAnyStrategyInCompositeStrategy());
            }
            this.strategies = strategies.ToArray();
            if (!strategies.Any())
            {
                throw new ArgumentException(StringResources.NotFoundAnyStrategyInCompositeStrategy());
            }
                
        }

        public PresenterDiscoveryResult GetBinding(IViewLogic viewInstance)
        {
            if (ReferenceEquals(viewInstance, null))
            {
                throw new ArgumentException(StringResources.ParamIsNull("viewInstance"));  
            }

            // 找到的绑定数据结果
            var results = new List<PresenterDiscoveryResult>();

            foreach (var strategy in strategies)
            {
                var resultsThisRound = strategy.GetBinding(viewInstance);
                if (ReferenceEquals(resultsThisRound, null))
                    continue;

                results.Add(resultsThisRound);// 2种策略的叠加, 可能会有重复
            }

            // 根据ViewInstances进行分组， 其实就只有一组
            // 返回第一个
            return results.GroupBy(r => r.ViewLogicInstance).Select(r => BuildMergedResult(r.Key, r)).First();
        }

        static PresenterDiscoveryResult BuildMergedResult(IViewLogic viewInstance, IEnumerable<PresenterDiscoveryResult> results)
        {
            return new PresenterDiscoveryResult
            (
                viewInstance,
                string.Format(
                    CultureInfo.InvariantCulture,
                    "CompositePresenterDiscoveryStrategy:\r\n\r\n{0}",
                    string.Join("\r\n\r\n", results.Select(r => r.LogMessageRecord).ToArray())
                ),
                results.SelectMany(r => r.Bindings.Distinct())
            );
        }
    }
}
