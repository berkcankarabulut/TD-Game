using _Project._Scripts.Cores.Events;
using _Project._Scripts.Cores.Teams;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Player
{
    public class PlayerLine : MonoBehaviour
    {
        [SerializeField] private TeamTypeSO _playerTeam;
        private bool _isEnemyReached = false;

        [Header("Broadcasting")]
        [SerializeField] private VoidChannelSO _onEnemyReachedToPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (_isEnemyReached) return;

            if (TryGetEnemyUnit(other, out Unit enemyUnit))
            {
                HandleEnemyReached();
            }
        }

        private bool TryGetEnemyUnit(Collider collider, out Unit unit)
        {
            unit = collider.GetComponent<Unit>();

            if (unit == null) return false;
            if (unit.TeamType.Equals(_playerTeam)) return false;

            return true;
        }

        private void HandleEnemyReached()
        {
            _isEnemyReached = true;
            _onEnemyReachedToPlayer?.RaiseEvent();
        }
    }
}