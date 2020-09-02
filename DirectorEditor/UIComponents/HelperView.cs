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

namespace DirectorEditor.UIComponents
{
    /// <summary>
    /// 帮助界面
    /// </summary>
    public partial class HelperView : MVPForm,IHelperView
    {
        public HelperView()
        {
            InitializeComponent();
        }
    }
}
