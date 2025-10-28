using _Project._Scripts.Cores.Health;
using _Project._Scripts.Cores.Stats;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Defences.Projectile;
using _Project._Scripts.Projectiles;
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Defences
{
    public class DefenceUnit : Unit
    {   
        [Header("Stat Types")] [SerializeField]
        private UnitStatType _attackIntervalType; 
        [SerializeField] private UnitStatType _rangeType; 
        
        [Header("Components")]
        [SerializeField] private DirectionalProjectileLauncher _projectileLauncher; 
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private Animator _animator;
        
        //Stat Values
        private Stat<UnitStatType> _damageStat;
        private Stat<UnitStatType> _attackIntervalStat;
        private Stat<UnitStatType> _rangeStat;
        
        private DefenceStateMachine _stateMachine;
        public Animator Animator => _animator;
        public DefenceStateMachine StateMachine => _stateMachine;
        public ProjectileLauncher ProjectileLauncher => _projectileLauncher;

        public float DamageStat => _damageStat.TotalValue;
        public float AttackInterval => _attackIntervalStat.TotalValue;
        public float RangeStat => _rangeStat.TotalValue;

        public override void Initialize()
        {
            base.Initialize();
            StatsInitialize();
            InitializeAnimation();

            _healthBarView.Initialize(unitHealth);
            _projectileLauncher.Initialize(this);
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

        public void Destroy()
        { 
            Destroy(gameObject);
        }
    }
}