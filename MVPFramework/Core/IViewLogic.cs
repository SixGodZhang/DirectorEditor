using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    /// <summary>
    /// 纯View(需要在Presenter中访问的接口就在这里进行定义)
    /// 因为Presenter操作的只是从IView继承的接口, 而不是IView实例
    /// </summary>
    public interface IViewLogic
    {
        /// <summary>
        /// true: 如果此View没有绑定的Presenter， 则会在获取绑定的过程中抛出异常
        /// </summary>
        bool ThrowExceptionIfNoPresenterBound { get; set; }

        /// <summary>
        /// 显示窗口
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭窗口
        /// </summary>
        void Close();

        /// <summary>
        /// 激活窗口
        /// </summary>
        void Activate();

    }
}
