using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace PW.Controls
{
    public class ColorComboBox : Control
    {
        #region Public Methods

        static ColorComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorComboBox), new FrameworkPropertyMetadata(typeof(ColorComboBox)));

            EventManager.RegisterClassHandler(typeof(ColorComboBox), ColorPicker.SelectedColorChangedEvent, new RoutedPropertyChangedEventHandler<Color>(OnColorPickerSelectedColorChanged));
        }

        #endregion

        #region Dependency Properties

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(ColorComboBox),
            new UIPropertyMetadata(false, new PropertyChangedCallback(OnIsDropDownOpenChanged)));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorComboBox),
            new UIPropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnSelectedColorPropertyChanged)));

        #endregion

        #region Handling Events

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorComboBox colorComboBox = d as ColorComboBox;
            bool newValue = (bool)e.NewValue;

            // Mask HistTest visibility of toggle button otherwise when pressing it
            // and popup is open the popup is closed (since StaysOpen is false)
            // and reopens immediately
            if (colorComboBox.m_toggleButton != null)
            {
                colorComboBox.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                      delegate ()
                      {
                          colorComboBox.m_toggleButton.IsHitTestVisible = !newValue;
                      }
                  ));
            }
            //Console.WriteLine("OnIsDropDownOpenChanged - Popup Focused {0} {1}",
            //    colorComboBox.m_popup.IsFocused, colorComboBox.m_popup.IsKeyboardFocused);
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorComboBox colorComboBox = d as ColorComboBox;

            if (colorComboBox.m_withinChange)
                return;

            colorComboBox.m_withinChange = true;
            if (colorComboBox.m_colorPicker != null)
                colorComboBox.m_colorPicker.SelectedColor = colorComboBox.SelectedColor;
            colorComboBox.m_withinChange = false;
        }

        private static void OnColorPickerSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            ColorComboBox colorComboBox = sender as ColorComboBox;

            if (colorComboBox.m_withinChange)
                return;

            colorComboBox.m_withinChange = true;
            if (colorComboBox.m_colorPicker != null)
                colorComboBox.SelectedColor = colorComboBox.m_colorPicker.SelectedColor;
            colorComboBox.m_withinChange = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_popup = GetTemplateChild("PART_Popup") as UIElement;
            m_colorPicker = GetTemplateChild("PART_ColorPicker") as ColorPicker;
            m_toggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;

            if (m_colorPicker != null)
                m_colorPicker.SelectedColor = SelectedColor;
        }


        #endregion

        #region Private Members

        private UIElement m_popup;
        private ColorPicker m_colorPicker;
        private bool m_withinChange;
        private ToggleButton m_toggleButton;

        #endregion
    }
}
