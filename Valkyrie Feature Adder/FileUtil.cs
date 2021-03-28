using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class FileUtil
    {
        public const string Untested = "Untested. ";
        public const string NeedsFireStrategy = "Doesn't add a matching FireStrategy class. ";
        public const string NeedsToPairUnityPrefab = "Needs to edit the GameScene.unity (or prefab, not sure which) file to automatically connect prefab inside Unity. ";
        public const string NeedsToAddEnemyBullet = "Needs to create a matching bullet for this enemy. ";
        public const string NeedsToCreateFireStrategyBalance = "Fire strategies need an entry in PlayerFireStrategyManager.PlayerRatio. ";

        //public static string LastNewFeatureCsPath { get; private set; }

        public static void CopyNewFeatureCsFile(NewFeature feature)
        {
            const string TemplateName = "Basic";

            string templateFilePath = feature.PathTemplateCs;
            string destinationDirectory = feature.DirDestination;
            string destinationPath = feature.PathDestinationCs;
            string featureName = feature.FeatureName;

            FileInfo fileInfo = new FileInfo(templateFilePath);
            Debug.Assert(fileInfo.Exists);

            Debug.Assert(fileInfo.Extension == ".cs");

            //LastNewFeatureCsPath = destinationDirectory + fileInfo.Name;
            Debugger.Break();

            Debug.Assert(Directory.Exists(destinationDirectory));
            Debug.Assert(!File.Exists(destinationPath));

            string fileContents = File.ReadAllText(templateFilePath);
            fileContents = fileContents.Replace(TemplateName, featureName);

            File.WriteAllText(destinationPath, fileContents);

            AddCsFileToProjectCompile(feature);
        }

        private static void AppendPrefabVariableToPoolListCs(NewFeature feature)
        {
            string filePath = feature.PathObjectPoolCs;
            string endTag = feature.TagPrefab;
            string featureName = feature.FeatureName;
            string className = feature.ClassName;

            Debug.Assert(File.Exists(filePath));

            string[] lines = File.ReadAllLines(filePath);
            int endTagLine = FindEndTagLine(endTag, lines, filePath);

            // Assert coding style - one line of blank space
            // between last prefab and end tag.
            string _blankLine = lines[endTagLine];
            string _lastPrefabLine = lines[endTagLine - 1];
            Debug.Assert(String.IsNullOrWhiteSpace(_blankLine));
            Debug.Assert(!String.IsNullOrWhiteSpace(_lastPrefabLine));

            const string SerializeTag = "        [SerializeField]";
            const string Private = "        private";

            string variableName = $"{featureName}Prefab";

            string variableLine = $"{Private} {className} {variableName} = null;";

            List<string> allLines = new List<string>(lines.Length + 2);

            int insertLineNumber = endTagLine - 2;

            for (int i = 0; i < insertLineNumber; i++)
                allLines.Add(lines[i]);

            allLines.Add(SerializeTag);
            allLines.Add(variableLine);

            for (int i = insertLineNumber; i < lines.Length; i++)
                allLines.Add(lines[i]);

            File.WriteAllLines(filePath, allLines);
        }

        private static int FindEndTagLine(string endTag, string[] lines, string filePath)
        {
            int endTagLine = 0;
            bool endTagLineFound = false;
            while (!endTagLineFound)
            {
                if (endTagLine == lines.Length)
                    Debug.Fail($"End tag {endTag} was not found in file {filePath}");

                string line = lines[endTagLine];

                if (line.EndsWith(endTag))
                    endTagLineFound = true;

                endTagLine++;
            }

            return endTagLine;
        }


        public static void AddCsFileToProjectCompile(NewFeature feature)
        {
            string filePath = feature.PathDestinationCs;
            string filePathTrimmed = filePath.Replace(UnityPaths.DirProject, "");

            Debug.Assert(File.Exists(filePath));
            Debug.Assert(filePathTrimmed.StartsWith("Assets\\"));
            Debug.Assert(filePathTrimmed.EndsWith(".cs"));

            string[] lines = File.ReadAllLines(UnityPaths.PathCsproj);
            int endTagLine = FindCsprojCompilationItemGroupLine(lines);

            const string CompileTagStart = "    <Compile Include=\"";
            const string CompileTagEnd = "\" />";

            string compileTag = $"{CompileTagStart}{filePathTrimmed}{CompileTagEnd}";

            List<string> allLines = new List<string>(lines.Length + 1);

            int insertLineNumber = endTagLine - 1;

            for (int i = 0; i < insertLineNumber; i++)
                allLines.Add(lines[i]);

            allLines.Add(compileTag);

            for (int i = insertLineNumber; i < lines.Length; i++)
                allLines.Add(lines[i]);

            File.WriteAllLines(UnityPaths.PathCsproj, allLines);
        }

        private static int FindCsprojCompilationItemGroupLine(string[] lines)
        {
            const string EndTag = "<Compile Include=\"";

            int endTagLine = 0;
            bool endTagLineFound = false;
            while (!endTagLineFound)
            {
                if (endTagLine == lines.Length)
                    Debug.Fail("Compilation group not found in .csjproj!");

                string line = lines[endTagLine];

                if (line.Contains(EndTag))
                    endTagLineFound = true;

                endTagLine++;
            }

            return endTagLine;
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddPlayerBulletPrefabVariableToCs(NewFeature feature)
        {
            //const string FilePath = UnityPaths.PathPlayerBulletPoolCs;
            //const string EndTag = UnityPaths.TagPlayerMainCannon;
            AppendPrefabVariableToPoolListCs(feature);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddPlayerAdditionalBulletPrefabVariableToCs(NewFeature feature)
        {
            //const string FilePath = UnityPaths.PathPlayerBulletPoolCs;
            //const string EndTag = UnityPaths.TagPlayerAdditionalBullets;
            AppendPrefabVariableToPoolListCs(feature);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddEnemyBulletPrefabVariableToCs(NewFeature feature)
        {
            //const string FilePath = UnityPaths.PathEnemyBulletPoolCs;
            //const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToPoolListCs(feature);
        }

        [Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet)]
        public static void AddEnemyPrefabVariableToCs(NewFeature feature)
        {
            //const string FilePath = UnityPaths.PathEnemyPoolCs;
            //const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToPoolListCs(feature);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddUIElementPrefabVariableToCs(NewFeature feature)
        {
            //const string FilePath = UnityPaths.PathUIElementPoolCs;
            //const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToPoolListCs(feature);
        }




        [Obsolete(Untested + NeedsToCreateFireStrategyBalance)]
        public static void AddFireStrategyToGameManagerCs(NewFeature feature)
        {
            string gameManagerPath = UnityPaths.PathGameManagerCs;

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagGameManagerInitFireStrategiesStart;
            string endTag = UnityPaths.TagGameManagerInitFireStrategiesEnd;
            string className = feature.ClassName;

            Debug.Assert(File.Exists(gameManagerPath));

            string[] lines = File.ReadAllLines(gameManagerPath);
            int endTagLine = FindEndTagLineAfterStartTagLine(startTag, endTag, lines, gameManagerPath);

            string newStrategyLine = $"                new {className}(Prefab<{featureName}Bullet>(), in _FireStrategyManager),";

            List<string> allLines = new List<string>(lines.Length + 1);

            int insertLineNumber = endTagLine - 1;

            for (int i = 0; i < insertLineNumber; i++)
                allLines.Add(lines[i]);

            allLines.Add(newStrategyLine);

            for (int i = insertLineNumber; i < lines.Length; i++)
                allLines.Add(lines[i]);

            File.WriteAllLines(gameManagerPath, allLines);
        }

        private static int FindEndTagLineAfterStartTagLine(string startTag, string endTag, string[] lines, string filePath)
        {
            int endTagLine = 0;
            bool startTagLineFound = false;
            bool endTagLineFound = false;

            while (!startTagLineFound)
            {
                if (endTagLine == lines.Length)
                    Debug.Fail($"Start tag {startTag} was not found in file {filePath}");

                string line = lines[endTagLine];

                if (line.EndsWith(startTag))
                    startTagLineFound = true;

                endTagLine++;
            }

            while (!endTagLineFound)
            {
                if (endTagLine == lines.Length)
                    Debug.Fail($"End tag {endTag} was not found in file {filePath}");

                string line = lines[endTagLine];

                if (line.EndsWith(endTag))
                    endTagLineFound = true;

                endTagLine++;
            }

            return endTagLine;
        }

    }
}
