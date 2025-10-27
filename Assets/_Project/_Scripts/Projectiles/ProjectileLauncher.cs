using _Project._Scripts.Cores.Units.Damages;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{
    public class ProjectileLauncher
    {
        private readonly ProjectileTypeSO _projectileType;
        private readonly Transform _projectileSpawnPoint;
        private readonly ProjectilePool _projectilePool;

        public ProjectileLauncher(ProjectileTypeSO projectileType, Transform projectileSpawnPoint,
            ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            _projectileType = projectileType;
            _projectileSpawnPoint = projectileSpawnPoint;
        }

        public void LaunchProjectile(IUnitDamage damage, float range)
        {
            IPooledProjectile projectileObj = _projectilePool?.SpawnFromPool(_projectileType, _projectileSpawnPoint);
            if (projectileObj == null) return;
            projectileObj.Launch(null, null, damage, damage.Source);
            if (projectileObj is LinearTriggerProjectile linearTriggerProjectile)
                linearTriggerProjectile.SetMaxDistance(range);
        }
    }
}