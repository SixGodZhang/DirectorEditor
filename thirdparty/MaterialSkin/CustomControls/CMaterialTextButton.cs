using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialSkin.Animations;
using System;
using MaterialSkin.Common;

/// <summary>
/// MaterialTextButton 是 仿造 MaterialRaisedButton, 会加上一些适应项目的改动
/// 1. AutoSize, AutoSize 会改变控件的默认值False, 但是WinForm控件机会记录变动值, 因而在界面上显示为False, 这段是不会出现在脚本中的
///    也就变相导致了开发者在开发视图界面时, 发现AutoSize设置的是False, 但是实际是True的效果, 因此, 不建议在自定义控件中修改控件的默认参数值
///  
/// 
/// ## 添加自定义字体
/// PrivateFontColllection pfc = new PrivateFontColllection();
/// pfc.AddFontFile("xxx.ttf");
/// </summary>

namespace MaterialSkin.Controls
{
    /// <summary>
    /// 自定义控件, 非官方控件
    /// </summary>
    public class MaterialTextButton : Button, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }
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
        /// 自定义字体大小,如果启用此属性, UseCustomFont 必须设置为True
        /// </summary>
        [Category("自定义字体")]
        public float CustomFontSize { get; set; }
        

        private readonly AnimationManager _animationManager;

        /// <summary>
        /// 此字段用来测量文字将要显示的大小, 然后根据这个大小来调整按钮的大小
        /// </summary>
        private SizeF _textSize;

        /// <summary>
        /// 显示的图片
        /// </summary>
        private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        public MaterialTextButton()
        {
            Primary = true;

            // 管理动画
            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //AutoSize = true;
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Font font = SkinManager.ROBOTO_MEDIUM_10; // 默认使用皮肤管理中的字体
                switch(UseFontType)
                {
                    case FontType.System:
                        font = new System.Drawing.Font(Font.Name, Font.Size);
                        break;
                    case FontType.CustomFont:
                        font = new Font(FontManager.GetFont(CustomFontName), CustomFontSize);
                        break;
                    case FontType.MaterialSkin:
                        font = SkinManager.ROBOTO_MEDIUM_10;
                        break;
                }
                _textSize = CreateGraphics().MeasureString(value.ToUpper(), font);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            _animationManager.StartNewAnimation(AnimationDirection.In, mevent.Location);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width - 1,
                ClientRectangle.Height - 1,
                1f))
            {
                g.FillPath(Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetRaisedButtonBackgroundBrush(), backgroundPath);
            }

            if (_animationManager.IsAnimating())
            {
                for (int i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);
                    var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.White));
                    var rippleSize = (int)(animationValue * Width * 2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }

            //Icon
            var iconRect = new Rectangle(8, 6, 24, 24);

            if (string.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;

            if (Icon != null)
            {
                //
                // Resize and move Text container
                //

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                // Third 8: right padding
                textRect.Width -= 8 + 24 + 4 + 8;

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                textRect.X += 8 + 24 + 4;
            }

            // 其实这里可以在上面做一个字体缓存, 这里就先做一下简单验证
            Font font = SkinManager.ROBOTO_MEDIUM_10; // 默认使用皮肤管理中的字体
            switch (UseFontType)
            {
                case FontType.System:
                    if(Font.Name == null || Font.Size == 0f)
                    {
                        MessageBox.Show("请先设置字体的名字和大小");
                        return;
                    }
                    font = new System.Drawing.Font(Font.Name, Font.Size);
                    break;
                case FontType.CustomFont:
                    if (CustomFontName == null || CustomFontSize == 0f)
                    {
                        MessageBox.Show("请先设置字体的名字和大小");
                        return;
                    }
                    font = new Font(FontManager.GetFont(CustomFontName), CustomFontSize);
                    break;
                case FontType.MaterialSkin:
                    font = SkinManager.ROBOTO_MEDIUM_10;
                    break;
            }

            // 绘制文本
            g.DrawString(
                Text.ToUpper(),
                font,
                SkinManager.GetRaisedButtonTextBrush(Primary),
                textRect,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            // Provides extra space for proper padding for content
            var extra = 16;

            if (Icon != null)
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;

            return new Size((int)Math.Ceiling(_textSize.Width) + extra, 36);
        }
    }
}
