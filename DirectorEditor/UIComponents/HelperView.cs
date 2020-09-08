using System.Windows.Forms;

namespace DirectorEditor.UIComponents
{
    /// <summary>
    /// 帮助界面
    /// </summary>
    public partial class HelperView:Form
    {
        public HelperView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 此函数的调用由Presenter来控制
        /// </summary>
        //public void LayoutView()
        //{
        //    //if (Presenter.PresenterType != PresenterType.ModelView)// 如果不是此种类型的PresenterType， 则返回
        //    //    return;

        //    //var presenter = Presenter as HelperPresenter;
        //    //this.desc.Text = presenter.Model.EditorDesc;
        //}
    }
}
