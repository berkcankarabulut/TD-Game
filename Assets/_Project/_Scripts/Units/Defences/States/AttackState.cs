using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Cores.Units.Damages;
using _Project._Scripts.Utilty;
using UnityEngine;

namespace _Project._Scripts.Units.Defence
{  
    public class AttackState : IState
    {
        private DefenceUnit _defenceUnit;
        private TimeCounter _timeCounter;

        private static readonly int AttackHash = Animator.StringToHash("OnAttack");
        public AttackState(DefenceStateMachine stateMachine)
        {
            _defenceUnit = stateMachine.DefenceUnit;
        }

        public void Enter()
        {
            _timeCounter = new TimeCounter(_defenceUnit.AttackInterval, true);
            _timeCounter.OnTimeReached += DamageToTarget;
        }

        public void Exit()
        {
            _timeCounter.OnTimeReached -= DamageToTarget;
            _timeCounter = null;
        }

        private void DamageToTarget()
        {
            UnitDamage damage = new UnitDamage(_defenceUnit.DamageStat, _defenceUnit.UnitDamageType, _defenceUnit);
            _defenceUnit.ProjectileLauncher.LaunchProjectile(damage);
            _defenceUnit.Animator.SetTrigger(AttackHash);
        }

        public void Tick(float deltaTime)
        {
            _timeCounter?.Tick(deltaTime);
        }
    }
}