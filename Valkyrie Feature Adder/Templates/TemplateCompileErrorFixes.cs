using System;
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

    public class PlayerBullet
    {
        public virtual int Damage { get; }
        public virtual AudioClip FireSound { get; }
        protected virtual void OnPlayerBulletInit() { }
        protected virtual void OnPermanentVelocityBulletInit() { }
        protected virtual void OnActivate() { }
        public virtual void OnSpawn() { }
        protected virtual void OnPlayerBulletFrameRun(float deltaTime, float realDeltaTime) { }
    }

    public class DefaultBullet : PlayerBullet { }


    public class PermanentVelocityPlayerBullet : PlayerBullet { }

    public abstract class PlayerFireStrategy<T>
    {
        protected abstract float GetFireSpeedRatio(in PlayerFireStrategyManager.PlayerRatio ratios);
        public virtual PlayerBullet[] GetBullets(int weaponLevel, Vector3 playerFirePos) => null;
        public PlayerFireStrategy(BasicBullet bullet, PlayerFireStrategyManager manager) { }
    }

    #endregion Player

    #region Powerup

    public abstract class Powerup { /*public abstract void Init();*/ }


    #region Basic

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
        public abstract void OnHit(Enemy enemy, PlayerBullet bullet, Vector3 hitPosition);
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

    #endregion Basic


    #region Default Weapon

    public abstract class OnDefaultWeaponFirePowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnDefaultWeaponFireBalance balance);
        public abstract void OnFire(Vector3 position, DefaultBullet[] bullets);
    }

    public abstract class OnDefaultWeaponHitPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnDefaultWeaponHitBalance balance);
        public abstract void OnHit(Enemy enemy, DefaultBullet bullet, Vector3 hitPosition);
    }

    public abstract class OnDefaultWeaponKillPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnDefaultWeaponKillBalance balance);
        public abstract void OnKill(Enemy enemy, DefaultBullet bullet);
    }

    public abstract class OnDefaultWeaponLevelUpPowerup : Powerup
    {
        protected abstract void InitBalance(in PowerupBalanceManager.OnDefaultWeaponLevelUpBalance balance);
        public abstract void OnLevelUp();

    }

    #endregion Default Weapon

    #endregion Powerup

    #region Enemy

    public abstract class Enemy
    {
        //protected abstract EnemyFireStrategy InitialFireStrategy();
        public abstract AudioClip FireSound { get; }
        protected abstract void OnEnemyInit();
        //protected abstract void OnFireStrategyEnemyActivate();
        protected abstract void OnEnemySpawn();
        //protected abstract void OnFireStrategyEnemyFrame(float deltaTime, float realDeltaTime);

        public Transform transform;
    }

    public abstract class FireStrategyEnemy : Enemy
    {
        protected float FireSpeed;
        protected float FireSpeedVariance;

        public LoopingFrameTimer FireTimer;
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
        public struct OnFireBalance { public BasicTemplate BasicOnFire; }
        public struct OnGetHitBalance { public BasicTemplate BasicOnGetHit; }
        public struct OnHitBalance { public BasicTemplate BasicOnHit; }
        public struct OnKillBalance { public BasicTemplate BasicOnKill; }
        public struct OnLevelUpBalance { public BasicTemplate BasicOnLevelUp; }
        public struct PassiveBalance { public BasicTemplate BasicPassive; }

        public struct OnDefaultWeaponFireBalance { public BasicTemplate BasicOnDefaultWeaponFire; }
        public struct OnDefaultWeaponHitBalance { public BasicTemplate BasicOnDefaultWeaponHit; }
        public struct OnDefaultWeaponKillBalance { public BasicTemplate BasicOnDefaultWeaponKill; }
        public struct OnDefaultWeaponLevelUpBalance { public BasicTemplate BasicOnDefaultWeaponLevelUp; }

        public struct BasicTemplate { public Template Chance; public Template Power; }
        public struct Template { public float Base; public float Increase; }
    }

    public class PoolManager
    {
        public static PoolManager Instance => new PoolManager();
        public PoolManager EnemyBulletPool => this;
        public BasicEnemyBullet GetPrefab<T>() => new BasicEnemyBullet();
    }
    public class LoopingFrameTimer
    {
        public static LoopingFrameTimer Default() => new LoopingFrameTimer();
    }

    public static class SoundBank
    {
        public static AudioClip LaserGeneric => null;
        public static AudioClip GunPistol => null;
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

    public class Transform
    {
        public object position { get; set; }
    }

    public class AudioClip
    { }

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

namespace Assets.GameTasks { }

#endregion Namespaces