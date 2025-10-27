using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project._Scripts.Cores.Commands
{
    public class CommandExecuteHandler : IDisposable
    {
        private List<ICommand> _commands;
        private int _commandIndex;
        
        protected int CommandCount => _commands?.Count ?? 0;

        public float ExecutionProgress
        {
            get
            {
                if (!IsThereAnyAvailableCommandOnCurrentIndex()) return 1f;

                float completionRateForPerCommand = 1f / CommandCount;
                ICommand currentCommand = _commands[_commandIndex];

                float currentCompletedCommandPercentage = (float)_commandIndex / CommandCount;
                float currentCommandPercentageCompletionRate = currentCommand.Percentage * completionRateForPerCommand;

                return currentCompletedCommandPercentage + currentCommandPercentageCompletionRate;
            }
        }

        public event Action OnAllCommandsExecuted;

        public CommandExecuteHandler(List<ICommand> commands)
        {
            _commands = commands ?? new List<ICommand>();
        }

        public void ExecuteCommands()
        {
            _commandIndex = -1;
            ResetAllCommand(); 
            ExecuteNextCommand();
        }

        private void ResetAllCommand()
        {
            for(int i = 0; i < _commands.Count; i++) 
                _commands[i].ResetCommand();
        }

        private void ExecuteNextCommand()
        {
            _commandIndex++; 
            if (IsThereAnyAvailableCommandOnCurrentIndex())
            {
                ExecuteCommand();
            }
            else
            {
                OnAllCommandsExecuted?.Invoke();
            }
        }

        private void ExecuteCommand()
        {
            ICommand currentCommand = _commands[_commandIndex];
            currentCommand.OnCommandComplete += OnCommandCompleted;
            currentCommand.StartCommand();
        }

        private void OnCommandCompleted(ICommand command)
        { 
            bool checkedAndClosedConnectionWithCommand = CheckAndCloseConnectionWithCompletedCommand(command);
            if (!checkedAndClosedConnectionWithCommand) return;

            ExecuteNextCommand();
        }

        private bool CheckAndCloseConnectionWithCompletedCommand(ICommand command)
        {
            bool isCompletedAndCurrentCommandEqual = CheckCompletedAndCurrentCommandEquality(command);
            if (!isCompletedAndCurrentCommandEqual) return false;

            CloseConnectionWithCommand(command);
            return true;
        }

        private bool CheckCompletedAndCurrentCommandEquality(ICommand command)
        {
            ICommand currentCommand = _commands[_commandIndex];
            if (currentCommand != command)
            {
                Debug.LogError($"CURRENT COMMAND: {currentCommand.GetType().Name} NOT EQUAL TO COMPLETED COMMAND: {command.GetType().Name}");
                return false;
            }

            return true;
        }

        private void CloseConnectionWithCommand(ICommand command)
        {
            command.OnCommandComplete -= OnCommandCompleted;
        }

        private bool IsThereAnyAvailableCommandOnCurrentIndex() => _commandIndex < CommandCount;

        public void Dispose()
        {
            if (_commands != null)
            {
                foreach (var command in _commands)
                {
                    command?.Dispose();
                }
                _commands.Clear();
                _commands = null;
            }
            
            OnAllCommandsExecuted = null;
        }
    }
}