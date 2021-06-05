namespace Valkyrie_Feature_Adder
{
    public enum FeatureType
    {
        Bullet,
        Powerup,
        Enemy,
        Strategy
    }

    public enum BulletType
    {
        BulletWithFireStrategy,
        AdditionalBullet,
    }

    public enum PowerupType
    {
        OnFire,
        OnGetHit,
        OnHit,
        OnKill,
        OnLevelUp,
        Passive,

        OnDefaultWeaponFire,
        OnDefaultWeaponHit,
        OnDefaultWeaponKill,
        OnDefaultWeaponLevelUp
    }

    public enum EnemyType
    {
        LoopingVariantFireStrategyEnemy,
        CustomFireStrategyEnemy,
        NoFireStrategyEnemy
    }
}
