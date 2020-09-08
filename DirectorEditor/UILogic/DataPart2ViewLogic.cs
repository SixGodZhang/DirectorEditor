using DirectorEditor.Models;
using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework;
using System;

namespace DirectorEditor.UILogic
{
    public class DataPart2ViewLogic : ViewLogic<DataPart2View, IDataPart2View>, IDataPart2View
    {
        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            target?.Show();
        }

        public void ShowUserAccountInfo(DataPart2Model model)
        {
            target.lbUserID.Text = model.UserId;
            target.lbVipLevel.Text = model.VIPLevel.ToString();
            target.lbUserPassword.Text = model.userPassword.ToString();
        }
    }
}
