using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class EnemyBulletBuilder : FeatureBuilder
    {
        public override string Type => "EnemyBullet";
        public override string InitialPathTemplateCs => TemplatePaths.PathEnemyBullet;
        public override string InitialDirDestination => UnityPaths.DirEnemyBullet;
        public override string InitialPathObjectPoolCs => UnityPaths.PathEnemyBulletPoolCs;

        public EnemyBulletBuilder(string name) : base(name)
        {
            TagPrefab = UnityPaths.TagGenericPrefabList;
        }
    }
}
