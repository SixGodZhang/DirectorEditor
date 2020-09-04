using DirectorEditor.Models;
using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    public class HZHDialogPresenter:Presenter<IHZHDialogView, HZHDialogModel>
    {
        public HZHDialogPresenter(IHZHDialogView view) : base(view)
        {
            View = view;
        }

        public void SetTip(HZHDialogModel modelInfo)
        {
            if (PresenterStatus == PresenterStatus.Initing)
            {
                cacheMethodCallAction += () => { SetTip(modelInfo); };
                return;
            }

            if (modelInfo != null)
            {
                Model = modelInfo;// 重新拉取数据
                View.ShowTip(modelInfo);// 刷新界面
            }
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
