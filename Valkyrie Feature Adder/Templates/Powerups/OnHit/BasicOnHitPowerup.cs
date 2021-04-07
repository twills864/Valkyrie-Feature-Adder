using System;
using System.Linq;
using Assets.Bullets.PlayerBullets;
using Assets.Constants;
using Assets.Enemies;
using Assets.ObjectPooling;
using Assets.Powerups.Balance;
using Assets.Util;
using UnityEngine;

namespace Assets.Powerups
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicOnHitPowerup : OnHitPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnHitBalance balance)
        {
            float chanceBase = balance.BasicOnHit.Chance.Base;
            float chanceIncrease = balance.BasicOnHit.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnHit.Power.Base;
            float powerIncrease = balance.BasicOnHit.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnHit(Enemy enemy, PlayerBullet bullet, Vector3 hitPosition)
        {
            GameManager.Instance.CreateFleetingText("[OnHit] BasicOnHit", hitPosition);
        }
    }
}
