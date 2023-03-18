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
        [field: SerializeField] public float currentHealth { get; private set; }


        public bool isAlive => currentHealth > 0;
        public event Action onHealthDepleted;
        public event Action onHealthChanged;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void UpdateHealth(float newHealth)
        {
            currentHealth = newHealth;
            onHealthChanged?.Invoke();
            CheckHealthDepletion();
        }

        private void CheckHealthDepletion()
        {
            if (!isAlive)
            {
                onHealthDepleted?.Invoke();
            }
        }

        public void TakeDamage(float damageAmount)
        {
            if (isAlive)
            {
                UpdateHealth(currentHealth - damageAmount);
            }
        }

    }
}