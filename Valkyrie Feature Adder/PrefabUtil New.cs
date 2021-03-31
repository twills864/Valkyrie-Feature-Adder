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
        public static void CopyPrefabData(FeatureBuilder feature)
        {
            FileInfo csFileInfo = new FileInfo(feature.PathTemplate.Cs);

            #region Assert
            Debug.Assert(csFileInfo.Exists);
            Debug.Assert(csFileInfo.Extension == ".cs");
            Debug.Assert(Directory.Exists(feature.DirDestination));
            #endregion Assert

            string csMetaTemplate = feature.PathTemplate.CsMeta;
            string prefabTemplate = feature.PathTemplate.Prefab;
            string prefabMetaTemplate = feature.PathTemplate.PrefabMeta;

            #region Assert
            Debug.Assert(File.Exists(csMetaTemplate));
            Debug.Assert(File.Exists(prefabTemplate));
            Debug.Assert(File.Exists(prefabMetaTemplate));
            #endregion Assert

            string csMetaDestination = feature.PathDestination.CsMeta;
            string prefabDestination = feature.PathDestination.Prefab;
            string prefabMetaDestination = feature.PathDestination.PrefabMeta;

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

            string poolPrefabPath = feature.PathObjectPool.Prefab;
            string featureName = feature.FeatureName;
            AppendPoolListPrefabData(poolPrefabPath, featureName, guidPrefab, fileId);

            //string fileContents = File.ReadAllText(csTemplateFilePath);
            //fileContents = fileContents.Replace(TemplateName, FeatureName);

            //File.WriteAllText(destinationPath, fileContents);
        }


        public static void AddFireStrategyToGameSceneFireStrategyManager(FeatureBuilder feature)
        {
            string gameScenePath = UnityPaths.PathGameScene;

            string featureName = feature.FeatureName;
            string startTag = UnityPaths.TagGameSceneFireStrategyManagerStart;
            string endTag = UnityPaths.TagGameSceneFireStrategyManagerEnd;

            Debug.Assert(File.Exists(gameScenePath));

            string[] lines = File.ReadAllLines(gameScenePath);
            int endTagLine = FileUtil.FindEndTagLineAfterStartTagLine(startTag, endTag, lines, gameScenePath);

            string newStrategyLine = $"      {featureName}: 1";

            FileUtil.InsertLineToFile(gameScenePath, lines, newStrategyLine, endTagLine);
        }
    }
}
