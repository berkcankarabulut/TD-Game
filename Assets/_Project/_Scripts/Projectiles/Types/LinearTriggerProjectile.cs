using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Projectiles
{ 
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class LinearTriggerProjectile : ProjectileBase
    {
        [Header("Linear Movement Settings")]
        [SerializeField] private float _maxDistance = 20f;
        
        [Header("Optional: Manual Direction")]
        [SerializeField] private bool _useManualDirection = false;
        [SerializeField] private Vector3 _manualDirection = Vector3.forward;

        private Vector3 _direction;
        private Vector3 _startPosition;
        private float _traveledDistance;
        private Rigidbody _rigidbody;
        private Collider _collider;

        public override void Initialize()
        {
            base.Initialize();
            
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
                _rigidbody.useGravity = false;
            }
            
            if (_collider != null)
            {
                _collider.isTrigger = true;
            }
        }

        protected override void OnLaunch(Unit target, Transform targetTransform)
        {
            _startPosition = _myTransform.position;
            _traveledDistance = 0f;
 
            if (_useManualDirection)
            {
                _direction = _manualDirection.normalized;
            }
            else if (targetTransform != null)
            { 
                var targetPosition = targetTransform.position;
                _direction = targetPosition - _startPosition;
                if (!_calculateY) _direction.y = 0f;
                _direction = _direction.normalized;
            }
            else
            { 
                _direction = _myTransform.forward;
            }

            SetRotationTowards(_direction);
        }

        protected override void UpdateMovement()
        { 
            if (_traveledDistance >= _maxDistance)
            {
                ReturnToPool();
                return;
            }
 
            float distance = _speed * Time.deltaTime;
            _myTransform.position += _direction * distance;
            _traveledDistance += distance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hit) return;
 
            var hitUnit = other.GetComponent<Unit>();
             
            if (hitUnit == null) return;
 
            if (IsSameTeam(hitUnit))
            {
                return;  
            }
 
            DealDamageToUnit(hitUnit);
        }
 
        private bool IsSameTeam(Unit hitUnit)
        { 
            if (_source == null || hitUnit == null) return false;
 
            if (hitUnit.TeamType == null || _source.TeamType == null) return false; 
            return hitUnit.TeamType.Id == _source.TeamType.Id;
        }
 
        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
            if (!_calculateY) _direction.y = 0f;
            SetRotationTowards(_direction);
        } 
        
        public void SetMaxDistance(float maxDistance)
        {
            _maxDistance = maxDistance;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            if (_released) return;
 
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_myTransform.position, 0.2f);
 
            Gizmos.color = Color.red;
            Vector3 endPoint = _startPosition + _direction * _maxDistance;
            Gizmos.DrawLine(_startPosition, endPoint);
 
            Gizmos.color = Color.green;
            Vector3 remainingEndPoint = _myTransform.position + _direction * (_maxDistance - _traveledDistance);
            Gizmos.DrawLine(_myTransform.position, remainingEndPoint);
        }
#endif
    }
}