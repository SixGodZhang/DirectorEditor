using DirectorEditor.Models;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Views
{
    public interface IHelperView:IViewLogic
    {
        void LayoutView(HelperModel modelInfo);// 刷新主界面UI
    }
}
