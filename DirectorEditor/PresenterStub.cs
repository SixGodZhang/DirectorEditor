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
                return ViewLogicStub.MainViewLogic.Presenters.ElementAt(0) as MainPresenter;
            }
        }

        // 帮助界面的P层
        private static HelperPresenter _helperPresenter = null;
        public static HelperPresenter HelperPresenter { set => _helperPresenter = value;
            get
            {
                return ViewLogicStub.HelperViewLogic.Presenters.ElementAt(0) as HelperPresenter;
            }

        }

        // 装饰器寻址界面的P层
        private static AttributeAddressingPresenter _attributeAddressingPresenter;
        public static AttributeAddressingPresenter AttributeAddressingPresenter { set => _attributeAddressingPresenter = value;
            get
            {
                return ViewLogicStub.AttributeAddressingViewLogic.Presenters.ElementAt(0) as AttributeAddressingPresenter;
            }
        }

        // 第三方HZH控件的P层
        private static HZHDialogPresenter _hZHDialogPresenter;
        public static HZHDialogPresenter HZHDialogPresenter
        {
            set => _hZHDialogPresenter = value;
            get
            {
                return ViewLogicStub.HZHDialogViewLogic?.Presenters?.ElementAt(0) as HZHDialogPresenter;
            }
        }

        // 第三方Material Skin控件的P层
        private static SkinDemoPresenter _skinDemoPresenter;
        public static SkinDemoPresenter SkinDemoPresenter
        {
            set => _skinDemoPresenter = value;
            get
            {
                return ViewLogicStub.MaterialSkinDemoViewLogic?.Presenters?.ElementAt(0) as SkinDemoPresenter;
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
                //return ViewLogicStub.MaterialSkinDemoViewLogic?.Presenters?.ElementAt(0) as DataPartPresenter;
            }
        }

    }
}
