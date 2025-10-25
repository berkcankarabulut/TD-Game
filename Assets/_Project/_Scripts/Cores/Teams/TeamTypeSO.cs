using GuidSystem.Runtime;
using UnityEngine;

namespace _Project._Scripts.Cores.Teams
{
    [CreateAssetMenu(menuName = "BerkcanKarabulut/Team/TeamTypeSO")]
    public class TeamTypeSO : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string DisplayName;

#if UNITY_EDITOR
        [ContextMenu("Reset ID")]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
#endif
        
    }
}