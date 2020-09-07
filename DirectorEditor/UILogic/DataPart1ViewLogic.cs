using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ShowUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
