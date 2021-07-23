using DataStore.Utils.PackUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Xml;

namespace DataStore
{
    [DataContract]
    public class Scenario
    {
        public enum ScenarioType
        {
            Text,
            Audio,
            Video,
            Image,
            Marker,
        }

        [DataMember]
        public ScenarioType Type { get; private set; }

        public bool IsMedia { get { return Type != ScenarioType.Text && Type != ScenarioType.Marker; } }

        [DataMember]
        public int Time { get; private set; }

        [DataMember]
        public string Data { get; private set; }

        public Scenario(string data, ScenarioType type, int time = -1)
        {
            if (type == ScenarioType.Marker)
                throw new Exception("type is marker");

            Data = data;
            Type = type;
            Time = time;
        }

        public Scenario(XmlElement item, Dictionary<string, string> rename, PackManager packManager)
        {
            switch (item.GetAttribute("type"))
            {
                case "image":
                    Type = ScenarioType.Image;
                    break;

                case "video":
                    Type = ScenarioType.Video;
                    break;

                case "voice":
                    Type = ScenarioType.Audio;
                    break;

                case "marker":
                    Type = ScenarioType.Marker;
                    break;

                default:
                    Type = ScenarioType.Text;
                    break;
            }

            int.TryParse(item.GetAttribute("time"), out int delay);
            Time = delay == 0 ? -1 : delay;

            Data = item.InnerText;
            if (Data.StartsWith("@"))
            {
                Data = "@" + rename[Uri.EscapeUriString(Data.Substring(1))];
            }

            if (Type != ScenarioType.Text && Type != ScenarioType.Marker)
            {
                Data = packManager.AddToPackFolder(Data);
            }
        }

        public void Update(string workPath)
        {
            if (Type != ScenarioType.Text && Type != ScenarioType.Marker)
            {
                Data = workPath + @"\" + Data;
            }
        }

        public void UpdataData(string data)
        {
            Data = data;
        }

    }
}
