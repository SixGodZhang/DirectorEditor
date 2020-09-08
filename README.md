有关调用实时(JIT)调试而不是此对话框的详细信息，
请参见此消息的结尾。

************** 异常文本 **************
System.NullReferenceException: 未将对象引用设置到对象的实例。
   在 DirectorEditor.Presenters.ViewLogicNNPart1Presenter.Show() 位置 C:\Users\zhanghui03\source\repos\DirectorEditor\DirectorEditor\Presenters\ViewLogicNNPart1Presenter.cs:行号 20
   在 DirectorEditor.MainView.viewLogicNNToolStripMenuItem_Click(Object sender, EventArgs e) 位置 C:\Users\zhanghui03\source\repos\DirectorEditor\DirectorEditor\UIComponents\MainView.cs:行号 102
   在 System.Windows.Forms.ToolStripItem.RaiseEvent(Object key, EventArgs e)
   在 System.Windows.Forms.ToolStripMenuItem.OnClick(EventArgs e)
   在 System.Windows.Forms.ToolStripItem.HandleClick(EventArgs e)
   在 System.Windows.Forms.ToolStripItem.HandleMouseUp(MouseEventArgs e)
   在 System.Windows.Forms.ToolStripItem.FireEventInteractive(EventArgs e, ToolStripItemEventType met)
   在 System.Windows.Forms.ToolStripItem.FireEvent(EventArgs e, ToolStripItemEventType met)
   在 System.Windows.Forms.ToolStrip.OnMouseUp(MouseEventArgs mea)
   在 System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)
   在 System.Windows.Forms.Control.WndProc(Message& m)
   在 System.Windows.Forms.ScrollableControl.WndProc(Message& m)
   在 System.Windows.Forms.ToolStrip.WndProc(Message& m)
   在 System.Windows.Forms.MenuStrip.WndProc(Message& m)
   在 System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
   在 System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
   在 System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)


************** 已加载的程序集 **************
mscorlib
    程序集版本:4.0.0.0
    Win32 版本:4.7.3468.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/mscorlib.dll
----------------------------------------
DirectorEditor
    程序集版本:1.0.0.0
    Win32 版本:1.0.0.0
    基本代码:file:///C:/Users/zhanghui03/source/repos/DirectorEditor/DirectorEditor/bin/Debug/DirectorEditor.exe
----------------------------------------
System.Windows.Forms
    程序集版本:4.0.0.0
    Win32 版本:4.7.3324.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Windows.Forms/v4.0_4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll
----------------------------------------
System
    程序集版本:4.0.0.0
    Win32 版本:4.7.3451.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System/v4.0_4.0.0.0__b77a5c561934e089/System.dll
----------------------------------------
System.Drawing
    程序集版本:4.0.0.0
    Win32 版本:4.7.3190.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Drawing/v4.0_4.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll
----------------------------------------
MVPFramework
    程序集版本:1.0.0.0
    Win32 版本:1.0.0.0
    基本代码:file:///C:/Users/zhanghui03/source/repos/DirectorEditor/DirectorEditor/bin/Debug/MVPFramework.DLL
----------------------------------------
System.Core
    程序集版本:4.0.0.0
    Win32 版本:4.7.3468.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Core/v4.0_4.0.0.0__b77a5c561934e089/System.Core.dll
----------------------------------------
HZH_Controls
    程序集版本:1.0.0.0
    Win32 版本:1.0.0.0
    基本代码:file:///C:/Users/zhanghui03/source/repos/DirectorEditor/DirectorEditor/bin/Debug/HZH_Controls.DLL
----------------------------------------
MaterialSkin
    程序集版本:1.0.0.0
    Win32 版本:1.0.0.0
    基本代码:file:///C:/Users/zhanghui03/source/repos/DirectorEditor/DirectorEditor/bin/Debug/MaterialSkin.DLL
----------------------------------------
System.Configuration
    程序集版本:4.0.0.0
    Win32 版本:4.7.3324.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Configuration/v4.0_4.0.0.0__b03f5f7f11d50a3a/System.Configuration.dll
----------------------------------------
System.Xml
    程序集版本:4.0.0.0
    Win32 版本:4.7.3190.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Xml/v4.0_4.0.0.0__b77a5c561934e089/System.Xml.dll
----------------------------------------
mscorlib.resources
    程序集版本:4.0.0.0
    Win32 版本:4.7.3190.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/mscorlib.resources/v4.0_4.0.0.0_zh-Hans_b77a5c561934e089/mscorlib.resources.dll
----------------------------------------
System.Windows.Forms.resources
    程序集版本:4.0.0.0
    Win32 版本:4.7.3190.0 built by: NET472REL1LAST_C
    基本代码:file:///C:/WINDOWS/Microsoft.Net/assembly/GAC_MSIL/System.Windows.Forms.resources/v4.0_4.0.0.0_zh-Hans_b77a5c561934e089/System.Windows.Forms.resources.dll
----------------------------------------

************** JIT 调试 **************
要启用实时(JIT)调试，
该应用程序或计算机的 .config 文件(machine.config)的 system.windows.forms 节中必须设置
jitDebugging 值。
编译应用程序时还必须启用
调试。

例如:

<configuration>
    <system.windows.forms jitDebugging="true" />
</configuration>

启用 JIT 调试后，任何未经处理的异常
都将被发送到在此计算机上注册的 JIT 调试程序，
而不是由此对话框处理。


