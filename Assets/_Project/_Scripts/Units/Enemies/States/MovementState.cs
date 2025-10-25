using _Project._Scripts.Cores.StateMachines;
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class MovementState : IState
    {
        private EnemyStateMachine _stateMachine;
        private float _movementSpeed;
        private Tweener _movementTween;
        private Transform _enemyTransform;
        
        public MovementState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _movementSpeed = stateMachine.EnemyUnit.MoveSpeed;
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
            Vector3 targetPosition = _enemyTransform.position + Vector3.back * _movementSpeed;
            
            _movementTween = _enemyTransform.DOMove(targetPosition, 1f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental)  
                .OnKill(() => _movementTween = null);
        }
    }
}