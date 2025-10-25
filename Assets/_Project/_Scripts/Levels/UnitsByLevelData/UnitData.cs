using System; 
using UnityEngine;

namespace _Project._Scripts.Levels
{
    [Serializable]
    public class UnitData<T>
    { 
        [SerializeField] UnitInfo<T> _unitInfo;
        [SerializeField] private int _unitCount; 
        public UnitInfo<T> UnitInfo => _unitInfo;
        public int UnitCount => _unitCount;
    }
}