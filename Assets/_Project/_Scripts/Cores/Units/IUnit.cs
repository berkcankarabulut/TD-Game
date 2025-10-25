using _Project._Scripts.Cores.Teams;
using UnityEngine;

namespace _Project._Scripts.Cores.Units
{
    public interface IUnit
    {
        public GameObject GameObject { get; }
        public TeamTypeSO TeamType { get; }
    }
}