using DirectorEditor.UIComponents;
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
                    _mainViewLogic.DestoryViewLogicEvent += (sender, args) => _mainViewLogic = null;
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
                    _helperViewLogic.DestoryViewLogicEvent += (sender, args) => _helperViewLogic = null;
                }
                return _helperViewLogic;
            }
        }

        // 装饰器寻址 演示界面
        private static AttributeAddressingViewLogic _attributeAddressingViewLogic = null;
        public static AttributeAddressingViewLogic AttributeAddressingViewLogic
        {
            get
            {
                if(_attributeAddressingViewLogic == null)
                {
                    _attributeAddressingViewLogic = new AttributeAddressingViewLogic();
                    _attributeAddressingViewLogic.DestoryViewLogicEvent += (sender, args) => _attributeAddressingViewLogic = null;
                }
                return _attributeAddressingViewLogic;
            }
        }

        // 第三方HZH控件 测试
        private static HZHDialogViewLogic _hZHDialogViewLogic = null;
        public static HZHDialogViewLogic HZHDialogViewLogic
        {
            get
            {
                if (_hZHDialogViewLogic == null)
                {
                    _hZHDialogViewLogic = new HZHDialogViewLogic();
                    _hZHDialogViewLogic.DestoryViewLogicEvent += (sender, args) => _hZHDialogViewLogic = null;
                }
                return _hZHDialogViewLogic;
            }
        }

        // 第三方MaterialSkin控件 测试
        private static SkinDemoViewLogic _materialSkinDemoViewLogic = null;
        public static SkinDemoViewLogic MaterialSkinDemoViewLogic
        {
            get
            {
                if (_materialSkinDemoViewLogic == null)
                {
                    _materialSkinDemoViewLogic = new SkinDemoViewLogic();
                    _materialSkinDemoViewLogic.DestoryViewLogicEvent += (sender, args) => _materialSkinDemoViewLogic = null;
                }
                return _materialSkinDemoViewLogic;
            }
        }

        // 演示一个ViewLogic对应多个Presenter
        private static ViewLogicNNViewLogic _viewLogicNNViewLogic = null;
        public static ViewLogicNNViewLogic ViewLogicNNViewLogic
        {
            get
            {
                if (_viewLogicNNViewLogic == null)
                {
                    _viewLogicNNViewLogic = new ViewLogicNNViewLogic();
                    _viewLogicNNViewLogic.DestoryViewLogicEvent += (sender, args) => _viewLogicNNViewLogic = null;
                }
                return _viewLogicNNViewLogic;
            }
        }




    }
}
