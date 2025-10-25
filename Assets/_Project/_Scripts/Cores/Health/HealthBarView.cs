using UnityEngine;
using UnityEngine.UI;

namespace _Project._Scripts.Cores.Health
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillDuration = 1f;

        private Health _health;
        private Vector3 _cameraPosition; 

        public void Initialize(Health health)
        {
            _health = health;
            _health.OnTakeDamage += HandleHealthChange;
            _health.OnHeal += HandleHealthChange;
            _health.OnDead += Dead;
            
            SetActive(true);
            HandleHealthChange(_health.CurrentHealth, _health.MaxHealth);
        }

        private void Dead(Health health, GameObject killer)
        {
            _health.OnTakeDamage -= HandleHealthChange;
            _health.OnHeal -= HandleHealthChange;
            _health.OnDead -= Dead;
            
            SetActive(false);
        }

        private void HandleHealthChange(float currentHealth, float amount, GameObject damageGiver)
        {
            HandleHealthChange(_health.CurrentHealth, _health.MaxHealth);
        }

        private void HandleHealthChange(float currentHealth, float maxHealth)
        {
            float fillAmount = currentHealth / maxHealth;
            _fillImage.fillAmount = fillAmount;  
        }

        private void SetActive(bool activate)
        { 
            gameObject.SetActive(activate);
        }
    }
}