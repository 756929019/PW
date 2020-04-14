using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PW.SocketServer
{
    
    public class MyServer
    {
        Thread threadWatch = null;// 负责监听客户端的线程
        Socket socketWatch = null;// 负责监听客户端的套接字
        Dictionary<String, ClientUser> clientList = null;
        Int32 con_msg_length = 10240;

        private static MyServer uniqueInstance;

        public delegate void ReceivedMsgHandler(string msg);
        public ReceivedMsgHandler ReceivedMsg;//自定义事件

        public delegate void ClientChangeHandler(ClientUser client, Dictionary<String, ClientUser> clientList);

        public ClientChangeHandler ClientAdd;//自定义事件
        public ClientChangeHandler ClientRemove;//自定义事件
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static MyServer GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new MyServer();
            }
            return uniqueInstance;
        }

        private MyServer()
        {
            try
            {
                string ServerIP = System.Configuration.ConfigurationManager.AppSettings["ServerIP"].ToString();
                string ServerPort = System.Configuration.ConfigurationManager.AppSettings["ServerPort"].ToString();
                con_msg_length = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MsgLength"].ToString());
                IPAddress ip = null;
                if (string.IsNullOrEmpty(ServerIP))
                {
                    ip = IPAddress.Any;//创建IP//Any 字段等效于以点分隔的四部分表示法格式的 0.0.0.0 这个IP地址，实际是一个广播地址。//对于SOCKET而言，使用 any ，表示，侦听本机的所有IP地址的对应的端口（本机可能有多个IP或只有一个IP）
                }
                else
                {
                    ip = IPAddress.Parse(ServerIP);//监听指定ip
                }
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(ServerPort));//创建终结点（EndPoint）
                clientList = new Dictionary<string, ClientUser>();

                // 定义一个套接字用于监听客户端发来的消息，包含三个参数（ipv4寻址协议，流式连接，tcp协议）
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 监听绑定的网路节点
                socketWatch.Bind(point);
                // 将套接字的监听队列长度设置限制为0，0表示无限
                socketWatch.Listen(0);

                // 创建一个监听线程
                threadWatch = new Thread(WatchConnecting);
                threadWatch.IsBackground = true;
               
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        public void BeginServer()
        {
            try
            {
                threadWatch.Start();
                AppendText("成功启动监听！");
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        /// <summary>
        ///  监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            //持续不断监听客户端发来的请求
            while (true)
            {
                try
                {
                    Socket clientSocket = socketWatch.Accept();
                    byte[] recMsg = new byte[con_msg_length];
                    if (clientSocket != null && clientSocket.Connected)
                    {
                        int length = clientSocket.Receive(recMsg);
                        string msg = "";
                        if (length > 0)
                        {
                            //将机器接受到的字节数组转换为人可以读懂的字符串
                            msg = Encoding.UTF8.GetString(recMsg, 0, length);
                        }
                        AppendText("==[" + msg + "]==" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                        SocketMsg smsg = new SocketMsg();
                        try
                        {
                            smsg.setAllParametersJsonStr(msg);
                        }
                        catch
                        {

                        }
                        Hashtable param = smsg.getAllParameters();
                        ClientUser cu = new ClientUser();
                        cu.ClientSocket = clientSocket;
                        IPEndPoint ip = (IPEndPoint)clientSocket.RemoteEndPoint;
                        cu.CliIp = ip.Address.ToString();
                        cu.CliPort = ip.Port.ToString();
                        cu.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        AppendText("客户端连接！" + cu.CliIp + ":" + cu.CliPort + "\r\n");
                        string userno = "";
                        if (param != null && param.Count > 0)
                        {
                            userno = param["UserNo"].ToString();
                            cu.UserNo = userno;
                            cu.UserName = param["UserName"].ToString();
                        }
                        if (!clientList.ContainsKey(userno))
                        {
                            clientList.Add(userno, cu);
                            if (ClientAdd != null)
                            {
                                ClientAdd(cu, clientList);
                            }
                        }
                        else
                        {
                            if (ClientRemove != null)
                            {
                                ClientRemove(clientList[userno], clientList);
                            }
                            clientList.Remove(userno);
                            
                            clientList.Add(userno, cu);
                            if (ClientAdd != null)
                            {
                                ClientAdd(cu, clientList);
                            }
                        }
                        AppendText("客户端连接成功！" + msg + "\r\n");
                        // 创建一个通信线程
                        ParameterizedThreadStart pts = new ParameterizedThreadStart(acceptMsg);
                        Thread thr = new Thread(pts);
                        thr.IsBackground = true;
                        thr.Start(clientSocket);
                    }
                }
                catch (Exception ex)
                {
                    WriteTxtLog(ex.Message);
                }
            }
        }

        /// <summary>
        ///  接收客户端发来的消息
        /// </summary>
        /// <param name="socket">客户端套接字对象</param>
        private void acceptMsg(object socket)
        {
            Socket socketServer = socket as Socket;
            while (true)
            {
                try
                {
                    //创建一个内存缓冲区 其大小为1024*1024字节  即1M
                    byte[] recMsg = new byte[con_msg_length];
                    //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                    int length = socketServer.Receive(recMsg);
                    //将机器接受到的字节数组转换为人可以读懂的字符串
                    if (length > 0)
                    {
                        try
                        {
                            string msg = Encoding.UTF8.GetString(recMsg, 0, length);
                            AppendText("==[" + msg + "]==" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                            SocketMsg smsg = new SocketMsg();
                            try
                            {
                                smsg.setAllParametersJsonStr(msg);
                            }
                            catch
                            {

                            }
                            Hashtable param = smsg.getAllParameters();
                            if (param != null && param.Count > 0)
                            {
                                AppendText("客户端(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "):" + smsg.ConvertJson2Str(param["msgContent"]) + "\r\n");

                                string usernos = param["UserNo"] == null ? "" : param["UserNo"].ToString();
                                string[] nos = usernos.Split(',');
                                if (nos != null && nos.Length > 0)
                                {
                                    //转发消息到目标客户端
                                    foreach (string no in nos)
                                    {
                                        if (clientList.ContainsKey(no) && clientList[no].ClientSocket != socketServer)
                                        {
                                            serverSendMsg(clientList[no].ClientSocket, smsg.ConvertJson2Str(param["msgContent"]));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception exx)
                        {
                            AppendText("" + exx.Message + "(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "):\r\n");
                        }
                    }
                    else
                    {
                        string clin = "";
                        foreach (string key in clientList.Keys)
                        {
                            if (clientList[key].ClientSocket == socketServer)
                            {
                                clin = "userno:" + clientList[key].UserNo + "  name:" + clientList[key].UserName + "  ip:" + clientList[key].CliIp;
                                if (ClientRemove != null)
                                {
                                    ClientRemove(clientList[key], clientList);
                                }
                                clientList.Remove(key);
                                break;
                            }
                        }
                        AppendText("客户端[" + clin + "]断开(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "):\r\n");
                        socketServer.Disconnect(true);
                        socketServer.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    string clin = "";
                    foreach (string key in clientList.Keys)
                    {
                        if (clientList[key].ClientSocket == socketServer)
                        {
                            clin = "userno:" + clientList[key].UserNo + "  name:" + clientList[key].UserName + "  ip:" + clientList[key].CliIp;
                            if (ClientRemove != null)
                            {
                                ClientRemove(clientList[key], clientList);
                            }
                            clientList.Remove(key);
                            break;
                        }
                    }
                    AppendText("客户端[" + clin + "]断开(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "):\r\n");
                    socketServer.Disconnect(true);
                    socketServer.Close();
                    break;
                }
            }
        }

        /// <summary>
        ///  发送消息到客户端
        /// </summary>
        /// <param name="msg"></param>
        public void serverSendMsg(Socket clientSocket, string msg)
        {
            try
            {
                byte[] sendMsg = Encoding.UTF8.GetBytes(msg);
                clientSocket.Send(sendMsg);
                AppendText("服务端(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "):" + msg + "\r\n");
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }


        private void AppendText(String theContent)
        {
            Console.WriteLine(theContent);
            WriteTxtLog(theContent);
        }

        private void WriteTxtLog(String theContent)
        {
            try
            {
                String theFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                String theFilePath = Application.StartupPath + "\\MsgLog\\";
                String theFullName = theFilePath + theFileName;

                //判断文件夹是否存在
                if (Directory.Exists(theFilePath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(theFilePath);
                }

                StreamWriter sw = null;
                //判断文件是否存在
                if (File.Exists(theFullName))
                {
                    sw = File.AppendText(theFullName);
                }
                else
                {
                    sw = File.CreateText(theFullName);//创建该文件
                }
                //导出传盘文件
                //开始写入
                sw.WriteLine(theContent);
                //关闭流
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
        public Dictionary<String, ClientUser> getClient()
        {
            return clientList;
        }
    }
}
