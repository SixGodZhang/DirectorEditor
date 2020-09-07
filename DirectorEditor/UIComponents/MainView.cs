﻿using DirectorEditor.Presenters;
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
                EditorDesc = string.Format("点击了帮助按钮, 修改帮助信息 - {0} - {1}", PresenterStub.HelperPresenter?.View.GetHashCode(), new Random().Next())
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

    }
}
