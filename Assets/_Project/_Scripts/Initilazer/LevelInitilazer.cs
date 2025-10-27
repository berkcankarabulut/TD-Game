using System.Collections.Generic;
using _Project._Scripts.Board;
using _Project._Scripts.Cores.Commands;
using _Project._Scripts.Cores.Services;
using _Project._Scripts.Managers;
using _Project._Scripts.Units.Defence;
using _Project._Scripts.Units.Enemies;
using UnityEngine; 

namespace _Project._Scripts.Initilazer
{
    public class LevelInitializer : MonoBehaviour
    { 
        [Header("UI References")]
        [SerializeField] private GameObject _defenceItemsUIContainer;
        [SerializeField] private DefenceUnitUI _defenceUnitUIPrefab;
        
        [Header("Level References")]
        [SerializeField] private Transform _playerLine;
        [SerializeField] private EnemySpawner _enemySpawner;
        
        [Header("Board References")]
        [SerializeField] private Transform _boardContainer;
        [SerializeField] private BoardItem _boardPrefab;
        
        [Header("Animation Settings")]  
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _delayBetweenTiles = 0.02f;
        [SerializeField] private float _startYPosition = -5f;

        private CommandExecuteHandler _commandHandler;
        private LevelManager _levelManager;

        private void Start()
        {
            _levelManager = ServiceLocator.Instance.Get<LevelManager>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            // 1. Board setup command - creates the game board
            var boardSetupCommand = new BoardBuilder(
                _boardContainer,
                _boardPrefab,
                _levelManager,
                _animationDuration,
                _delayBetweenTiles,
                _startYPosition
            );
 
            var playerLineSetupCommand = new PlayerBuilder(
                _playerLine,
                boardSetupCommand
            ); 
            var enemySpawnerSetupCommand = new EnemySpawnerBuilder(
                _enemySpawner,
                _levelManager,
                boardSetupCommand
            ); 
            var defenceItemUIBuilderCommand = new DefenceItemUIBuilder(
                _defenceUnitUIPrefab,
                _defenceItemsUIContainer,
                _levelManager
            ); 
            var commands = new List<ICommand>
            {
                boardSetupCommand,           
                playerLineSetupCommand,       
                enemySpawnerSetupCommand,    
                defenceItemUIBuilderCommand   
            };

            _commandHandler = new CommandExecuteHandler(commands);
            _commandHandler.OnAllCommandsExecuted += OnLevelInitializationComplete;
            _commandHandler.ExecuteCommands();
        }

        private void OnLevelInitializationComplete()
        {
            Debug.Log("Level initialization completed!"); 
        }

        private void OnDestroy()
        {
            _commandHandler?.Dispose();
            _commandHandler = null;
            _levelManager = null;
        }
    }
}