using GuidSystem.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project._Scripts.Projectiles
{
    [CreateAssetMenu(fileName = "New Projectile Type", menuName = "BerkcanKarabulut/Projectile System/Projectile Type")]
    public class ProjectileTypeSO : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public float Speed = 10f;
        public float MaxDistance = 20f;

#if UNITY_EDITOR
        private void OnValidate()
        {
            Id = SerializableGuid.NewGuid();
        }
#endif
    }
}