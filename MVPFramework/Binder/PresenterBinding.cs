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
        readonly Type viewType;
        readonly IView viewInstance;

        public PresenterBinding(Type presenterType, Type viewType, IView viewInstance)
        {
            this.presenterType = presenterType;
            this.viewType = viewType;
            this.viewInstance = viewInstance;
        }

        public Type PresenterType
        {
            get { return presenterType; }
        }

        public Type ViewType
        {
            get { return viewType; }
        }

        public IView ViewInstance
        {
            get { return viewInstance; }
        }

        public override bool Equals(object obj)
        {
            var target = obj as PresenterBinding;
            if (target == null) return false;

            return
                PresenterType == target.PresenterType &&
                ViewType == target.ViewType &&
                ViewInstance.Equals(target.ViewInstance);
        }

        public override int GetHashCode()
        {
            return
                PresenterType.GetHashCode() |
                ViewType.GetHashCode() |
                ViewInstance.GetHashCode();
        }
    }
}
