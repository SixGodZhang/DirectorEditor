using DirectorEditor.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor
{
    /// <summary>
    /// 全局保存需要访问的界面
    /// </summary>
    public static class ViewLogicStub
    {
        // 主界面
        private static MainViewLogic _mainViewLogic = null;
        public static MainViewLogic MainViewLogic
        {
            get
            {
                if (_mainViewLogic == null)
                {
                    _mainViewLogic = new MainViewLogic();
                    _mainViewLogic.DestroyViewLogic += () => _mainViewLogic = null;
                }

                return _mainViewLogic;
            }
        }

        // 帮助界面
        private static HelperViewLogic _helperViewLogic = null;
        public static HelperViewLogic HelperViewLogic
        {
            get
            {
                if(_helperViewLogic == null)
                {
                    _helperViewLogic = new HelperViewLogic();
                    _helperViewLogic.DestroyViewLogic += () => _helperViewLogic = null;
                }
                return _helperViewLogic;
            }
        }

    }
}
