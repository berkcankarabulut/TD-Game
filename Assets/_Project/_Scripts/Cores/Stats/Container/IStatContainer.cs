namespace _Project._Scripts.Cores.Stats
{
    public interface IStatContainer<T> where T : StatTypeSO
    {
        Stat<T> GetStatByStatType(T statType);
        void AddModifierStats(BaseStat<T>[] baseStats, object source);
        void RemoveModifiersFromSource(object source);
    }
}