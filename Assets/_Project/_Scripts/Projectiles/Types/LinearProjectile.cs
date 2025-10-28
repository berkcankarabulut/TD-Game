using System;
using _Project._Scripts.Cores.Units;
using UnityEngine;
using DG.Tweening;

namespace _Project._Scripts.Projectiles
{
    [RequireComponent(typeof(Collider))]
    public class LinearProjectile : ProjectileBase
    {
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Collider _collider;
        [Header("Optional: Manual Direction")] [SerializeField]
        private bool _useManualDirection = false;

        [SerializeField] private Vector3 _manualDirection = Vector3.forward;

        private float _maxDistance = 20f;
        private Vector3 _direction;
        private Vector3 _startPosition;
        private float _traveledDistance; 
        private Vector3 _defaultScale;
        private Tweener _scaleTweener;

        public override void Initialize()
        {
            base.Initialize();
            _defaultScale = _myTransform.localScale;
            if (_collider != null) _collider.isTrigger = true;
        }

        protected override void OnLaunch(IUnit target, Transform targetTransform)
        {
            _startPosition = _myTransform.position;
            _traveledDistance = 0f;
            _trail.gameObject.SetActive(true);
            _myTransform.localScale = _defaultScale;
            _scaleTweener?.Kill();

            if (_useManualDirection)
                _direction = _manualDirection.normalized;
            else if (targetTransform != null)
            {
                _direction = targetTransform.position - _startPosition;
                if (!_calculateY) _direction.y = 0f;
                _direction = _direction.normalized;
            }
            else
                _direction = _myTransform.forward;

            SetRotationTowards(_direction);
            _scaleTweener = _myTransform.DOScale(_myTransform.localScale / 2, _maxDistance / _speed)
                .SetEase(Ease.Linear);
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
            if (hitUnit == null || IsSameTeam(hitUnit)) return;

            Instantiate(_particleSystem, hitUnit.Body.localPosition, Quaternion.identity);
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

        public void SetMaxDistance(float maxDistance) => _maxDistance = maxDistance;

        public override void ReturnToPool()
        {
            _scaleTweener?.Kill();
            _myTransform.localScale = _defaultScale;
            _trail.Clear();
            _trail.gameObject.SetActive(false);
            base.ReturnToPool();
        }

#if UNITY_EDITOR
        private void OnValidate()
        { 
            _collider = GetComponent<Collider>();
        }
#endif
    }
}