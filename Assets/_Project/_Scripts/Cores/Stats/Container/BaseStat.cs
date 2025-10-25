namespace _Project._Scripts.Cores.Stats
{
    [System.Serializable]
    public class BaseStat<T> where T : StatTypeSO
    {
        public T StatType;
        public float Value;

        public bool HasSameStatType(T statType) => StatType == statType;
    }
}