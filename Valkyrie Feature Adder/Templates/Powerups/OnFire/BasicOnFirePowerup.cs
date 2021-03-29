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
    public class BasicOnFirePowerup : OnFirePowerup
    {
        private float Chance => ChanceCalculator.Value;
        private SumLevelValueCalculator ChanceCalculator { get; set; }

        private float PowerValue => PowerCalculator.Value;
        private SumLevelValueCalculator PowerCalculator { get; set; }

        protected override void InitBalance(in PowerupBalanceManager.OnFireBalance balance)
        {
            float chanceBase = balance.BasicOnFire.Chance.Base;
            float chanceIncrease = balance.BasicOnFire.Chance.Increase;
            ChanceCalculator = new SumLevelValueCalculator(chanceBase, chanceIncrease);

            float powerBase = balance.BasicOnFire.Power.Base;
            float powerIncrease = balance.BasicOnFire.Power.Increase;
            PowerCalculator = new SumLevelValueCalculator(powerBase, powerIncrease);
        }

        public override void OnFire(Vector3 position, PlayerBullet[] bullets)
        {
            GameManager.Instance.CreateFleetingText("[OnFire] BasicOnFire", SpaceUtil.WorldMap.Center);
        }
    }
}
