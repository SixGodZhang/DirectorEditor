using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectorEditor
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class MainView : MVPForm,IMainView
    {

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
    }
}
