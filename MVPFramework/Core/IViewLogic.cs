namespace MVPFramework
{
    /// <summary>
    /// 纯View(需要在Presenter中访问的接口就在这里进行定义)
    /// 因为Presenter操作的只是从IView继承的接口, 而不是IView实例
    /// </summary>
    public interface IViewLogic
    {

        /// <summary>
        /// 显示窗口
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭窗口
        /// </summary>
        void Close();

        /// <summary>
        /// 激活窗口
        /// </summary>
        void Activate();

    }
}
