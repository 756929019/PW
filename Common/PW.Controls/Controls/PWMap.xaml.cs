using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PW.Controls
{
    /// <summary>
    /// China.xaml 的交互逻辑
    /// </summary>
    public partial class PWMap : UserControl
    {
        public PWMap()
        {
            InitializeComponent();
        }

        #region 属性
        public PWMapSource MapSource
        {
            get
            {
                return (PWMapSource)GetValue(MapSourceProperty);
            }
            set
            {
                SetValue(MapSourceProperty, value);
            }
        }
        public static readonly DependencyProperty MapSourceProperty =
            DependencyProperty.Register("MapSource", typeof(PWMapSource), typeof(PWMap), new PropertyMetadata(new PropertyChangedCallback(OnSourceChanged)));
        //声明回调函数方法
        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PWMap map = (PWMap)sender;
            try
            {
                System.IO.Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("PW.Controls.Maps." + map.MapSource + ".xml");
                System.Xml.XmlDocument docc = new System.Xml.XmlDocument();
                docc.Load(sm);
                XDocument doc = XDocument.Parse(docc.InnerXml);
                var xdoc = from t in doc.Descendants("LiveChartsMap")                    //定位到节点 
                           select new
                           {
                               Width = t.Element("Width").Value,
                               Height = t.Element("Height").Value,
                               Shapes = t.Element("Shapes").Elements()
                           };
                if (map.mycanvas != null)
                {
                    map.mainCanvas.Width = double.Parse(xdoc.ElementAt(0).Width);
                    map.mainCanvas.Height = double.Parse(xdoc.ElementAt(0).Height);

                    map.mycanvas.Children.Clear();
                    map.nameView.Children.Clear();
                    foreach (var item in xdoc.ElementAt(0).Shapes)
                    {
                        Path path = new Path();
                       // path.Fill = new SolidColorBrush(Colors.Green);
                        path.Data = Geometry.Parse(item.Element("Path").Value);
                        path.ToolTip = item.Element("Text") == null || string.IsNullOrEmpty(item.Element("Text").Value) ? item.Element("Name").Value : item.Element("Text").Value;
                        path.MouseLeftButtonUp += Path_MouseLeftButtonUp;
                        path.Tag = item.Element("Name").Value;
                        map.mycanvas.Children.Add(path);

                        if (item.Element("Top") != null)
                        {
                            StackPanel sp = new StackPanel();
                            TextBlock tb = new TextBlock();
                            tb.Text = item.Element("Text") == null || string.IsNullOrEmpty(item.Element("Text").Value) ?item.Element("Name").Value: item.Element("Text").Value;
                            sp.Children.Add(tb);
                            Canvas.SetLeft(sp, double.Parse(item.Element("Left").Value));
                            Canvas.SetTop(sp, double.Parse(item.Element("Top").Value));
                            map.nameView.Children.Add(sp);
                        }
                    }
                    //<StackPanel Name="spSymbleXZ" Canvas.Left="178" Canvas.Top="390">
                    //    <TextBlock Text="西藏" />
                    //</StackPanel>
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void Path_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            MessageBox.Show(path.Tag.ToString());
        }

        public DataTable MapData
        {
            get
            {
                return (DataTable)GetValue(MapDataProperty);
            }
            set
            {
                SetValue(MapDataProperty, value);
            }
        }
        public static readonly DependencyProperty MapDataProperty =
            DependencyProperty.Register("MapData", typeof(DataTable), typeof(PWMap), new PropertyMetadata(new PropertyChangedCallback(OnDataChanged)));
        //声明回调函数方法
        private static void OnDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PWMap map = (PWMap)sender;
            try
            {
                if (map.MapData != null && map.MapData.Rows.Count>0)
                {
                    map.loadData(map,map.MapData);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void mainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.sfr.CenterX = mainCanvas.ActualWidth / 2;
            this.sfr.CenterY = mainCanvas.ActualHeight / 2;
            if (sfr.ScaleX < 0.3 && sfr.ScaleY < 0.3 && e.Delta < 0)
            {
                return;
            }
            if ((e.Delta < 0 && sfr.ScaleX > 0.2) || (e.Delta > 0 && sfr.ScaleX < 5.0))
            {
                sfr.ScaleX += (double)e.Delta / 480;
                sfr.ScaleY += (double)e.Delta / 480;
            }
            //e.OriginalSource
            e.Handled = true;
        }

        void loadData(PWMap map,DataTable dt)
        {
            List<Color> list = new List<Color>();
            list.Add((Color)ColorConverter.ConvertFromString("#880cd8f1"));
            list.Add((Color)ColorConverter.ConvertFromString("#88397aed"));
            list.Add((Color)ColorConverter.ConvertFromString("#88ffda4b"));
            list.Add((Color)ColorConverter.ConvertFromString("#88f62c61"));
            double sum = int.Parse( dt.Compute("Sum(cntm)+Sum(cntf)", "true").ToString());
            foreach (var item in map.mycanvas.Children)
            {
                if (item is Path)
                {
                    Path path = item as Path;
                    DataRow[] rows = dt.Select("name='" + path.ToolTip.ToString() + "'");
                    if (rows != null && rows.Length >0)
                    {
                        double rate = (int.Parse(rows[0]["cntm"].ToString()) + int.Parse(rows[0]["cntf"].ToString())) / sum * 100;
                        if (rate >= 1 && rate < 4)
                        {
                            path.Fill = new SolidColorBrush(list[0]);
                        }
                        else if (rate >= 4 && rate < 7)
                        {
                            path.Fill = new SolidColorBrush(list[1]);
                        }
                        else if (rate >= 7 && rate < 10)
                        {
                            path.Fill = new SolidColorBrush(list[2]);
                        }
                        else if (rate >= 10)
                        {
                            path.Fill = new SolidColorBrush(list[3]);
                        }
                        path.ToolTip = path.ToolTip.ToString() +"("+ rate + ")" + "(" + (int.Parse(rows[0]["cntm"].ToString()) + int.Parse(rows[0]["cntf"].ToString())) + ")";
                    }
                }
            }
        }
    }

    public enum PWMapSource
    {
        [Description("阿富汗")]
        Afghanistan,
        [Description("阿克罗蒂里亚基翁基斯地区")]
        Akrotiri_Sovereign_Base_Area,
        [Description("Aland")]
        Aland,
        [Description("阿尔巴尼亚")]
        Albania,
        [Description("阿尔及利亚")]
        Algeria,
        [Description("美国_萨摩亚")]
        American_Samoa,
        [Description("安道尔")]
        Andorra,
        [Description("安哥拉")]
        Angola,
        [Description("安圭拉")]
        Anguilla,
        [Description("南极洲")]
        Antarctica,
        [Description("安提瓜和巴巴达")]
        Antigua_and_Barbuda,
        [Description("阿根廷")]
        Argentina,
        [Description("亚美尼亚")]
        Armenia,
        [Description("Aruba")]
        Aruba,
        [Description("阿什莫里亚和卡蒂尔岛")]
        Ashmore_and_Cartier_Islands,
        [Description("澳大利亚")]
        Australia,
        [Description("奥地利")]
        Austria,
        [Description("阿塞拜疆")]
        Azerbaijan,
        [Description("巴林")]
        Bahrain,
        [Description("BajuNueoooBuang-PetRelyIIS")]
        Bajo_Nuevo_Bank_Petrel_Is,
        [Description("孟加拉国")]
        Bangladesh,
        [Description("巴巴多斯")]
        Barbados,
        [Description("白花蛇舌草")]
        Baykonur_Cosmodrome,
        [Description("白俄罗斯")]
        Belarus,
        [Description("比利时")]
        Belgium,
        [Description("伯利兹")]
        Belize,
        [Description("贝宁")]
        Benin,
        [Description("百慕大群岛")]
        Bermuda,
        [Description("不丹")]
        Bhutan,
        [Description("玻利维亚")]
        Bolivia,
        [Description("波斯尼亚和黑塞哥维那")]
        Bosnia_and_Herzegovina,
        [Description("博茨瓦纳")]
        Botswana,
        [Description("巴西")]
        Brazil,
        [Description("英国印度印度洋")]
        British_Indian_Ocean_Territory,
        [Description("布里奇什弗吉尼亚群岛")]
        British_Virgin_Islands,
        [Description("文莱")]
        Brunei,
        [Description("保加利亚")]
        Bulgaria,
        [Description("布基纳法索")]
        Burkina_Faso,
        [Description("布隆迪")]
        Burundi,
        [Description("柬埔寨")]
        Cambodia,
        [Description("喀麦隆")]
        Cameroon,
        [Description("加拿大")]
        Canada,
        [Description("卡佩弗尔德")]
        Cape_Verde,
        [Description("加勒比荷兰")]
        Caribbean_Netherlands,
        [Description("开曼群岛")]
        Cayman_Islands,
        [Description("中央非洲共和国")]
        Central_African_Republic,
        [Description("乍得")]
        Chad,
        [Description("智利")]
        Chile,
        [Description("中国")]
        China,
        [Description("克利珀顿岛")]
        Clipperton_Island,
        [Description("哥伦比亚")]
        Colombia,
        [Description("科摩罗")]
        Comoros,
        [Description("库克群岛")]
        Cook_Islands,
        [Description("科拉里海岛")]
        Coral_Sea_Islands,
        [Description("肋藻属")]
        Costa_Rica,
        [Description("克罗地亚")]
        Croatia,
        [Description("古巴")]
        Cuba,
        [Description("库拉索")]
        Curaçao,
        [Description("塞浦路斯")]
        Cyprus,
        [Description("捷克共和国")]
        Czech_Republic,
        [Description("民主共和国")]
        Democratic_Republic_of_the_Congo,
        [Description("丹麦")]
        Denmark,
        [Description("塞浦路斯英属基地区")]
        Dhekelia_Soverign_Base_Area,
        [Description("吉布提")]
        Djibouti,
        [Description("多米尼加")]
        Dominica,
        [Description("多米尼克共和国")]
        Dominican_Republic,
        [Description("伊斯特帝摩")]
        East_Timor,
        [Description("厄瓜多尔")]
        Ecuador,
        [Description("埃及")]
        Egypt,
        [Description("埃尔萨尔瓦多")]
        El_Salvador,
        [Description("几内亚赤道")]
        Equatorial_Guinea,
        [Description("厄立特里亚")]
        Eritrea,
        [Description("爱沙尼亚")]
        Estonia,
        [Description("埃塞俄比亚")]
        Ethiopia,
        [Description("福克兰群岛")]
        Falkland_Islands,
        [Description("法罗群岛")]
        Faroe_Islands,
        [Description("密克罗尼西亚联邦")]
        Federated_States_of_Micronesia,
        [Description("斐济")]
        Fiji,
        [Description("芬兰")]
        Finland,
        [Description("法国")]
        France,
        [Description("法兰西玻利尼西亚")]
        French_Polynesia,
        [Description("法国南部和南极洲")]
        French_Southern_and_Antarctic_Lands,
        [Description("Gabon")]
        Gabon,
        [Description("冈比亚")]
        Gambia,
        [Description("加扎带")]
        Gaza_Strip,
        [Description("格鲁吉亚")]
        Georgia,
        [Description("德国")]
        Germany,
        [Description("加纳")]
        Ghana,
        [Description("直布罗陀")]
        Gibraltar,
        [Description("希腊")]
        Greece,
        [Description("格陵兰岛")]
        Greenland,
        [Description("格林纳达")]
        Grenada,
        [Description("关岛")]
        Guam,
        [Description("瓜地马拉")]
        Guatemala,
        [Description("英属格恩西")]
        Guernsey,
        [Description("几内亚")]
        Guinea,
        [Description("几内亚比绍")]
        Guinea_Bissau,
        [Description("圭亚那")]
        Guyana,
        [Description("海地")]
        Haiti,
        [Description("赫德岛和麦当劳群岛")]
        Heard_Island_and_McDonald_Islands,
        [Description("洪都拉斯")]
        Honduras,
        [Description("香港香港")]
        Hong_Kong_S_A_R,
        [Description("匈牙利")]
        Hungary,
        [Description("冰岛")]
        Iceland,
        [Description("印度")]
        India,
        [Description("印第安纳海洋区")]
        Indian_Ocean_Territories,
        [Description("印度尼西亚")]
        Indonesia,
        [Description("伊朗")]
        Iran,
        [Description("伊拉克")]
        Iraq,
        [Description("爱尔兰")]
        Ireland,
        [Description("伊斯莱夫曼")]
        Isle_of_Man,
        [Description("以色列")]
        Israel,
        [Description("意大利")]
        Italy,
        [Description("伊沃里亚海岸")]
        Ivory_Coast,
        [Description("牙买加")]
        Jamaica,
        [Description("日本")]
        Japan,
        [Description("Jersey")]
        Jersey,
        [Description("乔丹")]
        Jordan,
        [Description("哈萨克斯坦")]
        Kazakhstan,
        [Description("肯尼亚")]
        Kenya,
        [Description("基里巴斯")]
        Kiribati,
        [Description("科索沃")]
        Kosovo,
        [Description("科威特")]
        Kuwait,
        [Description("吉尔吉斯斯坦")]
        Kyrgyzstan,
        [Description("老挝")]
        Laos,
        [Description("拉脱维亚")]
        Latvia,
        [Description("黎巴嫩")]
        Lebanon,
        [Description("莱索托")]
        Lesotho,
        [Description("利比里亚")]
        Liberia,
        [Description("利比亚")]
        Libya,
        [Description("列支敦士登")]
        Liechtenstein,
        [Description("立陶宛")]
        Lithuania,
        [Description("卢森堡")]
        Luxembourg,
        [Description("麦考斯")]
        Macau_S_A_R,
        [Description("马其顿")]
        Macedonia,
        [Description("马达加斯加")]
        Madagascar,
        [Description("马拉维")]
        Malawi,
        [Description("马来西亚")]
        Malaysia,
        [Description("马尔代夫")]
        Maldives,
        [Description("马里")]
        Mali,
        [Description("马耳他")]
        Malta,
        [Description("马歇尔群岛")]
        Marshall_Islands,
        [Description("毛里塔尼亚")]
        Mauritania,
        [Description("毛里求斯")]
        Mauritius,
        [Description("墨西哥")]
        Mexico,
        [Description("摩尔多瓦")]
        Moldova,
        [Description("摩纳哥")]
        Monaco,
        [Description("蒙古")]
        Mongolia,
        [Description("黑山")]
        Montenegro,
        [Description("普利茅斯")]
        Montserrat,
        [Description("摩洛哥")]
        Morocco,
        [Description("莫桑比克")]
        Mozambique,
        [Description("缅甸")]
        Myanmar,
        [Description("纳米比亚")]
        Namibia,
        [Description("瑙鲁")]
        Nauru,
        [Description("尼泊尔")]
        Nepal,
        [Description("荷兰")]
        Netherlands,
        [Description("纽卡里多尼亚")]
        New_Caledonia,
        [Description("纽西兰")]
        New_Zealand,
        [Description("尼加拉瓜")]
        Nicaragua,
        [Description("尼日尔")]
        Niger,
        [Description("尼日利亚")]
        Nigeria,
        [Description("纽埃")]
        Niue,
        [Description("诺福克岛")]
        Norfolk_Island,
        [Description("北塞浦路斯")]
        Northern_Cyprus,
        [Description("北马里亚纳群岛")]
        Northern_Mariana_Islands,
        [Description("北朝鲜")]
        North_Korea,
        [Description("挪威")]
        Norway,
        [Description("努尔岛")]
        Null_Island,
        [Description("阿曼")]
        Oman,
        [Description("巴基斯坦")]
        Pakistan,
        [Description("帕劳")]
        Palau,
        [Description("巴拿马")]
        Panama,
        [Description("巴布亚新几内亚")]
        Papua_New_Guinea,
        [Description("巴拉圭")]
        Paraguay,
        [Description("秘鲁")]
        Peru,
        [Description("菲律宾")]
        Philippines,
        [Description("皮特凯尔尼群岛")]
        Pitcairn_Islands,
        [Description("波兰")]
        Poland,
        [Description("葡萄牙")]
        Portugal,
        [Description("波多黎各")]
        Puerto_Rico,
        [Description("卡塔尔")]
        Qatar,
        [Description("塞尔维亚共和国")]
        Republic_of_Serbia,
        [Description("共和国共和国")]
        Republic_of_the_Congo,
        [Description("罗马尼亚")]
        Romania,
        [Description("俄罗斯")]
        Russia,
        [Description("卢旺达")]
        Rwanda,
        [Description("圣巴特勒米")]
        Saint_Barthelemy,
        [Description("圣海伦娜")]
        Saint_Helena,
        [Description("圣吉特斯和尼维斯")]
        Saint_Kitts_and_Nevis,
        [Description("圣女卢西亚")]
        Saint_Lucia,
        [Description("圣特马丁")]
        Saint_Martin,
        [Description("圣安东尼奥")]
        Saint_Pierre_and_Miquelon,
        [Description("圣安东尼")]
        Saint_Vincent_and_the_Grenadines,
        [Description("萨摩亚")]
        Samoa,
        [Description("圣马利诺")]
        San_Marino,
        [Description("圣安东尼奥")]
        Sao_Tome_and_Principe,
        [Description("沙特阿拉伯")]
        Saudi_Arabia,
        [Description("斯卡博罗伊礁")]
        Scarborough_Reef,
        [Description("塞内加尔")]
        Senegal,
        [Description("塞拉尼拉尔班克")]
        Serranilla_Bank,
        [Description("塞舌尔")]
        Seychelles,
        [Description("锡钦冰川")]
        Siachen_Glacier,
        [Description("西尔拉里昂")]
        Sierra_Leone,
        [Description("新加坡")]
        Singapore,
        [Description("辛特玛尔滕")]
        Sint_Maarten,
        [Description("斯洛伐克")]
        Slovakia,
        [Description("斯洛文尼亚")]
        Slovenia,
        [Description("所罗门洲群岛")]
        Solomon_Islands,
        [Description("索马里")]
        Somalia,
        [Description("索马里兰")]
        Somaliland,
        [Description("南非洲")]
        South_Africa,
        [Description("南乔治亚和安第斯群岛")]
        South_Georgia_and_the_Islands,
        [Description("韩国")]
        South_Korea,
        [Description("西班牙")]
        Spain,
        [Description("斯普拉特利亚斯")]
        Spratly_Is,
        [Description("斯里兰卡")]
        Sri_Lanka,
        [Description("苏丹")]
        Sudan,
        [Description("苏里南")]
        Suriname,
        [Description("斯威士兰")]
        Swaziland,
        [Description("瑞典")]
        Sweden,
        [Description("瑞士")]
        Switzerland,
        [Description("叙利亚")]
        Syria,
        [Description("苏丹苏丹")]
        S_Sudan,
        [Description("台湾")]
        Taiwan,
        [Description("塔吉克斯坦")]
        Tajikistan,
        [Description("泰国")]
        Thailand,
        [Description("巴哈马群岛")]
        The_Bahamas,
        [Description("多哥")]
        Togo,
        [Description("汤加")]
        Tonga,
        [Description("千里光")]
        Trinidad_and_Tobago,
        [Description("突尼斯")]
        Tunisia,
        [Description("土耳其")]
        Turkey,
        [Description("土库曼斯坦")]
        Turkmenistan,
        [Description("Turksl and Sy-CaysOsC岛")]
        Turks_and_Caicos_Islands,
        [Description("图瓦卢")]
        Tuvalu,
        [Description("乌干达")]
        Uganda,
        [Description("乌克兰")]
        Ukraine,
        [Description("阿拉伯阿拉伯酋长国")]
        United_Arab_Emirates,
        [Description("联合王国")]
        United_Kingdom,
        [Description("坦桑尼亚共和国")]
        United_Republic_of_Tanzania,
        [Description("联合国岛屿")]
        United_States_Minor_Outlying_Islands,
        [Description("美国美国")]
        United_States_of_America,
        [Description("弗吉尼亚群岛")]
        United_States_Virgin_Islands,
        [Description("乌拉圭")]
        Uruguay,
        [Description("UsNavalasBasyGuutaNaMaoBay")]
        US_Naval_Base_Guantanamo_Bay,
        [Description("乌兹别克斯坦")]
        Uzbekistan,
        [Description("瓦努阿图")]
        Vanuatu,
        [Description("梵蒂冈")]
        Vatican,
        [Description("委内瑞拉")]
        Venezuela,
        [Description("越南")]
        Vietnam,
        [Description("瓦利斯和阿福图纳")]
        Wallis_and_Futuna,
        [Description("西部撒哈拉沙漠")]
        Western_Sahara,
        [Description("韦斯特堡银行")]
        West_Bank,
        [Description("世界")]
        World,
        [Description("也门")]
        Yemen,
        [Description("赞比亚")]
        Zambia,
        [Description("津巴布韦")]
        Zimbabwe
    }
}
