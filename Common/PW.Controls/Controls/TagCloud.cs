// WPF User Control - TagCloud
// Version 1.0
// Developed by ShenSoft Copyright (c) 2010

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PW.Controls
{
    /// <summary>
    /// User Control - "TagCloud"
    /// </summary>
    public class TagCloud : Grid
    {
        private readonly RotateTransform3D rotateTransform;
        private bool isRunRotation;
        private double slowDownCounter;
        private ObservableCollection<TagCloudItem> tagCollection;
        private TagRotationType rotationType;
        private Point rotateDirection;
        private Canvas canvas;

        private double scaleRatio;
        private double opacityRatio;
        private Random _random = new Random();
        public TagCloud()
        {
            this.Background = Brushes.Transparent;

            canvas = new Canvas(){
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
            };
            this.Children.Add(canvas);
            rotateTransform = new RotateTransform3D();

            SizeChanged += OnPageSizeChanged;

            rotationType = TagRotationType.Mouse;
            rotateDirection = new Point(100, 0);
            slowDownCounter = 500;
            tagCollection = new ObservableCollection<TagCloudItem>();
            scaleRatio = 0.09;
            opacityRatio = 1.3;
        }

        #region Properties

        #region RotateDirectionProperty

        public static DependencyProperty RotateDirectionProperty =
            DependencyProperty.Register("RotateDirection", typeof(Point), typeof(TagCloud), new FrameworkPropertyMetadata(new Point(100, 0),
                                                                                                                                new PropertyChangedCallback(OnRotateDirectionChanged)));

        private static void OnRotateDirectionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TagCloud TagCloud = (TagCloud)sender;
            TagCloud.rotateDirection = (Point)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines the direction of rotation
        /// </summary>
        public Point RotateDirection
        {
            get { return (Point)GetValue(RotateDirectionProperty); }
            set
            {
                SetValue(RotateDirectionProperty, value);
                SetRotateTransform(value);
            }
        }

        #endregion

        #region ScaleRatioProperty

        public static DependencyProperty ScaleRatioProperty =
         DependencyProperty.Register("ScaleRatio", typeof(double), typeof(TagCloud), new FrameworkPropertyMetadata(0.09, new PropertyChangedCallback(OnScaleRatioChanged)));

        private static void OnScaleRatioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TagCloud TagCloud = (TagCloud)sender;
            TagCloud.scaleRatio = (double)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines a scaling of TagCloudItems when they stays further than other elements
        /// </summary>
        public double ScaleRatio
        {
            get { return (double)GetValue(ScaleRatioProperty); }
            set { SetValue(ScaleRatioProperty, value); }
        }

        #endregion

        #region OpacityRatioProperty

        public static DependencyProperty OpacityRatioProperty =
            DependencyProperty.Register("OpacityRatio", typeof(double), typeof(TagCloud), new FrameworkPropertyMetadata(1.3, new PropertyChangedCallback(OnOpacityRatioChanged)));

        private static void OnOpacityRatioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TagCloud TagCloud = (TagCloud)sender;
            TagCloud.opacityRatio = (double)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines a strengh of opacity when TagCloudItem stays behind other elements
        /// </summary>
        public double OpacityRatio
        {
            get { return (double)GetValue(OpacityRatioProperty); }
            set { SetValue(OpacityRatioProperty, value); }
        }

        #endregion

        #region Other properties
        //===================================================================================================================
        /// <summary>
        /// Allow to switch between manual or mouse rotation
        /// </summary>
        public TagRotationType TagRotationType
        {
            get { return rotationType; }
            set { rotationType = value; }
        }

        //===================================================================================================================
        /// <summary>
        /// Collection of elements
        /// </summary>
        public ObservableCollection<TagCloudItem> TagCollection
        {
            get { return tagCollection; }
            set { tagCollection = value; }
        }
        #endregion

        #endregion

        #region Methods
        //===================================================================================================================
        /// <summary>
        /// Stop rotation
        /// </summary>
        public void Stop()
        {
            if (isRunRotation == true)
            {
                CompositionTarget.Rendering -= OnCompositionTargetRendering;
                this.MouseEnter -= OnGridMouseEnter;
                this.MouseLeave -= OnGridMouseLeave;

                slowDownCounter = 500.0;
                isRunRotation = false;
                rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            }

        }
        //===================================================================================================================
        /// <summary>
        /// Start rotation
        /// </summary>
        public void Run()
        {
            if (isRunRotation == false)
            {
                CompositionTarget.Rendering += OnCompositionTargetRendering;
                this.MouseEnter += OnGridMouseEnter;
                this.MouseLeave += OnGridMouseLeave;
                this.MouseMove += OnGridMouseMove;
                slowDownCounter = 500.0;
                isRunRotation = true;
                rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(0.8, 0.6, 0), 0.5);

                SetRotateTransform(rotateDirection);
                RedrawElements();
            }
        }


        #region Private Methods
        //===================================================================================================================
        /// <summary>
        /// Configure rotate transformation
        /// </summary>
        ///<param name="position">Defines the direction of rotation</param>
        private void SetRotateTransform(Point position)
        {
            TagCloudItemSize size = GetElementsSize();

            double x = (position.X - size.XOffset) / size.XRadius;
            double y = (position.Y - size.YOffset) / size.YRadius;
            double angle = Math.Sqrt(x * x + y * y);
            rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(-y, -x, 0.0), angle);
        }
        //===================================================================================================================
        /// <summary>
        /// Redraw all elements in Canvas
        /// </summary>
        private void RedrawElements()
        {
            canvas.Children.Clear();
            double RADIUS = 1.2;
            int length = tagCollection.Count;
            for (int i = 0; i < length; i++)
            {
                /*
                double a = Math.Acos(-1.0 + (2.0 * i) / length);
                double d = Math.Sqrt(length * Math.PI) * a;
                double x = Math.Cos(d) * Math.Sin(a);
                double y = Math.Sin(d) * Math.Sin(a);
                double z = Math.Cos(a);

                tagCollection[i].CenterPoint = new Point3D(x, y, z);
                canvas.Children.Add(tagCollection[i]);
                */
                double k = -1.0 + (2.0 * (i + 1) - 1) / length;
                double a = Math.Acos(k);
                double b = a * Math.Sqrt(length * Math.PI);
                double x = RADIUS * Math.Sin(a) * Math.Cos(b);
                double y = RADIUS * Math.Sin(a) * Math.Sin(b);
                double z = RADIUS * Math.Cos(a);
                tagCollection[i].CenterPoint = new Point3D(x, y, z);
                if (tagCollection[i].Children[0] is Border)
                {
                    Border border = tagCollection[i].Children[0] as Border;
                    if (border.Child is TextBlock)
                    {
                        TextBlock text = border.Child as TextBlock;
                        text.Foreground = new SolidColorBrush(getColor());
                    }
                }
                else if (tagCollection[i].Children[0] is TextBlock)
                {
                    TextBlock text = tagCollection[i].Children[0] as TextBlock;
                    text.Foreground = new SolidColorBrush(getColor());
                }
                canvas.Children.Add(tagCollection[i]);

            }
        }
        Color getColor()
        {
            byte a = (byte)_random.Next(240, 255);
            byte r = (byte)_random.Next(0, 255);
            byte g = (byte)_random.Next(0, 255);
            int intb = g + _random.Next(0, 255);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;
            return Color.FromArgb(a, r, g, b);
        }
        //===================================================================================================================
        /// <summary>
        /// Rotate blocks
        /// </summary>
        private void RotateBlocks()
        {
            TagCloudItemSize size = GetElementsSize();

            foreach (TagCloudItem TagCloudItem in tagCollection)
            {
                Point3D point3D;
                if (rotateTransform.TryTransform(TagCloudItem.CenterPoint, out point3D))
                {
                    TagCloudItem.CenterPoint = point3D;
                    TagCloudItem.Redraw(size, scaleRatio, opacityRatio);
                }
            }
        }

        //===================================================================================================================
        /// <summary>
        /// Get new size for all elements depending of screen resolution
        /// </summary>
        private TagCloudItemSize GetElementsSize()
        {
            return new TagCloudItemSize
            {
                XOffset = canvas.ActualWidth / 2.0,
                YOffset = canvas.ActualHeight / 2.0
            };
        }
        #endregion

        #endregion

        #region Events
        //===================================================================================================================
        /// <summary>
        /// Redraw buttons with new size
        /// </summary>
        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (tagCollection != null)
            {
                TagCloudItemSize size = GetElementsSize();

                foreach (TagCloudItem button in tagCollection)
                {
                    button.Redraw(size, scaleRatio, opacityRatio);
                }
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Rendering
        /// </summary>
        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            if (!(isRunRotation || (slowDownCounter <= 0)))
            {
                AxisAngleRotation3D axis = (AxisAngleRotation3D)rotateTransform.Rotation;
                axis.Angle *= slowDownCounter / 500;
                rotateTransform.Rotation = axis;
                slowDownCounter--;
            }
            if (((AxisAngleRotation3D)rotateTransform.Rotation).Angle > 0.05)
            {
                RotateBlocks();
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Attach new event to grid when mouse enter to grid
        /// </summary>
        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            if (rotationType == TagRotationType.Mouse && isRunRotation == false)
            {
                this.MouseMove += OnGridMouseMove;
                isRunRotation = true;
                slowDownCounter = 500.0;
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Detach event when mouse leave grid
        /// </summary>
        private void OnGridMouseLeave(object sender, MouseEventArgs e)
        {
            if (rotationType == TagRotationType.Mouse && isRunRotation == true)
            {
                this.MouseMove -= OnGridMouseMove;
                isRunRotation = false;
                GC.Collect();
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Move and rotate buttons when mouse position changed
        /// </summary>
        private void OnGridMouseMove(object sender, MouseEventArgs e)
        {
            if (rotationType == TagRotationType.Mouse) rotateDirection = e.GetPosition(canvas);
            SetRotateTransform(rotateDirection);
        }
        #endregion

    }
}
