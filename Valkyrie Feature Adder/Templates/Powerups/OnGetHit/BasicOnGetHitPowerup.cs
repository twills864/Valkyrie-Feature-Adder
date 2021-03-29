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
    public class BasicOnGetHitPowerup : OnGetHitPowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnGetHitBalance balance)
        {
            float chanceBase = balance.BasicOnGetHit.Chance.Base;
            float chanceIncrease = balance.BasicOnGetHit.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnGetHit.Power.Base;
            float powerIncrease = balance.BasicOnGetHit.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnGetHit()
        {
            GameManager.Instance.CreateFleetingText("[OnGetHit] BasicOnGetHit", SpaceUtil.WorldMap.Center);
        }
    }
}
