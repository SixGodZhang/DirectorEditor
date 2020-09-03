using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    public class CompositePresenterDiscoveryStrategy:IPresenterDiscoveryStrategy
    {
        readonly IEnumerable<IPresenterDiscoveryStrategy> strategies;

        public CompositePresenterDiscoveryStrategy(params IPresenterDiscoveryStrategy[] strategies)
            :this((IEnumerable<IPresenterDiscoveryStrategy>) strategies)
        {

        }

        public CompositePresenterDiscoveryStrategy(IEnumerable<IPresenterDiscoveryStrategy> strategies)
        {
            if (strategies == null)
                throw new ArgumentNullException("strategies");

            this.strategies = strategies.ToArray();

            if (!strategies.Any())
                throw new ArgumentException("You must supple at least one strategy.", "strategies");
        }

        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (ReferenceEquals(viewInstance, null))
                throw new ArgumentNullException("viewInstance");

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
            return results.GroupBy(r => r.ViewInstance).Select(r => BuildMergedResult(r.Key, r)).First();
        }

        static PresenterDiscoveryResult BuildMergedResult(IView viewInstance, IEnumerable<PresenterDiscoveryResult> results)
        {
            return new PresenterDiscoveryResult
            (
                viewInstance,
                string.Format(
                    CultureInfo.InvariantCulture,
                    "CompositePresenterDiscoveryStrategy:\r\n\r\n{0}",
                    string.Join("\r\n\r\n", results.Select(r => r.Message).ToArray())
                ),
                results.SelectMany(r => r.Bindings)
            );
        }
    }
}
