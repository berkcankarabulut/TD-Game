using UnityEngine;

namespace _Project._Scripts.Utilities
{
    public class CameraLooker : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _rotateX, _rotateY, _rotateZ;

        private Transform _cameraTransform;
        private Vector3 _initialRotation;

        private void Awake()
        {
            if (Camera.main == null)
            {
                Debug.LogError("There is no main camera");
                return;
            }

            _initialRotation = _target.localEulerAngles;
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            var lookRotation = Quaternion.LookRotation(
                (_cameraTransform.position - _target.position).normalized
            ).eulerAngles;

            _target.rotation = Quaternion.Euler(
                _rotateX ? lookRotation.x : _initialRotation.x,
                _rotateY ? lookRotation.y : _initialRotation.y,
                _rotateZ ? lookRotation.z : _initialRotation.z
            );
        }
    }
}