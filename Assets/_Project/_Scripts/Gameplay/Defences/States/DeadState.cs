using _Project._Scripts.Utilities.StateMachines;
using _Project._Scripts.Utilities;
using UnityEngine;

namespace _Project._Scripts.Defences
{
    public class DeadState : IState
    {
        private readonly DefenceStateMachine _stateMachine;

        private readonly Vector3 _initialLocalPos;
        private readonly Vector3 _targetLocalPos;
        private TimeCounter _timeCounter;

        private readonly float _sinkDuration = .5f;


        public DeadState(DefenceStateMachine stateMachine, System.Action onComplete)
        {
            _stateMachine = stateMachine;

            Vector3 localPosition = _stateMachine.DefenceUnit.transform.localPosition;
            _initialLocalPos = localPosition;
            _targetLocalPos = localPosition + Vector3.down * 3f;

            _timeCounter = new TimeCounter(_sinkDuration, false);
            _timeCounter.OnTimeReached += onComplete;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _timeCounter?.Dispose();
        }

        public void Tick(float deltaTime)
        {
            _timeCounter.Tick(deltaTime);
            float progress = Mathf.Clamp01(_timeCounter.GetElapsedTime() / _sinkDuration);
            _stateMachine.DefenceUnit.transform.localPosition =
                Vector3.Lerp(_initialLocalPos, _targetLocalPos, progress);
        }
    }
}