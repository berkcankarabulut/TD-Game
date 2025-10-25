using _Project._Scripts.Cores.Health;
using _Project._Scripts.Cores.Stats;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private EnemyStateMachine _stateMachine; 
        [Header("Stat Types")]  
        [SerializeField] private UnitStatType _attackIntervalType; 
        [SerializeField] private UnitStatType _moveSpeedType;
        
        //Stat Values
        private Stat<UnitStatType> _damageStat;
        private Stat<UnitStatType> _attackIntervalStat;
        private Stat<UnitStatType> _moveSpeedStat;
        [Header("UI")] 
        [SerializeField] private HealthBarView _healthBarView;
        public float MoveSpeed { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            StatsInitialize();
            _stateMachine = new EnemyStateMachine(this);
            _healthBarView.Initialize(unitHealth);
        }
        
        private void StatsInitialize()
        { 
            _damageStat = unitStatContainer.GetStatByStatType(unitDamageType.DamageStatType);
            _attackIntervalStat = unitStatContainer.GetStatByStatType(_attackIntervalType);
            _moveSpeedStat = unitStatContainer.GetStatByStatType(_moveSpeedType);
        }
    }
}