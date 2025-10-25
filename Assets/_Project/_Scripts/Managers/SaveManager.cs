using _Project._Scripts.Cores.Events;
using UnityEngine;
using SaveSystem.Runtime;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string _saveFileName = "GameSave";
    [SerializeField] private SavingSystem _saveSystem; 
    [Header("Listening On...")]
    [SerializeField] private VoidChannelSO _onSaveRequested; 
    private void OnEnable()
    {
        _onSaveRequested.onEventRaised += Save;
    }

    private void OnDisable()
    {
        _onSaveRequested.onEventRaised -= Save;
    }

    private void Save()
    {
        _saveSystem.Save(_saveFileName);
    }
}