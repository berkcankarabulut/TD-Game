using _Project._Scripts.Units.Defence;
using _Project._Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project._Scripts.Levels
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "BerkcanKarabulut/Level/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private Vector2 _boardSize = new Vector2(4, 8);
        [SerializeField] private UnitData<DefenceUnit>[] _defenceUnits;
        [SerializeField] private UnitData<EnemyUnit>[] _enemyUnits;
        public Vector2 BoardSize => _boardSize;
        public UnitData<DefenceUnit>[] DefenceUnits => _defenceUnits;
        public UnitData<EnemyUnit>[] EnemyUnits => _enemyUnits;
    }
}