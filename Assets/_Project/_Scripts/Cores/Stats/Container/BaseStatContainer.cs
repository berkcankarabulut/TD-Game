using UnityEngine;

namespace _Project._Scripts.Cores.Stats
{
    public abstract class BaseStatContainer<T> : ScriptableObject where T : StatTypeSO
    {
        [SerializeField] private LevelBaseStat<T>[] _levelBaseStats;

        public BaseStat<T> GetBaseStatByLevelAndType(int level, T type)
        {
            var levelBaseStat = GetBaseStatsByLevel(level);
            return levelBaseStat.GetBaseStat(type);
        }

        public LevelBaseStat<T> GetBaseStatsByLevel(int level)
        {
            foreach (var levelBaseStat in _levelBaseStats)
            {
                if (!levelBaseStat.IsSameLevel(level)) continue;

                return levelBaseStat;
            }

            return _levelBaseStats[^1];
        }
    }
}