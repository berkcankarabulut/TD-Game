using UnityEngine;

namespace _Project._Scripts.Projectiles
{
    public interface IPooledObject
    {
        Transform Transform { get; }
        void OnObjectSpawn();
        void ReturnToPool();
    }
}