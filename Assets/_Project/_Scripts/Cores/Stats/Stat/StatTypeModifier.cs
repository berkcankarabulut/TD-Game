namespace _Project._Scripts.Cores.Stats
{
    [System.Serializable]
    public class StatTypeModifier<T> where T : StatTypeSO
    {
        public T StatType;
        public StatModifier Modifier;
    }
}