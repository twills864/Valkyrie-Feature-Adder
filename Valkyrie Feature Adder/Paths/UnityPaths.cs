using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class UnityPaths
    {
        /// <summary>
        /// The path to the directory of the root Unity project of Valkyrie Undying.
        /// Example: @"C:\Users\TJ\Unity\Valkyrie Undying\";
        /// </summary>
        public const string DirProject = null;

        public const string DirAssets = DirProject + @"Assets\";

        public const string PathCsproj = DirProject + @"Assembly-CSharp.csproj";
        public const string PathGameScene = DirAssets + @"GameScene.unity";
        public const string PathGameManagerCs = DirAssets + @"GameManager.cs";
        public const string PathFireStrategyManager = DirAssets + @"BalanceManagers\FireStrategyManager.cs";

        #region Bullet

        public const string DirPlayerBullet = DirAssets + @"Bullets\Player\";
        public const string DirEnemyBullet = DirAssets + @"Bullets\Enemy\";

        public const string DirSuffixAdditional = @"Additional\";

        #endregion Bullet

        #region Enemy

        public const string DirEnemy = DirAssets + @"Enemies\";

        #endregion Enemy

        #region FireStrategies

        public const string DirPlayerFireStrategy = DirAssets + @"FireStrategies\Player\";
        public const string DirEnemyFireStrategy = DirAssets + @"FireStrategies\Player\";

        #endregion FireStrategies

        #region Powerup

        public const string DirPowerupBase = DirAssets + @"Powerups\";

        public const string PathPowerupBalanceManager = DirPowerupBase + @"PowerupBalanceManager.cs";

        #endregion Powerup

        #region Object Pooling

        #region Paths

        #region Object Pools

        public const string DirPoolLists = DirAssets + @"ObjectPooling\PoolLists\";

        private const string PathPlayerBulletPool = DirPoolLists + @"BulletPoolList";
        public const string PathPlayerBulletPoolCs = PathPlayerBulletPool + @".cs";
        public const string PathPlayerBulletPoolPrefab = PathPlayerBulletPool + @".prefab";

        private const string PathEnemyBulletPool = DirPoolLists + @"EnemyBulletPoolList";
        public const string PathEnemyBulletPoolCs = PathEnemyBulletPool + @".cs";
        public const string PathEnemyBulletPoolPrefab = PathEnemyBulletPool + @".prefab";


        private const string PathEnemyPool = DirPoolLists + @"EnemyPoolList";
        public const string PathEnemyPoolCs = PathEnemyPool + @".cs";
        public const string PathEnemyPoolPrefab = PathEnemyPool + @".prefab";


        private const string PathUIElementPool = DirPoolLists + @"UIElementPoolList";
        public const string PathUIElementPoolCs = DirPoolLists + @".cs";
        public const string PathUIElementPoolPrefab = DirPoolLists + @".prefab";

        #endregion Object Pools

        #endregion Paths

        #region Tags

        public const string TagPlayerMainCannon = "#endregion Fired Bullets";
        public const string TagPlayerAdditionalBullets = "#endregion Additional Bullets";
        public const string TagGenericPrefabList = "#pragma warning restore 0414";
        public const string TagGameManagerInitFireStrategiesStart = "private void InitFireStrategies()";
        public const string TagGameManagerInitFireStrategiesEnd = "};";
        public const string TagFireStrategyManagerPlayerRatioStart = "public struct PlayerRatio";
        public const string TagFireStrategyManagerPlayerRatioEnd = "}";
        public const string TagGameSceneFireStrategyManagerStart = "_FireStrategyManager:";
        public const string TagGameSceneFireStrategyManagerEnd = "PowerupBalance:";

        #region PowerupBalanceManager

        public const string TagPowerupBalanceManagerVariablesEnd = "[Serializable]";
        public const string TagPowerupBalanceManagerBalanceStructEnd = "        }";

        public const string TagPowerupBalanceManagerSubTypeStart = "public struct ";
        public const string TagPowerupBalanceManagerSubTypeEnd = "Balance";

        #endregion PowerupBalanceManager

        #endregion Tags

        #endregion Object Pooling

        #region Existing Prefabs

        #region Bullet

        public const string PathPlayerBulletPrefab = DirAssets + @"Bullets\Player\BasicBullet.prefab";
        public const string PathEnemyBulletPrfefab = DirAssets + @"Bullets\Enemy\BasicBullet.prefab";

        #endregion Bullet

        #region Enemy

        public const string PathEnemyPrefab = DirAssets + @"Enemies\BasicEnemy.prefab";

        #endregion Enemy

        #endregion Existing Prefabs
    }
}
