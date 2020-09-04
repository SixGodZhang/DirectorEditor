using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    public class PresenterBinding
    {
        readonly Type presenterType;
        readonly Type viewLogicType;
        readonly IViewLogic viewInstance;

        public PresenterBinding(Type presenterType, Type viewLogicType, IViewLogic viewInstance)
        {
            this.presenterType = presenterType;
            this.viewLogicType = viewLogicType;
            this.viewInstance = viewInstance;
        }

        public Type PresenterType
        {
            get { return presenterType; }
        }

        public Type ViewLogicType
        {
            get { return viewLogicType; }
        }

        public IViewLogic ViewInstance
        {
            get { return viewInstance; }
        }

        public override bool Equals(object obj)
        {
            var target = obj as PresenterBinding;
            if (target == null) return false;

            return
                PresenterType == target.PresenterType &&
                ViewLogicType == target.ViewLogicType &&
                ViewInstance.Equals(target.ViewInstance);
        }

        public override int GetHashCode()
        {
            return
                PresenterType.GetHashCode() |
                ViewLogicType.GetHashCode() |
                ViewInstance.GetHashCode();
        }
    }
}
