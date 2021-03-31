using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static partial class FileUtil
    {
        public static void CopyNewFeatureCsFile(FeatureBuilder feature)
        {
            string templateFilePath = feature.PathTemplate.Cs;
            string destinationDirectory = feature.DirDestination;
            string destinationPath = feature.PathDestination.Cs;
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

        public static void AddCsFileToProjectCompile(FeatureBuilder feature)
        {
            string filePath = feature.PathDestination.Cs;
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


        public static void AppendPrefabVariableToPoolListCs(FeatureBuilder feature)
        {
            string filePath = feature.PathObjectPool.Cs;
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




        public static void AddFireStrategyToGameManagerCs(FeatureBuilder feature)
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


        public static void AddFireStrategyToFireStrategyManager(FeatureBuilder feature)
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
        public static void AddPowerupToPowerupManager(PowerupBuilder feature)
        {
            string powerupBalancePath = UnityPaths.PathPowerupBalanceManager;
            Debug.Assert(File.Exists(powerupBalancePath));

            string featureName = feature.FeatureName;

            string baseBalanceStartTag = feature.BaseBalanceStartTag;
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

        private static void AddPowerupManagerStructLines(PowerupBuilder feature, int baseBalanceEndTagLine)
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
    }
}