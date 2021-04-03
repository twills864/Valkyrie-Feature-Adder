using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static partial class PrefabUtil
    {
        public const string Untested = "Untested. ";
        public const string NeedsFireStrategy = "Doesn't add a matching FireStrategy class. ";
        public const string NeedsToPairUnityPrefab = "Needs to edit the GameScene.unity (or prefab, not sure which) file to automatically connect prefab inside Unity. ";
        public const string NeedsToAddEnemyBullet = "Needs to create a matching bullet for this enemy. ";

        private static Random Rand = new Random();

        /// <summary>
        /// Returns a new random 19-digit number to represent the file ID of a new Unity object.
        /// </summary>
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

        /// <summary>
        /// Returns a new random 32-digit hexadecimal number to represent the GUID of a new Unity object.
        /// </summary>
        public static string NewGuid() => Guid.NewGuid().ToString("N");

        /// <summary>
        /// Generates a new GUID and File ID for a given feature.
        /// Then, it uses this information to copy a metadata file, a prefab file,
        /// and a prefab metadata file based on the feature's information.
        /// </summary>
        public static void CopyPrefabData(FeatureBuilder feature)
        {
            FileInfo csFileInfo = new FileInfo(feature.PathTemplate.Cs);

            #region Assert
            Debug.Assert(csFileInfo.Exists);
            Debug.Assert(csFileInfo.Extension == ".cs");
            Debug.Assert(Directory.Exists(feature.DirDestination));
            #endregion Assert

            string guidCs = NewGuid();
            string guidPrefab = NewGuid();
            string fileId = NewFileId();

            WriteCsMetaContents(feature, guidCs);
            WritePrefabContents(feature, guidCs, fileId);
            WritePrefabMetaContents(feature, guidPrefab);
            AppendPoolListPrefabData(feature, guidPrefab, fileId);
        }

        private const string TagGuidCS = "#GUIDCS#";
        private const string TagGuidPrefab = "#GUIDPREFAB#";
        private const string TagFileId = "#FILEID#";

        /// <summary>
        /// Returns a StringBuilder containing the contents of a given file.
        /// </summary>
        /// <param name="path">The path of the file to read.</param>
        private static StringBuilder StringBuilderFromFile(string path)
        {
            Debug.Assert(File.Exists(path));
            string allText = File.ReadAllText(path);
            return new StringBuilder(allText);
        }

        /// <summary>
        /// Inserts a given GUID into the metadata file of a given feature's
        /// code template file, then writes the results to its proper destination.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        /// <param name="guidCs">The GUID to insert.</param>
        private static void WriteCsMetaContents(FeatureBuilder feature, string guidCs)
        {
            string templatePath = feature.PathTemplate.CsMeta;
            string csMetaDestination = feature.PathDestination.CsMeta;

            Debug.Assert(File.Exists(templatePath));
            Debug.Assert(!File.Exists(csMetaDestination));

            string contents = File.ReadAllText(templatePath);
            contents = contents.Replace(TagGuidCS, guidCs);

            File.WriteAllText(csMetaDestination, contents);
        }

        /// <summary>
        /// Inserts a given GUID and file ID into the prefab file of a given feature,
        /// then writes the results to its proper destination.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        /// <param name="guidCs">The GUID to insert.</param>
        /// <param name="fileId">The file ID to insert.</param>
        private static void WritePrefabContents(FeatureBuilder feature, string guidCs, string fileId)
        {
            string templatePath = feature.PathTemplate.Prefab;
            string prefabDestination = feature.PathDestination.Prefab;

            Debug.Assert(File.Exists(templatePath));
            Debug.Assert(!File.Exists(prefabDestination));

            StringBuilder sb = StringBuilderFromFile(templatePath);

            sb.Replace(TagFileId, fileId);
            sb.Replace(TagGuidCS, guidCs);

            string contents = sb.ToString();

            File.WriteAllText(prefabDestination, contents);
        }

        /// <summary>
        /// Inserts a given GUID into the metadata file of a given feature's
        /// prefab template file, then writes the results to its proper destination.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        /// <param name="guidPrefab">The GUID to insert.</param>
        private static void WritePrefabMetaContents(FeatureBuilder feature, string guidPrefab)
        {
            string templatePath = feature.PathTemplate.PrefabMeta;
            string prefabMetaDestination = feature.PathDestination.PrefabMeta;

            Debug.Assert(File.Exists(templatePath));
            Debug.Assert(!File.Exists(prefabMetaDestination));

            string contents = File.ReadAllText(templatePath);
            contents = contents.Replace(TagGuidPrefab, guidPrefab);

            File.WriteAllText(prefabMetaDestination, contents);
        }

        /// <summary>
        /// Attaches a new feature to a given the Unity object of a given Object Pool
        /// by adding an entry for the given feature's prefab to the metadata
        /// of the Object Pool.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        /// <param name="guidPrefab">The GUID of the new feature's prefab.</param>
        /// <param name="fileId">The file ID of the new feature's prefab.</param>
        private static void AppendPoolListPrefabData(FeatureBuilder feature, string guidPrefab, string fileId)
        {
            string poolPrefabPath = feature.PathObjectPool.Prefab;
            string featureName = feature.FeatureName;

            Debug.Assert(File.Exists(poolPrefabPath));
            string prefabToAdd = $"  {featureName}Prefab: {{fileID: {fileId}, guid: {guidPrefab},\r\n    type: 3}}\r\n";

            File.AppendAllText(poolPrefabPath, prefabToAdd);
        }

        /// <summary>
        /// Adds a new entry for a given fire strategy's PowerupBalanaceManager value
        /// to the GameScene.unity file of the project.
        /// Unity would automatically create this entry at runtime, but it would assign a default value of 0.
        /// This would cause the new fire strategy to fire every frame by default.
        /// (A value of 1 represents a fire speed equal to the base cannon's fire speed.)
        /// </summary>
        public static void AddFireStrategyToGameSceneFireStrategyManager(FireStrategyBuilder feature)
        {
            string gameScenePath = UnityPaths.PathGameScene;

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagGameSceneFireStrategyManagerStart;
            string endTag = UnityPaths.TagGameSceneFireStrategyManagerEnd;

            Debug.Assert(File.Exists(gameScenePath));

            string[] lines = File.ReadAllLines(gameScenePath);
            int endTagLine = FileUtil.FindEndTagLineAfterStartTagLine(startTag, endTag, lines, gameScenePath);

            string newStrategyLine = $"      {featureName}: 1";

            FileUtil.InsertLineToFile(gameScenePath, lines, newStrategyLine, endTagLine, true);
        }
    }
}
