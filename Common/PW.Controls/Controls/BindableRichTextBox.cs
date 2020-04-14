﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PW.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        public new FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDucumentChanged)));

        private static void OnDucumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)d;
            rtb.Document = (FlowDocument)e.NewValue;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            Size size = MeasureString(tr.Text, rtb);
            if(rtb.IsReadOnly)
            {
                rtb.Width = size.Width + 30;
                if (size.Width == 0)
                {
                    try
                    {
                        double wid = 0;
                        foreach(var item in rtb.Document.Blocks)
                        {
                            foreach (var obj in ((Paragraph)item).Inlines)
                            {
                                if (obj is InlineUIContainer)
                                {
                                    System.Windows.Controls.Image img = (System.Windows.Controls.Image)((InlineUIContainer)obj).Child;
                                    if (img.Source.ToString() == "System.Windows.Interop.InteropBitmap")
                                    {
                                        wid += img.Width;
                                    }
                                    else
                                    {
                                        wid += img.Width;
                                    }
                                }
                            }
                        }
                        rtb.Width = wid + 30;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            //rtb.Width = size.Width + 30;
        }
        private static Size MeasureString(string candidate, RichTextBox rtb)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(rtb.FontFamily, rtb.FontStyle, rtb.FontWeight, rtb.FontStretch),
                rtb.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
