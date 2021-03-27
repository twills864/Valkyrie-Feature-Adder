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

        public static string FeatureName => Program.FeatureName;
        public static string ClassName => Program.ClassName;

        public static string LastNewFeatureCsPath { get; private set; }

        public static void CopyNewFeatureCsFile(string templateFilePath, string destinationDirectory)
        {
            const string TemplateName = "Basic";

            FileInfo fileInfo = new FileInfo(templateFilePath);
            Debug.Assert(fileInfo.Exists);

            Debug.Assert(fileInfo.Extension == ".cs");

            LastNewFeatureCsPath = destinationDirectory + fileInfo.Name;
            Debugger.Break();

            Debug.Assert(Directory.Exists(destinationDirectory));

            string destinationPath = destinationDirectory + fileInfo.Name.Replace(TemplateName, FeatureName);
            Debug.Assert(!File.Exists(destinationPath));

            string fileContents = File.ReadAllText(templateFilePath);
            fileContents = fileContents.Replace(TemplateName, FeatureName);

            File.WriteAllText(destinationPath, fileContents);
        }

        private static void AppendPrefabVariableToCs(string filePath, string endTag)
        {
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

            string variableName = $"{FeatureName}Prefab";

            string variableLine = $"{Private} {ClassName} {variableName} = null;";

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


        private static void AddCsFileToProjectCompile(string filePath, string endTag)
        {
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(filePath.StartsWith("Assets\\"));
            Debug.Assert(filePath.EndsWith(".cs"));

            string[] lines = File.ReadAllLines(UnityPaths.PathCsproj);
            int endTagLine = FindCsprojCompilationItemGroupLine(lines);

            const string CompileTagStart = "    <Compile Include=\"";
            const string CompileTagEnd = "\" />";

            string compileTag = $"{CompileTagStart}{filePath}{CompileTagEnd}";

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

                if (line.EndsWith(EndTag))
                    endTagLineFound = true;

                endTagLine++;
            }

            return endTagLine;
        }

        [Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab)]
        public static void AddPlayerBulletPrefabVariableToCs()
        {
            const string FilePath = UnityPaths.PathPlayerBulletPoolCs;
            const string EndTag = UnityPaths.TagPlayerMainCannon;
            AppendPrefabVariableToCs(FilePath, EndTag);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddPlayerAdditionalBulletPrefabVariableToCs()
        {
            const string FilePath = UnityPaths.PathPlayerBulletPoolCs;
            const string EndTag = UnityPaths.TagPlayerAdditionalBullets;
            AppendPrefabVariableToCs(FilePath, EndTag);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddEnemyBulletPrefabVariableToCs()
        {
            const string FilePath = UnityPaths.PathEnemyBulletPoolCs;
            const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToCs(FilePath, EndTag);
        }

        [Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet)]
        public static void AddEnemyPrefabVariableToCs()
        {
            const string FilePath = UnityPaths.PathEnemyPoolCs;
            const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToCs(FilePath, EndTag);
        }

        [Obsolete(Untested + NeedsToPairUnityPrefab)]
        public static void AddUIElementPrefabVariableToCs()
        {
            const string FilePath = UnityPaths.PathUIElementPoolCs;
            const string EndTag = UnityPaths.TagGenericPrefabList;
            AppendPrefabVariableToCs(FilePath, EndTag);
        }
    }
}
