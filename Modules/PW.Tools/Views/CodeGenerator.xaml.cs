using PW.ServiceCenter;
using PW.ServiceCenter.ServiceCodeGenerator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Xsl;

namespace PW.Tools.Views
{
    /// <summary>
    /// CodeGenerator.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(CodeGenerator))]
    public partial class CodeGenerator : UserControl
    {
        TreeNodeInfo rootNode = new TreeNodeInfo();
        public CodeGenerator()
        {
            InitializeComponent();

            this.DataContext = rootNode;
        }

        void loadTab()
        {
            ServiceComm sc = new ServiceComm();
            sc.queryTablesCompleted += (serice, eve) =>
            {
                if (eve.Succesed) {
                    TreeNodeInfo nodetab = new TreeNodeInfo();
                    nodetab.ID = "表";
                    nodetab.Name = "表";
                    nodetab.IsExpanded = false;
                    foreach (table tab in eve.Result)
                    {
                        nodetab.Nodes.Add(new TreeNodeInfo() { ID = tab.table_name, Name = tab.table_name, Mark = tab.table_comment });
                    }
                    rootNode.Nodes.Add(nodetab);
                } else {
                }
            };
            sc.queryTables();

            sc.queryViewsCompleted += (serice, eve) =>
            {
                if (eve.Succesed)
                {
                    TreeNodeInfo nodeview = new TreeNodeInfo();
                    nodeview.ID = "视图";
                    nodeview.Name = "视图";

                    foreach (table tab in eve.Result)
                    {
                        nodeview.Nodes.Add(new TreeNodeInfo() { ID = tab.table_name, Name = tab.table_name });
                    }
                    rootNode.Nodes.Add(nodeview);
                }
                else
                {
                }
            };
            sc.queryViews();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            loadTab();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            loadCheckedTab(rootNode.Nodes[0].Nodes);
            loadCheckedTab(rootNode.Nodes[1].Nodes);
            MessageBox.Show("完成");
        }

        void loadCheckedTab(ObservableCollection<TreeNodeInfo> Nodes)
        {
            ServiceComm sc = new ServiceComm();
            foreach (var item in Nodes)
            {
                if (item.IsChecked == true)
                {
                    string name = Util.toHump(item.Name);
                    TableModel tm = new TableModel() { DbContextName = "qdbEntities", TableName = item.Name, ModelName = name, NameSpace = "CodeGenerator.Model", Comment = string.IsNullOrEmpty(item.Mark) ? item.Name : name };

                    column[] columns = sc.queryColumns(item.Name);
                    foreach (column col in columns)
                    {
                        string varname = Util.toHump(col.column_name);
                        string varnamelocal = Util.startLower(varname);
                        string VarType = DbTypeConvertMethod.GetVarTypeFromSqlDbType(col.data_type);
                        tm.Fields.Add(new FieldModel() { FieldName = col.column_name, DbType = col.data_type, VarType = VarType, ColumnKey = col.column_key, Mark = string.IsNullOrEmpty(col.column_comment) ? col.column_name : col.column_comment, VarName = varname, VarNameLocal = varnamelocal, DefaultValueVar = "" });
                    }

                    //Directory.GetCurrentDirectory();
                    Util.TransferXml(Util.object2xml<TableModel>(tm), AppDomain.CurrentDomain.BaseDirectory + "template/mode.xslt", "D:/CodeGenerator/model/" + name + ".cs");
                    Util.TransferXml(Util.object2xml<TableModel>(tm), AppDomain.CurrentDomain.BaseDirectory + "template/iservice.xslt", "D:/CodeGenerator/service/IService" + name + ".cs");
                    Util.TransferXml(Util.object2xml<TableModel>(tm), AppDomain.CurrentDomain.BaseDirectory + "template/service.xslt", "D:/CodeGenerator/service/Service" + name + ".svc.cs");
                    Util.TransferXml(Util.object2xml<TableModel>(tm), AppDomain.CurrentDomain.BaseDirectory + "template/service.svc.xslt", "D:/CodeGenerator/service/Service" + name + ".svc");
                    Util.TransferXml(Util.object2xml<TableModel>(tm), AppDomain.CurrentDomain.BaseDirectory + "template/dao.xslt", "D:/CodeGenerator/dao/" + name + "Dao.cs");
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Util.toHump("qqq_ccc_ddd"));
            MessageBox.Show(Util.toHump("_qqq_ccc_ddd_"));
        }

        static String XslTransform(string inputXmlConent, string inuptXslContent)
        {
            XmlReader readerXml = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(inputXmlConent)));
            XmlReader readerXsl = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(inuptXslContent)));
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(readerXsl);

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings Settings = new XmlWriterSettings()
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto
            };
            XmlWriter writer = XmlWriter.Create(sb, Settings);

            transform.Transform(readerXml, writer);

            return sb.ToString();
        }
    }
}
