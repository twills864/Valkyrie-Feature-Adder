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
    public class BasicOnLevelUpPowerup : OnLevelUpPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnLevelUpBalance balance)
        {
            float chanceBase = balance.BasicOnLevelUp.Chance.Base;
            float chanceIncrease = balance.BasicOnLevelUp.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnLevelUp.Power.Base;
            float powerIncrease = balance.BasicOnLevelUp.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnLevelUp()
        {
            GameManager.Instance.CreateFleetingText("[OnLevelUp] BasicOnLevelUp", SpaceUtil.WorldMap.Center);
        }
    }
}
