namespace _Project._Scripts.Cores.StateMachines
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick(float deltaTime);
    }
}