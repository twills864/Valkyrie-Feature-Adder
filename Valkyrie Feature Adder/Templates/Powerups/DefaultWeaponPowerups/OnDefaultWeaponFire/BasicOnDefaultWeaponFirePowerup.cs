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
    public class BasicOnDefaultWeaponFirePowerup : OnDefaultWeaponFirePowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnDefaultWeaponFireBalance balance)
        {
            float chanceBase = balance.BasicOnDefaultWeaponFire.Chance.Base;
            float chanceIncrease = balance.BasicOnDefaultWeaponFire.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnDefaultWeaponFire.Power.Base;
            float powerIncrease = balance.BasicOnDefaultWeaponFire.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnFire(Vector3 position, DefaultBullet[] bullets)
        {
            GameManager.Instance.CreateFleetingText("[OnDefaultWeaponFire] BasicOnDefaultWeaponFire", position);
        }
    }
}
