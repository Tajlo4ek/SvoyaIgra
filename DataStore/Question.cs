using DataStore.Utils.PackUtils;
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
    public class Question
    {
        public enum QuestionType
        {
            Normal,
            Bagcat,
            Auction,
        }

        [DataMember]
        private readonly Scenario[] scenarios;

        public int CountScenarios { get { return scenarios.Length; } }

        public int CountAnswer { get { return answers.Length; } }

        [DataMember]
        private readonly Scenario[] answers;

        public string StrAnswer { get; private set; }

        [DataMember]
        public int Cost { get; private set; }

        public bool IsBagcat { get { return questionType == QuestionType.Bagcat; } }

        public bool IsAuction { get { return questionType == QuestionType.Auction; } }

        public bool IsNormal { get { return questionType == QuestionType.Normal; } }

        [DataMember]
        public string ThemeName { get; private set; }

        [DataMember]
        public int SpecialCost { get; private set; }

        [DataMember]
        private readonly QuestionType questionType;

        public bool IsUsed { get; private set; }

        public Scenario GetScenario(int id)
        {
            if (id < CountScenarios)
                return scenarios[id];

            throw new Exception("out of range");
        }

        public Scenario GetAnswer(int id)
        {
            if (id < CountAnswer)
                return answers[id];

            throw new Exception("out of range");
        }

        public Question(XmlElement item, Dictionary<string, string> rename, PackManager packManager)
        {
            Cost = int.Parse(item.GetAttribute("price"));
            questionType = QuestionType.Normal;
            ThemeName = "";

            var scenarios = new List<Scenario>();
            var answer = new List<Scenario>();

            StringBuilder sb = new StringBuilder();

            foreach (var right in Utils.MyXmlUtils.GetNodeWithName(item, "right"))
            {
                foreach (var anwer in Utils.MyXmlUtils.GetNodeWithName(right, "answer"))
                {
                    sb.AppendLine(anwer.InnerText);
                }
            }

            foreach (var scenariosXml in Utils.MyXmlUtils.GetNodeWithName(item, "scenario"))
            {
                foreach (var scenario in Utils.MyXmlUtils.GetNodeWithName(scenariosXml, "atom"))
                {
                    scenarios.Add(new Scenario(scenario, rename, packManager));
                }
            }

            foreach (var typeXml in Utils.MyXmlUtils.GetNodeWithName(item, "type"))
            {
                var name = typeXml.GetAttribute("name");
                if (name.Equals("cat") || name.Equals("bagcat"))
                {
                    string theme = "";
                    int cost = 0;
                    questionType = QuestionType.Bagcat;

                    foreach (var param in Utils.MyXmlUtils.GetNodeWithName(typeXml, "param"))
                    {
                        var paramName = param.GetAttribute("name");
                        if (paramName.Equals("theme"))
                        {
                            theme = param.InnerText;
                        }
                        else if (paramName.Equals("cost"))
                        {
                            cost = int.Parse(param.InnerText);
                        }
                    }
                    ThemeName = theme;
                    SpecialCost = cost;
                }
                else if (name.Equals("auction"))
                {
                    questionType = QuestionType.Auction;
                }
            }

            int markerId = -1;
            for (int scId = 0; scId < scenarios.Count; scId++)
            {
                if (scenarios[scId].Type == Scenario.ScenarioType.Marker)
                {
                    markerId = scId;
                    break;
                }
            }

            if (markerId != -1)
            {
                for (int scId = markerId + 1; scId < scenarios.Count; scId++)
                {
                    answer.Add(scenarios[scId]);
                }

                for (int scId = scenarios.Count - 1; scId >= markerId; scId--)
                {
                    scenarios.RemoveAt(scId);
                }
            }

            answer.Add(new Scenario(sb.ToString(), Scenario.ScenarioType.Text));

            this.scenarios = scenarios.ToArray();
            this.answers = answer.ToArray();
        }

        public Question(List<Scenario> qScenario, List<Scenario> aScenario, int cost, QuestionType questionType)
        {
            this.scenarios = qScenario.ToArray();
            this.answers = aScenario.ToArray();
            this.Cost = cost;
            this.questionType = questionType;
        }

        public Question(List<Scenario> qScenario, List<Scenario> aScenario, int cost, QuestionType questionType, string themeName, int specialCost) :
            this(qScenario, aScenario, cost, questionType)
        {
            this.ThemeName = themeName;
            this.SpecialCost = specialCost;
        }

        public void Update(string workPath, string themeName)
        {
            foreach (var s in scenarios)
            {
                s.Update(workPath);
            }

            foreach (var s in answers)
            {
                s.Update(workPath);
            }

            if (!IsBagcat || ThemeName.Equals(""))
            {
                ThemeName = themeName;
            }

            var find = answers.First((x) => x.Type == Scenario.ScenarioType.Text);

            StrAnswer = find != null ? find.Data : "no data";
        }

        public void SetEnd()
        {
            IsUsed = true;
        }

    }
}
