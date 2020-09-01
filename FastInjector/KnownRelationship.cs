using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    public sealed class KnownRelationship:IEquatable<KnownRelationship>
    {
        public KnownRelationship(Type implementationType, Lifestyle lifestyle, InstanceProducer dependency)
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));
            Requires.IsNotNull(lifestyle, nameof(lifestyle));
            Requires.IsNotNull(dependency, nameof(dependency));

            this.ImplementationType = implementationType;
            this.Lifestyle = lifestyle;
            this.Dependency = dependency;
        }

        public Type ImplementationType { get; }

        public Lifestyle Lifestyle { get; }

        public InstanceProducer Dependency { get; }

        private string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
            "{0} = {1}, {2} = {3}, {4} = {{{5}}}",
            nameof(this.ImplementationType), this.ImplementationTypeDebuggerDisplay,
            nameof(this.Lifestyle), this.Lifestyle.Name,
            nameof(this.Dependency), this.Dependency.DebuggerDisplay);

        public override int GetHashCode() =>
            this.ImplementationType.GetHashCode() ^ this.Lifestyle.GetHashCode() ^ this.Dependency.GetHashCode();

        public bool Equals(KnownRelationship other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
                {
                return true;
            }

            return this.ImplementationType == other.ImplementationType &&
                this.Lifestyle == other.Lifestyle &&
                this.Dependency == other.Dependency;
        }
    }
}
