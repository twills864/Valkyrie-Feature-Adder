using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Valkyrie_Feature_Adder
{
    public static partial class FileUtil
    {
        public const string TemplateName = "Basic";

        public static void WriteAllTextWithBackup(string path, string contents)
        {
            BackupUtil.BackupFile(path);
            File.WriteAllText(path, contents);
        }
        public static void WriteAllLinesWithBackup(string path, IEnumerable<string> contents)
        {
            BackupUtil.BackupFile(path);
            File.WriteAllLines(path, contents);
        }

        /// <summary>
        /// Returns the name of a file located at a given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The file path to use.</param>
        /// <returns>The name of the file.</returns>
        public static string FileName(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Name;
        }

        /// <summary>
        /// Reads a feature's template file,
        /// replaces the template name inside file with the feature's name,
        /// and writes the feature file to its destination.
        /// </summary>
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

            string fileContents = feature.ReadTemplateCsFileContents();
            fileContents = fileContents.Replace(templateName, featureName);

            File.WriteAllText(destinationPath, fileContents);

            AddCsFileToProjectCompile(feature);
        }

        /// <summary>
        /// Adds a variable for a feature's prefab inside the appropriate Object Pool file.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        public static void AppendPrefabVariableToPoolListCs(FeatureBuilder feature)
        {
            string filePath = feature.PathObjectPool.Cs;
            Debug.Assert(File.Exists(filePath));

            string endTag = feature.TagPrefab;
            string featureName = feature.FeatureName;
            string className = feature.ClassName;

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
            InsertLinesToFile(filePath, lines, linesToAdd, insertLineNumber, true);
        }

        /// <summary>
        /// Adds a given feature's source code file path
        /// to the project's compilation files list.
        /// </summary>
        public static void AddCsFileToProjectCompile(FeatureBuilder feature)
        {
            string filePath = feature.PathDestination.Cs;
            string filePathTrimmed = filePath.Replace(UnityPaths.DirProject, "");
            string pathCsProj = UnityPaths.PathCsproj;

            #region Assert
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(File.Exists(pathCsProj));
            Debug.Assert(filePathTrimmed.StartsWith("Assets\\"));
            Debug.Assert(filePathTrimmed.EndsWith(".cs"));
            #endregion Assert

            string[] lines = File.ReadAllLines(pathCsProj);
            int endTagLine = FindCsprojCompilationItemGroupLine(lines);

            const string CompileTagStart = "    <Compile Include=\"";
            const string CompileTagEnd = "\" />";

            string compileTag = $"{CompileTagStart}{filePathTrimmed}{CompileTagEnd}";

            InsertLineToFile(pathCsProj, lines, compileTag, endTagLine, true);
        }

        /// <summary>
        /// Finds and returns the first line number index within a .csproj file
        /// for which new newly-included files to be compiled can be added.
        /// </summary>
        /// <param name="lines">The lines of the from the .csproj file.</param>
        /// <returns>The first line number index to insert newly-included files to be compiled.</returns>
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

        /// <summary>
        /// Adds an entry for a given fire strategy to the
        /// list of fire strategies inside GameManager.cs
        /// </summary>
        public static void AddFireStrategyToGameManagerCs(FireStrategyBuilder feature)
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

            InsertLineToFile(gameManagerPath, lines, newStrategyLine, endTagLine, true);
        }

        /// <summary>
        /// Adds an entry for a given fire strategy to the fire strategy balance manager.
        /// </summary>
        public static void AddFireStrategyToFireStrategyManager(FireStrategyBuilder feature)
        {
            string fireStrategyPath = UnityPaths.PathFireStrategyManager;
            Debug.Assert(File.Exists(fireStrategyPath));

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagFireStrategyManagerPlayerRatioStart;
            string endTag = UnityPaths.TagFireStrategyManagerPlayerRatioEnd;

            string[] lines = File.ReadAllLines(fireStrategyPath);
            int endTagLine = FindEndTagLineAfterStartTagLine(startTag, endTag, lines, fireStrategyPath);

            string newStrategyLine = $"            public float {featureName};";

            InsertLineToFile(fireStrategyPath, lines, newStrategyLine, endTagLine, true);
        }

        /// <summary>
        /// Adds an entry for a given powerup to the powerup balance manager.
        /// </summary>
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

            InsertLineToFile(powerupBalancePath, lines, variableLine, variableLineNumber, true);

            AddPowerupManagerStructLines(feature, baseBalanceEndTagLine);
        }

        /// <summary>
        /// Adds a balance manager struct entry for a given powerup to the powerup balance manager.
        /// </summary>
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

            InsertLinesToFile(powerupBalancePath, lines, structLines, endOfStructLine, true);
        }

        /// <summary>
        /// Finds and returns the index of the first line inside an array of <paramref name="lines"/>
        /// that is equivalent to a given line to find.
        /// </summary>
        /// <param name="lineToFind">The line to find inside the array of lines.</param>
        /// <param name="lines">The lines for which to search.</param>
        /// <param name="filePath">The path of the file the lines originated from (used for error messages only).</param>
        /// <param name="startLine">The line number to start the search.</param>
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

        /// <summary>
        /// Finds and returns the index of the first line inside an array of <paramref name="lines"/>
        /// that ends with a given <paramref name="endTag"/>.
        /// </summary>
        /// <param name="endTag">The end tag to find inside the array of lines.</param>
        /// <param name="lines">The lines for which to search.</param>
        /// <param name="filePath">The path of the file the lines originated from (used for error messages only).</param>
        /// <param name="startLine">The line number to start the search.</param>
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

        /// <summary>
        /// Finds and returns the index of the first line inside an array of <paramref name="lines"/>
        /// that appears after a given <paramref name="startTag"/> and ends with a given <paramref name="endTag"/>.
        /// </summary>
        /// <param name="startTag">The start tag to find inside the array of lines.</param>
        /// <param name="endTag">The end tag to find inside the array of lines.</param>
        /// <param name="lines">The lines for which to search.</param>
        /// <param name="filePath">The path of the file the lines originated from (used for error messages only).</param>
        public static int FindEndTagLineAfterStartTagLine(string startTag, string endTag, string[] lines, string filePath)
        {
            int startTagLine = FindEndTagLine(startTag, lines, filePath);
            int endTagLine = FindEndTagLine(endTag, lines, filePath, startTagLine);
            return endTagLine;
        }

        /// <summary>
        /// Inserts a line to a given array of lines at a specified index,
        /// and writes the results to a given file path.
        /// </summary>
        /// <param name="filePathToInsert">The file path to write the results to.</param>
        /// <param name="existingFileLines">The lines to insert the new line to.</param>
        /// <param name="lineToInsert">The line to insert into the existing lines.</param>
        /// <param name="lineNumber">The index inside the existing lines to insert the new line.</param>
        public static void InsertLineToFile(string filePathToInsert, string[] existingFileLines, string lineToInsert, int lineNumber, bool createBackup)
        {
            string[] linestoInsert = new string[] { lineToInsert };
            InsertLinesToFile(filePathToInsert, existingFileLines, linestoInsert, lineNumber, createBackup);
        }

        /// <summary>
        /// Inserts an array of lines to a given array of lines at a specified index,
        /// and writes the results to a given file path.
        /// </summary>
        /// <param name="filePathToInsert">The file path to write the results to.</param>
        /// <param name="existingFileLines">The lines to insert the new line to.</param>
        /// <param name="linesToInsert">The lines to insert into the existing lines.</param>
        /// <param name="lineNumber">The index inside the existing lines to insert the new line.</param>
        public static void InsertLinesToFile(string filePathToInsert, string[] existingFileLines, string[] linesToInsert, int lineNumber, bool createBackup)
        {
            IEnumerable<string> beforeInsert = existingFileLines.Take(lineNumber);
            IEnumerable<string> afterInsert = existingFileLines.Skip(lineNumber);

            IEnumerable<string> linesToAdd = beforeInsert.Concat(linesToInsert).Concat(afterInsert);

            if(createBackup)
                FileUtil.WriteAllLinesWithBackup(filePathToInsert, linesToAdd);
            else
                File.WriteAllLines(filePathToInsert, linesToAdd);
        }
    }
}