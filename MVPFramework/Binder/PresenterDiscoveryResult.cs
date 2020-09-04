using MVPFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    public class PresenterDiscoveryResult
    {
        readonly IViewLogic viewLogicInstance;
        /// <summary>
        /// 查找过程关键信息记录
        /// </summary>
        readonly string logMessageRecord;
        readonly IEnumerable<PresenterBinding> bindings;

        public PresenterDiscoveryResult(IViewLogic viewLogicInstance, string logMessageRecord, IEnumerable<PresenterBinding> bindings)
        {
            this.viewLogicInstance = viewLogicInstance;
            this.logMessageRecord = logMessageRecord;
            this.bindings = bindings;
        }

        public IViewLogic ViewLogicInstance { get { return viewLogicInstance; } }

        public string LogMessageRecord { get { return logMessageRecord; } }

        public IEnumerable<PresenterBinding> Bindings { get { return bindings; } }

        public override bool Equals(object obj)
        {
            var target = obj as PresenterDiscoveryResult;
            if (target == null) return false;

            return
               viewLogicInstance.Equals(target.viewLogicInstance) &&
                LogMessageRecord.Equals(target.LogMessageRecord, StringComparison.OrdinalIgnoreCase) &&
                Bindings.SetEqual(target.Bindings);
        }

        public override int GetHashCode()
        {
            return
                viewLogicInstance.GetHashCode() |
                LogMessageRecord.GetHashCode() |
                Bindings.GetHashCode();
        }

    }
}
