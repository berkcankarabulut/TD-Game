using System; 
using UnityEngine;

namespace _Project._Scripts.Utilities.Services
{
    [DefaultExecutionOrder(-500)]
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance;

        private ServiceManager _services;

        private void Awake()
        {
            int length = FindObjectsOfType<ServiceLocator>().Length;
            if (length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                _services = new ServiceManager();
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public ServiceLocator Register<T>(T service)
        {
            _services.Register(service);
            return this;
        }

        public ServiceLocator Register(Type type, object service)
        {
            _services.Register(type, service);
            return this;
        }
 
        public ServiceLocator Unregister<T>(T service)
        {
            _services.Unregister<T>(service);
            return this;
        }
 
        public ServiceLocator Unregister(Type type)
        {
            _services.Unregister(type);
            return this;
        } 
 
        public void ClearAllServices()
        {
            _services.Clear();
        } 

        public T Get<T>() where T : class
        {
            var type = typeof(T);
            T service = null;

            if (TryGetService<T>(out service)) return service;

            throw new ArgumentException($"Could not resolve type '{typeof(T).FullName}'.");
        }

        private bool TryGetService<T>(out T service) where T : class
        {
            return _services.TryGet(out service);
        }
        
        private bool TryGetService<T>(Type type, out T service) where T : class
        {
            return _services.TryGet(out service);
        } 
    }
}