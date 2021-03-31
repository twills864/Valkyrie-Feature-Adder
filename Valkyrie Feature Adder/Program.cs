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
                //case FeatureType.Enemy:
                //    AddEnemy(featureName);
                //    break;
                default:
                    throw new ArgumentException($"UNKNOWN FEATURE {featureType}");
            }
            //}



            Log.Write("\r\nPress the any key to continue...", ConsoleColor.White);
            Console.ReadKey(true);
        }

        public static void ObsoleteAddFeature()
        {
            const string PromptMessage = "Enter feature name.\r\n" +
                "Example: Shrapnel, Shotgun, Cradle, ...";

            //while (true)
            //{
            string featureName = EnumUtil.ReadStringFromConsole(PromptMessage);
            FeatureType featureType = (FeatureType)EnumPrompt(typeof(FeatureType));

            NewFeature newFeature = new NewFeature(featureName, featureType);


            switch (featureType)
            {
                case FeatureType.Bullet:
                    AddBullet(newFeature);
                    break;
                case FeatureType.Powerup:
                    AddPowerup(newFeature);
                    break;
                case FeatureType.Enemy:
                    AddEnemy(newFeature);
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN FEATURE {featureType}");
            }
            //}

        }

        public static int EnumPrompt(Type type)
        {
            string prompt = $"Select {type.Name} type:";
            return EnumUtil.ReadNumberSelection(prompt, type);
        }



        #region Bullet

        [Obsolete(DEPRECATED)]
        public static void AddBullet(NewFeature feature)
        {
            Bullet bullet = (Bullet)EnumPrompt(typeof(Bullet));

            feature.InitPlayerBullet(bullet);

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

        [Obsolete(DEPRECATED)]
        public static void AddBulletWithFireStrategy(NewFeature feature)
        {
            Console.WriteLine("AddBulletWithFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);

            NewFeature strategy = feature.CloneAs(FeatureType.Strategy);
            AddPlayerFireStrategy(strategy);
        }

        [Obsolete(DEPRECATED)]
        private static void AddPlayerFireStrategy(NewFeature feature)
        {
            Console.WriteLine("AddPlayerFireStrategy()");

            FileUtil.CopyNewFeatureCsFile(feature);
            FileUtil.AddFireStrategyToGameManagerCs(feature);

            FileUtil.AddFireStrategyToFireStrategyManager(feature);
            PrefabUtil.AddFireStrategyToGameSceneFireStrategyManager(feature);
        }

        [Obsolete(DEPRECATED)]
        public static void AddAdditionalBullet(NewFeature feature)
        {
            Console.WriteLine("AddAdditionalBullet()");
            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }

        #endregion Bullet

        #region Powerup

        [Obsolete(DEPRECATED)]
        public static void AddPowerup(NewFeature feature)
        {
            Powerup powerup = (Powerup)EnumPrompt(typeof(Powerup));

            feature.InitPowerup(powerup);


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

        [Obsolete(DEPRECATED)]
        public static void AddEnemy(NewFeature feature)
        {
            Enemy enemy = (Enemy)EnumPrompt(typeof(Enemy));

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
        [Obsolete(DEPRECATED)]
        public static void AddLoopingVariantFireStrategyEnemy(NewFeature feature)
        {
            Console.WriteLine("AddLoopingVariantFireStrategyEnemy");
        }

        //[Obsolete(Untested + NeedsFireStrategy + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]
        [Obsolete(DEPRECATED)]
        public static void AddCustomFireStrategyEnemy(NewFeature feature)
        {
            Console.WriteLine("AddCustomFireStrategyEnemy");
        }

        //[Obsolete(Untested + NeedsToPairUnityPrefab + NeedsToAddEnemyBullet + NeedsRecoloringAndRebalancing)]
        [Obsolete(DEPRECATED)]
        public static void AddNoFireStrategyenemy(NewFeature feature)
        {
            Console.WriteLine("AddNoFireStrategyenemy");
        }

        #endregion Enemy
    }
}
