using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterHealth : MonoBehaviour
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        //Here we will add UI view for the health in due time...

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
                CheckHealthDepletion();
            }
        }


        void CheckHealthDepletion()
        {
            if (_currentHealth <= 0)
            {
                OnHealthDepleted?.Invoke();
            }
        }
    }
}