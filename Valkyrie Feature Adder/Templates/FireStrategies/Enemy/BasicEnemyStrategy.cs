using System;
using System.Linq;
using Assets.Bullets.EnemyBullets;
using Assets.Constants;
using Assets.Enemies;
using Assets.ObjectPooling;
using Assets.Powerups.Balance;
using Assets.Util;
using UnityEngine;

namespace Assets.FireStrategies.EnemyFireStrategies
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicEnemyStrategy : EnemyFireStrategy<BasicEnemyBullet>
    {
        public BasicEnemyStrategy() : this(PoolManager.Instance.EnemyBulletPool.GetPrefab<BasicEnemyBullet>())
        {
        }
        public BasicEnemyStrategy(BasicEnemyBullet bulletPrefab) : base(bulletPrefab)
        {
        }
    }
}
