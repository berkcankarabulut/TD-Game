using _Project._Scripts.Cores.Events;
using SceneLoadSystem.Runtime;
using UnityEngine;

namespace _Project._Scripts.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private SceneLoader _gameSceneLoader;
        [Header("Listening on")]
        [SerializeField] private VoidChannelSO _onRequestLoadGameScene;
        private void Start()
        {
            LoadGameScene();
        }

        private void OnEnable()
        {
            _onRequestLoadGameScene.onEventRaised += LoadGameScene;
        }

        private void OnDisable()
        {
            _onRequestLoadGameScene.onEventRaised -= LoadGameScene;
        }

        private void LoadGameScene()
        {
            _gameSceneLoader?.LoadScene();
        }
    }
}