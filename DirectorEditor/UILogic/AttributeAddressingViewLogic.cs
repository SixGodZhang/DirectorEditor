using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework.Core;
using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectorEditor.Presenters;

namespace DirectorEditor.UILogic
{
    [PresenterBinding(typeof(AttributeAddressingPresenter))]
    public sealed class AttributeAddressingViewLogic:ViewLogic<AttributeAddressingView, IAttributeAddressingView>,IAttributeAddressingView
    {
        /// <summary>
        /// 此处使用装饰器的寻址策略
        /// </summary>
        public AttributeAddressingViewLogic():base(addressingType:PresenterAddressingType.Attribute)
        {
            InitViewLogic += () => { };
        }

        public void Activate()
        {
            target?.Activate();
        }

        public void Close()
        {
            target?.Close();
        }

        public void Show()
        {
            target?.Show();
        }

    }
}
