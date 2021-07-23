using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServer
{
    public class Client
    {
        static int id = 0;

        private readonly IPAddress ipAddr;
        private readonly IPEndPoint ipEndPoint;

        public string Token { get; private set; }
        private readonly string name;

        private string workPath;
        private string logPath;

        private readonly StreamWriter outStream;

        public Action<Message.FileType, string> OnFileLoaded;

        public Action<Message> OnGetMessage;

        public delegate string GetMessageDelegate(string token, out Message.MessageType type);
        public GetMessageDelegate GetMessageForServer;

        private readonly Action<Exception> onErrorAction;

        public Action<string> OnFileLoadProcess;

        Thread workThread;

        public delegate string GetFilePathDelegate(string name);
        public GetFilePathDelegate GetFilePath;

        private bool needStop = false;

        public Client(string ip, string name, Action<Exception> onError)
        {
            ipAddr = IPAddress.Parse(ip);
            ipEndPoint = new IPEndPoint(ipAddr, Utils.port);

            this.name = name;
            this.onErrorAction += onError;
            Token = "";

            outStream = new StreamWriter("client" + id + ".log");
            id++;
        }

        public void Start()
        {
            needStop = false;
            workThread = new Thread(Work);
            workThread.Start();
        }

        public void Stop()
        {
            needStop = true;
            if (!(outStream.BaseStream == null))
            {
                outStream.Flush();
                outStream.Close();
            }
        }

        public void SetWorkPath(string path)
        {
            workPath = path;
        }

        public void SetLogPath(string path)
        {
            logPath = path;
        }

        public void Work()
        {
            try
            {
                while (true)
                {
                    if (Token == "")
                    {
                        var type = Message.MessageType.GetReg;
                        var message = new Message()
                            .SetToken(Token)
                            .SetType(type)
                            .Add("name", name);
                        SendMessage(message.GetJson(), type);
                    }
                    else
                    {
                        Thread.Sleep(5);
                        SendMessage(GetMessageForServer(Token, out Message.MessageType type), type);
                    }

                    if (needStop)
                    {
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter(logPath + @"\logs\client\" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".log", true))
                {
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine(ex.StackTrace);
                    sw.WriteLine("\n\n");
                }

                onErrorAction(ex);
                workThread.Abort();
            }
        }

        private void OnEnd(Message message, Socket socket)
        {
            try
            {
                if (message.Type == Message.MessageType.SendReg)
                {
                    Token = message.GetData("token");

                    var user = new Message()
                        .SetToken(Server.ServerToken)
                        .SetType(Message.MessageType.SendUserData)
                        .Add("token", Token)
                        .Add("name", name);

                    OnGetMessage(user);
                    OnGetMessage(message);
                }
                else if (message.Type == Message.MessageType.SendFile)
                {
                    string fileName = message.GetData("fileName");

                    var path = workPath + @"\" + fileName;

                    OnFileLoadProcess?.Invoke(fileName);

                    Utils.ReceiveFile(
                        path,
                        long.Parse(message.GetData("totalSize")),
                        socket,
                        OnFileLoadProcess);

                    OnFileLoaded((Message.FileType)Enum.Parse(typeof(Message.FileType), message.GetData("fileType")), path);
                }
                else
                {
                    OnGetMessage(message);
                }

                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
                onErrorAction(ex);
            }
            finally
            {
                socket.Close();
            }
        }

        private void SendMessage(string messageData, Message.MessageType type)
        {

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);


            if (type == Message.MessageType.SendFile)
            {
                var message = Message.FromJson(messageData);

                var filePath = GetFilePath(message.GetData("fileName"));

                if (File.Exists(filePath))
                {
                    message.Add("totalSize", Utils.GetFileSize(filePath).ToString());
                    Utils.SendPackage(sender, message.GetJson());
                    Utils.SendFile(filePath, sender);
                }
                else
                {
                    var ans = new Message()
                        .SetToken(this.Token)
                        .SetType(Message.MessageType.FileNotExists);

                    Utils.SendPackage(sender, ans.GetJson());
                }
            }
            else
            {
                Utils.SendPackage(sender, messageData);
            }


            var bytes = Utils.GetPackage(sender);
            var data = Encoding.UTF32.GetString(bytes);

            var messageFrom = Message.FromJson(data);

            OnEnd(messageFrom, sender);

            if (type != Message.MessageType.Ping)
            {
                Log("send " + messageData);
            }

            if (messageFrom.Type != Message.MessageType.Ping)
            {
                Log("recive " + data);
            }

        }

        private void Log(string data)
        {
            new Task(new Action(() =>
            {
                lock (outStream)
                {
                    if (outStream.BaseStream != null)
                    {
                        outStream.Write((int)(DateTime.Now - Utils.startTime).TotalMilliseconds);
                        outStream.Write(" ");
                        outStream.WriteLine(data);
                        outStream.Flush();
                    }
                }
            })).Start();
        }

    }
}


