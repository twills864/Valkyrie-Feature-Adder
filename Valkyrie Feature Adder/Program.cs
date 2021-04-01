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
        public const string Untested = "Untested. ";
        public const string NeedsFireStrategy = "Doesn't add a matching FireStrategy class. ";
        public const string NeedsToPairUnityPrefab = "Needs to edit the GameScene.unity (or prefab, not sure which) file to automatically connect prefab inside Unity. ";
        public const string NeedsToAddEnemyBullet = "Needs to create a matching bullet for this enemy. ";
        public const string NeedsRecoloringAndRebalancing = "Needs include entries for the ColorManager and the BalanceManager. ";
        public const string DEPRECATED = "USE FEATUREBUILDER INSTEAD";

        //public static string FeatureName { get; private set; }
        //private static Feature FeatureType { get; set; }

        //public static string ClassName => $"{FeatureName}{FeatureType}";
        //public static string BulletPoolPath { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            const string PromptMessage = "Enter feature name.\r\n" +
                "Example: Shrapnel, Shotgun, Cradle, ...";

            //while (true)
            //{
            string featureName = EnumUtil.ReadStringFromConsole(PromptMessage);
            FeatureType featureType = (FeatureType)EnumPrompt(typeof(FeatureType));

            switch (featureType)
            {
                case FeatureType.Bullet:
                    AddBullet(featureName);
                    break;
                case FeatureType.Powerup:
                    AddPowerup(featureName);
                    break;
                case FeatureType.Enemy:
                    AddEnemy(featureName);
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN FEATURE {featureType}");
            }
            //}



            Log.Write("\r\nPress the any key to continue...", ConsoleColor.White);
            Console.ReadKey(true);
        }

        public static int EnumPrompt(Type type)
        {
            string prompt = $"Select {type.Name} type:";
            return EnumUtil.ReadNumberSelection(prompt, type);
        }


        #region Bullet

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

        private static void AddPlayerFireStrategy(PlayerFireStrategyBuilder feature)
        {
            Console.WriteLine("AddPlayerFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            FileUtil.AddFireStrategyToGameManagerCs(feature);

            FileUtil.AddFireStrategyToFireStrategyManager(feature);
            PrefabUtil.AddFireStrategyToGameSceneFireStrategyManager(feature);
        }


        public static void AddAdditionalBullet(BulletBuilder feature)
        {
            Console.WriteLine("AddAdditionalBullet()");
            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }

        #endregion Bullet

        #region Powerup

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

        //[Obsolete(Untested + NeedsRecoloringAndRebalancing)]
        //public static void AddOnFire(NewFeature feature)
        //{
        //    Console.WriteLine("AddOnFire");
        //}

        //[Obsolete(Untested + NeedsRecoloringAndRebalancing)]
        //public static void AddOnGetHit(NewFeature feature)
        //{
        //    Console.WriteLine("AddOnGetHit");
        //}

        //public static void AddOnHit(NewFeature feature)
        //{
        //    Console.WriteLine("AddOnHit");
        //}

        //[Obsolete(Untested + NeedsRecoloringAndRebalancing)]
        //public static void AddOnKill(NewFeature feature)
        //{
        //    Console.WriteLine("AddOnKill");
        //}

        //[Obsolete(Untested + NeedsRecoloringAndRebalancing)]
        //public static void AddOnLevelUp(NewFeature feature)
        //{
        //    Console.WriteLine("AddOnLevelUp");
        //}

        //[Obsolete(Untested + NeedsRecoloringAndRebalancing)]
        //public static void AddPassive(NewFeature feature)
        //{
        //    Console.WriteLine("AddPassive");
        //}

        #endregion Powerup

        #region Enemy

        public static void AddEnemy(string featureName)
        {
            Enemy enemy = (Enemy)EnumPrompt(typeof(Enemy));

            EnemyBuilder feature = new EnemyBuilder(featureName, enemy);

            switch (enemy)
            {
                case Enemy.LoopingVariantFireStrategyEnemy:
                    AddLoopingVariantFireStrategyEnemy(feature);
                    break;
                case Enemy.CustomFireStrategyEnemy:
                    AddCustomFireStrategyEnemy(feature);
                    break;
                case Enemy.NoFireStrategyEnemy:
                    AddNoFireStrategyenemy(feature);
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {enemy}");
            }
        }

        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]

        public static void AddLoopingVariantFireStrategyEnemy(EnemyBuilder feature)
        {
            Console.WriteLine("AddLoopingVariantFireStrategyEnemy");

            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);

            EnemyBulletBuilder bullet = new EnemyBulletBuilder(feature.FeatureName);
            AddEnemyBullet(bullet);

            EnemyFireStrategyBuilder strategy = new EnemyFireStrategyBuilder(feature.FeatureName);
            AddEnemyFireStrategy(strategy);
        }


        public static void AddEnemyBullet(EnemyBulletBuilder feature)
        {
            Console.WriteLine("AddBulletWithFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }

        private static void AddEnemyFireStrategy(EnemyFireStrategyBuilder feature)
        {
            Console.WriteLine("AddEnemyFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
        }

        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]

        public static void AddCustomFireStrategyEnemy(EnemyBuilder feature)
        {
            Console.WriteLine("AddCustomFireStrategyEnemy (May be handled in subclass - just try looping variant)");

            AddLoopingVariantFireStrategyEnemy(feature);
        }

        //[Obsolete(Untested + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]

        public static void AddNoFireStrategyenemy(EnemyBuilder feature)
        {
            Console.WriteLine("AddNoFireStrategyenemy");

            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }

        #endregion Enemy
    }
}
