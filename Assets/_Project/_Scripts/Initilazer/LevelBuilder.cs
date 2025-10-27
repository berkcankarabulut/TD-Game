using System;
using System.Collections.Generic; 
using _Project._Scripts.Board;
using _Project._Scripts.Cores.Commands;
using _Project._Scripts.Cores.Services;
using _Project._Scripts.Levels;
using _Project._Scripts.Managers;
using _Project._Scripts.Units.Enemies;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Project._Scripts.Initilazer
{
    public class LevelBuilder : Command
    {
        [SerializeField] private Transform _enemyWinLine;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Transform _boardContainer;
        [SerializeField] private BoardItem _boardPrefab;

        [Header("Animation Settings")]  
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _delayBetweenTiles = 0.02f;
        [SerializeField] private float _startYPosition = -5f; 
        
        private Dictionary<int, List<BoardItem>> _boardItems = new Dictionary<int, List<BoardItem>>();
        private LevelManager _levelManager; 

        public override async void StartCommand()
        {
            _levelManager = ServiceLocator.Instance.Get<LevelManager>();
            Vector2 boardSize = _levelManager.GetBoardSize();
             
            await SetupBoard((int)boardSize.x, (int)boardSize.y);
            SetupEnemyWinLine();
            SetupEnemies(_levelManager.GetLevelEnemies(), _levelManager.GetBoardSize().y);
            
            CompleteCommand();
        }

        private void SetupEnemyWinLine()
        {
            Vector3 spawnBoards = _boardItems[0][0].transform.position;
            _enemyWinLine.transform.position = spawnBoards + Vector3.back;
        }

        private void SetupEnemies(UnitData<EnemyUnit>[] getLevelEnemies, float height)
        {
            List<BoardItem> spawnBoards = _boardItems[(int)height - 1];
            _enemySpawner.Init(getLevelEnemies, spawnBoards);
        }

        public override void ResetCommand()
        {
        }

        private async UniTask SetupBoard(int width, int height)
        {
            float xOffset = (width - 1) / 2.0f;
            Tween lastTween = null;

            for (int z = 0; z < height; z++)
            {
                List<BoardItem> boardItems = new List<BoardItem>();
                
                for (int x = 0; x < width; x++)
                {
                    Vector3 targetPosition = new Vector3((x - xOffset), 0, z);
                    Vector3 startPosition = new Vector3(targetPosition.x, _startYPosition, targetPosition.z);

                    BoardItem tile = Instantiate(_boardPrefab, _boardContainer); 
                    tile.UnitSetable(z < height / 2);
 
                    lastTween = tile.transform.DOMove(targetPosition, _animationDuration)
                        .From(startPosition)
                        .SetEase(Ease.OutBack);
                    
                    boardItems.Add(tile);

                    await UniTask.Delay((int)(_delayBetweenTiles * 1000));
                }

                _boardItems.Add(z, boardItems);
            }
 
            if (lastTween != null)
            {
                await lastTween.AsyncWaitForCompletion().AsUniTask();
            }
        }
    }
}