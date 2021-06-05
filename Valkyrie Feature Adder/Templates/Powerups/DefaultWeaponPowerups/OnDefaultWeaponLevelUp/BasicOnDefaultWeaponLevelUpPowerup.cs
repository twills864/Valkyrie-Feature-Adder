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
    public class BasicOnDefaultWeaponLevelUpPowerup : OnDefaultWeaponLevelUpPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnDefaultWeaponLevelUpBalance balance)
        {
            float chanceBase = balance.BasicOnDefaultWeaponLevelUp.Chance.Base;
            float chanceIncrease = balance.BasicOnDefaultWeaponLevelUp.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnDefaultWeaponLevelUp.Power.Base;
            float powerIncrease = balance.BasicOnDefaultWeaponLevelUp.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnLevelUp()
        {
            GameManager.Instance.CreateFleetingText("[OnLevelUp] BasicOnDefaultWeaponLevelUp", SpaceUtil.WorldMap.Center);
        }
    }
}
