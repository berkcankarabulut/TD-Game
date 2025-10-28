using System;
using _Project._Scripts.Cores.Events;
using _Project._Scripts.Cores.Services;
using _Project._Scripts.Levels;
using SaveSystem.Runtime;
using UnityEngine;

namespace _Project._Scripts.Managers
{
    public class LevelManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private LevelSO[] _levels;
        private int _currentLevel = 0;

        [Header("Listening On..")] [SerializeField]
        private VoidChannelSO _onLevelCompleted;

        [Header("Broadcasting On...")] [SerializeField]
        private VoidChannelSO _onSaveRequested;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Unregister(this);
        }

        private void OnEnable()
        {
            _onLevelCompleted.onEventRaised += LevelComplete;
        }

        private void OnDisable()
        {
            _onLevelCompleted.onEventRaised -= LevelComplete;
        }

        private int GetCurrentLevelIndex()
        {
            return _currentLevel;
        }

        public UnitData<Units.Defence.DefenceUnit>[] GetLevelDefenceItems()
        {
            return _currentLevel > _levels.Length - 1 ? _levels[0].DefenceUnits : _levels[_currentLevel].DefenceUnits;
        }

        public UnitData<Units.Enemies.EnemyUnit>[] GetLevelEnemies()
        {
            return _currentLevel > _levels.Length - 1 ? _levels[0].EnemyUnits : _levels[_currentLevel].EnemyUnits;
        }

        public Vector2 GetBoardSize()
        {
            return _currentLevel > _levels.Length - 1 ? _levels[0].BoardSize : _levels[_currentLevel].BoardSize;
        }

        private void LevelComplete()
        {
            _currentLevel++;
            _onSaveRequested.RaiseEvent();
        }

        public object CaptureState()
        {
            return _currentLevel;
        }

        public void RestoreState(object state)
        {
            _currentLevel = (int)state;
        }
    }
}