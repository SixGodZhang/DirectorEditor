using MVPFramework.Binder;
using MVPFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// 尝试根据一组格式命名的约定找到一个Presenter
    /// </summary>
    public class ConventionBasedPresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        public ConventionBasedPresenterDiscoveryStrategy()
        {

        }

        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            return GetBinding(viewInstance, ViewInstanceSuffixes, CandidatePresenterTypeFullNameFormats);
        }

        static readonly IEnumerable<string> defaultViewInstanceSuffixes =
            new[]
            {
                "UserControl",
                "Control",
                "View",
                "Form",
            };

        protected virtual IEnumerable<string> ViewInstanceSuffixes
        {
            get { return defaultViewInstanceSuffixes; }
        }

        static readonly IEnumerable<string> defaultCandidatePresenterTypeFullNameFormats =
            new[]
            {
                "{namespace}.Presenters.{presenter}",
                "{namespace}.{presenter}"
            };

        /// <summary>
        /// 后补Presenter类型完全限定名(直到命名空间)
        /// </summary>
        public virtual IEnumerable<string> CandidatePresenterTypeFullNameFormats
        {
            get { return defaultCandidatePresenterTypeFullNameFormats; }
        }

        static readonly IDictionary<RuntimeTypeHandle, ConventionSearchResult> viewTypeToPresenterTypeCache = new Dictionary<RuntimeTypeHandle, ConventionSearchResult>();
        internal static PresenterDiscoveryResult GetBinding(IView viewInstance, IEnumerable<string> viewInstanceSuffixes, IEnumerable<string> presenterTypeFullNameFormats)
        {
            var viewLogicType = viewInstance.GetType();
            ConventionSearchResult searchResult = viewTypeToPresenterTypeCache.GetOrCreateValue(viewLogicType.TypeHandle, () =>
            PerformSearch(viewInstance, viewInstanceSuffixes, presenterTypeFullNameFormats));

            return new PresenterDiscoveryResult(
                viewInstance,
                searchResult.Message,
                searchResult.PresentType == null
                ? new PresenterBinding[0]
                : new[] { new PresenterBinding(searchResult.PresentType, viewLogicType, viewInstance) });
        }

        static ConventionSearchResult PerformSearch(IView viewInstance, IEnumerable<string> viewInstanceSuffixes, 
            IEnumerable<string> presenterTypeFullNameFormats)
        {
            var viewLogicType = viewInstance.GetType();
            var presenterType = default(Type);

            //根据viewLogicType查找所有符合要求的presenterTypeName
            // 这里的意思就是
            // ViewLogic的命名都以ViewInstanceSuffixes中的suffix结尾
            // Presenter都以Presenter结尾
            var presenterTypeName = GetPresenterTypeNameFromViewLogicTypeName(viewLogicType, viewInstanceSuffixes);

            // 获取View逻辑层实现的所有接口 --> 放弃接口的实现方式
            // presenterTypeName.AddRange(GetPresenterTypeNamesFromViewInterfaceTypeNames(viewLogicType.GetViewInterfaces()));

            // 获取候选Presenter的类型完全限定名
            // presenterTypeFullNameFormats -> presenterType FullName 格式
            var candidatePresenterTypeFullNames = GenerateCandidatePresenterTypeFullNames(viewLogicType, presenterTypeName, presenterTypeFullNameFormats);

            var messages = new List<string>();
            // 找到一个就短路
            foreach (var typeFullName in candidatePresenterTypeFullNames.Distinct())
            {
                // 在ViewType所在的Assembly中寻找此类型
                presenterType = viewLogicType.Assembly.GetType(typeFullName);

                if (presenterType == null)
                {
                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "could not find a presenter with type name {0}",
                        typeFullName
                    ));
                    continue;
                }

                if (!typeof(IPresenter).IsAssignableFrom(presenterType))
                {
                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "found, but ignored, potential presenter with type name {0} because it does not implement IPresenter",
                        typeFullName
                    ));
                    presenterType = null;
                    continue;
                }

                messages.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    "found presenter with type name {0}",
                    typeFullName
                ));
                break;
            }

            return new ConventionSearchResult(
                "ConventionBasedPresenterDiscoveryStrategy:\r\n" +
                    string.Join("\r\n", messages.Select(m => "- " + m).ToArray()),
                presenterType
            );
        }

        /// <summary>
        /// 生成候选的Presenter类型
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <param name="presenterTypeNames">presenter可能的类型名称</param>
        /// <param name="presenterTypeFullNameFormats"></param>
        /// <returns></returns>
        static IEnumerable<string> GenerateCandidatePresenterTypeFullNames(Type viewLogicType, string presenterTypeName, IEnumerable<string> presenterTypeFullNameFormats)
        {
            yield return viewLogicType.Namespace + "." + presenterTypeName;// 先范湖

            var assemblyName = viewLogicType.Assembly.GetNameSafe();
            foreach (var typeNameFormat in presenterTypeFullNameFormats)//定义的presenter类型名称格式
            {
                yield return typeNameFormat.Replace("{namespace}", assemblyName)
                                           .Replace("{presenter}", presenterTypeName);
            }
        }

        /// <summary>
        /// 根据View接口类型名获取Presenter类型名
        /// 这里不会返回重复的
        /// 返回推测的Presenter类型名, 是没有加上Presenter尾缀的
        /// </summary>
        /// <param name="viewInterfaces"></param>
        /// <returns></returns>
        internal static IEnumerable<string> GetPresenterTypeNamesFromViewInterfaceTypeNames(IEnumerable<Type> viewInterfaces)
        {
            return viewInterfaces
                .Where(i => i.Name != "IView" && i.Name != "IView`1")
                .Select(i => i.Name.TrimStart('I').TrimFromEnd("View"));// IXXXView为了处理这种情况
        }

        /// <summary>
        /// 根据ViewType类型名字获取Presenter类型名字
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <param name="viewInstanceSuffixes"></param>
        /// <returns></returns>
        internal static string GetPresenterTypeNameFromViewLogicTypeName(Type viewLogicType, IEnumerable<string> viewInstanceSuffixes)
        {
            var presenterTypeName = (from suffix in viewInstanceSuffixes
                                     where viewLogicType.Name.EndsWith(string.Format("{0}Logic",suffix), StringComparison.OrdinalIgnoreCase)
                                     select viewLogicType.Name.TrimFromEnd(string.Format("{0}Logic", suffix)))
                                     .FirstOrDefault();
            return (string.IsNullOrEmpty(presenterTypeName) ? viewLogicType.Name : presenterTypeName) + "Presenter";
        }

        class ConventionSearchResult
        {
            readonly string message;
            readonly Type presenterType;

            public ConventionSearchResult(string message, Type presenterType)
            {
                this.message = message;
                this.presenterType = presenterType;
            }

            public string Message { get { return message; } }
            public Type PresentType { get { return presenterType; } }
        }


    }
}
