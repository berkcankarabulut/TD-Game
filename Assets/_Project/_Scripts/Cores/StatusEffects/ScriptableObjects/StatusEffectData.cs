using System.Collections.Generic; 
using GuidSystem.Runtime; 
using UnityEngine;

namespace _Project._Scripts.Cores.StatusEffects 
{
    public abstract class StatusEffectData : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string Name;
        public string Description;
        public Sprite Icon;
        public bool IsStackable;
        public bool IsPermanent;
        public List<StatusEffectLevelData> Levels = new List<StatusEffectLevelData>();

        public StatusEffectLevelData GetLevelData(int level)
        {
            return Levels.Find(l => l.Level == level) ?? Levels[0];
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Id")]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
#endif
    }
}