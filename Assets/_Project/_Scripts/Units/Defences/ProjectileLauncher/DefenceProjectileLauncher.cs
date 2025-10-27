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

            if (HasDirection(FireDirection.ForwardLeft))
                LaunchInDirection(damage, (Vector3.forward + Vector3.left).normalized);

            if (HasDirection(FireDirection.ForwardRight))
                LaunchInDirection(damage, (Vector3.forward + Vector3.right).normalized);

            if (HasDirection(FireDirection.BackLeft))
                LaunchInDirection(damage, (Vector3.back + Vector3.left).normalized);

            if (HasDirection(FireDirection.BackRight))
                LaunchInDirection(damage, (Vector3.back + Vector3.right).normalized);
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

            if (projectileObj is not LinearProjectile linearTriggerProjectile) return;
            linearTriggerProjectile.SetMaxDistance(_defenceUnit.RangeStat);
            linearTriggerProjectile.SetDirection(direction);
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (projectileSpawnPoint == null) return;

            float range = _defenceUnit != null ? _defenceUnit.RangeStat : 5f;
            Vector3 start = projectileSpawnPoint.position;

            // Her aktif yön için çizim yap
            if (HasDirection(FireDirection.Forward))
                DrawDirectionGizmo(start, Vector3.forward, range, Color.green);

            if (HasDirection(FireDirection.Back))
                DrawDirectionGizmo(start, Vector3.back, range, Color.red);

            if (HasDirection(FireDirection.Left))
                DrawDirectionGizmo(start, Vector3.left, range, Color.blue);

            if (HasDirection(FireDirection.Right))
                DrawDirectionGizmo(start, Vector3.right, range, Color.yellow);

            if (HasDirection(FireDirection.ForwardLeft))
                DrawDirectionGizmo(start, (Vector3.forward + Vector3.left).normalized, range, Color.cyan);

            if (HasDirection(FireDirection.ForwardRight))
                DrawDirectionGizmo(start, (Vector3.forward + Vector3.right).normalized, range, Color.magenta);

            if (HasDirection(FireDirection.BackLeft))
                DrawDirectionGizmo(start, (Vector3.back + Vector3.left).normalized, range, new Color(1f, 0.5f, 0f));

            if (HasDirection(FireDirection.BackRight))
                DrawDirectionGizmo(start, (Vector3.back + Vector3.right).normalized, range, new Color(0.5f, 0f, 1f));
        }

        private void DrawDirectionGizmo(Vector3 start, Vector3 direction, float range, Color color)
        {
            Gizmos.color = color;
            Vector3 end = start + direction * range;
            Gizmos.DrawLine(start, end);
            Gizmos.DrawSphere(end, 0.2f);

            // Ok başı çiz
            Vector3 arrowRight = Quaternion.Euler(0, 30, 0) * -direction * 0.5f;
            Vector3 arrowLeft = Quaternion.Euler(0, -30, 0) * -direction * 0.5f;
            Gizmos.DrawLine(end, end + arrowRight);
            Gizmos.DrawLine(end, end + arrowLeft);
        }
#endif
    }
}