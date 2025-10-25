using System.Linq; 

namespace _Project._Scripts.Cores.Stats 
{
    [System.Serializable]
    public class LevelBaseStat<T> where T : StatTypeSO
    {
        public int Level;
        public BaseStat<T>[] BaseStats;

        public BaseStat<T> GetBaseStat(T statType)
        {
            return BaseStats.FirstOrDefault(baseStat => baseStat.HasSameStatType(statType));
        }

        public bool IsSameLevel(int level) => Level == level;
    }
}