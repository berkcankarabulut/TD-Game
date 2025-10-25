using System.Collections.Generic;

namespace _Project._Scripts.Cores.StatusEffects
{
    [System.Serializable]
    public class StatusEffectLevelData
    {
        public int Level;
        public float DefaultDuration;
        public List<StatTypeStatModifier> StatTypeStatModifiers;
    }
}