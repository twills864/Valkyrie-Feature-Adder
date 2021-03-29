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

        public const string TemplateName = "Basic";

        //public static string LastNewFeatureCsPath { get; private set; }

        public static void CopyNewFeatureCsFile(NewFeature feature)
        {
            string templateFilePath = feature.PathTemplateCs;
            string destinationDirectory = feature.DirDestination;
            string destinationPath = feature.PathDestinationCs;
            string featureName = feature.FeatureName;
            string templateName = feature.TemplateName;

            #region Assert
            FileInfo fileInfo = new FileInfo(templateFilePath);
            Debug.Assert(fileInfo.Exists);
            Debug.Assert(fileInfo.Extension == ".cs");
            Debug.Assert(Directory.Exists(destinationDirectory));
            Debug.Assert(!File.Exists(destinationPath));
            #endregion Assert

            string fileContents = File.ReadAllText(templateFilePath);
            fileContents = fileContents.Replace(templateName, featureName);

            File.WriteAllText(destinationPath, fileContents);

            AddCsFileToProjectCompile(feature);
        }

        public static void AppendPrefabVariableToPoolListCs(NewFeature feature)
        {
            string filePath = feature.PathObjectPoolCs;
            string endTag = feature.TagPrefab;
            string featureName = feature.FeatureName;
            string className = feature.ClassName;

            Debug.Assert(File.Exists(filePath));

            string[] lines = File.ReadAllLines(filePath);
            int endTagLine = FindEndTagLine(endTag, lines, filePath);

            #region Assert
            // Assert coding style - one line of blank space
            // between last prefab and end tag.
            string _blankLine = lines[endTagLine + 1];
            string _lastPrefabLine = lines[endTagLine];
            Debug.Assert(String.IsNullOrWhiteSpace(_blankLine));
            Debug.Assert(!String.IsNullOrWhiteSpace(_lastPrefabLine));
            #endregion Assert

            const string SerializeTag = "        [SerializeField]";
            const string Private = "        private";

            string variableName = $"{featureName}Prefab";
            string variableLine = $"{Private} {className} {variableName} = null;";

            string[] linesToAdd = new string[]
            {
                SerializeTag,
                variableLine
            };

            int insertLineNumber = endTagLine - 1;
            InsertLinesToFile(filePath, lines, linesToAdd, insertLineNumber);
        }

        public static void AddCsFileToProjectCompile(NewFeature feature)
        {
            string filePath = feature.PathDestinationCs;
            string filePathTrimmed = filePath.Replace(UnityPaths.DirProject, "");

            #region Assert
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(filePathTrimmed.StartsWith("Assets\\"));
            Debug.Assert(filePathTrimmed.EndsWith(".cs"));
            #endregion Assert

            string[] lines = File.ReadAllLines(UnityPaths.PathCsproj);
            int endTagLine = FindCsprojCompilationItemGroupLine(lines);

            const string CompileTagStart = "    <Compile Include=\"";
            const string CompileTagEnd = "\" />";

            string compileTag = $"{CompileTagStart}{filePathTrimmed}{CompileTagEnd}";

            InsertLineToFile(UnityPaths.PathCsproj, lines, compileTag, endTagLine);
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

        //public static void AddPlayerBulletPrefabVariableToCs(NewFeature feature)
        //{
        //    AppendPrefabVariableToPoolListCs(feature);
        //}

        //[Obsolete(Untested + NeedsToPairUnityPrefab)]
        //public static void AddPlayerAdditionalBulletPrefabVariableToCs(NewFeature feature)
        //{
        //    //const string FilePath = UnityPaths.PathPlayerBulletPoolCs;
        //    //const string EndTag = UnityPaths.TagPlayerAdditionalBullets;
        //    AppendPrefabVariableToPoolListCs(feature);
        //}

        //[Obsolete(Untested + NeedsToPairUnityPrefab)]
        //public static void AddEnemyBulletPrefabVariableToCs(NewFeature feature)
        //{
        //    //const string FilePath = UnityPaths.PathEnemyBulletPoolCs;
        //    //const string EndTag = UnityPaths.TagGenericPrefabList;
        //    AppendPrefabVariableToPoolListCs(feature);
        //}

        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet)]
        //public static void AddEnemyPrefabVariableToCs(NewFeature feature)
        //{
        //    //const string FilePath = UnityPaths.PathEnemyPoolCs;
        //    //const string EndTag = UnityPaths.TagGenericPrefabList;
        //    AppendPrefabVariableToPoolListCs(feature);
        //}

        //[Obsolete(Untested + NeedsToPairUnityPrefab)]
        //public static void AddUIElementPrefabVariableToCs(NewFeature feature)
        //{
        //    //const string FilePath = UnityPaths.PathUIElementPoolCs;
        //    //const string EndTag = UnityPaths.TagGenericPrefabList;
        //    AppendPrefabVariableToPoolListCs(feature);
        //}


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

            InsertLineToFile(gameManagerPath, lines, newStrategyLine, endTagLine);
        }

        public static void AddFireStrategyToFireStrategyManager(NewFeature feature)
        {
            string fireStrategyPath = UnityPaths.PathFireStrategyManager;
            Debug.Assert(File.Exists(fireStrategyPath));

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagFireStrategyManagerPlayerRatioStart;
            string endTag = UnityPaths.TagFireStrategyManagerPlayerRatioEnd;

            string[] lines = File.ReadAllLines(fireStrategyPath);
            int endTagLine = FindEndTagLineAfterStartTagLine(startTag, endTag, lines, fireStrategyPath);

            string newStrategyLine = $"            public float {featureName};";

            InsertLineToFile(fireStrategyPath, lines, newStrategyLine, endTagLine);
        }

        [Obsolete(Untested)]
        public static void AddPowerupToPowerupManager(NewFeature feature)
        {
            string powerupBalancePath = UnityPaths.PathPowerupBalanceManager;
            Debug.Assert(File.Exists(powerupBalancePath));

            string featureName = feature.FeatureName;

            string baseBalanceStartTag = BaseBalanceStartTag(feature);
            string baseBalanceEndTag = UnityPaths.TagPowerupBalanceManagerVariablesEnd;

            string[] lines = File.ReadAllLines(powerupBalancePath);
            int baseBalanceEndTagLine = FindEndTagLineAfterStartTagLine(baseBalanceStartTag, baseBalanceEndTag, lines, powerupBalancePath);

            #region Assert
            // Assert coding style - one line of blank space
            // between last balance and end tag.
            string _blankLine = lines[baseBalanceEndTagLine - 1];
            string _lastBalance = lines[baseBalanceEndTagLine - 2];
            Debug.Assert(String.IsNullOrWhiteSpace(_blankLine));
            Debug.Assert(!String.IsNullOrWhiteSpace(_lastBalance));
            #endregion Assert

            int variableLineNumber = baseBalanceEndTagLine - 1;
            string variableLine = $"            public {featureName}Balance {featureName};";

            InsertLineToFile(powerupBalancePath, lines, variableLine, variableLineNumber);

            AddPowerupManagerStructLines(feature, baseBalanceEndTagLine);
        }

        private static void AddPowerupManagerStructLines(NewFeature feature, int baseBalanceEndTagLine)
        {
            string powerupBalancePath = UnityPaths.PathPowerupBalanceManager;

            string featureName = feature.FeatureName;
            string balanceStructEndLine = UnityPaths.TagPowerupBalanceManagerBalanceStructEnd;

            string[] lines = File.ReadAllLines(powerupBalancePath);
            int endOfStructLine = FindNextEquivalentLine(balanceStructEndLine, lines, powerupBalancePath, baseBalanceEndTagLine);

            string powerupBalanceStructTemplatePath = TemplatePaths.PathPowerupBalanceStructTemplate;
            Debug.Assert(File.Exists(powerupBalanceStructTemplatePath));
            string[] structLines = File.ReadAllLines(powerupBalanceStructTemplatePath);

            for (int i = 0; i < structLines.Length; i++)
                structLines[i] = structLines[i].Replace(TemplateName, featureName);

            InsertLinesToFile(powerupBalancePath, lines, structLines, endOfStructLine);
        }


        // Example: "public struct OnFireBalance";
        private static string BaseBalanceStartTag(NewFeature feature)
        {
            string tagStart = UnityPaths.TagPowerupBalanceManagerSubTypeStart;
            string tagEnd = UnityPaths.TagPowerupBalanceManagerSubTypeEnd;
            string subType = feature.SubTypeName;

            string ret = $"{tagStart}{subType}{tagEnd}";
            return ret;
        }


        public static int FindNextEquivalentLine(string lineToFind, string[] lines, string filePath, int startLine)
        {
            bool lineFound = false;
            int currentLineNumber = startLine;
            while (!lineFound)
            {
                if (currentLineNumber >= lines.Length)
                    Debug.Fail($"Line {lineToFind} was not found in file {filePath}");

                string line = lines[currentLineNumber];

                if (line == lineToFind)
                    lineFound = true;
                else
                    currentLineNumber++;
            }

            return currentLineNumber;
        }

        public static int FindEndTagLine(string endTag, string[] lines, string filePath, int startLine = 0)
        {
            int endTagLine = startLine;
            bool endTagLineFound = false;
            while (!endTagLineFound)
            {
                if (endTagLine >= lines.Length)
                    Debug.Fail($"End tag {endTag} was not found in file {filePath}");

                string line = lines[endTagLine];

                if (line.EndsWith(endTag))
                    endTagLineFound = true;
                else
                    endTagLine++;
            }

            return endTagLine;
        }



        public static int FindEndTagLineAfterStartTagLine(string startTag, string endTag, string[] lines, string filePath)
        {
            int startTagLine = FindEndTagLine(startTag, lines, filePath);
            int endTagLine = FindEndTagLine(endTag, lines, filePath, startTagLine);
            return endTagLine;
        }

        public static void InsertLineToFile(string filePathToInsert, string[] existingFileLines, string lineToInsert, int lineNumber)
        {
            string[] linestoInsert = new string[] { lineToInsert };
            InsertLinesToFile(filePathToInsert, existingFileLines, linestoInsert, lineNumber);
        }


        public static void InsertLinesToFile(string filePathToInsert, string[] existingFileLines, string[] linesToInsert, int lineNumber)
        {
            IEnumerable<string> beforeInsert = existingFileLines.Take(lineNumber);
            IEnumerable<string> afterInsert = existingFileLines.Skip(lineNumber);

            IEnumerable<string> linesToAdd = beforeInsert.Concat(linesToInsert).Concat(afterInsert);
            File.WriteAllLines(filePathToInsert, linesToAdd);
        }
    }
}