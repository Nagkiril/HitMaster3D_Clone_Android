using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.UI;

namespace TestTask.Characters.Components
{
    public class CharacterHealth : MonoBehaviour
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [SerializeField] HealthBar barUI;

        public bool IsAlive => _currentHealth > 0;

        float _currentHealth;
        public event Action OnHealthDepleted;

        private void Awake()
        {
            _currentHealth = MaxHealth;
        }


        public void TakeDamage(float damageAmount)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damageAmount;
                if (barUI != null)
                    barUI.SetValue(_currentHealth / MaxHealth);
                CheckHealthDepletion();
            }
        }


        void CheckHealthDepletion()
        {
            if (_currentHealth <= 0)
            {
                if (barUI != null)
                    barUI.Hide();
                OnHealthDepleted?.Invoke();
            }
        }
    }
}