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
    public class BasicPassivePowerup : PassivePowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.PassiveBalance balance)
        {
            float chanceBase = balance.BasicPassive.Chance.Base;
            float chanceIncrease = balance.BasicPassive.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicPassive.Power.Base;
            float powerIncrease = balance.BasicPassive.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void RunFrame(float deltaTime, float realDeltaTime)
        {
            GameManager.Instance.CreateFleetingText("[Passive] BasicPassive", SpaceUtil.WorldMap.Center);
        }
    }
}
