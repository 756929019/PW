using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Prism.Commands;
using PW.Controls;
using PW.ServiceCenter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PW.Map.ViewModel
{
    public class ViewDefModel: INotifyPropertyChanged
    {
        public ViewDefModel()
        {
            Init();
            getData();
        }

        void getData()
        {
            ServiceComm sc = new ServiceComm();
            sc.AgeCntsCompleted += (serice, e) =>
            {
                if (e.Succesed && e.Result!=null)
                {
                    DataTable dt = e.Result;
                    dataAgeInit(dt);
                }
            };

            sc.DateCntsCompleted += (serice, e) =>
            {
                if (e.Succesed && e.Result != null)
                {
                    DataTable dt = e.Result;
                    dataDateInit(dt);
                }
                //一个个读取
                sc.AgeCnts();
            };

            sc.AddrCntsCompleted += (serice, e) =>
            {
                if (e.Succesed && e.Result != null)
                {
                    DataTable dt = e.Result;
                    dataAddrInit(dt);
                }
                //一个个读取
                sc.DateCnts();
            };
            //一个个读取
            sc.AddrCnts();
            
        }

        #region 属性
        public Func<ChartPoint, string> PiePointLabel = chartPoint =>
             string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        private double _total;
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        private double _totalM;
        public double TotalM
        {
            get { return _totalM; }
            set
            {
                _totalM = value;
                OnPropertyChanged("TotalM");
            }
        }

        private double _totalF;
        public double TotalF
        {
            get { return _totalF; }
            set
            {
                _totalF = value;
                OnPropertyChanged("TotalF");
            }
        }

        private double _valueAgeF;

        public double ValueAgeF
        {
            get { return _valueAgeF; }
            set
            {
                _valueAgeF = value;
                OnPropertyChanged("ValueAgeF");
            }
        }

        private double _valueAgeM;

        public double ValueAgeM
        {
            get { return _valueAgeM; }
            set
            {
                _valueAgeM = value;
                OnPropertyChanged("ValueAgeM");
            }
        }

        private double _valueGenderF;

        public double ValueGenderF
        {
            get { return _valueGenderF; }
            set
            {
                _valueGenderF = value;
                OnPropertyChanged("ValueGenderF");
            }
        }

        private double _valueGenderM;

        public double ValueGenderM
        {
            get { return _valueGenderM; }
            set
            {
                _valueGenderM = value;
                OnPropertyChanged("ValueGenderM");
            }
        }

        private DataTable _mapData;
        public DataTable MapData
        {
            get { return _mapData; }
            set
            {
                _mapData = value;
                OnPropertyChanged("MapData");
            }
        }
        public ObservableCollection<TagCloudItem> TagCollection { get; set; }
        public SeriesCollection SeriesCollectionAddr { get; set; }
        public Func<double, string> FormatterAddr { get; set; }

        private string[] _labelsAddr;
        public string[] LabelsAddr
        {
            get { return _labelsAddr; }
            set
            {
                _labelsAddr = value;
                OnPropertyChanged("LabelsAddr");
            }
        }

        public SeriesCollection AgeSeries { get; set; }

        public SeriesCollection SeriesCollectionPoint { get; set; }


        public SeriesCollection SeriesCollectionWeek { get; set; }

        private string[] _labelsWeek;
        public string[] LabelsWeek
        {
            get { return _labelsWeek; }
            set
            {
                _labelsWeek = value;
                OnPropertyChanged("LabelsWeek");
            }
        }
        public Func<double, string> FormatterWeek { get; set; }

        void loadChartWeek()
        {
            SeriesCollectionWeek = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "女",
                    Values = new ChartValues<double> { 1 }
                }
                ,new RowSeries
                {
                    Title = "男",
                    Values = new ChartValues<double> { 1 }
                }
            };

            LabelsWeek = new[] { "1" };
            FormatterWeek = value => value.ToString("N");
        }

        public SeriesCollection SeriesCollectionMonth { get; set; }

        private string[] _labelsMonth;
        public string[] LabelsMonth
        {
            get { return _labelsMonth; }
            set
            {
                _labelsMonth = value;
                OnPropertyChanged("LabelsMonth");
            }
        }
        public Func<double, string> FormatterMonth { get; set; }
        void loadChartMonth()
        {
            SeriesCollectionMonth = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "女",
                    Values = new ChartValues<double> { 1},
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#30D3CFC7")),
                    LineSmoothness = 0
                }
                ,new LineSeries
                {
                    Title = "男",
                    Values = new ChartValues<double> { 1 },
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#70000000")),
                    LineSmoothness = 0
                }
            };

            LabelsMonth = new[] { "1" };
            FormatterMonth = value => value.ToString("N");
        }

        #endregion

        private TagCloud tagCloud = null;
        private ICommand _loadedCommand;
        /// <summary>
        /// FileCommand
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new DelegateCommand<TagCloud>((tagc) =>
                {
                    tagCloud = tagc;
                    //if (TagCollection != null)
                    //{
                    //    tagCloud.TagCollection = TagCollection;
                    //    tagCloud.Run();
                    //}
                }));
            }
        }

        void loadChartAddr()
        {
            SeriesCollectionAddr = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "女",
                    Values = new ChartValues<double> {4, 5, 6, 8},
                    StackMode = StackMode.Values
                },
                new StackedColumnSeries
                {
                    Title = "男",
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values
                }
            };
            LabelsAddr = new[] { "L1", "L2", "L3", "L4" };
            FormatterAddr = value => value + " (人)";
        }

        void Init()
        {
            AgeSeries = new SeriesCollection {
                new PieSeries
                {
                    Title = "temp",
                    StrokeThickness = 0,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    LabelPoint = PiePointLabel
                }
            };
            loadChartAddr();
            ValueAgeM = 10;
            ValueAgeF = 10;
            ValueGenderM = 0;
            ValueGenderF = 0;
            TotalM = 0;
            TotalF = 0;
            Total = 0;

            SeriesCollectionPoint = new SeriesCollection
            {
                new ScatterSeries
                {
                    Title = "女",
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(5, 5, 20),
                        new ScatterPoint(3, 4, 80),
                        new ScatterPoint(7, 2, 20),
                        new ScatterPoint(2, 6, 60),
                        new ScatterPoint(8, 2, 70)
                    },
                    MinPointShapeDiameter = 15,
                    MaxPointShapeDiameter = 45
                },
                new ScatterSeries
                {
                    Title = "男",
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(7, 5, 1),
                        new ScatterPoint(2, 2, 1),
                        new ScatterPoint(1, 1, 1),
                        new ScatterPoint(6, 3, 1),
                        new ScatterPoint(8, 8, 1)
                    },
                    PointGeometry = DefaultGeometries.Triangle,
                    MinPointShapeDiameter = 15,
                    MaxPointShapeDiameter = 45
                }
            };

            loadChartWeek();
            loadChartMonth();
            loadChartWeekPie();

            TagCollection = new ObservableCollection<TagCloudItem>();
        }
       
        void dataAgeInit(DataTable dt)
        {
            int[] arr = new int[5];
            AgeSeries.Clear();
            //,fyear,cntm,fntm,age
            int _60h = 0;
            int _70h = 0;
            int _80h = 0;
            int _90h = 0;
            int _00h = 0;
            long msum = 0;
            long fsum = 0;
            foreach (DataRow row in dt.Rows)
            {
                TotalM += Int32.Parse(row["cntm"].ToString());
                TotalF += Int32.Parse(row["cntf"].ToString());
                msum += Int32.Parse(row["cntm"].ToString()) * Int32.Parse(row["age"].ToString());
                fsum += Int32.Parse(row["cntf"].ToString()) * Int32.Parse(row["age"].ToString());
                if (row["fyear"].ToString().Substring(0, 3) == "196")
                {
                    _60h += Int32.Parse(row["cntm"].ToString());
                    _60h += Int32.Parse(row["cntf"].ToString());
                }
                else if (row["fyear"].ToString().Substring(0, 3) == "197")
                {
                    _70h += Int32.Parse(row["cntm"].ToString());
                    _70h += Int32.Parse(row["cntf"].ToString());
                }
                else if (row["fyear"].ToString().Substring(0, 3) == "198")
                {
                    _80h += Int32.Parse(row["cntm"].ToString());
                    _80h += Int32.Parse(row["cntf"].ToString());
                }
                else if (row["fyear"].ToString().Substring(0, 3) == "199")
                {
                    _90h += Int32.Parse(row["cntm"].ToString());
                    _90h += Int32.Parse(row["cntf"].ToString());
                }
                else if (row["fyear"].ToString().Substring(0, 3) == "200")
                {
                    _00h += Int32.Parse(row["cntm"].ToString());
                    _00h += Int32.Parse(row["cntf"].ToString());
                }
            }
            ValueAgeM = msum / TotalM;
            ValueAgeF = fsum / TotalF;
            Total = TotalF + TotalM;
            ValueGenderM = double.Parse((TotalM / Total).ToString("f2")) * 100;
            ValueGenderF = double.Parse((TotalF / Total).ToString("f2")) * 100;
            AgeSeriesAdd(_60h,"60后");
            AgeSeriesAdd(_70h, "70后");
            AgeSeriesAdd(_80h, "80后");
            AgeSeriesAdd(_90h, "90后");
            AgeSeriesAdd(_00h, "00后");
        }
        void dataAddrInit(DataTable dt)
        {
            MapData = dt;
            //,code,name,cntm,cntf,addr
            string[] _Addr = new string[dt.Rows.Count];
            SeriesCollectionAddr[0].Values.Clear();
            SeriesCollectionAddr[1].Values.Clear();
            Total = 0;
            for (int i = 0;i<dt.Rows.Count;i++)
            {
                DataRow row = dt.Rows[i];
                Total += Int32.Parse(row["cntm"].ToString());
                Total += Int32.Parse(row["cntf"].ToString());
                _Addr[i] = row["name"].ToString();
                SeriesCollectionAddr[0].Values.Add(double.Parse(row["cntf"].ToString()));
                SeriesCollectionAddr[1].Values.Add(double.Parse(row["cntm"].ToString()));
            }
            LabelsAddr = _Addr;
        }

        void dataDateInit(DataTable dt)
        {
            //SeriesCollectionPoint[0].Values.Clear();
            //SeriesCollectionPoint[1].Values.Clear();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow row = dt.Rows[i];
            //    SeriesCollectionPoint[0].Values.Add(new ScatterPoint(double.Parse(row["month"].ToString()), FormatWeekDay(row["weekday"].ToString()), double.Parse(row["cntf"].ToString())));
            //    SeriesCollectionPoint[1].Values.Add(new ScatterPoint(double.Parse(row["month"].ToString()), FormatWeekDay(row["weekday"].ToString()), double.Parse(row["cntm"].ToString())));
            //}
            if (dt == null || dt.Rows.Count < 1) {
                return;
            }
            //var querytmp = from t in dt.AsEnumerable()
            //            group t by new { t1 = t.Field<string>("weekday")} into m
            //            select new
            //            {
            //                name = m.Key.t1,
            //                week = FormatWeekDay(m.Key.t1),
            //                cntm = m.Sum(n => n.Field<Int32>("cntm")),
            //                cntf = m.Sum(n => n.Field<Int32>("cntf"))
            //            };
            //var query = from v in querytmp orderby v.week ascending select v;
            //string[] _Week = new string[query.Count()];
            //SeriesCollectionWeek[0].Values.Clear();
            //SeriesCollectionWeek[1].Values.Clear();

            //SeriesCollectionWeekPie.Clear();
            //if (query.Count() > 0)
            //{
            //    int i = 0;
            //    query.ToList().ForEach(q =>
            //    {
            //        _Week[i] = q.name;
            //        SeriesCollectionWeek[0].Values.Add(double.Parse(q.cntf.ToString()));
            //        SeriesCollectionWeek[1].Values.Add(double.Parse(q.cntm.ToString()));
            //        i++;

            //        SeriesCollectionWeekPie.Add(new PieSeries
            //        {
            //            Title = q.name,
            //            Values = new ChartValues<ObservableValue> { new ObservableValue(q.cntf), new ObservableValue(q.cntm) },
            //            DataLabels = true,
            //            StrokeThickness = 0
            //        });
            //    });
            //}
            //LabelsWeek = _Week;

            //var querytmp1 = from t in dt.AsEnumerable()
            //               group t by new { t1 = t.Field<string>("month") } into m
            //               select new
            //               {
            //                   name = m.Key.t1,
            //                   cntm = m.Sum(n => n.Field<Int32>("cntm")),
            //                   cntf = m.Sum(n => n.Field<Int32>("cntf"))
            //               };
            //var query1 = from v in querytmp1 orderby v.name ascending select v;
            //string[] _Month = new string[query1.Count()];
            //SeriesCollectionMonth[0].Values.Clear();
            //SeriesCollectionMonth[1].Values.Clear();
            //if (query1.Count() > 0)
            //{
            //    int i = 0;
            //    query1.ToList().ForEach(q =>
            //    {
            //        _Month[i] = q.name;
            //        SeriesCollectionMonth[0].Values.Add(double.Parse(q.cntf.ToString()));
            //        SeriesCollectionMonth[1].Values.Add(double.Parse(q.cntm.ToString()));
            //        i++;
            //    });
            //}
            //LabelsMonth = _Month;
        }

        double FormatWeekDay(string str)
        {
            double rtn = 1;
            switch (str)
            {
                case "星期一": rtn = 1d; break;
                case "星期二": rtn = 2d; break;
                case "星期三": rtn = 3d; break;
                case "星期四": rtn = 4d; break;
                case "星期五": rtn = 5d; break;
                case "星期六": rtn = 6d; break;
                case "星期日": rtn = 7d; break;
            }
            return rtn;
        }

        private void AgeSeriesAdd(int _h,string title)
        {
            AgeSeries.Add(new PieSeries
            {
                Title = title,
                StrokeThickness = 0,
                Values = new ChartValues<ObservableValue> { new ObservableValue(_h) },
                DataLabels = true,
                LabelPoint = PiePointLabel
            });
        }
       

       
        public SeriesCollection SeriesCollectionWeekPie { get; set; }
        void loadChartWeekPie()
        {
            SeriesCollectionWeekPie = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "xxx",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4)},
                    DataLabels = true,
                    StrokeThickness=0
                }
            };
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
