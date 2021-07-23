﻿using DataStore.Utils.PackUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataStore
{
    [DataContract]
    public class Package
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        private readonly PackInfo info;

        [DataMember]
        private readonly Round[] rounds;

        public int CountRounds
        {
            get { return rounds.Length; }
        }

        public Round GetRound(int id)
        {
            if (id < CountRounds)
                return rounds[id];


            throw new Exception("out of range");
        }

        public string GetInfoString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine(Name);
            sb.AppendLine("\nАвтор(ы) пака: ");
            sb.AppendLine(Authors);

            return sb.ToString();
        }

        public string Authors { get { return info.Authors; } }

        public Package(XmlElement item, Dictionary<string, string> rename, PackManager packManager)
        {
            Name = item.GetAttribute("name");

            var infoData = Utils.MyXmlUtils.GetNodeWithName(item, "info");
            if (infoData.Count > 0)
            {
                info = new PackInfo(infoData[0]);
            }
            else
            {
                info = new PackInfo("No data");
            }

            var rounds = new List<Round>();

            foreach (var roundDataXml in Utils.MyXmlUtils.GetNodeWithName(item, "rounds"))
            {
                foreach (var data in Utils.MyXmlUtils.GetNodeWithName(roundDataXml, "round"))
                {
                    rounds.Add(new Round(data, rename, packManager));
                }
            }

            this.rounds = rounds.ToArray();
        }

        public Package(string name, List<Round> rounds, PackInfo packInfo)
        {
            this.Name = name;
            this.rounds = rounds.ToArray();
            this.info = packInfo;
        }

        public void Update(string workPath)
        {
            foreach (var r in rounds)
            {
                r.Update(workPath);
            }
        }

        public string GetAllThemes()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var round in rounds)
            {
                if (!round.IsFinal)
                {
                    sb.Append(round.GetThemesName());
                }
            }

            return sb.ToString();
        }

    }
}
