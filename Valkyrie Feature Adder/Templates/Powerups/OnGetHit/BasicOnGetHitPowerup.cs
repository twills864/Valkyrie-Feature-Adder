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
    public class BasicOnGetHitFirePowerup : OnGetHitPowerup
    {
        protected override void InitBalance(in PowerupBalanceManager.OnGetHitBalance balance)
        {
            float baseValue = balance.Basic.Base;
            float exponentBase = balance.Basic.Increase;
            float maxValue = balance.Basic.Max;

            ChanceModifierCalculator = new AsymptoteRatioLevelValueCalculator(baseValue, exponentBase, maxValue);
        }

        private float ChanceModifier => ChanceModifierCalculator.Value;
        private AsymptoteRatioLevelValueCalculator ChanceModifierCalculator { get; set; }

        public override void Init()
        {

        }

        public override void OnGetHit()
        {

        }
    }
}
