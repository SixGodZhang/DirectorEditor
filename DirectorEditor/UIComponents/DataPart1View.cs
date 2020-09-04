using DirectorEditor.Views;
using MVPFramework;
using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectorEditor.UIComponents
{
    /// <summary>
    /// 演示了一个View 与 多个Presenter对应的情况
    /// </summary>
    [PresenterBinding(typeof(DataPart1View))]
    [PresenterBinding(typeof(DataPart2View))]
    public partial class DataPart1View : Form
    {
        public DataPart1View()
        {
            InitializeComponent();
        }
    }
}
