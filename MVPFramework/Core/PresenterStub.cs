using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Core
{
    /// <summary>
    /// Presenter 中转中心
    /// </summary>
    public static class PresenterStub
    {
        /// <summary>
        /// Presenter可以以类型做区分, 同一类型的presenter不会存在两个, 所以以Type作为键
        /// </summary>
        public static Dictionary<Type, IPresenter> cachePresenters = new Dictionary<Type, IPresenter>();

        /// <summary>
        /// 添加presenter到缓存中
        /// </summary>
        /// <param name="presenter"></param>
        public static void Add(IPresenter presenter)
        {
            if(!cachePresenters.ContainsKey(presenter.GetType()))
            {
                cachePresenters.Add(presenter.GetType(), presenter);
            }
            {
                cachePresenters[presenter.GetType()] = presenter;
            }
            
        }

        /// <summary>
        /// 根据Type查找一个Presenter
        /// </summary>
        /// <param name="persenterType"></param>
        /// <returns></returns>
        public static IPresenter Get(Type persenterType)
        {
            if (!cachePresenters.ContainsKey(persenterType))
                return null;
            return cachePresenters[persenterType];
        }

        /// <summary>
        /// 销毁指定的Presenter
        /// </summary>
        /// <param name="persenterFullName"></param>
        public static void Destroy(Type persenterType)
        {
            if (cachePresenters.ContainsKey(persenterType))
            {
                cachePresenters.Remove(persenterType);
            }
        }
    }
}
