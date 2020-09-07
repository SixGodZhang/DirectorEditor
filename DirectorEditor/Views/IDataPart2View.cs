using DirectorEditor.Models;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Views
{
    public interface IDataPart2View:IViewLogic
    {
        void ShowUserAccountInfo(DataPart2Model model);// 显示玩家账户信息
    }
}
