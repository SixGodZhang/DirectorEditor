using DirectorEditor.Models;
using DirectorEditor.UIComponents;
using MVPFramework;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DirectorEditor.Views;

namespace DirectorEditor.Presenters
{
    public class HelperPresenter:Presenter<IHelperView,HelperModel>
    {
        public HelperPresenter(IHelperView view):base(view)
        {
            // 这里直接处理View中的Model的初始化
            var model = new HelperModel();
            model.EditorDesc = "这里显示的是帮助信息";

            SetHelperInfo(model);
        }



        /// <summary>
        /// 设置帮助信息(这里函数需要在Presenter初始化完成之后调用)
        /// </summary>
        /// <param name="modelInfo"></param>
        public void SetHelperInfo(HelperModel modelInfo)
        {
            if (PresenterStatus == PresenterStatus.Initing)
            {
                //string methodName = new StackTrace().GetFrame(0).GetMethod().Name;
                cacheMethodCallAction += () => { SetHelperInfo(modelInfo); };
                return;
            }

            if (modelInfo!= null)
            {
                Model = modelInfo;// 重新拉取数据
                View.LayoutView();// 刷新界面
            }
        }
    }
}
