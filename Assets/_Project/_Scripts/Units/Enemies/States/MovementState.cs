using _Project._Scripts.Cores.StateMachines;
using DG.Tweening;

namespace _Project._Scripts.Units.Enemies
{
    public class MovementState : IState
    {
        private EnemyStateMachine _stateMachine;
        private float _movementSpeed; 
        
        public MovementState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _movementSpeed = stateMachine.EnemyUnit.MoveSpeed; 
        }

        public void Enter()
        {
          
        }

        public void Exit()
        {
        }

        public void Tick(float deltaTime)
        { 
        }
    }
}