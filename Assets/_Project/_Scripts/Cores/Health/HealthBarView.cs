using System;
using _Project._Scripts.Utilty;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._Scripts.Cores.Health
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _fillDuration = 1f;
 
        private Health _health;
        private Vector3 _cameraPosition;

        private void Awake()
        {
            BarActive(false);
        }

        public void Initialize(Health health)
        {
            _health = health;  
            HandleHealthChange(_health.CurrentHealth, _health.MaxHealth);
            _health.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(float currentHealth, float maxHealth, GameObject damageSource)
        {
            HandleHealthChange(_health.CurrentHealth, _health.MaxHealth);
            if (_health.AmIDead)
            {
                OnDead(); 
            }
            else
            {
                BarActive(true);
            }
        }

        private void OnDead()
        {
            _health.OnTakeDamage -= OnTakeDamage;
            BarActive(false);
        }

        private void HandleHealthChange(float currentHealth, float maxHealth)
        { 
            float fillAmount = currentHealth / maxHealth;
            _slider.value = fillAmount;
        }

        private void BarActive(bool status)
        {
            _slider.gameObject.SetActive(status);
        }
 
    }
}