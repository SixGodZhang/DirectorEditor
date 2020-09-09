using DirectorEditor.Presenters;
using DirectorEditor.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TX3Installer.Presenters;

namespace DirectorEditor
{
    public static class PresenterStub
    {
        // 第三方Material Skin控件的P层
        private static SkinDemoPresenter _skinDemoPresenter;
        public static SkinDemoPresenter SkinDemoPresenter
        {
            set => _skinDemoPresenter = value;
            get
            {
                if (_skinDemoPresenter == null)
                {
                    _skinDemoPresenter = new SkinDemoPresenter();
                }
                return _skinDemoPresenter;
            }
        }

        // 安装主界面
        private static MainInstallerPresenter _mainInstallerPresenter;
        public static MainInstallerPresenter MainInstallerPresenter
        {
            set => _mainInstallerPresenter = value;
            get
            {
                if(_mainInstallerPresenter == null)
                {
                    _mainInstallerPresenter = new MainInstallerPresenter();
                }
                return _mainInstallerPresenter;
            }
        }
    }
}
