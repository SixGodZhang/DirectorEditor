﻿using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPFramework
{
    public partial class MVPForm : Form, IView
    {
        private readonly PresenterBinder presenterBinder = new PresenterBinder();

        public MVPForm()
        {
            //ThrowExceptionIfNoPresenterBound = true;// 如果没有绑定PresenterBound， 则会抛出异常
            presenterBinder.PresenterCreated += PresenterBinder_PresenterCreated;
            presenterBinder.PerformBinding(this);
        }

        /// <summary>
        /// 窗口显示时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        /// <summary>
        /// 窗口关闭时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }


        /// <summary>
        /// Presenter 创建之后的回调函数
        /// </summary>
        /// <param name="sender">typeof(sender) == PresenterBinder</param>
        /// <param name="e"></param>
        private void PresenterBinder_PresenterCreated(object sender, PresenterCreatedEventArgs e)
        {
            // e.Presenter 就是与此View绑定的Presenter
            //MessageBox.Show("PresenterCreated之后的回调...");
            Presenter = e.Presenter;
            Presenter.PresenterStatus = PresenterStatus.Inited;
        }

        public IPresenter Presenter { get; set; }//绑定到View上的Presenter
        public bool ThrowExceptionIfNoPresenterBound { get; set; }
    }
}
