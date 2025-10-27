using System.Collections.Generic;
using _Project._Scripts.Board;
using _Project._Scripts.Cores.Commands;
using _Project._Scripts.Levels;
using _Project._Scripts.Managers;
using _Project._Scripts.Units.Enemies;
using UnityEngine;

namespace _Project._Scripts.Initilazer
{
    public class EnemySpawnerBuilder : Command
    {
        private readonly EnemySpawner _enemySpawner;
        private readonly LevelManager _levelManager;
        private readonly BoardBuilder _boardBuilder;

        public EnemySpawnerBuilder(
            EnemySpawner enemySpawner,
            LevelManager levelManager,
            BoardBuilder boardBuilder)
        {
            _enemySpawner = enemySpawner;
            _levelManager = levelManager;
            _boardBuilder = boardBuilder;
        }

        public override void StartCommand()
        {
            SetupEnemies();
            CompleteCommand();
        }

        private void SetupEnemies()
        {
            UnitData<EnemyUnit>[] levelEnemies = _levelManager.GetLevelEnemies();
            float height = _levelManager.GetBoardSize().y;
            
            Dictionary<int, List<BoardItem>> boardItems = _boardBuilder.BoardItems;
            
            if (boardItems != null && boardItems.Count > 0)
            {
                int lastRowIndex = (int)height - 1;
                
                if (boardItems.ContainsKey(lastRowIndex))
                {
                    List<BoardItem> spawnBoards = boardItems[lastRowIndex];
                    _enemySpawner.Init(levelEnemies, spawnBoards);
                }
                else
                {
                    Debug.LogError($"Spawn row index {lastRowIndex} not found in board items");
                }
            }
            else
            {
                Debug.LogWarning("Board items not found or empty when setting up enemies");
            }
        }

        public override void ResetCommand()
        {
            // Reset if needed
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}