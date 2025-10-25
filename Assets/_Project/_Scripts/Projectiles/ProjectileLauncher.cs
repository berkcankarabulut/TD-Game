using _Project._Scripts.Cores.Services;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Units.Damages;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{
    public class ProjectileLauncher
    {
        private readonly ProjectileTypeSO _projectileType;
        private readonly Transform _projectileSpawnPoint;
        private readonly ProjectilePool _projectilePool;

        public ProjectileLauncher(ProjectileTypeSO projectileType, Transform projectileSpawnPoint)
        {
            _projectilePool = ServiceLocator.Instance.Get<ProjectilePool>();
            _projectileType = projectileType;
            _projectileSpawnPoint = projectileSpawnPoint;
        } 
        
        public void LaunchProjectile(Unit target, Transform targetTransform, IUnitDamage damage, Unit source)
        {
            var projectileObj = _projectilePool?.SpawnFromPool(_projectileType, _projectileSpawnPoint);
            projectileObj?.Launch(target, targetTransform, damage, source);
        }
        
        public void LaunchProjectile(IUnitDamage damage, Unit source, float range)
        {
            var projectileObj = _projectilePool?.SpawnFromPool(_projectileType, _projectileSpawnPoint);

            if (projectileObj == null) return;
            projectileObj.Launch(null, null, damage, source);

            if (projectileObj is not LinearTriggerProjectile linearProjectile) return;
            linearProjectile.SetMaxDistance(range);
        } 
    }
}