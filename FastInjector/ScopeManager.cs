using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    internal sealed class ScopeManager
    {
        private readonly Container container;
        private readonly Func<Scope> scopeRetriever;
        private readonly Action<Scope> scopeReplacer;

        internal ScopeManager(Container container, Func<Scope> scopeRetriever, Action<Scope> scopeReplacer)
        {
           // Requires.is
        }
    }
}
