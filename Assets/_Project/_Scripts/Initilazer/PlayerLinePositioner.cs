using System.Collections.Generic;
using _Project._Scripts.Board;
using _Project._Scripts.Utilities.Commands;
using UnityEngine;

namespace _Project._Scripts.Initilazer
{
    public class PlayerLinePositioner : Command
    {
        private readonly Transform _playerLine;
        private readonly BoardBuilder _boardBuilder;

        public PlayerLinePositioner(
            Transform playerLine,
            BoardBuilder boardBuilder)
        {
            _playerLine = playerLine;
            _boardBuilder = boardBuilder;
        }

        public override void StartCommand()
        {
            PlayerPositionSetter();
            CompleteCommand();
        }

        private void PlayerPositionSetter()
        {
            Dictionary<int, List<BoardItem>> boardItems = _boardBuilder.BoardItems;
            
            if (boardItems != null && boardItems.Count > 0 && boardItems[0].Count > 0)
            {
                Vector3 spawnBoards = boardItems[0][0].transform.position;
                _playerLine.position = spawnBoards + Vector3.back;
            }
            else
            {
                Debug.LogWarning("Board items not found or empty when setting up player line");
            }
        }

        public override void ResetCommand()
        { 
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}