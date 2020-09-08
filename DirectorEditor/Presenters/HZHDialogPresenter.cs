using DirectorEditor.Models;
using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(HZHDialogViewLogic))]
    public class HZHDialogPresenter:Presenter<IHZHDialogView, HZHDialogModel>
    {
        public HZHDialogPresenter()
        {
        }

        public void SetTip(HZHDialogModel modelInfo)
        {

            if (modelInfo != null)
            {
                Model = modelInfo;// 重新拉取数据
                ViewLogic.ShowTip(modelInfo);// 刷新界面
            }
        }

        /// <summary>
        ///  显示界面
        /// </summary>
        public void Show()
        {
            ViewLogic.Show();
        }

        /// <summary>
        ///  界面获取焦点
        /// </summary>
        public void Activate()
        {
            ViewLogic.Activate();
        }
    }
}
