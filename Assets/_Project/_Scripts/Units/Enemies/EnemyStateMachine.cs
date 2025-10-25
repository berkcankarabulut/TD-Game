using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    [RequireComponent(typeof(EnemyUnit))]
    public class EnemyStateMachine: StateMachine
    {
        private EnemyUnit _enemyUnit; 
        private UnitDetector _unitDetector;
        public EnemyUnit EnemyUnit => _enemyUnit;
        public EnemyStateMachine(EnemyUnit enemyUnit)
        {
            _enemyUnit = enemyUnit;
            ChangeState(new MovementState(this));
            _enemyUnit.OnDead += DeadState;
        }

        private void DeadState(Unit arg1, GameObject arg2)
        {
            _enemyUnit.OnDead -= DeadState;
            ChangeState(new DeadState());
        }
    }
}