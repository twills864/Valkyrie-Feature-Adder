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
        public override string FeatureType => "Enemy";
        public override string InitialPathTemplateCs => TemplatePaths.PathEnemy;
        public override string InitialDirDestination => UnityPaths.DirEnemy;
        public override string InitialPathObjectPoolCs => UnityPaths.PathEnemyPoolCs;

        private string FireStrategyTagStart { get; }
        private const string FireStrategyTagEnd = "#endif";

        private const string TagStartCustom = "#if CustomFireStrategyEnemy";
        private const string TagStartLoopingVariant = "#if LoopingVariantFireStrategyEnemy";
        private const string TagStartNo = "#if NoFireStrategyEnemy";

        private EnemyType SubType { get; }

        public EnemyBuilder(string name, EnemyType type) : base(name)
        {
            TagPrefab = UnityPaths.TagGenericPrefabList;
            SubType = type;

            switch (type)
            {
                case EnemyType.CustomFireStrategyEnemy:
                    FireStrategyTagStart = TagStartCustom;
                    break;
                case EnemyType.LoopingVariantFireStrategyEnemy:
                    FireStrategyTagStart = TagStartLoopingVariant;
                    break;
                case EnemyType.NoFireStrategyEnemy:
                    FireStrategyTagStart = TagStartNo;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN ENEMY {type}");
            }
        }

        /// <summary>
        /// Parses the template file lines and keeps only the lines that are relevant
        /// to this subclass, designated in the template file using the
        /// following #if tags based on their entries in the EnemyType enum:
        /// #if CustomFireStrategyEnemy
        /// #if LoopingVariantFireStrategyEnemy
        /// #if NoFireStrategyEnemy
        /// </summary>
        public override string ReadTemplateCsFileContents()
        {
            string[] lines = File.ReadAllLines(PathTemplate.Cs);

            if (SubType == EnemyType.LoopingVariantFireStrategyEnemy)
                FixLoopingVariantFireStrategyEnemyLines(lines);

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
                    writing = false;
                else if(line == FireStrategyTagEnd)
                    writing = true;
                else if(line != FireStrategyTagStart && writing)
                    contents.AppendLine(line);
            }

            string ret = contents.ToString();
            return ret;
        }

        /// <summary>
        /// Changes the base class of an Enemy template from Enemy to FireStrategyEnemy.
        /// </summary>
        /// <param name="lines">The source lines from the template file.</param>
        private void FixLoopingVariantFireStrategyEnemyLines(string[] lines)
        {
            int i = 0;
            while (lines[i] != "    public class BasicEnemy : Enemy")
                i++;
            lines[i] = "    public class BasicEnemy : FireStrategyEnemy";
        }
    }
}
