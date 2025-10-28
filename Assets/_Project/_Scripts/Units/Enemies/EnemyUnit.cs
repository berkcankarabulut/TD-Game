using _Project._Scripts.Cores.Health;
using _Project._Scripts.Cores.Stats;
using _Project._Scripts.Cores.Units; 
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class EnemyUnit : Unit
    {
        [Header("Stat Types")]  
        [SerializeField] private UnitStatType _attackIntervalType; 
        [SerializeField] private UnitStatType _moveSpeedType;
         
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitDetector _unitDetector;
        [SerializeField] private HealthBarView _healthBarView;
 
        private Stat<UnitStatType> _damageStat;
        private Stat<UnitStatType> _attackIntervalStat;
        private Stat<UnitStatType> _moveSpeedStat;
         
        private EnemyStateMachine _stateMachine; 

        //Stat Values
        public float Damage => _damageStat.TotalValue;
        public float AttackInterval => _attackIntervalStat.TotalValue;
        public float MoveSpeed => _moveSpeedStat.TotalValue;
        public Animator Animator => _animator;

        public override void Initialize()
        {
            base.Initialize();
            _unitDetector.Init(this);
            StatsInitialize();
            _healthBarView.Initialize(unitHealth);
            _stateMachine = new EnemyStateMachine(this,_unitDetector);
        }

        private void Update()
        {
            _stateMachine?.Tick(Time.deltaTime);
        }

        private void StatsInitialize()
        { 
            _damageStat = unitStatContainer.GetStatByStatType(unitDamageType.DamageStatType);
            _attackIntervalStat = unitStatContainer.GetStatByStatType(_attackIntervalType);
            _moveSpeedStat = unitStatContainer.GetStatByStatType(_moveSpeedType);
        } 
        
        public virtual void Destroy()
        {
            Destroy(gameObject);
        } 
    }
}