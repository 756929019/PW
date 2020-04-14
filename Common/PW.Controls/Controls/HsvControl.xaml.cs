using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace PW.Controls
{
    public class HsvControl : Control
    {
        #region Public Methods

        static HsvControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HsvControl), new FrameworkPropertyMetadata(typeof(HsvControl)));

            // Register Event Handler for the Thumb 
            EventManager.RegisterClassHandler(typeof(HsvControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(HsvControl.OnThumbDragDelta));
        }

        #endregion

        #region Dependency Properties

        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(HsvControl),
                new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnHueChanged)));

        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        public static readonly DependencyProperty SaturationProperty =
            DependencyProperty.Register("Saturation", typeof(double), typeof(HsvControl),
                new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnSaturationChanged)));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(HsvControl),
                new UIPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChanged)));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(HsvControl), new UIPropertyMetadata(Colors.Transparent));

        #endregion

        #region Routed Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedColorChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(HsvControl)
        );

        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        #endregion

        #region Event Handlers

        private void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            double offsetX = m_thumbTransform.X + e.HorizontalChange;
            double offsetY = m_thumbTransform.Y + e.VerticalChange;

            UpdatePositionAndSaturationAndValue(offsetX, offsetY);
        }

        private static void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            HsvControl hsvControl = sender as HsvControl;
            hsvControl.OnThumbDragDelta(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (m_thumb != null)
            {
                Point position = e.GetPosition(this);

                UpdatePositionAndSaturationAndValue(position.X, position.Y);

                // Initiate mouse event on thumb so it will start drag
                m_thumb.RaiseEvent(e);
            }

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            UpdateThumbPosition();

            base.OnRenderSizeChanged(sizeInfo);
        }

        private static void OnHueChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            HsvControl hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.m_withinUpdate)
                hsvControl.UpdateSelectedColor();
        }

        private static void OnSaturationChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            HsvControl hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.m_withinUpdate)
                hsvControl.UpdateThumbPosition();
        }

        private static void OnValueChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            HsvControl hsvControl = relatedObject as HsvControl;
            if (hsvControl != null && !hsvControl.m_withinUpdate)
                hsvControl.UpdateThumbPosition();
        }

        #endregion

        #region Overridden Members

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_thumb = GetTemplateChild(ThumbName) as Thumb;
            if (m_thumb != null)
            {
                UpdateThumbPosition();
                m_thumb.RenderTransform = m_thumbTransform;
            }
        }

        #endregion

        #region Private Methods

        // Limit value to range (0 , max] 
        private double LimitValue(double value, double max)
        {
            if (value < 0)
                value = 0;
            if (value > max)
                value = max;
            return value;
        }

        private void UpdateSelectedColor()
        {
            Color oldColor = SelectedColor;
            Color newColor = ColorUtils.ConvertHsvToRgb(Hue, Saturation, Value);

            SelectedColor = newColor;
            ColorUtils.FireSelectedColorChangedEvent(this, SelectedColorChangedEvent, oldColor, newColor);
        }

        private void UpdatePositionAndSaturationAndValue(double positionX, double positionY)
        {
            positionX = LimitValue(positionX, ActualWidth);
            positionY = LimitValue(positionY, ActualHeight);

            m_thumbTransform.X = positionX;
            m_thumbTransform.Y = positionY;

            Saturation = positionX / ActualWidth;
            Value = 1 - positionY / ActualHeight;

            UpdateSelectedColor();
        }

        private void UpdateThumbPosition()
        {
            m_thumbTransform.X = Saturation * ActualWidth;
            m_thumbTransform.Y = (1 - Value) * ActualHeight;

            SelectedColor = ColorUtils.ConvertHsvToRgb(Hue, Saturation, Value);
        }

        #endregion

        #region Private Members

        private const string ThumbName = "PART_Thumb";

        private TranslateTransform m_thumbTransform = new TranslateTransform();
        private Thumb m_thumb;
        private bool m_withinUpdate = false;

        #endregion
    }
}
