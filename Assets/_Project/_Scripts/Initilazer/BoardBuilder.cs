using System.Collections.Generic;
using _Project._Scripts.Board;
using _Project._Scripts.Utilities.Commands; 
using _Project._Scripts.Managers;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace _Project._Scripts.Initilazer
{
    public class BoardBuilder : Command
    {
        private readonly Transform _boardContainer;
        private readonly BoardItem _boardPrefab;
        private readonly LevelManager _levelManager;
        private readonly float _animationDuration;
        private readonly float _delayBetweenTiles;
        private readonly float _startYPosition;
        
        private Dictionary<int, List<BoardItem>> _boardItems = new Dictionary<int, List<BoardItem>>();

        public Dictionary<int, List<BoardItem>> BoardItems => _boardItems;

        public BoardBuilder(
            Transform boardContainer,
            BoardItem boardPrefab,
            LevelManager levelManager,
            float animationDuration,
            float delayBetweenTiles,
            float startYPosition)
        {
            _boardContainer = boardContainer;
            _boardPrefab = boardPrefab;
            _levelManager = levelManager;
            _animationDuration = animationDuration;
            _delayBetweenTiles = delayBetweenTiles;
            _startYPosition = startYPosition;
        }

        public override async void StartCommand()
        {
            Vector2 boardSize = _levelManager.GetBoardSize();
            await SetupBoard((int)boardSize.x, (int)boardSize.y);
            CompleteCommand();
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

                    BoardItem tile = Object.Instantiate(_boardPrefab, _boardContainer); 
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

        public override void ResetCommand()
        {
            // Reset if needed
        }

        public override void Dispose()
        {
            base.Dispose();
            
            if (_boardItems != null)
            {
                foreach (var kvp in _boardItems)
                {
                    foreach (var item in kvp.Value)
                    {
                        if (item != null)
                        {
                            Object.Destroy(item.gameObject);
                        }
                    }
                    kvp.Value.Clear();
                }
                _boardItems.Clear();
                _boardItems = null;
            }
        }
    }
}