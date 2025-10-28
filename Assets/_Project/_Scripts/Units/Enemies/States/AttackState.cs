using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Units.Damages;
using _Project._Scripts.Utilty;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{  
    public class AttackState : IState
    {
        private EnemyStateMachine _stateMachine;
        private TimeCounter _timeCounter; 
        private Unit _target;

        private static readonly int AttackHash = Animator.StringToHash("OnAttack");
        public AttackState(EnemyStateMachine enemyStateMachine, Unit target)
        {
            _stateMachine = enemyStateMachine;
            _target = target;
        }

        public void Enter()
        {
            _timeCounter = new TimeCounter(_stateMachine.EnemyUnit.AttackInterval, true);
            _timeCounter.OnTimeReached += AttackToTarget;
        }

        public void Exit()
        {
            _timeCounter.OnTimeReached -= AttackToTarget;
            _timeCounter = null;
        }

        private void AttackToTarget()
        {
            _stateMachine.EnemyUnit.Animator.SetTrigger(AttackHash);
            DamageToTarget(_target);
        }

        public void Tick(float deltaTime)
        {
            if (_target.UnitHealth.AmIDead)
            {
                _stateMachine.ChangeMoveState();
            }

            _timeCounter?.Tick(deltaTime);
        }

        public void DamageToTarget(Unit target)
        {
            UnitDamage damage = new UnitDamage(_stateMachine.EnemyUnit.Damage, _stateMachine.EnemyUnit.UnitDamageType, _stateMachine.EnemyUnit);
            target.TakeDamage(damage);
        }
    }
}