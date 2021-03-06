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
    public class BasicOnKillPowerup : OnKillPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnKillBalance balance)
        {
            float chanceBase = balance.BasicOnKill.Chance.Base;
            float chanceIncrease = balance.BasicOnKill.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnKill.Power.Base;
            float powerIncrease = balance.BasicOnKill.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnKill(Enemy enemy, PlayerBullet bullet)
        {
            GameManager.Instance.CreateFleetingText("[OnKill] BasicOnKill", SpaceUtil.WorldMap.Center);
        }
    }
}
