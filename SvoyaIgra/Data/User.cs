using System;
using System.Collections.Generic;
using DataStore;

namespace SvoyaIgra.Data
{
    public class User
    {
        private class MessageData
        {
            public readonly string Data;
            public readonly ClientServer.Message.MessageType Type;

            public MessageData(string data, ClientServer.Message.MessageType type)
            {
                this.Data = data;
                this.Type = type;
            }

        }

        public const string MyImgName = "myImg";
        public const string AdminImgName = "adminImg";

        public bool CanAnswer { get; private set; }

        private const int maxNotActiveTime = 10000;

        private const int timeToLoadFile = 5 * 60 * 1000;

        public readonly string Name;

        public int Money { get; set; }

        public readonly string Token;

        private readonly Queue<MessageData> messages;

        private readonly string pingMessage;

        private bool isLoadFile;

        public string FinalAns { get; private set; }

        public bool IsImgAvailable { get; private set; }

        private DateTime lastUpdateTime = DateTime.Now;
        private DateTime startLoadFileTIme;

        public int Rate { get; private set; }

        public bool IsPass { get { return Rate == -1; } }

        public bool IsAllIn { get { return Rate == Money && Rate > 0; } }

        public bool IsActive
        {
            get
            {
                if (isLoadFile)
                {
                    return (DateTime.Now - startLoadFileTIme).TotalMilliseconds < timeToLoadFile;
                }
                else
                {
                    return (DateTime.Now - lastUpdateTime).TotalMilliseconds < maxNotActiveTime;
                }
            }
        }

        public bool IsReady { get; private set; }


        public User(string Token, string name, bool isServer)
        {
            this.Name = name;
            this.Token = Token;
            this.Money = 0;
            messages = new Queue<MessageData>();

            IsImgAvailable = false;
            CanAnswer = true;

            if (!isServer)
            {
                var message = new ClientServer.Message()
                   .SetToken(Token)
                   .SetType(ClientServer.Message.MessageType.GetFile)
                   .Add("fileName", DataStore.Utils.PackUtils.PackManager.BasePackName)
                   .Add("fileType", ClientServer.Message.FileType.Pack.ToString());

                AddDataToSend(message);

                message = new ClientServer.Message()
                  .SetToken(Token)
                  .SetType(ClientServer.Message.MessageType.GetFile)
                  .Add("fileName", AdminImgName)
                  .Add("fileType", ClientServer.Message.FileType.AdminImg.ToString());

                AddDataToSend(message);
            }

            pingMessage = new ClientServer.Message()
                   .SetToken(Token)
                   .SetType(ClientServer.Message.MessageType.Ping)
                   .GetJson();

            IsReady = false;
            Rate = -1;
        }

        public void AddDataToSend(ClientServer.Message message)
        {
            var newMessage = new MessageData(message.GetJson(), message.Type);

            lock (messages)
            {
                messages.Enqueue(newMessage);
            }
        }

        public string GetMessage(out ClientServer.Message.MessageType type)
        {
            if (messages.Count > 0)
            {
                var message = messages.Dequeue();
                type = message.Type;
                return message.Data;
            }

            type = ClientServer.Message.MessageType.Ping;
            return pingMessage;
        }

        public void SetReady()
        {
            IsReady = true;
        }

        public void Update()
        {
            lastUpdateTime = DateTime.Now;
        }

        public void StartLoadFile()
        {
            startLoadFileTIme = DateTime.Now;
            isLoadFile = true;
        }

        public void FileLoaded()
        {
            isLoadFile = false;
            lastUpdateTime = DateTime.Now;
        }

        public void SetCanAnswer(bool can)
        {
            CanAnswer = can;
        }

        public void SetImgAvailable(bool isAvailable)
        {
            IsImgAvailable = isAvailable;
        }

        public void SetRate(int value)
        {
            if (value == -1)
            {
                Console.WriteLine(Token + " pass");
            }

            Rate = value;
        }

        public void SetFinalAns(string data)
        {
            FinalAns = data;
        }

    }
}
