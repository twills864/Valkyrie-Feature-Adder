using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public enum EnemyType
    {
        LoopingVariantFireStrategyEnemy,
        CustomFireStrategyEnemy,
        NoFireStrategyEnemy
    }
}
