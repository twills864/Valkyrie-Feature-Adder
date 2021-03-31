using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class UnityFileSet
    {
        public string Cs { get; set; }
        public string CsMeta => TemplatePaths.GetCsMetadataPath(Cs);
        public string Prefab => TemplatePaths.GetPrefabPath(Cs);
        public string PrefabMeta => TemplatePaths.GetPrefabMetadataPath(Cs);

        public UnityFileSet(string baseFilePath)
        {
            Cs = baseFilePath;
        }
    }
}
