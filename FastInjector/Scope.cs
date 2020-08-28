using System;
using System.Collections.Generic;
using System.Text;

namespace FastInjector
{
    public class Scope : IDisposable
    {
        private const int MaxRecursion = 100;
        private readonly object syncRoot = new object();
        private readonly ScopeManager manager;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
