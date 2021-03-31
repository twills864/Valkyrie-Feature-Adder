using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public abstract class FeatureBuilder
    {
        private UnityFileSet _pathTemplate;

        public string FeatureName { get; }
        public abstract string Type { get; }
        public string ClassName => FeatureName + Type;

        public abstract string InitialPathTemplateCs { get; }
        public abstract string InitialDirDestination { get; }
        public abstract string InitialPathObjectPoolCs { get; }

        public virtual string TemplateName => "Basic";
        public virtual string DirDestination { get; protected set; }


        public UnityFileSet PathTemplate { get; set; }

        [Obsolete("Override this in Powerup. ret = ret.Replace($\"{SubTypeName}Powerup.cs\", \"Powerup.cs\"")]
        public virtual string PathTemplateCsFileName => FileName(PathTemplate.Cs).Replace(TemplateName, FeatureName);
        public UnityFileSet PathDestination
        {
            get
            {
                string pathDestination = DirDestination + PathTemplateCsFileName;
                var ret = new UnityFileSet(pathDestination);
                return ret;
            }

        }


        public string TagPrefab { get; protected set; }
        public UnityFileSet PathObjectPool { get; protected set; }

        //public string LastNewFeatureCsPath => DirDestination + PathTemplateCsFileName;


        //public string StartTag { get; private set; }

        public FeatureBuilder(string name)
        {
            FeatureName = name;

            DirDestination = InitialDirDestination;
            PathTemplate = new UnityFileSet(InitialPathTemplateCs);
            PathObjectPool = new UnityFileSet(InitialPathObjectPoolCs);

            //switch (Type)
            //{
            //    case FeatureType.Bullet:
            //        DirDestinationBase = UnityPaths.DirPlayerBullet;
            //        PathTemplateCs = TemplatePaths.PathPlayerBullet;
            //        PathObjectPoolCs = UnityPaths.PathPlayerBulletPoolCs;
            //        break;
            //    case FeatureType.Enemy:
            //        DirDestinationBase = UnityPaths.DirEnemy;
            //        PathTemplateCs = TemplatePaths.PathEnemy;
            //        PathObjectPoolCs = UnityPaths.PathEnemyPoolCs;
            //        break;
            //    case FeatureType.Powerup:
            //        DirDestinationBase = UnityPaths.DirPowerupBase;
            //        //PathTemplateCs = TemplatePaths.DirTemplate;
            //        PathObjectPoolCs = null;
            //        break;
            //    case FeatureType.Strategy:
            //        DirDestinationBase = UnityPaths.DirPlayerFireStrategy;
            //        PathTemplateCs = TemplatePaths.PathPlayerFireStrategy;
            //        PathObjectPoolCs = null;
            //        break;
            //    default:
            //        throw new ArgumentException($"UNKNOWN FEATURE TYPE {Type}");
            //}
        }



        //public void InitPowerup(Powerup powerup)
        //{
        //    SubTypeName = powerup.ToString();
        //    TemplateNameOverride = $"Basic{SubTypeName}";
        //    DirDestinationSuffix = $@"{SubTypeName}\";
        //    PathTemplateCs = TemplatePaths.DirPowerup + $@"{SubTypeName}\Basic{SubTypeName}Powerup.cs";

        //    //switch (powerup)
        //    //{
        //    //    case Powerup.OnFire:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixOnFire;
        //    //        PathTemplateCs = TemplatePaths.PathOnFire;
        //    //        break;
        //    //    case Powerup.OnGetHit:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixOnGetHit;
        //    //        PathTemplateCs = TemplatePaths.PathOnGetHit;
        //    //        break;
        //    //    case Powerup.OnHit:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixOnHit;
        //    //        PathTemplateCs = TemplatePaths.PathOnHit;
        //    //        break;
        //    //    case Powerup.OnKill:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixOnKill;
        //    //        PathTemplateCs = TemplatePaths.PathOnKill;
        //    //        break;
        //    //    case Powerup.OnLevelUp:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixOnLevelUp;
        //    //        PathTemplateCs = TemplatePaths.PathOnLevelUp;
        //    //        break;
        //    //    case Powerup.Passive:
        //    //        DirDestinationSuffix = UnityPaths.DirSuffixPassive;
        //    //        PathTemplateCs = TemplatePaths.PathPassive;
        //    //        break;
        //    //    default:
        //    //        throw new ArgumentException($"UNKNOWN POWERUP {powerup}");
        //    //}
        //}

        private static string FileName(string path)
        {
            //Debug.Assert(File.Exists(path));
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Name;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
