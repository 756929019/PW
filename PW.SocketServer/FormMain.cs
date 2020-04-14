using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace PW.SocketServer
{
    public partial class FormMain : Form
    {
        MyServer myServer = null;
      
        public FormMain()
        {
            myServer = MyServer.GetInstance();
            myServer.ClientAdd = new MyServer.ClientChangeHandler(myServer_ClientAdd);
            myServer.ClientRemove = new MyServer.ClientChangeHandler(myServer_ClientRemove);
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
        }

        void myServer_ClientAdd(ClientUser client, Dictionary<String, ClientUser> clientList)
        {
            LoadGrid();
        }

        void myServer_ClientRemove(ClientUser client, Dictionary<String, ClientUser> clientList)
        {
            LoadGrid();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }


        private void LoadGrid()
        {
            try
            {
                dataGridView1.Rows.Clear();
                Dictionary<String, ClientUser> tmps = myServer.getClient();
                foreach (ClientUser cu in tmps.Values)
                {
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["UserNo"].Value = cu.UserNo;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["UserName"].Value = cu.UserName;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["CliIp"].Value = cu.CliIp;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["CliPort"].Value = cu.CliPort;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["LoginTime"].Value = cu.LoginTime;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                string msg = "{title:'" + txtTitle.Text + "',content:'" + txtContent.Text + "'}";
                Dictionary<String, ClientUser> tmps = myServer.getClient();
                try
                {
                    
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (tmps.ContainsKey(row.Cells["UserNo"].Value.ToString()))
                        {
                            myServer.serverSendMsg(tmps[row.Cells["UserNo"].Value.ToString()].ClientSocket, msg);
                        }
                    }
                }
                catch
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        var query = tmps.SingleOrDefault(p => p.Value.CliIp == row.Cells["CliIp"].Value.ToString());
                        if (query.Value != null)
                        {
                            myServer.serverSendMsg(query.Value.ClientSocket, msg);
                        }
                    }
                }
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
    }
}
