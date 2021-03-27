using System.Linq;
using Assets.Util;
using Assets.Constants;
using UnityEngine;

namespace Assets.Bullets.PlayerBullets
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicBullet : PlayerBullet
    {
        [SerializeField]
        private float Speed = GameConstants.PrefabNumber;
    }
}