﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/" name="outputtemplate">
  <UserControl xmlns:Controls="clr-namespace:PW.Controls.Controls;assembly=PW.Controls"  x:Class="PW.SystemSet.Views.UserView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
             xmlns:local="clr-namespace:PW.SystemSet.Views"
             mc:Ignorable="d"
             d:DesignHeight="300" d:DesignWidth="300" Loaded="UserControl_Loaded">
    <Grid>
      <Grid.RowDefinitions>
        <RowDefinition Height="Auto"></RowDefinition>
        <RowDefinition Height="40"></RowDefinition>
        <RowDefinition Height="*"></RowDefinition>
        <RowDefinition Height="30"></RowDefinition>
      </Grid.RowDefinitions>
      <Border>
        <Grid HorizontalAlignment="Left" VerticalAlignment="Center">
          <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"></ColumnDefinition>
            <ColumnDefinition Width="*"></ColumnDefinition>
            <ColumnDefinition Width="*"></ColumnDefinition>
          </Grid.ColumnDefinitions>
          <Grid.RowDefinitions>
            <RowDefinition Height="30"></RowDefinition>
            <RowDefinition Height="30"></RowDefinition>
          </Grid.RowDefinitions>
          <StackPanel Grid.Column="0" Orientation="Horizontal">
            <TextBlock Text="username" VerticalAlignment="Center"></TextBlock>
            <TextBox Width="100" Height="30" Text="｛Binding QueryObj.USERNAME｝"></TextBox>
          </StackPanel>
          <StackPanel Grid.Column="1" Orientation="Horizontal">
            <TextBlock Text="username" VerticalAlignment="Center"></TextBlock>
            <TextBox Width="100" Height="30" Text="｛Binding QueryObj.USERNAME｝"></TextBox>
          </StackPanel>
          <StackPanel Grid.Column="2" Orientation="Horizontal">
            <TextBlock Text="username" VerticalAlignment="Center"></TextBlock>
            <TextBox Width="100" Height="30" Text="｛Binding QueryObj.USERNAME｝"></TextBox>
          </StackPanel>
          <Controls:FiconButton Grid.Row="1" Content="查询" FIcon="&#xe658;" Command="｛Binding Path=QueryCommand｝"/>
        </Grid>
      </Border>
      <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="0,5,0,5">
        <Controls:FiconButton Content="添加" FIcon="&#xe62c;" Padding="5,0,5,0" CornerRadius="10,0,0,10" AllowsAnimation="True" Command="｛Binding Path=AddCommand｝"/>
        <Controls:FiconButton Content="删除" FIcon="&#xe659;" Padding="5,0,5,0" AllowsAnimation="True" BorderThickness="0,1" Command="｛Binding Path=DeleteCommand｝"/>
        <Controls:FiconButton Content="修改" FIcon="&#xe665;" Padding="5,0,5,0" AllowsAnimation="True" BorderThickness="1,1,0,1" Command="｛Binding Path=ModifyCommand｝"/>
        <Controls:FiconButton Content="详情" FIcon="&#xe75a;" Padding="5,0,5,0" CornerRadius="0,10,10,0" Command="｛Binding Path=InfoCommand｝"/>
      </StackPanel>
      <DataGrid Grid.Row="2" AutoGenerateColumns="False" x:Name="dataGrid" ItemsSource="｛Binding Path=list,Mode=TwoWay｝">
        <DataGrid.Columns>
          <DataGridTemplateColumn>
            <DataGridTemplateColumn.HeaderTemplate>
              <DataTemplate>
                <CheckBox HorizontalAlignment="Center" VerticalAlignment="Center"
 IsChecked="｛Binding RelativeSource=｛RelativeSource AncestorType=｛x:Type DataGrid｝｝,Path=DataContext.IsCheckedAll｝">
                  全选
                  <i:Interaction.Triggers>
                    <i:EventTrigger EventName="Checked">
                      <i:InvokeCommandAction  Command="｛Binding Path=DataContext.SelectAllCommand,RelativeSource=｛RelativeSource AncestorType=｛x:Type DataGrid｝｝｝"/>
                    </i:EventTrigger>
                    <i:EventTrigger EventName="Unchecked">
                      <i:InvokeCommandAction  Command="｛Binding Path=DataContext.UnSelectAllCommand,RelativeSource=｛RelativeSource AncestorType=｛x:Type DataGrid｝｝｝"/>
                    </i:EventTrigger>
                  </i:Interaction.Triggers>
                </CheckBox>
              </DataTemplate>
            </DataGridTemplateColumn.HeaderTemplate>
            <DataGridTemplateColumn.CellTemplate>
              <DataTemplate>
                <CheckBox HorizontalAlignment="Center" VerticalAlignment="Center" IsChecked="｛Binding IsChecked｝">
                </CheckBox>
              </DataTemplate>
            </DataGridTemplateColumn.CellTemplate>
          </DataGridTemplateColumn>
          <DataGridTextColumn Binding="｛Binding ObjData.ID｝" Header="编号"/>
          <DataGridTextColumn Binding="｛Binding ObjData.USERNAME｝" Header="USERNAME" Width="100"/>
          <DataGridTextColumn Binding="｛Binding ObjData.NICK_NAME｝" Header="NICK_NAME"/>
        </DataGrid.Columns>
      </DataGrid>
      <Controls:Pager  Grid.Row="3"  TotalPage="｛Binding Path=TotalPage,Mode=TwoWay｝" PageSize="｛Binding Path=PageSize,Mode=TwoWay｝" CurrentPage="｛Binding Path=CurrentPage,Mode=TwoWay｝">
        <i:Interaction.Triggers>
          <i:EventTrigger  EventName="PageChanged">
            <i:InvokeCommandAction  Command="｛Binding Path=NextPageSearchCommand, Mode=TwoWay｝" />
          </i:EventTrigger>
        </i:Interaction.Triggers>
      </Controls:Pager>
    </Grid>
  </UserControl>
</xsl:template>
</xsl:stylesheet>