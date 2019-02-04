﻿using Flexlive.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Color_yr.Minecraft_QQ
{
    public class socket
    {
        public static Dictionary<string, Socket> clients = new Dictionary<string, Socket>();
        public static string MCserver = null;
        public Print print;                     // 运行时的信息输出方法
        public delegate void Print(string info);
        private static Socket serverSocket;
        private static byte[] read = new byte[4096];
        public static bool start;
        public static bool ready = false;
        private static Thread server_thread;
        private static Thread read_thread;

        public void Start_socket()
        {
            try
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]正在启动端口");
                logs.Log_write("[INFO][Socket]正在启动端口");
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, Minecraft_QQ.Port));
                serverSocket.Listen(5);

                server_thread = new Thread(listenClientConnect);
                server_thread.Start(serverSocket);
                start = true;
                ready = false;
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]端口已启动");
                logs.Log_write("[INFO][Socket]端口已启动");
            }
            catch (Exception exception)
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]启动失败，请看日志");
                logs.Log_write("[ERROR][Socket]端口启动失败\n" + exception.Message);
                start = false;
                ready = false;
            }
        }
        private static void listenClientConnect(object obj)
        {
            Socket socket = (Socket)obj;
            try
            {
                while (true)
                {
                    Socket clientScoket = socket.Accept();

                    if (read_thread != null)
                    {
                        read_thread.Abort();
                        read_thread.DisableComObjectEagerCleanup();
                        read_thread = null;
                    }
                    read_thread = new Thread(receiveData);
                    read_thread.Start(clientScoket);                   // 在新的线程中接收客户端信息

                    GC.Collect();
                    CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]服务器已连接");
                    logs.Log_write("[INFO][Socket]服务器已连接");
                    ready = true;

                    Thread.Sleep(1000);                            // 延时1秒后，接收连接请求
                    if (!start) return;
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }
        private static string Receive(Socket socket)
        {
            string data = "";

            byte[] bytes = null;
            int len = socket.Available;
            if (len > 0)
            {
                bytes = new byte[len];
                int receiveNumber = socket.Receive(bytes);

                if (XML.read(config_read.config, "编码") == "UTF-8")
                {
                    data = Encoding.UTF8.GetString(bytes, 0, receiveNumber);
                }
                if (XML.read(config_read.config, "编码") == "ANSI（GBK）")
                {
                    data = Encoding.Default.GetString(bytes, 0, receiveNumber);
                }
            }
            return data;
        }

        private static void receiveData(object obj)
        {
            try
            {
                Socket socket = (Socket)obj;

                string clientIp = socket.RemoteEndPoint.ToString();                 // 获取客户端标识 ip和端口
                if (!clients.ContainsKey(clientIp)) clients.Add(clientIp, socket);  // 将连接的客户端socket添加到clients中保存
                else clients[clientIp] = socket;
                MCserver = clientIp;

                while (true)
                {
                    try
                    {
                        string str = Receive(socket);
                        if (!str.Equals(""))
                        {
                            message(str);
                        }
                    }
                    catch (Exception)
                    {
                        CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]连接已断开-连接丢失");
                        logs.Log_write("[INFO][Socket]连接已断开-连接丢失");
                        ready = false;

                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        socket = null;

                        clients.Clear();
                        MCserver = null;

                        GC.Collect();
                        return;
                    }

                    if (!start)
                    {
                        CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "线程已关闭");
                        return;
                    }
                    Thread.Sleep(100);      // 延时0.1秒后再接收客户端发送的消息
                }
            }
            catch (ThreadAbortException)
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]连接已断开-主动断开");
                logs.Log_write("[INFO][Socket]连接已断开-主动断开");
                return;
            }
        }
        public static void Send(string info, string id)
        {
            if (clients.ContainsKey(id))
            {
                Socket socket = clients[id];
                try
                {
                    Send(socket, info);
                }
                catch (Exception)
                {
                    clients.Clear();
                    MCserver = null;

                    GC.Collect();
                    CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]连接已断开，无法发送");
                    ready = false;
                }
            }
            else
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, "[Minecraft_QQ]服务器未连接，无法发送");
            }
        }
        private static void Send(Socket socket, string data)
        {
            if (socket != null && data != null && !data.Equals(""))
            {
                byte[] bytes = null;
                if (XML.read(config_read.config, "编码") == "UTF-8")
                {
                    bytes = Encoding.UTF8.GetBytes(data);
                }
                if (XML.read(config_read.config, "编码") == "ANSI（GBK）")
                {
                    bytes = Encoding.Default.GetBytes(data);
                }
                socket.Send(bytes);                
            }
        }
        private static void message(string read)
        {
            if (read.IndexOf("[群消息]") == 0)
            {
                var sb = new StringBuilder(read);
                sb.Replace("[群消息]", string.Empty);
                string x = sb.ToString();
                string z = use.get_string(x, "(", ")");
                if (XML.read(config_read.mute, z) != "true")
                {
                    x = x.Replace("(" + z + ")", "");
                    x = use.code_CQ(x);
                    CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, x);
                    if (Minecraft_QQ.GroupSet2 != 0 && Minecraft_QQ.Group2_on == true)
                    {
                        CQ.SendGroupMessage(Minecraft_QQ.GroupSet2, x);
                    }
                    if (Minecraft_QQ.GroupSet3 != 0 && Minecraft_QQ.Group3_on == true)
                    {
                        CQ.SendGroupMessage(Minecraft_QQ.GroupSet3, x);
                    }
                }
                Minecraft_QQ.Group = 0;
                return;
            }
            else if (Minecraft_QQ.Group == 1)
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet1, read);
                Minecraft_QQ.Group = 0;
                return;
            }
            else if (Minecraft_QQ.Group == 2)
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet2, read);
                Minecraft_QQ.Group = 0;
                return;
            }
            else if (Minecraft_QQ.Group == 3)
            {
                CQ.SendGroupMessage(Minecraft_QQ.GroupSet3, read);
                Minecraft_QQ.Group = 0;
                return;
            }
        }
    }
}
