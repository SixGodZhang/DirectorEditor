﻿using DirectorEditor.Presenters;
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

    }
}
