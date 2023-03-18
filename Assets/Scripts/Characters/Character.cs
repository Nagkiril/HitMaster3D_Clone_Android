using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;
using TestTask.Level;

namespace TestTask.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] protected CharacterMovement _movement;
        [SerializeField] protected CharacterVisuals _visuals;
        [SerializeField] protected CharacterInteractor _interactor;
        [SerializeField] protected CharacterHealth _health;


        public float maxHealth => _health.maxHealth;
        public float currentHealth => _health.currentHealth;

        public event Action onHealthChanged;
        public event Action onDeath;

        protected virtual void Awake()
        {
            _movement.onMovementStopped += OnMovementStop;
            _interactor.onCharacterTouched += OnCharacterTouch;
            _interactor.onWaypointTouched += OnWaypointTouch;
            _interactor.Initialize(this);
            _health.onHealthDepleted += OnHealthDepletion;
            _health.onHealthChanged += OnHealthChange;
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnCharacterTouch(Character other)
        {

        }

        protected virtual void OnWaypointTouch(Waypoint other)
        {

        }

        protected virtual void OnHealthDepletion()
        {
            Die();
        }

        protected virtual void OnHealthChange()
        {
            onHealthChanged?.Invoke();
        }

        protected virtual void OnMovementStop()
        {
            _visuals.SetMoving(false);
        }

        public virtual void Die()
        {
            _visuals.SetDeath();
            _movement.Disable();
            _interactor.Disable();
            onDeath?.Invoke();
        }

        public virtual void StartMoving(Vector3 targetPosition)
        {
            _movement.MoveToPosition(targetPosition);
            _visuals.SetMoving(true);
        }

        public virtual void Warp(Vector3 targetPosition)
        {
            _movement.WarpToPosition(targetPosition);
        }
        
        public virtual void TakeDamage(float damageAmount)
        {
            _health.TakeDamage(damageAmount);
        }

        public virtual void TakeDamage(float damageAmount, Vector3 hitVector, Transform hitTarget)
        {
            _health.TakeDamage(damageAmount);
        }
    }
}