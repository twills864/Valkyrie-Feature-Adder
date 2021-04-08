using System;

namespace Valkyrie_Feature_Adder
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Log.WriteLine(BackupUtil.ScriptTime, ConsoleColor.White);

            bool needsPaths = TemplatePaths.DirTemplate == null
                || UnityPaths.DirProject == null
                || BackupUtil.DirBackup == null;

            if(!needsPaths)
            {
                LogDirectories();

                CreateFeature();
                //RunTests();
            }
            else
            {
                Log.WriteLine("\r\nERROR\r\n", Log.ColorError);

                const string PathError = "Please assign valid path values to the following const member variables:"
                    + "\r\n\r\nTemplatePaths.DirTemplate (Paths/TemplatePaths.cs)"
                    + "\r\nUnityPaths.DirProject (Paths/UnityPaths.cs)"
                    + "\r\nBackupUtil.DirBackup (BackupUtil.cs)\r\n";
                Log.WriteLine(PathError, Log.ColorErrorDetails);
            }

            Log.Write("\r\nPress any key to continue...", ConsoleColor.White);
            Console.ReadKey(true);
        }

        public static void RunTests()
        {
            string featureName = Log.ReadStringFromConsole(PromptTest);
            RunBulletTests(featureName);
            RunPowerupTests(featureName);
            RunEnemyTests(featureName);
        }

        public static void LogDirectories()
        {
            void LogDirectory(string directoryDescription, string directoryPath)
            {
                Log.Write($"{directoryDescription}: ", Log.ColorPrompt);
                Log.WriteLine(directoryPath, Log.ColorPrintInfo);
            }

            Log.WriteLine();
            LogDirectory(" Project directory", UnityPaths.DirProject);
            LogDirectory("Template directory", TemplatePaths.DirTemplate);
            LogDirectory("  Backup directory", BackupUtil.DirBackup);
        }

        public static void CreateFeature()
        {
            const string PromptMessage = "Enter feature name.\r\n" +
                "Example: Shrapnel, Shotgun, Cradle, ...";

            string featureName = Log.ReadStringFromConsole(PromptMessage);
            FeatureType featureType = (FeatureType)Log.EnumPrompt(typeof(FeatureType), (int)FeatureType.Strategy);

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
        }

        #region Bullet

        public static void AddBullet(string featureName)
        {
            BulletType bullet = (BulletType)Log.EnumPrompt(typeof(BulletType));
            BulletBuilder feature = new BulletBuilder(featureName, bullet);

            Action<BulletBuilder> add;
            switch (bullet)
            {
                case BulletType.BulletWithFireStrategy:
                    add = AddBulletWithFireStrategy;
                    break;
                case BulletType.AdditionalBullet:
                    add = AddAdditionalBullet;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN BULLET {bullet}");
            }

            if (ConfirmFeature(feature, add.Method.Name))
                add(feature);
        }

        #region Bullet With Fire Strategy

        public static void AddBulletWithFireStrategy(BulletBuilder feature)
        {
            LogProgress("Bullet with FireStrategy", feature);

            AddFeatureWithPrefab(feature);
            PlayerFireStrategyBuilder strategy = new PlayerFireStrategyBuilder(feature.FeatureName);
            AddPlayerFireStrategy(strategy);
        }

        private static void AddPlayerFireStrategy(PlayerFireStrategyBuilder feature)
        {
            LogProgress("PlayerFireStrategy", feature);

            FileUtil.CopyNewFeatureCsFile(feature);
            FileUtil.AddFireStrategyToGameManagerCs(feature);

            FileUtil.AddFireStrategyToFireStrategyManager(feature);
            PrefabUtil.AddFireStrategyToGameSceneFireStrategyManager(feature);
        }

        #endregion Bullet With Fire Strategy

        public static void AddAdditionalBullet(BulletBuilder feature)
        {
            LogProgress("Additional Bullet", feature);

            AddFeatureWithPrefab(feature);
        }

        #endregion Bullet

        #region Powerup

        public static void AddPowerup(string featureName)
        {
            PowerupType powerup = (PowerupType)Log.EnumPrompt(typeof(PowerupType));

            PowerupBuilder feature = new PowerupBuilder(featureName, powerup);

            if (ConfirmFeature(feature, "AddPowerup"))
                AddPowerup(feature);
        }

        public static void AddPowerup(PowerupBuilder feature)
        {
            LogProgress($"{feature.PowerupType}Powerup", feature);

            FileUtil.CopyNewFeatureCsFile(feature);
            FileUtil.AddPowerupToPowerupManager(feature);
        }

        #endregion Powerup

        #region Enemy

        public static void AddEnemy(string featureName)
        {
            EnemyType enemy = (EnemyType)Log.EnumPrompt(typeof(EnemyType));

            EnemyBuilder feature = new EnemyBuilder(featureName, enemy);

            Action<EnemyBuilder> add;
            switch (enemy)
            {
                case EnemyType.LoopingVariantFireStrategyEnemy:
                    add = AddLoopingVariantFireStrategyEnemy;
                    break;
                case EnemyType.CustomFireStrategyEnemy:
                    add = AddCustomFireStrategyEnemy;
                    break;
                case EnemyType.NoFireStrategyEnemy:
                    add = AddNoFireStrategyEnemy;
                    break;
                default:
                    throw new ArgumentException($"UNKNOWN ENEMY {enemy}");
            }

            if(ConfirmFeature(feature, add.Method.Name))
                add(feature);
        }

        private static void AddEnemyWithFireStrategy(EnemyBuilder feature)
        {
            AddFeatureWithPrefab(feature);

            EnemyBulletBuilder bullet = new EnemyBulletBuilder(feature.FeatureName);
            AddEnemyBullet(bullet);

            EnemyFireStrategyBuilder strategy = new EnemyFireStrategyBuilder(feature.FeatureName);
            AddEnemyFireStrategy(strategy);
        }

        public static void AddLoopingVariantFireStrategyEnemy(EnemyBuilder feature)
        {
            LogProgress("LoopingVariantFireStrategyEnemy", feature);
            AddEnemyWithFireStrategy(feature);
        }

        public static void AddCustomFireStrategyEnemy(EnemyBuilder feature)
        {
            LogProgress("Enemy with custom FireStrategy", feature);
            AddEnemyWithFireStrategy(feature);
        }

        public static void AddNoFireStrategyEnemy(EnemyBuilder feature)
        {
            LogProgress("Enemy without FireStrategy", feature);
            AddFeatureWithPrefab(feature);
        }

        #region Enemy Bullets

        public static void AddEnemyBullet(EnemyBulletBuilder feature)
        {
            LogProgress("EnemyBullet", feature);
            AddFeatureWithPrefab(feature);
        }

        private static void AddEnemyFireStrategy(EnemyFireStrategyBuilder feature)
        {
            LogProgress("EnemyFireStrategy", feature);
            FileUtil.CopyNewFeatureCsFile(feature);
        }

        #endregion Enemy Bullets

        #endregion Enemy

        public static void AddFeatureWithPrefab(FeatureBuilder feature)
        {
            FileUtil.CopyNewFeatureCsFile(feature);
            PrefabUtil.CopyPrefabData(feature);
            FileUtil.AppendPrefabVariableToPoolListCs(feature);
        }

        public static bool ConfirmFeature(FeatureBuilder feature, string methodName)
        {
            const ConsoleColor ColorPrompt = Log.ColorPrompt;
            const ConsoleColor ColorSelection = Log.ColorPrintInfo;
            string name = feature.FeatureName;
            string type = feature.FeatureType;

            void LogSelection(string prompt, string selection)
            {
                Log.Write(prompt, ColorPrompt);
                Log.WriteLine(selection, ColorSelection);
            }

            Log.WriteLine();
            LogSelection("FEATURE NAME: ", name);
            LogSelection("FEATURE TYPE: ", type);
            LogSelection("CLASS NAME: ", feature.ClassName);
            LogSelection("TEMPLATE NAME: ", feature.TemplateName);
            LogSelection("METHOD NAME: ", $"{methodName}()");
            Log.WriteLine();

            Log.Write("Type ", ColorPrompt);
            Log.Write("Y", ColorSelection);
            Log.Write("/", ColorPrompt);
            Log.Write("y", ColorSelection);
            Log.WriteLine(" to confirm.", ColorPrompt);
            string line = Log.ReadStringFromConsole();

            Log.WriteLine();

            bool ret = line.Equals("y", StringComparison.InvariantCultureIgnoreCase);
            return ret;
        }

        public static void LogProgress(FeatureBuilder feature)
        {
            Log.WriteLine($"Adding {feature.ClassName}...", Log.ColorMetaInfo);
        }

        public static void LogProgress(string descriptor, FeatureBuilder feature)
        {
            Log.WriteLine($"Adding {descriptor} {feature.ClassName}...", Log.ColorMetaInfo);
        }
    }
}
