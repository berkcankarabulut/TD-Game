using _Project._Scripts.Utilities.StateMachines;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Enemies
{
    [RequireComponent(typeof(EnemyUnit))]
    public class EnemyStateMachine : StateMachine
    {
        private EnemyUnit _enemyUnit;
        private UnitDetector _unitDetector;
        public EnemyUnit EnemyUnit => _enemyUnit;

        public EnemyStateMachine(EnemyUnit enemyUnit, UnitDetector unitDetector)
        {
            _enemyUnit = enemyUnit;
            _unitDetector = unitDetector;
            _enemyUnit.OnDead += ChangeDeadState;
            _unitDetector.OnUnitDetected += ChangeAttackState;
            ChangeMoveState();
        }

        private void ChangeAttackState(Unit target)
        {
            ChangeState(new AttackState(this, target));
        }

        private void ChangeDeadState(Unit arg1, GameObject arg2)
        {
            _enemyUnit.OnDead -= ChangeDeadState;
            _unitDetector.OnUnitDetected -= ChangeAttackState;

            ChangeState(new DeadState(this, EnemyUnit.Destroy));
        }

        public void ChangeMoveState()
        {
            ChangeState(new MovementState(this));
        }
    }
}