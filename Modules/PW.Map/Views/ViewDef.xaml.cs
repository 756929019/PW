using PW.Controls;
using PW.Map.ViewModel;
using PW.ServiceCenter;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Windows.Controls;

namespace PW.Map.Views
{
    /// <summary>
    /// ViewDef.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(ViewDef))]
    public partial class ViewDef : UserControl
    {
        public ViewDef()
        {
            InitializeComponent();
            ViewDefModel vdm = new ViewDefModel();
            DataContext = vdm;
            ServiceComm sc = new ServiceComm();
            sc.NameTagsCompleted += (serice, e) =>
            {
                if (e.Succesed && e.Result != null)
                {
                    DataTable dt = e.Result;
                    ObservableCollection<TagCloudItem> tagCollection = new ObservableCollection<TagCloudItem>();
                    foreach (DataRow row in dt.Rows)
                    {
                        TagCloudItem item = new TagCloudItem();
                        Border border = new Border();
                        TextBlock tb = new TextBlock();
                        tb.Text = row["Name"].ToString();
                        border.Child = tb;
                        item.Children.Add(border);
                        tagCollection.Add(item);
                    }
                    if (cloud != null)
                    {
                        cloud.TagCollection = tagCollection;
                        cloud.Run();
                    }
                }

            };
            sc.NameTags();
        }
    }
}
