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
    public class BasicOnDefaultWeaponKillPowerup : OnDefaultWeaponKillPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnDefaultWeaponKillBalance balance)
        {
            float chanceBase = balance.BasicOnDefaultWeaponKill.Chance.Base;
            float chanceIncrease = balance.BasicOnDefaultWeaponKill.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnDefaultWeaponKill.Power.Base;
            float powerIncrease = balance.BasicOnDefaultWeaponKill.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnKill(Enemy enemy, DefaultBullet bullet)
        {
            GameManager.Instance.CreateFleetingText("[OnDefaultWeaponKill] BasicOnDefaultWeaponKill", SpaceUtil.WorldMap.Center);
        }
    }
}
