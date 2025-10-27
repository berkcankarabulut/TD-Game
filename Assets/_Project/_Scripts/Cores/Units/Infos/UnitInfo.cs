using UnityEngine;

namespace _Project._Scripts.Cores.Units
{
    public abstract class UnitInfo<T> : ScriptableObject
    {
        [SerializeField] private Sprite _icon; 
        [SerializeField] private T _unit;
        [SerializeField] private string _unitName; 
        public Sprite Icon => _icon; 
        public T Unit => _unit; 
        public string UnitName => _unitName;  

    }
}