using _Project._Scripts.Cores.Units.Damages;
using _Project._Scripts.Projectiles;
using UnityEngine;

namespace _Project._Scripts.Units.Defence
{
    public class DirectionalProjectileLauncher : ProjectileLauncher
    {
        [Header("Fire Directions")]
        [SerializeField] private FireDirection _fireDirections = FireDirection.Forward; 
        private DefenceUnit _defenceUnit;
        
        public void Initialize(DefenceUnit defenceUnit)
        {
            _defenceUnit = defenceUnit;
        }
        
        public override void LaunchProjectile(IUnitDamage damage)
        { 
            if (HasDirection(FireDirection.Forward))
                LaunchInDirection(damage, Vector3.forward);

            if (HasDirection(FireDirection.Back))
                LaunchInDirection(damage, Vector3.back);

            if (HasDirection(FireDirection.Left))
                LaunchInDirection(damage, Vector3.left);

            if (HasDirection(FireDirection.Right))
                LaunchInDirection(damage, Vector3.right); 
        }

        private bool HasDirection(FireDirection direction)
        {
            return (_fireDirections & direction) == direction;
        }

        private void LaunchInDirection(IUnitDamage damage, Vector3 direction)
        {
            IPooledProjectile projectileObj = projectilePool?.SpawnFromPool(projectileType, projectileSpawnPoint);
            if (projectileObj == null) return;

            projectileObj.Launch(null, null, damage, damage.Source);

            if (projectileObj is not LinearProjectile linearProjectile) return;
            linearProjectile.SetMaxDistance(_defenceUnit.RangeStat);
            linearProjectile.SetDirection(direction);
        } 
    }
}