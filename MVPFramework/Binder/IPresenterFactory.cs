using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 定义可以创建presenters的工厂
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        /// 创建一个指定类型的presenter、view 
        /// </summary>
        /// <param name="presenterType"></param>
        /// <param name="viewType"></param>
        /// <param name="viewInstance"></param>
        /// <returns></returns>
        IPresenter Create(Type presenterType, Type viewType, IViewLogic viewInstance);

        /// <summary>
        /// 释放指定类型的presenter
        /// </summary>
        /// <param name="presenter"></param>
        void Release(IPresenter presenter);
    }
}
