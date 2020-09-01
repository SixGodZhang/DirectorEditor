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
    public partial class MainView : MVPForm,IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public event EventHandler LoadMainFormEvent;

        public void LoadMainForm(Type formType)
        {
            
        }
    }
}
