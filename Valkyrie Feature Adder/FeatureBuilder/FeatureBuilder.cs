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

        public FeatureBuilder(string name)
        {
            FeatureName = name;

            DirDestination = InitialDirDestination;
            PathTemplate = new UnityFileSet(InitialPathTemplateCs);
            PathObjectPool = new UnityFileSet(InitialPathObjectPoolCs);
        }

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

        public virtual string ReadTemplateCsFileContents()
        {
            string ret = File.ReadAllText(PathTemplate.Cs);
            return ret;
        }
    }
}
