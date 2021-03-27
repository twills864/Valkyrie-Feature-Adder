using System.Linq;
using Assets.Bullets.EnemyBullets;
using Assets.Constants;
using Assets.FireStrategies.EnemyFireStrategies;
using Assets.ObjectPooling;
using UnityEngine;

namespace Assets.Enemies
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicEnemy : Enemy
    {
        protected override EnemyFireStrategy InitialFireStrategy()
            => new VariantLoopingEnemyFireStrategy<BasicEnemyBullet>(FireSpeed, FireSpeedVariance);
    }
}