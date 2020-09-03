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
        readonly IView viewInstance;
        readonly string message;
        readonly IEnumerable<PresenterBinding> bindings;

        public PresenterDiscoveryResult(IView viewInstance, string message, IEnumerable<PresenterBinding> bindings)
        {
            this.viewInstance = viewInstance;
            this.message = message;
            this.bindings = bindings;
        }

        public IView ViewInstance { get { return viewInstance; } }

        public string Message { get { return message; } }

        public IEnumerable<PresenterBinding> Bindings { get { return bindings; } }

        public override bool Equals(object obj)
        {
            var target = obj as PresenterDiscoveryResult;
            if (target == null) return false;

            return
               viewInstance.Equals(target.viewInstance) &&
                Message.Equals(target.Message, StringComparison.OrdinalIgnoreCase) &&
                Bindings.SetEqual(target.Bindings);
        }

        public override int GetHashCode()
        {
            return
                viewInstance.GetHashCode() |
                Message.GetHashCode() |
                Bindings.GetHashCode();
        }

    }
}
