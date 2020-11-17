<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/" name="outputtemplate">
  using PW.SystemSet.ViewModel;
  using System.ComponentModel.Composition;
  using System.Windows;
  using System.Windows.Controls;

  namespace PW.SystemSet.Views
  {
      [Export(typeof(<xsl:value-of select="TableModel/ModelName"/>View))]
      public partial class <xsl:value-of select="TableModel/ModelName"/>View : UserControl
      {
          public <xsl:value-of select="TableModel/ModelName"/>View()
          {
              InitializeComponent();
          }

          private void UserControl_Loaded(object sender, RoutedEventArgs e)
          {
              this.DataContext = new <xsl:value-of select="TableModel/ModelName"/>ViewModel();
          }
      }
  }
</xsl:template>
</xsl:stylesheet>