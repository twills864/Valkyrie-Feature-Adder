using System.Linq;
using Assets.Constants;
using Assets.ObjectPooling;
using Assets.Util;
using UnityEngine;


namespace Assets.Bullets.EnemyBullets
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicEnemyBullet : PermanentVelocityEnemyBullet
    {
        [SerializeField]
        private float Speed = GameConstants.PrefabNumber;
    }
}