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
    public static class PresenterManager
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
        /// 批量查找Presenter缓存的实例。
        /// 返回结果 是可以查找到的 Presenter实例
        /// failedPresenters 是未查找到的类型
        /// </summary>
        /// <param name="persenterTypes"></param>
        /// <param name="failedPresenters"></param>
        /// <returns></returns>
        public static List<IPresenter> Gets(IEnumerable<Type> persenterTypes,out List<Type> failedPresenters)
        {
            List<IPresenter> result = new List<IPresenter>();
            failedPresenters = new List<Type>();
            foreach (var persenterType in persenterTypes)
            {
                if (cachePresenters.ContainsKey(persenterType))
                {
                    result.Add(cachePresenters[persenterType]);
                }
                else
                {
                    failedPresenters.Add(persenterType);
                }
            }

            return result;
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
