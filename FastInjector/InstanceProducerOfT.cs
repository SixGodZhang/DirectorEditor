using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    public class InstanceProducer<TService>:InstanceProducer where TService:class
    {
        public InstanceProducer(Registration registration)
            :base(typeof(TService),registration)
        {

        }

        public new TService GetInstance() => (TService)base.GetInstance();
    }
}
