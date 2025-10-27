using _Project._Scripts.Cores.Health;
using _Project._Scripts.Cores.Services;
using _Project._Scripts.Cores.Stats;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Units.Damages;
using _Project._Scripts.Projectiles;
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Units.Defence
{
    public class DefenceUnit : Unit
    {
        [Header("Projectile Settings")]
        [SerializeField] private ProjectileTypeSO _projectileType; 
        [SerializeField] private Transform _projectileSpawnPoint;

        [Header("Stat Types")] 
        [SerializeField] private UnitStatType _attackIntervalType; 
        [SerializeField] private UnitStatType _rangeType; 
        
        [Header("UI")] 
        [SerializeField] private HealthBarView _healthBarView;

        //Stat Values
        private Stat<UnitStatType> _damageStat;
        private Stat<UnitStatType> _attackIntervalStat;
        private Stat<UnitStatType> _rangeStat;

        private Projectiles.ProjectileLauncher _projectileLauncher;
        private DefenceStateMachine _stateMachine;

        public DefenceStateMachine StateMachine => _stateMachine;
        public Projectiles.ProjectileLauncher ProjectileLauncher => _projectileLauncher;

        public float DamageStat => _damageStat.TotalValue;
        public float AttackInterval => _attackIntervalStat.TotalValue;
        public float RangeStat => _rangeStat.TotalValue;

        public override void Initialize()
        {
            base.Initialize();
            StatsInitialize();
            InitializeAnimation(); 

            _healthBarView.Initialize(unitHealth);
            _projectileLauncher = new Projectiles.ProjectileLauncher(_projectileType, _projectileSpawnPoint, ServiceLocator.Instance.Get<ProjectilePool>());
            _stateMachine = new DefenceStateMachine(this);
        }

        private void InitializeAnimation()
        {
            transform.DOScale(transform.localScale, .2f)
                .From(Vector3.zero)
                .SetEase(Ease.OutBack);
        }

        private void StatsInitialize()
        {
            _damageStat = unitStatContainer.GetStatByStatType(unitDamageType.DamageStatType);
            _attackIntervalStat = unitStatContainer.GetStatByStatType(_attackIntervalType);
            _rangeStat = unitStatContainer.GetStatByStatType(_rangeType);
        }

        private void Update()
        {
            _stateMachine?.Tick(Time.deltaTime);
        } 
        
        public override void TakeDamage(IUnitDamage damage)
        {
            base.TakeDamage(damage);
            _healthBarView.HandleHealthChange(unitHealth.CurrentHealth, unitHealth.MaxHealth);
        } 
    }
}