using _Project._Scripts.Cores.Events;
using UnityEngine;

namespace _Project._Scripts.UI.GamePanel
{
    public class PanelUIManager : MonoBehaviour
    {
        private PanelUIStateMachine _stateMachine;

        [Header("Panels")] [SerializeField] private GameObject _gamePlay;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        [Header("Listening on")]
        [SerializeField] private VoidChannelSO _onLevelCompleted;
        [SerializeField] private VoidChannelSO _onEnemyReachedToPlayer;
        private void Awake()
        {
            _stateMachine = new PanelUIStateMachine(_gamePlay, _winPanel, _losePanel);
            ShowGamePlay();
        }

        private void OnEnable()
        {
            _onLevelCompleted.onEventRaised += ShowWin;
            _onEnemyReachedToPlayer.onEventRaised += ShowLose;
        }

        private void OnDisable()
        {
            _onLevelCompleted.onEventRaised -= ShowWin;
            _onEnemyReachedToPlayer.onEventRaised -= ShowLose;
        }

        public void ShowGamePlay()
        {
            _stateMachine.ChangeToGamePlay();
        }

        public void ShowWin()
        {
            _stateMachine.ChangeToWin();
        }

        public void ShowLose()
        {
            _stateMachine.ChangeToLose();
        }
    }
}