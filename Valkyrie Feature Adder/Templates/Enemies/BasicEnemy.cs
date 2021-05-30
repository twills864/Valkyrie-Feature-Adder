using System;
using System.Linq;
using Assets.Bullets.EnemyBullets;
using Assets.Constants;
using Assets.FireStrategies.EnemyFireStrategies;
using Assets.ObjectPooling;
using Assets.Util;
using UnityEngine;

namespace Assets.Enemies
{
    /// <summary>
    ///
    /// </summary>
    /// <inheritdoc/>
    public class BasicEnemy : Enemy
    {
        public override AudioClip FireSound => SoundBank.GunPistol;

        #region Prefabs

        [SerializeField]
        private float _Speed = GameConstants.PrefabNumber;

        #endregion Prefabs


        #region Prefab Properties

        public float Speed => _Speed;

        #endregion Prefab Properties


#if LoopingVariantFireStrategyEnemy
        protected override EnemyFireStrategy InitialFireStrategy()
            => new VariantLoopingEnemyFireStrategy<BasicEnemyBullet>(VariantFireSpeed);
#endif
#if CustomFireStrategyEnemy
        protected override EnemyFireStrategy InitialFireStrategy()
            => new BasicEnemyStrategy();
#endif
#if NoFireStrategyEnemy
        protected override EnemyFireStrategy InitialFireStrategy()
            => new InactiveEnemyStrategy();
#endif

        protected override void OnEnemyInit()
        {

        }

#if LoopingVariantFireStrategyEnemy
        protected override void OnFireStrategyEnemyActivate()
        {

        }
#endif
#if CustomFireStrategyEnemy
        protected override void OnEnemyActivate()
        {

        }
#endif
#if NoFireStrategyEnemy
        protected override void OnEnemyActivate()
        {

        }
#endif

        protected override void OnEnemySpawn()
        {
            transform.position = SpaceUtil.WorldMap.Center;
        }

#if LoopingVariantFireStrategyEnemy
        protected override void OnFireStrategyEnemyFrame(float deltaTime, float realDeltaTime)
        {

        }
#endif
#if CustomFireStrategyEnemy
        protected override void OnEnemyFrame(float deltaTime, float realDeltaTime)
        {

        }
#endif
#if NoFireStrategyEnemy
        protected override void OnEnemyFrame(float deltaTime, float realDeltaTime)
        {

        }
#endif
    }
}