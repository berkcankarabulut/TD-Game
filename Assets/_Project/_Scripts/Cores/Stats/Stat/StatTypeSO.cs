using GuidSystem.Runtime;
using UnityEngine;

namespace _Project._Scripts.Cores.Stats
{
    public abstract class StatTypeSO : ScriptableObject
    {
        private SerializableGuid _id = SerializableGuid.NewGuid();

        public SerializableGuid Id => _id;

#if UNITY_EDITOR
        [ContextMenu("Reset ID")]
        private void AssignNewGuid()
        {
            _id = SerializableGuid.NewGuid();
            Debug.Log($"New ID:{_id.ToGuid()}");
        }
#endif
    }
}