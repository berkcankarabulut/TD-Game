using _Project._Scripts.Cores.StateMachines;
using _Project._Scripts.Utilty;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class DeadState : IState
    {
        private readonly EnemyStateMachine _stateMachine;
        private readonly System.Action _onComplete;
        
        private readonly Vector3 _initialLocalPos;
        private readonly Vector3 _targetLocalPos;
        
        private TimeCounter _delayCounter;
        private TimeCounter _timeCounter;
        
        private bool _isSinking;
        
        private const float OnCompleteDelay = 2f;
        private const float SinkDuration = 1f;
        
        private static readonly int DeadHash = Animator.StringToHash("OnDead");

        public DeadState(EnemyStateMachine stateMachine, System.Action onComplete)
        {
            _stateMachine = stateMachine;
            _onComplete = onComplete;
            
            Vector3 localPosition = _stateMachine.EnemyUnit.transform.localPosition;
            _initialLocalPos = localPosition;
            _targetLocalPos = localPosition + Vector3.down * 3f;
            
            _delayCounter = new TimeCounter(OnCompleteDelay, false);
            _delayCounter.OnTimeReached += StartSinking;
            
            _timeCounter = new TimeCounter(SinkDuration, false);
            _timeCounter.OnTimeReached += OnSinkComplete;
        }

        public void Enter()
        {
            _stateMachine.EnemyUnit.Animator.SetTrigger(DeadHash);
            _isSinking = false;
        }

        public void Exit()
        {
            _delayCounter?.Dispose();
            _timeCounter?.Dispose();
        }

        public void Tick(float deltaTime)
        {
            if (!_isSinking)
            {
                _delayCounter.Tick(deltaTime);
            }
            else
            {
                _timeCounter.Tick(deltaTime);
                float progress = Mathf.Clamp01(_timeCounter.GetElapsedTime() / SinkDuration);
                _stateMachine.EnemyUnit.transform.localPosition =
                    Vector3.Lerp(_initialLocalPos, _targetLocalPos, progress);
            }
        }
        
        private void StartSinking()
        {
            _isSinking = true;
            _delayCounter.OnTimeReached += StartSinking;
        }
        
        private void OnSinkComplete()
        {
            _onComplete?.Invoke();
        }
    }
}