using DirectorEditor.Models;
using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(HelperViewLogic))]// 指定Presenter绑定的ViewLogic类型
    public class HelperPresenter:Presenter<IHelperView,HelperModel>
    {
        public HelperPresenter()
        {
            // 这里直接处理View中的Model的初始化
            //var model = new HelperModel();
            //model.EditorDesc = "这里显示的是帮助信息";
            //SetHelperInfo(model);
        }

        /// <summary>
        /// 设置帮助信息(这里函数需要在Presenter初始化完成之后调用)
        /// </summary>
        /// <param name="modelInfo"></param>
        public void SetHelperInfo(HelperModel modelInfo)
        {

            if (modelInfo!= null)
            {
                Model = modelInfo;// 重新拉取数据
                ViewLogic.LayoutView(modelInfo);// 刷新界面
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
