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

        public string ClassName => FeatureName + Type;

        public string FeatureName { get; }
        public FeatureType Type { get; }
        public string SubTypeName { get; private set; }

        public string DirDestination => DirDestinationBase + DirDestinationSuffix;
        private string DirDestinationBase { get; }
        private string DirDestinationSuffix { get; set; }


        public string PathTemplateCs
        {
            get => _pathTemplateCs;
            set
            {
                _pathTemplateCs = value;
                PathTemplateCsFileName = FileName(PathTemplateCs).Replace(TemplateName, FeatureName);
            }
        }

        public string PathTemplateCsMeta => TemplatePaths.GetCsMetadataPath(PathTemplateCs);
        public string PathTemplatePrefab => TemplatePaths.GetPrefabPath(PathTemplateCs);
        public string PathTemplatePrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathTemplateCs);

        public string PathTemplateCsFileName { get; private set; }

        public string PathDestinationCs => DirDestination + PathTemplateCsFileName;
        public string PathDestinationCsMeta => TemplatePaths.GetCsMetadataPath(PathDestinationCs);
        public string PathDestinationPrefab => TemplatePaths.GetPrefabPath(PathDestinationCs);
        public string PathDestinationPrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathDestinationCs);

        public string TagPrefab { get; set; }
        public string PathObjectPoolCs { get; }
        public string PathObjectPoolCsMeta => TemplatePaths.GetCsMetadataPath(PathObjectPoolCs);
        public string PathObjectPoolPrefab => TemplatePaths.GetPrefabPath(PathObjectPoolCs);
        public string PathObjectPoolPrefabMeta => TemplatePaths.GetPrefabMetadataPath(PathObjectPoolCs);

        public string LastNewFeatureCsPath => DirDestination + PathTemplateCsFileName;


        public string StartTag { get; private set; }

        public NewFeature(string name, FeatureType type)
        {
            FeatureName = name;
            Type = type;

            switch (Type)
            {
                case FeatureType.Bullet:
                    DirDestinationBase = UnityPaths.DirPlayerBullet;
                    PathTemplateCs = TemplatePaths.PathPlayerBullet;
                    PathObjectPoolCs = UnityPaths.PathPlayerBulletPoolCs;
                    break;
                case FeatureType.Enemy:
                    DirDestinationBase = UnityPaths.DirEnemy;
                    PathTemplateCs = TemplatePaths.PathEnemy;
                    PathObjectPoolCs = UnityPaths.PathEnemyPoolCs;
                    break;
                case FeatureType.Powerup:
                    DirDestinationBase = UnityPaths.DirPowerupBase;
                    //PathTemplateCs = TemplatePaths.DirTemplate;
                    PathObjectPoolCs = null;
                    break;
                case FeatureType.Strategy:
                    DirDestinationBase = UnityPaths.DirPlayerFireStrategy;
                    PathTemplateCs = TemplatePaths.PathPlayerFireStrategy;
                    PathObjectPoolCs = null;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN FEATURE TYPE {Type}");
            }
        }


        public void InitPlayerBullet(Bullet bullet)
        {
            switch(bullet)
            {
                case Bullet.BulletWithFireStrategy:
                    TagPrefab = UnityPaths.TagPlayerMainCannon;
                    break;
                case Bullet.AdditionalBullet:
                    TagPrefab = UnityPaths.TagPlayerAdditionalBullets;
                    DirDestinationSuffix = UnityPaths.DirSuffixAdditional;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {bullet}");
            }
        }


        public void InitPowerup(Powerup powerup)
        {
            SubTypeName = powerup.ToString();
            DirDestinationSuffix = $@"{SubTypeName}\";
            PathTemplateCs = TemplatePaths.DirPowerup + $@"{SubTypeName}\Basic{SubTypeName}Powerup.cs";

            //switch (powerup)
            //{
            //    case Powerup.OnFire:
            //        DirDestinationSuffix = UnityPaths.DirSuffixOnFire;
            //        PathTemplateCs = TemplatePaths.PathOnFire;
            //        break;
            //    case Powerup.OnGetHit:
            //        DirDestinationSuffix = UnityPaths.DirSuffixOnGetHit;
            //        PathTemplateCs = TemplatePaths.PathOnGetHit;
            //        break;
            //    case Powerup.OnHit:
            //        DirDestinationSuffix = UnityPaths.DirSuffixOnHit;
            //        PathTemplateCs = TemplatePaths.PathOnHit;
            //        break;
            //    case Powerup.OnKill:
            //        DirDestinationSuffix = UnityPaths.DirSuffixOnKill;
            //        PathTemplateCs = TemplatePaths.PathOnKill;
            //        break;
            //    case Powerup.OnLevelUp:
            //        DirDestinationSuffix = UnityPaths.DirSuffixOnLevelUp;
            //        PathTemplateCs = TemplatePaths.PathOnLevelUp;
            //        break;
            //    case Powerup.Passive:
            //        DirDestinationSuffix = UnityPaths.DirSuffixPassive;
            //        PathTemplateCs = TemplatePaths.PathPassive;
            //        break;
            //    default:
            //        throw new ArgumentException($"UNKNOWN POWERUP {powerup}");
            //}
        }

        public NewFeature CloneAs(FeatureType featureType)
        {
            return new NewFeature(FeatureName, featureType);
        }





        private static string FileName(string path)
        {
            //Debug.Assert(File.Exists(path));
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Name;
        }











    }
}
