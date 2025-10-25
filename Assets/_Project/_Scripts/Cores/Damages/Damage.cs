using _Project._Scripts.Cores.Units;

namespace _Project._Scripts.Cores.Damages
{
    public class Damage
    {
        public readonly bool Resistable;
        public readonly float Value;
        public readonly DamageTypeSO DamageType;
        public readonly Unit Source;

        public Damage(bool resistable, float value, DamageTypeSO damageType, Unit source)
        {
            Resistable = resistable;
            Value = value;
            DamageType = damageType;
            Source = source;
        }
    }
}