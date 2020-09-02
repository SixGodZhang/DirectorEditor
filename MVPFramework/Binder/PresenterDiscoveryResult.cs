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
        readonly IEnumerable<IView> viewInstances;
        readonly string message;
        readonly IEnumerable<PresenterBinding> bindings;

        public PresenterDiscoveryResult(IEnumerable<IView> viewInstances, string message, IEnumerable<PresenterBinding> bindings)
        {
            this.viewInstances = viewInstances;
            this.message = message;
            this.bindings = bindings;
        }

        public IEnumerable<IView> ViewInstances { get { return viewInstances; } }

        public string Message { get { return message; } }

        public IEnumerable<PresenterBinding> Bindings { get { return bindings; } }

        public override bool Equals(object obj)
        {
            var target = obj as PresenterDiscoveryResult;
            if (target == null) return false;

            return
                ViewInstances.SetEqual(target.ViewInstances) &&
                Message.Equals(target.Message, StringComparison.OrdinalIgnoreCase) &&
                Bindings.SetEqual(target.Bindings);
        }

        public override int GetHashCode()
        {
            return
                ViewInstances.GetHashCode() |
                Message.GetHashCode() |
                Bindings.GetHashCode();
        }

    }
}
