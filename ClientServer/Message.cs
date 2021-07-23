using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ClientServer
{
    public class Message
    {
        public enum MessageType
        {
            None,
            Ping,
            GetReg,
            SendReg,
            GetFile,
            SendFile,
            AddToChat,
            SendUserData,
            StartGame,
            Ready,
            ChoiseQuestion,
            ShowQuestion,
            SetPause,
            TryAnswer,
            ShowAnswer,
            AdminSay,
            UpdateMoney,
            SendAdminData,
            Kick,
            ForseShowMain,
            SendUsedQuestion,
            FileSended,
            StartCanAnswer,
            FileNotExists,
            AvailableImage,
            CanChoiceUser,
            UserClickUser,
            StartAutoAnswer,
            ForceShowQuestion,
            AuctionChoice,
            AddTextToMainScreen,
            KickTheme,
            ChoiseTheme,
            FinalRate,
            FinalAnswer,
            EndGame,
        }

        public enum FileType
        {
            AdminImg,
            Pack,
            UserImg,
        }

        public MessageType Type { get; set; }

        public string Token { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public Message()
        {
            Data = new Dictionary<string, string>();
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, typeof(Message));
        }

        public Message SetToken(string token)
        {
            this.Token = token;
            return this;
        }

        public static Message FromJson(string json)
        {
            return (Message)JsonSerializer.Deserialize(json, typeof(Message));
        }

        public Message Add(string key, string value)
        {
            Data.Add(key, value);

            return this;
        }

        public Message SetType(MessageType type)
        {
            Type = type;
            return this;
        }

        public string GetData(string key)
        {
            if (Data.TryGetValue(key, out string value))
                return value;

            return "";
        }

    }
}
