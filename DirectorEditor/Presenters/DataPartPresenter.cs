using DirectorEditor.Models;
using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 此demo用于演示Presenter的N:N的对应关系
/// 通俗点说, 就是一个Presenter可以和多个ViewLogic、多个Model关联起来
/// </summary>

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(DataPart1ViewLogic))]// 绑定DataPart1ViewLogic
    [ViewLogicBinding(typeof(DataPart2ViewLogic))]// 绑定DataPart2ViewLogic
    public class DataPartPresenter : PresenterNN
    {
        public DataPartPresenter()
        {
            // TODO 初始化
        }

        /// <summary>
        /// 显示玩家信息
        /// </summary>
        public void ShowUserInfo()
        {
            if(hasViewLogicType<DataPart1ViewLogic>())
            {
                var viewLogic = GetOrCreateViewLogic(typeof(DataPart1ViewLogic)) as DataPart1ViewLogic;
                viewLogic.Show();//先调用show方法吧
                viewLogic.ShowUserInfo(GetModel<DataPart1Model>());
            }
            else
            {
                // 测试
                MessageBox.Show(string.Format("{0}不可以处理{1},请检查presenter的装饰器", this.GetType().FullName, typeof(DataPart1ViewLogic).FullName));
            }

        }

        /// <summary>
        /// 显示玩家账户信息
        /// </summary>
        public void ShowUserAccountInfo()
        {
            if (hasViewLogicType<DataPart2ViewLogic>())
            {
                var viewLogic = GetOrCreateViewLogic(typeof(DataPart2ViewLogic)) as DataPart2ViewLogic;
                viewLogic.Show();//先调用show方法吧
                viewLogic.ShowUserAccountInfo(GetModel<DataPart2Model>());
            }
            else
            {
                // 测试
                MessageBox.Show(string.Format("{0}不可以处理{1},请检查presenter的装饰器", this.GetType().FullName, typeof(DataPart2ViewLogic).FullName));
            }
        }


        /// <summary>
        /// 设置玩家信息
        /// </summary>
        /// <param name="model"></param>
        public void SetUserInfo(DataPart1Model model)
        {
            AddModel(model);
        }

        /// <summary>
        /// 设置玩家账户信息
        /// </summary>
        /// <param name="model"></param>
        public void SetUserAccountInfo(DataPart2Model model)
        {
            AddModel(model);
        }


    }
}
