using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PW.Controls
{
    /// <summary>
    /// WindowCopy.xaml 的交互逻辑
    /// </summary>
    public partial class WindowCopy : Window
    {
        private ImageSource ISource;
        private int width;
        private int height;
        private Color mainColor;
        private Point mousePoint;
        private Point firstPoint = new Point(0, 0);
        private Point lastPoint = new Point(0, 0);
        private bool IsMouseDown_T = false;
        private bool IsMouseDown_R = false;
        private bool IsMouseDown_B = false;
        private bool IsMouseDown_L = false;
        //private bool IsMouseDown_Copy = false;
        private bool IsShouldChange = true;

        public Window winMain;
        public Rectangle rectImage;

        Rectangle boxRect = null;
        Brush[] brushes = { Brushes.Red, Brushes.LightBlue, Brushes.SeaGreen, Brushes.Sienna };
        List<Point> pointList = new List<Point>();
        System.Drawing.Graphics gbox = null;
        public WindowCopy(ImageSource ISource, Window _mainWindow, Rectangle _rectImage)
        {
            InitializeComponent();
            this.winMain = _mainWindow;
            this.rectImage = _rectImage;
            this.ISource = ISource;
            this.mainColor = Color.FromArgb((byte)128, (byte)0, (byte)0, (byte)0);
            this.width = (int)SystemParameters.PrimaryScreenWidth;
            this.height = (int)SystemParameters.PrimaryScreenHeight;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            toolsSp.Visibility = Visibility.Collapsed;
            this.Background = new ImageBrush(ISource);
            polygonCopy.Points = InitPoints(firstPoint, lastPoint);
            polygonCopy.Fill = new SolidColorBrush(mainColor);
        }

        /// <summary>
        /// 鼠标按下时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (rectangleCopy.Width == 0 || rectangleCopy.Height == 0)
                {
                    firstPoint = e.GetPosition(this);
                }
                else
                {
                    IsShouldChange = false;

                    if (rectLT.IsMouseOver)
                    {
                        IsShouldChange = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, rectangleCopy.Margin.Top + rectangleCopy.Height);
                    }
                    else if (rectT.IsMouseOver)
                    {
                        IsShouldChange = true;
                        IsMouseDown_T = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top + rectangleCopy.Height);
                    }
                    else if (rectTR.IsMouseOver)
                    {
                        IsShouldChange = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top + rectangleCopy.Height);
                    }
                    else if (rectR.IsMouseOver)
                    {
                        IsShouldChange = true;
                        IsMouseDown_R = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top);
                    }
                    else if (rectRB.IsMouseOver)
                    {
                        IsShouldChange = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top);
                    }
                    else if (rectB.IsMouseOver)
                    {
                        IsShouldChange = true;
                        IsMouseDown_B = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, rectangleCopy.Margin.Top);
                    }
                    else if (rectBL.IsMouseOver)
                    {
                        IsShouldChange = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, rectangleCopy.Margin.Top);
                    }
                    else if (rectL.IsMouseOver)
                    {
                        IsShouldChange = true;
                        IsMouseDown_L = true;
                        firstPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, rectangleCopy.Margin.Top + rectangleCopy.Height);
                    }
                    //else if (rectangleCopy.IsMouseOver)
                    //{
                    //    IsShouldChange = true;
                    //    IsMouseDown_Copy = true;
                    //    mousePoint = e.GetPosition(this);
                    //}
                }
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (rectangleCopy.Width == 0 || rectangleCopy.Height == 0)
                {
                    this.Close();
                    if (winMain != null)
                        winMain.Show();
                }
                else
                {
                    rectangleCopy.Width = 0;
                    rectangleCopy.Height = 0;
                    firstPoint = lastPoint = new Point(0, 0);

                    SetAll();
                }
                IsShouldChange = true;
            }
        }

        /// <summary>
        /// 鼠标在窗体上移动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                lastPoint = e.GetPosition(this);

                if (rectT.IsMouseOver || IsMouseDown_T)
                {
                    lastPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, lastPoint.Y);
                }
                if (rectR.IsMouseOver || IsMouseDown_R)
                {
                    lastPoint = new Point(lastPoint.X, rectangleCopy.Margin.Top + rectangleCopy.Height);
                }
                if (rectB.IsMouseOver || IsMouseDown_B)
                {
                    lastPoint = new Point(rectangleCopy.Margin.Left, lastPoint.Y);
                }
                if (rectL.IsMouseOver || IsMouseDown_L)
                {
                    lastPoint = new Point(lastPoint.X, rectangleCopy.Margin.Top);
                }
                //if (rectangleCopy.IsMouseOver || IsMouseDown_Copy)
                //{
                //    double moveX = e.GetPosition(this).X - mousePoint.X;
                //    double moveY = e.GetPosition(this).Y - mousePoint.Y;
                //    firstPoint = new Point(rectangleCopy.Margin.Left + moveX,rectangleCopy.Margin.Top + moveY);
                //    lastPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width + moveX, rectangleCopy.Margin.Top + rectangleCopy.Height + moveY);

                //    mousePoint = e.GetPosition(this);
                //}

                if (IsShouldChange)
                {
                    SetAll();
                }
            }
        }

        /// <summary>
        /// 鼠标释放时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                IsMouseDown_T = false;
                IsMouseDown_R = false;
                IsMouseDown_B = false;
                IsMouseDown_L = false;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FinshCopy();
        }

        void FinshCopy()
        {
            Rect rect = new Rect(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top, rectangleCopy.Width, rectangleCopy.Height);
            System.Drawing.Bitmap bit = CopyHelper.CutPicture(rect);
            ImageSource ISource = CopyHelper.BitMapToImageSource(bit);
            if (rectImage != null)
            {
                rectImage.Fill = new ImageBrush(ISource);
                rectImage.Width = rectangleCopy.Width;
                rectImage.Height = rectangleCopy.Height;
            }
            Clipboard.SetImage(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bit.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()));
            this.Close();
            if(winMain!=null)
                winMain.Show();
        }

        void SaveImage()
        {
            Rect rect = new Rect(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top, rectangleCopy.Width, rectangleCopy.Height);
            System.Drawing.Bitmap bit = CopyHelper.CutPicture(rect);
            ImageSource ISource = CopyHelper.BitMapToImageSource(bit);
            Clipboard.SetImage(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bit.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPEG Files(*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            sfd.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录 
            if (sfd.ShowDialog() == true)
            {
                var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create((System.Windows.Media.Imaging.BitmapSource)ISource));
                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    encoder.Save(stream);

                this.Close();
                if(winMain!=null)
                    winMain.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetAll()
        {
            SetSPMsg(firstPoint, lastPoint);
            SetBorderAndRect(firstPoint, lastPoint);
            polygonCopy.Points = InitPoints(firstPoint, lastPoint);
        }

        /// <summary>
        /// 根据矩形选择框的两个相对的端点来初始化Polygon的Points
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="lastPoint"></param>
        /// <returns></returns>
        private PointCollection InitPoints(Point firstPoint, Point lastPoint)
        {
            double minX = Math.Min(firstPoint.X, lastPoint.X);
            double minY = Math.Min(firstPoint.Y, lastPoint.Y);
            double maxX = Math.Max(firstPoint.X, lastPoint.X);
            double maxY = Math.Max(firstPoint.Y, lastPoint.Y);

            List<Point> listPoints = new List<Point>();
            listPoints.Add(new Point(0, 0));
            listPoints.Add(new Point(width, 0));
            listPoints.Add(new Point(width, height));
            listPoints.Add(new Point(0, height));
            listPoints.Add(new Point(0, 0));
            listPoints.Add(new Point(minX, minY));
            listPoints.Add(new Point(maxX, minY));
            listPoints.Add(new Point(maxX, maxY));
            listPoints.Add(new Point(minX, maxY));
            listPoints.Add(new Point(minX, minY));

            return new PointCollection(listPoints);
        }

        /// <summary>
        /// 显示截图时的信息
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="lastPoint"></param>
        private void SetSPMsg(Point firstPoint, Point lastPoint)
        {
            if (rectangleCopy.Width == 0 || rectangleCopy.Height == 0)
            {
                SPMsg.Visibility = Visibility.Hidden;
                toolsSp.Visibility = Visibility.Hidden;
            }
            else
            {
                SPMsg.Visibility = Visibility.Visible;
                Point tempPoint = Mouse.GetPosition(this);
                Color tempColor = CopyHelper.GetColor(tempPoint);
                txtSize.Text = "区域大小：" + rectangleCopy.Width.ToString() + " * " + rectangleCopy.Height.ToString();
                txtPos.Text = "鼠标位置：(" + tempPoint.X.ToString() + "," + tempPoint.Y.ToString() + ")";
                txtRGB.Text = "当前RGB：(" + tempColor.R.ToString() + "," + tempColor.G.ToString() + "," + tempColor.B + ")"; ;

                double minX = Math.Min(firstPoint.X, lastPoint.X);
                double minY = Math.Min(firstPoint.Y, lastPoint.Y);

                if (minY < SPMsg.Height + 8)
                {
                    SPMsg.Margin = new Thickness(minX, minY + 8, 0, 0);
                }
                else
                {
                    SPMsg.Margin = new Thickness(minX, minY - SPMsg.Height - 8, 0, 0);
                }

                double maxX = Math.Max(firstPoint.X, lastPoint.X);
                double maxY = Math.Max(firstPoint.Y, lastPoint.Y);
                toolsSp.Visibility = Visibility.Visible;
                if ((this.ActualHeight - maxY) > (toolsSp.Height+8))
                {
                    toolsSp.Margin = new Thickness(maxX- toolsSp.ActualWidth, maxY + 8, 0, 0);
                }
                else
                {
                    toolsSp.Margin = new Thickness(maxX - toolsSp.ActualWidth, maxY - toolsSp.Height - 8, 0, 0);
                }
            }
        }

        /// <summary>
        /// 根据矩形选择框的两个相对的端点来设置矩形选框的颜色及矩形选框顶点及边上小矩形的位置
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="lastPoint"></param>
        private void SetBorderAndRect(Point firstPoint, Point lastPoint)
        {
            double minX = Math.Min(firstPoint.X, lastPoint.X);
            double minY = Math.Min(firstPoint.Y, lastPoint.Y);
            double maxX = Math.Max(firstPoint.X, lastPoint.X);
            double maxY = Math.Max(firstPoint.Y, lastPoint.Y);

            rectangleCopy.Margin = new Thickness(minX, minY, 0, 0);
            rectangleCopy.Width = maxX - minX;
            rectangleCopy.Height = maxY - minY;

            if (rectangleCopy.Width == 0 || rectangleCopy.Height == 0)
            {
                rectLT.Visibility = Visibility.Hidden;
                rectT.Visibility = Visibility.Hidden;
                rectTR.Visibility = Visibility.Hidden;
                rectR.Visibility = Visibility.Hidden;
                rectRB.Visibility = Visibility.Hidden;
                rectB.Visibility = Visibility.Hidden;
                rectBL.Visibility = Visibility.Hidden;
                rectL.Visibility = Visibility.Hidden;
            }
            else
            {
                rectLT.Visibility = Visibility.Visible;
                rectT.Visibility = Visibility.Visible;
                rectTR.Visibility = Visibility.Visible;
                rectR.Visibility = Visibility.Visible;
                rectRB.Visibility = Visibility.Visible;
                rectB.Visibility = Visibility.Visible;
                rectBL.Visibility = Visibility.Visible;
                rectL.Visibility = Visibility.Visible;
            }

            rectLT.Margin = new Thickness(minX - 2, minY - 2, 0, 0);
            rectT.Margin = new Thickness(minX - 2 + rectangleCopy.Width / 2, minY - 2, 0, 0);
            rectTR.Margin = new Thickness(maxX - 3, minY - 2, width - maxX - 2, height - maxY - 2);
            rectR.Margin = new Thickness(maxX - 3, minY - 2 + rectangleCopy.Height / 2, 0, 0);
            rectRB.Margin = new Thickness(maxX - 3, maxY - 3, 0, 0);
            rectB.Margin = new Thickness(minX - 2 + rectangleCopy.Width / 2, maxY - 3, 0, 0);
            rectBL.Margin = new Thickness(minX - 2, maxY - 3, 0, 0);
            rectL.Margin = new Thickness(minX - 2, minY - 2 + rectangleCopy.Height / 2, 0, 0);
        }

        //画方框
        private void DrawSquare(System.Windows.Point point1, System.Windows.Point point2)
        {
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            FinshCopy();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (winMain != null)
                winMain.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveImage();
        }


        /// <summary>
        /// 移动矩形选框MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rectangleCopy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mousePoint = e.GetPosition(null);
            }
        }

        /// <summary>
        /// 移动矩形选框MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rectangleCopy_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //if (btnBox.IsChecked.Value)
                //{
                //    Point tempPoint = e.GetPosition(this.myCanvas);
                //    if (tempPoint != this.mousePoint && boxRect == null)
                //    {
                //        boxRect = new Rectangle() {  StrokeThickness = 2,Stroke = brushes[0]};
                //        this.myCanvas.Children.Add(boxRect);
                //    }

                //    boxRect.Width = Math.Abs(tempPoint.X - mousePoint.X);
                //    boxRect.Height = Math.Abs(tempPoint.Y - mousePoint.Y);
                //    Canvas.SetLeft(boxRect, Math.Min(tempPoint.X, mousePoint.X));
                //    Canvas.SetTop(boxRect, Math.Min(tempPoint.Y, mousePoint.Y));
                //}
                //else if (btnLine.IsChecked.Value)
                //{
                //    Point tempPoint = e.GetPosition(this.myCanvas);

                //    if (pointList.Count == 0)
                //    {
                //        // 加入起始点                    
                //        pointList.Add(new Point(this.mousePoint.X, this.mousePoint.Y));
                //    }
                //    else
                //    {
                //        // 加入移动过程中的point                    
                //        pointList.Add(tempPoint);
                //    }
                //    // 去重复点                
                //    var disList = pointList.Distinct().ToList();
                //    var count = disList.Count();
                //    // 总点数                
                //    if (tempPoint != this.mousePoint && this.mousePoint != null)
                //    {
                //        var l = new Line();
                //        l.Stroke = brushes[0];
                //        l.StrokeThickness = 2;
                //        if (count < 2)
                //            return;
                //        l.X1 = disList[count - 2].X;
                //        // count-2  保证 line的起始点为点集合中的倒数第二个点。                    
                //        l.Y1 = disList[count - 2].Y;
                //        // 终点X,Y 为当前point的X,Y                    
                //        l.X2 = tempPoint.X;
                //        l.Y2 = tempPoint.Y;
                //        myCanvas.Children.Add(l);
                //    }
                //}
                Point tempPoint = e.GetPosition(this);

                double deltaX = tempPoint.X - mousePoint.X;
                double deltaY = tempPoint.Y - mousePoint.Y;

                rectangleCopy.Margin = new Thickness(rectangleCopy.Margin.Left + deltaX, rectangleCopy.Margin.Top + deltaY, 0, 0);
                mousePoint = e.GetPosition(null);

                firstPoint = new Point(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top);
                lastPoint = new Point(rectangleCopy.Margin.Left + rectangleCopy.Width, rectangleCopy.Margin.Top + rectangleCopy.Height);

                SetAll();
            }
        }

        private void rectangleCopy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (btnBox.IsChecked.Value)
            //{
            //    this.boxRect = null;
            //}
        }

        System.Drawing.Image boximg = null;
        private void btnBox_Click(object sender, RoutedEventArgs e)
        {
            if(gbox!=null)
            {
                //boximg = ImageSourceConvertToGDI();
                //gbox = System.Drawing.Graphics.FromImage(boximg);
                //gbox.DrawImage(boximg, (int)rectangleCopy.Margin.Left, (int)rectangleCopy.Margin.Top, boximg.Width, boximg.Height);
            }
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            if (gbox != null)
            {
                //boximg = ImageSourceConvertToGDI();
                //gbox = System.Drawing.Graphics.FromImage(boximg);
                //gbox.DrawImage(boximg, (int)rectangleCopy.Margin.Left, (int)rectangleCopy.Margin.Top, boximg.Width, boximg.Height);
            }
        }
        System.Drawing.Image ImageSourceConvertToGDI()
        {
            Rect rect = new Rect(rectangleCopy.Margin.Left, rectangleCopy.Margin.Top, rectangleCopy.Width, rectangleCopy.Height);
            ImageSource ISource = CopyHelper.BitMapToImageSource(CopyHelper.CutPicture(rect));
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create((System.Windows.Media.Imaging.BitmapSource)ISource));

            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);
            ms.Flush();
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

    }
}
