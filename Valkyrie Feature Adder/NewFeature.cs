using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class NewFeature
    {
        const string TemplateName = "Basic";

        #region Property Fields
        private string _pathTemplateCs;
        #endregion Property Fields

        public string ClassName => $"{FeatureName}{Type}";

        public string FeatureName { get; }
        public Feature Type { get; }

        public string DirDestinationCs { get; set; }


        public string PathTemplateCs
        {
            get => _pathTemplateCs;
            set
            {
                _pathTemplateCs = value;
                PathTemplateCsFileName = FileName(PathTemplateCs).Replace(TemplateName, FeatureName);
            }
        }
        public string PathTemplateCsFileName { get; private set; }
        public string PathTemplateCsMeta => TemplatePaths.GetCsMetadataPath(PathTemplateCs);
        public string PathTemplatePrefab => TemplatePaths.GetPrefabPath(PathTemplateCs);
        public string PathTemplatePrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathTemplateCs);

        public string PathDestinationCs => DirDestinationCs + PathTemplateCsFileName;
        public string PathDestinationCsMeta => TemplatePaths.GetCsMetadataPath(PathDestinationCs);
        public string PathDestinationPrefab => TemplatePaths.GetPrefabPath(PathDestinationCs);
        public string PathDestinationPrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathDestinationCs);

        public string PathObjectPoolCs { get; }
        public string PathObjectPoolCsMeta => TemplatePaths.GetCsMetadataPath(PathObjectPoolCs);
        public string PathObjectPoolPrefab => TemplatePaths.GetPrefabPath(PathObjectPoolCs);
        public string PathObjectPoolPrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathObjectPoolCs);

        public NewFeature(string name, Feature type)
        {
            FeatureName = name;
            Type = type;

            switch (Type)
            {
                case Feature.Bullet:
                    DirDestinationCs = UnityPaths.DirPlayerBullet;
                    PathTemplateCs = TemplatePaths.PathPlayerBullet;
                    PathObjectPoolCs = UnityPaths.PathPlayerBulletPoolCs;
                    break;
                case Feature.Enemy:
                    DirDestinationCs = UnityPaths.DirEnemy;
                    PathTemplateCs = TemplatePaths.PathEnemy;
                    PathObjectPoolCs = UnityPaths.PathEnemyPoolCs;
                    break;
                case Feature.Powerup:
                    //DirDestinationCs = UnityPaths.Dirpowerup;
                    //PathTemplateCs = TemplatePaths.pathpowerup;
                    PathObjectPoolCs = null;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN FEATURE TYPE {Type}");
            }
        }











        private static string FileName(string path)
        {
            Debug.Assert(File.Exists(path));
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Name;
        }
    }
}
