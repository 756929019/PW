using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PW.Controls
{
    public class ColorPicker : Control
    {
        #region Public Methods

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));

            // Register Event Handler for slider
            EventManager.RegisterClassHandler(typeof(ColorPicker), Slider.ValueChangedEvent, new RoutedPropertyChangedEventHandler<double>(ColorPicker.OnSliderValueChanged));

            // Register Event Handler for Hsv Control
            EventManager.RegisterClassHandler(typeof(ColorPicker), HsvControl.SelectedColorChangedEvent, new RoutedPropertyChangedEventHandler<Color>(ColorPicker.OnHsvControlSelectedColorChanged));
        }

        #endregion

        #region Dependency Properties

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker),
            new UIPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnSelectedColorPropertyChanged)));

        public bool FixedSliderColor
        {
            get { return (bool)GetValue(FixedSliderColorProperty); }
            set { SetValue(FixedSliderColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FixedSliderColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FixedSliderColorProperty =
            DependencyProperty.Register("FixedSliderColor", typeof(bool), typeof(SpectrumSlider),
            new UIPropertyMetadata(false, new PropertyChangedCallback(OnFixedSliderColorPropertyChanged)));

        #endregion

        #region Routed Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedColorChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>),
            typeof(ColorPicker)
        );

        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        #endregion

        #region Event Handling

        private void OnSliderValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {
            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;
            if (e.OriginalSource == m_redColorSlider ||
                e.OriginalSource == m_greenColorSlider ||
                e.OriginalSource == m_blueColorSlider ||
                e.OriginalSource == m_alphaColorSlider)
            {
                Color newColor = GetRgbColor();
                UpdateHsvControlColor(newColor);
                UpdateSelectedColor(newColor);
            }
            else if (e.OriginalSource == m_spectrumSlider)
            {
                double hue = m_spectrumSlider.Hue;
                UpdateHsvControlHue(hue);
                Color newColor = GetHsvColor();
                UpdateRgbColors(newColor);
                UpdateSelectedColor(newColor);
            }

            m_withinChange = false;
        }

        private static void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorPicker colorPicker = sender as ColorPicker;
            colorPicker.OnSliderValueChanged(e);
        }

        private void OnHsvControlSelectedColorChanged(RoutedPropertyChangedEventArgs<Color> e)
        {
            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;

            Color newColor = GetHsvColor();
            UpdateRgbColors(newColor);
            UpdateSelectedColor(newColor);

            m_withinChange = false;
        }

        private static void OnHsvControlSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            ColorPicker colorPicker = sender as ColorPicker;
            colorPicker.OnHsvControlSelectedColorChanged(e);
        }

        private void OnSelectedColorPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!m_templateApplied)
                return;

            // Avoid endless loop
            if (m_withinChange)
                return;

            m_withinChange = true;

            Color newColor = (Color)e.NewValue;
            UpdateControlColors(newColor);

            m_withinChange = false;
        }

        private static void OnSelectedColorPropertyChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = relatedObject as ColorPicker;
            colorPicker.OnSelectedColorPropertyChanged(e);
        }

        private static void OnFixedSliderColorPropertyChanged(
            DependencyObject relatedObject, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = relatedObject as ColorPicker;
            colorPicker.UpdateColorSlidersBackground();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_redColorSlider = GetTemplateChild(RedColorSliderName) as ColorSlider;
            m_greenColorSlider = GetTemplateChild(GreenColorSliderName) as ColorSlider;
            m_blueColorSlider = GetTemplateChild(BlueColorSliderName) as ColorSlider;
            m_alphaColorSlider = GetTemplateChild(AlphaColorSliderName) as ColorSlider;
            m_spectrumSlider = GetTemplateChild(SpectrumSliderName) as SpectrumSlider;
            m_hsvControl = GetTemplateChild(HsvControlName) as HsvControl;

            m_templateApplied = true;
            UpdateControlColors(SelectedColor);

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == UIElement.IsVisibleProperty && (bool)e.NewValue == true)
                Focus();
            base.OnPropertyChanged(e);
        }

        #endregion

        #region Private Methods

        private void SetColorSliderBackground(ColorSlider colorSlider, Color leftColor, Color rightColor)
        {
            colorSlider.LeftColor = leftColor;
            colorSlider.RightColor = rightColor;
        }

        private void UpdateColorSlidersBackground()
        {
            if (FixedSliderColor)
            {
                SetColorSliderBackground(m_redColorSlider, Colors.Red, Colors.Red);
                SetColorSliderBackground(m_greenColorSlider, Colors.Green, Colors.Green);
                SetColorSliderBackground(m_blueColorSlider, Colors.Blue, Colors.Blue);
                SetColorSliderBackground(m_alphaColorSlider, Colors.Transparent, Colors.White);
            }
            else
            {
                byte red = SelectedColor.R;
                byte green = SelectedColor.G;
                byte blue = SelectedColor.B;
                SetColorSliderBackground(m_redColorSlider,
                    Color.FromRgb(0, green, blue), Color.FromRgb(255, green, blue));
                SetColorSliderBackground(m_greenColorSlider,
                    Color.FromRgb(red, 0, blue), Color.FromRgb(red, 255, blue));
                SetColorSliderBackground(m_blueColorSlider,
                    Color.FromRgb(red, green, 0), Color.FromRgb(red, green, 255));
                SetColorSliderBackground(m_alphaColorSlider,
                    Color.FromArgb(0, red, green, blue), Color.FromArgb(255, red, green, blue));
            }
        }

        private Color GetRgbColor()
        {
            return Color.FromArgb(
                (byte)m_alphaColorSlider.Value,
                (byte)m_redColorSlider.Value,
                (byte)m_greenColorSlider.Value,
                (byte)m_blueColorSlider.Value);
        }

        private void UpdateRgbColors(Color newColor)
        {
            m_alphaColorSlider.Value = newColor.A;
            m_redColorSlider.Value = newColor.R;
            m_greenColorSlider.Value = newColor.G;
            m_blueColorSlider.Value = newColor.B;
        }

        private Color GetHsvColor()
        {
            Color hsvColor = m_hsvControl.SelectedColor;
            hsvColor.A = (byte)m_alphaColorSlider.Value;
            return hsvColor;
        }

        private void UpdateSpectrumColor(Color newColor)
        {
        }

        private void UpdateHsvControlHue(double hue)
        {
            m_hsvControl.Hue = hue;
        }


        private void UpdateHsvControlColor(Color newColor)
        {
            double hue, saturation, value;

            ColorUtils.ConvertRgbToHsv(newColor, out hue, out saturation, out value);

            // if saturation == 0 or value == 1 hue don't count so we save the old hue
            if (saturation != 0 && value != 0)
                m_hsvControl.Hue = hue;

            m_hsvControl.Saturation = saturation;
            m_hsvControl.Value = value;

            m_spectrumSlider.Hue = m_hsvControl.Hue;
        }

        private void UpdateSelectedColor(Color newColor)
        {
            Color oldColor = SelectedColor;
            SelectedColor = newColor;

            if (!FixedSliderColor)
                UpdateColorSlidersBackground();

            ColorUtils.FireSelectedColorChangedEvent(this, SelectedColorChangedEvent, oldColor, newColor);
        }

        private void UpdateControlColors(Color newColor)
        {
            UpdateRgbColors(newColor);
            UpdateSpectrumColor(newColor);
            UpdateHsvControlColor(newColor);
            UpdateColorSlidersBackground();
        }

        #endregion

        #region Private Members

        private const string RedColorSliderName = "PART_RedColorSlider";
        private const string GreenColorSliderName = "PART_GreenColorSlider";
        private const string BlueColorSliderName = "PART_BlueColorSlider";
        private const string AlphaColorSliderName = "PART_AlphaColorSlider";

        private const string SpectrumSliderName = "PART_SpectrumSlider1";
        private const string HsvControlName = "PART_HsvControl";

        private ColorSlider m_redColorSlider;
        private ColorSlider m_greenColorSlider;
        private ColorSlider m_blueColorSlider;
        private ColorSlider m_alphaColorSlider;

        private SpectrumSlider m_spectrumSlider;

        private HsvControl m_hsvControl;

        private bool m_withinChange;
        private bool m_templateApplied;


        #endregion
    }
}
