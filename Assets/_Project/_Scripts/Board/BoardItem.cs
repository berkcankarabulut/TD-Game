using _Project._Scripts.Cores.Events;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Units.Defence;
using UnityEngine;

namespace _Project._Scripts.Board
{
    public class BoardItem : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _setableShower;
        private bool _defenceUnitSetable = false;
        private DefenceUnit _currentDefenceUnit;
        
        public bool IsDefenceUnitSetable => _defenceUnitSetable;

        [Header("Broadcasting")] 
        [SerializeField] private UnitEventChannelSO _onDefenceItemSetOnBoard;

        public void UnitSetable(bool status)
        {
            _setableShower.SetActive(status);
            _defenceUnitSetable = status;
        }

        public void SetUnit(DefenceUnit currentDefenceUnit)
        {
            _currentDefenceUnit = Instantiate(currentDefenceUnit, _spawnPoint);
            _currentDefenceUnit.Initialize();
            _currentDefenceUnit.OnDead += UnitOnDead;
            UnitSetable(false);
            _onDefenceItemSetOnBoard.RaiseEvent(currentDefenceUnit);
        }

        private void UnitOnDead(Unit unit, GameObject killer)
        {
            _currentDefenceUnit.OnDead -= UnitOnDead;
            UnitSetable(true);
            _currentDefenceUnit = null;
        }
    }
}