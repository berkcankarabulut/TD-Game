using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Cores.StatusEffects
{
    [CreateAssetMenu(fileName = "Freeze Status Effect",
        menuName = "BerkcanKarabulut/StatusEffects/Freeze Status Effect Data")]
    public class FreezeStatusEffectData : StatusEffectData
    {
        public UnitStatType FreezeResistanceStatType;
        public float FreezeDuration = 3f;
    }
}