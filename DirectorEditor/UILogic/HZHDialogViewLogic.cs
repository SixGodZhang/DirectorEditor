using DirectorEditor.Models;
using DirectorEditor.Presenters;
using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.UILogic
{
    public class HZHDialogViewLogic : ViewLogic<ThirdParty_HZH_DialogView, IHZHDialogView>, IHZHDialogView
    {
        public HZHDialogViewLogic():base()
        {
            //
        }

        public void Activate()
        {
            target?.Activate();
        }

        public void Close()
        {
            target?.Close();
        }

        public void Show()
        {
            target?.Show();
        }

        public void ShowTip(HZHDialogModel msg)
        {
            target.lblMsg.Text = msg.TipInfo;
        }
    }
}
