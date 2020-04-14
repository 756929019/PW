 using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace PW.Controls.Controls
{
    public class NavigateRoot
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public List<NavigateDetail> Details = new List<NavigateDetail>();
    }
}
