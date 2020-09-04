using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 查找策略
    /// 根据View查找所有与之相关的Presenter
    /// </summary>
    public interface IPresenterDiscoveryStrategy
    {
        /// <summary>
        /// 获取绑定到View的presenters
        /// </summary>
        /// <param name="viewInstance"></param>
        /// <returns></returns>
        PresenterDiscoveryResult GetBinding(IViewLogic viewInstance);
    }
}
