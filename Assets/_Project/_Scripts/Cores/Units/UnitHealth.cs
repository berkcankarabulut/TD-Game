using _Project._Scripts.Cores.Stats;
using UnityEngine;

namespace _Project._Scripts.Cores.Units
{
    [RequireComponent(typeof(UnitStatContainer))]
    public class UnitHealth : Health.Health, IStatListener
    {
        [SerializeField] private UnitStatType _totalHealthStatType; 
        public void Initialize()
        {
            var unitStatContainer = GetComponent<UnitStatContainer>();
            var healthStat = unitStatContainer.GetStatByStatType(_totalHealthStatType);
            healthStat.RegisterValueListener(this);
        }

        public void OnTotalValueChanged(StatTypeSO statType, float newTotalHealth)
        {
            if (statType.Id != _totalHealthStatType.Id) return;

            UpdateMaxHealth(newTotalHealth);
        }
    }
}