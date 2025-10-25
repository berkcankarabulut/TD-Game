using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Cores.Utilty;

namespace _Project._Scripts.Units.Defence
{
    public class AttackState : IState
    {
        private readonly DefenceUnit _defenceUnit;
        private TimeCounter _timeCounter;

        public AttackState(DefenceStateMachine stateMachine)
        {
            _defenceUnit = stateMachine.DefenceUnit;
        }

        public void Enter()
        {
            _timeCounter = new TimeCounter(_defenceUnit.AttackInterval, true);
            _timeCounter.OnTimeReached += _defenceUnit.FireProjectileWithRange;
        }

        public void Exit()
        {
            _timeCounter.OnTimeReached -= _defenceUnit.FireProjectileWithRange;
            _timeCounter = null;
        }

        public void Tick(float deltaTime)
        {
            _timeCounter?.Tick(deltaTime);
        }
    }
}