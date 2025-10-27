using _Project._Scripts.Cores.Services;
using _Project._Scripts.Cores.Units.Damages;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{
    public abstract class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] protected ProjectileTypeSO projectileType;
        [SerializeField] protected Transform projectileSpawnPoint;
        protected ProjectilePool projectilePool;

        protected virtual void Awake()
        {
            projectilePool = ServiceLocator.Instance.Get<ProjectilePool>();
        }

        public abstract void LaunchProjectile(IUnitDamage damage);
    }
}