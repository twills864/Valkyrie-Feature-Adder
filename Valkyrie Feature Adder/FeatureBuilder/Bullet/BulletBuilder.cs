using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class BulletBuilder : FeatureBuilder
    {
        public override string Type => "Bullet";
        public override string InitialPathTemplateCs => TemplatePaths.PathPlayerBullet;
        public override string InitialDirDestination => UnityPaths.DirPlayerBullet;
        public override string InitialPathObjectPoolCs => UnityPaths.PathPlayerBulletPoolCs;

        public BulletBuilder(string name, Bullet type) : base(name)
        {
            switch (type)
            {
                case Bullet.BulletWithFireStrategy:
                    TagPrefab = UnityPaths.TagPlayerMainCannon;
                    break;
                case Bullet.AdditionalBullet:
                    TagPrefab = UnityPaths.TagPlayerAdditionalBullets;
                    DirDestination += UnityPaths.DirSuffixAdditional;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {type}");
            }
        }
    }
}
