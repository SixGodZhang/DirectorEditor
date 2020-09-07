using DirectorEditor.Models;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Views
{
    public interface IDataPart1View:IViewLogic
    {
        void ShowUserInfo(DataPart1Model data);// 显示玩家个人信息
    }
}
