using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    public class InitializerContext
    {
        internal InitializerContext(Registration registration)
        {
            Requires.IsNotNull(registration, nameof(registration));
            this.Registration = registration;
        }

        public Registration Registration { get;}

        public InstanceProducer Producer { get; }

        internal string DebuggerDisplay =>
            string.Format(CultureInfo.InvariantCulture,
                "Registration.ImplementationType: {0}", this.Registration.ImplementationType.ToFriendlyName(true));


    }
}
