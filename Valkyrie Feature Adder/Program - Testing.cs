using System;

namespace Valkyrie_Feature_Adder
{
    public partial class Program
    {
        public const string PromptTest = "Enter test name. (Ex: Test0)";

        public static void RunBulletTests(string featureName)
        {
            BulletBuilder CreateBullet(string nameSuffix, BulletType type)
            {
                string name = $"{featureName}{nameSuffix}";
                BulletBuilder feature = new BulletBuilder(name, type);
                return feature;
            }

            BulletBuilder strategy = CreateBullet("WithStrategy", BulletType.BulletWithFireStrategy);
            AddBulletWithFireStrategy(strategy);

            BulletBuilder additional = CreateBullet("Additional", BulletType.AdditionalBullet);
            AddAdditionalBullet(additional);
        }

        public static void RunPowerupTests(string featureName)
        {
            foreach (PowerupType powerup in Enum.GetValues(typeof(PowerupType)))
            {
                string name = $"{featureName}{powerup}";
                PowerupBuilder feature = new PowerupBuilder(name, powerup);
                AddPowerup(feature);
            }
        }

        public static void RunEnemyTests(string featureName)
        {
            EnemyBuilder CreateEnemy(string nameSuffix, EnemyType type)
            {
                string name = $"{featureName}{nameSuffix}";
                EnemyBuilder feature = new EnemyBuilder(name, type);
                return feature;
            }

            EnemyBuilder looping = CreateEnemy("LoopingStrategy", EnemyType.LoopingVariantFireStrategyEnemy);
            AddLoopingVariantFireStrategyEnemy(looping);

            EnemyBuilder custom = CreateEnemy("CustomStrategy", EnemyType.CustomFireStrategyEnemy);
            AddCustomFireStrategyEnemy(custom);

            EnemyBuilder noStrategy = CreateEnemy("NoStrategy", EnemyType.NoFireStrategyEnemy);
            AddNoFireStrategyEnemy(noStrategy);
        }
    }
}
