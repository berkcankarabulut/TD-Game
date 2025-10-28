namespace _Project._Scripts.Utilities.StateMachines
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick(float deltaTime);
    }
}