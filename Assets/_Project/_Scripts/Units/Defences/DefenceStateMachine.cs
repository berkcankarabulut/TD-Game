using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Units.Defence
{
    public class DefenceStateMachine : StateMachine
    {
        private DefenceUnit _defenceUnit;
        private AttackState _attackState;
        public DefenceUnit DefenceUnit => _defenceUnit;

        public DefenceStateMachine(DefenceUnit defenceUnit)
        {
            _defenceUnit = defenceUnit;
            _attackState = new AttackState(this);
            ChangeState(_attackState);
            _defenceUnit.OnDead += ChangeDeadState;
        }

        private void ChangeDeadState(Unit unit, GameObject killer)
        {
            _defenceUnit.OnDead -= ChangeDeadState;
            ChangeState(new DeadState(this, _defenceUnit.Destroy));
        }
    }
}