using _Project._Scripts.Cores.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._Scripts.UI
{
    public class LoadGameSceneButtonInteraction : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Header("Broadcasting..")] [SerializeField]
        private VoidChannelSO _onRequestLoadGameScene;

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadGameScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadGameScene);
        }

        private void LoadGameScene()
        {
            _onRequestLoadGameScene?.RaiseEvent();
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }
#endif
    }
}