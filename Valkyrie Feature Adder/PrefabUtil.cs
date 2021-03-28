using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class PrefabUtil
    {
        public const string Untested = "Untested. ";
        public const string NeedsFireStrategy = "Doesn't add a matching FireStrategy class. ";
        public const string NeedsToPairUnityPrefab = "Needs to edit the GameScene.unity (or prefab, not sure which) file to automatically connect prefab inside Unity. ";
        public const string NeedsToAddEnemyBullet = "Needs to create a matching bullet for this enemy. ";

        private static Random Rand = new Random();
        public static string NewFileId()
        {
            const int FileIdLength = 19;
            char[] FileIdDigits = new char[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < FileIdLength; i++)
            {
                int charIndex = Rand.Next(10);
                sb.Append(FileIdDigits[charIndex]);
            }

            string ret = sb.ToString();
            return ret;
        }
        public static string NewGuid() => Guid.NewGuid().ToString("N");

        public static void CopyPrefabData(NewFeature feature)
        {
            FileInfo csFileInfo = new FileInfo(feature.PathTemplateCs);

            #region Assert
            Debug.Assert(csFileInfo.Exists);
            Debug.Assert(csFileInfo.Extension == ".cs");
            Debug.Assert(Directory.Exists(feature.DirDestination));
            #endregion Assert

            Debugger.Break();

            string csMetaTemplate = feature.PathTemplateCsMeta;
            string prefabTemplate = feature.PathTemplatePrefab;
            string prefabMetaTemplate = feature.PathTemplatePrefabMeta;

            #region Assert
            Debug.Assert(File.Exists(csMetaTemplate));
            Debug.Assert(File.Exists(prefabTemplate));
            Debug.Assert(File.Exists(prefabMetaTemplate));
            #endregion Assert

            string csMetaDestination = feature.PathDestinationCsMeta;
            string prefabDestination = feature.PathDestinationPrefab;
            string prefabMetaDestination = feature.PathDestinationPrefabMeta;

            #region Assert
            Debug.Assert(!File.Exists(csMetaDestination));
            Debug.Assert(!File.Exists(prefabDestination));
            Debug.Assert(!File.Exists(prefabMetaDestination));
            #endregion Assert

            string guidCs = NewGuid();
            string guidPrefab = NewGuid();
            string fileId = NewFileId();

            string csMetaText = CsMetaContents(csMetaTemplate, guidCs);
            File.WriteAllText(csMetaDestination, csMetaText);

            string prefabText = PrefabContents(prefabTemplate, guidCs, fileId);
            File.WriteAllText(prefabDestination, prefabText);

            string prefabMetaText = PrefabMetaContents(prefabMetaTemplate, guidPrefab);
            File.WriteAllText(prefabMetaDestination, prefabMetaText);

            string poolPrefabPath = feature.PathObjectPoolPrefab;
            string featureName = feature.FeatureName;
            AppendPoolListPrefabData(poolPrefabPath, featureName, guidPrefab, fileId);

            //string fileContents = File.ReadAllText(csTemplateFilePath);
            //fileContents = fileContents.Replace(TemplateName, FeatureName);

            //File.WriteAllText(destinationPath, fileContents);
        }

        private const string TagGuidCS = "#GUIDCS#";
        private const string TagGuidPrefab = "#GUIDPREFAB#";
        private const string TagFileId = "#FILEID#";

        private static StringBuilder StringBuilderFromFile(string path)
        {
            Debug.Assert(File.Exists(path));
            return new StringBuilder(File.ReadAllText(path));
        }

        private static string CsMetaContents(string templatePath, string guidCs)
        {
            StringBuilder sb = StringBuilderFromFile(templatePath);

            sb.Replace(TagGuidCS, guidCs);

            return sb.ToString();
        }

        private static string PrefabContents(string templatePath, string guidCs, string fileId)
        {
            StringBuilder sb = StringBuilderFromFile(templatePath);

            sb.Replace(TagFileId, fileId);
            sb.Replace(TagGuidCS, guidCs);

            return sb.ToString();
        }

        private static string PrefabMetaContents(string templatePath, string guidPrefab)
        {
            StringBuilder sb = StringBuilderFromFile(templatePath);

            sb.Replace(TagGuidPrefab, guidPrefab);

            return sb.ToString();
        }

        private static void AppendPoolListPrefabData(string poolPrefabPath, string featureName, string guidPrefab, string fileId)
        {
            Debug.Assert(File.Exists(poolPrefabPath));
            string prefabToAdd = $"  {featureName}Prefab: {{fileID: {fileId}, guid: {guidPrefab},\r\n    type: 3}}\r\n";

            File.AppendAllText(poolPrefabPath, prefabToAdd);
        }






        public static void AddFireStrategyToGameSceneFireStrategyManager(NewFeature feature)
        {
            string gameScenePath = UnityPaths.PathGameScene;

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagGameSceneFireStrategyManagerStart;
            string endTag = UnityPaths.TagGameSceneFireStrategyManagerEnd;
            string className = feature.ClassName;

            Debug.Assert(File.Exists(gameScenePath));

            string[] lines = File.ReadAllLines(gameScenePath);
            int endTagLine = FileUtil.FindEndTagLineAfterStartTagLine(startTag, endTag, lines, gameScenePath);

            string newStrategyLine = $"      {featureName}: 1";

            FileUtil.InsertLineToFile(gameScenePath, lines, newStrategyLine, endTagLine);

            //List<string> allLines = new List<string>(lines.Length + 1);


            //for (int i = 0; i < insertLineNumber; i++)
            //    allLines.Add(lines[i]);

            //allLines.Add(newStrategyLine);

            //for (int i = insertLineNumber; i < lines.Length; i++)
            //    allLines.Add(lines[i]);

            //File.WriteAllLines(gameScenePath, allLines);
        }
    }
}
