 

using UnityEngine;

namespace _Project._Scripts.Cores.StateMachines
{
    public class StateMachine
    {
        private IState _currentState;

        public void ChangeState(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }

        public void Tick(float deltaTime)
        { 
            _currentState?.Tick(deltaTime);
        }
    }
}