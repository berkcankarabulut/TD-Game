using System.Collections.Generic;
using _Project._Scripts.Utilities.Services;
using GuidSystem.Runtime;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project._Scripts.Projectiles
{
    public class ProjectilePool : MonoBehaviour
    {
        [System.Serializable]
        public class PoolItem
        {
            public ProjectileTypeSO Type;
            public GameObject Prefab;
            public int DefaultCapacity = 10;
            public int MaxSize = 20;
        }

        public List<PoolItem> PoolItems;
        private Dictionary<SerializableGuid, ObjectPool<IPooledProjectile>> _poolDictionary;

        private void Awake()
        {
            _poolDictionary = new Dictionary<SerializableGuid, ObjectPool<IPooledProjectile>>();

            foreach (var item in PoolItems)
            {
                ObjectPool<IPooledProjectile> objectPool = new ObjectPool<IPooledProjectile>(
                    () => CreatePooledItem(item),
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    item.DefaultCapacity,
                    item.MaxSize
                );

                _poolDictionary.Add(item.Type.Id, objectPool);
            } 
        }

        private void OnEnable()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void OnDisable()
        {
            ServiceLocator.Instance.Unregister(this);
        }

        private IPooledProjectile CreatePooledItem(PoolItem item)
        {
            var obj = Instantiate(item.Prefab, transform);
            obj.SetActive(false);
            var pooledObject = obj.GetComponent<IPooledProjectile>();
            if (pooledObject != null)
            {
                pooledObject.Initialize();
                pooledObject.Pool = this;
                pooledObject.ProjectileType = item.Type;
            }

            return pooledObject;
        }

        private void OnTakeFromPool(IPooledProjectile obj)
        {
            obj.GameObject.SetActive(true);
        }

        private void OnReturnedToPool(IPooledProjectile obj)
        {
            obj.Transform.parent = transform;
            obj.Transform.localPosition = Vector3.zero;
            obj.GameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(IPooledProjectile obj)
        {
            Destroy(obj.GameObject);
        }

        public IPooledProjectile SpawnFromPool(ProjectileTypeSO type, Transform spawnPoint)
        {
            var pooledProjectile = SpawnFromPool(type);
            pooledProjectile.Transform.position = spawnPoint.position;

            return pooledProjectile;
        }

        public IPooledProjectile SpawnFromPool(ProjectileTypeSO type)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for projectile type " + type.name + " doesn't exist.");
                return null;
            }

            var obj = _poolDictionary[type.Id].Get();
            obj.OnObjectSpawn();

            return obj;
        }

        public void ReturnToPool(ProjectileTypeSO type, IPooledProjectile pooledProjectile)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for projectile type " + type.name + " doesn't exist.");
                return;
            }

            _poolDictionary[type.Id].Release(pooledProjectile);
        }
    }
}