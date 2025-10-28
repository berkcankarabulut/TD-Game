using _Project._Scripts.Board;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Cores.Events;
using _Project._Scripts.Defences;
using UnityEngine;

namespace _Project._Scripts.Player
{
    public class PlayerUnitSelection : MonoBehaviour
    {
        [SerializeField] private LayerMask _hitLayer;
        private DefenceUnit _selectedDefenceUnit;

        [Header("Broadcasting..")] [SerializeField]
        private UnitEventChannelSO _onDefenceItemFailedToSet;

        [Header("Listening On..")] [SerializeField]
        private UnitEventChannelSO _onDefenceItemSelected;

        private void OnEnable()
        {
            _onDefenceItemSelected.onEventRaised += DefenceItemSelected;
        }

        private void OnDisable()
        {
            _onDefenceItemSelected.onEventRaised -= DefenceItemSelected;
        }

        private void DefenceItemSelected(Unit unit)
        {
            _selectedDefenceUnit = (DefenceUnit)unit;
        }

        private void Update()
        {
            if (_selectedDefenceUnit == null) return; 
            if (!Input.GetMouseButtonDown(0)) return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _hitLayer)) return;
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject == null)
            {
                _onDefenceItemFailedToSet.RaiseEvent(_selectedDefenceUnit);
                _selectedDefenceUnit = null;
                return;
            }

            BoardItem boardItem = hitObject.GetComponent<BoardItem>();
            if (boardItem == null || !boardItem.IsDefenceUnitSetable)
            {
                _onDefenceItemFailedToSet.RaiseEvent(_selectedDefenceUnit);
                _selectedDefenceUnit = null;
                return;
            }

            boardItem.SetUnit(_selectedDefenceUnit);
            _selectedDefenceUnit = null;
        }
    }
}