using System.Collections.Generic;
using _Project._Scripts.Board;
using _Project._Scripts.Cores.Utilty;
using _Project._Scripts.Levels;
using UnityEngine;

namespace _Project._Scripts.Units.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay = .5f;
        [SerializeField] private Vector3 _spawnOffset = new Vector3(0, 2, 2);
        [SerializeField] private float _spaceBetweenEnemies = 2f;

        private List<EnemyUnit> _enemyPool = new List<EnemyUnit>();
        private TimeCounter _timeCounter;
        private List<Vector3> _spawnPoints;
        private bool _isActive = false;
        private float _timer = 0;

        public void Init(UnitData<EnemyUnit>[] enemies, List<BoardItem> spawnBoards)
        {
            CreateEnemyPool(enemies);
            CreateSpawnPoints(spawnBoards);
            SetupSpawnTimer();
        }

        private void SetupSpawnTimer()
        {
            _isActive = true;
            _timeCounter = new TimeCounter(_spawnDelay, true);
            _timeCounter.OnTimeReached += Spawn;
        }

        private void CreateEnemyPool(UnitData<EnemyUnit>[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                for (int y = 0; y < enemies[i].UnitCount; y++)
                {
                    _enemyPool.Add(enemies[i].UnitInfo.Unit);
                }
            }
        }

        private void CreateSpawnPoints(List<BoardItem> spawnBoards)
        {
            _spawnPoints = new List<Vector3>();
            for (int i = 0; i < spawnBoards.Count; i++)
            {
                Vector3 spawnPoint = spawnBoards[i].transform.position + _spawnOffset;
                _spawnPoints.Add(spawnPoint);
            }
        }

        private void Update()
        {
            if (!_isActive) return;
            _timeCounter?.Tick(Time.deltaTime);
        }

        private void Spawn()
        {
            int randomSpawnIndex = Random.Range(0, _spawnPoints.Count);
            int randomEnemyIndex = Random.Range(0, _enemyPool.Count);
            
            EnemyUnit enemyUnit = _enemyPool[randomEnemyIndex];
            EnemyUnit spawnUnit = Instantiate(enemyUnit, _spawnPoints[randomSpawnIndex], Quaternion.identity);
            spawnUnit.Initialize();
            
            _enemyPool.RemoveAt(randomEnemyIndex);
            HandleSpawnCompletion();
        }


        private void HandleSpawnCompletion()
        {
            if (_enemyPool.Count != 0) return;
            _isActive = false;
            _timeCounter.Dispose();
            _timeCounter = null;
        }
    }
}