using DirectorEditor.Views;
using MVPFramework;
using System.Windows.Forms;

namespace DirectorEditor.UILogic
{
    // ViewLogic<T1,T2>
    // T1 : 指的是绑定的UI 组件
    // T2 : 指的是暴露给Presenter的接口
    public class MainViewLogic : ViewLogic<MainView,IMainView>, IMainView
    {
        /// <summary>
        /// 默认采用单例的方式
        /// </summary>
        /// <param name="type"></param>
        public MainViewLogic():base()
        {
            //Instance.Show();
        }

        public void ShowMainForm()
        {
            MessageBox.Show("call ShowMainForm ...");
        }

        public void Show()
        {
            target.Show();
        }

        public void Close()
        {
            target.Close(); 
        }

        public void Activate()
        {
            target.Activate();
        }
    }
}
