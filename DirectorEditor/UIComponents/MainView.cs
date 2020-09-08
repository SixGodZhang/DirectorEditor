using DirectorEditor.Models;
using DirectorEditor.Presenters;
using DirectorEditor.UIComponents;
using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Windows.Forms;

namespace DirectorEditor
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class MainView:Form
    {
        /// <summary>
        /// 1. 先调用父类的构造函数, 完成Presenter的绑定相关工作
        /// 2. 调用此构造函数, 进行界面上的UI初始化
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {
            //_helpView = new HelperView();
            //_helpView.Show();
            // 显示一个界面
            //ViewLogicStub.HelperViewLogic.Show(); 先不要通过viewLogic去操作
            PresenterStub.HelperPresenter?.Show();// 通过P层去访问界面
        }

        private void testChangeHelpInfoBtn_Click(object sender, EventArgs e)
        {
            //if(_helpView == null)
            //{
            //    _helpView = new HelperView();
            //    _helpView.Show();
            //}

            //(_helpView.Presenter as HelperPresenter).SetHelperInfo(new Models.HelperModel()
            //{

            //    EditorDesc = string.Format("点击了帮助按钮, 修改帮助信息 - {0}", new Random().Next())
            //});

            PresenterStub.HelperPresenter?.SetHelperInfo(new Models.HelperModel()
            {
                EditorDesc = string.Format("点击了帮助按钮, 修改帮助信息 - {0} - {1}", PresenterStub.HelperPresenter?.ViewLogic.GetHashCode(), new Random().Next())
            });
            PresenterStub.HelperPresenter?.Activate();
        }

        private void attributeBtn_Click(object sender, EventArgs e)
        {
            PresenterStub.AttributeAddressingPresenter?.Show();
            PresenterStub.AttributeAddressingPresenter?.Activate();
        }

        private void thirdPartyDialogBtn_Click(object sender, EventArgs e)
        {
            PresenterStub.HZHDialogPresenter?.Show();
            PresenterStub.HZHDialogPresenter?.Activate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PresenterStub.SkinDemoPresenter?.Show();
            PresenterStub.SkinDemoPresenter?.Activate();
        }

        private void dataPart1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 测试数据
            DataPart1Model model1 = new DataPart1Model
            {
                Id = "10000",
                Name = "张三",
                Age = "25"
            };
            PresenterStub.DataPartPresenter?.AddModel(model1);
            PresenterStub.DataPartPresenter?.ShowUserInfo();
        }

        private void dataPart2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 测试数据
            DataPart2Model model2 = new DataPart2Model
            {
                UserId = "98328329@163.com",
                userPassword = "12345678",
                VIPLevel = 12
            };
            PresenterStub.DataPartPresenter?.AddModel(model2);
            PresenterStub.DataPartPresenter?.ShowUserAccountInfo();
        }

        private void viewLogicNNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresenterStub.ViewLogicNNPart1Presenter?.Show();
        }

        private void viewLogicNNCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresenterStub.ViewLogicNNPart2Presenter?.Show();
        }
    }
}
