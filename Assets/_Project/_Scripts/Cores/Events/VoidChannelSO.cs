using System;
using UnityEngine;

namespace _Project._Scripts.Cores.Events
{
    [CreateAssetMenu(menuName = "BerkcanKarabulut/EventChannels/VoidChannelSO")]
    public class VoidChannelSO : ScriptableObject
    {
        public Action onEventRaised;

        public void RaiseEvent()
        {
            onEventRaised?.Invoke();
        }
    }
}