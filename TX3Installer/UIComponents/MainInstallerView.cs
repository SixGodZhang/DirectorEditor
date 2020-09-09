                using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TX3Installer.UIComponents
{
    public partial class MainInstallerView : MaterialForm
    {
        public MainInstallerView()
        {
            InitializeComponent();

            //// 创建一个颜色主题管理器
            //MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            ////
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400,
            //    Primary.Blue500,
            //    Primary.Blue500,
            //    Accent.LightBlue200,
            //    TextShade.WHITE);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("你点击了一次按钮！");
        }
    }
}
