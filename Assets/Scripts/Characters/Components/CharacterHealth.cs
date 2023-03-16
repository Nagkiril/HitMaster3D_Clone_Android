using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.UI;

namespace TestTask.Characters.Components
{
    public class CharacterHealth : MonoBehaviour
    {
        [field: SerializeField] public float maxHealth { get; private set; }
        [SerializeField] private HealthBar _barUI;
        private float _currentHealth;


        public bool isAlive => _currentHealth > 0;
        public event Action onHealthDepleted;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }


        public void TakeDamage(float damageAmount)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damageAmount;
                if (_barUI != null)
                    _barUI.SetValue(_currentHealth / maxHealth);
                CheckHealthDepletion();
            }
        }


        void CheckHealthDepletion()
        {
            if (_currentHealth <= 0)
            {
                if (_barUI != null)
                    _barUI.Hide();
                onHealthDepleted?.Invoke();
            }
        }
    }
}