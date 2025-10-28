using _Project._Scripts.Cores.Damages;
using _Project._Scripts.Cores.StatusEffects;
using _Project._Scripts.Cores.Teams;
using _Project._Scripts.Cores.Units.Damages; 
using UnityEngine; 

namespace _Project._Scripts.Cores.Units
{
    [RequireComponent(typeof(StatusEffectManager), typeof(UnitHealth), typeof(UnitStatContainer))]
    public abstract class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] protected Transform _body;
        [SerializeField] protected UnitHealth unitHealth;
        [SerializeField] protected UnitStatContainer unitStatContainer;
        [SerializeField] protected StatusEffectManager statusEffectManager;
 
        [Header("Type Datas")] 
        [SerializeField] protected TeamTypeSO teamType; 
        [SerializeField] protected DamageTypeSO unitDamageType;

        public DamageTypeSO UnitDamageType => unitDamageType;
        public UnitStatContainer UnitStatContainer => unitStatContainer;
        public UnitHealth UnitHealth => unitHealth;
        public GameObject GameObject => gameObject;
        public TeamTypeSO TeamType => teamType;
        public StatusEffectManager StatusEffectManager => statusEffectManager;

        public Transform Body => _body;

        public System.Action<Unit, GameObject> OnDead = (_, _) => { };
        public System.Action<Unit, float> OnTakeDamage = (_, _) => { };
        public System.Action<Unit, float> OnHeal = (_, _) => { };
        public System.Action<Unit> OnRevive = _ => { };


        public virtual void Initialize()
        {
            unitStatContainer.Initialize();
            unitHealth.Initialize(); 
            unitHealth.OnDead += OnImDead;
        }

        protected virtual void OnImDead(Health.Health myHealth, GameObject killer)
        {
            OnDead.Invoke(this, killer);
        }

        public virtual void TakeDamage(IUnitDamage damage)
        {
            float resistance = damage.Resistable ? GetDamageResistance(damage.Type) : 0f;
            float finalDamage = damage.Amount * (1 - resistance);

            unitHealth.TakeDamage(finalDamage, damage.Source.GameObject);
            OnTakeDamage.Invoke(this, finalDamage);
        }

        private float GetDamageResistance(DamageTypeSO unitDamageType)
        {
            var resistanceStat = unitStatContainer.GetStatByStatType(unitDamageType.ResistanceStatType);
            return resistanceStat != null ? resistanceStat.TotalValue / 100f : 0f;
        }

        public virtual void Heal(float heal, GameObject healer)
        {
            unitHealth.Heal(heal, healer);
            OnHeal.Invoke(this, heal);
        }

        public virtual void Revive()
        {
            if (!unitHealth.AmIDead) return;

            unitHealth.Revive(100f);
            OnRevive.Invoke(this);
        }

        public void SetLevel(int level)
        {
            UnitStatContainer.SetLevel(level);
        }

        public void Suicide()
        {
            if (unitHealth.AmIDead) return;

            var damage = new UnitDamage(unitHealth.CurrentHealth, unitDamageType, this, false);
            TakeDamage(damage);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (statusEffectManager == null) statusEffectManager = GetComponent<StatusEffectManager>();
            if (unitHealth == null) unitHealth = GetComponent<UnitHealth>();
            if (unitStatContainer == null) unitStatContainer = GetComponent<UnitStatContainer>();
        }
#endif
    }
}