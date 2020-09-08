using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(SkinDemoViewLogic))]
    public class SkinDemoPresenter:Presenter<ISkinDemoView>
    {
        public SkinDemoPresenter()
        {
        }

        /// <summary>
        ///  显示界面
        /// </summary>
        public void Show()
        {
            View.Show();
        }

        /// <summary>
        ///  界面获取焦点
        /// </summary>
        public void Activate()
        {
            View.Activate();
        }

    }
}
