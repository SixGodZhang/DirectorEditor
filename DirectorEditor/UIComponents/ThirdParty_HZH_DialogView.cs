using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DirectorEditor.UIComponents
{
    public partial class ThirdParty_HZH_DialogView : HZH_Controls.Forms.FrmBase
    {
        public ThirdParty_HZH_DialogView()
        {
            InitializeComponent();
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void okBtn_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
