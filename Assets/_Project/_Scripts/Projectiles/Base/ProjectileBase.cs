using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Units.Damages;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{ 
    public abstract class ProjectileBase : MonoBehaviour, IPooledProjectile
    {
        [SerializeField] protected float _speed = 10f;
        [SerializeField] protected bool _calculateY = true;

        protected IUnitDamage _damage;
        protected Unit _source;

        protected Transform _myTransform;
        protected GameObject _myGameObject;

        public Transform Transform => _myTransform;
        public GameObject GameObject => _myGameObject;
        public ProjectilePool Pool { get; set; }
        public Unit Source => _source;
        public IUnitDamage Damage => _damage;

        public ProjectileTypeSO ProjectileType { get; set; }

        protected bool _released;
        protected bool _hit;

        public virtual void Initialize()
        {
            _myTransform = transform;
            _myGameObject = gameObject;
        }
 
        public virtual void Launch(Unit target, Transform targetTransform, IUnitDamage damage, Unit source)
        {
            _damage = damage;
            _source = source;
            _released = false;
            _hit = false;

            OnLaunch(target, targetTransform);
        }
 
        protected abstract void OnLaunch(Unit target, Transform targetTransform);

        protected void SetRotationTowards(Vector3 direction)
        {
            if (!_calculateY) direction.y = 0f;
            direction = direction.normalized;

            float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        protected virtual void Update()
        {
            if (_hit) return;

            UpdateMovement();
        }

        protected abstract void UpdateMovement();
 
        protected virtual void DealDamageToUnit(Unit unit)
        {
            if (_hit) return;
            if (unit == null) return;

            _hit = true;
            unit.TakeDamage(_damage);
            ReturnToPool();
        }

        public void OnObjectSpawn()
        {
        }

        public virtual void ReturnToPool()
        {
            if (_released) return;

            if (Pool != null && ProjectileType != null)
            {
                Pool.ReturnToPool(ProjectileType, this);
                _released = true;
            }
        }
    }
}