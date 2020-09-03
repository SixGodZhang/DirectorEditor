﻿using DirectorEditor.Models;
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
    // ViewLogic<T1,T2>
    // T1 : 指的是绑定的UI 组件
    // T2 : 指的是暴露给Presenter的接口
    public class HelperViewLogic : ViewLogic<HelperView, IHelperView>, IHelperView
    {

        public HelperViewLogic(ViewType type = ViewType.Single) : base(type)
        {
            
        }

        public void Activate()
        {
            target?.Activate();
        }

        public void Close()
        {
            target?.Close();
        }

        public void LayoutView(HelperModel modelInfo)
        {
            target.desc.Text = modelInfo.EditorDesc;
        }

        public void Show()
        {
            target?.Show();
        }
    }
}
