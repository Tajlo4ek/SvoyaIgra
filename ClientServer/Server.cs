using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServer
{
    public class Server
    {
        private const int tokenSize = 20;

        public static string ServerToken = "server";

        private readonly Socket sListener;
        private readonly IPEndPoint ipEndPoint;

        private readonly StreamWriter outStream;

        private string logPath;
        private string workPath;

        private Action<Message> onGetMessage;
        public Action<Message.FileType, string> OnFileLoaded;

        private readonly Action<Exception, string> onErrorAction;

        public delegate string GetMessageDelegate(string token, out Message.MessageType type);
        private readonly GetMessageDelegate getMessageForUser;

        Thread workThread;

        public delegate string GetFileNameDelegate(Message.FileType type);
        public GetFileNameDelegate GetFileName;

        public delegate string GetFilePathDelegate(string name);
        public GetFilePathDelegate GetFilePath;

        public Server(string ip, GetMessageDelegate getMessage, Action<Exception, string> onError)
        {
            var ipAddr = IPAddress.Parse(ip);
            ipEndPoint = new IPEndPoint(ipAddr, Utils.port);

            sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            outStream = new StreamWriter("server.log");

            getMessageForUser += getMessage;
            onErrorAction += onError;
        }

        public void SetLogPath(string path)
        {
            logPath = path;
        }

        public void SetWorkPath(string path)
        {
            workPath = path;
        }

        public void Start()
        {
            workThread = new Thread(Work);
            workThread.Start();
        }

        public void Stop()
        {
            sListener.Close();
            workThread.Abort();
            outStream.Flush();
            outStream.Close();
        }

        public void AddActionGetMessage(Action<Message> action)
        {
            onGetMessage += action;
        }

        private string CheckMessage(Message message, Socket socket, out Message.MessageType type, out string sendFilePath)
        {
            sendFilePath = "";

            if (message.Type == Message.MessageType.GetReg)
            {
                var token = TokenGenerator.Generate(tokenSize);

                var ans = new Message()
                    .SetToken(ServerToken)
                    .SetType(Message.MessageType.SendReg)
                    .Add("token", token);

                message.Token = token;
                onGetMessage(message);
                type = ans.Type;
                return ans.GetJson();
            }
            else if (message.Type == Message.MessageType.GetFile)
            {
                var fileType = (Message.FileType)Enum.Parse(typeof(Message.FileType), message.GetData("fileType"));

                string fileName = "";
                string filePath = "";

                if (fileType == Message.FileType.UserImg)
                {
                    fileName = message.GetData("fileName");
                }
                else
                {
                    fileName = GetFileName(fileType);
                }

                filePath = GetFilePath(fileName);

                if (File.Exists(filePath))
                {
                    var length = Utils.GetFileSize(filePath);
                    onGetMessage(message);

                    type = Message.MessageType.SendFile;

                    var ans = new Message()
                        .SetToken(ServerToken)
                        .SetType(type)
                        .Add("totalSize", length.ToString())
                        .Add("fileName", fileName)
                        .Add("fileType", fileType.ToString());

                    sendFilePath = filePath;

                    return ans.GetJson();
                }
                else
                {
                    type = Message.MessageType.FileNotExists;

                    var ans = new Message()
                        .SetToken(ServerToken)
                        .SetType(type);

                    return ans.GetJson();
                }
            }
            else if (message.Type == Message.MessageType.SendFile)
            {
                onGetMessage(message);

                var path = workPath + @"\" + message.GetData("fileName");

                Utils.ReceiveFile(
                    path,
                    long.Parse(message.GetData("totalSize")),
                    socket,
                    null);

                OnFileLoaded((Message.FileType)Enum.Parse(typeof(Message.FileType), message.GetData("fileType")), path);

                onGetMessage(message.SetType(Message.MessageType.FileSended));

                type = Message.MessageType.FileSended;
                var ans = new Message()
                        .SetToken(ServerToken)
                        .SetType(type);

                return ans.GetJson();
            }
            else
            {
                onGetMessage(message);
                return getMessageForUser(message.Token, out type);
            }
        }

        private void WorkWithConnect(Socket handler)
        {
            string token = "";

            while (true)
            {
                try
                {

                    var bytes = Utils.GetPackage(handler);
                    string data = Encoding.UTF32.GetString(bytes);
                    var messageFrom = Message.FromJson(data);


                    var reply = CheckMessage(messageFrom, handler, out Message.MessageType type, out string sendFilePath);

                    Utils.SendPackage(handler, reply);

                    if (token.Length == 0)
                    {
                        token = messageFrom.Token;
                    }

                    if (type == Message.MessageType.SendFile)
                    {
                        Utils.SendFile(sendFilePath, handler);
                        onGetMessage(messageFrom.SetType(Message.MessageType.FileSended));
                    }

                    if (type == Message.MessageType.FileSended)
                    {
                        Thread.Sleep(250);
                    }

                }
                catch (Exception ex)
                {

                    handler.Shutdown(SocketShutdown.Both);

                    using (StreamWriter sw = new StreamWriter(logPath + @"/logs/server/" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".log", true))
                    {
                        sw.WriteLine(ex.ToString());
                        sw.WriteLine(ex.StackTrace);
                        sw.WriteLine("\n\n");
                    }

                    Log("error " + ex.ToString());
                    onErrorAction(ex, token);

                    break;
                }
            }
        }

        private void Work()
        {
            sListener.Bind(ipEndPoint);
            sListener.Listen(100);

            while (true)
            {
                Socket handler = sListener.Accept();
                new Task(() => WorkWithConnect(handler)).Start();
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
