using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace PW.Infrastructure
{

    public class SocketClient
    {
        private static SocketClient uniqueInstance;
        // 创建一个客户端套接字
        Socket clientSocket = null;
        // 创建一个监听服务端的线程
        Thread threadServer = null;

        string start_msg = "";

        public delegate void ReceivedMsgHandler(string msg);
        public ReceivedMsgHandler ReceivedMsg;//自定义事件
        private SocketClient()
        {
            try
            {
                start_msg = "{UserNo:'" + GlobalData.UserName + "',UserName:'" + GlobalData.NickName + "'}";
            }
            catch(Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static SocketClient GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (uniqueInstance == null)
            {
                uniqueInstance = new SocketClient();
            }
            return uniqueInstance;
        }

        public void BeginConnect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(GlobalData.SocketIP);

                clientSocket.BeginConnect(ip, GlobalData.SocketPort, (args) =>
                {
                    if (args.IsCompleted)   //判断该异步操作是否执行完毕
                    {
                        Byte[] bytesSend = new Byte[GlobalData.SocketMsgSize];
                        bytesSend = Encoding.UTF8.GetBytes(start_msg);  //用户名，这里是刚刚连接上时需要传过去
                        if (clientSocket != null && clientSocket.Connected)
                        {
                            clientSocket.Send(bytesSend);
                            WriteTxtLog("链接成功(" + GetCurrentTime() + "):\r\n");
                            // 创建一个线程监听服务端发来的消息
                            threadServer = new Thread(recMsg);
                            threadServer.IsBackground = true;
                            threadServer.Start();
                        }
                        else
                        {
                            WriteTxtLog("服务器未开启(" + GetCurrentTime() + "):\r\n");
                            // 创建一个线程监听服务端发来的消息
                            threadServer = new Thread(recMsg);
                            threadServer.IsBackground = true;
                            threadServer.Start();
                        }
                    }
                }, null);
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        /// <summary>
        ///  接收服务端发来的消息
        /// </summary>
        private void recMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                //定义一个1M的内存缓冲区 用于临时性存储接收到的信息
                byte[] arrRecMsg = new byte[GlobalData.SocketMsgSize];
                int length = 0;
                try
                {
                    //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                    length = clientSocket.Receive(arrRecMsg);

                    //将套接字获取到的字节数组转换为人可以看懂的字符串
                    string strRecMsg = Encoding.UTF8.GetString(arrRecMsg, 0, length);
                    if (ReceivedMsg != null)
                    {
                        ReceivedMsg(strRecMsg);
                    }
                    //将发送的信息追加到聊天内容文本框中
                    WriteTxtLog("服务端(" + GetCurrentTime() + "):" + strRecMsg + "\r\n");
                }
                catch(Exception ex)
                {
                    WriteTxtLog("" + ex.Message+ ":\r\n");
                    WriteTxtLog("服务器已关闭(" + GetCurrentTime() + "):\r\n");
                    //重连
                    Thread.Sleep(10 * 1000);
                    if (!clientSocket.Connected)
                    {
                        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        clientSocket.BeginConnect(IPAddress.Parse(GlobalData.SocketIP), GlobalData.SocketPort, (args) =>
                        {
                            if (args.IsCompleted)   //判断该异步操作是否执行完毕
                            {
                                Byte[] bytesSend = new Byte[GlobalData.SocketMsgSize];
                                bytesSend = Encoding.UTF8.GetBytes(start_msg);  //用户名，这里是刚刚连接上时需要传过去
                                if (clientSocket != null && clientSocket.Connected)
                                {
                                    clientSocket.Send(bytesSend);
                                    WriteTxtLog("重连链接成功(" + GetCurrentTime() + "):\r\n");
                                }
                                else
                                {
                                    WriteTxtLog("重连失败服务器已关闭(" + GetCurrentTime() + "):\r\n");
                                }
                            }
                        }, null);
                    }
                }
            }
        }

        /// <summary>
        /// 发送消息到服务端
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string title, string content, string userno)
        {
            try
            {
                string msg = "{title:'" + title + "',content:'" + content + "'}";
                //userno:"001,002,003"
                if (string.IsNullOrEmpty(userno))
                {
                    userno = "0";
                }
                string msgcon = "{msgType:'msg',msgContent:'" + msg + "',UserNo:'" + userno + "'}";
                clientSendMsg(msgcon);
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        public void CloseConnect()
        {
            try
            {
                if (clientSocket != null)
                    clientSocket.Close();
                if (threadServer != null && threadServer.IsAlive)
                {
                    threadServer.Abort();
                }
                clientSocket = null;
                threadServer = null;
            }
            catch (Exception ex)
            {
                WriteTxtLog(ex.Message);
            }
        }

        /// <summary>
        /// 发送消息到服务端
        /// </summary>
        /// <param name="msg"></param>
        public void clientSendMsg(string msg)
        {
            byte[] sendMsg = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(sendMsg);
            WriteTxtLog("客户端(" + GetCurrentTime() + "):" + msg + "\r\n");
        }
        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        public static void WriteTxtLog(String theContent)
        {
            try
            {
                //String theFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                //String theFilePath = Application.StartupPath +"\\MsgLog\\";
                //String theFullName = theFilePath + theFileName;

                ////判断文件夹是否存在
                //if (Directory.Exists(theFilePath) == false)//如果不存在就创建file文件夹
                //{
                //    Directory.CreateDirectory(theFilePath);
                //}

                //StreamWriter sw = null;
                ////判断文件是否存在
                //if (File.Exists(theFullName))
                //{
                //    sw = File.AppendText(theFullName);
                //}
                //else
                //{
                //    sw = File.CreateText(theFullName);//创建该文件
                //}
                ////导出传盘文件
                ////开始写入
                //sw.WriteLine(theContent);
                ////关闭流
                //sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }

}
