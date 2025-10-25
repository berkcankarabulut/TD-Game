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
            HandleHealthChange(_health.CurrentHealth, _health.MaxHealth);
        }  
        
        public void HandleHealthChange(float currentHealth, float maxHealth)
        {
            float fillAmount = currentHealth / maxHealth;
            _fillImage.fillAmount = fillAmount;
            bool isDead = currentHealth == 0;
            SetActive(!isDead);
        }

        private void SetActive(bool activate)
        { 
            gameObject.SetActive(activate);
        }
    }
}