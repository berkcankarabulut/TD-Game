using _Project._Scripts.Cores.Units.Damages;
using _Project._Scripts.Projectiles;
using UnityEngine;

namespace _Project._Scripts.Defences.Projectile
{
    public class DirectionalProjectileLauncher : ProjectileLauncher
    {
        [SerializeField] private ParticleSystem[] _launchParticles;
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
            TriggerLauchParticles();
        }

        private void TriggerLauchParticles()
        {
            for (int i = 0; i < _launchParticles.Length; i++)
            {
                _launchParticles[i].gameObject.SetActive(true);
                _launchParticles[i].Play();
            }
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