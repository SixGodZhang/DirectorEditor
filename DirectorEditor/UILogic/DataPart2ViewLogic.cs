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

        public void ShowUserAccountInfo()
        {
            throw new NotImplementedException();
        }
    }
}
