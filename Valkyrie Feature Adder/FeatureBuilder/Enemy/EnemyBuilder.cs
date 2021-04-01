using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class EnemyBuilder : FeatureBuilder
    {
        public override string Type => "Enemy";
        public override string InitialPathTemplateCs => TemplatePaths.PathEnemy;
        public override string InitialDirDestination => UnityPaths.DirEnemy;
        public override string InitialPathObjectPoolCs => UnityPaths.PathEnemyPoolCs;

        private string FireStrategyTagStart { get; }
        private const string FireStrategyTagEnd = "#endif";

        private const string TagStartCustom = "#if CustomFireStrategyEnemy";
        private const string TagStartLoopingVariant = "#if LoopingVariantFireStrategyEnemy";
        private const string TagStartNo = "#if NoFireStrategyEnemy";

        private Enemy SubType { get; }

        public EnemyBuilder(string name, Enemy type) : base(name)
        {
            TagPrefab = UnityPaths.TagGenericPrefabList;
            SubType = type;

            switch (type)
            {
                case Enemy.CustomFireStrategyEnemy:
                    FireStrategyTagStart = TagStartCustom;
                    break;
                case Enemy.LoopingVariantFireStrategyEnemy:
                    FireStrategyTagStart = TagStartLoopingVariant;
                    break;
                case Enemy.NoFireStrategyEnemy:
                    FireStrategyTagStart = TagStartNo;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN ENEMY {type}");
            }
        }

        //public override string ReadTemplateCsFileContents()
        //{
        //    string[] lines = File.ReadAllLines(PathTemplate.Cs);
        //    StringBuilder contents = new StringBuilder();

        //    int i = 0;
        //    string line = lines[0];

        //    // Copy lines before relevant fire strategy
        //    do
        //    {
        //        contents.AppendLine(line);
        //        i++;
        //        line = lines[i];
        //    } while (!line.StartsWith("#if"));

        //    // Skip to relevant fire strategy
        //    while (!line.StartsWith(FireStrategyTagStart))
        //    {
        //        i++;
        //        line = lines[i];
        //    }
        //    i++;

        //    // Append relevant fire strategy
        //    contents.AppendLine(lines[i++]);
        //    contents.AppendLine(lines[i++]);

        //    // Skip to rest of file
        //    i++;
        //    while(lines[i].StartsWith("#if"))
        //    {
        //        do
        //            i++;
        //        while (!lines[i].StartsWith(FireStrategyTagEnd));
        //        i++;
        //    }

        //    // Copy rest of file
        //    while(i < lines.Length)
        //        contents.AppendLine(lines[i++]);

        //    string ret = contents.ToString();
        //    return ret;
        //}

        public override string ReadTemplateCsFileContents()
        {
            string[] lines = File.ReadAllLines(PathTemplate.Cs);
            if(SubType == Enemy.LoopingVariantFireStrategyEnemy)
            {
                int i = 0;
                while (lines[i] != "    public class BasicEnemy : Enemy")
                    i++;
                lines[i] = "    public class BasicEnemy : FireStrategyEnemy";
            }


            StringBuilder contents = new StringBuilder();

            List<string> invalidStartTags = new List<string>()
            {
                TagStartCustom,
                TagStartLoopingVariant,
                TagStartNo
            };

            Debug.Assert(invalidStartTags.Contains(FireStrategyTagStart));
            invalidStartTags.Remove(FireStrategyTagStart);

            bool writing = true;
            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if(invalidStartTags.Contains(line))
                {
                    writing = false;
                }
                else if(line == FireStrategyTagEnd)
                {
                    writing = true;
                }
                else if(line != FireStrategyTagStart && writing)
                {
                    contents.AppendLine(line);
                }
            }

            string ret = contents.ToString();
            return ret;
        }
    }
}
