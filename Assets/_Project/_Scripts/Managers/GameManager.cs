using _Project._Scripts.Cores.Events;
using SceneLoadSystem.Runtime;
using UnityEngine;

namespace Project._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [Header("Listening on")]
        [SerializeField] private VoidChannelSO _onGameSceneResetRequested;
        private void Start()
        {
            LoadGameScene();
        }

        private void OnEnable()
        {
            _onGameSceneResetRequested.onEventRaised += LoadGameScene;
        }

        private void OnDisable()
        {
            _onGameSceneResetRequested.onEventRaised -= LoadGameScene;
        }

        public void LoadGameScene()
        {
            _sceneLoader?.LoadScene();
        }
    }
}