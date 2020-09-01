﻿using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Views
{
    public interface IMainView:IView
    {
        event EventHandler LoadMainFormEvent;
        void LoadMainForm(Type formType);
    }
}
