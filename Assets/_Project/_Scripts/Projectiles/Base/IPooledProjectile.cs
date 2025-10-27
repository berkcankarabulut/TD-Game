using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Units.Damages;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{
    public interface IPooledProjectile : IPooledObject
    {
        void Initialize();
        void Launch(IUnit target, Transform targetTransform, IUnitDamage damage, IUnit source);
        Transform Transform { get; }
        GameObject GameObject { get; }
        ProjectilePool Pool { get; set; }
        ProjectileTypeSO ProjectileType { get; set; } 
    }
}