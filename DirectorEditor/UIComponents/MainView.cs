using DirectorEditor.Presenters;
using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework;
using System;

namespace DirectorEditor
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class MainView : MVPForm,IMainView
    {

        //帮助窗口
        private HelperView _helpView;

        /// <summary>
        /// 1. 先调用父类的构造函数, 完成Presenter的绑定相关工作
        /// 2. 调用此构造函数, 进行界面上的UI初始化
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            ThrowExceptionIfNoPresenterBound = false;
        }

        /// <summary>
        /// 显示主界面
        /// </summary>
        public void ShowMainForm()
        {
            
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {
            _helpView = new HelperView();
            _helpView.Show();
        }

        private void testChangeHelpInfoBtn_Click(object sender, EventArgs e)
        {
            if(_helpView == null)
            {
                _helpView = new HelperView();
                _helpView.Show();
            }

            (_helpView.Presenter as HelperPresenter).SetHelperInfo(new Models.HelperModel()
            {

                EditorDesc = string.Format("点击了帮助按钮, 修改帮助信息 - {0}", new Random().Next())
            });
        }
    }
}
