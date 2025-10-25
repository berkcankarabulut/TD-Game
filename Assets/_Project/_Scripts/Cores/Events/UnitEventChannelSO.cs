using System;
using _Project._Scripts.Cores.Units;
using UnityEngine;

namespace _Project._Scripts.Cores.Events
{
    [CreateAssetMenu(menuName = "BerkcanKarabulut/EventChannels/UnitEventChannelSO")]
    public class UnitEventChannelSO : ScriptableObject
    {
        public Action<Unit> onEventRaised;

        public void RaiseEvent(Unit defenceItem)
        {
            onEventRaised?.Invoke(defenceItem);
        }
    }
}