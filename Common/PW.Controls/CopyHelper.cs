using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Interop;
using System.Windows.Controls;

namespace PW.Controls
{
    public class CopyHelper
    {
        public static Bitmap newBitmap;

        /// <summary>
        /// 调用API函数获取整个屏幕的图像
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        private static extern int BitBlt(
            IntPtr hDestDC, 
            int x, 
            int y, 
            int nWidth, 
            int nHeight, 
            IntPtr hSrcDC, 
            int xSrc, 
            int ySrc, 
            int dwRop
        );

        /// <summary>
        /// 截取整个屏幕的图像
        /// </summary>
        /// <returns></returns>
        public static Bitmap CopyFromScreen()
        {
            int width = (int)SystemParameters.PrimaryScreenWidth;
            int height = (int)SystemParameters.PrimaryScreenHeight;
            Bitmap newBitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(newBitmap);

            IntPtr DeskHwnd = GetWindowDC(GetDesktopWindow());
            IntPtr Ghwnd = g.GetHdc();

            BitBlt(Ghwnd, 0, 0, width, height, DeskHwnd, 0, 0, 13369376);
            g.ReleaseHdc(Ghwnd);

            CopyHelper.newBitmap = newBitmap;
            return newBitmap;
        }

        /// <summary>
        /// 将Bitmap转换为WPF中的ImageSource
        /// </summary>
        /// <param name="newBitmap"></param>
        /// <returns></returns>
        public static ImageSource BitMapToImageSource(Bitmap newBitmap)
        {
            MemoryStream mStream = new MemoryStream();
            newBitmap.Save(mStream, ImageFormat.Bmp);
            ImageSourceConverter ISConverter = new ImageSourceConverter();
            ImageSource ISource = ISConverter.ConvertFrom(mStream) as ImageSource;
            return ISource;
        }

        /// <summary>
        /// 获取位图上某点的颜色
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static System.Windows.Media.Color GetColor(System.Windows.Point point)
        {
            int x = Convert .ToInt32(point.X);
            int y = Convert.ToInt32(point.Y);
            System.Drawing.Color tempColor = newBitmap.GetPixel(x, y);
            return System.Windows.Media.Color.FromArgb(tempColor.A, tempColor.R, tempColor.G, tempColor.B);
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Bitmap CutPicture(Rect rect)
        {
            Rectangle rectan = new Rectangle((int)rect.Left,(int)rect.Top,(int)rect.Width,(int)rect.Height);
            return newBitmap.Clone(rectan,System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        }
    }
}
