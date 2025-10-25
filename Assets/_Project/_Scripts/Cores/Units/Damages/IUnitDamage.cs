using _Project._Scripts.Cores.Damages;

namespace _Project._Scripts.Cores.Units.Damages
{
    public interface IUnitDamage
    {
        bool Resistable { get; }
        float Amount { get; }
        DamageTypeSO Type { get; }
        IUnit Source { get; }
    }
}