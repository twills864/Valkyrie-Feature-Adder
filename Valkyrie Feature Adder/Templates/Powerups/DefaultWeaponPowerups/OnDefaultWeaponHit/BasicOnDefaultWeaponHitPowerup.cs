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
    public class BasicOnDefaultWeaponHitPowerup : OnDefaultWeaponHitPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnDefaultWeaponHitBalance balance)
        {
            float chanceBase = balance.BasicOnDefaultWeaponHit.Chance.Base;
            float chanceIncrease = balance.BasicOnDefaultWeaponHit.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnDefaultWeaponHit.Power.Base;
            float powerIncrease = balance.BasicOnDefaultWeaponHit.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnHit(Enemy enemy, DefaultBullet bullet, Vector3 hitPosition)
        {
            GameManager.Instance.CreateFleetingText("[OnDefaultWeaponHit] BasicOnDefaultWeaponHit", hitPosition);
        }
    }
}
