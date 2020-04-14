using PW.Infrastructure;
using PW.ServiceCenter.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.ServiceCenter
{
    public class ServiceComm
    {
        #region ServiceLogin
        public event System.EventHandler<ServicesEventArgs<bool>> LoginCompleted;
        public void Login(string username,string pwd)
        {
            ServiceLogin.ServiceLoginClient client = new ServiceLogin.ServiceLoginClient();
            client.LoginCompleted += (sender, e) =>
            {
                ServicesEventArgs<bool> arg = new ServicesEventArgs<bool>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    arg.Result = true;
#endif
                    //写错误日志
                    //.....
                }
                if (LoginCompleted != null)
                {
                    LoginCompleted.Invoke(this, arg);
                }
            };
            client.LoginAsync(username, pwd);
        }

        public event EventHandler<ServicesEventArgs<PW.Common.UserInfo>> GetUserInfoCompleted;
        public void GetUserInfo(string userid)
        {
            ServiceLogin.ServiceLoginClient client = new ServiceLogin.ServiceLoginClient();
            client.GetUserInfoCompleted += (sender, e) =>
            {
                ServicesEventArgs<PW.Common.UserInfo> arg = new ServicesEventArgs<PW.Common.UserInfo>();

                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        arg.Result = new PW.Common.UserInfo() { fullname = e.Result.fullname, role = e.Result.role, userid = e.Result.userid, username = e.Result.username };
                    }
                    else
                    {
                        arg.Result = null;
                    }

                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
                    //写错误日志
                    //.....
                }
                if (GetUserInfoCompleted != null)
                {
                    GetUserInfoCompleted.Invoke(this, arg);
                }
            };
            client.GetUserInfoAsync(userid);
        }
        #endregion

        #region Statistics
        public event System.EventHandler<ServicesEventArgs<DataTable>> AgeCntsCompleted;
        public void AgeCnts()
        {
            ServiceStatisticsClient client = new ServiceStatisticsClient();
            client.AgeCntsCompleted += (sender, e) =>
            {
                ServicesEventArgs<DataTable> arg = new ServicesEventArgs<DataTable>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("fyear"));
                    dt.Columns.Add(new DataColumn("age"));
                    dt.Columns.Add(new DataColumn("cntm"));
                    dt.Columns.Add(new DataColumn("cntf"));

                    DataRow row = dt.NewRow(); row["fyear"] = "1951"; row["age"] = 67; row["cntm"] = 44118; row["cntf"] = 27486; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1952"; row["age"] = 66; row["cntm"] = 48474; row["cntf"] = 28317; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1953"; row["age"] = 65; row["cntm"] = 54006; row["cntf"] = 30362; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1954"; row["age"] = 64; row["cntm"] = 67726; row["cntf"] = 36214; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1955"; row["age"] = 63; row["cntm"] = 70135; row["cntf"] = 35327; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1956"; row["age"] = 62; row["cntm"] = 77885; row["cntf"] = 38497; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1957"; row["age"] = 61; row["cntm"] = 87561; row["cntf"] = 41472; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1958"; row["age"] = 60; row["cntm"] = 86166; row["cntf"] = 38938; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1959"; row["age"] = 59; row["cntm"] = 78402; row["cntf"] = 34686; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1960"; row["age"] = 58; row["cntm"] = 93441; row["cntf"] = 41646; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1961"; row["age"] = 57; row["cntm"] = 94346; row["cntf"] = 42254; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1962"; row["age"] = 56; row["cntm"] = 177931; row["cntf"] = 73819; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1963"; row["age"] = 55; row["cntm"] = 239019; row["cntf"] = 99083; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1964"; row["age"] = 54; row["cntm"] = 206242; row["cntf"] = 83558; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1965"; row["age"] = 53; row["cntm"] = 205272; row["cntf"] = 83334; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1966"; row["age"] = 52; row["cntm"] = 204604; row["cntf"] = 82740; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1967"; row["age"] = 51; row["cntm"] = 196529; row["cntf"] = 80262; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1968"; row["age"] = 50; row["cntm"] = 281254; row["cntf"] = 120412; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1969"; row["age"] = 49; row["cntm"] = 276881; row["cntf"] = 117302; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1970"; row["age"] = 48; row["cntm"] = 320256; row["cntf"] = 133135; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1971"; row["age"] = 47; row["cntm"] = 325433; row["cntf"] = 135658; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1972"; row["age"] = 46; row["cntm"] = 336923; row["cntf"] = 140616; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1973"; row["age"] = 45; row["cntm"] = 335876; row["cntf"] = 144494; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1974"; row["age"] = 44; row["cntm"] = 332494; row["cntf"] = 145499; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1975"; row["age"] = 43; row["cntm"] = 350082; row["cntf"] = 159725; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1976"; row["age"] = 42; row["cntm"] = 362359; row["cntf"] = 167611; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1977"; row["age"] = 41; row["cntm"] = 362979; row["cntf"] = 177934; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1978"; row["age"] = 40; row["cntm"] = 408248; row["cntf"] = 205984; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1979"; row["age"] = 39; row["cntm"] = 421898; row["cntf"] = 213088; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1980"; row["age"] = 38; row["cntm"] = 396136; row["cntf"] = 208418; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1981"; row["age"] = 37; row["cntm"] = 474882; row["cntf"] = 260106; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1982"; row["age"] = 36; row["cntm"] = 535492; row["cntf"] = 306447; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1983"; row["age"] = 35; row["cntm"] = 466795; row["cntf"] = 278885; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1984"; row["age"] = 34; row["cntm"] = 446842; row["cntf"] = 284906; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1985"; row["age"] = 33; row["cntm"] = 441239; row["cntf"] = 312836; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1986"; row["age"] = 32; row["cntm"] = 475595; row["cntf"] = 371742; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1987"; row["age"] = 31; row["cntm"] = 471167; row["cntf"] = 401511; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1988"; row["age"] = 30; row["cntm"] = 391075; row["cntf"] = 366162; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1989"; row["age"] = 29; row["cntm"] = 335849; row["cntf"] = 344875; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1990"; row["age"] = 28; row["cntm"] = 250282; row["cntf"] = 279612; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1991"; row["age"] = 27; row["cntm"] = 161167; row["cntf"] = 189983; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1992"; row["age"] = 26; row["cntm"] = 115533; row["cntf"] = 143231; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1993"; row["age"] = 25; row["cntm"] = 77998; row["cntf"] = 94545; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1994"; row["age"] = 24; row["cntm"] = 41783; row["cntf"] = 48280; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1995"; row["age"] = 23; row["cntm"] = 20814; row["cntf"] = 21449; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1996"; row["age"] = 22; row["cntm"] = 19914; row["cntf"] = 16728; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1997"; row["age"] = 21; row["cntm"] = 24610; row["cntf"] = 18070; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1998"; row["age"] = 20; row["cntm"] = 26754; row["cntf"] = 21458; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "1999"; row["age"] = 19; row["cntm"] = 6254; row["cntf"] = 5645; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2000"; row["age"] = 18; row["cntm"] = 1941; row["cntf"] = 2012; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2001"; row["age"] = 17; row["cntm"] = 1347; row["cntf"] = 1295; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2002"; row["age"] = 16; row["cntm"] = 1168; row["cntf"] = 1056; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2003"; row["age"] = 15; row["cntm"] = 626; row["cntf"] = 609; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2004"; row["age"] = 14; row["cntm"] = 670; row["cntf"] = 549; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2005"; row["age"] = 13; row["cntm"] = 404; row["cntf"] = 356; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2006"; row["age"] = 12; row["cntm"] = 328; row["cntf"] = 260; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2007"; row["age"] = 11; row["cntm"] = 244; row["cntf"] = 216; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2008"; row["age"] = 10; row["cntm"] = 168; row["cntf"] = 138; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2009"; row["age"] = 9; row["cntm"] = 104; row["cntf"] = 85; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2010"; row["age"] = 8; row["cntm"] = 72; row["cntf"] = 48; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2011"; row["age"] = 7; row["cntm"] = 88; row["cntf"] = 59; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2012"; row["age"] = 6; row["cntm"] = 190; row["cntf"] = 27; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2013"; row["age"] = 5; row["cntm"] = 6; row["cntf"] = 2; dt.Rows.Add(row);
                    row = dt.NewRow(); row["fyear"] = "2014"; row["age"] = 4; row["cntm"] = 4; row["cntf"] = 1; dt.Rows.Add(row);
                    arg.Result = dt;
#endif
                }
                if (AgeCntsCompleted != null)
                {
                    AgeCntsCompleted.Invoke(this, arg);
                }
            };
            client.AgeCntsAsync();
        }

        public event System.EventHandler<ServicesEventArgs<DataTable>> AddrCntsCompleted;
        public void AddrCnts()
        {
            ServiceStatisticsClient client = new ServiceStatisticsClient();
            client.AddrCntsCompleted += (sender, e) =>
            {
                ServicesEventArgs<DataTable> arg = new ServicesEventArgs<DataTable>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("code"));
                    dt.Columns.Add(new DataColumn("name"));
                    dt.Columns.Add(new DataColumn("addr"));
                    dt.Columns.Add(new DataColumn("cntm"));
                    dt.Columns.Add(new DataColumn("cntf"));

                    DataRow row = dt.NewRow(); row["code"] = "210000"; row["name"] = "辽宁"; row["addr"] = "21"; row["cntm"] = 437371; row["cntf"] = 309883; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "310000"; row["name"] = "上海"; row["addr"] = "31"; row["cntm"] = 553968; row["cntf"] = 357710; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "220000"; row["name"] = "吉林"; row["addr"] = "22"; row["cntm"] = 264968; row["cntf"] = 189711; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "500000"; row["name"] = "重庆"; row["addr"] = "50"; row["cntm"] = 36427; row["cntf"] = 37571; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "320000"; row["name"] = "江苏"; row["addr"] = "32"; row["cntm"] = 1539132; row["cntf"] = 850746; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "330000"; row["name"] = "浙江"; row["addr"] = "33"; row["cntm"] = 859867; row["cntf"] = 473071; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "640000"; row["name"] = "宁夏"; row["addr"] = "64"; row["cntm"] = 45435; row["cntf"] = 29321; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "230000"; row["name"] = "黑龙江"; row["addr"] = "23"; row["cntm"] = 337033; row["cntf"] = 240847; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "410000"; row["name"] = "河南"; row["addr"] = "41"; row["cntm"] = 713429; row["cntf"] = 399587; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "910000"; row["name"] = "澳门"; row["addr"] = "91"; row["cntm"] = 11388; row["cntf"] = 8713; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "350000"; row["name"] = "福建"; row["addr"] = "35"; row["cntm"] = 383907; row["cntf"] = 186816; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "140000"; row["name"] = "山西"; row["addr"] = "14"; row["cntm"] = 423844; row["cntf"] = 264822; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "420000"; row["name"] = "湖北"; row["addr"] = "42"; row["cntm"] = 607974; row["cntf"] = 363144; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "430000"; row["name"] = "湖南"; row["addr"] = "43"; row["cntm"] = 344809; row["cntf"] = 219637; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "150000"; row["name"] = "内蒙古"; row["addr"] = "15"; row["cntm"] = 241534; row["cntf"] = 150008; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "460000"; row["name"] = "海南"; row["addr"] = "46"; row["cntm"] = 28987; row["cntf"] = 19130; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "510000"; row["name"] = "四川"; row["addr"] = "51"; row["cntm"] = 395284; row["cntf"] = 267289; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "650000"; row["name"] = "新疆"; row["addr"] = "65"; row["cntm"] = 80659; row["cntf"] = 64101; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "810000"; row["name"] = "香港"; row["addr"] = "81"; row["cntm"] = 7676; row["cntf"] = 5249; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "520000"; row["name"] = "贵州"; row["addr"] = "52"; row["cntm"] = 105559; row["cntf"] = 81595; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "440000"; row["name"] = "广东"; row["addr"] = "44"; row["cntm"] = 350239; row["cntf"] = 209463; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "530000"; row["name"] = "云南"; row["addr"] = "53"; row["cntm"] = 83951; row["cntf"] = 57510; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "370000"; row["name"] = "山东"; row["addr"] = "37"; row["cntm"] = 994653; row["cntf"] = 526584; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "630000"; row["name"] = "青海"; row["addr"] = "63"; row["cntm"] = 45456; row["cntf"] = 29776; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "340000"; row["name"] = "安徽"; row["addr"] = "34"; row["cntm"] = 622829; row["cntf"] = 354370; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "450000"; row["name"] = "广西"; row["addr"] = "45"; row["cntm"] = 110902; row["cntf"] = 81653; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "110000"; row["name"] = "北京"; row["addr"] = "11"; row["cntm"] = 303993; row["cntf"] = 188437; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "540000"; row["name"] = "西藏"; row["addr"] = "54"; row["cntm"] = 11538; row["cntf"] = 5816; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "120000"; row["name"] = "天津"; row["addr"] = "12"; row["cntm"] = 199093; row["cntf"] = 120648; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "130000"; row["name"] = "河北"; row["addr"] = "13"; row["cntm"] = 494912; row["cntf"] = 292493; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "610000"; row["name"] = "陕西"; row["addr"] = "61"; row["cntm"] = 355439; row["cntf"] = 212805; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "620000"; row["name"] = "甘肃"; row["addr"] = "62"; row["cntm"] = 140213; row["cntf"] = 86120; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "710000"; row["name"] = "台湾"; row["addr"] = "71"; row["cntm"] = 1653; row["cntf"] = 501; dt.Rows.Add(row);
                    row = dt.NewRow(); row["code"] = "360000"; row["name"] = "江西"; row["addr"] = "36"; row["cntm"] = 387198; row["cntf"] = 204320; dt.Rows.Add(row);
                    arg.Result = dt;
#endif
                }
                if (AddrCntsCompleted != null)
                {
                    AddrCntsCompleted.Invoke(this, arg);
                }
            };
            client.AddrCntsAsync();
        }

        public event System.EventHandler<ServicesEventArgs<DataTable>> DateCntsCompleted;
        public void DateCnts()
        {
            ServiceStatisticsClient client = new ServiceStatisticsClient();
            client.DateCntsCompleted += (sender, e) =>
            {
                ServicesEventArgs<DataTable> arg = new ServicesEventArgs<DataTable>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("month"));
                    dt.Columns.Add(new DataColumn("weekday"));
                    dt.Columns.Add(new DataColumn("cntm"));
                    dt.Columns.Add(new DataColumn("cntf"));

                    DataRow row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期二"; row["cntm"] = 93219; row["cntf"] = 64694; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期六"; row["cntm"] = 78914; row["cntf"] = 43658; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期日"; row["cntm"] = 90507; row["cntf"] = 52572; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期三"; row["cntm"] = 68307; row["cntf"] = 38799; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期四"; row["cntm"] = 73658; row["cntf"] = 39739; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期五"; row["cntm"] = 71626; row["cntf"] = 38855; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "1"; row["weekday"] = "星期一"; row["cntm"] = 92610; row["cntf"] = 57409; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期二"; row["cntm"] = 61915; row["cntf"] = 35969; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期六"; row["cntm"] = 71813; row["cntf"] = 36095; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期日"; row["cntm"] = 69098; row["cntf"] = 40984; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期三"; row["cntm"] = 79440; row["cntf"] = 43454; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期四"; row["cntm"] = 64138; row["cntf"] = 35014; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期五"; row["cntm"] = 64941; row["cntf"] = 34315; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "2"; row["weekday"] = "星期一"; row["cntm"] = 68542; row["cntf"] = 42399; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期二"; row["cntm"] = 84485; row["cntf"] = 44175; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期六"; row["cntm"] = 100172; row["cntf"] = 48835; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期日"; row["cntm"] = 96400; row["cntf"] = 55818; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期三"; row["cntm"] = 78475; row["cntf"] = 37418; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期四"; row["cntm"] = 100438; row["cntf"] = 46112; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期五"; row["cntm"] = 92836; row["cntf"] = 43041; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "3"; row["weekday"] = "星期一"; row["cntm"] = 124536; row["cntf"] = 74275; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期二"; row["cntm"] = 79880; row["cntf"] = 46365; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期六"; row["cntm"] = 100166; row["cntf"] = 51562; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期日"; row["cntm"] = 114652; row["cntf"] = 66637; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期三"; row["cntm"] = 89720; row["cntf"] = 52006; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期四"; row["cntm"] = 85753; row["cntf"] = 47471; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期五"; row["cntm"] = 91265; row["cntf"] = 47449; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "4"; row["weekday"] = "星期一"; row["cntm"] = 307205; row["cntf"] = 74602; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期二"; row["cntm"] = 117197; row["cntf"] = 71668; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期六"; row["cntm"] = 96318; row["cntf"] = 50164; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期日"; row["cntm"] = 119753; row["cntf"] = 72230; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期三"; row["cntm"] = 103617; row["cntf"] = 58513; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期四"; row["cntm"] = 97350; row["cntf"] = 49215; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期五"; row["cntm"] = 122965; row["cntf"] = 43960; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "5"; row["weekday"] = "星期一"; row["cntm"] = 130377; row["cntf"] = 82305; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期二"; row["cntm"] = 105830; row["cntf"] = 57568; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期六"; row["cntm"] = 114052; row["cntf"] = 59239; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期日"; row["cntm"] = 103274; row["cntf"] = 62335; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期三"; row["cntm"] = 136043; row["cntf"] = 68486; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期四"; row["cntm"] = 106302; row["cntf"] = 54750; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期五"; row["cntm"] = 113939; row["cntf"] = 56624; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "6"; row["weekday"] = "星期一"; row["cntm"] = 113702; row["cntf"] = 71639; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期二"; row["cntm"] = 128721; row["cntf"] = 77060; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期六"; row["cntm"] = 135477; row["cntf"] = 77591; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期日"; row["cntm"] = 162176; row["cntf"] = 101458; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期三"; row["cntm"] = 120966; row["cntf"] = 67771; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期四"; row["cntm"] = 110378; row["cntf"] = 61996; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期五"; row["cntm"] = 120691; row["cntf"] = 66572; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "7"; row["weekday"] = "星期一"; row["cntm"] = 666972; row["cntf"] = 368764; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期二"; row["cntm"] = 130802; row["cntf"] = 81410; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期六"; row["cntm"] = 124949; row["cntf"] = 74636; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期日"; row["cntm"] = 144419; row["cntf"] = 93373; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期三"; row["cntm"] = 154039; row["cntf"] = 90068; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期四"; row["cntm"] = 147459; row["cntf"] = 84820; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期五"; row["cntm"] = 160535; row["cntf"] = 92063; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "8"; row["weekday"] = "星期一"; row["cntm"] = 155680; row["cntf"] = 104681; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期二"; row["cntm"] = 395552; row["cntf"] = 268904; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期六"; row["cntm"] = 199056; row["cntf"] = 108849; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期日"; row["cntm"] = 173898; row["cntf"] = 100619; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期三"; row["cntm"] = 162804; row["cntf"] = 89518; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期四"; row["cntm"] = 239073; row["cntf"] = 138444; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期五"; row["cntm"] = 174821; row["cntf"] = 92113; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "9"; row["weekday"] = "星期一"; row["cntm"] = 181744; row["cntf"] = 112780; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期二"; row["cntm"] = 186323; row["cntf"] = 114294; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期六"; row["cntm"] = 231899; row["cntf"] = 136243; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期日"; row["cntm"] = 218486; row["cntf"] = 138521; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期三"; row["cntm"] = 184768; row["cntf"] = 109007; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期四"; row["cntm"] = 172106; row["cntf"] = 139820; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期五"; row["cntm"] = 216558; row["cntf"] = 130833; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "10"; row["weekday"] = "星期一"; row["cntm"] = 255252; row["cntf"] = 161567; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期二"; row["cntm"] = 213700; row["cntf"] = 106254; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期六"; row["cntm"] = 167892; row["cntf"] = 82288; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期日"; row["cntm"] = 174698; row["cntf"] = 96304; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期三"; row["cntm"] = 168734; row["cntf"] = 81735; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期四"; row["cntm"] = 167908; row["cntf"] = 78029; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期五"; row["cntm"] = 191809; row["cntf"] = 90673; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "11"; row["weekday"] = "星期一"; row["cntm"] = 201246; row["cntf"] = 122245; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期二"; row["cntm"] = 187240; row["cntf"] = 87590; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期六"; row["cntm"] = 292793; row["cntf"] = 128551; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期日"; row["cntm"] = 206415; row["cntf"] = 106418; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期三"; row["cntm"] = 198481; row["cntf"] = 88072; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期四"; row["cntm"] = 216682; row["cntf"] = 93435; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期五"; row["cntm"] = 228613; row["cntf"] = 97568; dt.Rows.Add(row);
                    row = dt.NewRow(); row["month"] = "12"; row["weekday"] = "星期一"; row["cntm"] = 194018; row["cntf"] = 105171; dt.Rows.Add(row);

                    arg.Result = dt;
#endif
                }
                if (DateCntsCompleted != null)
                {
                    DateCntsCompleted.Invoke(this, arg);
                }
            };
            client.DateCntsAsync();
        }

        public event System.EventHandler<ServicesEventArgs<DataTable>> NameTagsCompleted;
        public void NameTags()
        {
            ServiceStatisticsClient client = new ServiceStatisticsClient();
            client.NameTagsCompleted += (sender, e) =>
            {
                ServicesEventArgs<DataTable> arg = new ServicesEventArgs<DataTable>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Name"));
                    dt.Columns.Add(new DataColumn("cnt"));

                    DataRow row = dt.NewRow(); row["Name"] = "张伟"; row["cnt"] = 12759; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王伟"; row["cnt"] = 12510; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王磊"; row["cnt"] = 10776; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李伟"; row["cnt"] = 10206; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张磊"; row["cnt"] = 10069; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘伟"; row["cnt"] = 9151; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李强"; row["cnt"] = 8338; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张勇"; row["cnt"] = 8172; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王勇"; row["cnt"] = 8166; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘洋"; row["cnt"] = 7727; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王军"; row["cnt"] = 7670; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李军"; row["cnt"] = 7527; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王强"; row["cnt"] = 7320; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王涛"; row["cnt"] = 7161; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王静"; row["cnt"] = 7035; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张静"; row["cnt"] = 7005; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张涛"; row["cnt"] = 6988; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李娜"; row["cnt"] = 6914; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李静"; row["cnt"] = 6901; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张军"; row["cnt"] = 6792; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王鹏"; row["cnt"] = 6762; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李明"; row["cnt"] = 6661; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王超"; row["cnt"] = 6628; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王芳"; row["cnt"] = 6612; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张杰"; row["cnt"] = 6439; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张鹏"; row["cnt"] = 6310; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王斌"; row["cnt"] = 6211; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张敏"; row["cnt"] = 6209; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王刚"; row["cnt"] = 6187; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王辉"; row["cnt"] = 6003; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "陈伟"; row["cnt"] = 5982; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李勇"; row["cnt"] = 5928; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李鹏"; row["cnt"] = 5912; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李刚"; row["cnt"] = 5822; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘军"; row["cnt"] = 5808; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张强"; row["cnt"] = 5742; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘涛"; row["cnt"] = 5469; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李斌"; row["cnt"] = 5410; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李杰"; row["cnt"] = 5357; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王健"; row["cnt"] = 5330; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王敏"; row["cnt"] = 5281; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李涛"; row["cnt"] = 5248; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘勇"; row["cnt"] = 5222; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张健"; row["cnt"] = 5220; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张超"; row["cnt"] = 5080; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘杰"; row["cnt"] = 5062; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李敏"; row["cnt"] = 5059; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张斌"; row["cnt"] = 5052; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "杨帆"; row["cnt"] = 4952; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王飞"; row["cnt"] = 4949; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王建军"; row["cnt"] = 2305; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王志强"; row["cnt"] = 2193; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王建华"; row["cnt"] = 2068; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王晓东"; row["cnt"] = 2036; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张建军"; row["cnt"] = 2001; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王婷婷"; row["cnt"] = 1979; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王志刚"; row["cnt"] = 1961; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张建华"; row["cnt"] = 1916; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张志强"; row["cnt"] = 1878; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张婷婷"; row["cnt"] = 1835; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王海燕"; row["cnt"] = 1834; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王建国"; row["cnt"] = 1829; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李建军"; row["cnt"] = 1787; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张建国"; row["cnt"] = 1742; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李志强"; row["cnt"] = 1728; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李建华"; row["cnt"] = 1695; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王海涛"; row["cnt"] = 1582; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张海燕"; row["cnt"] = 1558; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "王建平"; row["cnt"] = 1503; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李婷婷"; row["cnt"] = 1493; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李建国"; row["cnt"] = 1484; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张晓东"; row["cnt"] = 1450; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘婷婷"; row["cnt"] = 1432; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘建军"; row["cnt"] = 1429; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘志强"; row["cnt"] = 1428; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "张海涛"; row["cnt"] = 1377; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "陈建华"; row["cnt"] = 1369; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "刘建华"; row["cnt"] = 1343; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李志刚"; row["cnt"] = 1333; dt.Rows.Add(row);
                    row = dt.NewRow(); row["Name"] = "李海燕"; row["cnt"] = 1331; dt.Rows.Add(row);

                    arg.Result = dt;
#endif
                }
                if (NameTagsCompleted != null)
                {
                    NameTagsCompleted.Invoke(this, arg);
                }
            };
            client.NameTagsAsync();
        }
        #endregion

        #region ServiceCodeGenerator
        public event System.EventHandler<ServicesEventArgs<ServiceCodeGenerator.table[]>> queryTablesCompleted;
        public void queryTables()
        {
            ServiceCodeGenerator.ServiceCodeGeneratorClient client = new ServiceCodeGenerator.ServiceCodeGeneratorClient();
            client.queryTablesCompleted += (sender, e) =>
            {
                ServicesEventArgs<ServiceCodeGenerator.table[]> arg = new ServicesEventArgs<ServiceCodeGenerator.table[]>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    arg.Result = null;
#endif
                    //写错误日志
                    //.....
                }
                if (queryTablesCompleted != null)
                {
                    queryTablesCompleted.Invoke(this, arg);
                }
            };
            client.queryTablesAsync();
        }

        public event System.EventHandler<ServicesEventArgs<ServiceCodeGenerator.table[]>> queryViewsCompleted;
        public void queryViews()
        {
            ServiceCodeGenerator.ServiceCodeGeneratorClient client = new ServiceCodeGenerator.ServiceCodeGeneratorClient();
            client.queryViewsCompleted += (sender, e) =>
            {
                ServicesEventArgs<ServiceCodeGenerator.table[]> arg = new ServicesEventArgs<ServiceCodeGenerator.table[]>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
#if DEBUG
                    arg.Succesed = true;
                    arg.Result = null;
#endif
                    //写错误日志
                    //.....
                }
                if (queryViewsCompleted != null)
                {
                    queryViewsCompleted.Invoke(this, arg);
                }
            };
            client.queryViewsAsync();
        }

        public event System.EventHandler<ServicesEventArgs<ServiceCodeGenerator.column[]>> queryColumnsCompleted;
        public ServiceCodeGenerator.column[] queryColumns(string tableName)
        {
            ServiceCodeGenerator.ServiceCodeGeneratorClient client = new ServiceCodeGenerator.ServiceCodeGeneratorClient();
            return client.queryColumns(tableName);
        }
        #endregion
    }
}
