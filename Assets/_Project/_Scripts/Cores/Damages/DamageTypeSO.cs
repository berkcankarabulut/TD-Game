using _Project._Scripts.Cores.Units;
using GuidSystem.Runtime;
using UnityEngine;

namespace _Project._Scripts.Cores.Damages
{
    [CreateAssetMenu(menuName = "BerkcanKarabulut/Stat/Damage Type")]
    public class DamageTypeSO : ScriptableObject
    {
        [SerializeField] private UnitStatType _damageType;
        [SerializeField] private UnitStatType _resistanceType;

        public UnitStatType DamageStatType => _damageType;
        public UnitStatType ResistanceStatType => _resistanceType;

        public DamageTypeInstance GetDamageTypeInstance()
        {
            return new DamageTypeInstance(_damageType.Id, _resistanceType.Id);
        }
    }

    public struct DamageTypeInstance
    {
        public readonly SerializableGuid DamageTypeId;
        public readonly SerializableGuid ResistanceTypeId;

        public DamageTypeInstance(SerializableGuid damageTypeId, SerializableGuid resistanceTypeId)
        {
            DamageTypeId = damageTypeId;
            ResistanceTypeId = resistanceTypeId;
        }
    }
}