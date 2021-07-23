using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace DataStore.Utils.PackUtils
{
    public static class PackConverter
    {
        private const string BufPath = @"\buf";

        public static void Convert(string packPath, PackManager packManager, string packName, Action<string> process = null)
        {
            var bufPath = packManager.WorkDirectory + BufPath;

            CreateDirectory(bufPath);

            Dictionary<string, string> rename = new Dictionary<string, string>();

            process?.Invoke("start unzip");

            using (ZipArchive zipArchive = ZipFile.OpenRead(packPath))
            {
                var entries = zipArchive.Entries;
                int countMax = zipArchive.Entries.Count;
                int count = 1;

                foreach (ZipArchiveEntry entry in entries)
                {
                    process?.Invoke(string.Format("unzip {0} / {1}", count, countMax));

                    var newName = count.ToString();

                    int ind = entry.FullName.LastIndexOf('.');
                    if (ind != -1)
                    {
                        newName += entry.FullName.Substring(ind);
                    }

                    if (!rename.ContainsKey(entry.Name))
                    {
                        rename.Add(entry.Name, bufPath + @"\" + newName);
                        entry.ExtractToFile(bufPath + @"\" + newName);
                        count++;
                    }
                }
            }

            process?.Invoke("end unzip");

            File.Move(rename["content.xml"], bufPath + @"\content.xml");

            var doc = new XmlDocument();
            doc.Load(bufPath + @"\content.xml");
            var root = doc.DocumentElement;

            process?.Invoke("start parse content");

            var package = new Package(root, rename, packManager);

            process?.Invoke("end parse content");

            process?.Invoke("start create zip");

            FileStream writer = new FileStream(packManager.WorkDirectory + @"\content.xml", FileMode.Create);
            DataContractSerializer ser = new DataContractSerializer(typeof(Package));
            ser.WriteObject(writer, package);
            writer.Close();

            DeleteDirectory(bufPath);

            var bufManager = new PackManager();
            var buf = bufManager.WorkDirectory;

            ZipFile.CreateFromDirectory(packManager.WorkDirectory, buf + @"\" + packName);

            process?.Invoke("zip created. finishing...");

            RecreateDirectory(packManager.WorkDirectory);

            File.Copy(buf + @"\" + packName, packManager.WorkDirectory + @"\" + packName);

            DeleteDirectory(buf);
        }

        public static void SavePack(Package package, string path, Action<string> process = null)
        {
            var packManager = new PackManager();
            var bufDir = packManager.WorkDirectory;

            int countRound = package.CountRounds;
            for (int roundId = 0; roundId < countRound; roundId++)
            {
                var roundString = string.Format("Раунд {0} / {1} \n", roundId + 1, countRound);
                var round = package.GetRound(roundId);
                int themeCount = round.CountThemes;
                for (int themeId = 0; themeId < themeCount; themeId++)
                {
                    var themeString = string.Format("{0} Тема {1} / {2} \n", roundString, themeId + 1, themeCount);
                    var theme = round.GetTheme(themeId);
                    var countQuestion = theme.CountQuestions;
                    for (int questionId = 0; questionId < countQuestion; questionId++)
                    {
                        process?.Invoke(string.Format("{0} Вопрос {1} / {2} \n", themeString, questionId + 1, countQuestion));
                        var question = theme.GetQuestion(questionId);
                        for (int scenarioId = 0; scenarioId < question.CountScenarios; scenarioId++)
                        {
                            var scenario = question.GetScenario(scenarioId);
                            if (scenario.IsMedia)
                            {
                                scenario.UpdataData(packManager.AddToPackFolder(scenario.Data));
                            }
                        }

                        for (int scenarioId = 0; scenarioId < question.CountAnswer; scenarioId++)
                        {
                            var scenario = question.GetAnswer(scenarioId);
                            if (scenario.IsMedia)
                            {
                                scenario.UpdataData(packManager.AddToPackFolder(scenario.Data));
                            }
                        }
                    }
                }
            }

            using (FileStream writer = new FileStream(bufDir + @"\content.xml", FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Package));
                ser.WriteObject(writer, package);
            }


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Create))
            {
                var files = Directory.EnumerateFiles(bufDir);
                int total = files.Count();
                int count = 1;
                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);
                    archive.CreateEntryFromFile(file, info.Name);
                    process?.Invoke(string.Format("сохранение медиафайлов \n {0} / {1}", count++, total));
                }
            }

            packManager.Dispose();
        }

        private static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            foreach (var dir in Directory.EnumerateDirectories(path))
            {
                DeleteDirectory(dir);
            }

            foreach (var file in Directory.EnumerateFiles(path))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            Directory.Delete(path, true);
        }

        public static void RecreateDirectory(string path)
        {
            DeleteDirectory(path);
            CreateDirectory(path);
        }

    }
}
