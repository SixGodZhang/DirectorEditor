using MaterialSkin.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSkin.Common
{
    /// <summary>
    /// 处理字体
    /// </summary>
    public class FontManager
    {

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        /// <summary>
        /// 缓存从资源中加载的字体
        /// </summary>
        private static readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        /// <summary>
        /// 加载字体资源
        /// </summary>
        /// <param name="fontResource"></param>
        /// <returns></returns>
        private static FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }

        /// <summary>
        /// 获取指定名字的字体
        /// </summary>
        /// <param name="fontFamilyName"></param>
        /// <returns></returns>
        public static FontFamily GetFont(string fontFamilyName)
        {
            IEnumerable<FontFamily> families =  privateFontCollection.Families.Where(family => family.Name == fontFamilyName);
            if(families.Count() >= 1)
            {
                return families.ElementAt(0);
            }
            else
            {
                try
                {
                    // 获取字体资源
                    object obj = Resources.ResourceManager.GetObject(fontFamilyName, Resources.Culture);
                    if (obj != null)
                    {
                        // 加载字体
                        return LoadFont((byte[])(obj));
                    }
                }catch(Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    return null;
                }

            }

            return null;
        }
    }
}
