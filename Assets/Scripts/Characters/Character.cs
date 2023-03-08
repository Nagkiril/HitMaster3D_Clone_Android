using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;

namespace TestTask.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterMovement movement;
        [SerializeField] CharacterVisuals visuals;
        [SerializeField] CharacterInteractor interactor;
        [SerializeField] CharacterHealth health;


        public float MaxHealth => health.MaxHealth;
        public event Action OnDeath;

        protected virtual void Awake()
        {
            movement.OnMovementStopped += OnMovementStop;
            interactor.OnCharacterTouched += OnCharacterTouch;
            interactor.Initialize(this);
            health.OnHealthDepleted += OnHealthDepletion;
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnCharacterTouch(Character other)
        {

        }

        protected virtual void OnHealthDepletion()
        {
            Die();
        }

        protected virtual void OnMovementStop()
        {
            visuals.SetMoving(false);
        }

        public virtual void Die()
        {
            OnDeath?.Invoke();
        }

        public virtual void StartMoving(Vector3 targetPosition)
        {
            movement.MoveToPosition(targetPosition);
            visuals.SetMoving(true);
        }

        public virtual void Warp(Vector3 targetPosition)
        {
            movement.WarpToPosition(targetPosition);
        }
        
        public void TakeDamage(float damageAmount)
        {
            health.TakeDamage(damageAmount);
        }
    }
}