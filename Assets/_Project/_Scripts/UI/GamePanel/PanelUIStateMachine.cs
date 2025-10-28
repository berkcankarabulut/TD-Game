using _Project._Scripts.Utilities.StateMachines;
using UnityEngine;

namespace _Project._Scripts.UI.GamePanel
{
    public class PanelUIStateMachine : StateMachine
    {
        private GameplayState _gameplayState;
        private WinState _winState;
        private LoseState _loseState;

        public PanelUIStateMachine(GameObject gamePlay, GameObject win, GameObject lose)
        {
            _gameplayState = new GameplayState(gamePlay);
            _winState = new WinState(win);
            _loseState = new LoseState(lose);
        }

        public void ChangeToGamePlay()
        {
            ChangeState(_gameplayState);
        }

        public void ChangeToWin()
        {
            ChangeState(_winState);
        }

        public void ChangeToLose()
        {
            ChangeState(_loseState);
        }
    }
}