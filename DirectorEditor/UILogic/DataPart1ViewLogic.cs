using DirectorEditor.Models;
using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework;
using System;

namespace DirectorEditor.UILogic
{
    public class DataPart1ViewLogic : ViewLogic<DataPart1View, IDataPart1View>, IDataPart1View
    {
        public DataPart1ViewLogic():base()
        {

        }

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

        public void ShowUserInfo(DataPart1Model model)
        {
            target.lbName.Text = model.Name;
            target.lbAge.Text = model.Age;
            target.lbID.Text = model.Id;
        }
    }
}
