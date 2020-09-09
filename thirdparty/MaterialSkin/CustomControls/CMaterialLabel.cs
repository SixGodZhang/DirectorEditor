using MaterialSkin.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    public class CMaterialLabel : Label, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        /// <summary>
        /// 是否使用自定义字体
        /// </summary>
        [Category("自定义字体")]
        [DefaultValue(FontType.MaterialSkin)]
        public FontType UseFontType { get; set; }
        /// <summary>
        /// 自定字体名, 如果启用此属性, UseCustomFont 必须设置为True
        /// 通常, 会将自定义字体放在Resources.resx文件中
        /// </summary>
        [Category("自定义字体")]
        public string CustomFontName { get; set; }

        /// <summary>
        /// 文本显示的正常颜色
        /// </summary>
        [Category("自定义字体")]
        public Color CustomLabelNMLColor { get; set; }

        /// <summary>
        /// 鼠标覆盖在文本上的颜色
        /// </summary>
        [Category("自定义字体")]
        public Color CustomLabelOVRColor { get; set; }

        /// <summary>
        /// 鼠标按下去的时候文本的颜色
        /// </summary>
        [Category("自定义字体")]
        public Color CustomLabelDWNColor { get; set; }

        // 文字的颜色流程: nml -> mouse enter -> mouse down -> mouse leave -> nml
        // 显示的颜色分为三种: nml、ovr、dwn
        // 也就是: nml -> ovr -> dwn -> ovr -> nml
        //                 |--->nml

        /// <summary>
        /// 当鼠标进入的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            ForeColor = CustomLabelOVRColor ;
        }

        /// <summary>
        /// 当鼠标移开时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            ForeColor = CustomLabelNMLColor;
        }

        /// <summary>
        /// 当鼠标抬起的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            ForeColor = CustomLabelOVRColor;
        }

        /// <summary>
        /// 当鼠标按下时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ForeColor = CustomLabelDWNColor;
        }


        /// <summary>
        /// 当Font属性改变时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            if(UseFontType!=FontType.CustomFont)
            {
                return;
            }
            if (string.IsNullOrEmpty(CustomFontName))
            {
                MessageBox.Show("CustomFontName 没有设置!");
                return;
            }

            // 确认字体
            Font = new Font(FontManager.GetFont(CustomFontName), Font.Size);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            AutoSize = false;
            ForeColor = SkinManager.GetPrimaryTextColor();
            UseFontType = FontType.CustomFont;
            CustomFontName = "fzjz";

            // 确认字体
            Font font = SkinManager.ROBOTO_REGULAR_11; // 默认使用皮肤管理中的字体
            switch (UseFontType)
            {
                case FontType.System:
                    font = new System.Drawing.Font(Font.Name, Font.Size);
                    break;
                case FontType.CustomFont:
                    font = new Font(FontManager.GetFont(CustomFontName), Font.Size);
                    break;
                case FontType.MaterialSkin:
                    font = SkinManager.ROBOTO_REGULAR_11;
                    break;
            }
            Font = font;

            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }
    }
}
