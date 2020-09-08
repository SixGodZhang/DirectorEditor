using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(ViewLogicNNViewLogic))]
    public class ViewLogicNNPart1Presenter: Presenter<IViewLogicNNView>
    {
        public ViewLogicNNPart1Presenter()
        {
        }

        public void Show()
        {
            View.Show();
        }

        public void Close()
        {
            View.Close();
        }
    }
}
