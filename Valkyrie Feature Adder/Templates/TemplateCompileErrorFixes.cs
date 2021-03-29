using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Bullets.EnemyBullets;
using Assets.Bullets.PlayerBullets;

// This class provides base classes for template classes
// to inherit from without throwing compiler errors.
// It also provides fixes for other common Unity features
// that don't exist in standard C#.
namespace Assets
{
    #region BaseClass

    public class BaseClass
    {
    }

    #endregion BaseClass

    #region Player

    public class PlayerBullet { }

    public abstract class PlayerFireStrategy<T>
    {
        protected abstract float GetFireSpeedRatio(in PlayerFireStrategyManager.PlayerRatio ratios);
        public PlayerFireStrategy(BasicBullet bullet, PlayerFireStrategyManager manager) { }
    }

    #endregion Player

    #region Powerup

    public abstract class Powerup { /*public abstract void Init();*/ }

    public abstract class OnFirePowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnFireBalance balance);
        public abstract void OnFire(Vector3 position, PlayerBullet[] bullets);
    }

    public abstract class OnGetHitPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnGetHitBalance balance);
        public abstract void OnGetHit();
    }

    public abstract class OnHitPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnHitBalance balance);
        public abstract void OnHit(Enemy enemy, PlayerBullet bullet);
    }

    public abstract class OnKillPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnKillBalance balance);
        public abstract void OnKill(Enemy enemy, PlayerBullet bullet);
    }

    public abstract class OnLevelUpPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnLevelUpBalance balance);
        public abstract void OnLevelUp();

    }

    public abstract class PassivePowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.PassiveBalance balance);
        public abstract void RunFrame(float deltaTime, float realDeltaTime);
    }

    #endregion Powerup

    #region Enemy

    public abstract class Enemy
    {
        protected float FireSpeed;
        protected float FireSpeedVariance;

        protected abstract EnemyFireStrategy InitialFireStrategy();
    }

    public class VariantLoopingEnemyFireStrategy<T> : EnemyFireStrategy<T>
    {
        public VariantLoopingEnemyFireStrategy(float f1, float f2)
            : base(null) { }
    }

    public class PermanentVelocityEnemyBullet { }

    public abstract class EnemyFireStrategy
    {
        public EnemyFireStrategy(BasicEnemyBullet bullet) { }
    }
    public abstract class EnemyFireStrategy<T> : EnemyFireStrategy
    {
        public EnemyFireStrategy(BasicEnemyBullet bullet) : base(bullet)
        {
        }
    }

    #endregion Enemy

    #region Valkyrie

    public class AsymptoteRatioLevelValueCalculator
    {
        public AsymptoteRatioLevelValueCalculator(float f1, float f2, float f3) { }
        public float Value => 0.0f;
    }

    public class SumLevelValueCalculator
    {
        public SumLevelValueCalculator(float f1, float f2) { }
        public float Value => 0.0f;
    }

    public struct PlayerFireStrategyManager
    {
        public struct PlayerRatio { public float Basic; }
    }

    public struct PowerupBalanceManager
    {
        public struct OnFireBalance { public BasicTemplate Basic; }
        public struct OnGetHitBalance { public BasicTemplate Basic; }
        public struct OnHitBalance { public BasicTemplate Basic; }
        public struct OnKillBalance { public BasicTemplate Basic; }
        public struct OnLevelUpBalance { public BasicTemplate Basic; }
        public struct PassiveBalance { public BasicTemplate Basic; }

        public struct BasicTemplate { public Template Chance; public Template Power; }
        public struct Template { public float Base; public float Increase; }
    }

    public class PoolManager
    {
        public static PoolManager Instance => new PoolManager();
        public PoolManager EnemyBulletPool => this;
        public BasicEnemyBullet GetPrefab<T>() => new BasicEnemyBullet();
    }

    #endregion Valkyrie


    #region Unity

    public class SerializeFieldAttribute : Attribute { }

    public class Vector3 { }

    public static class GameConstants
    {
        public const int PrefabNumber = 0;
    }

    public class GameManager
    {
        public static GameManager Instance => new GameManager();
        public void CreateFleetingText(string message, object position) { }
    }

    public class SpaceUtil
    {
        public static SpaceUtil WorldMap => new SpaceUtil();
        public object Center => new object();
    }

    #endregion Unity
}

#region Namespaces

namespace UnityEngine { }

namespace Assets.Util { }

namespace Assets.FireStrategyManagers { }

namespace Assets.Bullets.EnemyBullets { }

namespace Assets.ObjectPooling { }

namespace Assets.Powerups.Balance { }

namespace Assets.Constants { }

#endregion Namespaces