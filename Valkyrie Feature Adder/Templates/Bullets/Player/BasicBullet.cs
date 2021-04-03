using System.Linq;
using Assets.Util;
using Assets.Constants;
using Assets.ObjectPooling;
using UnityEngine;

namespace Assets.Bullets.PlayerBullets
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicBullet : PermanentVelocityPlayerBullet
    {
        [SerializeField]
        private float Speed = GameConstants.PrefabNumber;
    }
}