using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public partial class Program
    {
        [Obsolete(Untested)]
        public static void AddBullet(string featureName)
        {
            Bullet bullet = (Bullet)EnumPrompt(typeof(Bullet));

            BulletBuilder feature = new BulletBuilder(featureName, bullet);

            switch (bullet)
            {
                case Bullet.BulletWithFireStrategy:
                    AddBulletWithFireStrategy(feature);
                    break;
                case Bullet.AdditionalBullet:
                    AddAdditionalBullet(feature);
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {bullet}");
            }
        }

        [Obsolete(Untested)]
        public static void AddBulletWithFireStrategy(BulletBuilder feature)
        {
            Console.WriteLine("AddBulletWithFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);

            PlayerFireStrategyBuilder strategy = new PlayerFireStrategyBuilder(feature.FeatureName);
            //NewFeature strategy = feature.CloneAs(FeatureType.Strategy);
            AddPlayerFireStrategy(strategy);
        }

        [Obsolete(Untested)]
        private static void AddPlayerFireStrategy(PlayerFireStrategyBuilder feature)
        {
            Console.WriteLine("AddPlayerFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            FileUtil.AddFireStrategyToGameManagerCs(feature);

            FileUtil.AddFireStrategyToFireStrategyManager(feature);
            PrefabUtil.AddFireStrategyToGameSceneFireStrategyManager(feature);
        }

        [Obsolete(Untested)]
        public static void AddAdditionalBullet(BulletBuilder feature)
        {
            Console.WriteLine("AddAdditionalBullet()");
            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }















        [Obsolete(Untested)]
        public static void AddPowerup(string featureName)
        {
            Powerup powerup = (Powerup)EnumPrompt(typeof(Powerup));

            PowerupBuilder feature = new PowerupBuilder(featureName, powerup);


            FileUtil.CopyNewFeatureCsFile(feature);
            //PrefabUtil.CopyPrefabData(feature);
            //FileUtil.AppendPrefabVariableToPoolListCs(feature);

            FileUtil.AddPowerupToPowerupManager(feature);
            //PrefabUtil.AddFireStrategyToGameSceneFireStrategyManager(feature);
        }



















        //public static void AddEnemy(string featureName)
        //{
        //    Enemy enemy = (Enemy)EnumPrompt(typeof(Enemy));

        //    EnemyBuilder feature = new EnemyBuilder(featureName, powerup);

        //    switch (enemy)
        //    {
        //        case Enemy.LoopingVariantFireStrategyEnemy:
        //            AddLoopingVariantFireStrategyEnemy(feature);
        //            break;
        //        case Enemy.CustomFireStrategyEnemy:
        //            AddCustomFireStrategyEnemy(feature);
        //            break;
        //        case Enemy.NoFireStrategyEnemy:
        //            AddNoFireStrategyenemy(feature);
        //            break;
        //        default:
        //            throw new ArgumentException($"UNKNOWN BULLET {enemy}");
        //    }
        //}



        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]
        //public static void AddLoopingVariantFireStrategyEnemy(EnemyBuilder feature)
        //{
        //    Console.WriteLine("AddLoopingVariantFireStrategyEnemy");
        //}

        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]
        //public static void AddCustomFireStrategyEnemy(EnemyBuilder feature)
        //{
        //    Console.WriteLine("AddCustomFireStrategyEnemy");
        //}

        //[Obsolete(Untested + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]
        //public static void AddNoFireStrategyenemy(EnemyBuilder feature)
        //{
        //    Console.WriteLine("AddNoFireStrategyenemy");
        //}
    }
}
