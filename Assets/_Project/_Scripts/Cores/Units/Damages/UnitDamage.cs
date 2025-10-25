using _Project._Scripts.Cores.Damages;

namespace _Project._Scripts.Cores.Units.Damages
{
    public class UnitDamage : IUnitDamage
    {
        public bool Resistable { get; }
        public float Amount { get; private set; }
        public DamageTypeSO Type { get; private set; }
        public IUnit Source { get; private set; }

        public UnitDamage(float amount, DamageTypeSO type, IUnit source, bool resistable = true)
        {
            Amount = amount;
            Type = type;
            Source = source;
            Resistable = resistable;
        }
    }
}