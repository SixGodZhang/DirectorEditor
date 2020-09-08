using DirectorEditor.Presenters;
using DirectorEditor.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor
{
    public static class PresenterStub
    {
        // 主界面的P层
        private static MainPresenter _mainPresenter = null;
        public static MainPresenter MainPresenter { set => _mainPresenter = value;
            get
            {
                if (_mainPresenter == null)
                {
                    _mainPresenter = new MainPresenter();
                }
                return _mainPresenter;
            }
        }

        // 帮助界面的P层
        private static HelperPresenter _helperPresenter = null;
        public static HelperPresenter HelperPresenter { set => _helperPresenter = value;
            get
            {
                if (_helperPresenter == null)
                {
                    _helperPresenter = new HelperPresenter();
                }
                return _helperPresenter;
            }

        }

        // 装饰器寻址界面的P层
        private static AttributeAddressingPresenter _attributeAddressingPresenter;
        public static AttributeAddressingPresenter AttributeAddressingPresenter { set => _attributeAddressingPresenter = value;
            get
            {
                if (_attributeAddressingPresenter == null)
                {
                    _attributeAddressingPresenter = new AttributeAddressingPresenter();
                }
                return _attributeAddressingPresenter;
            }
        }

        // 第三方HZH控件的P层
        private static HZHDialogPresenter _hZHDialogPresenter;
        public static HZHDialogPresenter HZHDialogPresenter
        {
            set => _hZHDialogPresenter = value;
            get
            {
                if (_hZHDialogPresenter == null)
                {
                    _hZHDialogPresenter = new HZHDialogPresenter();
                }
                return _hZHDialogPresenter;
            }
        }

        // 第三方Material Skin控件的P层
        private static SkinDemoPresenter _skinDemoPresenter;
        public static SkinDemoPresenter SkinDemoPresenter
        {
            set => _skinDemoPresenter = value;
            get
            {
                if(_skinDemoPresenter == null)
                {
                    _skinDemoPresenter = new SkinDemoPresenter();
                }
                return _skinDemoPresenter;
            }
        }

        // PresenterNN 测试
        private static DataPartPresenter _dataPartPresenter;
        public static DataPartPresenter DataPartPresenter
        {
            set => _dataPartPresenter = value;
            get
            {
                if (_dataPartPresenter == null)
                {
                    _dataPartPresenter = new DataPartPresenter();
                }
                return _dataPartPresenter;
            }
        }

        // ViewLogicNN 测试一
        private static ViewLogicNNPart1Presenter _viewLogicNNPart1Presenter;
        public static ViewLogicNNPart1Presenter ViewLogicNNPart1Presenter
        {
            set => _viewLogicNNPart1Presenter = value;
            get
            {
                if(_viewLogicNNPart1Presenter == null)
                {
                    _viewLogicNNPart1Presenter = new ViewLogicNNPart1Presenter();
                }
                return _viewLogicNNPart1Presenter;
            }
        }

        // ViewLogicNN 测试二
        private static ViewLogicNNPart2Presenter _viewLogicNNPart2Presenter;
        public static ViewLogicNNPart2Presenter ViewLogicNNPart2Presenter
        {
            set => _viewLogicNNPart2Presenter = value;
            get
            {
                if (_viewLogicNNPart2Presenter == null)
                {
                    _viewLogicNNPart2Presenter = new ViewLogicNNPart2Presenter();
                }
                return _viewLogicNNPart2Presenter;
            }
        }

    }
}
