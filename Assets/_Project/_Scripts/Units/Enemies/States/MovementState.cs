using _Project._Scripts.Cores.StateMachines;
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class MovementState : IState
    {
        private EnemyStateMachine _stateMachine;
        private float _blocksPerSecond;   
        private Tweener _movementTween;
        private Transform _enemyTransform;
        
        public MovementState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _blocksPerSecond = stateMachine.EnemyUnit.MoveSpeed;  
            _enemyTransform = stateMachine.EnemyUnit.transform;
        }

        public void Enter()
        {
            StartMovement();
        }

        public void Exit()
        {
            _movementTween?.Kill();
        }

        public void Tick(float deltaTime)
        { 
        }

        private void StartMovement()
        {  
            float timePerBlock = 1f / _blocksPerSecond;
            
            Vector3 direction = Vector3.back;
            Vector3 moveDistance = direction;

            _movementTween = _enemyTransform.DOMove(
                    _enemyTransform.position + moveDistance,
                    timePerBlock
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}