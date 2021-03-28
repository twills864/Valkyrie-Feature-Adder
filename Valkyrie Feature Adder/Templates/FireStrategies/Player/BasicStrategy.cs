using System.Linq;
using Assets.Bullets.PlayerBullets;
using Assets.Constants;
using Assets.FireStrategyManagers;
using Assets.Util;
using UnityEngine;

namespace Assets.FireStrategies.PlayerFireStrategies
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicStrategy : PlayerFireStrategy<BasicBullet>
    {
        public BasicStrategy(BasicBullet bullet, in PlayerFireStrategyManager manager) : base(bullet, manager)
        {
        }

        protected override float GetFireSpeedRatio(in PlayerFireStrategyManager.PlayerRatio ratios)
            => ratios.Basic;
    }
}
