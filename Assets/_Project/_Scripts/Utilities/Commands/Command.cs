using System;

namespace _Project._Scripts.Utilities.Commands
{
    public interface ICommand : IDisposable
    {
        event Action<ICommand> OnCommandComplete;
        float Percentage { get; }
        
        void StartCommand();
        void CompleteCommand();
        void ResetCommand();
    }

    public abstract class Command : ICommand
    {
        public event Action<ICommand> OnCommandComplete;
        
        protected float percentage;
        public float Percentage => percentage;

        public abstract void StartCommand();

        public virtual void CompleteCommand()
        {
            percentage = 1f;
            OnCommandComplete?.Invoke(this);
        }

        public abstract void ResetCommand();

        public virtual void Dispose()
        {
            OnCommandComplete = null;
            ResetCommand();
        }
    }
}