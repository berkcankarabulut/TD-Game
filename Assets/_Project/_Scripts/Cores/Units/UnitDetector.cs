using System.Collections.Generic;
using UnityEngine;

namespace _Project._Scripts.Cores.Units
{
    public class UnitDetector : MonoBehaviour
    { 
        [SerializeField] private Collider _collider;
        private List<Unit> _detectedUnits = new List<Unit>();
        
        private Unit _ownerUnit; 
        public System.Action<Unit> OnUnitDetected = _ => { };
        public System.Action<Unit> OnUnitRemoved = _ => { }; 
 
        public void Init(Unit owner)
        {
            _ownerUnit = owner;
            _collider.isTrigger = true; 
        } 
        
        private void OnEnable()
        {
            if (_ownerUnit == null) return;

            _ownerUnit.OnDead += RemoveDeadUnit;
        }

        private void OnDisable()
        {
            if (_ownerUnit == null) return;

            _ownerUnit.OnDead -= RemoveDeadUnit;
        }

        public List<Unit> GetDetectedUnits() => new List<Unit>(_detectedUnits);
        public List<Unit> GetAlliedUnits() => _detectedUnits.FindAll(unit => unit.TeamType == _ownerUnit.TeamType);
        public List<Unit> GetEnemyUnits() => _detectedUnits.FindAll(unit => unit.TeamType != _ownerUnit.TeamType);

        public Unit FindClosestEnemyUnit()
        {
            var enemyUnits = GetEnemyUnits();
            if (enemyUnits.Count == 0) return null;

            Unit closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in enemyUnits)
            {
                float distance = Vector3.Distance(_ownerUnit.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void OnTriggerEnter(Collider other)
        { 
            var unit = other.GetComponent<Unit>();
            if (unit == null || unit.UnitHealth.AmIDead || unit == _ownerUnit || _detectedUnits.Contains(unit)) return;   
            AddUnit(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit == null) return;
            RemoveUnit(unit);
        }

        private void RemoveDeadUnit(Unit unit, GameObject killer)
        {
            RemoveUnit(unit);
        }

        private void AddUnit(Unit unit)
        {
            _detectedUnits.Add(unit);
            unit.OnDead += RemoveDeadUnit;

            OnUnitDetected.Invoke(unit);
        }

        private void RemoveUnit(Unit unit)
        {
            _detectedUnits.Remove(unit);
            unit.OnDead -= RemoveDeadUnit;
            OnUnitRemoved.Invoke(unit);
        } 
    }
}