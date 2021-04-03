using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    [DebuggerDisplay("{FeatureName} ({FeatureType})")]
    public abstract class FeatureBuilder
    {
        /// <summary>
        /// The name of this feature, minus any qualifying types.
        /// Existing feature names in Valkyrie Undying today include Shrapnel, Shotgun, and Cradle.
        /// </summary>
        public string FeatureName { get; }

        /// <summary>
        /// The type of this feature.
        /// Examples include Bullet, Enemy, Powerup, etc.
        /// </summary>
        public abstract string FeatureType { get; }

        /// <summary>
        /// The name of the C# class that will be assigned to this feature.
        /// Examples include ShrapnelPowerup, ShotgunBullet, CradleEnemy.
        /// </summary>
        public string ClassName => FeatureName + FeatureType;

        /// <summary>
        /// The path to the template C# source code file assigned to this feature at construction.
        /// </summary>
        public abstract string InitialPathTemplateCs { get; }

        /// <summary>
        /// The path to the destination directory assigned to this feature at construction.
        /// </summary>
        public abstract string InitialDirDestination { get; }

        /// <summary>
        /// The path to the Object Pool C# source code file assigned to this feature at construction.
        /// May be null if the feature does not represent a prefab.
        /// </summary>
        public abstract string InitialPathObjectPoolCs { get; }

        /// <summary>
        /// The template name inside each template file that will be replace with the feature name.
        /// For example, the player bullet's template file is called BasicBullet.cs.
        /// If the feature is named "Fast", this will turn into FastBullet.cs.
        /// </summary>
        public virtual string TemplateName => "Basic";

        /// <summary>
        /// The paths related to the relevant template files representing this feature.
        /// </summary>
        public UnityFileSet PathTemplate { get; set; }

        /// <summary>
        /// The directory where all new feature files will be written.
        /// </summary>
        public virtual string DirDestination { get; protected set; }

        /// <summary>
        /// The name of the file that will be written in the destination directory.
        /// </summary>
        public virtual string PathDestinationCsFileName => FileUtil.FileName(PathTemplate.Cs).Replace(TemplateName, FeatureName);

        /// <summary>
        /// The paths related to the relevant destination files that may be written for this feature.
        /// </summary>
        public UnityFileSet PathDestination
        {
            get
            {
                string pathDestination = DirDestination + PathDestinationCsFileName;
                var ret = new UnityFileSet(pathDestination);
                return ret;
            }

        }

        /// <summary>
        /// The tag inside an Object Pool C# source code file that designates
        /// the end of the relevant prefab variable region of code.
        /// The prefab should be inserted two lines before the line
        /// on which this tag is found.
        /// </summary>
        public string TagPrefab { get; protected set; }

        /// <summary>
        /// The path to the Object Pool file that will hold a prefab
        /// representing this feature.
        /// </summary>
        public UnityFileSet PathObjectPool { get; protected set; }

        public FeatureBuilder(string name)
        {
            FeatureName = name;

            DirDestination = InitialDirDestination;
            PathTemplate = new UnityFileSet(InitialPathTemplateCs);
            PathObjectPool = new UnityFileSet(InitialPathObjectPoolCs);
        }

        /// <summary>
        /// Returns the contents of the C# source code template file
        /// representing this feature.
        /// Can be overridden to alter contents of file as needed.
        /// </summary>
        public virtual string ReadTemplateCsFileContents()
        {
            string ret = File.ReadAllText(PathTemplate.Cs);
            return ret;
        }
    }
}
