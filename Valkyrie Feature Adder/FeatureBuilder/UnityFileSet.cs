using System.Diagnostics;

namespace Valkyrie_Feature_Adder
{
    /// <summary>
    /// Represents a set of paths used to represent Unity objects.
    /// </summary>
    public class UnityFileSet
    {
        public string Cs { get; set; }
        public string CsMeta => GetCsMetadataPath(Cs);
        public string Prefab => GetPrefabPath(Cs);
        public string PrefabMeta => GetPrefabMetadataPath(Cs);

        public UnityFileSet(string baseFilePath)
        {
            Cs = baseFilePath;
        }

        public static string GetPrefabPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = input.Replace(".cs", ".prefab");
            return ret;
        }

        public static string GetCsMetadataPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = input + ".meta";
            return ret;
        }

        public static string GetPrefabMetadataPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = GetPrefabPath(input) + ".meta";
            return ret;
        }
    }
}
