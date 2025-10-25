using _Project._Scripts.Cores.Stats;
using _Project._Scripts.Cores.Units;

namespace _Project._Scripts.Cores.StatusEffects 
{
    [System.Serializable]
    public class StatTypeStatModifier
    {
        public UnitStatType StatType;
        public StatModifier StatModifier;
    }
}