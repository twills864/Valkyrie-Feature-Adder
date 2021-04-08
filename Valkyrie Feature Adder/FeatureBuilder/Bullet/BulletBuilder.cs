using System;

namespace Valkyrie_Feature_Adder
{
    public class BulletBuilder : FeatureBuilder
    {
        public override string FeatureType => "Bullet";
        public override string InitialPathTemplateCs => TemplatePaths.PathPlayerBullet;
        public override string InitialDirDestination => UnityPaths.DirPlayerBullet;
        public override string InitialPathObjectPoolCs => UnityPaths.PathPlayerBulletPoolCs;

        public BulletBuilder(string name, BulletType type) : base(name)
        {
            switch (type)
            {
                case BulletType.BulletWithFireStrategy:
                    TagPrefab = UnityPaths.TagPlayerMainCannon;
                    break;
                case BulletType.AdditionalBullet:
                    TagPrefab = UnityPaths.TagPlayerAdditionalBullets;
                    DirDestination += UnityPaths.DirSuffixAdditional;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {type}");
            }
        }
    }
}