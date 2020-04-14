using Prism.Commands;
using PW.Chat.Model;
using PW.Common.Helpers;
using PW.Controls;
using PW.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Utility.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PW.Chat.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        public MainViewModel()
        {
            timer.Interval = 2000;
            timer.Elapsed += Timer_Elapsed;
            Init();
        }

        #region 字段属性
        /// <summary>
        /// 当前登录用户
        /// </summary>
        private ChatUser _me;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public ChatUser Me
        {
            get
            {
                return _me;
            }

            set
            {
                _me = value;
                NotifyPropertyChanged("Me");
            }
        }

        private ChatUser _friendUser;
        /// <summary>
        ///  聊天好友
        /// </summary>
        public ChatUser FriendUser
        {
            get
            {
                return _friendUser;
            }

            set
            {
                if (value != _friendUser)
                {
                    if (_friendUser != null)
                    {
                        _friendUser.MsgRecved -= new ChatUser.MsgRecvedEventHandler(_friendUser_MsgRecved);
                        _friendUser.MsgSent -= new ChatUser.MsgSentEventHandler(_friendUser_MsgSent);
                    }
                    _friendUser = value;
                    if (_friendUser != null)
                    {
                        _friendUser.MsgRecved += new ChatUser.MsgRecvedEventHandler(_friendUser_MsgRecved);
                        _friendUser.MsgSent += new ChatUser.MsgSentEventHandler(_friendUser_MsgSent);
                        IEnumerable<KeyValuePair<Guid, ChatMsgData>> dic = _friendUser.RecvedMsg.Concat(_friendUser.SentMsg);
                        dic = dic.OrderBy(p => p.Value.Time);
                        foreach (KeyValuePair<Guid, ChatMsgData> p in dic)
                        {
                            if (p.Value.From == _friendUser.UserName)
                            {
                                ShowReceiveMsg(p.Value);
                            }
                            else
                            {
                                ShowSendMsg(p.Value);
                            }
                            p.Value.Readed = true;
                            _friendUser.UnReadCount = 0;//读了以后，就清除                         
                        }
                    }
                }
                NotifyPropertyChanged("FriendUser");
            }
        }

        private ChatUser _friendInfo;
        /// <summary>
        /// 好友信息
        /// </summary>
        public ChatUser FriendInfo
        {
            get
            {
                return _friendInfo;
            }

            set
            {
                _friendInfo = value;
                NotifyPropertyChanged("FriendInfo");
            }
        }

        /// <summary>
        /// 所有好友列表
        /// </summary>
        private List<object> _contact_all = new List<object>();
        /// <summary>
        /// 通讯录
        /// </summary>
        public List<object> Contact_all
        {
            get
            {
                return _contact_all;
            }

            set
            {
                _contact_all = value;
                NotifyPropertyChanged("Contact_all");
            }
        }
        /// <summary>
        /// 部分好友列表
        /// </summary>
        private ObservableCollection<object> _contact_latest = new ObservableCollection<object>();
        /// <summary>
        /// 最近联系人
        /// </summary>
        public ObservableCollection<object> Contact_latest
        {
            get
            {
                return _contact_latest;
            }

            set
            {
                _contact_latest = value;
                NotifyPropertyChanged("Contact_latest");
            }
        }

        private object _select_Contact_latest = new object();
        /// <summary>
        /// 聊天列表选中
        /// </summary>
        public object Select_Contact_latest
        {
            get
            {
                return _select_Contact_latest;
            }

            set
            {
                _select_Contact_latest = value;
                NotifyPropertyChanged("Select_Contact_latest");
            }
        }

        private object _select_Contact_all = new object();
        /// <summary>
        /// 通讯录选中
        /// </summary>
        public object Select_Contact_all
        {
            get
            {
                return _select_Contact_all;
            }

            set
            {
                _select_Contact_all = value;
                NotifyPropertyChanged("Select_Contact_all");
            }
        }

        private string _userName = string.Empty;
        /// <summary>
        /// 用于在顶部显示用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private ObservableCollection<ChatMsg> chatList = new ObservableCollection<ChatMsg>();
        /// <summary>
        /// 聊天列表
        /// </summary>
        public ObservableCollection<ChatMsg> ChatList
        {
            get
            {
                return chatList;
            }

            set
            {
                chatList = value;
                NotifyPropertyChanged("ChatList");
            }
        }
        /// <summary>
        /// 发送消息内容
        /// </summary>
        private string _sendMessage;
        public string SendMessage
        {
            get
            {
                return _sendMessage;
            }

            set
            {
                _sendMessage = value;
                NotifyPropertyChanged("SendMessage");
            }
        }

        private FlowDocument _showSendMessage = new FlowDocument();
        /// <summary>
        /// 发送框显示的发送内容
        /// </summary>
        public FlowDocument ShowSendMessage
        {
            get
            {
                return _showSendMessage;
            }

            set
            {
                _showSendMessage = value;
                NotifyPropertyChanged("ShowSendMessage");
            }
        }

        private Visibility tootip_Visibility = Visibility.Collapsed;
        /// <summary>
        /// 是否显示提示
        /// </summary>
        public Visibility Tootip_Visibility
        {
            get
            {
                return tootip_Visibility;
            }

            set
            {
                tootip_Visibility = value;
                NotifyPropertyChanged("Tootip_Visibility");
            }
        }

        private bool _isChecked = true;
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }

        private Visibility emoji_Visibility = Visibility.Collapsed;
        /// <summary>
        /// Emoji显隐
        /// </summary>
        public Visibility Emoji_Visibility
        {
            get
            {
                return emoji_Visibility;
            }

            set
            {
                emoji_Visibility = value;
                NotifyPropertyChanged("Emoji_Visibility");
            }
        }

        private bool emoji_Popup = false;
        /// <summary>
        /// Popup是否弹出
        /// </summary>
        public bool Emoji_Popup
        {
            get
            {
                return emoji_Popup;
            }

            set
            {
                emoji_Popup = value;
                NotifyPropertyChanged("Emoji_Popup");
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //初始化
            //JObject init_result = wcs.WeChatInit();

            List<object> contact_all = new List<object>();

            _me = new ChatUser();
            _me.UserName = "meUsername";
            _me.City = "City" ;
            _me.HeadImgUrl = "HeadImgUrl";
            _me.NickName = "NickName";
            _me.PyQuanPin = "PYQuanPin" ;
            _me.RemarkName = "RemarkName" ;
            _me.RemarkPYQuanPin = "RemarkPYQuanPin" ;
            _me.Sex = "Sex";
            _me.Icon = GetIcon(_me);
            //最近好友名单
            for (int i = 0;i<15;i++)
            {
                ChatUser user = new ChatUser();
                user.UserName = "UserName"+i;
                user.City = "City" + i;
                user.HeadImgUrl = "HeadImgUrl" + i;
                user.NickName = "NickName" + i;
                user.PyQuanPin = "PYQuanPin" + i;
                user.RemarkName = "张三" + i;
                user.RemarkPYQuanPin = "RemarkPYQuanPin" + i;
                user.LastTime = DateTime.Now.ToShortTimeString();
                user.Sex = "Sex" + i;
                user.Icon = GetIcon(user);
                user.UnReadCount = i%5;
                _contact_latest.Add(user);
            }
            //设置最后
            Select_Contact_latest = _contact_latest[0];
          
            //通讯录
            for (int i = 0; i < 100; i++)
            {
                ChatUser user = new ChatUser();
                user.UserName = "UserName" + i;
                user.City = "City" + i;
                user.HeadImgUrl = "HeadImgUrl" + i;
                user.NickName = "NickName" + i;
                user.PyQuanPin = "PYQuanPin"+i;
                user.RemarkName = "RemarkName" + i;
                switch (i % 5)
                {
                    case 0:
                        user.RemarkPYQuanPin = "ARemarkPYQuanPin" + i;
                        break;
                    case 1:
                        user.RemarkPYQuanPin = "BRemarkPYQuanPin" + i;
                        break;
                    case 2:
                        user.RemarkPYQuanPin = "CRemarkPYQuanPin" + i;
                        break;
                    case 3:
                        user.RemarkPYQuanPin = "DRemarkPYQuanPin" + i;
                        break;
                    case 4:
                        user.RemarkPYQuanPin = "ERemarkPYQuanPin" + i;
                        break;
                }
                user.Sex = "Sex" + i;
                user.StartChar = GetStartChar(user);
                user.Icon = GetIcon(user);
                contact_all.Add(user);
            }

            IOrderedEnumerable<object> list_all = contact_all.OrderBy(p => (p as ChatUser).StartChar).ThenBy(p => (p as ChatUser).ShowPinYin.Substring(0, 1));

            ChatUser wcu;
            string start_char;
            foreach (object o in list_all)
            {
                wcu = o as ChatUser;
                start_char = wcu.StartChar;
                if (!_contact_all.Contains(start_char.ToUpper()))
                {
                    _contact_all.Add(start_char.ToUpper());
                }
                _contact_all.Add(o);
            }

            SocketClient socketClient = SocketClient.GetInstance();
            socketClient.ReceivedMsg = new SocketClient.ReceivedMsgHandler(socketClient_ReceivedMsg);
        }
        void socketClient_ReceivedMsg(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate
            {
                ChatUser us = _contact_latest[3] as ChatUser;
                //ChatUser us = this._friendUser;
                SocketMsg smsg = new SocketMsg();
                smsg.setAllParametersJsonStr(msg);
                Hashtable param = smsg.getAllParameters();
                ChatMsgData msg1 = new ChatMsgData();
                msg1.From = us.UserName;
                msg1.Msg = param["content"].ToString();
                msg1.Readed = false;
                msg1.Time = DateTime.Now;
                msg1.To = _me.UserName;
                msg1.Type = 1;
                us.UnReadCount = us.UnReadCount + 1;
                us.ReceivedMsg(msg1);
            }));
        }
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="wcs"></param>
        /// <param name="_userName"></param>
        /// <returns></returns>
        private ImageSource GetIcon(ChatUser user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return null;
            }
            ImageSource _icon = null;
            //service获取头像流
            //byte[] bytes = null;
            //_icon = ImageHelper.MemoryToImageSourceOther(new MemoryStream(bytes));
            //群聊
            if (_userName.Contains("@@"))
            {
                
            }
            //好友
            else if (_userName.Contains("@"))
            {
            }
            else
            {
            }
            if (_icon == null)
            {
                _icon = Images.CreateImageSourceFromImage(Properties.Resources.img);
            }
            return _icon;
        }

        /// <summary>
        /// 获取分组的头
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetStartChar(ChatUser user)
        {
            string start_char;
            if (user.UserName.Contains("@@") && user.SnsFlag.Equals("0"))
            {
                start_char = "群聊";
            }
            else
            {
                start_char = string.IsNullOrEmpty(user.ShowPinYin) ? string.Empty : user.ShowPinYin.Substring(0, 1);
            }
            return start_char;
        }

        /// <summary>
        /// 倒计时隐藏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tootip_Visibility = Visibility.Collapsed;
            timer.Stop();
        }
        /// <summary>
        /// 获取要发送的Emoji名
        /// </summary>
        /// <param name="str">相对路径的值</param>
        /// <returns></returns>
        private string GetEmojiName(string str)
        {
            foreach (var item in ContantClass.EmojiCode)
            {
                if (item.Value.ToString().Equals(str))
                {
                    return item.Key;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 将Document里的值都换成String
        /// </summary>
        /// <param name="fld"></param>
        /// <returns></returns>
        private List<object> GetSendMessage(FlowDocument fld)
        {
            List<object> list = new List<object>();
            if (fld == null)
            {
                list.Add(string.Empty);
                return list;
            }
            string resutStr = string.Empty;
            foreach (var root in fld.Blocks)
            {
                if (resutStr.Length > 0)
                {
                    resutStr += "\n";
                }
                foreach (var item in ((Paragraph)root).Inlines)
                {
                    
                    if (item is InlineUIContainer)
                    {
                        System.Windows.Controls.Image img = (System.Windows.Controls.Image)((InlineUIContainer)item).Child;
                        if (img.Source.ToString() == "System.Windows.Interop.InteropBitmap")
                        {
                            //截图
                            if (resutStr.Length > 0)
                            {
                                list.Add(resutStr);
                                resutStr = "";
                            }
                            list.Add(img);
                        }
                        else
                        {
                            //如果是Emoji则进行转换
                            resutStr += GetEmojiName(img.Source.ToString());
                        }
                    }
                    //如果是文本，则直接赋值
                    if (item is Run)
                    {
                        resutStr += ((Run)item).Text;
                    }
                }
            }
            if(resutStr.Length>0)
            {
                list.Add(resutStr);
            }
            
            return list;
        }
        /// <summary>
        /// 获取Emoji的名字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetEmojiNameByRegex(string str)
        {
            string name = Regex.Match(str, "(?<=\\[).*?(?=\\])").Value;
            return "[" + name + "]";
        }
        /// <summary>
        /// 获取文本信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetTextByRegex(string str)
        {
            string text = Regex.Match(str, "^.*?(?=\\[)").Value;
            return text;
        }
        /// <summary>
        /// 将字符串转换成FlowDocument
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="fld">要被赋值的Flowdocument</param>
        /// <param name="par">要添加到Flowdocument里的Paragraph</param>
        private void StrToFlDoc(string str, FlowDocument fld)
        {
            //当递归结束以后，也就是长度为0的时候，就跳出
            if (str.Length <= 0)
            {
                fld.Blocks.Add(new Paragraph());
                return;
            }
            string[] linesStr = str.Split('\n');
            foreach (string item in linesStr)
            {
                LineStrToFlDoc(item, fld, new Paragraph());
            }
        }

        /// <summary>
        /// 将字符串转换成FlowDocument
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="fld">要被赋值的Flowdocument</param>
        /// <param name="par">要添加到Flowdocument里的Paragraph</param>
        private void LineStrToFlDoc(string str, FlowDocument fld, Paragraph par)
        {
            //当递归结束以后，也就是长度为0的时候，就跳出
            if (str.Length <= 0)
            {
                fld.Blocks.Add(par);
                return;
            }
            //如果字符串里不存在[时，则直接添加内容
            if (!str.Contains('['))
            {
                par.Inlines.Add(new Run(str));
                str = str.Remove(0, str.Length);
                LineStrToFlDoc(str, fld, par);
            }
            else
            {
                //设置字符串长度
                int strLength = str.Length;
                if (str[0].Equals('['))
                {
                    par.Inlines.Add(new InlineUIContainer(new System.Windows.Controls.Image { Width = 20, Height = 20, Source = ContantClass.EmojiCode[GetEmojiNameByRegex(str)] }));
                    str = str.Remove(0, GetEmojiNameByRegex(str).Length);
                    LineStrToFlDoc(str, fld, par);
                }
                else
                {//如果第一位不是[的话，则是字符串，直接添加进去
                    par.Inlines.Add(new Run(GetTextByRegex(str)));
                    str = str.Remove(0, GetTextByRegex(str).Length);
                    LineStrToFlDoc(str, fld, par);
                }
            }
        }
        #endregion

        #region 聊天事件
        private ICommand _loadedCommand;
        /// <summary>
        /// 载入
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new DelegateCommand(() =>
                {
                    //Thread listener = new Thread(new ThreadStart(new Action(() =>
                    //{

                    //})));
                    //listener.Start();
                    
                    if(_contact_latest!=null&& _contact_latest.Count>0)
                    {
                        DateTime tmpdt = DateTime.Now;
                        //初始化最近聊天记录
                        foreach (ChatUser item in _contact_latest)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                ChatMsgData msg = new ChatMsgData();
                                msg.From = item.UserName;
                                msg.Msg = "吃了吗";//只接受文本消息
                                msg.Readed = false;
                                msg.Time = tmpdt.AddMinutes(i + 1);
                                msg.To = _me.UserName;
                                msg.Type = 1;
                                item.SendMsg(msg, true);
                                ChatMsgData msg1 = new ChatMsgData();
                                msg1.From = _me.UserName;
                                msg1.Msg = "吃了";//只接受文本消息
                                msg1.Readed = false;
                                msg1.Time = tmpdt.AddMinutes(i + 2);
                                msg1.To = item.UserName;
                                msg1.Type = 1;
                                item.ReceivedMsg(msg1);
                            }
                        }
                        UserName = (Select_Contact_latest as ChatUser).ShowName;
                        ChatList.Clear();
                        FriendUser = Select_Contact_latest as ChatUser;
                    }
                }));
            }
        }

        private ICommand _chatCommand;
        /// <summary>
        /// 聊天列表的选中事件
        /// </summary>
        public ICommand ChatCommand
        {
            get
            {
                return _chatCommand ?? (_chatCommand = new DelegateCommand(() =>
                {
                    if (Select_Contact_latest is ChatUser)
                    {
                        UserName = (Select_Contact_latest as ChatUser).ShowName;
                        ChatList.Clear();
                        FriendUser = Select_Contact_latest as ChatUser;
                    }
                }));
            }
        }

        private ICommand _friendCommand;
        /// <summary>
        /// 通讯录选中事件
        /// </summary>
        public ICommand FirendCommand
        {
            get
            {
                return _friendCommand ?? (_friendCommand = new DelegateCommand(() =>
                {
                    if (Select_Contact_all is ChatUser)
                    {
                        FriendInfo = Select_Contact_all as ChatUser;
                    }
                }));
            }
        }

        private ICommand _friendSendComamnd;
        /// <summary>
        /// 用户信息页面的发送消息按钮
        /// </summary>
        public ICommand FriendSendComamnd
        {
            get
            {
                return _friendSendComamnd ?? (_friendSendComamnd = new DelegateCommand(() =>
                {
                    Contact_latest.Remove(Select_Contact_all);
                    Contact_latest.Insert(0, Select_Contact_all);
                    IsChecked = true;
                }));
            }
        }

        private ICommand _sendCommand;
        /// <summary>
        /// 发送消息
        /// </summary>
        public ICommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new DelegateCommand(() =>
                {
                    if (_friendUser != null) { 
                        List<object> list = GetSendMessage(ShowSendMessage);
                        if(list!=null&& list.Count>0)
                        {
                            if (list.Count == 1 && list[0] is string && list[0].ToString() == "")
                            {
                                Tootip_Visibility = Visibility.Visible;
                                timer.Start();
                            }
                            else
                            {
                                foreach (var item in list)
                                {
                                    if (item is string)
                                    {
                                        SendMessage = item.ToString();
                                        if (!string.IsNullOrEmpty(SendMessage))
                                        {
                                            ChatMsgData msg = new ChatMsgData();
                                            msg.From = _me.UserName;
                                            msg.Readed = false;
                                            msg.To = _friendUser.UserName;
                                            msg.Type = 1;
                                            msg.Msg = SendMessage;
                                            msg.Time = DateTime.Now;
                                            _friendUser.SendMsg(msg, false);
                                            SendMessage = string.Empty;
                                            
                                        }
                                    }
                                    else
                                    {
                                        ChatMsgData msg = new ChatMsgData();
                                        msg.From = _me.UserName;
                                        msg.Readed = false;
                                        msg.To = _friendUser.UserName;
                                        msg.Type = 9;
                                        msg.Msg = "";
                                        Image img = new Image();
                                        img.Source = (item as Image).Source;
                                        img.Width = (item as Image).Width;
                                        img.Height = (item as Image).Height;
                                        msg.MsgImg = img; 
                                        msg.Time = DateTime.Now;
                                        _friendUser.SendMsg(msg, false);
                                        SendMessage = string.Empty;
                                    }
                                }
                                ShowSendMessage.Blocks.Clear();
                            }
                        }
                    }
                }));
            }
        }

        private ICommand _ctrlEnterCommand;
        /// <summary>
        /// CtrlEnterCommand
        /// </summary>
        public ICommand CtrlEnterCommand
        {
            get
            {
                return _ctrlEnterCommand ?? (_ctrlEnterCommand = new DelegateCommand<BindableRichTextBox>((brt) =>
                {
                    //brt.CaretPosition.InsertLineBreak();
                    //brt.CaretPosition = brt.CaretPosition.GetNextInsertionPosition(LogicalDirection.Forward);
                    brt.Focus();
                    brt.CaretPosition = brt.CaretPosition.InsertParagraphBreak();
                }));
            }
        }

        private ICommand _shiftEnterCommand;
        /// <summary>
        /// CtrlEnterCommand
        /// </summary>
        public ICommand ShiftEnterCommand
        {
            get
            {
                return _shiftEnterCommand ?? (_shiftEnterCommand = new DelegateCommand<BindableRichTextBox>((brt) =>
                {
                    brt.Focus();
                    brt.CaretPosition = brt.CaretPosition.InsertParagraphBreak();
                }));
            }
        }

        /// <summary>
        /// 表示处理开启聊天事件的方法
        /// </summary>
        /// <param name="user"></param>
        public delegate void StartChatEventHandler(ChatUser user);
        public event StartChatEventHandler StartChat;

        /// <summary>
        /// 发送消息完成
        /// </summary>
        /// <param name="msg"></param>
        void _friendUser_MsgSent(ChatMsgData msg)
        {
            ShowSendMsg(msg);
        }
        /// <summary>
        /// 收到新消息
        /// </summary>
        /// <param name="msg"></param>
        void _friendUser_MsgRecved(ChatMsgData msg)
        {
            ShowReceiveMsg(msg);
        }
        /// <summary>
        /// 显示发出的消息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowSendMsg(ChatMsgData msg)
        {
            try
            {
                ChatMsg chatmsg = new ChatMsg();
                chatmsg.Image = _me.Icon;
                //此处的Paragraph必须是新New的
                if (msg.Type == 9)
                {
                    Paragraph imgper = new Paragraph();
                    imgper.Inlines.Add(new InlineUIContainer(msg.MsgImg));
                    chatmsg.Message.Blocks.Add(imgper);
                }
                else
                {
                    StrToFlDoc(msg.Msg, chatmsg.Message);
                }
                
                chatmsg.FlowDir = FlowDirection.RightToLeft;
                chatmsg.TbColor = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#FF98E165");
                ChatList.Add(chatmsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 显示收到的信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowReceiveMsg(ChatMsgData msg)
        {
            ChatMsg chatmsg = new ChatMsg();
            Contact_all.ForEach(p =>
            {
                if (p is ChatUser)
                {
                    if ((p as ChatUser).UserName == msg.From)
                    {
                        chatmsg.Image = (p as ChatUser).Icon;
                        return;
                    }
                }
            });

            if (msg.Type == 9)
            {
                Paragraph imgper = new Paragraph(new Floater(new BlockUIContainer(msg.MsgImg)));
                chatmsg.Message.Blocks.Add(imgper);
            }
            else
            {
                StrToFlDoc(msg.Msg, chatmsg.Message);
            }

            chatmsg.FlowDir = FlowDirection.LeftToRight;
            chatmsg.TbColor = System.Windows.Media.Brushes.White;
            ChatList.Add(chatmsg);
        }
        #endregion

        #region 插件部分按钮事件
        private ICommand _emojiCommand;
        /// <summary>
        /// Emoji按钮事件
        /// </summary>
        public ICommand EmojiCommand
        {
            get
            {
                return _emojiCommand ?? (_emojiCommand = new DelegateCommand(() =>
                {
                    Emoji_Popup = true;
                }));
            }
        }

        private ICommand _fileCommand;
        /// <summary>
        /// FileCommand
        /// </summary>
        public ICommand FileCommand
        {
            get
            {
                return _fileCommand ?? (_fileCommand = new DelegateCommand<BindableRichTextBox>((brt) =>
                {
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Paragraph par = new Paragraph();
                        par.Inlines.Add(new Run("file:"+openFileDialog.FileName));
                        brt.Document.Blocks.Add(par);
                    }
                }));
            }
        }

        private ICommand _cutCommand;
        /// <summary>
        /// CutCommand
        /// </summary>
        public ICommand CutCommand
        {
            get
            {
                return _cutCommand ?? (_cutCommand = new DelegateCommand<BindableRichTextBox>((brt) =>
                {
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Paragraph par = new Paragraph();
                        par.Inlines.Add(new Run("img:" + openFileDialog.FileName));
                        brt.Document.Blocks.Add(par);
                    }
                }));
            }
        }
        #endregion

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}