﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public static class TemplatePaths
    {
        public static string GetPrefabPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = input.Replace(".cs", ".prefab");
            return ret;
        }
        public static string GetCsMetadataPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = input + ".meta";
            return ret;
        }
        public static string GetPrefabMetadataPath(string input)
        {
            Debug.Assert(input.EndsWith(".cs"));
            string ret = GetCsMetadataPath(input) + ".meta";
            return ret;
        }

        public const string DirTemplate = @"C:\Users\TJ\source\repos\Valkyrie Feature Adder\Valkyrie Feature Adder\Templates\";

        #region Bullet

        public const string PathPlayerBullet = DirTemplate + @"Bullets\Player\BasicBullet.cs";
        public const string PathEnemyBullet = DirTemplate + @"Bullets\Enemy\BasicBullet.cs";

        #endregion Bullet

        #region Enemy

        public const string PathEnemy = DirTemplate + @"Enemies\BasicEnemy.cs";

        #endregion Enemy

        #region FireStrategies

        public const string PathPlayerFireStrategy = @"FireStrategies\Player\BasicStrategy.cs";
        public const string PathEnemyFireStrategy = @"FireStrategies\Enemy\BasicEnemyStrategy.cs";

        #endregion FireStrategies

        #region Powerup

        private const string PathPowerup = DirTemplate + @"Powerups\";
        public const string PathOnFire = PathPowerup + @"OnFire\BasicOnFirePowerup.cs";
        public const string PathOnGetHit = PathPowerup + @"OnFire\BasicOnGetHitPowerup.cs";
        public const string PathOnHit = PathPowerup + @"OnFire\BasicOnHitowerup.cs";
        public const string PathOnKill = PathPowerup + @"OnFire\BasicOnKillPowerup.cs";
        public const string PathOnLevelUp = PathPowerup + @"OnFire\BasicOnLevelUpPowerup.cs";
        public const string PathPassive = PathPowerup + @"OnFire\BasicPassivePowerup.cs";

        #endregion Powerup
    }

    public static class UnityPaths
    {
        public static string TrimProjectDirectory(string input)
        {
            Debug.Assert(input.StartsWith(DirProject));

            string ret = input.Substring(DirProject.Length);
            return ret;
        }

        public const string DirProject = @"C:\Users\TJ\Unity\Valkyrie Undying Script\";
        public const string DirAssets = DirProject + @"Assets\";

        public const string PathCsproj = DirProject + @"Assembly-CSharp.csproj";

        #region Bullet

        public const string DirPlayerBullet = DirAssets + @"Bullets\Player\";
        public const string DirEnemyBullet = DirAssets + @"Bullets\Enemy\";

        #endregion Bullet

        #region Enemy

        public const string DirEnemy = DirAssets + @"Enemies\";

        #endregion Enemy

        #region FireStrategies

        public const string DirPlayerFireStrategy = DirAssets + @"FireStrategies\Player\";
        public const string DirEnemyFireStrategy = DirAssets + @"FireStrategies\Player\";

        #endregion FireStrategies

        #region Powerup

        private const string DirPowerup = DirAssets + @"Powerups\";
        public const string DirOnFire = DirPowerup + @"OnFire\";
        public const string DirOnGetHit = DirPowerup + @"OnFire\";
        public const string DirOnHit = DirPowerup + @"OnFire\";
        public const string DirOnKill = DirPowerup + @"OnFire\";
        public const string DirOnLevelUp = DirPowerup + @"OnFire\";
        public const string DirPassive = DirPowerup + @"OnFire\";

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
